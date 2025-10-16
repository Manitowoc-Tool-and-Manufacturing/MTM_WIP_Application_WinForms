# HTML Viewer Configuration Guide

## Overview

The `procedure-review-tool.html` automatically loads `SPSettings.json` to configure project-specific paths and settings. This makes the tool truly portable - no code changes needed when moving between projects.

## How It Works

### Automatic Configuration Loading

When you open `procedure-review-tool.html` in a browser:

1. **Page Load**: HTML file loads `SPSettings.json` from the same directory
2. **Path Extraction**: Reads `Paths.StoredProceduresFolder` setting
3. **Dynamic Paths**: Uses this path for all "View SQL" button operations
4. **Fallback**: If SPSettings.json isn't found, uses default path `../Database/UpdatedStoredProcedures`

### Configuration Flow

```
procedure-review-tool.html (opens)
    ↓
Fetch ./SPSettings.json
    ↓
Parse JSON → Extract Paths.StoredProceduresFolder
    ↓
Store in: storedProceduresBasePath variable
    ↓
Use for all SQL file lookups
```

### View SQL Button Behavior

When you click "View SQL" on any procedure:

1. Tool reads `storedProceduresBasePath` variable
2. Builds paths for common subdirectories:
   - `{base}/ReadyForVerification/inventory/`
   - `{base}/ReadyForVerification/transactions/`
   - `{base}/ReadyForVerification/users/`
   - `{base}/ReadyForVerification/system/`
   - `{base}/ReadyForVerification/logging/`
   - `{base}/ReadyForVerification/master-data/`
   - `{base}/ReadyForVerification/quick-buttons/`
   - `{base}/ReadyForVerification/roles/`
   - `{base}/ReadyForVerification/user-based/`
   - `{base}/ReadyForVerification/uncategorized/`
   - `{base}/` (root folder as fallback)
3. Tries each path until SQL file is found
4. Displays SQL in modal popup

## SPSettings.json Structure

### Relevant Sections for HTML Viewer

```json
{
  "Paths": {
    "StoredProceduresFolder": "..\\Database\\UpdatedStoredProcedures",
    "OutputCSV": "procedure-transaction-analysis.csv"
  },
  "HTMLViewerSettings": {
    "DefaultTheme": "purple-gradient",
    "EnableTooltips": true,
    "ShowProgressBar": true,
    "AutoSaveInterval": 30,
    "EnableDragDrop": true,
    "ShowSQLPreview": true
  }
}
```

### Path Formats

**Windows PowerShell Format** (in SPSettings.json):
```json
"StoredProceduresFolder": "..\\Database\\UpdatedStoredProcedures"
```

**Web Path Format** (converted by HTML):
```javascript
storedProceduresBasePath = '../Database/UpdatedStoredProcedures'
```

The HTML tool automatically converts backslashes to forward slashes for browser compatibility.

## Portability Benefits

### Before Configuration (Hardcoded Paths)
```javascript
// ❌ NOT PORTABLE - hardcoded paths
const paths = [
    '../Database/UpdatedStoredProcedures/ReadyForVerification/inventory/proc.sql',
    // ... more hardcoded paths
];
```

**Problem**: Moving to different project requires editing HTML file.

### After Configuration (Dynamic Paths)
```javascript
// ✅ PORTABLE - reads from SPSettings.json
const paths = subfolders.map(subfolder => 
    `${storedProceduresBasePath}/ReadyForVerification/${subfolder}/${procedureName}.sql`
);
```

**Benefit**: Same HTML file works for any project - just update SPSettings.json.

## Using with Different Projects

### Project A (MTM Application)
```json
{
  "Paths": {
    "StoredProceduresFolder": "..\\Database\\UpdatedStoredProcedures"
  }
}
```

### Project B (Different Structure)
```json
{
  "Paths": {
    "StoredProceduresFolder": "..\\sql\\stored_procedures"
  }
}
```

### Project C (Flat Structure)
```json
{
  "Paths": {
    "StoredProceduresFolder": "..\\database\\procs"
  }
}
```

**Same HTML file works for all three!** Just update SPSettings.json.

## Troubleshooting

### "View SQL" Button Shows Error

**Symptom**: Clicking "View SQL" displays error message.

**Possible Causes**:
1. SPSettings.json not found (check browser console)
2. StoredProceduresFolder path is incorrect
3. SQL file doesn't exist in any searched subdirectory
4. File permissions blocking access

**Debug Steps**:
1. Open browser developer console (F12)
2. Look for messages:
   - `Loaded SPSettings.json, SP path: ...` (success)
   - `SPSettings.json not found, using default path` (warning)
   - `Error loading SPSettings.json: ...` (error)
3. Check if path in console message is correct
4. Verify SQL files exist at that path

### SPSettings.json Not Loading

**Symptom**: Console shows "using default path" warning.

**Solution**:
1. Verify SPSettings.json exists in same folder as HTML file
2. Check file name spelling (case-sensitive on some servers)
3. Ensure file is valid JSON (no syntax errors)
4. If running from file:// protocol, some browsers block fetch requests

### Paths Not Resolving Correctly

**Symptom**: SQL files exist but "View SQL" can't find them.

**Solution**:
1. Check path separators (backslash vs forward slash)
2. Verify relative path is correct from HTML file location
3. Test with browser's network tab (F12 → Network) to see exact URLs attempted
4. Ensure no typos in folder/file names

## Browser Compatibility

### Supported Browsers
- ✅ Chrome/Edge (latest)
- ✅ Firefox (latest)
- ✅ Safari (latest)

### fetch() API Requirements
- Modern browsers (ES6+)
- May need CORS headers if running from web server
- Local file:// protocol may have restrictions

### Testing Locally

**Best Practice**: Use local web server instead of file:// protocol:

```powershell
# Python 3
python -m http.server 8000

# Node.js (http-server)
npx http-server -p 8000

# Then open: http://localhost:8000/procedure-review-tool.html
```

This avoids CORS and fetch() issues with file:// protocol.

## Advanced Configuration

### Custom Subdirectory Structure

If your project uses different subdirectory names, update the HTML file's subfolder array:

```javascript
// In procedure-review-tool.html, line ~1925
const subfolders = [
    'inventory', 'transactions', 'users', 'system', 'logging', 
    'master-data', 'quick-buttons', 'roles', 'user-based', 'uncategorized'
];
```

Change to match your structure:

```javascript
const subfolders = [
    'inv', 'trans', 'usr', 'sys', 'log', 
    'masterdata', 'buttons', 'security', 'userbased', 'misc'
];
```

### No Subdirectories

If SQL files are in a flat structure (no subfolders):

```json
{
  "Paths": {
    "StoredProceduresFolder": "..\\database\\all_procedures"
  }
}
```

The fallback path `{base}/{procedureName}.sql` will find them.

## Future Enhancements

Potential improvements:
- Load HTMLViewerSettings from SPSettings.json
- Custom theme colors from config
- Auto-refresh settings without page reload
- Multiple base path support
- Regex-based file search patterns

## Summary

✅ **Automatic**: Loads SPSettings.json on page load  
✅ **Portable**: Same HTML works for any project  
✅ **Fallback**: Uses defaults if config not found  
✅ **Flexible**: Handles various directory structures  
✅ **Debuggable**: Console logging for troubleshooting  

The HTML viewer is now fully configuration-driven, making it a truly portable tool for any project!
