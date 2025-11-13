using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MTM_WIP_Application_Winforms.Models
{
    /// <summary>
    /// Configuration model for SuggestionTextBox behavior.
    /// Provides convenient way to set multiple properties at once.
    /// </summary>
    public class Model_Suggestion_Config
    {
        #region Properties

        /// <summary>
        /// Gets or sets the async data provider that returns suggestion strings.
        /// Required field - must be set before using configuration.
        /// </summary>
        public Func<Task<List<string>>>? DataProvider { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of suggestions to display.
        /// Default: 100. Valid range: 1-1000.
        /// </summary>
        public int MaxResults { get; set; } = 100;

        /// <summary>
        /// Gets or sets whether wildcard pattern matching is enabled using % symbol.
        /// Default: true.
        /// </summary>
        public bool EnableWildcards { get; set; } = true;

        /// <summary>
        /// Gets or sets whether to show loading indicator for slow data sources.
        /// Default: true.
        /// </summary>
        public bool ShowLoadingIndicator { get; set; } = true;

        /// <summary>
        /// Gets or sets the delay (milliseconds) before loading indicator appears.
        /// Default: 100ms.
        /// </summary>
        public int LoadingThresholdMs { get; set; } = 100;

        /// <summary>
        /// Gets or sets whether to suppress overlay for exact matches.
        /// Default: true.
        /// </summary>
        public bool SuppressExactMatch { get; set; } = true;

        /// <summary>
        /// Gets or sets whether to clear field when no matches found.
        /// Default: true (MTM validation pattern).
        /// </summary>
        public bool ClearOnNoMatch { get; set; } = true;

        /// <summary>
        /// Gets or sets the minimum input length before triggering suggestions.
        /// Default: 0 (no minimum).
        /// </summary>
        public int MinimumInputLength { get; set; } = 0;

        #endregion

        #region Methods

        /// <summary>
        /// Validates configuration settings.
        /// </summary>
        /// <exception cref="InvalidOperationException">If configuration is invalid.</exception>
        public void Validate()
        {
            if (DataProvider == null)
                throw new InvalidOperationException("DataProvider is required");

            if (MaxResults < 1 || MaxResults > 1000)
                throw new InvalidOperationException("MaxResults must be between 1 and 1000");

            if (LoadingThresholdMs < 0)
                throw new InvalidOperationException("LoadingThresholdMs cannot be negative");

            if (MinimumInputLength < 0)
                throw new InvalidOperationException("MinimumInputLength cannot be negative");
        }

        /// <summary>
        /// Creates a copy of this configuration.
        /// </summary>
        /// <returns>New instance with same property values.</returns>
        public Model_Suggestion_Config Clone()
        {
            return new Model_Suggestion_Config
            {
                DataProvider = this.DataProvider,
                MaxResults = this.MaxResults,
                EnableWildcards = this.EnableWildcards,
                ShowLoadingIndicator = this.ShowLoadingIndicator,
                LoadingThresholdMs = this.LoadingThresholdMs,
                SuppressExactMatch = this.SuppressExactMatch,
                ClearOnNoMatch = this.ClearOnNoMatch,
                MinimumInputLength = this.MinimumInputLength
            };
        }

        #endregion
    }
}
