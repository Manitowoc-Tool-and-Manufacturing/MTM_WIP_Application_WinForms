/**
 * Data Model Classes for MySQL 5.7 Stored Procedure Builder
 * 
 * This module defines the core data structures for representing stored procedures,
 * parameters, validation rules, DML operations, and related entities.
 */

/**
 * Represents a parameter for a stored procedure
 */
export class Parameter {
    constructor(config = {}) {
        this.name = this._ensurePPrefix(config.name || '');
        this.direction = config.direction || 'IN'; // IN, OUT, INOUT
        this.dataType = config.dataType || 'VARCHAR';
        this.length = config.length || null;
        this.precision = config.precision || null;
        this.scale = config.scale || null;
        this.description = config.description || '';
        this.defaultValue = config.defaultValue || null;
        this.usedInValidations = config.usedInValidations || [];
        this.usedInOperations = config.usedInOperations || [];
        this.order = config.order || 0;
    }

    /**
     * Ensures parameter name starts with p_ prefix
     * @param {string} name - Parameter name
     * @returns {string} Name with p_ prefix
     */
    _ensurePPrefix(name) {
        if (!name) return '';
        const trimmed = name.trim();
        return trimmed.startsWith('p_') ? trimmed : `p_${trimmed}`;
    }

    /**
     * Generates MySQL type declaration (e.g., "VARCHAR(50)", "DECIMAL(10,2)")
     * @returns {string} Type declaration
     */
    getTypeDeclaration() {
        switch (this.dataType.toUpperCase()) {
            case 'VARCHAR':
            case 'CHAR':
                return `${this.dataType.toUpperCase()}(${this.length || 50})`;
            
            case 'DECIMAL':
            case 'NUMERIC':
                return `${this.dataType.toUpperCase()}(${this.precision || 10},${this.scale || 2})`;
            
            case 'INT':
            case 'INTEGER':
            case 'BIGINT':
            case 'SMALLINT':
            case 'TINYINT':
            case 'BOOLEAN':
            case 'BOOL':
            case 'DATE':
            case 'DATETIME':
            case 'TIMESTAMP':
            case 'TIME':
            case 'TEXT':
            case 'LONGTEXT':
            case 'MEDIUMTEXT':
                return this.dataType.toUpperCase();
            
            default:
                return this.dataType.toUpperCase();
        }
    }

    /**
     * Checks if parameter is required (IN parameter with no default)
     * @returns {boolean}
     */
    isRequired() {
        return this.direction === 'IN' && this.defaultValue === null;
    }

    /**
     * Validates parameter configuration
     * @returns {string[]} Array of error messages (empty if valid)
     */
    validate() {
        const errors = [];

        if (!this.name) {
            errors.push('Parameter name is required');
        } else if (!this.name.startsWith('p_')) {
            errors.push('Parameter name must start with p_ prefix');
        }

        if (!['IN', 'OUT', 'INOUT'].includes(this.direction)) {
            errors.push('Parameter direction must be IN, OUT, or INOUT');
        }

        if (!this.dataType) {
            errors.push('Data type is required');
        }

        if (['VARCHAR', 'CHAR'].includes(this.dataType.toUpperCase()) && !this.length) {
            errors.push(`${this.dataType} requires length specification`);
        }

        if (['DECIMAL', 'NUMERIC'].includes(this.dataType.toUpperCase())) {
            if (!this.precision) {
                errors.push(`${this.dataType} requires precision specification`);
            }
            if (this.scale === null || this.scale === undefined) {
                errors.push(`${this.dataType} requires scale specification`);
            }
        }

        return errors;
    }

    /**
     * Serializes parameter to JSON
     * @returns {object}
     */
    toJSON() {
        return {
            name: this.name,
            direction: this.direction,
            dataType: this.dataType,
            length: this.length,
            precision: this.precision,
            scale: this.scale,
            description: this.description,
            defaultValue: this.defaultValue,
            usedInValidations: this.usedInValidations,
            usedInOperations: this.usedInOperations,
            order: this.order
        };
    }

    /**
     * Creates Parameter instance from JSON
     * @param {object} data - JSON data
     * @returns {Parameter}
     */
    static fromJSON(data) {
        return new Parameter(data);
    }
}

/**
 * Represents a complete stored procedure definition
 */
export class ProcedureDefinition {
    constructor(config = {}) {
        // Identity
        this.name = config.name || '';
        this.description = config.description || '';
        this.version = config.version || 1;

        // Metadata
        this.author = config.author || '';
        this.createdDate = config.createdDate ? new Date(config.createdDate) : new Date();
        this.modifiedDate = config.modifiedDate ? new Date(config.modifiedDate) : new Date();

        // Components (ordered collections)
        this.parameters = this._initializeParameters(config.parameters);
        this.localVariables = config.localVariables || [];
        this.validations = config.validations || [];
        this.operations = config.operations || [];
        this.advancedFeatures = config.advancedFeatures || {
            loops: [],
            cursors: [],
            nestedCalls: []
        };

        // Settings
        this.transactionControl = config.transactionControl || {
            enabled: true,
            isolationLevel: 'DEFAULT'
        };

        // Wizard state
        this.currentStep = config.currentStep || 1;
        this.completedSteps = config.completedSteps || [];

        // Flow diagram
        this.flowDiagram = config.flowDiagram || {
            nodes: [],
            connections: []
        };
    }

    /**
     * Initializes parameters array with mandatory p_Status and p_ErrorMsg
     * @param {array} params - User-provided parameters
     * @returns {array} Parameters with mandatory ones included
     */
    _initializeParameters(params = []) {
        const userParams = params.map(p => p instanceof Parameter ? p : new Parameter(p));
        
        // Check if mandatory parameters already exist
        const hasStatus = userParams.some(p => p.name === 'p_Status');
        const hasErrorMsg = userParams.some(p => p.name === 'p_ErrorMsg');

        const mandatory = [];
        
        if (!hasStatus) {
            mandatory.push(new Parameter({
                name: 'p_Status',
                direction: 'OUT',
                dataType: 'INT',
                description: 'Status code: 0=success, <0=error',
                order: 9999
            }));
        }

        if (!hasErrorMsg) {
            mandatory.push(new Parameter({
                name: 'p_ErrorMsg',
                direction: 'OUT',
                dataType: 'VARCHAR',
                length: 500,
                description: 'Error message if status < 0',
                order: 10000
            }));
        }

        return [...userParams, ...mandatory];
    }

    /**
     * Adds a parameter to the procedure
     * @param {Parameter|object} param - Parameter to add
     * @returns {boolean} Success
     */
    addParameter(param) {
        const parameter = param instanceof Parameter ? param : new Parameter(param);
        
        // Validate uniqueness
        if (this.parameters.some(p => p.name === parameter.name)) {
            throw new Error(`Parameter ${parameter.name} already exists`);
        }

        // Validate parameter
        const errors = parameter.validate();
        if (errors.length > 0) {
            throw new Error(`Invalid parameter: ${errors.join(', ')}`);
        }

        this.parameters.push(parameter);
        this.modifiedDate = new Date();
        return true;
    }

    /**
     * Removes a parameter from the procedure
     * @param {string} paramName - Parameter name to remove
     * @returns {boolean} Success
     */
    removeParameter(paramName) {
        // Prevent removal of mandatory parameters
        if (paramName === 'p_Status' || paramName === 'p_ErrorMsg') {
            throw new Error('Cannot remove mandatory parameters p_Status and p_ErrorMsg');
        }

        const index = this.parameters.findIndex(p => p.name === paramName);
        if (index === -1) {
            return false;
        }

        this.parameters.splice(index, 1);
        this.modifiedDate = new Date();
        return true;
    }

    /**
     * Reorders operations by moving from one index to another
     * @param {number} fromIdx - Source index
     * @param {number} toIdx - Destination index
     */
    reorderOperations(fromIdx, toIdx) {
        if (fromIdx < 0 || fromIdx >= this.operations.length) {
            throw new Error('Invalid source index');
        }
        if (toIdx < 0 || toIdx >= this.operations.length) {
            throw new Error('Invalid destination index');
        }

        const [operation] = this.operations.splice(fromIdx, 1);
        this.operations.splice(toIdx, 0, operation);

        // Update order property
        this.operations.forEach((op, idx) => {
            op.order = idx;
        });

        this.modifiedDate = new Date();
    }

    /**
     * Validates the complete procedure definition
     * @returns {object} Validation result with errors array
     */
    validate() {
        const errors = [];

        // Validate name
        if (!this.name) {
            errors.push('Procedure name is required');
        } else {
            // Check domain_table_action pattern
            const namePattern = /^[a-z]+_[a-z]+_[A-Z][a-zA-Z_]+$/;
            if (!namePattern.test(this.name)) {
                errors.push('Procedure name must follow pattern: domain_table_action (e.g., inv_inventory_Add_Item)');
            }
        }

        // Validate parameters
        this.parameters.forEach((param, idx) => {
            const paramErrors = param.validate();
            if (paramErrors.length > 0) {
                errors.push(`Parameter ${idx + 1} (${param.name}): ${paramErrors.join(', ')}`);
            }
        });

        // Check mandatory parameters
        const hasStatus = this.parameters.some(p => p.name === 'p_Status' && p.direction === 'OUT');
        const hasErrorMsg = this.parameters.some(p => p.name === 'p_ErrorMsg' && p.direction === 'OUT');

        if (!hasStatus) {
            errors.push('Missing mandatory parameter: p_Status INT OUT');
        }
        if (!hasErrorMsg) {
            errors.push('Missing mandatory parameter: p_ErrorMsg VARCHAR(500) OUT');
        }

        // Validate operations exist (at least one)
        if (this.operations.length === 0 && this.validations.length === 0) {
            errors.push('Procedure must contain at least one validation rule or operation');
        }

        return {
            isValid: errors.length === 0,
            errors: errors
        };
    }

    /**
     * Serializes procedure to JSON for storage
     * @returns {object}
     */
    toJSON() {
        return {
            name: this.name,
            description: this.description,
            version: this.version,
            author: this.author,
            createdDate: this.createdDate.toISOString(),
            modifiedDate: this.modifiedDate.toISOString(),
            parameters: this.parameters.map(p => p.toJSON()),
            localVariables: this.localVariables,
            validations: this.validations,
            operations: this.operations,
            advancedFeatures: this.advancedFeatures,
            transactionControl: this.transactionControl,
            currentStep: this.currentStep,
            completedSteps: this.completedSteps,
            flowDiagram: this.flowDiagram
        };
    }

    /**
     * Creates ProcedureDefinition instance from JSON
     * @param {object} data - JSON data
     * @returns {ProcedureDefinition}
     */
    static fromJSON(data) {
        return new ProcedureDefinition(data);
    }

    /**
     * Generates SQL code for the procedure
     * Note: This is a placeholder - actual SQL generation is in SQLGenerator class
     * @returns {string}
     */
    toSQL() {
        // This will be implemented by SQLGenerator class
        throw new Error('Use SQLGenerator.generate(procedure) instead');
    }
}

/**
 * Auto-included parameters that cannot be removed
 */
export const MANDATORY_PARAMETERS = [
    {
        name: 'p_Status',
        direction: 'OUT',
        dataType: 'INT',
        description: 'Status code: 0=success, <0=error'
    },
    {
        name: 'p_ErrorMsg',
        direction: 'OUT',
        dataType: 'VARCHAR',
        length: 500,
        description: 'Error message if status < 0'
    }
];

/**
 * Supported MySQL 5.7 data types
 */
export const DATA_TYPES = [
    'VARCHAR',
    'CHAR',
    'TEXT',
    'MEDIUMTEXT',
    'LONGTEXT',
    'INT',
    'INTEGER',
    'BIGINT',
    'SMALLINT',
    'TINYINT',
    'DECIMAL',
    'NUMERIC',
    'FLOAT',
    'DOUBLE',
    'BOOLEAN',
    'BOOL',
    'DATE',
    'DATETIME',
    'TIMESTAMP',
    'TIME',
    'YEAR'
];

/**
 * Parameter direction options
 */
export const PARAMETER_DIRECTIONS = ['IN', 'OUT', 'INOUT'];

/**
 * DML Operation Types
 */
export const DML_OPERATION_TYPES = ['INSERT', 'UPDATE', 'DELETE', 'SELECT'];

/**
 * SQL comparison operators
 */
export const WHERE_OPERATORS = [
    '=', '!=', '<>', '>', '<', '>=', '<=', 
    'LIKE', 'NOT LIKE', 
    'IN', 'NOT IN', 
    'IS NULL', 'IS NOT NULL',
    'BETWEEN'
];

/**
 * Represents a column-to-value mapping for INSERT/UPDATE operations
 */
export class ColumnMapping {
    constructor(config = {}) {
        this.columnName = config.columnName || '';
        this.value = config.value || '';
        this.isExpression = config.isExpression || false; // true if value is SQL expression
    }

    toJSON() {
        return {
            columnName: this.columnName,
            value: this.value,
            isExpression: this.isExpression
        };
    }

    static fromJSON(data) {
        return new ColumnMapping(data);
    }
}

/**
 * Represents a WHERE clause condition
 */
export class WhereCondition {
    constructor(config = {}) {
        this.columnName = config.columnName || '';
        this.operator = config.operator || '=';
        this.value = config.value || '';
        this.logicalOperator = config.logicalOperator || 'AND'; // AND or OR for next condition
    }

    toSQL() {
        let sql = `${this.columnName} ${this.operator}`;
        
        if (this.operator === 'IS NULL' || this.operator === 'IS NOT NULL') {
            return sql;
        }
        
        if (this.operator === 'BETWEEN') {
            // Expect value like "1 AND 100"
            return `${this.columnName} BETWEEN ${this.value}`;
        }
        
        if (this.operator === 'IN' || this.operator === 'NOT IN') {
            // Expect value like "(1,2,3)" or array
            const valueStr = Array.isArray(this.value) 
                ? `(${this.value.join(',')})` 
                : this.value;
            return `${sql} ${valueStr}`;
        }
        
        // Regular comparison - add quotes if not a parameter or number
        const valueStr = this.value.startsWith('p_') || this.value.startsWith('v_') || !isNaN(this.value)
            ? this.value
            : `'${this.value}'`;
        
        return `${sql} ${valueStr}`;
    }

    toJSON() {
        return {
            columnName: this.columnName,
            operator: this.operator,
            value: this.value,
            logicalOperator: this.logicalOperator
        };
    }

    static fromJSON(data) {
        return new WhereCondition(data);
    }
}

/**
 * Represents a JOIN clause
 */
export class Join {
    constructor(config = {}) {
        this.type = config.type || 'INNER JOIN'; // INNER JOIN, LEFT JOIN, RIGHT JOIN
        this.targetTable = config.targetTable || '';
        this.alias = config.alias || '';
        this.onCondition = config.onCondition || ''; // e.g., "a.PartID = b.PartID"
    }

    toSQL(baseTableAlias = '') {
        const tableExpr = this.alias ? `${this.targetTable} ${this.alias}` : this.targetTable;
        return `${this.type} ${tableExpr} ON ${this.onCondition}`;
    }

    toJSON() {
        return {
            type: this.type,
            targetTable: this.targetTable,
            alias: this.alias,
            onCondition: this.onCondition
        };
    }

    static fromJSON(data) {
        return new Join(data);
    }
}

/**
 * Represents an ORDER BY clause
 */
export class OrderByClause {
    constructor(config = {}) {
        this.columnName = config.columnName || '';
        this.direction = config.direction || 'ASC'; // ASC or DESC
    }

    toSQL() {
        return `${this.columnName} ${this.direction}`;
    }

    toJSON() {
        return {
            columnName: this.columnName,
            direction: this.direction
        };
    }

    static fromJSON(data) {
        return new OrderByClause(data);
    }
}

/**
 * Represents a DML operation (INSERT, UPDATE, DELETE, SELECT)
 */
export class DMLOperation {
    constructor(config = {}) {
        this.id = config.id || this._generateId();
        this.type = config.type || 'INSERT'; // INSERT, UPDATE, DELETE, SELECT
        this.targetTable = config.targetTable || '';
        this.alias = config.alias || '';
        
        // INSERT/UPDATE column mappings
        this.columnMappings = (config.columnMappings || []).map(cm => 
            cm instanceof ColumnMapping ? cm : new ColumnMapping(cm)
        );
        
        // WHERE clause (UPDATE/DELETE/SELECT)
        this.whereConditions = (config.whereConditions || []).map(wc => 
            wc instanceof WhereCondition ? wc : new WhereCondition(wc)
        );
        
        // SELECT-specific
        this.selectColumns = config.selectColumns || ['*'];
        this.joins = (config.joins || []).map(j => 
            j instanceof Join ? j : new Join(j)
        );
        this.orderBy = (config.orderBy || []).map(ob => 
            ob instanceof OrderByClause ? ob : new OrderByClause(ob)
        );
        this.groupBy = config.groupBy || [];
        this.limit = config.limit || null;
        this.outputVariable = config.outputVariable || ''; // For SELECT INTO
        
        // UPDATE-specific
        this.trackRowCount = config.trackRowCount || false;
        
        // INSERT-specific
        this.onDuplicateKeyUpdate = config.onDuplicateKeyUpdate || {
            enabled: false,
            updateMappings: []
        };
        
        // Conditional execution
        this.conditionalBranch = config.conditionalBranch || null;
        
        this.order = config.order || 0;
        this.description = config.description || '';
    }

    _generateId() {
        return `op_${Date.now()}_${Math.random().toString(36).substr(2, 9)}`;
    }

    /**
     * Generate SQL for this operation
     * @returns {string} SQL statement
     */
    toSQL() {
        switch (this.type.toUpperCase()) {
            case 'INSERT':
                return this._generateInsertSQL();
            case 'UPDATE':
                return this._generateUpdateSQL();
            case 'DELETE':
                return this._generateDeleteSQL();
            case 'SELECT':
                return this._generateSelectSQL();
            default:
                return `-- Unknown operation type: ${this.type}`;
        }
    }

    _generateInsertSQL() {
        if (this.columnMappings.length === 0) {
            return `-- INSERT requires at least one column mapping`;
        }

        const columns = this.columnMappings.map(cm => cm.columnName).join(', ');
        const values = this.columnMappings.map(cm => cm.value).join(', ');
        
        let sql = `INSERT INTO ${this.targetTable} (${columns})\nVALUES (${values})`;
        
        // Add ON DUPLICATE KEY UPDATE if enabled
        if (this.onDuplicateKeyUpdate.enabled && this.onDuplicateKeyUpdate.updateMappings.length > 0) {
            const updates = this.onDuplicateKeyUpdate.updateMappings
                .map(cm => `${cm.columnName} = ${cm.value}`)
                .join(',\n    ');
            sql += `\nON DUPLICATE KEY UPDATE\n    ${updates}`;
        }
        
        return sql;
    }

    _generateUpdateSQL() {
        if (this.columnMappings.length === 0) {
            return `-- UPDATE requires at least one column mapping`;
        }

        const setClauses = this.columnMappings
            .map(cm => `${cm.columnName} = ${cm.value}`)
            .join(',\n    ');
        
        let sql = `UPDATE ${this.targetTable}\nSET ${setClauses}`;
        
        // Add WHERE clause
        if (this.whereConditions.length > 0) {
            const whereClause = this._buildWhereClause();
            sql += `\n${whereClause}`;
        }
        
        return sql;
    }

    _generateDeleteSQL() {
        let sql = `DELETE FROM ${this.targetTable}`;
        
        // Add WHERE clause
        if (this.whereConditions.length > 0) {
            const whereClause = this._buildWhereClause();
            sql += `\n${whereClause}`;
        } else {
            sql += `\n-- WARNING: No WHERE clause - will delete ALL rows!`;
        }
        
        return sql;
    }

    _generateSelectSQL() {
        const columns = this.selectColumns.join(', ');
        const tableExpr = this.alias ? `${this.targetTable} ${this.alias}` : this.targetTable;
        
        let sql = `SELECT ${columns}\n`;
        
        // INTO clause for storing result
        if (this.outputVariable) {
            sql += `INTO ${this.outputVariable}\n`;
        }
        
        sql += `FROM ${tableExpr}`;
        
        // Add JOINs
        if (this.joins.length > 0) {
            this.joins.forEach(join => {
                sql += `\n${join.toSQL()}`;
            });
        }
        
        // Add WHERE clause
        if (this.whereConditions.length > 0) {
            const whereClause = this._buildWhereClause();
            sql += `\n${whereClause}`;
        }
        
        // Add GROUP BY
        if (this.groupBy.length > 0) {
            sql += `\nGROUP BY ${this.groupBy.join(', ')}`;
        }
        
        // Add ORDER BY
        if (this.orderBy.length > 0) {
            const orderClauses = this.orderBy.map(ob => ob.toSQL()).join(', ');
            sql += `\nORDER BY ${orderClauses}`;
        }
        
        // Add LIMIT
        if (this.limit) {
            sql += `\nLIMIT ${this.limit}`;
        }
        
        return sql;
    }

    _buildWhereClause() {
        if (this.whereConditions.length === 0) {
            return '';
        }
        
        const conditions = [];
        for (let i = 0; i < this.whereConditions.length; i++) {
            const wc = this.whereConditions[i];
            conditions.push(wc.toSQL());
            
            // Add logical operator if not the last condition
            if (i < this.whereConditions.length - 1) {
                conditions.push(wc.logicalOperator);
            }
        }
        
        return `WHERE ${conditions.join(' ')}`;
    }

    /**
     * Check if operation requires WHERE clause (UPDATE/DELETE safety)
     * @returns {boolean}
     */
    requiresWhereClause() {
        return this.type === 'UPDATE' || this.type === 'DELETE';
    }

    /**
     * Validate operation configuration
     * @returns {Array} Array of error messages (empty if valid)
     */
    validate() {
        const errors = [];
        
        if (!this.targetTable) {
            errors.push('Target table is required');
        }
        
        if (this.type === 'INSERT' && this.columnMappings.length === 0) {
            errors.push('INSERT requires at least one column mapping');
        }
        
        if (this.type === 'UPDATE' && this.columnMappings.length === 0) {
            errors.push('UPDATE requires at least one column mapping');
        }
        
        if (this.requiresWhereClause() && this.whereConditions.length === 0) {
            errors.push(`${this.type} without WHERE clause affects all rows (potentially dangerous)`);
        }
        
        return errors;
    }

    /**
     * Get list of local variables needed by this operation
     * @returns {Array} Variable names
     */
    getDependencies() {
        const deps = [];
        
        if (this.trackRowCount) {
            deps.push('v_RowsAffected');
        }
        
        if (this.outputVariable && this.outputVariable.startsWith('v_')) {
            deps.push(this.outputVariable);
        }
        
        return deps;
    }

    toJSON() {
        return {
            id: this.id,
            type: this.type,
            targetTable: this.targetTable,
            alias: this.alias,
            columnMappings: this.columnMappings.map(cm => cm.toJSON()),
            whereConditions: this.whereConditions.map(wc => wc.toJSON()),
            selectColumns: this.selectColumns,
            joins: this.joins.map(j => j.toJSON()),
            orderBy: this.orderBy.map(ob => ob.toJSON()),
            groupBy: this.groupBy,
            limit: this.limit,
            outputVariable: this.outputVariable,
            trackRowCount: this.trackRowCount,
            onDuplicateKeyUpdate: this.onDuplicateKeyUpdate,
            conditionalBranch: this.conditionalBranch,
            order: this.order,
            description: this.description
        };
    }

    static fromJSON(data) {
        return new DMLOperation(data);
    }
}

/**
 * Validation Rule Types
 */
export const VALIDATION_RULE_TYPES = [
    'REQUIRED_FIELD',
    'POSITIVE_NUMBER',
    'DATE_RANGE',
    'STRING_LENGTH',
    'FOREIGN_KEY_CHECK',
    'ENUM_VALUE',
    'CUSTOM_CONDITION'
];

/**
 * Represents a validation rule that generates IF...THEN ROLLBACK blocks
 */
export class ValidationRule {
    constructor(config = {}) {
        this.id = config.id || this._generateId();
        this.type = config.type || 'REQUIRED_FIELD';
        this.parameterName = config.parameterName || '';
        this.errorMessage = config.errorMessage || '';
        this.errorStatusCode = config.errorStatusCode || -1;
        
        // Type-specific configuration
        this.config = config.config || {};
        
        // For POSITIVE_NUMBER: { allowZero: false }
        // For DATE_RANGE: { minDate: 'p_StartDate', maxDate: 'p_EndDate' }
        // For STRING_LENGTH: { minLength: 1, maxLength: 50 }
        // For FOREIGN_KEY_CHECK: { referenceTable: 'Parts', referenceColumn: 'PartID', localColumn: 'p_PartID' }
        // For ENUM_VALUE: { allowedValues: ['ACTIVE', 'INACTIVE', 'PENDING'] }
        // For CUSTOM_CONDITION: { condition: 'p_Quantity > 0 AND p_Quantity < 1000' }
        
        this.order = config.order || 0;
        this.enabled = config.enabled !== undefined ? config.enabled : true;
    }

    _generateId() {
        return `val_${Date.now()}_${Math.random().toString(36).substr(2, 9)}`;
    }

    /**
     * Generate SQL IF block for this validation rule
     * @returns {string} SQL validation block
     */
    toSQL() {
        if (!this.enabled) {
            return `-- Validation disabled: ${this.type} for ${this.parameterName}`;
        }

        switch (this.type) {
            case 'REQUIRED_FIELD':
                return this._generateRequiredFieldSQL();
            case 'POSITIVE_NUMBER':
                return this._generatePositiveNumberSQL();
            case 'DATE_RANGE':
                return this._generateDateRangeSQL();
            case 'STRING_LENGTH':
                return this._generateStringLengthSQL();
            case 'FOREIGN_KEY_CHECK':
                return this._generateForeignKeyCheckSQL();
            case 'ENUM_VALUE':
                return this._generateEnumValueSQL();
            case 'CUSTOM_CONDITION':
                return this._generateCustomConditionSQL();
            default:
                return `-- Unknown validation type: ${this.type}`;
        }
    }

    _generateRequiredFieldSQL() {
        return `-- Required Field Validation: ${this.parameterName}
IF ${this.parameterName} IS NULL OR ${this.parameterName} = '' THEN
    SET p_Status = ${this.errorStatusCode};
    SET p_ErrorMsg = '${this.errorMessage}';
    ROLLBACK;
    LEAVE proc_label;
END IF;`;
    }

    _generatePositiveNumberSQL() {
        const allowZero = this.config.allowZero || false;
        const operator = allowZero ? '<' : '<=';
        const comparison = allowZero ? '< 0' : '<= 0';
        
        return `-- Positive Number Validation: ${this.parameterName}
IF ${this.parameterName} ${comparison} THEN
    SET p_Status = ${this.errorStatusCode};
    SET p_ErrorMsg = '${this.errorMessage}';
    ROLLBACK;
    LEAVE proc_label;
END IF;`;
    }

    _generateDateRangeSQL() {
        const minDate = this.config.minDate || 'NULL';
        const maxDate = this.config.maxDate || 'NULL';
        
        let sql = `-- Date Range Validation: ${this.parameterName}\n`;
        
        if (minDate !== 'NULL') {
            sql += `IF ${this.parameterName} < ${minDate} THEN
    SET p_Status = ${this.errorStatusCode};
    SET p_ErrorMsg = '${this.errorMessage}';
    ROLLBACK;
    LEAVE proc_label;
END IF;\n`;
        }
        
        if (maxDate !== 'NULL') {
            sql += `IF ${this.parameterName} > ${maxDate} THEN
    SET p_Status = ${this.errorStatusCode};
    SET p_ErrorMsg = '${this.errorMessage}';
    ROLLBACK;
    LEAVE proc_label;
END IF;`;
        }
        
        return sql;
    }

    _generateStringLengthSQL() {
        const minLength = this.config.minLength || 1;
        const maxLength = this.config.maxLength || null;
        
        let sql = `-- String Length Validation: ${this.parameterName}\n`;
        
        if (minLength > 0) {
            sql += `IF LENGTH(${this.parameterName}) < ${minLength} THEN
    SET p_Status = ${this.errorStatusCode};
    SET p_ErrorMsg = '${this.errorMessage}';
    ROLLBACK;
    LEAVE proc_label;
END IF;\n`;
        }
        
        if (maxLength) {
            sql += `IF LENGTH(${this.parameterName}) > ${maxLength} THEN
    SET p_Status = ${this.errorStatusCode};
    SET p_ErrorMsg = '${this.errorMessage}';
    ROLLBACK;
    LEAVE proc_label;
END IF;`;
        }
        
        return sql;
    }

    _generateForeignKeyCheckSQL() {
        const refTable = this.config.referenceTable || 'ReferenceTable';
        const refColumn = this.config.referenceColumn || 'ID';
        const localColumn = this.config.localColumn || this.parameterName;
        
        return `-- Foreign Key Check Validation: ${this.parameterName}
DECLARE v_FkExists INT DEFAULT 0;
SELECT COUNT(*) INTO v_FkExists
FROM ${refTable}
WHERE ${refColumn} = ${localColumn};

IF v_FkExists = 0 THEN
    SET p_Status = ${this.errorStatusCode};
    SET p_ErrorMsg = '${this.errorMessage}';
    ROLLBACK;
    LEAVE proc_label;
END IF;`;
    }

    _generateEnumValueSQL() {
        const allowedValues = this.config.allowedValues || [];
        const valueList = allowedValues.map(v => `'${v}'`).join(', ');
        
        return `-- Enum Value Validation: ${this.parameterName}
IF ${this.parameterName} NOT IN (${valueList}) THEN
    SET p_Status = ${this.errorStatusCode};
    SET p_ErrorMsg = '${this.errorMessage}';
    ROLLBACK;
    LEAVE proc_label;
END IF;`;
    }

    _generateCustomConditionSQL() {
        const condition = this.config.condition || 'TRUE';
        
        return `-- Custom Validation: ${this.parameterName}
IF NOT (${condition}) THEN
    SET p_Status = ${this.errorStatusCode};
    SET p_ErrorMsg = '${this.errorMessage}';
    ROLLBACK;
    LEAVE proc_label;
END IF;`;
    }

    /**
     * Get dependencies (local variables) needed by this rule
     * @returns {Array<string>} Variable names
     */
    getDependencies() {
        const deps = [];
        
        if (this.type === 'FOREIGN_KEY_CHECK') {
            deps.push('v_FkExists');
        }
        
        return deps;
    }

    /**
     * Get human-readable description of this rule
     * @returns {string} Description
     */
    getDescription() {
        switch (this.type) {
            case 'REQUIRED_FIELD':
                return `${this.parameterName} is required`;
            case 'POSITIVE_NUMBER':
                return `${this.parameterName} must be positive`;
            case 'DATE_RANGE':
                return `${this.parameterName} must be within date range`;
            case 'STRING_LENGTH':
                return `${this.parameterName} length must be ${this.config.minLength}-${this.config.maxLength}`;
            case 'FOREIGN_KEY_CHECK':
                return `${this.parameterName} must reference ${this.config.referenceTable}`;
            case 'ENUM_VALUE':
                return `${this.parameterName} must be one of: ${this.config.allowedValues?.join(', ')}`;
            case 'CUSTOM_CONDITION':
                return `Custom: ${this.config.condition}`;
            default:
                return 'Unknown validation rule';
        }
    }

    /**
     * Validate rule configuration
     * @returns {Array<string>} Error messages (empty if valid)
     */
    validate() {
        const errors = [];
        
        if (!this.parameterName) {
            errors.push('Parameter name is required');
        }
        
        if (!this.errorMessage) {
            errors.push('Error message is required');
        }
        
        // Type-specific validation
        switch (this.type) {
            case 'FOREIGN_KEY_CHECK':
                if (!this.config.referenceTable) {
                    errors.push('Reference table is required for foreign key check');
                }
                if (!this.config.referenceColumn) {
                    errors.push('Reference column is required for foreign key check');
                }
                break;
            
            case 'ENUM_VALUE':
                if (!this.config.allowedValues || this.config.allowedValues.length === 0) {
                    errors.push('At least one allowed value is required for enum validation');
                }
                break;
            
            case 'CUSTOM_CONDITION':
                if (!this.config.condition) {
                    errors.push('Condition is required for custom validation');
                }
                break;
        }
        
        return errors;
    }

    toJSON() {
        return {
            id: this.id,
            type: this.type,
            parameterName: this.parameterName,
            errorMessage: this.errorMessage,
            errorStatusCode: this.errorStatusCode,
            config: this.config,
            order: this.order,
            enabled: this.enabled
        };
    }

    static fromJSON(data) {
        return new ValidationRule(data);
    }
}
