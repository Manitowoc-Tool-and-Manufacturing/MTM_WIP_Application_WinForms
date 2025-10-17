# Stored Procedure Builder - Specification Prompt

## Feature Description

Create an interactive MySQL 5.7 Stored Procedure Builder as a multi-file HTML/CSS/JavaScript application that integrates with the existing MTM WIP Application analysis tools. The builder will be organized into modular HTML files by functional category/task, allowing developers to visually design, validate, and export stored procedures through a wizard-style interface with live database connectivity.

## Core Requirements from SP-BUILDER-REQUIREMENTS.md

### Architecture & File Structure

The application must be split into multiple HTML files organized by task/category:

1. **sp-builder-main.html** - Main entry point with navigation and shared components
2. **sp-builder-wizard.html** - Wizard stepper interface for procedure creation workflow
3. **sp-builder-parameters.html** - Parameter management (IN/OUT) interface
4. **sp-builder-validation.html** - Validation logic builder with drag-drop
5. **sp-builder-dml-operations.html** - INSERT/UPDATE/DELETE/SELECT builders
6. **sp-builder-flow-diagram.html** - Visual operation sequence flow diagram (IMPORTANT)
7. **sp-builder-preview.html** - Live SQL preview with syntax highlighting
8. **sp-builder-templates.html** - Template library (CRUD, Batch, Transfer, Audit)
9. **sp-builder-advanced.html** - Loops, cursors, conditional branching, stored procedure calls
10. **sp-builder-export.html** - File generation, version control, and comparison
11. **sp-builder-help.html** - Tutorial, walkthrough, and inline help system

Each file should be self-contained but share common CSS/JavaScript libraries.

### Database Connectivity (Critical)

- Connect directly to MySQL 5.7 database: `localhost:3306/mtm_wip_application_winforms_test`
- Credentials: root/root
- Fetch real-time table/column metadata from database schema
- Validate generated SQL syntax against actual MySQL 5.7 server
- Support dry-run execution with mock parameters
- No server-side deployment required (client-side only)

### Integration Points

- Load existing procedure analysis from `procedure-transaction-analysis.csv`
- Pre-populate builder from `SQLOperationsDetail` JSON data
- Replace Notes/Developer Correction section in `procedure-review-tool.html`
- Update analysis metadata after procedure generation
- Parse existing .sql files to learn patterns and table structures

### Wizard Workflow (User Journey)

1. User opens procedure-review-tool.html
2. Database metadata loads automatically from localhost
3. User selects a procedure (e.g., `inv_inventory_Add_Item`)
4. Clicks "Builder" tab/section
5. **Mode Selection**: Choose "Edit Current Procedure" or "Create from Scratch"
6. **Wizard Steps**:
   - Step 1: Procedure Name (domain_table_Action pattern)
   - Step 2: Parameters (IN/OUT with tooltips explaining types in plain English)
   - Step 3: Validation Logic (drag-drop validation checks with custom messages)
   - Step 4: DML Operations (INSERT/UPDATE/DELETE/SELECT builders)
   - Step 5: Flow Diagram (visual operation sequence - draggable cards)
   - Step 6: Advanced Options (loops, cursors, branching, nested calls)
   - Step 7: Preview & Export (syntax-highlighted SQL with comparison)
7. Export generates .sql file with proper folder structure
8. Prompt to run RUN-COMPLETE-ANALYSIS.ps1

### Key Features by Category

#### Parameters & Variables
- Auto-include `p_Status INT` and `p_ErrorMsg VARCHAR(500)` OUT parameters
- Enforce `p_` prefix for parameters (auto-applied)
- Use `v_` prefix for local variables (auto-applied)
- Dropdown for parameter types (VARCHAR, INT, DATETIME, etc.) with plain-English tooltips
- Show visual connections between parameters and SQL operations
- Auto-generate DECLARE statements based on usage
- Smart suggestions (e.g., "Using ROW_COUNT()? Declare v_RowsAffected INT")

#### Validation Logic
- Drag-and-drop validation check builder
- Pre-built templates:
  - Required field check (param IS NULL OR param = '')
  - Positive number check (param > 0)
  - Date range check (startDate < endDate)
  - String length check (LENGTH(param) <= maxLen)
- Custom error messages per validation
- Reorderable validation sequence (drag to reorder)
- Auto-add ROLLBACK to validation failure branches

#### DML Operations
- **INSERT**: Table dropdown, column checkboxes, column-to-value mapping, smart defaults (User=p_User, CreatedDate=NOW()), ON DUPLICATE KEY UPDATE support
- **UPDATE**: Visual SET clause builder, query builder for WHERE conditions, templates (decrement quantity, set timestamp, set user), auto-track ROW_COUNT()
- **DELETE**: Safety warnings (no WHERE clause), same WHERE builder as UPDATE, soft-delete template option
- **SELECT**: Column picker, ORDER BY/GROUP BY/LIMIT dropdowns, JOIN support (with plain-English explanations), FOUND_ROWS() checkbox

#### Transaction Control
- Auto-include START TRANSACTION/COMMIT/ROLLBACK for DML operations
- Toggle transaction handling on/off
- Auto-add ROLLBACK to validation failures
- Savepoint support for multi-step procedures
- Standard status codes (-5 to 1) with customizable messages

#### UI/UX Requirements
- Wizard/stepper interface (not tabs or vertical form)
- Live SQL preview panel with syntax highlighting
- Collapsible Advanced Options section
- Drag-and-drop for DML operation reordering
- Snippet library (batch number generation, timestamps)
- Draggable parameters into SQL fields (visual linking)
- Auto-complete for table names, column names, parameters
- Inline hints ("💡 Consider adding User and LastUpdated columns")

#### Templates & Presets
- Pre-built templates: CRUD, Batch operations, Transfer operations, Audit logging
- Save custom templates
- Templates categorized by domain (Inventory, Users, Logging, etc.)
- Load template based on procedure pattern detection

#### Advanced Features (MySQL 5.7 Compatible Only)
- Conditional branching (IF/ELSEIF/ELSE blocks)
- WHILE loops
- Cursor support (DECLARE CURSOR, FETCH, CLOSE)
- Nested stored procedure calls (CALL statement with parameter mapping)
- Switch to manual SQL editing at any time
- Batch number generation UI (inv_inventory_batch_seq pattern)

#### Visual Flow Diagram (CRITICAL)
- Interactive flow diagram showing operation sequence
- Draggable cards for each operation
- Visual connections between operations
- Conditional branching visualization
- Auto-layout with manual adjustment
- Export diagram as image/PDF

#### Export & Version Control
- Export includes DELIMITER statements and DROP PROCEDURE IF EXISTS
- Auto-naming: `domain_table_action.sql`
- Auto-folder selection based on domain
- Header comment with metadata (author, date, description)
- Track procedure versions
- Side-by-side diff view (Current SQL vs Generated SQL)
- Log changes in DeveloperCorrection CSV field
- Option to export documentation as Markdown or HTML

#### Validation & Testing
- Client-side JavaScript SQL parser for syntax validation
- Check for common errors (missing semicolons, unmatched quotes, undefined parameters)
- Inline error highlighting (red underline)
- Dry-run mode with mock parameter execution preview
- Real-time updates to procedure analysis (InsertCount, UpdateCount, etc.)

#### Help System
- Tooltip help on every form field (hover for explanation)
- Help sidebar with examples
- First-time user tutorial/walkthrough
- Auto-generated procedure documentation (parameters, purpose, tables)

#### Technical Constraints
- Chrome browser only
- Offline NOT supported (requires database connection)
- Save state in localStorage (resume after reload)
- Dynamic fetch of table/column metadata (real-time)
- Parse existing .sql files for pattern learning

### Success Criteria

1. User can create a complete stored procedure in under 10 minutes using the wizard
2. Generated SQL passes MySQL 5.7 syntax validation 100% of the time
3. Pre-population from existing procedures works for all 75 analyzed procedures
4. Visual flow diagram accurately represents operation sequence for multi-step procedures
5. Drag-drop interface allows reordering of operations without manual SQL editing
6. All form fields have helpful tooltips explaining concepts in plain English for SQL beginners
7. Export generates properly formatted .sql files with correct folder structure
8. Side-by-side comparison shows differences between original and modified procedures
9. Template library covers 80% of common procedure patterns (CRUD, batch, transfer, audit)
10. Auto-complete reduces typing by 50% when building SQL operations

### Non-Goals (Out of Scope)

- Server-side deployment to database (export only, no DEPLOY button)
- Multi-user collaboration features
- Automatic execution of RUN-COMPLETE-ANALYSIS.ps1 (prompt only)
- SQL formatting/beautification (user can do in VS Code)
- Support for databases other than MySQL 5.7
- Mobile/tablet interface (desktop Chrome only)

### Assumptions

- User has MySQL 5.7 running locally via MAMP
- User has existing procedure-transaction-analysis.csv loaded
- User understands basic database concepts (tables, columns, rows)
- Chrome browser supports modern JavaScript features (ES6+, localStorage, Fetch API)
- Database connection is stable during builder session

### Dependencies

- MySQL 5.7 database server running on localhost:3306
- Existing analysis CSV and JSON files
- procedure-review-tool.html as integration host
- RUN-COMPLETE-ANALYSIS.ps1 script for post-export analysis

### Risk Factors

- Browser-based MySQL connection may require CORS configuration or proxy
- Complex procedures with 10+ operations may strain visual flow diagram rendering
- Large database schemas (100+ tables) may slow down auto-complete
- LocalStorage size limits may restrict stored template library

### Open Questions Requiring Clarification

1. **Database Connection Method**: How should the browser connect to MySQL 5.7?
   - Option A: Use Node.js backend proxy (requires additional setup)
   - Option B: Use MySQL WebSocket API (if available)
   - Option C: Load schema statically from exported JSON (offline mode)

2. **Flow Diagram Complexity Limit**: What is the maximum number of operations to visualize?
   - Option A: 10 operations (simple procedures)
   - Option B: 25 operations (complex procedures)
   - Option C: Unlimited (with zoom/pan controls)

3. **Template Storage**: Where should custom templates be saved?
   - Option A: Browser localStorage (persists per browser)
   - Option B: JSON file in project directory (shareable)
   - Option C: Database table (requires schema changes)

## Deliverables

1. 11 modular HTML files as specified above
2. Shared CSS file (sp-builder-styles.css) with wizard, drag-drop, and flow diagram styles
3. Shared JavaScript libraries:
   - sp-builder-core.js (common utilities, database connection)
   - sp-builder-wizard.js (stepper navigation)
   - sp-builder-sql-generator.js (SQL code generation)
   - sp-builder-validator.js (syntax validation)
   - sp-builder-flow-diagram.js (visual flow rendering)
4. Integration guide for embedding in procedure-review-tool.html
5. User manual/tutorial (embedded in sp-builder-help.html)

## File Organization

```
StoredProcedureValidation/
├── sp-builder/
│   ├── index.html (redirects to sp-builder-main.html)
│   ├── sp-builder-main.html
│   ├── sp-builder-wizard.html
│   ├── sp-builder-parameters.html
│   ├── sp-builder-validation.html
│   ├── sp-builder-dml-operations.html
│   ├── sp-builder-flow-diagram.html
│   ├── sp-builder-preview.html
│   ├── sp-builder-templates.html
│   ├── sp-builder-advanced.html
│   ├── sp-builder-export.html
│   ├── sp-builder-help.html
│   ├── css/
│   │   └── sp-builder-styles.css
│   ├── js/
│   │   ├── sp-builder-core.js
│   │   ├── sp-builder-wizard.js
│   │   ├── sp-builder-sql-generator.js
│   │   ├── sp-builder-validator.js
│   │   └── sp-builder-flow-diagram.js
│   └── templates/
│       ├── crud-templates.json
│       ├── batch-templates.json
│       ├── transfer-templates.json
│       └── audit-templates.json
└── procedure-review-tool.html (updated to embed builder)
```

## Next Steps

After specification approval:
1. Run `/speckit.clarify` to resolve open questions
2. Run `/speckit.plan` to create implementation plan for each HTML file
3. Implement files in priority order: main → wizard → parameters → DML → flow diagram → preview → export → templates → advanced → help
4. Integrate with procedure-review-tool.html
5. Test with all 75 existing stored procedures
