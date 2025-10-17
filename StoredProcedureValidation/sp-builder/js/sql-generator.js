/**
 * SQL Generator for MySQL 5.7 Stored Procedures
 * 
 * Generates MySQL 5.7 compatible SQL from ProcedureDefinition objects
 */

export class SQLGenerator {
    constructor() {
        this.indentSize = 4;
    }

    /**
     * Generate complete SQL for a procedure
     * @param {ProcedureDefinition} procedure - Procedure definition
     * @returns {string} MySQL 5.7 SQL code
     */
    generate(procedure) {
        const parts = [
            this._generateDelimiterStart(),
            this._generateDropStatement(procedure),
            this._generateCreateProcedure(procedure),
            this._generateDelimiterEnd()
        ];

        return parts.join('\n\n');
    }

    /**
     * Generate header comment with metadata
     * @param {ProcedureDefinition} procedure
     * @returns {string}
     */
    generateHeader(procedure) {
        const lines = [
            '/*',
            ` * Procedure: ${procedure.name}`,
            ` * Description: ${procedure.description}`,
            ' *',
            ' * Parameters:'
        ];

        // Add parameter documentation
        procedure.parameters.forEach(param => {
            const desc = param.description ? ` - ${param.description}` : '';
            lines.push(` *   ${param.direction} ${param.name} ${param.getTypeDeclaration()}${desc}`);
        });

        lines.push(' *');

        // Add tables accessed (if operations exist)
        const tables = this._extractTablesFromOperations(procedure.operations);
        if (tables.length > 0) {
            lines.push(` * Tables Accessed: ${tables.join(', ')}`);
        }

        lines.push(' *');
        lines.push(` * Author: ${procedure.author || 'Unknown'}`);
        lines.push(` * Created: ${procedure.createdDate.toISOString().split('T')[0]}`);
        lines.push(` * Modified: ${procedure.modifiedDate.toISOString().split('T')[0]}`);
        lines.push(` * Version: ${procedure.version}`);
        lines.push(' */');

        return lines.join('\n');
    }

    /**
     * Generate parameter declarations
     * @param {ProcedureDefinition} procedure
     * @returns {string}
     */
    generateParameters(procedure) {
        return procedure.parameters
            .sort((a, b) => a.order - b.order)
            .map(param => {
                return `    ${param.direction} ${param.name} ${param.getTypeDeclaration()}`;
            })
            .join(',\n');
    }

    /**
     * Generate local variable declarations
     * @param {ProcedureDefinition} procedure
     * @returns {string}
     */
    generateVariables(procedure) {
        if (procedure.localVariables.length === 0) {
            return '';
        }

        const declarations = procedure.localVariables.map(variable => {
            const defaultVal = variable.defaultValue !== null 
                ? ` DEFAULT ${this._formatValue(variable.defaultValue)}`
                : '';
            
            return `    DECLARE ${variable.name} ${this._getTypeDeclaration(variable)}${defaultVal};`;
        });

        return declarations.join('\n');
    }

    /**
     * Generate validation rules as SQL IF blocks
     * @param {array} validations - Validation rules
     * @returns {string}
     */
    generateValidations(validations) {
        if (!validations || validations.length === 0) {
            return '';
        }

        const blocks = validations
            .sort((a, b) => a.order - b.order)
            .map(validation => this._generateValidationBlock(validation));

        return blocks.join('\n\n');
    }

    /**
     * Generate DML operations
     * @param {array} operations - DML operations
     * @returns {string}
     */
    generateOperations(operations) {
        if (!operations || operations.length === 0) {
            return '';
        }

        const statements = operations
            .sort((a, b) => a.order - b.order)
            .map(op => this._generateOperationSQL(op));

        return statements.join('\n\n');
    }

    /**
     * Generate DELIMITER $$ statement
     * @returns {string}
     * @private
     */
    _generateDelimiterStart() {
        return 'DELIMITER $$';
    }

    /**
     * Generate DELIMITER ; statement
     * @returns {string}
     * @private
     */
    _generateDelimiterEnd() {
        return 'DELIMITER ;';
    }

    /**
     * Generate DROP PROCEDURE statement
     * @param {ProcedureDefinition} procedure
     * @returns {string}
     * @private
     */
    _generateDropStatement(procedure) {
        return `DROP PROCEDURE IF EXISTS ${procedure.name}$$`;
    }

    /**
     * Generate complete CREATE PROCEDURE statement
     * @param {ProcedureDefinition} procedure
     * @returns {string}
     * @private
     */
    _generateCreateProcedure(procedure) {
        const parts = [];

        // Header comment
        parts.push(this.generateHeader(procedure));

        // CREATE PROCEDURE
        parts.push(`CREATE PROCEDURE ${procedure.name}(`);
        parts.push(this.generateParameters(procedure));
        parts.push(')');
        parts.push('BEGIN');

        // Local variables
        const variables = this.generateVariables(procedure);
        if (variables) {
            parts.push(variables);
            parts.push('');
        }

        // Error handler
        parts.push(this._generateErrorHandler());
        parts.push('');

        // Transaction start (if enabled)
        if (procedure.transactionControl.enabled) {
            parts.push('    START TRANSACTION;');
            parts.push('');
        }

        // Validation rules
        const validations = this.generateValidations(procedure.validations);
        if (validations) {
            parts.push('    -- Validation Rules');
            parts.push(validations);
        }

        // DML Operations
        const operations = this.generateOperations(procedure.operations);
        if (operations) {
            if (validations) {
                // If we have validations, wrap operations in ELSE block
                parts.push('    ELSE');
                parts.push('        -- DML Operations');
                parts.push(this._indentCode(operations, 2));
                parts.push('');
                parts.push('        -- Success');
                parts.push('        SET p_Status = 0;');
                parts.push('        SET p_ErrorMsg = NULL;');
                if (procedure.transactionControl.enabled) {
                    parts.push('        COMMIT;');
                }
                parts.push('    END IF;');
            } else {
                // No validations, just operations
                parts.push('    -- DML Operations');
                parts.push(operations);
                parts.push('');
                parts.push('    -- Success');
                parts.push('    SET p_Status = 0;');
                parts.push('    SET p_ErrorMsg = NULL;');
                if (procedure.transactionControl.enabled) {
                    parts.push('    COMMIT;');
                }
            }
        } else {
            // No operations defined yet
            parts.push('    -- TODO: Add DML operations here');
            parts.push('');
            parts.push('    SET p_Status = 0;');
            parts.push('    SET p_ErrorMsg = NULL;');
        }

        parts.push('END$$');

        return parts.join('\n');
    }

    /**
     * Generate error handler block
     * @returns {string}
     * @private
     */
    _generateErrorHandler() {
        return `    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        SET p_Status = -99;
        SET p_ErrorMsg = 'Database error occurred';
    END;`;
    }

    /**
     * Generate validation block SQL
     * @param {object} validation - Validation rule
     * @returns {string}
     * @private
     */
    _generateValidationBlock(validation) {
        let condition;

        switch (validation.type) {
            case 'Required Field':
                condition = `${validation.targetParameter} IS NULL OR ${validation.targetParameter} = ''`;
                break;

            case 'Positive Number':
                condition = `${validation.targetParameter} <= 0`;
                break;

            case 'Numeric Range':
                condition = `${validation.targetParameter} NOT BETWEEN ${validation.minValue} AND ${validation.maxValue}`;
                break;

            case 'String Length':
                condition = `LENGTH(${validation.targetParameter}) NOT BETWEEN ${validation.minLength} AND ${validation.maxLength}`;
                break;

            case 'Date Range':
                condition = `${validation.targetParameter} NOT BETWEEN ${validation.dateFrom} AND ${validation.dateTo}`;
                break;

            case 'Foreign Key Check':
                // This requires a SELECT COUNT check, which is more complex
                return `    SELECT COUNT(*) INTO v_Exists FROM ${validation.referenceTable} WHERE ${validation.referenceColumn} = ${validation.targetParameter};
    IF v_Exists = 0 THEN
        ROLLBACK;
        SET p_Status = ${validation.errorCode};
        SET p_ErrorMsg = '${validation.errorMessage}';`;

            case 'Custom':
                condition = validation.targetExpression;
                break;

            default:
                condition = 'FALSE';
        }

        return `    IF ${condition} THEN
        ROLLBACK;
        SET p_Status = ${validation.errorCode};
        SET p_ErrorMsg = '${validation.errorMessage}';`;
    }

    /**
     * Generate SQL for a DML operation
     * @param {object} operation - DML operation
     * @returns {string}
     * @private
     */
    _generateOperationSQL(operation) {
        switch (operation.type) {
            case 'INSERT':
                return this._generateInsert(operation);
            case 'UPDATE':
                return this._generateUpdate(operation);
            case 'DELETE':
                return this._generateDelete(operation);
            case 'SELECT':
                return this._generateSelect(operation);
            default:
                return `    -- Unknown operation type: ${operation.type}`;
        }
    }

    /**
     * Generate INSERT statement
     * @param {object} operation
     * @returns {string}
     * @private
     */
    _generateInsert(operation) {
        const columns = operation.columnMappings.map(m => m.columnName).join(', ');
        const values = operation.columnMappings.map(m => m.value).join(', ');

        let sql = `    INSERT INTO ${operation.targetTable} (${columns})\n`;
        sql += `    VALUES (${values});`;

        // ON DUPLICATE KEY UPDATE
        if (operation.onDuplicateKeyUpdate?.enabled) {
            const updates = operation.onDuplicateKeyUpdate.updateMappings
                .map(m => `${m.columnName} = ${m.value}`)
                .join(', ');
            sql += `\n    ON DUPLICATE KEY UPDATE ${updates};`;
        }

        return sql;
    }

    /**
     * Generate UPDATE statement
     * @param {object} operation
     * @returns {string}
     * @private
     */
    _generateUpdate(operation) {
        const setClauses = operation.columnMappings
            .map(m => `${m.columnName} = ${m.value}`)
            .join(', ');

        let sql = `    UPDATE ${operation.targetTable}\n`;
        sql += `    SET ${setClauses}`;

        if (operation.whereConditions && operation.whereConditions.length > 0) {
            const where = this._generateWhereClause(operation.whereConditions);
            sql += `\n    WHERE ${where}`;
        }

        sql += ';';

        // Track row count if requested
        if (operation.trackRowCount) {
            sql += '\n    SELECT ROW_COUNT() INTO v_RowsAffected;';
        }

        return sql;
    }

    /**
     * Generate DELETE statement
     * @param {object} operation
     * @returns {string}
     * @private
     */
    _generateDelete(operation) {
        let sql = `    DELETE FROM ${operation.targetTable}`;

        if (operation.whereConditions && operation.whereConditions.length > 0) {
            const where = this._generateWhereClause(operation.whereConditions);
            sql += `\n    WHERE ${where}`;
        }

        sql += ';';

        return sql;
    }

    /**
     * Generate SELECT statement
     * @param {object} operation
     * @returns {string}
     * @private
     */
    _generateSelect(operation) {
        const columns = operation.selectColumns.join(', ');
        let sql = `    SELECT ${columns}\n`;

        if (operation.outputVariable) {
            sql += `    INTO ${operation.outputVariable}\n`;
        }

        sql += `    FROM ${operation.targetTable}`;

        // JOINs
        if (operation.joins && operation.joins.length > 0) {
            operation.joins.forEach(join => {
                sql += `\n    ${join.type} ${join.targetTable} ON ${join.onCondition}`;
            });
        }

        // WHERE clause
        if (operation.whereConditions && operation.whereConditions.length > 0) {
            const where = this._generateWhereClause(operation.whereConditions);
            sql += `\n    WHERE ${where}`;
        }

        // ORDER BY
        if (operation.orderBy && operation.orderBy.length > 0) {
            const orderBy = operation.orderBy
                .map(o => `${o.columnName} ${o.direction}`)
                .join(', ');
            sql += `\n    ORDER BY ${orderBy}`;
        }

        // LIMIT
        if (operation.limit) {
            sql += `\n    LIMIT ${operation.limit}`;
        }

        sql += ';';

        return sql;
    }

    /**
     * Generate WHERE clause from conditions
     * @param {array} conditions
     * @returns {string}
     * @private
     */
    _generateWhereClause(conditions) {
        return conditions
            .map((cond, idx) => {
                const operator = cond.operator === 'IS NULL' || cond.operator === 'IS NOT NULL'
                    ? cond.operator
                    : `${cond.operator} ${cond.value}`;
                
                const clause = `${cond.columnName} ${operator}`;
                
                if (idx < conditions.length - 1) {
                    return `${clause} ${cond.logicalOperator || 'AND'}`;
                }
                return clause;
            })
            .join(' ');
    }

    /**
     * Extract table names from operations
     * @param {array} operations
     * @returns {array} Unique table names
     * @private
     */
    _extractTablesFromOperations(operations) {
        const tables = new Set();
        operations.forEach(op => {
            if (op.targetTable) {
                tables.add(op.targetTable);
            }
        });
        return Array.from(tables);
    }

    /**
     * Get type declaration for variable
     * @param {object} variable
     * @returns {string}
     * @private
     */
    _getTypeDeclaration(variable) {
        // Similar to Parameter.getTypeDeclaration()
        switch (variable.dataType.toUpperCase()) {
            case 'VARCHAR':
            case 'CHAR':
                return `${variable.dataType.toUpperCase()}(${variable.length || 50})`;
            
            case 'DECIMAL':
            case 'NUMERIC':
                return `${variable.dataType.toUpperCase()}(${variable.precision || 10},${variable.scale || 2})`;
            
            default:
                return variable.dataType.toUpperCase();
        }
    }

    /**
     * Format value for SQL
     * @param {any} value
     * @returns {string}
     * @private
     */
    _formatValue(value) {
        if (value === null) {
            return 'NULL';
        }
        if (typeof value === 'string') {
            return `'${value.replace(/'/g, "''")}'`;
        }
        if (typeof value === 'boolean') {
            return value ? '1' : '0';
        }
        return String(value);
    }

    /**
     * Indent code block
     * @param {string} code
     * @param {number} levels - Number of indent levels
     * @returns {string}
     * @private
     */
    _indentCode(code, levels) {
        const indent = ' '.repeat(this.indentSize * levels);
        return code.split('\n').map(line => indent + line).join('\n');
    }
}

// Export singleton instance
export const sqlGenerator = new SQLGenerator();
