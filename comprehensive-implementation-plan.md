# Comprehensive Implementation Plan

This document consolidates tasks from `refactor-error-handling.md`, `refactor-suggestion-controls.md`, and `AdditonalUpdatesNeeded.md` into a prioritized, safe-to-implement checklist.

## I. Master Implementation Checklist

### Phase 1: Database & Core Infrastructure
*These tasks establish the foundation for feature updates and should be completed first to prevent dependency errors.*

- [ ] **Update Enums**: Add missing types to `Enum_SuggestionDataSource.cs` (ItemType, Building, Warehouse, Infor types).
- [ ] **Database Schema**: Create new table `sys_visual` in `mtm_wip_application_winforms` database.
    - [ ] Add column `json_shift_data` (JSON type).
    - [ ] Add column `json_user_fullnames` (JSON type).
- [ ] **Backend Logic (User Shifts)**: Implement logic to calculate user shifts based on Infor Visual Transaction History (Last 50 transactions).
    - [ ] Rules: 1st (06:00-14:00), 2nd (14:00-22:00), 3rd (22:00-06:00), Weekend (Fri-Mon 06:00-18:00).
- [ ] **Backend Logic (User Names)**: Implement logic to map Visual UserNames to Full Names using Visual database.

### Phase 2: Technical Refactoring (High Stability Impact)
*Refactoring existing controls to use new patterns. This reduces technical debt before adding new features.*

#### Error Handling Refactoring
- [ ] **MainForm.cs**: Replace `MessageBox.Show` with `Service_ErrorHandler`.
- [ ] **SettingsForm.cs**: Replace `MessageBox.Show` with `Service_ErrorHandler`.
- [ ] **Dialog_EditParameterOverride.cs**: Replace `MessageBox.Show` with `Service_ErrorHandler`.
- [ ] **Form_QuickButtonEdit.cs**: Replace `MessageBox.Show` with `Service_ErrorHandler`.
- [ ] **Service_OnStartup_AppLifecycle.cs**: Review and replace `MessageBox.Show` where safe.

#### Suggestion Control Refactoring
- [ ] **Control_ReceivingAnalytics.cs**: Update `SuggestionDataSource` and review `SuggestionSelected`/`KeyDown` events.
- [ ] **Control_VisualInventory.cs**: Update `SuggestionDataSource` properties.
- [ ] **Control_InventoryAudit.cs**: Update `SuggestionDataSource` and review events.
- [ ] **Control_PartIDManagement.cs**: Update `SuggestionDataSource` and set `SelectionAction = None` for manual handlers.
- [ ] **Control_OperationManagement.cs**: Update `SuggestionDataSource` and set `SelectionAction = None` for manual handlers.
- [ ] **Control_LocationManagement.cs**: Update `SuggestionDataSource` and set `SelectionAction = None` for manual handlers.
- [ ] **Control_ItemTypeManagement.cs**: Review events and set `SelectionAction = None`.
- [ ] **TransactionSearchControl.cs**: Update `SuggestionDataSource` properties.

### Phase 3: Administrative & Maintenance Features
*Updates to the Development/Maintenance tools to support the new database features.*

- [ ] **MainForm (Dev Menu)**: Add "InforVisual Related" Groupbox to Database Maintenance view.
- [ ] **Shift Update Button**: Add button to trigger "Update User Shifts" logic (populates `sys_visual.json_shift_data`).
- [ ] **Name Update Button**: Add button to trigger "Update User Full Names" logic (populates `sys_visual.json_user_fullnames`).
- [ ] **MainForm (Menu Structure)**: Move "MaterialHandlerAnalytics" from Development to View menu.
- [ ] **MainForm (Menu Structure)**: Hide "Help" menu item.

### Phase 4: Feature Updates - Inventory & Analytics
*Enhancing existing analytics and inventory controls.*

- [ ] **Control_InventoryAudit.cs (General)**: Add Date Range Radio Buttons (Today, This Week [Default], Month, Quarter, Year, Custom). Disable calendars unless "Custom" is selected.
- [ ] **Control_InventoryAudit.cs (User Analytics)**:
    - [ ] Update "Load Users" to display Full Names from `sys_visual` (Format: `JKOLL (John Koll)`).
    - [ ] Add Date Range Radio Buttons (same logic as above).
    - [ ] Add Shift Filter Checkboxes (First, Second, Third, Weekend).
- [ ] **Control_MaterialHandlerAnalytics.cs**:
    - [ ] Refactor to match Visual Inventory Audit User Analytics style.
    - [ ] Create new HTML template for graphs/scoring.
    - [ ] Implement Scoring: 1pt (Add/Remove), 2pt (Transfer).
    - [ ] Remove tabs: Quality & Anomalies, User Detail, Glossary.
- [ ] **Control_ReceivingAnalytics.cs**:
    - [ ] Ensure toggle buttons respect `Control_Theme` animation settings.
    - [ ] Fix Layout: Filters, PO State, Receiving Scope groupboxes should extend to bottom of Row 1.

### Phase 5: Feature Updates - Search & Discovery
*Updates to search tools and detailed views.*

- [ ] **Control_AdvancedInventory.cs**: Add F4 buttons for Part, Operation, and Location fields.
- [ ] **Control_DieToolDiscovery.cs (General)**: Update "Enter Part Number or Die Number" to accept both Part Numbers and FGTs.
- [ ] **Control_DieToolDiscovery.cs (Coil/Flatstock)**:
    - [ ] Implement logic to find Auto-issue Location ID from InforVisual CSVs.
    - [ ] Add "Where Used" button (Shows Work Orders, Parts, FGTs in DGV). Disable if MMC/MMF selected.
- [ ] **Form_PODetails.cs**: Complete Refactor.
    - [ ] Remove DataGridView.
    - [ ] Implement Layout: TextBoxes with Labels for columns.
    - [ ] Add Navigation: Next/Back buttons for PO Lines (Disable if single line).
    - [ ] Add RichTextBox: Display Line Specs from InforVisual CSVs.

---

## II. User Stories

### 1. System Stability & Standards
*   **US-1.1**: As a developer, I want all error messages to use `Service_ErrorHandler` so that logs are consistent and the UI is uniform.
*   **US-1.2**: As a developer, I want suggestion text boxes to be configured via Enums in the designer so that code is cleaner and configuration is centralized.

### 2. Administrative Tools
*   **US-2.1**: As an admin, I want to update cached User Shift and Full Name data from Infor Visual via the Database Maintenance menu so that analytics reports are accurate.
*   **US-2.2**: As a user, I want the "Help" menu hidden and "Material Handler Analytics" moved to the View menu to streamline the navigation bar.

### 3. Inventory Audit & Analytics
*   **US-3.1**: As a manager, I want to filter Inventory Audit logs by quick date ranges (Today, Week, Month, etc.) so I don't have to manually select dates.
*   **US-3.2**: As a manager, I want to see full names (e.g., "John Koll") instead of just usernames in User Analytics so I can easily identify employees.
*   **US-3.3**: As a manager, I want to filter User Analytics by Shift (1st, 2nd, 3rd) so I can compare performance across teams.
*   **US-3.4**: As a manager, I want Material Handler Analytics to score performance (1pt moves, 2pt transfers) and show simple graphs so I can evaluate handler efficiency.

### 4. Search & Discovery
*   **US-4.1**: As a user, I want to search Die Tool Discovery using either a Part Number or an FGT number so I can find tools regardless of which ID I have.
*   **US-4.2**: As a user, I want to see the "Auto-issue Location" for Coil/Flatstock items so I know where materials are staged.
*   **US-4.3**: As a user, I want to click "Where Used" on a part to see all associated Work Orders and FGTs so I can understand part usage.
*   **US-4.4**: As a user, I want to view PO Details in a form view with Next/Back buttons and full Line Specs so I can read detailed requirements without scrolling a grid.

---

## III. Clarification Questions & Edge Cases

### Category A: Database & Data Integrity
**Q1: How should the `sys_visual` table handle users who are no longer active in Infor Visual?**
*   A) Remove them from the JSON data entirely.
*   B) Keep them but mark as "Inactive".
*   C) Keep them as-is to preserve historical data for analytics.
*   *Suggested Answer: C) Keep them. Analytics often look at historical data where those users were active. Removing them might break reports for past date ranges.*

**Q2: For the "User Shift" calculation, what happens if a user's transaction history is empty or sparse (less than 50)?**
*   A) Mark as "Unknown".
*   B) Use whatever transactions are available, even if only 1.
*   C) Default to 1st Shift.
*   *Suggested Answer: B) Use available transactions. If 0 transactions, mark as "Unknown". This provides the best guess with available data.*

### Category B: UI/UX Behavior
**Q3: In `Form_PODetails`, if a PO has 50 lines, is "Next/Back" navigation sufficient, or should there be a "Jump to Line" feature?**
*   A) Next/Back is fine.
*   B) Add a dropdown to select Line Number.
*   C) Keep the GridView as a secondary navigation tool.
*   *Suggested Answer: B) Add a dropdown or simple "Line X of Y" input. Clicking "Next" 49 times is poor UX for large orders.*

**Q4: For `Control_DieToolDiscovery` "Where Used", what defines "Used"?**
*   A) Only open Work Orders.
*   B) Open and Closed Work Orders (History).
*   C) Bill of Materials (BOM) definition only.
*   *Suggested Answer: C) BOM definition. "Where Used" typically implies "Where is this part designed to be used?" rather than "Where was it used yesterday?". However, if the goal is tracking, B might be better. Clarification needed on intent.*

### Category C: Technical Implementation
**Q5: For `Control_MaterialHandlerAnalytics` scoring, do "Transfers" include bin-to-bin moves, or only warehouse-to-warehouse?**
*   A) Any transaction classified as a "Transfer" in the database.
*   B) Only specific transaction codes.
*   *Suggested Answer: A) Any transaction classified as "Transfer". Keep logic simple and consistent with existing transaction types.*

**Q6: Regarding `Control_ReceivingAnalytics` layout fixes, if the groupboxes extend to the bottom, how should they handle resizing?**
*   A) Fixed height.
*   B) Anchor Top/Bottom.
*   C) Fill Dock style in TableLayoutPanel.
*   *Suggested Answer: C) Fill Dock style. This ensures they resize dynamically with the form/resolution.*
