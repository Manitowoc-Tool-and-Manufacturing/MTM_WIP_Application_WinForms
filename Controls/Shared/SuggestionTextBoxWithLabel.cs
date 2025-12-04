using System.ComponentModel;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Models.Enums;
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
    public partial class SuggestionTextBoxWithLabel : ThemedUserControl
    {
        #region Fields

        private IValidator? _validator;
        private bool _enableSuggestions = true;
        private bool _showValidationColor = true;
        private bool _isSuggestionInvocationInProgress;

        #endregion

        #region Properties

        private Enum_SuggestionDataSource _suggestionDataSource = Enum_SuggestionDataSource.None;

        /// <summary>
        /// Gets or sets the data source type for suggestions.
        /// Automatically configures the DataProvider based on the selected type.
        /// </summary>
        [Category("Suggestion Data")]
        [Description("The type of dataset to use for suggestions")]
        [DefaultValue(Enum_SuggestionDataSource.None)]
        public Enum_SuggestionDataSource SuggestionDataSource
        {
            get => _suggestionDataSource;
            set
            {
                _suggestionDataSource = value;
                if (!DesignMode)
                {
                    ConfigureDataSource();
                }
            }
        }

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

        [Category("Appearance")]
        [Description("Shows or hides the label")]
        [DefaultValue(Enum_LabelVisibility.Visible)]
        public Enum_LabelVisibility LabelVisibility
        {
            get => SuggestionTextBoxWithLabel_Label_Main.Visible ? Enum_LabelVisibility.Visible : Enum_LabelVisibility.Hidden;
            set => SuggestionTextBoxWithLabel_Label_Main.Visible = value == Enum_LabelVisibility.Visible;
        }

        /// <summary>
        /// Gets or sets the action to take when a suggestion is selected.
        /// Default: MoveFocusToNextControl.
        /// </summary>
        [Category("Behavior")]
        [Description("Action to take when a suggestion is selected")]
        [DefaultValue(Enum_SuggestionSelectionAction.MoveFocusToNextControl)]
        public Enum_SuggestionSelectionAction SelectionAction
        {
            get => SuggestionTextBoxWithLabel_TextBox_Main.SelectionAction;
            set => SuggestionTextBoxWithLabel_TextBox_Main.SelectionAction = value;
        }

        /// <summary>
        /// Gets or sets the action to take when no matches are found.
        /// Default: ShowWarningAndClear.
        /// </summary>
        [Category("Behavior")]
        [Description("Action to take when no suggestions match")]
        [DefaultValue(Enum_SuggestionNoMatchAction.ShowWarningAndClear)]
        public Enum_SuggestionNoMatchAction NoMatchAction
        {
            get => SuggestionTextBoxWithLabel_TextBox_Main.NoMatchAction;
            set => SuggestionTextBoxWithLabel_TextBox_Main.NoMatchAction = value;
        }

        /// <summary>
        /// Gets or sets the minimum width of the label.
        /// </summary>
        [Category("Layout")]
        [Description("Minimum width of the label")]
        public int MinLength
        {
            get => SuggestionTextBoxWithLabel_Label_Main.MinimumSize.Width;
            set => SuggestionTextBoxWithLabel_Label_Main.MinimumSize = new Size(value, SuggestionTextBoxWithLabel_TextBox_Main.MinimumSize.Height);
        }

        /// <summary>
        /// Gets or sets the maximum width of the label.
        /// </summary>
        [Category("Layout")]
        [Description("Maximum width of the label")]
        public int MaxLength
        {
            get => SuggestionTextBoxWithLabel_Label_Main.MaximumSize.Width;
            set => SuggestionTextBoxWithLabel_Label_Main.MaximumSize = new Size(value, SuggestionTextBoxWithLabel_TextBox_Main.MaximumSize.Height);
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
            #pragma warning disable CS8764 // Nullability of return type doesn't match overridden member (possibly because of nullability attributes).
                get => SuggestionTextBoxWithLabel_TextBox_Main.Text;
            #pragma warning restore CS8764 // Nullability of return type doesn't match overridden member (possibly because of nullability attributes).

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
        /// Sets the TabStop property of the F4 button.
        /// Use this to exclude the F4 button from the tab order.
        /// </summary>
        /// <param name="tabStop">True to include in tab order, false to exclude</param>
        public void SetF4ButtonTabStop(bool tabStop)
        {
            SuggestionTextBoxWithLabel_Button_F4.TabStop = tabStop;
        }

        /// <summary>
        /// Gets or sets the tab index of the composite control.
        /// This sets the tab index of the UserControl itself, which determines when focus enters the control.
        /// </summary>
        [Category("Behavior")]
        [Description("Tab order of the control")]
        public new int TabIndex
        {
            get => base.TabIndex;
            set => base.TabIndex = value;
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
                UpdateTextBoxEnabledState();
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
        public event EventHandler<EventArgs_SuggestionSelectedEventArgs>? SuggestionSelected;

        /// <summary>
        /// Occurs when user cancels suggestion selection.
        /// Forwarded from inner SuggestionTextBox.
        /// </summary>
        [Category("Action")]
        [Description("Occurs when user cancels suggestion selection")]
        public event EventHandler<EventArgs_SuggestionCancelledEventArgs>? SuggestionCancelled;

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
            TabStop = true;
            
            // Ensure focus is forwarded to the internal TextBox when UserControl receives focus via Tab
            // Use GotFocus instead of Enter for better tab navigation handling
            this.GotFocus += (s, e) =>
            {
                if (!SuggestionTextBoxWithLabel_TextBox_Main.Focused)
                {
                    SuggestionTextBoxWithLabel_TextBox_Main.Focus();
                }
            };
        }

        #endregion

        #region Methods

        private void ConfigureDataSource()
        {
            switch (_suggestionDataSource)
            {
                case Enum_SuggestionDataSource.MTM_PartNumber:
                    Helper_SuggestionTextBox.ConfigureForPartNumbers(this, Helper_SuggestionTextBox.GetCachedPartNumbersAsync);
                    break;
                case Enum_SuggestionDataSource.MTM_Operation:
                    Helper_SuggestionTextBox.ConfigureForOperations(this, Helper_SuggestionTextBox.GetCachedOperationsAsync);
                    break;
                case Enum_SuggestionDataSource.MTM_Location:
                    Helper_SuggestionTextBox.ConfigureForLocations(this, Helper_SuggestionTextBox.GetCachedLocationsAsync);
                    break;
                case Enum_SuggestionDataSource.MTM_Color:
                    Helper_SuggestionTextBox.ConfigureForColorCodes(this, Helper_SuggestionTextBox.GetCachedColorsAsync);
                    break;
                case Enum_SuggestionDataSource.MTM_User:
                    Helper_SuggestionTextBox.ConfigureForUsers(this, Helper_SuggestionTextBox.GetCachedUsersAsync);
                    break;
                // InforVisual types - Placeholder for now as logic is not implemented
                case Enum_SuggestionDataSource.Infor_PartNumber:
                case Enum_SuggestionDataSource.Infor_User:
                case Enum_SuggestionDataSource.Infor_Location:
                case Enum_SuggestionDataSource.Infor_Operation:
                case Enum_SuggestionDataSource.Infor_PONumber:
                case Enum_SuggestionDataSource.Infor_CONumber:
                case Enum_SuggestionDataSource.Infor_WONumber:
                case Enum_SuggestionDataSource.Infor_FGTNumber:
                case Enum_SuggestionDataSource.Infor_MMCNumber:
                case Enum_SuggestionDataSource.Infor_MMFNumber:
                    // TODO: Implement InforVisual data providers
                    break;
                case Enum_SuggestionDataSource.None:
                default:
                    break;
            }
        }

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
        public new Model_Validation_Result Validate()
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
            _validator = Service_Validation_Core.GetValidator(validatorType);
            UpdateTextBoxEnabledState();

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
            UpdateTextBoxEnabledState();

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
            SuggestionTextBoxWithLabel_Button_F4.Click += async (sender, e) =>
            {
                if (!_enableSuggestions)
                {
                    return;
                }

                await TriggerFullSuggestionListAsync();
            };

            // Keyboard shortcuts (F4, Tab, or Down arrow on empty input) should behave like standalone SuggestionTextBox
            SuggestionTextBoxWithLabel_TextBox_Main.KeyDown += async (sender, e) =>
            {
                if (!_enableSuggestions)
                {
                    return;
                }

                bool isF4 = e.KeyCode == Keys.F4;
                bool isDownOnEmptyInput = e.KeyCode == Keys.Down && SuggestionTextBoxWithLabel_TextBox_Main.Text.Length == 0;
                bool isTabOnEmptyInput = e.KeyCode == Keys.Tab && SuggestionTextBoxWithLabel_TextBox_Main.Text.Length == 0;

                if (!isF4 && !isDownOnEmptyInput && !isTabOnEmptyInput)
                {
                    return;
                }

                e.Handled = true;
                e.SuppressKeyPress = true;
                await TriggerFullSuggestionListAsync();
            };
        }

        private async Task TriggerFullSuggestionListAsync()
        {
            if (_isSuggestionInvocationInProgress)
            {
                return;
            }

            _isSuggestionInvocationInProgress = true;
            try
            {
                await Helper_SuggestionTextBox.ShowFullSuggestionListAsync(SuggestionTextBoxWithLabel_TextBox_Main);
            }
            finally
            {
                _isSuggestionInvocationInProgress = false;
            }
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
        /// Ensures the inner textbox is enabled when suggestions are on or a validator is configured.
        /// </summary>
        private void UpdateTextBoxEnabledState()
        {
            bool shouldEnable = _enableSuggestions || _validator != null;
            SuggestionTextBoxWithLabel_TextBox_Main.Enabled = shouldEnable;
        }

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
