# Feature Proposal: MTM WIP Application Update

This document outlines the proposed new features and improvements for the next version of the MTM WIP Application. Please review the items below and indicate which features you would like to include in this release.

## 1. Inventory Audit & Analytics Improvements

### ✅ Quick Date Filters
**Current State:** Users must manually select start and end dates from a calendar.
**Proposed Change:** Add quick-select radio buttons for common ranges:
*   Today
*   This Week (Default)
*   This Month
*   This Quarter
*   Custom (Enables the calendar pickers)
**Benefit:** Faster reporting for standard timeframes.

### ✅ Enhanced User Analytics
**Current State:** Reports show raw usernames (e.g., `JKOLL`) and lack shift context.
**Proposed Change:**
*   **Full Names:** Display "John Koll (JKOLL)" instead of just the ID.
*   **Shift Filtering:** Add checkboxes to filter data by Shift (1st, 2nd, 3rd, Weekend).
**Benefit:** Easier identification of employees and ability to compare performance across shifts.

### ✅ Material Handler Performance Dashboard
**Current State:** Basic data view.
**Proposed Change:** A completely redesigned dashboard featuring:
*   **Fair Scoring System:** Points awarded based on transaction type (1pt for Add/Remove, 2pt for Transfers), weighted by shift difficulty (busier shifts = normalized score).
*   **New Visual Charts:**
    *   **Hot Parts:** Top 10 most moved parts.
    *   **Location Heatmap:** Top 10 locations visited.
    *   **Shift Performance:** Moves per shift.
    *   **Transaction Types:** Breakdown of Issues vs. Receipts vs. Transfers.
**Benefit:** Provides actionable insights into material handler efficiency and workload distribution.

## 2. Receiving Analytics Enhancements

### ✅ Vendor & Receipt Insights
**Current State:** Focuses mainly on line counts.
**Proposed Change:** Add new analytical tools:
*   **Vendor Scorecard:** Top 10 Vendors by receipt volume.
*   **Receipts by Hour:** A heatmap showing the busiest times of day for receiving.
*   **Fair Grading:** Performance grades (A-F) that account for how busy the shift was (Shift Difficulty Factor).
**Benefit:** Helps identify peak receiving times and top suppliers.

## 3. Search & Discovery Tools

### ✅ Die Tool Discovery Upgrades
**Current State:** Search is limited to specific ID types.
**Proposed Change:**
*   **Flexible Search:** "Enter Part Number or Die Number" box will accept *either* a Part Number OR an FGT Number.
*   **Coil/Flatstock "Where Used":** New button to see all Work Orders and FGTs associated with a specific coil or flatstock item.
*   **Auto-Issue Location:** Display the default "Auto-Issue" location for Coil/Flatstock items (sourced from Visual).
**Benefit:** Reduces time spent looking up cross-referenced part numbers and locations.

### ✅ PO Details "Form View"
**Current State:** Displays PO lines in a spreadsheet-like grid that requires scrolling.
**Proposed Change:** A simplified "Form View" for reading POs:
*   **Layout:** Header info at the top, large text box for Line Specs/Description at the bottom.
*   **Navigation:** "Next" and "Previous" buttons to flip through PO lines one by one.
**Benefit:** Much easier to read detailed line specifications and instructions without horizontal scrolling.

### ✅ Advanced Inventory Search
**Current State:** Text boxes for Part, Operation, and Location are manual entry only.
**Proposed Change:** Add "F4" search buttons to these fields to open the standard search/lookup window.
**Benefit:** Consistent search experience across the application.

## 4. Menu & Navigation Cleanup

### ✅ Streamlined Menu
**Current State:** "Material Handler Analytics" is hidden in the Development menu; "Help" menu is unused.
**Proposed Change:**
*   Move "Material Handler Analytics" to the main **View** menu.
*   Hide the **Help** menu item.
**Benefit:** Cleaner interface with easier access to frequently used tools.
