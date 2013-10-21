using Microsoft.Ccr.Core;
using MulticopterSimulation.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MulticopterSimulation.Windows
{
    public partial class WindowSimulationSettings : Form
    {
        public enum OnActionActions
        {
            ACTION_BUTTONCLICKED_APPLY
        }

        // This is the PortSet for the different types of messages from the form
        public class WindowSimulationSettingsEvents : PortSet
            <OnLoad, OnClosed, OnAction, OnWindSettingsChanged>
        {
        }

        public class OnAction
        {
            public OnActionActions action;
            public MulticopterType multicopterType;
            public float propellerInclination;

            public OnAction(OnActionActions action, MulticopterType multicopterType, float propellerInclination)
            {
                this.action = action;
                this.multicopterType = multicopterType;
                this.propellerInclination = propellerInclination;
            }
        }

        public class OnWindSettingsChanged
        {
            public bool enableWindSimulation;
            public float windDirection;
            public float windIntensity;
            public float windDirectionFluctuation;
            public float windIntensityFluctuation;
        }

        // This port is passed across to the constructor, it allows messages to be sent back to the main service
        private WindowSimulationSettingsEvents eventsPort;

        private OnWindSettingsChanged onWindSettingsChanged;

        public WindowSimulationSettings(WindowSimulationSettingsEvents eventsPort)
        {
            this.eventsPort = eventsPort;
            onWindSettingsChanged = new OnWindSettingsChanged();

            this.ControlBox = false;

            InitializeComponent();
        }

        internal void SetInitialMulticopterSettings(MulticopterType multicopterType, float propellerInclination)
        {
            trackBarMulticopterType.Value = Globals.Instance.multicopterPropellerCount[multicopterType];
            trackBarPropellerInclination.Value = (int)(propellerInclination * 10);
        }

        internal void SetInitialWindValues(bool windSimulationActivated, float windDirection, float windIntensity, float windDirectionFluctuation, float windIntensityFluctuation)
        {
            checkBoxEnableWindSimulation.Checked = windSimulationActivated;
            trackBarWindDirection.Value = (int)windDirection;
            trackBarWindIntensity.Value = (int)(windIntensity * 100f);
            trackBarWindDirectionFluctuation.Value = (int)(windDirectionFluctuation * 100f);
            trackBarWindIntensityFluctuation.Value = (int)(windIntensityFluctuation * 100f);

            checkBoxEnableWindSimulation_CheckedChanged(this, null);
            trackBarWindDirection_Scroll(this, null);
            trackBarWindIntensity_Scroll(this, null);
            trackBarWindDirectionFluctuation_Scroll(this, null);
            trackBarWindIntensityFluctuation_Scroll(this, null);
        }

        private void WindowSimulationSettings_Load(object sender, EventArgs e)
        {
            eventsPort.Post(new OnLoad(this));
        }

        private void WindowSimulationSettings_FormClosed(object sender, FormClosedEventArgs e)
        {
            eventsPort.Post(new OnClosed(this));
        }

        private void trackBarMulticopterType_ValueChanged(object sender, EventArgs e)
        {
            byte newValue = (byte)(sender as TrackBar).Value;
            lblMulticopterType.Text = Globals.Instance.multicopterDescriptions[newValue];
        }

        private void trackBarPropellerInclination_ValueChanged(object sender, EventArgs e)
        {
            float newValue = (sender as TrackBar).Value / 10.0f;
            lblInclination.Text = newValue.ToString() + "°";
        }

        private void trackBarWindDirection_ValueChanged(object sender, EventArgs e)
        {
            float newValue = (sender as TrackBar).Value;
            lblWindDirection.Text = newValue.ToString() + "°";
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            var selectedMulticopterType = Globals.Instance.multicopterPropellerCount.FirstOrDefault(x => x.Value == trackBarMulticopterType.Value).Key;

            eventsPort.Post(
                new OnAction(
                    OnActionActions.ACTION_BUTTONCLICKED_APPLY, 
                    selectedMulticopterType,
                    trackBarPropellerInclination.Value / 10.0f
                )
            );
        }

        private void btnApplyLNDW_Click(object sender, EventArgs e)
        {       
            DialogResult result = MessageBox.Show(
                "This function is not properly implemented yet, the program may crash! Do you still want to continue with this action?",
                "Warning",
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Warning
            );

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                btnApply_Click(sender, e);
            }
        }

        private void trackBarWindDirection_Scroll(object sender, EventArgs e)
        {
            onWindSettingsChanged.windDirection = trackBarWindDirection.Value;
            lblWindDirection.Text = onWindSettingsChanged.windDirection.ToString();

            eventsPort.Post(onWindSettingsChanged);
        }

        private void trackBarWindIntensity_Scroll(object sender, EventArgs e)
        {
            onWindSettingsChanged.windIntensity = trackBarWindIntensity.Value / 100.0f;
            lblWindIntensity.Text = onWindSettingsChanged.windIntensity.ToString();

            eventsPort.Post(onWindSettingsChanged);
        }

        private void trackBarWindDirectionFluctuation_Scroll(object sender, EventArgs e)
        {
            onWindSettingsChanged.windDirectionFluctuation = trackBarWindDirectionFluctuation.Value / 100.0f;
            lblWindDirectionFluctuation.Text = onWindSettingsChanged.windDirectionFluctuation.ToString();

            eventsPort.Post(onWindSettingsChanged);
        }

        private void trackBarWindIntensityFluctuation_Scroll(object sender, EventArgs e)
        {
            onWindSettingsChanged.windIntensityFluctuation = trackBarWindIntensityFluctuation.Value / 100.0f;
            lblWindIntensityFluctuation.Text = onWindSettingsChanged.windIntensityFluctuation.ToString();

            eventsPort.Post(onWindSettingsChanged);
        }

        private void checkBoxEnableWindSimulation_CheckedChanged(object sender, EventArgs e)
        {
            onWindSettingsChanged.enableWindSimulation = checkBoxEnableWindSimulation.Checked;

            eventsPort.Post(onWindSettingsChanged);
        }
    }
}
