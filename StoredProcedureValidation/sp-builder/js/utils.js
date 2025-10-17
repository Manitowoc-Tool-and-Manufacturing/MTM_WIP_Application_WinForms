/**
 * Utility Functions and UI Component Builders
 * 
 * Provides reusable helper functions and UI component generators
 * for database metadata integration.
 * 
 * @module utils
 */

import { dbMetadata } from './database-metadata.js';

/**
 * Create a table dropdown populated with database metadata
 * 
 * @param {Object} options Configuration options
 * @param {string} options.containerId - ID of container element
 * @param {string} options.placeholder - Placeholder text
 * @param {Function} options.onChange - Change callback (tableName) => {}
 * @param {string} options.selectedTable - Initially selected table
 * @returns {HTMLSelectElement} Dropdown element
 */
export function createTableDropdown(options = {}) {
    const {
        containerId,
        placeholder = 'Select a table...',
        onChange = () => {},
        selectedTable = ''
    } = options;
    
    const select = document.createElement('select');
    select.className = 'form-select table-dropdown';
    select.id = containerId || 'table-select';
    
    // Add placeholder option
    const placeholderOption = document.createElement('option');
    placeholderOption.value = '';
    placeholderOption.textContent = placeholder;
    placeholderOption.disabled = true;
    placeholderOption.selected = !selectedTable;
    select.appendChild(placeholderOption);
    
    // Populate with tables (sorted alphabetically)
    const sortedTables = [...dbMetadata.tables].sort((a, b) => 
        a.name.localeCompare(b.name)
    );
    
    sortedTables.forEach(table => {
        const option = document.createElement('option');
        option.value = table.name;
        option.textContent = table.name;
        option.selected = table.name === selectedTable;
        
        // Add metadata as data attributes
        option.dataset.engine = table.engine || '';
        option.dataset.rows = table.rows || 0;
        
        select.appendChild(option);
    });
    
    // Attach change handler
    select.addEventListener('change', (e) => {
        onChange(e.target.value);
    });
    
    return select;
}

/**
 * Create a column checklist with type annotations
 * 
 * @param {Object} options Configuration options
 * @param {string} options.tableName - Table to fetch columns for
 * @param {string} options.containerId - ID of container element
 * @param {Array} options.selectedColumns - Initially selected column names
 * @param {Function} options.onChange - Change callback (selectedColumns) => {}
 * @param {boolean} options.showTypes - Show column types in parentheses
 * @param {boolean} options.disableAutoIncrement - Disable auto-increment columns
 * @returns {Promise<HTMLDivElement>} Column checklist container
 */
export async function createColumnChecklist(options = {}) {
    const {
        tableName,
        containerId = 'column-checklist',
        selectedColumns = [],
        onChange = () => {},
        showTypes = true,
        disableAutoIncrement = true
    } = options;
    
    const container = document.createElement('div');
    container.className = 'column-checklist';
    container.id = containerId;
    
    if (!tableName) {
        container.innerHTML = '<p class="text-muted">Select a table first</p>';
        return container;
    }
    
    // Show loading state
    container.innerHTML = '<p class="text-muted">Loading columns...</p>';
    
    // Fetch columns
    const columns = await dbMetadata.getColumns(tableName);
    
    if (columns.length === 0) {
        container.innerHTML = '<p class="text-warning">No columns found</p>';
        return container;
    }
    
    // Clear loading message
    container.innerHTML = '';
    
    // Create checkbox for each column
    columns.forEach(column => {
        const item = document.createElement('div');
        item.className = 'column-checkbox-item';
        
        const checkbox = document.createElement('input');
        checkbox.type = 'checkbox';
        checkbox.id = `col-${column.name}`;
        checkbox.value = column.name;
        checkbox.checked = selectedColumns.includes(column.name);
        
        // Disable auto-increment columns if requested
        if (disableAutoIncrement && column.autoIncrement) {
            checkbox.disabled = true;
            item.classList.add('disabled');
            item.title = 'Auto-increment column (cannot be manually set)';
        }
        
        const label = document.createElement('label');
        label.htmlFor = `col-${column.name}`;
        
        // Build label text with type
        let labelText = column.name;
        if (showTypes) {
            const typeDisplay = formatColumnType(column);
            labelText += ` <span class="column-type">(${typeDisplay})</span>`;
        }
        label.innerHTML = labelText;
        
        // Add primary key indicator
        if (column.key === 'PRI') {
            const pkBadge = document.createElement('span');
            pkBadge.className = 'badge bg-primary ms-2';
            pkBadge.textContent = 'PK';
            label.appendChild(pkBadge);
        }
        
        // Add nullable indicator
        if (!column.nullable && column.key !== 'PRI') {
            const reqBadge = document.createElement('span');
            reqBadge.className = 'badge bg-warning ms-1';
            reqBadge.textContent = 'Required';
            label.appendChild(reqBadge);
        }
        
        item.appendChild(checkbox);
        item.appendChild(label);
        container.appendChild(item);
        
        // Attach change handler
        checkbox.addEventListener('change', () => {
            const selected = Array.from(
                container.querySelectorAll('input[type="checkbox"]:checked')
            ).map(cb => cb.value);
            onChange(selected);
        });
    });
    
    return container;
}

/**
 * Format column type for display
 * @param {Object} column - Column metadata
 * @returns {string} Formatted type (e.g., "VARCHAR(50)", "INT", "DECIMAL(10,2)")
 */
function formatColumnType(column) {
    const baseType = column.type.toUpperCase();
    
    if (baseType.includes('VARCHAR') || baseType.includes('CHAR')) {
        return `VARCHAR(${column.length || 255})`;
    }
    
    if (baseType.includes('DECIMAL') || baseType.includes('NUMERIC')) {
        return `DECIMAL(${column.precision || 10},${column.scale || 0})`;
    }
    
    if (baseType.includes('INT')) {
        return 'INT';
    }
    
    if (baseType.includes('DATE')) {
        if (baseType === 'DATETIME') return 'DATETIME';
        if (baseType === 'DATE') return 'DATE';
        if (baseType === 'TIMESTAMP') return 'TIMESTAMP';
    }
    
    if (baseType.includes('TEXT')) {
        return 'TEXT';
    }
    
    if (baseType.includes('BOOL') || baseType.includes('BIT')) {
        return 'BOOLEAN';
    }
    
    return baseType;
}

/**
 * Show error dialog with consistent styling
 * @param {Object} error - Error object from API response
 * @param {string} error.error_type - Error type
 * @param {string} error.user_message - User-friendly message
 * @param {string} error.technical_detail - Technical details
 */
export function showError(error) {
    const modal = document.createElement('div');
    modal.className = 'modal fade show';
    modal.style.display = 'block';
    modal.setAttribute('role', 'dialog');
    
    const errorIcon = getErrorIcon(error.error_type);
    
    modal.innerHTML = `
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-danger text-white">
                    <h5 class="modal-title">
                        ${errorIcon} Error
                    </h5>
                    <button type="button" class="btn-close btn-close-white" data-dismiss="modal"></button>
                </div>
                <div class="modal-body">
                    <p class="lead">${escapeHTML(error.user_message || 'An error occurred')}</p>
                    ${error.technical_detail ? `
                        <details class="mt-3">
                            <summary class="text-muted">Technical Details</summary>
                            <pre class="bg-light p-3 mt-2 rounded"><code>${escapeHTML(error.technical_detail)}</code></pre>
                        </details>
                    ` : ''}
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    `;
    
    document.body.appendChild(modal);
    
    // Close button handlers
    modal.querySelectorAll('[data-dismiss="modal"]').forEach(btn => {
        btn.addEventListener('click', () => {
            modal.remove();
        });
    });
}

/**
 * Get error icon based on error type
 * @param {string} errorType - Error type from API
 * @returns {string} Icon HTML
 */
function getErrorIcon(errorType) {
    const icons = {
        'connection_failed': '🔌',
        'validation': '⚠️',
        'database': '🗄️',
        'syntax': '📝',
        'default': '❌'
    };
    
    return icons[errorType] || icons.default;
}

/**
 * Escape HTML to prevent XSS
 * @param {string} str - String to escape
 * @returns {string} Escaped string
 */
export function escapeHTML(str) {
    const div = document.createElement('div');
    div.textContent = str;
    return div.innerHTML;
}

/**
 * Show loading spinner in container
 * @param {string|HTMLElement} container - Container ID or element
 * @param {string} message - Loading message
 */
export function showLoading(container, message = 'Loading...') {
    const el = typeof container === 'string' 
        ? document.getElementById(container) 
        : container;
    
    if (!el) return;
    
    el.innerHTML = `
        <div class="text-center py-4">
            <div class="spinner-border text-primary" role="status">
                <span class="visually-hidden">Loading...</span>
            </div>
            <p class="mt-2 text-muted">${escapeHTML(message)}</p>
        </div>
    `;
}

/**
 * Debounce function execution
 * @param {Function} func - Function to debounce
 * @param {number} wait - Wait time in milliseconds
 * @returns {Function} Debounced function
 */
export function debounce(func, wait = 300) {
    let timeout;
    return function executedFunction(...args) {
        const later = () => {
            clearTimeout(timeout);
            func(...args);
        };
        clearTimeout(timeout);
        timeout = setTimeout(later, wait);
    };
}

/**
 * Format timestamp for display
 * @param {Date|string} date - Date to format
 * @returns {string} Formatted date string
 */
export function formatTimestamp(date) {
    const d = date instanceof Date ? date : new Date(date);
    
    if (isNaN(d.getTime())) {
        return 'Invalid date';
    }
    
    const now = new Date();
    const diffMs = now - d;
    const diffMins = Math.floor(diffMs / 60000);
    const diffHours = Math.floor(diffMins / 60);
    const diffDays = Math.floor(diffHours / 24);
    
    if (diffMins < 1) {
        return 'Just now';
    } else if (diffMins < 60) {
        return `${diffMins} minute${diffMins !== 1 ? 's' : ''} ago`;
    } else if (diffHours < 24) {
        return `${diffHours} hour${diffHours !== 1 ? 's' : ''} ago`;
    } else if (diffDays < 7) {
        return `${diffDays} day${diffDays !== 1 ? 's' : ''} ago`;
    } else {
        return d.toLocaleDateString();
    }
}

/**
 * Copy text to clipboard
 * @param {string} text - Text to copy
 * @returns {Promise<boolean>} Success status
 */
export async function copyToClipboard(text) {
    try {
        if (navigator.clipboard && navigator.clipboard.writeText) {
            await navigator.clipboard.writeText(text);
            return true;
        } else {
            // Fallback for older browsers
            const textarea = document.createElement('textarea');
            textarea.value = text;
            textarea.style.position = 'fixed';
            textarea.style.opacity = '0';
            document.body.appendChild(textarea);
            textarea.select();
            const success = document.execCommand('copy');
            document.body.removeChild(textarea);
            return success;
        }
    } catch (error) {
        console.error('Copy to clipboard failed:', error);
        return false;
    }
}

/**
 * Show success toast notification
 * @param {string} message - Success message
 * @param {number} duration - Duration in milliseconds
 */
export function showSuccess(message, duration = 3000) {
    const toast = document.createElement('div');
    toast.className = 'toast-notification success';
    toast.innerHTML = `
        <span class="toast-icon">✓</span>
        <span class="toast-message">${escapeHTML(message)}</span>
    `;
    
    document.body.appendChild(toast);
    
    // Trigger animation
    setTimeout(() => toast.classList.add('show'), 10);
    
    // Remove after duration
    setTimeout(() => {
        toast.classList.remove('show');
        setTimeout(() => toast.remove(), 300);
    }, duration);
}
