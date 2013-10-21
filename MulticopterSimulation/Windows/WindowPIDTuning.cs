using Microsoft.Ccr.Core;
using System;
using System.Windows.Forms;

using xna = Microsoft.Xna.Framework;

namespace MulticopterSimulation.Windows
{
    /// <summary>
    /// Class containing an interface to the form
    /// </summary>
    public partial class WindowPIDTuning : Form
    {
        public enum OnPIDChangedType
        {
            TYPE_ATTITUDE,
            TYPE_ALTITUDE,
            TYPE_POSITION
        }

        // This is the PortSet for the different types of messages from the form
        public class WindowPIDTuningEvents : PortSet<OnLoad, OnClosed, OnPIDValueChanged>
        {
        }

        public class OnPIDValueChanged
        {
            public OnPIDChangedType pidChangedType;
            public float newP;
            public float newI;
            public float newD;
        }

        // This port is passed across to the constructor, it allows messages to be sent back to the main service
        private WindowPIDTuningEvents eventsPort;

        private OnPIDValueChanged onPIDChanged;

        /// <summary>
        /// Constructor creating a new debug window
        /// </summary>
        /// <param name="eventsPort">An event port to exchange messages</param>
        public WindowPIDTuning(WindowPIDTuningEvents eventsPort)
        {
            this.eventsPort = eventsPort;
            onPIDChanged = new OnPIDValueChanged();

            this.ControlBox = false;

            InitializeComponent();
        }

        internal void SetInitialAltitudePIDValues(float p, float i, float d)
        {
            trackBarAltitudeP.Value = (int)(p * 10.0f);
            trackBarAltitudeI.Value = (int)(i * 1000.0f);
            trackBarAltitudeD.Value = (int)(d * 10.0f);

            trackBarAltitudePID_Scroll(this, null);
        }

        internal void SetInitialAttitudePIDValues(float p, float i, float d)
        {
            trackBarAttitudeP.Value = (int)(p * 10.0f);
            trackBarAttitudeI.Value = (int)(i * 1000.0f);
            trackBarAttitudeD.Value = (int)(d * 10.0f);

            trackBarAttitudePID_Scroll(this, null);
        }

        internal void SetInitialPositionPIDValues(float p, float i, float d)
        {
            trackBarPositionP.Value = (int)(p * 10.0f);
            trackBarPositionI.Value = (int)(i * 1000.0f);
            trackBarPositionD.Value = (int)(d * 10.0f);

            trackBarPositionPID_Scroll(this, null);
        }

        /// <summary>
        /// Called, when the debug window is loaded
        /// </summary>
        private void WindowPIDTuning_Load(object sender, EventArgs e)
        {
            eventsPort.Post(new OnLoad(this));
        }

        /// <summary>
        /// Called, when the debug window is closed
        /// </summary>
        private void WindowPIDTuning_FormClosed(object sender, FormClosedEventArgs e)
        {
            eventsPort.Post(new OnClosed(this));
        }

        private void trackBarAttitudePID_Scroll(object sender, EventArgs e)
        {
            onPIDChanged.pidChangedType = OnPIDChangedType.TYPE_ATTITUDE;
            onPIDChanged.newP = trackBarAttitudeP.Value / 10.0f;
            onPIDChanged.newI = trackBarAttitudeI.Value / 1000.0f;
            onPIDChanged.newD = trackBarAttitudeD.Value / 10.0f;

            lblAttitudeP.Text = onPIDChanged.newP.ToString();
            lblAttitudeI.Text = onPIDChanged.newI.ToString();
            lblAttitudeD.Text = onPIDChanged.newD.ToString();

            eventsPort.Post(onPIDChanged);
        }

        private void trackBarAltitudePID_Scroll(object sender, EventArgs e)
        {
            onPIDChanged.pidChangedType = OnPIDChangedType.TYPE_ALTITUDE;
            onPIDChanged.newP = trackBarAltitudeP.Value / 10.0f;
            onPIDChanged.newI = trackBarAltitudeI.Value / 1000.0f;
            onPIDChanged.newD = trackBarAltitudeD.Value / 10.0f;

            lblAltitudeP.Text = onPIDChanged.newP.ToString();
            lblAltitudeI.Text = onPIDChanged.newI.ToString();
            lblAltitudeD.Text = onPIDChanged.newD.ToString();

            eventsPort.Post(onPIDChanged);
        }

        private void trackBarPositionPID_Scroll(object sender, EventArgs e)
        {
            onPIDChanged.pidChangedType = OnPIDChangedType.TYPE_POSITION;
            onPIDChanged.newP = trackBarPositionP.Value / 10.0f;
            onPIDChanged.newI = trackBarPositionI.Value / 1000.0f;
            onPIDChanged.newD = trackBarPositionD.Value / 10.0f;

            lblPositionP.Text = onPIDChanged.newP.ToString();
            lblPositionI.Text = onPIDChanged.newI.ToString();
            lblPositionD.Text = onPIDChanged.newD.ToString();

            eventsPort.Post(onPIDChanged);
        }
    }
}
