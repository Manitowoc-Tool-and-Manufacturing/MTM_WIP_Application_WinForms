# User Analytics Feature - Questions & Suggestions

## Feature Overview
The goal is to add a "User Analytics" tab to `Control_InventoryAudit` that allows comparing up to 10 users' inventory transaction performance over a selected date range. The visualization will use an HTML-based dashboard similar to `ReceivingAnalytics_Enhanced.html`.

## Questions

### 1. Transaction Type Definitions
To correctly filter the data, I need to know the specific values in the `INVENTORY_TRANS` table (specifically the `TYPE` column) that correspond to your categories.

*   **Work Order Entries:** Is this identified by `WORKORDER_BASE_ID` being NOT NULL? Or is there a specific `TYPE` code (e.g., 'W', 'R')?  Defined by Workorder_Base_ID not being null AND PART_ID not null
*   **Location to Location:** Is this a specific `TYPE` code (e.g., 'T', 'TRANSFER')? Or is it identified by having both a source and destination location?  Location to location can be found by looking for a transaction where the same part number was removed from one location and added to a different location at the same exact time, as visual does 2 trasactions for this out - in.  Also PART_ID must not be null for any of the transaction types.  Transaction Types are "I" for in and "O" for out.  

*   **Adjusted In:** Is this `TYPE` = 'I' , No matching CREATE_DATE (Date and time of this filed must NOT match any other transaction)
*   **Adjusted Out:** Is this `TYPE` = 'O' , No matching CREATE_DATE (Date and time of this filed must NOT match any other transaction)

### 2. Date Range Clarification
You mentioned "current day 12am to current day 12pm (24 hours)".
*   **Clarification:** Does "12pm" mean noon (12:00 PM) or midnight (12:00 AM next day)? Midnight - 11:59pm
*   **Assumption:** If you mean a full 24-hour cycle, the range would be `00:00:00` to `23:59:59`. - Correct

### 3. HTML Template Strategy
*   **New File:** Should I create a new HTML file (e.g., `UserAnalytics.html`) based on the design of `ReceivingAnalytics_Enhanced.html`, or should I try to make the existing file dynamic enough to handle both?
    *   *Recommendation:* Create a new file to keep the logic clean and specific to User Analytics.
    Create New File

### 4. User Selection Logic
*   **Source:** You mentioned pulling "all transaction data" to get the user list.
*   **Optimization:** If the date range is large, pulling all rows just to get names might be slow. Can we use a `SELECT DISTINCT USER_ID` query instead? Yes

## Suggestions

### 1. Data Fetching Strategy
Instead of pulling raw transaction rows and processing them in C# (which might be heavy for the UI), I suggest adding a dedicated method in `Service_VisualDatabase` (e.g., `GetUserAnalyticsDataAsync`) that performs the aggregation in SQL.
*   **Why:** Faster performance, less data transfer.
*   **Output:** A list of objects containing `{ User, TransactionType, Count, TotalQty }`.
Ok, but for Location to Location add "From Location" (the location in the Out Transaction ID) and a "To Location" (Location in the matching In Trasaction ID)

### 2. UI Implementation
*   **User Selection:** Use a `CheckedListBox` for selecting users. It's compact and intuitive for "up to 10" selections.
*   **Validation:** Add logic to disable the "Generate" button or show a warning if more than 10 users are checked.
    Show a Label that shows the current selection count {Selected Users} / 10. if {Selected Users} >= 10 disable Generate Button, show / hide label with warning text

### 3. HTML/JS Integration
*   **Data Injection:** I will inject the data into the HTML using `WebView2.CoreWebView2.ExecuteScriptAsync` to set a JavaScript variable (e.g., `window.analyticsData = ...;`) before calling the render function.
*   **Chart Library:** The existing HTML uses Chart.js. I will stick with that for consistency.
If you need to create more than 1 type of file for the HTML to make this easier, and more user friendly do so.

### 4. Proposed "User Analytics" Tab Layout
*   **Top:** Date Range Pickers (Start/End).
*   **Left:** `CheckedListBox` for Users (populated after Date Range selection).
*   **Right/Bottom:** `WebView2` control to display the dashboard.
*   **Action:** "Load Users" button (after date change) and "Generate Report" button (after user selection).

Top - Date Range Pickers
Middle - User CheckListBox
Bottom - "Load Users" Button / "Generate Report" Button / "Reset Button"

HTML File will load in its own window to maximize space

## Next Steps
Once you clarify the Transaction Type definitions, I can proceed with:
1.  Creating the `UserAnalytics.html` template.
2.  Updating `Service_VisualDatabase.cs` with the new query methods.
3.  Modifying `Control_InventoryAudit.cs` to add the new tab and logic.

## Further Questions & Refinements (Added Dec 1)

### 1. Location-to-Location Logic
You requested "From Location" and "To Location" for transfers. This implies that for the **Table View**, we need to list individual transactions, not just aggregated counts.
*   **Assumption:** The "Chart View" will still use aggregated counts (e.g., "User A: 5 Transfers"), but the "Table View" will list the details (e.g., "User A | Transfer | Loc A -> Loc B | Qty 10").
*   **SQL Strategy:** I will write a query that returns a unified list of "Analytics Events" rather than raw `INVENTORY_TRANS` rows.
    *   Columns: `User`, `Type` (WO, Transfer, AdjIn, AdjOut), `Part`, `Qty`, `Date`, `FromLoc`, `ToLoc`, `WorkOrder`.
    *   For Transfers, I will pair the 'Out' and 'In' transactions in SQL to present them as a single line item with From/To.

### 2. "Own Window" Implementation
You mentioned "HTML File will load in its own window to maximize space".
*   **Plan:** I will create a new Form `Form_AnalyticsViewer` that contains the `WebView2` control.
I believe one already exists
*   **Workflow:**
    1.  User selects Date Range & Users in `Control_InventoryAudit`.
    2.  User clicks "Generate Report".
    3.  App fetches data.
    4.  App opens `Form_AnalyticsViewer` (popup) and loads the HTML with the data.

### 3. Data Volume Warning
Since we are fetching detailed records (for the table) and not just counts, selecting a large date range for 10 users might return a lot of data.
*   **Refinement:** I will add a `TOP 5000` limit or similar to the query to prevent memory issues, with a warning if the limit is reached.

### 4. SQL Logic for "Adjustments" (Orphans)
To identify "Adjusted In" (Orphan I) and "Adjusted Out" (Orphan O), I need to ensure they *don't* match a corresponding transaction.
*   **Logic:**
    *   **Transfer:** `t1.TYPE='O'` AND `t2.TYPE='I'` AND `t1.CREATE_DATE = t2.CREATE_DATE` AND `t1.PART_ID = t2.PART_ID`.
    *   **Adj In:** `TYPE='I'` AND NOT EXISTS (matching 'O' at same time).
    *   **Adj Out:** `TYPE='O'` AND NOT EXISTS (matching 'I' at same time).
    *   **WO:** `WORKORDER_BASE_ID` IS NOT NULL (takes precedence over Adj/Transfer logic? Or can a WO transaction also be a Transfer? usually WO is separate).
    *   *Refinement:* I will prioritize WO check first. If `WORKORDER_BASE_ID` is not null, it's a WO entry. If null, then check Transfer/Adj logic.

I will proceed with this understanding.
