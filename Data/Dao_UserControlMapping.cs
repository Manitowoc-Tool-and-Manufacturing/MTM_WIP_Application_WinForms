using System.Data;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Models;
using MySql.Data.MySqlClient;

namespace MTM_WIP_Application_Winforms.Data
{
    /// <summary>
    /// Data Access Object for UserControlMapping operations.
    /// </summary>
    public class Dao_UserControlMapping : IDao_UserControlMapping
    {
        /// <inheritdoc/>
        public async Task<Model_Dao_Result<DataTable>> GetByWindowAsync(int windowFormMappingId, bool includeInactive = false)
        {
            return await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                Model_Application_Variables.ConnectionString,
                "sys_usercontrol_mapping_GetByWindow",
                new Dictionary<string, object> 
                { 
                    { "WindowFormMappingID", windowFormMappingId },
                    { "IncludeInactive", includeInactive ? 1 : 0 } 
                });
        }

        /// <inheritdoc/>
        public async Task<Model_Dao_Result<int>> UpsertAsync(int windowFormMappingId, string codebaseName, string userFriendlyName, bool isActive)
        {
            var parameters = new Dictionary<string, object>
            {
                { "WindowFormMappingID", windowFormMappingId },
                { "CodebaseName", codebaseName },
                { "UserFriendlyName", userFriendlyName },
                { "IsActive", isActive ? 1 : 0 }
            };

            var outputParams = new List<string> { "MappingID" };

            var result = await Helper_Database_StoredProcedure.ExecuteWithCustomOutputAsync(
                Model_Application_Variables.ConnectionString,
                "sys_usercontrol_mapping_Upsert",
                parameters,
                outputParams);

            if (result.IsSuccess && result.Data != null)
            {
                if (result.Data.TryGetValue("MappingID", out var idObj) && int.TryParse(idObj?.ToString(), out int id))
                {
                    return Model_Dao_Result<int>.Success(id);
                }
            }
            return Model_Dao_Result<int>.Failure(result.ErrorMessage ?? "Failed to upsert mapping");
        }
    }
}
