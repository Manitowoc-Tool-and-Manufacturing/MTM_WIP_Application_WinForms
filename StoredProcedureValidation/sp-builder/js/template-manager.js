/**
 * Template Manager for Stored Procedure Builder
 * 
 * Manages loading, applying, and saving procedure templates.
 * Supports built-in templates and custom user templates.
 * 
 * @module template-manager
 */

import { Template, TEMPLATE_CATEGORIES } from './procedure-model.js';
import { dbMetadata } from './database-metadata.js';
import { showSuccess, showError } from './utils.js';

/**
 * Template Manager Class
 * Handles template library and customization
 */
export class TemplateManager {
    constructor() {
        this.builtInTemplates = [];
        this.customTemplates = [];
        this.allTemplates = [];
    }

    /**
     * Initialize template manager and load templates
     */
    async initialize() {
        try {
            await this.loadBuiltInTemplates();
            await this.loadCustomTemplates();
            this.allTemplates = [...this.builtInTemplates, ...this.customTemplates];
            return true;
        } catch (error) {
            console.error('Error initializing templates:', error);
            return false;
        }
    }

    /**
     * Load built-in templates from embedded data
     * (In a real app, would load from JSON files)
     */
    async loadBuiltInTemplates() {
        // For now, create templates programmatically
        // In production, these would be loaded from JSON files
        
        this.builtInTemplates = [
            this._createCRUDAddTemplate(),
            this._createCRUDUpdateTemplate(),
            this._createCRUDDeleteTemplate(),
            this._createCRUDGetTemplate(),
            this._createBatchInsertTemplate(),
            this._createBatchUpdateTemplate(),
            this._createTransferTemplate(),
            this._createAuditLogTemplate()
        ];
    }

    /**
     * Load custom templates from localStorage
     */
    async loadCustomTemplates() {
        try {
            const stored = localStorage.getItem('sp_builder_custom_templates');
            if (stored) {
                const data = JSON.parse(stored);
                this.customTemplates = data.map(t => Template.fromJSON(t));
            }
        } catch (error) {
            console.error('Error loading custom templates:', error);
            this.customTemplates = [];
        }
    }

    /**
     * Get templates by category
     * @param {string} category - Category name
     * @returns {Array<Template>} Templates in category
     */
    getTemplatesByCategory(category) {
        return this.allTemplates.filter(t => t.category === category);
    }

    /**
     * Get all categories with template counts
     * @returns {Array<Object>} Categories with counts
     */
    getCategories() {
        const counts = {};
        for (const template of this.allTemplates) {
            counts[template.category] = (counts[template.category] || 0) + 1;
        }
        
        return TEMPLATE_CATEGORIES.map(cat => ({
            name: cat,
            count: counts[cat] || 0
        }));
    }

    /**
     * Get template by ID
     * @param {string} id - Template ID
     * @returns {Template|null} Template or null
     */
    getTemplateById(id) {
        return this.allTemplates.find(t => t.id === id) || null;
    }

    /**
     * Apply template with substitutions
     * @param {string} templateId - Template ID
     * @param {Object} values - Substitution values
     * @returns {ProcedureDefinition|null} Generated procedure
     */
    applyTemplate(templateId, values) {
        const template = this.getTemplateById(templateId);
        if (!template) {
            showError({
                error_type: 'template',
                user_message: 'Template not found',
                technical_detail: `Template ID: ${templateId}`
            });
            return null;
        }
        
        // Validate customizations
        const validation = template.validateCustomizations(values);
        if (!validation.valid) {
            showError({
                error_type: 'validation',
                user_message: 'Missing required values',
                technical_detail: `Missing: ${validation.missing.join(', ')}`
            });
            return null;
        }
        
        // Apply template
        try {
            const procedure = template.apply(values);
            
            // Increment usage count
            template.usageCount++;
            this._saveTemplateUsage(template);
            
            showSuccess(`Template "${template.name}" applied successfully`);
            return procedure;
            
        } catch (error) {
            showError({
                error_type: 'template',
                user_message: 'Failed to apply template',
                technical_detail: error.message
            });
            return null;
        }
    }

    /**
     * Save usage count update
     * @param {Template} template - Template to update
     */
    _saveTemplateUsage(template) {
        if (!template.isBuiltIn) {
            this.saveCustomTemplates();
        }
    }

    /**
     * Save current procedure as custom template
     * @param {ProcedureDefinition} procedure - Procedure to save
     * @param {Object} templateInfo - Template metadata
     * @returns {Template} Created template
     */
    saveAsTemplate(procedure, templateInfo) {
        try {
            // Convert procedure to template structure
            const template = new Template({
                name: templateInfo.name || procedure.name + ' Template',
                description: templateInfo.description || 'Custom template',
                category: templateInfo.category || 'CUSTOM',
                author: procedure.author,
                procedureTemplate: procedure.toJSON(),
                customizationPoints: this._extractCustomizationPoints(procedure),
                isBuiltIn: false
            });
            
            // Add to custom templates
            this.customTemplates.push(template);
            this.allTemplates.push(template);
            
            // Save to localStorage
            this.saveCustomTemplates();
            
            showSuccess(`Template "${template.name}" saved successfully`);
            return template;
            
        } catch (error) {
            showError({
                error_type: 'template',
                user_message: 'Failed to save template',
                technical_detail: error.message
            });
            return null;
        }
    }

    /**
     * Extract customization points from procedure
     * @param {ProcedureDefinition} procedure - Procedure
     * @returns {Array<Object>} Customization points
     */
    _extractCustomizationPoints(procedure) {
        // Default customization points
        return [
            { key: 'PROCEDURE_NAME', label: 'Procedure Name', type: 'text', required: true },
            { key: 'DESCRIPTION', label: 'Description', type: 'text', required: false }
        ];
    }

    /**
     * Save custom templates to localStorage
     */
    saveCustomTemplates() {
        try {
            const data = this.customTemplates.map(t => t.toJSON());
            localStorage.setItem('sp_builder_custom_templates', JSON.stringify(data));
        } catch (error) {
            console.error('Error saving custom templates:', error);
        }
    }

    /**
     * Delete custom template
     * @param {string} templateId - Template ID
     * @returns {boolean} Success
     */
    deleteTemplate(templateId) {
        const template = this.getTemplateById(templateId);
        
        if (!template) {
            return false;
        }
        
        if (template.isBuiltIn) {
            showError({
                error_type: 'template',
                user_message: 'Cannot delete built-in template',
                technical_detail: ''
            });
            return false;
        }
        
        // Remove from arrays
        this.customTemplates = this.customTemplates.filter(t => t.id !== templateId);
        this.allTemplates = this.allTemplates.filter(t => t.id !== templateId);
        
        // Save changes
        this.saveCustomTemplates();
        
        showSuccess('Template deleted successfully');
        return true;
    }

    /**
     * Validate template with database metadata
     * @param {Template} template - Template to validate
     * @param {Object} values - Substitution values
     * @returns {Object} Validation result {valid, warnings, suggestions}
     */
    validateWithMetadata(template, values) {
        const warnings = [];
        const suggestions = {};
        
        // Check if referenced table exists
        if (values.TABLE_NAME) {
            const tableExists = dbMetadata.tableExists(values.TABLE_NAME);
            
            if (!tableExists) {
                warnings.push(`Table "${values.TABLE_NAME}" not found in database`);
                
                // Find similar table names (fuzzy matching)
                const similarTables = this._findSimilarTables(values.TABLE_NAME);
                if (similarTables.length > 0) {
                    suggestions.TABLE_NAME = similarTables;
                }
            }
        }
        
        // Check if referenced columns exist
        const operations = template.procedureTemplate.operations || [];
        for (const op of operations) {
            if (op.targetTable && values[op.targetTable]) {
                const tableName = values[op.targetTable];
                const columns = op.columnMappings || [];
                
                for (const col of columns) {
                    if (col.targetColumn && !dbMetadata.columnExists(tableName, col.targetColumn)) {
                        warnings.push(`Column "${col.targetColumn}" not found in table "${tableName}"`);
                    }
                }
            }
        }
        
        return {
            valid: warnings.length === 0,
            warnings: warnings,
            suggestions: suggestions
        };
    }

    /**
     * Find similar table names using fuzzy matching
     * @param {string} tableName - Table name to match
     * @returns {Array<string>} Similar table names
     */
    _findSimilarTables(tableName) {
        const allTables = dbMetadata.getAllTables();
        const similar = [];
        
        const lowerSearch = tableName.toLowerCase();
        
        for (const table of allTables) {
            const lowerTable = table.name.toLowerCase();
            
            // Simple similarity: contains substring or Levenshtein distance
            if (lowerTable.includes(lowerSearch) || lowerSearch.includes(lowerTable)) {
                similar.push(table.name);
            } else if (this._levenshteinDistance(lowerSearch, lowerTable) <= 3) {
                similar.push(table.name);
            }
        }
        
        return similar.slice(0, 5); // Return top 5
    }

    /**
     * Calculate Levenshtein distance between two strings
     * @param {string} a - First string
     * @param {string} b - Second string
     * @returns {number} Edit distance
     */
    _levenshteinDistance(a, b) {
        const matrix = [];
        
        for (let i = 0; i <= b.length; i++) {
            matrix[i] = [i];
        }
        
        for (let j = 0; j <= a.length; j++) {
            matrix[0][j] = j;
        }
        
        for (let i = 1; i <= b.length; i++) {
            for (let j = 1; j <= a.length; j++) {
                if (b.charAt(i - 1) === a.charAt(j - 1)) {
                    matrix[i][j] = matrix[i - 1][j - 1];
                } else {
                    matrix[i][j] = Math.min(
                        matrix[i - 1][j - 1] + 1, // substitution
                        matrix[i][j - 1] + 1,     // insertion
                        matrix[i - 1][j] + 1      // deletion
                    );
                }
            }
        }
        
        return matrix[b.length][a.length];
    }

    // ========================================================================
    // BUILT-IN TEMPLATE CREATORS
    // ========================================================================

    /**
     * Create CRUD Add template
     */
    _createCRUDAddTemplate() {
        return new Template({
            id: 'tmpl_crud_add',
            name: 'CRUD: Add Record',
            description: 'Insert a new record into a table with validation',
            category: 'CRUD',
            author: 'System',
            isBuiltIn: true,
            tags: ['crud', 'insert', 'add'],
            customizationPoints: [
                { key: 'DOMAIN', label: 'Domain Prefix (e.g., inv, usr)', type: 'text', required: true },
                { key: 'TABLE_NAME', label: 'Table Name', type: 'text', required: true },
                { key: 'ENTITY', label: 'Entity Name (singular)', type: 'text', required: true }
            ],
            procedureTemplate: {
                name: '{{DOMAIN}}_{{TABLE_NAME}}_Add_{{ENTITY}}',
                description: 'Add new {{ENTITY}} record to {{TABLE_NAME}} table',
                parameters: [
                    { name: 'p_Name', direction: 'IN', dataType: 'VARCHAR', length: 100 },
                    { name: 'p_Status', direction: 'OUT', dataType: 'INT' },
                    { name: 'p_ErrorMsg', direction: 'OUT', dataType: 'VARCHAR', length: 500 }
                ],
                validations: [
                    {
                        type: 'REQUIRED_FIELD',
                        parameterName: 'p_Name',
                        errorMessage: '{{ENTITY}} name is required',
                        errorStatusCode: -1,
                        order: 0
                    }
                ],
                operations: [
                    {
                        type: 'INSERT',
                        targetTable: '{{TABLE_NAME}}',
                        columnMappings: [
                            { targetColumn: 'Name', sourceExpression: 'p_Name' },
                            { targetColumn: 'CreatedDate', sourceExpression: 'NOW()' }
                        ]
                    }
                ]
            }
        });
    }

    /**
     * Create CRUD Update template
     */
    _createCRUDUpdateTemplate() {
        return new Template({
            id: 'tmpl_crud_update',
            name: 'CRUD: Update Record',
            description: 'Update an existing record with validation',
            category: 'CRUD',
            author: 'System',
            isBuiltIn: true,
            tags: ['crud', 'update', 'modify'],
            customizationPoints: [
                { key: 'DOMAIN', label: 'Domain Prefix', type: 'text', required: true },
                { key: 'TABLE_NAME', label: 'Table Name', type: 'text', required: true },
                { key: 'ENTITY', label: 'Entity Name', type: 'text', required: true }
            ],
            procedureTemplate: {
                name: '{{DOMAIN}}_{{TABLE_NAME}}_Update_{{ENTITY}}',
                description: 'Update existing {{ENTITY}} record in {{TABLE_NAME}} table',
                parameters: [
                    { name: 'p_ID', direction: 'IN', dataType: 'INT' },
                    { name: 'p_Name', direction: 'IN', dataType: 'VARCHAR', length: 100 },
                    { name: 'p_Status', direction: 'OUT', dataType: 'INT' },
                    { name: 'p_ErrorMsg', direction: 'OUT', dataType: 'VARCHAR', length: 500 }
                ],
                validations: [
                    {
                        type: 'POSITIVE_NUMBER',
                        parameterName: 'p_ID',
                        errorMessage: '{{ENTITY}} ID must be positive',
                        errorStatusCode: -1,
                        order: 0
                    }
                ],
                operations: [
                    {
                        type: 'UPDATE',
                        targetTable: '{{TABLE_NAME}}',
                        columnMappings: [
                            { targetColumn: 'Name', sourceExpression: 'p_Name' },
                            { targetColumn: 'ModifiedDate', sourceExpression: 'NOW()' }
                        ],
                        whereConditions: [
                            { column: 'ID', operator: '=', value: 'p_ID' }
                        ]
                    }
                ]
            }
        });
    }

    /**
     * Create CRUD Delete template
     */
    _createCRUDDeleteTemplate() {
        return new Template({
            id: 'tmpl_crud_delete',
            name: 'CRUD: Delete Record',
            description: 'Delete a record with safety checks',
            category: 'CRUD',
            author: 'System',
            isBuiltIn: true,
            tags: ['crud', 'delete', 'remove'],
            customizationPoints: [
                { key: 'DOMAIN', label: 'Domain Prefix', type: 'text', required: true },
                { key: 'TABLE_NAME', label: 'Table Name', type: 'text', required: true },
                { key: 'ENTITY', label: 'Entity Name', type: 'text', required: true }
            ],
            procedureTemplate: {
                name: '{{DOMAIN}}_{{TABLE_NAME}}_Delete_{{ENTITY}}',
                description: 'Delete {{ENTITY}} record from {{TABLE_NAME}} table',
                parameters: [
                    { name: 'p_ID', direction: 'IN', dataType: 'INT' },
                    { name: 'p_Status', direction: 'OUT', dataType: 'INT' },
                    { name: 'p_ErrorMsg', direction: 'OUT', dataType: 'VARCHAR', length: 500 }
                ],
                validations: [
                    {
                        type: 'POSITIVE_NUMBER',
                        parameterName: 'p_ID',
                        errorMessage: '{{ENTITY}} ID must be positive',
                        errorStatusCode: -1,
                        order: 0
                    }
                ],
                operations: [
                    {
                        type: 'DELETE',
                        targetTable: '{{TABLE_NAME}}',
                        whereConditions: [
                            { column: 'ID', operator: '=', value: 'p_ID' }
                        ]
                    }
                ]
            }
        });
    }

    /**
     * Create CRUD Get template
     */
    _createCRUDGetTemplate() {
        return new Template({
            id: 'tmpl_crud_get',
            name: 'CRUD: Get Record',
            description: 'Retrieve a single record by ID',
            category: 'CRUD',
            author: 'System',
            isBuiltIn: true,
            tags: ['crud', 'select', 'get', 'read'],
            customizationPoints: [
                { key: 'DOMAIN', label: 'Domain Prefix', type: 'text', required: true },
                { key: 'TABLE_NAME', label: 'Table Name', type: 'text', required: true },
                { key: 'ENTITY', label: 'Entity Name', type: 'text', required: true }
            ],
            procedureTemplate: {
                name: '{{DOMAIN}}_{{TABLE_NAME}}_Get_{{ENTITY}}',
                description: 'Get {{ENTITY}} record from {{TABLE_NAME}} table by ID',
                parameters: [
                    { name: 'p_ID', direction: 'IN', dataType: 'INT' },
                    { name: 'p_Status', direction: 'OUT', dataType: 'INT' },
                    { name: 'p_ErrorMsg', direction: 'OUT', dataType: 'VARCHAR', length: 500 }
                ],
                validations: [
                    {
                        type: 'POSITIVE_NUMBER',
                        parameterName: 'p_ID',
                        errorMessage: '{{ENTITY}} ID must be positive',
                        errorStatusCode: -1,
                        order: 0
                    }
                ],
                operations: [
                    {
                        type: 'SELECT',
                        targetTable: '{{TABLE_NAME}}',
                        selectColumns: ['*'],
                        whereConditions: [
                            { column: 'ID', operator: '=', value: 'p_ID' }
                        ],
                        limit: 1
                    }
                ]
            }
        });
    }

    /**
     * Create Batch Insert template
     */
    _createBatchInsertTemplate() {
        return new Template({
            id: 'tmpl_batch_insert',
            name: 'Batch: Insert Multiple Records',
            description: 'Insert multiple records in a single transaction',
            category: 'BATCH',
            author: 'System',
            isBuiltIn: true,
            tags: ['batch', 'insert', 'bulk'],
            customizationPoints: [
                { key: 'DOMAIN', label: 'Domain Prefix', type: 'text', required: true },
                { key: 'TABLE_NAME', label: 'Table Name', type: 'text', required: true },
                { key: 'ENTITY', label: 'Entity Name (plural)', type: 'text', required: true }
            ],
            procedureTemplate: {
                name: '{{DOMAIN}}_{{TABLE_NAME}}_Batch_Add_{{ENTITY}}',
                description: 'Add multiple {{ENTITY}} records to {{TABLE_NAME}} table',
                parameters: [
                    { name: 'p_JSON', direction: 'IN', dataType: 'TEXT' },
                    { name: 'p_Status', direction: 'OUT', dataType: 'INT' },
                    { name: 'p_ErrorMsg', direction: 'OUT', dataType: 'VARCHAR', length: 500 }
                ],
                validations: [
                    {
                        type: 'REQUIRED_FIELD',
                        parameterName: 'p_JSON',
                        errorMessage: 'JSON data is required',
                        errorStatusCode: -1,
                        order: 0
                    }
                ],
                operations: []
            }
        });
    }

    /**
     * Create Batch Update template
     */
    _createBatchUpdateTemplate() {
        return new Template({
            id: 'tmpl_batch_update',
            name: 'Batch: Update Multiple Records',
            description: 'Update multiple records based on criteria',
            category: 'BATCH',
            author: 'System',
            isBuiltIn: true,
            tags: ['batch', 'update', 'bulk'],
            customizationPoints: [
                { key: 'DOMAIN', label: 'Domain Prefix', type: 'text', required: true },
                { key: 'TABLE_NAME', label: 'Table Name', type: 'text', required: true },
                { key: 'ENTITY', label: 'Entity Name (plural)', type: 'text', required: true }
            ],
            procedureTemplate: {
                name: '{{DOMAIN}}_{{TABLE_NAME}}_Batch_Update_{{ENTITY}}',
                description: 'Update multiple {{ENTITY}} records in {{TABLE_NAME}} table',
                parameters: [
                    { name: 'p_Status', direction: 'IN', dataType: 'VARCHAR', length: 50 },
                    { name: 'p_Status', direction: 'OUT', dataType: 'INT' },
                    { name: 'p_ErrorMsg', direction: 'OUT', dataType: 'VARCHAR', length: 500 }
                ],
                validations: [],
                operations: [
                    {
                        type: 'UPDATE',
                        targetTable: '{{TABLE_NAME}}',
                        columnMappings: [
                            { targetColumn: 'Status', sourceExpression: 'p_Status' },
                            { targetColumn: 'ModifiedDate', sourceExpression: 'NOW()' }
                        ],
                        whereConditions: []
                    }
                ]
            }
        });
    }

    /**
     * Create Transfer template
     */
    _createTransferTemplate() {
        return new Template({
            id: 'tmpl_transfer',
            name: 'Transfer: Move Records Between Tables',
            description: 'Transfer records from one location/status to another',
            category: 'TRANSFER',
            author: 'System',
            isBuiltIn: true,
            tags: ['transfer', 'move', 'workflow'],
            customizationPoints: [
                { key: 'DOMAIN', label: 'Domain Prefix', type: 'text', required: true },
                { key: 'TABLE_NAME', label: 'Table Name', type: 'text', required: true },
                { key: 'ENTITY', label: 'Entity Name', type: 'text', required: true }
            ],
            procedureTemplate: {
                name: '{{DOMAIN}}_{{TABLE_NAME}}_Transfer_{{ENTITY}}',
                description: 'Transfer {{ENTITY}} between locations/statuses',
                parameters: [
                    { name: 'p_ID', direction: 'IN', dataType: 'INT' },
                    { name: 'p_FromLocation', direction: 'IN', dataType: 'VARCHAR', length: 50 },
                    { name: 'p_ToLocation', direction: 'IN', dataType: 'VARCHAR', length: 50 },
                    { name: 'p_Status', direction: 'OUT', dataType: 'INT' },
                    { name: 'p_ErrorMsg', direction: 'OUT', dataType: 'VARCHAR', length: 500 }
                ],
                validations: [
                    {
                        type: 'POSITIVE_NUMBER',
                        parameterName: 'p_ID',
                        errorMessage: '{{ENTITY}} ID must be positive',
                        errorStatusCode: -1,
                        order: 0
                    }
                ],
                operations: [
                    {
                        type: 'UPDATE',
                        targetTable: '{{TABLE_NAME}}',
                        columnMappings: [
                            { targetColumn: 'Location', sourceExpression: 'p_ToLocation' },
                            { targetColumn: 'ModifiedDate', sourceExpression: 'NOW()' }
                        ],
                        whereConditions: [
                            { column: 'ID', operator: '=', value: 'p_ID' },
                            { column: 'Location', operator: '=', value: 'p_FromLocation' }
                        ]
                    }
                ]
            }
        });
    }

    /**
     * Create Audit Log template
     */
    _createAuditLogTemplate() {
        return new Template({
            id: 'tmpl_audit_log',
            name: 'Audit: Log Activity',
            description: 'Insert audit trail record for user activity',
            category: 'AUDIT',
            author: 'System',
            isBuiltIn: true,
            tags: ['audit', 'log', 'history'],
            customizationPoints: [
                { key: 'DOMAIN', label: 'Domain Prefix', type: 'text', required: true },
                { key: 'ENTITY', label: 'Entity Name', type: 'text', required: true }
            ],
            procedureTemplate: {
                name: '{{DOMAIN}}_Audit_Log_{{ENTITY}}_Activity',
                description: 'Log {{ENTITY}} activity to audit trail',
                parameters: [
                    { name: 'p_UserID', direction: 'IN', dataType: 'INT' },
                    { name: 'p_Action', direction: 'IN', dataType: 'VARCHAR', length: 50 },
                    { name: 'p_EntityID', direction: 'IN', dataType: 'INT' },
                    { name: 'p_Details', direction: 'IN', dataType: 'TEXT' },
                    { name: 'p_Status', direction: 'OUT', dataType: 'INT' },
                    { name: 'p_ErrorMsg', direction: 'OUT', dataType: 'VARCHAR', length: 500 }
                ],
                validations: [
                    {
                        type: 'POSITIVE_NUMBER',
                        parameterName: 'p_UserID',
                        errorMessage: 'User ID is required',
                        errorStatusCode: -1,
                        order: 0
                    },
                    {
                        type: 'REQUIRED_FIELD',
                        parameterName: 'p_Action',
                        errorMessage: 'Action is required',
                        errorStatusCode: -2,
                        order: 1
                    }
                ],
                operations: [
                    {
                        type: 'INSERT',
                        targetTable: 'AuditLog',
                        columnMappings: [
                            { targetColumn: 'UserID', sourceExpression: 'p_UserID' },
                            { targetColumn: 'Action', sourceExpression: 'p_Action' },
                            { targetColumn: 'EntityType', sourceExpression: "'{{ENTITY}}'" },
                            { targetColumn: 'EntityID', sourceExpression: 'p_EntityID' },
                            { targetColumn: 'Details', sourceExpression: 'p_Details' },
                            { targetColumn: 'CreatedDate', sourceExpression: 'NOW()' }
                        ]
                    }
                ]
            }
        });
    }
}

// Export singleton instance
export const templateManager = new TemplateManager();
