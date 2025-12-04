namespace MTM_WIP_Application_Winforms.Models
{
    #region Model_ErrorReport_Core_Filter

    /// <summary>
    /// Represents filter criteria used when querying error reports for the View Error Reports window.
    /// Provides normalization helpers and validation to keep database queries predictable.
    /// </summary>
    internal sealed class Model_ErrorReport_Core_Filter
    {
        #region Fields

        private string? _userName;
        private string? _machineName;
        private string? _status;
        private string? _searchText;

        #endregion

        #region Properties

        /// <summary>
        /// Inclusive starting boundary for the report date filter. Null disables the lower bound.
        /// </summary>
        public DateTime? DateFrom { get; set; }

        /// <summary>
        /// Inclusive ending boundary for the report date filter. Null disables the upper bound.
        /// </summary>
        public DateTime? DateTo { get; set; }

        /// <summary>
        /// Specific username to filter against. Null indicates no user filter.
        /// </summary>
        public string? UserName
        {
            get => _userName;
            set => _userName = Normalize(value);
        }

        /// <summary>
        /// Specific machine name to filter against. Null indicates no machine filter.
        /// </summary>
        public string? MachineName
        {
            get => _machineName;
            set => _machineName = Normalize(value);
        }

        /// <summary>
        /// Status filter. Accepts "New", "Reviewed", or "Resolved". Null removes the status filter.
        /// </summary>
        public string? Status
        {
            get => _status;
            set => _status = Normalize(value);
        }

        /// <summary>
        /// Free-form search text applied across summary, user notes, and technical details.
        /// Null or whitespace disables the search filter.
        /// </summary>
        public string? SearchText
        {
            get => _searchText;
            set => _searchText = Normalize(value);
        }

        /// <summary>
        /// Indicates whether any filter value is currently applied. Helpful for UI state management.
        /// </summary>
        public bool HasFilters =>
            DateFrom.HasValue ||
            DateTo.HasValue ||
            !string.IsNullOrEmpty(_userName) ||
            !string.IsNullOrEmpty(_machineName) ||
            !string.IsNullOrEmpty(_status) ||
            !string.IsNullOrEmpty(_searchText);

        /// <summary>
        /// Indicates whether the search filter should be applied.
        /// </summary>
        public bool HasSearchText => !string.IsNullOrEmpty(_searchText);

        #endregion

        #region Validation

        /// <summary>
        /// Validates the current filter values to ensure they meet UI and database expectations.
        /// </summary>
        /// <param name="validationMessage">Provides a user-friendly message when validation fails.</param>
        /// <returns>True when the filter values are valid, otherwise false.</returns>
        public bool TryValidate(out string? validationMessage)
        {
            if (DateFrom.HasValue && DateTo.HasValue && DateFrom > DateTo)
            {
                validationMessage = "Date From must be earlier than or equal to Date To.";
                return false;
            }

            if (!string.IsNullOrEmpty(_status) && _status is not ("New" or "Reviewed" or "Resolved"))
            {
                validationMessage = "Status filter must be New, Reviewed, or Resolved.";
                return false;
            }

            if (!string.IsNullOrEmpty(_searchText) && _searchText!.Length < 3)
            {
                validationMessage = "Search text must be at least 3 characters long.";
                return false;
            }

            validationMessage = null;
            return true;
        }

        #endregion

        #region Helpers

        private static string? Normalize(string? value) => string.IsNullOrWhiteSpace(value) ? null : value.Trim();

        #endregion
    }

    #endregion
}
