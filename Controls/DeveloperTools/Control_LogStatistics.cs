using System;
using System.Windows.Forms;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Models.DeveloperTools;

namespace MTM_WIP_Application_Winforms.Controls.DeveloperTools
{
    public partial class Control_LogStatistics : ThemedUserControl
    {
        public Control_LogStatistics()
        {
            InitializeComponent();
        }

        public void UpdateStatistics(Model_LogStatistics stats)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<Model_LogStatistics>(UpdateStatistics), stats);
                return;
            }

            Control_LogStatistics_Label_Total.Text = stats.TotalCount.ToString("N0");
            Control_LogStatistics_Label_Errors.Text = stats.ErrorCount.ToString("N0");
            Control_LogStatistics_Label_Warnings.Text = stats.WarningCount.ToString("N0");
            Control_LogStatistics_Label_Info.Text = stats.InfoCount.ToString("N0");
        }
    }
}

