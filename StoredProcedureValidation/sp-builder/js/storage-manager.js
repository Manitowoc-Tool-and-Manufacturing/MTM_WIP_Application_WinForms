/**
 * Storage Manager for Stored Procedure Builder
 * 
 * Handles localStorage persistence, auto-save, version history, and session restoration
 */

export class StorageManager {
    constructor() {
        this.MAX_VERSIONS = 5;
        this.AUTO_SAVE_INTERVAL = 30000; // 30 seconds
        this.STORAGE_KEYS = {
            STATE: 'sp_builder_state',
            AUTOSAVE: 'sp_builder_autosave',
            VERSIONS: 'sp_versions_',
            METADATA: 'sp_database_metadata',
            CUSTOM_TEMPLATES: 'sp_custom_templates',
            EXPORT_CONFIG: 'sp_export_config'
        };
        
        this.autoSaveTimer = null;
    }

    /**
     * Save procedure version to localStorage
     * @param {string} procedureName - Procedure name
     * @param {object} procedureData - Procedure data (from toJSON())
     * @returns {boolean} Success
     */
    saveVersion(procedureName, procedureData) {
        try {
            const key = `${this.STORAGE_KEYS.VERSIONS}${procedureName}`;
            const versions = this.getVersions(procedureName);
            
            // Create new version entry
            const newVersion = {
                timestamp: Date.now(),
                data: procedureData,
                hash: this._hashProcedure(procedureData)
            };
            
            // Check if this is a duplicate of the most recent version
            if (versions.length > 0 && versions[0].hash === newVersion.hash) {
                console.log('Skipping duplicate version save');
                return true;
            }
            
            // Add new version at the beginning
            versions.unshift(newVersion);
            
            // Prune to MAX_VERSIONS
            if (versions.length > this.MAX_VERSIONS) {
                versions.splice(this.MAX_VERSIONS);
            }
            
            localStorage.setItem(key, JSON.stringify(versions));
            console.log(`Saved version for ${procedureName}. Total versions: ${versions.length}`);
            return true;
        } catch (error) {
            if (error.name === 'QuotaExceededError') {
                console.error('localStorage quota exceeded');
                this._handleQuotaExceeded();
                return false;
            }
            console.error('Error saving version:', error);
            return false;
        }
    }

    /**
     * Get all versions for a procedure
     * @param {string} procedureName - Procedure name
     * @returns {array} Array of version objects
     */
    getVersions(procedureName) {
        try {
            const key = `${this.STORAGE_KEYS.VERSIONS}${procedureName}`;
            const data = localStorage.getItem(key);
            return data ? JSON.parse(data) : [];
        } catch (error) {
            console.error('Error getting versions:', error);
            return [];
        }
    }

    /**
     * Compare two versions and generate diff
     * @param {string} procedureName - Procedure name
     * @param {number} version1Idx - Index of first version
     * @param {number} version2Idx - Index of second version
     * @returns {object} Diff object
     */
    compareVersions(procedureName, version1Idx, version2Idx) {
        const versions = this.getVersions(procedureName);
        
        if (version1Idx >= versions.length || version2Idx >= versions.length) {
            throw new Error('Invalid version indices');
        }
        
        const v1 = versions[version1Idx].data;
        const v2 = versions[version2Idx].data;
        
        return {
            timestamp1: versions[version1Idx].timestamp,
            timestamp2: versions[version2Idx].timestamp,
            parameterChanges: this._compareArrays(v1.parameters, v2.parameters),
            validationChanges: this._compareArrays(v1.validations, v2.validations),
            operationChanges: this._compareArrays(v1.operations, v2.operations),
            metadataChanges: {
                nameChanged: v1.name !== v2.name,
                descriptionChanged: v1.description !== v2.description
            }
        };
    }

    /**
     * Setup auto-save functionality
     * @param {function} getCurrentProcedure - Function that returns current ProcedureDefinition
     */
    autoSaveSetup(getCurrentProcedure) {
        // Clear existing timer
        if (this.autoSaveTimer) {
            clearInterval(this.autoSaveTimer);
        }
        
        // Setup new timer
        this.autoSaveTimer = setInterval(() => {
            try {
                const procedure = getCurrentProcedure();
                if (procedure && procedure.name) {
                    const state = procedure.toJSON();
                    
                    localStorage.setItem(this.STORAGE_KEYS.AUTOSAVE, JSON.stringify({
                        timestamp: Date.now(),
                        procedure: state
                    }));
                    
                    console.log('Auto-saved at', new Date().toLocaleTimeString());
                }
            } catch (error) {
                console.error('Auto-save failed:', error);
            }
        }, this.AUTO_SAVE_INTERVAL);
        
        console.log('Auto-save enabled (every 30 seconds)');
    }

    /**
     * Stop auto-save
     */
    stopAutoSave() {
        if (this.autoSaveTimer) {
            clearInterval(this.autoSaveTimer);
            this.autoSaveTimer = null;
            console.log('Auto-save disabled');
        }
    }

    /**
     * Restore session from auto-save
     * @returns {object|null} Session data with prompt message
     */
    restoreSession() {
        try {
            const autosave = localStorage.getItem(this.STORAGE_KEYS.AUTOSAVE);
            if (!autosave) {
                return null;
            }
            
            const { timestamp, procedure } = JSON.parse(autosave);
            const age = Date.now() - timestamp;
            const ageHours = Math.floor(age / 3600000);
            const ageMinutes = Math.floor((age % 3600000) / 60000);
            
            // Format age string
            let ageString;
            if (ageHours > 0) {
                ageString = `${ageHours} hour${ageHours > 1 ? 's' : ''}`;
            } else {
                ageString = `${ageMinutes} minute${ageMinutes > 1 ? 's' : ''}`;
            }
            
            const stepName = this._getStepName(procedure.currentStep || 1);
            
            return {
                procedure,
                timestamp,
                age,
                promptMessage: `Resume '${procedure.name}'? Last edited: ${ageString} ago (Step ${procedure.currentStep || 1}: ${stepName})`
            };
        } catch (error) {
            console.error('Error restoring session:', error);
            return null;
        }
    }

    /**
     * Clear auto-save data
     */
    clearAutoSave() {
        localStorage.removeItem(this.STORAGE_KEYS.AUTOSAVE);
        console.log('Auto-save data cleared');
    }

    /**
     * Save current state
     * @param {object} procedureData - Procedure data from toJSON()
     */
    saveState(procedureData) {
        try {
            localStorage.setItem(this.STORAGE_KEYS.STATE, JSON.stringify(procedureData));
            return true;
        } catch (error) {
            console.error('Error saving state:', error);
            return false;
        }
    }

    /**
     * Load saved state
     * @returns {object|null} Procedure data
     */
    loadState() {
        try {
            const data = localStorage.getItem(this.STORAGE_KEYS.STATE);
            return data ? JSON.parse(data) : null;
        } catch (error) {
            console.error('Error loading state:', error);
            return null;
        }
    }

    /**
     * Clear all storage data (with confirmation)
     * @returns {boolean} Success
     */
    clearAllData() {
        try {
            Object.values(this.STORAGE_KEYS).forEach(key => {
                if (key.endsWith('_')) {
                    // Clear all keys with this prefix
                    Object.keys(localStorage).forEach(storageKey => {
                        if (storageKey.startsWith(key)) {
                            localStorage.removeItem(storageKey);
                        }
                    });
                } else {
                    localStorage.removeItem(key);
                }
            });
            
            console.log('All storage data cleared');
            return true;
        } catch (error) {
            console.error('Error clearing data:', error);
            return false;
        }
    }

    /**
     * Get storage usage information
     * @returns {object} Storage usage stats
     */
    getStorageInfo() {
        try {
            let totalSize = 0;
            const items = {};
            
            Object.keys(localStorage).forEach(key => {
                const value = localStorage.getItem(key);
                const size = new Blob([value]).size;
                totalSize += size;
                items[key] = size;
            });
            
            // Estimate limit (typically 5-10MB, use 5MB as safe estimate)
            const estimatedLimit = 5 * 1024 * 1024;
            const percentUsed = (totalSize / estimatedLimit) * 100;
            
            return {
                totalSize,
                totalSizeFormatted: this._formatBytes(totalSize),
                estimatedLimit,
                estimatedLimitFormatted: this._formatBytes(estimatedLimit),
                percentUsed: percentUsed.toFixed(1),
                items
            };
        } catch (error) {
            console.error('Error getting storage info:', error);
            return null;
        }
    }

    /**
     * Handle localStorage quota exceeded
     * @private
     */
    _handleQuotaExceeded() {
        console.warn('localStorage quota exceeded, pruning old data...');
        
        // Strategy: Remove oldest versions from all procedures
        const versionKeys = Object.keys(localStorage).filter(k => 
            k.startsWith(this.STORAGE_KEYS.VERSIONS)
        );
        
        versionKeys.forEach(key => {
            try {
                const versions = JSON.parse(localStorage.getItem(key));
                if (versions.length > 2) {
                    // Keep only 2 most recent versions
                    versions.splice(2);
                    localStorage.setItem(key, JSON.stringify(versions));
                }
            } catch (error) {
                console.error(`Error pruning versions for ${key}:`, error);
            }
        });
        
        // Show warning to user
        const event = new CustomEvent('storage-warning', {
            detail: {
                message: 'Storage nearly full. Older versions have been removed.',
                severity: 'warning'
            }
        });
        document.dispatchEvent(event);
    }

    /**
     * Generate hash of procedure data for duplicate detection
     * @param {object} procedureData
     * @returns {string} Hash string
     * @private
     */
    _hashProcedure(procedureData) {
        // Simple hash based on key properties
        const keyData = JSON.stringify({
            name: procedureData.name,
            parameters: procedureData.parameters,
            operations: procedureData.operations,
            validations: procedureData.validations
        });
        
        // Simple string hash
        let hash = 0;
        for (let i = 0; i < keyData.length; i++) {
            const char = keyData.charCodeAt(i);
            hash = ((hash << 5) - hash) + char;
            hash = hash & hash; // Convert to 32-bit integer
        }
        
        return hash.toString(36);
    }

    /**
     * Compare two arrays for changes
     * @param {array} arr1
     * @param {array} arr2
     * @returns {object} Change summary
     * @private
     */
    _compareArrays(arr1, arr2) {
        return {
            added: arr2.length - arr1.length,
            removed: arr1.length - arr2.length,
            lengthChanged: arr1.length !== arr2.length
        };
    }

    /**
     * Get human-readable step name
     * @param {number} step - Step number
     * @returns {string} Step name
     * @private
     */
    _getStepName(step) {
        const steps = [
            'Name',
            'Parameters',
            'Validation',
            'Operations',
            'Flow Diagram',
            'Advanced',
            'Preview',
            'Export'
        ];
        
        return steps[step - 1] || 'Unknown';
    }

    /**
     * Format bytes to human-readable string
     * @param {number} bytes
     * @returns {string}
     * @private
     */
    _formatBytes(bytes) {
        if (bytes === 0) return '0 Bytes';
        
        const k = 1024;
        const sizes = ['Bytes', 'KB', 'MB', 'GB'];
        const i = Math.floor(Math.log(bytes) / Math.log(k));
        
        return Math.round((bytes / Math.pow(k, i)) * 100) / 100 + ' ' + sizes[i];
    }
}

// Export singleton instance
export const storageManager = new StorageManager();
