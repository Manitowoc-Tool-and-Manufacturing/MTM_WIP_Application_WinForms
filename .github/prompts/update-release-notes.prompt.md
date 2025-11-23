# Update Release Notes and Version

**Purpose**: Automate the process of updating release notes, application version, and database changelog after completing a feature or release.

---

## Instructions for GitHub Copilot

When this prompt is invoked, perform the following steps in order:

### Step 1: Read Current State
1. Read the entire conversation history from the beginning to understand what changes were made.
2. Read `RELEASE_NOTES_USER_FRIENDLY.md` to understand its structure (Quick Summary table, Latest Update section).
3. Read `RELEASE_NOTES.json` to understand the JSON structure used by the application UI.
4. Check the current version in `MTM_WIP_Application_Winforms.csproj` (look for `<Version>` tag).

### Step 2: Determine New Version Number
- **Major version bump** (X.0.0.0): Breaking changes, complete redesigns, architecture changes.
- **Minor version bump** (6.X.0.0): New features, significant improvements, new tabs/forms.
- **Patch version bump** (6.2.X.0): Bug fixes, small improvements, refinements.
- **Build version bump** (6.2.0.X): Hotfixes, documentation updates (rarely used).

Based on conversation content, determine appropriate version increment.

### Step 3: Update Application Version
Update the `<Version>` tag in `MTM_WIP_Application_Winforms.csproj`:
```xml
<Version>6.X.X.X</Version>
```

### Step 4: Update User-Friendly Release Notes
Update `RELEASE_NOTES_USER_FRIENDLY.md`:

1. **Update "Quick Summary" Table**:
   - Add a new row at the top of the table with the new Date, Version, Summary, and "What to do".
   - Link the Date and Version to the new anchor (e.g., `#latest-update---month-day-year-version-x-x-x`).

2. **Add new "Latest Update" section**:
   - Insert a new section after the Quick Summary table.
   - Format:
     ```markdown
     ## Latest Update - [Month] [Day], [Year] (Version [X.X.X])

     **What Changed**: [Brief, friendly description]
     **Do I Need To Do Anything?**: [Yes/No - instructions]

     ---

     ### 🎯 What This Means For You

     #### [Feature Name]

     **What's new**:
     - [Bullet points]

     **Why this helps**:
     - [Benefits]
     ```

3. **Demote previous "Latest Update"**:
   - Change the header of the previous update from `## Latest Update...` to `## Previous Update...`.

### Step 5: Update JSON Release Notes
Update `RELEASE_NOTES.json`:

1. **Add new entry at the top** of the JSON array:
   ```json
   {
     "Version": "X.X.X",
     "Date": "[Month] [Day], [Year]",
     "Summary": "[Brief summary matching MD file]",
     "Details": "<h3>[Feature Name]</h3><p><strong>What's new</strong>:</p><ul><li>[Item 1]</li><li>[Item 2]</li></ul>..."
   }
   ```
   - **Important**: The `Details` field must contain HTML (h3, p, ul, li, strong) as it is rendered in a WebView. Ensure it matches the content in the MD file but formatted as HTML string.

### Step 6: Update Database Changelog
Update `log_changelog` table in BOTH databases:

**Production database**:
```powershell
& "C:\MAMP\bin\mysql\bin\mysql.exe" -h localhost -P 3306 -u root -proot mtm_wip_application_winforms -e "UPDATE log_changelog SET Version = 'X.X.X.X', Notes = 'Brief summary of changes' WHERE Version = '[OLD_VERSION]';"
```

**Test database**:
```powershell
& "C:\MAMP\bin\mysql\bin\mysql.exe" -h localhost -P 3306 -u root -proot mtm_wip_application_winforms_test -e "UPDATE log_changelog SET Version = 'X.X.X.X', Notes = 'Brief summary of changes' WHERE Version = '[OLD_VERSION]';"
```
