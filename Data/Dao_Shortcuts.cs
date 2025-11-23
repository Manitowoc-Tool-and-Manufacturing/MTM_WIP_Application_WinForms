using System.Data;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Data
{
    public class Dao_Shortcuts : IDao_Shortcuts
    {
        public async Task<Model_Dao_Result<DataTable>> GetAllSystemShortcutsAsync()
        {
            return await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                Model_Application_Variables.ConnectionString, "sys_shortcuts_GetAll", null);
        }

        public async Task<Model_Dao_Result<DataTable>> GetUserShortcutsAsync(string userName)
        {
            var parameters = new Dictionary<string, object>
            {
                { "UserName", userName }
            };
            return await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                Model_Application_Variables.ConnectionString, "usr_user_shortcuts_GetByUser", parameters);
        }

        public async Task<Model_Dao_Result<bool>> UpsertUserShortcutAsync(string userName, string shortcutName, int customKeys)
        {
            var parameters = new Dictionary<string, object>
            {
                { "UserName", userName },
                { "ShortcutName", shortcutName },
                { "CustomKeys", customKeys }
            };
            var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                Model_Application_Variables.ConnectionString, "usr_user_shortcuts_Upsert", parameters);
            
            if (result.IsSuccess)
                return Model_Dao_Result<bool>.Success(true, result.StatusMessage);
            else
                return Model_Dao_Result<bool>.Failure(result.ErrorMessage);
        }

        public async Task<Model_Dao_Result<bool>> ResetUserShortcutsAsync(string userName)
        {
            var parameters = new Dictionary<string, object>
            {
                { "UserName", userName }
            };
            var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                Model_Application_Variables.ConnectionString, "usr_user_shortcuts_Reset", parameters);

            if (result.IsSuccess)
                return Model_Dao_Result<bool>.Success(true, result.StatusMessage);
            else
                return Model_Dao_Result<bool>.Failure(result.ErrorMessage);
        }
    }
}
