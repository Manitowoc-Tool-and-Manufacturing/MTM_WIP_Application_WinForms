using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MTM_WIP_Application_Winforms.Core;
using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Forms.Settings;

/// <summary>
/// Dialog for adding new parameter prefix overrides.
/// Provides autocomplete for procedure and parameter names.
/// </summary>
public partial class Dialog_AddParameterOverride : Form
{
    #region Fields

    private List<string> _procedureNames = new();
    private List<string> _parameterNames = new();
    private Model_ParameterPrefix_Override? _result;

    #endregion

    #region Properties

    /// <summary>
    /// Gets the created override if dialog was confirmed.
    /// </summary>
    public Model_ParameterPrefix_Override? Result => _result;

    #endregion

    #region Constructors

    public Dialog_AddParameterOverride()
    {
        InitializeComponent();
        Core_Themes.ApplyDpiScaling(this);
        Core_Themes.ApplyRuntimeLayoutAdjustments(this);
        
        // Set default prefix to empty string (most common case)
        txtOverridePrefix.Text = string.Empty;
    }

    #endregion

    #region Form Events

    private async void Dialog_AddParameterOverride_Load(object sender, EventArgs e)
    {
        await LoadProcedureNamesAsync();
        SetupAutocomplete();
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
            
            // Query INFORMATION_SCHEMA for stored procedures  
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
            // Hide status after 2 seconds
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

                // Update autocomplete
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
        // Procedure name autocomplete
        txtProcedureName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        txtProcedureName.AutoCompleteSource = AutoCompleteSource.CustomSource;
        txtProcedureName.AutoCompleteCustomSource.Clear();
        txtProcedureName.AutoCompleteCustomSource.AddRange(_procedureNames.ToArray());

        // Parameter name autocomplete (populated when procedure selected)
        txtParameterName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        txtParameterName.AutoCompleteSource = AutoCompleteSource.CustomSource;
    }

    #endregion

    #region Private Methods - Validation

    private bool ValidateInput()
    {
        // Procedure name required
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

        // Parameter name required
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

        // Reason required
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

            // Check if procedure exists (warning only)
            var procedureExists = await CheckProcedureExistsAsync(txtProcedureName.Text.Trim());
            
            if (!procedureExists)
            {
                var result = MessageBox.Show(
                    $"Warning: Procedure '{txtProcedureName.Text.Trim()}' was not found in INFORMATION_SCHEMA.\n\n" +
                    "This may mean:\n" +
                    "• The procedure doesn't exist yet\n" +
                    "• The procedure name is misspelled\n" +
                    "• You're creating an override for a future procedure\n\n" +
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

            // Create override model
            _result = new Model_ParameterPrefix_Override
            {
                ProcedureName = txtProcedureName.Text.Trim(),
                ParameterName = txtParameterName.Text.Trim(),
                OverridePrefix = string.IsNullOrWhiteSpace(txtOverridePrefix.Text) 
                    ? string.Empty 
                    : txtOverridePrefix.Text.Trim(),
                Reason = txtReason.Text.Trim(),
                CreatedBy = Model_Application_Variables.User,
                CreatedDate = DateTime.Now,
                IsActive = true
            };

            lblStatus.Text = "Saving...";

            // Save to database
            var saveResult = await Dao_ParameterPrefixOverrides.AddAsync(_result);

            if (saveResult.IsSuccess)
            {
                _result.OverrideId = saveResult.Data;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show(
                    $"Failed to save override:\n\n{saveResult.ErrorMessage}",
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
        // When procedure name changes, reload parameter autocomplete
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
