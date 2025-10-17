/**
 * Export Manager for Stored Procedure Builder
 * 
 * Handles exporting generated SQL to files, clipboard, and direct execution.
 * Supports multiple export formats and MySQL 5.7 compatibility.
 * 
 * @module export-manager
 */

import { showSuccess, showError } from './utils.js';

/**
 * Export Manager Class
 * Handles all export operations for stored procedures
 */
export class ExportManager {
    constructor() {
        this.defaultExtension = '.sql';
        this.defaultEncoding = 'utf-8';
    }

    /**
     * Export SQL to file using File System Access API or fallback to download
     * @param {string} sql - SQL content to export
     * @param {string} procedureName - Name of procedure (for filename)
     * @param {Object} options - Export options
     * @returns {Promise<boolean>} Success status
     */
    async exportToFile(sql, procedureName, options = {}) {
        try {
            const filename = this.generateFileName(procedureName, options);
            const content = this.formatSQLForExport(sql, procedureName, options);
            
            // Try File System Access API first (Chrome/Edge)
            if ('showSaveFilePicker' in window) {
                await this.exportWithFileSystemAPI(content, filename);
            } else {
                // Fallback to download link
                this.downloadFile(content, filename);
            }
            
            showSuccess(`Exported ${filename} successfully`);
            return true;
            
        } catch (error) {
            if (error.name === 'AbortError') {
                // User cancelled - not an error
                return false;
            }
            
            showError({
                error_type: 'export',
                user_message: 'Failed to export file',
                technical_detail: error.message
            });
            return false;
        }
    }

    /**
     * Export using File System Access API
     * @param {string} content - File content
     * @param {string} filename - Suggested filename
     */
    async exportWithFileSystemAPI(content, filename) {
        const options = {
            suggestedName: filename,
            types: [{
                description: 'SQL Files',
                accept: { 'text/sql': ['.sql'] }
            }]
        };
        
        const handle = await window.showSaveFilePicker(options);
        const writable = await handle.createWritable();
        await writable.write(content);
        await writable.close();
    }

    /**
     * Download file using traditional download link
     * @param {string} content - File content
     * @param {string} filename - Filename
     */
    downloadFile(content, filename) {
        const blob = new Blob([content], { type: 'text/sql;charset=utf-8' });
        const url = URL.createObjectURL(blob);
        
        const link = document.createElement('a');
        link.href = url;
        link.download = filename;
        link.style.display = 'none';
        
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
        
        // Cleanup
        setTimeout(() => URL.revokeObjectURL(url), 100);
    }

    /**
     * Copy SQL to clipboard
     * @param {string} sql - SQL content
     * @param {string} procedureName - Procedure name
     * @param {Object} options - Format options
     * @returns {Promise<boolean>} Success status
     */
    async copyToClipboard(sql, procedureName, options = {}) {
        try {
            const content = this.formatSQLForExport(sql, procedureName, options);
            
            if (navigator.clipboard && navigator.clipboard.writeText) {
                await navigator.clipboard.writeText(content);
            } else {
                // Fallback for older browsers
                this.copyToClipboardFallback(content);
            }
            
            showSuccess('SQL copied to clipboard');
            return true;
            
        } catch (error) {
            showError({
                error_type: 'clipboard',
                user_message: 'Failed to copy to clipboard',
                technical_detail: error.message
            });
            return false;
        }
    }

    /**
     * Fallback clipboard copy for older browsers
     * @param {string} content - Content to copy
     */
    copyToClipboardFallback(content) {
        const textarea = document.createElement('textarea');
        textarea.value = content;
        textarea.style.position = 'fixed';
        textarea.style.opacity = '0';
        
        document.body.appendChild(textarea);
        textarea.select();
        
        try {
            document.execCommand('copy');
        } finally {
            document.body.removeChild(textarea);
        }
    }

    /**
     * Generate filename for export
     * @param {string} procedureName - Procedure name
     * @param {Object} options - Options
     * @returns {string} Filename
     */
    generateFileName(procedureName, options = {}) {
        const timestamp = options.includeTimestamp ? 
            `_${new Date().toISOString().replace(/[:.]/g, '-').slice(0, -5)}` : 
            '';
        
        const prefix = options.prefix || '';
        const suffix = options.suffix || '';
        
        return `${prefix}${procedureName}${suffix}${timestamp}${this.defaultExtension}`;
    }

    /**
     * Format SQL for export with proper delimiters and headers
     * @param {string} sql - Raw SQL
     * @param {string} procedureName - Procedure name
     * @param {Object} options - Format options
     * @returns {string} Formatted SQL
     */
    formatSQLForExport(sql, procedureName, options = {}) {
        const parts = [];
        
        // Add header comment
        if (options.includeHeader !== false) {
            parts.push(this.generateHeader(procedureName, options));
        }
        
        // Add DROP statement if requested
        if (options.includeDrop) {
            parts.push(`DROP PROCEDURE IF EXISTS ${procedureName};`);
            parts.push('');
        }
        
        // Add DELIMITER change for MySQL client
        if (options.includeDelimiter !== false) {
            parts.push('DELIMITER $$');
            parts.push('');
        }
        
        // Add the procedure SQL
        parts.push(sql.trim());
        
        // Reset delimiter
        if (options.includeDelimiter !== false) {
            parts.push('');
            parts.push('$$');
            parts.push('');
            parts.push('DELIMITER ;');
        }
        
        // Add footer comment
        if (options.includeFooter) {
            parts.push('');
            parts.push(this.generateFooter(procedureName));
        }
        
        return parts.join('\n');
    }

    /**
     * Generate header comment
     * @param {string} procedureName - Procedure name
     * @param {Object} options - Options
     * @returns {string} Header comment
     */
    generateHeader(procedureName, options = {}) {
        const lines = [];
        
        lines.push('/*');
        lines.push(` * Stored Procedure: ${procedureName}`);
        
        if (options.description) {
            lines.push(` * Description: ${options.description}`);
        }
        
        if (options.author) {
            lines.push(` * Author: ${options.author}`);
        }
        
        lines.push(` * Generated: ${new Date().toISOString().replace('T', ' ').slice(0, 19)}`);
        lines.push(` * Generator: MySQL 5.7 Stored Procedure Builder`);
        lines.push(` * Target: MySQL 5.7+`);
        
        if (options.includeWarning !== false) {
            lines.push(' *');
            lines.push(' * WARNING: This procedure was auto-generated.');
            lines.push(' * Review carefully before executing in production.');
        }
        
        lines.push(' */');
        lines.push('');
        
        return lines.join('\n');
    }

    /**
     * Generate footer comment
     * @param {string} procedureName - Procedure name
     * @returns {string} Footer comment
     */
    generateFooter(procedureName) {
        return `-- End of ${procedureName}`;
    }

    /**
     * Export multiple procedures to single file
     * @param {Array<Object>} procedures - Array of {sql, name, description}
     * @param {string} filename - Output filename
     * @param {Object} options - Export options
     * @returns {Promise<boolean>} Success status
     */
    async exportBatch(procedures, filename, options = {}) {
        try {
            const parts = [];
            
            // Add batch header
            parts.push('/*');
            parts.push(' * Batch Export of Stored Procedures');
            parts.push(` * Generated: ${new Date().toISOString().replace('T', ' ').slice(0, 19)}`);
            parts.push(` * Procedures: ${procedures.length}`);
            parts.push(' */');
            parts.push('');
            
            // Add each procedure
            for (let i = 0; i < procedures.length; i++) {
                const proc = procedures[i];
                
                if (i > 0) {
                    parts.push('');
                    parts.push('-- ' + '='.repeat(70));
                    parts.push('');
                }
                
                const formattedSQL = this.formatSQLForExport(
                    proc.sql, 
                    proc.name,
                    { ...options, description: proc.description }
                );
                
                parts.push(formattedSQL);
            }
            
            const content = parts.join('\n');
            
            // Export the batch file
            if ('showSaveFilePicker' in window) {
                await this.exportWithFileSystemAPI(content, filename);
            } else {
                this.downloadFile(content, filename);
            }
            
            showSuccess(`Exported ${procedures.length} procedures to ${filename}`);
            return true;
            
        } catch (error) {
            if (error.name === 'AbortError') {
                return false;
            }
            
            showError({
                error_type: 'export',
                user_message: 'Failed to export batch',
                technical_detail: error.message
            });
            return false;
        }
    }

    /**
     * Export as SQL template (with placeholders)
     * @param {string} sql - SQL content
     * @param {string} procedureName - Procedure name
     * @returns {Promise<boolean>} Success status
     */
    async exportAsTemplate(sql, procedureName) {
        try {
            // Replace specific values with placeholders
            let template = sql;
            
            // Replace procedure name
            template = template.replace(
                new RegExp(`\\b${procedureName}\\b`, 'g'),
                '{{PROCEDURE_NAME}}'
            );
            
            // Replace common patterns
            template = template.replace(/p_Status = -?\d+/g, 'p_Status = {{STATUS_CODE}}');
            template = template.replace(/p_ErrorMsg = '.*?'/g, "p_ErrorMsg = '{{ERROR_MESSAGE}}'");
            
            const filename = this.generateFileName(procedureName, { suffix: '_template' });
            const content = this.formatSQLForExport(template, procedureName, {
                description: 'Template - Replace {{placeholders}} with actual values',
                includeWarning: true
            });
            
            if ('showSaveFilePicker' in window) {
                await this.exportWithFileSystemAPI(content, filename);
            } else {
                this.downloadFile(content, filename);
            }
            
            showSuccess(`Exported template: ${filename}`);
            return true;
            
        } catch (error) {
            if (error.name === 'AbortError') {
                return false;
            }
            
            showError({
                error_type: 'export',
                user_message: 'Failed to export template',
                technical_detail: error.message
            });
            return false;
        }
    }

    /**
     * Validate SQL before export
     * @param {string} sql - SQL to validate
     * @returns {Object} Validation result {valid, errors, warnings}
     */
    validateSQL(sql) {
        const errors = [];
        const warnings = [];
        
        // Check for required elements
        if (!sql.includes('CREATE PROCEDURE')) {
            errors.push('Missing CREATE PROCEDURE statement');
        }
        
        if (!sql.includes('BEGIN')) {
            errors.push('Missing BEGIN statement');
        }
        
        if (!sql.includes('END')) {
            errors.push('Missing END statement');
        }
        
        // Check for common issues
        if (!sql.includes('OUT p_Status')) {
            warnings.push('Missing OUT p_Status parameter (recommended)');
        }
        
        if (!sql.includes('OUT p_ErrorMsg')) {
            warnings.push('Missing OUT p_ErrorMsg parameter (recommended)');
        }
        
        if (sql.includes('SELECT *')) {
            warnings.push('SELECT * found - specify columns explicitly for better performance');
        }
        
        // Check for MySQL 5.7 incompatibilities
        if (sql.match(/WITH\s+\w+\s+AS\s*\(/i)) {
            errors.push('CTE (WITH clause) not supported in MySQL 5.7');
        }
        
        if (sql.match(/ROW_NUMBER\(\s*\)/i) || sql.match(/RANK\(\s*\)/i)) {
            errors.push('Window functions not supported in MySQL 5.7');
        }
        
        return {
            valid: errors.length === 0,
            errors: errors,
            warnings: warnings
        };
    }

    /**
     * Get export statistics
     * @param {string} sql - SQL content
     * @returns {Object} Statistics
     */
    getStatistics(sql) {
        return {
            lines: sql.split('\n').length,
            characters: sql.length,
            size: new Blob([sql]).size,
            sizeFormatted: this.formatBytes(new Blob([sql]).size),
            parameters: (sql.match(/\bp_\w+\b/g) || []).length,
            operations: (sql.match(/\b(INSERT|UPDATE|DELETE|SELECT)\b/gi) || []).length,
            validations: (sql.match(/IF\s+.*?\s+THEN/gi) || []).length
        };
    }

    /**
     * Format bytes to human-readable string
     * @param {number} bytes - Bytes
     * @returns {string} Formatted string
     */
    formatBytes(bytes) {
        if (bytes === 0) return '0 Bytes';
        
        const k = 1024;
        const sizes = ['Bytes', 'KB', 'MB'];
        const i = Math.floor(Math.log(bytes) / Math.log(k));
        
        return Math.round((bytes / Math.pow(k, i)) * 100) / 100 + ' ' + sizes[i];
    }
}

// Export singleton instance
export const exportManager = new ExportManager();
