namespace MulticopterSimulation.Windows
{
    partial class WindowStatistics
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
            bmpPlot.Dispose();
            bmpPlotCaption.Dispose();
            bmpComplete.Dispose();

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
            this.pictureBoxStats = new System.Windows.Forms.PictureBox();
            this.btnPlot = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.trackBarYScale = new System.Windows.Forms.TrackBar();
            this.lblDescYScale = new System.Windows.Forms.Label();
            this.lblSimulationRuntime = new System.Windows.Forms.Label();
            this.groupBoxTorque = new System.Windows.Forms.GroupBox();
            this.lblTorqueZ = new System.Windows.Forms.Label();
            this.lblTorqueY = new System.Windows.Forms.Label();
            this.lblTorqueX = new System.Windows.Forms.Label();
            this.lblDescTorqueZ = new System.Windows.Forms.Label();
            this.lblDescTorqueY = new System.Windows.Forms.Label();
            this.lblDescTorqueX = new System.Windows.Forms.Label();
            this.groupBoxAngularVelocity = new System.Windows.Forms.GroupBox();
            this.lblAVelocityZ = new System.Windows.Forms.Label();
            this.lblAVelocityY = new System.Windows.Forms.Label();
            this.lblAVelocityX = new System.Windows.Forms.Label();
            this.lblDescAVelocityZ = new System.Windows.Forms.Label();
            this.lblDescAVelocityY = new System.Windows.Forms.Label();
            this.lblDescAVelocityX = new System.Windows.Forms.Label();
            this.groupBoxRotation = new System.Windows.Forms.GroupBox();
            this.lblRotationZ = new System.Windows.Forms.Label();
            this.lblRotationY = new System.Windows.Forms.Label();
            this.lblRotationX = new System.Windows.Forms.Label();
            this.lblDescRotationZ = new System.Windows.Forms.Label();
            this.lblDescRotationY = new System.Windows.Forms.Label();
            this.lblDescRotationX = new System.Windows.Forms.Label();
            this.groupBoxForce = new System.Windows.Forms.GroupBox();
            this.lblForceZ = new System.Windows.Forms.Label();
            this.lblForceY = new System.Windows.Forms.Label();
            this.lblForceX = new System.Windows.Forms.Label();
            this.lblDescForceZ = new System.Windows.Forms.Label();
            this.lblDescForceY = new System.Windows.Forms.Label();
            this.lblDescForceX = new System.Windows.Forms.Label();
            this.groupBoxVelocity = new System.Windows.Forms.GroupBox();
            this.lblVelocityZ = new System.Windows.Forms.Label();
            this.lblVelocityY = new System.Windows.Forms.Label();
            this.lblVelocityX = new System.Windows.Forms.Label();
            this.lblDescVelocityZ = new System.Windows.Forms.Label();
            this.lblDescVelocityY = new System.Windows.Forms.Label();
            this.lblDescVelocityX = new System.Windows.Forms.Label();
            this.groupBoxPosition = new System.Windows.Forms.GroupBox();
            this.lblPositionY = new System.Windows.Forms.Label();
            this.lblDescPositionX = new System.Windows.Forms.Label();
            this.lblDescPositionY = new System.Windows.Forms.Label();
            this.lblDescPositionZ = new System.Windows.Forms.Label();
            this.lblPositionX = new System.Windows.Forms.Label();
            this.lblPositionZ = new System.Windows.Forms.Label();
            this.groupBoxGeneralValues = new System.Windows.Forms.GroupBox();
            this.lblMulticopterWeight = new System.Windows.Forms.Label();
            this.lblDescMulticopterWeight = new System.Windows.Forms.Label();
            this.lblDescSimulationRuntime = new System.Windows.Forms.Label();
            this.groupBoxVisualization = new System.Windows.Forms.GroupBox();
            this.trackBarUpdateInterval = new System.Windows.Forms.TrackBar();
            this.lblDescUpdateFreq = new System.Windows.Forms.Label();
            this.comboBoxValueToPlot = new System.Windows.Forms.ComboBox();
            this.lblDescValueToPlot = new System.Windows.Forms.Label();
            this.btnRecord = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxStats)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarYScale)).BeginInit();
            this.groupBoxTorque.SuspendLayout();
            this.groupBoxAngularVelocity.SuspendLayout();
            this.groupBoxRotation.SuspendLayout();
            this.groupBoxForce.SuspendLayout();
            this.groupBoxVelocity.SuspendLayout();
            this.groupBoxPosition.SuspendLayout();
            this.groupBoxGeneralValues.SuspendLayout();
            this.groupBoxVisualization.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarUpdateInterval)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxStats
            // 
            this.pictureBoxStats.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxStats.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxStats.Location = new System.Drawing.Point(9, 46);
            this.pictureBoxStats.Name = "pictureBoxStats";
            this.pictureBoxStats.Size = new System.Drawing.Size(299, 127);
            this.pictureBoxStats.TabIndex = 1;
            this.pictureBoxStats.TabStop = false;
            this.pictureBoxStats.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxStats_MouseMove);
            // 
            // btnPlot
            // 
            this.btnPlot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPlot.Location = new System.Drawing.Point(9, 236);
            this.btnPlot.Name = "btnPlot";
            this.btnPlot.Size = new System.Drawing.Size(99, 23);
            this.btnPlot.TabIndex = 2;
            this.btnPlot.Text = "Stop plotting";
            this.btnPlot.UseVisualStyleBackColor = true;
            this.btnPlot.Click += new System.EventHandler(this.btnPlot_Click);
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReset.Location = new System.Drawing.Point(233, 236);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 3;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // trackBarYScale
            // 
            this.trackBarYScale.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarYScale.AutoSize = false;
            this.trackBarYScale.LargeChange = 25;
            this.trackBarYScale.Location = new System.Drawing.Point(96, 206);
            this.trackBarYScale.Maximum = 1000;
            this.trackBarYScale.Name = "trackBarYScale";
            this.trackBarYScale.Size = new System.Drawing.Size(212, 24);
            this.trackBarYScale.SmallChange = 10;
            this.trackBarYScale.TabIndex = 4;
            this.trackBarYScale.TickFrequency = 100;
            this.trackBarYScale.Value = 500;
            this.trackBarYScale.Scroll += new System.EventHandler(this.trackBarYScale_Scroll);
            // 
            // lblDescYScale
            // 
            this.lblDescYScale.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDescYScale.AutoSize = true;
            this.lblDescYScale.Location = new System.Drawing.Point(9, 211);
            this.lblDescYScale.Name = "lblDescYScale";
            this.lblDescYScale.Size = new System.Drawing.Size(45, 13);
            this.lblDescYScale.TabIndex = 5;
            this.lblDescYScale.Text = "Y scale:";
            // 
            // lblSimulationRuntime
            // 
            this.lblSimulationRuntime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSimulationRuntime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSimulationRuntime.Location = new System.Drawing.Point(108, 14);
            this.lblSimulationRuntime.Name = "lblSimulationRuntime";
            this.lblSimulationRuntime.Size = new System.Drawing.Size(199, 17);
            this.lblSimulationRuntime.TabIndex = 54;
            this.lblSimulationRuntime.Text = "0 s";
            this.lblSimulationRuntime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBoxTorque
            // 
            this.groupBoxTorque.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxTorque.Controls.Add(this.lblTorqueZ);
            this.groupBoxTorque.Controls.Add(this.lblTorqueY);
            this.groupBoxTorque.Controls.Add(this.lblTorqueX);
            this.groupBoxTorque.Controls.Add(this.lblDescTorqueZ);
            this.groupBoxTorque.Controls.Add(this.lblDescTorqueY);
            this.groupBoxTorque.Controls.Add(this.lblDescTorqueX);
            this.groupBoxTorque.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxTorque.Location = new System.Drawing.Point(219, 140);
            this.groupBoxTorque.Name = "groupBoxTorque";
            this.groupBoxTorque.Size = new System.Drawing.Size(104, 66);
            this.groupBoxTorque.TabIndex = 60;
            this.groupBoxTorque.TabStop = false;
            this.groupBoxTorque.Text = "Torque";
            // 
            // lblTorqueZ
            // 
            this.lblTorqueZ.AutoSize = true;
            this.lblTorqueZ.Location = new System.Drawing.Point(29, 42);
            this.lblTorqueZ.Name = "lblTorqueZ";
            this.lblTorqueZ.Size = new System.Drawing.Size(16, 13);
            this.lblTorqueZ.TabIndex = 38;
            this.lblTorqueZ.Text = " 0";
            // 
            // lblTorqueY
            // 
            this.lblTorqueY.AutoSize = true;
            this.lblTorqueY.Location = new System.Drawing.Point(29, 29);
            this.lblTorqueY.Name = "lblTorqueY";
            this.lblTorqueY.Size = new System.Drawing.Size(16, 13);
            this.lblTorqueY.TabIndex = 37;
            this.lblTorqueY.Text = " 0";
            // 
            // lblTorqueX
            // 
            this.lblTorqueX.AutoSize = true;
            this.lblTorqueX.Location = new System.Drawing.Point(29, 16);
            this.lblTorqueX.Name = "lblTorqueX";
            this.lblTorqueX.Size = new System.Drawing.Size(16, 13);
            this.lblTorqueX.TabIndex = 36;
            this.lblTorqueX.Text = " 0";
            // 
            // lblDescTorqueZ
            // 
            this.lblDescTorqueZ.AutoSize = true;
            this.lblDescTorqueZ.Location = new System.Drawing.Point(6, 42);
            this.lblDescTorqueZ.Name = "lblDescTorqueZ";
            this.lblDescTorqueZ.Size = new System.Drawing.Size(17, 13);
            this.lblDescTorqueZ.TabIndex = 35;
            this.lblDescTorqueZ.Text = "Z:";
            // 
            // lblDescTorqueY
            // 
            this.lblDescTorqueY.AutoSize = true;
            this.lblDescTorqueY.Location = new System.Drawing.Point(6, 29);
            this.lblDescTorqueY.Name = "lblDescTorqueY";
            this.lblDescTorqueY.Size = new System.Drawing.Size(17, 13);
            this.lblDescTorqueY.TabIndex = 34;
            this.lblDescTorqueY.Text = "Y:";
            // 
            // lblDescTorqueX
            // 
            this.lblDescTorqueX.AutoSize = true;
            this.lblDescTorqueX.Location = new System.Drawing.Point(6, 16);
            this.lblDescTorqueX.Name = "lblDescTorqueX";
            this.lblDescTorqueX.Size = new System.Drawing.Size(17, 13);
            this.lblDescTorqueX.TabIndex = 33;
            this.lblDescTorqueX.Text = "X:";
            // 
            // groupBoxAngularVelocity
            // 
            this.groupBoxAngularVelocity.Controls.Add(this.lblAVelocityZ);
            this.groupBoxAngularVelocity.Controls.Add(this.lblAVelocityY);
            this.groupBoxAngularVelocity.Controls.Add(this.lblAVelocityX);
            this.groupBoxAngularVelocity.Controls.Add(this.lblDescAVelocityZ);
            this.groupBoxAngularVelocity.Controls.Add(this.lblDescAVelocityY);
            this.groupBoxAngularVelocity.Controls.Add(this.lblDescAVelocityX);
            this.groupBoxAngularVelocity.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxAngularVelocity.Location = new System.Drawing.Point(111, 140);
            this.groupBoxAngularVelocity.Name = "groupBoxAngularVelocity";
            this.groupBoxAngularVelocity.Size = new System.Drawing.Size(102, 66);
            this.groupBoxAngularVelocity.TabIndex = 59;
            this.groupBoxAngularVelocity.TabStop = false;
            this.groupBoxAngularVelocity.Text = "Angular velocity";
            // 
            // lblAVelocityZ
            // 
            this.lblAVelocityZ.AutoSize = true;
            this.lblAVelocityZ.Location = new System.Drawing.Point(29, 42);
            this.lblAVelocityZ.Name = "lblAVelocityZ";
            this.lblAVelocityZ.Size = new System.Drawing.Size(16, 13);
            this.lblAVelocityZ.TabIndex = 41;
            this.lblAVelocityZ.Text = " 0";
            // 
            // lblAVelocityY
            // 
            this.lblAVelocityY.AutoSize = true;
            this.lblAVelocityY.Location = new System.Drawing.Point(29, 29);
            this.lblAVelocityY.Name = "lblAVelocityY";
            this.lblAVelocityY.Size = new System.Drawing.Size(16, 13);
            this.lblAVelocityY.TabIndex = 40;
            this.lblAVelocityY.Text = " 0";
            // 
            // lblAVelocityX
            // 
            this.lblAVelocityX.AutoSize = true;
            this.lblAVelocityX.Location = new System.Drawing.Point(29, 16);
            this.lblAVelocityX.Name = "lblAVelocityX";
            this.lblAVelocityX.Size = new System.Drawing.Size(16, 13);
            this.lblAVelocityX.TabIndex = 39;
            this.lblAVelocityX.Text = " 0";
            // 
            // lblDescAVelocityZ
            // 
            this.lblDescAVelocityZ.AutoSize = true;
            this.lblDescAVelocityZ.Location = new System.Drawing.Point(6, 42);
            this.lblDescAVelocityZ.Name = "lblDescAVelocityZ";
            this.lblDescAVelocityZ.Size = new System.Drawing.Size(17, 13);
            this.lblDescAVelocityZ.TabIndex = 38;
            this.lblDescAVelocityZ.Text = "Z:";
            // 
            // lblDescAVelocityY
            // 
            this.lblDescAVelocityY.AutoSize = true;
            this.lblDescAVelocityY.Location = new System.Drawing.Point(6, 29);
            this.lblDescAVelocityY.Name = "lblDescAVelocityY";
            this.lblDescAVelocityY.Size = new System.Drawing.Size(17, 13);
            this.lblDescAVelocityY.TabIndex = 37;
            this.lblDescAVelocityY.Text = "Y:";
            // 
            // lblDescAVelocityX
            // 
            this.lblDescAVelocityX.AutoSize = true;
            this.lblDescAVelocityX.Location = new System.Drawing.Point(6, 16);
            this.lblDescAVelocityX.Name = "lblDescAVelocityX";
            this.lblDescAVelocityX.Size = new System.Drawing.Size(17, 13);
            this.lblDescAVelocityX.TabIndex = 36;
            this.lblDescAVelocityX.Text = "X:";
            // 
            // groupBoxRotation
            // 
            this.groupBoxRotation.Controls.Add(this.lblRotationZ);
            this.groupBoxRotation.Controls.Add(this.lblRotationY);
            this.groupBoxRotation.Controls.Add(this.lblRotationX);
            this.groupBoxRotation.Controls.Add(this.lblDescRotationZ);
            this.groupBoxRotation.Controls.Add(this.lblDescRotationY);
            this.groupBoxRotation.Controls.Add(this.lblDescRotationX);
            this.groupBoxRotation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxRotation.Location = new System.Drawing.Point(9, 140);
            this.groupBoxRotation.Name = "groupBoxRotation";
            this.groupBoxRotation.Size = new System.Drawing.Size(96, 66);
            this.groupBoxRotation.TabIndex = 58;
            this.groupBoxRotation.TabStop = false;
            this.groupBoxRotation.Text = "Rotation";
            // 
            // lblRotationZ
            // 
            this.lblRotationZ.AutoSize = true;
            this.lblRotationZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRotationZ.Location = new System.Drawing.Point(29, 42);
            this.lblRotationZ.Name = "lblRotationZ";
            this.lblRotationZ.Size = new System.Drawing.Size(16, 13);
            this.lblRotationZ.TabIndex = 44;
            this.lblRotationZ.Text = " 0";
            // 
            // lblRotationY
            // 
            this.lblRotationY.AutoSize = true;
            this.lblRotationY.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRotationY.Location = new System.Drawing.Point(29, 29);
            this.lblRotationY.Name = "lblRotationY";
            this.lblRotationY.Size = new System.Drawing.Size(16, 13);
            this.lblRotationY.TabIndex = 43;
            this.lblRotationY.Text = " 0";
            // 
            // lblRotationX
            // 
            this.lblRotationX.AutoSize = true;
            this.lblRotationX.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRotationX.Location = new System.Drawing.Point(29, 16);
            this.lblRotationX.Name = "lblRotationX";
            this.lblRotationX.Size = new System.Drawing.Size(16, 13);
            this.lblRotationX.TabIndex = 42;
            this.lblRotationX.Text = " 0";
            // 
            // lblDescRotationZ
            // 
            this.lblDescRotationZ.AutoSize = true;
            this.lblDescRotationZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescRotationZ.Location = new System.Drawing.Point(6, 42);
            this.lblDescRotationZ.Name = "lblDescRotationZ";
            this.lblDescRotationZ.Size = new System.Drawing.Size(17, 13);
            this.lblDescRotationZ.TabIndex = 41;
            this.lblDescRotationZ.Text = "Z:";
            // 
            // lblDescRotationY
            // 
            this.lblDescRotationY.AutoSize = true;
            this.lblDescRotationY.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescRotationY.Location = new System.Drawing.Point(6, 29);
            this.lblDescRotationY.Name = "lblDescRotationY";
            this.lblDescRotationY.Size = new System.Drawing.Size(17, 13);
            this.lblDescRotationY.TabIndex = 40;
            this.lblDescRotationY.Text = "Y:";
            // 
            // lblDescRotationX
            // 
            this.lblDescRotationX.AutoSize = true;
            this.lblDescRotationX.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescRotationX.Location = new System.Drawing.Point(6, 16);
            this.lblDescRotationX.Name = "lblDescRotationX";
            this.lblDescRotationX.Size = new System.Drawing.Size(17, 13);
            this.lblDescRotationX.TabIndex = 39;
            this.lblDescRotationX.Text = "X:";
            // 
            // groupBoxForce
            // 
            this.groupBoxForce.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxForce.Controls.Add(this.lblForceZ);
            this.groupBoxForce.Controls.Add(this.lblForceY);
            this.groupBoxForce.Controls.Add(this.lblForceX);
            this.groupBoxForce.Controls.Add(this.lblDescForceZ);
            this.groupBoxForce.Controls.Add(this.lblDescForceY);
            this.groupBoxForce.Controls.Add(this.lblDescForceX);
            this.groupBoxForce.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxForce.Location = new System.Drawing.Point(219, 68);
            this.groupBoxForce.Name = "groupBoxForce";
            this.groupBoxForce.Size = new System.Drawing.Size(104, 66);
            this.groupBoxForce.TabIndex = 57;
            this.groupBoxForce.TabStop = false;
            this.groupBoxForce.Text = "Linear force";
            // 
            // lblForceZ
            // 
            this.lblForceZ.AutoSize = true;
            this.lblForceZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblForceZ.Location = new System.Drawing.Point(29, 44);
            this.lblForceZ.Name = "lblForceZ";
            this.lblForceZ.Size = new System.Drawing.Size(16, 13);
            this.lblForceZ.TabIndex = 11;
            this.lblForceZ.Text = " 0";
            // 
            // lblForceY
            // 
            this.lblForceY.AutoSize = true;
            this.lblForceY.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblForceY.Location = new System.Drawing.Point(29, 31);
            this.lblForceY.Name = "lblForceY";
            this.lblForceY.Size = new System.Drawing.Size(16, 13);
            this.lblForceY.TabIndex = 10;
            this.lblForceY.Text = " 0";
            // 
            // lblForceX
            // 
            this.lblForceX.AutoSize = true;
            this.lblForceX.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblForceX.Location = new System.Drawing.Point(29, 18);
            this.lblForceX.Name = "lblForceX";
            this.lblForceX.Size = new System.Drawing.Size(16, 13);
            this.lblForceX.TabIndex = 9;
            this.lblForceX.Text = " 0";
            // 
            // lblDescForceZ
            // 
            this.lblDescForceZ.AutoSize = true;
            this.lblDescForceZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescForceZ.Location = new System.Drawing.Point(6, 44);
            this.lblDescForceZ.Name = "lblDescForceZ";
            this.lblDescForceZ.Size = new System.Drawing.Size(17, 13);
            this.lblDescForceZ.TabIndex = 8;
            this.lblDescForceZ.Text = "Z:";
            // 
            // lblDescForceY
            // 
            this.lblDescForceY.AutoSize = true;
            this.lblDescForceY.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescForceY.Location = new System.Drawing.Point(6, 31);
            this.lblDescForceY.Name = "lblDescForceY";
            this.lblDescForceY.Size = new System.Drawing.Size(17, 13);
            this.lblDescForceY.TabIndex = 7;
            this.lblDescForceY.Text = "Y:";
            // 
            // lblDescForceX
            // 
            this.lblDescForceX.AutoSize = true;
            this.lblDescForceX.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescForceX.Location = new System.Drawing.Point(6, 18);
            this.lblDescForceX.Name = "lblDescForceX";
            this.lblDescForceX.Size = new System.Drawing.Size(17, 13);
            this.lblDescForceX.TabIndex = 6;
            this.lblDescForceX.Text = "X:";
            // 
            // groupBoxVelocity
            // 
            this.groupBoxVelocity.Controls.Add(this.lblVelocityZ);
            this.groupBoxVelocity.Controls.Add(this.lblVelocityY);
            this.groupBoxVelocity.Controls.Add(this.lblVelocityX);
            this.groupBoxVelocity.Controls.Add(this.lblDescVelocityZ);
            this.groupBoxVelocity.Controls.Add(this.lblDescVelocityY);
            this.groupBoxVelocity.Controls.Add(this.lblDescVelocityX);
            this.groupBoxVelocity.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxVelocity.Location = new System.Drawing.Point(111, 68);
            this.groupBoxVelocity.Name = "groupBoxVelocity";
            this.groupBoxVelocity.Size = new System.Drawing.Size(102, 66);
            this.groupBoxVelocity.TabIndex = 56;
            this.groupBoxVelocity.TabStop = false;
            this.groupBoxVelocity.Text = "Velocity";
            // 
            // lblVelocityZ
            // 
            this.lblVelocityZ.AutoSize = true;
            this.lblVelocityZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVelocityZ.Location = new System.Drawing.Point(29, 44);
            this.lblVelocityZ.Name = "lblVelocityZ";
            this.lblVelocityZ.Size = new System.Drawing.Size(16, 13);
            this.lblVelocityZ.TabIndex = 32;
            this.lblVelocityZ.Text = " 0";
            // 
            // lblVelocityY
            // 
            this.lblVelocityY.AutoSize = true;
            this.lblVelocityY.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVelocityY.Location = new System.Drawing.Point(29, 31);
            this.lblVelocityY.Name = "lblVelocityY";
            this.lblVelocityY.Size = new System.Drawing.Size(16, 13);
            this.lblVelocityY.TabIndex = 31;
            this.lblVelocityY.Text = " 0";
            // 
            // lblVelocityX
            // 
            this.lblVelocityX.AutoSize = true;
            this.lblVelocityX.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVelocityX.Location = new System.Drawing.Point(29, 18);
            this.lblVelocityX.Name = "lblVelocityX";
            this.lblVelocityX.Size = new System.Drawing.Size(16, 13);
            this.lblVelocityX.TabIndex = 30;
            this.lblVelocityX.Text = " 0";
            // 
            // lblDescVelocityZ
            // 
            this.lblDescVelocityZ.AutoSize = true;
            this.lblDescVelocityZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescVelocityZ.Location = new System.Drawing.Point(6, 44);
            this.lblDescVelocityZ.Name = "lblDescVelocityZ";
            this.lblDescVelocityZ.Size = new System.Drawing.Size(17, 13);
            this.lblDescVelocityZ.TabIndex = 29;
            this.lblDescVelocityZ.Text = "Z:";
            // 
            // lblDescVelocityY
            // 
            this.lblDescVelocityY.AutoSize = true;
            this.lblDescVelocityY.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescVelocityY.Location = new System.Drawing.Point(6, 31);
            this.lblDescVelocityY.Name = "lblDescVelocityY";
            this.lblDescVelocityY.Size = new System.Drawing.Size(17, 13);
            this.lblDescVelocityY.TabIndex = 28;
            this.lblDescVelocityY.Text = "Y:";
            // 
            // lblDescVelocityX
            // 
            this.lblDescVelocityX.AutoSize = true;
            this.lblDescVelocityX.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescVelocityX.Location = new System.Drawing.Point(6, 18);
            this.lblDescVelocityX.Name = "lblDescVelocityX";
            this.lblDescVelocityX.Size = new System.Drawing.Size(17, 13);
            this.lblDescVelocityX.TabIndex = 27;
            this.lblDescVelocityX.Text = "X:";
            // 
            // groupBoxPosition
            // 
            this.groupBoxPosition.Controls.Add(this.lblPositionY);
            this.groupBoxPosition.Controls.Add(this.lblDescPositionX);
            this.groupBoxPosition.Controls.Add(this.lblDescPositionY);
            this.groupBoxPosition.Controls.Add(this.lblDescPositionZ);
            this.groupBoxPosition.Controls.Add(this.lblPositionX);
            this.groupBoxPosition.Controls.Add(this.lblPositionZ);
            this.groupBoxPosition.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxPosition.Location = new System.Drawing.Point(9, 68);
            this.groupBoxPosition.Name = "groupBoxPosition";
            this.groupBoxPosition.Size = new System.Drawing.Size(96, 66);
            this.groupBoxPosition.TabIndex = 55;
            this.groupBoxPosition.TabStop = false;
            this.groupBoxPosition.Text = "Position";
            // 
            // lblPositionY
            // 
            this.lblPositionY.AutoSize = true;
            this.lblPositionY.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPositionY.Location = new System.Drawing.Point(29, 31);
            this.lblPositionY.Name = "lblPositionY";
            this.lblPositionY.Size = new System.Drawing.Size(16, 13);
            this.lblPositionY.TabIndex = 34;
            this.lblPositionY.Text = " 0";
            // 
            // lblDescPositionX
            // 
            this.lblDescPositionX.AutoSize = true;
            this.lblDescPositionX.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescPositionX.Location = new System.Drawing.Point(6, 18);
            this.lblDescPositionX.Name = "lblDescPositionX";
            this.lblDescPositionX.Size = new System.Drawing.Size(17, 13);
            this.lblDescPositionX.TabIndex = 30;
            this.lblDescPositionX.Text = "X:";
            // 
            // lblDescPositionY
            // 
            this.lblDescPositionY.AutoSize = true;
            this.lblDescPositionY.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescPositionY.Location = new System.Drawing.Point(6, 31);
            this.lblDescPositionY.Name = "lblDescPositionY";
            this.lblDescPositionY.Size = new System.Drawing.Size(17, 13);
            this.lblDescPositionY.TabIndex = 31;
            this.lblDescPositionY.Text = "Y:";
            // 
            // lblDescPositionZ
            // 
            this.lblDescPositionZ.AutoSize = true;
            this.lblDescPositionZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescPositionZ.Location = new System.Drawing.Point(6, 44);
            this.lblDescPositionZ.Name = "lblDescPositionZ";
            this.lblDescPositionZ.Size = new System.Drawing.Size(17, 13);
            this.lblDescPositionZ.TabIndex = 32;
            this.lblDescPositionZ.Text = "Z:";
            // 
            // lblPositionX
            // 
            this.lblPositionX.AutoSize = true;
            this.lblPositionX.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPositionX.Location = new System.Drawing.Point(29, 18);
            this.lblPositionX.Name = "lblPositionX";
            this.lblPositionX.Size = new System.Drawing.Size(16, 13);
            this.lblPositionX.TabIndex = 33;
            this.lblPositionX.Text = " 0";
            // 
            // lblPositionZ
            // 
            this.lblPositionZ.AutoSize = true;
            this.lblPositionZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPositionZ.Location = new System.Drawing.Point(29, 44);
            this.lblPositionZ.Name = "lblPositionZ";
            this.lblPositionZ.Size = new System.Drawing.Size(16, 13);
            this.lblPositionZ.TabIndex = 35;
            this.lblPositionZ.Text = " 0";
            // 
            // groupBoxGeneralValues
            // 
            this.groupBoxGeneralValues.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxGeneralValues.Controls.Add(this.lblMulticopterWeight);
            this.groupBoxGeneralValues.Controls.Add(this.lblDescMulticopterWeight);
            this.groupBoxGeneralValues.Controls.Add(this.lblDescSimulationRuntime);
            this.groupBoxGeneralValues.Controls.Add(this.lblSimulationRuntime);
            this.groupBoxGeneralValues.Location = new System.Drawing.Point(9, 11);
            this.groupBoxGeneralValues.Name = "groupBoxGeneralValues";
            this.groupBoxGeneralValues.Size = new System.Drawing.Size(314, 51);
            this.groupBoxGeneralValues.TabIndex = 61;
            this.groupBoxGeneralValues.TabStop = false;
            this.groupBoxGeneralValues.Text = "General values";
            // 
            // lblMulticopterWeight
            // 
            this.lblMulticopterWeight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMulticopterWeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMulticopterWeight.Location = new System.Drawing.Point(108, 31);
            this.lblMulticopterWeight.Name = "lblMulticopterWeight";
            this.lblMulticopterWeight.Size = new System.Drawing.Size(200, 13);
            this.lblMulticopterWeight.TabIndex = 57;
            this.lblMulticopterWeight.Text = "0 g";
            this.lblMulticopterWeight.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDescMulticopterWeight
            // 
            this.lblDescMulticopterWeight.AutoSize = true;
            this.lblDescMulticopterWeight.Location = new System.Drawing.Point(6, 31);
            this.lblDescMulticopterWeight.Name = "lblDescMulticopterWeight";
            this.lblDescMulticopterWeight.Size = new System.Drawing.Size(96, 13);
            this.lblDescMulticopterWeight.TabIndex = 56;
            this.lblDescMulticopterWeight.Text = "Multicopter weight:";
            // 
            // lblDescSimulationRuntime
            // 
            this.lblDescSimulationRuntime.AutoSize = true;
            this.lblDescSimulationRuntime.Location = new System.Drawing.Point(6, 16);
            this.lblDescSimulationRuntime.Name = "lblDescSimulationRuntime";
            this.lblDescSimulationRuntime.Size = new System.Drawing.Size(95, 13);
            this.lblDescSimulationRuntime.TabIndex = 55;
            this.lblDescSimulationRuntime.Text = "Simulation runtime:";
            // 
            // groupBoxVisualization
            // 
            this.groupBoxVisualization.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxVisualization.Controls.Add(this.trackBarUpdateInterval);
            this.groupBoxVisualization.Controls.Add(this.lblDescUpdateFreq);
            this.groupBoxVisualization.Controls.Add(this.comboBoxValueToPlot);
            this.groupBoxVisualization.Controls.Add(this.lblDescValueToPlot);
            this.groupBoxVisualization.Controls.Add(this.btnRecord);
            this.groupBoxVisualization.Controls.Add(this.btnPlot);
            this.groupBoxVisualization.Controls.Add(this.pictureBoxStats);
            this.groupBoxVisualization.Controls.Add(this.btnReset);
            this.groupBoxVisualization.Controls.Add(this.trackBarYScale);
            this.groupBoxVisualization.Controls.Add(this.lblDescYScale);
            this.groupBoxVisualization.Location = new System.Drawing.Point(9, 212);
            this.groupBoxVisualization.Name = "groupBoxVisualization";
            this.groupBoxVisualization.Size = new System.Drawing.Size(314, 265);
            this.groupBoxVisualization.TabIndex = 62;
            this.groupBoxVisualization.TabStop = false;
            this.groupBoxVisualization.Text = "Graphical visualization";
            // 
            // trackBarUpdateInterval
            // 
            this.trackBarUpdateInterval.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarUpdateInterval.AutoSize = false;
            this.trackBarUpdateInterval.LargeChange = 25;
            this.trackBarUpdateInterval.Location = new System.Drawing.Point(96, 179);
            this.trackBarUpdateInterval.Maximum = 250;
            this.trackBarUpdateInterval.Minimum = 1;
            this.trackBarUpdateInterval.Name = "trackBarUpdateInterval";
            this.trackBarUpdateInterval.Size = new System.Drawing.Size(211, 24);
            this.trackBarUpdateInterval.SmallChange = 10;
            this.trackBarUpdateInterval.TabIndex = 9;
            this.trackBarUpdateInterval.TickFrequency = 50;
            this.trackBarUpdateInterval.Value = 50;
            this.trackBarUpdateInterval.Scroll += new System.EventHandler(this.trackBarUpdateInterval_Scroll);
            // 
            // lblDescUpdateFreq
            // 
            this.lblDescUpdateFreq.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDescUpdateFreq.AutoSize = true;
            this.lblDescUpdateFreq.Location = new System.Drawing.Point(8, 184);
            this.lblDescUpdateFreq.Name = "lblDescUpdateFreq";
            this.lblDescUpdateFreq.Size = new System.Drawing.Size(82, 13);
            this.lblDescUpdateFreq.TabIndex = 10;
            this.lblDescUpdateFreq.Text = "Update interval:";
            // 
            // comboBoxValueToPlot
            // 
            this.comboBoxValueToPlot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxValueToPlot.Items.AddRange(new object[] {
            "Thrust",
            "Position Y",
            "Position Deviation XZ",
            "Velocity Y",
            "Velocity XZ",
            "Rotation Y",
            "Rotation Deviation XZ",
            "Angular velocity XZ",
            "Angular velocity Y",
            "PID Value Attitude",
            "PID Value Altitude",
            "PID Value Position",
            "Wind direction",
            "Wind intensity"});
            this.comboBoxValueToPlot.Location = new System.Drawing.Point(84, 19);
            this.comboBoxValueToPlot.Name = "comboBoxValueToPlot";
            this.comboBoxValueToPlot.Size = new System.Drawing.Size(224, 21);
            this.comboBoxValueToPlot.TabIndex = 8;
            this.comboBoxValueToPlot.SelectedIndexChanged += new System.EventHandler(this.comboBoxValueToPlot_SelectedIndexChanged);
            // 
            // lblDescValueToPlot
            // 
            this.lblDescValueToPlot.AutoSize = true;
            this.lblDescValueToPlot.Location = new System.Drawing.Point(9, 23);
            this.lblDescValueToPlot.Name = "lblDescValueToPlot";
            this.lblDescValueToPlot.Size = new System.Drawing.Size(69, 13);
            this.lblDescValueToPlot.TabIndex = 7;
            this.lblDescValueToPlot.Text = "Value to plot:";
            // 
            // btnRecord
            // 
            this.btnRecord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRecord.Location = new System.Drawing.Point(114, 236);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(113, 23);
            this.btnRecord.TabIndex = 6;
            this.btnRecord.Text = "Start recording";
            this.btnRecord.UseVisualStyleBackColor = true;
            this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);
            // 
            // WindowStatistics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 487);
            this.Controls.Add(this.groupBoxVisualization);
            this.Controls.Add(this.groupBoxGeneralValues);
            this.Controls.Add(this.groupBoxTorque);
            this.Controls.Add(this.groupBoxAngularVelocity);
            this.Controls.Add(this.groupBoxRotation);
            this.Controls.Add(this.groupBoxForce);
            this.Controls.Add(this.groupBoxVelocity);
            this.Controls.Add(this.groupBoxPosition);
            this.DoubleBuffered = true;
            this.MinimumSize = new System.Drawing.Size(350, 430);
            this.Name = "WindowStatistics";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Statistics";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.WindowStatistics_FormClosed);
            this.Load += new System.EventHandler(this.WindowStatistics_Load);
            this.ResizeEnd += new System.EventHandler(this.WindowStatistics_ResizeEnd);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxStats)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarYScale)).EndInit();
            this.groupBoxTorque.ResumeLayout(false);
            this.groupBoxTorque.PerformLayout();
            this.groupBoxAngularVelocity.ResumeLayout(false);
            this.groupBoxAngularVelocity.PerformLayout();
            this.groupBoxRotation.ResumeLayout(false);
            this.groupBoxRotation.PerformLayout();
            this.groupBoxForce.ResumeLayout(false);
            this.groupBoxForce.PerformLayout();
            this.groupBoxVelocity.ResumeLayout(false);
            this.groupBoxVelocity.PerformLayout();
            this.groupBoxPosition.ResumeLayout(false);
            this.groupBoxPosition.PerformLayout();
            this.groupBoxGeneralValues.ResumeLayout(false);
            this.groupBoxGeneralValues.PerformLayout();
            this.groupBoxVisualization.ResumeLayout(false);
            this.groupBoxVisualization.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarUpdateInterval)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxStats;
        private System.Windows.Forms.Button btnPlot;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.TrackBar trackBarYScale;
        private System.Windows.Forms.Label lblDescYScale;
        private System.Windows.Forms.Label lblSimulationRuntime;
        private System.Windows.Forms.GroupBox groupBoxTorque;
        private System.Windows.Forms.Label lblTorqueZ;
        private System.Windows.Forms.Label lblTorqueY;
        private System.Windows.Forms.Label lblTorqueX;
        private System.Windows.Forms.Label lblDescTorqueZ;
        private System.Windows.Forms.Label lblDescTorqueY;
        private System.Windows.Forms.Label lblDescTorqueX;
        private System.Windows.Forms.GroupBox groupBoxAngularVelocity;
        private System.Windows.Forms.Label lblAVelocityZ;
        private System.Windows.Forms.Label lblAVelocityY;
        private System.Windows.Forms.Label lblAVelocityX;
        private System.Windows.Forms.Label lblDescAVelocityZ;
        private System.Windows.Forms.Label lblDescAVelocityY;
        private System.Windows.Forms.Label lblDescAVelocityX;
        private System.Windows.Forms.GroupBox groupBoxRotation;
        private System.Windows.Forms.Label lblRotationZ;
        private System.Windows.Forms.Label lblRotationY;
        private System.Windows.Forms.Label lblRotationX;
        private System.Windows.Forms.Label lblDescRotationZ;
        private System.Windows.Forms.Label lblDescRotationY;
        private System.Windows.Forms.Label lblDescRotationX;
        private System.Windows.Forms.GroupBox groupBoxForce;
        private System.Windows.Forms.Label lblForceZ;
        private System.Windows.Forms.Label lblForceY;
        private System.Windows.Forms.Label lblForceX;
        private System.Windows.Forms.Label lblDescForceZ;
        private System.Windows.Forms.Label lblDescForceY;
        private System.Windows.Forms.Label lblDescForceX;
        private System.Windows.Forms.GroupBox groupBoxVelocity;
        private System.Windows.Forms.Label lblVelocityZ;
        private System.Windows.Forms.Label lblVelocityY;
        private System.Windows.Forms.Label lblVelocityX;
        private System.Windows.Forms.Label lblDescVelocityZ;
        private System.Windows.Forms.Label lblDescVelocityY;
        private System.Windows.Forms.Label lblDescVelocityX;
        private System.Windows.Forms.GroupBox groupBoxPosition;
        private System.Windows.Forms.Label lblPositionY;
        private System.Windows.Forms.Label lblDescPositionX;
        private System.Windows.Forms.Label lblDescPositionY;
        private System.Windows.Forms.Label lblDescPositionZ;
        private System.Windows.Forms.Label lblPositionX;
        private System.Windows.Forms.Label lblPositionZ;
        private System.Windows.Forms.GroupBox groupBoxGeneralValues;
        private System.Windows.Forms.GroupBox groupBoxVisualization;
        private System.Windows.Forms.Button btnRecord;
        private System.Windows.Forms.ComboBox comboBoxValueToPlot;
        private System.Windows.Forms.Label lblDescValueToPlot;
        private System.Windows.Forms.Label lblDescMulticopterWeight;
        private System.Windows.Forms.Label lblDescSimulationRuntime;
        private System.Windows.Forms.Label lblMulticopterWeight;
        private System.Windows.Forms.TrackBar trackBarUpdateInterval;
        private System.Windows.Forms.Label lblDescUpdateFreq;
    }
}