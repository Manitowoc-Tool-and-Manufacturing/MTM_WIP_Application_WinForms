using Microsoft.Extensions.DependencyInjection;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Services;
using MTM_WIP_Application_Winforms.Services.Visual;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services.Logging;

namespace MTM_WIP_Application_Winforms.Controls.Visual
{
    /// <summary>
    /// User control for discovering and searching die/tool information in the Visual database.
    /// Provides search functionality by part number or die/tool number.
    /// </summary>
    public partial class Control_DieToolDiscovery : ThemedUserControl
    {
        #region Fields

        private readonly IService_VisualDatabase? _visualService;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Control_DieToolDiscovery"/> class.
        /// </summary>
        public Control_DieToolDiscovery()
        {
            InitializeComponent();
            _visualService = Program.ServiceProvider?.GetService<IService_VisualDatabase>();

            if (_visualService == null)
            {
                LoggingUtility.Log("[Control_DieToolDiscovery] Visual Database Service not available in DI container");
            }
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handles the Click event of the search button.
        /// Executes die/tool search based on current criteria.
        /// </summary>
        private async void btnSearch_Click(object? sender, EventArgs e)
        {
            await PerformSearch();
        }

        /// <summary>
        /// Handles the KeyDown event of the search textbox.
        /// Triggers search when Enter key is pressed.
        /// </summary>
        private async void txtSearch_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                await PerformSearch();
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Performs the die/tool search operation based on current search criteria.
        /// </summary>
        /// <returns>Task representing the asynchronous operation.</returns>
        private async Task PerformSearch()
        {
            if (_visualService == null)
            {
                Service_ErrorHandler.ShowUserError("Visual Database Service is not available. Please restart the application.");
                LoggingUtility.Log("[Control_DieToolDiscovery] Search failed - Service not available");
                return;
            }

            string searchTerm = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(searchTerm))
            {
                Service_ErrorHandler.HandleValidationError(
                    "Please enter a search term.",
                    field: "Search Term",
                    callerName: nameof(PerformSearch),
                    controlName: this.Name);
                txtSearch.Focus();
                return;
            }

            LoggingUtility.Log($"[Control_DieToolDiscovery] Search initiated | SearchTerm={searchTerm}, SearchByPart={radioSearchByPart.Checked}");

            try
            {
                bool searchByPart = radioSearchByPart.Checked;
                
                var result = await _visualService.SearchDiesAsync(searchTerm, searchByPart);

                if (result.IsSuccess)
                {
                    gridResults.DataSource = result.Data;
                    Service_DataGridView.ApplySmartNumericFormatting(gridResults);
                    
                    if (result.Data != null && result.Data.Rows.Count > 0)
                    {
                        gridResults.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
                        LoggingUtility.Log($"[Control_DieToolDiscovery] Search completed successfully | ResultCount={result.Data.Rows.Count}");
                        Service_ErrorHandler.ShowInformation($"Found {result.Data.Rows.Count} result(s).");
                    }
                    else
                    {
                        LoggingUtility.Log($"[Control_DieToolDiscovery] Search completed with no results | SearchTerm={searchTerm}");
                        Service_ErrorHandler.ShowInformation("No results found for the specified search criteria.");
                    }
                }
                else
                {
                    Service_ErrorHandler.ShowUserError($"Search failed: {result.ErrorMessage}");
                    LoggingUtility.Log($"[Control_DieToolDiscovery] Search failed | ErrorMessage={result.ErrorMessage}");
                }
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(
                    ex,
                    Enum_ErrorSeverity.Medium,
                    contextData: new Dictionary<string, object>
                    {
                        ["User"] = Model_Application_Variables.User,
                        ["SearchTerm"] = searchTerm,
                        ["SearchByPart"] = radioSearchByPart.Checked,
                        ["Operation"] = "SearchDies"
                    },
                    callerName: nameof(PerformSearch),
                    controlName: this.Name);
            }
        }

        #endregion

        #region Cleanup / Dispose

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                try
                {
                    // Unsubscribe from events to prevent memory leaks
                    if (btnSearch != null)
                    {
                        btnSearch.Click -= btnSearch_Click;
                    }

                    if (txtSearch != null)
                    {
                        txtSearch.KeyDown -= txtSearch_KeyDown;
                    }

                    // Dispose components if present
                    components?.Dispose();
                }
                catch (Exception ex)
                {
                    // Log but don't throw during disposal
                    LoggingUtility.LogApplicationError(ex);
                }
            }

            base.Dispose(disposing);
        }

        #endregion
    }
}
