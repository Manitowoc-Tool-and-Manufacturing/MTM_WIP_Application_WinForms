using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Models;
using System.Data;
using System.Text.Json;

namespace MTM_WIP_Application_Winforms.Services
{
    public class Service_Shortcut : IShortcutService
    {
        private readonly IDao_Shortcuts _dao;
        private readonly Dictionary<string, Model_Shortcut> _shortcutCache = new();
        private string _currentUserName = string.Empty;

        public Service_Shortcut(IDao_Shortcuts dao)
        {
            _dao = dao;
        }

        public async Task InitializeAsync(string userName)
        {
            _currentUserName = userName;
            bool hasCustom = await RefreshCacheAsync();

            // If no custom shortcuts found (meaning user relies entirely on system defaults or nothing),
            // load defaults from JSON and populate for user so they have their own copy.
            if (!hasCustom)
            {
                await PopulateDefaultsFromJsonAsync();
                await RefreshCacheAsync();
            }
        }

        private async Task PopulateDefaultsFromJsonAsync()
        {
            try
            {
                string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "default_shortcuts.json");
                if (File.Exists(jsonPath))
                {
                    string jsonContent = await File.ReadAllTextAsync(jsonPath);
                    var defaults = JsonSerializer.Deserialize<List<Model_Shortcut>>(jsonContent);

                    if (defaults != null)
                    {
                        foreach (var shortcut in defaults)
                        {
                            // Insert as user shortcut since system shortcuts might be empty or read-only
                            await _dao.UpsertUserShortcutAsync(_currentUserName, shortcut.Name, (int)shortcut.Keys);
                        }
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"Default shortcuts JSON not found at: {jsonPath}");
                }
            }
            catch (Exception ex)
            {
                // Log error but don't crash
                System.Diagnostics.Debug.WriteLine($"Failed to populate default shortcuts: {ex.Message}");
            }
        }

        private async Task<bool> RefreshCacheAsync()
        {
            _shortcutCache.Clear();
            bool hasCustomShortcuts = false;
            
            // Get user shortcuts (which includes system defaults if no override)
            var result = await _dao.GetUserShortcutsAsync(_currentUserName);
            if (result.IsSuccess && result.Data != null)
            {
                foreach (DataRow row in result.Data.Rows)
                {
                    // Check IsCustom column to see if user has any overrides
                    if (row.Table.Columns.Contains("IsCustom") && row["IsCustom"] != DBNull.Value && Convert.ToInt32(row["IsCustom"]) == 1)
                    {
                        hasCustomShortcuts = true;
                    }

                    var shortcut = new Model_Shortcut
                    {
                        Name = row["ShortcutName"].ToString() ?? string.Empty,
                        Keys = (Keys)Convert.ToInt32(row["EffectiveKeys"]),
                        Description = row["Description"].ToString() ?? string.Empty,
                        Category = row["Category"].ToString() ?? string.Empty
                    };
                    _shortcutCache[shortcut.Name] = shortcut;
                }
            }
            
            return hasCustomShortcuts;
        }

        public Keys GetShortcutKey(string shortcutName)
        {
            if (_shortcutCache.TryGetValue(shortcutName, out var shortcut))
            {
                return shortcut.Keys;
            }
            return Keys.None;
        }

        public string GetShortcutDisplay(string shortcutName)
        {
            if (_shortcutCache.TryGetValue(shortcutName, out var shortcut))
            {
                return shortcut.DisplayString;
            }
            return string.Empty;
        }

        public async Task<Model_Dao_Result<bool>> UpdateShortcutAsync(string shortcutName, Keys newKeys)
        {
            if (string.IsNullOrEmpty(_currentUserName)) 
                return new Model_Dao_Result<bool> { IsSuccess = false, ErrorMessage = "User not initialized." };

            // Validation
            if (IsReservedKey(newKeys))
            {
                return new Model_Dao_Result<bool> { IsSuccess = false, ErrorMessage = "This key combination is reserved for Quick Buttons (Alt+0-9)." };
            }

            if (IsDuplicate(newKeys, shortcutName))
            {
                return new Model_Dao_Result<bool> { IsSuccess = false, ErrorMessage = "This key combination is already in use." };
            }

            var result = await _dao.UpsertUserShortcutAsync(_currentUserName, shortcutName, (int)newKeys);
            if (result.IsSuccess)
            {
                // Update cache immediately
                if (_shortcutCache.TryGetValue(shortcutName, out var shortcut))
                {
                    shortcut.Keys = newKeys;
                }
                return new Model_Dao_Result<bool> { IsSuccess = true, Data = true };
            }
            return new Model_Dao_Result<bool> { IsSuccess = false, ErrorMessage = result.ErrorMessage };
        }

        public async Task<bool> ResetToDefaultsAsync()
        {
            if (string.IsNullOrEmpty(_currentUserName)) return false;

            var result = await _dao.ResetUserShortcutsAsync(_currentUserName);
            if (result.IsSuccess)
            {
                await RefreshCacheAsync();
                return true;
            }
            return false;
        }

        public List<Model_Shortcut> GetAllShortcuts()
        {
            return _shortcutCache.Values.ToList();
        }

        public bool IsDelete(Keys keyData)
        {
            var deleteKey = GetShortcutKey("Delete");
            if (deleteKey == Keys.None) deleteKey = Keys.Delete; // Default
            return keyData == deleteKey;
        }

        public bool IsReservedKey(Keys keyData)
        {
            // Check if key is Alt+0 through Alt+9 (QuickButtons)
            // Alt modifier is Keys.Alt (262144)
            // D0 is 48, D9 is 57
            
            if ((keyData & Keys.Alt) == Keys.Alt)
            {
                Keys keyCode = keyData & Keys.KeyCode;
                if (keyCode >= Keys.D0 && keyCode <= Keys.D9)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsDuplicate(Keys keyData, string excludeShortcutName)
        {
            if (keyData == Keys.None) return false;

            foreach (var shortcut in _shortcutCache.Values)
            {
                if (shortcut.Name == excludeShortcutName) continue;

                if (shortcut.Keys == keyData)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
