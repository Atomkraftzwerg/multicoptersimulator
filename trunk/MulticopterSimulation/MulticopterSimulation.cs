using Microsoft.Ccr.Adapters.WinForms;
using Microsoft.Ccr.Core;
using Microsoft.Dss.Core;
using Microsoft.Dss.Core.Attributes;
using Microsoft.Dss.ServiceModel.Dssp;
using Microsoft.Dss.ServiceModel.DsspServiceBase;
using Microsoft.Robotics.PhysicalModel;
using Microsoft.Robotics.Simulation.Engine;
using MulticopterSimulation.Entities;
using MulticopterSimulation.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;

using engineproxy = Microsoft.Robotics.Simulation.Engine.Proxy;
using submgr = Microsoft.Dss.Services.SubscriptionManager;
using xinput = Microsoft.Robotics.Services.Sample.XInputGamepad.Proxy;

namespace MulticopterSimulation
{
    /// <summary>
    /// Service providing a simulation environment for multicopters
    /// </summary>
    [DisplayName("Multicopter Simulation")]
    [Description("Service providing a simulation environment for multicopters")]
    [Contract(Contract.Identifier)]
    class MulticopterSimulationService : DsspServiceBase
    {
        #region Constants

        /// <summary>
        /// The start type of the multicopter (Tetracopter, Quadrocopter, Pentacopter, Hexacopter, ...)
        /// </summary>
        public const MulticopterType INITIAL_MULTICOPTER_TYPE = MulticopterType.TYPE_4_QUADROCOPTER;

        /// <summary>
        /// The start inclination of the propellers
        /// </summary>
        public const float INITIAL_PROPELLER_INCLINATION = 0.0f;

        /// <summary>
        /// The start position of the multicopter entity
        /// </summary>
        public static readonly Vector3 INITIAL_MULTICOPTER_POSITION = new Vector3(3.379f, 0.147f, -6.565f);

        /// <summary>
        /// The initial camera position
        /// </summary>
        public static readonly Vector3 INITIAL_CAMERA_EYEPOS = new Vector3(2.38f, 0.54f, -6.05f);

        /// <summary>
        /// The look-at point of the camera
        /// </summary>
        public static readonly Vector3 INITIAL_CAMERA_LOOKAT = new Vector3(3.16f, 0.47f, -6.67f);

        #endregion

        #region Service state and ports

        /// <summary>
        /// Service state
        /// </summary>
        [ServiceState]
        [InitialStatePartner(Optional = true, ServiceUri = ServicePaths.Store + "/MulticopterSimulation.config.xml")]
        MulticopterSimulationState _state = new MulticopterSimulationState();

        /// <summary>
        /// Main service port
        /// </summary>
        [ServicePort("/MulticopterSimulation", AllowMultipleInstances = true)]
        MulticopterSimulationOperations _mainPort = new MulticopterSimulationOperations();

        /// <summary>
        /// Subscription manager port
        /// </summary>
        [SubscriptionManagerPartner]
        submgr.SubscriptionManagerPort _submgrPort = new submgr.SubscriptionManagerPort();

        #endregion

        #region Service partners

        /// <summary>
        /// SimulationEngine partner
        /// </summary>
        /// <remarks>
        /// This partner definition is only to start the simulation engine
        /// </remarks>
        [Partner("SimulationEngine", Contract = engineproxy.Contract.Identifier, CreationPolicy = PartnerCreationPolicy.UseExistingOrCreate)]
        engineproxy.SimulationEnginePort _simulationEnginePort = new engineproxy.SimulationEnginePort();

        /// <summary>
        /// XInputGamePad partner
        /// </summary>
        [Partner("XInputGamePad", Contract = xinput.Contract.Identifier, CreationPolicy = PartnerCreationPolicy.UseExistingOrCreate)]
        xinput.XInputGamepadOperations _xinputPort = new xinput.XInputGamepadOperations();
        xinput.XInputGamepadOperations _xinputNotify = new xinput.XInputGamepadOperations();

        #endregion

        #region Fields

        /// <summary>
        /// The simulated multicopter entity
        /// </summary>
        private MulticopterEntity _multicopterEntity;

        // The camera entities
        private CameraView _cameraView;
        private PursuitCameraEntity _pursuitCameraEntity;

        // Window handles
        WindowControl _windowControl;
        WindowSimulationSettings _windowSimulationSettings;
        WindowStatistics _windowStatistics;
        WindowPIDTuning _windowPIDTuning;

        // Window event ports
        WindowControl.WindowControlEvents _windowControlEventsPort = new WindowControl.WindowControlEvents();
        WindowSimulationSettings.WindowSimulationSettingsEvents _windowSimulationSettingsEventsPort = new WindowSimulationSettings.WindowSimulationSettingsEvents();
        WindowStatistics.WindowStatisticsEvents _windowStatisticsEventsPort = new WindowStatistics.WindowStatisticsEvents();
        WindowPIDTuning.WindowPIDTuningEvents _windowPIDTuningEventsPort = new WindowPIDTuning.WindowPIDTuningEvents();

        #endregion

        /// <summary>
        /// Service constructor
        /// </summary>
        public MulticopterSimulationService(DsspServiceCreationPort creationPort)
            : base(creationPort)
        {
        }

        /// <summary>
        /// Service start
        /// </summary>
        protected override void Start()
        {
            // The initial state is optional, so we must be prepared to create a new state if there is none
            if (_state == null)
            {
                _state = new MulticopterSimulationState();

                // TODO Initial state definieren, Methode implementieren
                // CreateInitialState();
            }

            SetupDropHandler();

            // TODO Globals setup eventuell eine eigene Methode?
            // Setup globals
            Globals.GlobalWinFormsServicePort = WinFormsServicePort;

            // InitializePlot windows
            SetupWindowControl();
            SetupWindowSimulationSettings();
            SetupWindowStatistics();
            SetupWindowPIDTuning();

            // InitializePlot the environment
            SimulatorAddMainCamera();
            SimulatorAddMulticopterEntity(INITIAL_MULTICOPTER_TYPE, INITIAL_PROPELLER_INCLINATION);
            SimulatorAddPursuitCamera();

            // Connect with the game controller
            SetupXInputController();

            base.Start();
        }

        #region Helper methods

        /// <summary>
        /// Setup a drop handler which saves the current state
        /// </summary>
        private void SetupDropHandler()
        {
            // TODO Arbiter.Interleave?
            Activate(
                Arbiter.Receive<DsspDefaultDrop>(false, _mainPort, DropHandler)
            );
        }

        /// <summary>
        /// Establish a connection with the XInput controller
        /// </summary>
        private void SetupXInputController()
        {
            // Connect with the XInputController service
            _xinputPort.Subscribe(_xinputNotify);

            // TODO Arbiter.Interleave?
            // Start handlers handling the different inputs of the controller
            Activate<ITask>(
                Arbiter.Receive<xinput.ButtonsChanged>(true, _xinputNotify, OnGamePadButtonsChanged),
                Arbiter.Receive<xinput.ThumbsticksChanged>(true, _xinputNotify, OnGamePadThumbsticksChanged),
                Arbiter.Receive<xinput.DPadChanged>(true, _xinputNotify, OnGamePadDPadChanged),
                Arbiter.Receive<xinput.TriggersChanged>(true, _xinputNotify, OnGamePadTriggerChanged)
            );
        }

        /// <summary>
        /// Start the control window
        /// </summary>
        private void SetupWindowControl()
        {
            // TODO Arbiter.Interleave?
            // Start handlers handling the different window events allowing the communication between simulator and window
            Activate<ITask>(
                Arbiter.ReceiveWithIterator<OnLoad>(true, _windowControlEventsPort, OnWindowControlLoadHandler),
                Arbiter.Receive<OnClosed>(true, _windowControlEventsPort, OnWindowControlClosedHandler),
                Arbiter.Receive<WindowControl.OnVirtualJoystickChanged>(true, _windowControlEventsPort, OnWindowControlVirtualJoystickHandler),
                Arbiter.Receive<WindowControl.OnAction>(true, _windowControlEventsPort, OnWindowControlActionHandler),
                Arbiter.Receive<WindowControl.OnValueChanged>(true, _windowControlEventsPort, OnWindowControlValueChangedHandler),
                Arbiter.Receive<WindowControl.OnUpdateDisplay>(true, _windowControlEventsPort, OnWindowControlUpdateDisplayHandler)
            );

            // Start the control window
            WinFormsServicePort.Post(new RunForm(CreateWindowControl));
        }

        /// <summary>
        /// Start the simulation settings window
        /// </summary>
        private void SetupWindowSimulationSettings()
        {
            // TODO Arbiter.Interleave?
            // Start handlers handling the different window events allowing the communication between simulator and window
            Activate<ITask>(
                Arbiter.ReceiveWithIterator<OnLoad>(true, _windowSimulationSettingsEventsPort, OnWindowSimulationSettingsLoadHandler),
                Arbiter.Receive<OnClosed>(true, _windowSimulationSettingsEventsPort, OnWindowSimulationSettingsCloseHandler),
                Arbiter.Receive<WindowSimulationSettings.OnAction>(true, _windowSimulationSettingsEventsPort, OnWindowSimulationActionHandler),
                Arbiter.Receive<WindowSimulationSettings.OnWindSettingsChanged>(true, _windowSimulationSettingsEventsPort, OnWindowSimulationWindSettingsChangedHandler)
            );

            // Start the simulation settings window
            WinFormsServicePort.Post(new RunForm(CreateWindowSimulationSettings));
        }

        /// <summary>
        /// Start the statistics window
        /// </summary>
        private void SetupWindowStatistics()
        {
            // TODO Arbiter.Interleave?
            // Start handlers handling the different window events allowing the communication between simulator and window
            Activate<ITask>(
                Arbiter.ReceiveWithIterator<OnLoad>(true, _windowStatisticsEventsPort, OnWindowStatisticsLoadHandler),
                Arbiter.Receive<OnClosed>(true, _windowStatisticsEventsPort, OnWindowStatisticsClosedHandler),
                Arbiter.Receive<WindowStatistics.OnPlotValue>(true, _windowStatisticsEventsPort, OnWindowStatisticsPlotValueHandler),
                Arbiter.Receive<WindowStatistics.OnDisplayUpdate>(true, _windowStatisticsEventsPort, OnWindowStatisticsDisplayUpdateHandler),
                Arbiter.Receive<WindowStatistics.OnSingleDisplayUpdate>(true, _windowStatisticsEventsPort, OnWindowStatisticsSingleDisplayUpdateHandler)
            );

            // Start the statistics window
            WinFormsServicePort.Post(new RunForm(CreateWindowStatistics));
        }

        /// <summary>
        /// Start the PID controller tuning window
        /// </summary>
        private void SetupWindowPIDTuning()
        {
            // TODO Arbiter.Interleave?
            // Start handlers handling the different window events allowing the communication between simulator and window
            Activate<ITask>(
                Arbiter.ReceiveWithIterator<OnLoad>(true, _windowPIDTuningEventsPort, OnWindowPIDTuningLoadHandler),
                Arbiter.Receive<OnClosed>(true, _windowPIDTuningEventsPort, OnWindowPIDTuningClosedHandler),
                Arbiter.Receive<WindowPIDTuning.OnPIDValueChanged>(true, _windowPIDTuningEventsPort, OnWindowPIDTuningPIDValueChangedHandler)
            );

            // Start the PID controller window
            WinFormsServicePort.Post(new RunForm(CreateWindowPIDTuning));
        }

        /// <summary>
        /// Adds a multicopter to the simulation environment
        /// </summary>
        /// <param name="position">The initial start position</param>
        private void SimulatorAddMulticopterEntity(MulticopterType multicopterType, float propellerInclination)
        {
            _multicopterEntity = new MulticopterEntity(multicopterType, propellerInclination, INITIAL_MULTICOPTER_POSITION);
            _multicopterEntity.cameraView = _cameraView;
            _multicopterEntity.windowStatisticsEventsPort = _windowStatisticsEventsPort;
            _multicopterEntity.windowControlEventsPort = _windowControlEventsPort;

            SimulationEngine.GlobalInstancePort.Insert(_multicopterEntity);
        }

        /// <summary>
        /// InitializePlot and frameUpdate the main camera of the simulation
        /// </summary>
        private void SimulatorAddMainCamera()
        {
            _cameraView = new CameraView();

            // Set up initial view
            _cameraView.CameraName = "Main camera";
            _cameraView.EyePosition = INITIAL_CAMERA_EYEPOS;
            _cameraView.LookAtPoint = INITIAL_CAMERA_LOOKAT;

            SimulationEngine.GlobalInstancePort.Update(_cameraView);
        }

        /// <summary>
        /// Adds a pursuit camer to the simulation environment, following the multicopter entity during flight
        /// </summary>
        private void SimulatorAddPursuitCamera()
        {
            // Bind the pursuit camera to the multicopter
            _pursuitCameraEntity = new PursuitCameraEntity(_multicopterEntity.State.Name);
            _pursuitCameraEntity.State.Name = "Pursuit camera";

            SimulationEngine.GlobalInstancePort.Insert(_pursuitCameraEntity);
        }

        #endregion

        #region Service Handler

        /// <summary>
        /// A drop handler which saves the current state and quits the program
        /// </summary>
        /// <param name="drop">The drop informations</param>
        [ServiceHandler(ServiceHandlerBehavior.Teardown)]
        public void DropHandler(DsspDefaultDrop drop)
        {
            // Collect the state informations and write them in the state
            _state.WindowControlX = _windowControl.Location.X;
            _state.WindowControlY = _windowControl.Location.Y;
            _state.WindowPIDTuningX = _windowPIDTuning.Location.X;
            _state.WindowPIDTuningY = _windowPIDTuning.Location.Y;
            _state.WindowSimulationSettingsX = _windowSimulationSettings.Location.X;
            _state.WindowSimulationSettingsY = _windowSimulationSettings.Location.Y;
            _state.WindowStatisticsX = _windowStatistics.Location.X;
            _state.WindowStatisticsY = _windowStatistics.Location.Y;

            _state.AttitudeP = _multicopterEntity.pidControllerAttitudeX.Kp;
            _state.AttitudeI = _multicopterEntity.pidControllerAttitudeX.Ki;
            _state.AttitudeD = _multicopterEntity.pidControllerAttitudeX.Kd;
            _state.AltitudeP = _multicopterEntity.pidControllerAltitude.Kp;
            _state.AltitudeI = _multicopterEntity.pidControllerAltitude.Ki;
            _state.AltitudeD = _multicopterEntity.pidControllerAltitude.Kd;
            _state.PositionP = _multicopterEntity.pidControllerPositionX.Kp;
            _state.PositionI = _multicopterEntity.pidControllerPositionX.Ki;
            _state.PositionD = _multicopterEntity.pidControllerPositionX.Kd;

            _state.WindSimulationActivated = _multicopterEntity.windSimulationActivated;
            _state.WindDirection = _multicopterEntity.settingsWindDirection;
            _state.WindIntensity = _multicopterEntity.settingsWindIntensity;
            _state.WindDirectionFluctuation = _multicopterEntity.settingsWindDirectionFluctuation;
            _state.WindIntensityFluctuation = _multicopterEntity.settingsWindIntensityFluctuation;

            _state.YScale = _windowStatistics.YScale;

            // Save the state as a file
            base.SaveState(_state);

            // Quit the program
            base.DefaultDropHandler(drop);
        }

        /// <summary>
        /// Handles Subscribe messages
        /// </summary>
        /// <param name="subscribe">The subscribe request</param>
        [ServiceHandler]
        public void SubscribeHandler(Subscribe subscribe)
        {
            // InitializePlot the subscribe handler
            SubscribeHelper(_submgrPort, subscribe.Body, subscribe.ResponsePort);
        }

        #endregion

        #region XInputController handler

        /// <summary>
        /// Handles state changes of buttons of the controller
        /// </summary>
        /// <param name="buttons">The button states of the controller</param>
        void OnGamePadButtonsChanged(xinput.ButtonsChanged buttons)
        {
            // If the A-Button is pressed toggle the attitude stabilization
            if (buttons.Body.A)
            {
                Globals.GlobalWinFormsServicePort.FormInvoke(
                    delegate()
                    {
                        _windowControl.ToggleCheckBoxKeepAttitude();
                    }
                );
            }

            // If the B-Button is pressed toggle the attitude stabilization
            if (buttons.Body.B)
            {
                Globals.GlobalWinFormsServicePort.FormInvoke(
                    delegate()
                    {
                        _windowControl.ToggleCheckBoxKeepAltitude();
                    }
                );
            }

            if (buttons.Body.X)
            {
            }

            if (buttons.Body.Y)
            {
            }

            // If the LS-Button is clicked start zooming the camera in
            if (buttons.Body.LeftShoulder)
            {
                _pursuitCameraEntity.zoomChange = 0.01f;
            }
            // If the RS-Button ist clicked start zooming the camera out
            else if (buttons.Body.RightShoulder)
            {
                _pursuitCameraEntity.zoomChange = -0.01f;
            }
            // If none of the Shoulder buttons is pressed stop zooming
            else
            {
                _pursuitCameraEntity.zoomChange = 0.0f;
            }

            if (buttons.Body.LeftStick)
            {                
            }

            if (buttons.Body.RightStick)
            {   
            }

            // If the Reset button is clicked reset the multicopter
            if (buttons.Body.Start)
            {
                _multicopterEntity.Reset();
            }

            // If the Back button is clicked change the camera mode
            if (buttons.Body.Back)
            {
                _windowControlEventsPort.Post(new WindowControl.OnAction(WindowControl.OnActionActions.ACTION_TOGGLE_CAMERAMODE));
            }
        }

        /// <summary>
        /// Handles state changes of two thumbsticks of the controller
        /// </summary>
        /// <param name="thumbsticks">The state of the thumbsticks of the controller</param>
        void OnGamePadThumbsticksChanged(xinput.ThumbsticksChanged thumbsticks)
        {
            // The thrust of the multicopter is controlled by the y-axis of the left controller stick
            _multicopterEntity.raw_leftY = thumbsticks.Body.LeftY;

            // The yaw (turning the multicopter clockwise or opposite) is controlled by the x-axis of the left stick
            _multicopterEntity.raw_leftX = thumbsticks.Body.LeftX;

            // The pitch (allowing the multicopter to move forward and backward) is controlled by the y-axis of the right stick
            _multicopterEntity.raw_rightY = thumbsticks.Body.RightY;

            // The roll (allowing the multicopter to move sideways) is controlled by the x-axis of the right stick
            _multicopterEntity.raw_rightX = thumbsticks.Body.RightX;

            // Update the virtual joystick with the values from the controller
            WinFormsServicePort.FormInvoke(
                delegate
                {
                    _windowControl.DrawJoystickLeft(thumbsticks.Body.LeftX, thumbsticks.Body.LeftY);
                    _windowControl.DrawJoystickRight(thumbsticks.Body.RightX, thumbsticks.Body.RightY);
                }
            );
        }

        /// <summary>
        /// Handles state changes of DPad of the controller
        /// </summary>
        /// <param name="dpad">The state of the DPad of the controller</param>
        void OnGamePadDPadChanged(xinput.DPadChanged dpad)
        {
            // Start rotating camera left
            if (dpad.Body.Left)
            {
                _pursuitCameraEntity.angleChange = 0.02f;
            }
            // Start rotating camera right
            else if (dpad.Body.Right)
            {
              
                _pursuitCameraEntity.angleChange = -0.02f;
            }
            // Stop rotating the camera
            else
            {
                _pursuitCameraEntity.angleChange = 0.0f;
            }

            // If the up direction is pressed start raising the altitude of the camera
            if (dpad.Body.Up)
            {
                _pursuitCameraEntity.altitudeChange = 0.02f;
            }
            // If the down direction is pressed start lowering the altitude of the camera
            else if (dpad.Body.Down)
            {
                _pursuitCameraEntity.altitudeChange = -0.02f;
            }
            // If neither up or down direction is pressed stop moving the camera
            else
            {
                _pursuitCameraEntity.altitudeChange = 0.0f;
            }
        }

        /// <summary>
        /// Handles state changes of two triggers of the controller
        /// </summary>
        /// <param name="triggers">The state of the triggers of the controller</param>
        void OnGamePadTriggerChanged(xinput.TriggersChanged triggers)
        {
            string debugMessage = "[GamePad] ";

            debugMessage += "Left Trigger: " + triggers.Body.Left + " ";
            debugMessage += "Right Trigger: " + triggers.Body.Right + " ";

            LogInfo(debugMessage);
        }

        #endregion

        #region Control window handler

        /// <summary>
        /// OnLoad Handler called when the control window loaded
        /// </summary>
        /// <param name="onLoad">Details about the loaded form</param>
        /// <returns>Iterators</returns>
        IEnumerator<ITask> OnWindowControlLoadHandler(OnLoad onLoad)
        {
            // Save a handle to the form
            _windowControl = (WindowControl)onLoad.Form;

            // Load saved window position
            Globals.GlobalWinFormsServicePort.FormInvoke(
                delegate()
                {
                    _windowControl.SetDesktopLocation(_state.WindowControlX, _state.WindowControlY);
                }
            );       

            LogInfo("Control window loaded");

            yield break;
        }

        /// <summary>
        /// OnClose Handler called when the control window is closed
        /// </summary>
        /// <param name="onClosed">Details about the closed form</param>
        void OnWindowControlClosedHandler(OnClosed onClosed)
        {
            if (onClosed.Form == _windowControl)
            {
                LogInfo("Control window closed");

                // Send a drop message to ourselves
                //_mainPort.Post(new DsspDefaultDrop(DropRequestType.Instance));
            }
        }

        /// <summary>
        /// Handler called when one the virtual joystick is moved
        /// </summary>
        /// <param name="onVirtualJoystickChanged">The details about the moved joystick</param>
        void OnWindowControlVirtualJoystickHandler(WindowControl.OnVirtualJoystickChanged onVirtualJoystickChanged)
        {
            // The yaw (turning the multicopter clockwise or opposite) is controlled by the x-axis of the left stick
            _multicopterEntity.raw_leftX = onVirtualJoystickChanged.LeftX;

            // The thrust of the multicopter is controlled by the y-axis of the left controller stick
            _multicopterEntity.raw_leftY = onVirtualJoystickChanged.LeftY;

            // The roll (allowing the multicopter to move sideways) is controlled by the x-axis of the right stick
            _multicopterEntity.raw_rightX = onVirtualJoystickChanged.RightX;

            // The pitch (allowing the multicopter to move forward and backward) is controlled by the y-axis of the right stick
            _multicopterEntity.raw_rightY = onVirtualJoystickChanged.RightY;
        }

        /// <summary>
        /// Handler called when one particular action in the window was performed
        /// </summary>
        /// <param name="onAction"></param>
        void OnWindowControlActionHandler(WindowControl.OnAction onAction)
        {
            switch (onAction.action)
            {
                // The multicopter has to be resetted
                case WindowControl.OnActionActions.ACTION_RESET_MULTICOPTER:
                    _multicopterEntity.Reset(); 
                    break;
            }
        }

        /// <summary>
        /// Handler getting called when a value of the window has changed
        /// </summary>
        /// <param name="onValueChanged">Details about the changed value</param>
        void OnWindowControlValueChangedHandler(WindowControl.OnValueChanged onValueChanged)
        {
            switch (onValueChanged.valueType)
            {
                // If the keep attitude value was changed frameUpdate the multicopter value
                case WindowControl.OnValueChangedValueTypes.VALUETYPE_KEEPATTITUDE:
                    _multicopterEntity.keepAttitude = (bool)onValueChanged.newValue;
                    break;

                // If the keep altitude value was changed frameUpdate the multicopter value
                case WindowControl.OnValueChangedValueTypes.VALUETYPE_KEEPALTITUDE:
                    _multicopterEntity.keepAltitude = (bool)onValueChanged.newValue;
                    break;

                // If the keep position value was changed frameUpdate the multicopter value
                case WindowControl.OnValueChangedValueTypes.VALUETYPE_KEEPPOSITION:
                    _multicopterEntity.keepPosition = (bool)onValueChanged.newValue;
                    break;

                case WindowControl.OnValueChangedValueTypes.VALUETYPE_ALTITUDE:
                    _multicopterEntity.pidControllerAltitude.referenceValue = (float)onValueChanged.newValue;
                    break;

                case WindowControl.OnValueChangedValueTypes.VALUETYPE_POSITION_X:
                    _multicopterEntity.pidControllerPositionX.referenceValue = (float)onValueChanged.newValue;
                    break;

                case WindowControl.OnValueChangedValueTypes.VALUETYPE_POSITION_Z:
                    _multicopterEntity.pidControllerPositionZ.referenceValue = (float)onValueChanged.newValue;
                    break;

                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Handler getting called when the displayed stats in the window have to be updated
        /// </summary>
        /// <param name="onUpdateDisplay">The new values to be displayed</param>
        void OnWindowControlUpdateDisplayHandler(WindowControl.OnUpdateDisplay onUpdateDisplay)
        {
            Globals.GlobalWinFormsServicePort.FormInvoke(
                delegate()
                {
                    _windowControl.UpdateStats(onUpdateDisplay);
                }
            );
        }

        #endregion

        #region Simulator settings window handler

        /// <summary>
        /// OnLoad Handler called when the simulation settings window loaded
        /// </summary>
        /// <param name="onLoad">Details about the loaded form</param>
        /// <returns>Iterators</returns>
        IEnumerator<ITask> OnWindowSimulationSettingsLoadHandler(OnLoad onLoad)
        {
            // Save a handle to the form
            _windowSimulationSettings = (WindowSimulationSettings)onLoad.Form;

            // Load saved window position
            Globals.GlobalWinFormsServicePort.FormInvoke(
                delegate()
                {
                    _windowSimulationSettings.SetDesktopLocation(_state.WindowSimulationSettingsX, _state.WindowSimulationSettingsY);
                    _windowSimulationSettings.SetInitialMulticopterSettings(
                        INITIAL_MULTICOPTER_TYPE,
                        INITIAL_PROPELLER_INCLINATION
                    );
                    _windowSimulationSettings.SetInitialWindValues(
                        _state.WindSimulationActivated,
                        _state.WindDirection, 
                        _state.WindIntensity, 
                        _state.WindDirectionFluctuation, 
                        _state.WindIntensityFluctuation
                    );
                }
            );   

            LogInfo("Simulation settings window loaded");

            yield break;
        }

        /// <summary>
        /// OnClose Handler called when the simulation settings window is closed
        /// </summary>
        /// <param name="onClosed">Details about the closed form</param>
        void OnWindowSimulationSettingsCloseHandler(OnClosed onClosed)
        {
            if (onClosed.Form == _windowSimulationSettings)
            {
                LogInfo("Simulation settings closed");

                // Send a drop message to ourselves
                //_mainPort.Post(new DsspDefaultDrop(DropRequestType.Instance));
            }
        }

        /// <summary>
        /// Handler called when a particular action on the window is performed
        /// </summary>
        /// <param name="onAction">The action to be performed</param>
        void OnWindowSimulationActionHandler(WindowSimulationSettings.OnAction onAction)
        {
            if (onAction.action == WindowSimulationSettings.OnActionActions.ACTION_BUTTONCLICKED_APPLY)
            {        
                // Prepare a request to delete the new multicopter
                var delRequest = new DeleteSimulationEntity(_multicopterEntity);

                // Activate  success / failure handlers
                Activate(
                    Arbiter.Choice<DefaultDeleteResponseType, W3C.Soap.Fault>(
                        delRequest.ResponsePort,
                        // Remove was a success
                        delegate(DefaultDeleteResponseType t)
                        {
                            // Create new multicopter based on selection
                            SimulatorAddMulticopterEntity(onAction.multicopterType, onAction.propellerInclination);
                        },
                        // Remove failed
                        delegate(W3C.Soap.Fault f)
                        {
                            throw new NotImplementedException();
                        }
                    )
                );

                // Remove the old multicopter
                SimulationEngine.GlobalInstancePort.Post(delRequest);
            }
        }

        /// <summary>
        /// Handler called when the wind settings were changed
        /// </summary>
        /// <param name="onWindSettingsChanged">The new wind settings</param>
        void OnWindowSimulationWindSettingsChangedHandler(WindowSimulationSettings.OnWindSettingsChanged onWindSettingsChanged)
        {
            // Apply the wind settings to the multicopter
            _multicopterEntity.settingsWindDirection = onWindSettingsChanged.windDirection;
            _multicopterEntity.settingsWindIntensity = onWindSettingsChanged.windIntensity;
            _multicopterEntity.settingsWindDirectionFluctuation = onWindSettingsChanged.windDirectionFluctuation;
            _multicopterEntity.settingsWindIntensityFluctuation = onWindSettingsChanged.windIntensityFluctuation;
            _multicopterEntity.windSimulationActivated = onWindSettingsChanged.enableWindSimulation;
        }

        #endregion

        #region Statistics window handler

        /// <summary>
        /// OnLoad Handler called when the statistics window loaded
        /// </summary>
        /// <param name="onLoad">Details about the loaded form</param>
        /// <returns>Iterators</returns>
        private IEnumerator<ITask> OnWindowStatisticsLoadHandler(OnLoad onLoad)
        {
            // Save a handle to the form
            _windowStatistics = (WindowStatistics)onLoad.Form;

            // Load saved window position
            Globals.GlobalWinFormsServicePort.FormInvoke(
                delegate()
                {
                    _windowStatistics.SetDesktopLocation(_state.WindowStatisticsX, _state.WindowStatisticsY);
                }
            ); 

            LogInfo("Statistics window form loaded");

            yield break;
        }

        /// <summary>
        /// OnClose Handler called when the statistics window is closed
        /// </summary>
        /// <param name="onClosed">Details about the closed form</param>
        private void OnWindowStatisticsClosedHandler(OnClosed onClosed)
        {
            if (onClosed.Form == _windowStatistics)
            {
                LogInfo("Statistics window closed");

                // Send a drop message to ourselves
                _mainPort.Post(new DsspDefaultDrop(DropRequestType.Instance));
            }
        }

        /// <summary>
        /// Gets called when there is a new value to be plotted in the function plot
        /// </summary>
        /// <param name="onPlotValue">Details about the value to be plotted</param>
        private void OnWindowStatisticsPlotValueHandler(WindowStatistics.OnPlotValue onPlotValue)
        {
            if (_windowStatistics != null)
            {
                Globals.GlobalWinFormsServicePort.FormInvoke(
                    delegate()
                    {
                        //_windowStatistics.valueToPlot = onPlotValue.value;
                    }
                );
            }
        }

        /// <summary>
        /// Gets called when the statistic display has to be updated
        /// </summary>
        /// <param name="onDisplayUpdate">The updated values to be displayed</param>
        private void OnWindowStatisticsDisplayUpdateHandler(WindowStatistics.OnDisplayUpdate onDisplayUpdate)
        {
            Globals.GlobalWinFormsServicePort.FormInvoke(
                delegate()
                {
                    _windowStatistics.onDisplayUpdate = onDisplayUpdate;
                    _windowStatistics.UpdateDisplayValues();
                }
            );
        }

        private void OnWindowStatisticsSingleDisplayUpdateHandler(WindowStatistics.OnSingleDisplayUpdate onSingleDisplayUpdate)
        {
            Globals.GlobalWinFormsServicePort.FormInvoke(
                delegate()
                {
                    _windowStatistics.SingleUpdateDisplayValues(onSingleDisplayUpdate);
                }
            );
        }

        #endregion

        #region PID tuning window handler

        /// <summary>
        /// OnLoad Handler called when the pid tuning window loaded
        /// </summary>
        /// <param name="onLoad">Details about the loaded form</param>
        /// <returns>Iterators</returns>
        IEnumerator<ITask> OnWindowPIDTuningLoadHandler(OnLoad onLoad)
        {
            // Save a handle to the form
            _windowPIDTuning = (WindowPIDTuning)onLoad.Form;

            // Load saved window position
            Globals.GlobalWinFormsServicePort.FormInvoke(
                delegate()
                {
                    _windowPIDTuning.SetDesktopLocation(_state.WindowPIDTuningX, _state.WindowPIDTuningY);
                    _windowPIDTuning.SetInitialAttitudePIDValues(_state.AttitudeP, _state.AttitudeI, _state.AttitudeD);
                    _windowPIDTuning.SetInitialAltitudePIDValues(_state.AltitudeP, _state.AltitudeI, _state.AltitudeD);
                    _windowPIDTuning.SetInitialPositionPIDValues(_state.PositionP, _state.PositionI, _state.PositionD);
                }
            );

            LogInfo("PID tuning window loaded");

            yield break;
        }

        /// <summary>
        /// OnClose Handler called when the pid tuning window is closed
        /// </summary>
        /// <param name="onClosed">Details about the closed form</param>
        void OnWindowPIDTuningClosedHandler(OnClosed onClosed)
        {
            if (onClosed.Form == _windowPIDTuning)
            {
                LogInfo("PID tuning window closed");

                // Send a drop message to ourselves
                //_mainPort.Post(new DsspDefaultDrop(DropRequestType.Instance));
            }
        }

        /// <summary>
        /// Gets called when a PID value in the status bar was changed
        /// </summary>
        /// <param name="onPIDValueChanged">The new values</param>
        void OnWindowPIDTuningPIDValueChangedHandler(WindowPIDTuning.OnPIDValueChanged onPIDValueChanged)
        {
            switch (onPIDValueChanged.pidChangedType)
            {
                // Update the PID values for the attitude PID controller
                case WindowPIDTuning.OnPIDChangedType.TYPE_ATTITUDE:
                    _multicopterEntity.pidControllerAttitudeX.Kp = onPIDValueChanged.newP;
                    _multicopterEntity.pidControllerAttitudeX.Ki = onPIDValueChanged.newI;
                    _multicopterEntity.pidControllerAttitudeX.Kd = onPIDValueChanged.newD;
                    _multicopterEntity.pidControllerAttitudeZ.Kp = onPIDValueChanged.newP;
                    _multicopterEntity.pidControllerAttitudeZ.Ki = onPIDValueChanged.newI;
                    _multicopterEntity.pidControllerAttitudeZ.Kd = onPIDValueChanged.newD;
                    break;

                // Update the PID values for the altitude PID controller
                case WindowPIDTuning.OnPIDChangedType.TYPE_ALTITUDE:
                    _multicopterEntity.pidControllerAltitude.Kp = onPIDValueChanged.newP;
                    _multicopterEntity.pidControllerAltitude.Ki = onPIDValueChanged.newI;
                    _multicopterEntity.pidControllerAltitude.Kd = onPIDValueChanged.newD;
                    break;

                // Update the PID values for the position PID controller
                case WindowPIDTuning.OnPIDChangedType.TYPE_POSITION:
                    _multicopterEntity.pidControllerPositionX.Kp = onPIDValueChanged.newP;
                    _multicopterEntity.pidControllerPositionX.Ki = onPIDValueChanged.newI;
                    _multicopterEntity.pidControllerPositionX.Kd = onPIDValueChanged.newD;
                    _multicopterEntity.pidControllerPositionZ.Kp = onPIDValueChanged.newP;
                    _multicopterEntity.pidControllerPositionZ.Ki = onPIDValueChanged.newI;
                    _multicopterEntity.pidControllerPositionZ.Kd = onPIDValueChanged.newD;
                    break;

                default:
                    throw new NotImplementedException();
            }
        }

        #endregion

        #region Window creators

        /// <summary>
        /// Function creating a new PID tuning window
        /// </summary>
        /// <returns>The new window</returns>
        private System.Windows.Forms.Form CreateWindowPIDTuning()
        {
            return new WindowPIDTuning(_windowPIDTuningEventsPort);
        }

        /// <summary>
        /// Function creating a new control window
        /// </summary>
        /// <returns>The new window</returns>
        private System.Windows.Forms.Form CreateWindowControl()
        {
            return new WindowControl(_windowControlEventsPort);
        }

        /// <summary>
        /// Function creating a new simulation settings window
        /// </summary>
        /// <returns>The new window</returns>
        private System.Windows.Forms.Form CreateWindowSimulationSettings()
        {
            return new WindowSimulationSettings(_windowSimulationSettingsEventsPort);
        }

        /// <summary>
        /// Function creating a new statistics window
        /// </summary>
        /// <returns>The new window</returns>
        private System.Windows.Forms.Form CreateWindowStatistics()
        {
            return new WindowStatistics(_windowStatisticsEventsPort);
        }

        #endregion
    }
}