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
export function createNavigation(activePage = 'home') {
    const links = [
        { key: 'home', href: 'index.html', label: 'MySQL 5.7 SP Builder' },
        { key: 'wizard', href: 'wizard.html', label: 'Wizard' },
        { key: 'templates', href: 'templates.html', label: 'Templates' },
        { key: 'dml', href: 'dml-operations.html', label: 'DML Builder' }
    ];

    const navLinks = links.map(link => {
        const isActive = link.key === activePage ? 'active' : '';
        return `<a href="${link.href}" class="nav-link ${isActive}">${link.label}</a>`;
    }).join('');

    return `
        <header class="app-header">
            <div class="container">
                <nav class="app-nav">
                    ${navLinks}
                </nav>
                <div class="app-actions">
                    <button id="nav-btn-help" class="btn btn-sm btn-outline-secondary">❓ Help</button>
                    <button id="nav-btn-save" class="btn btn-sm btn-primary">💾 Save</button>
                </div>
            </div>
        </header>
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
        if (activePage === 'help') {
            helpBtn.classList.remove('btn-sm', 'btn-outline-secondary');
            helpBtn.classList.add('btn-sm', 'btn-primary');
            helpBtn.disabled = true;
        } else {
            helpBtn.addEventListener('click', () => {
                window.location.href = 'help.html';
            });
        }
    }
    
    if (saveBtn) {
        if (onSave) {
            saveBtn.style.display = 'inline-flex';
            saveBtn.addEventListener('click', onSave);
        } else {
            saveBtn.style.display = 'none';
        }
    }
    
    // Add keyboard shortcut for help
    document.addEventListener('keydown', (e) => {
        if (e.key === 'F1') {
            e.preventDefault();
            window.location.href = 'help.html';
        }
    });
}
