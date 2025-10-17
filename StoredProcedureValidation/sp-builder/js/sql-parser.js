/**
 * SQL Parser for MySQL 5.7 Stored Procedures
 * 
 * Parses CREATE PROCEDURE syntax and converts to ProcedureDefinition object.
 * Supports reverse-engineering of parameters, DECLARE statements, validation blocks,
 * DML operations, and control flow.
 * 
 * @module sql-parser
 */

import { ProcedureDefinition, Parameter, DMLOperation, ColumnMapping, WhereCondition } from './procedure-model.js';

export class SQLParser {
    constructor() {
        this.sql = '';
        this.lines = [];
        this.currentLine = 0;
    }

    /**
     * Parse complete CREATE PROCEDURE statement
     * @param {string} sql - SQL text to parse
     * @returns {Object} Result with success flag and ProcedureDefinition or error
     */
    parse(sql) {
        try {
            this.sql = sql.trim();
            this.lines = this.sql.split('\n').map(line => line.trim());
            this.currentLine = 0;

            // Extract procedure metadata
            const procedureName = this.extractProcedureName();
            const parameters = this.extractParameters();
            const declares = this.extractDeclareStatements();
            const operations = this.extractDMLOperations();
            
            // Create procedure definition
            const procedure = new ProcedureDefinition({
                name: procedureName,
                description: this.extractDescription(),
                author: this.extractAuthor(),
                parameters: parameters,
                localVariables: declares,
                operations: operations
            });

            return {
                success: true,
                procedure: procedure,
                warnings: this.getWarnings()
            };

        } catch (error) {
            return {
                success: false,
                error: error.message,
                line: this.currentLine
            };
        }
    }

    /**
     * Extract procedure name from CREATE PROCEDURE statement
     * @returns {string} Procedure name
     */
    extractProcedureName() {
        // Pattern: CREATE PROCEDURE procedure_name(
        // Also handle: CREATE DEFINER=`root`@`localhost` PROCEDURE procedure_name(
        const pattern = /CREATE\s+(?:DEFINER\s*=\s*[^\s]+\s+)?PROCEDURE\s+`?([a-zA-Z0-9_]+)`?\s*\(/i;
        
        for (let i = 0; i < this.lines.length; i++) {
            const match = this.lines[i].match(pattern);
            if (match) {
                this.currentLine = i;
                return match[1];
            }
        }
        
        throw new Error('Could not find CREATE PROCEDURE statement');
    }

    /**
     * Extract parameters from procedure definition
     * @returns {Array<Parameter>} Array of Parameter objects
     */
    extractParameters() {
        const parameters = [];
        
        // Find parameter block (between ( and ))
        let paramBlock = this.extractParameterBlock();
        
        if (!paramBlock) {
            return parameters;
        }

        // Split by comma (accounting for nested parentheses in data types)
        const paramStrings = this.splitParameters(paramBlock);
        
        for (const paramStr of paramStrings) {
            const param = this.parseParameter(paramStr);
            if (param) {
                parameters.push(param);
            }
        }

        return parameters;
    }

    /**
     * Extract parameter block from CREATE PROCEDURE statement
     * @returns {string} Parameter block text
     */
    extractParameterBlock() {
        let inParams = false;
        let paramBlock = '';
        let parenCount = 0;

        for (let i = 0; i < this.lines.length; i++) {
            const line = this.lines[i];
            
            for (let j = 0; j < line.length; j++) {
                const char = line[j];
                
                if (char === '(' && !inParams) {
                    inParams = true;
                    parenCount = 1;
                    continue;
                }
                
                if (inParams) {
                    if (char === '(') parenCount++;
                    if (char === ')') parenCount--;
                    
                    if (parenCount === 0) {
                        return paramBlock.trim();
                    }
                    
                    paramBlock += char;
                }
            }
            
            if (inParams) {
                paramBlock += ' ';
            }
        }

        return paramBlock.trim();
    }

    /**
     * Split parameter block by commas (respecting nested parentheses)
     * @param {string} paramBlock - Parameter block text
     * @returns {Array<string>} Array of parameter strings
     */
    splitParameters(paramBlock) {
        const params = [];
        let current = '';
        let parenCount = 0;

        for (let i = 0; i < paramBlock.length; i++) {
            const char = paramBlock[i];
            
            if (char === '(') parenCount++;
            if (char === ')') parenCount--;
            
            if (char === ',' && parenCount === 0) {
                params.push(current.trim());
                current = '';
            } else {
                current += char;
            }
        }

        if (current.trim()) {
            params.push(current.trim());
        }

        return params;
    }

    /**
     * Parse single parameter string
     * @param {string} paramStr - Parameter string (e.g., "IN p_PartNumber VARCHAR(50)")
     * @returns {Parameter} Parameter object
     */
    parseParameter(paramStr) {
        // Pattern: [IN|OUT|INOUT] param_name data_type[(length[,scale])]
        const pattern = /^\s*(IN|OUT|INOUT)?\s+`?([a-zA-Z0-9_]+)`?\s+([A-Z]+)(?:\((\d+)(?:,(\d+))?\))?\s*$/i;
        
        const match = paramStr.match(pattern);
        if (!match) {
            console.warn(`Could not parse parameter: ${paramStr}`);
            return null;
        }

        const direction = match[1] ? match[1].toUpperCase() : 'IN';
        const name = match[2];
        const dataType = match[3].toUpperCase();
        const length = match[4] ? parseInt(match[4]) : null;
        const scale = match[5] ? parseInt(match[5]) : null;

        return new Parameter({
            name: name,
            direction: direction,
            dataType: dataType,
            length: length,
            precision: length,
            scale: scale
        });
    }

    /**
     * Extract DECLARE statements for local variables
     * @returns {Array<Object>} Array of variable declarations
     */
    extractDeclareStatements() {
        const declares = [];
        const pattern = /DECLARE\s+`?([a-zA-Z0-9_]+)`?\s+([A-Z]+)(?:\((\d+)(?:,(\d+))?\))?(?:\s+DEFAULT\s+(.+?))?;/gi;

        for (const line of this.lines) {
            let match;
            while ((match = pattern.exec(line)) !== null) {
                declares.push({
                    name: match[1],
                    dataType: match[2].toUpperCase(),
                    length: match[3] ? parseInt(match[3]) : null,
                    precision: match[3] ? parseInt(match[3]) : null,
                    scale: match[4] ? parseInt(match[4]) : null,
                    defaultValue: match[5] ? match[5].trim() : null
                });
            }
        }

        return declares;
    }

    /**
     * Extract DML operations (INSERT, UPDATE, DELETE, SELECT)
     * @returns {Array<DMLOperation>} Array of DMLOperation objects
     */
    extractDMLOperations() {
        const operations = [];
        
        // Find DML statements
        operations.push(...this.extractInsertStatements());
        operations.push(...this.extractUpdateStatements());
        operations.push(...this.extractDeleteStatements());
        operations.push(...this.extractSelectStatements());
        
        // Sort by order of appearance
        operations.sort((a, b) => a.order - b.order);
        
        return operations;
    }

    /**
     * Extract INSERT statements
     * @returns {Array<DMLOperation>} Array of INSERT operations
     */
    extractInsertStatements() {
        const operations = [];
        
        // Pattern: INSERT INTO table_name (col1, col2) VALUES (val1, val2)
        const pattern = /INSERT\s+INTO\s+`?([a-zA-Z0-9_]+)`?\s*\(([^)]+)\)\s*VALUES\s*\(([^)]+)\)/gi;
        
        for (let i = 0; i < this.lines.length; i++) {
            const line = this.lines[i];
            let match;
            
            while ((match = pattern.exec(line)) !== null) {
                const tableName = match[1];
                const columns = match[2].split(',').map(c => c.trim().replace(/`/g, ''));
                const values = match[3].split(',').map(v => v.trim());
                
                const columnMappings = columns.map((col, idx) => 
                    new ColumnMapping({
                        columnName: col,
                        value: values[idx] || ''
                    })
                );
                
                operations.push(new DMLOperation({
                    type: 'INSERT',
                    targetTable: tableName,
                    columnMappings: columnMappings,
                    order: i
                }));
            }
        }
        
        return operations;
    }

    /**
     * Extract UPDATE statements
     * @returns {Array<DMLOperation>} Array of UPDATE operations
     */
    extractUpdateStatements() {
        const operations = [];
        
        // Pattern: UPDATE table_name SET col1=val1, col2=val2 WHERE condition
        const pattern = /UPDATE\s+`?([a-zA-Z0-9_]+)`?\s+SET\s+(.+?)(?:WHERE\s+(.+?))?(?:;|$)/gi;
        
        for (let i = 0; i < this.lines.length; i++) {
            const line = this.lines[i];
            let match;
            
            while ((match = pattern.exec(line)) !== null) {
                const tableName = match[1];
                const setClause = match[2];
                const whereClause = match[3];
                
                // Parse SET clause
                const columnMappings = this.parseSetClause(setClause);
                
                // Parse WHERE clause
                const whereConditions = whereClause ? this.parseWhereClause(whereClause) : [];
                
                operations.push(new DMLOperation({
                    type: 'UPDATE',
                    targetTable: tableName,
                    columnMappings: columnMappings,
                    whereConditions: whereConditions,
                    order: i
                }));
            }
        }
        
        return operations;
    }

    /**
     * Extract DELETE statements
     * @returns {Array<DMLOperation>} Array of DELETE operations
     */
    extractDeleteStatements() {
        const operations = [];
        
        // Pattern: DELETE FROM table_name WHERE condition
        const pattern = /DELETE\s+FROM\s+`?([a-zA-Z0-9_]+)`?(?:\s+WHERE\s+(.+?))?(?:;|$)/gi;
        
        for (let i = 0; i < this.lines.length; i++) {
            const line = this.lines[i];
            let match;
            
            while ((match = pattern.exec(line)) !== null) {
                const tableName = match[1];
                const whereClause = match[2];
                
                const whereConditions = whereClause ? this.parseWhereClause(whereClause) : [];
                
                operations.push(new DMLOperation({
                    type: 'DELETE',
                    targetTable: tableName,
                    whereConditions: whereConditions,
                    order: i
                }));
            }
        }
        
        return operations;
    }

    /**
     * Extract SELECT statements
     * @returns {Array<DMLOperation>} Array of SELECT operations
     */
    extractSelectStatements() {
        const operations = [];
        
        // Pattern: SELECT columns FROM table WHERE condition ORDER BY col LIMIT num
        const pattern = /SELECT\s+(.+?)\s+(?:INTO\s+([a-zA-Z0-9_@]+)\s+)?FROM\s+`?([a-zA-Z0-9_]+)`?(?:\s+WHERE\s+(.+?))?(?:\s+ORDER\s+BY\s+(.+?))?(?:\s+LIMIT\s+(\d+))?(?:;|$)/gi;
        
        for (let i = 0; i < this.lines.length; i++) {
            const line = this.lines[i];
            let match;
            
            while ((match = pattern.exec(line)) !== null) {
                const columnsStr = match[1];
                const outputVariable = match[2] || '';
                const tableName = match[3];
                const whereClause = match[4];
                const orderByClause = match[5];
                const limit = match[6] ? parseInt(match[6]) : null;
                
                // Parse columns
                const selectColumns = columnsStr.split(',').map(c => c.trim());
                
                // Parse WHERE clause
                const whereConditions = whereClause ? this.parseWhereClause(whereClause) : [];
                
                // Parse ORDER BY (simplified)
                const orderBy = orderByClause ? this.parseOrderByClause(orderByClause) : [];
                
                operations.push(new DMLOperation({
                    type: 'SELECT',
                    targetTable: tableName,
                    selectColumns: selectColumns,
                    outputVariable: outputVariable,
                    whereConditions: whereConditions,
                    orderBy: orderBy,
                    limit: limit,
                    order: i
                }));
            }
        }
        
        return operations;
    }

    /**
     * Parse SET clause into column mappings
     * @param {string} setClause - SET clause text
     * @returns {Array<ColumnMapping>} Array of column mappings
     */
    parseSetClause(setClause) {
        const mappings = [];
        const assignments = setClause.split(',');
        
        for (const assignment of assignments) {
            const parts = assignment.split('=').map(p => p.trim());
            if (parts.length === 2) {
                mappings.push(new ColumnMapping({
                    columnName: parts[0].replace(/`/g, ''),
                    value: parts[1]
                }));
            }
        }
        
        return mappings;
    }

    /**
     * Parse WHERE clause into conditions
     * @param {string} whereClause - WHERE clause text
     * @returns {Array<WhereCondition>} Array of where conditions
     */
    parseWhereClause(whereClause) {
        const conditions = [];
        
        // Simple split by AND/OR (doesn't handle nested conditions perfectly)
        const parts = whereClause.split(/\s+(AND|OR)\s+/i);
        
        for (let i = 0; i < parts.length; i += 2) {
            const conditionStr = parts[i].trim();
            const logicalOp = parts[i + 1] ? parts[i + 1].toUpperCase() : 'AND';
            
            // Parse condition: column operator value
            const match = conditionStr.match(/`?([a-zA-Z0-9_]+)`?\s*(=|!=|<>|>|<|>=|<=|LIKE|IN)\s*(.+)/i);
            
            if (match) {
                conditions.push(new WhereCondition({
                    columnName: match[1],
                    operator: match[2],
                    value: match[3].trim(),
                    logicalOperator: logicalOp
                }));
            }
        }
        
        return conditions;
    }

    /**
     * Parse ORDER BY clause
     * @param {string} orderByClause - ORDER BY clause text
     * @returns {Array<Object>} Array of order by objects
     */
    parseOrderByClause(orderByClause) {
        const orderBy = [];
        const parts = orderByClause.split(',');
        
        for (const part of parts) {
            const match = part.trim().match(/`?([a-zA-Z0-9_]+)`?\s*(ASC|DESC)?/i);
            if (match) {
                orderBy.push({
                    columnName: match[1],
                    direction: match[2] ? match[2].toUpperCase() : 'ASC'
                });
            }
        }
        
        return orderBy;
    }

    /**
     * Extract description from comment header
     * @returns {string} Description text
     */
    extractDescription() {
        // Look for -- Description: or /* Description: */
        for (const line of this.lines) {
            const match = line.match(/--\s*Description:\s*(.+)/i) || 
                         line.match(/\/\*\s*Description:\s*(.+?)\s*\*\//i);
            if (match) {
                return match[1].trim();
            }
        }
        return '';
    }

    /**
     * Extract author from comment header
     * @returns {string} Author name
     */
    extractAuthor() {
        // Look for -- Author: or /* Author: */
        for (const line of this.lines) {
            const match = line.match(/--\s*Author:\s*(.+)/i) || 
                         line.match(/\/\*\s*Author:\s*(.+?)\s*\*\//i);
            if (match) {
                return match[1].trim();
            }
        }
        return '';
    }

    /**
     * Get warnings from parsing process
     * @returns {Array<string>} Array of warning messages
     */
    getWarnings() {
        const warnings = [];
        
        // Check for unsupported features
        if (this.sql.includes('CURSOR')) {
            warnings.push('CURSOR statements found - not fully supported yet');
        }
        if (this.sql.includes('LOOP')) {
            warnings.push('LOOP statements found - not fully supported yet');
        }
        if (this.sql.includes('CASE')) {
            warnings.push('CASE statements found - may need manual review');
        }
        
        return warnings;
    }
}

// Export singleton instance
export const sqlParser = new SQLParser();
