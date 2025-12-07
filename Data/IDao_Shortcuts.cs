using System.Data;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Data
{
    public interface IDao_Shortcuts
    {
        Task<Model_Dao_Result<DataTable>> GetAllSystemShortcutsAsync();
        Task<Model_Dao_Result<DataTable>> GetUserShortcutsAsync(string userName);
        Task<Model_Dao_Result<bool>> UpsertSystemShortcutAsync(string shortcutName, int defaultKeys, string description, string category);
        Task<Model_Dao_Result<bool>> UpsertUserShortcutAsync(string userName, string shortcutName, int customKeys);
        Task<Model_Dao_Result<bool>> ResetUserShortcutsAsync(string userName);
    }
}
