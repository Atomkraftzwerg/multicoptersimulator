namespace MulticopterSimulation.Windows
{
    partial class WindowSimulationSettings
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
            this.trackBarMulticopterType = new System.Windows.Forms.TrackBar();
            this.groupBoxMulticopterSettings = new System.Windows.Forms.GroupBox();
            this.lblDescInclination = new System.Windows.Forms.Label();
            this.lblDescMulticopterType = new System.Windows.Forms.Label();
            this.btnApply = new System.Windows.Forms.Button();
            this.lblInclination = new System.Windows.Forms.Label();
            this.trackBarPropellerInclination = new System.Windows.Forms.TrackBar();
            this.lblMulticopterType = new System.Windows.Forms.Label();
            this.groupBoxWindSimulation = new System.Windows.Forms.GroupBox();
            this.lblWindIntensityFluctuation = new System.Windows.Forms.Label();
            this.lblDescIntensityFluctuation = new System.Windows.Forms.Label();
            this.lblDescDirFluctuation = new System.Windows.Forms.Label();
            this.trackBarWindIntensityFluctuation = new System.Windows.Forms.TrackBar();
            this.lblWindDirectionFluctuation = new System.Windows.Forms.Label();
            this.trackBarWindDirectionFluctuation = new System.Windows.Forms.TrackBar();
            this.lblWindIntensity = new System.Windows.Forms.Label();
            this.lblDescWindIntensity = new System.Windows.Forms.Label();
            this.lblDescWindDirection = new System.Windows.Forms.Label();
            this.checkBoxEnableWindSimulation = new System.Windows.Forms.CheckBox();
            this.trackBarWindIntensity = new System.Windows.Forms.TrackBar();
            this.lblWindDirection = new System.Windows.Forms.Label();
            this.trackBarWindDirection = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMulticopterType)).BeginInit();
            this.groupBoxMulticopterSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPropellerInclination)).BeginInit();
            this.groupBoxWindSimulation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarWindIntensityFluctuation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarWindDirectionFluctuation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarWindIntensity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarWindDirection)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBarMulticopterType
            // 
            this.trackBarMulticopterType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarMulticopterType.AutoSize = false;
            this.trackBarMulticopterType.LargeChange = 1;
            this.trackBarMulticopterType.Location = new System.Drawing.Point(6, 37);
            this.trackBarMulticopterType.Maximum = 8;
            this.trackBarMulticopterType.Minimum = 3;
            this.trackBarMulticopterType.Name = "trackBarMulticopterType";
            this.trackBarMulticopterType.Size = new System.Drawing.Size(325, 28);
            this.trackBarMulticopterType.TabIndex = 0;
            this.trackBarMulticopterType.Value = 4;
            this.trackBarMulticopterType.ValueChanged += new System.EventHandler(this.trackBarMulticopterType_ValueChanged);
            // 
            // groupBoxMulticopterSettings
            // 
            this.groupBoxMulticopterSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxMulticopterSettings.Controls.Add(this.lblDescInclination);
            this.groupBoxMulticopterSettings.Controls.Add(this.lblDescMulticopterType);
            this.groupBoxMulticopterSettings.Controls.Add(this.btnApply);
            this.groupBoxMulticopterSettings.Controls.Add(this.lblInclination);
            this.groupBoxMulticopterSettings.Controls.Add(this.trackBarPropellerInclination);
            this.groupBoxMulticopterSettings.Controls.Add(this.lblMulticopterType);
            this.groupBoxMulticopterSettings.Controls.Add(this.trackBarMulticopterType);
            this.groupBoxMulticopterSettings.Location = new System.Drawing.Point(12, 12);
            this.groupBoxMulticopterSettings.Name = "groupBoxMulticopterSettings";
            this.groupBoxMulticopterSettings.Size = new System.Drawing.Size(337, 198);
            this.groupBoxMulticopterSettings.TabIndex = 1;
            this.groupBoxMulticopterSettings.TabStop = false;
            this.groupBoxMulticopterSettings.Text = "Multicopter settings";
            // 
            // lblDescInclination
            // 
            this.lblDescInclination.AutoSize = true;
            this.lblDescInclination.Location = new System.Drawing.Point(11, 96);
            this.lblDescInclination.Name = "lblDescInclination";
            this.lblDescInclination.Size = new System.Drawing.Size(101, 13);
            this.lblDescInclination.TabIndex = 3;
            this.lblDescInclination.Text = "Propeller inclination:";
            // 
            // lblDescMulticopterType
            // 
            this.lblDescMulticopterType.AutoSize = true;
            this.lblDescMulticopterType.Location = new System.Drawing.Point(11, 21);
            this.lblDescMulticopterType.Name = "lblDescMulticopterType";
            this.lblDescMulticopterType.Size = new System.Drawing.Size(85, 13);
            this.lblDescMulticopterType.TabIndex = 2;
            this.lblDescMulticopterType.Text = "Multicopter type:";
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnApply.Location = new System.Drawing.Point(117, 165);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(99, 27);
            this.btnApply.TabIndex = 3;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApplyLNDW_Click);
            // 
            // lblInclination
            // 
            this.lblInclination.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInclination.Location = new System.Drawing.Point(304, 112);
            this.lblInclination.Name = "lblInclination";
            this.lblInclination.Size = new System.Drawing.Size(27, 29);
            this.lblInclination.TabIndex = 1;
            this.lblInclination.Text = "0°";
            this.lblInclination.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // trackBarPropellerInclination
            // 
            this.trackBarPropellerInclination.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarPropellerInclination.AutoSize = false;
            this.trackBarPropellerInclination.BackColor = System.Drawing.SystemColors.Control;
            this.trackBarPropellerInclination.LargeChange = 10;
            this.trackBarPropellerInclination.Location = new System.Drawing.Point(15, 112);
            this.trackBarPropellerInclination.Maximum = 450;
            this.trackBarPropellerInclination.Minimum = -450;
            this.trackBarPropellerInclination.Name = "trackBarPropellerInclination";
            this.trackBarPropellerInclination.Size = new System.Drawing.Size(293, 29);
            this.trackBarPropellerInclination.TabIndex = 0;
            this.trackBarPropellerInclination.TickFrequency = 50;
            this.trackBarPropellerInclination.ValueChanged += new System.EventHandler(this.trackBarPropellerInclination_ValueChanged);
            // 
            // lblMulticopterType
            // 
            this.lblMulticopterType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMulticopterType.Location = new System.Drawing.Point(10, 68);
            this.lblMulticopterType.Name = "lblMulticopterType";
            this.lblMulticopterType.Size = new System.Drawing.Size(321, 14);
            this.lblMulticopterType.TabIndex = 1;
            this.lblMulticopterType.Text = "Quadrocopter (4 propellers)";
            this.lblMulticopterType.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // groupBoxWindSimulation
            // 
            this.groupBoxWindSimulation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxWindSimulation.Controls.Add(this.lblWindIntensityFluctuation);
            this.groupBoxWindSimulation.Controls.Add(this.lblDescIntensityFluctuation);
            this.groupBoxWindSimulation.Controls.Add(this.lblDescDirFluctuation);
            this.groupBoxWindSimulation.Controls.Add(this.trackBarWindIntensityFluctuation);
            this.groupBoxWindSimulation.Controls.Add(this.lblWindDirectionFluctuation);
            this.groupBoxWindSimulation.Controls.Add(this.trackBarWindDirectionFluctuation);
            this.groupBoxWindSimulation.Controls.Add(this.lblWindIntensity);
            this.groupBoxWindSimulation.Controls.Add(this.lblDescWindIntensity);
            this.groupBoxWindSimulation.Controls.Add(this.lblDescWindDirection);
            this.groupBoxWindSimulation.Controls.Add(this.checkBoxEnableWindSimulation);
            this.groupBoxWindSimulation.Controls.Add(this.trackBarWindIntensity);
            this.groupBoxWindSimulation.Controls.Add(this.lblWindDirection);
            this.groupBoxWindSimulation.Controls.Add(this.trackBarWindDirection);
            this.groupBoxWindSimulation.Location = new System.Drawing.Point(12, 216);
            this.groupBoxWindSimulation.Name = "groupBoxWindSimulation";
            this.groupBoxWindSimulation.Size = new System.Drawing.Size(337, 147);
            this.groupBoxWindSimulation.TabIndex = 5;
            this.groupBoxWindSimulation.TabStop = false;
            this.groupBoxWindSimulation.Text = "Wind simulation";
            // 
            // lblWindIntensityFluctuation
            // 
            this.lblWindIntensityFluctuation.Location = new System.Drawing.Point(287, 108);
            this.lblWindIntensityFluctuation.Name = "lblWindIntensityFluctuation";
            this.lblWindIntensityFluctuation.Size = new System.Drawing.Size(44, 24);
            this.lblWindIntensityFluctuation.TabIndex = 12;
            this.lblWindIntensityFluctuation.Text = "0";
            this.lblWindIntensityFluctuation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDescIntensityFluctuation
            // 
            this.lblDescIntensityFluctuation.AutoSize = true;
            this.lblDescIntensityFluctuation.Location = new System.Drawing.Point(170, 92);
            this.lblDescIntensityFluctuation.Name = "lblDescIntensityFluctuation";
            this.lblDescIntensityFluctuation.Size = new System.Drawing.Size(160, 13);
            this.lblDescIntensityFluctuation.TabIndex = 11;
            this.lblDescIntensityFluctuation.Text = "Wind intensity fluctuation speed:";
            // 
            // lblDescDirFluctuation
            // 
            this.lblDescDirFluctuation.AutoSize = true;
            this.lblDescDirFluctuation.Location = new System.Drawing.Point(6, 92);
            this.lblDescDirFluctuation.Name = "lblDescDirFluctuation";
            this.lblDescDirFluctuation.Size = new System.Drawing.Size(162, 13);
            this.lblDescDirFluctuation.TabIndex = 10;
            this.lblDescDirFluctuation.Text = "Wind direction fluctuation speed:";
            // 
            // trackBarWindIntensityFluctuation
            // 
            this.trackBarWindIntensityFluctuation.AutoSize = false;
            this.trackBarWindIntensityFluctuation.Location = new System.Drawing.Point(173, 108);
            this.trackBarWindIntensityFluctuation.Maximum = 1000;
            this.trackBarWindIntensityFluctuation.Name = "trackBarWindIntensityFluctuation";
            this.trackBarWindIntensityFluctuation.Size = new System.Drawing.Size(120, 24);
            this.trackBarWindIntensityFluctuation.TabIndex = 9;
            this.trackBarWindIntensityFluctuation.TickFrequency = 100;
            this.trackBarWindIntensityFluctuation.Scroll += new System.EventHandler(this.trackBarWindIntensityFluctuation_Scroll);
            // 
            // lblWindDirectionFluctuation
            // 
            this.lblWindDirectionFluctuation.Location = new System.Drawing.Point(132, 108);
            this.lblWindDirectionFluctuation.Name = "lblWindDirectionFluctuation";
            this.lblWindDirectionFluctuation.Size = new System.Drawing.Size(35, 24);
            this.lblWindDirectionFluctuation.TabIndex = 8;
            this.lblWindDirectionFluctuation.Text = "0";
            this.lblWindDirectionFluctuation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // trackBarWindDirectionFluctuation
            // 
            this.trackBarWindDirectionFluctuation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarWindDirectionFluctuation.AutoSize = false;
            this.trackBarWindDirectionFluctuation.Location = new System.Drawing.Point(6, 108);
            this.trackBarWindDirectionFluctuation.Maximum = 1000;
            this.trackBarWindDirectionFluctuation.Name = "trackBarWindDirectionFluctuation";
            this.trackBarWindDirectionFluctuation.Size = new System.Drawing.Size(120, 24);
            this.trackBarWindDirectionFluctuation.TabIndex = 7;
            this.trackBarWindDirectionFluctuation.TickFrequency = 100;
            this.trackBarWindDirectionFluctuation.Scroll += new System.EventHandler(this.trackBarWindDirectionFluctuation_Scroll);
            // 
            // lblWindIntensity
            // 
            this.lblWindIntensity.Location = new System.Drawing.Point(290, 59);
            this.lblWindIntensity.Name = "lblWindIntensity";
            this.lblWindIntensity.Size = new System.Drawing.Size(41, 24);
            this.lblWindIntensity.TabIndex = 6;
            this.lblWindIntensity.Text = "0";
            this.lblWindIntensity.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDescWindIntensity
            // 
            this.lblDescWindIntensity.AutoSize = true;
            this.lblDescWindIntensity.Location = new System.Drawing.Point(170, 43);
            this.lblDescWindIntensity.Name = "lblDescWindIntensity";
            this.lblDescWindIntensity.Size = new System.Drawing.Size(76, 13);
            this.lblDescWindIntensity.TabIndex = 5;
            this.lblDescWindIntensity.Text = "Wind intensity:";
            // 
            // lblDescWindDirection
            // 
            this.lblDescWindDirection.AutoSize = true;
            this.lblDescWindDirection.Location = new System.Drawing.Point(6, 43);
            this.lblDescWindDirection.Name = "lblDescWindDirection";
            this.lblDescWindDirection.Size = new System.Drawing.Size(78, 13);
            this.lblDescWindDirection.TabIndex = 4;
            this.lblDescWindDirection.Text = "Wind direction:";
            // 
            // checkBoxEnableWindSimulation
            // 
            this.checkBoxEnableWindSimulation.AutoSize = true;
            this.checkBoxEnableWindSimulation.Checked = true;
            this.checkBoxEnableWindSimulation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxEnableWindSimulation.Location = new System.Drawing.Point(11, 19);
            this.checkBoxEnableWindSimulation.Name = "checkBoxEnableWindSimulation";
            this.checkBoxEnableWindSimulation.Size = new System.Drawing.Size(133, 17);
            this.checkBoxEnableWindSimulation.TabIndex = 3;
            this.checkBoxEnableWindSimulation.Text = "Enable wind simulation";
            this.checkBoxEnableWindSimulation.UseVisualStyleBackColor = true;
            this.checkBoxEnableWindSimulation.CheckedChanged += new System.EventHandler(this.checkBoxEnableWindSimulation_CheckedChanged);
            // 
            // trackBarWindIntensity
            // 
            this.trackBarWindIntensity.AutoSize = false;
            this.trackBarWindIntensity.Location = new System.Drawing.Point(173, 59);
            this.trackBarWindIntensity.Maximum = 1000;
            this.trackBarWindIntensity.Name = "trackBarWindIntensity";
            this.trackBarWindIntensity.Size = new System.Drawing.Size(120, 24);
            this.trackBarWindIntensity.TabIndex = 2;
            this.trackBarWindIntensity.TickFrequency = 100;
            this.trackBarWindIntensity.Scroll += new System.EventHandler(this.trackBarWindIntensity_Scroll);
            // 
            // lblWindDirection
            // 
            this.lblWindDirection.Location = new System.Drawing.Point(132, 59);
            this.lblWindDirection.Name = "lblWindDirection";
            this.lblWindDirection.Size = new System.Drawing.Size(35, 24);
            this.lblWindDirection.TabIndex = 1;
            this.lblWindDirection.Text = "0°";
            this.lblWindDirection.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // trackBarWindDirection
            // 
            this.trackBarWindDirection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarWindDirection.AutoSize = false;
            this.trackBarWindDirection.Location = new System.Drawing.Point(6, 59);
            this.trackBarWindDirection.Maximum = 359;
            this.trackBarWindDirection.Name = "trackBarWindDirection";
            this.trackBarWindDirection.Size = new System.Drawing.Size(120, 24);
            this.trackBarWindDirection.TabIndex = 0;
            this.trackBarWindDirection.TickFrequency = 45;
            this.trackBarWindDirection.Scroll += new System.EventHandler(this.trackBarWindDirection_Scroll);
            this.trackBarWindDirection.ValueChanged += new System.EventHandler(this.trackBarWindDirection_ValueChanged);
            // 
            // WindowSimulationSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 375);
            this.Controls.Add(this.groupBoxWindSimulation);
            this.Controls.Add(this.groupBoxMulticopterSettings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Location = new System.Drawing.Point(10, 400);
            this.MaximizeBox = false;
            this.Name = "WindowSimulationSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Simulation settings";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.WindowSimulationSettings_FormClosed);
            this.Load += new System.EventHandler(this.WindowSimulationSettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMulticopterType)).EndInit();
            this.groupBoxMulticopterSettings.ResumeLayout(false);
            this.groupBoxMulticopterSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPropellerInclination)).EndInit();
            this.groupBoxWindSimulation.ResumeLayout(false);
            this.groupBoxWindSimulation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarWindIntensityFluctuation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarWindDirectionFluctuation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarWindIntensity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarWindDirection)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TrackBar trackBarMulticopterType;
        private System.Windows.Forms.GroupBox groupBoxMulticopterSettings;
        private System.Windows.Forms.Label lblMulticopterType;
        private System.Windows.Forms.Label lblInclination;
        private System.Windows.Forms.TrackBar trackBarPropellerInclination;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.GroupBox groupBoxWindSimulation;
        private System.Windows.Forms.Label lblWindDirection;
        private System.Windows.Forms.TrackBar trackBarWindDirection;
        private System.Windows.Forms.Label lblDescInclination;
        private System.Windows.Forms.Label lblDescMulticopterType;
        private System.Windows.Forms.TrackBar trackBarWindIntensity;
        private System.Windows.Forms.CheckBox checkBoxEnableWindSimulation;
        private System.Windows.Forms.Label lblDescWindDirection;
        private System.Windows.Forms.Label lblWindIntensityFluctuation;
        private System.Windows.Forms.Label lblDescIntensityFluctuation;
        private System.Windows.Forms.Label lblDescDirFluctuation;
        private System.Windows.Forms.TrackBar trackBarWindIntensityFluctuation;
        private System.Windows.Forms.Label lblWindDirectionFluctuation;
        private System.Windows.Forms.TrackBar trackBarWindDirectionFluctuation;
        private System.Windows.Forms.Label lblWindIntensity;
        private System.Windows.Forms.Label lblDescWindIntensity;
    }
}