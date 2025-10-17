/**
 * DML Operations Controller
 * 
 * Manages the visual builder for INSERT, UPDATE, DELETE, and SELECT operations
 */

import { DMLOperation, ColumnMapping, WhereCondition, OrderByClause, DML_OPERATION_TYPES, WHERE_OPERATORS } from './procedure-model.js';
import { dbMetadata } from './database-metadata.js';
import { createTableDropdown, createColumnChecklist, showError, showSuccess, debounce } from './utils.js';
import { sqlGenerator } from './sql-generator.js';

export class DMLOperationsController {
    constructor() {
        this.operations = [];
        this.currentEditingId = null;
        
        this.initializeElements();
        this.attachEventListeners();
        this.initializeMetadata();
        this.updatePreview();
    }

    initializeElements() {
        this.operationTypeSelect = document.getElementById('operation-type-select');
        this.btnAddOperation = document.getElementById('btn-add-operation');
        this.operationsList = document.getElementById('operations-list');
        this.noOperationsMessage = document.getElementById('no-operations-message');
        this.sqlPreview = document.getElementById('sql-preview-content');
        this.btnCopySQL = document.getElementById('btn-copy-sql');
    }

    attachEventListeners() {
        this.btnAddOperation.addEventListener('click', () => this.addOperation());
        this.btnCopySQL.addEventListener('click', () => this.copySQL());
        
        // Debounced preview update
        this.debouncedUpdatePreview = debounce(() => this.updatePreview(), 300);
    }

    async initializeMetadata() {
        if (dbMetadata.tables.length === 0) {
            const result = await dbMetadata.fetchTables();
            if (!result.success) {
                showError({
                    error_type: 'database',
                    user_message: 'Failed to load database metadata',
                    technical_detail: result.error
                });
            }
        }
    }

    addOperation() {
        const type = this.operationTypeSelect.value;
        
        if (!type) {
            showError({
                error_type: 'validation',
                user_message: 'Please select an operation type'
            });
            return;
        }

        const operation = new DMLOperation({
            type: type,
            order: this.operations.length
        });

        this.operations.push(operation);
        this.renderOperation(operation);
        this.editOperation(operation.id);
        
        // Hide "no operations" message
        if (this.noOperationsMessage) {
            this.noOperationsMessage.style.display = 'none';
        }

        // Reset selector
        this.operationTypeSelect.value = '';
        
        this.updatePreview();
    }

    renderOperation(operation) {
        const template = document.getElementById('operation-card-template');
        const card = template.content.cloneNode(true).querySelector('.operation-card');
        
        card.dataset.operationId = operation.id;
        
        // Update header
        const typeBadge = card.querySelector('.operation-type-badge');
        typeBadge.textContent = operation.type;
        typeBadge.className = `operation-type-badge ${operation.type}`;
        
        const tableName = card.querySelector('.operation-table-name');
        tableName.textContent = operation.targetTable || '(no table selected)';
        
        // Update summary
        this.updateOperationSummary(card, operation);
        
        // Attach event listeners
        card.querySelector('.btn-edit-operation').addEventListener('click', () => 
            this.editOperation(operation.id));
        card.querySelector('.btn-move-up').addEventListener('click', () => 
            this.moveOperation(operation.id, -1));
        card.querySelector('.btn-move-down').addEventListener('click', () => 
            this.moveOperation(operation.id, 1));
        card.querySelector('.btn-delete-operation').addEventListener('click', () => 
            this.deleteOperation(operation.id));
        
        this.operationsList.appendChild(card);
    }

    updateOperationSummary(card, operation) {
        const summary = card.querySelector('.operation-summary');
        let text = '';
        
        switch (operation.type) {
            case 'INSERT':
                text = `Insert ${operation.columnMappings.length} columns`;
                break;
            case 'UPDATE':
                text = `Update ${operation.columnMappings.length} columns`;
                if (operation.whereConditions.length > 0) {
                    text += ` with ${operation.whereConditions.length} WHERE conditions`;
                }
                break;
            case 'DELETE':
                text = operation.whereConditions.length > 0 
                    ? `Delete with ${operation.whereConditions.length} WHERE conditions`
                    : 'Delete ALL rows (no WHERE clause)';
                break;
            case 'SELECT':
                text = `Select ${operation.selectColumns.join(', ')}`;
                if (operation.joins.length > 0) {
                    text += ` with ${operation.joins.length} JOINs`;
                }
                break;
        }
        
        summary.textContent = text;
    }

    async editOperation(operationId) {
        // Close any currently editing operation
        if (this.currentEditingId) {
            const prevCard = this.operationsList.querySelector(`[data-operation-id="${this.currentEditingId}"]`);
            if (prevCard) {
                prevCard.classList.remove('editing');
                prevCard.querySelector('.operation-form').classList.remove('active');
            }
        }

        const operation = this.operations.find(op => op.id === operationId);
        if (!operation) return;

        const card = this.operationsList.querySelector(`[data-operation-id="${operationId}"]`);
        if (!card) return;

        card.classList.add('editing');
        const form = card.querySelector('.operation-form');
        
        // Build form based on operation type
        form.innerHTML = await this.buildOperationForm(operation);
        form.classList.add('active');
        
        this.currentEditingId = operationId;
        
        // Attach form event listeners
        this.attachFormListeners(form, operation);
    }

    async buildOperationForm(operation) {
        switch (operation.type) {
            case 'INSERT':
                return await this.buildInsertForm(operation);
            case 'UPDATE':
                return await this.buildUpdateForm(operation);
            case 'DELETE':
                return await this.buildDeleteForm(operation);
            case 'SELECT':
                return await this.buildSelectForm(operation);
            default:
                return '<p>Unknown operation type</p>';
        }
    }

    async buildInsertForm(operation) {
        let html = '<h4>INSERT Configuration</h4>';
        
        // Table selector
        html += '<div class="form-group">';
        html += '<label class="form-label">Target Table *</label>';
        html += '<div id="insert-table-container"></div>';
        html += '</div>';
        
        // Column mappings
        html += '<div class="form-group">';
        html += '<label class="form-label">Column Mappings</label>';
        html += '<div id="insert-columns-container">Select a table first</div>';
        html += '</div>';
        
        // ON DUPLICATE KEY UPDATE
        html += '<div class="form-group">';
        html += '<div class="form-check">';
        html += `<input type="checkbox" id="insert-duplicate-key" class="form-check-input" ${operation.onDuplicateKeyUpdate.enabled ? 'checked' : ''}>`;
        html += '<label for="insert-duplicate-key" class="form-check-label">ON DUPLICATE KEY UPDATE</label>';
        html += '</div>';
        html += '<div id="insert-duplicate-container" style="display: none;"></div>';
        html += '</div>';
        
        html += '<button type="button" class="btn btn-primary btn-save-operation">Save Changes</button>';
        
        return html;
    }

    async buildUpdateForm(operation) {
        let html = '<h4>UPDATE Configuration</h4>';
        
        // Table selector
        html += '<div class="form-group">';
        html += '<label class="form-label">Target Table *</label>';
        html += '<div id="update-table-container"></div>';
        html += '</div>';
        
        // SET clause (column mappings)
        html += '<div class="form-group">';
        html += '<label class="form-label">SET Clause (Columns to Update)</label>';
        html += '<div id="update-set-container">Select a table first</div>';
        html += '</div>';
        
        // WHERE clause
        html += '<div class="form-group">';
        html += '<label class="form-label">WHERE Clause</label>';
        html += '<div id="update-where-container"></div>';
        html += '<button type="button" class="btn btn-sm btn-outline-secondary btn-add-where">+ Add Condition</button>';
        html += '</div>';
        
        // Track ROW_COUNT
        html += '<div class="form-group">';
        html += '<div class="form-check">';
        html += `<input type="checkbox" id="update-track-count" class="form-check-input" ${operation.trackRowCount ? 'checked' : ''}>`;
        html += '<label for="update-track-count" class="form-check-label">Track affected row count (SELECT ROW_COUNT() INTO v_RowsAffected)</label>';
        html += '</div>';
        html += '</div>';
        
        html += '<button type="button" class="btn btn-primary btn-save-operation">Save Changes</button>';
        
        return html;
    }

    async buildDeleteForm(operation) {
        let html = '<h4>DELETE Configuration</h4>';
        
        // Table selector
        html += '<div class="form-group">';
        html += '<label class="form-label">Target Table *</label>';
        html += '<div id="delete-table-container"></div>';
        html += '</div>';
        
        // WHERE clause
        html += '<div class="form-group">';
        html += '<label class="form-label">WHERE Clause</label>';
        html += '<div class="alert alert-warning">⚠️ DELETE without WHERE clause will remove ALL rows from the table!</div>';
        html += '<div id="delete-where-container"></div>';
        html += '<button type="button" class="btn btn-sm btn-outline-secondary btn-add-where">+ Add Condition</button>';
        html += '</div>';
        
        html += '<button type="button" class="btn btn-primary btn-save-operation">Save Changes</button>';
        
        return html;
    }

    async buildSelectForm(operation) {
        let html = '<h4>SELECT Configuration</h4>';
        
        // Table selector
        html += '<div class="form-group">';
        html += '<label class="form-label">Target Table *</label>';
        html += '<div id="select-table-container"></div>';
        html += '</div>';
        
        // Column selection
        html += '<div class="form-group">';
        html += '<label class="form-label">Columns to Select</label>';
        html += '<div id="select-columns-container">Select a table first</div>';
        html += '</div>';
        
        // Output variable (INTO clause)
        html += '<div class="form-group">';
        html += '<label class="form-label">Output Variable (optional)</label>';
        html += `<input type="text" id="select-output-var" class="form-control" value="${operation.outputVariable || ''}" placeholder="v_ResultVariable">`;
        html += '<span class="form-help">Store result in a variable (use v_ prefix)</span>';
        html += '</div>';
        
        // WHERE clause
        html += '<div class="form-group">';
        html += '<label class="form-label">WHERE Clause (optional)</label>';
        html += '<div id="select-where-container"></div>';
        html += '<button type="button" class="btn btn-sm btn-outline-secondary btn-add-where">+ Add Condition</button>';
        html += '</div>';
        
        // ORDER BY
        html += '<div class="form-group">';
        html += '<label class="form-label">ORDER BY (optional)</label>';
        html += '<div id="select-orderby-container"></div>';
        html += '<button type="button" class="btn btn-sm btn-outline-secondary btn-add-orderby">+ Add Sort</button>';
        html += '</div>';
        
        // LIMIT
        html += '<div class="form-group">';
        html += '<label class="form-label">LIMIT (optional)</label>';
        html += `<input type="number" id="select-limit" class="form-control" value="${operation.limit || ''}" placeholder="10" min="1">`;
        html += '</div>';
        
        html += '<button type="button" class="btn btn-primary btn-save-operation">Save Changes</button>';
        
        return html;
    }

    attachFormListeners(form, operation) {
        // Save button
        const btnSave = form.querySelector('.btn-save-operation');
        if (btnSave) {
            btnSave.addEventListener('click', () => this.saveOperation(operation.id));
        }
        
        // Table dropdowns
        const tableContainers = {
            'insert-table-container': () => this.handleTableSelect(operation, 'INSERT'),
            'update-table-container': () => this.handleTableSelect(operation, 'UPDATE'),
            'delete-table-container': () => this.handleTableSelect(operation, 'DELETE'),
            'select-table-container': () => this.handleTableSelect(operation, 'SELECT')
        };
        
        Object.entries(tableContainers).forEach(([containerId, handler]) => {
            const container = form.querySelector(`#${containerId}`);
            if (container) {
                const dropdown = createTableDropdown({
                    containerId: containerId,
                    selectedTable: operation.targetTable,
                    onChange: (tableName) => {
                        operation.targetTable = tableName;
                        handler();
                        this.debouncedUpdatePreview();
                    }
                });
                container.appendChild(dropdown);
            }
        });
        
        // Add WHERE condition buttons
        form.querySelectorAll('.btn-add-where').forEach(btn => {
            btn.addEventListener('click', () => this.addWhereCondition(operation));
        });
        
        // Add ORDER BY buttons
        form.querySelectorAll('.btn-add-orderby').forEach(btn => {
            btn.addEventListener('click', () => this.addOrderBy(operation));
        });
        
        // Trigger table select if table already chosen
        if (operation.targetTable) {
            this.handleTableSelect(operation, operation.type);
        }
    }

    async handleTableSelect(operation, type) {
        if (!operation.targetTable) return;
        
        // Update card header
        const card = this.operationsList.querySelector(`[data-operation-id="${operation.id}"]`);
        if (card) {
            card.querySelector('.operation-table-name').textContent = operation.targetTable;
        }
        
        // Load column checklist for INSERT/UPDATE/SELECT
        if (type === 'INSERT') {
            await this.loadInsertColumns(operation);
        } else if (type === 'UPDATE') {
            await this.loadUpdateColumns(operation);
        } else if (type === 'SELECT') {
            await this.loadSelectColumns(operation);
        }
    }

    async loadInsertColumns(operation) {
        const container = document.getElementById('insert-columns-container');
        if (!container) return;
        
        const checklist = await createColumnChecklist({
            tableName: operation.targetTable,
            selectedColumns: operation.columnMappings.map(cm => cm.columnName),
            disableAutoIncrement: true,
            onChange: (selectedColumns) => {
                // Update column mappings
                operation.columnMappings = selectedColumns.map(col => {
                    // Check if mapping already exists
                    const existing = operation.columnMappings.find(cm => cm.columnName === col);
                    if (existing) return existing;
                    
                    // Create new mapping with smart default
                    return new ColumnMapping({
                        columnName: col,
                        value: this.getSmartDefaultSync(operation.targetTable, col)
                    });
                });
                
                this.debouncedUpdatePreview();
            }
        });
        
        container.innerHTML = '';
        container.appendChild(checklist);
    }

    async loadUpdateColumns(operation) {
        const container = document.getElementById('update-set-container');
        if (!container) return;
        
        const checklist = await createColumnChecklist({
            tableName: operation.targetTable,
            selectedColumns: operation.columnMappings.map(cm => cm.columnName),
            disableAutoIncrement: true,
            onChange: (selectedColumns) => {
                operation.columnMappings = selectedColumns.map(col => {
                    const existing = operation.columnMappings.find(cm => cm.columnName === col);
                    if (existing) return existing;
                    
                    return new ColumnMapping({
                        columnName: col,
                        value: this.getSmartDefaultSync(operation.targetTable, col)
                    });
                });
                
                this.debouncedUpdatePreview();
            }
        });
        
        container.innerHTML = '';
        container.appendChild(checklist);
    }

    async loadSelectColumns(operation) {
        const container = document.getElementById('select-columns-container');
        if (!container) return;
        
        const checklist = await createColumnChecklist({
            tableName: operation.targetTable,
            selectedColumns: operation.selectColumns.includes('*') ? [] : operation.selectColumns,
            disableAutoIncrement: false,
            showTypes: true,
            onChange: (selectedColumns) => {
                operation.selectColumns = selectedColumns.length > 0 ? selectedColumns : ['*'];
                this.debouncedUpdatePreview();
            }
        });
        
        container.innerHTML = '';
        container.appendChild(checklist);
    }

    async getSmartDefault(tableName, columnName) {
        const columns = await dbMetadata.getColumns(tableName);
        const column = columns.find(c => c.name === columnName);
        
        if (!column) return '';
        
        // Smart defaults based on column name patterns
        const lowerName = columnName.toLowerCase();
        
        if (lowerName.includes('date') && lowerName.includes('created')) {
            return 'NOW()';
        }
        if (lowerName.includes('date') && lowerName.includes('updated')) {
            return 'NOW()';
        }
        if (lowerName.includes('user') && lowerName.includes('created')) {
            return 'p_UserID';
        }
        if (lowerName.includes('user') && lowerName.includes('updated')) {
            return 'p_UserID';
        }
        if (lowerName === 'isactive' || lowerName === 'active') {
            return '1';
        }
        
        // Default to parameter name matching column
        return `p_${columnName}`;
    }

    getSmartDefaultSync(tableName, columnName) {
        // Synchronous version for use in non-async contexts
        const lowerName = columnName.toLowerCase();
        
        if (lowerName.includes('date') && lowerName.includes('created')) {
            return 'NOW()';
        }
        if (lowerName.includes('date') && lowerName.includes('updated')) {
            return 'NOW()';
        }
        if (lowerName.includes('user') && lowerName.includes('created')) {
            return 'p_UserID';
        }
        if (lowerName.includes('user') && lowerName.includes('updated')) {
            return 'p_UserID';
        }
        if (lowerName === 'isactive' || lowerName === 'active') {
            return '1';
        }
        
        // Default to parameter name matching column
        return `p_${columnName}`;
    }

    addWhereCondition(operation) {
        const condition = new WhereCondition();
        operation.whereConditions.push(condition);
        
        // Re-render form to show new condition
        this.editOperation(operation.id);
    }

    addOrderBy(operation) {
        const orderBy = new OrderByClause();
        operation.orderBy.push(orderBy);
        
        this.editOperation(operation.id);
    }

    saveOperation(operationId) {
        const card = this.operationsList.querySelector(`[data-operation-id="${operationId}"]`);
        if (!card) return;
        
        card.classList.remove('editing');
        card.querySelector('.operation-form').classList.remove('active');
        
        const operation = this.operations.find(op => op.id === operationId);
        if (operation) {
            this.updateOperationSummary(card, operation);
        }
        
        this.currentEditingId = null;
        this.updatePreview();
        
        showSuccess('Operation saved');
    }

    moveOperation(operationId, direction) {
        const index = this.operations.findIndex(op => op.id === operationId);
        if (index === -1) return;
        
        const newIndex = index + direction;
        if (newIndex < 0 || newIndex >= this.operations.length) return;
        
        // Swap operations
        [this.operations[index], this.operations[newIndex]] = [this.operations[newIndex], this.operations[index]];
        
        // Update order property
        this.operations.forEach((op, idx) => op.order = idx);
        
        // Re-render
        this.renderAllOperations();
        this.updatePreview();
    }

    deleteOperation(operationId) {
        if (!confirm('Are you sure you want to delete this operation?')) {
            return;
        }
        
        this.operations = this.operations.filter(op => op.id !== operationId);
        
        // Re-render
        this.renderAllOperations();
        this.updatePreview();
        
        showSuccess('Operation deleted');
    }

    renderAllOperations() {
        this.operationsList.innerHTML = '';
        
        if (this.operations.length === 0) {
            this.operationsList.innerHTML = '<div class="alert alert-info" id="no-operations-message">No operations added yet.</div>';
            this.noOperationsMessage = document.getElementById('no-operations-message');
            return;
        }
        
        this.operations.forEach(op => this.renderOperation(op));
    }

    updatePreview() {
        if (this.operations.length === 0) {
            this.sqlPreview.textContent = '-- Add operations to see SQL preview';
            return;
        }
        
        let sql = '-- Generated DML Operations\n\n';
        
        this.operations.forEach((op, index) => {
            sql += `-- Operation ${index + 1}: ${op.type} ${op.targetTable || '(no table)'}\n`;
            sql += op.toSQL();
            sql += ';\n\n';
        });
        
        this.sqlPreview.textContent = sql;
    }

    async copySQL() {
        const sql = this.sqlPreview.textContent;
        
        try {
            await navigator.clipboard.writeText(sql);
            showSuccess('SQL copied to clipboard');
        } catch (error) {
            showError({
                error_type: 'browser',
                user_message: 'Failed to copy SQL',
                technical_detail: error.message
            });
        }
    }
}

// Initialize controller when DOM is ready
document.addEventListener('DOMContentLoaded', () => {
    window.dmlController = new DMLOperationsController();
});
