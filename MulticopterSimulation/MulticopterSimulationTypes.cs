using Microsoft.Ccr.Core;
using Microsoft.Dss.Core.Attributes;
using Microsoft.Dss.ServiceModel.Dssp;
using W3C.Soap;

namespace MulticopterSimulation
{
    /// <summary>
    /// MulticopterSimulation contract class
    /// </summary>
    public sealed class Contract
    {
        /// <summary>
        /// DSS contract identifer for MulticopterSimulation
        /// </summary>
        [DataMember]
        public const string Identifier = "http://simulator.archaeocopter.de/2013/05/multicoptersimulation.html";
    }

    /// <summary>
    /// MulticopterSimulation state
    /// </summary>
    [DataContract]
    public class MulticopterSimulationState
    {
        private int _windowControlX, _windowControlY;
        private int _windowSimulationSettingsX, _windowsSimulationSettingsY;
        private int _windowStatisticsX, _windowStatisticsY;
        private int _windowPIDTuningX, _windowPIDTuningY;

        private float _attitudeP, _attitudeI, _attitudeD;
        private float _altitudeP, _altitudeI, _altitudeD;
        private float _positionP, _positionI, _positionD;

        private bool _windSimulationActivated;
        private float _windDirection, _windIntensity;
        private float _windDirectionFluctuation, _windIntensityFluctuation;

        private float _yscale;

        [DataMember]
        public int WindowControlX
        {
            get { return _windowControlX; }
            set { _windowControlX = value; }
        }

        [DataMember]
        public int WindowControlY
        {
            get { return _windowControlY; }
            set { _windowControlY = value; }
        }

        [DataMember]
        public int WindowSimulationSettingsX
        {
            get { return _windowSimulationSettingsX; }
            set { _windowSimulationSettingsX = value; }
        }

        [DataMember]
        public int WindowSimulationSettingsY
        {
            get { return _windowsSimulationSettingsY; }
            set { _windowsSimulationSettingsY = value; }
        }

        [DataMember]
        public int WindowStatisticsX
        {
            get { return _windowStatisticsX; }
            set { _windowStatisticsX = value; }
        }

        [DataMember]
        public int WindowStatisticsY
        {
            get { return _windowStatisticsY; }
            set { _windowStatisticsY = value; }
        }

        [DataMember]
        public int WindowPIDTuningX
        {
            get { return _windowPIDTuningX; }
            set { _windowPIDTuningX = value; }
        }

        [DataMember]
        public int WindowPIDTuningY
        {
            get { return _windowPIDTuningY; }
            set { _windowPIDTuningY = value; }
        }

        [DataMember]
        public float AttitudeP
        {
            get { return _attitudeP; }
            set { _attitudeP = value; }
        }

        [DataMember]
        public float AttitudeI
        {
            get { return _attitudeI; }
            set { _attitudeI = value; }
        }

        [DataMember]
        public float AttitudeD
        {
            get { return _attitudeD; }
            set { _attitudeD = value; }
        }

        [DataMember]
        public float AltitudeP
        {
            get { return _altitudeP; }
            set { _altitudeP = value; }
        }

        [DataMember]
        public float AltitudeI
        {
            get { return _altitudeI; }
            set { _altitudeI = value; }
        }

        [DataMember]
        public float AltitudeD
        {
            get { return _altitudeD; }
            set { _altitudeD = value; }
        }

        [DataMember]
        public float PositionP
        {
            get { return _positionP; }
            set { _positionP = value; }
        }

        [DataMember]
        public float PositionI
        {
            get { return _positionI; }
            set { _positionI = value; }
        }

        [DataMember]
        public float PositionD
        {
            get { return _positionD; }
            set { _positionD = value; }
        }

        [DataMember]
        public bool WindSimulationActivated
        {
            get { return _windSimulationActivated; }
            set { _windSimulationActivated = value; }
        }

        [DataMember]
        public float WindDirection
        {
            get { return _windDirection; }
            set { _windDirection = value; }
        }

        [DataMember]
        public float WindIntensity
        {
            get { return _windIntensity; }
            set { _windIntensity = value; }
        }

        [DataMember]
        public float WindDirectionFluctuation
        {
            get { return _windDirectionFluctuation; }
            set { _windDirectionFluctuation = value; }
        }

        [DataMember]
        public float WindIntensityFluctuation
        {
            get { return _windIntensityFluctuation; }
            set { _windIntensityFluctuation = value; }
        }

        [DataMember]
        public float YScale
        {
            get { return _yscale; }
            set { _yscale = value; }
        }
    }

    /// <summary>
    /// MulticopterSimulation main operations port
    /// </summary>
    [ServicePort]
    public class MulticopterSimulationOperations : PortSet<DsspDefaultLookup, DsspDefaultDrop, Get, Subscribe>
    {
    }

    /// <summary>
    /// MulticopterSimulation get operation
    /// </summary>
    public class Get : Get<GetRequestType, PortSet<MulticopterSimulationState, Fault>>
    {
        /// <summary>
        /// Creates a new instance of Get
        /// </summary>
        public Get()
        {
        }

        /// <summary>
        /// Creates a new instance of Get
        /// </summary>
        /// <param name="body">the request message body</param>
        public Get(GetRequestType body)
            : base(body)
        {
        }

        /// <summary>
        /// Creates a new instance of Get
        /// </summary>
        /// <param name="body">the request message body</param>
        /// <param name="responsePort">the response port for the request</param>
        public Get(GetRequestType body, PortSet<MulticopterSimulationState, Fault> responsePort)
            : base(body, responsePort)
        {
        }
    }

    /// <summary>
    /// MulticopterSimulation subscribe operation
    /// </summary>
    public class Subscribe : Subscribe<SubscribeRequestType, PortSet<SubscribeResponseType, Fault>>
    {
        /// <summary>
        /// Creates a new instance of Subscribe
        /// </summary>
        public Subscribe()
        {
        }

        /// <summary>
        /// Creates a new instance of Subscribe
        /// </summary>
        /// <param name="body">the request message body</param>
        public Subscribe(SubscribeRequestType body)
            : base(body)
        {
        }

        /// <summary>
        /// Creates a new instance of Subscribe
        /// </summary>
        /// <param name="body">the request message body</param>
        /// <param name="responsePort">the response port for the request</param>
        public Subscribe(SubscribeRequestType body, PortSet<SubscribeResponseType, Fault> responsePort)
            : base(body, responsePort)
        {
        }
    }
}


