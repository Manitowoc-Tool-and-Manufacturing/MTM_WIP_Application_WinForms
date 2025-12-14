using System;
using System.Windows.Forms;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Models.DeveloperTools;
using MTM_WIP_Application_Winforms.Services.DeveloperTools;
using MTM_WIP_Application_Winforms.Services.ErrorHandling;

namespace MTM_WIP_Application_Winforms.Controls.DeveloperTools
{
    public partial class Control_LogStatistics : ThemedUserControl
    {
        private IService_DeveloperTools? _service;
        private IService_ErrorHandler? _errorHandler;

        public Control_LogStatistics()
        {
            InitializeComponent();
        }

        public void Initialize(IService_DeveloperTools service, IService_ErrorHandler errorHandler)
        {
            _service = service;
            _errorHandler = errorHandler;
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

        private async void Control_LogStatistics_Button_Sync_Click(object sender, EventArgs e)
        {
            if (_service == null) return;

            Control_LogStatistics_Button_Sync.Enabled = false;
            Control_LogStatistics_Label_SyncStatus.Text = "Preparing...";
            Control_LogStatistics_ProgressBar_Sync.Value = 0;

            var progress = new Progress<(int current, int total)>(update =>
            {
                if (InvokeRequired)
                {
                    Invoke(new Action(() => UpdateProgress(update.current, update.total)));
                }
                else
                {
                    UpdateProgress(update.current, update.total);
                }
            });

            try
            {
                var result = await _service.SyncLogsToDatabaseAsync(progress);
                Control_LogStatistics_Label_SyncStatus.Text = result.IsSuccess ? "Sync Complete" : "Sync Failed";
                if (!result.IsSuccess && _errorHandler != null)
                {
                    _errorHandler.ShowUserError(result.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                Control_LogStatistics_Label_SyncStatus.Text = "Error";
                _errorHandler?.HandleException(ex, Models.Enum_ErrorSeverity.Medium, callerName: nameof(Control_LogStatistics_Button_Sync_Click), controlName: this.Name);
            }
            finally
            {
                Control_LogStatistics_Button_Sync.Enabled = true;
            }
        }

        private async void Control_LogStatistics_Button_Purge_Click(object sender, EventArgs e)
        {
            if (_service == null) return;

            var confirm = MessageBox.Show("Are you sure you want to delete ALL log files? This cannot be undone.", 
                "Confirm Purge", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            
            if (confirm != DialogResult.Yes) return;

            Control_LogStatistics_Button_Purge.Enabled = false;
            Control_LogStatistics_Label_SyncStatus.Text = "Purging...";

            try
            {
                var result = await _service.PurgeLogsAsync();
                Control_LogStatistics_Label_SyncStatus.Text = result.IsSuccess ? "Purge Complete" : "Purge Failed";
                
                if (result.IsSuccess)
                {
                    MessageBox.Show(result.ErrorMessage, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (_errorHandler != null)
                {
                    _errorHandler.ShowUserError(result.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                Control_LogStatistics_Label_SyncStatus.Text = "Error";
                _errorHandler?.HandleException(ex, Models.Enum_ErrorSeverity.Medium, callerName: nameof(Control_LogStatistics_Button_Purge_Click), controlName: this.Name);
            }
            finally
            {
                Control_LogStatistics_Button_Purge.Enabled = true;
            }
        }

        private void UpdateProgress(int current, int total)
        {
            if (total > 0)
            {
                int percentage = (int)((double)current / total * 100);
                Control_LogStatistics_ProgressBar_Sync.Value = Math.Min(100, Math.Max(0, percentage));
                Control_LogStatistics_Label_SyncStatus.Text = $"Syncing Row {current} / {total}";
            }
        }
    }
}

