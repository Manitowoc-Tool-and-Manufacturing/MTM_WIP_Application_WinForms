using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MTM_Inventory_Application.Core;
using MTM_Inventory_Application.Data;
using MTM_Inventory_Application.Logging;
using MTM_Inventory_Application.Models;
using MTM_Inventory_Application.Services;

namespace MTM_Inventory_Application.Controls.SettingsForm
{
    /// <summary>
    /// Developer control for managing parameter prefix overrides.
    /// Provides CRUD interface for sys_parameter_prefix_overrides table with real-time cache refresh.
    /// </summary>
    public partial class Control_Developer_ParameterPrefixMaintenance : UserControl
    {
        #region Fields

        private List<Model_ParameterPrefixOverride> _overrides = new();
        private Model_ParameterPrefixOverride? _selectedOverride;

        #endregion

        #region Events

        /// <summary>
        /// Event fired when status message should be displayed.
        /// Subscribe in parent SettingsForm to update status label.
        /// </summary>
        public event EventHandler<string>? StatusMessageChanged;

        /// <summary>
        /// Event fired when overrides are modified (add/edit/delete).
        /// Subscribe in parent to trigger application-wide reload.
        /// </summary>
        public event EventHandler? OverridesModified;

        #endregion

        #region Constructors

        public Control_Developer_ParameterPrefixMaintenance()
        {
            InitializeComponent();
            Core_Themes.ApplyDpiScaling(this);
            Core_Themes.ApplyRuntimeLayoutAdjustments(this);
            ConfigureDataGridView();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Loads all active parameter prefix overrides from database.
        /// Called by SettingsForm when control becomes visible.
        /// </summary>
        public async Task ReloadAsync()
        {
            try
            {
                StatusMessageChanged?.Invoke(this, "Loading parameter prefix overrides...");
                
                var result = await Dao_ParameterPrefixOverrides.GetAllActiveAsync();
                
                if (!result.IsSuccess)
                {
                    StatusMessageChanged?.Invoke(this, $"Error loading overrides: {result.ErrorMessage}");
                    if (result.Exception != null)
                    {
                        LoggingUtility.LogApplicationError(result.Exception);
                    }
                    return;
                }

                _overrides = result.Data ?? new List<Model_ParameterPrefixOverride>();
                
                PopulateDataGridView();
                
                StatusMessageChanged?.Invoke(this, $"Loaded {_overrides.Count} override(s)");
            }
            catch (Exception ex)
            {
                StatusMessageChanged?.Invoke(this, $"Unexpected error: {ex.Message}");
                LoggingUtility.LogApplicationError(ex);
            }
        }

        /// <summary>
        /// Clears displayed data when control is hidden.
        /// Called by SettingsForm when navigating away.
        /// </summary>
        public Task ClearAsync()
        {
            _overrides.Clear();
            dgvOverrides.Rows.Clear();
            ClearDetails();
            return Task.CompletedTask;
        }

        #endregion

        #region Private Methods - Data Management

        private void ConfigureDataGridView()
        {
            dgvOverrides.AutoGenerateColumns = false;
            dgvOverrides.Columns.Clear();

            // OverrideId (hidden, used for operations)
            dgvOverrides.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "OverrideId",
                DataPropertyName = "OverrideId",
                Visible = false
            });

            // ProcedureName
            dgvOverrides.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ProcedureName",
                HeaderText = "Procedure Name",
                DataPropertyName = "ProcedureName",
                Width = 200,
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            // ParameterName
            dgvOverrides.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ParameterName",
                HeaderText = "Parameter Name",
                DataPropertyName = "ParameterName",
                Width = 150,
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            // OverridePrefix
            dgvOverrides.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "OverridePrefix",
                HeaderText = "Override Prefix",
                DataPropertyName = "OverridePrefix",
                Width = 100
            });

            // Reason
            dgvOverrides.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Reason",
                HeaderText = "Reason",
                DataPropertyName = "Reason",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                MinimumWidth = 200
            });

            // CreatedBy
            dgvOverrides.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CreatedBy",
                HeaderText = "Created By",
                DataPropertyName = "CreatedBy",
                Width = 100
            });

            // CreatedDate
            dgvOverrides.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CreatedDate",
                HeaderText = "Created Date",
                DataPropertyName = "CreatedDate",
                Width = 150,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "yyyy-MM-dd HH:mm" }
            });
        }

        private void PopulateDataGridView()
        {
            dgvOverrides.DataSource = null;
            dgvOverrides.DataSource = _overrides;
            
            if (_overrides.Count > 0)
            {
                dgvOverrides.Rows[0].Selected = true;
            }
        }

        private void DisplayOverrideDetails(Model_ParameterPrefixOverride? @override)
        {
            if (@override == null)
            {
                ClearDetails();
                return;
            }

            txtProcedureName.Text = @override.ProcedureName;
            txtParameterName.Text = @override.ParameterName;
            txtOverridePrefix.Text = string.IsNullOrEmpty(@override.OverridePrefix) 
                ? "(no prefix)" 
                : @override.OverridePrefix;
            txtReason.Text = @override.Reason ?? string.Empty;

            btnEdit.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void ClearDetails()
        {
            txtProcedureName.Clear();
            txtParameterName.Clear();
            txtOverridePrefix.Clear();
            txtReason.Clear();
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            _selectedOverride = null;
        }

        #endregion

        #region Event Handlers

        private void DgvOverrides_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvOverrides.CurrentRow?.DataBoundItem is Model_ParameterPrefixOverride selected)
            {
                _selectedOverride = selected;
                DisplayOverrideDetails(selected);
            }
            else
            {
                ClearDetails();
            }
        }

        private async void BtnAdd_Click(object? sender, EventArgs e)
        {
            try
            {
                using var dialog = new Forms.Settings.Dialog_AddParameterOverride();
                
                if (dialog.ShowDialog(this.FindForm()) == DialogResult.OK && dialog.Result != null)
                {
                    StatusMessageChanged?.Invoke(this, "Override added successfully");
                    OverridesModified?.Invoke(this, EventArgs.Empty);
                    await ReloadAsync();
                }
            }
            catch (Exception ex)
            {
                StatusMessageChanged?.Invoke(this, $"Error: {ex.Message}");
                LoggingUtility.LogApplicationError(ex);
            }
        }

        private async void BtnEdit_Click(object? sender, EventArgs e)
        {
            if (_selectedOverride == null) return;

            try
            {
                using var dialog = new Forms.Settings.Dialog_EditParameterOverride(_selectedOverride);
                
                if (dialog.ShowDialog(this.FindForm()) == DialogResult.OK && dialog.Result != null)
                {
                    StatusMessageChanged?.Invoke(this, "Override updated successfully");
                    OverridesModified?.Invoke(this, EventArgs.Empty);
                    await ReloadAsync();
                }
            }
            catch (Exception ex)
            {
                StatusMessageChanged?.Invoke(this, $"Error: {ex.Message}");
                LoggingUtility.LogApplicationError(ex);
            }
        }

        private async void BtnDelete_Click(object? sender, EventArgs e)
        {
            if (_selectedOverride == null) return;

            // T026 - Delete Override Confirmation
            var confirmResult = MessageBox.Show(
                $"Are you sure you want to delete the override for:\n\n" +
                $"Procedure: {_selectedOverride.ProcedureName}\n" +
                $"Parameter: {_selectedOverride.ParameterName}\n" +
                $"Prefix: {(string.IsNullOrEmpty(_selectedOverride.OverridePrefix) ? "(none)" : _selectedOverride.OverridePrefix)}\n\n" +
                "This will restore default prefix detection for this parameter.\n\n" +
                "This action cannot be undone.",
                "Confirm Delete Override",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);

            if (confirmResult != DialogResult.Yes) return;

            try
            {
                StatusMessageChanged?.Invoke(this, "Deleting override...");

                var result = await Dao_ParameterPrefixOverrides.DeleteAsync(
                    _selectedOverride.OverrideId, 
                    Model_AppVariables.User);

                if (result.IsSuccess)
                {
                    StatusMessageChanged?.Invoke(this, "Override deleted successfully");
                    OverridesModified?.Invoke(this, EventArgs.Empty);
                    await ReloadAsync();
                }
                else
                {
                    StatusMessageChanged?.Invoke(this, $"Delete failed: {result.ErrorMessage}");
                    if (result.Exception != null)
                    {
                        LoggingUtility.LogApplicationError(result.Exception);
                    }
                }
            }
            catch (Exception ex)
            {
                StatusMessageChanged?.Invoke(this, $"Error: {ex.Message}");
                LoggingUtility.LogApplicationError(ex);
            }
        }

        private async void BtnRefresh_Click(object? sender, EventArgs e)
        {
            await ReloadAsync();
        }

        #endregion
    }
}
