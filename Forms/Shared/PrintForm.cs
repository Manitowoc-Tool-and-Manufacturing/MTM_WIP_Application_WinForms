using System.Data;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Forms.Shared;

/// <summary>
/// Comprehensive print dialog with preview, column selection, filtering, and export capabilities.
/// Uses clean Model_PrintJob and Helper_PrintManager/Helper_ExportManager architecture.
/// </summary>
public partial class PrintForm : Form
{
    #region Fields

    private readonly DataGridView _sourceDataGridView;
    private DataTable _sourceData;
    private DataTable _filteredData;
    private Model_PrintJob _printJob;
    private Helper_PrintManager? _printManager;
    private Model_PrintSettings? _userSettings;
    private readonly List<PrintFilter> _activeFilters = new();
    private readonly List<int> _selectedRowIndices = new();
    
    // Track page range values for change detection
    private int _previousFromPage = 1;
    private int _previousToPage = 1;

    #endregion

    #region Constructors

    public PrintForm(DataGridView sourceDataGridView)
    {
        ArgumentNullException.ThrowIfNull(sourceDataGridView);
        
        _sourceDataGridView = sourceDataGridView;
        
        InitializeComponent();
        Core.Core_Themes.ApplyDpiScaling(this);
        Core.Core_Themes.ApplyRuntimeLayoutAdjustments(this);
        
        // Initialize data
        _sourceData = GetDataTableFromDataGridView(_sourceDataGridView);
        _filteredData = _sourceData.Copy();
        
        // Capture selected row indices
        foreach (DataGridViewRow row in _sourceDataGridView.SelectedRows)
        {
            if (!row.IsNewRow)
                _selectedRowIndices.Add(row.Index);
        }
        
        // Initialize print job with defaults
        _printJob = new Model_PrintJob
        {
            Data = _filteredData,
            ColumnOrder = GetColumnOrder(),
            VisibleColumns = GetVisibleColumns(),
            IsLandscape = false,
            IsColor = true,
            PageRangeType = PrintRangeType.AllPages
        };
        
        InitializeForm();
    }

    #endregion

    #region Initialization

    private void InitializeForm()
    {
        LoadUserSettings();
        PopulatePrinters();
        PopulateColumns();
        PopulateFilterColumns();
        PopulatePresets();
        PrintForm_ComboBox_Zoom.Items.AddRange(new object[] { "25%", "50%", "75%", "100%", "125%", "150%", "200%" });
        PrintForm_ComboBox_Zoom.Text = "100%";
        
        // Initialize page range values - set Maximum first, then Value
        PrintForm_NumericUpDown_FromPage.Minimum = 1;
        PrintForm_NumericUpDown_FromPage.Maximum = 9999;
        PrintForm_NumericUpDown_FromPage.Value = 1;
        
        PrintForm_NumericUpDown_ToPage.Minimum = 1;
        PrintForm_NumericUpDown_ToPage.Maximum = 9999;
        PrintForm_NumericUpDown_ToPage.Value = 9999; // Will be updated after preview generation
        
        _previousFromPage = 1;
        _previousToPage = 9999;
        
        // Wire up page range value changed events
        PrintForm_NumericUpDown_FromPage.ValueChanged += PageRangeValue_Changed;
        PrintForm_NumericUpDown_ToPage.ValueChanged += PageRangeValue_Changed;
        PrintForm_NumericUpDown_FromPage.Leave += PageRangeControl_Leave;
        PrintForm_NumericUpDown_ToPage.Leave += PageRangeControl_Leave;
        
        // Set initial page range controls state
        PageRange_CheckedChanged(null, EventArgs.Empty);
        
        RefreshPreview();
        UpdateStatusBar($"Ready - Rows: {_filteredData.Rows.Count}");
    }

    private void PopulatePrinters()
    {
        PrintForm_ComboBox_Printer.Items.Clear();
        foreach (string printer in PrinterSettings.InstalledPrinters)
        {
            PrintForm_ComboBox_Printer.Items.Add(printer);
        }
        
        if (PrintForm_ComboBox_Printer.Items.Count > 0)
        {
            var defaultPrinter = new PrintDocument().PrinterSettings.PrinterName;
            int index = PrintForm_ComboBox_Printer.Items.IndexOf(defaultPrinter);
            PrintForm_ComboBox_Printer.SelectedIndex = index >= 0 ? index : 0;
        }
    }

    private void PopulateColumns()
    {
        PrintForm_CheckedListBox_Columns.Items.Clear();
        
        // Add ALL columns (including hidden ones) so user can select them for printing
        foreach (DataGridViewColumn col in _sourceDataGridView.Columns)
        {
            // Check the column if it's currently visible in the DataGridView
            bool isChecked = col.Visible;
            PrintForm_CheckedListBox_Columns.Items.Add(col.HeaderText, isChecked);
        }
    }

    private void PopulateFilterColumns()
    {
        PrintForm_ComboBox_FilterColumn.Items.Clear();
        PrintForm_ComboBox_FilterColumn.Items.Add("-- Select Column --");
        foreach (DataColumn col in _sourceData.Columns)
        {
            PrintForm_ComboBox_FilterColumn.Items.Add(col.ColumnName);
        }
        PrintForm_ComboBox_FilterColumn.SelectedIndex = 0;
        
        PrintForm_ComboBox_FilterType.Items.Clear();
        PrintForm_ComboBox_FilterType.Items.AddRange(new object[]
        {
            "Contains",
            "Equals",
            "Starts With",
            "Ends With",
            "Date Range",
            "Show Selected Rows Only"
        });
        PrintForm_ComboBox_FilterType.SelectedIndex = 0;
    }

    private void PopulatePresets()
    {
        PrintForm_ComboBox_Presets.Items.Clear();
        PrintForm_ComboBox_Presets.Items.Add("-- Select Preset --");
        
        string presetsDir = GetPresetsDirectory();
        if (Directory.Exists(presetsDir))
        {
            foreach (string file in Directory.GetFiles(presetsDir, "*.json"))
            {
                PrintForm_ComboBox_Presets.Items.Add(Path.GetFileNameWithoutExtension(file));
            }
        }
        PrintForm_ComboBox_Presets.SelectedIndex = 0;
    }

    #endregion

    #region Preview

    private void RefreshPreview()
    {
        try
        {
            // Show progress form
            using (var progressForm = new Form())
            {
                progressForm.Text = "Generating Preview";
                progressForm.Size = new Size(300, 100);
                progressForm.StartPosition = FormStartPosition.CenterParent;
                progressForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                progressForm.ControlBox = false;
                progressForm.MaximizeBox = false;
                progressForm.MinimizeBox = false;
                
                var label = new Label
                {
                    Text = "Please wait while generating preview...",
                    AutoSize = true,
                    Location = new Point(20, 30)
                };
                progressForm.Controls.Add(label);
                
                // Show the form non-modal and process the refresh
                progressForm.Show(this);
                Application.DoEvents();
                
                UpdateStatusBar("Refreshing preview...");
                
                // Dispose old print manager
                _printManager?.Dispose();
                
                // Update print job with current settings
                UpdatePrintJobFromUI();
                
                // Create new print manager
                _printManager = new Helper_PrintManager(_printJob);
                var printDoc = _printManager.PreparePrintDocument();
                
                if (printDoc != null)
                {
                    PrintForm_PrintPreviewControl_Main.Document = printDoc;
                    UpdatePageNavigationButtons();
                    UpdateStatusBar($"Ready - {_filteredData.Rows.Count} rows");
                }
                else
                {
                    UpdateStatusBar("Error preparing preview");
                }
                
                Application.DoEvents();
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            UpdateStatusBar($"Preview error: {ex.Message}");
        }
    }

    private void UpdatePrintJobFromUI()
    {
        _printJob.Data = _filteredData;
        _printJob.ColumnOrder = GetColumnOrder();
        _printJob.VisibleColumns = GetVisibleColumns();
        _printJob.PrinterName = PrintForm_ComboBox_Printer.SelectedItem?.ToString() ?? string.Empty;
        _printJob.IsLandscape = PrintForm_RadioButton_Landscape.Checked;
        _printJob.IsColor = PrintForm_RadioButton_Color.Checked;
        _printJob.CurrentPreviewPage = PrintForm_PrintPreviewControl_Main.StartPage + 1;
        
        if (PrintForm_RadioButton_AllPages.Checked)
        {
            _printJob.PageRangeType = PrintRangeType.AllPages;
        }
        else if (PrintForm_RadioButton_CurrentPage.Checked)
        {
            _printJob.PageRangeType = PrintRangeType.CurrentPage;
        }
        else if (PrintForm_RadioButton_PageRange.Checked)
        {
            _printJob.PageRangeType = PrintRangeType.PageRange;
            _printJob.FromPage = (int)PrintForm_NumericUpDown_FromPage.Value;
            _printJob.ToPage = (int)PrintForm_NumericUpDown_ToPage.Value;
        }
    }

    #endregion

    #region Printing

    private void PrintForm_Button_Print_Click(object? sender, EventArgs e)
    {
        try
        {
            if (_printManager == null)
            {
                MessageBox.Show("Print preview not ready. Please wait.", "Print", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            UpdatePrintJobFromUI();
            
            if (_printManager.Print())
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            MessageBox.Show($"Print failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    #endregion

    #region Export

    private void PrintForm_Button_ExportSettings_Click(object? sender, EventArgs e)
    {
        if (!PrintForm_CheckBox_ExportPdf.Checked && 
            !PrintForm_CheckBox_ExportExcel.Checked && 
            !PrintForm_CheckBox_ExportImage.Checked)
        {
            MessageBox.Show("Please select at least one export format.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        
        UpdatePrintJobFromUI();
        int completed = 0;
        
        // PDF Export
        if (PrintForm_CheckBox_ExportPdf.Checked)
        {
            string? path = Helper_ExportManager.ShowPdfSaveDialog("DataExport");
            if (!string.IsNullOrEmpty(path))
            {
                if (Helper_ExportManager.ExportToPdf(_printJob, path))
                {
                    completed++;
                    MessageBox.Show($"PDF export successful!{Environment.NewLine}{Environment.NewLine}{path}", 
                        "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        
        // Excel Export
        if (PrintForm_CheckBox_ExportExcel.Checked)
        {
            string? path = Helper_ExportManager.ShowExcelSaveDialog("DataExport");
            if (!string.IsNullOrEmpty(path))
            {
                if (Helper_ExportManager.ExportToExcel(_printJob, path))
                {
                    completed++;
                    MessageBox.Show($"Excel export successful!{Environment.NewLine}{Environment.NewLine}{path}", 
                        "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        
        // Image Export
        if (PrintForm_CheckBox_ExportImage.Checked)
        {
            string? path = Helper_ExportManager.ShowImageSaveDialog("DataExport");
            if (!string.IsNullOrEmpty(path))
            {
                ImageFormat format = path.EndsWith(".jpg") ? ImageFormat.Jpeg : ImageFormat.Png;
                if (Helper_ExportManager.ExportToImage(_printJob, path, format))
                {
                    completed++;
                    MessageBox.Show($"Image export successful!{Environment.NewLine}{Environment.NewLine}{path}", 
                        "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        
        if (completed > 0)
        {
            UpdateStatusBar($"Exported {completed} file(s)");
        }
    }

    #endregion

    #region Column Management

    private List<string> GetColumnOrder()
    {
        var order = new List<string>();
        for (int i = 0; i < PrintForm_CheckedListBox_Columns.Items.Count; i++)
        {
            order.Add(PrintForm_CheckedListBox_Columns.Items[i].ToString()!);
        }
        return order;
    }

    private List<string> GetVisibleColumns()
    {
        var visible = new List<string>();
        for (int i = 0; i < PrintForm_CheckedListBox_Columns.Items.Count; i++)
        {
            if (PrintForm_CheckedListBox_Columns.GetItemChecked(i))
            {
                visible.Add(PrintForm_CheckedListBox_Columns.Items[i].ToString()!);
            }
        }
        return visible;
    }

    private void PrintForm_Button_SelectAll_Click(object? sender, EventArgs e)
    {
        for (int i = 0; i < PrintForm_CheckedListBox_Columns.Items.Count; i++)
        {
            PrintForm_CheckedListBox_Columns.SetItemChecked(i, true);
        }
        RefreshPreview();
    }

    private void PrintForm_Button_DeselectAll_Click(object? sender, EventArgs e)
    {
        for (int i = 0; i < PrintForm_CheckedListBox_Columns.Items.Count; i++)
        {
            PrintForm_CheckedListBox_Columns.SetItemChecked(i, false);
        }
        RefreshPreview();
    }

    private void PrintForm_Button_MoveUp_Click(object? sender, EventArgs e)
    {
        int index = PrintForm_CheckedListBox_Columns.SelectedIndex;
        if (index > 0)
        {
            object item = PrintForm_CheckedListBox_Columns.Items[index];
            bool isChecked = PrintForm_CheckedListBox_Columns.GetItemChecked(index);
            PrintForm_CheckedListBox_Columns.Items.RemoveAt(index);
            PrintForm_CheckedListBox_Columns.Items.Insert(index - 1, item);
            PrintForm_CheckedListBox_Columns.SetItemChecked(index - 1, isChecked);
            PrintForm_CheckedListBox_Columns.SelectedIndex = index - 1;
            RefreshPreview();
        }
    }

    private void PrintForm_Button_MoveDown_Click(object? sender, EventArgs e)
    {
        int index = PrintForm_CheckedListBox_Columns.SelectedIndex;
        if (index >= 0 && index < PrintForm_CheckedListBox_Columns.Items.Count - 1)
        {
            object item = PrintForm_CheckedListBox_Columns.Items[index];
            bool isChecked = PrintForm_CheckedListBox_Columns.GetItemChecked(index);
            PrintForm_CheckedListBox_Columns.Items.RemoveAt(index);
            PrintForm_CheckedListBox_Columns.Items.Insert(index + 1, item);
            PrintForm_CheckedListBox_Columns.SetItemChecked(index + 1, isChecked);
            PrintForm_CheckedListBox_Columns.SelectedIndex = index + 1;
            RefreshPreview();
        }
    }

    private void PrintForm_CheckedListBox_Columns_ItemCheck(object? sender, ItemCheckEventArgs e)
    {
        // Only refresh preview if the form handle has been created (i.e., after initialization)
        if (IsHandleCreated)
        {
            BeginInvoke(() => RefreshPreview());
        }
    }

    #endregion

    #region Filters

    private void PrintForm_Button_AddFilter_Click(object? sender, EventArgs e)
    {
        string filterType = PrintForm_ComboBox_FilterType.SelectedItem?.ToString() ?? string.Empty;
        
        if (filterType == "Show Selected Rows Only")
        {
            ApplySelectedRowsFilter();
            return;
        }
        
        string column = PrintForm_ComboBox_FilterColumn.SelectedItem?.ToString() ?? string.Empty;
        if (column == "-- Select Column --")
        {
            MessageBox.Show("Please select a column to filter.", "Filter", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        
        var filter = new PrintFilter
        {
            ColumnName = column,
            FilterType = filterType
        };
        
        if (filterType == "Date Range")
        {
            filter.DateFrom = PrintForm_DateTimePicker_DateFrom.Value.Date;
            filter.DateTo = PrintForm_DateTimePicker_DateTo.Value.Date;
        }
        else
        {
            filter.Value = PrintForm_TextBox_FilterValue.Text;
        }
        
        _activeFilters.Add(filter);
        RefreshActiveFiltersList();
        ApplyFilters();
    }

    private void PrintForm_Button_RemoveFilter_Click(object? sender, EventArgs e)
    {
        if (PrintForm_ListBox_ActiveFilters.SelectedIndex >= 0)
        {
            _activeFilters.RemoveAt(PrintForm_ListBox_ActiveFilters.SelectedIndex);
            RefreshActiveFiltersList();
            ApplyFilters();
        }
    }

    private void PrintForm_Button_ClearFilters_Click(object? sender, EventArgs e)
    {
        _activeFilters.Clear();
        RefreshActiveFiltersList();
        ApplyFilters();
    }

    private void RefreshActiveFiltersList()
    {
        PrintForm_ListBox_ActiveFilters.Items.Clear();
        foreach (var filter in _activeFilters)
        {
            PrintForm_ListBox_ActiveFilters.Items.Add(filter.ToString());
        }
    }

    private void ApplyFilters()
    {
        _filteredData = _sourceData.Copy();
        
        foreach (var filter in _activeFilters)
        {
            ApplySingleFilter(filter);
        }
        
        RefreshPreview();
        UpdateStatusBar($"Filtered to {_filteredData.Rows.Count} rows");
    }

    private void ApplySingleFilter(PrintFilter filter)
    {
        var rowsToRemove = new List<DataRow>();
        
        foreach (DataRow row in _filteredData.Rows)
        {
            if (!filter.MatchesRow(row))
            {
                rowsToRemove.Add(row);
            }
        }
        
        foreach (var row in rowsToRemove)
        {
            _filteredData.Rows.Remove(row);
        }
    }

    private void ApplySelectedRowsFilter()
    {
        if (_selectedRowIndices.Count == 0)
        {
            MessageBox.Show("No rows were selected in the source grid.", "Filter", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        
        _filteredData = _sourceData.Clone();
        foreach (int index in _selectedRowIndices)
        {
            if (index < _sourceData.Rows.Count)
            {
                _filteredData.ImportRow(_sourceData.Rows[index]);
            }
        }
        
        RefreshPreview();
        UpdateStatusBar($"Showing {_filteredData.Rows.Count} selected rows");
    }

    private void PrintForm_ComboBox_FilterType_SelectedIndexChanged(object? sender, EventArgs e)
    {
        string filterType = PrintForm_ComboBox_FilterType.SelectedItem?.ToString() ?? string.Empty;
        bool isDateRange = filterType == "Date Range";
        bool isSelectedRows = filterType == "Show Selected Rows Only";
        
        PrintForm_ComboBox_FilterColumn.Enabled = !isSelectedRows;
        PrintForm_TextBox_FilterValue.Visible = !isDateRange && !isSelectedRows;
        PrintForm_DateTimePicker_DateFrom.Visible = isDateRange;
        PrintForm_DateTimePicker_DateTo.Visible = isDateRange;
    }

    #endregion

    #region Settings Changed

    private void PrintForm_RadioButton_Landscape_CheckedChanged(object? sender, EventArgs e)
    {
        if (PrintForm_RadioButton_Landscape.Checked)
        {
            RefreshPreview();
        }
    }

    private void PrintForm_RadioButton_Color_CheckedChanged(object? sender, EventArgs e)
    {
        if (PrintForm_RadioButton_Color.Checked)
        {
            RefreshPreview();
        }
    }

    private void PrintForm_ComboBox_Printer_SelectedIndexChanged(object? sender, EventArgs e)
    {
        RefreshPreview();
    }

    private void PageRange_CheckedChanged(object? sender, EventArgs e)
    {
        bool enablePageNumbers = PrintForm_RadioButton_PageRange.Checked;
        PrintForm_NumericUpDown_FromPage.Enabled = enablePageNumbers;
        PrintForm_NumericUpDown_ToPage.Enabled = enablePageNumbers;
        PrintForm_Label_PageFrom.Enabled = enablePageNumbers;
        PrintForm_Label_PageTo.Enabled = enablePageNumbers;
    }

    private void PageRangeValue_Changed(object? sender, EventArgs e)
    {
        // Track that values have changed, but don't refresh yet
        // Refresh will happen on Leave event when neither control has focus
    }

    private void PageRangeControl_Leave(object? sender, EventArgs e)
    {
        // Only refresh if neither control has focus and values have actually changed
        if (!PrintForm_NumericUpDown_FromPage.Focused && 
            !PrintForm_NumericUpDown_ToPage.Focused)
        {
            int currentFrom = (int)PrintForm_NumericUpDown_FromPage.Value;
            int currentTo = (int)PrintForm_NumericUpDown_ToPage.Value;
            
            if (currentFrom != _previousFromPage || currentTo != _previousToPage)
            {
                _previousFromPage = currentFrom;
                _previousToPage = currentTo;
                RefreshPreview();
            }
        }
    }

    #endregion

    #region Preview Navigation

    private void PrintForm_Button_FirstPage_Click(object? sender, EventArgs e)
    {
        PrintForm_PrintPreviewControl_Main.StartPage = 0;
        UpdatePageNavigationButtons();
    }

    private void PrintForm_Button_PreviousPage_Click(object? sender, EventArgs e)
    {
        if (PrintForm_PrintPreviewControl_Main.StartPage > 0)
        {
            PrintForm_PrintPreviewControl_Main.StartPage--;
            UpdatePageNavigationButtons();
        }
    }

    private void PrintForm_Button_NextPage_Click(object? sender, EventArgs e)
    {
        PrintForm_PrintPreviewControl_Main.StartPage++;
        UpdatePageNavigationButtons();
    }

    private void PrintForm_Button_LastPage_Click(object? sender, EventArgs e)
    {
        // Navigate to the last page by setting StartPage to a high value
        // PrintPreviewControl will automatically cap it at the last available page
        if (_printManager?.PreparePrintDocument() is PrintDocument doc)
        {
            // Set to a high page number - the control will cap it at the actual last page
            PrintForm_PrintPreviewControl_Main.StartPage = 9999;
            UpdatePageNavigationButtons();
        }
    }

    private void UpdatePageNavigationButtons()
    {
        int currentPage = PrintForm_PrintPreviewControl_Main.StartPage + 1;
        PrintForm_Label_PageInfo.Text = $"Page {currentPage}";
        PrintForm_Button_FirstPage.Enabled = currentPage > 1;
        PrintForm_Button_PreviousPage.Enabled = currentPage > 1;
    }

    private void PrintForm_Button_ZoomIn_Click(object? sender, EventArgs e)
    {
        PrintForm_PrintPreviewControl_Main.Zoom = Math.Min(2.0, PrintForm_PrintPreviewControl_Main.Zoom + 0.25);
        PrintForm_ComboBox_Zoom.Text = $"{(int)(PrintForm_PrintPreviewControl_Main.Zoom * 100)}%";
    }

    private void PrintForm_Button_ZoomOut_Click(object? sender, EventArgs e)
    {
        PrintForm_PrintPreviewControl_Main.Zoom = Math.Max(0.25, PrintForm_PrintPreviewControl_Main.Zoom - 0.25);
        PrintForm_ComboBox_Zoom.Text = $"{(int)(PrintForm_PrintPreviewControl_Main.Zoom * 100)}%";
    }

    private void PrintForm_ComboBox_Zoom_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (PrintForm_ComboBox_Zoom.SelectedItem != null)
        {
            string zoom = PrintForm_ComboBox_Zoom.SelectedItem.ToString()!.Replace("%", string.Empty);
            if (int.TryParse(zoom, out int zoomPercent))
            {
                PrintForm_PrintPreviewControl_Main.Zoom = zoomPercent / 100.0;
            }
        }
    }

    #endregion

    #region Presets

    private void PrintForm_Button_SavePreset_Click(object? sender, EventArgs e)
    {
        string presetName = Microsoft.VisualBasic.Interaction.InputBox("Enter preset name:", "Save Preset", string.Empty);
        if (string.IsNullOrWhiteSpace(presetName)) return;
        
        var preset = CreatePresetFromUI();
        SavePreset(presetName, preset);
        PopulatePresets();
        MessageBox.Show("Preset saved successfully.", "Save Preset", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void PrintForm_Button_DeletePreset_Click(object? sender, EventArgs e)
    {
        if (PrintForm_ComboBox_Presets.SelectedIndex > 0)
        {
            string presetName = PrintForm_ComboBox_Presets.SelectedItem!.ToString()!;
            if (MessageBox.Show($"Delete preset \"{presetName}\"?", "Delete Preset", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DeletePreset(presetName);
                PopulatePresets();
            }
        }
    }

    private void PrintForm_ComboBox_Presets_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (PrintForm_ComboBox_Presets.SelectedIndex > 0)
        {
            string presetName = PrintForm_ComboBox_Presets.SelectedItem!.ToString()!;
            LoadPreset(presetName);
        }
    }

    #endregion

    #region Utilities

    private DataTable GetDataTableFromDataGridView(DataGridView dgv)
    {
        var dt = new DataTable();
        
        // Add ALL columns (including hidden ones) to the DataTable
        foreach (DataGridViewColumn col in dgv.Columns)
        {
            dt.Columns.Add(col.HeaderText, typeof(string));
        }
        
        // Extract all cell values (including from hidden columns)
        foreach (DataGridViewRow row in dgv.Rows)
        {
            if (!row.IsNewRow)
            {
                var values = new List<object>();
                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    values.Add(row.Cells[col.Index].Value?.ToString() ?? string.Empty);
                }
                dt.Rows.Add(values.ToArray());
            }
        }
        
        return dt;
    }

    private void UpdateStatusBar(string message)
    {
        PrintForm_ToolStripStatusLabel_Status.Text = message;
    }

    private string GetPresetsDirectory()
    {
        string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        string dir = Path.Combine(appData, "MTM", "PrintPresets");
        Directory.CreateDirectory(dir);
        return dir;
    }

    private PrintPreset CreatePresetFromUI()
    {
        return new PrintPreset
        {
            ColumnOrder = GetColumnOrder(),
            VisibleColumns = GetVisibleColumns(),
            IsLandscape = PrintForm_RadioButton_Landscape.Checked,
            IsColor = PrintForm_RadioButton_Color.Checked
        };
    }

    private void SavePreset(string name, PrintPreset preset)
    {
        string path = Path.Combine(GetPresetsDirectory(), $"{name}.json");
        string json = System.Text.Json.JsonSerializer.Serialize(preset);
        File.WriteAllText(path, json);
    }

    private void LoadPreset(string name)
    {
        string path = Path.Combine(GetPresetsDirectory(), $"{name}.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            var preset = System.Text.Json.JsonSerializer.Deserialize<PrintPreset>(json);
            if (preset != null)
            {
                ApplyPresetToUI(preset);
                RefreshPreview();
            }
        }
    }

    private void ApplyPresetToUI(PrintPreset preset)
    {
        PrintForm_RadioButton_Landscape.Checked = preset.IsLandscape;
        PrintForm_RadioButton_Portrait.Checked = !preset.IsLandscape;
        PrintForm_RadioButton_Color.Checked = preset.IsColor;
        PrintForm_RadioButton_BlackWhite.Checked = !preset.IsColor;
    }

    private void DeletePreset(string name)
    {
        string path = Path.Combine(GetPresetsDirectory(), $"{name}.json");
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

    private void LoadUserSettings()
    {
        // Load from %AppData%\MTM\PrintSettings.json if needed
    }

    private void PrintForm_Button_Cancel_Click(object? sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _printManager?.Dispose();
        }
        base.Dispose(disposing);
    }

    #endregion
}

#region Supporting Classes

public class PrintFilter
{
    public string ColumnName { get; set; } = string.Empty;
    public string FilterType { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }

    public bool MatchesRow(DataRow row)
    {
        if (!row.Table.Columns.Contains(ColumnName)) return false;
        
        string cellValue = row[ColumnName]?.ToString() ?? string.Empty;
        
        return FilterType switch
        {
            "Contains" => cellValue.Contains(Value, StringComparison.OrdinalIgnoreCase),
            "Equals" => cellValue.Equals(Value, StringComparison.OrdinalIgnoreCase),
            "Starts With" => cellValue.StartsWith(Value, StringComparison.OrdinalIgnoreCase),
            "Ends With" => cellValue.EndsWith(Value, StringComparison.OrdinalIgnoreCase),
            "Date Range" => DateTime.TryParse(cellValue, out DateTime date) && date >= DateFrom && date <= DateTo,
            _ => true
        };
    }

    public override string ToString()
    {
        return FilterType == "Date Range"
            ? $"{ColumnName}: {FilterType} ({DateFrom:d} to {DateTo:d})"
            : $"{ColumnName}: {FilterType} \"{Value}\"";
    }
}

public class PrintPreset
{
    public List<string> ColumnOrder { get; set; } = new();
    public List<string> VisibleColumns { get; set; } = new();
    public bool IsLandscape { get; set; }
    public bool IsColor { get; set; }
}

#endregion
