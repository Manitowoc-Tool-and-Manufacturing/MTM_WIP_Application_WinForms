# Feature Specification: Interactive MySQL 5.7 Stored Procedure Builder

**Feature Branch**: `004-interactive-mysql-5`  
**Created**: 2025-10-17  
**Status**: Ready for Planning  
**Input**: User description: "Interactive MySQL 5.7 Stored Procedure Builder - multi-file HTML/CSS/JavaScript application for visually designing, validating, and exporting stored procedures with wizard interface and live database connectivity"

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Create New Stored Procedure from Scratch (Priority: P1)

A database developer needs to create a new inventory adjustment stored procedure. They open the builder, name the procedure following domain_table_action pattern, add required parameters (inventory ID, quantity delta, user ID), define validation rules (inventory exists, quantity is positive), add an UPDATE operation to adjust inventory quantity, add an INSERT operation to log the transaction, preview the generated SQL, and export it as a .sql file.

**Why this priority**: This is the core value proposition - enabling rapid creation of standard CRUD procedures without manual SQL writing. Delivers immediate time savings and consistency.

**Independent Test**: Can be fully tested by creating a simple single-table UPDATE procedure (e.g., update inventory quantity) from start to finish and verifying the exported SQL executes successfully in MySQL 5.7.

**Acceptance Scenarios**:

1. **Given** builder is open with blank procedure, **When** developer enters procedure name "inv_inventory_Adjust_Quantity", adds parameters (p_InventoryID INT IN, p_QuantityDelta INT IN, p_Status INT OUT, p_ErrorMsg VARCHAR(500) OUT), defines UPDATE operation on Inventory table setting Quantity = Quantity + p_QuantityDelta WHERE InventoryID = p_InventoryID, **Then** system generates syntactically valid MySQL 5.7 SQL with proper DELIMITER statements, transaction control, and status handling.

2. **Given** procedure definition is complete, **When** developer clicks Export, **Then** system generates .sql file named correctly (inv_inventory_Adjust_Quantity.sql), includes header comment with metadata, includes DROP PROCEDURE IF EXISTS, and prompts developer to run analysis script.

3. **Given** developer has defined validation rules (check p_QuantityDelta > 0, check inventory record exists), **When** validation fails, **Then** generated SQL includes ROLLBACK, sets p_Status to appropriate error code, and sets p_ErrorMsg to developer-defined message.

---

### User Story 2 - Edit Existing Stored Procedure (Priority: P1)

A developer needs to modify an existing stored procedure (e.g., inv_inventory_Add_Item). They open the procedure review tool, select the procedure, click "Edit in Builder", and the builder pre-populates all fields by parsing the existing SQL. They add a new validation check (verify user has permission), reorder DML operations in the visual flow diagram, update the error message for duplicate key scenario, preview side-by-side comparison of original vs modified SQL, and export the updated procedure.

**Why this priority**: Editing existing procedures is as critical as creating new ones. The ability to pre-populate from existing SQL and show visual diffs prevents errors and saves significant time.

**Independent Test**: Load an existing simple stored procedure (e.g., one with 1 parameter and 1 INSERT operation), make a single change (e.g., add a validation check), and verify the exported SQL reflects the change accurately.

**Acceptance Scenarios**:

1. **Given** procedure review tool shows inv_inventory_Add_Item, **When** developer clicks "Edit in Builder", **Then** builder parses existing SQL and pre-populates procedure name, all IN/OUT parameters with correct types, all DECLARE statements as local variables, all validation checks in order, all DML operations (INSERT/UPDATE/DELETE/SELECT) with table/column mappings, and transaction control settings.

2. **Given** developer modifies validation logic (adds new check for p_Quantity > 0), **When** developer views Preview, **Then** preview shows side-by-side diff highlighting added validation block in green.

3. **Given** visual flow diagram shows 4 operations (validate, check duplicate, INSERT, log), **When** developer drags INSERT operation before check duplicate, **Then** generated SQL reflects new operation order with INSERT before duplicate key check.

---

### User Story 3 - Use Templates for Common Patterns (Priority: P2)

A developer needs to create a batch processing procedure (generate unique batch numbers for multiple parts). Instead of building from scratch, they select "Batch Operations" template from library, which pre-fills parameters (p_PartNumbers VARCHAR list, p_BatchPrefix VARCHAR, p_UserID INT), includes WHILE loop structure, includes batch sequence table interaction pattern, and includes audit logging. Developer customizes table names and column mappings, then exports.

**Why this priority**: Templates accelerate development for repetitive patterns. Provides consistency across similar procedures and teaches best practices.

**Independent Test**: Select a CRUD template, customize it with a single table name and 3 columns, and verify the exported SQL performs all four CRUD operations correctly.

**Acceptance Scenarios**:

1. **Given** developer selects "CRUD Operations" template, **When** developer specifies table name (Parts) and columns (PartNumber, Description, IsActive), **Then** builder generates 4 procedures (Create, Read, Update, Delete) with appropriate parameters, validation, DML operations, and status handling.

2. **Given** developer selects "Audit Logging" template, **When** developer specifies source table (Inventory) and audit table (Inventory_History), **Then** builder generates INSERT into audit table with trigger-style column mapping (OLD.* values to History_* columns, NOW() for audit timestamp, p_UserID for audit user).

3. **Given** developer customizes a template by adding extra validation, **When** developer saves as custom template with name "Inventory Transfer Extended", **Then** custom template appears in template library under user-defined category and can be reused for future procedures.

---

### User Story 4 - Visual Flow Diagram for Complex Procedures (Priority: P2)

A developer is creating a multi-step transfer procedure (remove from location A, add to location B, log transaction, update batch status). They use the visual flow diagram to arrange operation cards in sequence, draw conditional branches (if quantity insufficient, ROLLBACK and exit), and connect parameter outputs to subsequent operation inputs. The diagram auto-updates as they add operations in other wizard steps.

**Why this priority**: Complex procedures with 5+ operations and conditional logic are hard to visualize in pure SQL. Flow diagrams reduce errors in operation sequencing and branching logic.

**Independent Test**: Create a procedure with 3 sequential operations and 1 conditional branch, arrange them in flow diagram, and verify the exported SQL executes operations in diagram order with correct IF/ELSE structure.

**Acceptance Scenarios**:

1. **Given** developer has defined 5 DML operations, **When** developer opens Flow Diagram step, **Then** diagram displays 5 draggable cards labeled with operation type and target table, connected by arrows showing execution sequence.

2. **Given** developer drags "Check Inventory Exists" operation before "Update Inventory" operation in diagram, **When** developer returns to DML Operations step, **Then** operation list reflects new order.

3. **Given** developer adds IF condition (v_RowsAffected = 0) after UPDATE operation, **When** developer connects ROLLBACK operation to ELSE branch, **Then** generated SQL includes IF v_RowsAffected = 0 THEN [success path] ELSE ROLLBACK; SET p_Status = -3; SET p_ErrorMsg = 'No rows updated'; END IF.

---

### User Story 5 - Database Metadata Integration (Priority: P1)

Developer is building an INSERT operation for the Parts table. When they select "Parts" from table dropdown, the column list auto-populates with all columns from live database (PartNumber VARCHAR(50), Description VARCHAR(200), CreatedDate DATETIME, CreatedUser INT, IsActive BOOLEAN). Developer checks which columns to include in INSERT, and the builder shows smart defaults (CreatedDate = NOW(), CreatedUser = p_UserID, IsActive = 1). Developer overrides defaults where needed.

**Why this priority**: Live database metadata eliminates manual typing, prevents column name typos, ensures type safety, and teaches developers the actual schema structure.

**Independent Test**: Connect to test database, select any table, and verify column list matches actual database schema with correct data types.

**Acceptance Scenarios**:

1. **Given** builder is connected to mtm_wip_application_winforms_test database, **When** developer selects "Inventory" table in INSERT builder, **Then** column checkboxes show all columns with types in parentheses (InventoryID INT AUTO_INCREMENT - grayed out, PartNumber VARCHAR(50), Quantity DECIMAL(10,2), LocationCode VARCHAR(20), ReceivedDate DATETIME, ReceivedUser INT, LastUpdated DATETIME, LastUpdatedUser INT, IsActive BOOLEAN).

2. **Given** developer checks columns PartNumber, Quantity, LocationCode, ReceivedDate, ReceivedUser, IsActive for INSERT, **When** builder shows value mapping, **Then** smart suggestions appear (ReceivedDate = NOW(), ReceivedUser = p_UserID, IsActive = 1) with option to override.

3. **Given** database connection fails, **When** developer tries to load table metadata, **Then** system displays clear error message with connection troubleshooting steps and offers to retry connection.

---

### User Story 6 - Validation Logic Builder with Drag-Drop (Priority: P2)

Developer needs to validate multiple inputs (p_PartNumber not empty, p_Quantity > 0, p_LocationCode exists in Locations table). They drag pre-built validation check cards (Required Field, Positive Number, Foreign Key Check) from palette to validation sequence area, configure each with specific parameter names and error messages, and reorder by dragging. Generated SQL includes all checks in order with proper ROLLBACK and status setting.

**Why this priority**: Validation is critical for data integrity but repetitive to write manually. Drag-drop interface reduces errors and ensures consistent error handling patterns.

**Independent Test**: Create 2 validation rules (required field + positive number check), configure error messages, and verify exported SQL includes both validations with correct ROLLBACK logic.

**Acceptance Scenarios**:

1. **Given** developer drags "Required Field Check" card to validation area, **When** developer configures it for p_PartNumber, **Then** card displays configuration form (Parameter: dropdown of defined parameters, Error Message: text field defaulting to "Part Number is required", Status Code: dropdown defaulting to -1).

2. **Given** developer has configured 3 validation checks, **When** developer drags second check to first position, **Then** validation sequence updates and generated SQL shows reordered validation logic.

3. **Given** developer selects "Foreign Key Check" validation, **When** developer configures it (Parameter: p_LocationCode, Reference Table: Locations, Reference Column: LocationCode), **Then** generated SQL includes SELECT COUNT(*) INTO v_Exists FROM Locations WHERE LocationCode = p_LocationCode; IF v_Exists = 0 THEN ROLLBACK; SET p_Status = -2; SET p_ErrorMsg = 'Invalid location code'; END IF.

---

### User Story 7 - DML Operation Builders with Auto-Complete (Priority: P1)

Developer is building an UPDATE operation. They select target table (Inventory), builder shows all columns as checkboxes for SET clause, developer checks Quantity and LastUpdated columns, maps Quantity to expression "Quantity + p_QuantityDelta", maps LastUpdated to NOW(), builds WHERE clause by selecting InventoryID column and operator (=) and value (p_InventoryID), and sees live SQL preview update as they make selections.

**Why this priority**: UPDATE/INSERT operations are the most common DML operations and most error-prone when hand-written (typos in column names, missing WHERE clause). Visual builder prevents SQL injection patterns and schema mismatches.

**Independent Test**: Build a single UPDATE operation setting 1 column with 1 WHERE condition, and verify exported SQL syntax is correct and executes without errors.

**Acceptance Scenarios**:

1. **Given** developer selects UPDATE operation and target table Inventory, **When** developer checks Quantity and LastUpdatedUser columns for SET clause, **Then** builder shows value input fields (Quantity: text field with suggestion "Quantity + p_QuantityDelta", LastUpdatedUser: dropdown with suggestions [p_UserID, literal value]).

2. **Given** developer builds WHERE clause, **When** developer adds condition InventoryID = p_InventoryID, **Then** SQL preview shows UPDATE Inventory SET Quantity = Quantity + p_QuantityDelta, LastUpdatedUser = p_UserID WHERE InventoryID = p_InventoryID; and tracks ROW_COUNT() into v_RowsAffected.

3. **Given** developer builds UPDATE without WHERE clause, **When** developer tries to proceed, **Then** system shows warning "UPDATE without WHERE clause will affect all rows. Are you sure?" with option to add WHERE condition.

4. **Given** developer selects INSERT operation, **When** developer uses ON DUPLICATE KEY UPDATE toggle, **Then** builder adds second column mapping section for UPDATE clause and generates INSERT ... ON DUPLICATE KEY UPDATE syntax.

---

### User Story 8 - Export with Version Control and Documentation (Priority: P3)

Developer has completed procedure definition. They click Export, system generates .sql file with header comment (author, date, description, parameters, affected tables), proper DELIMITER statements, DROP PROCEDURE IF EXISTS, procedure definition, and DELIMITER reset. Developer sees prompt to run RUN-COMPLETE-ANALYSIS.ps1. System logs this export to procedure version history. Developer opens side-by-side comparison view to see what changed from previous version.

**Why this priority**: Export is the final deliverable, but proper formatting and documentation are quality-of-life improvements rather than core functionality. Version tracking helps with auditing.

**Independent Test**: Export any procedure and verify the .sql file is properly formatted with all required components and executes without syntax errors in MySQL 5.7.

**Acceptance Scenarios**:

1. **Given** developer clicks Export for procedure inv_inventory_Adjust_Quantity, **When** export completes, **Then** generated file includes header comment block with Procedure Name, Description (from wizard), Parameters (all IN/OUT with types), Tables Accessed (Inventory, Inventory_Transactions), Author (from system user), Created Date, Modified Date.

2. **Given** procedure has been exported before (version exists), **When** developer exports updated version, **Then** system increments version number in header comment, shows side-by-side diff comparing previous version to current version with color-coded changes (green = added, red = removed, yellow = modified).

3. **Given** export is successful, **When** file is saved, **Then** system displays confirmation dialog with file path, file size, and button labeled "Run Analysis Script" which prompts developer to execute RUN-COMPLETE-ANALYSIS.ps1.

---

### User Story 9 - Advanced Features: Loops and Cursors (Priority: P3)

Developer needs to process multiple rows from a result set (batch processing scenario). They access Advanced Options section, select "Add Cursor" widget, configure cursor query (SELECT PartID, PartNumber FROM Parts WHERE BatchID = p_BatchID), define loop body operations (UPDATE each part), and add cursor cleanup. Builder generates DECLARE CURSOR, FETCH loop, and CLOSE CURSOR syntax compatible with MySQL 5.7.

**Why this priority**: Advanced features like cursors are needed infrequently but are complex to write correctly. This is a power-user feature that enhances capabilities but isn't needed for basic CRUD procedures.

**Independent Test**: Create a cursor that fetches 3 rows and executes 1 UPDATE per row, verify the exported SQL includes proper DECLARE CURSOR, FETCH loop with CONTINUE HANDLER, and CLOSE CURSOR.

**Acceptance Scenarios**:

1. **Given** developer selects "Add Cursor" in Advanced section, **When** developer enters cursor query and names cursor (cur_Parts), **Then** builder generates DECLARE cur_Parts CURSOR FOR [query]; DECLARE CONTINUE HANDLER FOR NOT FOUND SET v_Done = TRUE; OPEN cur_Parts; [fetch loop]; CLOSE cur_Parts;

2. **Given** developer adds operations inside cursor loop body, **When** developer adds UPDATE operation referencing cursor variables, **Then** generated SQL includes FETCH cur_Parts INTO v_PartID, v_PartNumber; WHILE NOT v_Done DO [UPDATE operation using v_PartID]; FETCH cur_Parts INTO v_PartID, v_PartNumber; END WHILE;

3. **Given** developer adds nested stored procedure call inside cursor loop, **When** developer configures CALL statement (procedure name sp_ProcessPart, parameters v_PartID, @status, @errMsg), **Then** generated SQL includes CALL sp_ProcessPart(v_PartID, @status, @errMsg); IF @status != 0 THEN [error handling]; END IF;

---

### User Story 10 - Inline Help and Tutorial System (Priority: P3)

First-time user opens builder and is greeted with optional tutorial walkthrough. Tutorial guides them through creating a simple INSERT procedure step-by-step with tooltips, highlights, and explanations. User can skip tutorial and access help sidebar at any time. Each form field has tooltip on hover explaining concepts in plain English for SQL beginners (e.g., "VARCHAR(50) means a text field that can hold up to 50 characters").

**Why this priority**: Reduces learning curve and increases adoption, but the builder should be intuitive enough to use without extensive help. This is a nice-to-have that improves user experience.

**Independent Test**: Open builder as new user, trigger tutorial walkthrough, complete first step (name procedure), and verify help text is clear and accurate.

**Acceptance Scenarios**:

1. **Given** user opens builder for first time (detected via localStorage), **When** builder loads, **Then** system displays modal "Welcome to Stored Procedure Builder - Would you like a guided tutorial?" with Start Tutorial and Skip buttons.

2. **Given** user clicks Start Tutorial, **When** tutorial begins, **Then** system dims background, highlights Procedure Name field, shows tooltip "Enter your procedure name following the pattern: domain_table_action (e.g., inv_inventory_Add_Item)", and waits for user to enter valid name before proceeding to next step.

3. **Given** user hovers over parameter type dropdown, **When** user hovers over "VARCHAR(50)" option, **Then** tooltip appears: "Text field with maximum length of 50 characters. Use for short text like names, codes, or descriptions."

4. **Given** user clicks help icon in navigation, **When** help sidebar opens, **Then** sidebar displays searchable help topics organized by category (Getting Started, Parameters, Validation, DML Operations, Advanced Features) with code examples for each.

---

### Edge Cases

- What happens when database connection is lost mid-wizard (e.g., MySQL server restarts)? System should detect connection loss, save wizard state to localStorage, display reconnect dialog, and allow user to resume after reconnection.

- How does system handle very large database schemas (100+ tables with 50+ columns each)? Table and column dropdowns should implement virtual scrolling, lazy loading, and type-ahead search filtering to prevent UI freezing.

- What happens when user tries to export procedure with same name as existing file? System should prompt for overwrite confirmation, offer to rename with timestamp suffix, or cancel export.

- How does system handle invalid MySQL 5.7 syntax in manually edited SQL (user switches to manual mode)? Client-side parser should highlight syntax errors with red underlines and error messages, but allow export with warning.

- What happens when user closes browser mid-wizard without exporting? System should auto-save wizard state to localStorage every 30 seconds and prompt user to restore session when returning.

- How does system handle procedures with 20+ DML operations in flow diagram? Flow diagram should support zoom/pan controls, minimap overview, and auto-layout algorithm to prevent visual clutter.

- What happens when user drags parameter into SQL field but parameter is undefined? System should show validation error immediately and highlight missing parameter definition.

- How does builder handle circular dependencies in nested stored procedure calls? System should detect and warn about circular CALL chains during validation phase.

- What happens when database schema changes while builder is open (e.g., column added to table)? System should periodically refresh metadata (every 5 minutes) or provide manual refresh button, and warn if cached metadata is stale.

- How does system handle UTF-8 special characters in procedure names or comments? Builder should properly encode/decode UTF-8 and validate that generated SQL is compatible with MySQL 5.7 character set settings.

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: System MUST connect directly to MySQL 5.7 database at localhost:3306/mtm_wip_application_winforms_test using credentials root/root and fetch real-time table/column metadata from information_schema.

- **FR-002**: System MUST provide wizard-style interface with sequential steps (Procedure Name → Parameters → Validation → DML Operations → Flow Diagram → Advanced → Preview → Export) with ability to navigate forward/backward between steps.

- **FR-003**: System MUST organize functionality into 11 separate HTML files (main, wizard, parameters, validation, DML operations, flow diagram, preview, templates, advanced, export, help) sharing common CSS and JavaScript libraries.

- **FR-004**: System MUST enforce procedure naming pattern domain_table_action (e.g., inv_inventory_Add_Item) with validation highlighting invalid names.

- **FR-005**: System MUST auto-include OUT parameters p_Status INT and p_ErrorMsg VARCHAR(500) on every generated procedure with option to customize default error messages.

- **FR-006**: System MUST enforce p_ prefix for all parameters and v_ prefix for all local variables, auto-adding prefixes if developer omits them.

- **FR-007**: System MUST provide parameter configuration interface with dropdowns for data types (VARCHAR with length, INT, DECIMAL with precision/scale, DATETIME, BOOLEAN, TEXT) showing plain-English tooltips explaining each type.

- **FR-008**: System MUST provide drag-and-drop validation rule builder with pre-built validation templates (Required Field, Positive Number, Date Range, String Length, Foreign Key Check) and custom validation option.

- **FR-009**: System MUST allow reordering validation rules by dragging and reflect order in generated SQL.

- **FR-010**: System MUST auto-add ROLLBACK to all validation failure branches with configurable status code and error message per validation.

- **FR-011**: System MUST provide visual DML operation builders for INSERT (with column selection, value mapping, ON DUPLICATE KEY UPDATE support), UPDATE (with SET clause builder, WHERE condition builder, ROW_COUNT tracking), DELETE (with WHERE builder and safety warnings), and SELECT (with column picker, JOIN support, ORDER BY/GROUP BY/LIMIT, FOUND_ROWS option).

- **FR-012**: System MUST populate table dropdowns from live database metadata and populate column checkboxes/dropdowns when table is selected.

- **FR-013**: System MUST provide smart defaults for common column patterns (User columns = p_UserID, Date columns = NOW(), IsActive = 1) with option to override.

- **FR-014**: System MUST show warning when UPDATE or DELETE operation has no WHERE clause and require explicit confirmation.

- **FR-015**: System MUST provide interactive flow diagram showing all operations as draggable cards connected by arrows, allowing visual reordering of operation sequence.

- **FR-016**: System MUST support conditional branching in flow diagram (IF/ELSE blocks) with visual connections showing condition paths.

- **FR-017**: System MUST update flow diagram automatically when operations are added/removed in DML Operations step and vice versa.

- **FR-018**: System MUST auto-include START TRANSACTION before first DML operation and COMMIT after last operation with toggle to disable transaction management.

- **FR-019**: System MUST provide live SQL preview panel with syntax highlighting that updates in real-time as wizard fields change.

- **FR-020**: System MUST validate generated SQL syntax using client-side JavaScript parser compatible with MySQL 5.7 syntax rules.

- **FR-021**: System MUST highlight syntax errors in preview with red underlines and show error messages with line numbers.

- **FR-022**: System MUST provide template library with pre-built templates for CRUD operations, Batch processing, Transfer operations, and Audit logging organized by category.

- **FR-023**: System MUST allow users to save current procedure definition as custom template with name and description.

- **FR-024**: System MUST support loading template which pre-populates all wizard fields with template values.

- **FR-025**: System MUST support editing existing procedures by parsing .sql file content and pre-populating wizard fields (procedure name, parameters, validation logic, DML operations, transaction control).

- **FR-026**: System MUST show side-by-side comparison view (original SQL vs generated SQL) with color-coded differences (green = added, red = removed, yellow = modified).

- **FR-027**: System MUST support Advanced Features section for WHILE loops, cursors (DECLARE CURSOR, FETCH, CLOSE), conditional branching (IF/ELSEIF/ELSE), and nested stored procedure calls (CALL statement).

- **FR-028**: System MUST generate cursor syntax compatible with MySQL 5.7 including DECLARE CONTINUE HANDLER FOR NOT FOUND.

- **FR-029**: System MUST support drag-and-drop of parameters into SQL expression fields creating visual links.

- **FR-030**: System MUST provide auto-complete for table names, column names, and defined parameters in text input fields.

- **FR-031**: System MUST export generated SQL as .sql file with DELIMITER statements, DROP PROCEDURE IF EXISTS, procedure definition, and DELIMITER reset.

- **FR-032**: System MUST include header comment in exported file with procedure name, description, parameters list, tables accessed, author, created date, modified date.

- **FR-033**: System MUST prompt user to run RUN-COMPLETE-ANALYSIS.ps1 after successful export.

- **FR-034**: System MUST save wizard state to browser localStorage every 30 seconds and offer to restore session if browser closed/refreshed mid-wizard.

- **FR-035**: System MUST provide tooltip help on all form fields explaining concepts in plain English suitable for SQL beginners.

- **FR-036**: System MUST provide optional first-time user tutorial walkthrough with step-by-step guided flow.

- **FR-037**: System MUST provide searchable help sidebar with categorized topics, code examples, and links to wizard steps.

- **FR-038**: System MUST load existing procedure analysis data from procedure-transaction-analysis.csv and SQLOperationsDetail JSON files to pre-populate metadata.

- **FR-039**: System MUST work in Chrome browser without server-side deployment (client-side only HTML/CSS/JavaScript).

- **FR-040**: System MUST validate all inputs and show validation errors inline with clear, actionable error messages.

### Key Entities

- **Procedure Definition**: Represents a stored procedure being built or edited. Key attributes: name (following domain_table_action pattern), description, parameters list (IN/OUT with types), local variables list (with types and default values), validation rules sequence (ordered list of checks with error messages and status codes), DML operations sequence (ordered list of INSERT/UPDATE/DELETE/SELECT with table/column mappings), transaction control settings (enabled/disabled, isolation level), advanced features configuration (loops, cursors, branches, nested calls), version number, author, created date, modified date.

- **Parameter**: Represents an input or output parameter for the procedure. Key attributes: name (with p_ prefix), direction (IN/OUT/INOUT), data type (VARCHAR/INT/DECIMAL/DATETIME/BOOLEAN/TEXT), length or precision/scale (for VARCHAR and DECIMAL), description, default value (optional), usage in operations (which DML operations reference this parameter).

- **Validation Rule**: Represents a data validation check executed before DML operations. Key attributes: rule type (Required Field/Positive Number/Date Range/String Length/Foreign Key/Custom), target parameter or expression, error message text, error status code, execution order position, condition logic (SQL expression for custom rules).

- **DML Operation**: Represents a single database operation (INSERT/UPDATE/DELETE/SELECT). Key attributes: operation type, target table name, column mappings (for INSERT/UPDATE), WHERE conditions (for UPDATE/DELETE/SELECT), SET clause expressions (for UPDATE), ON DUPLICATE KEY UPDATE clause (for INSERT), SELECT columns and joins (for SELECT), execution order position, output variable (for SELECT into variable), row count tracking flag.

- **Flow Diagram Node**: Represents a visual element in the operation sequence diagram. Key attributes: node type (DML operation/validation/conditional branch/loop/cursor), linked operation or validation reference, position coordinates (x, y), connections to other nodes (arrows showing execution flow), conditional expression (for IF/WHILE nodes), loop body nodes (for cursor/loop containers).

- **Template**: Represents a reusable procedure pattern. Key attributes: template name, category (CRUD/Batch/Transfer/Audit/Custom), description, pre-filled procedure definition (all wizard fields), customization points (which fields user must change vs optional), author (system-provided or user-created), usage count, last used date.

- **Database Table Metadata**: Represents a table in the connected database. Key attributes: table name, schema name, columns list (with names, types, nullable, default values, auto-increment flag), primary key columns, foreign key relationships, index information. Fetched from MySQL information_schema.

- **Export Configuration**: Represents settings for SQL file generation. Key attributes: file naming pattern, folder path selection, include DELIMITER flag, include DROP PROCEDURE flag, include header comment flag, version control enabled flag, auto-increment version number.

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: Developers can create a complete single-table CRUD stored procedure (with validation, INSERT, UPDATE, DELETE, SELECT operations) in under 10 minutes using the wizard without writing manual SQL.

- **SC-002**: Generated SQL passes MySQL 5.7 syntax validation 100% of the time when exported (no syntax errors requiring manual correction).

- **SC-003**: Builder successfully pre-populates wizard fields from existing .sql files for at least 90% of the 75 analyzed stored procedures in the project.

- **SC-004**: Visual flow diagram accurately represents operation sequence for procedures with up to 25 operations without requiring manual layout adjustment.

- **SC-005**: Drag-drop validation rule builder reduces time to add 5 common validation checks (required field, positive number, foreign key, date range, string length) by 70% compared to manual SQL writing.

- **SC-006**: Auto-complete feature reduces typing by at least 50% when building DML operations (measured by keystrokes saved on table/column name entry).

- **SC-007**: Template library covers 80% of common procedure patterns in the MTM application (CRUD, batch operations, inventory transfers, audit logging).

- **SC-008**: Side-by-side comparison view highlights 100% of differences between original and modified procedures with correct color coding.

- **SC-009**: At least 90% of form fields have helpful tooltips that explain SQL concepts in plain language understandable to developers with basic database knowledge.

- **SC-010**: System handles database schemas with up to 100 tables and 50 columns per table without UI freezing or lag exceeding 2 seconds on table/column selection.

- **SC-011**: Browser localStorage successfully preserves wizard state across page refreshes with 100% fidelity (all field values, operation sequence, validation rules restored exactly).

- **SC-012**: Exported .sql files are properly formatted with correct DELIMITER statements, header comments, and execute without errors in MySQL 5.7 for 100% of generated procedures.

- **SC-013**: First-time users complete the tutorial walkthrough and successfully create their first stored procedure within 20 minutes with 80% task completion rate.

- **SC-014**: Database metadata refresh completes within 5 seconds for schemas with up to 100 tables when user clicks refresh button or reconnects after connection loss.

- **SC-015**: Advanced features (cursors, loops, nested calls) generate syntactically correct MySQL 5.7 code 100% of the time as verified by MySQL validation.

## Assumptions

- Developers have MySQL 5.7 running locally via MAMP on default port 3306 with root/root credentials.
- Developers have MAMP installed with PHP enabled and mysqli/PDO extensions available.
- Developers have basic understanding of database concepts (tables, columns, primary keys, foreign keys) but may not be SQL experts.
- Chrome browser version 86 or higher is available with File System Access API support for template import/export.
- Database connection remains stable during wizard session (no frequent disconnections).
- Existing procedure analysis files (procedure-transaction-analysis.csv, SQLOperationsDetail JSON) are available and up-to-date.
- RUN-COMPLETE-ANALYSIS.ps1 script exists in Database folder and is executable.
- Developers are working on desktop computers (not mobile/tablet devices) with screen resolution of at least 1920x1080.
- Browser localStorage limit (typically 5-10MB) is sufficient to store wizard state.
- Database schema changes are infrequent (not constantly adding/removing tables during wizard usage).
- Developers prefer visual/wizard interfaces over manual SQL editing for routine CRUD procedures.
- Generated procedures follow MTM application patterns (p_ prefix for parameters, v_ prefix for variables, status/error output parameters, transaction control).
- Developers are comfortable with drag-and-drop interfaces and modern web application UX patterns.
- Team members share custom templates through Git commits to the StoredProcedureValidation/sp-builder/templates/ folder.
- PHP API endpoints can be accessed via relative paths from HTML files (same origin policy satisfied).

## Dependencies

- MySQL 5.7 database server accessible at localhost:3306 with test database mtm_wip_application_winforms_test.
- MAMP installation with PHP support (for PHP backend proxy API endpoints).
- PHP MySQL extension (mysqli or PDO) enabled in MAMP PHP configuration.
- Existing analysis CSV file (procedure-transaction-analysis.csv) generated by previous analysis scripts.
- Existing JSON file (SQLOperationsDetail JSON) with detailed procedure operation metadata.
- procedure-review-tool.html as integration host for embedding builder functionality.
- RUN-COMPLETE-ANALYSIS.ps1 PowerShell script for post-export analysis regeneration.
- Chrome browser 86+ with JavaScript enabled, localStorage permissions, and File System Access API support.
- Network access to localhost for database connections and PHP API calls (not blocked by firewall).
- Write permissions to StoredProcedureValidation/sp-builder/templates/ folder for custom template storage.

## Non-Goals (Out of Scope)

- Server-side deployment functionality (no DEPLOY button to execute generated SQL directly against database).
- Multi-user collaboration features (no real-time co-editing, commenting, or version control beyond local history).
- Automatic execution of RUN-COMPLETE-ANALYSIS.ps1 (user must manually run the script).
- SQL code formatting/beautification (user can use external tools like VS Code formatters).
- Support for database systems other than MySQL 5.7 (no PostgreSQL, SQL Server, Oracle compatibility).
- Mobile or tablet interface (desktop Chrome only).
- Offline mode (database connection required for metadata and validation).
- Integration with external version control systems (Git, SVN) beyond file export.
- Automated testing framework for generated procedures (dry-run execution with mock data only).
- Performance optimization analysis or query execution plan visualization.
- Database migration or schema change management.
- User authentication or access control (single-user desktop tool).

## Clarifications

### Session 2025-10-17

- Q: How should the builder handle errors when users make mistakes during the wizard process? → A: Inline validation with warnings - Show validation errors inline with red highlights and warning icons, but allow users to proceed to other steps. Only block Export button if critical errors exist.

- Q: What should happen when users try to access the builder from unsupported browsers? → A: Browser detection with graceful degradation - Allow access from modern Firefox/Edge/Safari but disable advanced features (File System Access API, certain drag-drop behaviors). Show banner: "Some features limited - use Chrome 86+ for full experience."

- Q: When a user's wizard session is auto-saved to localStorage, what information should be included in the recovery prompt? → A: Summary with last modified timestamp - Show procedure name (if entered), step last completed, and timestamp. Example: "Resume 'inv_inventory_Add_Item'? Last edited: 2 hours ago (Step 4: DML Operations)"

- Q: How quickly should the live SQL preview update as users make changes in the wizard? → A: 300ms debounce with loading indicator - Wait 300 milliseconds after user stops typing/clicking before updating preview. Show subtle loading spinner during generation.

- Q: Should database metadata refresh happen automatically or only when user explicitly requests it? → A: Manual refresh with staleness detection - Provide "Refresh Metadata" button. Track metadata age. Show warning banner if metadata is >10 minutes old: "Schema data may be outdated. Refresh?" Include auto-refresh option in settings for power users.

- Q: Should the drag-and-drop interfaces have keyboard-accessible alternatives for accessibility compliance? → A: Dual interface (drag-drop + keyboard) - Provide keyboard shortcuts for reordering (Ctrl+Up/Down arrows), context menus for actions, and "Move Up/Down" buttons next to each item. Screen reader announces changes.

- Q: How should the JavaScript front-end handle PHP backend API errors? → A: Structured error responses with retry - PHP returns JSON with error_type, user_message, and technical_detail fields. UI shows user-friendly message in dialog with "Retry" and "Details" buttons. Details expand to show technical info for troubleshooting.

- Q: When tracking exported procedure versions for side-by-side comparison, how many previous versions should be retained? → A: Last 5 versions per procedure - Keep 5 most recent exports per procedure name. Show dropdown in comparison view to select which versions to compare.

- Q: When a user selects a template, should the builder validate that required database tables/columns exist before applying the template? → A: Validate with substitution suggestions - Check if template's referenced tables exist. If missing, show dialog with suggested alternatives based on fuzzy matching. User can accept substitution, select different table, or cancel.

- Q: When user enters a procedure name, should the builder check if a procedure with that name already exists in the database? → A: Check with override option - Query database for existing procedure when name is entered. If exists, show warning icon with tooltip: "Procedure 'inv_inventory_Add_Item' already exists. Exporting will replace it." Allow user to proceed if intentional.

## Architecture Decisions

### Database Connection Method

**Decision**: Use PHP backend proxy (leverage existing MAMP)

**Rationale**: MAMP includes PHP, so no additional installation needed. PHP scripts can connect to MySQL and expose JSON endpoints. This provides a middle ground between static JSON files and requiring Node.js installation - it leverages the existing MAMP infrastructure that developers already have running for MySQL 5.7.

**Implementation Notes**: Create PHP API endpoints in StoredProcedureValidation/sp-builder/api/ folder that:
- Fetch table/column metadata from information_schema
- Validate SQL syntax by attempting to prepare statements
- Return JSON responses for JavaScript consumption
- Handle CORS headers for local browser access

---

### Flow Diagram Complexity Limit

**Decision**: Optimize for 25 operations (complex procedures)

**Rationale**: Requires zoom/pan controls and minimap. Auto-layout takes 1-2 seconds. Handles most MTM procedures based on analysis (the 75 analyzed procedures). Good balance between simplicity and power - supports complex real-world procedures while maintaining usable performance.

**Implementation Notes**: 
- Implement zoom/pan controls with mouse wheel and drag support
- Add minimap overview in corner for navigation
- Auto-layout algorithm should complete within 2 seconds for 25 operations
- Show operation count indicator and performance hint when approaching limit

---

### Custom Template Storage Location

**Decision**: JSON file in project directory (shareable)

**Rationale**: Templates stored in StoredProcedureValidation/sp-builder/templates/custom-templates.json. Shareable via Git so team members can contribute templates. Requires file system API or manual file upload/download for browser-based tool. Balances persistence with team collaboration.

**Implementation Notes**:
- Use File System Access API where available (Chrome 86+)
- Provide export/import buttons for manual file download/upload as fallback
- Store built-in templates in separate files (crud-templates.json, batch-templates.json, etc.)
- Custom templates append to custom-templates.json with versioning metadata
