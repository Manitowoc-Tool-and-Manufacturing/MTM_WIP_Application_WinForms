# Update Release Notes and Version

**Purpose**: Automate the process of updating release notes, application version, and database changelog after completing a feature or release.

---

## Instructions for GitHub Copilot

When this prompt is invoked, perform the following steps in order:

### Step 1: Read Current State
1. Read the entire conversation history from the beginning to understand what changes were made
2. Read `RELEASE_NOTES_USER_FRIENDLY.md` to understand its structure and format
3. Check the current version in `MTM_WIP_Application_Winforms.csproj` (look for `<Version>` tag)

### Step 2: Determine New Version Number
- **Major version bump** (X.0.0.0): Breaking changes, complete redesigns, architecture changes
- **Minor version bump** (6.X.0.0): New features, significant improvements, new tabs/forms
- **Patch version bump** (6.2.X.0): Bug fixes, small improvements, refinements
- **Build version bump** (6.2.0.X): Hotfixes, documentation updates (rarely used)

Based on conversation content, determine appropriate version increment.

### Step 3: Update Application Version
Update the `<Version>` tag in `MTM_WIP_Application_Winforms.csproj`:
```xml
<Version>6.X.X.X</Version>
```

### Step 4: Update Release Notes
Update `RELEASE_NOTES_USER_FRIENDLY.md`:

1. **Add new "Latest Update" section** at the top with:
   - Current date (format: "November 13, 2025")
   - New version number
   - Brief "What Changed" summary (1-2 sentences)
   - "Do I Need To Do Anything?" answer

2. **Move previous "Latest Update" to "Previous Update"**

3. **Write user-friendly feature descriptions**:
   - Use "What's new" / "What's better" / "What's fixed" sections
   - Write for shop floor workers, not developers
   - Include concrete examples and screenshots descriptions
   - Explain WHY changes help users, not just WHAT changed
   - Use bullet points for readability
   - Include "How to use it" sections for new features

4. **Writing style guidelines**:
   - Avoid technical jargon (use "dropdown" not "ComboBox", "text field" not "TextBox")
   - Be specific ("3x faster" not "faster")
   - Use conversational tone
   - Include keyboard shortcuts and examples
   - Explain benefits, not just features

5. **ONLY include changes that affect RELEASE builds**:
   - ❌ NO developer tools, debug features, internal refactoring
   - ❌ NO changes in #if DEBUG or development-only code
   - ✅ YES user-visible features, bug fixes, performance improvements
   - ✅ YES UI changes, new workflows, keyboard shortcuts

### Step 5: Update Database Changelog
Update `log_changelog` table in BOTH databases:

**Production database**:
```powershell
& "C:\MAMP\bin\mysql\bin\mysql.exe" -h localhost -P 3306 -u root -proot mtm_wip_application_winforms -e "UPDATE log_changelog SET Version = 'X.X.X.X', Notes = 'Brief summary of changes' WHERE Version = '[OLD_VERSION]';"
```

**Test database**:
```powershell
& "C:\MAMP\bin\mysql\bin\mysql.exe" -h localhost -P 3306 -u root -proot mtm_wip_application_winforms_test -e "UPDATE log_changelog SET Version = 'X.X.X.X', Notes = 'Brief summary of changes' WHERE Version = '[OLD_VERSION]';"
```

**Notes column format**: Single line, 100-150 characters max, describes key changes

### Step 6: Verify Updates
1. Verify `.csproj` version updated
2. Verify `RELEASE_NOTES_USER_FRIENDLY.md` structure correct
3. Query both databases to confirm version updated:
   ```powershell
   & "C:\MAMP\bin\mysql\bin\mysql.exe" -h localhost -P 3306 -u root -proot mtm_wip_application_winforms -e "SELECT * FROM log_changelog;"
   & "C:\MAMP\bin\mysql\bin\mysql.exe" -h localhost -P 3306 -u root -proot mtm_wip_application_winforms_test -e "SELECT * FROM log_changelog;"
   ```

### Step 7: Summary Report
Provide a summary showing:
- Old version → New version
- Files updated
- Database changelog updated (both prod and test confirmed)
- Key features added to release notes

---

## Example Usage

**User says**: "We just completed the suggestion textbox feature and confirmation dialogs. Update the release notes."

**Copilot should**:
1. Read conversation to identify: SuggestionTextBox system, confirmation dialogs, focus highlighting fixes
2. Determine this is a minor version bump (new feature): 6.1.0 → 6.2.0
3. Update `.csproj` to version 6.2.0.0
4. Add comprehensive release notes section explaining autocomplete, wildcards, confirmations
5. Update both database changelogs
6. Verify all changes
7. Report: "✅ Updated to version 6.2.0.0 - Release notes updated, databases updated"

---

## Error Handling

If errors occur:
- **Database connection fails**: Report error, suggest checking MAMP MySQL is running
- **Version parse fails**: Report current version as "Error: Not Found", suggest manual check
- **File update fails**: Report which file/step failed, provide manual update instructions

---

## Checklist Before Completion

- [ ] `.csproj` version incremented appropriately
- [ ] `RELEASE_NOTES_USER_FRIENDLY.md` has new "Latest Update" section
- [ ] Previous "Latest Update" moved to "Previous Update"  
- [ ] Release notes written in user-friendly language
- [ ] Only RELEASE-visible changes documented (no debug/developer features)
- [ ] Production database `log_changelog` updated and verified
- [ ] Test database `log_changelog` updated and verified
- [ ] Summary report provided with old → new version

---

## Notes

- This prompt focuses on **user-facing documentation** - internal code changes are documented elsewhere
- Release notes should be understandable by shop floor workers with no technical background
- Database must have exactly 1 row in `log_changelog` - we UPDATE, not INSERT
- Version format: `Major.Minor.Patch.Build` (e.g., 6.2.0.0)
