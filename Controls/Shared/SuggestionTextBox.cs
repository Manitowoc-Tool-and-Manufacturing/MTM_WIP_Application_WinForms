using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;

namespace MTM_WIP_Application_Winforms.Controls.Shared
{
    /// <summary>
    /// Enhanced TextBox control that provides intelligent suggestion/autocomplete
    /// functionality when user exits the field. Supports any data source via
    /// delegate pattern, wildcard filtering, and keyboard/mouse navigation.
    /// </summary>
    [DesignerCategory("Component")]
    [ToolboxItem(true)]
    [Description("TextBox with intelligent suggestion/autocomplete support")]
    public class SuggestionTextBox : TextBox
    {
        #region Fields

        private SuggestionOverlayForm? _currentOverlay;
        private bool _isOverlayVisible;
        private List<string>? _lastFilteredResults;
        private string _originalInput = string.Empty;

        #endregion

        #region Properties

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
        /// Gets or sets whether to clear field when no matches found.
        /// Follows MTM validation pattern: invalid input is cleared.
        /// Default: true.
        /// </summary>
        [Category("Behavior")]
        [Description("Clear field when no suggestions match (MTM pattern)")]
        [DefaultValue(true)]
        public bool ClearOnNoMatch { get; set; } = true;

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

        #endregion

        #region Events

        /// <summary>
        /// Occurs when user selects a suggestion from the overlay.
        /// Text property is updated before this event fires.
        /// </summary>
        [Category("Action")]
        [Description("Occurs when user selects a suggestion")]
        public event EventHandler<SuggestionSelectedEventArgs>? SuggestionSelected;

        /// <summary>
        /// Occurs when user cancels suggestion selection.
        /// Text property is preserved (not changed).
        /// </summary>
        [Category("Action")]
        [Description("Occurs when user cancels suggestion selection")]
        public event EventHandler<SuggestionCancelledEventArgs>? SuggestionCancelled;

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
        public void RaiseSuggestionSelectedEvent(SuggestionSelectedEventArgs e)
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
            this.ClearOnNoMatch = config.ClearOnNoMatch;
            this.MinimumInputLength = config.MinimumInputLength;
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Handles LostFocus event to trigger suggestion overlay.
        /// This is the primary trigger point for suggestion display.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        protected override async void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);

            // Don't trigger if DataProvider not set
            if (DataProvider == null) return;

            // Don't trigger if overlay already visible
            if (_isOverlayVisible) return;

            // Check minimum input length
            if (this.Text.Length < MinimumInputLength) return;

            // Show suggestions
            await ShowSuggestionOverlayAsync();
        }

        /// <summary>
        /// Handles keyboard input to intercept navigation keys when overlay is visible.
        /// Arrow keys, Enter, Escape, Home, End are intercepted and delegated to overlay.
        /// </summary>
        /// <param name="e">Key event arguments.</param>
        protected override void OnKeyDown(KeyEventArgs e)
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

        /// <summary>
        /// Handles Leave event to capitalize valid input.
        /// If the user has entered text and no overlay is showing, capitalize it.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);

            // If there's text and we're not showing the overlay, capitalize it
            if (!string.IsNullOrWhiteSpace(this.Text) && !_isOverlayVisible)
            {
                // Store cursor position
                int cursorPosition = this.SelectionStart;
                
                // Capitalize the text
                this.Text = this.Text.ToUpperInvariant();
                
                // Restore cursor position (adjust if needed)
                if (cursorPosition <= this.Text.Length)
                {
                    this.SelectionStart = cursorPosition;
                }
            }
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
                    OnSuggestionCancelled(new SuggestionCancelledEventArgs
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

                LoggingUtility.Log($"[SuggestionTextBox] Overlay opened: Field={this.Name}, Matches={suggestions.Count}, Input='{_originalInput}'");

                _currentOverlay = new SuggestionOverlayForm(suggestions, this);
                
                var parentForm = this.FindForm();
                if (parentForm == null)
                {
                    LoggingUtility.LogApplicationError(new InvalidOperationException($"[SuggestionTextBox] Cannot find parent form for control: {this.Name}"));
                    _currentOverlay.Dispose();
                    _currentOverlay = null;
                    _isOverlayVisible = false;
                    return;
                }
                
                LoggingUtility.Log($"[SuggestionTextBox] About to show dialog");
                var result = _currentOverlay.ShowDialog(parentForm);
                LoggingUtility.Log($"[SuggestionTextBox] Dialog closed with result: {result}");

                // Capture selected item IMMEDIATELY after dialog closes (before any disposal)
                string? selectedValue = null;
                try
                {
                    selectedValue = _currentOverlay?.SelectedItem;
                    LoggingUtility.Log($"[SuggestionTextBox] Captured selectedValue: '{selectedValue}'");
                }
                catch (Exception ex)
                {
                    LoggingUtility.Log($"[SuggestionTextBox] ERROR capturing selectedValue: {ex.Message}");
                }

                // Dispose overlay safely
                try
                {
                    _currentOverlay?.Dispose();
                }
                catch (Exception ex)
                {
                    LoggingUtility.Log($"[SuggestionTextBox] ERROR disposing overlay: {ex.Message}");
                }
                finally
                {
                    _currentOverlay = null;
                }

                if (result == DialogResult.OK && selectedValue != null)
                {
                    // User accepted selection - update TextBox with capitalized value
                    LoggingUtility.Log($"[SuggestionTextBox] BEFORE text assignment: Field={this.Name}, Current Text='{this.Text}', Will set to='{selectedValue}'");
                    
                    this.Text = selectedValue.ToUpperInvariant();
                    
                    LoggingUtility.Log($"[SuggestionTextBox] AFTER text assignment: Field={this.Name}, Text is now='{this.Text}'");

                    OnSuggestionSelected(new SuggestionSelectedEventArgs
                    {
                        OriginalInput = _originalInput,
                        SelectedValue = selectedValue,
                        SelectionIndex = 0, // Index not available after disposal
                        FieldName = this.Name
                    });

                    LoggingUtility.Log($"[SuggestionTextBox] Suggestion selected event raised: Field={this.Name}, Value='{selectedValue}', Original='{_originalInput}'");

                    // Move focus to next control
                    var nextControlFocused = this.FindForm()?.SelectNextControl(this, forward: true, tabStopOnly: true, nested: true, wrap: false);
                    LoggingUtility.Log($"[SuggestionTextBox] Focus moved to next control: {nextControlFocused}");
                }
                else
                {
                    // User cancelled - restore focus to this field
                    LoggingUtility.Log($"[SuggestionTextBox] User cancelled - result={result}, selectedValue={selectedValue}");
                    
                    OnSuggestionCancelled(new SuggestionCancelledEventArgs
                    {
                        OriginalInput = _originalInput,
                        Reason = SuggestionCancelReason.Escape,
                        FieldName = this.Name
                    });

                    LoggingUtility.Log($"[SuggestionTextBox] Suggestion cancelled: Field={this.Name}, Reason=Escape, Input='{_originalInput}'");
                    
                    // Ensure focus returns to this field (prevents focus moving to next field)
                    this.Focus();
                    LoggingUtility.Log($"[SuggestionTextBox] Focus restored to field: {this.Name}");
                }
            }
            finally
            {
                _isOverlayVisible = false;
                OnSuggestionOverlayClosed(EventArgs.Empty);
                LoggingUtility.Log($"[SuggestionTextBox] Overlay closed finally block");
            }
        }

        /// <summary>
        /// Handles scenario when no matches found.
        /// </summary>
        private void HandleNoMatches()
        {
            OnSuggestionCancelled(new SuggestionCancelledEventArgs
            {
                OriginalInput = _originalInput,
                Reason = SuggestionCancelReason.NoMatches,
                FieldName = this.Name
            });

            LoggingUtility.Log($"No matches found: Field={this.Name}, Input='{_originalInput}'");

            if (ClearOnNoMatch)
            {
                this.Text = string.Empty;
                Service_ErrorHandler.ShowWarning($"No matching values found for '{_originalInput}'");
            }
        }

        /// <summary>
        /// Raises the SuggestionSelected event.
        /// </summary>
        protected virtual void OnSuggestionSelected(SuggestionSelectedEventArgs e)
        {
            SuggestionSelected?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the SuggestionCancelled event.
        /// </summary>
        protected virtual void OnSuggestionCancelled(SuggestionCancelledEventArgs e)
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
            OnSuggestionSelected(new SuggestionSelectedEventArgs
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
