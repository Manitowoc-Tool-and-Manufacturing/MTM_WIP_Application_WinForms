# Research Phase: Interactive MySQL 5.7 Stored Procedure Builder

**Feature Branch**: `004-interactive-mysql-5`  
**Date**: 2025-10-17  
**Status**: Research Complete

---

## Overview

This document consolidates research findings for building a client-side web application that generates MySQL 5.7 stored procedures through a visual wizard interface. Research covers technology choices, architectural patterns, MySQL 5.7 syntax constraints, drag-drop accessibility, and integration with existing MTM infrastructure.

---

## 1. MySQL 5.7 Syntax Constraints and Code Generation

### Decision: Target MySQL 5.7 with explicit syntax restrictions

**Rationale**: MTM application uses MySQL 5.7 via MAMP. Generated procedures must be compatible with this version, which lacks modern features added in MySQL 8.0.

**Key Restrictions**:
- **No Common Table Expressions (CTEs)**: WITH clause unavailable. Use subqueries or temporary tables instead.
- **No Window Functions**: ROW_NUMBER(), RANK(), LEAD(), LAG() unavailable. Use variables and subqueries for ranking/aggregation.
- **Limited JSON Support**: JSON functions exist but are limited compared to MySQL 8.0.
- **DELIMITER Statements Required**: Stored procedures need DELIMITER changes for batch execution.

**Code Generation Strategy**:
```sql
-- Template structure for all generated procedures
DELIMITER $$

DROP PROCEDURE IF EXISTS {procedure_name}$$

CREATE PROCEDURE {procedure_name}(
    -- IN parameters
    IN p_ParameterName DataType,
    -- OUT parameters (mandatory)
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    -- Local variable declarations
    DECLARE v_VariableName DataType;
    
    -- Error handler
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        SET p_Status = -99;
        SET p_ErrorMsg = 'Database error occurred';
    END;
    
    -- Transaction start
    START TRANSACTION;
    
    -- Validation rules (before DML)
    IF {validation_condition} THEN
        ROLLBACK;
        SET p_Status = {error_code};
        SET p_ErrorMsg = '{error_message}';
    ELSE
        -- DML operations
        {operations}
        
        -- Success
        SET p_Status = 0;
        SET p_ErrorMsg = NULL;
        COMMIT;
    END IF;
END$$

DELIMITER ;
```

**Alternatives Considered**:
- Target MySQL 8.0+ with feature detection: Rejected - MTM uses 5.7 in production
- Generate MySQL 5.7 with upgrade hints: Too complex, no clear migration path
- Use lowest common denominator MySQL 5.5: Unnecessary, 5.7 is baseline

---

## 2. Client-Side Architecture: Vanilla JavaScript vs Frameworks

### Decision: Vanilla JavaScript with ES6 modules

**Rationale**: No build step required, runs directly from file:// or MAMP, zero npm dependencies, simpler deployment for desktop-focused tool. Complexity manageable for 11-page wizard application.

**Implementation Pattern**:
```javascript
// ES6 modules with explicit exports/imports
// procedure-model.js
export class ProcedureDefinition {
    constructor() {
        this.name = '';
        this.parameters = [];
        this.validations = [];
        this.operations = [];
        this.flowDiagram = { nodes: [], connections: [] };
    }
    
    toJSON() { /* serialization for localStorage */ }
    static fromJSON(data) { /* deserialization */ }
}

// wizard-controller.js
import { ProcedureDefinition } from './procedure-model.js';
import { SQLGenerator } from './sql-generator.js';

class WizardController {
    constructor() {
        this.procedure = new ProcedureDefinition();
        this.currentStep = 1;
    }
    
    async saveState() {
        localStorage.setItem('sp_builder_state', JSON.stringify(this.procedure));
    }
}
```

**Module Organization**:
- **procedure-model.js**: Core data structures (ProcedureDefinition, Parameter, Validation, DMLOperation)
- **sql-generator.js**: Template-based SQL generation from model
- **wizard-controller.js**: Navigation, validation, state persistence
- **database-metadata.js**: PHP API calls for table/column metadata
- **storage-manager.js**: localStorage operations, version history, auto-save

**Alternatives Considered**:
- React/Vue: Rejected - requires build step, npm, bundling complexity
- jQuery: Rejected - unnecessary for modern browser targets (Chrome 86+)
- Web Components: Considered but vanilla JS simpler for wizard pattern

---

## 3. PHP Backend for Database Metadata

### Decision: PHP proxy endpoints leveraging existing MAMP installation

**Rationale**: MAMP includes PHP, developers already have it running. No additional server setup required (Node.js, Python). Simple mysqli connection to query information_schema.

**API Endpoint Pattern**:
```php
<?php
// api/get-columns.php
header('Content-Type: application/json');
header('Access-Control-Allow-Origin: *'); // For file:// access

require_once 'config.php'; // Database connection

$tableName = $_GET['table'] ?? '';

if (empty($tableName)) {
    echo json_encode([
        'success' => false,
        'error_type' => 'validation',
        'user_message' => 'Table name is required',
        'technical_detail' => 'Missing table parameter'
    ]);
    exit;
}

try {
    $conn = new mysqli(DB_HOST, DB_USER, DB_PASS, DB_NAME);
    
    if ($conn->connect_error) {
        throw new Exception("Connection failed: " . $conn->connect_error);
    }
    
    $stmt = $conn->prepare("
        SELECT COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH,
               NUMERIC_PRECISION, NUMERIC_SCALE, IS_NULLABLE, 
               COLUMN_DEFAULT, EXTRA
        FROM information_schema.COLUMNS
        WHERE TABLE_SCHEMA = ? AND TABLE_NAME = ?
        ORDER BY ORDINAL_POSITION
    ");
    
    $stmt->bind_param('ss', DB_NAME, $tableName);
    $stmt->execute();
    $result = $stmt->get_result();
    
    $columns = [];
    while ($row = $result->fetch_assoc()) {
        $columns[] = [
            'name' => $row['COLUMN_NAME'],
            'type' => $row['DATA_TYPE'],
            'length' => $row['CHARACTER_MAXIMUM_LENGTH'],
            'precision' => $row['NUMERIC_PRECISION'],
            'scale' => $row['NUMERIC_SCALE'],
            'nullable' => $row['IS_NULLABLE'] === 'YES',
            'default' => $row['COLUMN_DEFAULT'],
            'autoIncrement' => strpos($row['EXTRA'], 'auto_increment') !== false
        ];
    }
    
    echo json_encode([
        'success' => true,
        'data' => $columns
    ]);
    
} catch (Exception $e) {
    echo json_encode([
        'success' => false,
        'error_type' => 'database',
        'user_message' => 'Database connection failed',
        'technical_detail' => $e->getMessage()
    ]);
}
?>
```

**Required Endpoints**:
1. **get-tables.php**: Returns list of tables from information_schema.TABLES
2. **get-columns.php**: Returns column details for specified table
3. **validate-syntax.php**: Uses `PREPARE` statement to validate SQL syntax
4. **check-procedure-exists.php**: Queries information_schema.ROUTINES for existing procedure

**Error Response Format** (from clarifications):
```json
{
    "success": false,
    "error_type": "connection_failed|validation|database|syntax",
    "user_message": "Database connection lost",
    "technical_detail": "mysqli_connect failed: Access denied for user 'root'@'localhost'"
}
```

**Alternatives Considered**:
- Static JSON dumps: Rejected - data becomes stale, no dynamic schema support
- Node.js Express: Rejected - requires npm, separate server process
- Direct MySQL connection from JS: Not possible in browser environment

---

## 4. Drag-and-Drop with Keyboard Accessibility

### Decision: HTML5 Drag and Drop API with dual keyboard interface

**Rationale**: Native drag-drop for visual users, keyboard shortcuts and buttons for accessibility. Meets WCAG 2.1 Level AA guidelines.

**Implementation Pattern**:
```javascript
// drag-drop.js
export class DragDropManager {
    constructor(containerSelector, options = {}) {
        this.container = document.querySelector(containerSelector);
        this.onReorder = options.onReorder || (() => {});
        this.items = [];
        
        this.setupDragDrop();
        this.setupKeyboardControls();
    }
    
    setupDragDrop() {
        this.container.addEventListener('dragstart', (e) => {
            if (!e.target.classList.contains('draggable-item')) return;
            e.dataTransfer.effectAllowed = 'move';
            e.dataTransfer.setData('text/html', e.target.innerHTML);
            e.target.classList.add('dragging');
        });
        
        this.container.addEventListener('dragover', (e) => {
            e.preventDefault();
            const afterElement = this.getDragAfterElement(e.clientY);
            const dragging = this.container.querySelector('.dragging');
            if (afterElement) {
                this.container.insertBefore(dragging, afterElement);
            } else {
                this.container.appendChild(dragging);
            }
        });
        
        this.container.addEventListener('dragend', (e) => {
            e.target.classList.remove('dragging');
            this.updateOrder();
            this.announceToScreenReader('Item reordered');
        });
    }
    
    setupKeyboardControls() {
        this.container.addEventListener('keydown', (e) => {
            const item = e.target.closest('.draggable-item');
            if (!item) return;
            
            if (e.ctrlKey && e.key === 'ArrowUp') {
                e.preventDefault();
                this.moveUp(item);
                item.focus();
                this.announceToScreenReader('Item moved up');
            } else if (e.ctrlKey && e.key === 'ArrowDown') {
                e.preventDefault();
                this.moveDown(item);
                item.focus();
                this.announceToScreenReader('Item moved down');
            }
        });
        
        // Add visible up/down buttons for each item
        this.container.querySelectorAll('.draggable-item').forEach(item => {
            const controls = document.createElement('div');
            controls.className = 'item-controls';
            controls.innerHTML = `
                <button class="btn-move-up" aria-label="Move up">↑</button>
                <button class="btn-move-down" aria-label="Move down">↓</button>
            `;
            item.appendChild(controls);
        });
    }
    
    announceToScreenReader(message) {
        const announcer = document.getElementById('sr-announcer');
        announcer.textContent = message;
    }
}
```

**Accessibility Features**:
- ARIA live region for screen reader announcements
- Keyboard shortcuts: Ctrl+Up/Down to reorder items
- Visible up/down buttons next to each item
- Focus management (keyboard focus follows moved item)
- Context menu (right-click) for additional actions

**Alternatives Considered**:
- Drag-drop only: Fails WCAG accessibility requirements
- Keyboard only: Reduces visual appeal for mouse users
- Third-party library (Sortable.js): Adds dependency, overkill for simple reordering

---

## 5. SQL Syntax Validation (Client-Side)

### Decision: Client-side parser for instant feedback + PHP validation for accuracy

**Rationale**: Two-tier validation balances instant feedback with accurate MySQL 5.7 compatibility checking.

**Tier 1: Client-Side Parser (Instant Feedback)**
```javascript
// sql-validator.js
export class MySQLValidator {
    validate(sql) {
        const errors = [];
        
        // Check for MySQL 8.0+ features (CTEs, window functions)
        if (/\bWITH\s+\w+\s+AS\s*\(/i.test(sql)) {
            errors.push({
                line: this.getLineNumber(sql, 'WITH'),
                message: 'CTEs (WITH clause) not supported in MySQL 5.7',
                severity: 'error'
            });
        }
        
        if (/\b(ROW_NUMBER|RANK|DENSE_RANK|LEAD|LAG)\s*\(/i.test(sql)) {
            errors.push({
                line: this.getLineNumber(sql, 'ROW_NUMBER'),
                message: 'Window functions not supported in MySQL 5.7',
                severity: 'error'
            });
        }
        
        // Check for missing WHERE clause on UPDATE/DELETE
        if (/UPDATE\s+\w+\s+SET\s+.*?(?!WHERE)/i.test(sql)) {
            errors.push({
                line: this.getLineNumber(sql, 'UPDATE'),
                message: 'UPDATE without WHERE clause affects all rows',
                severity: 'warning'
            });
        }
        
        // Check parameter/variable naming conventions
        const params = sql.match(/\b([a-z_]\w*)\s*(?:INT|VARCHAR|DECIMAL)/gi) || [];
        params.forEach(param => {
            if (!/^[pv]_/.test(param)) {
                errors.push({
                    line: this.getLineNumber(sql, param),
                    message: `Parameter '${param}' should start with p_ or v_ prefix`,
                    severity: 'warning'
                });
            }
        });
        
        return errors;
    }
}
```

**Tier 2: PHP Validation (Accurate Syntax Check)**
```php
// api/validate-syntax.php
$sql = $_POST['sql'] ?? '';

try {
    $conn = new mysqli(DB_HOST, DB_USER, DB_PASS, DB_NAME);
    $stmt = $conn->prepare($sql); // MySQL validates syntax on PREPARE
    
    if ($stmt) {
        echo json_encode(['success' => true, 'message' => 'Syntax valid']);
    } else {
        echo json_encode([
            'success' => false,
            'error_type' => 'syntax',
            'user_message' => 'SQL syntax error detected',
            'technical_detail' => $conn->error
        ]);
    }
} catch (Exception $e) {
    // Return structured error
}
```

**Alternatives Considered**:
- Client-side only: Cannot guarantee MySQL 5.7 compatibility
- Server-side only: Slow feedback loop (requires round-trip for every change)
- Third-party SQL parser: Adds large dependency, may not match MySQL exactly

---

## 6. Flow Diagram Rendering and Auto-Layout

### Decision: Canvas-based rendering with Dagre.js auto-layout algorithm

**Rationale**: Canvas provides 60fps performance for complex diagrams. Dagre.js is lightweight (no dependencies), well-suited for directed acyclic graphs (DAG) representing operation sequences.

**Implementation Pattern**:
```javascript
// flow-diagram.js
import dagre from '../lib/dagre.min.js';

export class FlowDiagram {
    constructor(canvasElement, procedure) {
        this.canvas = canvasElement;
        this.ctx = this.canvas.getContext('2d');
        this.procedure = procedure;
        this.graph = new dagre.graphlib.Graph();
        this.nodes = [];
        this.edges = [];
        
        this.zoom = 1.0;
        this.panX = 0;
        this.panY = 0;
    }
    
    buildGraph() {
        this.graph.setGraph({ rankdir: 'TB', nodesep: 50, ranksep: 100 });
        this.graph.setDefaultEdgeLabel(() => ({}));
        
        // Add nodes for each operation
        this.procedure.operations.forEach((op, idx) => {
            this.graph.setNode(`op-${idx}`, {
                label: `${op.type}\n${op.targetTable}`,
                width: 150,
                height: 80,
                operation: op
            });
        });
        
        // Add edges for sequential flow
        for (let i = 0; i < this.procedure.operations.length - 1; i++) {
            this.graph.setEdge(`op-${i}`, `op-${i + 1}`);
        }
        
        // Add conditional branches
        this.procedure.operations.forEach((op, idx) => {
            if (op.conditionalBranch) {
                this.graph.setEdge(`op-${idx}`, `op-${op.conditionalBranch.targetIdx}`, {
                    label: op.conditionalBranch.condition
                });
            }
        });
        
        // Run auto-layout
        dagre.layout(this.graph);
    }
    
    render() {
        this.ctx.clearRect(0, 0, this.canvas.width, this.canvas.height);
        this.ctx.save();
        this.ctx.translate(this.panX, this.panY);
        this.ctx.scale(this.zoom, this.zoom);
        
        // Draw edges
        this.graph.edges().forEach(e => {
            const edge = this.graph.edge(e);
            this.drawEdge(edge);
        });
        
        // Draw nodes
        this.graph.nodes().forEach(nodeId => {
            const node = this.graph.node(nodeId);
            this.drawNode(node);
        });
        
        this.ctx.restore();
    }
    
    drawNode(node) {
        const { x, y, width, height, label } = node;
        
        // Draw node rectangle
        this.ctx.fillStyle = '#ffffff';
        this.ctx.strokeStyle = '#3b82f6';
        this.ctx.lineWidth = 2;
        this.ctx.fillRect(x - width/2, y - height/2, width, height);
        this.ctx.strokeRect(x - width/2, y - height/2, width, height);
        
        // Draw label
        this.ctx.fillStyle = '#1f2937';
        this.ctx.font = '14px sans-serif';
        this.ctx.textAlign = 'center';
        this.ctx.textBaseline = 'middle';
        this.ctx.fillText(label, x, y);
    }
    
    setupZoomPan() {
        this.canvas.addEventListener('wheel', (e) => {
            e.preventDefault();
            const delta = e.deltaY > 0 ? 0.9 : 1.1;
            this.zoom = Math.max(0.1, Math.min(3, this.zoom * delta));
            this.render();
        });
        
        // Pan with mouse drag
        let isPanning = false;
        let startX, startY;
        
        this.canvas.addEventListener('mousedown', (e) => {
            isPanning = true;
            startX = e.clientX - this.panX;
            startY = e.clientY - this.panY;
        });
        
        this.canvas.addEventListener('mousemove', (e) => {
            if (!isPanning) return;
            this.panX = e.clientX - startX;
            this.panY = e.clientY - startY;
            this.render();
        });
        
        this.canvas.addEventListener('mouseup', () => {
            isPanning = false;
        });
    }
}
```

**Performance Optimization**:
- Limit to 25 operations (per spec) to maintain <2s auto-layout
- Use requestAnimationFrame for smooth zoom/pan
- Minimap overview for navigation in large diagrams
- Lazy render (only redraw on state change, not every frame)

**Alternatives Considered**:
- SVG-based (D3.js): Slower for >20 nodes, DOM manipulation overhead
- HTML/CSS Grid positioning: Difficult to implement auto-layout, no smooth zoom
- Third-party diagramming library (mxGraph, GoJS): Commercial licensing or too heavyweight

---

## 7. Template Storage and Sharing Strategy

### Decision: JSON files in project repository with File System Access API

**Rationale**: Templates stored in Git-tracked JSON files enable team collaboration. File System Access API (Chrome 86+) allows import/export. Fallback to manual file upload/download for other browsers.

**Template File Format**:
```json
{
  "name": "CRUD Operations",
  "category": "crud",
  "description": "Creates four procedures (Create, Read, Update, Delete) for a single table",
  "author": "system",
  "created": "2025-10-17",
  "customizationPoints": ["tableName", "columns"],
  "procedureTemplate": {
    "namePattern": "{domain}_{table}_{action}",
    "parameters": [
      {
        "name": "PartNumber",
        "type": "VARCHAR",
        "length": 50,
        "direction": "IN",
        "customizable": true
      }
    ],
    "validations": [
      {
        "type": "Required Field",
        "targetParameter": "PartNumber",
        "errorCode": -1,
        "errorMessage": "Part Number is required"
      }
    ],
    "operations": [
      {
        "type": "INSERT",
        "targetTable": "{table}",
        "columnMappings": [
          { "column": "PartNumber", "value": "p_PartNumber", "customizable": true }
        ]
      }
    ]
  }
}
```

**File System Access API Pattern**:
```javascript
// template-manager.js
export class TemplateManager {
    async saveCustomTemplate(template) {
        if ('showSaveFilePicker' in window) {
            // Chrome 86+ with File System Access API
            const handle = await window.showSaveFilePicker({
                suggestedName: `${template.name}.json`,
                types: [{
                    description: 'Template JSON',
                    accept: { 'application/json': ['.json'] }
                }]
            });
            
            const writable = await handle.createWritable();
            await writable.write(JSON.stringify(template, null, 2));
            await writable.close();
        } else {
            // Fallback: manual download
            const blob = new Blob([JSON.stringify(template, null, 2)], 
                                   { type: 'application/json' });
            const url = URL.createObjectURL(blob);
            const a = document.createElement('a');
            a.href = url;
            a.download = `${template.name}.json`;
            a.click();
            URL.revokeObjectURL(url);
        }
    }
    
    async loadCustomTemplate() {
        if ('showOpenFilePicker' in window) {
            // Chrome 86+ with File System Access API
            const [handle] = await window.showOpenFilePicker({
                types: [{
                    description: 'Template JSON',
                    accept: { 'application/json': ['.json'] }
                }]
            });
            
            const file = await handle.getFile();
            const text = await file.text();
            return JSON.parse(text);
        } else {
            // Fallback: file input element
            return new Promise((resolve) => {
                const input = document.createElement('input');
                input.type = 'file';
                input.accept = '.json';
                input.onchange = async (e) => {
                    const file = e.target.files[0];
                    const text = await file.text();
                    resolve(JSON.parse(text));
                };
                input.click();
            });
        }
    }
}
```

**Template Validation Strategy** (from clarifications):
- Check if referenced tables exist in database metadata
- If missing, suggest fuzzy-matched alternatives (Levenshtein distance)
- User can accept substitution, select manually, or cancel

**Alternatives Considered**:
- IndexedDB storage: Not shareable across team, no Git tracking
- Server-side storage: Requires backend infrastructure, authentication
- Import/export only: No centralized template library, harder discoverability

---

## 8. Version History and localStorage Management

### Decision: Store last 5 versions per procedure in localStorage with size monitoring

**Rationale**: 5 versions covers typical iteration cycle. Size monitoring prevents localStorage quota exhaustion (~5-10MB limit).

**Implementation Pattern**:
```javascript
// storage-manager.js
export class StorageManager {
    constructor() {
        this.MAX_VERSIONS = 5;
        this.AUTO_SAVE_INTERVAL = 30000; // 30 seconds
    }
    
    saveVersion(procedureName, procedureData) {
        const key = `sp_versions_${procedureName}`;
        const versions = this.getVersions(procedureName);
        
        // Add new version with timestamp
        versions.unshift({
            timestamp: Date.now(),
            data: procedureData,
            hash: this.hashProcedure(procedureData) // Detect duplicates
        });
        
        // Prune to MAX_VERSIONS
        if (versions.length > this.MAX_VERSIONS) {
            versions.splice(this.MAX_VERSIONS);
        }
        
        try {
            localStorage.setItem(key, JSON.stringify(versions));
            return true;
        } catch (e) {
            if (e.name === 'QuotaExceededError') {
                this.handleQuotaExceeded();
                return false;
            }
            throw e;
        }
    }
    
    getVersions(procedureName) {
        const key = `sp_versions_${procedureName}`;
        const data = localStorage.getItem(key);
        return data ? JSON.parse(data) : [];
    }
    
    compareVersions(procedureName, version1Idx, version2Idx) {
        const versions = this.getVersions(procedureName);
        const v1 = versions[version1Idx].data;
        const v2 = versions[version2Idx].data;
        
        // Generate SQL for both versions
        const sql1 = this.generateSQL(v1);
        const sql2 = this.generateSQL(v2);
        
        // Compute diff (line-by-line comparison)
        return this.diffSQL(sql1, sql2);
    }
    
    handleQuotaExceeded() {
        // Strategy: Remove oldest versions from all procedures
        const keys = Object.keys(localStorage).filter(k => k.startsWith('sp_versions_'));
        
        keys.forEach(key => {
            const versions = JSON.parse(localStorage.getItem(key));
            if (versions.length > 2) {
                // Keep only 2 most recent
                versions.splice(2);
                localStorage.setItem(key, JSON.stringify(versions));
            }
        });
        
        // Show warning to user
        this.showQuotaWarning();
    }
    
    autoSaveSetup(getCurrentProcedure) {
        setInterval(() => {
            const procedure = getCurrentProcedure();
            if (procedure && procedure.name) {
                localStorage.setItem('sp_builder_autosave', JSON.stringify({
                    timestamp: Date.now(),
                    procedure: procedure
                }));
            }
        }, this.AUTO_SAVE_INTERVAL);
    }
    
    restoreSession() {
        const autosave = localStorage.getItem('sp_builder_autosave');
        if (!autosave) return null;
        
        const { timestamp, procedure } = JSON.parse(autosave);
        const age = Date.now() - timestamp;
        const ageHours = Math.floor(age / 3600000);
        const ageMinutes = Math.floor((age % 3600000) / 60000);
        
        // Format per clarifications: "Resume 'inv_inventory_Add_Item'? Last edited: 2 hours ago (Step 4: DML Operations)"
        return {
            procedure,
            promptMessage: `Resume '${procedure.name}'? Last edited: ${ageHours > 0 ? ageHours + ' hours' : ageMinutes + ' minutes'} ago (Step ${procedure.currentStep || 1})`
        };
    }
}
```

**Size Monitoring**:
- Track localStorage usage with `JSON.stringify(localStorage).length`
- Warn at 80% capacity
- Auto-prune old versions when exceeding 90%

**Alternatives Considered**:
- Unlimited versions: localStorage quota exhaustion risk
- Last 2 versions only: Too limited for iterative development
- IndexedDB for versions: Overkill for this use case, adds complexity

---

## 9. SQL Diff and Side-by-Side Comparison

### Decision: Line-by-line diff with Myers algorithm and color-coded rendering

**Rationale**: Myers diff algorithm (used in Git) provides accurate, human-readable diffs. Color coding matches developer familiarity (green=added, red=removed, yellow=modified).

**Implementation Pattern**:
```javascript
// sql-diff.js (using diff library or custom implementation)
export function diffSQL(oldSQL, newSQL) {
    const oldLines = oldSQL.split('\n');
    const newLines = newSQL.split('\n');
    
    // Simplified Myers diff (or use library like jsdiff)
    const diff = computeDiff(oldLines, newLines);
    
    return {
        additions: diff.filter(d => d.type === 'add').length,
        deletions: diff.filter(d => d.type === 'delete').length,
        modifications: diff.filter(d => d.type === 'modify').length,
        changes: diff
    };
}

function renderDiff(diffResult, containerElement) {
    const html = diffResult.changes.map(change => {
        let className = '';
        let prefix = ' ';
        
        if (change.type === 'add') {
            className = 'diff-added';
            prefix = '+';
        } else if (change.type === 'delete') {
            className = 'diff-deleted';
            prefix = '-';
        } else if (change.type === 'modify') {
            className = 'diff-modified';
            prefix = '~';
        }
        
        return `<div class="${className}"><span class="diff-prefix">${prefix}</span>${escapeHTML(change.line)}</div>`;
    }).join('');
    
    containerElement.innerHTML = html;
}
```

**CSS Styling**:
```css
.diff-added {
    background-color: #d4edda;
    color: #155724;
}

.diff-deleted {
    background-color: #f8d7da;
    color: #721c24;
    text-decoration: line-through;
}

.diff-modified {
    background-color: #fff3cd;
    color: #856404;
}

.diff-prefix {
    display: inline-block;
    width: 20px;
    font-weight: bold;
    margin-right: 8px;
}
```

**Alternatives Considered**:
- Character-level diff: Too granular, noisy for SQL
- Word-level diff: Better than character-level but less clear than line-level for SQL
- External diff tool integration: Requires extra steps, not seamless UX

---

## 10. Tutorial and Help System

### Decision: Interactive tutorial with highlight overlays + searchable help sidebar

**Rationale**: First-time users benefit from guided walkthrough. Experienced users need quick reference. Separate tutorial (one-time) from help (always available).

**Tutorial Implementation**:
```javascript
// tutorial.js
export class TutorialController {
    constructor() {
        this.steps = [
            {
                target: '#procedure-name',
                title: 'Step 1: Name Your Procedure',
                content: 'Enter procedure name following domain_table_action pattern (e.g., inv_inventory_Add_Item)',
                position: 'bottom',
                action: 'waitForInput',
                validation: (value) => /^[a-z]+_[a-z]+_[A-Z][a-zA-Z_]+$/.test(value)
            },
            {
                target: '#add-parameter-btn',
                title: 'Step 2: Add Parameters',
                content: 'Click to add input/output parameters. All procedures have p_Status and p_ErrorMsg automatically.',
                position: 'right',
                action: 'waitForClick'
            },
            // ... more steps
        ];
        
        this.currentStep = 0;
        this.overlay = null;
    }
    
    start() {
        if (localStorage.getItem('tutorial_completed')) {
            return; // Don't show again
        }
        
        this.showStep(0);
    }
    
    showStep(stepIdx) {
        const step = this.steps[stepIdx];
        const target = document.querySelector(step.target);
        
        // Create overlay (dims background)
        this.overlay = document.createElement('div');
        this.overlay.className = 'tutorial-overlay';
        document.body.appendChild(this.overlay);
        
        // Highlight target element
        target.classList.add('tutorial-highlight');
        target.scrollIntoView({ behavior: 'smooth', block: 'center' });
        
        // Show tooltip
        const tooltip = this.createTooltip(step, target);
        document.body.appendChild(tooltip);
        
        // Setup action listener
        if (step.action === 'waitForInput') {
            target.addEventListener('input', (e) => {
                if (step.validation(e.target.value)) {
                    this.nextStep();
                }
            }, { once: true });
        } else if (step.action === 'waitForClick') {
            target.addEventListener('click', () => {
                this.nextStep();
            }, { once: true });
        }
    }
    
    nextStep() {
        this.cleanup();
        this.currentStep++;
        
        if (this.currentStep >= this.steps.length) {
            this.complete();
        } else {
            this.showStep(this.currentStep);
        }
    }
    
    complete() {
        localStorage.setItem('tutorial_completed', 'true');
        this.showCompletionMessage();
    }
    
    cleanup() {
        if (this.overlay) {
            this.overlay.remove();
        }
        document.querySelectorAll('.tutorial-highlight').forEach(el => {
            el.classList.remove('tutorial-highlight');
        });
        document.querySelectorAll('.tutorial-tooltip').forEach(el => {
            el.remove();
        });
    }
}
```

**Help Sidebar Implementation**:
```javascript
// help.js
export class HelpSystem {
    constructor() {
        this.topics = this.loadHelpTopics(); // From JSON file
        this.searchIndex = this.buildSearchIndex();
    }
    
    search(query) {
        const results = [];
        const lowerQuery = query.toLowerCase();
        
        Object.entries(this.searchIndex).forEach(([topic, keywords]) => {
            if (keywords.some(kw => kw.includes(lowerQuery))) {
                results.push(this.topics[topic]);
            }
        });
        
        return results;
    }
    
    showTopic(topicId) {
        const topic = this.topics[topicId];
        const sidebar = document.getElementById('help-sidebar');
        
        sidebar.innerHTML = `
            <h2>${topic.title}</h2>
            <div class="help-content">${topic.content}</div>
            ${topic.codeExample ? `<pre><code>${topic.codeExample}</code></pre>` : ''}
            <div class="help-related">
                <h3>Related Topics</h3>
                ${topic.related.map(r => `<a href="#" data-topic="${r}">${this.topics[r].title}</a>`).join('')}
            </div>
        `;
    }
}
```

**Help Topics Structure** (JSON):
```json
{
  "getting-started": {
    "title": "Getting Started",
    "category": "basics",
    "content": "Welcome to the Stored Procedure Builder...",
    "keywords": ["intro", "start", "begin", "tutorial"],
    "related": ["procedure-naming", "parameters"]
  },
  "varchar-datatype": {
    "title": "VARCHAR Data Type",
    "category": "datatypes",
    "content": "VARCHAR stores variable-length text up to specified maximum length...",
    "keywords": ["text", "string", "char", "varchar"],
    "codeExample": "p_PartNumber VARCHAR(50)",
    "related": ["text-datatype", "int-datatype"]
  }
}
```

**Alternatives Considered**:
- Video tutorials: Harder to maintain, file size concerns, less accessible
- External documentation: Context-switching, not integrated into workflow
- Chatbot assistant: Requires AI integration, complexity overkill

---

## Summary of Key Research Decisions

| Research Area | Decision | Rationale |
|---------------|----------|-----------|
| MySQL Syntax | Target 5.7 with explicit restrictions | Production compatibility, avoid CTEs/window functions |
| JavaScript Architecture | Vanilla ES6 modules | No build step, zero npm dependencies, simpler deployment |
| Backend API | PHP proxy leveraging MAMP | Developers already have MAMP running, no new infrastructure |
| Drag-Drop | HTML5 API + keyboard controls | Accessibility (WCAG 2.1), dual interaction modes |
| SQL Validation | Client-side + PHP two-tier | Instant feedback with accurate MySQL validation |
| Flow Diagram | Canvas + Dagre.js auto-layout | 60fps performance, 25-node complexity support |
| Template Storage | JSON files + File System Access API | Git-tracked collaboration, browser native file I/O |
| Version History | Last 5 versions in localStorage | Balances history depth with storage constraints |
| Diff Algorithm | Myers line-diff with color coding | Git-style diffs familiar to developers |
| Help System | Interactive tutorial + searchable sidebar | First-time users guided, experienced users self-serve |

---

## Next Steps (Phase 1)

With research complete, Phase 1 will generate:

1. **data-model.md**: Entity definitions (ProcedureDefinition, Parameter, Validation, DMLOperation, Template)
2. **contracts/php-api-endpoints.md**: API contract for PHP backend endpoints
3. **contracts/javascript-modules.md**: JS module interfaces and dependencies
4. **quickstart.md**: Developer setup and first-time user guide

Phase 1 outputs will inform implementation tasks in Phase 2 (/speckit.tasks command).
