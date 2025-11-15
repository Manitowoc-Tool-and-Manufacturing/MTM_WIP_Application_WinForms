using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MTM_WIP_Application_Winforms.Controls.Shared;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;

namespace MTM_WIP_Application_Winforms.Helpers
{
    #region Helper_SuggestionTextBox

    /// <summary>
    /// Helper class for SuggestionTextBox control operations.
    /// Provides methods to configure, trigger, and manage suggestion overlays.
    /// Centralizes all suggestion-related functionality for consistent behavior across the application.
    /// </summary>
    public static class Helper_SuggestionTextBox
    {
        #region F4 Trigger (Show Full List)

        /// <summary>
        /// Shows the full suggestion list regardless of current text (F4 functionality).
        /// </summary>
        /// <param name="suggestionTextBox">The SuggestionTextBox control to trigger</param>
        /// <returns>Task that completes when overlay is closed</returns>
        public static async Task ShowFullSuggestionListAsync(SuggestionTextBox suggestionTextBox)
        {
            if (suggestionTextBox == null)
                throw new ArgumentNullException(nameof(suggestionTextBox));

            if (suggestionTextBox.DataProvider == null)
            {
                LoggingUtility.Log($"[Helper_SuggestionTextBox] Cannot show suggestions for {suggestionTextBox.Name} - DataProvider is null");
                return;
            }

            try
            {
                // Get all suggestions from data provider
                var allSuggestions = await suggestionTextBox.DataProvider.Invoke();
                
                if (allSuggestions == null || allSuggestions.Count == 0)
                {
                    Service_ErrorHandler.ShowWarning("No suggestions available.");
                    return;
                }

                // Clean the source list
                allSuggestions = Service_SuggestionFilter.CleanSourceList(allSuggestions);

                // Show overlay with full list
                var overlay = new SuggestionOverlayForm(allSuggestions, suggestionTextBox);
                var parentForm = suggestionTextBox.FindForm();
                
                if (parentForm == null)
                {
                    LoggingUtility.Log($"[Helper_SuggestionTextBox] Cannot find parent form for {suggestionTextBox.Name}");
                    return;
                }

                var result = overlay.ShowDialog(parentForm);
                
                if (result == DialogResult.OK && !string.IsNullOrEmpty(overlay.SelectedItem))
                {
                    suggestionTextBox.Text = overlay.SelectedItem.ToUpperInvariant();
                    
                    // Manually trigger SuggestionSelected event
                    suggestionTextBox.RaiseSuggestionSelectedEvent(new SuggestionSelectedEventArgs
                    {
                        OriginalInput = suggestionTextBox.Text,
                        SelectedValue = overlay.SelectedItem,
                        SelectionIndex = 0,
                        FieldName = suggestionTextBox.Name
                    });

                    // Move to next control
                    parentForm.SelectNextControl(suggestionTextBox, forward: true, tabStopOnly: true, nested: true, wrap: false);
                }
                else
                {
                    // User cancelled - keep focus on this field
                    suggestionTextBox.Focus();
                }
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Low,
                    controlName: suggestionTextBox.Name,
                    callerName: nameof(ShowFullSuggestionListAsync));
            }
        }

        #endregion

        #region F4 Key Handler Registration

        /// <summary>
        /// Registers F4 and Down arrow key handlers on a SuggestionTextBox to show full suggestion list.
        /// </summary>
        /// <param name="suggestionTextBox">The SuggestionTextBox control</param>
        public static void RegisterF4Handler(SuggestionTextBox suggestionTextBox)
        {
            if (suggestionTextBox == null)
                throw new ArgumentNullException(nameof(suggestionTextBox));

            suggestionTextBox.KeyDown += async (sender, e) =>
            {
                if (e.KeyCode == Keys.F4 || (e.KeyCode == Keys.Down && suggestionTextBox.Text.Length == 0))
                {
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    await ShowFullSuggestionListAsync(suggestionTextBox);
                }
            };

            LoggingUtility.Log($"[Helper_SuggestionTextBox] F4 handler registered for {suggestionTextBox.Name}");
        }

        #endregion

        #region Configuration Helpers

        /// <summary>
        /// Configures a SuggestionTextBox for Part Number suggestions.
        /// Standard configuration: MaxResults=100, EnableWildcards=true, ClearOnNoMatch=true
        /// </summary>
        public static void ConfigureForPartNumbers(SuggestionTextBox suggestionTextBox, Func<Task<List<string>>> dataProvider, bool enableF4 = true)
        {
            if (suggestionTextBox == null)
                throw new ArgumentNullException(nameof(suggestionTextBox));

            suggestionTextBox.DataProvider = dataProvider;
            suggestionTextBox.MaxResults = 100;
            suggestionTextBox.EnableWildcards = true;
            suggestionTextBox.ClearOnNoMatch = true;
            suggestionTextBox.SuppressExactMatch = true;

            if (enableF4)
            {
                RegisterF4Handler(suggestionTextBox);
            }

            LoggingUtility.Log($"[Helper_SuggestionTextBox] Configured {suggestionTextBox.Name} for Part Numbers");
        }

        /// <summary>
        /// Configures a SuggestionTextBox for Operation suggestions.
        /// Standard configuration: MaxResults=50, EnableWildcards=true, ClearOnNoMatch=true
        /// </summary>
        public static void ConfigureForOperations(SuggestionTextBox suggestionTextBox, Func<Task<List<string>>> dataProvider, bool enableF4 = true)
        {
            if (suggestionTextBox == null)
                throw new ArgumentNullException(nameof(suggestionTextBox));

            suggestionTextBox.DataProvider = dataProvider;
            suggestionTextBox.MaxResults = 50;
            suggestionTextBox.EnableWildcards = true;
            suggestionTextBox.ClearOnNoMatch = true;
            suggestionTextBox.SuppressExactMatch = true;

            if (enableF4)
            {
                RegisterF4Handler(suggestionTextBox);
            }

            LoggingUtility.Log($"[Helper_SuggestionTextBox] Configured {suggestionTextBox.Name} for Operations");
        }

        /// <summary>
        /// Configures a SuggestionTextBox for Location suggestions.
        /// Standard configuration: MaxResults=100, EnableWildcards=true, ClearOnNoMatch=true
        /// </summary>
        public static void ConfigureForLocations(SuggestionTextBox suggestionTextBox, Func<Task<List<string>>> dataProvider, bool enableF4 = true)
        {
            if (suggestionTextBox == null)
                throw new ArgumentNullException(nameof(suggestionTextBox));

            suggestionTextBox.DataProvider = dataProvider;
            suggestionTextBox.MaxResults = 100;
            suggestionTextBox.EnableWildcards = true;
            suggestionTextBox.ClearOnNoMatch = true;
            suggestionTextBox.SuppressExactMatch = true;

            if (enableF4)
            {
                RegisterF4Handler(suggestionTextBox);
            }

            LoggingUtility.Log($"[Helper_SuggestionTextBox] Configured {suggestionTextBox.Name} for Locations");
        }

        /// <summary>
        /// Configures a SuggestionTextBox for Item Type suggestions.
        /// Standard configuration: MaxResults=50, EnableWildcards=false, ClearOnNoMatch=true
        /// </summary>
        public static void ConfigureForItemTypes(SuggestionTextBox suggestionTextBox, Func<Task<List<string>>> dataProvider, bool enableF4 = true)
        {
            if (suggestionTextBox == null)
                throw new ArgumentNullException(nameof(suggestionTextBox));

            suggestionTextBox.DataProvider = dataProvider;
            suggestionTextBox.MaxResults = 50;
            suggestionTextBox.EnableWildcards = false; // Item types are short, no wildcards needed
            suggestionTextBox.ClearOnNoMatch = true;
            suggestionTextBox.SuppressExactMatch = true;

            if (enableF4)
            {
                RegisterF4Handler(suggestionTextBox);
            }

            LoggingUtility.Log($"[Helper_SuggestionTextBox] Configured {suggestionTextBox.Name} for Item Types");
        }

        /// <summary>
        /// Configures a SuggestionTextBox for Color Code suggestions.
        /// Standard configuration: MaxResults=20, EnableWildcards=false, ClearOnNoMatch=false
        /// </summary>
        public static void ConfigureForColorCodes(SuggestionTextBox suggestionTextBox, Func<Task<List<string>>> dataProvider, bool enableF4 = true)
        {
            if (suggestionTextBox == null)
                throw new ArgumentNullException(nameof(suggestionTextBox));

            suggestionTextBox.DataProvider = dataProvider;
            suggestionTextBox.MaxResults = 20;
            suggestionTextBox.EnableWildcards = false; // Color codes are simple, no wildcards
            suggestionTextBox.ClearOnNoMatch = false; // Allow custom color codes (e.g., "OTHER")
            suggestionTextBox.SuppressExactMatch = true;

            if (enableF4)
            {
                RegisterF4Handler(suggestionTextBox);
            }

            LoggingUtility.Log($"[Helper_SuggestionTextBox] Configured {suggestionTextBox.Name} for Color Codes");
        }

        #endregion

        #region Bulk Configuration

        /// <summary>
        /// Configures multiple SuggestionTextBox controls at once with F4 support.
        /// </summary>
        /// <param name="configurations">Array of configuration tuples (control, dataProvider, configurationType)</param>
        public static void ConfigureMultiple(params (SuggestionTextBox control, Func<Task<List<string>>> dataProvider, SuggestionType type)[] configurations)
        {
            foreach (var (control, dataProvider, type) in configurations)
            {
                switch (type)
                {
                    case SuggestionType.PartNumber:
                        ConfigureForPartNumbers(control, dataProvider, enableF4: true);
                        break;
                    case SuggestionType.Operation:
                        ConfigureForOperations(control, dataProvider, enableF4: true);
                        break;
                    case SuggestionType.Location:
                        ConfigureForLocations(control, dataProvider, enableF4: true);
                        break;
                    case SuggestionType.ItemType:
                        ConfigureForItemTypes(control, dataProvider, enableF4: true);
                        break;
                    case SuggestionType.ColorCode:
                        ConfigureForColorCodes(control, dataProvider, enableF4: true);
                        break;
                }
            }
        }

        #endregion

        #region Validation Helpers

        /// <summary>
        /// Validates that a SuggestionTextBox has a selection from its data provider.
        /// </summary>
        /// <param name="suggestionTextBox">The control to validate</param>
        /// <param name="fieldName">Friendly name for error messages</param>
        /// <returns>True if valid, false otherwise</returns>
        public static async Task<bool> ValidateSelectionAsync(SuggestionTextBox suggestionTextBox, string fieldName)
        {
            if (suggestionTextBox == null)
                throw new ArgumentNullException(nameof(suggestionTextBox));

            if (string.IsNullOrWhiteSpace(suggestionTextBox.Text))
            {
                Service_ErrorHandler.ShowWarning($"{fieldName} is required.");
                suggestionTextBox.Focus();
                return false;
            }

            if (suggestionTextBox.DataProvider != null)
            {
                try
                {
                    var allSuggestions = await suggestionTextBox.DataProvider.Invoke();
                    var exactMatch = allSuggestions?.FirstOrDefault(s => 
                        string.Equals(s, suggestionTextBox.Text, StringComparison.OrdinalIgnoreCase));

                    if (exactMatch == null)
                    {
                        Service_ErrorHandler.ShowWarning($"{fieldName} '{suggestionTextBox.Text}' is not valid. Please select from the list.");
                        suggestionTextBox.Focus();
                        suggestionTextBox.SelectAll();
                        return false;
                    }

                    // Normalize casing
                    suggestionTextBox.Text = exactMatch;
                    return true;
                }
                catch (Exception ex)
                {
                    LoggingUtility.LogApplicationError(ex);
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Clears a SuggestionTextBox and optionally refreshes its data source.
        /// </summary>
        public static void Clear(SuggestionTextBox suggestionTextBox, bool refreshDataSource = false)
        {
            if (suggestionTextBox == null)
                throw new ArgumentNullException(nameof(suggestionTextBox));

            suggestionTextBox.Text = string.Empty;
            
            if (refreshDataSource)
            {
                suggestionTextBox.RefreshDataSource();
            }
        }

        #endregion

        #region Cached Data Providers

        /// <summary>
        /// Gets part numbers from the pre-loaded Helper_UI_ComboBoxes cache.
        /// Returns empty list if cache not populated. Thread-safe access.
        /// </summary>
        /// <returns>List of all part IDs from cache</returns>
        public static Task<List<string>> GetCachedPartNumbersAsync()
        {
            try
            {
                var cachedParts = Helper_UI_ComboBoxes.GetCachedPartNumbers();
                LoggingUtility.Log($"[Helper_SuggestionTextBox] Retrieved {cachedParts.Count} part numbers from cache");
                return Task.FromResult(cachedParts);
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                return Task.FromResult(new List<string>());
            }
        }

        /// <summary>
        /// Gets item types from the pre-loaded Helper_UI_ComboBoxes cache.
        /// Returns empty list if cache not populated. Thread-safe access.
        /// </summary>
        /// <returns>List of all item types from cache</returns>
        public static Task<List<string>> GetCachedItemTypesAsync()
        {
            try
            {
                var cachedItemTypes = Helper_UI_ComboBoxes.GetCachedItemTypes();
                LoggingUtility.Log($"[Helper_SuggestionTextBox] Retrieved {cachedItemTypes.Count} item types from cache");
                return Task.FromResult(cachedItemTypes);
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                return Task.FromResult(new List<string>());
            }
        }

        /// <summary>
        /// Gets operations from the pre-loaded Helper_UI_ComboBoxes cache.
        /// Returns empty list if cache not populated. Thread-safe access.
        /// </summary>
        /// <returns>List of all operations from cache</returns>
        public static Task<List<string>> GetCachedOperationsAsync()
        {
            try
            {
                var cachedOperations = Helper_UI_ComboBoxes.GetCachedOperations();
                LoggingUtility.Log($"[Helper_SuggestionTextBox] Retrieved {cachedOperations.Count} operations from cache");
                return Task.FromResult(cachedOperations);
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                return Task.FromResult(new List<string>());
            }
        }

        /// <summary>
        /// Gets locations from the pre-loaded Helper_UI_ComboBoxes cache.
        /// Returns empty list if cache not populated. Thread-safe access.
        /// </summary>
        /// <returns>List of all locations from cache</returns>
        public static Task<List<string>> GetCachedLocationsAsync()
        {
            try
            {
                var cachedLocations = Helper_UI_ComboBoxes.GetCachedLocations();
                LoggingUtility.Log($"[Helper_SuggestionTextBox] Retrieved {cachedLocations.Count} locations from cache");
                return Task.FromResult(cachedLocations);
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                return Task.FromResult(new List<string>());
            }
        }

        #endregion

        #region State Management

        /// <summary>
        /// Enables or disables a SuggestionTextBox.
        /// </summary>
        public static void SetEnabled(SuggestionTextBox suggestionTextBox, bool enabled)
        {
            if (suggestionTextBox == null)
                throw new ArgumentNullException(nameof(suggestionTextBox));

            suggestionTextBox.Enabled = enabled;
        }

        /// <summary>
        /// Sets the enabled state of multiple SuggestionTextBox controls.
        /// </summary>
        public static void SetEnabledMultiple(bool enabled, params SuggestionTextBox[] suggestionTextBoxes)
        {
            foreach (var control in suggestionTextBoxes)
            {
                SetEnabled(control, enabled);
            }
        }

        /// <summary>
        /// Clears multiple SuggestionTextBox controls.
        /// </summary>
        public static void ClearMultiple(bool refreshDataSource = false, params SuggestionTextBox[] suggestionTextBoxes)
        {
            foreach (var control in suggestionTextBoxes)
            {
                Clear(control, refreshDataSource);
            }
        }

        #endregion
    }

    #endregion

    #region Enumerations

    /// <summary>
    /// Types of suggestion configurations available.
    /// </summary>
    public enum SuggestionType
    {
        PartNumber,
        Operation,
        Location,
        ItemType,
        ColorCode,
        Custom
    }

    #endregion
}
