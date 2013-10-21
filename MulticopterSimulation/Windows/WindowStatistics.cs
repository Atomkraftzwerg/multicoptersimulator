using Microsoft.Ccr.Core;
using Microsoft.Robotics.PhysicalModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using xna = Microsoft.Xna.Framework;

namespace MulticopterSimulation.Windows
{
    public partial class WindowStatistics : Form
    {
        // This is the PortSet for the different types of messages from the form
        public class WindowStatisticsEvents : PortSet<OnLoad, OnClosed, OnPlotValue, OnDisplayUpdate, OnSingleDisplayUpdate>
        {
        }

        public class OnPlotValue
        {
            public float value;
        }

        public class OnDisplayUpdate
        {
            public double applicationTime;
            public double frameElapsedTime;
            public xna.Vector3 position;
            public Vector3 velocity;
            public Vector3 linearForce;
            public xna.Vector3 rotation;
            public Vector3 angularVelocity;
            public Vector3 rotationTorque;
            public float thrust;
            public float pidControllerAttitudeXValue;
            public float pidControllerAttitudeZValue;
            public float pidControllerPositionXValue;
            public float pidControllerPositionZValue;
            public float pidControllerAltitudeValue;
            public float windDirection;
            public float windIntensity;
        }

        public class OnSingleDisplayUpdate
        {
            public float multicopterWeight;
        }

        public OnDisplayUpdate onDisplayUpdate;

        private float y0 = 0.0f;
        private float yScale = 50.0f;

        private bool isRecording = false;

        private Bitmap bmpComplete;
        private Bitmap bmpPlot;
        private Bitmap bmpPlotCaption;

        private Graphics gComplete;
        private Graphics gPlot;
        private Graphics gPlotCaption;

        private Queue<float> values;
        private Queue<long> pointsOfTime;

        private ToolTip toolTipPlot;

        private TimeSpan timeSpan;

        private WindowStatisticsEvents eventsPort;

        private StreamWriter sw;

        private Timer timerUpdatePlot;
        private Stopwatch stopWatch;
        private Stopwatch recorderStopWatch;

        public delegate void AddNewValueToPlot();
        AddNewValueToPlot addNewValueToPlotDelegate;

        public float YScale
        {
            get { return yScale; }
        }

        public WindowStatistics(WindowStatisticsEvents eventsPort)
        {
            this.eventsPort = eventsPort;
            values = new Queue<float>();
            pointsOfTime = new Queue<long>();
            onDisplayUpdate = new OnDisplayUpdate();

            this.ControlBox = false;

            addNewValueToPlotDelegate = AddNewNullValueToPlot;

            timerUpdatePlot = new Timer();
            timerUpdatePlot.Interval = 50;
            timerUpdatePlot.Tick += timerUpdatePlot_Tick;

            stopWatch = new Stopwatch();
            recorderStopWatch = new Stopwatch();

            InitializeComponent();
        }

        private void InitializePlot()
        {
            bmpComplete = new Bitmap(pictureBoxStats.Width, pictureBoxStats.Height);
            bmpPlot = new Bitmap(pictureBoxStats.Width, pictureBoxStats.Height);
            bmpPlotCaption = new Bitmap(pictureBoxStats.Width, pictureBoxStats.Height);

            gComplete = Graphics.FromImage(bmpComplete);
            gPlot = Graphics.FromImage(bmpPlot);
            gPlotCaption = Graphics.FromImage(bmpPlotCaption);

            gPlot.SmoothingMode = SmoothingMode.HighQuality;
            gPlotCaption.SmoothingMode = SmoothingMode.HighQuality;

            ClearPlot();

            DrawPlotScale();

            pictureBoxStats.Image = bmpComplete;

            timerUpdatePlot.Enabled = true;
            stopWatch.Start();
        }

        private void ClearPlot()
        {
            values.Clear();
            pointsOfTime.Clear();

            gPlot.Clear(Color.White);
        }

        internal void UpdateDisplayValues()
        {
            // Update application run pointsOfTime
            timeSpan = TimeSpan.FromSeconds(onDisplayUpdate.applicationTime);
            lblSimulationRuntime.Text =
                string.Format("{0:D2} h {1:D2} m {2:D2} s",
                              timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

            // Update the position display
            lblPositionX.Text = onDisplayUpdate.position.X.ToString("N3");
            lblPositionY.Text = onDisplayUpdate.position.Y.ToString("N3");
            lblPositionZ.Text = onDisplayUpdate.position.Z.ToString("N3");

            // Update the velocity display
            lblVelocityX.Text = onDisplayUpdate.velocity.X.ToString("N3");
            lblVelocityY.Text = onDisplayUpdate.velocity.Y.ToString("N3");
            lblVelocityZ.Text = onDisplayUpdate.velocity.Z.ToString("N3");

            // Update the force display
            lblForceX.Text = onDisplayUpdate.linearForce.X.ToString("N3");
            lblForceY.Text = onDisplayUpdate.linearForce.Y.ToString("N3");
            lblForceZ.Text = onDisplayUpdate.linearForce.Z.ToString("N3");

            // Update the rotation display
            lblRotationX.Text = onDisplayUpdate.rotation.X.ToString("N3");
            lblRotationY.Text = onDisplayUpdate.rotation.Y.ToString("N3");
            lblRotationZ.Text = onDisplayUpdate.rotation.Z.ToString("N3");

            // Update the angular velocity display
            lblAVelocityX.Text = onDisplayUpdate.angularVelocity.X.ToString("N3");
            lblAVelocityY.Text = onDisplayUpdate.angularVelocity.Y.ToString("N3");
            lblAVelocityZ.Text = onDisplayUpdate.angularVelocity.Z.ToString("N3");

            // Update the torque display
            lblTorqueX.Text = onDisplayUpdate.rotationTorque.X.ToString("N3");
            lblTorqueY.Text = onDisplayUpdate.rotationTorque.Y.ToString("N3");
            lblTorqueZ.Text = onDisplayUpdate.rotationTorque.Z.ToString("N3");
        }

        internal void SingleUpdateDisplayValues(OnSingleDisplayUpdate onSingleDisplayUpdate)
        {
            lblMulticopterWeight.Text = onSingleDisplayUpdate.multicopterWeight.ToString("N3") + " g";
        }

        public void AddNewPlotValue(float value)
        {
            values.Enqueue(value);
            pointsOfTime.Enqueue(stopWatch.ElapsedMilliseconds);

            if (isRecording)
            {
                sw.WriteLine(stopWatch.ElapsedMilliseconds / 1000f + "s" + "\t" + value);
                btnRecord.Text = "Stop recording (" + (30 - stopWatch.Elapsed.Seconds).ToString() + ")";
                if (stopWatch.Elapsed.Seconds >= 30)
                    btnRecord_Click(null, null);
            }
        }

        public void DrawPlot()
        {
            float yValBefore = y0;
            float transformedValue;

            gPlot.Clear(Color.Transparent);

            if (values.Count == pictureBoxStats.Width)
            {
                yValBefore = -values.Dequeue() * yScale + y0;
                pointsOfTime.Dequeue();
            }

            long lastMillis = 0;
            for (int xPos = 0; xPos < values.Count; xPos++)
            {
                // Draw time scale tick on x-axis
                if (pointsOfTime.ElementAt(xPos) % 1000 <= lastMillis % 1000)
                    gPlot.DrawLine(Pens.Black, xPos, y0 - 4, xPos, y0 + 4);
                lastMillis = pointsOfTime.ElementAt(xPos);

                // Draw value
                transformedValue = -values.ElementAt(xPos) * yScale + y0;
                gPlot.DrawLine(Pens.Red, xPos - 1, yValBefore, xPos, transformedValue);
                yValBefore = transformedValue;
            }

            gComplete.DrawImage(bmpPlotCaption, 0, 0);
            gComplete.DrawImage(bmpPlot, 0, 0);

            pictureBoxStats.Image = bmpComplete;
        }

        private void DrawPlotScale()
        {
            gPlotCaption.Clear(Color.White);
            gPlotCaption.DrawLine(Pens.Black, 0.0f, y0, bmpPlot.Width, y0);
            gPlotCaption.DrawLine(Pens.DarkGray, 0.0f, y0 - yScale, bmpPlot.Width, y0 - yScale);
            gPlotCaption.DrawLine(Pens.DarkGray, 0.0f, y0 + yScale, bmpPlot.Width, y0 + yScale);
        }

        #region Methods adding new Values to the Plot

        private void AddNewNullValueToPlot()
        {
            AddNewPlotValue(0.0f);
        }

        //Wind direction
        //Wind intensity

        private void AddNewThrustValueToPlot()
        {
            AddNewPlotValue(onDisplayUpdate.thrust);
        }

        private void AddNewPositionYValueToPlot()
        {
            // TODO Restore original
            //AddNewPlotValue(onDisplayUpdate.position.Y);
            AddNewPlotValue(Math.Abs(onDisplayUpdate.position.Y - 15.0f));
        }

        private void AddNewPositionDeviationXZValueToPlot()
        {
            AddNewPlotValue(Math.Abs(onDisplayUpdate.position.X) + Math.Abs(onDisplayUpdate.position.Z));
        }

        private void AddNewVelocityYValueToPlot()
        {
            AddNewPlotValue(onDisplayUpdate.velocity.Y);
        }

        private void AddNewVelocityXZValueToPlot()
        {
            AddNewPlotValue(Math.Abs(onDisplayUpdate.velocity.X) + Math.Abs(onDisplayUpdate.velocity.Z));
        }

        private void AddNewRotationYValueToPlot()
        {
            AddNewPlotValue(onDisplayUpdate.rotation.Y);
        }

        private void AddNewRotationDeviationXZValueToPlot()
        {
            AddNewPlotValue(Math.Abs(onDisplayUpdate.rotation.X) + Math.Abs(onDisplayUpdate.rotation.Z));
        }

        private void AddNewAngularVelocityYValueToPlot()
        {
            AddNewPlotValue(onDisplayUpdate.angularVelocity.Y);
        }

        private void AddNewAngularVelocityXZValueToPlot()
        {
            AddNewPlotValue(Math.Abs(onDisplayUpdate.angularVelocity.X) + Math.Abs(onDisplayUpdate.angularVelocity.Z));
        }

        private void AddNewPIDPositionValueToPlot()
        {
            AddNewPlotValue(onDisplayUpdate.pidControllerPositionXValue + onDisplayUpdate.pidControllerPositionZValue);
        }

        private void AddNewPIDAltitudeValueToPlot()
        {
            AddNewPlotValue(onDisplayUpdate.pidControllerAltitudeValue);
        }

        private void AddNewPIDAttitudeValueToPlot()
        {
            AddNewPlotValue(onDisplayUpdate.pidControllerAttitudeXValue + onDisplayUpdate.pidControllerAttitudeZValue);
        }

        private void AddNewWindDirectionValueToPlot()
        {
            AddNewPlotValue(onDisplayUpdate.windDirection);
        }

        private void AddNewWindIntensityValueToPlot()
        {
            AddNewPlotValue(onDisplayUpdate.windIntensity);
        }

        #endregion

        private void timerUpdatePlot_Tick(object sender, EventArgs e)
        {
            addNewValueToPlotDelegate();
            DrawPlot();
        }

        private void WindowStatistics_Load(object sender, EventArgs e)
        {
            eventsPort.Post(new OnLoad(this));

            y0 = pictureBoxStats.Height / 2.0f;
            InitializePlot();

            toolTipPlot = new ToolTip();
            toolTipPlot.AutoPopDelay = 5000;
            toolTipPlot.InitialDelay = 1000;
            toolTipPlot.ReshowDelay = 500;
        }

        private void WindowStatistics_FormClosed(object sender, FormClosedEventArgs e)
        {
            eventsPort.Post(new OnClosed(this));
        }

        private void WindowStatistics_ResizeEnd(object sender, EventArgs e)
        {
            y0 = pictureBoxStats.Height / 2.0f;

            bmpComplete = new Bitmap(pictureBoxStats.Width, pictureBoxStats.Height);
            bmpPlot = new Bitmap(pictureBoxStats.Width, pictureBoxStats.Height);
            bmpPlotCaption = new Bitmap(pictureBoxStats.Width, pictureBoxStats.Height);

            gComplete = Graphics.FromImage(bmpComplete);
            gPlot = Graphics.FromImage(bmpPlot);
            gPlotCaption = Graphics.FromImage(bmpPlotCaption);

            gPlot.SmoothingMode = SmoothingMode.HighQuality;
            gPlotCaption.SmoothingMode = SmoothingMode.HighQuality;

            DrawPlotScale();
        }

        private void trackBarYScale_Scroll(object sender, EventArgs e)
        {
            yScale = trackBarYScale.Value / 10.0f;
            DrawPlotScale();
            DrawPlot();
        }

        int oldX = 0;
        private void pictureBoxStats_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X != oldX)
            {
                string result = "";
                if (e.X < values.Count)
                {
                    result += "X: " + pointsOfTime.ElementAt(e.X) / 1000f + "s ";
                    result += "Y: " + values.ElementAt(e.X);
                }
                toolTipPlot.SetToolTip(pictureBoxStats, result);

                oldX = e.X;
            }
        }

        private void btnPlot_Click(object sender, EventArgs e)
        {
            timerUpdatePlot.Enabled = !timerUpdatePlot.Enabled;

            if (timerUpdatePlot.Enabled)
            {
                ClearPlot();
                stopWatch.Restart();
                btnPlot.Text = "Stop plotting";
            }
            else
            {
                stopWatch.Stop();
                btnPlot.Text = "Start plotting";
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ClearPlot();
            stopWatch.Restart();
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            if (!isRecording)
            {
                if (!timerUpdatePlot.Enabled)
                    btnPlot_Click(sender, e);

                recorderStopWatch.Start();

                string fileName = Globals.currentMulticopterType + "_" + Globals.currentInclinationAngle + "_"
                    + comboBoxValueToPlot.Text + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")+ ".log";
                sw = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + fileName);

                isRecording = true;
                btnRecord.Text = "Stop recording";

                btnReset_Click(sender, e);
            }

            else
            {
                isRecording = false;
                btnRecord.Text = "Start recording";

                recorderStopWatch.Stop();
                recorderStopWatch.Reset();

                sw.Close();
            }
        }

        private void trackBarUpdateInterval_Scroll(object sender, EventArgs e)
        {
            timerUpdatePlot.Interval = trackBarUpdateInterval.Value;
        }

        private void comboBoxValueToPlot_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearPlot();

            if (comboBoxValueToPlot.Text.Equals("Thrust"))
                addNewValueToPlotDelegate = AddNewThrustValueToPlot;
            else if (comboBoxValueToPlot.Text.Equals("Position Y"))
                addNewValueToPlotDelegate = AddNewPositionYValueToPlot;
            else if (comboBoxValueToPlot.Text.Equals("Position Deviation XZ"))
                addNewValueToPlotDelegate = AddNewPositionDeviationXZValueToPlot;
            else if (comboBoxValueToPlot.Text.Equals("Velocity Y"))
                addNewValueToPlotDelegate = AddNewVelocityYValueToPlot;
            else if (comboBoxValueToPlot.Text.Equals("Velocity XZ"))
                addNewValueToPlotDelegate = AddNewVelocityXZValueToPlot;
            else if (comboBoxValueToPlot.Text.Equals("Rotation Y"))
                addNewValueToPlotDelegate = AddNewRotationYValueToPlot;
            else if (comboBoxValueToPlot.Text.Equals("Rotation Deviation XZ"))
                addNewValueToPlotDelegate = AddNewRotationDeviationXZValueToPlot;
            else if (comboBoxValueToPlot.Text.Equals("Angular velocity XZ"))
                addNewValueToPlotDelegate = AddNewAngularVelocityXZValueToPlot;
            else if (comboBoxValueToPlot.Text.Equals("Angular velocity Y"))
                addNewValueToPlotDelegate = AddNewAngularVelocityYValueToPlot;
            else if (comboBoxValueToPlot.Text.Equals("PID Value Attitude"))
                addNewValueToPlotDelegate = AddNewPIDAttitudeValueToPlot;
            else if (comboBoxValueToPlot.Text.Equals("PID Value Altitude"))
                addNewValueToPlotDelegate = AddNewPIDAltitudeValueToPlot;
            else if (comboBoxValueToPlot.Text.Equals("PID Value Position"))
                addNewValueToPlotDelegate = AddNewPIDPositionValueToPlot;
            else if (comboBoxValueToPlot.Text.Equals("Wind direction"))
                addNewValueToPlotDelegate = AddNewWindDirectionValueToPlot;
            else if (comboBoxValueToPlot.Text.Equals("Wind intensity"))
                addNewValueToPlotDelegate = AddNewWindIntensityValueToPlot;
            else
                throw new NotImplementedException();
        }
    }
}
