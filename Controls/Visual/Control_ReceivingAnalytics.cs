using System.Data;
using Microsoft.Extensions.DependencyInjection;
using MTM_WIP_Application_Winforms.Components.Shared;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;
using MTM_WIP_Application_Winforms.Services.Visual;
using MTM_WIP_Application_Winforms.Services.Logging;
using MTM_WIP_Application_Winforms.Models.Enums;

namespace MTM_WIP_Application_Winforms.Controls.Visual
{
    public partial class Control_ReceivingAnalytics : ThemedUserControl
    {
        #region Fields
        private readonly IService_VisualDatabase? _visualService;
        private CancellationTokenSource? _analyticsRequestCancellationTokenSource;
        private CancellationTokenSource? _fetchRequestCancellationTokenSource;
        private bool _isHandlingFilterLogic = false;

        // Animation controls for expand/collapse
        private readonly Component_TextAnimationSequence _dateRangeAnimation;
        private readonly Component_TextAnimationSequence _filtersAnimation;
        private readonly Component_TextAnimationSequence _poStatesAnimation;
        private readonly Component_TextAnimationSequence _deliveryStatesAnimation;
        private readonly Component_TextAnimationSequence _receivingScopeAnimation;
        private readonly Component_TextAnimationSequence _searchPanelAnimation;

        // Expanded state tracking
        private bool _isDateRangeExpanded = true;
        private bool _isFiltersExpanded = true;
        private bool _isPOStatesExpanded = true;
        private bool _isDeliveryStatesExpanded = true;
        private bool _isReceivingScopeExpanded = true;
        private bool _isSearchPanelExpanded = true;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Control_ReceivingAnalytics"/> class.
        /// </summary>
        public Control_ReceivingAnalytics()
        {
            InitializeComponent();
            this.MinimumSize = new Size(1100, 700);
            _visualService = Program.ServiceProvider?.GetService<IService_VisualDatabase>();
            Disposed += (_, _) => CancelPendingRequests();

            // Initialize Animation Controls
            _dateRangeAnimation = new Component_TextAnimationSequence();
            _dateRangeAnimation.TargetButton = Control_ReceivingAnalytics_Button_DateRangeHeader;
            UpdateSectionState(_dateRangeAnimation, Control_ReceivingAnalytics_Button_DateRangeHeader, _isDateRangeExpanded);

            _filtersAnimation = new Component_TextAnimationSequence();
            _filtersAnimation.TargetButton = Control_ReceivingAnalytics_Button_FiltersHeader;
            UpdateSectionState(_filtersAnimation, Control_ReceivingAnalytics_Button_FiltersHeader, _isFiltersExpanded);

            _poStatesAnimation = new Component_TextAnimationSequence();
            _poStatesAnimation.TargetButton = Control_ReceivingAnalytics_Button_POStatesHeader;
            UpdateSectionState(_poStatesAnimation, Control_ReceivingAnalytics_Button_POStatesHeader, _isPOStatesExpanded);

            _deliveryStatesAnimation = new Component_TextAnimationSequence();
            _deliveryStatesAnimation.TargetButton = Control_ReceivingAnalytics_Button_DeliveryStatesHeader;
            UpdateSectionState(_deliveryStatesAnimation, Control_ReceivingAnalytics_Button_DeliveryStatesHeader, _isDeliveryStatesExpanded);

            _receivingScopeAnimation = new Component_TextAnimationSequence();
            _receivingScopeAnimation.TargetButton = Control_ReceivingAnalytics_Button_ReceivingScopeHeader;
            UpdateSectionState(_receivingScopeAnimation, Control_ReceivingAnalytics_Button_ReceivingScopeHeader, _isReceivingScopeExpanded);

            _searchPanelAnimation = new Component_TextAnimationSequence();
            _searchPanelAnimation.TargetButton = button1;
            _searchPanelAnimation.SuffixText = " All";
            UpdateSectionState(_searchPanelAnimation, button1, _isSearchPanelExpanded);

            // Initialize Suggestion Boxes
            InitializeSuggestionBoxes();

            // Set default dates to current week (Monday - Friday)
            SetCurrentWeek();

            // Set default checkboxes
            Control_ReceivingAnalytics_CheckBox_ShowClosed.Checked = false;
            Control_ReceivingAnalytics_CheckBox_ShowOutsideService.Checked = false;
            Control_ReceivingAnalytics_CheckBox_ShowLate.Checked = true;
            Control_ReceivingAnalytics_CheckBox_ShowPartial.Checked = true;
            Control_ReceivingAnalytics_CheckBox_ShowOnTime.Checked = true;
            Control_ReceivingAnalytics_CheckBox_ShowWithPartID.Checked = false;
            Control_ReceivingAnalytics_CheckBox_ShowOpen.Checked = true;
            Control_ReceivingAnalytics_CheckBox_ShowMMC.Checked = false;
            Control_ReceivingAnalytics_CheckBox_ShowMMF.Checked = false;

            // Wire up events for auto-refresh
            WireUpEvents();

            // Add context menu for column ordering
            var contextMenu = new ContextMenuStrip();
            var itemColumnOrder = new ToolStripMenuItem("Column Order...");
            itemColumnOrder.Click += ContextMenuItem_ColumnOrder_Click;
            contextMenu.Items.Add(itemColumnOrder);
            Control_ReceivingAnalytics_DataGridView_Results.ContextMenuStrip = contextMenu;

            // Initial Load
            _ = FetchDataAsync();
        }
        #endregion

        #region Methods
        private CancellationTokenSource BeginAnalyticsRequest()
        {
            CancelPendingAnalyticsRequest();
            _analyticsRequestCancellationTokenSource = new CancellationTokenSource();
            return _analyticsRequestCancellationTokenSource;
        }

        private CancellationTokenSource BeginFetchRequest()
        {
            CancelPendingFetchRequest();
            _fetchRequestCancellationTokenSource = new CancellationTokenSource();
            return _fetchRequestCancellationTokenSource;
        }

        private void CancelPendingAnalyticsRequest()
        {
            _analyticsRequestCancellationTokenSource?.Cancel();
            _analyticsRequestCancellationTokenSource?.Dispose();
            _analyticsRequestCancellationTokenSource = null;
        }

        private void CancelPendingFetchRequest()
        {
            _fetchRequestCancellationTokenSource?.Cancel();
            _fetchRequestCancellationTokenSource?.Dispose();
            _fetchRequestCancellationTokenSource = null;
        }

        private void CancelPendingRequests()
        {
            CancelPendingFetchRequest();
            CancelPendingAnalyticsRequest();
        }

        /// <summary>
        /// Applies the theme to the control and restores legend colors.
        /// </summary>
        /// <param name="theme">The theme to apply.</param>
        protected override void ApplyTheme(Model_Shared_UserUiColors theme)
        {
            base.ApplyTheme(theme);

            // Restore legend colors that might have been overwritten by the theme
            if (Control_ReceivingAnalytics_Panel_LegendClosed != null) Control_ReceivingAnalytics_Panel_LegendClosed.BackColor = Color.FromArgb(200, 255, 200);
            if (Control_ReceivingAnalytics_Panel_LegendLate != null) Control_ReceivingAnalytics_Panel_LegendLate.BackColor = Color.FromArgb(255, 200, 200);
            if (Control_ReceivingAnalytics_Panel_LegendPartial != null) Control_ReceivingAnalytics_Panel_LegendPartial.BackColor = Color.FromArgb(255, 255, 200);
            if (Control_ReceivingAnalytics_Panel_LegendOnTime != null) Control_ReceivingAnalytics_Panel_LegendOnTime.BackColor = Color.FromArgb(200, 240, 255);
        }

        private void WireUpEvents()
        {
            // Search Button
            Control_ReceivingAnalytics_Button_Search.Click += async (s, e) => await FetchDataAsync();

            // Analytics Button
            Control_ReceivingAnalytics_Button_Analytics.Click += Control_ReceivingAnalytics_Button_Analytics_Click;

            // DataGridView Events
            Control_ReceivingAnalytics_DataGridView_Results.CellDoubleClick += DataGridViewResults_CellDoubleClick;
            Control_ReceivingAnalytics_DataGridView_Results.DataBindingComplete += (s, e) => ApplyRowColors();
            Control_ReceivingAnalytics_DataGridView_Results.Sorted += (s, e) => ApplyRowColors();

            // Reloading filters
            Control_ReceivingAnalytics_CheckBox_ShowClosed.CheckedChanged += async (s, e) => await ApplyClosedFilterAsync();

            // Mutually Exclusive / Interaction Filters
            Control_ReceivingAnalytics_CheckBox_ShowOutsideService.CheckedChanged += async (s, e) => await ApplyOutsideServiceFilterAsync();
            Control_ReceivingAnalytics_CheckBox_ShowWithPartID.CheckedChanged += async (s, e) => await ApplyWithPartIdFilterAsync();

            // Status filters
            Control_ReceivingAnalytics_CheckBox_ShowLate.CheckedChanged += async (s, e) => await ApplyLateFilterAsync();
            Control_ReceivingAnalytics_CheckBox_ShowPartial.CheckedChanged += async (s, e) => await ApplyPartialFilterAsync();
            Control_ReceivingAnalytics_CheckBox_ShowOnTime.CheckedChanged += async (s, e) => await ApplyOnTimeFilterAsync();
            Control_ReceivingAnalytics_CheckBox_ShowOpen.CheckedChanged += async (s, e) => await ApplyOpenFilterAsync();

            // MMC/MMF Logic
            Control_ReceivingAnalytics_CheckBox_ShowMMC.CheckedChanged += async (s, e) => await ApplyMmcFilterAsync();
            Control_ReceivingAnalytics_CheckBox_ShowMMF.CheckedChanged += async (s, e) => await ApplyMmfFilterAsync();

            // Suggestion Boxes
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PartNumber.SuggestionSelected += async (s, e) => await ApplyPartNumberFilterAsync();
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Carrier.SuggestionSelected += async (s, e) => await ApplyCarrierFilterAsync();
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Supplier.SuggestionSelected += async (s, e) => await ApplyVendorFilterAsync();
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PONumber.SuggestionSelected += async (s, e) => await ApplyPoFilterAsync();

            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PartNumber.TextBox.Leave += async (s, e) => await ApplyPartNumberFilterAsync();
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Carrier.TextBox.Leave += async (s, e) => await ApplyCarrierFilterAsync();
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Supplier.TextBox.Leave += async (s, e) => await ApplyVendorFilterAsync();
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PONumber.TextBox.Leave += async (s, e) => await ApplyPoFilterAsync();

            // Also trigger on Enter key for text boxes
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PartNumber.TextBox.KeyDown += async (s, e) => { if (e.KeyCode == Keys.Enter) await ApplyPartNumberFilterAsync(); };
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Carrier.TextBox.KeyDown += async (s, e) => { if (e.KeyCode == Keys.Enter) await ApplyCarrierFilterAsync(); };
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Supplier.TextBox.KeyDown += async (s, e) => { if (e.KeyCode == Keys.Enter) await ApplyVendorFilterAsync(); };
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PONumber.TextBox.KeyDown += async (s, e) => { if (e.KeyCode == Keys.Enter) await ApplyPoFilterAsync(); };

            // Week Navigation
            Control_ReceivingAnalytics_Button_PreviousWeek.Click += async (s, e) => { SetPreviousWeek(); await FetchDataAsync(); };
            Control_ReceivingAnalytics_Button_NextWeek.Click += async (s, e) => { SetNextWeek(); await FetchDataAsync(); };
            Control_ReceivingAnalytics_Button_CurrentWeek.Click += async (s, e) => { SetCurrentWeek(); await FetchDataAsync(); };

            // Expand/Collapse Header Buttons
            Control_ReceivingAnalytics_Button_DateRangeHeader.Click += ToggleDateRangeSection;
            Control_ReceivingAnalytics_Button_FiltersHeader.Click += ToggleFiltersSection;
            Control_ReceivingAnalytics_Button_POStatesHeader.Click += TogglePOStatesSection;
            Control_ReceivingAnalytics_Button_DeliveryStatesHeader.Click += ToggleDeliveryStatesSection;
            Control_ReceivingAnalytics_Button_ReceivingScopeHeader.Click += ToggleReceivingScopeSection;
            button1.Click += ToggleSearchPanel;
        }

        private void InitializeSuggestionBoxes()
        {
            // Date Type
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_DateType.LabelText = "Filter By";
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_DateType.TextBox.DataProvider = async () => await Task.FromResult(new List<string>
            {
                "PO Desired Date",
                "PO Promise Date",
                "Line Desired Date",
                "Line Promise Date",
                "All of the Above"
            });
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_DateType.Text = "All of the Above";
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_DateType.EnableSuggestions = true;

            // Supplier
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Supplier.LabelText = "Vendor";
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Supplier.EnableSuggestions = true;
            // Note: Assuming no specific data provider for now, or user types in. 
            // If there is a vendor list service, it should be hooked up here.

            // PO Number
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PONumber.LabelText = "PO #";
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PONumber.SuggestionDataSource = Enum_SuggestionDataSource.Infor_PONumber;

            // Part Number
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PartNumber.LabelText = "Part #";
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PartNumber.SuggestionDataSource = Enum_SuggestionDataSource.Infor_PartNumber;

            // Carrier
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Carrier.LabelText = "Carrier";
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Carrier.EnableSuggestions = true;
        }

        private void SetCurrentWeek()
        {
            DateTime today = DateTime.Today;
            int diff = (7 + (today.DayOfWeek - DayOfWeek.Monday)) % 7;
            DateTime monday = today.AddDays(-1 * diff).Date;
            DateTime friday = monday.AddDays(4).Date;

            Control_ReceivingAnalytics_DateTimePicker_StartDate.Value = monday;
            Control_ReceivingAnalytics_DateTimePicker_EndDate.Value = friday;
        }

        private void SetPreviousWeek()
        {
            // Snap to the week of the current start date, then move back 1 week
            DateTime currentStart = Control_ReceivingAnalytics_DateTimePicker_StartDate.Value;
            int diff = (7 + (currentStart.DayOfWeek - DayOfWeek.Monday)) % 7;
            DateTime currentMonday = currentStart.AddDays(-1 * diff).Date;

            DateTime prevMonday = currentMonday.AddDays(-7);
            DateTime prevFriday = prevMonday.AddDays(4);

            Control_ReceivingAnalytics_DateTimePicker_StartDate.Value = prevMonday;
            Control_ReceivingAnalytics_DateTimePicker_EndDate.Value = prevFriday;
        }

        private void SetNextWeek()
        {
            // Snap to the week of the current start date, then move forward 1 week
            DateTime currentStart = Control_ReceivingAnalytics_DateTimePicker_StartDate.Value;
            int diff = (7 + (currentStart.DayOfWeek - DayOfWeek.Monday)) % 7;
            DateTime currentMonday = currentStart.AddDays(-1 * diff).Date;

            DateTime nextMonday = currentMonday.AddDays(7);
            DateTime nextFriday = nextMonday.AddDays(4);

            Control_ReceivingAnalytics_DateTimePicker_StartDate.Value = nextMonday;
            Control_ReceivingAnalytics_DateTimePicker_EndDate.Value = nextFriday;
        }

        private async Task FetchDataAsync()
        {
            if (_visualService == null)
            {
                Service_ErrorHandler.ShowError("Visual Database Service not available.");
                return;
            }

            try
            {
                var requestCancellationTokenSource = BeginFetchRequest();
                var filters = CaptureFilterSnapshot();
                LoggingUtility.Log("[Control_ReceivingAnalytics] FetchData started");
                Control_ReceivingAnalytics_Button_Search.Enabled = false;
                Control_ReceivingAnalytics_Button_Search.Text = "Loading...";

                var result = await _visualService.GetReceivingScheduleAsync(
                    Control_ReceivingAnalytics_DateTimePicker_StartDate.Value,
                    Control_ReceivingAnalytics_DateTimePicker_EndDate.Value,
                    Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_DateType.Text ?? "Any of the Above",
                    Control_ReceivingAnalytics_CheckBox_ShowClosed.Checked,
                    false,
                    false,
                    Control_ReceivingAnalytics_CheckBox_ShowOutsideService.Checked,
                    Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Supplier.Text?.Trim() ?? "",
                    Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PONumber.Text?.Trim() ?? "",
                    Control_ReceivingAnalytics_CheckBox_ShowWithPartID.Checked,
                    true,
                    requestCancellationTokenSource.Token
                );
                if (requestCancellationTokenSource.IsCancellationRequested)
                {
                    return;
                }

                if (result.IsSuccess && result.Data != null)
                {
                    PopulateSuggestionBoxes(result.Data);
                    await RenderFilteredResultsAsync(result.Data, filters);
                }
                else
                {
                    Service_ErrorHandler.ShowError(result.ErrorMessage);
                    Control_ReceivingAnalytics_DataGridView_Results.DataSource = null;
                }
            }
            catch (OperationCanceledException)
            {
                LoggingUtility.Log("[Control_ReceivingAnalytics] Fetch canceled");
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Models.Enum_ErrorSeverity.Medium);
            }
            finally
            {
                if (_fetchRequestCancellationTokenSource != null)
                {
                    Control_ReceivingAnalytics_Button_Search.Enabled = true;
                    Control_ReceivingAnalytics_Button_Search.Text = "Search";
                }
            }
        }

        private void PopulateSuggestionBoxes(DataTable dt)
        {
            try
            {
                // Extract unique values for suggestion boxes
                var vendors = dt.AsEnumerable()
                    .Select(r => r["Vendor"]?.ToString())
                    .Where(s => !string.IsNullOrEmpty(s))
                    .Select(s => s!)
                    .Distinct()
                    .OrderBy(s => s)
                    .ToList();

                var carriers = dt.AsEnumerable()
                    .Select(r => r["Ship Via"]?.ToString())
                    .Where(s => !string.IsNullOrEmpty(s))
                    .Select(s => s!)
                    .Distinct()
                    .OrderBy(s => s)
                    .ToList();

                Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Supplier.TextBox.DataProvider = async () => await Task.FromResult(vendors);
                Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Carrier.TextBox.DataProvider = async () => await Task.FromResult(carriers);
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
            }
        }

        private void ApplyRowColors()
        {
            foreach (DataGridViewRow row in Control_ReceivingAnalytics_DataGridView_Results.Rows)
            {
                if (row.DataBoundItem is not DataRowView drv) continue;

                string poStatus = drv["PO Status"]?.ToString() ?? "";
                string lineStatus = drv["Line Status"]?.ToString() ?? "";

                decimal orderQty = ReadDecimal(drv.Row, "Order Qty");
                decimal receivedQty = ReadDecimal(drv.Row, "Received Qty");
                bool hasEffectiveReceiveDate = TryGetEffectiveReceiveDate(drv.Row, out DateTime effectiveReceiveDate, out bool missingDateType);

                Color backColor = Color.White;

                if (missingDateType)
                {
                    backColor = Color.Red;
                }
                else if (poStatus == "C" || lineStatus == "C")
                {
                    backColor = Color.FromArgb(200, 255, 200);
                }
                else if (hasEffectiveReceiveDate && effectiveReceiveDate.Date < DateTime.Today && receivedQty < orderQty)
                {
                    backColor = Color.FromArgb(255, 200, 200);
                }
                else if (receivedQty > 0 && receivedQty < (orderQty * 0.9m))
                {
                    backColor = Color.FromArgb(255, 255, 200);
                }
                else
                {
                    backColor = Color.FromArgb(200, 240, 255);
                }

                row.DefaultCellStyle.BackColor = backColor;
            }
        }
        #endregion

        #region Events

        private void ToggleDateRangeSection(object? sender, EventArgs e)
        {
            _isDateRangeExpanded = !_isDateRangeExpanded;
            Control_ReceivingAnalytics_Panel_DateRangeContents.Visible = _isDateRangeExpanded;

            UpdateSectionState(_dateRangeAnimation, Control_ReceivingAnalytics_Button_DateRangeHeader, _isDateRangeExpanded);
        }

        private void ToggleFiltersSection(object? sender, EventArgs e)
        {
            _isFiltersExpanded = !_isFiltersExpanded;
            Control_ReceivingAnalytics_Panel_FiltersContents.Visible = _isFiltersExpanded;

            UpdateSectionState(_filtersAnimation, Control_ReceivingAnalytics_Button_FiltersHeader, _isFiltersExpanded);
        }

        private void TogglePOStatesSection(object? sender, EventArgs e)
        {
            _isPOStatesExpanded = !_isPOStatesExpanded;
            Control_ReceivingAnalytics_TableLayoutPanel_POStatesContents.Visible = _isPOStatesExpanded;

            UpdateSectionState(_poStatesAnimation, Control_ReceivingAnalytics_Button_POStatesHeader, _isPOStatesExpanded);
        }

        private void ToggleDeliveryStatesSection(object? sender, EventArgs e)
        {
            _isDeliveryStatesExpanded = !_isDeliveryStatesExpanded;
            Control_ReceivingAnalytics_Panel_DeliveryStatesContents.Visible = _isDeliveryStatesExpanded;

            UpdateSectionState(_deliveryStatesAnimation, Control_ReceivingAnalytics_Button_DeliveryStatesHeader, _isDeliveryStatesExpanded);
        }

        private void ToggleReceivingScopeSection(object? sender, EventArgs e)
        {
            _isReceivingScopeExpanded = !_isReceivingScopeExpanded;
            Control_ReceivingAnalytics_Panel_ReceivingScopeContents.Visible = _isReceivingScopeExpanded;

            UpdateSectionState(_receivingScopeAnimation, Control_ReceivingAnalytics_Button_ReceivingScopeHeader, _isReceivingScopeExpanded);
        }

        void ToggleSearchPanel(object? sender, EventArgs e)
        {
            _isSearchPanelExpanded = !_isSearchPanelExpanded;

            // Toggle visibility of all search/filter panels
            tableLayoutPanel2.Visible = _isSearchPanelExpanded;
            tableLayoutPanel3.Visible = _isSearchPanelExpanded;
            tableLayoutPanel5.Visible = _isSearchPanelExpanded;
            Control_ReceivingAnalytics_TableLayoutPanel_DateRangeMain.Visible = _isSearchPanelExpanded;

            UpdateSectionState(_searchPanelAnimation, button1, _isSearchPanelExpanded);
        }

        private async void Control_ReceivingAnalytics_Button_Analytics_Click(object? sender, EventArgs e)
        {
            try
            {
                LoggingUtility.Log("[Control_ReceivingAnalytics] Analytics clicked");
                string htmlPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Html", "ReceivingAnalytics_Enhanced.html");

                // Fallback for development environment if not copied to bin
                if (!System.IO.File.Exists(htmlPath))
                {
                    // Try to find it in the source tree (up 3 levels from bin/Debug/net8.0-windows)
                    string sourcePath = System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Resources\Html\ReceivingAnalytics_Enhanced.html"));
                    if (System.IO.File.Exists(sourcePath))
                    {
                        htmlPath = sourcePath;
                    }
                    else
                    {
                        Service_ErrorHandler.ShowError($"Analytics template not found at: {htmlPath}");
                        return;
                    }
                }

                // Use FileStream with FileShare.ReadWrite to allow concurrent reads
                string htmlContent;
                using (var fs = new System.IO.FileStream(htmlPath, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite))
                using (var sr = new System.IO.StreamReader(fs))
                {
                    htmlContent = await sr.ReadToEndAsync();
                }

                // Fetch Data
                if (_visualService == null) return;
                var requestCancellationTokenSource = BeginAnalyticsRequest();

                Control_ReceivingAnalytics_Button_Analytics.Enabled = false;
                Control_ReceivingAnalytics_Button_Analytics.Text = "Loading...";

                // Capture user selection for initial view in the HTML chart
                var userStartDate = Control_ReceivingAnalytics_DateTimePicker_StartDate.Value;
                var userEndDate = Control_ReceivingAnalytics_DateTimePicker_EndDate.Value;

                // Fetch a wider range of data (1 year history, 6 months forecast) so the user can explore in the HTML view
                var dataStart = DateTime.Today.AddYears(-1);
                var dataEnd = DateTime.Today.AddMonths(6);

                var result = await _visualService.GetReceivingAnalyticsAsync(dataStart, dataEnd, requestCancellationTokenSource.Token);
                if (requestCancellationTokenSource.IsCancellationRequested)
                {
                    return;
                }

                Control_ReceivingAnalytics_Button_Analytics.Enabled = true;
                Control_ReceivingAnalytics_Button_Analytics.Text = "Receiving Analytics";

                if (result.IsSuccess && result.Data != null)
                {
                    // Transform data to match expected format with Type categorization
                    var transformedData = new
                    {
                        History = result.Data.History.Select(h => new
                        {
                            Date = h.Date.ToString("yyyy-MM-dd"),
                            Type = DetermineReceivingType(h.Type, h.PartNumber),
                            Count = h.Count,
                            PartNumber = h.PartNumber,
                            ReceivedBy = h.ReceivedBy
                        }),
                        Forecast = result.Data.Forecast.Select(f => new
                        {
                            Date = f.Date.ToString("yyyy-MM-dd"),
                            Type = DetermineReceivingType(f.Type, f.PartNumber),
                            Count = f.Count,
                            PartNumber = f.PartNumber,
                            ReceivedBy = f.ReceivedBy
                        })
                        // Removed InitialStartDate/EndDate to let HTML show full dataset by default
                    };

                    var jsonOptions = new System.Text.Json.JsonSerializerOptions
                    {
                        PropertyNamingPolicy = null,
                        WriteIndented = false
                    };

                    string jsonData = System.Text.Json.JsonSerializer.Serialize(transformedData, jsonOptions);

                    // Escape for JavaScript string
                    jsonData = jsonData.Replace("\\", "\\\\").Replace("'", "\\'").Replace("\r", "").Replace("\n", "");

                    htmlContent = htmlContent.Replace("// To be populated by C#", $"loadData('{jsonData}');");

                    // Use temp file to avoid NavigateToString size limits
                    string tempFilePath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), $"MTM_ReceivingAnalytics_{Guid.NewGuid()}.html");
                    await System.IO.File.WriteAllTextAsync(tempFilePath, htmlContent);

                    using (var viewer = new Form_HtmlViewer("Receiving Analytics", new Uri(tempFilePath), tempFilePath))
                    {
                        viewer.ShowDialog(this);
                    }
                }
                else
                {
                    Service_ErrorHandler.ShowError(result.ErrorMessage);
                }
            }
            catch (OperationCanceledException)
            {
                LoggingUtility.Log("[Control_ReceivingAnalytics] Analytics request canceled");
                Control_ReceivingAnalytics_Button_Analytics.Enabled = true;
                Control_ReceivingAnalytics_Button_Analytics.Text = "Receiving Analytics";
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, controlName: this.Name);
                Control_ReceivingAnalytics_Button_Analytics.Enabled = true;
                Control_ReceivingAnalytics_Button_Analytics.Text = "Receiving Analytics";
            }
        }

        private string DetermineReceivingType(string originalType, string partNumber)
        {
            // Map the type from Visual Analytics to chart categories
            // This handles the categorization from GetReceivingAnalyticsAsync

            // Check for Uninventoried (missing part number) first
            if (string.IsNullOrWhiteSpace(partNumber)) return "Uninventoried";

            if (originalType == null) return "Part";

            var type = originalType.ToUpperInvariant();

            if (type.Contains("MMC")) return "MMC";
            if (type.Contains("MMF")) return "MMF";
            if (type.Contains("SERVICE")) return "Service";
            // Consignment and Internal removed per requirements

            return "Part";
        }

        private void ContextMenuItem_ColumnOrder_Click(object? sender, EventArgs e)
        {
            try
            {
                using (var dlg = new Component_ColumnOrderDialog(Control_ReceivingAnalytics_DataGridView_Results))
                {
                    if (dlg.ShowDialog(this) == DialogResult.OK)
                    {
                        // Settings are saved within the dialog logic via Core_Themes.SaveGridSettingsAsync
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Low, controlName: this.Name);
            }
        }

        private void DataGridViewResults_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                var row = Control_ReceivingAnalytics_DataGridView_Results.Rows[e.RowIndex];
                if (row.DataBoundItem is DataRowView drv)
                {
                    string poNumber = drv["PO Number"]?.ToString() ?? "";
                    if (!string.IsNullOrEmpty(poNumber))
                    {
                        using (var detailsForm = new MTM_WIP_Application_Winforms.Forms.Visual.Form_PODetails(poNumber))
                        {
                            detailsForm.ShowDialog(this);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Low, controlName: this.Name);
            }
        }

        #endregion

        #region Helpers
        private async Task RenderFilteredResultsAsync(DataTable source, ReceivingAnalyticsFilterSnapshot filters)
        {
            try
            {
                DataTable dt = source.Copy();
                var rowsToRemove = new List<DataRow>();

                foreach (DataRow row in dt.Rows)
                {
                    string poStatus = row["PO Status"]?.ToString() ?? "";
                    string lineStatus = row["Line Status"]?.ToString() ?? "";
                    decimal orderQty = ReadDecimal(row, "Order Qty");
                    decimal receivedQty = ReadDecimal(row, "Received Qty");
                    bool hasEffectiveReceiveDate = TryGetEffectiveReceiveDate(row, out DateTime effectiveReceiveDate, out bool missingDateType);
                    string partNumber = row["Part Number"]?.ToString() ?? "";

                    bool isClosed = poStatus == "C" || lineStatus == "C";
                    bool isLate = !isClosed && hasEffectiveReceiveDate && effectiveReceiveDate.Date < DateTime.Today && receivedQty < orderQty;
                    bool isPartial = !isClosed && receivedQty > 0 && receivedQty < (orderQty * 0.9m);
                    bool isOnTime = !isClosed && !isLate && !isPartial;

                    if (isLate && !filters.ShowLate) { rowsToRemove.Add(row); continue; }
                    if (isPartial && !filters.ShowPartial) { rowsToRemove.Add(row); continue; }
                    if (isOnTime && !filters.ShowOnTime) { rowsToRemove.Add(row); continue; }
                    if (!isClosed && !filters.ShowOpen) { rowsToRemove.Add(row); continue; }

                    if (filters.ShowMMC || filters.ShowMMF)
                    {
                        bool isMMC = partNumber.StartsWith("MMC", StringComparison.OrdinalIgnoreCase);
                        bool isMMF = partNumber.StartsWith("MMF", StringComparison.OrdinalIgnoreCase);

                        if (filters.ShowMMC && filters.ShowMMF)
                        {
                            if (!isMMC && !isMMF) { rowsToRemove.Add(row); continue; }
                        }
                        else if (filters.ShowMMC)
                        {
                            if (!isMMC) { rowsToRemove.Add(row); continue; }
                        }
                        else if (filters.ShowMMF)
                        {
                            if (!isMMF) { rowsToRemove.Add(row); continue; }
                        }
                    }

                    if (!string.IsNullOrEmpty(filters.CarrierFilter))
                    {
                        string carrier = row["Ship Via"]?.ToString() ?? "";
                        if (string.IsNullOrEmpty(carrier) || !carrier.Contains(filters.CarrierFilter, StringComparison.OrdinalIgnoreCase)) { rowsToRemove.Add(row); continue; }
                    }

                    if (!string.IsNullOrEmpty(filters.PartNumberFilter))
                    {
                        if (string.IsNullOrEmpty(partNumber) || !partNumber.Contains(filters.PartNumberFilter, StringComparison.OrdinalIgnoreCase)) { rowsToRemove.Add(row); continue; }
                    }

                    if (!string.IsNullOrEmpty(filters.VendorFilter))
                    {
                        string vendor = row["Vendor"]?.ToString() ?? "";
                        if (string.IsNullOrEmpty(vendor) || !vendor.Contains(filters.VendorFilter, StringComparison.OrdinalIgnoreCase)) { rowsToRemove.Add(row); continue; }
                    }

                    if (!string.IsNullOrEmpty(filters.PoFilter))
                    {
                        string po = row["PO Number"]?.ToString() ?? "";
                        if (string.IsNullOrEmpty(po) || !po.Contains(filters.PoFilter, StringComparison.OrdinalIgnoreCase)) { rowsToRemove.Add(row); continue; }
                    }
                }

                foreach (var row in rowsToRemove)
                {
                    dt.Rows.Remove(row);
                }

                Control_ReceivingAnalytics_DataGridView_Results.DataSource = dt;

                if (Control_ReceivingAnalytics_DataGridView_Results.Columns["Order Date"] != null)
                    Control_ReceivingAnalytics_DataGridView_Results.Columns["Order Date"].DefaultCellStyle.Format = "d";
                if (Control_ReceivingAnalytics_DataGridView_Results.Columns["PO Desired Date"] != null)
                    Control_ReceivingAnalytics_DataGridView_Results.Columns["PO Desired Date"].DefaultCellStyle.Format = "d";
                if (Control_ReceivingAnalytics_DataGridView_Results.Columns["PO Promise Date"] != null)
                    Control_ReceivingAnalytics_DataGridView_Results.Columns["PO Promise Date"].DefaultCellStyle.Format = "d";
                if (Control_ReceivingAnalytics_DataGridView_Results.Columns["Line Desired Date"] != null)
                    Control_ReceivingAnalytics_DataGridView_Results.Columns["Line Desired Date"].DefaultCellStyle.Format = "d";
                if (Control_ReceivingAnalytics_DataGridView_Results.Columns["Line Promise Date"] != null)
                    Control_ReceivingAnalytics_DataGridView_Results.Columns["Line Promise Date"].DefaultCellStyle.Format = "d";

                await Service_DataGridView.ApplyStandardSettingsAsync(Control_ReceivingAnalytics_DataGridView_Results, Model_Application_Variables.User);
                Service_DataGridView.ApplySmartNumericFormatting(Control_ReceivingAnalytics_DataGridView_Results);
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Models.Enum_ErrorSeverity.Medium);
            }
        }

        private Task ApplyClosedFilterAsync() => FetchDataAsync();

        private async Task ApplyOutsideServiceFilterAsync()
        {
            HandleFilterLogic(Control_ReceivingAnalytics_CheckBox_ShowOutsideService);
            await FetchDataAsync();
        }

        private async Task ApplyWithPartIdFilterAsync()
        {
            HandleFilterLogic(Control_ReceivingAnalytics_CheckBox_ShowWithPartID);
            await FetchDataAsync();
        }

        private Task ApplyLateFilterAsync() => FetchDataAsync();

        private Task ApplyPartialFilterAsync() => FetchDataAsync();

        private Task ApplyOnTimeFilterAsync() => FetchDataAsync();

        private Task ApplyOpenFilterAsync() => FetchDataAsync();

        private async Task ApplyMmcFilterAsync()
        {
            HandleFilterLogic(Control_ReceivingAnalytics_CheckBox_ShowMMC);
            await FetchDataAsync();
        }

        private async Task ApplyMmfFilterAsync()
        {
            HandleFilterLogic(Control_ReceivingAnalytics_CheckBox_ShowMMF);
            await FetchDataAsync();
        }

        private Task ApplyPartNumberFilterAsync() => FetchDataAsync();

        private Task ApplyCarrierFilterAsync() => FetchDataAsync();

        private Task ApplyVendorFilterAsync() => FetchDataAsync();

        private Task ApplyPoFilterAsync() => FetchDataAsync();

        private ReceivingAnalyticsFilterSnapshot CaptureFilterSnapshot()
        {
            return new ReceivingAnalyticsFilterSnapshot(
                Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Carrier.Text?.Trim() ?? string.Empty,
                Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PartNumber.Text?.Trim() ?? string.Empty,
                Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Supplier.Text?.Trim() ?? string.Empty,
                Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PONumber.Text?.Trim() ?? string.Empty,
                Control_ReceivingAnalytics_CheckBox_ShowLate.Checked,
                Control_ReceivingAnalytics_CheckBox_ShowPartial.Checked,
                Control_ReceivingAnalytics_CheckBox_ShowOnTime.Checked,
                Control_ReceivingAnalytics_CheckBox_ShowOpen.Checked,
                Control_ReceivingAnalytics_CheckBox_ShowMMC.Checked,
                Control_ReceivingAnalytics_CheckBox_ShowMMF.Checked);
        }

        private static decimal ReadDecimal(DataRow row, string columnName)
        {
            decimal.TryParse(row[columnName]?.ToString(), out decimal value);
            return value;
        }

        private static bool TryGetEffectiveReceiveDate(DataRow row, out DateTime effectiveDate, out bool missingDateType)
        {
            missingDateType = false;

            if (TryReadDate(row, "Line Desired Date", out effectiveDate)) return true;
            if (TryReadDate(row, "Line Promise Date", out effectiveDate)) return true;
            if (TryReadDate(row, "PO Desired Date", out effectiveDate)) return true;
            if (TryReadDate(row, "PO Promise Date", out effectiveDate)) return true;
            if (TryReadDate(row, "Order Date", out effectiveDate))
            {
                missingDateType = true;
                return true;
            }

            effectiveDate = DateTime.Today;
            missingDateType = true;
            return false;
        }

        private static bool TryReadDate(DataRow row, string columnName, out DateTime dateValue)
        {
            if (DateTime.TryParse(row[columnName]?.ToString(), out dateValue))
            {
                return true;
            }

            dateValue = default;
            return false;
        }

        private readonly record struct ReceivingAnalyticsFilterSnapshot(
            string CarrierFilter,
            string PartNumberFilter,
            string VendorFilter,
            string PoFilter,
            bool ShowLate,
            bool ShowPartial,
            bool ShowOnTime,
            bool ShowOpen,
            bool ShowMMC,
            bool ShowMMF);
        private void HandleFilterLogic(CheckBox source)
        {
            if (_isHandlingFilterLogic) return;
            _isHandlingFilterLogic = true;
            try
            {
                if (source.Checked)
                {
                    if (source == Control_ReceivingAnalytics_CheckBox_ShowOutsideService)
                    {
                        UncheckAndDisable(Control_ReceivingAnalytics_CheckBox_ShowWithPartID);
                        UncheckAndDisable(Control_ReceivingAnalytics_CheckBox_ShowMMC);
                        UncheckAndDisable(Control_ReceivingAnalytics_CheckBox_ShowMMF);
                    }
                    else if (source == Control_ReceivingAnalytics_CheckBox_ShowWithPartID)
                    {
                        Uncheck(Control_ReceivingAnalytics_CheckBox_ShowOutsideService);
                        Uncheck(Control_ReceivingAnalytics_CheckBox_ShowMMC);
                        Uncheck(Control_ReceivingAnalytics_CheckBox_ShowMMF);

                        EnableAllFilters();
                    }
                    else if (source == Control_ReceivingAnalytics_CheckBox_ShowMMC)
                    {
                        Uncheck(Control_ReceivingAnalytics_CheckBox_ShowWithPartID);
                        Uncheck(Control_ReceivingAnalytics_CheckBox_ShowOutsideService);

                        EnableAllFilters();
                    }
                    else if (source == Control_ReceivingAnalytics_CheckBox_ShowMMF)
                    {
                        Uncheck(Control_ReceivingAnalytics_CheckBox_ShowWithPartID);
                        Uncheck(Control_ReceivingAnalytics_CheckBox_ShowOutsideService);

                        EnableAllFilters();
                    }
                }
                else
                {
                    // If unchecked, we might need to re-enable things.
                    if (source == Control_ReceivingAnalytics_CheckBox_ShowOutsideService)
                    {
                        EnableAllFilters();
                    }
                }
            }
            finally
            {
                _isHandlingFilterLogic = false;
            }
        }

        private void UncheckAndDisable(CheckBox chk)
        {
            chk.Checked = false;
            chk.Enabled = false;
        }

        private void Uncheck(CheckBox chk)
        {
            chk.Checked = false;
        }

        private void EnableAllFilters()
        {
            Control_ReceivingAnalytics_CheckBox_ShowOutsideService.Enabled = true;
            Control_ReceivingAnalytics_CheckBox_ShowWithPartID.Enabled = true;
            Control_ReceivingAnalytics_CheckBox_ShowMMC.Enabled = true;
            Control_ReceivingAnalytics_CheckBox_ShowMMF.Enabled = true;
        }

        private void UpdateSectionState(Component_TextAnimationSequence animation, Button button, bool isExpanded)
        {
            var preset = isExpanded ? TextAnimationPreset.Up : TextAnimationPreset.Down;
            animation.UsePreset(preset);

            if (Model_Application_Variables.AnimationsEnabled)
            {
                if (!animation.IsRunning) animation.StartAnimation();
            }
            else
            {
                if (animation.IsRunning) animation.StopAnimation();

                // Set static text (first frame of the preset)
                if (animation.Frames.Count > 0)
                {
                    string frame = animation.Frames[0];
                    button.Text = animation.PrefixText + frame + animation.SuffixText;
                }
            }
        }
        #endregion

    }
}
