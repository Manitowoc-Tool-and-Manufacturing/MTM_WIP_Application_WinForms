using System.Data;
using Microsoft.Extensions.DependencyInjection;
using MTM_WIP_Application_Winforms.Controls.Shared;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;
using MTM_WIP_Application_Winforms.Services.Visual;
using MTM_WIP_Application_Winforms.Services.Logging;
using MTM_WIP_Application_Winforms.Helpers;

namespace MTM_WIP_Application_Winforms.Controls.Visual
{
    public partial class Control_ReceivingAnalytics : ThemedUserControl
    {
        #region Fields
        private readonly IService_VisualDatabase? _visualService;
        private bool _isFiltersVisible = true;
        private readonly Control_TextAnimationSequence _toggleButtonAnimation;
        private DataTable? _cachedDataTable;
        private bool _isHandlingFilterLogic = false;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Control_ReceivingAnalytics"/> class.
        /// </summary>
        public Control_ReceivingAnalytics()
        {
            InitializeComponent();
            _visualService = Program.ServiceProvider?.GetService<IService_VisualDatabase>();

            // Initialize Toggle Button Animation
            _toggleButtonAnimation = new Control_TextAnimationSequence();
            _toggleButtonAnimation.TargetButton = Control_ReceivingAnalytics_Button_ToggleOptions;
            _toggleButtonAnimation.UsePreset(TextAnimationPreset.Up); // Initially visible, so arrow up
            _toggleButtonAnimation.StartAnimation();

            // Initialize Suggestion Boxes
            InitializeSuggestionBoxes();

            // Set default dates to current week (Monday - Friday)
            SetCurrentWeek();

            // Set default checkboxes
            Control_ReceivingAnalytics_CheckBox_ShowClosed.Checked = false;
            Control_ReceivingAnalytics_CheckBox_ShowConsignment.Checked = false;
            Control_ReceivingAnalytics_CheckBox_ShowInternal.Checked = false;
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

            // Toggle Options
            Control_ReceivingAnalytics_Button_ToggleOptions.Click += Control_ReceivingAnalytics_Button_ToggleOptions_Click;

            // DataGridView Events
            Control_ReceivingAnalytics_DataGridView_Results.CellDoubleClick += DataGridViewResults_CellDoubleClick;
            Control_ReceivingAnalytics_DataGridView_Results.DataBindingComplete += (s, e) => ApplyRowColors();
            Control_ReceivingAnalytics_DataGridView_Results.Sorted += (s, e) => ApplyRowColors();

            // Server-side filters (require fetch)
            Control_ReceivingAnalytics_CheckBox_ShowClosed.CheckedChanged += async (s, e) => await FetchDataAsync();
            
            // Mutually Exclusive / Interaction Filters
            Control_ReceivingAnalytics_CheckBox_ShowOutsideService.CheckedChanged += async (s, e) => { HandleFilterLogic(Control_ReceivingAnalytics_CheckBox_ShowOutsideService); await FetchDataAsync(); };
            Control_ReceivingAnalytics_CheckBox_ShowConsignment.CheckedChanged += async (s, e) => { HandleFilterLogic(Control_ReceivingAnalytics_CheckBox_ShowConsignment); await FetchDataAsync(); };
            Control_ReceivingAnalytics_CheckBox_ShowInternal.CheckedChanged += async (s, e) => { HandleFilterLogic(Control_ReceivingAnalytics_CheckBox_ShowInternal); await FetchDataAsync(); };
            Control_ReceivingAnalytics_CheckBox_ShowWithPartID.CheckedChanged += async (s, e) => { HandleFilterLogic(Control_ReceivingAnalytics_CheckBox_ShowWithPartID); await FetchDataAsync(); };

            // Client-side filters (apply to cached data)
            Control_ReceivingAnalytics_CheckBox_ShowLate.CheckedChanged += async (s, e) => await ApplyFiltersAsync();
            Control_ReceivingAnalytics_CheckBox_ShowPartial.CheckedChanged += async (s, e) => await ApplyFiltersAsync();
            Control_ReceivingAnalytics_CheckBox_ShowOnTime.CheckedChanged += async (s, e) => await ApplyFiltersAsync();
            Control_ReceivingAnalytics_CheckBox_ShowOpen.CheckedChanged += async (s, e) => await ApplyFiltersAsync();
            
            // MMC/MMF Logic
            Control_ReceivingAnalytics_CheckBox_ShowMMC.CheckedChanged += async (s, e) => { HandleFilterLogic(Control_ReceivingAnalytics_CheckBox_ShowMMC); await ApplyFiltersAsync(); };
            Control_ReceivingAnalytics_CheckBox_ShowMMF.CheckedChanged += async (s, e) => { HandleFilterLogic(Control_ReceivingAnalytics_CheckBox_ShowMMF); await ApplyFiltersAsync(); };

            // Suggestion Boxes (Client-side)
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PartNumber.SuggestionSelected += async (s, e) => await ApplyFiltersAsync();
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Carrier.SuggestionSelected += async (s, e) => await ApplyFiltersAsync();
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Supplier.SuggestionSelected += async (s, e) => await ApplyFiltersAsync();
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PONumber.SuggestionSelected += async (s, e) => await ApplyFiltersAsync();
            
            // Also trigger on Enter key for text boxes
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PartNumber.TextBox.KeyDown += async (s, e) => { if (e.KeyCode == Keys.Enter) await ApplyFiltersAsync(); };
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Carrier.TextBox.KeyDown += async (s, e) => { if (e.KeyCode == Keys.Enter) await ApplyFiltersAsync(); };
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Supplier.TextBox.KeyDown += async (s, e) => { if (e.KeyCode == Keys.Enter) await ApplyFiltersAsync(); };
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PONumber.TextBox.KeyDown += async (s, e) => { if (e.KeyCode == Keys.Enter) await ApplyFiltersAsync(); };
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
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PONumber.EnableSuggestions = true;

            // Part Number
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PartNumber.LabelText = "Part #";
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PartNumber.EnableSuggestions = true;
            // Use helper if available, otherwise default behavior
            Helper_SuggestionTextBox.ConfigureForPartNumbers(Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PartNumber, Helper_SuggestionTextBox.GetCachedPartNumbersAsync);

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

        private async Task FetchDataAsync()
        {
            if (_visualService == null)
            {
                Service_ErrorHandler.ShowError("Visual Database Service not available.");
                return;
            }

            try
            {
                Control_ReceivingAnalytics_Button_Search.Enabled = false;
                Control_ReceivingAnalytics_Button_Search.Text = "Loading...";

                var result = await _visualService.GetReceivingScheduleAsync(
                    Control_ReceivingAnalytics_DateTimePicker_StartDate.Value,
                    Control_ReceivingAnalytics_DateTimePicker_EndDate.Value,
                    Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_DateType.Text ?? "Any of the Above",
                    Control_ReceivingAnalytics_CheckBox_ShowClosed.Checked,
                    Control_ReceivingAnalytics_CheckBox_ShowConsignment.Checked,
                    Control_ReceivingAnalytics_CheckBox_ShowInternal.Checked,
                    Control_ReceivingAnalytics_CheckBox_ShowOutsideService.Checked,
                    Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Supplier.Text?.Trim() ?? "",
                    Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PONumber.Text?.Trim() ?? "",
                    Control_ReceivingAnalytics_CheckBox_ShowWithPartID.Checked
                );

                if (result.IsSuccess && result.Data != null)
                {
                    _cachedDataTable = result.Data;
                    PopulateSuggestionBoxes(_cachedDataTable);
                    await ApplyFiltersAsync();
                }
                else
                {
                    Service_ErrorHandler.ShowError(result.ErrorMessage);
                    _cachedDataTable = null;
                    Control_ReceivingAnalytics_DataGridView_Results.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Models.Enum_ErrorSeverity.Medium);
            }
            finally
            {
                Control_ReceivingAnalytics_Button_Search.Enabled = true;
                Control_ReceivingAnalytics_Button_Search.Text = "Search";
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

                var poNumbers = dt.AsEnumerable()
                    .Select(r => r["PO Number"]?.ToString())
                    .Where(s => !string.IsNullOrEmpty(s))
                    .Select(s => s!)
                    .Distinct()
                    .OrderBy(s => s)
                    .ToList();

                var partNumbers = dt.AsEnumerable()
                    .Select(r => r["Part Number"]?.ToString())
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
                Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PONumber.TextBox.DataProvider = async () => await Task.FromResult(poNumbers);
                Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PartNumber.TextBox.DataProvider = async () => await Task.FromResult(partNumbers);
                Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Carrier.TextBox.DataProvider = async () => await Task.FromResult(carriers);
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
            }
        }

        private async Task ApplyFiltersAsync()
        {
            if (_cachedDataTable == null) return;

            try
            {
                // Create a view or clone to filter without modifying cache
                DataTable dt = _cachedDataTable.Copy();
                var rowsToRemove = new List<DataRow>();

                // Client-side filtering logic from LoadDataAsync
                string carrierFilter = Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Carrier.Text?.Trim() ?? "";
                string partNumberFilter = Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PartNumber.Text?.Trim() ?? "";
                string vendorFilter = Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Supplier.Text?.Trim() ?? "";
                string poFilter = Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PONumber.Text?.Trim() ?? "";

                foreach (DataRow row in dt.Rows)
                {
                    string poStatus = row["PO Status"]?.ToString() ?? "";
                    string lineStatus = row["Line Status"]?.ToString() ?? "";
                    
                    decimal orderQty = 0;
                    decimal receivedQty = 0;
                    decimal.TryParse(row["Order Qty"]?.ToString(), out orderQty);
                    decimal.TryParse(row["Received Qty"]?.ToString(), out receivedQty);

                    DateTime? desiredDate = null;
                    if (DateTime.TryParse(row["Line Desired Date"]?.ToString(), out DateTime d1)) desiredDate = d1;
                    else if (DateTime.TryParse(row["PO Desired Date"]?.ToString(), out DateTime d2)) desiredDate = d2;

                    bool isClosed = poStatus == "C" || lineStatus == "C";
                    bool isLate = !isClosed && desiredDate.HasValue && desiredDate.Value.Date < DateTime.Today && receivedQty < orderQty;
                    bool isPartial = !isClosed && !isLate && receivedQty > 0 && receivedQty < orderQty;
                    bool isOnTime = !isClosed && !isLate && !isPartial;

                    // Status Filters
                    if (isLate && !Control_ReceivingAnalytics_CheckBox_ShowLate.Checked) { rowsToRemove.Add(row); continue; }
                    if (isPartial && !Control_ReceivingAnalytics_CheckBox_ShowPartial.Checked) { rowsToRemove.Add(row); continue; }
                    if (isOnTime && !Control_ReceivingAnalytics_CheckBox_ShowOnTime.Checked) { rowsToRemove.Add(row); continue; }
                    if (!isClosed && !Control_ReceivingAnalytics_CheckBox_ShowOpen.Checked) { rowsToRemove.Add(row); continue; }

                    // MMC/MMF Filters (Inclusive Logic)
                    string partNumber = row["Part Number"]?.ToString() ?? "";
                    bool showMMC = Control_ReceivingAnalytics_CheckBox_ShowMMC.Checked;
                    bool showMMF = Control_ReceivingAnalytics_CheckBox_ShowMMF.Checked;

                    if (showMMC || showMMF)
                    {
                        bool isMMC = partNumber.StartsWith("MMC", StringComparison.OrdinalIgnoreCase);
                        bool isMMF = partNumber.StartsWith("MMF", StringComparison.OrdinalIgnoreCase);

                        if (showMMC && showMMF)
                        {
                            if (!isMMC && !isMMF) { rowsToRemove.Add(row); continue; }
                        }
                        else if (showMMC)
                        {
                            if (!isMMC) { rowsToRemove.Add(row); continue; }
                        }
                        else if (showMMF)
                        {
                            if (!isMMF) { rowsToRemove.Add(row); continue; }
                        }
                    }

                    // Carrier Filter
                    if (!string.IsNullOrEmpty(carrierFilter))
                    {
                        string carrier = row["Ship Via"]?.ToString() ?? "";
                        if (string.IsNullOrEmpty(carrier) || !carrier.Contains(carrierFilter, StringComparison.OrdinalIgnoreCase)) { rowsToRemove.Add(row); continue; }
                    }

                    // Part Number Filter
                    if (!string.IsNullOrEmpty(partNumberFilter))
                    {
                        if (string.IsNullOrEmpty(partNumber) || !partNumber.Contains(partNumberFilter, StringComparison.OrdinalIgnoreCase)) { rowsToRemove.Add(row); continue; }
                    }

                    // Vendor Filter
                    if (!string.IsNullOrEmpty(vendorFilter))
                    {
                        string vendor = row["Vendor"]?.ToString() ?? "";
                        if (string.IsNullOrEmpty(vendor) || !vendor.Contains(vendorFilter, StringComparison.OrdinalIgnoreCase)) { rowsToRemove.Add(row); continue; }
                    }

                    // PO Filter
                    if (!string.IsNullOrEmpty(poFilter))
                    {
                        string po = row["PO Number"]?.ToString() ?? "";
                        if (string.IsNullOrEmpty(po) || !po.Contains(poFilter, StringComparison.OrdinalIgnoreCase)) { rowsToRemove.Add(row); continue; }
                    }
                }

                foreach (var row in rowsToRemove)
                {
                    dt.Rows.Remove(row);
                }

                Control_ReceivingAnalytics_DataGridView_Results.DataSource = dt;

                // Format columns
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
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Models.Enum_ErrorSeverity.Medium);
            }
        }

        private void ApplyRowColors()
        {
            foreach (DataGridViewRow row in Control_ReceivingAnalytics_DataGridView_Results.Rows)
            {
                if (row.DataBoundItem is not DataRowView drv) continue;

                // Get values safely from DataRowView (faster than Cells access)
                string poStatus = drv["PO Status"]?.ToString() ?? "";
                string lineStatus = drv["Line Status"]?.ToString() ?? "";
                
                decimal orderQty = 0;
                decimal receivedQty = 0;
                decimal.TryParse(drv["Order Qty"]?.ToString(), out orderQty);
                decimal.TryParse(drv["Received Qty"]?.ToString(), out receivedQty);

                DateTime? desiredDate = null;
                if (DateTime.TryParse(drv["Line Desired Date"]?.ToString(), out DateTime dt))
                {
                    desiredDate = dt;
                }
                else if (DateTime.TryParse(drv["PO Desired Date"]?.ToString(), out DateTime dt2))
                {
                    desiredDate = dt2;
                }

                // Logic for coloring
                Color backColor = Color.White;

                if (poStatus == "C" || lineStatus == "C")
                {
                    // Closed - Green
                    backColor = Color.FromArgb(200, 255, 200);
                }
                else if (desiredDate.HasValue && desiredDate.Value.Date < DateTime.Today && receivedQty < orderQty)
                {
                    // Late - Red
                    backColor = Color.FromArgb(255, 200, 200);
                }
                else if (receivedQty > 0 && receivedQty < orderQty)
                {
                    // Partial - Yellow
                    backColor = Color.FromArgb(255, 255, 200);
                }
                else
                {
                    // On Time / Open - Blue (Light)
                    backColor = Color.FromArgb(200, 240, 255);
                }

                row.DefaultCellStyle.BackColor = backColor;
            }
        }
        #endregion

        #region Events
        private void Control_ReceivingAnalytics_Button_ToggleOptions_Click(object? sender, EventArgs e)
        {
            _isFiltersVisible = !_isFiltersVisible;
            Control_ReceivingAnalytics_TableLayoutPanel_Filters.Visible = _isFiltersVisible;
            Control_ReceivingAnalytics_TableLayoutPanel_CheckBoxes.Visible = _isFiltersVisible;
            
            if (_isFiltersVisible)
            {
                _toggleButtonAnimation.UsePreset(TextAnimationPreset.Up);
            }
            else
            {
                _toggleButtonAnimation.UsePreset(TextAnimationPreset.Down);
            }
        }

        private async void Control_ReceivingAnalytics_Button_Analytics_Click(object? sender, EventArgs e)
        {
            try
            {
                string htmlPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Html", "ReceivingAnalytics.html");
                
                // Fallback for development environment if not copied to bin
                if (!System.IO.File.Exists(htmlPath))
                {
                    // Try to find it in the source tree (up 3 levels from bin/Debug/net8.0-windows)
                    string sourcePath = System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Resources\Html\ReceivingAnalytics.html"));
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

                string htmlContent = System.IO.File.ReadAllText(htmlPath);
                
                // Fetch Data
                if (_visualService == null) return;
                
                Control_ReceivingAnalytics_Button_Analytics.Enabled = false;
                Control_ReceivingAnalytics_Button_Analytics.Text = "Loading...";

                var result = await _visualService.GetReceivingAnalyticsAsync();
                
                Control_ReceivingAnalytics_Button_Analytics.Enabled = true;
                Control_ReceivingAnalytics_Button_Analytics.Text = "Receiving Analytics";

                if (result.IsSuccess && result.Data != null)
                {
                    string jsonData = System.Text.Json.JsonSerializer.Serialize(result.Data);
                    htmlContent = htmlContent.Replace("// To be populated by C#", $"loadData('{jsonData.Replace("'", "\\'")}');");

                    using (var viewer = new Form_HtmlViewer("Receiving Analytics", htmlContent))
                    {
                        viewer.ShowDialog(this);
                    }
                }
                else
                {
                    Service_ErrorHandler.ShowError(result.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, controlName: this.Name);
                Control_ReceivingAnalytics_Button_Analytics.Enabled = true;
                Control_ReceivingAnalytics_Button_Analytics.Text = "Receiving Analytics";
            }
        }

        private void ContextMenuItem_ColumnOrder_Click(object? sender, EventArgs e)
        {
            try
            {
                using (var dlg = new ColumnOrderDialog(Control_ReceivingAnalytics_DataGridView_Results))
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
                        UncheckAndDisable(Control_ReceivingAnalytics_CheckBox_ShowConsignment);
                        UncheckAndDisable(Control_ReceivingAnalytics_CheckBox_ShowInternal);
                        UncheckAndDisable(Control_ReceivingAnalytics_CheckBox_ShowWithPartID);
                        UncheckAndDisable(Control_ReceivingAnalytics_CheckBox_ShowMMC);
                        UncheckAndDisable(Control_ReceivingAnalytics_CheckBox_ShowMMF);
                    }
                    else if (source == Control_ReceivingAnalytics_CheckBox_ShowConsignment)
                    {
                        UncheckAndDisable(Control_ReceivingAnalytics_CheckBox_ShowOutsideService);
                        UncheckAndDisable(Control_ReceivingAnalytics_CheckBox_ShowInternal);
                        UncheckAndDisable(Control_ReceivingAnalytics_CheckBox_ShowWithPartID);
                        UncheckAndDisable(Control_ReceivingAnalytics_CheckBox_ShowMMC);
                        UncheckAndDisable(Control_ReceivingAnalytics_CheckBox_ShowMMF);
                    }
                    else if (source == Control_ReceivingAnalytics_CheckBox_ShowInternal)
                    {
                        UncheckAndDisable(Control_ReceivingAnalytics_CheckBox_ShowOutsideService);
                        UncheckAndDisable(Control_ReceivingAnalytics_CheckBox_ShowConsignment);
                        UncheckAndDisable(Control_ReceivingAnalytics_CheckBox_ShowWithPartID);
                        UncheckAndDisable(Control_ReceivingAnalytics_CheckBox_ShowMMC);
                        UncheckAndDisable(Control_ReceivingAnalytics_CheckBox_ShowMMF);
                    }
                    else if (source == Control_ReceivingAnalytics_CheckBox_ShowWithPartID)
                    {
                        Uncheck(Control_ReceivingAnalytics_CheckBox_ShowOutsideService);
                        Uncheck(Control_ReceivingAnalytics_CheckBox_ShowConsignment);
                        Uncheck(Control_ReceivingAnalytics_CheckBox_ShowInternal);
                        Uncheck(Control_ReceivingAnalytics_CheckBox_ShowMMC);
                        Uncheck(Control_ReceivingAnalytics_CheckBox_ShowMMF);
                        
                        EnableAllFilters();
                    }
                    else if (source == Control_ReceivingAnalytics_CheckBox_ShowMMC)
                    {
                        Uncheck(Control_ReceivingAnalytics_CheckBox_ShowWithPartID);
                        Uncheck(Control_ReceivingAnalytics_CheckBox_ShowOutsideService);
                        Uncheck(Control_ReceivingAnalytics_CheckBox_ShowConsignment);
                        Uncheck(Control_ReceivingAnalytics_CheckBox_ShowInternal);
                        
                        EnableAllFilters();
                    }
                    else if (source == Control_ReceivingAnalytics_CheckBox_ShowMMF)
                    {
                        Uncheck(Control_ReceivingAnalytics_CheckBox_ShowWithPartID);
                        Uncheck(Control_ReceivingAnalytics_CheckBox_ShowOutsideService);
                        Uncheck(Control_ReceivingAnalytics_CheckBox_ShowConsignment);
                        Uncheck(Control_ReceivingAnalytics_CheckBox_ShowInternal);
                        
                        EnableAllFilters();
                    }
                }
                else
                {
                    // If unchecked, we might need to re-enable things.
                    if (source == Control_ReceivingAnalytics_CheckBox_ShowOutsideService ||
                        source == Control_ReceivingAnalytics_CheckBox_ShowConsignment ||
                        source == Control_ReceivingAnalytics_CheckBox_ShowInternal)
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
            Control_ReceivingAnalytics_CheckBox_ShowConsignment.Enabled = true;
            Control_ReceivingAnalytics_CheckBox_ShowInternal.Enabled = true;
            Control_ReceivingAnalytics_CheckBox_ShowWithPartID.Enabled = true;
            Control_ReceivingAnalytics_CheckBox_ShowMMC.Enabled = true;
            Control_ReceivingAnalytics_CheckBox_ShowMMF.Enabled = true;
        }
        #endregion

    }
}
