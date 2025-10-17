# JavaScript Modules Contract

**Feature Branch**: `004-interactive-mysql-5`  
**Date**: 2025-10-17  
**Module System**: ES6 Modules (type="module" in HTML)

---

## Module Overview

All JavaScript files are ES6 modules with explicit import/export. No global namespace pollution. Modules communicate through well-defined interfaces.

**Module Dependency Graph**:
```
app.js (entry point)
├── wizard-controller.js
│   ├── procedure-model.js
│   ├── storage-manager.js
│   └── sql-generator.js
├── database-metadata.js
│   └── utils.js
├── drag-drop.js
├── flow-diagram.js
│   └── procedure-model.js
├── template-manager.js
│   ├── procedure-model.js
│   └── storage-manager.js
├── export-manager.js
│   ├── sql-generator.js
│   └── storage-manager.js
└── sql-validator.js
```

---

## Core Modules

### procedure-model.js

Defines all data model classes.

**Exports**:
```javascript
export class ProcedureDefinition { }
export class Parameter { }
export class ValidationRule { }
export class DMLOperation { }
export class LocalVariable { }
export class FlowDiagram { }
export class Template { }

export const ValidationRuleType = { /* enum */ };
export const DataTypes = { /* enum */ };
```

**Key Methods**:
- `ProcedureDefinition.toJSON()`, `fromJSON(data)`, `validate()`, `toSQL()`
- `Parameter.getTypeDeclaration()`, `validate()`
- `ValidationRule.toSQL()`, `getDependencies()`
- `DMLOperation.toSQL()`, `requiresWhereClause()`

**No Dependencies**

---

### wizard-controller.js

Manages wizard navigation and state.

**Exports**:
```javascript
export class WizardController {
    constructor()
    goToStep(stepNumber)
    nextStep()
    previousStep()
    validateCurrentStep()
    saveState()
    async restoreSession()
}
```

**Dependencies**: `procedure-model.js`, `storage-manager.js`, `sql-generator.js`

**Events Emitted**:
- `step-changed`: Fired when wizard step changes
- `validation-error`: Fired when step validation fails
- `state-saved`: Fired after successful auto-save

---

### database-metadata.js

Handles database connection and metadata fetching.

**Exports**:
```javascript
export class DatabaseMetadata {
    constructor(apiBaseUrl)
    async fetchTables()
    async fetchColumns(tableName)
    getTable(tableName)
    getColumns(tableName)
    isStale()
    async refresh()
}

export async function testConnection(apiBaseUrl)
```

**Dependencies**: `utils.js` (for API calls)

**Caching**: Stores fetched metadata in memory and localStorage. Expires after 10 minutes.

---

### sql-generator.js

Generates MySQL 5.7 SQL from ProcedureDefinition.

**Exports**:
```javascript
export class SQLGenerator {
    constructor(procedure, options = {})
    generate()
    generateHeader()
    generateParameters()
    generateVariables()
    generateValidations()
    generateOperations()
    generateErrorHandler()
}

export function formatSQL(sql, indentSize = 4)
```

**Dependencies**: `procedure-model.js`

**Configuration Options**:
```javascript
{
    indentSize: 4,
    includeComments: true,
    includeHeaderComment: true,
    transactionControl: true
}
```

---

### sql-validator.js

Client-side SQL syntax validation + PHP backend validation.

**Exports**:
```javascript
export class MySQLValidator {
    constructor(apiBaseUrl)
    validateClientSide(sql)
    async validateServerSide(sql)
    async validate(sql)  // Runs both tiers
}

export function highlightSyntaxErrors(sql, errors)
```

**Dependencies**: None

**Returns**:
```javascript
{
    valid: Boolean,
    errors: [
        { line: Number, message: String, severity: 'error'|'warning' }
    ]
}
```

---

### storage-manager.js

Manages localStorage operations, auto-save, version history.

**Exports**:
```javascript
export class StorageManager {
    constructor()
    saveVersion(procedureName, procedureData)
    getVersions(procedureName)
    compareVersions(procedureName, v1Idx, v2Idx)
    autoSaveSetup(getCurrentProcedure)
    restoreSession()
    clearAll()
    getStorageUsage()
}

export const STORAGE_KEYS = {
    STATE: 'sp_builder_state',
    AUTOSAVE: 'sp_builder_autosave',
    METADATA: 'sp_database_metadata',
    TEMPLATES: 'sp_custom_templates',
    CONFIG: 'sp_export_config'
};
```

**Dependencies**: `procedure-model.js`

**Auto-save**: Saves every 30 seconds when procedure name is set.

---

### drag-drop.js

Drag-and-drop with keyboard accessibility.

**Exports**:
```javascript
export class DragDropManager {
    constructor(containerSelector, options)
    enable()
    disable()
    getOrder()
    setOrder(itemIds)
}

export function setupKeyboardControls(container, onReorder)
```

**Dependencies**: None

**Options**:
```javascript
{
    onReorder: (newOrder) => { },
    itemSelector: '.draggable-item',
    handleSelector: '.drag-handle',
    allowKeyboard: true
}
```

---

### flow-diagram.js

Canvas-based flow diagram rendering with Dagre layout.

**Exports**:
```javascript
export class FlowDiagram {
    constructor(canvasElement, procedure)
    buildGraph()
    render()
    setupZoomPan()
    exportImage()
}
```

**Dependencies**: `procedure-model.js`, `dagre.min.js` (external library)

**Interaction**:
- Mouse wheel: Zoom
- Mouse drag: Pan
- Click node: Highlight operation in wizard

---

### template-manager.js

Template loading, saving, validation, substitution.

**Exports**:
```javascript
export class TemplateManager {
    constructor(apiBaseUrl)
    async loadBuiltInTemplates()
    async loadCustomTemplates()
    async saveCustomTemplate(template)
    async loadFromFile()
    async saveToFile(template)
    applyTemplate(template, substitutions)
    async validateTemplate(template, databaseMetadata)
}
```

**Dependencies**: `procedure-model.js`, `storage-manager.js`

**Built-in Template Files**:
- `/templates/crud-templates.json`
- `/templates/batch-templates.json`
- `/templates/transfer-templates.json`
- `/templates/audit-templates.json`

---

### export-manager.js

SQL file export with version control.

**Exports**:
```javascript
export class ExportManager {
    constructor(config)
    async exportToFile(procedure)
    generateFileName(procedure)
    showAnalysisPrompt()
}

export async function downloadFile(content, fileName, mimeType)
```

**Dependencies**: `sql-generator.js`, `storage-manager.js`

**File System Access API**:
- Uses `showSaveFilePicker()` when available (Chrome 86+)
- Fallback to blob download for other browsers

---

### utils.js

Shared utility functions.

**Exports**:
```javascript
export async function apiCall(endpoint, options = {})
export function debounce(func, wait)
export function throttle(func, limit)
export function escapeHTML(text)
export function formatDate(date)
export function generateUUID()
export function deepClone(obj)
```

**Dependencies**: None

---

## Integration Pattern

### Initialization (app.js)

```javascript
import { WizardController } from './wizard-controller.js';
import { DatabaseMetadata } from './database-metadata.js';
import { StorageManager } from './storage-manager.js';
import { TemplateManager } from './template-manager.js';

const API_BASE_URL = '/api/';

async function initialize() {
    const storage = new StorageManager();
    const metadata = new DatabaseMetadata(API_BASE_URL);
    const templates = new TemplateManager(API_BASE_URL);
    const wizard = new WizardController();
    
    // Restore session
    const session = storage.restoreSession();
    if (session) {
        const resume = confirm(session.promptMessage);
        if (resume) {
            wizard.restoreProcedure(session.procedure);
        }
    }
    
    // Fetch metadata
    try {
        await metadata.fetchTables();
    } catch (e) {
        showError('Database connection failed. Check MAMP.');
    }
    
    // Load templates
    await templates.loadBuiltInTemplates();
    await templates.loadCustomTemplates();
    
    // Setup auto-save
    storage.autoSaveSetup(() => wizard.getCurrentProcedure());
    
    // Start wizard
    wizard.goToStep(1);
}

document.addEventListener('DOMContentLoaded', initialize);
```

---

## Event System

Modules communicate via CustomEvents:

```javascript
// Dispatch
window.dispatchEvent(new CustomEvent('step-changed', { 
    detail: { step: 2, procedure: wizard.procedure } 
}));

// Listen
window.addEventListener('step-changed', (e) => {
    console.log('Now on step', e.detail.step);
    updateFlowDiagram(e.detail.procedure);
});
```

**Standard Events**:
- `step-changed`: Wizard navigation
- `validation-error`: Step validation failed
- `state-saved`: Auto-save completed
- `metadata-refreshed`: Database metadata updated
- `template-applied`: Template loaded into wizard
- `procedure-exported`: SQL file exported

---

## Error Handling Pattern

All async operations use consistent error handling:

```javascript
try {
    const result = await someAsyncOperation();
    if (!result) {
        throw new Error('Operation failed');
    }
    return result;
} catch (error) {
    console.error('Error in someAsyncOperation:', error);
    showError(error.message, { retry: () => someAsyncOperation() });
    return null;
}
```

**User-Facing Errors**:
```javascript
function showError(message, options = {}) {
    const dialog = document.getElementById('error-dialog');
    dialog.querySelector('.message').textContent = message;
    
    if (options.retry) {
        const retryBtn = dialog.querySelector('.retry-btn');
        retryBtn.style.display = 'block';
        retryBtn.onclick = options.retry;
    }
    
    if (options.technicalDetail) {
        const details = dialog.querySelector('.technical-detail');
        details.textContent = options.technicalDetail;
        details.style.display = 'block';
    }
    
    dialog.showModal();
}
```

---

## Testing Strategy

### Manual Testing Scenarios

1. **Module Loading**: Verify all modules load without errors in browser console
2. **API Integration**: Test each API call with success/failure scenarios
3. **localStorage**: Fill localStorage to near capacity and verify pruning
4. **Drag-Drop**: Test with mouse and keyboard (Ctrl+Up/Down)
5. **Flow Diagram**: Create procedure with 20+ operations and verify rendering
6. **Template Application**: Apply each built-in template and verify generated SQL
7. **Export**: Export procedures and verify .sql files execute in MySQL 5.7

### Browser Compatibility Testing

- Chrome 86+ (primary)
- Firefox 90+ (graceful degradation)
- Edge 90+ (graceful degradation)
- Safari 14+ (File System Access API unavailable, fallback works)

---

## Performance Benchmarks

- Module loading: <500ms for all modules
- SQL generation: <500ms for 25-operation procedure
- Flow diagram render: <2 seconds auto-layout
- Database metadata fetch: <5 seconds for 100 tables
- localStorage operations: <100ms save/restore

---

## Next Steps

See `quickstart.md` for developer setup instructions and first-time user guide.
