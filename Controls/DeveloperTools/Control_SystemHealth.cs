using System;
using System.Drawing;
using System.Windows.Forms;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Models.DeveloperTools;

namespace MTM_WIP_Application_Winforms.Controls.DeveloperTools
{
    public partial class Control_SystemHealth : ThemedUserControl
    {
        public event EventHandler? StatusClicked;

        public Control_SystemHealth()
        {
            InitializeComponent();
            SetupClickHandlers();
        }

        private void SetupClickHandlers()
        {
            this.Cursor = Cursors.Hand;
            this.Click += (s, e) => StatusClicked?.Invoke(this, EventArgs.Empty);
            
            foreach (Control c in this.Controls)
            {
                c.Cursor = Cursors.Hand;
                c.Click += (s, e) => StatusClicked?.Invoke(this, EventArgs.Empty);
            }
        }

        public void UpdateHealth(Model_SystemHealthStatus health)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<Model_SystemHealthStatus>(UpdateHealth), health);
                return;
            }

            Control_SystemHealth_Label_Status.Text = health.Message;
            Control_SystemHealth_Label_ErrorCount.Text = $"{health.ErrorCount} Errors";
            Control_SystemHealth_Label_WarningCount.Text = $"{health.WarningCount} Warnings";
            Control_SystemHealth_Label_LastCheck.Text = $"Last Checked: {health.Timestamp:HH:mm:ss}";

            switch (health.Status)
            {
                case Enum_HealthIndicator.Green:
                    Control_SystemHealth_Panel_StatusColor.BackColor = Color.ForestGreen;
                    Control_SystemHealth_Label_Status.ForeColor = Color.ForestGreen;
                    break;
                case Enum_HealthIndicator.Yellow:
                    Control_SystemHealth_Panel_StatusColor.BackColor = Color.Orange;
                    Control_SystemHealth_Label_Status.ForeColor = Color.Orange;
                    break;
                case Enum_HealthIndicator.Red:
                    Control_SystemHealth_Panel_StatusColor.BackColor = Color.Red;
                    Control_SystemHealth_Label_Status.ForeColor = Color.Red;
                    break;
            }
        }
    }
}

