# Service Contract: IHelpSystem

**Namespace**: `MTM_WIP_Application_Winforms.Services.Help`

## Interface Definition

```csharp
public interface IHelpSystem
{
    /// <summary>
    /// Loads all help content from JSON files asynchronously.
    /// </summary>
    Task InitializeAsync();

    /// <summary>
    /// Gets all loaded help categories sorted by Order.
    /// </summary>
    IEnumerable<HelpCategory> GetCategories();

    /// <summary>
    /// Retrieves a specific category by ID.
    /// </summary>
    HelpCategory? GetCategory(string categoryId);

    /// <summary>
    /// Searches all topics for the given query string.
    /// </summary>
    /// <param name="query">Search terms</param>
    /// <returns>List of results sorted by relevance</returns>
    IEnumerable<HelpSearchResult> Search(string query);

    /// <summary>
    /// Generates the HTML for the Index page (category cards).
    /// </summary>
    string GenerateIndexHtml();

    /// <summary>
    /// Generates the HTML for a specific category and topic.
    /// </summary>
    /// <param name="categoryId">Category ID</param>
    /// <param name="topicId">Topic ID (optional, defaults to first)</param>
    string GenerateTopicHtml(string categoryId, string topicId = null);
}
```
