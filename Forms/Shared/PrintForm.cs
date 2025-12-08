using System.Data;
using System.Drawing.Printing;
using System.Globalization;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Services.Logging;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;
using MTM_WIP_Application_Winforms.Forms.Help;

namespace MTM_WIP_Application_Winforms.Forms.Shared;

/// <summary>
/// Compact sidebar dialog that orchestrates print preview, printing, and export flows for any grid-backed dataset.
/// Migrated to ThemedForm for automatic DPI scaling and theme support.
/// </summary>
/// <remarks>
/// The constructor follows the mandatory theme integration sequence: <c>InitializeComponent()</c>,
/// with DPI scaling and layout adjustments now handled automatically by ThemedForm.OnLoad.
/// Callers must provide a fully populated <see cref="Model_Print_Job"/> and its previously persisted
/// <see cref="Model_Print_Settings"/> so the dialog can restore printer and column preferences.
/// </remarks>
public partial class PrintForm : ThemedForm
{
    #region Fields

    private const string PrinterUnavailableDisplayText = "(No printers installed)";
    private const string ZoomFitToPage = "Fit to Page";

    private readonly Model_Print_Job _printJob;
    private readonly Model_Print_Settings _printSettings;
    private readonly Dictionary<string, double> _zoomLevelLookup = new(StringComparer.OrdinalIgnoreCase)
    {
        ["25%"] = 0.25,
        ["50%"] = 0.5,
        ["75%"] = 0.75,
        ["100%"] = 1.0,
        ["125%"] = 1.25,
        ["150%"] = 1.5,
        ["200%"] = 2.0
    };

    private Helper_PrintManager? _printManager;
    private PrintDocument? _previewDocument;
    private PreviewPageInfo[] _previewPageInfos = Array.Empty<PreviewPageInfo>();

    private bool _isPrinterSettingsExpanded = true;
    private bool _isUpdatingPrinterSection;
    private bool _isPageSettingsExpanded = true;
    private bool _isUpdatingPageSettings;
    private bool _isColumnSettingsExpanded = true;
    private bool _isUpdatingColumnSettings;
    private bool _isOptionsExpanded = true;
    private bool _isUpdatingOptionsSection;
    private bool _isUpdatingZoomSelection;
    private bool _isNavigationUpdating;
    private bool _isGeneratingPreview;
    private bool _isPreviewRefreshPending;
    private bool _isCustomRangeValid = true;

    private int _previewTotalPages;
    private string _selectedZoomOption = ZoomFitToPage;
    private string? _windowTitleSnapshot;
    private string? _exportButtonTextSnapshot;

    private bool _isLeftPanelExpanded = false;
    private bool _isRightPanelExpanded = false;
    private bool _isPrinterOnline = true;

    #endregion

    #region Properties

    /// <summary>
    /// Gets the mutable print settings instance backing the dialog.
    /// </summary>
    /// <summary>
    /// Gets the user-specific print settings backing this dialog instance.
    /// </summary>
    /// <remarks>
    /// Settings are loaded on construction and saved after a successful print or export operation.
    /// Only printer information and column visibility/order are persisted per FR-030.
    /// </remarks>
    public Model_Print_Settings PrintSettings => _printSettings;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="PrintForm"/> class.
    /// </summary>
    /// <param name="printJob">Print job describing the current preview configuration.</param>
    /// <param name="printSettings">Persisted user preferences for the originating grid.</param>
    /// <summary>
    /// Initializes a new instance of the <see cref="PrintForm"/> class with the supplied job configuration and persisted settings.
    /// </summary>
    /// <param name="printJob">Fully prepared print job containing source data, visible columns, and page configuration.</param>
    /// <param name="printSettings">Persisted user settings for the originating grid, used to restore printer and column preferences.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="printJob"/> or <paramref name="printSettings"/> is <see langword="null"/>.</exception>
    /// <remarks>
    /// The constructor applies model settings, initializes collapsible sidebar sections, and immediately schedules a preview refresh.
    /// Manual callers should invoke <see cref="ShowDialog()"/> to display the modal workflow.
    /// </remarks>
    public PrintForm(Model_Print_Job printJob, Model_Print_Settings printSettings)
    {
        ArgumentNullException.ThrowIfNull(printJob);
        ArgumentNullException.ThrowIfNull(printSettings);

        _printJob = printJob;
        _printSettings = printSettings;



        InitializeComponent();
        InitializeHelpButton();
        // DPI scaling and layout now handled by ThemedForm.OnLoad
        ApplyThemeColors();

        WireEventHandlers();

        LoadSettings();
        InitializeSections();
    }

    private void WireEventHandlers()
    {
        // Form
        Shown += PrintForm_Shown;
        PrintForm_PrintPreviewControl.StartPageChanged += PrintForm_PrintPreviewControl_StartPageChanged;

        // Buttons
        PrintForm_Button_PrinterSettingsToggle.Click += PrintForm_Button_PrinterSettingsToggle_Click;
        PrintForm_Button_PageSettingsToggle.Click += PrintForm_Button_PageSettingsToggle_Click;
        PrintForm_Button_ColumnSettingsToggle.Click += PrintForm_Button_ColumnSettingsToggle_Click;
        PrintForm_Button_OptionsToggle.Click += PrintForm_Button_OptionsToggle_Click;
        PrintForm_Button_ColumnUp.Click += PrintForm_Button_ColumnUp_Click;
        PrintForm_Button_ColumnDown.Click += PrintForm_Button_ColumnDown_Click;
        PrintForm_Button_FirstPage.Click += PrintForm_Button_FirstPage_Click;
        PrintForm_Button_PreviousPage.Click += PrintForm_Button_PreviousPage_Click;
        PrintForm_Button_NextPage.Click += PrintForm_Button_NextPage_Click;
        PrintForm_Button_LastPage.Click += PrintForm_Button_LastPage_Click;
        PrintForm_Button_Print.Click += PrintForm_Button_Print_Click;
        PrintForm_Button_Export.Click += PrintForm_Button_Export_Click;
        PrintForm_Button_Cancel.Click += PrintForm_Button_Cancel_Click;
        PrintForm_Button_LeftPanelToggle.Click += PrintForm_Button_LeftPanelToggle_Click;
        PrintForm_Button_RightPanelToggle.Click += PrintForm_Button_RightPanelToggle_Click;

        // Context Menu
        PrintForm_ToolStripMenuItem_ExportPdf.Click += PrintForm_ToolStripMenuItem_ExportPdf_Click;
        PrintForm_ToolStripMenuItem_ExportExcel.Click += PrintForm_ToolStripMenuItem_ExportExcel_Click;

        // ComboBoxes
        PrintForm_ComboBox_Printer.SelectedIndexChanged += PrintForm_ComboBox_Printer_SelectedIndexChanged;
        PrintForm_ComboBox_Zoom.SelectedIndexChanged += PrintForm_ComboBox_Zoom_SelectedIndexChanged;

        // RadioButtons
        PrintForm_RadioButton_Portrait.CheckedChanged += PrintForm_RadioButton_Portrait_CheckedChanged;
        PrintForm_RadioButton_Landscape.CheckedChanged += PrintForm_RadioButton_Landscape_CheckedChanged;
        PrintForm_RadioButton_AllPages.CheckedChanged += PrintForm_RadioButton_AllPages_CheckedChanged;
        PrintForm_RadioButton_CurrentPage.CheckedChanged += PrintForm_RadioButton_CurrentPage_CheckedChanged;
        PrintForm_RadioButton_CustomRange.CheckedChanged += PrintForm_RadioButton_CustomRange_CheckedChanged;
        PrintForm_RadioButton_Color.CheckedChanged += PrintForm_RadioButton_Color_CheckedChanged;
        PrintForm_RadioButton_Grayscale.CheckedChanged += PrintForm_RadioButton_Grayscale_CheckedChanged;

        // TextBoxes
        PrintForm_TextBox_FromPage.TextChanged += PrintForm_TextBox_FromPage_TextChanged;
        PrintForm_TextBox_ToPage.TextChanged += PrintForm_TextBox_ToPage_TextChanged;

        // CheckedListBox
        PrintForm_CheckedListBox_Columns.SelectedIndexChanged += PrintForm_CheckedListBox_Columns_SelectedIndexChanged;
        PrintForm_CheckedListBox_Columns.ItemCheck += PrintForm_CheckedListBox_Columns_ItemCheck;

        // CheckBox
        PrintForm_CheckBox_AddNotesColumn.CheckedChanged += PrintForm_CheckBox_AddNotesColumn_CheckedChanged;

        // NumericUpDown
        PrintForm_NumericUpDown_AddNotesColumn.ValueChanged += PrintForm_NumericUpDown_AddNotesColumn_ValueChanged;
    }

    #endregion

    #region Initialization

    private void InitializeSections()
    {
        InitializePrinterSettingsSection();
        InitializePageSettingsSection();
        InitializeColumnSettingsSection();
        InitializeOptionsSection();
        InitializePreviewSection();
        InitializeActionButtons();
        InitializeLeftPanelToggle();
        InitializeRightPanelToggle();
    }

    private void InitializeLeftPanelToggle()
    {
        if (PrintForm_TableLayout_LeftSidebar.ColumnStyles.Count > 1)
        {
            PrintForm_Button_LeftPanelToggle.Text = _isLeftPanelExpanded ? "📋 🡲" : "🡰 📋";
            UpdateLeftPanelToggleState();
        }
    }

    private void InitializeRightPanelToggle()
    {
        if (PrintForm_TableLayout_RightSidebar.ColumnStyles.Count > 0)
        {
            PrintForm_Button_RightPanelToggle.Text = _isRightPanelExpanded ? "🡰 📋" : "📋 🡲";
            UpdateRightPanelToggleState();
        }
    }

    private void UpdateLeftPanelToggleState()
    {
        if (_isLeftPanelExpanded)
        {
            PrintForm_TableLayout_PrinterSettingsSection.Visible = false;
            PrintForm_TableLayout_PageSettingsSection.Visible = false;
            PrintForm_TableLayout_ActionButtons.Visible = false;
            _isLeftPanelExpanded = false;
        }
        else
        {
            PrintForm_TableLayout_PrinterSettingsSection.Visible = true;
            PrintForm_TableLayout_PageSettingsSection.Visible = true;
            PrintForm_TableLayout_ActionButtons.Visible = true;
            _isLeftPanelExpanded = true;
        }
    }

    private void UpdateRightPanelToggleState()
    {
        if (_isRightPanelExpanded)
        {
            PrintForm_TableLayout_ColumnSettingsSection.Visible = false;
            PrintForm_TableLayout_OptionsSection.Visible = false;
            _isRightPanelExpanded = false;
        }
        else
        {
            PrintForm_TableLayout_ColumnSettingsSection.Visible = true;
            PrintForm_TableLayout_OptionsSection.Visible = true;
            _isRightPanelExpanded = true;
        }
    }

    #endregion

    #region Key Processing

    private async void PrintForm_Shown(object? sender, EventArgs e)
    {
        Shown -= PrintForm_Shown;
        await GeneratePreviewAsync();
    }

    private void SchedulePreviewRefresh()
    {
        if (!IsHandleCreated)
        {
            return;
        }

        if (_isGeneratingPreview)
        {
            _isPreviewRefreshPending = true;
            return;
        }

        _isPreviewRefreshPending = false;
        _ = GeneratePreviewAsync();
    }

    private async Task GeneratePreviewAsync()
    {
        // Prevent re-entry
        if (_isGeneratingPreview)
        {
            _isPreviewRefreshPending = true;
            return;
        }

        // Validate data availability
        if (_printJob.VisibleColumns.Count == 0 || _printJob.Data is not { Rows.Count: > 0 })
        {
            ResetPreviewState();
            ValidateCustomRangeInputs();
            return;
        }

        _isGeneratingPreview = true;
        UpdateActionButtonsState();

        // Detach document to prevent accessing disposed object
        if (PrintForm_PrintPreviewControl.Document != null)
        {
            PrintForm_PrintPreviewControl.Document = null;
        }

        Cursor? previousCursor = Cursor.Current;
        Cursor.Current = Cursors.WaitCursor;

        // Capture state to restore later
        Enum_PrintRangeType originalRange = _printJob.PageRangeType;
        int originalFromPage = _printJob.FromPage;
        int originalToPage = _printJob.ToPage;
        int originalCurrentPage = _printJob.CurrentPage;
        string? originalPrinterName = _printJob.PrinterName;

        PrintDocument? previewDocument = null;
        PreviewPageInfo[] pageInfos = Array.Empty<PreviewPageInfo>();
        bool generationSucceeded = false;

        try
        {
            // Configure for full preview generation
            _printJob.PageRangeType = Enum_PrintRangeType.AllPages;
            _printJob.FromPage = 1;
            _printJob.ToPage = Math.Max(1, _printJob.TotalPages);
            // Try to maintain current page if possible, otherwise default to 1
            _printJob.CurrentPage = Math.Max(1, originalCurrentPage);

            // Attempt 1: Primary Printer
            try
            {
                // Use a 4-second timeout for the primary printer to prevent hanging if offline
                (previewDocument, pageInfos) = await CreatePreviewDocumentAsync(4000);
                generationSucceeded = true;
            }
            catch (Exception ex)
            {
                LoggingUtility.Log($"Primary preview generation failed: {ex.Message}");
            }

            // Attempt 2: Fallback to Default Printer (if primary failed)
            if (!generationSucceeded)
            {
                try
                {
                    LoggingUtility.Log("Attempting fallback preview generation with default printer.");
                    _printJob.PrinterName = new PrinterSettings().PrinterName;
                    
                    // Re-configure range in case it was modified
                    _printJob.PageRangeType = Enum_PrintRangeType.AllPages;
                    _printJob.FromPage = 1;
                    _printJob.ToPage = Math.Max(1, _printJob.TotalPages);
                    _printJob.CurrentPage = Math.Max(1, originalCurrentPage);

                    // Force new print manager for fallback to ensure clean state
                    if (_printManager != null)
                    {
                        _printManager.Dispose();
                        _printManager = null;
                    }

                    // Give fallback more time as it should be reliable
                    (previewDocument, pageInfos) = await CreatePreviewDocumentAsync(10000);
                    generationSucceeded = true;
                }
                catch (Exception ex)
                {
                    LoggingUtility.LogApplicationError(ex);
                    Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, controlName: nameof(PrintForm));
                }
            }
        }
        finally
        {
            // Restore original settings
            _printJob.PrinterName = originalPrinterName;
            _printJob.PageRangeType = originalRange;
            _printJob.FromPage = originalFromPage;
            _printJob.ToPage = originalToPage;
            _printJob.CurrentPage = originalCurrentPage;

            Cursor.Current = previousCursor ?? Cursors.Default;
            _isGeneratingPreview = false;
        }

        // Apply results or reset
        if (generationSucceeded && previewDocument != null)
        {
            ApplyPreviewDocument(previewDocument, pageInfos, originalCurrentPage);
        }
        else
        {
            ResetPreviewState();
            ValidateCustomRangeInputs();
        }

        // Handle pending refresh
        if (_isPreviewRefreshPending)
        {
            _isPreviewRefreshPending = false;
            _ = GeneratePreviewAsync();
        }
    }

    private async Task<(PrintDocument?, PreviewPageInfo[])> CreatePreviewDocumentAsync(int timeoutMs)
    {
        _printManager ??= new Helper_PrintManager(_printJob);
        _printManager.IsPreview = true;
        
        PrintDocument? doc = _printManager.PreparePrintDocument();
        if (doc is null) return (null, Array.Empty<PreviewPageInfo>());

        var controller = new PreviewPrintController { UseAntiAlias = true };
        doc.PrintController = controller;

        // Run printing with timeout to prevent hanging on offline printers
        var printTask = Task.Run(() => doc.Print());
        
        if (await Task.WhenAny(printTask, Task.Delay(timeoutMs)) != printTask)
        {
            throw new TimeoutException($"Preview generation timed out after {timeoutMs}ms");
        }

        // Ensure we catch any exceptions from the print task
        await printTask;
        
        var infos = controller.GetPreviewPageInfo() ?? Array.Empty<PreviewPageInfo>();
        _printManager.SyncPageBoundariesFromPrinter();
        
        return (doc, infos);
    }

    private void ApplyPreviewDocument(PrintDocument doc, PreviewPageInfo[] infos, int targetPage)
    {
        _previewDocument = doc;
        _previewPageInfos = infos;
        _previewTotalPages = _printJob.TotalPages > 0 ? _printJob.TotalPages : infos.Length;
        _printJob.TotalPages = _previewTotalPages;

        // Validate ranges
        if (_previewTotalPages > 0)
        {
            _printJob.FromPage = Math.Clamp(_printJob.FromPage, 1, _previewTotalPages);
            _printJob.ToPage = Math.Clamp(_printJob.ToPage, _printJob.FromPage, _previewTotalPages);
        }
        else
        {
            _printJob.FromPage = 1;
            _printJob.ToPage = 1;
        }

        // Calculate target index
        int targetIndex = _previewTotalPages > 0
            ? Math.Clamp(targetPage - 1, 0, _previewTotalPages - 1)
            : 0;

        PrintForm_PrintPreviewControl.Document = null;
        PrintForm_PrintPreviewControl.Document = doc;
        PrintForm_PrintPreviewControl.StartPage = targetIndex;
        PrintForm_PrintPreviewControl.InvalidatePreview();

        _printJob.CurrentPage = _previewTotalPages > 0 ? targetIndex + 1 : 1;

        ApplyZoomSelection();
        UpdatePreviewNavigationState();
        UpdatePageRangeControls();
        UpdateActionButtonsState();
    }

    private void ResetPreviewState()
    {
        _previewTotalPages = 0;
        _printJob.TotalPages = 0;
        _printJob.SetPageBoundaries(Array.Empty<Model_Print_PageBoundary>());
        _previewDocument = null;
        _previewPageInfos = Array.Empty<PreviewPageInfo>();
        PrintForm_PrintPreviewControl.Document = null;
        UpdatePreviewNavigationState();
        UpdateActionButtonsState();
    }

    private void InitializePreviewSection()
    {
        _previewTotalPages = Math.Max(0, _printJob.TotalPages);
        PrintForm_PrintPreviewControl.AutoZoom = true;
        PrintForm_PrintPreviewControl.Zoom = 1.0;


        PrintForm_PrintPreviewControl.StartPage = 0;


        UpdatePreviewNavigationState();
    }

    private void UpdatePreviewNavigationState()
    {
        if (!IsHandleCreated)
        {
            return;
        }

        if (InvokeRequired)
        {
            BeginInvoke(new Action(UpdatePreviewNavigationState));
            return;
        }

        _isNavigationUpdating = true;

        try
        {
            int currentIndex = Math.Clamp(PrintForm_PrintPreviewControl.StartPage, 0, Math.Max(0, _previewTotalPages - 1));
            int displayPage = _previewTotalPages > 0 ? currentIndex + 1 : 0;



            PrintForm_Label_PageCounter.Text = $"Page {displayPage} / {_previewTotalPages}";

            bool hasPages = _previewTotalPages > 0;
            bool hasPrevious = hasPages && currentIndex > 0;
            bool hasNext = hasPages && currentIndex < _previewTotalPages - 1;

            PrintForm_Button_FirstPage.Enabled = hasPrevious;
            PrintForm_Button_PreviousPage.Enabled = hasPrevious;
            PrintForm_Button_NextPage.Enabled = hasNext;
            PrintForm_Button_LastPage.Enabled = hasNext;

            _printJob.CurrentPage = hasPages ? currentIndex + 1 : 1;



            if (_printJob.PageRangeType == Enum_PrintRangeType.AllPages)
            {
                UpdateCustomRangeTextBoxValue(PrintForm_TextBox_FromPage, 1);
                UpdateCustomRangeTextBoxValue(PrintForm_TextBox_ToPage, Math.Max(1, _previewTotalPages));
            }
        }
        finally
        {
            _isNavigationUpdating = false;
        }

        UpdateActionButtonsState();
    }

    private void ChangePreviewPage(int requestedIndex)
    {
        if (_isNavigationUpdating || _previewTotalPages <= 0)
        {
            return;
        }

        int clampedIndex = Math.Clamp(requestedIndex, 0, Math.Max(0, _previewTotalPages - 1));
        PrintForm_PrintPreviewControl.StartPage = clampedIndex;
        UpdatePreviewNavigationState();

        if (_printJob.PageRangeType == Enum_PrintRangeType.CurrentPage)
        {
            _printJob.FromPage = _printJob.CurrentPage;
            _printJob.ToPage = _printJob.CurrentPage;
            UpdateCustomRangeTextBoxValue(PrintForm_TextBox_FromPage, _printJob.FromPage);
            UpdateCustomRangeTextBoxValue(PrintForm_TextBox_ToPage, _printJob.ToPage);
        }
    }

    #endregion

    #region Button Clicks

    private void PrintForm_Button_PrinterSettingsToggle_Click(object? sender, EventArgs e)
    {
        _isPrinterSettingsExpanded = !_isPrinterSettingsExpanded;
        UpdatePrinterSettingsCollapseState();
    }

    private void PrintForm_Button_PageSettingsToggle_Click(object? sender, EventArgs e)
    {
        _isPageSettingsExpanded = !_isPageSettingsExpanded;
        UpdatePageSettingsCollapseState();
    }

    private void PrintForm_Button_ColumnSettingsToggle_Click(object? sender, EventArgs e)
    {
        _isColumnSettingsExpanded = !_isColumnSettingsExpanded;
        UpdateColumnSettingsCollapseState();
    }

    private void PrintForm_Button_OptionsToggle_Click(object? sender, EventArgs e)
    {
        _isOptionsExpanded = !_isOptionsExpanded;
        UpdateOptionsCollapseState();
    }

    private void PrintForm_Button_ColumnUp_Click(object? sender, EventArgs e)
    {
        MoveColumnItem(-1);
    }

    private void PrintForm_Button_ColumnDown_Click(object? sender, EventArgs e)
    {
        MoveColumnItem(1);
    }

    private void PrintForm_Button_FirstPage_Click(object? sender, EventArgs e)
    {
        ChangePreviewPage(0);
    }

    private void PrintForm_Button_PreviousPage_Click(object? sender, EventArgs e)
    {
        ChangePreviewPage(PrintForm_PrintPreviewControl.StartPage - 1);
    }

    private void PrintForm_Button_NextPage_Click(object? sender, EventArgs e)
    {
        ChangePreviewPage(PrintForm_PrintPreviewControl.StartPage + 1);
    }

    private void PrintForm_Button_LastPage_Click(object? sender, EventArgs e)
    {
        if (_previewTotalPages <= 0)
        {
            return;
        }

        ChangePreviewPage(_previewTotalPages - 1);
    }

    private void PrintForm_Button_Print_Click(object? sender, EventArgs e)
    {
        if (_previewDocument is null)
        {
            Service_ErrorHandler.HandleValidationError("Generate a preview before printing.", nameof(PrintForm));
            return;
        }

        try
        {
            UpdateActionButtonsState();

            _printManager ??= new Helper_PrintManager(_printJob);
            _printManager.IsPreview = false;
            bool printSucceeded = _printManager.Print();

            if (printSucceeded)
            {
                SaveSettings();
                DialogResult = DialogResult.OK;
                Close();
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, controlName: nameof(PrintForm));
        }
        finally
        {
            UpdateActionButtonsState();
        }
    }

    private void PrintForm_Button_Export_Click(object? sender, EventArgs e)
    {
        if (!PrintForm_Button_Export.Enabled)
        {
            return;
        }

        Point menuLocation = new(0, PrintForm_Button_Export.Height);
        PrintForm_ContextMenu_Export.Show(PrintForm_Button_Export, menuLocation);
    }

    private void PrintForm_Button_Cancel_Click(object? sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }

    private async void PrintForm_ToolStripMenuItem_ExportPdf_Click(object? sender, EventArgs e)
    {
        PrintForm_ContextMenu_Export.Close();
        await ExecuteExportAsync(
            Helper_ExportManager.ExportToPdfAsync,
            "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*",
            ".pdf",
            "Export to PDF");
    }

    private async void PrintForm_ToolStripMenuItem_ExportExcel_Click(object? sender, EventArgs e)
    {
        PrintForm_ContextMenu_Export.Close();
        await ExecuteExportAsync(
            Helper_ExportManager.ExportToExcelAsync,
            "Excel Workbook (*.xlsx)|*.xlsx|All files (*.*)|*.*",
            ".xlsx",
            "Export to Excel");
    }

    private void PrintForm_Button_LeftPanelToggle_Click(object? sender, EventArgs e)
    {
        PrintForm_Button_LeftPanelToggle.Text = _isLeftPanelExpanded ? "📋 🡲" : "🡰 📋";
        UpdateLeftPanelToggleState();
    }

    private void PrintForm_Button_RightPanelToggle_Click(object? sender, EventArgs e)
    {
        PrintForm_Button_RightPanelToggle.Text = _isRightPanelExpanded ? "🡰 📋" : "📋 🡲";
        UpdateRightPanelToggleState();
    }

    #endregion

    #region ComboBox & UI Events

    private void PrintForm_ComboBox_Printer_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (_isUpdatingPrinterSection) return;

        string? selectedText = PrintForm_ComboBox_Printer.SelectedItem?.ToString();
        if (string.IsNullOrEmpty(selectedText) ||
            string.Equals(selectedText, PrinterUnavailableDisplayText, StringComparison.OrdinalIgnoreCase))
        {
            _isPrinterOnline = false;
            UpdateActionButtonsState();
            return;
        }

        // We rely on Windows to handle printer connectivity. 
        // We assume the printer is available if it's in the list.
        _isPrinterOnline = true;
        
        // Reset visual state in case it was changed previously
        Color normalColor = Model_Application_Variables.UserUiColors.ComboBoxForeColor ?? SystemColors.WindowText;
        PrintForm_ComboBox_Printer.ForeColor = normalColor;

        _printJob.PrinterName = selectedText;
        _printSettings.PrinterName = selectedText;

        UpdateActionButtonsState();
        SchedulePreviewRefresh();
    }

    private void PrintForm_RadioButton_Portrait_CheckedChanged(object? sender, EventArgs e)
    {
        if (_isUpdatingPrinterSection || !PrintForm_RadioButton_Portrait.Checked)
        {
            return;
        }

        _printJob.Landscape = false;
        SchedulePreviewRefresh();
    }

    private void PrintForm_RadioButton_Landscape_CheckedChanged(object? sender, EventArgs e)
    {
        if (_isUpdatingPrinterSection || !PrintForm_RadioButton_Landscape.Checked)
        {
            return;
        }

        _printJob.Landscape = true;
        SchedulePreviewRefresh();
    }

    private void PrintForm_RadioButton_AllPages_CheckedChanged(object? sender, EventArgs e)
    {
        if (_isUpdatingPageSettings || !PrintForm_RadioButton_AllPages.Checked)
        {
            return;
        }

        _printJob.PageRangeType = Enum_PrintRangeType.AllPages;
        UpdatePageRangeControls();
    }

    private void PrintForm_RadioButton_CurrentPage_CheckedChanged(object? sender, EventArgs e)
    {
        if (_isUpdatingPageSettings || !PrintForm_RadioButton_CurrentPage.Checked)
        {
            return;
        }

        _printJob.PageRangeType = Enum_PrintRangeType.CurrentPage;
        UpdatePageRangeControls();
    }

    private void PrintForm_RadioButton_CustomRange_CheckedChanged(object? sender, EventArgs e)
    {
        if (_isUpdatingPageSettings || !PrintForm_RadioButton_CustomRange.Checked)
        {
            return;
        }

        _printJob.PageRangeType = Enum_PrintRangeType.PageRange;
        UpdatePageRangeControls();
    }

    private void PrintForm_TextBox_FromPage_TextChanged(object? sender, EventArgs e)
    {
        if (_isUpdatingPageSettings)
        {
            return;
        }

        ValidateCustomRangeInputs();
    }

    private void PrintForm_TextBox_ToPage_TextChanged(object? sender, EventArgs e)
    {
        if (_isUpdatingPageSettings)
        {
            return;
        }

        ValidateCustomRangeInputs();
    }

    private void PrintForm_CheckedListBox_Columns_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (_isUpdatingColumnSettings)
        {
            return;
        }

        UpdateColumnActionStates();
    }

    private void PrintForm_CheckedListBox_Columns_ItemCheck(object? sender, ItemCheckEventArgs e)
    {
        if (_isUpdatingColumnSettings)
        {
            return;
        }

        BeginInvoke(new Action(() => ApplyColumnSelectionFromListBoxWithPendingState(e.Index, e.NewValue == CheckState.Checked)));
    }

    private void PrintForm_RadioButton_Color_CheckedChanged(object? sender, EventArgs e)
    {
        if (_isUpdatingOptionsSection || !PrintForm_RadioButton_Color.Checked)
        {
            return;
        }

        _printJob.ColorMode = Enum_PrintColorMode.Color;
        SchedulePreviewRefresh();
    }

    private void PrintForm_RadioButton_Grayscale_CheckedChanged(object? sender, EventArgs e)
    {
        if (_isUpdatingOptionsSection || !PrintForm_RadioButton_Grayscale.Checked)
        {
            return;
        }

        _printJob.ColorMode = Enum_PrintColorMode.Grayscale;
        SchedulePreviewRefresh();
    }

    private void PrintForm_ComboBox_Zoom_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (_isUpdatingZoomSelection)
        {
            return;
        }

        if (PrintForm_ComboBox_Zoom.SelectedItem is string selected)
        {
            _selectedZoomOption = selected;
            ApplyZoomSelection();
        }
    }

    private void PrintForm_CheckBox_AddNotesColumn_CheckedChanged(object? sender, EventArgs e)
    {
        if (_isUpdatingOptionsSection)
        {
            return;
        }

        _printJob.AddNotesColumn = PrintForm_CheckBox_AddNotesColumn.Checked;
        PrintForm_NumericUpDown_AddNotesColumn.Enabled = PrintForm_CheckBox_AddNotesColumn.Checked;

        if (PrintForm_CheckBox_AddNotesColumn.Checked && !_printJob.Landscape)
        {
            var result = Service_ErrorHandler.ShowConfirmation(
                "It is recommended to use landscape mode when adding the corrections column.\n\nWould you like to switch to landscape mode?",
                "Recommended Orientation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                _isUpdatingPrinterSection = true;
                try
                {
                    PrintForm_RadioButton_Landscape.Checked = true;
                    _printJob.Landscape = true;
                }
                finally
                {
                    _isUpdatingPrinterSection = false;
                }
            }
        }

        SchedulePreviewRefresh();
    }

    private void PrintForm_NumericUpDown_AddNotesColumn_ValueChanged(object? sender, EventArgs e)
    {
        if (_isUpdatingOptionsSection)
        {
            return;
        }

        _printJob.NotesColumnWidthPercentage = (int)PrintForm_NumericUpDown_AddNotesColumn.Value;
        SchedulePreviewRefresh();
    }

    private void PrintForm_PrintPreviewControl_StartPageChanged(object? sender, EventArgs e)
    {


        UpdatePreviewNavigationState();

        if (_printJob.PageRangeType == Enum_PrintRangeType.CurrentPage)
        {
            _printJob.FromPage = _printJob.CurrentPage;
            _printJob.ToPage = _printJob.CurrentPage;
            UpdateCustomRangeTextBoxValue(PrintForm_TextBox_FromPage, _printJob.FromPage);
            UpdateCustomRangeTextBoxValue(PrintForm_TextBox_ToPage, _printJob.ToPage);
        }
    }



    #endregion

    #region Helpers

    private void ApplyThemeColors()
    {
        Model_Shared_UserUiColors colors = Model_Application_Variables.UserUiColors;

        Color formBack = colors.FormBackColor ?? SystemColors.Control;
        Color formFore = colors.FormForeColor ?? SystemColors.ControlText;
        BackColor = formBack;
        ForeColor = formFore;

        Color panelBack = colors.PanelBackColor ?? SystemColors.Control;
        PrintForm_Panel_Main.BackColor = panelBack;
        PrintForm_TableLayout_Master.BackColor = panelBack;
        PrintForm_Panel_LeftSidebar.BackColor = panelBack;
        PrintForm_TableLayout_LeftSidebar.BackColor = panelBack;
        PrintForm_Panel_PrinterSettingsContent.BackColor = panelBack;
        PrintForm_Panel_PageSettingsContent.BackColor = panelBack;
        PrintForm_Panel_ColumnSettingsContent.BackColor = panelBack;
        PrintForm_Panel_OptionsContent.BackColor = panelBack;

        PrintForm_Panel_PreviewViewport.BackColor = Color.FromArgb(0x95, 0xA5, 0xA6); // ACCEPTABLE: Mockup-specified preview backdrop color

        Color labelFore = colors.LabelForeColor ?? SystemColors.ControlText;
        foreach (Label label in new[]
        {
                PrintForm_Label_PrinterSettingsHeader,
                PrintForm_Label_PrinterName,
                PrintForm_Label_PrinterOrientation,
                PrintForm_Label_PageSettingsHeader,
                PrintForm_Label_PageRange,
                PrintForm_Label_FromPage,
                PrintForm_Label_ToPage,
                PrintForm_Label_ColumnSettingsHeader,
                PrintForm_Label_Columns,
                PrintForm_Label_OptionsHeader,
                PrintForm_Label_ColorMode,
                PrintForm_Label_Zoom,
                PrintForm_Label_PageCounter,
                PrintForm_Label_AddNotesColumn
            })
        {
            label.ForeColor = labelFore;
        }

        Color radioBack = colors.RadioButtonBackColor ?? panelBack;
        Color radioFore = colors.RadioButtonForeColor ?? labelFore;
        PrintForm_RadioButton_Portrait.BackColor = radioBack;
        PrintForm_RadioButton_Portrait.ForeColor = radioFore;
        PrintForm_RadioButton_Landscape.BackColor = radioBack;
        PrintForm_RadioButton_Landscape.ForeColor = radioFore;
        PrintForm_RadioButton_AllPages.BackColor = radioBack;
        PrintForm_RadioButton_AllPages.ForeColor = radioFore;
        PrintForm_RadioButton_CurrentPage.BackColor = radioBack;
        PrintForm_RadioButton_CurrentPage.ForeColor = radioFore;
        PrintForm_RadioButton_CustomRange.BackColor = radioBack;
        PrintForm_RadioButton_CustomRange.ForeColor = radioFore;
        PrintForm_RadioButton_Color.BackColor = radioBack;
        PrintForm_RadioButton_Color.ForeColor = radioFore;
        PrintForm_RadioButton_Grayscale.BackColor = radioBack;
        PrintForm_RadioButton_Grayscale.ForeColor = radioFore;
        PrintForm_CheckBox_AddNotesColumn.BackColor = radioBack;
        PrintForm_CheckBox_AddNotesColumn.ForeColor = radioFore;

        Color textBoxBack = colors.TextBoxBackColor ?? SystemColors.Window;
        Color textBoxFore = colors.TextBoxForeColor ?? SystemColors.WindowText;
        foreach (TextBox textBox in new[] { PrintForm_TextBox_FromPage, PrintForm_TextBox_ToPage })
        {
            textBox.BackColor = textBoxBack;
            textBox.ForeColor = textBoxFore;
        }

        PrintForm_NumericUpDown_AddNotesColumn.BackColor = textBoxBack;
        PrintForm_NumericUpDown_AddNotesColumn.ForeColor = textBoxFore;

        Color comboBack = colors.ComboBoxBackColor ?? SystemColors.Window;
        Color comboFore = colors.ComboBoxForeColor ?? SystemColors.WindowText;
        PrintForm_ComboBox_Printer.BackColor = comboBack;
        PrintForm_ComboBox_Printer.ForeColor = comboFore;
        PrintForm_ComboBox_Zoom.BackColor = comboBack;
        PrintForm_ComboBox_Zoom.ForeColor = comboFore;

        Color listBack = colors.CheckedListBoxBackColor ?? SystemColors.Window;
        Color listFore = colors.CheckedListBoxForeColor ?? SystemColors.WindowText;
        PrintForm_CheckedListBox_Columns.BackColor = listBack;
        PrintForm_CheckedListBox_Columns.ForeColor = listFore;

        Color buttonBack = colors.ButtonBackColor ?? SystemColors.Control;
        Color buttonFore = colors.ButtonForeColor ?? SystemColors.ControlText;
        foreach (Button button in new[]
        {
                PrintForm_Button_PrinterSettingsToggle,
                PrintForm_Button_PageSettingsToggle,
                PrintForm_Button_ColumnSettingsToggle,
                PrintForm_Button_OptionsToggle,
                PrintForm_Button_ColumnUp,
                PrintForm_Button_ColumnDown,
                PrintForm_Button_FirstPage,
                PrintForm_Button_PreviousPage,
                PrintForm_Button_NextPage,
                PrintForm_Button_LastPage,
                PrintForm_Button_Export,
                PrintForm_Button_Cancel,
                PrintForm_Button_LeftPanelToggle,
                PrintForm_Button_RightPanelToggle
            })
        {
            button.BackColor = buttonBack;
            button.ForeColor = buttonFore;
            button.UseVisualStyleBackColor = false;
        }

        Color accent = colors.AccentColor ?? SystemColors.Highlight;
        PrintForm_Button_Print.BackColor = accent;
        PrintForm_Button_Print.ForeColor = colors.ButtonForeColor ?? SystemColors.HighlightText;
        PrintForm_Button_Print.UseVisualStyleBackColor = false;
    }

    private void LoadSettings()
    {
        var availableColumns = new List<string>(_printJob.ColumnOrder);

        if (_printSettings.ColumnOrder.Count > 0)
        {
            var mergedOrder = _printSettings.ColumnOrder.Where(availableColumns.Contains).ToList();
            foreach (string column in availableColumns)
            {
                if (!mergedOrder.Contains(column))
                {
                    mergedOrder.Add(column);
                }
            }

            if (mergedOrder.Count == availableColumns.Count)
            {
                _printJob.ColumnOrder = new List<string>(mergedOrder);
            }
        }

        if (_printSettings.VisibleColumns.Count > 0)
        {
            List<string> visible = _printSettings.VisibleColumns
                .Where(column => _printJob.ColumnOrder.Contains(column))
                .ToList();

            if (visible.Count > 0)
            {
                _printJob.VisibleColumns = new List<string>(visible);
            }
        }

        if (_printJob.VisibleColumns.Count == 0)
        {
            _printJob.VisibleColumns = new List<string>(_printJob.ColumnOrder);
        }

        if (!string.IsNullOrWhiteSpace(_printSettings.PrinterName))
        {
            _printJob.PrinterName = _printSettings.PrinterName;
        }
    }

    private void SaveSettings()
    {
        _printSettings.PrinterName = _printJob.PrinterName;
        _printSettings.VisibleColumns = new List<string>(_printJob.VisibleColumns);
        _printSettings.ColumnOrder = new List<string>(_printJob.ColumnOrder);
        _printSettings.LastModified = DateTime.UtcNow;

        try
        {
            _printSettings.Save();
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            // Failure to persist settings should not block printing; surface non-blocking diagnostic
            Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Low, controlName: nameof(PrintForm));
        }
    }

    private void InitializePrinterSettingsSection()
    {
        _isUpdatingPrinterSection = true;
        try
        {
            PrintForm_ComboBox_Printer.Items.Clear();
            var installedPrinters = PrinterSettings.InstalledPrinters.Cast<string>().ToList();

            if (installedPrinters.Count == 0)
            {
                PrintForm_ComboBox_Printer.Items.Add(PrinterUnavailableDisplayText);
                PrintForm_ComboBox_Printer.SelectedIndex = 0;
                PrintForm_ComboBox_Printer.Enabled = false;
                _printJob.PrinterName = null;
            }
            else
            {
                foreach (string printer in installedPrinters)
                {
                    PrintForm_ComboBox_Printer.Items.Add(printer);
                }

                string desired = _printJob.PrinterName ?? _printSettings.PrinterName ?? new PrinterSettings().PrinterName;
                string resolved = installedPrinters.FirstOrDefault(p => string.Equals(p, desired, StringComparison.OrdinalIgnoreCase))
                        ?? installedPrinters.First();

                _printJob.PrinterName = resolved;
                _printSettings.PrinterName = resolved;
                PrintForm_ComboBox_Printer.Enabled = true;
                PrintForm_ComboBox_Printer.SelectedItem = installedPrinters.First(p => string.Equals(p, resolved, StringComparison.OrdinalIgnoreCase));
            }

            PrintForm_RadioButton_Portrait.Checked = !_printJob.Landscape;
            PrintForm_RadioButton_Landscape.Checked = _printJob.Landscape;
        }
        finally
        {
            _isUpdatingPrinterSection = false;
        }

        UpdatePrinterSettingsCollapseState();
    }

    private void InitializePageSettingsSection()
    {
        _isUpdatingPageSettings = true;
        try
        {
            PrintForm_TextBox_FromPage.Text = Math.Max(1, _printJob.FromPage).ToString(CultureInfo.InvariantCulture);
            PrintForm_TextBox_ToPage.Text = Math.Max(_printJob.FromPage, _printJob.ToPage).ToString(CultureInfo.InvariantCulture);
        }
        finally
        {
            _isUpdatingPageSettings = false;
        }

        UpdatePageRangeControls();
        UpdatePageSettingsCollapseState();
    }

    private void InitializeColumnSettingsSection()
    {
        _isUpdatingColumnSettings = true;
        try
        {
            PrintForm_CheckedListBox_Columns.Items.Clear();
            foreach (string column in _printJob.ColumnOrder)
            {
                bool isChecked = _printJob.VisibleColumns.Contains(column);
                PrintForm_CheckedListBox_Columns.Items.Add(column, isChecked);
            }

            if (PrintForm_CheckedListBox_Columns.Items.Count > 0)
            {
                PrintForm_CheckedListBox_Columns.SelectedIndex = 0;
            }
        }
        finally
        {
            _isUpdatingColumnSettings = false;
        }

        UpdateColumnActionStates();
        UpdateColumnSettingsCollapseState();
    }

    private void InitializeOptionsSection()
    {
        _isUpdatingOptionsSection = true;
        try
        {
            PrintForm_RadioButton_Color.Checked = _printJob.ColorMode == Enum_PrintColorMode.Color;
            PrintForm_RadioButton_Grayscale.Checked = _printJob.ColorMode == Enum_PrintColorMode.Grayscale;
            PrintForm_CheckBox_AddNotesColumn.Checked = _printJob.AddNotesColumn;
            PrintForm_NumericUpDown_AddNotesColumn.Value = Math.Clamp(_printJob.NotesColumnWidthPercentage, 5, 75);
            PrintForm_NumericUpDown_AddNotesColumn.Enabled = _printJob.AddNotesColumn;

            _isUpdatingZoomSelection = true;
            try
            {
                _selectedZoomOption = ZoomFitToPage;
                string? comboItem = PrintForm_ComboBox_Zoom.Items.Cast<object>()
                    .OfType<string>()
                    .FirstOrDefault(item => item.Equals(ZoomFitToPage, StringComparison.OrdinalIgnoreCase));

                if (comboItem is null)
                {
                    PrintForm_ComboBox_Zoom.Items.Add(ZoomFitToPage);
                    comboItem = ZoomFitToPage;
                }

                PrintForm_ComboBox_Zoom.SelectedItem = comboItem;
            }
            finally
            {
                _isUpdatingZoomSelection = false;
            }
        }
        finally
        {
            _isUpdatingOptionsSection = false;
        }

        UpdateOptionsCollapseState();
    }

    private void InitializeActionButtons()
    {
        PrintForm_Button_Print.Enabled = false;
        PrintForm_Button_Export.Enabled = false;
        PrintForm_Button_Cancel.Enabled = true;
        UpdateActionButtonsState();
    }

    private void UpdatePrinterSettingsCollapseState()
    {
        PrintForm_Panel_PrinterSettingsContent.Visible = _isPrinterSettingsExpanded;
        PrintForm_Button_PrinterSettingsToggle.Text = _isPrinterSettingsExpanded ? "🡱 📋" : "🡳 📋";
    }

    private void UpdatePageSettingsCollapseState()
    {
        PrintForm_Panel_PageSettingsContent.Visible = _isPageSettingsExpanded;
        PrintForm_Button_PageSettingsToggle.Text = _isPageSettingsExpanded ? "🡱 📋" : "🡳 📋";
    }

    private void UpdateColumnSettingsCollapseState()
    {
        PrintForm_Panel_ColumnSettingsContent.Visible = _isColumnSettingsExpanded;
        PrintForm_Button_ColumnSettingsToggle.Text = _isColumnSettingsExpanded ? "🡱 📋" : "🡳 📋";
    }

    private void UpdateOptionsCollapseState()
    {
        PrintForm_Panel_OptionsContent.Visible = _isOptionsExpanded;
        PrintForm_Button_OptionsToggle.Text = _isOptionsExpanded ? "🡱 📋" : "🡳 📋";
    }

    private void UpdatePageRangeControls()
    {
        _isUpdatingPageSettings = true;
        try
        {
            PrintForm_RadioButton_AllPages.Checked = _printJob.PageRangeType == Enum_PrintRangeType.AllPages;
            PrintForm_RadioButton_CurrentPage.Checked = _printJob.PageRangeType == Enum_PrintRangeType.CurrentPage;
            PrintForm_RadioButton_CustomRange.Checked = _printJob.PageRangeType == Enum_PrintRangeType.PageRange;

            bool enableCustom = _printJob.PageRangeType == Enum_PrintRangeType.PageRange;
            PrintForm_TextBox_FromPage.Enabled = enableCustom;
            PrintForm_TextBox_ToPage.Enabled = enableCustom;

            if (_printJob.PageRangeType == Enum_PrintRangeType.AllPages)
            {
                _printJob.FromPage = 1;
                _printJob.ToPage = Math.Max(1, _previewTotalPages);
            }
            else if (_printJob.PageRangeType == Enum_PrintRangeType.CurrentPage)
            {
                int currentPage = Math.Clamp(_printJob.CurrentPage, 1, Math.Max(1, _previewTotalPages));
                _printJob.FromPage = currentPage;
                _printJob.ToPage = currentPage;
            }

            PrintForm_TextBox_FromPage.Text = _printJob.FromPage.ToString(CultureInfo.InvariantCulture);
            PrintForm_TextBox_ToPage.Text = _printJob.ToPage.ToString(CultureInfo.InvariantCulture);
        }
        finally
        {
            _isUpdatingPageSettings = false;
        }

        ValidateCustomRangeInputs();
    }

    private void ValidateCustomRangeInputs()
    {
        if (_printJob.PageRangeType != Enum_PrintRangeType.PageRange)
        {
            _isCustomRangeValid = true;
            UpdateCustomRangeFieldVisualState(true);
            UpdateActionButtonsState();
            return;
        }

        bool fromValid = TryParsePositiveInt(PrintForm_TextBox_FromPage.Text, out int fromValue);
        bool toValid = TryParsePositiveInt(PrintForm_TextBox_ToPage.Text, out int toValue);

        int maxPage = Math.Max(1, _previewTotalPages > 0 ? _previewTotalPages : _printJob.TotalPages);
        if (fromValid)
        {
            fromValue = Math.Clamp(fromValue, 1, maxPage);
        }

        if (toValid)
        {
            toValue = Math.Clamp(toValue, 1, maxPage);
        }

        bool isValid = fromValid && toValid && fromValue <= toValue;
        _isCustomRangeValid = isValid;

        if (isValid)
        {
            _printJob.FromPage = fromValue;
            _printJob.ToPage = toValue;
            UpdateCustomRangeTextBoxValue(PrintForm_TextBox_FromPage, fromValue);
            UpdateCustomRangeTextBoxValue(PrintForm_TextBox_ToPage, toValue);
        }

        UpdateCustomRangeFieldVisualState(isValid);
        UpdateActionButtonsState();
    }

    private void UpdateCustomRangeTextBoxValue(TextBox textBox, int value)
    {
        string valueText = value.ToString(CultureInfo.InvariantCulture);
        if (!string.Equals(textBox.Text, valueText, StringComparison.Ordinal))
        {
            bool previousUpdatingState = _isUpdatingPageSettings;
            _isUpdatingPageSettings = true;
            textBox.Text = valueText;
            _isUpdatingPageSettings = previousUpdatingState;
        }
    }

    private void UpdateCustomRangeFieldVisualState(bool isValid)
    {
        Color validColor = Model_Application_Variables.UserUiColors.TextBoxForeColor ?? SystemColors.WindowText;
        Color invalidColor = Model_Application_Variables.UserUiColors.TextBoxErrorForeColor ?? Color.Red;
        Color target = isValid ? validColor : invalidColor;
        PrintForm_TextBox_FromPage.ForeColor = target;
        PrintForm_TextBox_ToPage.ForeColor = target;
    }

    private static bool TryParsePositiveInt(string? value, out int result)
    {
        bool parsed = int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out result);
        return parsed && result > 0;
    }

    private void UpdateColumnActionStates()
    {
        int selectedIndex = PrintForm_CheckedListBox_Columns.SelectedIndex;
        bool hasSelection = selectedIndex >= 0;

        PrintForm_Button_ColumnUp.Enabled = hasSelection && selectedIndex > 0;
        PrintForm_Button_ColumnDown.Enabled = hasSelection && selectedIndex >= 0 && selectedIndex < PrintForm_CheckedListBox_Columns.Items.Count - 1;
        UpdateActionButtonsState();
    }

    private void SetBusyState(bool isBusy, string? statusText = null)
    {
        if (InvokeRequired)
        {
            BeginInvoke(new Action(() => SetBusyState(isBusy, statusText)));
            return;
        }

        if (isBusy)
        {
            _windowTitleSnapshot ??= Text;
            _exportButtonTextSnapshot ??= PrintForm_Button_Export.Text;

            string suffix = string.IsNullOrWhiteSpace(statusText) ? "Working..." : statusText!;
            Text = string.IsNullOrWhiteSpace(_windowTitleSnapshot)
                ? suffix
                : $"{_windowTitleSnapshot} – {suffix}";

            UseWaitCursor = true;
            Cursor = Cursors.WaitCursor;

            PrintForm_Button_Print.Enabled = false;
            PrintForm_Button_Export.Enabled = false;
            PrintForm_Button_Cancel.Enabled = false;
            PrintForm_ContextMenu_Export.Enabled = false;

            PrintForm_Button_Export.Text = "Exporting...";
            PrintForm_Button_Export.Refresh();
        }
        else
        {
            if (!string.IsNullOrWhiteSpace(_windowTitleSnapshot))
            {
                Text = _windowTitleSnapshot!;
            }

            UseWaitCursor = false;
            Cursor = Cursors.Default;

            PrintForm_Button_Cancel.Enabled = true;
            PrintForm_ContextMenu_Export.Enabled = true;

            if (!string.IsNullOrWhiteSpace(_exportButtonTextSnapshot))
            {
                PrintForm_Button_Export.Text = _exportButtonTextSnapshot!;
            }

            UpdateActionButtonsState();
        }
    }

    private async Task ExecuteExportAsync(
        Func<Model_Print_Job, string, CancellationToken, Task<Model_Dao_Result>> exportFunc,
        string fileFilter,
        string defaultExtension,
        string dialogTitle)
    {
        ArgumentNullException.ThrowIfNull(exportFunc);

        if (_previewDocument is null)
        {
            Service_ErrorHandler.HandleValidationError("Generate a preview before exporting.", nameof(PrintForm));
            return;
        }

        ValidateCustomRangeInputs();

        if (_printJob.PageRangeType == Enum_PrintRangeType.PageRange && !_isCustomRangeValid)
        {
            Service_ErrorHandler.HandleValidationError("Fix the page range before exporting.", nameof(PrintForm));
            return;
        }

        if (_printJob.VisibleColumns.Count == 0)
        {
            Service_ErrorHandler.HandleValidationError("Select at least one column before exporting.", nameof(PrintForm));
            return;
        }

        using SaveFileDialog saveFileDialog = CreateExportSaveFileDialog(fileFilter, defaultExtension, dialogTitle);
        if (saveFileDialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        try
        {
            SetBusyState(true, "Preparing export...");

            using var cancellationSource = new CancellationTokenSource();
            Model_Dao_Result exportResult = await exportFunc(_printJob, saveFileDialog.FileName, cancellationSource.Token);

            if (exportResult.IsSuccess)
            {
                SaveSettings();
                string message = string.IsNullOrWhiteSpace(exportResult.StatusMessage)
                    ? $"Export completed successfully.{Environment.NewLine}{Environment.NewLine}Location: {saveFileDialog.FileName}"
                    : exportResult.StatusMessage;
                Service_ErrorHandler.ShowInformation(message, "Export Completed", controlName: nameof(PrintForm));
            }
            else
            {
                HandleExportFailure(exportResult, saveFileDialog.FileName);
            }
        }
        catch (OperationCanceledException)
        {
            Service_ErrorHandler.ShowInformation("Export was cancelled.", "Export Cancelled", controlName: nameof(PrintForm));
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, controlName: nameof(PrintForm));
        }
        finally
        {
            SetBusyState(false);
        }
    }

    private void HandleExportFailure(Model_Dao_Result exportResult, string destinationPath)
    {
        string message = string.IsNullOrWhiteSpace(exportResult.ErrorMessage)
            ? $"The export operation failed for {destinationPath}."
            : exportResult.ErrorMessage;

        if (exportResult.Exception is not null)
        {
            var context = new Dictionary<string, object>
            {
                ["DestinationPath"] = destinationPath
            };
            Service_ErrorHandler.HandleException(exportResult.Exception, Enum_ErrorSeverity.Medium, contextData: context, controlName: nameof(PrintForm));
            return;
        }

        Service_ErrorHandler.HandleValidationError(message, nameof(PrintForm));
    }

    private SaveFileDialog CreateExportSaveFileDialog(string fileFilter, string defaultExtension, string dialogTitle)
    {
        string normalizedExtension = NormalizeExtension(defaultExtension);
        return new SaveFileDialog
        {
            Title = dialogTitle,
            Filter = fileFilter,
            DefaultExt = normalizedExtension.TrimStart('.'),
            AddExtension = true,
            FileName = BuildDefaultExportFileName(_printJob.Title, normalizedExtension),
            InitialDirectory = GetDefaultExportDirectory(),
            RestoreDirectory = true,
            OverwritePrompt = true
        };
    }

    private static string BuildDefaultExportFileName(string? title, string extension)
    {
        string safeTitle = string.IsNullOrWhiteSpace(title) ? "MTM-Export" : title;
        char[] invalidChars = Path.GetInvalidFileNameChars();
        safeTitle = new string(safeTitle.Select(ch => invalidChars.Contains(ch) ? '_' : ch).ToArray());
        string timestamp = DateTime.Now.ToString("yyyyMMdd-HHmmss", CultureInfo.InvariantCulture);
        return $"{safeTitle}_{timestamp}{extension}";
    }

    private static string GetDefaultExportDirectory()
    {
        try
        {
            string documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (!string.IsNullOrWhiteSpace(documents) && Directory.Exists(documents))
            {
                return documents;
            }
        }
        catch
        {
            // Fallback handled below if we cannot resolve documents directory.
        }

        return Environment.CurrentDirectory;
    }

    private static string NormalizeExtension(string extension)
    {
        if (string.IsNullOrWhiteSpace(extension))
        {
            return ".dat";
        }

        return extension.StartsWith(".", StringComparison.Ordinal) ? extension : "." + extension;
    }

    private void ApplyColumnSelectionFromListBoxWithPendingState(int index, bool shouldCheck)
    {
        if (index < 0 || index >= PrintForm_CheckedListBox_Columns.Items.Count)
        {
            return;
        }

        if (!shouldCheck && PrintForm_CheckedListBox_Columns.GetItemChecked(index) && PrintForm_CheckedListBox_Columns.CheckedItems.Count <= 1)
        {
            _isUpdatingColumnSettings = true;
            try
            {
                PrintForm_CheckedListBox_Columns.SetItemChecked(index, true);
            }
            finally
            {
                _isUpdatingColumnSettings = false;
            }

            Service_ErrorHandler.HandleValidationError("At least one column must remain visible.", nameof(PrintForm));
            UpdateActionButtonsState();
            return;
        }

        ApplyColumnSelectionFromListBox();
    }

    private void ApplyColumnSelectionFromListBox()
    {
        List<string> orderedColumns = PrintForm_CheckedListBox_Columns.Items.Cast<object>()
            .OfType<string>()
            .ToList();
        List<string> visibleColumns = PrintForm_CheckedListBox_Columns.CheckedItems.Cast<object>()
            .OfType<string>()
            .ToList();

        _printJob.ColumnOrder = new List<string>(orderedColumns);
        _printJob.VisibleColumns = new List<string>(visibleColumns);

        _printSettings.ColumnOrder = new List<string>(orderedColumns);
        _printSettings.VisibleColumns = new List<string>(visibleColumns);
        _printSettings.LastModified = DateTime.UtcNow;

        UpdateColumnActionStates();
        SchedulePreviewRefresh();
    }

    private void MoveColumnItem(int direction)
    {
        if (direction == 0 || PrintForm_CheckedListBox_Columns.SelectedIndex < 0)
        {
            return;
        }

        int currentIndex = PrintForm_CheckedListBox_Columns.SelectedIndex;
        int newIndex = currentIndex + direction;
        if (newIndex < 0 || newIndex >= PrintForm_CheckedListBox_Columns.Items.Count)
        {
            return;
        }

        _isUpdatingColumnSettings = true;
        try
        {
            object item = PrintForm_CheckedListBox_Columns.Items[currentIndex]!;
            bool isChecked = PrintForm_CheckedListBox_Columns.GetItemChecked(currentIndex);

            PrintForm_CheckedListBox_Columns.Items.RemoveAt(currentIndex);
            PrintForm_CheckedListBox_Columns.Items.Insert(newIndex, item);
            PrintForm_CheckedListBox_Columns.SetItemChecked(newIndex, isChecked);
            PrintForm_CheckedListBox_Columns.SelectedIndex = newIndex;
        }
        finally
        {
            _isUpdatingColumnSettings = false;
        }

        ApplyColumnSelectionFromListBox();
    }

    private void ApplyZoomSelection()
    {
        if (PrintForm_PrintPreviewControl.Document is null)
        {
            return;
        }

        if (_selectedZoomOption.Equals(ZoomFitToPage, StringComparison.OrdinalIgnoreCase))
        {
            PrintForm_PrintPreviewControl.AutoZoom = true;
            return;
        }

        PrintForm_PrintPreviewControl.AutoZoom = false;

        if (_zoomLevelLookup.TryGetValue(_selectedZoomOption, out double zoomValue))
        {
            PrintForm_PrintPreviewControl.Zoom = Math.Clamp(zoomValue, 0.1, 5.0);
        }
    }

    private void UpdateActionButtonsState()
    {
        bool hasPreview = _previewDocument is not null && _previewTotalPages > 0 && _printJob.VisibleColumns.Count > 0;
        bool rangeValid = _printJob.PageRangeType != Enum_PrintRangeType.PageRange || _isCustomRangeValid;
        bool baseState = hasPreview && rangeValid && !_isGeneratingPreview;

        bool canPrint = baseState && _isPrinterOnline;
        bool canExport = baseState;

        if (PrintForm_Button_Print.Enabled != canPrint || PrintForm_Button_Export.Enabled != canExport)
        {
            LoggingUtility.Log($"[PrintForm] Updating action buttons. Print: {canPrint}, Export: {canExport}. State: Online={_isPrinterOnline}, HasPreview={hasPreview}, RangeValid={rangeValid}, IsGenerating={_isGeneratingPreview}");
            PrintForm_Button_Print.Enabled = canPrint;
            PrintForm_Button_Export.Enabled = canExport;
        }
    }

    #endregion

    #region Helpers

    private Button? PrintForm_Button_Help;

    private void InitializeHelpButton()
    {
        PrintForm_Button_Help = new Button();
        PrintForm_Button_Help.Name = "PrintForm_Button_Help";
        PrintForm_Button_Help.Text = "?";
        PrintForm_Button_Help.Size = new Size(24, 24);
        PrintForm_Button_Help.Location = new Point(PrintForm_Panel_Main.Width - 30, 10); 
        PrintForm_Button_Help.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        PrintForm_Button_Help.Click += (s, e) => 
        {
            HelpViewerForm.GetInstance().BringToFrontAndNavigate("print-operations", "print-overview");
        };
        
        PrintForm_Panel_Main.Controls.Add(PrintForm_Button_Help);
        PrintForm_Button_Help.BringToFront();
    }

    #endregion

    #region Cleanup

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        base.OnFormClosed(e);
        _previewDocument?.Dispose();
        _previewDocument = null;
        _previewPageInfos = Array.Empty<PreviewPageInfo>();
        _printManager?.Dispose();
        _printManager = null;
    }

    #endregion

    private void PrintForm_TableLayout_LeftSidebar_Paint(object sender, PaintEventArgs e)
    {

    }
}
