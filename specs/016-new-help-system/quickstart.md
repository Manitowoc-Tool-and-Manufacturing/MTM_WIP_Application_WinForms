# Quickstart: Adding Help Content

## 1. Create a JSON File
Create a new file in `Documentation/Help/JSON/` named after your category (e.g., `my-new-feature.json`).

## 2. Define Structure
Copy this template:

```json
{
  "CategoryId": "my-new-feature",
  "Title": "My New Feature",
  "Icon": "feature_icon",
  "Description": "Short description for the index card.",
  "Order": 100,
  "Topics": []
}
```

## 3. Add Topics
Add topic objects to the `Topics` array:

```json
{
  "TopicId": "overview",
  "Title": "Feature Overview",
  "Summary": "What this feature does.",
  "Content": "<h1>Overview</h1><p>HTML content here...</p>",
  "Keywords": ["feature", "guide"]
}
```

## 4. Test
Run the application and press F1. Your new category should appear in the index.
