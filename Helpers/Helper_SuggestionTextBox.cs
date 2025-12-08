using MTM_WIP_Application_Winforms.Components.Shared;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Services.Logging;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Models.Enums;
using MTM_WIP_Application_Winforms.Services;
using MTM_WIP_Application_Winforms.Services.Visual;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

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

        #region Validation Helpers

        /// <summary>
        /// Validates that a SuggestionTextBox has a selection from its data provider.
        /// </summary>
        /// <param name="suggestionTextBox">The control to validate</param>
        /// <param name="fieldName">Friendly name for error messages</param>
        /// <returns>True if valid, false otherwise</returns>
        public static async Task<bool> ValidateSelectionAsync(Component_SuggestionTextBox suggestionTextBox, string fieldName)
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
        public static void Clear(Component_SuggestionTextBox suggestionTextBox, bool refreshDataSource = false)
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

        private static IService_VisualDatabase? GetVisualService()
        {
            return Program.ServiceProvider?.GetService<IService_VisualDatabase>();
        }

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
                
                return Task.FromResult(cachedLocations);
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                return Task.FromResult(new List<string>());
            }
        }

        /// <summary>
        /// Gets users from the pre-loaded Helper_UI_ComboBoxes cache.
        /// Returns empty list if cache not populated. Thread-safe access.
        /// </summary>
        /// <returns>List of all users from cache</returns>
        public static Task<List<string>> GetCachedUsersAsync()
        {
            try
            {
                var cachedUsers = Helper_UI_ComboBoxes.GetCachedUsers();
                
                return Task.FromResult(cachedUsers);
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                return Task.FromResult(new List<string>());
            }
        }

        /// <summary>
        /// Gets color codes from the database.
        /// </summary>
        /// <returns>List of color codes</returns>
        public static async Task<List<string>> GetCachedColorsAsync()
        {
            try
            {
                var dao = new Data.Dao_ColorCode();
                var result = await dao.GetAllAsync();
                
                if (result.IsSuccess && result.Data != null)
                {
                    var colors = new List<string>();
                    foreach (DataRow row in result.Data.Rows)
                    {
                        if (row["ColorCode"] != DBNull.Value)
                        {
                            colors.Add(row["ColorCode"].ToString() ?? string.Empty);
                        }
                    }
                    return colors;
                }
                
                return new List<string>();
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                return new List<string>();
            }
        }

        #endregion

        #region Infor Visual Data Providers

        public static async Task<List<string>> GetCachedInforPartNumbersAsync()
        {
            var service = GetVisualService();
            if (service == null) return new List<string>();
            var result = await service.GetPartIdsAsync();
            return result.IsSuccess ? result.Data ?? new List<string>() : new List<string>();
        }

        public static async Task<List<string>> GetCachedInforUsersAsync()
        {
            var service = GetVisualService();
            if (service == null) return new List<string>();
            var result = await service.GetUserIdsAsync();
            return result.IsSuccess ? result.Data ?? new List<string>() : new List<string>();
        }

        public static async Task<List<string>> GetCachedInforLocationsAsync()
        {
            var service = GetVisualService();
            if (service == null) return new List<string>();
            var result = await service.GetLocationIdsAsync();
            return result.IsSuccess ? result.Data ?? new List<string>() : new List<string>();
        }

        public static async Task<List<string>> GetCachedInforWarehousesAsync()
        {
            var service = GetVisualService();
            if (service == null) return new List<string>();
            var result = await service.GetWarehouseIdsAsync();
            return result.IsSuccess ? result.Data ?? new List<string>() : new List<string>();
        }

        public static async Task<List<string>> GetCachedInforWorkOrdersAsync()
        {
            var service = GetVisualService();
            if (service == null) return new List<string>();
            var result = await service.GetWorkOrdersAsync();
            return result.IsSuccess ? result.Data ?? new List<string>() : new List<string>();
        }

        public static async Task<List<string>> GetCachedInforPurchaseOrdersAsync()
        {
            var service = GetVisualService();
            if (service == null) return new List<string>();
            var result = await service.GetPurchaseOrdersAsync();
            return result.IsSuccess ? result.Data ?? new List<string>() : new List<string>();
        }

        public static async Task<List<string>> GetCachedInforCustomerOrdersAsync()
        {
            var service = GetVisualService();
            if (service == null) return new List<string>();
            var result = await service.GetCustomerOrdersAsync();
            return result.IsSuccess ? result.Data ?? new List<string>() : new List<string>();
        }

        public static async Task<List<string>> GetCachedInforFGTNumbersAsync()
        {
            var service = GetVisualService();
            if (service == null) return new List<string>();
            var result = await service.GetDieIdsAsync();
            return result.IsSuccess ? result.Data ?? new List<string>() : new List<string>();
        }

        public static async Task<List<string>> GetCachedInforCoilFlatstockNumbersAsync()
        {
            var service = GetVisualService();
            if (service == null) return new List<string>();
            var result = await service.GetCoilFlatstockPartIdsAsync();
            return result.IsSuccess ? result.Data ?? new List<string>() : new List<string>();
        }

        #endregion

        #region State Management

        /// <summary>
        /// Enables or disables a SuggestionTextBox.
        /// </summary>
        public static void SetEnabled(Component_SuggestionTextBox suggestionTextBox, bool enabled)
        {
            if (suggestionTextBox == null)
                throw new ArgumentNullException(nameof(suggestionTextBox));

            suggestionTextBox.Enabled = enabled;
        }

        /// <summary>
        /// Sets the enabled state of multiple SuggestionTextBox controls.
        /// </summary>
        public static void SetEnabledMultiple(bool enabled, params Component_SuggestionTextBox[] suggestionTextBoxes)
        {
            foreach (var control in suggestionTextBoxes)
            {
                SetEnabled(control, enabled);
            }
        }

        /// <summary>
        /// Clears multiple SuggestionTextBox controls.
        /// </summary>
        public static void ClearMultiple(bool refreshDataSource = false, params Component_SuggestionTextBox[] suggestionTextBoxes)
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
