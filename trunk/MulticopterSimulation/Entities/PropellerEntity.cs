using Microsoft.Dss.Core.Attributes;
using Microsoft.Robotics.PhysicalModel;
using Microsoft.Robotics.Simulation.Engine;
using Microsoft.Robotics.Simulation.Physics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using xna = Microsoft.Xna.Framework;
using xnagfx = Microsoft.Xna.Framework.Graphics;

namespace MulticopterSimulation.Entities
{
    /// <summary>
    /// A visual entity describing a single rotating propeller
    /// </summary>
    [DataContract]
    [Browsable(false)]
    class PropellerEntity : VisualEntity
    {
        #region Constants

        const float PROPELLER_MASS = 0.1f; // The mass of the propeller
        const string PROPELLER_MESH_FILE = "MulticopterSimulation/multicopter_propeller.obj"; // The 3D mesh file

        readonly Vector3 PROPELLER_BOXSHAPE_DIMENSION = new Vector3(0.1f, 0.01f, 0.1f); // The dimensions of the propeller

        #endregion

        #region Fields

        public float pitchRate = 0.0f; // A counter used as the angular velocity of the propeller

        public Vector3 localPosition;  // The position of the propeller relative to the multicopter
        public Vector3 forceDirectionVector;

        private BoxShapeProperties propellerShapeProperties;    // The properties of the propeller shape
        private BoxShape propellerShape;        // The physics shape of the propeller
        private VisualEntityMesh propellerVisualMesh; // The visual mesh of the propeller
        private Vector3 meshRotationVector;     // The rotation of the mesh

        #endregion

        /// <summary>
        /// Empty constructor, needed for the deserialization
        /// </summary>
        public PropellerEntity()
        {
        }

        /// <summary>
        /// Creates a new propeller entity
        /// </summary>
        /// <param name="name">Name of the entity</param>
        /// <param name="position">The position of the propeller to be placed</param>
        public PropellerEntity(string name, Vector3 position, Quaternion orientation, Vector3 forceVector)
        {
            // Apply the entity from the given parameter
            State.Name = name;
            localPosition = position;
            this.forceDirectionVector = forceVector;

            // Create global objects
            meshRotationVector = new Vector3();

            // Load and scale the 3D mesh of the multicopter
            //Flags |= VisualEntityProperties.UsesAlphaBlending;
            //Flags |= VisualEntityProperties.DisableBackfaceCulling;

            // Apply a box shape for the propeller
            propellerShapeProperties = new BoxShapeProperties(
                "BoxShapeProperties " + name,
                PROPELLER_MASS,
                new Pose(localPosition),
                PROPELLER_BOXSHAPE_DIMENSION
            );

            propellerShapeProperties.LocalPose.Position = localPosition;
            propellerShapeProperties.LocalPose.Orientation = orientation;
        }

        /// <summary>
        /// Called, when the new propeller entity
        /// </summary>
        /// <param name="device">The graphics device on which the entity is displayed</param>
        /// <param name="physicsEngine">The physics engine handling the entity physics</param>
        public override void Initialize(xnagfx.GraphicsDevice device, PhysicsEngine physicsEngine)
        {
            try
            {
                InitError = string.Empty;
                
                AddsShapeToParent = true;
                
                base.Initialize(device, physicsEngine);

                // Set the mesh for the propeller
                State.Assets.Mesh = PROPELLER_MESH_FILE;
                MeshScale = new Vector3(0.01f, 0.01f, 0.01f);
                MeshRotation = new Vector3(0.0f, (float)(Globals.RandomGenerator.NextDouble() * 1000.0), 0.0f);

                // TODO Transparente Propeller-Texture ab gewisser Drehgeschwindigkeit
                //HeightFieldShapeProperties hf = new HeightFieldShapeProperties("height field", 2, 0.02f, 2, 0.02f, 0, 0, 1, 1);
                //hf.HeightSamples = new HeightFieldSample[hf.RowCount * hf.ColumnCount];
                //for (int i = 0; i < hf.HeightSamples.Length; i++)
                //    hf.HeightSamples[i] = new HeightFieldSample();
                //Shape _particlePlane = new Shape(hf);
                //_particlePlane.State.Name = "Propeller plane";
                //Meshes.Add(SimulationEngine.ResourceCache.CreateMesh(device, _particlePlane.State));
                //Meshes[0].Textures[0] = SimulationEngine.ResourceCache.CreateTextureFromFile(device, "A-Eye/prop_rot.png");

                if (Parent == null)
                    throw new Exception("This entity must be a child of another entity.");

                // Add to parent
                propellerShape = new BoxShape(propellerShapeProperties);
                Meshes.Add(SimulationEngine.ResourceCache.CreateMeshFromFile(device, State.Assets.Mesh));
                propellerVisualMesh = Parent.AddShapeToPhysicsEntity(propellerShape, null);
            }
            catch (Exception ex)
            {
                HasBeenInitialized = false;
                InitError = ex.ToString();
            }
        }

        /// <summary>
        /// The frameUpdate method gets called every frame iteration to frameUpdate all values
        /// </summary>
        /// <param name="frameUpdate">Additional data provided with the current frame</param>
        public override void Update(FrameUpdate update)
        {
            ProcessDeferredTaskQueue();

            // Update the pose to the position of the multicopter base
            if (Parent != null)
                State.Pose = Parent.State.Pose;

            // Update the propeller rotation to simulate a moving propeller
            meshRotationVector.Y = MeshRotation.Y + pitchRate;
            MeshRotation = meshRotationVector;
        }

        /// <summary>
        /// The render method gets called every frame iteration to render the new frame
        /// </summary>
        /// <param name="renderMode">The current render mode</param>
        /// <param name="transforms">The matrix transformations to be applied</param>
        /// <param name="currentCamera">The curren camera</param>
        public override void Render(VisualEntity.RenderMode renderMode, MatrixTransforms transforms, CameraEntity currentCamera)
        {
            // Render the propeller shape
            RenderShape(renderMode, transforms, propellerShapeProperties, Meshes[0]);
        }

        /// <summary>
        /// The dispose method gets called, when the entity is removed
        /// </summary>
        public override void Dispose()
        {
            // Remove the physic shapes from the simulation engine
            if (Parent != null)
            {
                ((VisualEntity)Parent).RemoveShapeFromPhysicsEntity(propellerShape, propellerVisualMesh);
                propellerVisualMesh = null;
            }

            base.Dispose();
        }

        
    }
}
