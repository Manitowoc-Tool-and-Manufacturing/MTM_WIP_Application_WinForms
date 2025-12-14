using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Models.DeveloperTools;
using MTM_WIP_Application_Winforms.Services.DeveloperTools;
using MTM_WIP_Application_Winforms.Services.ErrorHandling;

namespace MTM_WIP_Application_Winforms.Controls.DeveloperTools
{
    public partial class Control_DatabaseHealth : ThemedUserControl
    {
        #region Fields

        private IService_DeveloperTools? _devToolsService;
        private IService_ErrorHandler? _errorHandler;
        private bool _isInitialized;

        #endregion

        #region Constructors

        public Control_DatabaseHealth()
        {
            InitializeComponent();
        }

        #endregion

        #region Initialization

        public void Initialize(IService_DeveloperTools devToolsService, IService_ErrorHandler errorHandler)
        {
            _devToolsService = devToolsService ?? throw new ArgumentNullException(nameof(devToolsService));
            _errorHandler = errorHandler ?? throw new ArgumentNullException(nameof(errorHandler));
            _isInitialized = true;

            // Initial load
            _ = LoadDatabaseHealthAsync();
        }

        #endregion

        #region Methods

        private async Task LoadDatabaseHealthAsync()
        {
            if (!_isInitialized || _devToolsService == null || _errorHandler == null) return;

            try
            {
                Control_DatabaseHealth_Button_Refresh.Enabled = false;
                Cursor = Cursors.WaitCursor;

                var result = await _devToolsService.GetDatabaseHealthAsync();
                if (result.IsSuccess && result.Data != null)
                {
                    UpdateUI(result.Data);
                }
                else
                {
                    _errorHandler.ShowUserError(result.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                _errorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                    callerName: nameof(LoadDatabaseHealthAsync),
                    controlName: Name);
            }
            finally
            {
                Control_DatabaseHealth_Button_Refresh.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private void UpdateUI(Model_DatabaseHealth health)
        {
            if (health == null) return;

            Control_DatabaseHealth_Label_ConnectionStatus.Text = $"Connection Status: {health.StatusMessage}";
            Control_DatabaseHealth_Label_ConnectionStatus.ForeColor = health.IsConnected ? Color.Green : Color.Red;
            
            Control_DatabaseHealth_Label_ActiveConnections.Text = $"Active Connections: {health.ConnectionCount}";
            
            Control_DatabaseHealth_DataGridView_Tables.DataSource = health.TableStatistics;
            
            // Format grid
            if (Control_DatabaseHealth_DataGridView_Tables.Columns.Count > 0)
            {
                SetColumnHeader("TableName", "Table Name");
                SetColumnHeader("RowCount", "Rows");
                SetColumnHeader("DataSizeMB", "Data Size (MB)");
                SetColumnHeader("IndexSizeMB", "Index Size (MB)");
                SetColumnHeader("TotalSizeMB", "Total Size (MB)");
                
                Control_DatabaseHealth_DataGridView_Tables.AutoResizeColumns();
            }
        }

        private void SetColumnHeader(string columnName, string headerText)
        {
            if (Control_DatabaseHealth_DataGridView_Tables.Columns.Contains(columnName))
            {
                Control_DatabaseHealth_DataGridView_Tables.Columns[columnName].HeaderText = headerText;
            }
        }

        private async void Control_DatabaseHealth_Button_Refresh_Click(object sender, EventArgs e)
        {
            await LoadDatabaseHealthAsync();
        }

        #endregion
    }
}
