using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MTM_WIP_Application_Winforms.Core;
using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Forms.Settings;

/// <summary>
/// Dialog for editing existing parameter prefix overrides.
/// Allows changing prefix and verification type.
/// Migrated to ThemedForm for automatic DPI scaling and theme support.
/// </summary>
public partial class Dialog_EditParameterOverride : ThemedForm
{
    #region Fields

    private readonly Model_ParameterPrefix_Override _originalOverride;
    private List<string> _procedureNames = new();
    private List<string> _parameterNames = new();
    private Model_ParameterPrefix_Override? _result;

    #endregion

    #region Properties

    /// <summary>
    /// Gets the updated override if dialog was confirmed.
    /// </summary>
    public Model_ParameterPrefix_Override? Result => _result;

    #endregion

    #region Constructors

    public Dialog_EditParameterOverride(Model_ParameterPrefix_Override existingOverride)
    {
        if (existingOverride == null)
            throw new ArgumentNullException(nameof(existingOverride));

        _originalOverride = existingOverride;

        InitializeComponent();
        SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        
        PopulateFields();
    }

    #endregion

    #region Form Events

    private async void Dialog_EditParameterOverride_Load(object sender, EventArgs e)
    {
        await LoadProcedureNamesAsync();
        await LoadParameterNamesAsync(_originalOverride.ProcedureName);
        SetupAutocomplete();
    }

    #endregion

    #region Private Methods - Initialization

    private void PopulateFields()
    {
        txtProcedureName.Text = _originalOverride.ProcedureName;
        txtParameterName.Text = _originalOverride.ParameterName;
        txtOverridePrefix.Text = string.IsNullOrEmpty(_originalOverride.OverridePrefix) 
            ? string.Empty 
            : _originalOverride.OverridePrefix;
        txtReason.Text = _originalOverride.Reason ?? string.Empty;

        // Show audit trail
        lblCreatedInfo.Text = $"Created by {_originalOverride.CreatedBy} on {_originalOverride.CreatedDate:yyyy-MM-dd HH:mm}";
        
        if (_originalOverride.ModifiedBy != null && _originalOverride.ModifiedDate.HasValue)
        {
            lblModifiedInfo.Text = $"Last modified by {_originalOverride.ModifiedBy} on {_originalOverride.ModifiedDate:yyyy-MM-dd HH:mm}";
            lblModifiedInfo.Visible = true;
        }
    }

    #endregion

    #region Private Methods - Data Loading

    private async Task LoadProcedureNamesAsync()
    {
        try
        {
            lblStatus.Text = "Loading stored procedures...";
            lblStatus.Visible = true;

            var connectionString = Helper_Database_Variables.GetConnectionString(null, null, null, null);
            
            var parameters = new Dictionary<string, object>();
            
            var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                connectionString,
                "query_get_all_stored_procedures",
                parameters);

            if (result.IsSuccess && result.Data != null)
            {
                _procedureNames = result.Data.AsEnumerable()
                    .Select(row => row["ROUTINE_NAME"].ToString() ?? string.Empty)
                    .Where(name => !string.IsNullOrWhiteSpace(name))
                    .ToList();

                lblStatus.Text = $"Loaded {_procedureNames.Count} procedures";
            }
            else
            {
                lblStatus.Text = "Warning: Could not load procedure names";
                if (result.Exception != null)
                {
                    LoggingUtility.LogApplicationError(result.Exception);
                }
            }
        }
        catch (Exception ex)
        {
            lblStatus.Text = "Error loading procedures";
            LoggingUtility.LogApplicationError(ex);
        }
        finally
        {
            await Task.Delay(2000);
            lblStatus.Visible = false;
        }
    }

    private async Task LoadParameterNamesAsync(string procedureName)
    {
        try
        {
            lblStatus.Text = "Loading parameters...";
            lblStatus.Visible = true;

            var connectionString = Helper_Database_Variables.GetConnectionString(null, null, null, null);
            
            var parameters = new Dictionary<string, object>
            {
                ["ProcedureName"] = procedureName
            };
            
            var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                connectionString,
                "query_get_procedure_parameters",
                parameters);

            if (result.IsSuccess && result.Data != null)
            {
                _parameterNames = result.Data.AsEnumerable()
                    .Select(row => row["PARAMETER_NAME"].ToString() ?? string.Empty)
                    .Where(name => !string.IsNullOrWhiteSpace(name))
                    .ToList();

                txtParameterName.AutoCompleteCustomSource.Clear();
                txtParameterName.AutoCompleteCustomSource.AddRange(_parameterNames.ToArray());

                lblStatus.Text = $"Loaded {_parameterNames.Count} parameters";
            }
            else
            {
                lblStatus.Text = "Warning: Could not load parameter names";
                _parameterNames.Clear();
                txtParameterName.AutoCompleteCustomSource.Clear();
            }
        }
        catch (Exception ex)
        {
            lblStatus.Text = "Error loading parameters";
            LoggingUtility.LogApplicationError(ex);
        }
        finally
        {
            await Task.Delay(2000);
            lblStatus.Visible = false;
        }
    }

    private void SetupAutocomplete()
    {
        txtProcedureName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        txtProcedureName.AutoCompleteSource = AutoCompleteSource.CustomSource;
        txtProcedureName.AutoCompleteCustomSource.Clear();
        txtProcedureName.AutoCompleteCustomSource.AddRange(_procedureNames.ToArray());

        txtParameterName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        txtParameterName.AutoCompleteSource = AutoCompleteSource.CustomSource;
    }

    #endregion

    #region Private Methods - Validation

    private bool ValidateInput()
    {
        if (string.IsNullOrWhiteSpace(txtProcedureName.Text))
        {
            MessageBox.Show(
                "Procedure name is required.",
                "Validation Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            txtProcedureName.Focus();
            return false;
        }

        if (string.IsNullOrWhiteSpace(txtParameterName.Text))
        {
            MessageBox.Show(
                "Parameter name is required.",
                "Validation Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            txtParameterName.Focus();
            return false;
        }

        if (string.IsNullOrWhiteSpace(txtReason.Text))
        {
            MessageBox.Show(
                "Reason is required to document why this override is needed.",
                "Validation Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            txtReason.Focus();
            return false;
        }

        return true;
    }

    private async Task<bool> CheckProcedureExistsAsync(string procedureName)
    {
        try
        {
            var connectionString = Helper_Database_Variables.GetConnectionString(null, null, null, null);
            
            var parameters = new Dictionary<string, object>
            {
                ["ProcedureName"] = procedureName
            };
            
            var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                connectionString,
                "query_check_procedure_exists",
                parameters);

            if (result.IsSuccess && result.Data != null && result.Data.Rows.Count > 0)
            {
                var count = Convert.ToInt32(result.Data.Rows[0]["ProcedureCount"]);
                return count > 0;
            }

            return false;
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            return false;
        }
    }

    #endregion

    #region Button Event Handlers

    private async void BtnSave_Click(object sender, EventArgs e)
    {
        if (!ValidateInput())
            return;

        try
        {
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            lblStatus.Text = "Validating...";
            lblStatus.Visible = true;

            var procedureExists = await CheckProcedureExistsAsync(txtProcedureName.Text.Trim());
            
            if (!procedureExists)
            {
                var result = MessageBox.Show(
                    $"Warning: Procedure '{txtProcedureName.Text.Trim()}' was not found in INFORMATION_SCHEMA.\n\n" +
                    "Do you want to continue anyway?",
                    "Procedure Not Found",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result != DialogResult.Yes)
                {
                    lblStatus.Visible = false;
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                    return;
                }
            }

            // Create updated override model
            _result = new Model_ParameterPrefix_Override
            {
                OverrideId = _originalOverride.OverrideId,
                ProcedureName = txtProcedureName.Text.Trim(),
                ParameterName = txtParameterName.Text.Trim(),
                OverridePrefix = string.IsNullOrWhiteSpace(txtOverridePrefix.Text) 
                    ? string.Empty 
                    : txtOverridePrefix.Text.Trim(),
                Reason = txtReason.Text.Trim(),
                CreatedBy = _originalOverride.CreatedBy,
                CreatedDate = _originalOverride.CreatedDate,
                ModifiedBy = Model_Application_Variables.User,
                ModifiedDate = DateTime.Now,
                IsActive = true
            };

            lblStatus.Text = "Saving...";

            var saveResult = await Dao_ParameterPrefixOverrides.UpdateAsync(_result);

            if (saveResult.IsSuccess)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show(
                    $"Failed to update override:\n\n{saveResult.ErrorMessage}",
                    "Save Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                if (saveResult.Exception != null)
                {
                    LoggingUtility.LogApplicationError(saveResult.Exception);
                }

                lblStatus.Visible = false;
                btnSave.Enabled = true;
                btnCancel.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"Unexpected error:\n\n{ex.Message}",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);

            LoggingUtility.LogApplicationError(ex);
            
            lblStatus.Visible = false;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
        }
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }

    #endregion

    #region Text Changed Event Handlers

    private async void TxtProcedureName_TextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(txtProcedureName.Text))
        {
            await LoadParameterNamesAsync(txtProcedureName.Text.Trim());
        }
        else
        {
            _parameterNames.Clear();
            txtParameterName.AutoCompleteCustomSource.Clear();
        }
    }

    #endregion
}
