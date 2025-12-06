using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Services.Logging;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Models.Enums;
using MTM_WIP_Application_Winforms.Services;
using MTM_WIP_Application_Winforms.Helpers;

namespace MTM_WIP_Application_Winforms.Components.Shared
{
    /// <summary>
    /// Enhanced TextBox control that provides intelligent suggestion/autocomplete
    /// functionality when user exits the field. Supports any data source via
    /// delegate pattern, wildcard filtering, and keyboard/mouse navigation.
    /// </summary>
    [DesignerCategory("Component")]
    [ToolboxItem(true)]
    [Description("TextBox with intelligent suggestion/autocomplete support")]
    public partial class Component_SuggestionTextBox : ThemedUserControl
    {
        #region Native Interop

        private const int EM_SETCUEBANNER = 0x1501;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        #endregion

        #region Fields

        private SuggestionOverlayForm? _currentOverlay;
        private bool _isOverlayVisible;
        private List<string>? _lastFilteredResults;
        private string _originalInput = string.Empty;

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
        /// Gets or sets the async data provider that returns suggestion strings.
        /// Invoked when user exits field (LostFocus) with non-empty text.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Func<Task<List<string>>>? DataProvider { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of suggestions to display.
        /// Default: 100. Range: 1-1000.
        /// </summary>
        [Category("Behavior")]
        [Description("Maximum number of suggestions to display")]
        [DefaultValue(100)]
        public int MaxResults { get; set; } = 100;

        /// <summary>
        /// Gets or sets whether wildcard pattern matching is enabled using % symbol.
        /// Example: "R-%-01" matches "R-ABC-01", "R-XYZ-01", etc.
        /// Default: true.
        /// </summary>
        [Category("Behavior")]
        [Description("Enable wildcard pattern matching using % symbol")]
        [DefaultValue(true)]
        public bool EnableWildcards { get; set; } = true;

        /// <summary>
        /// Gets or sets whether to show loading indicator for slow data sources.
        /// Loading indicator appears if data loading exceeds LoadingThresholdMs.
        /// Default: true.
        /// </summary>
        [Category("Appearance")]
        [Description("Show loading indicator for slow data sources")]
        [DefaultValue(true)]
        public bool ShowLoadingIndicator { get; set; } = true;

        /// <summary>
        /// Gets or sets the delay (milliseconds) before loading indicator appears.
        /// Default: 100ms.
        /// </summary>
        [Category("Behavior")]
        [Description("Delay before loading indicator appears (milliseconds)")]
        [DefaultValue(100)]
        public int LoadingThresholdMs { get; set; } = 100;

        /// <summary>
        /// Gets or sets whether to suppress overlay for exact matches.
        /// If user types exact match of existing value, no overlay is shown.
        /// Default: true.
        /// </summary>
        [Category("Behavior")]
        [Description("Suppress overlay when user types exact match")]
        [DefaultValue(true)]
        public bool SuppressExactMatch { get; set; } = true;

        /// <summary>
        /// Gets or sets the action to take when no matches are found.
        /// Default: ShowWarningAndClear.
        /// </summary>
        [Category("Behavior")]
        [Description("Action to take when no suggestions match")]
        [DefaultValue(Enum_SuggestionNoMatchAction.ShowWarningAndClear)]
        public Enum_SuggestionNoMatchAction NoMatchAction { get; set; } = Enum_SuggestionNoMatchAction.ShowWarningAndClear;

        /// <summary>
        /// Gets or sets the action to take when a suggestion is selected.
        /// Default: MoveFocusToNextControl.
        /// </summary>
        [Category("Behavior")]
        [Description("Action to take when a suggestion is selected")]
        [DefaultValue(Enum_SuggestionSelectionAction.MoveFocusToNextControl)]
        public Enum_SuggestionSelectionAction SelectionAction { get; set; } = Enum_SuggestionSelectionAction.MoveFocusToNextControl;

        /// <summary>
        /// Gets or sets whether to clear field when no matches found.
        /// Follows MTM validation pattern: invalid input is cleared.
        /// Default: true.
        /// </summary>
        [Category("Behavior")]
        [Description("Clear field when no suggestions match (MTM pattern)")]
        [DefaultValue(true)]
        [Browsable(false)]
        [Obsolete("Use NoMatchAction instead")]
        public bool ClearOnNoMatch
        {
            get => NoMatchAction == Enum_SuggestionNoMatchAction.ShowWarningAndClear || NoMatchAction == Enum_SuggestionNoMatchAction.ClearField;
            set => NoMatchAction = value ? Enum_SuggestionNoMatchAction.ShowWarningAndClear : Enum_SuggestionNoMatchAction.None;
        }

        /// <summary>
        /// Gets or sets the minimum input length before triggering suggestions.
        /// If text length is below this value, no overlay is shown.
        /// Default: 0 (no minimum).
        /// </summary>
        [Category("Behavior")]
        [Description("Minimum characters required before showing suggestions")]
        [DefaultValue(0)]
        public int MinimumInputLength { get; set; } = 0;

        /// <summary>
        /// Gets whether the suggestion overlay is currently visible.
        /// </summary>
        [Browsable(false)]
        public bool IsOverlayVisible => _isOverlayVisible;

        /// <summary>
        /// Provides access to the inner TextBox used for rendering.
        /// </summary>
        internal TextBox InnerTextBox => SuggestionTextBox_TextBox;


               
        /// <inheritdoc />
        [Browsable(true)]
        [Category("Appearance")]
        [AllowNull]
        public override string Text
        {
            get => SuggestionTextBox_TextBox.Text;
            set => SuggestionTextBox_TextBox.Text = value ?? string.Empty;
        }


        /// <inheritdoc />
        [AllowNull]
        [Category("Appearance")]
        public override Font Font
        {
            get => SuggestionTextBox_TextBox.Font;
            set => SuggestionTextBox_TextBox.Font = value ?? SuggestionTextBox_TextBox.Font;
        }

        /// <inheritdoc />
        [Category("Appearance")]
        public override Color BackColor
        {
            get => SuggestionTextBox_TextBox.BackColor;
            set => SuggestionTextBox_TextBox.BackColor = value;
        }

        /// <inheritdoc />
        [Category("Appearance")]
        public override Color ForeColor
        {
            get => SuggestionTextBox_TextBox.ForeColor;
            set => SuggestionTextBox_TextBox.ForeColor = value;
        }

        /// <summary>
        /// Gets or sets the border style applied to the inner text box.
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(BorderStyle.FixedSingle)]
        public new BorderStyle BorderStyle
        {
            get => SuggestionTextBox_TextBox.BorderStyle;
            set => SuggestionTextBox_TextBox.BorderStyle = value;
        }

        /// <summary>
        /// Gets or sets whether the inner text box is read-only.
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(false)]
        public bool ReadOnly
        {
            get => SuggestionTextBox_TextBox.ReadOnly;
            set => SuggestionTextBox_TextBox.ReadOnly = value;
        }

        /// <summary>
        /// Gets or sets whether the placeholder text remains visible when the control has focus
        /// (until the user starts typing).
        /// </summary>
        [Category("Appearance")]
        [Description("Keep placeholder text visible when control has focus")]
        [DefaultValue(true)]
        public bool KeepPlaceholderOnFocus { get; set; } = true;

        /// <summary>
        /// Gets or sets the placeholder text shown when the control is empty.
        /// </summary>
        [Category("Appearance")]
        [DefaultValue("")]
        public string PlaceholderText
        {
            get => SuggestionTextBox_TextBox.PlaceholderText;
            set
            {
                SuggestionTextBox_TextBox.PlaceholderText = value;
                UpdateCueBanner();
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Component_SuggestionTextBox"/> class.
        /// </summary>
        public Component_SuggestionTextBox()
        {
            InitializeComponent();
            WireInnerTextBoxEvents();
        }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when user selects a suggestion from the overlay.
        /// Text property is updated before this event fires.
        /// </summary>
        [Category("Action")]
        [Description("Occurs when user selects a suggestion")]
        public event EventHandler<EventArgs_SuggestionSelectedEventArgs>? SuggestionSelected;

        /// <summary>
        /// Occurs when user cancels suggestion selection.
        /// Text property is preserved (not changed).
        /// </summary>
        [Category("Action")]
        [Description("Occurs when user cancels suggestion selection")]
        public event EventHandler<EventArgs_SuggestionCancelledEventArgs>? SuggestionCancelled;

        /// <summary>
        /// Occurs when suggestion overlay is opened.
        /// </summary>
        [Category("Action")]
        [Description("Occurs when suggestion overlay opens")]
        public event EventHandler<EventArgs>? SuggestionOverlayOpened;



        /// <summary>
        /// Occurs when suggestion overlay is closed (any reason).
        /// </summary>
        [Category("Action")]
        [Description("Occurs when suggestion overlay closes")]
        public event EventHandler<EventArgs>? SuggestionOverlayClosed;

        #endregion

        #region Methods

        private void ConfigureDataSource()
        {
            switch (_suggestionDataSource)
            {
                case Enum_SuggestionDataSource.MTM_PartNumber:
                    Helper_SuggestionTextBox.ConfigureForPartNumbers(this, Helper_SuggestionTextBox.GetCachedPartNumbersAsync, enableF4: true);
                    break;
                case Enum_SuggestionDataSource.MTM_Operation:
                    Helper_SuggestionTextBox.ConfigureForOperations(this, Helper_SuggestionTextBox.GetCachedOperationsAsync, enableF4: true);
                    break;
                case Enum_SuggestionDataSource.MTM_Location:
                    Helper_SuggestionTextBox.ConfigureForLocations(this, Helper_SuggestionTextBox.GetCachedLocationsAsync, enableF4: true);
                    break;
                case Enum_SuggestionDataSource.MTM_Color:
                    Helper_SuggestionTextBox.ConfigureForColorCodes(this, Helper_SuggestionTextBox.GetCachedColorsAsync, enableF4: true);
                    break;
                case Enum_SuggestionDataSource.MTM_User:
                    Helper_SuggestionTextBox.ConfigureForUsers(this, Helper_SuggestionTextBox.GetCachedUsersAsync, enableF4: true);
                    break;
                // InforVisual types
                case Enum_SuggestionDataSource.Infor_PartNumber:
                    Helper_SuggestionTextBox.ConfigureForInforPartNumbers(this, enableF4: true);
                    break;
                case Enum_SuggestionDataSource.Infor_User:
                    Helper_SuggestionTextBox.ConfigureForInforUsers(this, enableF4: true);
                    break;
                case Enum_SuggestionDataSource.Infor_Location:
                    Helper_SuggestionTextBox.ConfigureForInforLocations(this, enableF4: true);
                    break;
                case Enum_SuggestionDataSource.Infor_Warehouse:
                    Helper_SuggestionTextBox.ConfigureForInforWarehouses(this, enableF4: true);
                    break;
                case Enum_SuggestionDataSource.Infor_Operation:
                    // Not implemented yet
                    break;
                case Enum_SuggestionDataSource.Infor_PONumber:
                    Helper_SuggestionTextBox.ConfigureForInforPurchaseOrders(this, enableF4: true);
                    break;
                case Enum_SuggestionDataSource.Infor_CONumber:
                    Helper_SuggestionTextBox.ConfigureForInforCustomerOrders(this, enableF4: true);
                    break;
                case Enum_SuggestionDataSource.Infor_WONumber:
                    Helper_SuggestionTextBox.ConfigureForInforWorkOrders(this, enableF4: true);
                    break;
                case Enum_SuggestionDataSource.Infor_FGTNumber:
                    Helper_SuggestionTextBox.ConfigureForInforFGTNumbers(this, enableF4: true);
                    break;
                case Enum_SuggestionDataSource.Infor_MMCNumber:
                case Enum_SuggestionDataSource.Infor_MMFNumber:
                    Helper_SuggestionTextBox.ConfigureForInforCoilFlatstockNumbers(this, enableF4: true);
                    break;
                case Enum_SuggestionDataSource.None:
                default:
                    break;
            }
        }

            private void WireInnerTextBoxEvents()
            {
                SuggestionTextBox_TextBox.LostFocus += InnerTextBox_LostFocus;
                SuggestionTextBox_TextBox.KeyDown += InnerTextBox_KeyDown;
                SuggestionTextBox_TextBox.Leave += InnerTextBox_Leave;
                SuggestionTextBox_TextBox.KeyPress += InnerTextBox_KeyPress;
                SuggestionTextBox_TextBox.KeyUp += InnerTextBox_KeyUp;
                SuggestionTextBox_TextBox.TextChanged += InnerTextBox_TextChanged;
                SuggestionTextBox_TextBox.Click += InnerTextBox_Click;
                SuggestionTextBox_TextBox.DoubleClick += InnerTextBox_DoubleClick;
                HandleCreated += (s, e) => UpdateCueBanner();
                SuggestionTextBox_TextBox.HandleCreated += (s, e) => UpdateCueBanner();
            }
private void UpdateCueBanner()
        {
            if (SuggestionTextBox_TextBox.IsHandleCreated && !string.IsNullOrEmpty(SuggestionTextBox_TextBox.PlaceholderText))
            {
                // wParam = 1 (TRUE) to show when focused
                SendMessage(SuggestionTextBox_TextBox.Handle, EM_SETCUEBANNER, (IntPtr)(KeepPlaceholderOnFocus ? 1 : 0), SuggestionTextBox_TextBox.PlaceholderText);
            }
        }

        
        /// <summary>
        /// Manually triggers suggestion display for current Text value.
        /// Useful for programmatically showing suggestions without focus change.
        /// </summary>
        /// <returns>Task that completes when overlay is closed.</returns>
        /// <exception cref="InvalidOperationException">If DataProvider is null.</exception>
        public async Task ShowSuggestionsAsync()
        {
            if (DataProvider == null)
                throw new InvalidOperationException("DataProvider must be set before showing suggestions");

            await ShowSuggestionOverlayAsync();
        }

        /// <summary>
        /// Manually raises the SuggestionSelected event.
        /// Used by helper methods to trigger event after programmatic selection.
        /// </summary>
        /// <param name="e">Event arguments</param>
        public void RaiseSuggestionSelectedEvent(EventArgs_SuggestionSelectedEventArgs e)
        {
            OnSuggestionSelected(e);
        }

        /// <summary>
        /// Clears any cached data source (if caching is implemented).
        /// Forces next trigger to re-query DataProvider.
        /// </summary>
        public void RefreshDataSource()
        {
            _lastFilteredResults = null;
        }

        /// <summary>
        /// Clears the current text value.
        /// </summary>
        public void Clear()
        {
            SuggestionTextBox_TextBox.Clear();
        }

        /// <summary>
        /// Selects all text inside the inner text box.
        /// </summary>
        public void SelectAll()
        {
            SuggestionTextBox_TextBox.SelectAll();
        }

        /// <summary>
        /// Applies configuration from Model_Suggestion_Config.
        /// Convenience method to set multiple properties at once.
        /// </summary>
        /// <param name="config">Configuration model.</param>
        /// <exception cref="ArgumentNullException">If config is null.</exception>
        public void ApplyConfig(Model_Suggestion_Config config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            config.Validate();

            this.DataProvider = config.DataProvider;
            this.MaxResults = config.MaxResults;
            this.EnableWildcards = config.EnableWildcards;
            this.ShowLoadingIndicator = config.ShowLoadingIndicator;
            this.LoadingThresholdMs = config.LoadingThresholdMs;
            this.SuppressExactMatch = config.SuppressExactMatch;
            this.NoMatchAction = config.NoMatchAction;
            this.MinimumInputLength = config.MinimumInputLength;
        }

        #endregion

        #region Overrides

        /// <inheritdoc />
        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            SuggestionTextBox_TextBox.Focus();
        }

        /// <inheritdoc />
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            if (!SuggestionTextBox_TextBox.Focused)
            {
                SuggestionTextBox_TextBox.Focus();
            }
        }

        /// <inheritdoc />
        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            SuggestionTextBox_TextBox.Font = this.Font;
        }

        /// <inheritdoc />
        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            SuggestionTextBox_TextBox.BackColor = this.BackColor;
        }

        /// <inheritdoc />
        protected override void OnForeColorChanged(EventArgs e)
        {
            base.OnForeColorChanged(e);
            SuggestionTextBox_TextBox.ForeColor = this.ForeColor;
        }

        /// <inheritdoc />
        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            SuggestionTextBox_TextBox.Enabled = this.Enabled;
        }

        /// <summary>
        /// Moves the focus to the inner text box.
        /// </summary>
        public new bool Focus()
        {
            return SuggestionTextBox_TextBox.Focus();
        }

        /// <inheritdoc />
        protected override void OnTabIndexChanged(EventArgs e)
        {
            base.OnTabIndexChanged(e);
            SuggestionTextBox_TextBox.TabIndex = this.TabIndex;
        }

        /// <inheritdoc />
        protected override void OnTabStopChanged(EventArgs e)
        {
            base.OnTabStopChanged(e);
            SuggestionTextBox_TextBox.TabStop = this.TabStop;
        }

        /// <summary>
        /// Disposes resources used by SuggestionTextBox.
        /// Ensures overlay form is disposed if still open.
        /// </summary>
        /// <param name="disposing">True if managed resources should be disposed.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();

                if (_currentOverlay != null && !_currentOverlay.IsDisposed)
                {
                    _currentOverlay.Dispose();
                    _currentOverlay = null;
                }
            }

            base.Dispose(disposing);
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Shows the suggestion overlay with filtered results.
        /// </summary>
        private async Task ShowSuggestionOverlayAsync()
        {
            try
            {
                // Store original input
                _originalInput = this.Text;

                // Check for empty input
                if (string.IsNullOrWhiteSpace(_originalInput))
                {
                    OnSuggestionCancelled(new EventArgs_SuggestionCancelledEventArgs
                    {
                        OriginalInput = _originalInput,
                        Reason = SuggestionCancelReason.EmptyInput,
                        FieldName = this.Name
                    });
                    return;
                }

                // Invoke data provider
                var allSuggestions = await DataProvider!.Invoke();

                if (allSuggestions == null || allSuggestions.Count == 0)
                {
                    HandleNoMatches();
                    return;
                }

                // Clean source list
                allSuggestions = Service_SuggestionFilter.CleanSourceList(allSuggestions);

                // Filter suggestions
                var filteredSuggestions = Service_SuggestionFilter.FilterSuggestions(
                    allSuggestions,
                    _originalInput,
                    MaxResults,
                    EnableWildcards);

                // Check if exact match (overlay suppressed)
                if (SuppressExactMatch && filteredSuggestions.Count == 0 &&
                    Service_SuggestionFilter.IsExactMatch(allSuggestions, _originalInput))
                {
                    HandleExactMatchWithoutOverlay(allSuggestions);
                    return;
                }

                // Check for no matches
                if (filteredSuggestions.Count == 0)
                {
                    HandleNoMatches();
                    return;
                }

                // Cache filtered results
                _lastFilteredResults = filteredSuggestions;

                // Show overlay
                DisplayOverlay(filteredSuggestions);
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(
                    ex,
                    Enum_ErrorSeverity.Medium,
                    contextData: new Dictionary<string, object>
                    {
                        ["Control"] = this.Name,
                        ["Input"] = _originalInput
                    },
                    callerName: nameof(ShowSuggestionOverlayAsync));
            }
        }

        /// <summary>
        /// Displays the overlay form with suggestions.
        /// </summary>
        private void DisplayOverlay(List<string> suggestions)
        {
            try
            {
                _isOverlayVisible = true;
                OnSuggestionOverlayOpened(EventArgs.Empty);



                _currentOverlay = new SuggestionOverlayForm(suggestions, SuggestionTextBox_TextBox);

                var parentForm = this.FindForm();
                if (parentForm == null)
                {
                    LoggingUtility.LogApplicationError(new InvalidOperationException($"[SuggestionTextBox] Cannot find parent form for control: {this.Name}"));
                    _currentOverlay.Dispose();
                    _currentOverlay = null;
                    _isOverlayVisible = false;
                    return;
                }


                var result = _currentOverlay.ShowDialog(parentForm);


                // Capture selected item IMMEDIATELY after dialog closes (before any disposal)
                string? selectedValue = null;
                try
                {
                    selectedValue = _currentOverlay?.SelectedItem;

                }
                catch (Exception ex)
                {
                    LoggingUtility.LogApplicationError(ex);
                }

                // Dispose overlay safely
                try
                {
                    _currentOverlay?.Dispose();
                }
                catch (Exception ex)
                {
                    LoggingUtility.LogApplicationError(ex);
                }
                finally
                {
                    _currentOverlay = null;
                }

                if (result == DialogResult.OK && selectedValue != null)
                {
                    // User accepted selection - update TextBox with capitalized value


                    this.Text = selectedValue.ToUpperInvariant();



                    OnSuggestionSelected(new EventArgs_SuggestionSelectedEventArgs
                    {
                        OriginalInput = _originalInput,
                        SelectedValue = selectedValue,
                        SelectionIndex = 0, // Index not available after disposal
                        FieldName = this.Name
                    });

                    // Handle Selection Action
                    if (SelectionAction == Enum_SuggestionSelectionAction.MoveFocusToNextControl)
                    {
                        // Move focus to next control
                        var nextControlFocused = this.FindForm()?.SelectNextControl(this, forward: true, tabStopOnly: true, nested: true, wrap: false);
                    }
                }
                else
                {
                    // User cancelled - restore focus to this field


                    OnSuggestionCancelled(new EventArgs_SuggestionCancelledEventArgs
                    {
                        OriginalInput = _originalInput,
                        Reason = SuggestionCancelReason.Escape,
                        FieldName = this.Name
                    });



                    // Ensure focus returns to this field (prevents focus moving to next field)
                    SuggestionTextBox_TextBox.Focus();

                }
            }
            finally
            {
                _isOverlayVisible = false;
                OnSuggestionOverlayClosed(EventArgs.Empty);

            }
        }

        /// <summary>
        /// Handles scenario when no matches found.
        /// </summary>
        private void HandleNoMatches()
        {
            OnSuggestionCancelled(new EventArgs_SuggestionCancelledEventArgs
            {
                OriginalInput = _originalInput,
                Reason = SuggestionCancelReason.NoMatches,
                FieldName = this.Name
            });

            if (NoMatchAction == Enum_SuggestionNoMatchAction.ShowWarningAndClear)
            {
                this.Text = string.Empty;
                Service_ErrorHandler.ShowWarning($"No matching values found for '{_originalInput}'");
            }
            else if (NoMatchAction == Enum_SuggestionNoMatchAction.ClearField)
            {
                this.Text = string.Empty;
            }
        }

        /// <summary>
        /// Raises the SuggestionSelected event.
        /// </summary>
        protected virtual void OnSuggestionSelected(EventArgs_SuggestionSelectedEventArgs e)
        {
            SuggestionSelected?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the SuggestionCancelled event.
        /// </summary>
        protected virtual void OnSuggestionCancelled(EventArgs_SuggestionCancelledEventArgs e)
        {
            SuggestionCancelled?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the SuggestionOverlayOpened event.
        /// </summary>
        protected virtual void OnSuggestionOverlayOpened(EventArgs e)
        {
            SuggestionOverlayOpened?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the SuggestionOverlayClosed event.
        /// </summary>
        protected virtual void OnSuggestionOverlayClosed(EventArgs e)
        {
            SuggestionOverlayClosed?.Invoke(this, e);
        }

        #endregion

        #region Inner TextBox Event Handlers

        private async void InnerTextBox_LostFocus(object? sender, EventArgs e)
        {
            base.OnLostFocus(e);

            if (DataProvider == null)
            {
                return;
            }

            if (_isOverlayVisible)
            {
                return;
            }

            if (Text.Length < MinimumInputLength)
            {
                return;
            }

            await ShowSuggestionOverlayAsync();
        }

        private void InnerTextBox_KeyDown(object? sender, KeyEventArgs e)
        {
            if (_isOverlayVisible && _currentOverlay != null)
            {
                switch (e.KeyCode)
                {
                    case Keys.Down:
                        _currentOverlay.SelectNext();
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        return;

                    case Keys.Up:
                        _currentOverlay.SelectPrevious();
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        return;

                    case Keys.Enter:
                        _currentOverlay.AcceptSelection();
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        return;

                    case Keys.Escape:
                        _currentOverlay.CancelSelection();
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        return;

                    case Keys.Home:
                        _currentOverlay.SelectFirst();
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        return;

                    case Keys.End:
                        _currentOverlay.SelectLast();
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        return;
                }
            }

            base.OnKeyDown(e);
        }

        private void InnerTextBox_Leave(object? sender, EventArgs e)
        {
            base.OnLeave(e);

            if (!string.IsNullOrWhiteSpace(Text) && !_isOverlayVisible)
            {
                int cursorPosition = SuggestionTextBox_TextBox.SelectionStart;

                Text = Text.ToUpperInvariant();

                if (cursorPosition <= Text.Length)
                {
                    SuggestionTextBox_TextBox.SelectionStart = cursorPosition;
                }
            }
        }

        private void InnerTextBox_KeyPress(object? sender, KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
        }

        private void InnerTextBox_KeyUp(object? sender, KeyEventArgs e)
        {
            base.OnKeyUp(e);
        }

        private void InnerTextBox_TextChanged(object? sender, EventArgs e)
        {
            base.OnTextChanged(e);
        }

        private void InnerTextBox_Click(object? sender, EventArgs e)
        {
            base.OnClick(e);
        }

        private void InnerTextBox_DoubleClick(object? sender, EventArgs e)
        {
            base.OnDoubleClick(e);
        }

        #endregion

        #region Exact Match Handling

        /// <summary>
        /// Treats manually typed exact matches like overlay selections so downstream handlers fire.
        /// </summary>
        /// <param name="source">Complete suggestion list.</param>
        private void HandleExactMatchWithoutOverlay(List<string> source)
        {
            if (source == null || string.IsNullOrWhiteSpace(_originalInput))
            {
                return;
            }

            var matchedValue = source.FirstOrDefault(s => s.Equals(_originalInput, StringComparison.OrdinalIgnoreCase));
            if (matchedValue == null)
            {
                return;
            }

            var normalizedValue = matchedValue.ToUpperInvariant();
            if (!string.Equals(Text, normalizedValue, StringComparison.Ordinal))
            {
                Text = normalizedValue;
            }

            var selectionIndex = source.FindIndex(s => s.Equals(matchedValue, StringComparison.OrdinalIgnoreCase));
            OnSuggestionSelected(new EventArgs_SuggestionSelectedEventArgs
            {
                OriginalInput = _originalInput,
                SelectedValue = matchedValue,
                SelectionIndex = selectionIndex < 0 ? 0 : selectionIndex,
                FieldName = this.Name
            });
        }

        #endregion
    }
}
