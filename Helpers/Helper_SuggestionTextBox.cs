using System.Collections.Concurrent;
using System.Data;
using Microsoft.Extensions.DependencyInjection;
using MTM_WIP_Application_Winforms.Components.Shared;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Models.Enums;
using MTM_WIP_Application_Winforms.Services;
using MTM_WIP_Application_Winforms.Services.Logging;
using MTM_WIP_Application_Winforms.Services.Visual;

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
        #region Fields

        private const int VISUAL_SUGGESTION_CACHE_TTL_MINUTES = 10;
        private static readonly ConcurrentDictionary<
            string,
            VisualSuggestionCacheEntry
        > VisualSuggestionCache = new(StringComparer.OrdinalIgnoreCase);
        private static readonly ConcurrentDictionary<
            string,
            SemaphoreSlim
        > VisualSuggestionCacheLocks = new(StringComparer.OrdinalIgnoreCase);

        #endregion

        #region Validation Helpers

        /// <summary>
        /// Validates that a SuggestionTextBox has a selection from its data provider.
        /// </summary>
        /// <param name="suggestionTextBox">The control to validate</param>
        /// <param name="fieldName">Friendly name for error messages</param>
        /// <returns>True if valid, false otherwise</returns>
        public static async Task<bool> ValidateSelectionAsync(
            Component_SuggestionTextBox suggestionTextBox,
            string fieldName
        )
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
                        string.Equals(s, suggestionTextBox.Text, StringComparison.OrdinalIgnoreCase)
                    );

                    if (exactMatch == null)
                    {
                        Service_ErrorHandler.ShowWarning(
                            $"{fieldName} '{suggestionTextBox.Text}' is not valid. Please select from the list."
                        );
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
        public static void Clear(
            Component_SuggestionTextBox suggestionTextBox,
            bool refreshDataSource = false
        )
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

        private sealed class VisualSuggestionCacheEntry
        {
            public required List<string> Values { get; init; }
            public required DateTime ExpiresAtUtc { get; init; }
        }

        private static IService_VisualDatabase? GetVisualService()
        {
            return Program.ServiceProvider?.GetService<IService_VisualDatabase>();
        }

        private static bool TryGetVisualSuggestionCache(string cacheKey, out List<string> values)
        {
            values = new List<string>();

            if (
                !VisualSuggestionCache.TryGetValue(
                    cacheKey,
                    out VisualSuggestionCacheEntry? cacheEntry
                )
            )
            {
                return false;
            }

            if (cacheEntry.ExpiresAtUtc <= DateTime.UtcNow)
            {
                VisualSuggestionCache.TryRemove(cacheKey, out _);
                return false;
            }

            values = new List<string>(cacheEntry.Values);
            return true;
        }

        private static List<string> NormalizeVisualSuggestionValues(IEnumerable<string> values)
        {
            return values
                .Where(value => !string.IsNullOrWhiteSpace(value))
                .Select(value => value.Trim())
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(value => value, StringComparer.OrdinalIgnoreCase)
                .ToList();
        }

        private static async Task<List<string>> GetOrLoadVisualSuggestionsAsync(
            string cacheKey,
            Func<IService_VisualDatabase, Task<Model_Dao_Result<List<string>>>> loader
        )
        {
            if (TryGetVisualSuggestionCache(cacheKey, out List<string> cachedValues))
            {
                return cachedValues;
            }

            IService_VisualDatabase? service = GetVisualService();
            if (service == null)
            {
                return new List<string>();
            }

            SemaphoreSlim cacheLock = VisualSuggestionCacheLocks.GetOrAdd(
                cacheKey,
                static _ => new SemaphoreSlim(1, 1)
            );
            await cacheLock.WaitAsync();

            try
            {
                if (TryGetVisualSuggestionCache(cacheKey, out cachedValues))
                {
                    return cachedValues;
                }

                Model_Dao_Result<List<string>> result = await loader(service);
                if (!result.IsSuccess || result.Data == null)
                {
                    return new List<string>();
                }

                List<string> normalizedValues = NormalizeVisualSuggestionValues(result.Data);
                VisualSuggestionCache[cacheKey] = new VisualSuggestionCacheEntry
                {
                    Values = normalizedValues,
                    ExpiresAtUtc = DateTime.UtcNow.AddMinutes(VISUAL_SUGGESTION_CACHE_TTL_MINUTES),
                };

                return new List<string>(normalizedValues);
            }
            finally
            {
                cacheLock.Release();
            }
        }

        private static string? GetVisualSuggestionCacheKey(
            Enum_SuggestionDataSource suggestionDataSource
        )
        {
            return suggestionDataSource switch
            {
                Enum_SuggestionDataSource.Infor_PartNumber => "Infor.PartNumber",
                Enum_SuggestionDataSource.Infor_User => "Infor.User",
                Enum_SuggestionDataSource.Infor_Location => "Infor.Location",
                Enum_SuggestionDataSource.Infor_Warehouse => "Infor.Warehouse",
                Enum_SuggestionDataSource.Infor_PONumber
                or Enum_SuggestionDataSource.Infor_PurchaseOrder => "Infor.PurchaseOrder",
                Enum_SuggestionDataSource.Infor_CONumber
                or Enum_SuggestionDataSource.Infor_CustomerOrder => "Infor.CustomerOrder",
                Enum_SuggestionDataSource.Infor_WONumber
                or Enum_SuggestionDataSource.Infor_WorkOrder => "Infor.WorkOrder",
                Enum_SuggestionDataSource.Infor_FGTNumber => "Infor.FGTNumber",
                Enum_SuggestionDataSource.Infor_MMCNumber
                or Enum_SuggestionDataSource.Infor_MMFNumber => "Infor.CoilFlatstock",
                _ => null,
            };
        }

        /// <summary>
        /// Invalidates the shared Visual suggestion cache for a specific Infor data source.
        /// Non-Infor data sources are ignored.
        /// </summary>
        /// <param name="suggestionDataSource">The data source to invalidate.</param>
        public static void InvalidateVisualSuggestionCache(
            Enum_SuggestionDataSource suggestionDataSource
        )
        {
            string? cacheKey = GetVisualSuggestionCacheKey(suggestionDataSource);
            if (cacheKey == null)
            {
                return;
            }

            VisualSuggestionCache.TryRemove(cacheKey, out _);
        }

        /// <summary>
        /// Invalidates all shared Infor Visual suggestion caches.
        /// </summary>
        public static void InvalidateAllVisualSuggestionCaches()
        {
            foreach (string cacheKey in VisualSuggestionCache.Keys)
            {
                VisualSuggestionCache.TryRemove(cacheKey, out _);
            }
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
            return await GetOrLoadVisualSuggestionsAsync(
                "Infor.PartNumber",
                static service => service.GetPartIdsAsync()
            );
        }

        public static async Task<List<string>> GetCachedInforUsersAsync()
        {
            return await GetOrLoadVisualSuggestionsAsync(
                "Infor.User",
                static service => service.GetUserIdsAsync()
            );
        }

        public static async Task<List<string>> GetCachedInforLocationsAsync()
        {
            return await GetOrLoadVisualSuggestionsAsync(
                "Infor.Location",
                static service => service.GetLocationIdsAsync()
            );
        }

        public static async Task<List<string>> GetCachedInforWarehousesAsync()
        {
            return await GetOrLoadVisualSuggestionsAsync(
                "Infor.Warehouse",
                static service => service.GetWarehouseIdsAsync()
            );
        }

        public static async Task<List<string>> GetCachedInforWorkOrdersAsync()
        {
            return await GetOrLoadVisualSuggestionsAsync(
                "Infor.WorkOrder",
                static service => service.GetWorkOrdersAsync()
            );
        }

        public static async Task<List<string>> GetCachedInforPurchaseOrdersAsync()
        {
            return await GetOrLoadVisualSuggestionsAsync(
                "Infor.PurchaseOrder",
                static service => service.GetPurchaseOrdersAsync()
            );
        }

        public static async Task<List<string>> GetCachedInforCustomerOrdersAsync()
        {
            return await GetOrLoadVisualSuggestionsAsync(
                "Infor.CustomerOrder",
                static service => service.GetCustomerOrdersAsync()
            );
        }

        public static async Task<List<string>> GetCachedInforFGTNumbersAsync()
        {
            return await GetOrLoadVisualSuggestionsAsync(
                "Infor.FGTNumber",
                static service => service.GetDieIdsAsync()
            );
        }

        public static async Task<List<string>> GetCachedInforCoilFlatstockNumbersAsync()
        {
            return await GetOrLoadVisualSuggestionsAsync(
                "Infor.CoilFlatstock",
                static service => service.GetCoilFlatstockPartIdsAsync()
            );
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
        public static void SetEnabledMultiple(
            bool enabled,
            params Component_SuggestionTextBox[] suggestionTextBoxes
        )
        {
            foreach (var control in suggestionTextBoxes)
            {
                SetEnabled(control, enabled);
            }
        }

        /// <summary>
        /// Clears multiple SuggestionTextBox controls.
        /// </summary>
        public static void ClearMultiple(
            bool refreshDataSource = false,
            params Component_SuggestionTextBox[] suggestionTextBoxes
        )
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
        Custom,
    }

    #endregion
}
