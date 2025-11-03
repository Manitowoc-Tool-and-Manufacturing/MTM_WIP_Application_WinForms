# MTM WIP Application - What's New

> **User-friendly release notes for shop floor and office staff**

---

## Latest Update - November 2, 2025 (Version 6.0.1)

**What Changed**: User Management, Part Number Management, and Settings Form improvements  
**Do I Need To Do Anything?**: No - these fixes make existing features work better

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

## Previous Update - November 1, 2025 (Version 6.0.0 - IN PROGRESS)

**What Changed**: Major improvements to Transaction History Viewer (HIGHLY REQUESTED!)  
**Do I Need To Do Anything?**: Yes - check out the new features when update is released!

---

### 🎯 What This Means For You

#### Faster, Easier Transaction History Search

**What's Better**:
- **Cleaner layout**: Search filters at top, results on left, details on right - everything visible at once
- **Faster searches**: Find transactions in under 2 seconds (even searching 90 days of history)
- **More filter options**: Search by Part Number, User, Location, Operation, Date Range, Transaction Type (IN/OUT/TRANSFER), and even Notes
- **Better pagination**: Navigate through large result sets with clear "Page 1 of 5" controls
- **Instant details**: Click any transaction to see full details on the right side (no more popup windows)
- **Remembers your searches**: Last search criteria stay filled in when you come back

**How to use**:
1. Click **Transaction History** from main menu (same as before)
2. **Fill in any search filters** at the top:
   - Part Number (dropdown shows your recent parts)
   - User (see everyone's transactions or just yours)
   - Location (FROM and TO locations for transfers)
   - Operation (manufacturing routing step)
   - Date Range (Today/This Week/This Month/Custom)
   - Transaction Types (check IN, OUT, or TRANSFER)
   - Notes keyword (find transactions with specific notes)
3. Click **Search** button
4. **Browse results** in the grid on the left
5. **Click any row** to see full details on the right

**Why this helps**:
- **Find transactions 3x faster** than the old interface
- **Less clicking** - everything on one screen instead of multiple windows
- **Better for multi-tasking** - details panel lets you keep the search results visible
- **Easier to track down issues** - more filter combinations help pinpoint specific transactions

#### 🎉 Coming in Next Phase (Week of November 4th)

**Transaction Life Cycle Viewer** (BRAND NEW FEATURE):
- **See the complete history** of a batch from start to finish
- **Visual tree view** shows how inventory splits and moves between locations
- **Track batch splits**: See exactly when 500 units became 250 at one location and 250 at another
- **Chronological timeline**: Every IN, TRANSFER, and OUT transaction in order
- **Click "Transaction Life Cycle" button** in detail panel to open the viewer

**Export to Excel**:
- Export your search results to Excel for reporting or analysis
- All visible columns included
- One-click export from Search or Detail view

**Print Transaction Reports**:
- Print transaction details for physical records
- Formatted for standard 8.5x11 paper
- Includes all transaction information and notes

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

**Version 6.0.0 (Transaction Viewer - IN PROGRESS)**:
- **All users**: Yes - this is a major improvement to one of the most-used features
- **Shop Floor Users**: Recommended - much faster transaction searches and better detail viewing
- **Office Staff**: Recommended - export to Excel makes reporting easier

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

**Q: Will the Transaction Viewer look completely different?**  
A: The layout is cleaner, but the same information is there - just better organized. Search filters at top, results on left, details on right.

**Q: Can I still search the old way?**  
A: The new interface is easier and faster, but uses the same underlying search. All your usual searches will work the same or better.

**Q: What's a "Transaction Life Cycle"?**  
A: It's a visual timeline showing what happened to a batch of parts. Example: 500 units came IN at Location A, then 250 were TRANSFERred to Location B, then 100 more moved to Location C. The tree view shows all these movements in order.

**Q: Why do I need to see batch splits?**  
A: When tracking down inventory discrepancies or verifying transfers, seeing the complete history of where units went helps find issues faster. No more guessing which transactions are related.

**Q: Will old transaction history still be searchable?**  
A: Yes - all 24,000+ historical transactions remain searchable. Nothing is deleted or changed.

**Q: How fast is the new search?**  
A: Most searches complete in under 2 seconds, even when searching 90 days of history. Large result sets (1000+ transactions) use pagination so the screen loads quickly.

**Q: Can I export filtered results or all results?**  
A: You can export the current page, current filter results, or all transactions matching your search (coming in next phase).

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
- **View Application Logs** - See what the application is doing behind the scenes (helpful for troubleshooting)
- **Enhanced Developer Settings Menu** - More tools and options for IT staff and power users
- Enhanced multi-monitor support and display scaling improvements
- Better visual feedback during network issues
- Inventory dashboard with real-time statistics
- Quick action buttons for common tasks

---

**Last Updated**: November 2, 2025  
**Questions?** Contact John Koll at (ext. 323) or jkoll@mantoolmfg.com / Dan Smith at (ext. 311) or dsmith@mantoolmfg.com / Ka Lee at (ext. ___ ) or klee@mantoolmfg.com
