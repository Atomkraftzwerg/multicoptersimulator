namespace MulticopterSimulation.Windows
{
    partial class WindowControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBoxKeyboard = new System.Windows.Forms.GroupBox();
            this.lblDescKeyboardHint = new System.Windows.Forms.Label();
            this.lblDescCamera = new System.Windows.Forms.Label();
            this.lblDescCameraKeys = new System.Windows.Forms.Label();
            this.lblDescReset = new System.Windows.Forms.Label();
            this.lblDescResetKeys = new System.Windows.Forms.Label();
            this.lblDescPitch = new System.Windows.Forms.Label();
            this.lblDescPitchKeys = new System.Windows.Forms.Label();
            this.lblDescRoll = new System.Windows.Forms.Label();
            this.lblDescRollKeys = new System.Windows.Forms.Label();
            this.lblDescYaw = new System.Windows.Forms.Label();
            this.lblDescYawKeys = new System.Windows.Forms.Label();
            this.lblDescThrust = new System.Windows.Forms.Label();
            this.lblDescThrustKeys = new System.Windows.Forms.Label();
            this.groupBoxControl = new System.Windows.Forms.GroupBox();
            this.lblDescPositionZ = new System.Windows.Forms.Label();
            this.textBoxPositionZ = new System.Windows.Forms.TextBox();
            this.lblDescPositionX = new System.Windows.Forms.Label();
            this.btnPositionOK = new System.Windows.Forms.Button();
            this.textBoxPositionX = new System.Windows.Forms.TextBox();
            this.btnAltitudeOK = new System.Windows.Forms.Button();
            this.lblDescAltitudeMeters = new System.Windows.Forms.Label();
            this.textBoxAltitude = new System.Windows.Forms.TextBox();
            this.checkBoxKeepPosition = new System.Windows.Forms.CheckBox();
            this.lblDescRightJoystick = new System.Windows.Forms.Label();
            this.lblDescLeftJoystick = new System.Windows.Forms.Label();
            this.checkBoxKeepAltitude = new System.Windows.Forms.CheckBox();
            this.checkBoxKeepAttitude = new System.Windows.Forms.CheckBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.picJoystickRight = new System.Windows.Forms.PictureBox();
            this.picJoystickLeft = new System.Windows.Forms.PictureBox();
            this.groupBoxStats = new System.Windows.Forms.GroupBox();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.lblHeight = new System.Windows.Forms.Label();
            this.lblDescSpeed = new System.Windows.Forms.Label();
            this.lblDescHeight = new System.Windows.Forms.Label();
            this.lblThrust = new System.Windows.Forms.Label();
            this.lblDescThrustDisplay = new System.Windows.Forms.Label();
            this.groupBoxKeyboard.SuspendLayout();
            this.groupBoxControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picJoystickRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picJoystickLeft)).BeginInit();
            this.groupBoxStats.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxKeyboard
            // 
            this.groupBoxKeyboard.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxKeyboard.Controls.Add(this.lblDescKeyboardHint);
            this.groupBoxKeyboard.Controls.Add(this.lblDescCamera);
            this.groupBoxKeyboard.Controls.Add(this.lblDescCameraKeys);
            this.groupBoxKeyboard.Controls.Add(this.lblDescReset);
            this.groupBoxKeyboard.Controls.Add(this.lblDescResetKeys);
            this.groupBoxKeyboard.Controls.Add(this.lblDescPitch);
            this.groupBoxKeyboard.Controls.Add(this.lblDescPitchKeys);
            this.groupBoxKeyboard.Controls.Add(this.lblDescRoll);
            this.groupBoxKeyboard.Controls.Add(this.lblDescRollKeys);
            this.groupBoxKeyboard.Controls.Add(this.lblDescYaw);
            this.groupBoxKeyboard.Controls.Add(this.lblDescYawKeys);
            this.groupBoxKeyboard.Controls.Add(this.lblDescThrust);
            this.groupBoxKeyboard.Controls.Add(this.lblDescThrustKeys);
            this.groupBoxKeyboard.Location = new System.Drawing.Point(13, 373);
            this.groupBoxKeyboard.Name = "groupBoxKeyboard";
            this.groupBoxKeyboard.Size = new System.Drawing.Size(183, 170);
            this.groupBoxKeyboard.TabIndex = 15;
            this.groupBoxKeyboard.TabStop = false;
            this.groupBoxKeyboard.Text = "Keyboard input";
            // 
            // lblDescKeyboardHint
            // 
            this.lblDescKeyboardHint.Location = new System.Drawing.Point(8, 133);
            this.lblDescKeyboardHint.Name = "lblDescKeyboardHint";
            this.lblDescKeyboardHint.Size = new System.Drawing.Size(163, 29);
            this.lblDescKeyboardHint.TabIndex = 12;
            this.lblDescKeyboardHint.Text = "For using keyboard control this window has to be selected!";
            // 
            // lblDescCamera
            // 
            this.lblDescCamera.AutoSize = true;
            this.lblDescCamera.Location = new System.Drawing.Point(8, 111);
            this.lblDescCamera.Name = "lblDescCamera";
            this.lblDescCamera.Size = new System.Drawing.Size(43, 13);
            this.lblDescCamera.TabIndex = 11;
            this.lblDescCamera.Text = "Camera";
            // 
            // lblDescCameraKeys
            // 
            this.lblDescCameraKeys.AutoSize = true;
            this.lblDescCameraKeys.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescCameraKeys.Location = new System.Drawing.Point(51, 111);
            this.lblDescCameraKeys.Name = "lblDescCameraKeys";
            this.lblDescCameraKeys.Size = new System.Drawing.Size(21, 13);
            this.lblDescCameraKeys.TabIndex = 10;
            this.lblDescCameraKeys.Text = "F8";
            // 
            // lblDescReset
            // 
            this.lblDescReset.AutoSize = true;
            this.lblDescReset.Location = new System.Drawing.Point(8, 93);
            this.lblDescReset.Name = "lblDescReset";
            this.lblDescReset.Size = new System.Drawing.Size(35, 13);
            this.lblDescReset.TabIndex = 9;
            this.lblDescReset.Text = "Reset";
            // 
            // lblDescResetKeys
            // 
            this.lblDescResetKeys.AutoSize = true;
            this.lblDescResetKeys.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescResetKeys.Location = new System.Drawing.Point(51, 93);
            this.lblDescResetKeys.Name = "lblDescResetKeys";
            this.lblDescResetKeys.Size = new System.Drawing.Size(47, 13);
            this.lblDescResetKeys.TabIndex = 8;
            this.lblDescResetKeys.Text = "SPACE";
            // 
            // lblDescPitch
            // 
            this.lblDescPitch.AutoSize = true;
            this.lblDescPitch.Location = new System.Drawing.Point(8, 76);
            this.lblDescPitch.Name = "lblDescPitch";
            this.lblDescPitch.Size = new System.Drawing.Size(31, 13);
            this.lblDescPitch.TabIndex = 7;
            this.lblDescPitch.Text = "Pitch";
            // 
            // lblDescPitchKeys
            // 
            this.lblDescPitchKeys.AutoSize = true;
            this.lblDescPitchKeys.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescPitchKeys.Location = new System.Drawing.Point(51, 76);
            this.lblDescPitchKeys.Name = "lblDescPitchKeys";
            this.lblDescPitchKeys.Size = new System.Drawing.Size(93, 13);
            this.lblDescPitchKeys.TabIndex = 6;
            this.lblDescPitchKeys.Text = "NUM 4, NUM 6";
            // 
            // lblDescRoll
            // 
            this.lblDescRoll.AutoSize = true;
            this.lblDescRoll.Location = new System.Drawing.Point(8, 58);
            this.lblDescRoll.Name = "lblDescRoll";
            this.lblDescRoll.Size = new System.Drawing.Size(25, 13);
            this.lblDescRoll.TabIndex = 5;
            this.lblDescRoll.Text = "Roll";
            // 
            // lblDescRollKeys
            // 
            this.lblDescRollKeys.AutoSize = true;
            this.lblDescRollKeys.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescRollKeys.Location = new System.Drawing.Point(51, 58);
            this.lblDescRollKeys.Name = "lblDescRollKeys";
            this.lblDescRollKeys.Size = new System.Drawing.Size(93, 13);
            this.lblDescRollKeys.TabIndex = 4;
            this.lblDescRollKeys.Text = "NUM 8, NUM 2";
            // 
            // lblDescYaw
            // 
            this.lblDescYaw.AutoSize = true;
            this.lblDescYaw.Location = new System.Drawing.Point(8, 41);
            this.lblDescYaw.Name = "lblDescYaw";
            this.lblDescYaw.Size = new System.Drawing.Size(28, 13);
            this.lblDescYaw.TabIndex = 3;
            this.lblDescYaw.Text = "Yaw";
            // 
            // lblDescYawKeys
            // 
            this.lblDescYawKeys.AutoSize = true;
            this.lblDescYawKeys.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescYawKeys.Location = new System.Drawing.Point(51, 41);
            this.lblDescYawKeys.Name = "lblDescYawKeys";
            this.lblDescYawKeys.Size = new System.Drawing.Size(32, 13);
            this.lblDescYawKeys.TabIndex = 2;
            this.lblDescYawKeys.Text = "A, D";
            // 
            // lblDescThrust
            // 
            this.lblDescThrust.AutoSize = true;
            this.lblDescThrust.Location = new System.Drawing.Point(8, 25);
            this.lblDescThrust.Name = "lblDescThrust";
            this.lblDescThrust.Size = new System.Drawing.Size(37, 13);
            this.lblDescThrust.TabIndex = 1;
            this.lblDescThrust.Text = "Thrust";
            // 
            // lblDescThrustKeys
            // 
            this.lblDescThrustKeys.AutoSize = true;
            this.lblDescThrustKeys.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescThrustKeys.Location = new System.Drawing.Point(51, 25);
            this.lblDescThrustKeys.Name = "lblDescThrustKeys";
            this.lblDescThrustKeys.Size = new System.Drawing.Size(35, 13);
            this.lblDescThrustKeys.TabIndex = 0;
            this.lblDescThrustKeys.Text = "W, S";
            // 
            // groupBoxControl
            // 
            this.groupBoxControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxControl.Controls.Add(this.lblDescPositionZ);
            this.groupBoxControl.Controls.Add(this.textBoxPositionZ);
            this.groupBoxControl.Controls.Add(this.lblDescPositionX);
            this.groupBoxControl.Controls.Add(this.btnPositionOK);
            this.groupBoxControl.Controls.Add(this.textBoxPositionX);
            this.groupBoxControl.Controls.Add(this.btnAltitudeOK);
            this.groupBoxControl.Controls.Add(this.lblDescAltitudeMeters);
            this.groupBoxControl.Controls.Add(this.textBoxAltitude);
            this.groupBoxControl.Controls.Add(this.checkBoxKeepPosition);
            this.groupBoxControl.Controls.Add(this.lblDescRightJoystick);
            this.groupBoxControl.Controls.Add(this.lblDescLeftJoystick);
            this.groupBoxControl.Controls.Add(this.checkBoxKeepAltitude);
            this.groupBoxControl.Controls.Add(this.checkBoxKeepAttitude);
            this.groupBoxControl.Controls.Add(this.btnReset);
            this.groupBoxControl.Controls.Add(this.picJoystickRight);
            this.groupBoxControl.Controls.Add(this.picJoystickLeft);
            this.groupBoxControl.Location = new System.Drawing.Point(12, 12);
            this.groupBoxControl.Name = "groupBoxControl";
            this.groupBoxControl.Size = new System.Drawing.Size(183, 270);
            this.groupBoxControl.TabIndex = 16;
            this.groupBoxControl.TabStop = false;
            this.groupBoxControl.Text = "Control";
            // 
            // lblDescPositionZ
            // 
            this.lblDescPositionZ.AutoSize = true;
            this.lblDescPositionZ.Location = new System.Drawing.Point(83, 238);
            this.lblDescPositionZ.Name = "lblDescPositionZ";
            this.lblDescPositionZ.Size = new System.Drawing.Size(17, 13);
            this.lblDescPositionZ.TabIndex = 33;
            this.lblDescPositionZ.Text = "Z:";
            // 
            // textBoxPositionZ
            // 
            this.textBoxPositionZ.Location = new System.Drawing.Point(100, 235);
            this.textBoxPositionZ.Name = "textBoxPositionZ";
            this.textBoxPositionZ.Size = new System.Drawing.Size(36, 20);
            this.textBoxPositionZ.TabIndex = 31;
            this.textBoxPositionZ.Text = "0,0";
            // 
            // lblDescPositionX
            // 
            this.lblDescPositionX.AutoSize = true;
            this.lblDescPositionX.Location = new System.Drawing.Point(27, 238);
            this.lblDescPositionX.Name = "lblDescPositionX";
            this.lblDescPositionX.Size = new System.Drawing.Size(17, 13);
            this.lblDescPositionX.TabIndex = 30;
            this.lblDescPositionX.Text = "X:";
            // 
            // btnPositionOK
            // 
            this.btnPositionOK.Location = new System.Drawing.Point(144, 234);
            this.btnPositionOK.Name = "btnPositionOK";
            this.btnPositionOK.Size = new System.Drawing.Size(31, 22);
            this.btnPositionOK.TabIndex = 29;
            this.btnPositionOK.Text = "OK";
            this.btnPositionOK.UseVisualStyleBackColor = true;
            this.btnPositionOK.Click += new System.EventHandler(this.btnPositionOK_Click);
            // 
            // textBoxPositionX
            // 
            this.textBoxPositionX.Location = new System.Drawing.Point(44, 235);
            this.textBoxPositionX.Name = "textBoxPositionX";
            this.textBoxPositionX.Size = new System.Drawing.Size(36, 20);
            this.textBoxPositionX.TabIndex = 27;
            this.textBoxPositionX.Text = "0,0";
            // 
            // btnAltitudeOK
            // 
            this.btnAltitudeOK.Location = new System.Drawing.Point(144, 188);
            this.btnAltitudeOK.Name = "btnAltitudeOK";
            this.btnAltitudeOK.Size = new System.Drawing.Size(31, 22);
            this.btnAltitudeOK.TabIndex = 26;
            this.btnAltitudeOK.Text = "OK";
            this.btnAltitudeOK.UseVisualStyleBackColor = true;
            this.btnAltitudeOK.Click += new System.EventHandler(this.btnAltitudeOK_Click);
            // 
            // lblDescAltitudeMeters
            // 
            this.lblDescAltitudeMeters.AutoSize = true;
            this.lblDescAltitudeMeters.Location = new System.Drawing.Point(123, 192);
            this.lblDescAltitudeMeters.Name = "lblDescAltitudeMeters";
            this.lblDescAltitudeMeters.Size = new System.Drawing.Size(15, 13);
            this.lblDescAltitudeMeters.TabIndex = 25;
            this.lblDescAltitudeMeters.Text = "m";
            // 
            // textBoxAltitude
            // 
            this.textBoxAltitude.Location = new System.Drawing.Point(31, 189);
            this.textBoxAltitude.Name = "textBoxAltitude";
            this.textBoxAltitude.Size = new System.Drawing.Size(91, 20);
            this.textBoxAltitude.TabIndex = 24;
            this.textBoxAltitude.Text = "15,0";
            // 
            // checkBoxKeepPosition
            // 
            this.checkBoxKeepPosition.AutoSize = true;
            this.checkBoxKeepPosition.Location = new System.Drawing.Point(11, 217);
            this.checkBoxKeepPosition.Name = "checkBoxKeepPosition";
            this.checkBoxKeepPosition.Size = new System.Drawing.Size(90, 17);
            this.checkBoxKeepPosition.TabIndex = 23;
            this.checkBoxKeepPosition.Text = "Keep position";
            this.checkBoxKeepPosition.UseVisualStyleBackColor = true;
            this.checkBoxKeepPosition.CheckedChanged += new System.EventHandler(this.checkBoxKeepPosition_CheckedChanged);
            // 
            // lblDescRightJoystick
            // 
            this.lblDescRightJoystick.Location = new System.Drawing.Point(96, 17);
            this.lblDescRightJoystick.Name = "lblDescRightJoystick";
            this.lblDescRightJoystick.Size = new System.Drawing.Size(75, 13);
            this.lblDescRightJoystick.TabIndex = 22;
            this.lblDescRightJoystick.Text = "Right joystick";
            this.lblDescRightJoystick.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDescLeftJoystick
            // 
            this.lblDescLeftJoystick.Location = new System.Drawing.Point(11, 17);
            this.lblDescLeftJoystick.Name = "lblDescLeftJoystick";
            this.lblDescLeftJoystick.Size = new System.Drawing.Size(75, 13);
            this.lblDescLeftJoystick.TabIndex = 21;
            this.lblDescLeftJoystick.Text = "Left joystick";
            this.lblDescLeftJoystick.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // checkBoxKeepAltitude
            // 
            this.checkBoxKeepAltitude.AutoSize = true;
            this.checkBoxKeepAltitude.Location = new System.Drawing.Point(11, 170);
            this.checkBoxKeepAltitude.Name = "checkBoxKeepAltitude";
            this.checkBoxKeepAltitude.Size = new System.Drawing.Size(88, 17);
            this.checkBoxKeepAltitude.TabIndex = 20;
            this.checkBoxKeepAltitude.Text = "Keep altitude";
            this.checkBoxKeepAltitude.UseVisualStyleBackColor = true;
            this.checkBoxKeepAltitude.CheckedChanged += new System.EventHandler(this.checkBoxKeepAltitude_CheckedChanged);
            // 
            // checkBoxKeepAttitude
            // 
            this.checkBoxKeepAttitude.AutoSize = true;
            this.checkBoxKeepAttitude.Location = new System.Drawing.Point(11, 147);
            this.checkBoxKeepAttitude.Name = "checkBoxKeepAttitude";
            this.checkBoxKeepAttitude.Size = new System.Drawing.Size(89, 17);
            this.checkBoxKeepAttitude.TabIndex = 19;
            this.checkBoxKeepAttitude.Text = "Keep attitude";
            this.checkBoxKeepAttitude.UseVisualStyleBackColor = true;
            this.checkBoxKeepAttitude.CheckedChanged += new System.EventHandler(this.checkBoxKeepAttitude_CheckedChanged);
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReset.Location = new System.Drawing.Point(11, 117);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(160, 23);
            this.btnReset.TabIndex = 18;
            this.btnReset.Text = "Reset multicopter";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // picJoystickRight
            // 
            this.picJoystickRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picJoystickRight.Location = new System.Drawing.Point(96, 36);
            this.picJoystickRight.Name = "picJoystickRight";
            this.picJoystickRight.Size = new System.Drawing.Size(75, 75);
            this.picJoystickRight.TabIndex = 16;
            this.picJoystickRight.TabStop = false;
            this.picJoystickRight.MouseLeave += new System.EventHandler(this.picJoystick_MouseLeave);
            this.picJoystickRight.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picJoystick_MouseMove);
            this.picJoystickRight.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picJoystick_MouseUp);
            // 
            // picJoystickLeft
            // 
            this.picJoystickLeft.Location = new System.Drawing.Point(11, 36);
            this.picJoystickLeft.Name = "picJoystickLeft";
            this.picJoystickLeft.Size = new System.Drawing.Size(75, 75);
            this.picJoystickLeft.TabIndex = 15;
            this.picJoystickLeft.TabStop = false;
            this.picJoystickLeft.MouseLeave += new System.EventHandler(this.picJoystick_MouseLeave);
            this.picJoystickLeft.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picJoystick_MouseMove);
            this.picJoystickLeft.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picJoystick_MouseUp);
            // 
            // groupBoxStats
            // 
            this.groupBoxStats.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxStats.Controls.Add(this.lblSpeed);
            this.groupBoxStats.Controls.Add(this.lblHeight);
            this.groupBoxStats.Controls.Add(this.lblDescSpeed);
            this.groupBoxStats.Controls.Add(this.lblDescHeight);
            this.groupBoxStats.Controls.Add(this.lblThrust);
            this.groupBoxStats.Controls.Add(this.lblDescThrustDisplay);
            this.groupBoxStats.Location = new System.Drawing.Point(13, 288);
            this.groupBoxStats.Name = "groupBoxStats";
            this.groupBoxStats.Size = new System.Drawing.Size(182, 79);
            this.groupBoxStats.TabIndex = 17;
            this.groupBoxStats.TabStop = false;
            this.groupBoxStats.Text = "Stats";
            // 
            // lblSpeed
            // 
            this.lblSpeed.AutoSize = true;
            this.lblSpeed.Location = new System.Drawing.Point(45, 51);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(34, 13);
            this.lblSpeed.TabIndex = 60;
            this.lblSpeed.Text = "0 kph";
            // 
            // lblHeight
            // 
            this.lblHeight.AutoSize = true;
            this.lblHeight.Location = new System.Drawing.Point(45, 36);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(24, 13);
            this.lblHeight.TabIndex = 59;
            this.lblHeight.Text = "0 m";
            // 
            // lblDescSpeed
            // 
            this.lblDescSpeed.AutoSize = true;
            this.lblDescSpeed.Location = new System.Drawing.Point(7, 51);
            this.lblDescSpeed.Name = "lblDescSpeed";
            this.lblDescSpeed.Size = new System.Drawing.Size(41, 13);
            this.lblDescSpeed.TabIndex = 58;
            this.lblDescSpeed.Text = "Speed:";
            // 
            // lblDescHeight
            // 
            this.lblDescHeight.AutoSize = true;
            this.lblDescHeight.Location = new System.Drawing.Point(7, 36);
            this.lblDescHeight.Name = "lblDescHeight";
            this.lblDescHeight.Size = new System.Drawing.Size(41, 13);
            this.lblDescHeight.TabIndex = 55;
            this.lblDescHeight.Text = "Height:";
            // 
            // lblThrust
            // 
            this.lblThrust.AutoSize = true;
            this.lblThrust.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThrust.Location = new System.Drawing.Point(45, 20);
            this.lblThrust.Name = "lblThrust";
            this.lblThrust.Size = new System.Drawing.Size(45, 13);
            this.lblThrust.TabIndex = 57;
            this.lblThrust.Text = "0% (0.0)";
            // 
            // lblDescThrustDisplay
            // 
            this.lblDescThrustDisplay.AutoSize = true;
            this.lblDescThrustDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescThrustDisplay.Location = new System.Drawing.Point(7, 20);
            this.lblDescThrustDisplay.Name = "lblDescThrustDisplay";
            this.lblDescThrustDisplay.Size = new System.Drawing.Size(40, 13);
            this.lblDescThrustDisplay.TabIndex = 56;
            this.lblDescThrustDisplay.Text = "Thrust:";
            // 
            // WindowControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(207, 555);
            this.Controls.Add(this.groupBoxStats);
            this.Controls.Add(this.groupBoxControl);
            this.Controls.Add(this.groupBoxKeyboard);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Location = new System.Drawing.Point(10, 10);
            this.MaximizeBox = false;
            this.Name = "WindowControl";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Control";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.WindowControl_FormClosed);
            this.Load += new System.EventHandler(this.WindowControl_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.WindowControl_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.WindowControl_KeyUp);
            this.groupBoxKeyboard.ResumeLayout(false);
            this.groupBoxKeyboard.PerformLayout();
            this.groupBoxControl.ResumeLayout(false);
            this.groupBoxControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picJoystickRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picJoystickLeft)).EndInit();
            this.groupBoxStats.ResumeLayout(false);
            this.groupBoxStats.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxKeyboard;
        private System.Windows.Forms.Label lblDescThrust;
        private System.Windows.Forms.Label lblDescThrustKeys;
        private System.Windows.Forms.Label lblDescCamera;
        private System.Windows.Forms.Label lblDescCameraKeys;
        private System.Windows.Forms.Label lblDescReset;
        private System.Windows.Forms.Label lblDescResetKeys;
        private System.Windows.Forms.Label lblDescPitch;
        private System.Windows.Forms.Label lblDescPitchKeys;
        private System.Windows.Forms.Label lblDescRoll;
        private System.Windows.Forms.Label lblDescRollKeys;
        private System.Windows.Forms.Label lblDescYaw;
        private System.Windows.Forms.Label lblDescYawKeys;
        private System.Windows.Forms.GroupBox groupBoxControl;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.PictureBox picJoystickRight;
        private System.Windows.Forms.PictureBox picJoystickLeft;
        private System.Windows.Forms.Label lblDescRightJoystick;
        private System.Windows.Forms.Label lblDescLeftJoystick;
        private System.Windows.Forms.CheckBox checkBoxKeepAltitude;
        private System.Windows.Forms.CheckBox checkBoxKeepAttitude;
        private System.Windows.Forms.GroupBox groupBoxStats;
        private System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.Label lblDescSpeed;
        private System.Windows.Forms.Label lblDescHeight;
        private System.Windows.Forms.Label lblThrust;
        private System.Windows.Forms.Label lblDescThrustDisplay;
        private System.Windows.Forms.Label lblDescKeyboardHint;
        private System.Windows.Forms.CheckBox checkBoxKeepPosition;
        private System.Windows.Forms.Button btnAltitudeOK;
        private System.Windows.Forms.Label lblDescAltitudeMeters;
        private System.Windows.Forms.TextBox textBoxAltitude;
        private System.Windows.Forms.Label lblDescPositionZ;
        private System.Windows.Forms.TextBox textBoxPositionZ;
        private System.Windows.Forms.Label lblDescPositionX;
        private System.Windows.Forms.Button btnPositionOK;
        private System.Windows.Forms.TextBox textBoxPositionX;
    }
}