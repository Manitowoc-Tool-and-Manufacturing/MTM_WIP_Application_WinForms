# Frequently Asked Questions - MTM WIP Application

> **Common questions about recent updates**  
> Last Updated: November 13, 2025

---

## 📑 Table of Contents

1. [General Questions](#general-questions)
2. [Themes (6.0.2 - 6.1.0)](#themes-602---610)
3. [QuickButtons (6.0.1 - 6.0.2)](#quickbuttons-601---602)
4. [Transaction Viewer (6.0.0)](#transaction-viewer-600)
5. [Smart Autocomplete (6.2.0)](#smart-autocomplete-620)
6. [Startup Arguments (6.2.1)](#startup-arguments-621)
7. [Troubleshooting](#troubleshooting)

---

## General Questions

### Q: How do I know which version I'm running?
**A:** Look at the bottom-right corner of the main window. You'll see "Version X.X.X" in the status bar.

### Q: Do I lose my work when updating?
**A:** No. All your data is stored in the database, not the application. Updates only change the program files, not your inventory data.

### Q: How long does an update take?
**A:** Most updates install in 2-5 minutes. You'll need to close the app, run the installer, and restart.

### Q: Can I skip versions (e.g., go from 5.9 to 6.2.1)?
**A:** Yes! You can jump directly to the latest version. All improvements from skipped versions are included.

### Q: Will my settings be preserved?
**A:** Yes. Your theme, QuickButtons, and preferences are saved in the database and persist across updates.

### Q: What if something breaks after updating?
**A:** Contact IT immediately. They can roll back to the previous version if needed (takes 10-15 minutes).

---

## Themes (6.0.2 - 6.1.0)

### Q: Why wasn't my theme saving before version 6.0.2?
**A:** The code was calling a stored procedure that doesn't exist. Version 6.0.2 fixed this to use the correct database procedure.

### Q: Will changing my theme affect other users?
**A:** No. Themes are per-user. Your choice only affects your login, not anyone else's.

### Q: What happens to my other settings when I change themes?
**A:** Nothing. Theme changes only update your color scheme. All your other preferences (grid columns, shortcuts, etc.) remain unchanged.

### Q: Can I go back to the default theme?
**A:** Yes. Open Settings → Theme tab → Select "Default" → Click Save.

### Q: Do themes change the layout or just colors?
**A:** Just colors and visual styling. The layout, buttons, and functionality remain exactly the same.

### Q: How many themes are available?
**A:** 10 themes: Arctic, Default, Fire Storm, Glacier, Ice, Lavender, Midnight, Neon, Sunset, and Wood.

### Q: Can I preview themes before saving? (Version 6.1.0+)
**A:** Yes! In Settings → Theme tab, click through different themes. All open windows update instantly so you can preview before saving.

### Q: Why do theme changes happen so fast now? (Version 6.1.0+)
**A:** Complete system overhaul. Themes now apply in under 100ms (was 300-500ms), and all windows update simultaneously.

---

## QuickButtons (6.0.1 - 6.0.2)

### Q: How do I know if I have duplicate QuickButtons?
**A:** If the same Part Number + Operation appears more than once in your QuickButtons panel, you have duplicates. Version 6.0.1+ cleans these automatically.

### Q: Why were my QuickButtons showing raw codes instead of descriptions?
**A:** This was a bug in how buttons filled dropdown fields. Version 6.0.1 fixed this - they now use proper value selection.

### Q: How do I edit a QuickButton?
**A:** Right-click any QuickButton → Select "Edit" → Modify Part ID, Operation, or Quantity → Click OK to save.

### Q: Can I reorder my QuickButtons?
**A:** Yes! Right-click any QuickButton → "Change Order" → Drag rows or use Shift+Up/Down arrows → Click OK to save.

### Q: Why do my newest QuickButtons appear at the top now?
**A:** This is the new intended behavior. Most users want recent transactions at the top for quick access. Older ones shift down automatically.

### Q: What happens to my existing QuickButtons after updating?
**A:** All existing buttons remain. Duplicates are removed automatically on first launch. Order stays the same unless you manually reorder.

### Q: Can I delete QuickButtons I don't use anymore?
**A:** Yes! Right-click the QuickButton → Select "Delete" → Confirm deletion.

---

## Transaction Viewer (6.0.0)

### Q: Will the Transaction Viewer look completely different?
**A:** Yes. The layout is much cleaner: search filters at top, results on left, details on right. But all the same information is there.

### Q: Can I still search the old way?
**A:** The old interface was completely replaced, but all your usual searches work the same or better with more filter options.

### Q: Where did my transaction history go?
**A:** All historical transactions are still there and fully searchable. Nothing was deleted - just the viewing interface is new.

### Q: How do I search for transactions from a specific part?
**A:** Click the Part Number dropdown at the top → Start typing the part number (autocomplete) → Select it → Click Search.

### Q: Can I search by multiple filters at once?
**A:** Yes! Fill in any combination of Part Number, User, Locations, Operation, Date Range, Transaction Type, and Notes. All filters work together.

### Q: What does the page jump feature do?
**A:** If you have 20 pages of results, type "15" in the page box and click Go to jump directly to page 15 (no clicking Next 14 times).

### Q: How fast is the new search?
**A:** Most searches complete in under 2 seconds, even when searching 90 days of history with thousands of transactions.

### Q: Can I sort the results?
**A:** Yes! Click any column header to sort by that column. Click again to reverse the sort order.

### Q: What are the analytics cards at the top?
**A:** Real-time summaries: Total Transactions, Total IN, Total OUT, Total TRANSFER counts for your current date range.

### Q: Why does it only show 50 transactions at a time?
**A:** Pagination keeps the interface fast. Navigate through all results using Previous/Next buttons or page jump. Total count is always displayed.

### Q: Can I change the page size?
**A:** The default is 50 per page. Contact IT if you need this adjusted for your workstation.

---

## Smart Autocomplete (6.2.0)

### Q: Why did dropdowns change to autocomplete?
**A:** Scrolling through 500+ parts was slow. Type-to-search is 3x faster and prevents typos.

### Q: Can I still use the old dropdown method?
**A:** No, but you can type exact codes and press Tab - it validates automatically without showing suggestions.

### Q: What if I don't remember the exact part number?
**A:** Just start typing anything you remember (e.g., "R-" or "21-"). Matching results appear instantly.

### Q: Do I have to use arrow keys to select?
**A:** No. You can double-click with the mouse, or just type the complete exact code and press Tab to auto-validate.

### Q: What are wildcard searches?
**A:** Type `%-A1-%` to find all locations containing "-A1-" anywhere in the code (DD-A1-01, DD-A1-02, EX-A1-01).

### Q: Will autocomplete work offline?
**A:** Yes. Autocomplete data loads when you open each tab and caches locally. Works even if database is slow.

### Q: Can I turn off autocomplete?
**A:** No, but power users can type exact codes and press Tab to bypass the suggestion list entirely.

### Q: Why does the suggestion list disappear when I press Escape?
**A:** Escape cancels the suggestion overlay and keeps whatever you typed. Useful if you want to type something unusual.

---

## Startup Arguments (6.2.1)

### Q: What are startup arguments?
**A:** Special commands you add to a desktop shortcut to control how the app launches (e.g., directly into Production or Test mode).

### Q: Why would I want to use this?
**A:** If you switch between Production and Test frequently, create two shortcuts - one for each environment. No manual switching needed.

### Q: How do I create a shortcut with startup arguments?
**A:** Right-click desktop → New Shortcut → Point to MTM WIP Application.exe → Edit the Target field → Add arguments like `-env=production`. See Help → "Startup Arguments" for full instructions.

### Q: What arguments are available?
- `-env=production` or `-env=test` - Launch into specific environment
- `-user="John Smith"` - Set username for logs (useful on shared workstations)
- `-server=localhost` - Specify database server (advanced)
- `-database=mtm_wip_application_winforms` - Specify database name (advanced)

### Q: Is it safe to put my password in a shortcut?
**A:** **No!** Anyone who can see the shortcut can see the password. Only use `-password=` on secured machines or test environments.

### Q: Can I have multiple shortcuts with different arguments?
**A:** Yes! Create separate shortcuts: "MTM WIP - Production" and "MTM WIP - Test" with different `-env=` arguments.

### Q: What if I type the wrong argument?
**A:** The app will show an error message and launch normally. Just correct the shortcut Target field.

---

## Troubleshooting

### Q: My theme keeps reverting to Lavender even after updating to 6.0.2
**A:** Clear your browser cache if using a web version, or contact IT to check your database user settings.

### Q: QuickButtons duplicates weren't removed after updating to 6.0.1
**A:** The cleanup runs automatically on first launch. Close and restart the app. If still present after restart, contact IT.

### Q: Autocomplete suggestions don't appear when I type
**A:** 
1. Make sure you're on version 6.2.0 or later
2. Press Tab or click out of the field to trigger suggestions
3. Check that you're typing in a supported field (Part Number, Operation, or Location)
4. Contact IT if still not working

### Q: I can't select anything in the autocomplete list
**A:** Try these keyboard shortcuts:
- **↑/↓ arrows**: Navigate list
- **Enter**: Select highlighted item
- **Escape**: Cancel and keep what you typed
- **Double-click**: Select with mouse

### Q: Save button won't enable even though I filled all fields (Version 6.2.0+)
**A:** 
1. Make sure you typed **valid** codes (system validates against master data)
2. Press Tab after each field to trigger validation
3. Check that Part Number, Operation, and Location match existing database entries
4. If entering a new part, add it in Settings → Part Numbers first

### Q: Transaction Viewer search is slow
**A:** 
1. Narrow your date range (search 1 week instead of 90 days)
2. Use more specific filters (add Part Number or User)
3. Clear your search filters and try again
4. Contact IT if searches consistently take over 5 seconds

### Q: Error message says "Unable to connect to database"
**A:** 
1. Check your network connection
2. Verify database server is running (ask IT)
3. Try restarting the application
4. Contact IT if problem persists

### Q: I accidentally deleted inventory and need to undo it
**A:** 
1. Contact IT immediately - they can restore from transaction logs
2. Note the Part ID, Location, and approximate time of deletion
3. Future versions (6.2.0+) have confirmation dialogs to prevent this

### Q: Application won't start after update
**A:**
1. Try restarting your computer
2. Check that you have .NET 8.0 installed
3. Contact IT - they may need to reinstall

### Q: Where do I report bugs or issues?
**A:** Click the "Report Issue" button when you see an error message, or contact:
- John Koll (ext. 323) - jkoll@mantoolmfg.com
- Dan Smith (ext. 311) - dsmith@mantoolmfg.com
- Ka Lee (ext. TBD) - klee@mantoolmfg.com

---

## 🔗 Related Documentation

- [What's New](WHATS_NEW.md) - Latest features and improvements
- [Release History](RELEASE_HISTORY.md) - Full changelog since October 2025
- [Developer Notes](DEVELOPER_CHANGELOG.md) - Technical details for IT/developers

---

**Last Updated**: November 13, 2025  
**Questions not answered here?** Contact IT Support
