/**
 * Database Metadata Manager
 * 
 * Fetches and caches table/column metadata from MySQL information_schema
 * via PHP backend. Implements 10-minute staleness detection and refresh.
 * 
 * @module database-metadata
 */

export class DatabaseMetadata {
    constructor(apiBaseUrl = '../api') {
        this.apiBaseUrl = apiBaseUrl;
        this.database = 'mtm_wip_application_winforms_test';
        this.tables = [];
        this.fetchedAt = null;
        this.stale = true;
        this.STALE_THRESHOLD_MS = 10 * 60 * 1000; // 10 minutes
        
        // Attempt to load from cache
        this.loadFromCache();
    }
    
    /**
     * Fetch tables from database via PHP API
     * @returns {Promise<Object>} Tables array with metadata
     */
    async fetchTables() {
        try {
            const response = await fetch(`${this.apiBaseUrl}/get-tables.php`);
            const result = await response.json();
            
            if (!result.success) {
                throw new Error(result.user_message || 'Failed to fetch tables');
            }
            
            this.tables = result.data.tables || [];
            this.database = result.data.database;
            this.fetchedAt = new Date();
            this.stale = false;
            
            // Cache in localStorage
            this.saveToCache();
            
            return {
                success: true,
                tables: this.tables,
                database: this.database
            };
            
        } catch (error) {
            console.error('[DatabaseMetadata] fetchTables error:', error);
            return {
                success: false,
                error: error.message,
                cached: this.tables.length > 0 // Indicate if we have cached data
            };
        }
    }
    
    /**
     * Fetch columns for a specific table
     * @param {string} tableName - Table name to fetch columns for
     * @returns {Promise<Object>} Columns array with metadata
     */
    async fetchColumns(tableName) {
        try {
            const response = await fetch(
                `${this.apiBaseUrl}/get-columns.php?table=${encodeURIComponent(tableName)}`
            );
            const result = await response.json();
            
            if (!result.success) {
                throw new Error(result.user_message || 'Failed to fetch columns');
            }
            
            const columns = result.data || [];
            
            // Update cached table with column data
            const table = this.tables.find(t => t.name === tableName);
            if (table) {
                table.columns = columns;
                table.columnsFetched = true;
                this.saveToCache();
            }
            
            return {
                success: true,
                columns: columns
            };
            
        } catch (error) {
            console.error('[DatabaseMetadata] fetchColumns error:', error);
            return {
                success: false,
                error: error.message
            };
        }
    }
    
    /**
     * Get table by name from cache
     * @param {string} tableName - Table name
     * @returns {Object|null} Table metadata or null if not found
     */
    getTable(tableName) {
        return this.tables.find(t => t.name === tableName) || null;
    }
    
    /**
     * Get columns for table from cache (fetches if not cached)
     * @param {string} tableName - Table name
     * @returns {Promise<Array>} Array of column metadata
     */
    async getColumns(tableName) {
        const table = this.getTable(tableName);
        
        if (table && table.columns && table.columnsFetched) {
            return table.columns;
        }
        
        // Fetch columns if not cached
        const result = await this.fetchColumns(tableName);
        return result.success ? result.columns : [];
    }
    
    /**
     * Check if cached metadata is stale (>10 minutes old)
     * @returns {boolean} True if metadata is stale
     */
    isStale() {
        if (!this.fetchedAt) {
            return true;
        }
        
        const age = Date.now() - this.fetchedAt.getTime();
        return age > this.STALE_THRESHOLD_MS;
    }
    
    /**
     * Force refresh metadata from database
     * @returns {Promise<Object>} Refresh result
     */
    async refresh() {
        console.log('[DatabaseMetadata] Refreshing metadata...');
        
        const result = await this.fetchTables();
        
        if (result.success) {
            // Clear column caches - they'll be re-fetched on demand
            this.tables.forEach(t => {
                delete t.columns;
                delete t.columnsFetched;
            });
            this.saveToCache();
        }
        
        return result;
    }
    
    /**
     * Save metadata to localStorage
     */
    saveToCache() {
        try {
            const cache = {
                database: this.database,
                tables: this.tables,
                fetchedAt: this.fetchedAt ? this.fetchedAt.toISOString() : null
            };
            
            localStorage.setItem('sp_database_metadata', JSON.stringify(cache));
        } catch (error) {
            console.warn('[DatabaseMetadata] Failed to save cache:', error);
        }
    }
    
    /**
     * Load metadata from localStorage cache
     */
    loadFromCache() {
        try {
            const cached = localStorage.getItem('sp_database_metadata');
            if (!cached) {
                return;
            }
            
            const data = JSON.parse(cached);
            this.database = data.database || this.database;
            this.tables = data.tables || [];
            this.fetchedAt = data.fetchedAt ? new Date(data.fetchedAt) : null;
            this.stale = this.isStale();
            
            console.log(`[DatabaseMetadata] Loaded ${this.tables.length} tables from cache`);
            
        } catch (error) {
            console.warn('[DatabaseMetadata] Failed to load cache:', error);
        }
    }
    
    /**
     * Clear all cached metadata
     */
    clearCache() {
        this.tables = [];
        this.fetchedAt = null;
        this.stale = true;
        localStorage.removeItem('sp_database_metadata');
        console.log('[DatabaseMetadata] Cache cleared');
    }
    
    /**
     * Get formatted age string for UI display
     * @returns {string} Human-readable age (e.g., "5 minutes ago")
     */
    getAgeDisplay() {
        if (!this.fetchedAt) {
            return 'Never fetched';
        }
        
        const ageMs = Date.now() - this.fetchedAt.getTime();
        const ageMinutes = Math.floor(ageMs / 60000);
        const ageHours = Math.floor(ageMinutes / 60);
        
        if (ageHours > 0) {
            return `${ageHours} hour${ageHours !== 1 ? 's' : ''} ago`;
        } else if (ageMinutes > 0) {
            return `${ageMinutes} minute${ageMinutes !== 1 ? 's' : ''} ago`;
        } else {
            return 'Just now';
        }
    }
    
    /**
     * Search tables by name (fuzzy match)
     * @param {string} query - Search query
     * @returns {Array} Matching tables
     */
    searchTables(query) {
        if (!query || query.trim() === '') {
            return this.tables;
        }
        
        const lowerQuery = query.toLowerCase();
        return this.tables.filter(t => 
            t.name.toLowerCase().includes(lowerQuery)
        );
    }
    
    /**
     * Get primary key column(s) for table
     * @param {string} tableName - Table name
     * @returns {Promise<Array>} Array of primary key column names
     */
    async getPrimaryKeys(tableName) {
        const columns = await this.getColumns(tableName);
        return columns
            .filter(col => col.key === 'PRI')
            .map(col => col.name);
    }
    
    /**
     * Get auto-increment column for table
     * @param {string} tableName - Table name
     * @returns {Promise<string|null>} Auto-increment column name or null
     */
    async getAutoIncrementColumn(tableName) {
        const columns = await this.getColumns(tableName);
        const autoIncCol = columns.find(col => col.autoIncrement);
        return autoIncCol ? autoIncCol.name : null;
    }
}

// Singleton instance for app-wide use
export const dbMetadata = new DatabaseMetadata();
