# Data Model: Modern JSON-Driven Help System

**Feature**: `016-new-help-system`

## Entities

### HelpCategory
Represents a major section of documentation, stored as a single JSON file.

| Property | Type | Description | Validation |
|----------|------|-------------|------------|
| `CategoryId` | `string` | Unique identifier (slug) | Required, unique, no spaces |
| `Title` | `string` | Display name of category | Required |
| `Icon` | `string` | Icon name/class for UI | Optional |
| `Description` | `string` | Short summary for index card | Optional |
| `Order` | `int` | Sort order in index | Default 0 |
| `Topics` | `List<HelpTopic>` | Collection of topics | Required |

### HelpTopic
Represents a single help article within a category.

| Property | Type | Description | Validation |
|----------|------|-------------|------------|
| `TopicId` | `string` | Unique identifier within category | Required, unique in category |
| `Title` | `string` | Article headline | Required |
| `Summary` | `string` | Short text for search results | Optional |
| `Content` | `string` | HTML body content | Required |
| `Keywords` | `List<string>` | Search keywords | Optional |
| `LastUpdated` | `DateTime` | Content freshness date | Optional |

### HelpSearchResult
Transient model for search operations.

| Property | Type | Description |
|----------|------|-------------|
| `Topic` | `HelpTopic` | The matching topic |
| `Category` | `HelpCategory` | The parent category |
| `RelevanceScore` | `int` | Calculated match quality |

## JSON Structure Example

```json
{
  "CategoryId": "inventory-operations",
  "Title": "Inventory Operations",
  "Icon": "inventory_box",
  "Description": "Learn how to add, edit, and manage inventory items.",
  "Order": 10,
  "Topics": [
    {
      "TopicId": "add-item",
      "Title": "Adding New Inventory",
      "Summary": "Step-by-step guide to creating new inventory records.",
      "Content": "<p>To add inventory...</p>",
      "Keywords": ["create", "new", "stock"],
      "LastUpdated": "2025-12-06T10:00:00"
    }
  ]
}
```
