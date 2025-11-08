# MTM WIP Application - What's New

> **User-friendly release notes for shop floor and office staff**

---

## Latest Update - November 8, 2025 (Version 6.0.2)

**What Changed**: Theme selection now saves correctly + QuickButtons improvements  
**Do I Need To Do Anything?**: No - but you can now change your theme and it will stick!

---

### 🎯 What This Means For You

#### Theme Selection Now Saves Properly

**What's fixed**:
- **Your theme choice is remembered**: When you change your theme in Settings, it now saves correctly and persists across application restarts
- **No more reverting to default**: Fixed issue where theme selection would reset to "Lavender" on next login even after saving a different theme
- **Proper JSON storage**: Theme preferences are now correctly stored in the database settings

**Why this was broken**:
1. **Wrong database procedure**: Code was trying to update a non-existent table column instead of updating the JSON settings
2. **Missing stored procedure**: The correct procedure for merging theme JSON wasn't being used
3. **Data corruption prevention**: Fixed ComboBox data extraction that could corrupt user settings

**What's better now**:
- **Pick your theme and keep it**: Choose any of the 9 available themes (Arctic, Default, Fire Storm, Glacier, Ice, Lavender, Midnight, Neon, Sunset, Wood) and your choice is saved
- **Instant application**: Theme applies immediately when you click Save
- **Works after restart**: Your selected theme loads automatically when you open the app
- **Error recovery**: If theme settings can't be loaded, the app uses a safe default instead of crashing

**How to change your theme**:
1. Click **Settings** (gear icon in top menu)
2. Select **Theme** tab
3. Pick a theme from the dropdown
4. Click **Save**
5. Your theme applies immediately and is remembered next time you log in

**Available themes**:
- **Arctic** - Cool blue tones
- **Default** - Standard gray/blue professional look
- **Fire Storm** - Warm orange/red tones
- **Glacier** - Light blue/white clean look
- **Ice** - Cool white/light blue
- **Lavender** - Purple/lavender accent colors
- **Midnight** - Dark blue professional theme
- **Neon** - Bright accent colors
- **Sunset** - Warm orange/pink tones
- **Wood** - Warm brown/tan earth tones

**Why this helps**: Personalize your workspace with colors that are easier on your eyes during long shifts. Some users prefer high-contrast themes for better visibility, others prefer softer tones to reduce eye strain.

---

## Previous Update - November 8, 2025 (Version 6.0.1)

**What Changed**: QuickButtons now work perfectly - fixes for duplicates, display issues, and save order  
**Do I Need To Do Anything?**: No - QuickButtons just work better now!

---

### 🎯 What This Means For You

#### QuickButtons Are Now 100% Reliable

**What's fixed**:
- **No more duplicate buttons**: Fixed issue where the same part/operation would appear multiple times in your QuickButtons list
- **Proper display when clicking buttons**: QuickButtons now fill in Part Number and Operation fields correctly with full descriptions (was showing raw codes like "21-28841-006" instead of formatted "21-28841-006 | Customer Name | Description")
- **Edit dialog with smart dropdowns**: Right-click Edit now shows ComboBoxes with autocomplete for Part ID and Operation - same experience as the main Inventory tab
- **New buttons appear at top**: When you save a transaction as a QuickButton, it now appears at the top of your list immediately (was going to the bottom)
- **Simplified reorder dialog**: Removed confusing Edit button from the "Change Order" dialog - use right-click Edit instead
- **Better validation**: Edit dialog only accepts valid Part IDs and Operations from your database - prevents typos and invalid data

**Why these were broken**:
1. **Duplicates**: Database cleanup was deleting buttons one-by-one, causing position shifts that let some duplicates survive
2. **Display issues**: QuickButtons were using old search-by-text method instead of proper value selection for multi-column dropdowns
3. **Edit not working**: The Edit menu item wasn't connected to its click handler
4. **Wrong save order**: Cleanup was reorganizing buttons after save, moving new ones to bottom instead of keeping them at top
5. **No validation**: Old edit dialog used free-text fields allowing invalid Part IDs and Operations

**What's better now**:
- **Clean list**: All duplicates removed automatically on next app restart
- **Correct data entry**: Clicking a QuickButton fills all fields properly formatted
- **Professional edit dialog**: ComboBoxes with autocomplete, data validation, and loading status feedback
- **Smart ordering**: Newest QuickButtons appear first (at top), older ones shift down
- **Cleaner interface**: Reorder dialog is simpler without redundant Edit button
- **Error prevention**: Can't enter invalid Part IDs or Operations - system validates your choices

**How to edit QuickButtons**:
1. Right-click any QuickButton
2. Select "Edit" from menu
3. **Part ID**: Type to search or select from dropdown (autocomplete enabled)
4. **Operation**: Select from dropdown (only valid operations shown)
5. **Quantity**: Adjust using up/down arrows or type directly
6. Click OK to save (Cancel to discard changes)

**How this helps**: 
- **No more confusion**: Your QuickButtons list shows unique buttons only, no duplicates
- **Faster data entry**: Fields populate correctly on first click, no manual fixing needed
- **Easy maintenance**: Edit buttons with same controls as main Inventory tab
- **Better organization**: Most recent transactions automatically appear at top where you need them
- **Data accuracy**: Validation prevents invalid Part IDs and Operations from being saved

---

## Previous Update - November 8, 2025 (Version 6.0.0)

**What Changed**: Complete redesign of Transaction History Viewer with major performance and usability improvements  
**Do I Need To Do Anything?**: Yes - check out the new features! Your old transaction history is still there, just easier to find now.

---

### 🎯 What This Means For You

#### Settings Form Now Works for All Users - Was broken during implementation of Stored Procedure fix, finally getting to it now.

**What's fixed**:
- **Settings form no longer crashes when opening**: Opening Settings → Database, Theme, or other settings now works reliably for all users in all environments
- **Fixed database connection switching issue**: The application no longer tries to switch to unreachable servers when loading saved settings (was causing "Unable to connect to MySQL host" errors)
- **Environment-aware server selection**: In development/test mode, the app stays connected to localhost even if saved settings mention a production server
- **Fresh database support**: The application handles empty settings tables gracefully (important for test environments and new installations)
- **Better error recovery**: If settings can't be loaded, the form displays default values instead of crashing

**Why this broke before**:
- When opening Settings form, it loaded saved database connection preferences
- If those preferences contained a production server address (172.16.1.104), the app tried to switch to it
- In test/development environments running on localhost, this caused connection failures and error spam
- Multiple stored procedures failed trying to reach the unreachable production server

**What's better now**:
- Test/development environments stay connected to localhost regardless of saved settings
- Production environments still work normally with saved server preferences
- Server address changes are validated before being applied
- First-time users see sensible defaults (localhost in dev, production server in release builds)
- Settings form loads instantly even with no saved preferences
- Makes testing and new installations smoother

**How this helps**: 
- Developers and testers can open Settings form without triggering connection errors
- New employees, test environments, and fresh database setups work without manual database setup
- Production users are unaffected - their saved settings work as expected
- The Settings form "just works" in all environments from day one

---

#### Easier User Management (For Administrators)

**What's fixed**:
- **Faster user deletion**: Removing users from the system now completes in 2-3 seconds (was freezing at 10%)
- **Cleaner process**: No more "Customer is required" errors when adding users
- **Simplified workflow**: System handles all cleanup automatically (user roles, settings, database records)

**Why this helps**: Administrators can add and remove users without unexpected errors or delays. The system now properly cleans up all user-related data when a user is removed.

#### Simplified Part Number Management

**What's fixed**:
- **Only 2 required fields**: When adding new parts, you only need to enter **Part Number** and **Item Type** (was requiring Customer and Description unnecessarily)
- **Item Type defaults to "WIP"**: The most common choice is pre-selected for you
- **Optional fields work properly**: Customer, Description, and other fields can be left blank if not needed

**What's better**:
- **Faster part entry**: Enter new parts in seconds without filling unnecessary fields
- **Less frustration**: No more "Customer is required" or "Description is required" error messages
- **Matches your workflow**: Most parts don't need customer or detailed descriptions right away

**How to use**:
1. Click **Settings** → **Part Numbers** → **Add Part Number**
2. Enter the **Part Number** (e.g., "12345-A")
3. Select **Item Type** from dropdown (defaults to "WIP" - most common choice)
4. Click **Save** - That's it!
5. *Optional*: Fill in Customer, Description if you want to add more details

**Example workflow**:
- Adding a new WIP part: Just type part number and click Save (Item Type already set to WIP)
- Adding a finished goods part: Type part number, change Item Type to "FG", click Save
- Adding a purchased part with details: Type part number, select Item Type, fill in Customer and Description, click Save

**Why this helps**:
- **3x faster data entry** - most parts are WIP and don't need extra details
- **Fewer mistakes** - less typing means fewer typos
- **Matches how you work** - add basic info fast, update details later if needed

---

## Previous Update - November 2, 2025 (Version 5.9.0)

**What Changed**: User Management, Part Number Management, and Settings Form improvements  
**Do I Need To Do Anything?**: No - these fixes make existing features work better

---

### 🎯 What This Means For You

#### Settings Form Now Works for All Users - Was broken during implementation of Stored Procedure fix, finally getting to it now.

**What's New**:
- **Modern 3-panel layout**: Search filters at top, results grid on left, transaction details on right - see everything at once
- **Lightning fast searches**: Find transactions in under 2 seconds, even when searching 90 days of history with thousands of records
- **Advanced filtering**: Search by Part Number, User, From Location, To Location, Operation, Date Range, Transaction Type (IN/OUT/TRANSFER), and Notes keywords
- **Smart pagination**: Navigate large result sets with Previous/Next buttons, page jump (go directly to page 5 of 20), and clear "Page X of Y" display
- **Instant detail view**: Click any transaction row to see complete information on the right - no popup windows
- **Remembers your last search**: Filters stay filled when you come back, making repeated searches faster
- **Configurable page size**: Default 50 transactions per page (adjustable in settings)

**How to use**:
1. Click **Transactions** tab from main menu (same location as before)
2. **Fill in search filters** at the top (all filters are optional - use what you need):
   - **Part Number**: Dropdown with autocomplete (type to search your parts)
   - **User**: Select any user (admins see all users, regular users see only themselves)
   - **From Location**: Where the inventory came from
   - **To Location**: Where the inventory went to
   - **Operation**: Manufacturing step (90, 100, 110, 120, 130, etc.)
   - **Transaction Type**: Check IN, OUT, or TRANSFER (or all three)
   - **Date Range**: Quick buttons (Today/This Week/This Month) or custom From/To dates
   - **Notes**: Search for keywords in transaction notes
3. Click **Search** button (or press Enter)
4. **View results** in the grid on left:
   - Shows: ID, Type, Part Number, Quantity, From→To locations, User, Date/Time
   - Sort by any column (click column header)
   - 50 transactions per page
5. **Click any row** to see full details on the right:
   - Complete transaction information
   - All dates and timestamps
   - Full notes text
   - Related batch information
6. **Navigate pages**:
   - Use **Previous/Next** buttons
   - Type page number and click **Go** to jump directly
   - See total record count ("Showing 50 of 1,247 transactions")

**Performance Improvements**:
- **3x faster search**: Old interface took 5-8 seconds for large searches, new one completes in under 2 seconds
- **Instant page navigation**: Switching between pages happens immediately (was 1-2 second delay)
- **Handles huge datasets**: Search 100,000+ transactions without freezing or slowing down
- **Responsive grid**: Scrolling and sorting work smoothly even with complex filters

**Why this helps**:
- **Save time**: Find specific transactions in seconds instead of minutes
- **Less clicking**: Everything visible on one screen - no jumping between windows
- **Better troubleshooting**: More filter combinations help pinpoint exactly what you're looking for
- **Easier tracking**: See transaction details while keeping search results visible
- **Reduced errors**: Clear layout prevents confusion about which transaction you're viewing

#### Transaction Analytics Summary (NEW!)

**What's New**:
- **Real-time statistics** displayed above the search results
- **Summary cards** show totals for current date range:
  - Total Transactions (all types combined)
  - Total IN transactions (receiving inventory)
  - Total OUT transactions (removing inventory)
  - Total TRANSFER transactions (moving between locations)
- **Date range indicator**: Shows which period the analytics cover
- **Updates automatically**: Statistics refresh when you change date filters

**How to use**:
1. Set your date range filters (Today/This Week/This Month/Custom)
2. Analytics cards update automatically above the search results
3. See at-a-glance how many transactions of each type occurred
4. Use to quickly validate daily/weekly activity levels

**Why this helps**:
- **Quick daily review**: See total transactions at a glance each morning
- **Activity monitoring**: Spot unusually high or low transaction counts
- **Shift reporting**: Quick summary of what happened during your shift
- **Trend awareness**: Compare this week to last week easily

#### Improved Data Display

**What's Better**:
- **Readable column widths**: Part numbers, locations, and dates no longer cut off
- **Better date formatting**: MM/dd/yy HH:mm format (example: 11/08/25 14:30)
- **Right-aligned numbers**: Quantities line up properly for easy reading
- **Alternating row colors**: Light gray/white rows make tracking across columns easier
- **Column sorting**: Click any column header to sort ascending/descending
- **Responsive layout**: Works correctly at 100%, 125%, 150%, and 200% screen scaling

**Keyboard Shortcuts** (NEW!):
- **F5**: Refresh/re-run current search
- **Ctrl+R**: Reset all filters to defaults
- **Escape**: Clear row selection
- **Tab**: Navigate between filter fields
- **Enter**: Execute search (when in any filter field)

---

### ✨ Coming Soon (Next Release - Week of November 11th)

**Transaction Lifecycle Viewer**:
- See the complete history of a batch from receiving to current location
- Visual tree showing how inventory splits between locations
- Track partial transfers (500 units → 250 here + 250 there)
- Chronological timeline of every IN, TRANSFER, and OUT
- Click "View Lifecycle" button in detail panel

**Export & Print**:
- Export search results to Excel for offline analysis
- Print formatted transaction reports
- PDF export option
- All current filters applied to exports

---

## Previous Update - October 26, 2025 (Version 5.4.0)

**What Changed**: New error report management system for developers  
**Do I Need To Do Anything?**: No - this is a developer-only feature

---

### 🎯 What This Means For You

#### For IT Support and Developers: Manage User-Reported Issues

**New "View Error Reports" feature** (accessible from Development menu):
- **Browse all error reports** submitted by users in one place
- **Search and filter** by date, user, machine, or error type
- **View complete details** including what the user was doing, technical information, and error stack traces
- **Track investigation progress** by marking reports as Reviewed or Resolved
- **Add developer notes** to document your findings and solutions
- **Export reports** to CSV or Excel for team analysis or management reporting

**Why this helps**:
- **Faster problem resolution**: See all error reports in one organized view instead of checking emails or logs
- **Better prioritization**: Color-coded status (Red=New, Yellow=In Review, Green=Resolved) helps you focus on urgent issues
- **Team coordination**: Developer notes prevent duplicate investigation efforts
- **Pattern detection**: Filter and export tools help identify recurring issues
- **Management reporting**: Export capabilities support monthly error trend reports

**How to access**:
1. Click **Development** menu in the main application
2. Select **View Error Reports**
3. Use filters to find specific reports or browse the full list

**Status workflow**:
- **New** (Red) → User just submitted, needs investigation
- **Reviewed** (Yellow) → Developer is investigating or needs more info
- **Resolved** (Green) → Issue fixed or documented

---

## Previous Update - October 26, 2025 (Version 5.3.1)

**What Changed**: Display scaling improvements  
**Do I Need To Do Anything?**: No - just click "Restart" when prompted if you move between monitors

---

### 🎯 What This Means For You

#### Better Display on Different Monitors and Screen Scaling

**If you use multiple monitors**, move your laptop between different displays, **or change your Windows screen scale** (100%, 125%, 150%, 175%, 200%):
- The application now **asks before automatically resizing** when display scaling changes
- You'll see a clear message like: "Display changed from 100% to 150%"
- You can choose what happens:
  - **Restart Now** (recommended) - Cleanest, fastest option - everything looks perfect
  - **Auto-Resize** - Immediate but windows may look weird temporarily
  - **Cancel** - Keep using as-is

**When does this happen?**
- Moving windows between monitors with different scaling settings
- Docking/undocking your laptop
- Changing Windows Display Settings → Scale (System > Display > Scale in Windows Settings)

**Why this helps**: No more surprise layout changes or stretched windows. You stay in control of when the app adjusts to your display changes.

#### Error Messages Look Right Now

**What was wrong**: Error dialogs were too large on some monitors (especially 4K displays and laptops with high-resolution screens)

**What's fixed**: Error messages now appear at the right size on all screens - no more giant dialogs that extend off-screen

**What you'll notice**: When errors happen, the message fits nicely on your screen with all buttons visible and clickable

---

## Recent Update - October 26, 2025 (Version 5.3.0)

**What Changed**: New error reporting feature  
**Do I Need To Do Anything?**: No - but you can now help us fix problems faster!

---

### 🎯 What This Means For You

#### Help Us Fix Problems Faster

**New "Report Issue" button** in error messages:
- When something goes wrong, you'll see a **"Report Issue"** button
- Click it to tell us what you were doing when the error happened
- Just type a quick note like "I was trying to transfer part #12345 to location B"
- Your report gets a tracking number so we can follow up with you

**Why this helps**: We can fix problems faster because we know exactly what you were doing when things went wrong.

#### Works Even When Network is Down

**Don't worry about losing reports**:
- If the network is down, your report is saved locally
- When the network comes back, reports are sent automatically
- You'll never lose a report due to network issues

**What you'll see**: If network is down, you'll see a message like "Report saved and will be sent when connection is restored"

---

## What You Need to Know

### ✅ Do I Need To Update?

**Version 6.0.2 (Theme Selection Fix - RELEASED)**:
- **All users**: Update when convenient - if you want to change your theme and have it stick, you need this update
- **If your theme keeps resetting**: YES - this fixes that issue
- **If you're happy with Lavender theme**: No urgent need - update at your convenience
- **First-time users**: Recommended - ensures theme selection works from day one

**Version 6.0.1 (QuickButtons Fixes - RELEASED)**:
- **All users who use QuickButtons**: YES - this fixes several annoying bugs that affect daily use
- **Shop Floor Users**: Strongly recommended - QuickButtons are more reliable and easier to use
- **If you see duplicate QuickButtons**: YES - this update cleans them up automatically
- **If QuickButtons aren't filling fields correctly**: YES - this fixes the display issue
- **Office Staff**: Update when convenient - QuickButtons improvements make data entry faster

**Version 6.0.0 (Transaction Viewer Redesign - RELEASED)**:
- **All users**: YES - this is a major improvement to one of the most-used features
- **Shop Floor Users**: Strongly recommended - much faster transaction searches, easier to find what you need
- **Office Staff**: Strongly recommended - analytics summaries and better filtering make reporting easier
- **Administrators**: Recommended - can now view all users' transactions with better filtering

**Version 5.9.0 (Settings & User Management Fixes)**:
- **All users**: Update when convenient - fixes Settings form crashes in development environments
- **Administrators**: Yes - faster user deletion and simplified part number management
- **Shop Floor Users**: No urgent need unless you access Settings frequently

**Version 5.4.0 (View Error Reports)**:
- **IT Support & Developers**: Yes - this new feature helps you manage and investigate user-reported issues more efficiently
- **Shop Floor Users**: No - this doesn't change anything you interact with daily

**Version 5.3.1 (Display Fix)**:
- **Most users**: No urgent need - update when convenient
- **If you use multiple monitors or 4K displays**: Yes - this fixes annoying display issues

**Version 5.3.0 (Error Reporting)**:
- **All users**: Update when convenient - this is a helpful new feature, not a critical fix

---

### 🆘 Need Help?

**Questions about the update**: Contact John Koll at (ext. 323) or jkoll@mantoolmfg.com / Dan Smith at (ext. 311) or dsmith@mantoolmfg.com / Ka Lee at (ext. ___ ) or klee@mantoolmfg.com

**Found a problem**: Use the new **"Report Issue"** button in error messages (that's what it's for!)

**Can't login after update**: Contact John Koll at (ext. 323) or jkoll@mantoolmfg.com / Dan Smith at (ext. 311) or dsmith@mantoolmfg.com / Ka Lee at (ext. ___ ) or klee@mantoolmfg.com

---

### 📱 Common Questions

**Q: Why wasn't my theme saving before?**  
A: The code was calling a stored procedure that doesn't exist (`usr_ui_settings_Upsert`). Now it uses the correct procedure (`usr_ui_settings_SetThemeJson`) that properly merges your theme choice into your settings.

**Q: Will changing my theme affect other users?**  
A: No - themes are per-user. Your theme choice only affects your login, not anyone else's.

**Q: What happens to my other settings when I change themes?**  
A: Nothing - theme changes only update your `Theme_Name` setting. All your other preferences (grid column settings, shortcuts, etc.) remain unchanged.

**Q: Can I go back to the default theme?**  
A: Yes - just select "Default" from the theme dropdown and click Save.

**Q: Why does the app still show Lavender after I picked a different theme?**  
A: If you're on version 6.0.1 or earlier, the save function is broken. Update to 6.0.2 and your theme selection will save correctly.

**Q: Do themes change the layout or just the colors?**  
A: Themes only change colors and visual styling. The layout, buttons, and functionality remain exactly the same regardless of theme choice.

**Q: How do I know if I have duplicate QuickButtons?**  
A: If you see the same Part Number + Operation combination appearing more than once in your QuickButtons panel, you have duplicates. Update to version 6.0.1 and they'll be cleaned up automatically on next restart.

**Q: Why were my QuickButtons showing raw codes instead of descriptions?**  
A: This was a bug in how the buttons filled the dropdown fields. Version 6.0.1 fixes this - now they use proper value selection instead of text search.

**Q: How do I edit a QuickButton?**  
A: Right-click any QuickButton and select "Edit" from the menu. You can change the Part ID, Operation, or Quantity. Click OK to save changes.

**Q: Can I reorder my QuickButtons?**  
A: Yes! Right-click any QuickButton and select "Change Order". Drag and drop rows to rearrange them, or use Shift+Up/Down arrow keys to move selected rows. Click OK to save the new order.

**Q: Why do my newest QuickButtons appear at the top now?**  
A: This is the new intended behavior - most users want their most recent transactions at the top for quick access. Older transactions shift down automatically.

**Q: What happens to my existing QuickButtons after this update?**  
A: All your existing buttons remain unchanged. Duplicates are removed automatically on first load after update. Order stays the same unless you manually reorder them.

**Q: Will the Transaction Viewer look completely different?**  
A: Yes - the layout is much cleaner and more modern. Search filters at top, results on left, details on right. But all the same information is there, just better organized and faster to access.

**Q: Can I still search the old way?**  
A: The old interface has been completely replaced with the new one, but all your usual searches work the same or better. The new interface has more filter options and is faster.

**Q: Where did my transaction history go?**  
A: All 24,000+ historical transactions are still there and fully searchable. Nothing was deleted or changed - just the viewing interface is new.

**Q: How do I search for transactions from a specific part?**  
A: Click the Part Number dropdown at the top, start typing the part number (it autocompletes), select it, then click Search. You'll see all transactions for that part.

**Q: Can I search by multiple filters at once?**  
A: Yes! Fill in any combination of filters - Part Number, User, Locations, Operation, Date Range, Transaction Type, and Notes. All filters work together (AND logic).

**Q: What does the page jump feature do?**  
A: If you have 20 pages of results, you can type "15" in the page box and click Go to jump directly to page 15. Much faster than clicking Next 14 times.

**Q: How fast is the new search?**  
A: Most searches complete in under 2 seconds, even when searching 90 days of history. Large result sets (1000+ transactions) use pagination so the screen loads instantly.

**Q: Can I sort the results?**  
A: Yes! Click any column header to sort by that column. Click again to reverse the sort order.

**Q: What are the analytics cards at the top?**  
A: Those show real-time summaries of your current search: total transactions, total IN, total OUT, and total TRANSFER counts. They update automatically when you change your date range.

**Q: Why does it only show 50 transactions at a time?**  
A: Pagination keeps the interface fast and responsive. You can navigate through all results using the Previous/Next buttons or page jump. Total count is always displayed.

**Q: Can I change the page size to show more or fewer transactions?**  
A: The default is 50 per page. Contact IT if you need to adjust this setting for your workstation.

**Q: What's a "Transaction Lifecycle"?**  
A: (Coming next release) It's a visual timeline showing what happened to a batch of parts from start to finish. Example: 500 units came IN at Location A, then 250 were TRANSFERred to Location B, then 100 more moved to Location C. The tree view shows all these movements chronologically.

**Q: Why do I need to see batch splits?**  
A: (Coming next release) When tracking down inventory discrepancies or verifying transfers, seeing the complete history of where units went helps find issues faster. No more guessing which transactions are related to each other.

**Q: Will I lose my work when the app restarts for display changes?**  
A: No - the application saves your current state and restarts instantly

**Q: How do I change my Windows screen scale?**  
A: Go to Windows Settings → System → Display → Scale. You can choose 100%, 125%, 150%, 175%, or 200%. The app will prompt you to restart after you change this setting.

**Q: Do I have to submit error reports?**  
A: No, it's optional - but it helps us fix problems faster if you do

**Q: What information is sent in error reports?**  
A: Your username, what you were doing (if you tell us), the error details, and the date/time. No personal information or production data is sent.

**Q: Can I see my submitted reports?**  
A: Yes - Contact John Koll at (ext. 323) or jkoll@mantoolmfg.com / Dan Smith at (ext. 311) or dsmith@mantoolmfg.com / Ka Lee at (ext. ___ ) or klee@mantoolmfg.com and provide your Report ID number

**Q: What if I submit a report by accident?**  
A: No problem - Contact John Koll at (ext. 323) or jkoll@mantoolmfg.com / Dan Smith at (ext. 311) or dsmith@mantoolmfg.com / Ka Lee at (ext. ___ ) or klee@mantoolmfg.com

---

### 🎉 Coming Soon

We're working on even more improvements:

**Next Updates (Planned)**:
- **Transaction Lifecycle Viewer** (Week of November 11th) - See complete batch history with visual tree showing splits and movements
- **Print & Export** (Week of November 11th) - Export to Excel, print formatted reports, PDF generation
- **Enhanced Print Dialog** - Modern print preview with compact sidebar, column management, page range selection
- **Enhanced Developer Settings Menu** - More tools and options for IT staff and power users
- Better visual feedback during network issues
- Inventory dashboard with real-time statistics
- Quick action buttons for common tasks

---

**Last Updated**: November 8, 2025  
**Questions?** Contact John Koll at (ext. 323) or jkoll@mantoolmfg.com / Dan Smith at (ext. 311) or dsmith@mantoolmfg.com / Ka Lee at (ext. ___ ) or klee@mantoolmfg.com
