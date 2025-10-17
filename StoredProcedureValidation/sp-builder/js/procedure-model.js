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
