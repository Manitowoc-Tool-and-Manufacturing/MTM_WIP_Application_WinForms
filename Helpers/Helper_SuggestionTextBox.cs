using MTM_WIP_Application_Winforms.Controls.Shared;
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
        private static readonly object OverlayLock = new();
        private static readonly HashSet<SuggestionTextBox> ActiveOverlayRequests = new();

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
                
                return;
            }

            lock (OverlayLock)
            {
                if (ActiveOverlayRequests.Contains(suggestionTextBox))
                {
                    
                    return;
                }

                ActiveOverlayRequests.Add(suggestionTextBox);
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
                    
                    return;
                }

                var result = overlay.ShowDialog(parentForm);
                
                if (result == DialogResult.OK && !string.IsNullOrEmpty(overlay.SelectedItem))
                {
                    suggestionTextBox.Text = overlay.SelectedItem.ToUpperInvariant();
                    
                    // Manually trigger SuggestionSelected event
                    suggestionTextBox.RaiseSuggestionSelectedEvent(new EventArgs_SuggestionSelectedEventArgs
                    {
                        OriginalInput = suggestionTextBox.Text,
                        SelectedValue = overlay.SelectedItem,
                        SelectionIndex = 0,
                        FieldName = suggestionTextBox.Name
                    });

                    // Handle Selection Action
                    if (suggestionTextBox.SelectionAction == Enum_SuggestionSelectionAction.MoveFocusToNextControl)
                    {
                        // Move to next control
                        parentForm.SelectNextControl(suggestionTextBox, forward: true, tabStopOnly: true, nested: true, wrap: false);
                    }
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
            finally
            {
                lock (OverlayLock)
                {
                    ActiveOverlayRequests.Remove(suggestionTextBox);
                }
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
            suggestionTextBox.NoMatchAction = Enum_SuggestionNoMatchAction.ShowWarningAndClear;
            suggestionTextBox.SuppressExactMatch = true;

            if (enableF4)
            {
                RegisterF4Handler(suggestionTextBox);
            }

            
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
            suggestionTextBox.NoMatchAction = Enum_SuggestionNoMatchAction.ShowWarningAndClear;
            suggestionTextBox.SuppressExactMatch = true;

            if (enableF4)
            {
                RegisterF4Handler(suggestionTextBox);
            }

            
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
            suggestionTextBox.NoMatchAction = Enum_SuggestionNoMatchAction.ShowWarningAndClear;
            suggestionTextBox.SuppressExactMatch = true;

            if (enableF4)
            {
                RegisterF4Handler(suggestionTextBox);
            }

            
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
            suggestionTextBox.NoMatchAction = Enum_SuggestionNoMatchAction.ShowWarningAndClear;
            suggestionTextBox.SuppressExactMatch = true;

            if (enableF4)
            {
                RegisterF4Handler(suggestionTextBox);
            }

            
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
            suggestionTextBox.NoMatchAction = Enum_SuggestionNoMatchAction.None; // Allow custom color codes (e.g., "OTHER")
            suggestionTextBox.SuppressExactMatch = true;

            if (enableF4)
            {
                RegisterF4Handler(suggestionTextBox);
            }

            
        }

        /// <summary>
        /// Configures a SuggestionTextBoxWithLabel for Part Number suggestions.
        /// Standard configuration: MaxResults=100, EnableWildcards=true, ClearOnNoMatch=true, F4 button enabled.
        /// </summary>
        public static void ConfigureForPartNumbers(SuggestionTextBoxWithLabel control, Func<Task<List<string>>> dataProvider)
        {
            if (control == null)
                throw new ArgumentNullException(nameof(control));

            ConfigureForPartNumbers(control.TextBox, dataProvider, enableF4: false); // F4 handled by button
            
        }

        /// <summary>
        /// Configures a SuggestionTextBoxWithLabel for Operation suggestions.
        /// Standard configuration: MaxResults=50, EnableWildcards=true, ClearOnNoMatch=true, F4 button enabled.
        /// </summary>
        public static void ConfigureForOperations(SuggestionTextBoxWithLabel control, Func<Task<List<string>>> dataProvider)
        {
            if (control == null)
                throw new ArgumentNullException(nameof(control));

            ConfigureForOperations(control.TextBox, dataProvider, enableF4: false); // F4 handled by button
            
        }

        /// <summary>
        /// Configures a SuggestionTextBoxWithLabel for Location suggestions.
        /// Standard configuration: MaxResults=100, EnableWildcards=true, ClearOnNoMatch=true, F4 button enabled.
        /// </summary>
        public static void ConfigureForLocations(SuggestionTextBoxWithLabel control, Func<Task<List<string>>> dataProvider)
        {
            if (control == null)
                throw new ArgumentNullException(nameof(control));

            ConfigureForLocations(control.TextBox, dataProvider, enableF4: false); // F4 handled by button
            
        }

        /// <summary>
        /// Configures a SuggestionTextBox for Warehouse suggestions.
        /// Standard configuration: MaxResults=50, EnableWildcards=true, ClearOnNoMatch=true
        /// </summary>
        public static void ConfigureForWarehouses(SuggestionTextBox suggestionTextBox, Func<Task<List<string>>> dataProvider, bool enableF4 = true)
        {
            if (suggestionTextBox == null)
                throw new ArgumentNullException(nameof(suggestionTextBox));

            suggestionTextBox.DataProvider = dataProvider;
            suggestionTextBox.MaxResults = 50;
            suggestionTextBox.EnableWildcards = true;
            suggestionTextBox.NoMatchAction = Enum_SuggestionNoMatchAction.ShowWarningAndClear;
            suggestionTextBox.SuppressExactMatch = true;

            if (enableF4)
            {
                RegisterF4Handler(suggestionTextBox);
            }
        }

        /// <summary>
        /// Configures a SuggestionTextBoxWithLabel for Warehouse suggestions.
        /// Standard configuration: MaxResults=50, EnableWildcards=true, ClearOnNoMatch=true, F4 button enabled.
        /// </summary>
        public static void ConfigureForWarehouses(SuggestionTextBoxWithLabel control, Func<Task<List<string>>> dataProvider)
        {
            if (control == null)
                throw new ArgumentNullException(nameof(control));

            ConfigureForWarehouses(control.TextBox, dataProvider, enableF4: false); // F4 handled by button
        }

        /// <summary>
        /// Configures a SuggestionTextBoxWithLabel for Item Type suggestions.
        /// Standard configuration: MaxResults=50, EnableWildcards=false, ClearOnNoMatch=true, F4 button enabled.
        /// </summary>
        public static void ConfigureForItemTypes(SuggestionTextBoxWithLabel control, Func<Task<List<string>>> dataProvider)
        {
            if (control == null)
                throw new ArgumentNullException(nameof(control));

            ConfigureForItemTypes(control.TextBox, dataProvider, enableF4: false); // F4 handled by button
            
        }

        /// <summary>
        /// Configures a SuggestionTextBoxWithLabel for Color Code suggestions.
        /// Standard configuration: MaxResults=20, EnableWildcards=false, ClearOnNoMatch=false, F4 button enabled.
        /// </summary>
        public static void ConfigureForColorCodes(SuggestionTextBoxWithLabel control, Func<Task<List<string>>> dataProvider)
        {
            if (control == null)
                throw new ArgumentNullException(nameof(control));

            ConfigureForColorCodes(control.TextBox, dataProvider, enableF4: false); // F4 handled by button
            
        }

        /// <summary>
        /// Configures a SuggestionTextBox for User suggestions.
        /// Standard configuration: MaxResults=50, EnableWildcards=false, ClearOnNoMatch=true
        /// </summary>
        public static void ConfigureForUsers(SuggestionTextBox suggestionTextBox, Func<Task<List<string>>> dataProvider, bool enableF4 = true)
        {
            if (suggestionTextBox == null)
                throw new ArgumentNullException(nameof(suggestionTextBox));

            suggestionTextBox.DataProvider = dataProvider;
            suggestionTextBox.MaxResults = 50;
            suggestionTextBox.EnableWildcards = false; // User names are short, no wildcards needed
            suggestionTextBox.NoMatchAction = Enum_SuggestionNoMatchAction.ShowWarningAndClear;
            suggestionTextBox.SuppressExactMatch = true;

            if (enableF4)
            {
                RegisterF4Handler(suggestionTextBox);
            }

            
        }

        /// <summary>
        /// Configures a SuggestionTextBoxWithLabel for User suggestions.
        /// Standard configuration: MaxResults=50, EnableWildcards=false, ClearOnNoMatch=true, F4 button enabled.
        /// </summary>
        public static void ConfigureForUsers(SuggestionTextBoxWithLabel control, Func<Task<List<string>>> dataProvider)
        {
            if (control == null)
                throw new ArgumentNullException(nameof(control));

            ConfigureForUsers(control.TextBox, dataProvider, enableF4: false); // F4 handled by button
            
        }

        /// <summary>
        /// Configures a SuggestionTextBox for Work Order suggestions.
        /// Standard configuration: MaxResults=50, EnableWildcards=true, ClearOnNoMatch=true
        /// </summary>
        public static void ConfigureForWorkOrders(SuggestionTextBox suggestionTextBox, Func<Task<List<string>>> dataProvider, bool enableF4 = true)
        {
            if (suggestionTextBox == null)
                throw new ArgumentNullException(nameof(suggestionTextBox));

            suggestionTextBox.DataProvider = dataProvider;
            suggestionTextBox.MaxResults = 50;
            suggestionTextBox.EnableWildcards = true;
            suggestionTextBox.NoMatchAction = Enum_SuggestionNoMatchAction.ShowWarningAndClear;
            suggestionTextBox.SuppressExactMatch = true;

            if (enableF4)
            {
                RegisterF4Handler(suggestionTextBox);
            }
        }

        /// <summary>
        /// Configures a SuggestionTextBoxWithLabel for Work Order suggestions.
        /// </summary>
        public static void ConfigureForWorkOrders(SuggestionTextBoxWithLabel control, Func<Task<List<string>>> dataProvider)
        {
            if (control == null) throw new ArgumentNullException(nameof(control));
            ConfigureForWorkOrders(control.TextBox, dataProvider, enableF4: false);
        }

        /// <summary>
        /// Configures a SuggestionTextBox for Purchase Order suggestions.
        /// Standard configuration: MaxResults=50, EnableWildcards=true, ClearOnNoMatch=true
        /// </summary>
        public static void ConfigureForPurchaseOrders(SuggestionTextBox suggestionTextBox, Func<Task<List<string>>> dataProvider, bool enableF4 = true)
        {
            if (suggestionTextBox == null)
                throw new ArgumentNullException(nameof(suggestionTextBox));

            suggestionTextBox.DataProvider = dataProvider;
            suggestionTextBox.MaxResults = 50;
            suggestionTextBox.EnableWildcards = true;
            suggestionTextBox.NoMatchAction = Enum_SuggestionNoMatchAction.ShowWarningAndClear;
            suggestionTextBox.SuppressExactMatch = true;

            if (enableF4)
            {
                RegisterF4Handler(suggestionTextBox);
            }
        }

        /// <summary>
        /// Configures a SuggestionTextBoxWithLabel for Purchase Order suggestions.
        /// </summary>
        public static void ConfigureForPurchaseOrders(SuggestionTextBoxWithLabel control, Func<Task<List<string>>> dataProvider)
        {
            if (control == null) throw new ArgumentNullException(nameof(control));
            ConfigureForPurchaseOrders(control.TextBox, dataProvider, enableF4: false);
        }

        /// <summary>
        /// Configures a SuggestionTextBox for Customer Order suggestions.
        /// Standard configuration: MaxResults=50, EnableWildcards=true, ClearOnNoMatch=true
        /// </summary>
        public static void ConfigureForCustomerOrders(SuggestionTextBox suggestionTextBox, Func<Task<List<string>>> dataProvider, bool enableF4 = true)
        {
            if (suggestionTextBox == null)
                throw new ArgumentNullException(nameof(suggestionTextBox));

            suggestionTextBox.DataProvider = dataProvider;
            suggestionTextBox.MaxResults = 50;
            suggestionTextBox.EnableWildcards = true;
            suggestionTextBox.NoMatchAction = Enum_SuggestionNoMatchAction.ShowWarningAndClear;
            suggestionTextBox.SuppressExactMatch = true;

            if (enableF4)
            {
                RegisterF4Handler(suggestionTextBox);
            }
        }

        /// <summary>
        /// Configures a SuggestionTextBoxWithLabel for Customer Order suggestions.
        /// </summary>
        public static void ConfigureForCustomerOrders(SuggestionTextBoxWithLabel control, Func<Task<List<string>>> dataProvider)
        {
            if (control == null) throw new ArgumentNullException(nameof(control));
            ConfigureForCustomerOrders(control.TextBox, dataProvider, enableF4: false);
        }

        /// <summary>
        /// Gets buildings from a static list.
        /// </summary>
        /// <returns>List of buildings</returns>
        public static Task<List<string>> GetCachedBuildingsAsync()
        {
            return Task.FromResult(new List<string> { "Expo", "Vits", "KK Warehouse", "Other" });
        }

        /// <summary>
        /// Configures a SuggestionTextBoxWithLabel for Building suggestions.
        /// Standard configuration: MaxResults=20, EnableWildcards=false, ClearOnNoMatch=true, F4 button enabled.
        /// </summary>
        public static void ConfigureForBuildings(SuggestionTextBoxWithLabel control)
        {
            if (control == null)
                throw new ArgumentNullException(nameof(control));

            var suggestionTextBox = control.TextBox;
            suggestionTextBox.DataProvider = GetCachedBuildingsAsync;
            suggestionTextBox.MaxResults = 20;
            suggestionTextBox.EnableWildcards = false;
            suggestionTextBox.NoMatchAction = Enum_SuggestionNoMatchAction.ShowWarningAndClear;
            suggestionTextBox.SuppressExactMatch = true;
        }

        #region Infor Visual Configuration Helpers

        public static void ConfigureForInforPartNumbers(SuggestionTextBox suggestionTextBox, bool enableF4 = true)
        {
            ConfigureForPartNumbers(suggestionTextBox, GetCachedInforPartNumbersAsync, enableF4);
        }

        public static void ConfigureForInforPartNumbers(SuggestionTextBoxWithLabel control)
        {
            ConfigureForPartNumbers(control.TextBox, GetCachedInforPartNumbersAsync, enableF4: false);
        }

        public static void ConfigureForInforUsers(SuggestionTextBox suggestionTextBox, bool enableF4 = true)
        {
            ConfigureForUsers(suggestionTextBox, GetCachedInforUsersAsync, enableF4);
        }

        public static void ConfigureForInforUsers(SuggestionTextBoxWithLabel control)
        {
            ConfigureForUsers(control.TextBox, GetCachedInforUsersAsync, enableF4: false);
        }

        public static void ConfigureForInforLocations(SuggestionTextBox suggestionTextBox, bool enableF4 = true)
        {
            ConfigureForLocations(suggestionTextBox, GetCachedInforLocationsAsync, enableF4);
        }

        public static void ConfigureForInforLocations(SuggestionTextBoxWithLabel control)
        {
            ConfigureForLocations(control.TextBox, GetCachedInforLocationsAsync, enableF4: false);
        }

        public static void ConfigureForInforWarehouses(SuggestionTextBox suggestionTextBox, bool enableF4 = true)
        {
            ConfigureForWarehouses(suggestionTextBox, GetCachedInforWarehousesAsync, enableF4);
        }

        public static void ConfigureForInforWarehouses(SuggestionTextBoxWithLabel control)
        {
            ConfigureForWarehouses(control.TextBox, GetCachedInforWarehousesAsync, enableF4: false);
        }

        public static void ConfigureForInforWorkOrders(SuggestionTextBox suggestionTextBox, bool enableF4 = true)
        {
            ConfigureForWorkOrders(suggestionTextBox, GetCachedInforWorkOrdersAsync, enableF4);
        }

        public static void ConfigureForInforWorkOrders(SuggestionTextBoxWithLabel control)
        {
            ConfigureForWorkOrders(control.TextBox, GetCachedInforWorkOrdersAsync, enableF4: false);
        }

        public static void ConfigureForInforPurchaseOrders(SuggestionTextBox suggestionTextBox, bool enableF4 = true)
        {
            ConfigureForPurchaseOrders(suggestionTextBox, GetCachedInforPurchaseOrdersAsync, enableF4);
        }

        public static void ConfigureForInforPurchaseOrders(SuggestionTextBoxWithLabel control)
        {
            ConfigureForPurchaseOrders(control.TextBox, GetCachedInforPurchaseOrdersAsync, enableF4: false);
        }

        public static void ConfigureForInforCustomerOrders(SuggestionTextBox suggestionTextBox, bool enableF4 = true)
        {
            ConfigureForCustomerOrders(suggestionTextBox, GetCachedInforCustomerOrdersAsync, enableF4);
        }

        public static void ConfigureForInforCustomerOrders(SuggestionTextBoxWithLabel control)
        {
            ConfigureForCustomerOrders(control.TextBox, GetCachedInforCustomerOrdersAsync, enableF4: false);
        }

        public static void ConfigureForInforFGTNumbers(SuggestionTextBox suggestionTextBox, bool enableF4 = true)
        {
            ConfigureForPartNumbers(suggestionTextBox, GetCachedInforFGTNumbersAsync, enableF4);
        }

        public static void ConfigureForInforFGTNumbers(SuggestionTextBoxWithLabel control)
        {
            ConfigureForPartNumbers(control.TextBox, GetCachedInforFGTNumbersAsync, enableF4: false);
        }

        public static void ConfigureForInforCoilFlatstockNumbers(SuggestionTextBox suggestionTextBox, bool enableF4 = true)
        {
            ConfigureForPartNumbers(suggestionTextBox, GetCachedInforCoilFlatstockNumbersAsync, enableF4);
        }

        public static void ConfigureForInforCoilFlatstockNumbers(SuggestionTextBoxWithLabel control)
        {
            ConfigureForPartNumbers(control.TextBox, GetCachedInforCoilFlatstockNumbersAsync, enableF4: false);
        }

        #endregion

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
