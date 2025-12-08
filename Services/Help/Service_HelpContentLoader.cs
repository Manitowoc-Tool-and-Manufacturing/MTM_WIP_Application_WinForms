using System.Text.Json;
using MTM_WIP_Application_Winforms.Models.Help;

namespace MTM_WIP_Application_Winforms.Services.Help
{
    /// <summary>
    /// Service responsible for loading and deserializing help content from JSON files.
    /// </summary>
    public class Service_HelpContentLoader
    {
        /// <summary>
        /// Loads all help categories from the specified directory.
        /// </summary>
        /// <param name="jsonDirectoryPath">Path to the directory containing JSON help files.</param>
        /// <returns>A list of loaded help categories.</returns>
        public async Task<List<Model_HelpCategory>> LoadCategoriesAsync(string jsonDirectoryPath)
        {
            var categories = new List<Model_HelpCategory>();

            if (!Directory.Exists(jsonDirectoryPath))
            {
                Logging.LoggingUtility.LogApplicationError(new DirectoryNotFoundException($"Help content directory not found: {jsonDirectoryPath}"));
                return categories;
            }

            var files = Directory.GetFiles(jsonDirectoryPath, "*.json");
            foreach (var file in files)
            {
                try
                {
                    using var stream = File.OpenRead(file);
                    var category = await JsonSerializer.DeserializeAsync<Model_HelpCategory>(stream);
                    if (category != null)
                    {
                        categories.Add(category);
                    }
                }
                catch (Exception ex)
                {
                    // Log error but continue loading other files
                    Service_ErrorHandler.HandleException(
                        ex, 
                        Models.Enum_ErrorSeverity.Low, 
                        callerName: nameof(LoadCategoriesAsync),
                        contextData: new Dictionary<string, object> { { "File", file } }
                    );
                }
            }

            return categories;
        }
    }
}
