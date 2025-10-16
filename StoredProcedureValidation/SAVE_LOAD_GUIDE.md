# Save/Load Progress Feature Guide

## Overview

The procedure review tool now supports saving and loading your review progress, allowing you to:
- Work on reviews in multiple sessions
- Share progress with team members
- Create checkpoints before major changes
- Restore previous states if needed

**Configuration**: The HTML tool automatically loads `SPSettings.json` on page load to determine:
- Where stored procedure SQL files are located (`StoredProceduresFolder` path)
- Project-specific paths and settings
- This makes the tool portable across different projects without code changes

## How It Works

### Save Progress (📥 Button)

**What Gets Saved:**
- All edits to `DeveloperCorrection` field
- All edits to `Notes` field
- `NeedsAttention` flags for each procedure
- Review status (which procedures you've marked as reviewed)
- Current position (which procedure you're viewing)
- Active filters (domain, pattern, confidence, review status, search term)

**File Format:**
- JSON file with timestamp in filename
- Example: `procedure-review-progress-2025-10-15.json`
- Human-readable format (can be edited manually if needed)

**When to Save:**
- End of each review session
- Before taking a break
- After reviewing a significant number of procedures
- Before experimenting with edits (create checkpoint)

### Load Progress (📤 Button)

**What Gets Restored:**
- All saved edits merged into current CSV data
- Review status for each procedure
- Filter settings
- Position in the procedure list

**Important Notes:**
- Progress files are matched by `ProcedureName`
- If CSV has new procedures, they'll be preserved
- If progress file has procedures not in CSV, they're ignored
- Existing CSV data is enhanced with saved edits

**When to Load:**
- Starting a new review session
- Continuing work from another machine
- Restoring a checkpoint after experimenting
- Reviewing team member's progress

## Workflow Examples

### Daily Review Workflow
```
Day 1:
1. Open procedure-review-tool.html
2. Load procedure-transaction-analysis.csv
3. Review 10 procedures, add notes
4. Click "📥 Save Progress" at end of day

Day 2:
1. Open procedure-review-tool.html
2. Load procedure-transaction-analysis.csv
3. Click "📤 Load Progress", select yesterday's file
4. Continue from where you left off
5. Save progress again at end of session
```

### Checkpoint Workflow
```
1. Review procedures and make edits
2. Click "📥 Save Progress" (checkpoint-1)
3. Try different correction approaches
4. Not satisfied? Click "📤 Load Progress", select checkpoint-1
5. Start over with different approach
```

### Team Collaboration Workflow
```
Developer A:
1. Review first 25 procedures
2. Save progress: dev-a-procedures-1-25.json
3. Share file with Developer B

Developer B:
1. Review next 25 procedures
2. Save progress: dev-b-procedures-26-50.json
3. Share file with team lead

Team Lead:
1. Load dev-a file, review corrections
2. Load dev-b file, review corrections
3. Export final CSV with all corrections
```

## Progress File Structure

```json
{
  "version": "1.0",
  "savedAt": "2025-10-15T19:13:00.000Z",
  "currentIndex": 15,
  "reviewedProcedures": [
    "inv_inventory_Add_Item",
    "inv_inventory_Fix_BatchNumbers",
    "usr_users_Get_All"
  ],
  "procedures": [
    {
      "ProcedureName": "inv_inventory_Add_Item",
      "DeveloperCorrection": "Pattern is correct",
      "Notes": "Reviewed 2025-10-15. All validation present.",
      "NeedsAttention": "False",
      "reviewed": true
    }
  ],
  "filters": {
    "domain": "Inventory",
    "pattern": "all",
    "confidence": "all",
    "review": "unreviewed",
    "search": ""
  }
}
```

## Tips & Best Practices

### Save Frequently
- Save after every major editing session
- Create dated backups: `progress-2025-10-15-morning.json`
- Keep multiple checkpoints if experimenting

### Organize Progress Files
```
StoredProcedureValidation/
  progress/
    checkpoint-initial-2025-10-15.json
    inventory-procedures-done.json
    user-procedures-done.json
    final-review-2025-10-16.json
```

### Version Control
- **Don't commit** progress JSON files to Git (personal state)
- **Do commit** final exported CSV files (shared deliverables)
- Add `*-progress-*.json` to `.gitignore`

### Team Guidelines
- Use descriptive filenames: `john-inventory-review-2025-10-15.json`
- Include review date in filename
- Document what's covered in the filename or Notes field
- Share via team communication channels, not Git

## Troubleshooting

### "Invalid progress file format"
- File may be corrupted
- Try opening in text editor to verify JSON structure
- Ensure `version` field exists and equals `"1.0"`

### "No procedures restored"
- Progress file ProcedureNames don't match current CSV
- CSV may be from different analysis run
- Check that procedure names are identical (case-sensitive)

### Edits disappear after reload
- Must save progress **after** loading CSV
- Loading CSV resets all data
- Always load CSV first, then load progress

### Progress file missing procedures
- Normal if CSV has more procedures than when progress was saved
- New procedures will appear unreviewed
- Only matching procedures get restored edits

## Keyboard Shortcuts

While reviewing (HTML tool):
- `→` / `↓` : Next procedure
- `←` / `↑` : Previous procedure
- `Home` : Jump to first procedure
- `End` : Jump to last procedure
- `Ctrl+S` : *(Not implemented - use 📥 button)*

## Future Enhancements

Potential improvements:
- Auto-save every N minutes
- Browser localStorage backup
- Merge multiple progress files
- Export progress report (summary of edits)
- Diff viewer (compare progress files)

## Support

Questions or issues? Check:
1. README.md for general documentation
2. VERIFICATION_REPORT.txt for analysis accuracy
3. SPSettings.json for configuration options
