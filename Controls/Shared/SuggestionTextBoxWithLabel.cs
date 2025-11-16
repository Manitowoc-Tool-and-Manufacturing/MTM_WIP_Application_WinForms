using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;

namespace MTM_WIP_Application_Winforms.Controls.Shared
{
    /// <summary>
    /// Composite control containing a Label, SuggestionTextBox/TextBox, and F4 button in a TableLayoutPanel.
    /// Supports both suggestion-based input (with datatable) and validated text input (without datatable).
    /// </summary>
    [DesignerCategory("Component")]
    [ToolboxItem(true)]
    [Description("SuggestionTextBox with integrated label, F4 button, and validation support")]
    public partial class SuggestionTextBoxWithLabel : UserControl
    {
        #region Fields

        private IValidator? _validator;
        private bool _enableSuggestions = true;
        private bool _showValidationColor = true;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the label text (automatically appends ": " suffix).
        /// </summary>
        [Category("Appearance")]
        [Description("Label text displayed before the textbox")]
        [DefaultValue("")]
        public string LabelText
        {
            get => SuggestionTextBoxWithLabel_Label_Main.Text.TrimEnd(':', ' ');
            set => SuggestionTextBoxWithLabel_Label_Main.Text = string.IsNullOrWhiteSpace(value) ? string.Empty : $"{value}: ";
        }

        /// <summary>
        /// Gets the inner SuggestionTextBox control for direct access.
        /// Use this to configure data provider, events, and behavior.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SuggestionTextBox TextBox => SuggestionTextBoxWithLabel_TextBox_Main;

        /// <summary>
        /// Gets or sets the text in the SuggestionTextBox.
        /// </summary>
        [Category("Appearance")]
        [Description("Text content of the SuggestionTextBox")]
        [DefaultValue("")]
        public override string? Text
        {
            get => SuggestionTextBoxWithLabel_TextBox_Main.Text;
            set => SuggestionTextBoxWithLabel_TextBox_Main.Text = value;
        }

        /// <summary>
        /// Gets or sets the placeholder text in the SuggestionTextBox.
        /// </summary>
        [Category("Appearance")]
        [Description("Placeholder text shown when textbox is empty")]
        [DefaultValue("")]
        public string PlaceholderText
        {
            get => SuggestionTextBoxWithLabel_TextBox_Main.PlaceholderText;
            set => SuggestionTextBoxWithLabel_TextBox_Main.PlaceholderText = value;
        }

        /// <summary>
        /// Gets or sets whether the F4 button is visible.
        /// </summary>
        [Category("Appearance")]
        [Description("Shows or hides the F4 magnifying glass button")]
        [DefaultValue(true)]
        public bool ShowF4Button
        {
            get => SuggestionTextBoxWithLabel_Button_F4.Visible;
            set => SuggestionTextBoxWithLabel_Button_F4.Visible = value;
        }

        /// <summary>
        /// Gets or sets the tab index of the textbox.
        /// </summary>
        [Category("Behavior")]
        [Description("Tab order of the textbox")]
        public new int TabIndex
        {
            get => SuggestionTextBoxWithLabel_TextBox_Main.TabIndex;
            set => SuggestionTextBoxWithLabel_TextBox_Main.TabIndex = value;
        }

        /// <summary>
        /// Gets or sets whether suggestion functionality is enabled.
        /// When false, acts as a regular validated textbox without suggestion overlay.
        /// </summary>
        [Category("Behavior")]
        [Description("Enables or disables suggestion functionality (false = validated textbox mode)")]
        [DefaultValue(true)]
        public bool EnableSuggestions
        {
            get => _enableSuggestions;
            set
            {
                _enableSuggestions = value;
                ShowF4Button = value; // Hide F4 button when suggestions disabled
                SuggestionTextBoxWithLabel_TextBox_Main.Enabled = value || _validator != null;
            }
        }

        /// <summary>
        /// Gets or sets whether validation colors are shown (red for invalid, black for valid).
        /// </summary>
        [Category("Appearance")]
        [Description("Shows validation colors (red for invalid, black for valid)")]
        [DefaultValue(true)]
        public bool ShowValidationColor
        {
            get => _showValidationColor;
            set => _showValidationColor = value;
        }

        /// <summary>
        /// Gets or sets the validator type to use for non-suggestion mode.
        /// Supported types: "Quantity", "Weight", "Price", or any registered validator.
        /// </summary>
        [Category("Behavior")]
        [Description("Validator type for non-suggestion mode (e.g., 'Quantity', 'Weight', 'Price')")]
        [DefaultValue("")]
        public string ValidatorType { get; set; } = string.Empty;

        /// <summary>
        /// Gets whether the current text is valid according to the validator.
        /// </summary>
        [Browsable(false)]
        public bool IsValid
        {
            get
            {
                if (_validator == null || !_enableSuggestions)
                {
                    return true; // No validation if no validator or suggestions enabled
                }

                var result = _validator.Validate(Text ?? string.Empty);
                return result.IsValid;
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when user selects a suggestion.
        /// Forwarded from inner SuggestionTextBox.
        /// </summary>
        [Category("Action")]
        [Description("Occurs when user selects a suggestion")]
        public event EventHandler<SuggestionSelectedEventArgs>? SuggestionSelected;

        /// <summary>
        /// Occurs when user cancels suggestion selection.
        /// Forwarded from inner SuggestionTextBox.
        /// </summary>
        [Category("Action")]
        [Description("Occurs when user cancels suggestion selection")]
        public event EventHandler<SuggestionCancelledEventArgs>? SuggestionCancelled;

        /// <summary>
        /// Occurs when validation state changes in non-suggestion mode.
        /// </summary>
        [Category("Action")]
        [Description("Occurs when validation state changes")]
        public event EventHandler<Model_Validation_Result>? ValidationChanged;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of SuggestionTextBoxWithLabel.
        /// </summary>
        public SuggestionTextBoxWithLabel()
        {
            InitializeComponent();
            WireUpEvents();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Manually triggers suggestion display for current text.
        /// </summary>
        public async Task ShowSuggestionsAsync()
        {
            await SuggestionTextBoxWithLabel_TextBox_Main.ShowSuggestionsAsync();
        }

        /// <summary>
        /// Clears the textbox content.
        /// </summary>
        public void ClearTextBox()
        {
            SuggestionTextBoxWithLabel_TextBox_Main.Clear();
        }

        /// <summary>
        /// Refreshes the data source cache.
        /// </summary>
        public void RefreshDataSource()
        {
            SuggestionTextBoxWithLabel_TextBox_Main.RefreshDataSource();
        }

        /// <summary>
        /// Sets focus to the textbox.
        /// </summary>
        public new bool Focus()
        {
            return SuggestionTextBoxWithLabel_TextBox_Main.Focus();
        }

        /// <summary>
        /// Validates the current text using the configured validator.
        /// </summary>
        /// <returns>Validation result</returns>
        public Model_Validation_Result Validate()
        {
            if (_validator == null)
            {
                return Model_Validation_Result.Success(LabelText);
            }

            var result = _validator.Validate(Text ?? string.Empty);
            UpdateValidationVisuals(result);
            ValidationChanged?.Invoke(this, result);
            return result;
        }

        /// <summary>
        /// Configures the control for validated input (non-suggestion mode).
        /// </summary>
        /// <param name="validatorType">Validator type (e.g., "Quantity", "Weight", "Price")</param>
        /// <param name="showValidationColor">Whether to show validation colors</param>
        public void ConfigureForValidation(string validatorType, bool showValidationColor = true)
        {
            ValidatorType = validatorType;
            _showValidationColor = showValidationColor;
            EnableSuggestions = false;
            ShowF4Button = false;

            // Get validator from Service_Validation
            _validator = Service_Validation.GetValidator(validatorType);

            if (_validator == null)
            {
                throw new InvalidOperationException($"No validator registered for type '{validatorType}'");
            }

            // Wire up validation events
            WireUpValidationEvents();
        }

        /// <summary>
        /// Configures the control with a custom validator.
        /// </summary>
        /// <param name="validator">Custom validator instance</param>
        /// <param name="showValidationColor">Whether to show validation colors</param>
        public void ConfigureForValidation(IValidator validator, bool showValidationColor = true)
        {
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _showValidationColor = showValidationColor;
            EnableSuggestions = false;
            ShowF4Button = false;

            // Wire up validation events
            WireUpValidationEvents();
        }

        #endregion

        #region Initialization

        private void WireUpEvents()
        {
            // Forward SuggestionTextBox events (only active when EnableSuggestions = true)
            SuggestionTextBoxWithLabel_TextBox_Main.SuggestionSelected += (sender, e) => SuggestionSelected?.Invoke(this, e);
            SuggestionTextBoxWithLabel_TextBox_Main.SuggestionCancelled += (sender, e) => SuggestionCancelled?.Invoke(this, e);

            // F4 button triggers suggestion display
            SuggestionTextBoxWithLabel_Button_F4.Click += async (sender, e) => await Helper_SuggestionTextBox.ShowFullSuggestionListAsync(SuggestionTextBoxWithLabel_TextBox_Main);
        }

        private void WireUpValidationEvents()
        {
            // TextChanged: Validate on each character
            SuggestionTextBoxWithLabel_TextBox_Main.TextChanged += (sender, e) =>
            {
                if (_validator != null && !_enableSuggestions)
                {
                    Validate();
                }
            };

            // Leave: Final validation when focus leaves
            SuggestionTextBoxWithLabel_TextBox_Main.Leave += (sender, e) =>
            {
                if (_validator != null && !_enableSuggestions)
                {
                    Validate();
                }
            };
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Updates visual feedback based on validation result.
        /// </summary>
        private void UpdateValidationVisuals(Model_Validation_Result result)
        {
            if (!_showValidationColor)
            {
                return;
            }

            var textBox = SuggestionTextBoxWithLabel_TextBox_Main;

            if (result.IsValid || string.IsNullOrWhiteSpace(textBox.Text))
            {
                // Valid or empty: Use normal color
                textBox.ForeColor = Model_Application_Variables.UserUiColors.TextBoxForeColor ?? Color.Black;
            }
            else
            {
                // Invalid: Use error color
                textBox.ForeColor = Model_Application_Variables.UserUiColors.TextBoxErrorForeColor ?? Color.Red;
            }
        }

        #endregion
    }
}
