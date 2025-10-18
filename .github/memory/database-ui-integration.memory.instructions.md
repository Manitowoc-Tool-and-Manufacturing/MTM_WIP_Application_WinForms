---
description: 'Database metadata integration with UI components, dropdown population, and fallback patterns'
applyTo: '**/*.js,**/*.html'
---

# Database-UI Integration Memory

Patterns for integrating database metadata with UI components, handling async data loading, and graceful degradation.

## Table Dropdown Population Pattern

Use database metadata to populate dropdowns dynamically with manual fallback:

```javascript
// Field type system
const customizationPoints = [
    {
        key: 'TABLE_NAME',
        label: 'Table Name',
        type: 'table-select',  // Special type for metadata-backed dropdown
        required: true,
        tooltip: 'Exact MySQL table the procedure will target'
    }
];

// Render function handles type-based rendering
const renderCustomizationField = (point) => {
    const fieldId = `field-${point.key}`;
    const requiredAttr = point.required ? 'required' : '';
    
    let inputHtml = '';
    
    switch (point.type) {
        case 'table-select':
            if (tableMetadataAvailable && tableNames.length > 0) {
                // Metadata available - render dropdown
                const optionsHtml = tableNames
                    .map(name => `<option value="${escapeHtml(name)}">${escapeHtml(name)}</option>`)
                    .join('');
                inputHtml = `
                    <select id="${fieldId}" ${requiredAttr}>
                        <option value="">Select a table...</option>
                        ${optionsHtml}
                    </select>
                `;
            } else {
                // Metadata unavailable - fallback to text input with warning
                inputHtml = `
                    <div class="metadata-warning">
                        Table list unavailable. Enter the table name manually.
                    </div>
                    <input type="text" id="${fieldId}" ${requiredAttr} placeholder="Enter table name">
                `;
            }
            break;
            
        case 'textarea':
            inputHtml = `<textarea id="${fieldId}" ${requiredAttr}></textarea>`;
            break;
            
        default:
            inputHtml = `<input type="${point.type || 'text'}" id="${fieldId}" ${requiredAttr}>`;
            break;
    }
    
    return inputHtml;
};
```

## Metadata Availability Tracking

Track metadata state across the application:

```javascript
let tableNames = [];
let tableMetadataAvailable = false;
let metadataErrorShown = false;

async function ensureTableList() {
    if (dbMetadata.tables.length === 0 || dbMetadata.isStale()) {
        const result = await dbMetadata.fetchTables();
        
        if (!result.success && dbMetadata.tables.length === 0 && !metadataErrorShown) {
            metadataErrorShown = true;
            showError({
                error_type: 'metadata',
                user_message: 'Unable to load table list from the database. Start MAMP and refresh, or type the table name manually.',
                technical_detail: result.error
            });
        }
    }
    
    tableNames = (dbMetadata.tables || [])
        .map(t => t.name)
        .sort((a, b) => a.localeCompare(b));
    
    tableMetadataAvailable = tableNames.length > 0;
}
```

**State tracking benefits:**
- Know when to show dropdowns vs text inputs
- Avoid redundant error messages
- Cache table list for performance
- Support graceful degradation

## Loading Indicators for Async Operations

Show loading states when fetching metadata:

```javascript
// Before opening panel that needs metadata
async function openCustomizationPanel(templateId) {
    if (!tableMetadataAvailable) {
        showLoading('Loading database metadata...');
        await ensureTableList();
        hideLoading();
    } else if (dbMetadata.isStale()) {
        // Refresh in background if stale
        await ensureTableList();
    }
    
    // Now render with metadata
    renderPanel(templateId);
}
```

## Stale Metadata Detection

Implement cache invalidation for database metadata:

```javascript
class DatabaseMetadata {
    constructor(apiBaseUrl = '/sp-builder/api') {
        this.apiBaseUrl = apiBaseUrl;
        this.tables = [];
        this.fetchedAt = null;
        this.STALE_THRESHOLD_MS = 10 * 60 * 1000; // 10 minutes
        
        this.loadFromCache();
    }
    
    isStale() {
        if (!this.fetchedAt) {
            return true;
        }
        
        const age = Date.now() - this.fetchedAt.getTime();
        return age > this.STALE_THRESHOLD_MS;
    }
    
    async refresh() {
        console.log('[DatabaseMetadata] Refreshing metadata...');
        const result = await this.fetchTables();
        
        if (result.success) {
            // Clear column caches
            this.tables.forEach(t => {
                delete t.columns;
                delete t.columnsFetched;
            });
            this.saveToCache();
        }
        
        return result;
    }
}
```

## LocalStorage Caching Pattern

Cache metadata in browser storage to reduce server requests:

```javascript
saveToCache() {
    try {
        const cache = {
            database: this.database,
            tables: this.tables,
            fetchedAt: this.fetchedAt ? this.fetchedAt.toISOString() : null
        };
        
        localStorage.setItem('sp_database_metadata', JSON.stringify(cache));
    } catch (error) {
        console.warn('[DatabaseMetadata] Failed to save cache:', error);
    }
}

loadFromCache() {
    try {
        const cached = localStorage.getItem('sp_database_metadata');
        if (!cached) {
            return;
        }
        
        const data = JSON.parse(cached);
        this.database = data.database || this.database;
        this.tables = data.tables || [];
        this.fetchedAt = data.fetchedAt ? new Date(data.fetchedAt) : null;
        this.stale = this.isStale();
        
        console.log(`[DatabaseMetadata] Loaded ${this.tables.length} tables from cache`);
    } catch (error) {
        console.warn('[DatabaseMetadata] Failed to load cache:', error);
    }
}
```

## Graceful Degradation Strategy

Always provide manual alternatives when automation fails:

1. **Dropdown with options** - Best UX when metadata available
2. **Text input with warning** - Fallback when metadata unavailable
3. **Validation feedback** - Check entered values against metadata when available
4. **Manual entry tooltip** - Guide users on correct format

```javascript
// Warning message when metadata unavailable
<div class="metadata-warning">
    Table list unavailable. Enter the table name manually.
</div>
```

```css
.metadata-warning {
    font-size: 0.85rem;
    color: var(--color-text-secondary);
    margin-bottom: var(--spacing-sm);
}
```

## Error Handling for Metadata Operations

Handle metadata fetch failures gracefully:

```javascript
async fetchTables() {
    try {
        const response = await fetch(`${this.apiBaseUrl}/get-tables.php`);
        const result = await response.json();
        
        if (!result.success) {
            throw new Error(result.user_message || 'Failed to fetch tables');
        }
        
        this.tables = result.data.tables || [];
        this.database = result.data.database;
        this.fetchedAt = new Date();
        this.stale = false;
        
        this.saveToCache();
        
        return {
            success: true,
            tables: this.tables,
            database: this.database
        };
        
    } catch (error) {
        console.error('[DatabaseMetadata] fetchTables error:', error);
        return {
            success: false,
            error: error.message,
            cached: this.tables.length > 0 // Can fall back to cache
        };
    }
}
```

**Error handling strategy:**
- Log errors for debugging
- Return structured error responses
- Indicate if cached data available
- Allow application to continue with degraded functionality

## Initialization Sequence

Load metadata early in application lifecycle:

```javascript
// After template manager loads
showLoading('Loading templates...');
await templateManager.initialize();
hideLoading();

// Load metadata in parallel (or show loading if cache empty)
let metadataLoadingShown = false;
if (dbMetadata.tables.length === 0) {
    metadataLoadingShown = true;
    showLoading('Loading database metadata...');
}

await ensureTableList();

if (metadataLoadingShown) {
    hideLoading();
}
```

**Benefits:**
- Metadata ready when user interacts
- Reduces perceived latency
- Provides feedback during load
- Graceful handling when unavailable
