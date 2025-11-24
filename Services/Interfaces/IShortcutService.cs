using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Services
{
    public interface IShortcutService
    {
        /// <summary>
        /// Initializes the service for the specified user, loading their shortcuts or defaults.
        /// </summary>
        Task InitializeAsync(string userName);

        /// <summary>
        /// Gets the shortcut keys for a specific action.
        /// </summary>
        Keys GetShortcutKey(string shortcutName);

        /// <summary>
        /// Gets the display string for a shortcut (e.g. "Ctrl+S").
        /// </summary>
        string GetShortcutDisplay(string shortcutName);

        /// <summary>
        /// Updates a user's shortcut preference.
        /// </summary>
        Task<Model_Dao_Result<bool>> UpdateShortcutAsync(string shortcutName, Keys newKeys);

        /// <summary>
        /// Resets all shortcuts to system defaults for the current user.
        /// </summary>
        Task<bool> ResetToDefaultsAsync();

        /// <summary>
        /// Gets all shortcuts for the current user.
        /// </summary>
        List<Model_Shortcut> GetAllShortcuts();

        /// <summary>
        /// Checks if the provided key data matches the Delete shortcut.
        /// </summary>
        bool IsDelete(Keys keyData);

        /// <summary>
        /// Checks if the key combination is reserved.
        /// </summary>
        bool IsReservedKey(Keys keyData);

        /// <summary>
        /// Checks if the key combination is already in use by another shortcut.
        /// </summary>
        bool IsDuplicate(Keys keyData, string excludeShortcutName);
    }
}
