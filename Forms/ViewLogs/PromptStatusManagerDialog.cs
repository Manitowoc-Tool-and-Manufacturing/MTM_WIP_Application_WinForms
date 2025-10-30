using MTM_Inventory_Application.Core;
using MTM_Inventory_Application.Logging;
using MTM_Inventory_Application.Models;
using MTM_Inventory_Application.Services;
using System.ComponentModel;

namespace MTM_Inventory_Application.Forms.ViewLogs;

/// <summary>
/// Modal dialog for managing prompt fix statuses.
/// Allows viewing and editing status, assignee, and notes for all prompts.
/// Implements T065 - Create Developer UI for status management.
/// </summary>
public partial class PromptStatusManagerDialog : Form
{
    #region Fields

    private List<Model_PromptStatus> _statuses = new();
    private List<Model_PromptStatus> _modifiedStatuses = new();
    private bool _hasUnsavedChanges = false;

    #endregion

    #region Properties

    /// <summary>
    /// Gets whether there are unsaved changes.
    /// </summary>
    public bool HasUnsavedChanges => _hasUnsavedChanges;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the PromptStatusManagerDialog.
    /// </summary>
    public PromptStatusManagerDialog()
    {
        InitializeComponent();

        // Theme system integration - CRITICAL PATTERN from theme-system.instructions.md
        Core_Themes.ApplyDpiScaling(this);              // Step 1: DPI scaling
        Core_Themes.ApplyRuntimeLayoutAdjustments(this); // Step 2: Layout adjustments
        Core_Themes.ApplyFocusHighlighting(this);        // Step 3: Focus highlighting

        InitializeDataGridView();
        WireUpEvents();

        LoggingUtility.Log("[PromptStatusManagerDialog] Dialog initialized");
    }

    #endregion

    #region Initialization

    /// <summary>
    /// Initializes the DataGridView with columns and styling.
    /// </summary>
    private void InitializeDataGridView()
    {
        dgvPromptStatuses.AutoGenerateColumns = false;
        dgvPromptStatuses.AllowUserToAddRows = false;
        dgvPromptStatuses.AllowUserToDeleteRows = false;
        dgvPromptStatuses.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvPromptStatuses.MultiSelect = false;
        dgvPromptStatuses.ReadOnly = false;
        dgvPromptStatuses.RowHeadersVisible = false;

        // Method Name column (readonly)
        dgvPromptStatuses.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colMethodName",
            HeaderText = "Method Name",
            DataPropertyName = "MethodName",
            ReadOnly = true,
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
            FillWeight = 30
        });

        // Status column (ComboBox)
        var statusColumn = new DataGridViewComboBoxColumn
        {
            Name = "colStatus",
            HeaderText = "Status",
            DataPropertyName = "Status",
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
            FillWeight = 15
        };
        statusColumn.Items.AddRange(new object[]
        {
            PromptStatusEnum.New,
            PromptStatusEnum.InProgress,
            PromptStatusEnum.Fixed,
            PromptStatusEnum.WontFix
        });
        dgvPromptStatuses.Columns.Add(statusColumn);

        // Assignee column (editable TextBox)
        dgvPromptStatuses.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colAssignee",
            HeaderText = "Assignee",
            DataPropertyName = "Assignee",
            ReadOnly = false,
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
            FillWeight = 15
        });

        // Notes column (editable TextBox)
        dgvPromptStatuses.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colNotes",
            HeaderText = "Notes",
            DataPropertyName = "Notes",
            ReadOnly = false,
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
            FillWeight = 25
        });

        // Created Date column (readonly)
        dgvPromptStatuses.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colCreatedDate",
            HeaderText = "Created Date",
            DataPropertyName = "CreatedDate",
            ReadOnly = true,
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
            FillWeight = 15,
            DefaultCellStyle = new DataGridViewCellStyle
            {
                Format = "yyyy-MM-dd HH:mm"
            }
        });

        // Last Updated column (readonly)
        dgvPromptStatuses.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colLastUpdated",
            HeaderText = "Last Updated",
            DataPropertyName = "LastUpdated",
            ReadOnly = true,
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
            FillWeight = 15,
            DefaultCellStyle = new DataGridViewCellStyle
            {
                Format = "yyyy-MM-dd HH:mm"
            }
        });

        LoggingUtility.Log("[PromptStatusManagerDialog] DataGridView columns initialized");
    }

    /// <summary>
    /// Wires up event handlers.
    /// </summary>
    private void WireUpEvents()
    {
        Load += PromptStatusManagerDialog_Load;
        FormClosing += PromptStatusManagerDialog_FormClosing;
        dgvPromptStatuses.CellValueChanged += DgvPromptStatuses_CellValueChanged;
        dgvPromptStatuses.CurrentCellDirtyStateChanged += DgvPromptStatuses_CurrentCellDirtyStateChanged;
        dgvPromptStatuses.CellFormatting += DgvPromptStatuses_CellFormatting;
        btnRefresh.Click += BtnRefresh_Click;
        btnSave.Click += BtnSave_Click;
        btnClose.Click += BtnClose_Click;

        LoggingUtility.Log("[PromptStatusManagerDialog] Event handlers wired up");
    }

    #endregion

    #region Event Handlers

    /// <summary>
    /// Handles form load event. Applies theme and loads initial data.
    /// </summary>
    private async void PromptStatusManagerDialog_Load(object? sender, EventArgs e)
    {
        try
        {
            // Apply theme colors - CRITICAL PATTERN from theme-system.instructions.md
            // Forms apply theme colors in Load event (not constructor)
            Core_Themes.ApplyTheme(this);

            await LoadPromptStatusesAsync();
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            MessageBox.Show(
                $"Error loading prompt statuses: {ex.Message}",
                "Load Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// Handles form closing event. Prompts to save if there are unsaved changes.
    /// </summary>
    private void PromptStatusManagerDialog_FormClosing(object? sender, FormClosingEventArgs e)
    {
        if (_hasUnsavedChanges)
        {
            var result = MessageBox.Show(
                "You have unsaved changes. Save before closing?",
                "Unsaved Changes",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                SaveChanges();
            }
            else if (result == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }
    }

    /// <summary>
    /// Handles cell value changes. Marks status as modified and tracks changes.
    /// </summary>
    private void DgvPromptStatuses_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0 || e.RowIndex >= dgvPromptStatuses.Rows.Count)
            return;

        var status = dgvPromptStatuses.Rows[e.RowIndex].DataBoundItem as Model_PromptStatus;
        if (status == null)
            return;

        // Mark as modified
        if (!_modifiedStatuses.Contains(status))
        {
            _modifiedStatuses.Add(status);
        }

        status.LastUpdated = DateTime.Now;
        _hasUnsavedChanges = true;

        // Update status label
        lblStatus.Text = $"Modified: {_modifiedStatuses.Count} item(s)";
        btnSave.Enabled = true;

        LoggingUtility.Log($"[PromptStatusManagerDialog] Status modified: {status.MethodName}");
    }

    /// <summary>
    /// Handles ComboBox cell dirty state changes to commit values immediately.
    /// </summary>
    private void DgvPromptStatuses_CurrentCellDirtyStateChanged(object? sender, EventArgs e)
    {
        if (dgvPromptStatuses.CurrentCell is DataGridViewComboBoxCell)
        {
            dgvPromptStatuses.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }
    }

    /// <summary>
    /// Handles cell formatting to apply color coding based on status.
    /// Implements T065 acceptance criteria - color coding visible.
    /// </summary>
    private void DgvPromptStatuses_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex < 0 || e.RowIndex >= dgvPromptStatuses.Rows.Count)
            return;

        var status = dgvPromptStatuses.Rows[e.RowIndex].DataBoundItem as Model_PromptStatus;
        if (status == null)
            return;

        // Apply color coding to Status column
        if (dgvPromptStatuses.Columns[e.ColumnIndex].Name == "colStatus")
        {
            switch (status.Status)
            {
                case PromptStatusEnum.New:
                    if (e.CellStyle != null)
                    {
                        e.CellStyle.BackColor = Color.LightBlue;
                        e.CellStyle.ForeColor = Color.Black;
                    }
                    break;
                case PromptStatusEnum.InProgress:
                    if (e.CellStyle != null)
                    {
                        e.CellStyle.BackColor = Color.LightYellow;
                        e.CellStyle.ForeColor = Color.Black;
                    }
                    break;
                case PromptStatusEnum.Fixed:
                    if (e.CellStyle != null)
                    {
                        e.CellStyle.BackColor = Color.LightGreen;
                        e.CellStyle.ForeColor = Color.Black;
                    }
                    break;
                case PromptStatusEnum.WontFix:
                    if (e.CellStyle != null)
                    {
                        e.CellStyle.BackColor = Color.LightGray;
                        e.CellStyle.ForeColor = Color.DarkGray;
                    }
                    break;
            }
        }
    }

    /// <summary>
    /// Handles Refresh button click. Reloads data from disk.
    /// </summary>
    private async void BtnRefresh_Click(object? sender, EventArgs e)
    {
        if (_hasUnsavedChanges)
        {
            var result = MessageBox.Show(
                "You have unsaved changes. Refreshing will discard them. Continue?",
                "Unsaved Changes",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes)
                return;
        }

        await LoadPromptStatusesAsync();
    }

    /// <summary>
    /// Handles Save button click. Persists all changes to disk.
    /// </summary>
    private void BtnSave_Click(object? sender, EventArgs e)
    {
        SaveChanges();
    }

    /// <summary>
    /// Handles Close button click. Closes the dialog.
    /// </summary>
    private void BtnClose_Click(object? sender, EventArgs e)
    {
        Close();
    }

    #endregion

    #region Data Operations

    /// <summary>
    /// Loads all prompt statuses from Service_PromptStatusManager.
    /// </summary>
    private async Task LoadPromptStatusesAsync()
    {
        try
        {
            lblStatus.Text = "Loading statuses...";
            dgvPromptStatuses.DataSource = null;
            _modifiedStatuses.Clear();
            _hasUnsavedChanges = false;
            btnSave.Enabled = false;

            // Load from service (runs synchronously but wrapped in Task for consistency)
            await Task.Run(() =>
            {
                _statuses = Service_PromptStatusManager.GetAllStatuses();
            });

            // Bind to DataGridView
            var bindingList = new BindingList<Model_PromptStatus>(_statuses);
            dgvPromptStatuses.DataSource = bindingList;

            lblStatus.Text = $"Loaded {_statuses.Count} prompt status(es)";

            LoggingUtility.Log($"[PromptStatusManagerDialog] Loaded {_statuses.Count} prompt statuses");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            lblStatus.Text = "Error loading statuses";
            MessageBox.Show(
                $"Error loading prompt statuses: {ex.Message}",
                "Load Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// Saves all modified statuses to disk via Service_PromptStatusManager.
    /// </summary>
    private void SaveChanges()
    {
        try
        {
            lblStatus.Text = "Saving changes...";

            // Save all statuses (service handles persistence)
            bool success = Service_PromptStatusManager.SaveStatus(_statuses);

            if (success)
            {
                _modifiedStatuses.Clear();
                _hasUnsavedChanges = false;
                btnSave.Enabled = false;
                lblStatus.Text = $"Saved successfully at {DateTime.Now:HH:mm:ss}";

                LoggingUtility.Log($"[PromptStatusManagerDialog] Saved {_statuses.Count} prompt statuses");

                MessageBox.Show(
                    "Changes saved successfully.",
                    "Save Complete",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                lblStatus.Text = "Save failed";
                MessageBox.Show(
                    "Failed to save changes. Check logs for details.",
                    "Save Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            lblStatus.Text = "Error saving changes";
            MessageBox.Show(
                $"Error saving changes: {ex.Message}",
                "Save Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    #endregion

    #region Cleanup

    /// <summary>
    /// Releases resources used by the form.
    /// </summary>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            components?.Dispose();
        }
        base.Dispose(disposing);
    }

    #endregion
}
