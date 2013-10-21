using Microsoft.Robotics.Simulation.Engine;
using Microsoft.Robotics.Simulation.Physics;
using xna = Microsoft.Xna.Framework;

namespace MulticopterSimulation.Entities
{
    /// <summary>
    /// This camera model is for a camera meant to be attached as
    /// a child of another entity. It is free to move in yaw, pitch,
    /// and roll unlike the FirstPersonCamera. Keyboard and mouse inputs are ignored
    /// </summary>
    public class AttachedCameraModel : Camera
    {
        public VisualEntity Parent = null;

        public override void SetViewParameters(xna.Vector3 eyePt, xna.Vector3 lookAtPt)
        {
            // if there is a parent set, the camera matrix is the inverse of the parent world matrix.
            if (Parent != null)
                _viewMatrix = xna.Matrix.Invert(Parent.World);
        }

        public override void Update(double elapsedRealTime, bool hasFocus)
        {
            // no need to frameUpdate this camera, the keyboard and mouse don't affect it
        }      
    }

    /// <summary>
    /// This camera is for a camera meant to be attached as
    /// a child of another entity. It is free to move in yaw, pitch,
    /// and roll unlike the FirstPersonCamera. Keyboard and mouse inputs are ignored
    /// </summary>
    class AttachedCameraEntity : CameraEntity
    {
        public AttachedCameraEntity()
            : base()
        {
        }

        public override void Initialize(Microsoft.Xna.Framework.Graphics.GraphicsDevice device, PhysicsEngine physicsEngine)
        {
            base.Initialize(device, physicsEngine);

            // Replace the camera model used by this camera entity using reflection
            System.Reflection.FieldInfo fInfo = GetType().BaseType.GetField(
                "_frameworkCamera",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic
            );

            if (fInfo != null)
            {
                AttachedCameraModel newCamera = new AttachedCameraModel();
                newCamera.Parent = this;
                fInfo.SetValue(this, newCamera);
            }

            SetProjectionParameters(
                ViewAngle,
                (float)ViewSizeX / (float)ViewSizeY,
                Near,
                Far,
                ViewSizeX,
                ViewSizeY
            );
        }
    }
}
