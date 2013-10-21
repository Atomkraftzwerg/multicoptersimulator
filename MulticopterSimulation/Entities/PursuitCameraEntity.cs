using Microsoft.Ccr.Core;
using Microsoft.Dss.Core.Attributes;
using Microsoft.Robotics.Simulation.Engine;
using Microsoft.Robotics.Simulation.Physics;
using MulticopterSimulation.Algorithms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

using xna = Microsoft.Xna.Framework;

namespace MulticopterSimulation.Entities
{
    /// <summary>
    /// PursuitCameraEntity is a camera entity that follows a target entity around
    /// </summary>
    [DataContract]
    public class PursuitCameraEntity : CameraEntity
    {
        public float altitudeChange = 0.0f;
        public float zoomChange = 0.0f;
        public float rotationChange = 0.0f; // TODO Rotation change
        public float angleChange = 0.0f;

        /// <summary>
        /// Get / set property that specifies the target entity to follow
        /// </summary>
        public VisualEntity Target { get; set; }

        string _targetName;
        /// <summary>
        /// Name of the entity to track
        /// </summary>
        [DataMember, Browsable(true)]
        public string TargetName
        {
            get
            {
                return _targetName;
            }
            set
            {
                if (value != _targetName)
                {
                    _targetName = value;
                    FindTarget();
                }
            }
        }

        /// <summary>
        /// Minimum distance to keep the camera from the entity
        /// </summary>
        [DataMember, Browsable(true)]
        public float MinDistance { get; set; }

        /// <summary>
        /// Maximum distance to keep the camera from the entity
        /// </summary>
        [DataMember, Browsable(true)]
        public float MaxDistance { get; set; }

        /// <summary>
        /// Height above the ground plane to keep the camera
        /// </summary>
        [DataMember, Browsable(true)]
        public float Altitude { get; set; }

        /// <summary>
        /// If true, try to prevent the view from the camera to the target from being occluded by obstacles
        /// </summary>
        [DataMember, Browsable(true)]
        public bool PreventOcclusion { get; set; }

        /// <summary>
        /// Threshold distance from the target position that is considered to be occlusion
        /// </summary>
        [DataMember, Browsable(true)]
        public float OcclusionThreshold { get; set; }

        Port<xna.Vector3> _clearest = new Port<xna.Vector3>();

        /// <summary>
        /// Default PursuitCameraEntity constructor
        /// </summary>
        public PursuitCameraEntity()
        {
            SetDefaults();
        }

        /// <summary>
        /// PursuitCameraEntity constructor that takes an entity name
        /// </summary>
        /// <param name="target">Name of the entity to follow</param>
        public PursuitCameraEntity(string target)
            : base()
        {
            _targetName = target;
            SetDefaults();
        }

        /// <summary>
        /// Set the default values
        /// </summary>
        private void SetDefaults()
        {
            MinDistance = 0.5f;
            MaxDistance = 1.5f;
            Altitude = 1.0f;
            OcclusionThreshold = 0.5f;
            PreventOcclusion = true;
        }

        /// <summary>
        /// Find the target for the camera
        /// </summary>
        private void FindTarget()
        {
            if (HasBeenInitialized)
            {
                var query = new VisualEntity();
                query.State.Name = _targetName;

                Activate(
                    Arbiter.Choice(
                        SimulationEngine.GlobalInstancePort.Query(query),
                        success => Target = success.Entity,
                        CcrServiceBase.EmptyHandler
                    )
                );
            }
        }

        /// <summary>
        /// Custom InitializePlot that ensures the PursuitCamera is a first person camera
        /// </summary>
        /// <param name="device"></param>
        /// <param name="physicsEngine"></param>
        public override void Initialize(xna.Graphics.GraphicsDevice device, PhysicsEngine physicsEngine)
        {
            base.Initialize(device, physicsEngine);

            if (CameraModel != CameraModelType.FirstPerson)
            {
                CameraModel = CameraModelType.FirstPerson;
            }

            FindTarget();
        }

        /// <summary>
        /// Custom frameUpdate that follows the entity the PursuitCamera is targeting
        /// </summary>
        /// <param name="frameUpdate">Details of the frame update</param>
        public override void Update(FrameUpdate frameUpdate)
        {
            if (Target != null)
            {
                Altitude += altitudeChange;
                MaxDistance += zoomChange;
                MinDistance += zoomChange;

                if (MinDistance < 0.5f) MinDistance = 0.5f;
                else if (MinDistance > 2.0f) MinDistance = 2.0f;

                if (MaxDistance < 0.5f) MaxDistance = 0.5f;
                else if (MaxDistance > 5.0f) MaxDistance = 5.0f;

                // Vector to the camera, used for computing the new position
                var entityToCamera = Location - Target.Position;

                var distance = entityToCamera.Length();

                if (distance > 0.1f)
                {
                    float scale = 1;
                    
                    // Scale the vector if the distance is outside the bounds
                    if (distance > MaxDistance)
                    {
                        scale = MaxDistance / distance;
                    }
                    else if (distance < MinDistance)
                    {
                        scale = MinDistance / distance;
                    }

                    var scaled = xna.Vector3.Multiply(entityToCamera, scale);

                    // Set the new camera position, although the altitude will be off
                    var newCamera = Target.Position + scaled;

                    // Constrain the altitude
                    newCamera.Y = Target.Position.Y + Altitude;

                    newCamera = MathHelper.RotateAroundAxis_Y(newCamera, Target.Position, angleChange);

                    if (PreventOcclusion)
                    {
                        // Rotate this new position around the target position 32 times.
                        // in each iteration perform an occlusion test. Find the direction
                        // that is furthest from an occluded direction (i.e. is in the 
                        // most free space)
                        var clearest = FindClearest(newCamera, Target.Position, 32);

                        // Interpolate these two positions together. The weight is given 
                        // predominantly to the first position, this weighting is scaled
                        // by the elapsed frame pointsOfTime to try and keep camera motion approximately
                        // constant. for example at 50 fps, the weight is 2%, at 10 fps the
                        // weight is 10% etc.
                        newCamera = xna.Vector3.Lerp(
                            newCamera,
                            clearest,
                            (float)frameUpdate.ElapsedTime);
                    }

                    base.SetViewParameters(newCamera, Target.Position);
                }
            }

            base.Update(frameUpdate);
        }

        void FindClearestAsync(xna.Vector3 from, xna.Vector3 to, int rayCount)
        {
            // the vector from the target to the current position
            var reference = from - to;

            Port<OcclusionRay> impacts = new Port<OcclusionRay>();

            for (int index = 0; index < rayCount; index++)
            {
                var angle = 2 * Math.PI * index / rayCount;
                var rotation = xna.Quaternion.CreateFromAxisAngle(xna.Vector3.UnitY, (float)angle);

                // Rotate the reference vector by the current angleChange
                var rotated = xna.Vector3.Transform(reference, rotation);

                // Rreate a new test position
                var curr = to + rotated;

                SingleRaycastAsync(curr, to, index, impacts);
            }

            Activate(
                Arbiter.MultipleItemReceive(false, impacts, rayCount, RayCastHandler)
            );
        }

        void RayCastHandler(params OcclusionRay[] input)
        {
            List<OcclusionRay> rays = new List<OcclusionRay>(input);

            rays.Sort((left, right) => left.Index.CompareTo(right.Index));

            _clearest.Post(FindMostOpenRay(rays, rays[0].Point));
        }

        void Activate<T>(params T[] tasks)
            where T : ITask
        {
            SimulationEngine.GlobalInstance.Activate(tasks);
        }

        private xna.Vector3 FindClearest(xna.Vector3 from, xna.Vector3 to, int rayCount)
        {
            // the vector from the target to the current position
            var reference = from - to;

            // orient the reference vector to the world basis.
            reference.X = (float)Math.Sqrt(reference.X * reference.X + reference.Z * reference.Z);
            reference.Z = 0;

            var rays = new List<OcclusionRay>(rayCount);

            for (int index = 0; index < rayCount; index++)
            {
                var angle = 2 * Math.PI * index / rayCount;
                var rotation = xna.Quaternion.CreateFromAxisAngle(xna.Vector3.UnitY, (float)angle);

                // rotate the reference vector by the current angleChange
                var rotated = xna.Vector3.Transform(reference, rotation);

                // create a new test position
                var curr = to + rotated;

                var impact = SingleRaycast(curr, to);

                var distance = (impact - to).Length();

                rays.Add(
                    new OcclusionRay
                    {
                        Point = curr,
                        Impact = impact,
                        Distance = distance,
                        Occluded = distance > OcclusionThreshold,
                        Run = 0
                    }
                );
            }

            return FindMostOpenRay(rays, from);
        }

        xna.Vector3 _previousClearest = xna.Vector3.UnitY;

        private xna.Vector3 FindMostOpenRay(List<OcclusionRay> rays, xna.Vector3 original)
        {
            int rayCount = rays.Count;

            var best = ComputeRunLengths(rays);

            if (best.Count == 0)
            {
                //
                // EITHER no rays were occluded.
                // OR no unoccluded ray was found
                // return the input origin point
                //
                _previousClearest = original;
                return _previousClearest;
            }
            else
            {
                var potential = new List<xna.Vector3>();

                foreach (var run in best)
                {
                    var ray = rays[run];
                    var half = ray.Run / 2;

                    if ((ray.Run % 2) == 0)
                    {
                        // for even run lengths take the two center points
                        var high = (run + half) % rayCount;
                        var low = (high - 1 + rayCount) % rayCount;

                        potential.Add(rays[low].Point);
                        potential.Add(rays[high].Point);
                    }
                    else if (ray.Run > 1)
                    {
                        // for odd run lengths take the three center points
                        var center = (run + half) % rayCount;
                        var low = (center - 1 + rayCount) % rayCount;
                        var high = (center + 1) % rayCount;

                        potential.Add(rays[low].Point);
                        potential.Add(rays[center].Point);
                        potential.Add(rays[high].Point);
                    }
                    else
                    {
                        potential.Add(ray.Point);
                    }
                }
                //
                // find closest point to the previous best point.
                //

                potential.Sort(
                    (left, right) =>
                    {
                        var a = left - _previousClearest;
                        var b = right - _previousClearest;

                        return a.LengthSquared().CompareTo(b.LengthSquared());
                    }
                );

                _previousClearest = potential[0];

                return _previousClearest;
            }
        }

        private List<int> ComputeRunLengths(List<OcclusionRay> rays)
        {
            int longest = 0;
            int best = -1;
            int rayCount = rays.Count;

            for (int i = 0; i < rayCount; i++)
            {
                var ray = rays[i];

                if (ray.Occluded)
                {
                    ray.Run = 0;
                }
                else
                {
                    if (i > 0 && !rays[i - 1].Occluded)
                    {
                        //
                        // the previous ray was not occluded, and this ray is not occluded,
                        // therefore the run count for this ray is the same as for the previous
                        //
                        ray.Run = rays[i - 1].Run;
                    }
                    else
                    {
                        //
                        // count the length of the series of unoccluded rays
                        //

                        ray.Run = 1;

                        for (int j = (i + 1) % rayCount; j != i; j = (j + 1) % rayCount)
                        {
                            if (rays[j].Occluded)
                            {
                                break;
                            }
                            ray.Run++;
                        }

                        if (ray.Run > longest)
                        {
                            longest = ray.Run;
                            best = i;
                        }
                    }
                }
            }

            if (longest == 0 || longest == rayCount)
            {
                return new List<int>();
            }
            else
            {
                var runStarts = new List<int>();

                for (int i = 0; i < rayCount; i++)
                {
                    if (!rays[i].Occluded)
                    {
                        int prev = (i + rayCount - 1) % rayCount;
                        if (rays[prev].Occluded)
                        {
                            runStarts.Add(i);
                        }
                    }
                }

                return runStarts.FindAll(test => rays[test].Run + 1 >= longest);
            }
        }

        private xna.Vector3 SingleRaycast(xna.Vector3 from, xna.Vector3 to)
        {
            var rayResult = PhysicsEngine.Raycast2D(
                RaycastProperties.FromSingleRay(
                    TypeConversion.FromXNA(from),
                    TypeConversion.FromXNA(to)
                )
            );

            RaycastResult result = null;

            //
            // !Warning! this code is relying on the Raycast operation being 
            // fundementally synchronous, wrapped in an async API. 
            //

            if (rayResult.Test(out result))
            {
                if (result.ImpactPoints.Count == 1)
                {
                    var impact = result.ImpactPoints[0].Position;
                    return new xna.Vector3(
                        impact.X,
                        impact.Y,
                        impact.Z
                    );
                }
            }

            return to;
        }

        private void SingleRaycastAsync(xna.Vector3 from, xna.Vector3 to, int index, Port<OcclusionRay> resultPort)
        {
            var rayResult = PhysicsEngine.Raycast2D(
                RaycastProperties.FromSingleRay(
                    TypeConversion.FromXNA(from),
                    TypeConversion.FromXNA(to)
                )
            );

            Activate(
                Arbiter.Receive(false, rayResult,
                    delegate(RaycastResult result)
                    {
                        xna.Vector3 hitPoint;

                        if (result.ImpactPoints.Count == 1)
                        {
                            var impact = result.ImpactPoints[0].Position;
                            hitPoint = new xna.Vector3(
                                impact.X,
                                impact.Y,
                                impact.Z
                            );
                        }
                        else
                        {
                            hitPoint = to;
                        }

                        float distance = (hitPoint - to).Length();

                        resultPort.Post(
                            new OcclusionRay
                            {
                                Index = index,
                                Point = from,
                                Impact = hitPoint,
                                Distance = distance,
                                Occluded = distance > OcclusionThreshold,
                                Run = 0
                            }
                        );
                    }
                )
            );
        }
    }

    class OcclusionRay
    {
        /// <summary>
        /// Index of this ray
        /// </summary>
        public int Index;
        /// <summary>
        /// Origin point of ray
        /// </summary>
        public xna.Vector3 Point;
        /// <summary>
        /// Impact point of ray
        /// </summary>
        public xna.Vector3 Impact;
        /// <summary>
        /// Distance from impact point to original target point
        /// </summary>
        public float Distance;
        /// <summary>
        /// was this ray occluded?
        /// </summary>
        public bool Occluded;
        /// <summary>
        /// Size of run of unoccluded rays that this ray is a member of (0 if this ray was occluded)
        /// </summary>
        public int Run;

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendFormat("({0:G3}, {1:G3}, {2:G3}) -> ",
                Point.X, Point.Y, Point.Z
                );
            builder.AppendFormat("({0:G3}, {1:G3}, {2:G3}) ",
                Impact.X, Impact.Y, Impact.Z
                );
            builder.AppendFormat("{0} {1} {2}", Distance, Occluded, Run);

            return builder.ToString();
        }
    }
}