# Task Breakdown: Developer Tools Suite Integration

**Branch**: `002-003-database-layer-complete`  
**Parent Spec**: [../spec.md](../spec.md)  
**Feature Spec**: [spec.md](./spec.md)  
**Plan**: [plan.md](./plan.md)  
**Created**: 2025-10-18

---

## Overview

This task breakdown implements the Developer Tools Suite Integration (002-003-001) as a sub-feature of the Comprehensive Database Layer Standardization initiative. Tasks are organized by user story priority (P1, P2, P3) to enable independent implementation and testing.

**Task Organization**:
- **Phase 1**: Setup & Infrastructure (T001-T005)
- **Phase 2**: Foundational Prerequisites (T006-T015)
- **Phase 3**: User Story 1 - Debug Dashboard (P1) (T016-T022)
- **Phase 4**: User Story 2 - Parameter Prefix Maintenance (P1) (T023-T035)
- **Phase 5**: User Story 3 - Schema Inspector (P2) (T036-T043)
- **Phase 6**: User Story 4 - Procedure Call Hierarchy (P2) (T044-T052)
- **Phase 7**: User Story 5 - Code Generator (P3) (T053-T060)
- **Phase 8**: Control_Database Integration (T061-T063)
- **Phase 9**: Polish & Integration (T064-T070)

**Total Tasks**: 70  
**Estimated Duration**: 3-4 weeks (2 developers)

---

## Dependencies

### Story Dependencies
- **US1 (Debug Dashboard)**: Independent - can implement immediately after foundational phase
- **US2 (Parameter Prefix)**: Independent - can implement immediately after foundational phase
- **US3 (Schema Inspector)**: Independent - can implement immediately after foundational phase
- **US4 (Call Hierarchy)**: Independent - can implement immediately after foundational phase
- **US5 (Code Generator)**: Independent - can implement immediately after foundational phase
- **Control_Database Refactor**: Independent - existing control move

### Blocking Prerequisites
All user stories require completion of:
- Phase 1 (Setup)
- Phase 2 (Foundational) - specifically:
  - T006: Database table creation
  - T007-T012: Stored procedure creation
  - T013: Model class
  - T014: DAO class
  - T015: Helper_Database_StoredProcedure override cache

---

## Phase 1: Setup & Infrastructure

**Goal**: Prepare development environment and enable Developer role for testing.

- [x] **T001** – Grant JOHNK Developer Role
  - **Reference**: `.github/instructions/mysql-database.instructions.md`
- [x] **T002** – Verify Database Analysis Artifacts Exist
  - **Reference**: Parent spec `../tasks.md` (T100-T106)
- [x] **T003** – Create Developer Tools Documentation Directory
- [x] **T004** – Update .gitignore for Developer Tools Artifacts
  - **Reference**: `.github/instructions/security-best-practices.instructions.md`
- [x] **T005** – Review Existing Settings Form Integration Patterns
  - **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` (WinForms Patterns section)

---

## Phase 2: Foundational Prerequisites (BLOCKING)

**Goal**: Create database infrastructure and base classes required by ALL developer tools.

**CRITICAL**: These tasks MUST complete before any user story implementation can begin.

- [x] **T006** – Create sys_parameter_prefix_overrides Table
  - **Reference**: `contracts/stored-procedures.md` + `.github/instructions/mysql-database.instructions.md`
- [x] **T007** – Create sys_parameter_prefix_overrides_Get_All Stored Procedure
  - **Reference**: `contracts/stored-procedures.md` (Procedure #1) + `.github/instructions/mysql-database.instructions.md`
- [x] **T008** – Create sys_parameter_prefix_overrides_Get_ById Stored Procedure
  - **Reference**: `contracts/stored-procedures.md` (Procedure #2) + `.github/instructions/mysql-database.instructions.md`
- [x] **T009** – Create sys_parameter_prefix_overrides_Add Stored Procedure
  - **Reference**: `contracts/stored-procedures.md` (Procedure #3) + `.github/instructions/mysql-database.instructions.md`
- [x] **T010** – Create sys_parameter_prefix_overrides_Update_ById Stored Procedure
  - **Reference**: `contracts/stored-procedures.md` (Procedure #4) + `.github/instructions/mysql-database.instructions.md`
- [x] **T011** – Create sys_parameter_prefix_overrides_Delete_ById Stored Procedure
  - **Reference**: `contracts/stored-procedures.md` (Procedure #5) + `.github/instructions/mysql-database.instructions.md`
- [x] **T012** – Test All 5 Stored Procedures Together
  - **Reference**: `.github/instructions/integration-testing.instructions.md`
- [x] **T013** – Create Model_ParameterPrefixOverride POCO
  - **Reference**: `.github/instructions/csharp-dotnet8.instructions.md`
- [x] **T014** – Create Dao_ParameterPrefixOverrides DAO Class
  - **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` + `.github/instructions/integration-testing.instructions.md`
- [x] **T015** – Extend Helper_Database_StoredProcedure with Override Cache
  - **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` + `.github/instructions/performance-optimization.instructions.md`

---

## Phase 3: User Story 1 – Debug Dashboard (P1)

**Goal**: Convert standalone DebugDashboardForm into Settings-integrated UserControl.

- [x] **T016** – [US1] Convert DebugDashboardForm to Control_Developer_DebugDashboard
  - **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` (WinForms Patterns)
- [x] **T017** – [US1] Add Developer Category to Settings TreeView
  - **Reference**: `.github/instructions/csharp-dotnet8.instructions.md`
- [x] **T018** – [US1] Implement TreeView Selection Handler for Debug Dashboard
  - **Reference**: `.github/instructions/csharp-dotnet8.instructions.md`
- [x] **T019** – [US1] Test Debug Dashboard Service_DebugTracer Integration
  - **Reference**: `.github/instructions/testing-standards.instructions.md`
- [x] **T020** – [US1] Add Debug Dashboard Progress Bar Integration
  - **Reference**: `.github/instructions/csharp-dotnet8.instructions.md`
- [x] **T021** – [US1] Document Debug Dashboard Usage in Quickstart
  - **Reference**: `.github/instructions/documentation.instructions.md`
- [x] **T022** – [US1] Integration Test – Debug Dashboard End-to-End
  - **Reference**: `.github/instructions/integration-testing.instructions.md`

---

## Phase 4: User Story 2 – Parameter Prefix Maintenance (P1)

**Goal**: Build full CRUD interface for parameter prefix override management.

- [x] **T023** – [US2] Create Control_Developer_ParameterPrefixMaintenance UserControl
  - **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` (WinForms Patterns)
- [ ] **T024** – [US2] Implement Add Override Dialog
  - **Reference**: `.github/instructions/csharp-dotnet8.instructions.md`
- [ ] **T025** – [US2] Implement Edit Override Dialog
  - **Reference**: `.github/instructions/csharp-dotnet8.instructions.md`
- [ ] **T026** – [US2] Implement Delete Override Confirmation
  - **Reference**: `.github/instructions/csharp-dotnet8.instructions.md`
- [ ] **T027** – [US2] Wire Up Add/Edit/Delete Button Handlers
  - **Reference**: `.github/instructions/csharp-dotnet8.instructions.md`
- [ ] **T028** – [US2] Add Procedure Name Autocomplete
  - **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` + `.github/instructions/performance-optimization.instructions.md`
- [ ] **T029** – [US2] Add Parameter Name Autocomplete (Context-Aware)
  - **Reference**: `.github/instructions/csharp-dotnet8.instructions.md`
- [ ] **T030** – [US2] Add TreeView Node for Parameter Prefix Maintenance
  - **Reference**: `.github/instructions/csharp-dotnet8.instructions.md`
- [ ] **T031** – [US2] Test Parameter Override End-to-End Workflow
  - **Reference**: `.github/instructions/testing-standards.instructions.md`
- [ ] **T032** – [US2] Test Duplicate Detection
  - **Reference**: `.github/instructions/testing-standards.instructions.md`
- [ ] **T033** – [US2] Test Non-Existent Procedure Warning
  - **Reference**: `.github/instructions/testing-standards.instructions.md`
- [ ] **T034** – [US2] Document Parameter Prefix Maintenance Usage
  - **Reference**: `.github/instructions/documentation.instructions.md`
- [ ] **T035** – [US2] Integration Test – Parameter Prefix Maintenance End-to-End
  - **Reference**: `.github/instructions/integration-testing.instructions.md`

---

## Phase 5: User Story 3 – Schema Inspector (P2)

**Goal**: Add read-only database schema browsing tool.

- [ ] **T036** – [US3] Create Control_Developer_SchemaInspector UserControl
  - **Reference**: `.github/instructions/csharp-dotnet8.instructions.md`
- [ ] **T037** – [US3] Populate TreeView with Tables and Procedures
  - **Reference**: `.github/instructions/mysql-database.instructions.md`
- [ ] **T038** – [US3] Implement Table Selection Handler
  - **Reference**: `.github/instructions/csharp-dotnet8.instructions.md`
- [ ] **T039** – [US3] Implement Stored Procedure Selection Handler
  - **Reference**: `.github/instructions/csharp-dotnet8.instructions.md`
- [ ] **T040** – [US3] Add Search/Filter Functionality
  - **Reference**: `.github/instructions/csharp-dotnet8.instructions.md`
- [ ] **T041** – [US3] Add Copy to Clipboard Buttons
  - **Reference**: `.github/instructions/csharp-dotnet8.instructions.md`
- [ ] **T042** – [US3] Add TreeView Node and Integration
  - **Reference**: `.github/instructions/csharp-dotnet8.instructions.md`
- [ ] **T043** – [US3] Integration Test – Schema Inspector End-to-End
  - **Reference**: `.github/instructions/integration-testing.instructions.md`

---

## Phase 6: User Story 4 – Procedure Call Hierarchy (P2)

**Goal**: Visualize stored procedure call dependencies.

- [ ] **T044** – [US4] Create Control_Developer_ProcedureCallHierarchy UserControl
  - **Reference**: `.github/instructions/csharp-dotnet8.instructions.md`
- [ ] **T045** – [US4] Load call-hierarchy-complete.json
  - **Reference**: `.github/instructions/csharp-dotnet8.instructions.md`
- [ ] **T046** – [US4] Build Procedure Dependency Graph
  - **Reference**: `.github/instructions/csharp-dotnet8.instructions.md`
- [ ] **T047** – [US4] Implement Procedure Selection Handler
  - **Reference**: `.github/instructions/csharp-dotnet8.instructions.md`
- [ ] **T048** – [US4] Add Search/Filter Functionality
  - **Reference**: `.github/instructions/csharp-dotnet8.instructions.md`
- [ ] **T049** – [US4] Add Export to PlantUML
  - **Reference**: `.github/instructions/csharp-dotnet8.instructions.md`
- [ ] **T050** – [US4] Add Circular Dependency Detection
  - **Reference**: `.github/instructions/csharp-dotnet8.instructions.md`
- [ ] **T051** – [US4] Add TreeView Node and Integration
  - **Reference**: `.github/instructions/csharp-dotnet8.instructions.md`
- [ ] **T052** – [US4] Integration Test – Procedure Call Hierarchy End-to-End
  - **Reference**: `.github/instructions/integration-testing.instructions.md`

---

## Phase 7: User Story 5 – Code Generator (P3)

**Goal**: Generate DAO wrapper code from stored procedures.

- [ ] **T053** – [US5] Create Control_Developer_CodeGenerator UserControl
  - **Reference**: `.github/instructions/csharp-dotnet8.instructions.md`
- [ ] **T054** – [US5] Load Stored Procedure Metadata
  - **Reference**: `.github/instructions/mysql-database.instructions.md`
- [ ] **T055** – [US5] Generate DAO Method Template
  - **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` + `.github/instructions/documentation.instructions.md`
- [ ] **T056** – [US5] Add Parameter Mapping Logic
  - **Reference**: `.github/instructions/csharp-dotnet8.instructions.md`
- [ ] **T057** – [US5] Add Copy to Clipboard / Save to File
  - **Reference**: `.github/instructions/csharp-dotnet8.instructions.md`
- [ ] **T058** – [US5] Add Template Customization Options
  - **Reference**: `.github/instructions/csharp-dotnet8.instructions.md`
- [ ] **T059** – [US5] Add TreeView Node and Integration
  - **Reference**: `.github/instructions/csharp-dotnet8.instructions.md`
- [ ] **T060** – [US5] Integration Test – Code Generator End-to-End
  - **Reference**: `.github/instructions/integration-testing.instructions.md`

---

## Phase 8: Control_Database Integration

**Goal**: Move existing Control_Database into Developer Tools suite.

- [ ] **T061** – [US3+] Add Schema Inspector Tab to Control_Database
  - **Reference**: `.github/instructions/csharp-dotnet8.instructions.md`
- [ ] **T062** – [US4+] Add Call Hierarchy Tab to Control_Database
  - **Reference**: `.github/instructions/csharp-dotnet8.instructions.md`
- [ ] **T063** – Test Control_Database Multi-Tab Functionality
  - **Reference**: `.github/instructions/testing-standards.instructions.md`

---

## Phase 9: Polish & Integration

**Goal**: Final testing, documentation, and quality assurance.

- [ ] **T064** – Verify All Developer Tools Respect IsDeveloper Role
  - **Reference**: `.github/instructions/security-best-practices.instructions.md`
- [ ] **T065** – Test All Tools with Non-Developer Account
  - **Reference**: `.github/instructions/testing-standards.instructions.md`
- [ ] **T066** – Run Full Integration Test Suite
  - **Reference**: `.github/instructions/integration-testing.instructions.md`
- [ ] **T067** – Update User Documentation with Screenshots
  - **Reference**: `.github/instructions/documentation.instructions.md`
- [ ] **T068** – Update Quickstart Guide with All Tools
  - **Reference**: `.github/instructions/documentation.instructions.md`
- [ ] **T069** – Code Review and Refactoring Pass
  - **Reference**: `.github/instructions/code-review-standards.instructions.md`
- [ ] **T070** – Final Validation and Sign-Off
  - **Reference**: `.github/instructions/testing-standards.instructions.md`

---

## Checkpoint Reviews

1. **Checkpoint A** – Phase 1 & 2 complete (T001-T015) ✔
2. **Checkpoint B** – US1 complete (T016-T022) ✔
3. **Checkpoint C** – US2 complete (T023-T035)
4. **Checkpoint D** – US3 complete (T036-T043)
5. **Checkpoint E** – US4 complete (T044-T052)
6. **Checkpoint F** – US5 complete (T053-T060)
7. **Checkpoint G** – Integration complete (T061-T063)
8. **Checkpoint H** – Polish complete (T064-T070)

Each checkpoint requires validation before moving forward.

---

## Progress Tracking Guidance

- Update this file at the end of each working session with completion markers and dates.
- Use indented bullet points under tasks to document progress notes.
- Reference instruction files guide implementation patterns.
