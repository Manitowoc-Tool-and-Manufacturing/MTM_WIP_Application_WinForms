/**
 * Loading Indicator Utilities
 * 
 * Provides consistent loading states across the application.
 */

let loadingOverlay = null;
let loadingCount = 0;

/**
 * Show loading indicator
 * @param {string} message - Loading message to display
 */
export function showLoading(message = 'Loading...') {
    loadingCount++;
    
    if (loadingOverlay) {
        // Update existing message
        const messageEl = loadingOverlay.querySelector('.loading-message');
        if (messageEl) {
            messageEl.textContent = message;
        }
        return;
    }
    
    loadingOverlay = document.createElement('div');
    loadingOverlay.id = 'loading-overlay';
    loadingOverlay.innerHTML = `
        <div style="
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background: rgba(0, 0, 0, 0.7);
            display: flex;
            justify-content: center;
            align-items: center;
            z-index: 99999;
            backdrop-filter: blur(4px);
        ">
            <div style="
                background: var(--color-bg-primary);
                padding: var(--spacing-xl);
                border-radius: var(--radius-lg);
                box-shadow: 0 4px 20px rgba(0, 0, 0, 0.3);
                text-align: center;
                min-width: 300px;
            ">
                <div class="loading-spinner" style="
                    width: 48px;
                    height: 48px;
                    border: 4px solid var(--color-border);
                    border-top-color: var(--color-primary);
                    border-radius: 50%;
                    animation: spin 1s linear infinite;
                    margin: 0 auto var(--spacing-md);
                "></div>
                <div class="loading-message" style="
                    font-size: var(--font-size-lg);
                    font-weight: var(--font-weight-semibold);
                    color: var(--color-text-primary);
                ">${message}</div>
            </div>
        </div>
        
        <style>
            @keyframes spin {
                to { transform: rotate(360deg); }
            }
        </style>
    `;
    
    document.body.appendChild(loadingOverlay);
}

/**
 * Hide loading indicator
 */
export function hideLoading() {
    loadingCount = Math.max(0, loadingCount - 1);
    
    if (loadingCount === 0 && loadingOverlay) {
        loadingOverlay.remove();
        loadingOverlay = null;
    }
}

/**
 * Show loading for async operation
 * @param {Promise} promise - Promise to wait for
 * @param {string} message - Loading message
 * @returns {Promise} Original promise result
 */
export async function withLoading(promise, message = 'Loading...') {
    showLoading(message);
    try {
        const result = await promise;
        return result;
    } finally {
        hideLoading();
    }
}

/**
 * Show inline loading state on button
 * @param {HTMLButtonElement} button - Button element
 * @param {boolean} loading - Loading state
 */
export function setButtonLoading(button, loading) {
    if (!button) return;
    
    if (loading) {
        button.disabled = true;
        button.dataset.originalText = button.textContent;
        button.innerHTML = `
            <span style="display: inline-flex; align-items: center; gap: 8px;">
                <span class="btn-spinner" style="
                    width: 16px;
                    height: 16px;
                    border: 2px solid currentColor;
                    border-top-color: transparent;
                    border-radius: 50%;
                    animation: spin 0.6s linear infinite;
                "></span>
                ${button.dataset.originalText}
            </span>
        `;
    } else {
        button.disabled = false;
        if (button.dataset.originalText) {
            button.textContent = button.dataset.originalText;
            delete button.dataset.originalText;
        }
    }
}
