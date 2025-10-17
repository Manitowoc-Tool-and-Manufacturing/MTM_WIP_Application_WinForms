/**
 * Main Application Controller for Stored Procedure Builder
 * 
 * Handles homepage interactions, session restoration, and navigation
 */

import { ProcedureDefinition } from './procedure-model.js';
import { storageManager } from './storage-manager.js';
import { sqlParser } from './sql-parser.js';
import { showError, showSuccess } from './utils.js';

class AppController {
    constructor() {
        this.storageManager = storageManager;
        this.initializeElements();
        this.attachEventListeners();
        this.checkForSavedSession();
    }

    /**
     * Initialize DOM element references
     */
    initializeElements() {
        this.btnNewProcedure = document.getElementById('btn-new-procedure');
        this.btnLoadTemplate = document.getElementById('btn-load-template');
        this.btnImportSql = document.getElementById('btn-import-sql');
        this.btnResumeSession = document.getElementById('btn-resume-session');
        this.btnDiscardSession = document.getElementById('btn-discard-session');
        this.resumeCard = document.getElementById('resume-card');
        this.resumeMessage = document.getElementById('resume-message');
        this.recentSection = document.getElementById('recent-section');
        this.recentList = document.getElementById('recent-list');
        
        // Modal elements
        this.importModal = document.getElementById('import-modal');
        this.sqlInput = document.getElementById('sql-input');
        this.fileInput = document.getElementById('file-input');
        this.btnUploadFile = document.getElementById('btn-upload-file');
        this.btnParseSql = document.getElementById('btn-parse-sql');
        this.fileName = document.getElementById('file-name');
        
        const modalClose = document.querySelector('.modal-close');
        if (modalClose) {
            modalClose.addEventListener('click', () => this.closeModal());
        }
    }

    /**
     * Attach event listeners
     */
    attachEventListeners() {
        this.btnNewProcedure?.addEventListener('click', () => this.startNewProcedure());
        this.btnLoadTemplate?.addEventListener('click', () => this.loadTemplate());
        this.btnImportSql?.addEventListener('click', () => this.showImportModal());
        this.btnResumeSession?.addEventListener('click', () => this.resumeSession());
        this.btnDiscardSession?.addEventListener('click', () => this.discardSession());
        
        // Import modal
        this.btnUploadFile?.addEventListener('click', () => this.fileInput.click());
        this.fileInput?.addEventListener('change', (e) => this.handleFileUpload(e));
        this.btnParseSql?.addEventListener('click', () => this.parseSqlInput());
        
        // Close modal on backdrop click
        this.importModal?.addEventListener('click', (e) => {
            if (e.target === this.importModal) {
                this.closeModal();
            }
        });
    }

    /**
     * Check for saved session and show resume card
     */
    checkForSavedSession() {
        const session = this.storageManager.restoreSession();
        
        if (session) {
            this.resumeMessage.textContent = session.promptMessage;
            this.resumeCard.style.display = 'block';
        } else {
            this.resumeCard.style.display = 'none';
        }
        
        // TODO: Load recent procedures
        // this.loadRecentProcedures();
    }

    /**
     * Start new procedure - navigate to wizard
     */
    startNewProcedure() {
        // Clear any existing state
        this.storageManager.clearAutoSave();
        this.storageManager.saveState(null);
        
        // Navigate to wizard
        window.location.href = 'wizard.html';
    }

    /**
     * Resume previous session
     */
    resumeSession() {
        const session = this.storageManager.restoreSession();
        
        if (session) {
            // Save to main state
            this.storageManager.saveState(session.procedure);
            
            // Navigate to wizard
            window.location.href = 'wizard.html';
        } else {
            alert('No session to resume');
        }
    }

    /**
     * Discard saved session
     */
    discardSession() {
        if (confirm('Are you sure you want to discard your saved session? This cannot be undone.')) {
            this.storageManager.clearAutoSave();
            this.resumeCard.style.display = 'none';
        }
    }

    /**
     * Load template - navigate to templates page
     */
    loadTemplate() {
        window.location.href = 'templates.html';
    }

    /**
     * Show import SQL modal
     */
    showImportModal() {
        this.importModal.style.display = 'flex';
    }

    /**
     * Close import modal
     */
    closeModal() {
        this.importModal.style.display = 'none';
        this.sqlInput.value = '';
        this.fileName.textContent = '';
    }

    /**
     * Handle file upload
     * @param {Event} event
     */
    handleFileUpload(event) {
        const file = event.target.files[0];
        if (!file) return;
        
        this.fileName.textContent = `Selected: ${file.name}`;
        
        const reader = new FileReader();
        reader.onload = (e) => {
            this.sqlInput.value = e.target.result;
        };
        reader.readAsText(file);
    }

    /**
     * Parse SQL input
     */
    async parseSqlInput() {
        const sql = this.sqlInput.value.trim();
        
        if (!sql) {
            showError({
                error_type: 'validation',
                user_message: 'Please enter SQL code or upload a file'
            });
            return;
        }
        
        // Show loading
        const spinner = document.getElementById('loading-spinner');
        if (spinner) spinner.style.display = 'flex';
        
        try {
            // Parse SQL using SQLParser
            const result = sqlParser.parse(sql);
            
            if (!result.success) {
                throw new Error(result.error);
            }
            
            // Show warnings if any
            if (result.warnings && result.warnings.length > 0) {
                console.warn('Parser warnings:', result.warnings);
                // Show warnings to user (non-blocking)
                const warningsMsg = result.warnings.join('\n');
                console.log(`Parsed with warnings:\n${warningsMsg}`);
            }
            
            // Save procedure and navigate to wizard
            this.storageManager.saveState(result.procedure.toJSON());
            
            showSuccess(`Successfully imported procedure: ${result.procedure.name}`);
            
            // Navigate to wizard after short delay
            setTimeout(() => {
                this.closeModal();
                window.location.href = 'wizard.html';
            }, 1000);
            
        } catch (error) {
            showError({
                error_type: 'parse',
                user_message: 'Failed to parse SQL',
                technical_detail: error.message
            });
        } finally {
            // Hide loading
            if (spinner) spinner.style.display = 'none';
        }
    }

    /**
     * Load recent procedures
     */
    loadRecentProcedures() {
        // TODO: Implement recent procedures list from localStorage
        // For now, hide the section
        this.recentSection.style.display = 'none';
    }
}

// Initialize app when DOM is ready
document.addEventListener('DOMContentLoaded', () => {
    window.appController = new AppController();
});
