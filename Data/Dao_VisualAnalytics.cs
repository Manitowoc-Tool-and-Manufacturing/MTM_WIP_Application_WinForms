using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Models.Analytics;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;

namespace MTM_WIP_Application_Winforms.Data
{
    /// <summary>
    /// Implementation of IDao_VisualAnalytics for accessing Infor Visual analytics data.
    /// </summary>
    public class Dao_VisualAnalytics : IDao_VisualAnalytics
    {
        /// <summary>
        /// Gets the current metadata from sys_visual table.
        /// </summary>
        /// <returns>Model_Dao_Result containing the sys_visual record.</returns>
        public async Task<Model_Dao_Result<Model_Visual_SysVisual>> GetSysVisualDataAsync()
        {
            try
            {
                var connectionString = Helper_Database_Variables.GetConnectionString(null, null, null, null);
                var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                    connectionString,
                    "md_sys_visual_Get",
                    null);

                if (!result.IsSuccess)
                {
                    return Model_Dao_Result<Model_Visual_SysVisual>.Failure(result.ErrorMessage);
                }

                if (result.Data == null || result.Data.Rows.Count == 0)
                {
                    // Return default empty model if no data exists yet
                    return Model_Dao_Result<Model_Visual_SysVisual>.Success(new Model_Visual_SysVisual());
                }

                var row = result.Data.Rows[0];
                var model = new Model_Visual_SysVisual
                {
                    Id = Convert.ToInt32(row["id"]),
                    JsonShiftData = row["json_shift_data"]?.ToString() ?? "{}",
                    JsonUserFullNames = row["json_user_fullnames"]?.ToString() ?? "{}",
                    LastUpdated = Convert.ToDateTime(row["last_updated"])
                };

                return Model_Dao_Result<Model_Visual_SysVisual>.Success(model);
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleDatabaseError(ex, callerName: nameof(GetSysVisualDataAsync));
                return Model_Dao_Result<Model_Visual_SysVisual>.Failure("Failed to retrieve Visual metadata.", ex);
            }
        }

        /// <summary>
        /// Updates the sys_visual table with new JSON data.
        /// </summary>
        /// <param name="jsonShifts">JSON string for shift data.</param>
        /// <param name="jsonNames">JSON string for user full names.</param>
        /// <returns>Model_Dao_Result indicating success.</returns>
        public async Task<Model_Dao_Result<bool>> UpdateSysVisualDataAsync(string jsonShifts, string jsonNames)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    { "JsonShifts", jsonShifts },
                    { "JsonNames", jsonNames }
                };

                var connectionString = Helper_Database_Variables.GetConnectionString(null, null, null, null);
                var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                    connectionString,
                    "md_sys_visual_Update",
                    parameters);

                if (result.IsSuccess)
                {
                    return Model_Dao_Result<bool>.Success(true, result.StatusMessage);
                }
                else
                {
                    return Model_Dao_Result<bool>.Failure(result.ErrorMessage, result.Exception);
                }
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleDatabaseError(ex, callerName: nameof(UpdateSysVisualDataAsync));
                return Model_Dao_Result<bool>.Failure("Failed to update Visual metadata.", ex);
            }
        }
    }
}
