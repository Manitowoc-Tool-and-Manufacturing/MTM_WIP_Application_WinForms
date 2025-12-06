// Contract: SuggestionTextBox Public API
// Purpose: Enhanced TextBox control with suggestion/autocomplete support
// Base Class: System.Windows.Forms.TextBox

namespace MTM_WIP_Application_Winforms.Components.Shared
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using System.Windows.Forms;

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
        #region Properties

        /// <summary>
        /// Gets or sets the async data provider that returns suggestion strings.
        /// Invoked when user exits field (LostFocus) with non-empty text.
        /// </summary>
        /// <example>
        /// <code>
        /// suggestionTextBox1.DataProvider = async () =>
        /// {
        ///     var dao = new Dao_Part();
        ///     var result = await dao.GetAllPartIDsAsync();
        ///     return result.IsSuccess 
        ///         ? result.Data.AsEnumerable().Select(r => r["PartID"].ToString()).ToList()
        ///         : new List&lt;string&gt;();
        /// };
        /// </code>
        /// </example>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Func<Task<List<string>>> DataProvider { get; set; }

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
        public bool IsOverlayVisible { get; private set; }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when user selects a suggestion from the overlay.
        /// Text property is updated before this event fires.
        /// </summary>
        [Category("Action")]
        [Description("Occurs when user selects a suggestion")]
        public event EventHandler<SuggestionSelectedEventArgs> SuggestionSelected;

        /// <summary>
        /// Occurs when user cancels suggestion selection.
        /// Text property is preserved (not changed).
        /// </summary>
        [Category("Action")]
        [Description("Occurs when user cancels suggestion selection")]
        public event EventHandler<SuggestionCancelledEventArgs> SuggestionCancelled;

        /// <summary>
        /// Occurs when suggestion overlay is opened.
        /// </summary>
        [Category("Action")]
        [Description("Occurs when suggestion overlay opens")]
        public event EventHandler<EventArgs> SuggestionOverlayOpened;

        /// <summary>
        /// Occurs when suggestion overlay is closed (any reason).
        /// </summary>
        [Category("Action")]
        [Description("Occurs when suggestion overlay closes")]
        public event EventHandler<EventArgs> SuggestionOverlayClosed;

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
            // Implementation will validate DataProvider != null,
            // invoke provider, filter results, and display overlay
            throw new NotImplementedException("Will be implemented in Phase 2");
        }

        /// <summary>
        /// Clears any cached data source (if caching is implemented).
        /// Forces next trigger to re-query DataProvider.
        /// </summary>
        public void RefreshDataSource()
        {
            // Implementation will clear _cachedData field
            throw new NotImplementedException("Will be implemented in Phase 2");
        }

        /// <summary>
        /// Applies configuration from Model_Suggestion_Config.
        /// Convenience method to set multiple properties at once.
        /// </summary>
        /// <param name="config">Configuration model.</param>
        /// <exception cref="ArgumentNullException">If config is null.</exception>
        public void ApplyConfig(Model_Suggestion_Config config)
        {
            // Implementation will set all properties from config
            throw new NotImplementedException("Will be implemented in Phase 2");
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Handles LostFocus event to trigger suggestion overlay.
        /// This is the primary trigger point for suggestion display.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        protected override void OnLostFocus(EventArgs e)
        {
            // Implementation will:
            // 1. Check MinimumInputLength
            // 2. Invoke DataProvider (async)
            // 3. Filter results via Service_SuggestionFilter
            // 4. Display SuggestionOverlayForm if matches exist
            throw new NotImplementedException("Will be implemented in Phase 2");
        }

        /// <summary>
        /// Handles keyboard input to intercept navigation keys when overlay is visible.
        /// Arrow keys, Enter, Escape, Home, End are intercepted and delegated to overlay.
        /// </summary>
        /// <param name="e">Key event arguments.</param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            // Implementation will:
            // - If overlay visible: intercept arrow keys, Enter, Escape, Home, End
            // - Delegate to overlay form methods (SelectNext, AcceptSelection, etc.)
            // - Set e.Handled = true and e.SuppressKeyPress = true
            // - Otherwise: call base.OnKeyDown(e)
            throw new NotImplementedException("Will be implemented in Phase 2");
        }

        /// <summary>
        /// Disposes resources used by SuggestionTextBox.
        /// Ensures overlay form is disposed if still open.
        /// </summary>
        /// <param name="disposing">True if managed resources should be disposed.</param>
        protected override void Dispose(bool disposing)
        {
            // Implementation will dispose overlay if open
            throw new NotImplementedException("Will be implemented in Phase 2");
        }

        #endregion
    }

    #region Event Args

    /// <summary>
    /// Event arguments for SuggestionSelected event.
    /// </summary>
    public class SuggestionSelectedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the user's original typed text before selection.
        /// </summary>
        public string OriginalInput { get; set; }

        /// <summary>
        /// Gets the selected suggestion value.
        /// </summary>
        public string SelectedValue { get; set; }

        /// <summary>
        /// Gets the index of the selected item in the filtered list.
        /// </summary>
        public int SelectionIndex { get; set; }

        /// <summary>
        /// Gets the timestamp when selection occurred.
        /// </summary>
        public DateTime Timestamp { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets the name of the TextBox control (for logging).
        /// </summary>
        public string FieldName { get; set; }
    }

    /// <summary>
    /// Event arguments for SuggestionCancelled event.
    /// </summary>
    public class SuggestionCancelledEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the user's original typed text (preserved, not changed).
        /// </summary>
        public string OriginalInput { get; set; }

        /// <summary>
        /// Gets the reason why selection was cancelled.
        /// </summary>
        public SuggestionCancelReason Reason { get; set; }

        /// <summary>
        /// Gets the timestamp when cancellation occurred.
        /// </summary>
        public DateTime Timestamp { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets the name of the TextBox control (for logging).
        /// </summary>
        public string FieldName { get; set; }
    }

    /// <summary>
    /// Reason why suggestion selection was cancelled.
    /// </summary>
    public enum SuggestionCancelReason
    {
        /// <summary>User pressed Escape key.</summary>
        Escape,

        /// <summary>User clicked outside overlay (light dismiss).</summary>
        ClickOutside,

        /// <summary>No matching suggestions found (auto-cancelled).</summary>
        NoMatches,

        /// <summary>User typed nothing (no overlay shown).</summary>
        EmptyInput
    }

    #endregion
}
