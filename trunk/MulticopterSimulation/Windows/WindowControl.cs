using Microsoft.Ccr.Core;
using Microsoft.Robotics.PhysicalModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MulticopterSimulation.Windows
{
    public partial class WindowControl : Form
    {
        // TODO Es sollte möglich sein mehrere Tasten zu drücken, benutze Polling wie beim XBox-Controller

        public enum OnActionActions
        {
            ACTION_TOGGLE_CAMERAMODE,
            ACTION_RESET_MULTICOPTER,
        }

        public enum OnValueChangedValueTypes
        {
            VALUETYPE_KEEPATTITUDE,
            VALUETYPE_KEEPALTITUDE,
            VALUETYPE_KEEPPOSITION,
            VALUETYPE_ALTITUDE,
            VALUETYPE_POSITION_X,
            VALUETYPE_POSITION_Z
        }

        // This is the PortSet for the different types of messages from the form
        public class WindowControlEvents : PortSet<
            OnLoad, OnClosed, OnVirtualJoystickChanged, OnAction, OnValueChanged, OnUpdateDisplay>
        {
        }

        public class OnVirtualJoystickChanged
        {
            float _leftX;

            public float LeftX
            {
                get { return _leftX; }
                set { _leftX = value; }
            }

            float _leftY;

            public float LeftY
            {
                get { return _leftY; }
                set { _leftY = value; }
            }

            float _rightX;

            public float RightX
            {
                get { return _rightX; }
                set { _rightX = value; }
            }

            float _rightY;

            public float RightY
            {
                get { return _rightY; }
                set { _rightY = value; }
            }

            public OnVirtualJoystickChanged(Form form, float leftX, float leftY, float rightX, float rightY)
            {
                _leftX = leftX;
                _leftY = leftY;
                _rightX = rightX;
                _rightY = rightY;
            }
        }

        public class OnAction
        {
            public OnActionActions action;

            public OnAction(OnActionActions action)
            {
                this.action = action;
            }
        }

        public class OnValueChanged
        {
            public OnValueChangedValueTypes valueType;
            public object newValue;

            public OnValueChanged(OnValueChangedValueTypes valueType, object newValue)
            {
                this.valueType = valueType;
                this.newValue = newValue;
            }
        }

        public class OnUpdateDisplay
        {
            public float thrust_ratio;
            public float thrust;
            public float altitude;
            public Vector3 speedVector;
        }
        
        WindowControlEvents eventsPort;

        public WindowControl(WindowControlEvents eventsPort)
        {
            this.eventsPort = eventsPort;

            this.ControlBox = false;

            InitializeComponent();
        }

        public void DrawJoystickLeft(float x, float y)
        {
            DrawJoystick(x, y, picJoystickLeft);
        }

        public void DrawJoystickRight(float x, float y)
        {
            DrawJoystick(x, y, picJoystickRight);
        }

        private void DrawJoystick(float x, float y, PictureBox picJoystick)
        {
            Bitmap bmp = new Bitmap(picJoystick.Width, picJoystick.Height);
            Graphics g = Graphics.FromImage(bmp);

            int width = bmp.Width - 1;
            int height = bmp.Height - 1;

            g.Clear(Color.Transparent);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(0, 0, width, height);

            PathGradientBrush pathBrush = new PathGradientBrush(path);
            pathBrush.CenterPoint = new PointF(width / 3f, height / 3f);
            pathBrush.CenterColor = Color.White;
            pathBrush.SurroundColors = new Color[] { Color.LightGray };

            g.FillPath(pathBrush, path);
            g.DrawPath(Pens.Black, path);

            int partial = (int)(y * height / 2);
            if (partial > 0)
            {
                g.DrawArc(Pens.Black,
                    0,
                    height / 2 - partial,
                    width,
                    2 * partial,
                    180,
                    180);
            }
            else if (partial == 0)
            {
                g.DrawLine(Pens.Black, 0, height / 2, width, height / 2);
            }
            else
            {
                g.DrawArc(Pens.Black,
                    0,
                    height / 2 + partial,
                    width,
                    -2 * partial,
                    0,
                    180);
            }

            partial = (int)(x * width / 2);
            if (partial > 0)
            {
                g.DrawArc(Pens.Black,
                    width / 2 - partial,
                    0,
                    2 * partial,
                    height,
                    270,
                    180);
            }
            else if (partial == 0)
            {
                g.DrawLine(Pens.Black, width / 2, 0, width / 2, height);
            }
            else
            {
                g.DrawArc(Pens.Black,
                    width / 2 + partial,
                    0,
                    -2 * partial,
                    height,
                    90,
                    180);
            }

            picJoystick.Image = bmp;
        }

        private void UpdateJoystickAxes(float x, float y, PictureBox picJoystick)
        {
            DrawJoystick(x, y, picJoystick);

            if (picJoystick == picJoystickLeft)
                eventsPort.Post(new OnVirtualJoystickChanged(this, x, y, 0.0f, 0.0f));
            else
                eventsPort.Post(new OnVirtualJoystickChanged(this, 0.0f, 0.0f, x, y));
        }

        public void UpdateStats(OnUpdateDisplay onUpdateStats)
        {
            lblThrust.Text = (onUpdateStats.thrust_ratio * 100f).ToString("N1") + "% (" + onUpdateStats.thrust.ToString("N3") + ")";
            lblHeight.Text = onUpdateStats.altitude.ToString("N1") + " m";

            float speed = 3.6f * (float)Math.Sqrt(onUpdateStats.speedVector.X * onUpdateStats.speedVector.X + onUpdateStats.speedVector.Z * onUpdateStats.speedVector.Z);
            lblSpeed.Text = speed.ToString("N1") + " kph";     
        }

        private void WindowControl_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;

            DrawJoystickLeft(0.0f, 0.0f);
            DrawJoystickRight(0.0f, 0.0f);

            eventsPort.Post(new OnLoad(this));
        }

        private void WindowControl_FormClosed(object sender, FormClosedEventArgs e)
        {
            eventsPort.Post(new OnClosed(this));
        }

        private void WindowControl_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W: UpdateJoystickAxes(0.0f, 1.0f, picJoystickLeft); break;
                case Keys.S: UpdateJoystickAxes(0.0f, -1.0f, picJoystickLeft); break;
                case Keys.A: UpdateJoystickAxes(-1.0f, 0.0f, picJoystickLeft); break;
                case Keys.D: UpdateJoystickAxes(1.0f, 0.0f, picJoystickLeft); break;

                case Keys.NumPad8: UpdateJoystickAxes(0.0f, 1.0f, picJoystickRight); break;
                case Keys.NumPad2: UpdateJoystickAxes(0.0f, -1.0f, picJoystickRight); break;
                case Keys.NumPad4: UpdateJoystickAxes(-1.0f, 0.0f, picJoystickRight); break;
                case Keys.NumPad6: UpdateJoystickAxes(1.0f, 0.0f, picJoystickRight); break;

                case Keys.Space: btnReset_Click(this, null); break;
                case Keys.C: eventsPort.Post(new OnAction(OnActionActions.ACTION_TOGGLE_CAMERAMODE)); break;
                case Keys.F1: ToggleCheckBoxKeepAttitude(); break;
                case Keys.F2: ToggleCheckBoxKeepAltitude(); break;
            }
        }

        public void ToggleCameraModes()
        {
            // TODO Toggle between the camera entities
        }

        public void ToggleCheckBoxKeepAttitude()
        {
            checkBoxKeepAttitude.Checked = !checkBoxKeepAttitude.Checked;
        }

        public void ToggleCheckBoxKeepAltitude()
        {
            checkBoxKeepAltitude.Checked = !checkBoxKeepAltitude.Checked;
        }

        public void ToggleCheckBoxKeepPosition()
        {
            checkBoxKeepPosition.Checked = !checkBoxKeepPosition.Checked;
        }

        private void WindowControl_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W: UpdateJoystickAxes(0.0f, 0.0f, picJoystickLeft); break;
                case Keys.A: UpdateJoystickAxes(0.0f, 0.0f, picJoystickLeft); break;
                case Keys.S: UpdateJoystickAxes(0.0f, 0.0f, picJoystickLeft); break;
                case Keys.D: UpdateJoystickAxes(0.0f, 0.0f, picJoystickLeft); break;

                case Keys.NumPad8: UpdateJoystickAxes(0.0f, 0.0f, picJoystickRight); break;
                case Keys.NumPad2: UpdateJoystickAxes(0.0f, 0.0f, picJoystickRight); break;
                case Keys.NumPad4: UpdateJoystickAxes(0.0f, 0.0f, picJoystickRight); break;
                case Keys.NumPad6: UpdateJoystickAxes(0.0f, 0.0f, picJoystickRight); break;
            }
        }

        private void picJoystick_MouseLeave(object sender, EventArgs e)
        {
            UpdateJoystickAxes(0.0f, 0.0f, (PictureBox)sender);
        }

        private void picJoystick_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                float x, y;
                PictureBox pbox = (PictureBox)sender;

                x = Math.Min(pbox.Width, Math.Max(e.X, 0));
                y = Math.Min(pbox.Height, Math.Max(e.Y, 0));

                x = 2 * x / (float)pbox.Width - 1.0f;
                y = -(2 * y / (float)pbox.Height - 1.0f);

                UpdateJoystickAxes(x, y, pbox);
            }
        }

        private void picJoystick_MouseUp(object sender, MouseEventArgs e)
        {
            picJoystick_MouseLeave(sender, e);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            eventsPort.Post(new OnAction(OnActionActions.ACTION_RESET_MULTICOPTER));
        }

        private void checkBoxKeepAttitude_CheckedChanged(object sender, EventArgs e)
        {
            eventsPort.Post(new OnValueChanged(OnValueChangedValueTypes.VALUETYPE_KEEPATTITUDE, checkBoxKeepAttitude.Checked));
        }

        private void checkBoxKeepAltitude_CheckedChanged(object sender, EventArgs e)
        {
            eventsPort.Post(new OnValueChanged(OnValueChangedValueTypes.VALUETYPE_KEEPALTITUDE, checkBoxKeepAltitude.Checked));
        }

        private void checkBoxKeepPosition_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxKeepAttitude.Enabled = !checkBoxKeepPosition.Checked;
            eventsPort.Post(new OnValueChanged(OnValueChangedValueTypes.VALUETYPE_KEEPPOSITION, checkBoxKeepPosition.Checked));
        }

        private void btnAltitudeOK_Click(object sender, EventArgs e)
        {
            float newAltitude;

            bool valid = float.TryParse(textBoxAltitude.Text, out newAltitude);

            if (valid)
            {
                eventsPort.Post(new OnValueChanged(OnValueChangedValueTypes.VALUETYPE_ALTITUDE, newAltitude));
            }
            else
            {
                MessageBox.Show("Please enter a valid altitude in meters!", "Invalid input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnPositionOK_Click(object sender, EventArgs e)
        {
            float newPositionX, newPositionZ;

            bool valid = float.TryParse(textBoxPositionX.Text, out newPositionX);
            valid &= float.TryParse(textBoxPositionZ.Text, out newPositionZ);

            if (valid)
            {
                eventsPort.Post(new OnValueChanged(OnValueChangedValueTypes.VALUETYPE_POSITION_X, newPositionX));
                eventsPort.Post(new OnValueChanged(OnValueChangedValueTypes.VALUETYPE_POSITION_Z, newPositionZ));
            }
            else
            {
                MessageBox.Show("Please enter position coordinates!", "Invalid input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
