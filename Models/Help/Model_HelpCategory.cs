namespace MTM_WIP_Application_Winforms.Models.Help
{
    /// <summary>
    /// Represents a major section of documentation, stored as a single JSON file.
    /// </summary>
    public class Model_HelpCategory
    {
        /// <summary>
        /// Unique identifier (slug) for the category.
        /// </summary>
        public string CategoryId { get; set; } = string.Empty;

        /// <summary>
        /// Display name of the category.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Icon name/class for UI display.
        /// </summary>
        public string Icon { get; set; } = string.Empty;

        /// <summary>
        /// Short summary for the index card.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Sort order in the index.
        /// </summary>
        public int Order { get; set; } = 0;

        /// <summary>
        /// Collection of topics within this category.
        /// </summary>
        public List<Model_HelpTopic> Topics { get; set; } = new List<Model_HelpTopic>();
    }
}
