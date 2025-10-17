/**
 * Wizard Controller for Stored Procedure Builder
 * 
 * Manages wizard navigation, state persistence, and step validation
 */

import { ProcedureDefinition, Parameter, DATA_TYPES } from './procedure-model.js';
import { storageManager } from './storage-manager.js';
import { sqlGenerator } from './sql-generator.js';

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
        this.paramLengthGroup = document.getElementById('param-length-group');
        
        // Step 7 elements
        this.sqlPreview = document.getElementById('sql-preview');
        this.btnValidateSyntax = document.getElementById('btn-validate-syntax');
        this.btnCopySql = document.getElementById('btn-copy-sql');
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
            // Simple SQL generation for now (full generator comes later)
            const sql = this.generateSimpleSQL();
            this.sqlPreview.textContent = sql;
            
            // Apply syntax highlighting
            if (window.Prism) {
                Prism.highlightElement(this.sqlPreview);
            }
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
}

// Initialize wizard when DOM is ready
document.addEventListener('DOMContentLoaded', () => {
    window.wizardController = new WizardController();
});
