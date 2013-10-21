using Microsoft.Dss.Core.Attributes;
using Microsoft.Robotics.PhysicalModel;
using Microsoft.Robotics.Simulation.Engine;
using Microsoft.Robotics.Simulation.Physics;
using MulticopterSimulation.Algorithms;
using MulticopterSimulation.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;

using xna = Microsoft.Xna.Framework;
using xnagrfx = Microsoft.Xna.Framework.Graphics;

namespace MulticopterSimulation.Entities
{
    /// <summary>
    /// Enumeration representing the different multicopter types
    /// </summary>
    public enum MulticopterType
    {
        TYPE_3_TRICOPTER,
        TYPE_4_QUADROCOPTER,
        TYPE_5_PENTACOPTER,
        TYPE_6_HEXACOPTER,
        TYPE_7_HEPTACOPTER,
        TYPE_8_OCTOCOPTER
    }

    /// <summary>
    /// A visual entity representing a multicopter
    /// </summary>
    [DataContract]
    [Browsable(false)]
    class MulticopterEntity : VisualEntity
    {
        #region Constants

        const float MAX_THRUST = 1.5f; // The maximal thrust of the multicopter
        const float MULTICOPTER_ARM_MASS = 0.125f;  // Mass of a single multicopter arm (e.g. quadrocopter = 4 arms)
        const float THRUST_TO_FORCE_FACTOR = 1.3f; // The relation between thrust and the force
        const float LINEAR_DAMPING = 1.0f;      // The damping of the linear force (rudimentary air resistance)
        const float ANGULAR_DAMPING = 5.0f;     // The damping of the rotation (rudimentary air resistance)
        const float ATTACHED_CAMERA_ROTATION_Y = xna.MathHelper.Pi / -2; // The view direction of the attached camera
        const float CHASSIS_DIMENSIONS_HEIGHT = 0.07f;  // Height of the chassis dimension

        readonly Vector3 ATTACHED_CAMERA_POSITION = new Vector3(0.0f, 0.02f, 0.0f); // The position of the attached camera
        readonly Vector3 ARM_CHASSIS_DIMENSIONS = new Vector3(0.15f, CHASSIS_DIMENSIONS_HEIGHT, 0.05f); // Chassis dimensions in meters

        #endregion

        #region Fields

        // The raw input from the controller
        public float raw_leftY = 0.0f; // Controls the thrust
        public float raw_leftX = 0.0f; // Controls the yaw
        public float raw_rightY = 0.0f;  // Controls the pitch 
        public float raw_rightX = 0.0f;  // Controls the roll

        // The distance of the chasing camera
        public float chaseCamDistance = 2.0f;

        // The activation states of the stabilization operations
        public bool keepAltitude = false;
        public bool keepAttitude = false;
        public bool keepPosition = false;

        // Values concerning wind simulation
        public bool windSimulationActivated = true;
        public float settingsWindDirection = 0.0f;
        public float settingsWindIntensity = 0.0f;
        public float settingsWindDirectionFluctuation = 0.0f;
        public float settingsWindIntensityFluctuation = 0.0f;

        // The inclination of the propellers in degrees
        private float propellerInclinationAngle = 0.0f;

        // The raw calculated values from the controller input
        private float raw_thrust = 0.0f; // The raw thrust of the multicopter (provided by the engine speed)
        private float raw_pitch = 0.0f; // The raw pitch velocity (moves the multicopter forwards or backwards)
        private float raw_yaw = 0.0f; // The raw yaw velocity (turns the multicopter clockwise or counter-clockwise)
        private float raw_roll = 0.0f; // The raw roll velocity (moves the multicopter sideward)

        // The ratio between thrust and MAX_THRUST
        private float thrust_ratio = 0.0f;

        private float attitude_dx = 0.0f;
        private float attitude_dz = 0.0f;
        private float pos_x_controlValue = 0.0f;
        private float pos_z_controlValue = 0.0f;

        // TODO Create a new camera reset handler in the multicopter simulation and delete this handle
        // The main camera to reset
        public CameraView cameraView;

        // TODO Make this global accessible
        // Handles to the window events port
        public WindowStatistics.WindowStatisticsEvents windowStatisticsEventsPort;
        public WindowControl.WindowControlEvents windowControlEventsPort;

        // Handles to the PID controllers
        public PIDController pidControllerAttitudeX;
        public PIDController pidControllerAttitudeZ;
        public PIDController pidControllerAltitude;
        public PIDController pidControllerPositionX;
        public PIDController pidControllerPositionZ;

        // Save the initial position so the reset can take it back to this position
        private Vector3 initialPos;

        // Save the type of the multicopter
        private MulticopterType multicopterType;

        // The rotation torque
        private Vector3 torqueVector;

        // A vector calculating the temporary force for every propeller
        private Vector3 forceVector;

        // A vector calculating the total vertical force for debug
        private Vector3 totalForceVector;

        // A vector containing the current wind force
        private Vector3 windForceVector;

        // Attached front cam entity
        private AttachedCameraEntity attachedFrontCam;

        // Propeller entities
        private List<PropellerEntity> propellerEntities;

        // Entity managing the sounds
        private SoundEntity soundEntity;

        // Event message containers to be updated
        private WindowStatistics.OnPlotValue plotValue;
        private WindowStatistics.OnDisplayUpdate updateDebugData;
        private WindowControl.OnUpdateDisplay updateStatsData;

        // Simplex noise generator instances
        private SimplexNoiseGenerator sngConstantWind;
        private SimplexNoiseGenerator sngWindGust;
        private SimplexNoiseGenerator sngWindDirectionFluctuation;
        private SimplexNoiseGenerator sngAttitudeDisturbances;

        #endregion

        /// <summary>
        /// Empty constructor, needed for the deserialization
        /// </summary>
        public MulticopterEntity()
        {
        }

        /// <summary>
        /// Creates a new multicopter entity
        /// </summary>
        /// <param name="name">The name of the entity</param>
        /// <param name="initialPos">The initial position for the multicopter to be placed</param>
        public MulticopterEntity(MulticopterType multicopterType, float propellerInclinationAngle, Vector3 initialPos)
        {
            // Save the initialization parameters
            this.multicopterType = multicopterType;
            this.propellerInclinationAngle = propellerInclinationAngle;
            this.initialPos = initialPos;

            // TO DO Entfernen
            Globals.currentMulticopterType = Globals.Instance.multicopterDesignation[multicopterType];
            Globals.currentInclinationAngle = ((int)propellerInclinationAngle).ToString() + "°";

            // InitializePlot vectors
            totalForceVector = new Vector3();
            torqueVector = new Vector3();
            windForceVector = new Vector3();

            // InitializePlot the sound entity
            soundEntity = new SoundEntity();
            soundEntity.PlaySound();

            // InitializePlot miscellaneous
            InitializeWindowEventMessages();
            InitializePIDController();
            InitializeSimplexNoiseGenerators();
            
            // Set the state name
            State.Name = Globals.Instance.multicopterDesignation[multicopterType];

            // Set the mass densitiy. Mass equals weight of a single multicopter arm times the arms
            State.MassDensity.Mass = MULTICOPTER_ARM_MASS * Globals.Instance.multicopterPropellerCount[multicopterType];
            State.MassDensity.Density = 0.0f;

            // Set the center of mass
            State.MassDensity.CenterOfMass = new Pose(new Vector3(0.0f, CHASSIS_DIMENSIONS_HEIGHT / 2, 0.0f));

            // Simulate air resistance
            // TODO Impuls und Drehimpuls selbst anhand der Fläche simulieren
            State.MassDensity.LinearDamping = LINEAR_DAMPING;
            State.MassDensity.AngularDamping = ANGULAR_DAMPING;

            // Load and scale the 3D mesh of the multicopter
            State.Assets.Mesh = Globals.Instance.multicopterMeshFiles[multicopterType];

            // Set mesh scale so 1.0f = 1 meter
            MeshScale = new Vector3(0.01f, 0.01f, 0.01f);

            InsertAttachedCameraEntity();
        }

        /// <summary>
        /// InitializePlot the multicopter entity simulation environment
        /// </summary>
        /// <param name="device">A reference to the graphics device</param>
        /// <param name="physicsEngine">A reference to the created instance of the physics engine</param>
        public override void Initialize(xnagrfx.GraphicsDevice device, PhysicsEngine physicsEngine)
        {
            try
            {
                InitError = string.Empty;

                // Build the model
                ProgrammaticallyBuildModel(device, physicsEngine);

                // Reset values, position, rotation of the multicopter entity
                Reset();

                // Insert propellers dynamically based on multicopter type
                InsertPropellers(device, physicsEngine);

                base.Initialize(device, physicsEngine);
            }

            catch (Exception ex)
            {
                // In case of exception delete physics entity and manage the errors
                if (PhysicsEntity != null)
                    PhysicsEngine.DeleteEntity(PhysicsEntity);

                HasBeenInitialized = false;
                InitError = ex.ToString();
            }
        }

        private float currentWindDirectionAngle = 0.0f;
        private float totalWindIntensity = 0.0f;
        xna.Vector3 globalRotation;

        /// <summary>
        /// The frameUpdate method gets called every frame iteration
        /// </summary>
        /// <param name="frameUpdate">Additional data provided with the current frame</param>
        public override void Update(FrameUpdate frameUpdate)
        {
            // Update state for us and all the shapes that make up the rigid body
            PhysicsEntity.UpdateState(true);

            // Calculate the raw values by the controller input
            raw_thrust += 0.025f * MAX_THRUST * raw_leftY;
            raw_yaw = -raw_leftX;
            raw_pitch = raw_rightY;
            raw_roll = raw_rightX;

            // Limit too high thrust values
            if (raw_thrust < 0.0f) raw_thrust = 0.0f;
            else if (raw_thrust > MAX_THRUST) raw_thrust = MAX_THRUST;

            // Calculate the thrust ratio (used for motor sound and rotation behaviour)
            thrust_ratio = raw_thrust / MAX_THRUST;

            // Clear total force vector for the plot
            totalForceVector.X = 0.0f;
            totalForceVector.Y = 0.0f;
            totalForceVector.Z = 0.0f;

            // Rotation torque
            torqueVector.X = raw_roll / 20;
            torqueVector.Y = raw_yaw / 10;
            torqueVector.Z = raw_pitch / 20;

            PhysicsEntity.ApplyTorque(torqueVector, true);

            // Calculate the total rotation (for sound effects an propeller)
            float rotationFactor = Math.Abs(torqueVector.X) + Math.Abs(torqueVector.Y) + Math.Abs(torqueVector.Z);

            // Let the linear force affect the multicopter
            foreach (PropellerEntity propellerEntity in propellerEntities)
            {
                // Calculate the force vector for the current propeller
                forceVector = Vector3.Scale(propellerEntity.forceDirectionVector, raw_thrust * THRUST_TO_FORCE_FACTOR);

                // Let the linear force act on every single propeller
                PhysicsEntity.ApplyForceAtPosition(
                    forceVector,
                    propellerEntity.localPosition,
                    true,
                    true
                );

                // Calculate the force to the current force vector
                totalForceVector += forceVector;
                
                // Let the propellers rotate depending on the linearForce
                propellerEntity.pitchRate = (raw_thrust + (10 * rotationFactor)) * 100;

                // Update the propeller entity
                propellerEntity.Update(frameUpdate);
            }

            #region Wind Force

            if (windSimulationActivated)
            {
                SimulateWind();
            }

            #endregion

            #region Auto stabilization

            globalRotation = MathHelper.RotateAroundAxis_Y(Rotation, new xna.Vector3(), xna.MathHelper.ToRadians(-Rotation.Y));

            // Use PID algorithm to keep the attitude 
            if (keepAttitude && raw_rightX == 0 && raw_rightY == 0)
            {
                KeepAttitude();
            }

            // Use PID algorithm to keep altitude
            if (keepAltitude && raw_leftY == 0.0f)
            {
                KeepAltitude();
            }

            // Keep position
            if (keepPosition && raw_rightX == 0 && raw_rightY == 0)
            {
                KeepPosition();
            }
            else
            {
                pidControllerAttitudeX.referenceValue = 0.0f;
                pidControllerAttitudeZ.referenceValue = 0.0f;
            }

            #endregion

            #region Update sound

            // Update sound
            float newVolume = thrust_ratio + rotationFactor;
            soundEntity.Volume = (newVolume > 1.0f) ? 1.0f : newVolume; // TODO In die SoundEntity machen
            soundEntity.Pitch = thrust_ratio + rotationFactor * 2;
            soundEntity.AudioEmitterPosition = Position;
            soundEntity.AudioListenerPosition = frameUpdate.ActiveCamera.Eye;
            //soundEntity.AudioListenerLookAt = frameUpdate.ActiveCamera.LookAt; // TODO Schauen was hier falsch läuft
            soundEntity.Update3DSound();

            #endregion

            #region Debug result

            // Debug result
            updateDebugData.applicationTime = frameUpdate.ApplicationTime;
            updateDebugData.position = Position;
            updateDebugData.velocity = State.Velocity;
            updateDebugData.linearForce = totalForceVector;
            //updateDebugData.rotation = Rotation;
            updateDebugData.rotation = globalRotation;
            updateDebugData.angularVelocity = State.AngularVelocity;
            updateDebugData.rotationTorque = torqueVector;
            updateDebugData.thrust = raw_thrust;
            updateDebugData.pidControllerAttitudeXValue = pidControllerAttitudeX.DebugOutputValue;
            updateDebugData.pidControllerAttitudeZValue = pidControllerAttitudeZ.DebugOutputValue;
            updateDebugData.pidControllerAltitudeValue = pidControllerAltitude.DebugOutputValue;
            updateDebugData.pidControllerPositionXValue = pidControllerPositionX.DebugOutputValue;
            updateDebugData.pidControllerPositionZValue = pidControllerPositionZ.DebugOutputValue;
            updateDebugData.windDirection = currentWindDirectionAngle;
            updateDebugData.windIntensity = totalWindIntensity;
            windowStatisticsEventsPort.Post(updateDebugData);

            #endregion

            #region Stats result

            // Stats result
            updateStatsData.thrust = raw_thrust;
            updateStatsData.thrust_ratio = thrust_ratio;
            updateStatsData.altitude = Position.Y;
            updateStatsData.speedVector = State.Velocity;
            windowControlEventsPort.Post(updateStatsData);

            #endregion

            // Simulation engine will frameUpdate children
            base.Update(frameUpdate);
        }

        /// <summary>
        /// The render method is called every frame and updates the propellers
        /// </summary>
        /// <param name="renderMode"></param>
        /// <param name="transforms"></param>
        /// <param name="currentCamera"></param>
        public override void Render(VisualEntity.RenderMode renderMode, MatrixTransforms transforms, CameraEntity currentCamera)
        {
            // Render the propeller updates
            foreach (PropellerEntity propellerEntity in propellerEntities)
            {
                propellerEntity.Render(renderMode, transforms, currentCamera);
            }

            // Render this entity
            base.Render(renderMode, transforms, currentCamera);
        }

        /// <summary>
        /// The dispose method gets called, when the entity is removed
        /// </summary>
        public override void Dispose()
        {
            // Stop the sound engine
            soundEntity.StopSound();
            
            // Remove the propellers
            foreach (PropellerEntity propellerEntity in propellerEntities)
            {
                propellerEntity.Dispose();
            }

            // Remove the attached camera
            attachedFrontCam.Dispose();
            RemoveEntity(attachedFrontCam);

            // Remove the physics primitives
            State.PhysicsPrimitives.Clear();

            // Delete physics entity
            PhysicsEngine.DeleteEntity(PhysicsEntity);
            PhysicsEntity = null;

            // Remove this entity
            base.Dispose();
        }

        /// <summary>
        /// Build the the physical model
        /// </summary>
        /// <param name="device">A reference to the used graphics graphics device</param>
        /// <param name="physicsEngine">A reference to the used physics engine</param>
        private void ProgrammaticallyBuildModel(xnagrfx.GraphicsDevice device, PhysicsEngine physicsEngine)
        {
            // Create central box
            BoxShapeProperties centerBoxChassisProperties = new BoxShapeProperties(
                    0.2f, // 200g
                    new Pose(new Vector3(0, CHASSIS_DIMENSIONS_HEIGHT / 2, 0)),
                    new Vector3(0.06f, CHASSIS_DIMENSIONS_HEIGHT, 0.06f)
                );
            //State.PhysicsPrimitives.Add(new BoxShape(centerBoxChassisProperties));

            // Create box shape dynamically for each multicopter arm
            float angleOffset = (2 * xna.MathHelper.Pi) / Globals.Instance.multicopterPropellerCount[multicopterType];
            Vector3 multicopterCenter = new Vector3(ARM_CHASSIS_DIMENSIONS.X / 2, ARM_CHASSIS_DIMENSIONS.Y / 2 - 0.004f, 0.0f);
            for (int i = 0; i < Globals.Instance.multicopterPropellerCount[multicopterType]; i++)
            {
                BoxShapeProperties chassisShapeProperties = new BoxShapeProperties(
                    MULTICOPTER_ARM_MASS,
                    new Pose(MathHelper.RotateAroundAxis_Y(multicopterCenter, new Vector3(), angleOffset * i),
                             Quaternion.FromAxisAngle(0.0f, -1.0f, 0.0f, angleOffset * i)),
                    ARM_CHASSIS_DIMENSIONS
                );

                // Set the material properties
                // -> Restitution = how bouncy the material is
                // -> Static friction = Friction between non-moving surfaces
                // -> Dynamic friction = Friction betwwen moving surfaces
                chassisShapeProperties.Material = new MaterialProperties("high friction", 0.1f, 1.0f, 20.0f);
                chassisShapeProperties.Name = State.Name + " Arm chassis " + i;

                // Add the chassis descriptions
                State.PhysicsPrimitives.Add(new BoxShape(chassisShapeProperties));
            }

            // Gesamtmasse des Multicopters im Statistics Fenster anzeigen
            WindowStatistics.OnSingleDisplayUpdate onSingleDisplayUpdate = new WindowStatistics.OnSingleDisplayUpdate();
            onSingleDisplayUpdate.multicopterWeight = Globals.Instance.multicopterPropellerCount[multicopterType] * MULTICOPTER_ARM_MASS;
            windowStatisticsEventsPort.Post(onSingleDisplayUpdate);

            // Create and insert the physics entity
            base.CreateAndInsertPhysicsEntity(physicsEngine);

            // Increase physics fidelity
            base.PhysicsEntity.SolverIterationCount = 60;
        }

        /// <summary>
        /// Reset the values, the position, rotation etc.
        /// </summary>
        public void Reset()
        {
            // Reset the raw values
            raw_thrust = 0.0f;
            raw_yaw = 0.0f;
            raw_pitch = 0.0f;
            raw_roll = 0.0f;

            //keepHeight = false;
            //autoHover = false;
            attitude_dx = 0.0f;
            attitude_dz = 0.0f;
            pidControllerAltitude.Reset();
            pidControllerAttitudeX.Reset();
            pidControllerAttitudeZ.Reset();

            // Reset rotation and position
            Rotation = new xna.Vector3();
            Position = TypeConversion.ToXNA(initialPos);

            // Reset the linear and the angular velocity
            PhysicsEntity.SetAngularVelocity(new Vector3());
            PhysicsEntity.SetLinearVelocity(new Vector3());

            // Reset the camera to the initial view
            cameraView.EyePosition = MulticopterSimulationService.INITIAL_CAMERA_EYEPOS;
            cameraView.LookAtPoint = MulticopterSimulationService.INITIAL_CAMERA_LOOKAT;
            SimulationEngine.GlobalInstancePort.Update(cameraView);
        }

        /// <summary>
        /// InitializePlot all window event messages
        /// </summary>
        private void InitializeWindowEventMessages()
        {
            updateDebugData = new WindowStatistics.OnDisplayUpdate();
            plotValue = new WindowStatistics.OnPlotValue();
            updateStatsData = new WindowControl.OnUpdateDisplay();
        }

        /// <summary>
        /// InitializePlot all the PID controllers
        /// </summary>
        private void InitializePIDController()
        {
            pidControllerAttitudeX = new PIDController();
            pidControllerAttitudeX.referenceValue = 0.0f;
            pidControllerAttitudeX.controlValueMaxChange = 0.001f;

            pidControllerAttitudeZ = new PIDController();
            pidControllerAttitudeZ.referenceValue = 0.0f;
            pidControllerAttitudeZ.controlValueMaxChange = 0.001f;

            pidControllerAltitude = new PIDController();
            pidControllerAltitude.referenceValue = 15.0f;
            pidControllerAltitude.controlValueMaxChange = 0.05f;

            pidControllerPositionX = new PIDController();
            pidControllerPositionX.referenceValue = 0.0f;
            pidControllerPositionX.controlValueMaxChange = 1.0f;

            pidControllerPositionZ = new PIDController();
            pidControllerPositionZ.referenceValue = 0.0f;
            pidControllerPositionZ.controlValueMaxChange = 1.0f;
        }

        /// <summary>
        /// InitializePlot all required simplex noises
        /// </summary>
        private void InitializeSimplexNoiseGenerators()
        {
            sngConstantWind = new SimplexNoiseGenerator();
            sngWindGust = new SimplexNoiseGenerator();
            sngWindDirectionFluctuation = new SimplexNoiseGenerator();
            sngAttitudeDisturbances = new SimplexNoiseGenerator();
        }

        /// <summary>
        /// Insert an attached camera
        /// </summary>
        private void InsertAttachedCameraEntity()
        {
            // Add an attached camera
            attachedFrontCam = new AttachedCameraEntity();
            attachedFrontCam.State.Name = "Attached front camera";
            attachedFrontCam.Parent = this;
            attachedFrontCam.Near = 0.01f;

            attachedFrontCam.State.Pose = new Pose(
                ATTACHED_CAMERA_POSITION, // Position of the camera
                Quaternion.FromAxisAngle(0.0f, 1.0f, 0.0f, ATTACHED_CAMERA_ROTATION_Y) // View direction of the camera
            );

            InsertEntity(attachedFrontCam);
        }

        /// <summary>
        /// Insert propellers dynamically based on multicopter type
        /// </summary>
        private void InsertPropellers(xnagrfx.GraphicsDevice device, PhysicsEngine physicsEngine)
        {
            // InitializePlot propeller list
            propellerEntities = new List<PropellerEntity>();

            // Calculate angleChange between arms by dividing 360° by the number of propellers
            float angleBetweenArms = (2 * xna.MathHelper.Pi) / Globals.Instance.multicopterPropellerCount[multicopterType];
            float propellerInclinationAngleRad = xna.MathHelper.ToRadians(propellerInclinationAngle);

            // Define the position of the first propeller (the other positions are calculated by rotating this vector)
            Vector3 positionFirstPropeller = new Vector3(0.11f, CHASSIS_DIMENSIONS_HEIGHT - 0.005f, 0.0f);

            // Define the orientation axis of the first propeller (the other orientation axis are calculated by rotating this vector)
            Vector3 orientationAxisFirstPropeller = new Vector3(0, 0, 1);

            // Calculate the force direction of the first propeller (the other force directions are calculated by rotating this vector)
            Vector3 forceDirectionFirstPropeller = MathHelper.RotateAroundAxis_Z(
                new Vector3(0, 1, 0), // The normal force direction at zero inclination is straight upwards
                new Vector3(), // The forces are local, we rotate around (0, 0, 0)
                propellerInclinationAngleRad // Get inclination angleChange in rad
            );

            // InitializePlot every propeller for every arm
            for (byte i = 0; i < Globals.Instance.multicopterPropellerCount[multicopterType]; i++)
            {
                // Construct the name by the current number
                string namePropeller = State.Name + " Propeller " + (i + 1);

                // Calculate the angleChange between this arm and the first arm
                float currentAngle = angleBetweenArms * i;

                // Calculate the position of the propeller by rotating the first position around the y-axis
                Vector3 positionPropeller = MathHelper.RotateAroundAxis_Y(positionFirstPropeller, new Vector3(), currentAngle);

                // Calculate the position of the orientation axis by rotating the first orientation axis around the y-axis
                Vector3 orientationAxisPropeller = MathHelper.RotateAroundAxis_Y(orientationAxisFirstPropeller, new Vector3(), currentAngle);
                //Quaternion orientationPropeller = Quaternion.FromAxisAngle(orientationAxisPropeller.X, 0, orientationAxisPropeller.Z, propellerInclinationAngleRad);
                Quaternion orientationPropeller = Quaternion.FromAxisAngle(new AxisAngle(orientationAxisPropeller, propellerInclinationAngleRad));

                // Calculate the force direction of the propeller by rotating the first force direction around the y-axis
                Vector3 forceDirectionPropeller = MathHelper.RotateAroundAxis_Y(forceDirectionFirstPropeller, new Vector3(), currentAngle);

                // Create the new propeller entity
                PropellerEntity newPropellerEntity = new PropellerEntity(namePropeller, positionPropeller, orientationPropeller, forceDirectionPropeller);
                newPropellerEntity.Parent = this;
                newPropellerEntity.Initialize(device, physicsEngine);

                // Add the new propeller entity
                propellerEntities.Add(newPropellerEntity);
            }
        }

        private void SimulateWind()
        {
            // Calculate wind intensity fluctuation by the settings
            float windIntensityFluctuation = settingsWindIntensityFluctuation / 1000.0f;
            float windDirectionFluctuation = settingsWindDirectionFluctuation / 1000.0f;

            // Constant wind
            float constantWind = sngConstantWind.GenerateNext_Between0And1(windIntensityFluctuation);
            constantWind /= 2.0f;
            constantWind *= (settingsWindIntensity / 2.0f);

            // Wind gust
            float windGust = sngWindGust.GenerateNext_OnlyPositive(windIntensityFluctuation * 3.0f);
            windGust *= settingsWindIntensity;

            totalWindIntensity = constantWind + windGust;

            float windDirectionChange = 5.0f * sngWindDirectionFluctuation.GenerateNext(windDirectionFluctuation);
            currentWindDirectionAngle = settingsWindDirection + windDirectionChange;

            // Create a wind force vector with length 1 and rotate so the wind affects the multicopter from a certain direction
            windForceVector = MathHelper.RotateAroundAxis_Y(
                new Vector3(1.0f, 0.0f, 0.0f),
                new Vector3(),
                xna.MathHelper.ToRadians(currentWindDirectionAngle)
            );
            windForceVector = MathHelper.RotateAroundAxis_X(
                windForceVector,
                new Vector3(),
                xna.MathHelper.ToRadians(windDirectionChange)
            );

            // Generate oscillation of the multicopter arms
            torqueVector.X = sngAttitudeDisturbances.GenerateNext(Globals.RandomGenerator.Next(100, 300) / 10000.0f) / 250.0f;
            torqueVector.Y = sngAttitudeDisturbances.GenerateNext(Globals.RandomGenerator.Next(100, 300) / 10000.0f) / 300.0f;
            torqueVector.Z = sngAttitudeDisturbances.GenerateNext(Globals.RandomGenerator.Next(100, 300) / 10000.0f) / 250.0f;

            // Scale the direction vector with the intensity
            windForceVector = Vector3.Scale(windForceVector, totalWindIntensity);
            torqueVector = Vector3.Scale(torqueVector, totalWindIntensity + 0.5f);

            // Let the global wind force and torque affect the multicopter
            PhysicsEntity.ApplyForce(windForceVector, false);
            PhysicsEntity.ApplyTorque(torqueVector, false);

            // Sum up for debug display
            totalForceVector += windForceVector;
        }

        private void KeepAttitude()
        {
            pidControllerAttitudeX.Control(globalRotation.X, ref attitude_dx);
            pidControllerAttitudeZ.Control(globalRotation.Z, ref attitude_dz);

            PhysicsEntity.ApplyTorque(new Vector3(attitude_dx, 0.0f, attitude_dz), false);
        }

        private void KeepAltitude()
        {
            pidControllerAltitude.Control(Position.Y, ref raw_thrust);
        }

        private void KeepPosition()
        {
            keepAttitude = true;

            pidControllerPositionX.Control(Position.X, ref pos_x_controlValue);
            if (pos_x_controlValue > 25f) pos_x_controlValue = 25f;
            else if (pos_x_controlValue < -25f) pos_x_controlValue = -25f;

            pidControllerAttitudeZ.referenceValue = -pos_x_controlValue;

            pidControllerPositionZ.Control(Position.Z, ref pos_z_controlValue);
            if (pos_z_controlValue > 25f) pos_z_controlValue = 25f;
            else if (pos_z_controlValue < -25f) pos_z_controlValue = -25f;

            pidControllerAttitudeX.referenceValue = pos_z_controlValue;
        }

        #region Obsolete Code

        // Alternative algorithm to keep the attidue
        //if (keepAttitude && raw_rightX == 0 && raw_rightY == 0)
        //{
        //    if (Rotation.X > 0.5f)
        //        PhysicsEntity.ApplyTorque(new Vector3(-0.01f, 0.0f, 0.0f), true);
        //    else if (Rotation.X < -0.5f)
        //        PhysicsEntity.ApplyTorque(new Vector3(0.01f, 0.0f, 0.0f), true);

        //    if (Rotation.Z > 0.5f)
        //        PhysicsEntity.ApplyTorque(new Vector3(0.0f, 0.0f, -0.01f), true);
        //    else if (Rotation.Z < -0.5f)
        //        PhysicsEntity.ApplyTorque(new Vector3(0.0f, 0.0f, 0.01f), true);
        //}

        //Keep position
        //if (Math.Abs(Rotation.Z) < 30.0f && Math.Abs(Rotation.X) < 30.0f)
        //{
        //    keepAttitude = false;
        //    if (Position.X > 0.2f)
        //    {
        //        PhysicsEntity.ApplyTorque(new Vector3(0.0f, 0.0f, 0.01f), false);
        //    }
        //    else if (Position.X < -0.2f)
        //    {
        //        PhysicsEntity.ApplyTorque(new Vector3(0.0f, 0.0f, -0.01f), false);
        //    }
        //    else
        //    {
        //        keepAttitude = true;
        //    }
        //}
        //else
        //{
        //    keepAttitude = true;
        //}

        //if (Math.Abs(Rotation.Z) < 30.0f && Math.Abs(Rotation.X) < 30.0f)
        //{
        //    keepAttitude = false;
        //    if (Math.Abs(Position.X) > 0.2f)
        //    {
        //        pidControllerPositionX.Control(Position.X, ref pos_x_controlValue);
        //        if (pos_x_controlValue > 0.01f) pos_x_controlValue = 0.01f;
        //        else if (pos_x_controlValue < -0.01f) pos_x_controlValue = -0.01f;

        //        PhysicsEntity.ApplyTorque(new Vector3(0.0f, 0.0f, -pos_x_controlValue), false);
        //    }
        //    else
        //    {
        //        keepAttitude = true;
        //    }
        //}
        //else
        //{
        //    keepAttitude = true;
        //}

        #endregion
    }
}
