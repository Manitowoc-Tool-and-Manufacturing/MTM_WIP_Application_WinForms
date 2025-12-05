# Tasks: Infor Visual Dashboard

**Feature Branch**: `014-visual-dashboard`
**Spec**: [specs/014-visual-dashboard/spec.md](specs/014-visual-dashboard/spec.md)

## Phase 1: Setup
*Goal: Initialize project structure and dependencies.*

- [x] T001 Install `System.Data.SqlClient` NuGet package
- [x] T002 Create folder structure: `Forms/Visual`, `Services/Visual`, `Resources/Sql/Visual`, `specs/014-visual-dashboard/prompts`
- [x] T003 Create `Enum_VisualDashboardCategory` in `Models/Enum_VisualDashboardCategory.cs`
- [x] T004 Create AI Prompt Asset pairs (instruction/prompt) for 'Inventory' in `specs/014-visual-dashboard/prompts/`
- [x] T005 Create AI Prompt Asset pairs (instruction/prompt) for 'Receiving' in `specs/014-visual-dashboard/prompts/`
- [x] T006 Create AI Prompt Asset pairs (instruction/prompt) for 'Shipping' in `specs/014-visual-dashboard/prompts/`
- [x] T007 Create AI Prompt Asset pairs (instruction/prompt) for 'InventoryAuditing' in `specs/014-visual-dashboard/prompts/`
- [x] T008 Create AI Prompt Asset pairs (instruction/prompt) for 'DieToolDiscovery' in `specs/014-visual-dashboard/prompts/`
- [x] T009 Create AI Prompt Asset pairs (instruction/prompt) for 'MaterialHandlerAnalytics_General' in `specs/014-visual-dashboard/prompts/`
- [x] T010 Create AI Prompt Asset pairs (instruction/prompt) for 'MaterialHandlerAnalytics_Team' in `specs/014-visual-dashboard/prompts/`

## Phase 2: Foundational
*Goal: Implement core services and shared controls required for the dashboard.*

- [x] T011 [P] Create `Control_EmptyState` in `Controls/Shared/Control_EmptyState.cs` (Refactor from `Control_AdvancedRemove` logic if applicable)
- [x] T012 [P] Define `IService_VisualDatabase` interface in `Services/Visual/IService_VisualDatabase.cs`
- [x] T013 Implement `Service_VisualDatabase` in `Services/Visual/Service_VisualDatabase.cs` (Connection management, `TestConnectionAsync`)
- [x] T014 Implement `GetDashboardDataAsync` skeleton in `Service_VisualDatabase.cs` to load embedded SQL resources

## Phase 3: User Story 1 - Secure Access to Dashboard (P1)
*Goal: Allow authorized users to access the dashboard shell.*
*Independent Test: Verify menu option appears/functions based on credentials.*

- [x] T015 [US1] Create `InforVisualDashboard` form in `Forms/Visual/InforVisualDashboard.cs` (Inherit `ThemedForm`)
- [x] T016 [US1] Add "Visual" menu option to `MainForm.Designer.cs` and `MainForm.cs`
- [x] T017 [US1] Implement credential check logic in `MainForm.cs` (Check `Model_Application_Variables.User.VisualUserName`)
- [x] T018 [US1] Implement `InforVisualDashboard_Load` to call `Service_VisualDatabase.TestConnectionAsync`
- [x] T019 [US1] Handle connection failures in `InforVisualDashboard.cs` using `Service_ErrorHandler`

## Phase 4: User Story 2 - View Inventory & Operations Data (P1)
*Goal: Display read-only data from Infor Visual.*
*Independent Test: Verify data loads for each category and matches ERP.*

- [x] T020 [P] [US2] Create embedded SQL file `Resources/Sql/Visual/Inventory.sql`
- [x] T021 [P] [US2] Create embedded SQL file `Resources/Sql/Visual/Receiving.sql`
- [x] T022 [P] [US2] Create embedded SQL file `Resources/Sql/Visual/Shipping.sql`
- [x] T023 [P] [US2] Create embedded SQL file `Resources/Sql/Visual/InventoryAuditing.sql`
- [x] T024 [P] [US2] Create embedded SQL file `Resources/Sql/Visual/DieToolDiscovery.sql`
- [x] T025 [P] [US2] Create embedded SQL file `Resources/Sql/Visual/MaterialHandlerAnalytics_General.sql`
- [x] T026 [P] [US2] Create embedded SQL file `Resources/Sql/Visual/MaterialHandlerAnalytics_Team.sql`
- [x] T027 [US2] Implement Sidebar Navigation in `InforVisualDashboard.cs` (Buttons for each category)
- [x] T028 [US2] Implement Data Grid display logic in `InforVisualDashboard.cs` (Bind `DataTable` from Service)
- [x] T029 [US2] Implement "Loading" state (Spinner or Text) in `InforVisualDashboard.cs`
- [x] T030 [US2] Integrate `Control_EmptyState` into `InforVisualDashboard.cs` for 0-row results

## Phase 5: User Story 3 - Modern & Responsive Interface (P2)
*Goal: Polish the UI and add productivity features.*
*Independent Test: Resize window, verify layout, test export.*

- [x] T031 [US3] Apply modern styling to Sidebar and Grid in `InforVisualDashboard.cs` (Padding, Colors, Flat Styles)
- [x] T032 [US3] Implement Responsive Anchoring/Docking in `InforVisualDashboard.Designer.cs`
- [x] T033 [US3] Implement "Export to CSV" functionality in `InforVisualDashboard.cs` (Use `Helper_ExportManager` if available or ClosedXML)
- [x] T034 [US3] Enable Column Sorting in DataGridView in `InforVisualDashboard.cs`

## Phase 6: Polish & Cross-Cutting
*Goal: Final cleanup and verification.*

- [x] T035 Verify all public members have XML Documentation
- [x] T036 Verify `Service_ErrorHandler` is used for all exceptions (No `MessageBox.Show`)
- [x] T037 Verify `LoggingUtility` captures SQL connection errors
- [x] T038 Final Code Review against Constitution

## Dependencies

1. **Setup** (T001-T010) must be completed first.
2. **Foundational** (T011-T014) blocks all User Stories.
3. **US1** (T015-T019) blocks US2 (need the form to show data).
4. **US2** (T020-T030) blocks US3 (need data to style/export).

## Parallel Execution Examples

*   **SQL & Prompts**: T004-T010 and T020-T026 can be done in parallel by different developers or agents once the folder structure is set.
*   **UI & Service**: T011 (EmptyState) and T012/T013 (Service) can be developed independently.

## Implementation Strategy

1.  **MVP (US1 + US2 Inventory)**: Get the connection working and show one category of data (Inventory). This proves the architecture.
2.  **Expansion (Rest of US2)**: Add the remaining 6 SQL queries and categories.
3.  **Refinement (US3)**: Style the UI and add Export/Sort features.
