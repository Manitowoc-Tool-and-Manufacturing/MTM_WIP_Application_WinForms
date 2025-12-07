using Ganss.Xss;

namespace MTM_WIP_Application_Winforms.Helpers
{
    /// <summary>
    /// Helper for sanitizing HTML content to prevent XSS attacks.
    /// </summary>
    public static class Helper_HtmlSanitizer
    {
        private static readonly HtmlSanitizer _sanitizer = new HtmlSanitizer();

        /// <summary>
        /// Sanitizes the input HTML string.
        /// </summary>
        /// <param name="html">The HTML string to sanitize.</param>
        /// <returns>The sanitized HTML string.</returns>
        public static string Sanitize(string html)
        {
            if (string.IsNullOrEmpty(html))
            {
                return string.Empty;
            }
            return _sanitizer.Sanitize(html);
        }
    }
}
