using Microsoft.Extensions.DependencyInjection;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Services;
using MTM_WIP_Application_Winforms.Services.Visual;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services.Logging;
using MTM_WIP_Application_Winforms.Helpers;

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

            // Ensure event handler is wired up (remove first to prevent duplicates)
            btnSearch.Click -= btnSearch_Click;
            btnSearch.Click += btnSearch_Click;

            // Wire up Coil Search button
            btnCoilSearch.Click -= btnCoilSearch_Click;
            btnCoilSearch.Click += btnCoilSearch_Click;

            // Wire up Where Used button
            btnWhereUsed.Click -= btnWhereUsed_Click;
            btnWhereUsed.Click += btnWhereUsed_Click;

            // Wire up Radio Buttons
            rbSearchByPart.CheckedChanged += (s, e) => UpdateSearchPlaceholder();
            rbSearchByDie.CheckedChanged += (s, e) => UpdateSearchPlaceholder();

            _visualService = Program.ServiceProvider?.GetService<IService_VisualDatabase>();

            if (_visualService == null)
            {
                LoggingUtility.Log("[Control_DieToolDiscovery] Visual Database Service not available in DI container");
            }
            else
            {
                InitializeSuggestionBoxes();
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
            LoggingUtility.Log("[Control_DieToolDiscovery] Search button clicked");
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
                e.Handled = true;
                LoggingUtility.Log("[Control_DieToolDiscovery] Enter key pressed in search box");
                await PerformSearch();
            }
        }

        /// <summary>
        /// Handles the Click event of the coil search button.
        /// </summary>
        private async void btnCoilSearch_Click(object? sender, EventArgs e)
        {
            LoggingUtility.Log("[Control_DieToolDiscovery] Coil Search button clicked");
            await PerformCoilSearch();
        }

        /// <summary>
        /// Handles the KeyDown event of the coil search textbox.
        /// Triggers search when Enter key is pressed.
        /// </summary>
        private async void txtCoilSearch_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                LoggingUtility.Log("[Control_DieToolDiscovery] Enter key pressed in coil search box");
                await PerformCoilSearch();
            }
        }

        #endregion

        #region Private Methods

        private void InitializeSuggestionBoxes()
        {
            if (_visualService == null) return;

            // Configure Die Search Suggestion Box
            txtSearch.EnableSuggestions = true;
            txtSearch.ShowF4Button = true;
            
            if (txtSearch.TextBox != null)
            {
                txtSearch.TextBox.DataProvider = async () =>
                {
                    if (rbSearchByPart.Checked)
                    {
                        var result = await _visualService.GetPartIdsAsync();
                        return result.IsSuccess && result.Data != null ? result.Data : new List<string>();
                    }
                    else
                    {
                        var result = await _visualService.GetDieIdsAsync();
                        return result.IsSuccess && result.Data != null ? result.Data : new List<string>();
                    }
                };

                // Wire up Enter key for search
                txtSearch.TextBox.KeyDown += txtSearch_KeyDown;
            }

            // Coil Search
            txtCoilSearch.EnableSuggestions = true;
            txtCoilSearch.ShowF4Button = true;
            if (txtCoilSearch.TextBox != null)
            {
                txtCoilSearch.TextBox.DataProvider = async () =>
                {
                    var result = await _visualService.GetCoilFlatstockPartIdsAsync();
                    return result.IsSuccess && result.Data != null ? result.Data : new List<string>();
                };

                // Wire up Enter key for coil search
                txtCoilSearch.TextBox.KeyDown += txtCoilSearch_KeyDown;
            }
        }

        private void UpdateSearchPlaceholder()
        {
            if (rbSearchByPart.Checked)
            {
                txtSearch.PlaceholderText = "Enter Part Number";
            }
            else
            {
                txtSearch.PlaceholderText = "Enter Die Number";
            }
        }

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

            string searchTerm = txtSearch.Text?.Trim() ?? "";
            LoggingUtility.Log($"[Control_DieToolDiscovery] PerformSearch | Term='{searchTerm}'");

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

            bool searchByPart = rbSearchByPart.Checked;
            LoggingUtility.Log($"[Control_DieToolDiscovery] Search initiated | SearchTerm={searchTerm}, SearchByPart={searchByPart}");

            try
            {
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
                        ["SearchByPart"] = searchByPart,
                        ["Operation"] = "SearchDies"
                    },
                    callerName: nameof(PerformSearch),
                    controlName: this.Name);
            }
        }

        /// <summary>
        /// Performs the coil/flatstock search operation.
        /// </summary>
        private async Task PerformCoilSearch()
        {
            if (_visualService == null)
            {
                Service_ErrorHandler.ShowUserError("Visual Database Service is not available. Please restart the application.");
                return;
            }

            string partNumber = txtCoilSearch.Text?.Trim() ?? "";
            if (string.IsNullOrEmpty(partNumber))
            {
                Service_ErrorHandler.HandleValidationError(
                    "Please enter a part number.",
                    field: "Part Number",
                    callerName: nameof(PerformCoilSearch),
                    controlName: this.Name);
                txtCoilSearch.Focus();
                return;
            }

            LoggingUtility.Log($"[Control_DieToolDiscovery] Coil Search initiated | PartNumber={partNumber}");

            try
            {
                var result = await _visualService.GetCoilFlatstockInfoAsync(partNumber);

                if (result.IsSuccess)
                {
                    var info = result.Data;
                    txtThickness.Text = info?.Thickness;
                    txtWidth.Text = info?.Width;
                    txtLength.Text = info?.Length;
                    txtGauge.Text = info?.Gauge;
                    txtWhereUsed.Text = info?.WhereUsed;
                    txtProgression.Text = info?.Progression;
                    txtCustomer.Text = info?.Customer;
                    txtScrapLocation.Text = info?.ScrapLocation;
                    txtGenericType.Text = info?.GenericType;
                    txtDetailedType.Text = info?.DetailedType;
                    txtAutoIssueLocation.Text = info?.AutoIssueLocation;
                    LoggingUtility.Log($"[Control_DieToolDiscovery] Coil Search completed successfully");
                }
                else
                {
                    Service_ErrorHandler.ShowUserError(result.ErrorMessage);
                    ClearCoilFields();
                }
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, 
                    callerName: nameof(PerformCoilSearch), controlName: this.Name);
                ClearCoilFields();
            }
        }

        private void ClearCoilFields()
        {
            txtThickness.Text = string.Empty;
            txtWidth.Text = string.Empty;
            txtLength.Text = string.Empty;
            txtGauge.Text = string.Empty;
            txtWhereUsed.Text = string.Empty;
            txtProgression.Text = string.Empty;
            txtCustomer.Text = string.Empty;
            txtScrapLocation.Text = string.Empty;
            txtGenericType.Text = string.Empty;
            txtDetailedType.Text = string.Empty;
            txtAutoIssueLocation.Text = string.Empty;
        }

        private async void btnWhereUsed_Click(object? sender, EventArgs e)
        {
            if (_visualService == null) return;

            string partId = txtSearch.Text?.Trim() ?? "";
            if (string.IsNullOrEmpty(partId))
            {
                Service_ErrorHandler.ShowInformation("Please enter a Part Number to search for usage.");
                return;
            }

            try
            {
                var result = await _visualService.GetWhereUsedAsync(partId);
                if (result.IsSuccess)
                {
                    gridResults.DataSource = result.Data;
                    Service_DataGridView.ApplySmartNumericFormatting(gridResults);
                    
                    if (result.Data != null && result.Data.Rows.Count > 0)
                    {
                        gridResults.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
                        Service_ErrorHandler.ShowInformation($"Found {result.Data.Rows.Count} parent part(s).");
                    }
                    else
                    {
                        Service_ErrorHandler.ShowInformation("No parent parts found (Where Used).");
                    }
                }
                else
                {
                    Service_ErrorHandler.ShowError(result.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, callerName: nameof(btnWhereUsed_Click), controlName: this.Name);
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

                    if (btnCoilSearch != null)
                    {
                        btnCoilSearch.Click -= btnCoilSearch_Click;
                    }

                    // txtSearch KeyDown removed

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
