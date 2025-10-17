/**
 * Wizard Controller for Stored Procedure Builder
 * 
 * Manages wizard navigation, state persistence, and step validation
 */

import { ProcedureDefinition, Parameter, DMLOperation, ValidationRule, VALIDATION_RULE_TYPES, DATA_TYPES } from './procedure-model.js';
import { storageManager } from './storage-manager.js';
import { sqlGenerator } from './sql-generator.js';
import { exportManager } from './export-manager.js';
import { dbMetadata } from './database-metadata.js';
import { showError, showSuccess } from './utils.js';

export class WizardController {
    constructor() {
        this.procedure = new ProcedureDefinition();
        this.currentStep = 1;
        this.totalSteps = 8;
        this.storageManager = storageManager;
        this.sqlGenerator = sqlGenerator;
        
        this.initializeElements();
        this.attachEventListeners();
        this.loadSavedState();
        this.updateUI();
        
        // Setup auto-save
        this.storageManager.autoSaveSetup(() => this.procedure);
    }

    /**
     * Initialize DOM element references
     */
    initializeElements() {
        // Navigation buttons
        this.btnPrev = document.getElementById('btn-prev');
        this.btnNext = document.getElementById('btn-next');
        this.btnSave = document.getElementById('btn-save');
        
        // Step 1 elements
        this.procedureName = document.getElementById('procedure-name');
        this.procedureDescription = document.getElementById('procedure-description');
        this.procedureAuthor = document.getElementById('procedure-author');
        this.nameError = document.getElementById('name-error');
        this.descriptionError = document.getElementById('description-error');
        
        // Step 2 elements
        this.paramName = document.getElementById('param-name');
        this.paramDirection = document.getElementById('param-direction');
        this.paramDataType = document.getElementById('param-datatype');
        this.paramLength = document.getElementById('param-length');
        this.paramDescription = document.getElementById('param-description');
        this.btnAddParameter = document.getElementById('btn-add-parameter');
        this.parametersList = document.getElementById('parameters-list');
        
        // Step 3 elements  
        this.validationCards = document.querySelectorAll('.validation-card');
        this.validationsList = document.getElementById('wizard-validations-list');
        this.validationsCount = document.getElementById('validations-count');
        this.noValidationsMsg = document.getElementById('no-validations-msg');
        
        // Step 4 elements
        this.btnAddInsert = document.getElementById('btn-add-insert');
        this.btnAddUpdate = document.getElementById('btn-add-update');
        this.btnAddDelete = document.getElementById('btn-add-delete');
        this.btnAddSelect = document.getElementById('btn-add-select');
        this.operationsList = document.getElementById('wizard-operations-list');
        this.operationsCount = document.getElementById('operations-count');
        this.noOperationsMsg = document.getElementById('no-operations-msg');
        this.paramLengthGroup = document.getElementById('param-length-group');
        
        // Step 7 elements
        this.sqlPreview = document.getElementById('sql-preview');
        this.btnValidateSyntax = document.getElementById('btn-validate-syntax');
        this.btnCopySql = document.getElementById('btn-copy-sql');
        this.btnExportFile = document.getElementById('btn-export-file');
        this.btnExportTemplate = document.getElementById('btn-export-template');
        this.statLines = document.getElementById('stat-lines');
        this.statSize = document.getElementById('stat-size');
        this.statParameters = document.getElementById('stat-parameters');
        this.statOperations = document.getElementById('stat-operations');
        this.statValidations = document.getElementById('stat-validations');
        this.syntaxErrors = document.getElementById('syntax-errors');
        this.syntaxWarnings = document.getElementById('syntax-warnings');
        this.syntaxSuccess = document.getElementById('syntax-success');
        this.errorList = document.getElementById('error-list');
        this.warningList = document.getElementById('warning-list');
    }

    /**
     * Attach event listeners to UI elements
     */
    attachEventListeners() {
        // Navigation
        this.btnPrev?.addEventListener('click', () => this.previousStep());
        this.btnNext?.addEventListener('click', () => this.nextStep());
        this.btnSave?.addEventListener('click', () => this.saveState());
        
        // Step 1 - Name validation
        this.procedureName?.addEventListener('input', () => this.validateProcedureName());
        this.procedureDescription?.addEventListener('input', () => this.validateDescription());
        
        // Step 2 - Parameter management
        this.btnAddParameter?.addEventListener('click', () => this.addParameter());
        this.paramDataType?.addEventListener('change', () => this.updateParameterForm());
        
        // Step 3 - Validation rules
        this.validationCards?.forEach(card => {
            card.addEventListener('click', () => {
                const ruleType = card.getAttribute('data-rule-type');
                this.quickAddValidation(ruleType);
            });
        });
        
        // Step 4 - Operations management
        this.btnAddInsert?.addEventListener('click', () => this.quickAddOperation('INSERT'));
        this.btnAddUpdate?.addEventListener('click', () => this.quickAddOperation('UPDATE'));
        this.btnAddDelete?.addEventListener('click', () => this.quickAddOperation('DELETE'));
        this.btnAddSelect?.addEventListener('click', () => this.quickAddOperation('SELECT'));
        
        // Step 7 - Export and validation
        this.btnExportFile?.addEventListener('click', () => this.exportSQLFile());
        this.btnCopySql?.addEventListener('click', () => this.copySQLToClipboard());
        this.btnExportTemplate?.addEventListener('click', () => this.exportAsTemplate());
        this.btnValidateSyntax?.addEventListener('click', () => this.validateSQLSyntax());
        
        // Keyboard shortcuts
        document.addEventListener('keydown', (e) => {
            if (e.ctrlKey && e.key === 'ArrowRight') {
                e.preventDefault();
                this.nextStep();
            } else if (e.ctrlKey && e.key === 'ArrowLeft') {
                e.preventDefault();
                this.previousStep();
            } else if (e.ctrlKey && e.key === 's') {
                e.preventDefault();
                this.saveState();
            }
        });
    }

    /**
     * Navigate to specific step
     * @param {number} step - Step number (1-8)
     */
    goToStep(step) {
        if (step < 1 || step > this.totalSteps) {
            return;
        }

        // Validate current step before moving
        if (step > this.currentStep && !this.validateCurrentStep()) {
            return;
        }

        // Hide current step
        document.getElementById(`step-${this.currentStep}`)?.classList.remove('active');
        document.querySelector(`.wizard-step[data-step="${this.currentStep}"]`)?.classList.remove('active');
        
        // Show new step
        this.currentStep = step;
        document.getElementById(`step-${this.currentStep}`)?.classList.add('active');
        document.querySelector(`.wizard-step[data-step="${this.currentStep}"]`)?.classList.add('active');
        
        // Mark completed steps
        for (let i = 1; i < this.currentStep; i++) {
            document.querySelector(`.wizard-step[data-step="${i}"]`)?.classList.add('completed');
        }
        
        // Update procedure state
        this.procedure.currentStep = this.currentStep;
        
        // Update UI elements
        this.updateNavigationButtons();
        this.updateStepContent();
        
        // Auto-save
        this.saveState();
    }

    /**
     * Move to next step
     */
    nextStep() {
        if (this.currentStep < this.totalSteps) {
            this.goToStep(this.currentStep + 1);
        }
    }

    /**
     * Move to previous step
     */
    previousStep() {
        if (this.currentStep > 1) {
            this.goToStep(this.currentStep - 1);
        }
    }

    /**
     * Validate current step before allowing navigation
     * @returns {boolean} True if step is valid
     */
    validateCurrentStep() {
        switch (this.currentStep) {
            case 1:
                return this.validateStep1();
            case 2:
                return this.validateStep2();
            default:
                return true;
        }
    }

    /**
     * Validate Step 1 (Procedure Name)
     * @returns {boolean}
     */
    validateStep1() {
        let isValid = true;

        // Validate name
        if (!this.validateProcedureName()) {
            isValid = false;
        }

        // Validate description
        if (!this.validateDescription()) {
            isValid = false;
        }

        // Update procedure model
        if (isValid) {
            this.procedure.name = this.procedureName.value.trim();
            this.procedure.description = this.procedureDescription.value.trim();
            this.procedure.author = this.procedureAuthor.value.trim() || 'Unknown';
        }

        return isValid;
    }

    /**
     * Validate procedure name format
     * @returns {boolean}
     */
    validateProcedureName() {
        const name = this.procedureName.value.trim();
        const pattern = /^[a-z]+_[a-z]+_[A-Z][a-zA-Z_]+$/;

        if (!name) {
            this.nameError.textContent = 'Procedure name is required';
            this.procedureName.style.borderColor = 'var(--color-error)';
            return false;
        }

        if (!pattern.test(name)) {
            this.nameError.textContent = 'Must follow pattern: domain_table_action (e.g., inv_inventory_Add_Item)';
            this.procedureName.style.borderColor = 'var(--color-error)';
            return false;
        }

        this.nameError.textContent = '';
        this.procedureName.style.borderColor = 'var(--color-success)';
        return true;
    }

    /**
     * Validate procedure description
     * @returns {boolean}
     */
    validateDescription() {
        const description = this.procedureDescription.value.trim();

        if (!description) {
            this.descriptionError.textContent = 'Description is required';
            this.procedureDescription.style.borderColor = 'var(--color-error)';
            return false;
        }

        if (description.length < 10) {
            this.descriptionError.textContent = 'Description must be at least 10 characters';
            this.procedureDescription.style.borderColor = 'var(--color-error)';
            return false;
        }

        this.descriptionError.textContent = '';
        this.procedureDescription.style.borderColor = 'var(--color-success)';
        return true;
    }

    /**
     * Validate Step 2 (Parameters)
     * @returns {boolean}
     */
    validateStep2() {
        // Step 2 is always valid (parameters are optional beyond mandatory ones)
        return true;
    }

    /**
     * Add parameter to procedure
     */
    addParameter() {
        const name = this.paramName.value.trim();
        const direction = this.paramDirection.value;
        const dataType = this.paramDataType.value;
        const length = parseInt(this.paramLength.value) || null;
        const description = this.paramDescription.value.trim();

        if (!name) {
            alert('Parameter name is required');
            return;
        }

        try {
            const param = new Parameter({
                name: name,
                direction: direction,
                dataType: dataType,
                length: length,
                description: description
            });

            this.procedure.addParameter(param);
            this.renderParametersList();
            this.clearParameterForm();
            
            // Show success feedback
            this.announceToScreenReader(`Parameter ${param.name} added`);
        } catch (error) {
            alert(`Error adding parameter: ${error.message}`);
        }
    }

    /**
     * Remove parameter from procedure
     * @param {string} paramName
     */
    removeParameter(paramName) {
        try {
            this.procedure.removeParameter(paramName);
            this.renderParametersList();
            this.announceToScreenReader(`Parameter ${paramName} removed`);
        } catch (error) {
            alert(`Error removing parameter: ${error.message}`);
        }
    }

    /**
     * Render parameters list
     */
    renderParametersList() {
        if (this.procedure.parameters.length === 0) {
            this.parametersList.innerHTML = '<p class="text-muted">No parameters added yet.</p>';
            return;
        }

        const html = this.procedure.parameters
            .sort((a, b) => a.order - b.order)
            .map(param => {
                const isMandatory = param.name === 'p_Status' || param.name === 'p_ErrorMsg';
                const removeBtn = isMandatory 
                    ? '<span class="badge badge-primary">Mandatory</span>'
                    : `<button class="btn btn-sm btn-link" onclick="window.wizardController.removeParameter('${param.name}')">Remove</button>`;
                
                return `
                    <div class="recent-item">
                        <div class="recent-item-info">
                            <h4>${param.name}</h4>
                            <p>
                                <strong>${param.direction}</strong> ${param.getTypeDeclaration()}
                                ${param.description ? `- ${param.description}` : ''}
                            </p>
                        </div>
                        <div class="recent-item-actions">
                            ${removeBtn}
                        </div>
                    </div>
                `;
            }).join('');

        this.parametersList.innerHTML = html;
    }

    /**
     * Clear parameter form
     */
    clearParameterForm() {
        this.paramName.value = '';
        this.paramDirection.value = 'IN';
        this.paramDataType.value = 'VARCHAR';
        this.paramLength.value = '50';
        this.paramDescription.value = '';
    }

    /**
     * Update parameter form based on selected data type
     */
    updateParameterForm() {
        const dataType = this.paramDataType.value;
        const requiresLength = ['VARCHAR', 'CHAR'].includes(dataType);
        
        if (requiresLength) {
            this.paramLengthGroup.style.display = 'block';
            this.paramLength.value = '50';
        } else {
            this.paramLengthGroup.style.display = 'none';
        }
    }

    /**
     * Update navigation buttons state
     */
    updateNavigationButtons() {
        this.btnPrev.disabled = this.currentStep === 1;
        
        if (this.currentStep === this.totalSteps) {
            this.btnNext.textContent = 'Finish';
        } else {
            this.btnNext.textContent = 'Next →';
        }
    }

    /**
     * Update step-specific content when navigating
     */
    updateStepContent() {
        switch (this.currentStep) {
            case 1:
                // Populate Step 1 fields if already filled
                if (this.procedure.name) {
                    this.procedureName.value = this.procedure.name;
                    this.procedureDescription.value = this.procedure.description;
                    this.procedureAuthor.value = this.procedure.author;
                }
                break;
                
            case 2:
                // Render parameters list
                this.renderParametersList();
                break;
                
            case 3:
                // Render validations list
                this.renderWizardValidations();
                break;
                
            case 4:
                // Render operations list
                this.renderWizardOperations();
                break;
                
            case 7:
                // Generate SQL preview
                this.updateSQLPreview();
                break;
        }
    }

    /**
     * Update SQL preview in Step 7
     */
    updateSQLPreview() {
        try {
            // Generate SQL
            const sql = this.generateSimpleSQL();
            this.sqlPreview.textContent = sql;
            
            // Apply syntax highlighting
            if (window.Prism) {
                Prism.highlightElement(this.sqlPreview);
            }
            
            // Update statistics
            this.updateSQLStatistics(sql);
            
            // Reset validation state
            this.clearValidationResults();
            
        } catch (error) {
            this.sqlPreview.textContent = `-- Error generating SQL: ${error.message}`;
        }
    }

    /**
     * Generate SQL using SQLGenerator
     * @returns {string}
     */
    generateSimpleSQL() {
        try {
            return this.sqlGenerator.generate(this.procedure);
        } catch (error) {
            console.error('Error generating SQL:', error);
            return `-- Error generating SQL: ${error.message}`;
        }
    }

    /**
     * Update SQL statistics display
     * @param {string} sql - SQL content
     */
    updateSQLStatistics(sql) {
        if (!this.statLines) return;
        
        const stats = exportManager.getStatistics(sql);
        
        this.statLines.textContent = stats.lines;
        this.statSize.textContent = stats.sizeFormatted;
        this.statParameters.textContent = stats.parameters;
        this.statOperations.textContent = stats.operations;
        this.statValidations.textContent = stats.validations;
    }

    /**
     * Export SQL to file
     */
    async exportSQLFile() {
        try {
            const sql = this.generateSimpleSQL();
            
            const success = await exportManager.exportToFile(sql, this.procedure.name, {
                description: this.procedure.description,
                author: this.procedure.author,
                includeDrop: true,
                includeDelimiter: true,
                includeHeader: true
            });
            
            if (success) {
                // Success message already shown by exportManager
            }
            
        } catch (error) {
            showError({
                error_type: 'export',
                user_message: 'Failed to export SQL file',
                technical_detail: error.message
            });
        }
    }

    /**
     * Copy SQL to clipboard
     */
    async copySQLToClipboard() {
        try {
            const sql = this.generateSimpleSQL();
            
            await exportManager.copyToClipboard(sql, this.procedure.name, {
                description: this.procedure.description,
                author: this.procedure.author,
                includeDrop: false,
                includeDelimiter: true,
                includeHeader: false
            });
            
        } catch (error) {
            showError({
                error_type: 'clipboard',
                user_message: 'Failed to copy to clipboard',
                technical_detail: error.message
            });
        }
    }

    /**
     * Export as template with placeholders
     */
    async exportAsTemplate() {
        try {
            const sql = this.generateSimpleSQL();
            
            await exportManager.exportAsTemplate(sql, this.procedure.name);
            
        } catch (error) {
            showError({
                error_type: 'export',
                user_message: 'Failed to export template',
                technical_detail: error.message
            });
        }
    }

    /**
     * Validate SQL syntax
     */
    validateSQLSyntax() {
        try {
            const sql = this.generateSimpleSQL();
            const result = exportManager.validateSQL(sql);
            
            this.displayValidationResults(result);
            
        } catch (error) {
            showError({
                error_type: 'validation',
                user_message: 'Failed to validate SQL',
                technical_detail: error.message
            });
        }
    }

    /**
     * Display validation results
     * @param {Object} result - Validation result from exportManager
     */
    displayValidationResults(result) {
        this.clearValidationResults();
        
        if (result.errors.length > 0) {
            // Show errors
            this.syntaxErrors.style.display = 'block';
            this.errorList.innerHTML = result.errors.map(err => `<li>${err}</li>`).join('');
        }
        
        if (result.warnings.length > 0) {
            // Show warnings
            this.syntaxWarnings.style.display = 'block';
            this.warningList.innerHTML = result.warnings.map(warn => `<li>${warn}</li>`).join('');
        }
        
        if (result.valid && result.warnings.length === 0) {
            // Show success
            this.syntaxSuccess.style.display = 'block';
        }
    }

    /**
     * Clear validation results
     */
    clearValidationResults() {
        if (this.syntaxErrors) this.syntaxErrors.style.display = 'none';
        if (this.syntaxWarnings) this.syntaxWarnings.style.display = 'none';
        if (this.syntaxSuccess) this.syntaxSuccess.style.display = 'none';
    }

    /**
     * Save current state to localStorage
     */
    saveState() {
        try {
            const state = this.procedure.toJSON();
            this.storageManager.saveState(state);
            
            // Also save version if procedure has a name
            if (this.procedure.name) {
                this.storageManager.saveVersion(this.procedure.name, state);
            }
            
            this.announceToScreenReader('Progress saved');
            console.log('State saved to localStorage');
        } catch (error) {
            console.error('Error saving state:', error);
            alert('Failed to save progress. Browser storage may be full.');
        }
    }

    /**
     * Load saved state from localStorage
     */
    loadSavedState() {
        try {
            const savedState = this.storageManager.loadState();
            if (savedState) {
                this.procedure = ProcedureDefinition.fromJSON(savedState);
                this.currentStep = this.procedure.currentStep || 1;
                console.log('Loaded saved state from localStorage');
            }
        } catch (error) {
            console.error('Error loading saved state:', error);
        }
    }

    /**
     * Update entire UI
     */
    updateUI() {
        this.goToStep(this.currentStep);
        this.updateNavigationButtons();
        this.updateStepContent();
    }

    /**
     * Announce message to screen readers
     * @param {string} message
     */
    announceToScreenReader(message) {
        const announcer = document.getElementById('sr-announcer');
        if (announcer) {
            announcer.textContent = message;
        }
    }

    /**
     * Quick add validation rule from wizard
     * @param {string} ruleType - Validation rule type
     */
    quickAddValidation(ruleType) {
        // Get first IN parameter for smart default
        const firstParam = this.procedure.parameters.find(p => p.direction === 'IN');
        const paramName = firstParam ? firstParam.name : 'p_Value';
        
        // Create validation rule with smart defaults
        const validation = new ValidationRule({
            type: ruleType,
            parameterName: paramName,
            errorMessage: this.getSmartErrorMessage(ruleType, paramName),
            errorStatusCode: -1,
            order: this.procedure.validations.length
        });
        
        this.procedure.validations.push(validation);
        this.renderWizardValidations();
        this.saveState();
        showSuccess(`${this.getRuleTypeName(ruleType)} validation added`);
    }

    /**
     * Get smart error message for validation rule
     * @param {string} ruleType - Rule type
     * @param {string} paramName - Parameter name
     * @returns {string} Error message
     */
    getSmartErrorMessage(ruleType, paramName) {
        // Remove p_ prefix for display
        const displayName = paramName.replace(/^p_/, '').replace(/_/g, ' ');
        
        switch (ruleType) {
            case 'REQUIRED_FIELD':
                return `${displayName} is required`;
            case 'POSITIVE_NUMBER':
                return `${displayName} must be a positive number`;
            case 'DATE_RANGE':
                return `${displayName} must be within valid date range`;
            case 'STRING_LENGTH':
                return `${displayName} length is invalid`;
            case 'FOREIGN_KEY_CHECK':
                return `${displayName} reference not found`;
            case 'ENUM_VALUE':
                return `${displayName} must be a valid value`;
            case 'CUSTOM_CONDITION':
                return `${displayName} validation failed`;
            default:
                return 'Validation failed';
        }
    }

    /**
     * Get human-readable rule type name
     * @param {string} ruleType - Rule type constant
     * @returns {string} Display name
     */
    getRuleTypeName(ruleType) {
        return ruleType.split('_').map(word => 
            word.charAt(0) + word.slice(1).toLowerCase()
        ).join(' ');
    }

    /**
     * Render validations list in wizard
     */
    renderWizardValidations() {
        if (!this.validationsList) return;
        
        if (this.procedure.validations.length === 0) {
            if (this.noValidationsMsg) {
                this.noValidationsMsg.style.display = 'block';
            }
            if (this.validationsCount) {
                this.validationsCount.textContent = '0';
            }
            return;
        }
        
        if (this.noValidationsMsg) {
            this.noValidationsMsg.style.display = 'none';
        }
        
        if (this.validationsCount) {
            this.validationsCount.textContent = this.procedure.validations.length.toString();
        }
        
        // Build validations list HTML
        let html = '<div style="display: flex; flex-direction: column; gap: var(--spacing-md);">';
        
        this.procedure.validations.forEach((validation, index) => {
            const ruleIcon = this.getRuleIcon(validation.type);
            const ruleName = this.getRuleTypeName(validation.type);
            
            html += `
                <div style="display: flex; justify-content: space-between; align-items: center; padding: var(--spacing-md); background-color: var(--color-bg-secondary); border-radius: var(--radius-md);">
                    <div style="display: flex; align-items: center; gap: var(--spacing-md);">
                        <span style="font-weight: var(--font-weight-medium);">${index + 1}.</span>
                        <span style="font-size: 1.5rem;">${ruleIcon}</span>
                        <div>
                            <div style="font-weight: var(--font-weight-medium);">${ruleName}</div>
                            <div style="font-size: var(--font-size-sm); color: var(--color-text-secondary);">${validation.parameterName}: ${validation.errorMessage}</div>
                        </div>
                    </div>
                    <div style="display: flex; gap: var(--spacing-sm);">
                        <button type="button" class="btn btn-sm btn-outline-secondary" onclick="window.wizardController.moveValidation(${index}, -1)" ${index === 0 ? 'disabled' : ''}>↑</button>
                        <button type="button" class="btn btn-sm btn-outline-secondary" onclick="window.wizardController.moveValidation(${index}, 1)" ${index === this.procedure.validations.length - 1 ? 'disabled' : ''}>↓</button>
                        <button type="button" class="btn btn-sm btn-outline-danger" onclick="window.wizardController.deleteValidation(${index})">🗑️</button>
                    </div>
                </div>
            `;
        });
        
        html += '</div>';
        
        // Find or create container
        const container = this.validationsList.querySelector('.validations-container') || 
                         document.createElement('div');
        container.className = 'validations-container';
        container.innerHTML = html;
        
        if (!this.validationsList.contains(container)) {
            this.validationsList.appendChild(container);
        }
    }

    /**
     * Get icon for validation rule type
     * @param {string} ruleType - Rule type
     * @returns {string} Icon emoji
     */
    getRuleIcon(ruleType) {
        switch (ruleType) {
            case 'REQUIRED_FIELD': return '✓';
            case 'POSITIVE_NUMBER': return '+';
            case 'DATE_RANGE': return '📅';
            case 'STRING_LENGTH': return '📏';
            case 'FOREIGN_KEY_CHECK': return '🔗';
            case 'ENUM_VALUE': return '📋';
            case 'CUSTOM_CONDITION': return '⚙️';
            default: return '❓';
        }
    }

    /**
     * Move validation up or down
     * @param {number} index - Validation index
     * @param {number} direction - -1 for up, 1 for down
     */
    moveValidation(index, direction) {
        const newIndex = index + direction;
        
        if (newIndex < 0 || newIndex >= this.procedure.validations.length) {
            return;
        }
        
        // Swap validations
        const temp = this.procedure.validations[index];
        this.procedure.validations[index] = this.procedure.validations[newIndex];
        this.procedure.validations[newIndex] = temp;
        
        // Update order property
        this.procedure.validations.forEach((v, i) => v.order = i);
        
        this.renderWizardValidations();
        this.saveState();
        showSuccess('Validation reordered');
    }

    /**
     * Delete validation by index
     * @param {number} index - Validation index
     */
    deleteValidation(index) {
        if (confirm('Are you sure you want to delete this validation rule?')) {
            this.procedure.validations.splice(index, 1);
            
            // Update order property
            this.procedure.validations.forEach((v, i) => v.order = i);
            
            this.renderWizardValidations();
            this.saveState();
            showSuccess('Validation deleted');
        }
    }

    /**
     * Quick add operation from wizard
     * @param {string} type - Operation type (INSERT, UPDATE, DELETE, SELECT)
     */
    quickAddOperation(type) {
        const operation = new DMLOperation({ type, order: this.procedure.operations.length });
        this.procedure.operations.push(operation);
        this.renderWizardOperations();
        this.saveState();
        showSuccess(`${type} operation added`);
    }

    /**
     * Render operations list in wizard
     */
    renderWizardOperations() {
        if (!this.operationsList) return;
        
        if (this.procedure.operations.length === 0) {
            if (this.noOperationsMsg) {
                this.noOperationsMsg.style.display = 'block';
            }
            if (this.operationsCount) {
                this.operationsCount.textContent = '0';
            }
            return;
        }
        
        if (this.noOperationsMsg) {
            this.noOperationsMsg.style.display = 'none';
        }
        
        if (this.operationsCount) {
            this.operationsCount.textContent = this.procedure.operations.length.toString();
        }
        
        // Build operations list HTML
        let html = '<div style="display: flex; flex-direction: column; gap: var(--spacing-md);">';
        
        this.procedure.operations.forEach((op, index) => {
            const typeBadge = `<span class="operation-type-badge ${op.type}" style="padding: 0.25rem 0.75rem; border-radius: var(--radius-sm); font-weight: var(--font-weight-medium); font-size: var(--font-size-sm);">${op.type}</span>`;
            const tableText = op.targetTable || '<em style="color: var(--color-text-secondary);">(configure in full builder)</em>';
            
            html += `
                <div style="display: flex; justify-content: space-between; align-items: center; padding: var(--spacing-md); background-color: var(--color-bg-secondary); border-radius: var(--radius-md);">
                    <div>
                        <span style="font-weight: var(--font-weight-medium); margin-right: var(--spacing-md);">${index + 1}.</span>
                        ${typeBadge}
                        <span style="margin-left: var(--spacing-md);">${tableText}</span>
                    </div>
                    <button type="button" class="btn btn-sm btn-outline-danger" onclick="window.wizardController.deleteOperation(${index})">
                        🗑️
                    </button>
                </div>
            `;
        });
        
        html += '</div>';
        
        // Find or create container
        const container = this.operationsList.querySelector('.operations-container') || 
                         document.createElement('div');
        container.className = 'operations-container';
        container.innerHTML = html;
        
        if (!this.operationsList.contains(container)) {
            this.operationsList.appendChild(container);
        }
    }

    /**
     * Delete operation by index
     * @param {number} index - Operation index
     */
    deleteOperation(index) {
        if (confirm('Are you sure you want to delete this operation?')) {
            this.procedure.operations.splice(index, 1);
            this.renderWizardOperations();
            this.saveState();
            showSuccess('Operation deleted');
        }
    }

    /**
     * Initialize database metadata
     */
    async initializeMetadata() {
        const refreshBtn = document.getElementById('refresh-metadata-btn');
        const timestampEl = document.getElementById('metadata-timestamp');
        const staleEl = document.getElementById('metadata-stale');
        
        if (!refreshBtn) return;
        
        // Update display
        const updateMetadataDisplay = () => {
            if (dbMetadata.fetchedAt) {
                timestampEl.textContent = dbMetadata.getAgeDisplay();
                
                if (dbMetadata.isStale()) {
                    staleEl.style.display = 'inline';
                } else {
                    staleEl.style.display = 'none';
                }
            } else {
                timestampEl.textContent = 'Not loaded';
                staleEl.style.display = 'none';
            }
        };
        
        // Refresh button handler
        refreshBtn.addEventListener('click', async () => {
            refreshBtn.disabled = true;
            refreshBtn.innerHTML = '⏳ Refreshing...';
            
            const result = await dbMetadata.refresh();
            
            if (result.success) {
                showSuccess(`Refreshed ${dbMetadata.tables.length} tables`);
                updateMetadataDisplay();
            } else {
                showError({
                    error_type: 'database',
                    user_message: 'Failed to refresh metadata',
                    technical_detail: result.error
                });
            }
            
            refreshBtn.disabled = false;
            refreshBtn.innerHTML = '🔄 Refresh Metadata';
        });
        
        // Initial load if not cached
        if (dbMetadata.tables.length === 0) {
            const result = await dbMetadata.fetchTables();
            if (result.success) {
                console.log(`[Wizard] Loaded ${dbMetadata.tables.length} tables`);
            }
        }
        
        updateMetadataDisplay();
        
        // Update display every minute
        setInterval(updateMetadataDisplay, 60000);
    }
}

// Initialize wizard when DOM is ready
document.addEventListener('DOMContentLoaded', () => {
    window.wizardController = new WizardController();
    
    // Initialize metadata after wizard
    window.wizardController.initializeMetadata();
});
