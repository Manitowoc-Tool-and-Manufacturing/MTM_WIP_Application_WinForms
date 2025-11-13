using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MTM_WIP_Application_Winforms.Services
{
    /// <summary>
    /// Static service providing filtering and sorting logic for suggestions.
    /// Thread-safe (no shared state).
    /// Optimized for &lt;10,000 items with &lt;50ms performance target.
    /// </summary>
    public static class Service_SuggestionFilter
    {
        #region Methods

        /// <summary>
        /// Filters suggestions based on user input with substring or wildcard matching.
        /// Sorts results by relevance (shortest match first, then alphabetically).
        /// </summary>
        /// <param name="source">Complete list of suggestion strings from data provider.</param>
        /// <param name="filter">User's typed input (may contain % wildcards).</param>
        /// <param name="maxResults">Maximum number of results to return.</param>
        /// <param name="enableWildcards">Whether to interpret % as wildcard character.</param>
        /// <returns>
        /// Filtered and sorted list of suggestions.
        /// Empty list if no matches or exact match found (suppression).
        /// </returns>
        /// <example>
        /// <code>
        /// var results = Service_SuggestionFilter.FilterSuggestions(
        ///     allParts,
        ///     "R-%",
        ///     100,
        ///     enableWildcards: true
        /// );
        /// // Returns: ["R-ABC-01", "R-XYZ-02", ...] (max 100 items)
        /// </code>
        /// </example>
        public static List<string> FilterSuggestions(
            List<string> source,
            string filter,
            int maxResults,
            bool enableWildcards)
        {
            // Return empty list if filter is null/whitespace
            if (string.IsNullOrWhiteSpace(filter))
                return new List<string>();

            // Check for exact match (case-insensitive) -> suppress overlay
            if (IsExactMatch(source, filter))
                return new List<string>();

            // Wildcard filtering
            if (enableWildcards && filter.Contains('%'))
            {
                var regex = ConvertWildcardToRegex(filter);
                return source.Where(s => regex.IsMatch(s))
                             .OrderBy(s => s.Length)
                             .ThenBy(s => s, StringComparer.OrdinalIgnoreCase)
                             .Take(maxResults)
                             .ToList();
            }

            // Substring filtering
            return source.Where(s => s.IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0)
                         .OrderBy(s => s.Length)
                         .ThenBy(s => s, StringComparer.OrdinalIgnoreCase)
                         .Take(maxResults)
                         .ToList();
        }

        /// <summary>
        /// Converts wildcard pattern to compiled regular expression.
        /// Replaces % with .* and escapes other regex special characters.
        /// </summary>
        /// <param name="wildcardPattern">Pattern with % wildcards (e.g., "R-%-01").</param>
        /// <returns>Compiled regex for case-insensitive matching.</returns>
        /// <example>
        /// <code>
        /// var regex = Service_SuggestionFilter.ConvertWildcardToRegex("R-%-01");
        /// // regex matches: "R-ABC-01", "R-XYZ-01", "R-123-01", etc.
        /// </code>
        /// </example>
        public static Regex ConvertWildcardToRegex(string wildcardPattern)
        {
            // Escape regex special characters
            var pattern = Regex.Escape(wildcardPattern);

            // Replace escaped % (becomes "\%") with ".*"
            pattern = pattern.Replace("\\%", ".*");

            // Wrap in anchors for full string match
            pattern = "^" + pattern + "$";

            // Compile with IgnoreCase and Compiled for performance
            return new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }

        /// <summary>
        /// Sorts suggestions by relevance:
        /// Primary: Length (shorter strings first - more specific matches)
        /// Secondary: Alphabetical order (case-insensitive)
        /// </summary>
        /// <param name="suggestions">List to sort.</param>
        /// <returns>Sorted list.</returns>
        public static List<string> SortByRelevance(List<string> suggestions)
        {
            return suggestions.OrderBy(s => s.Length)
                              .ThenBy(s => s, StringComparer.OrdinalIgnoreCase)
                              .ToList();
        }

        /// <summary>
        /// Checks if user input exactly matches one item in the source list.
        /// Used to suppress overlay for exact matches.
        /// </summary>
        /// <param name="source">Complete list of suggestions.</param>
        /// <param name="input">User's typed input.</param>
        /// <returns>True if input matches exactly one item (case-insensitive).</returns>
        public static bool IsExactMatch(List<string> source, string input)
        {
            if (source == null || string.IsNullOrWhiteSpace(input))
                return false;

            return source.Any(s => s.Equals(input, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Validates that source list contains no null or empty strings.
        /// Removes invalid entries if found.
        /// </summary>
        /// <param name="source">List to validate and clean.</param>
        /// <returns>Cleaned list with no null/empty entries.</returns>
        public static List<string> CleanSourceList(List<string> source)
        {
            if (source == null)
                return new List<string>();

            return source.Where(s => !string.IsNullOrWhiteSpace(s))
                         .Select(s => s.Trim())
                         .Distinct(StringComparer.OrdinalIgnoreCase)
                         .ToList();
        }

        #endregion
    }
}
