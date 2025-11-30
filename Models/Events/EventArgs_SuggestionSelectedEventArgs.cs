namespace MTM_WIP_Application_Winforms.Models
{
    /// <summary>
    /// Event arguments for SuggestionSelected event.
    /// </summary>
    public class EventArgs_SuggestionSelectedEventArgs : EventArgs
    {
        #region Properties

        /// <summary>
        /// Gets the user's original typed text before selection.
        /// </summary>
        public string OriginalInput { get; set; } = string.Empty;

        /// <summary>
        /// Gets the selected suggestion value.
        /// </summary>
        public string SelectedValue { get; set; } = string.Empty;

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
        public string FieldName { get; set; } = string.Empty;

        #endregion
    }
}
