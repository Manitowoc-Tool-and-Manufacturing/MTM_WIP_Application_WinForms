# Stored Procedure Builder - Requirements Questionnaire

**Project**: Interactive MySQL Stored Procedure Generator
**Target**: Replace Notes/Developer Correction section in procedure-review-tool.html
**Date**: 2025-10-17

---

## 🎯 CORE CONCEPT QUESTIONS

### 1. **Builder Purpose & Scope**

- **Q1.1**: Should the builder be able to CREATE new procedures from scratch, or only MODIFY existing ones based on the current procedure being reviewed? Both, but it will RECREATE existing ones
- **Q1.2**: Should users be able to save/export generated procedures as .sql files directly from the browser? Yes
- **Q1.3**: Should the builder validate generated SQL syntax before allowing save/export? Yes, use the current MAMP mysql 5.7 mtm_wip_application_winforms_test database to do this
- **Q1.4**: Do you want a "test mode" where users can preview the generated SQL without committing changes? Yes

### 2. **Integration with Current Analysis**

- **Q2.1**: Should the builder pre-populate fields based on the currently loaded procedure (parameters, table names, DML operations)? Yes - keep the naming logic / tooltips simple for this section for a user who is not familir with sql
- **Q2.2**: Should it use the `SQLOperationsDetail` JSON data to auto-populate INSERT/UPDATE/DELETE sections? Yes
- **Q2.3**: Should it display a side-by-side comparison of "Current SQL" vs "Generated SQL"? Yes
- **Q2.4**: Should changes made in the builder update the procedure analysis in real-time (e.g., update InsertCount, UpdateCount)? Yes

---

## 🏗️ PROCEDURE STRUCTURE QUESTIONS

### 3. **Procedure Naming & Header**

- **Q3.1**: Should the procedure name follow the existing pattern (`domain_table_Action` like `inv_inventory_Add_Item`)? yes
- **Q3.2**: Should users select from predefined domains (inventory, users, logging, master-data, etc.) via dropdown? yes + the ability to create new domains
- **Q3.3**: Should the builder auto-suggest a procedure name based on domain + table + action selections? yes
- **Q3.4**: Should users be able to set DEFINER (currently `root@localhost` in all procedures)? no just use root@localhost

### 4. **Parameters (IN/OUT)**

- **Q4.1**: How should users add parameters - one-by-one form, bulk text input, or drag-and-drop builder? make the sql builder portion a wizard styled format
- **Q4.2**: Should parameter names be validated (e.g., enforce `p_` prefix for consistency)? always use preset prefixes, users dont set these
- **Q4.3**: Should there be predefined parameter types (VARCHAR, INT, DATETIME, etc.) in a dropdown? yes, when the parameter type changes the tooltip changes to explain what the parameter type is normally used for in plain english
- **Q4.4**: Should OUT parameters `p_Status INT` and `p_ErrorMsg VARCHAR(500)` be auto-included (they're in every procedure)? yes
- **Q4.5**: Should users set default values for IN parameters? yes, make sure to tooltip this as well with simple plain english descriptions as in Q4.3
- **Q4.6**: Should the builder show which parameters are used in which SQL operations (visual connection)? yes

### 5. **Variable Declarations**

- **Q5.1**: Should DECLARE statements be auto-generated based on usage, or manually added by users? yes - auto-generated following predefined template using existing sql files as a template
- **Q5.2**: Should there be smart suggestions (e.g., "You're using ROW_COUNT(), declare v_RowsAffected INT")? yes
- **Q5.3**: Should variables follow a naming convention (e.g., `v_` prefix for local variables)? if the current stored precdures do then yes otherwise do the same naming convention as currently set by the codebase

---

## 🛡️ ERROR HANDLING & VALIDATION QUESTIONS

### 6. **Error Handler**

- **Q6.1**: Should the EXIT HANDLER FOR SQLEXCEPTION block be auto-included (it's in all procedures)? yes
- **Q6.2**: Should users be able to customize error messages, or use a standard template? default to steandard template - user can edit
- **Q6.3**: Should the builder support multiple error handlers (e.g., specific conditions like NOT FOUND)? yes

### 7. **Validation Logic**

- **Q7.1**: Should the builder provide a drag-and-drop interface for adding validation checks (e.g., "IF param IS NULL")? yes
- **Q7.2**: Should there be pre-built validation templates: yes
  - **Required field check** (param IS NULL OR param = '')
  - **Positive number check** (param > 0)
  - **Date range check** (startDate < endDate)
  - **String length check** (LENGTH(param) <= maxLen)
- **Q7.3**: Should each validation display a custom error message field? yes
- **Q7.4**: Should validation order be adjustable (drag to reorder)? yes

---

## 💾 DML OPERATIONS QUESTIONS

### 8. **INSERT Statements**

- **Q8.1**: Should users select target table from a dropdown (populated from database schema or existing procedures)? yes
- **Q8.2**: Should column selection be checkboxes (from table schema) or manual text input?  yes
- **Q8.3**: Should the builder show a column-to-value mapping interface (Column ← Value/Parameter)? yes
- **Q8.4**: Should there be smart defaults (e.g., auto-add `User = p_User`, `CreatedDate = NOW()`)? yes
- **Q8.5**: Should users be able to add multiple INSERT statements (like `inv_inventory` + `inv_transaction`)? yes
- **Q8.6**: Should INSERT statements support ON DUPLICATE KEY UPDATE clauses? yes

### 9. **UPDATE Statements**

- **Q9.1**: Should UPDATE statements have a visual SET clause builder (Add/Remove column-value pairs)? yes
- **Q9.2**: Should WHERE conditions use a query builder interface (Column, Operator, Value)? yes
- **Q9.3**: Should there be templates for common UPDATE patterns: yes
  - **Decrement quantity**: `Quantity = Quantity - p_Amount`
  - **Set timestamp**: `LastUpdated = NOW()`
  - **Set user**: `User = p_User`
- **Q9.4**: Should UPDATE ROW_COUNT() be automatically tracked (set v_RowsAffected)? yes

### 10. **DELETE Statements**

- **Q10.1**: Should DELETE operations have safety warnings (e.g., "Are you sure? No WHERE clause")? yes
- **Q10.2**: Should DELETE statements use the same WHERE builder as UPDATE? yes
- **Q10.3**: Should there be soft-delete template option (UPDATE table SET IsDeleted = 1)? yes

### 11. **SELECT Statements**

- **Q11.1**: Should SELECT queries have a column picker interface? yes
- **Q11.2**: Should users add ORDER BY, GROUP BY, LIMIT clauses via dropdowns/forms? yes
- **Q11.3**: Should the builder support JOIN operations (select related tables)? yes - explain this in plain english in the html file
- **Q11.4**: Should there be a checkbox for "SELECT FOUND_ROWS() INTO v_Count" pattern (used in Get_All procedures)? yes

---

## 🔄 TRANSACTION CONTROL QUESTIONS

### 12. **Transaction Management**

- **Q12.1**: Should START TRANSACTION/COMMIT/ROLLBACK be auto-included for procedures with DML operations? yes
- **Q12.2**: Should users be able to toggle transaction handling on/off (some read-only procedures don't need it)? yes
- **Q12.3**: Should ROLLBACK be auto-added to validation failure branches? yes
- **Q12.4**: Should there be explicit savepoint support for complex multi-step procedures? yes

### 13. **Success/Failure Handling**

- **Q13.1**: Should the builder auto-generate success/failure branches based on DML operations? yes
- **Q13.2**: Should status codes follow the existing pattern: yes on all
  - **1** = Success with data
  - **0** = Success but no rows affected
  - **-1** = Database error
  - **-2** = Validation error
  - **-3** = Business logic error
  - **-4** = Not found
  - **-5** = Conflict/duplicate
- **Q13.3**: Should success messages be customizable per operation? yes
- **Q13.4**: Should there be a checkbox for "Return affected row count in message"? yes

---

## 🎨 UI/UX DESIGN QUESTIONS

### 14. **Layout & Visual Design**

- **Q14.1**: Should the builder be a **tabbed interface** (Parameters | Validation | Operations | Advanced)? no
- **Q14.2**: Or a **vertical scrolling form** (all sections stacked)? no
- **Q14.3**: Or a **wizard/stepper** (Step 1: Name, Step 2: Params, Step 3: Logic, etc.)? yes
- **Q14.4**: Should there be a live SQL preview panel that updates as users make changes? yes
- **Q14.5**: Should the preview panel have syntax highlighting? yes
- **Q14.6**: Should there be a collapsible "Advanced Options" section for edge cases? yes

### 15. **Drag-and-Drop Features**

- **Q15.1**: Should DML operations be draggable cards that can be reordered? yes
- **Q15.2**: Should there be a "library" of common code snippets users can drag in (e.g., batch number generation, timestamp columns)? yes
- **Q15.3**: Should parameters be draggable into SQL fields (visual linking)? yes

### 16. **Templates & Presets**

- **Q16.1**: Should there be procedure templates for common patterns: yes to all
  - **CRUD operations** (Create, Read, Update, Delete)
  - **Batch operations** (Process multiple rows)
  - **Transfer operations** (Move inventory between locations)
  - **Audit logging** (Write to transaction history)
- **Q16.2**: Should users be able to save custom templates? yes
- **Q16.3**: Should templates be categorized by domain (Inventory, Users, Logging)? yes

### 17. **Auto-Complete & Suggestions**

- **Q17.1**: Should table names auto-complete from database schema? yes
- **Q17.2**: Should column names auto-complete based on selected table? yes
- **Q17.3**: Should parameter names appear in auto-complete when typing SQL? yes
- **Q17.4**: Should there be inline hints (e.g., "💡 Consider adding User and LastUpdated columns")? yes

---

## 🔧 ADVANCED FEATURES QUESTIONS

### 18. **Multi-Table Operations**

- **Q18.1**: Should the builder support procedures that touch multiple tables (like `inv_inventory` + `inv_transaction`)? yes
- **Q18.2**: Should there be a visual "flow diagram" showing operation sequence? yes - IMPORTANT
- **Q18.3**: Should users be able to add conditional branching (IF/ELSEIF/ELSE blocks)? YES - If compatible with mysql 5.7

### 19. **Loops & Cursors**

- **Q19.1**: Should the builder support WHILE loops? YES - If compatible with mysql 5.7
- **Q19.2**: Should there be cursor support (DECLARE CURSOR, FETCH, CLOSE)? YES - If compatible with mysql 5.7
- **Q19.3**: Or should complex logic require manual SQL editing? make it an option to swith to manual editing at any time

### 20. **Sequence/Auto-Increment Handling**

- **Q20.1**: Should there be a dedicated UI for batch number generation (like `inv_inventory_batch_seq` pattern)? yes
- **Q20.2**: Should auto-increment fields be detected and handled automatically? let the server deal with AI fields
- **Q20.3**: Should the builder support FOR UPDATE locking for sequence tables? let the server deal with AI fields

### 21. **Stored Procedure Calls**

- **Q21.1**: Should users be able to call other stored procedures from within (CALL statement)? yes
- **Q21.2**: Should there be parameter mapping for nested calls? yes

---

## 💾 SAVE/EXPORT QUESTIONS

### 22. **File Generation**

- **Q22.1**: Should the export include DELIMITER statements and DROP PROCEDURE IF EXISTS? yes
- **Q22.2**: Should files be named automatically (`domain_table_action.sql`)? yes
- **Q22.3**: Should users select the target folder structure (which subfolder: inventory, users, etc.)? set automaticly based on current structure
- **Q22.4**: Should the builder add a header comment with metadata (author, date, description)? yes

### 23. **Version Control**

- **Q23.1**: Should the builder track versions of generated procedures? yes
- **Q23.2**: Should there be a "Compare with original" diff view? yes
- **Q23.3**: Should changes be logged in the analysis CSV (DeveloperCorrection field)? yes

### 24. **Integration with Analysis Pipeline**

- **Q24.1**: Should saved procedures automatically re-run the analysis scripts (RUN-COMPLETE-ANALYSIS.ps1)? no but prompt user to run it 
- **Q24.2**: Should the builder update the procedure-transaction-analysis.csv with new metadata? no as Q24.1 will cover that
- **Q24.3**: Should there be a "Deploy to Database" button (if server connection is available)? no

---

## 🧪 VALIDATION & TESTING QUESTIONS

### 25. **SQL Syntax Validation**

- **Q25.1**: Should the builder validate SQL syntax client-side (JavaScript parser)? yes
- **Q25.2**: Should it check for common errors (missing semicolons, unmatched quotes, undefined parameters)? yes
- **Q25.3**: Should validation errors be highlighted inline (red underline)? yes

### 26. **Preview & Testing**

- **Q26.1**: Should there be a "dry run" mode that shows what the procedure would do without executing? yes
- **Q26.2**: Should the preview show sample execution with mock parameters? yes
- **Q26.3**: Should there be SQL formatting/beautification (auto-indent, keyword capitalization)? no, this can be done in vs code

---

## 📚 DOCUMENTATION QUESTIONS

### 27. **Inline Help**

- **Q27.1**: Should each form field have tooltip help (hover for explanation)? yes
- **Q27.2**: Should there be a help sidebar with examples? yes
- **Q27.3**: Should the builder include a tutorial/walkthrough for first-time users? yes

### 28. **Generated Documentation**

- **Q28.1**: Should the builder auto-generate procedure documentation (parameters, purpose, tables affected)? yes
- **Q28.2**: Should documentation be exportable as markdown or HTML? yes, user decides what one

---

## 🚀 PERFORMANCE & TECHNICAL QUESTIONS

### 29. **Browser Compatibility**

- **Q29.1**: Should the builder work offline (no server required)? no
- **Q29.2**: What browsers must be supported (Chrome, Edge, Firefox)? Chrome
- **Q29.3**: Should state be saved in localStorage (resume work after page reload)? yes

### 30. **Data Sources**

- **Q30.1**: Should table/column metadata come from:
  - **Static JSON** (extracted once from database) no
  - **Dynamic fetch** (real-time database queries) this
  - **CSV analysis** (inferred from existing procedures) no
- **Q30.2**: Should the builder parse existing .sql files to learn table structures? yes

---

## 🎯 PRIORITIZATION QUESTIONS

### 31. **MVP (Minimum Viable Product)**

Which features are MUST-HAVE for version 1.0?

Whatever is nessicary for the above answers to be implemented, nothing less nothing more.

- [ ] Procedure name builder
- [ ] Parameter management (IN/OUT)
- [ ] Basic INSERT/UPDATE/DELETE forms
- [ ] Validation logic builder
- [ ] Transaction handling toggle
- [ ] SQL preview panel
- [ ] Export to .sql file

Which features are NICE-TO-HAVE for future versions?

All

- [ ] Drag-and-drop operations
- [ ] Template library
- [ ] Multi-table flow diagrams
- [ ] Cursor/loop support
- [ ] Live syntax validation
- [ ] Database deployment integration
- [ ] Version control/history

---

## 📝 FINAL QUESTIONS

### 32. **User Workflow**

Describe the ideal workflow step-by-step:

1. User opens procedure-review-tool.html
2. Loads all required database data directly from database
   1. server = localhost
   2. database = mtm_wip_application_winforms_test
   3. user = root
   4. password = root
3. Navigates to a procedure (e.g., `inv_inventory_Add_Item`)
4. Clicks "Builder" tab/section
5. **Then what?** (Please describe the next 5-10 steps)
   1. Begins Build Wizard process, create these steps by compiling the above answers

### 33. **Existing vs New Procedures**

- **Q33.1**: Will this builder be used MORE for creating NEW procedures, or MODIFYING existing ones? Modifying
- **Q33.2**: Should there be two modes: "Create from Scratch" vs "Edit Current Procedure"? Yes

### 34. **Collaboration Features**

- **Q34.1**: Will multiple developers use this tool simultaneously? no
- **Q34.2**: Should there be export/import of builder state (share configurations)? no

---

## 📊 SUMMARY

**Total Questions**: 80+

Please answer as many as possible to guide the design. Even partial answers help!

**Priority Levels**:

- 🔴 **Critical**: Must answer before starting
- 🟡 **Important**: Should answer for good UX
- 🟢 **Optional**: Can decide during implementation

Mark your priorities, and I'll create the builder accordingly! 🚀
