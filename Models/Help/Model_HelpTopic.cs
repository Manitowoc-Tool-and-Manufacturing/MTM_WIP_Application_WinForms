namespace MTM_WIP_Application_Winforms.Models.Help
{
    /// <summary>
    /// Represents a single help article within a category.
    /// </summary>
    public class Model_HelpTopic
    {
        /// <summary>
        /// Unique identifier within the category.
        /// </summary>
        public string TopicId { get; set; } = string.Empty;

        /// <summary>
        /// Article headline.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Short text for search results.
        /// </summary>
        public string Summary { get; set; } = string.Empty;

        /// <summary>
        /// HTML body content.
        /// </summary>
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// Search keywords.
        /// </summary>
        public List<string> Keywords { get; set; } = new List<string>();

        /// <summary>
        /// Content freshness date.
        /// </summary>
        public DateTime? LastUpdated { get; set; }
    }
}
