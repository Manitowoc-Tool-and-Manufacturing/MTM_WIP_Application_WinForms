---
description: 'JavaScript module integration patterns, async operations, and defensive programming'
applyTo: '**/*.js,**/*.html'
---

# JavaScript Modules Memory

Module integration patterns, guard clauses, and defensive programming for robust JavaScript applications.

## Guard Clauses for Module Methods

Always verify that imported module methods exist before calling them:

```javascript
// ✅ CORRECT: Guard clause before use
if (!dbMetadata || typeof dbMetadata.tableExists !== 'function') {
    return { valid: true, warnings: [], suggestions: {} };
}

const tableExists = dbMetadata.tableExists(tableName);
```

**Why this matters:**
- Prevents "X is not a function" runtime errors
- Allows graceful degradation when modules aren't fully loaded
- Makes code resilient to import order issues
- Enables optional dependencies

## Module Method Implementation Checklist

When adding new methods to exported modules:

1. **Implement the method** in the module file
2. **Export it** if it's intended for external use
3. **Update callers** that reference it
4. **Add guard clauses** where the method is optional

```javascript
// In database-metadata.js
export class DatabaseMetadata {
    tableExists(tableName) {
        if (!tableName) {
            return false;
        }
        
        const normalized = tableName.toLowerCase();
        return this.tables.some(t => (t.name || '').toLowerCase() === normalized);
    }
}

// In template-manager.js (caller)
validateWithMetadata(template, values) {
    const warnings = [];
    const suggestions = {};
    
    // Guard clause for optional dbMetadata dependency
    if (!dbMetadata || typeof dbMetadata.tableExists !== 'function') {
        return { valid: true, warnings, suggestions };
    }
    
    if (values.TABLE_NAME) {
        const exists = dbMetadata.tableExists(values.TABLE_NAME);
        if (!exists) {
            warnings.push(`Table "${values.TABLE_NAME}" not found in database`);
        }
    }
    
    return { valid: warnings.length === 0, warnings, suggestions };
}
```

## Async Initialization Patterns

When modules depend on async data loading:

```javascript
// Track metadata availability
let metadataAvailable = false;
let metadataErrorShown = false;

async function ensureTableList() {
    if (dbMetadata.tables.length === 0 || dbMetadata.isStale()) {
        const result = await dbMetadata.fetchTables();
        
        if (!result.success && dbMetadata.tables.length === 0 && !metadataErrorShown) {
            metadataErrorShown = true;
            showError({
                error_type: 'metadata',
                user_message: 'Unable to load table list. Start MAMP and refresh, or type manually.',
                technical_detail: result.error
            });
        }
    }
    
    tableNames = (dbMetadata.tables || [])
        .map(t => t.name)
        .sort((a, b) => a.localeCompare(b));
    
    metadataAvailable = tableNames.length > 0;
}

// Call before using metadata
async function openPanel(templateId) {
    if (!metadataAvailable) {
        showLoading('Loading database metadata...');
        await ensureTableList();
        hideLoading();
    }
    
    // Now safe to use metadata
    renderForm(templateId);
}
```

**Key patterns:**
- Check availability before use
- Load async data once and cache
- Show loading indicators during fetch
- Provide user feedback on errors
- Track error display to avoid spam

## Defensive Data Access

Normalize and validate data before comparison:

```javascript
// ✅ CORRECT: Normalize for comparison
tableExists(tableName) {
    if (!tableName) {
        return false;
    }
    
    const normalized = tableName.toLowerCase();
    return this.tables.some(t => (t.name || '').toLowerCase() === normalized);
}
```

```javascript
// ❌ WRONG: Assumes data shape
tableExists(tableName) {
    return this.tables.some(t => t.name === tableName); // Breaks if name is null/undefined
}
```

**Defensive checks:**
- Validate input parameters (null/undefined/empty)
- Normalize strings for comparison (toLowerCase, trim)
- Use optional chaining: `t.name || ''`
- Provide sensible defaults

## Input Validation and Sanitization

Always trim and validate user input before processing:

```javascript
// Gather form values
const values = {};
selectedTemplate.customizationPoints.forEach(point => {
    const field = document.getElementById(`field-${point.key}`);
    if (field) {
        let value = field.value;
        
        // Trim strings
        if (typeof value === 'string') {
            value = value.trim();
        }
        
        values[point.key] = value;
    }
});
```

## HTML Escaping for Dynamic Content

Always escape user input before inserting into HTML:

```javascript
const escapeHtml = (value = '') => `${value}`
    .replace(/&/g, '&amp;')
    .replace(/</g, '&lt;')
    .replace(/>/g, '&gt;')
    .replace(/"/g, '&quot;')
    .replace(/'/g, '&#39;');

// Use in template generation
const tooltipHtml = point.tooltip
    ? `<span class="info-icon" title="${escapeHtml(point.tooltip)}">ⓘ</span>`
    : '';
```

**Why:** Prevents XSS attacks and rendering issues when user input contains special characters.

## Module Import Organization

Structure imports logically at the top of files:

```javascript
// External dependencies first (if any)
import { someLib } from 'external-lib';

// Internal modules grouped by purpose
import { templateManager } from './js/template-manager.js';
import { storageManager } from './js/storage-manager.js';
import { dbMetadata } from './js/database-metadata.js';

// Utility modules last
import { showSuccess, showError } from './js/utils.js';
import { initNavigation } from './js/navigation.js';
import { showLoading, hideLoading } from './js/loading.js';
```

**Benefits:**
- Easy to spot missing imports
- Clear dependency structure
- Consistent across files
- Helps avoid circular dependencies
