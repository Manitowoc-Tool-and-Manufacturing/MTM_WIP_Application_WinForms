namespace MTM_WIP_Application_Winforms.Models.Enums
{
    /// <summary>
    /// Defines the action to take when no matching suggestions are found.
    /// </summary>
    public enum Enum_SuggestionNoMatchAction
    {
        /// <summary>
        /// Do nothing (keep invalid text).
        /// </summary>
        None,

        /// <summary>
        /// Clear the field silently.
        /// </summary>
        ClearField,

        /// <summary>
        /// Show a warning message and clear the field.
        /// </summary>
        ShowWarningAndClear
    }
}
