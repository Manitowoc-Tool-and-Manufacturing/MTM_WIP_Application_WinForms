using System;

namespace MTM_WIP_Application_Winforms.Models
{
    /// <summary>
    /// Event arguments for SuggestionCancelled event.
    /// </summary>
    public class SuggestionCancelledEventArgs : EventArgs
    {
        #region Properties

        /// <summary>
        /// Gets the user's original typed text (preserved, not changed).
        /// </summary>
        public string OriginalInput { get; set; } = string.Empty;

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
        public string FieldName { get; set; } = string.Empty;

        #endregion
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
}
