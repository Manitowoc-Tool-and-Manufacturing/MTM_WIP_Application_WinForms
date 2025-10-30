using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Logging;
using MySql.Data.MySqlClient;

namespace MTM_WIP_Application_Winforms.Data
{
    /// <summary>
    /// Data Access Object for parameter prefix override management.
    /// Wraps sys_parameter_prefix_overrides stored procedures with DaoResult pattern.
    /// </summary>
    internal static class Dao_ParameterPrefixOverrides
    {
        #region Query Methods

        /// <summary>
        /// Retrieves all active parameter prefix overrides.
        /// Used for loading the startup cache in Helper_Database_StoredProcedure.
        /// </summary>
        /// <returns>DaoResult containing list of active overrides, or empty list if none exist.</returns>
        public static async Task<DaoResult<List<Model_ParameterPrefixOverride>>> GetAllActiveAsync()
        {
            try
            {
                var connectionString = Helper_Database_Variables.GetConnectionString(null, null, null, null);
                
                var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                    connectionString,
                    "sys_parameter_prefix_overrides_Get_All",
                    new Dictionary<string, object>(),
                    progressHelper: null);

                if (!result.IsSuccess)
                {
                    if (result.Exception != null)
                    {
                        LoggingUtility.LogApplicationError(result.Exception);
                    }
                    return DaoResult<List<Model_ParameterPrefixOverride>>.Failure(result.ErrorMessage);
                }

                var overrides = new List<Model_ParameterPrefixOverride>();
                
                if (result.Data != null && result.Data.Rows.Count > 0)
                {
                    foreach (DataRow row in result.Data.Rows)
                    {
                        overrides.Add(MapDataRowToModel(row));
                    }
                }

                return DaoResult<List<Model_ParameterPrefixOverride>>.Success(
                    overrides, 
                    $"Retrieved {overrides.Count} active override(s)");
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                return DaoResult<List<Model_ParameterPrefixOverride>>.Failure($"Unexpected error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a single parameter prefix override by ID.
        /// </summary>
        /// <param name="overrideId">The unique identifier of the override.</param>
        /// <returns>DaoResult containing the override, or failure if not found.</returns>
        public static async Task<DaoResult<Model_ParameterPrefixOverride>> GetByIdAsync(int overrideId)
        {
            try
            {
                if (overrideId <= 0)
                {
                    return DaoResult<Model_ParameterPrefixOverride>.Failure("Invalid OverrideId - must be positive integer");
                }

                var connectionString = Helper_Database_Variables.GetConnectionString(null, null, null, null);
                
                var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                    connectionString,
                    "sys_parameter_prefix_overrides_Get_ById",
                    new Dictionary<string, object> { ["OverrideId"] = overrideId },
                    progressHelper: null);

                if (!result.IsSuccess)
                {
                    if (result.Exception != null)
                    {
                        LoggingUtility.LogApplicationError(result.Exception);
                    }
                    return DaoResult<Model_ParameterPrefixOverride>.Failure(result.ErrorMessage);
                }

                if (result.Data == null || result.Data.Rows.Count == 0)
                {
                    return DaoResult<Model_ParameterPrefixOverride>.Failure($"Override {overrideId} not found");
                }

                var model = MapDataRowToModel(result.Data.Rows[0]);
                return DaoResult<Model_ParameterPrefixOverride>.Success(model, "Override retrieved successfully");
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                return DaoResult<Model_ParameterPrefixOverride>.Failure($"Unexpected error: {ex.Message}");
            }
        }

        #endregion

        #region CRUD Methods

        /// <summary>
        /// Adds a new parameter prefix override.
        /// </summary>
        /// <param name="model">The override to add (OverrideId will be populated on success).</param>
        /// <returns>DaoResult containing the new OverrideId on success.</returns>
        public static async Task<DaoResult<int>> AddAsync(Model_ParameterPrefixOverride model)
        {
            try
            {
                if (model == null)
                {
                    return DaoResult<int>.Failure("Override cannot be null");
                }

                // Validation
                if (string.IsNullOrWhiteSpace(model.ProcedureName))
                {
                    return DaoResult<int>.Failure("ProcedureName is required");
                }
                if (string.IsNullOrWhiteSpace(model.ParameterName))
                {
                    return DaoResult<int>.Failure("ParameterName is required");
                }
                if (string.IsNullOrWhiteSpace(model.CreatedBy))
                {
                    return DaoResult<int>.Failure("CreatedBy is required");
                }
                if (model.OverridePrefix == null)
                {
                    return DaoResult<int>.Failure("OverridePrefix cannot be null (use empty string for no prefix)");
                }

                var connectionString = Helper_Database_Variables.GetConnectionString(null, null, null, null);
                
                var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                    connectionString,
                    "sys_parameter_prefix_overrides_Add",
                    new Dictionary<string, object>
                    {
                        ["ProcedureName"] = model.ProcedureName,
                        ["ParameterName"] = model.ParameterName,
                        ["OverridePrefix"] = model.OverridePrefix,
                        ["Reason"] = model.Reason ?? (object)DBNull.Value,
                        ["CreatedBy"] = model.CreatedBy
                    },
                    progressHelper: null);

                if (!result.IsSuccess)
                {
                    if (result.Exception != null)
                    {
                        LoggingUtility.LogApplicationError(result.Exception);
                    }
                    
                    // Check error message for duplicate
                    if (result.ErrorMessage.Contains("already exists"))
                    {
                        return DaoResult<int>.Failure("Override already exists for this procedure-parameter combination");
                    }
                    
                    return DaoResult<int>.Failure(result.ErrorMessage);
                }

                // Query back to get the new ID (since OUT parameters aren't exposed by helper yet)
                var getAllResult = await GetAllActiveAsync();
                if (getAllResult.IsSuccess && getAllResult.Data != null)
                {
                    var newOverride = getAllResult.Data.Find(o => 
                        o.ProcedureName == model.ProcedureName && 
                        o.ParameterName == model.ParameterName);
                    
                    if (newOverride != null)
                    {
                        model.OverrideId = newOverride.OverrideId;
                        return DaoResult<int>.Success(newOverride.OverrideId, "Override created successfully");
                    }
                }

                return DaoResult<int>.Success(0, "Override created but ID could not be retrieved");
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                return DaoResult<int>.Failure($"Unexpected error: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates an existing parameter prefix override.
        /// </summary>
        /// <param name="model">The override with updated values.</param>
        /// <returns>DaoResult indicating success or failure.</returns>
        public static async Task<DaoResult> UpdateAsync(Model_ParameterPrefixOverride model,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
        {
            try
            {
                if (model == null)
                {
                    return DaoResult.Failure("Override cannot be null");
                }

                // Validation
                if (model.OverrideId <= 0)
                {
                    return DaoResult.Failure("Invalid OverrideId");
                }
                if (string.IsNullOrWhiteSpace(model.ProcedureName))
                {
                    return DaoResult.Failure("ProcedureName is required");
                }
                if (string.IsNullOrWhiteSpace(model.ParameterName))
                {
                    return DaoResult.Failure("ParameterName is required");
                }
                if (string.IsNullOrWhiteSpace(model.ModifiedBy))
                {
                    return DaoResult.Failure("ModifiedBy is required");
                }
                if (model.OverridePrefix == null)
                {
                    return DaoResult.Failure("OverridePrefix cannot be null");
                }

                var connectionString = Helper_Database_Variables.GetConnectionString(null, null, null, null);
                
                var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                    connectionString,
                    "sys_parameter_prefix_overrides_Update_ById",
                    new Dictionary<string, object>
                    {
                        ["OverrideId"] = model.OverrideId,
                        ["ProcedureName"] = model.ProcedureName,
                        ["ParameterName"] = model.ParameterName,
                        ["OverridePrefix"] = model.OverridePrefix,
                        ["Reason"] = model.Reason ?? (object)DBNull.Value,
                        ["ModifiedBy"] = model.ModifiedBy
                    },
                    progressHelper: null);

                if (!result.IsSuccess)
                {
                    if (result.Exception != null)
                    {
                        LoggingUtility.LogApplicationError(result.Exception);
                    }
                    
                    // Check specific error conditions
                    if (result.ErrorMessage.Contains("already exists"))
                    {
                        return DaoResult.Failure("Override already exists for this procedure-parameter combination");
                    }
                    if (result.ErrorMessage.Contains("not found"))
                    {
                        return DaoResult.Failure($"Override {model.OverrideId} not found");
                    }
                    
                    return DaoResult.Failure(result.ErrorMessage);
                }

                return DaoResult.Success("Override updated successfully");
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                return DaoResult.Failure($"Unexpected error: {ex.Message}");
            }
        }

        /// <summary>
        /// Soft-deletes a parameter prefix override (sets IsActive = 0).
        /// Idempotent - deleting an already-deleted override returns success.
        /// </summary>
        /// <param name="overrideId">The ID of the override to delete.</param>
        /// <param name="modifiedBy">User performing the deletion.</param>
        /// <returns>DaoResult indicating success or failure.</returns>
        public static async Task<DaoResult> DeleteAsync(int overrideId, string modifiedBy,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
        {
            try
            {
                if (overrideId <= 0)
                {
                    return DaoResult.Failure("Invalid OverrideId");
                }
                if (string.IsNullOrWhiteSpace(modifiedBy))
                {
                    return DaoResult.Failure("ModifiedBy is required");
                }

                var connectionString = Helper_Database_Variables.GetConnectionString(null, null, null, null);
                
                var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                    connectionString,
                    "sys_parameter_prefix_overrides_Delete_ById",
                    new Dictionary<string, object>
                    {
                        ["OverrideId"] = overrideId,
                        ["ModifiedBy"] = modifiedBy
                    },
                    progressHelper: null);

                if (!result.IsSuccess)
                {
                    if (result.Exception != null)
                    {
                        LoggingUtility.LogApplicationError(result.Exception);
                    }
                    
                    if (result.ErrorMessage.Contains("not found"))
                    {
                        return DaoResult.Failure($"Override {overrideId} not found");
                    }
                    
                    return DaoResult.Failure(result.ErrorMessage);
                }

                return DaoResult.Success("Override deleted successfully");
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                return DaoResult.Failure($"Unexpected error: {ex.Message}");
            }
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Maps a DataRow to Model_ParameterPrefixOverride.
        /// </summary>
        private static Model_ParameterPrefixOverride MapDataRowToModel(DataRow row)
        {
            return new Model_ParameterPrefixOverride
            {
                OverrideId = Convert.ToInt32(row["OverrideId"]),
                ProcedureName = row["ProcedureName"]?.ToString() ?? string.Empty,
                ParameterName = row["ParameterName"]?.ToString() ?? string.Empty,
                OverridePrefix = row["OverridePrefix"]?.ToString() ?? string.Empty,
                Reason = row["Reason"] == DBNull.Value ? null : row["Reason"]?.ToString(),
                CreatedBy = row["CreatedBy"]?.ToString() ?? string.Empty,
                CreatedDate = Convert.ToDateTime(row["CreatedDate"]),
                ModifiedBy = row["ModifiedBy"] == DBNull.Value ? null : row["ModifiedBy"]?.ToString(),
                ModifiedDate = row["ModifiedDate"] == DBNull.Value ? null : Convert.ToDateTime(row["ModifiedDate"]),
                IsActive = Convert.ToBoolean(row["IsActive"])
            };
        }

        #endregion
    }
}
