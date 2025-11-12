using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using MTM_WIP_Application_Winforms.Core;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Forms.Shared;

/// <summary>
/// Dialog form for editing QuickButton properties (Part ID, Operation, Quantity).
/// Provides ComboBox controls with autocomplete matching Control_InventoryTab behavior.
/// Uses color-coded validation (red for invalid, black for valid selections).
/// Migrated to ThemedForm for automatic DPI scaling and theme support.
/// </summary>
/// <remarks>
/// Layout: Standard 3-field vertical form with labels on left, controls on right.
/// Minimum size: 400x280
/// Form is non-resizable dialog.
/// Loads Part IDs and Operations asynchronously from database on form load.
/// </remarks>
public partial class Form_QuickButtonEdit : ThemedForm
{
    #region Fields

    public string PartId { get; private set; } = string.Empty;
    public string Operation { get; private set; } = string.Empty;
    public int Quantity { get; private set; }

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the Form_QuickButtonEdit dialog.
    /// </summary>
    /// <param name="partId">Initial Part ID value to display/edit.</param>
    /// <param name="operation">Initial Operation value to display/edit.</param>
    /// <param name="quantity">Initial Quantity value to display/edit.</param>
    public Form_QuickButtonEdit(string partId, string operation, int quantity)
    {
        InitializeComponent();

        // DPI scaling and layout now handled by ThemedForm.OnLoad
        SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

        // Initialize quantity
        Quantity = quantity;

        // Load data asynchronously after form is shown
        Load += async (s, e) => await LoadDataAsync(partId, operation);
    }

    #endregion

    #region Data Loading

    /// <summary>
    /// Loads Part IDs and Operations from database and sets initial values.
    /// Updates status label to show loading progress.
    /// </summary>
    private async Task LoadDataAsync(string initialPartId, string initialOperation)
    {
        try
        {
            Form_QuickButtonEdit_Label_Status.Text = "Loading parts and operations...";

            // Load Part IDs
            await Helper_UI_ComboBoxes.FillPartComboBoxesAsync(Form_QuickButtonEdit_ComboBox_PartId);

            // Load Operations
            await Helper_UI_ComboBoxes.FillOperationComboBoxesAsync(Form_QuickButtonEdit_ComboBox_Operation);

            // Set initial Part ID by searching display text (not SelectedValue which is the ID integer)
            if (!string.IsNullOrEmpty(initialPartId))
            {
                // Search for the part by display member (PartID text)
                for (int i = 0; i < Form_QuickButtonEdit_ComboBox_PartId.Items.Count; i++)
                {
                    string? displayText = Form_QuickButtonEdit_ComboBox_PartId.GetItemText(Form_QuickButtonEdit_ComboBox_PartId.Items[i]);
                    if (string.IsNullOrEmpty(displayText)) continue;

                    // Display format is "PartID" or "PartID | Customer | Description"
                    if (displayText.StartsWith(initialPartId, StringComparison.OrdinalIgnoreCase))
                    {
                        Form_QuickButtonEdit_ComboBox_PartId.SelectedIndex = i;
                        break;
                    }
                }

                // If not found, set text directly (allows typing)
                if (Form_QuickButtonEdit_ComboBox_PartId.SelectedIndex < 0)
                {
                    Form_QuickButtonEdit_ComboBox_PartId.Text = initialPartId;
                }
            }

            // Set initial Operation by searching display text
            if (!string.IsNullOrEmpty(initialOperation))
            {
                // Search for the operation by display member (Operation text)
                for (int i = 0; i < Form_QuickButtonEdit_ComboBox_Operation.Items.Count; i++)
                {
                    string? displayText = Form_QuickButtonEdit_ComboBox_Operation.GetItemText(Form_QuickButtonEdit_ComboBox_Operation.Items[i]);
                    if (string.IsNullOrEmpty(displayText)) continue;

                    // Display format is "90" or "90 | Description"
                    if (displayText.StartsWith(initialOperation, StringComparison.OrdinalIgnoreCase))
                    {
                        Form_QuickButtonEdit_ComboBox_Operation.SelectedIndex = i;
                        break;
                    }
                }
            }

            // Set initial colors based on selection state
            UpdateComboBoxColor(Form_QuickButtonEdit_ComboBox_PartId);
            UpdateComboBoxColor(Form_QuickButtonEdit_ComboBox_Operation);

            Form_QuickButtonEdit_Label_Status.Text = "Ready";
            Form_QuickButtonEdit_Button_OK.Enabled = true;
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            Form_QuickButtonEdit_Label_Status.Text = $"Error loading data: {ex.Message}";
            MessageBox.Show(
                $"Failed to load parts and operations:\n{ex.Message}",
                "Load Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }
    }

    #endregion

    #region Validation

    /// <summary>
    /// Updates ComboBox foreground color based on selection state.
    /// Matches Control_InventoryTab behavior - error color when placeholder, normal color when selected.
    /// </summary>
    private void UpdateComboBoxColor(ComboBox comboBox)
    {
        // Apply error color if nothing selected or placeholder selected (index 0)
        if (comboBox.SelectedIndex <= 0)
        {
            comboBox.ForeColor = Model_Application_Variables.UserUiColors.ComboBoxErrorForeColor ?? Color.Red;
        }
        else
        {
            // Valid selection - use normal foreground color
            comboBox.ForeColor = Model_Application_Variables.UserUiColors.ComboBoxForeColor ?? Color.Black;
        }
    }

    /// <summary>
    /// Updates the status label based on the validation state of both ComboBoxes.
    /// Shows error state if either ComboBox has invalid selection or text doesn't match data source.
    /// </summary>
    private void UpdateStatusLabel()
    {
        // Check if Part ID is valid (either selected from list OR typed text matches an item)
        bool partIdInvalid = false;
        if (Form_QuickButtonEdit_ComboBox_PartId.SelectedIndex <= 0)
        {
            // Not selected from list - check if typed text matches any item
            string typedText = Form_QuickButtonEdit_ComboBox_PartId.Text?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(typedText) || Form_QuickButtonEdit_ComboBox_PartId.SelectedIndex == 0)
            {
                partIdInvalid = true;
            }
            else
            {
                // Check if text matches any item in the data source
                bool foundMatch = false;
                for (int i = 0; i < Form_QuickButtonEdit_ComboBox_PartId.Items.Count; i++)
                {
                    string? displayText = Form_QuickButtonEdit_ComboBox_PartId.GetItemText(Form_QuickButtonEdit_ComboBox_PartId.Items[i]);
                    if (string.IsNullOrEmpty(displayText)) continue;
                    // Match if display text starts with typed text (allows partial match like "01-34577-000" matching "01-34577-000 | Customer | Description")
                    if (displayText.StartsWith(typedText, StringComparison.OrdinalIgnoreCase))
                    {
                        foundMatch = true;
                        break;
                    }
                }
                partIdInvalid = !foundMatch;
            }
        }

        // Check if Operation is valid (must be selected from list)
        bool operationInvalid = false;
        if (Form_QuickButtonEdit_ComboBox_Operation.SelectedIndex <= 0)
        {
            // Not selected from list - check if typed text matches any item
            string typedText = Form_QuickButtonEdit_ComboBox_Operation.Text?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(typedText) || Form_QuickButtonEdit_ComboBox_Operation.SelectedIndex == 0)
            {
                operationInvalid = true;
            }
            else
            {
                // Check if text matches any item in the data source
                bool foundMatch = false;
                for (int i = 0; i < Form_QuickButtonEdit_ComboBox_Operation.Items.Count; i++)
                {
                    string? displayText = Form_QuickButtonEdit_ComboBox_Operation.GetItemText(Form_QuickButtonEdit_ComboBox_Operation.Items[i]);
                    if (string.IsNullOrEmpty(displayText)) continue;
                    // Match if display text starts with typed text (allows partial match like "90" matching "90 | Description")
                    if (displayText.StartsWith(typedText, StringComparison.OrdinalIgnoreCase))
                    {
                        foundMatch = true;
                        break;
                    }
                }
                operationInvalid = !foundMatch;
            }
        }

        // Update status label based on validation results
        if (partIdInvalid && operationInvalid)
        {
            Form_QuickButtonEdit_Label_Status.Text = "Please select a Part ID and Operation";
        }
        else if (partIdInvalid)
        {
            Form_QuickButtonEdit_Label_Status.Text = "Please select or enter a valid Part ID";
        }
        else if (operationInvalid)
        {
            Form_QuickButtonEdit_Label_Status.Text = "Please select an Operation";
        }
        else
        {
            // Both fields are valid
            Form_QuickButtonEdit_Label_Status.Text = "Ready";
        }
    }

    #endregion

    #region Event Handlers

    /// <summary>
    /// Handles PartId ComboBox selection change to update color and status.
    /// </summary>
    private void Form_QuickButtonEdit_ComboBox_PartId_SelectedIndexChanged(object? sender, EventArgs e)
    {
        UpdateComboBoxColor(Form_QuickButtonEdit_ComboBox_PartId);
        UpdateStatusLabel();
    }

    /// <summary>
    /// Handles PartId ComboBox text change to update color and status.
    /// </summary>
    private void Form_QuickButtonEdit_ComboBox_PartId_TextChanged(object? sender, EventArgs e)
    {
        UpdateComboBoxColor(Form_QuickButtonEdit_ComboBox_PartId);
        UpdateStatusLabel();
    }

    /// <summary>
    /// Handles Operation ComboBox selection change to update color and status.
    /// </summary>
    private void Form_QuickButtonEdit_ComboBox_Operation_SelectedIndexChanged(object? sender, EventArgs e)
    {
        UpdateComboBoxColor(Form_QuickButtonEdit_ComboBox_Operation);
        UpdateStatusLabel();
    }

    /// <summary>
    /// Handles Operation ComboBox text change to update color and status.
    /// </summary>
    private void Form_QuickButtonEdit_ComboBox_Operation_TextChanged(object? sender, EventArgs e)
    {
        UpdateComboBoxColor(Form_QuickButtonEdit_ComboBox_Operation);
        UpdateStatusLabel();
    }

    /// <summary>
    /// Handles OK button click with validation before accepting dialog.
    /// </summary>
    private void Form_QuickButtonEdit_Button_OK_Click(object? sender, EventArgs e)
    {
        // Validate Part ID selection
        if (Form_QuickButtonEdit_ComboBox_PartId.SelectedIndex <= 0)
        {
            MessageBox.Show(
                "Please select a valid Part ID from the list or enter a valid part number.",
                "Invalid Part ID",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
            Form_QuickButtonEdit_ComboBox_PartId.Focus();
            return;
        }

        if (Form_QuickButtonEdit_ComboBox_Operation.SelectedIndex <= 0)
        {
            MessageBox.Show(
                "Please select a valid Operation from the list.",
                "Invalid Operation",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
            Form_QuickButtonEdit_ComboBox_Operation.Focus();
            return;
        }

        // Additional validation - check if something is selected or typed
        if (Form_QuickButtonEdit_ComboBox_PartId.SelectedIndex < 0 && string.IsNullOrWhiteSpace(Form_QuickButtonEdit_ComboBox_PartId.Text))
        {
            MessageBox.Show(
                "Please select or enter a Part ID.",
                "Validation Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
            Form_QuickButtonEdit_ComboBox_PartId.Focus();
            return;
        }

        // Validate Operation selection
        if (Form_QuickButtonEdit_ComboBox_Operation.SelectedIndex < 0)
        {
            MessageBox.Show(
                "Please select an Operation from the list.",
                "Validation Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
            Form_QuickButtonEdit_ComboBox_Operation.Focus();
            return;
        }

        // Extract Part ID from display text (format: "PartID" or "PartID | Customer | Description")
        if (Form_QuickButtonEdit_ComboBox_PartId.SelectedIndex >= 0)
        {
            string displayText = Form_QuickButtonEdit_ComboBox_PartId.GetItemText(Form_QuickButtonEdit_ComboBox_PartId.SelectedItem) ?? string.Empty;
            if (displayText.Contains(" | "))
            {
                // Extract just the PartID part (before first pipe)
                PartId = displayText.Split(new[] { " | " }, StringSplitOptions.None)[0].Trim();
            }
            else
            {
                PartId = displayText.Trim();
            }
        }
        else
        {
            // User typed a value - use as-is
            PartId = Form_QuickButtonEdit_ComboBox_PartId.Text.Trim();
        }

        // Extract Operation from display text (format: "90" or "90 | Description")
        string opDisplayText = Form_QuickButtonEdit_ComboBox_Operation.GetItemText(Form_QuickButtonEdit_ComboBox_Operation.SelectedItem) ?? string.Empty;
        if (opDisplayText.Contains(" | "))
        {
            // Extract just the operation number (before first pipe)
            Operation = opDisplayText.Split(new[] { " | " }, StringSplitOptions.None)[0].Trim();
        }
        else
        {
            Operation = opDisplayText.Trim();
        }

        Quantity = (int)Form_QuickButtonEdit_NumericUpDown_Quantity.Value;

        // Final validation
        if (string.IsNullOrEmpty(PartId) || string.IsNullOrEmpty(Operation))
        {
            MessageBox.Show(
                "Part ID and Operation are required.",
                "Validation Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
            return;
        }

        DialogResult = DialogResult.OK;
        Close();
    }

    #endregion
}
