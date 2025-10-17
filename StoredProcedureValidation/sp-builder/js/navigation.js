/**
 * Shared Navigation Component
 * 
 * Provides consistent navigation across all pages in the SP Builder application.
 * Handles active page highlighting and navigation state.
 */

/**
 * Create navigation bar HTML
 * @param {string} activePage - Current page identifier ('wizard', 'templates', 'dml')
 * @returns {string} Navigation HTML
 */
export function createNavigation(activePage = 'wizard') {
    return `
        <nav class="app-navigation" style="
            background: var(--color-bg-primary);
            border-bottom: 2px solid var(--color-border);
            padding: var(--spacing-md) var(--spacing-xl);
            display: flex;
            justify-content: space-between;
            align-items: center;
            position: sticky;
            top: 0;
            z-index: 100;
            box-shadow: 0 2px 8px rgba(0,0,0,0.1);
        ">
            <div style="display: flex; align-items: center; gap: var(--spacing-xl);">
                <h1 style="margin: 0; font-size: var(--font-size-lg); font-weight: var(--font-weight-bold);">
                    🔧 MySQL 5.7 SP Builder
                </h1>
                <div style="display: flex; gap: var(--spacing-sm);">
                    <a href="wizard.html" class="nav-link ${activePage === 'wizard' ? 'active' : ''}" data-page="wizard">
                        📝 Wizard
                    </a>
                    <a href="templates.html" class="nav-link ${activePage === 'templates' ? 'active' : ''}" data-page="templates">
                        📚 Templates
                    </a>
                    <a href="dml-operations.html" class="nav-link ${activePage === 'dml' ? 'active' : ''}" data-page="dml">
                        🔨 DML Builder
                    </a>
                </div>
            </div>
            <div style="display: flex; align-items: center; gap: var(--spacing-md);">
                <button id="nav-btn-help" class="btn btn-link" title="Help & Keyboard Shortcuts (F1)">
                    ❓ Help
                </button>
                <button id="nav-btn-save" class="btn btn-outline-primary" title="Save Progress (Ctrl+S)">
                    💾 Save
                </button>
            </div>
        </nav>
        
        <style>
            .nav-link {
                padding: var(--spacing-sm) var(--spacing-md);
                border-radius: var(--radius-md);
                text-decoration: none;
                color: var(--color-text-primary);
                transition: all 0.2s;
                font-weight: var(--font-weight-semibold);
            }
            
            .nav-link:hover {
                background: var(--color-bg-secondary);
                color: var(--color-primary);
            }
            
            .nav-link.active {
                background: var(--color-primary);
                color: white;
            }
        </style>
    `;
}

/**
 * Initialize navigation in current page
 * @param {string} activePage - Current page identifier
 * @param {Function} onSave - Callback for save button
 */
export function initNavigation(activePage, onSave) {
    // Insert navigation at top of body
    const nav = document.createElement('div');
    nav.innerHTML = createNavigation(activePage);
    document.body.insertBefore(nav.firstElementChild, document.body.firstChild);
    
    // Add event listeners
    const helpBtn = document.getElementById('nav-btn-help');
    const saveBtn = document.getElementById('nav-btn-save');
    
    if (helpBtn) {
        helpBtn.addEventListener('click', showKeyboardShortcuts);
    }
    
    if (saveBtn && onSave) {
        saveBtn.addEventListener('click', onSave);
    }
    
    // Add keyboard shortcut for help
    document.addEventListener('keydown', (e) => {
        if (e.key === 'F1') {
            e.preventDefault();
            showKeyboardShortcuts();
        }
    });
}

/**
 * Show keyboard shortcuts dialog
 */
function showKeyboardShortcuts() {
    const shortcuts = [
        { key: 'Ctrl + S', description: 'Save progress' },
        { key: 'Ctrl + →', description: 'Next step' },
        { key: 'Ctrl + ←', description: 'Previous step' },
        { key: 'F1', description: 'Show help' },
        { key: 'Esc', description: 'Close dialogs' }
    ];
    
    const html = `
        <div class="modal-overlay" id="shortcuts-modal" style="
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background: rgba(0,0,0,0.5);
            display: flex;
            justify-content: center;
            align-items: center;
            z-index: 10000;
        ">
            <div style="
                background: var(--color-bg-primary);
                border-radius: var(--radius-lg);
                padding: var(--spacing-xl);
                max-width: 500px;
                box-shadow: 0 4px 20px rgba(0,0,0,0.3);
            ">
                <div style="display: flex; justify-content: space-between; align-items: center; margin-bottom: var(--spacing-lg);">
                    <h2 style="margin: 0;">⌨️ Keyboard Shortcuts</h2>
                    <button class="btn btn-icon" onclick="this.closest('.modal-overlay').remove()">✕</button>
                </div>
                <table style="width: 100%; border-collapse: collapse;">
                    ${shortcuts.map(s => `
                        <tr style="border-bottom: 1px solid var(--color-border);">
                            <td style="padding: var(--spacing-md); font-weight: var(--font-weight-bold); font-family: monospace;">
                                ${s.key}
                            </td>
                            <td style="padding: var(--spacing-md); color: var(--color-text-secondary);">
                                ${s.description}
                            </td>
                        </tr>
                    `).join('')}
                </table>
                <div style="margin-top: var(--spacing-lg); text-align: right;">
                    <button class="btn btn-primary" onclick="this.closest('.modal-overlay').remove()">Got it!</button>
                </div>
            </div>
        </div>
    `;
    
    const modal = document.createElement('div');
    modal.innerHTML = html;
    document.body.appendChild(modal.firstElementChild);
    
    // Close on Esc
    document.addEventListener('keydown', function closeOnEsc(e) {
        if (e.key === 'Escape') {
            const shortcutsModal = document.getElementById('shortcuts-modal');
            if (shortcutsModal) {
                shortcutsModal.remove();
                document.removeEventListener('keydown', closeOnEsc);
            }
        }
    });
    
    // Close on overlay click
    const shortcutsModal = document.getElementById('shortcuts-modal');
    if (shortcutsModal) {
        shortcutsModal.addEventListener('click', (e) => {
            if (e.target === shortcutsModal) {
                shortcutsModal.remove();
            }
        });
    }
}
