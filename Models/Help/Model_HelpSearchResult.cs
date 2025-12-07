namespace MTM_WIP_Application_Winforms.Models.Help
{
    /// <summary>
    /// Transient model for search operations.
    /// </summary>
    public class Model_HelpSearchResult
    {
        /// <summary>
        /// The matching topic.
        /// </summary>
        public Model_HelpTopic Topic { get; set; } = new Model_HelpTopic();

        /// <summary>
        /// The parent category of the topic.
        /// </summary>
        public Model_HelpCategory Category { get; set; } = new Model_HelpCategory();

        /// <summary>
        /// Calculated match quality score.
        /// </summary>
        public int RelevanceScore { get; set; }
    }
}
