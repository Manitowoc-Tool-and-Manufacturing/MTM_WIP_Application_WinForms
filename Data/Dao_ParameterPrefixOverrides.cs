using System.Data;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Logging;
using MySql.Data.MySqlClient;

namespace MTM_WIP_Application_Winforms.Data
{
    /// <summary>
    /// Data Access Object for parameter prefix override management.
    /// Wraps sys_parameter_prefix_overrides stored procedures with Model_Dao_Result pattern.
    /// </summary>
    internal static class Dao_ParameterPrefixOverrides
    {
        #region Query Methods

        /// <summary>
        /// Retrieves all active parameter prefix overrides.
        /// Used for loading the startup cache in Helper_Database_StoredProcedure.
        /// </summary>
        /// <returns>Model_Dao_Result containing list of active overrides, or empty list if none exist.</returns>
        public static async Task<Model_Dao_Result<List<Model_ParameterPrefix_Override>>> GetAllActiveAsync(
            MySqlConnection? connection = null,
            MySqlTransaction? transaction = null)
        {
            try
            {
                var connectionString = Helper_Database_Variables.GetConnectionString(null, null, null, null);

                var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                    connectionString,
                    "sys_parameter_prefix_overrides_Get_All",
                    new Dictionary<string, object>(),
                    progressHelper: null,
                    connection: connection,
                    transaction: transaction);

                if (!result.IsSuccess)
                {
                    if (result.Exception != null)
                    {
                        LoggingUtility.LogApplicationError(result.Exception);
                    }
                    return Model_Dao_Result<List<Model_ParameterPrefix_Override>>.Failure(result.ErrorMessage);
                }

                var overrides = new List<Model_ParameterPrefix_Override>();

                if (result.Data != null && result.Data.Rows.Count > 0)
                {
                    foreach (DataRow row in result.Data.Rows)
                    {
                        overrides.Add(MapDataRowToModel(row));
                    }
                }

                return Model_Dao_Result<List<Model_ParameterPrefix_Override>>.Success(
                    overrides,
                    $"Retrieved {overrides.Count} active override(s)");
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                return Model_Dao_Result<List<Model_ParameterPrefix_Override>>.Failure($"Unexpected error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a single parameter prefix override by ID.
        /// </summary>
        /// <param name="overrideId">The unique identifier of the override.</param>
        /// <returns>Model_Dao_Result containing the override, or failure if not found.</returns>
        public static async Task<Model_Dao_Result<Model_ParameterPrefix_Override>> GetByIdAsync(int overrideId,
            MySqlConnection? connection = null,
            MySqlTransaction? transaction = null)
        {
            try
            {
                if (overrideId <= 0)
                {
                    return Model_Dao_Result<Model_ParameterPrefix_Override>.Failure("Invalid OverrideId - must be positive integer");
                }

                var connectionString = Helper_Database_Variables.GetConnectionString(null, null, null, null);

                var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                    connectionString,
                    "sys_parameter_prefix_overrides_Get_ById",
                    new Dictionary<string, object> { ["OverrideId"] = overrideId },
                    progressHelper: null,
                    connection: connection,
                    transaction: transaction);

                if (!result.IsSuccess)
                {
                    if (result.Exception != null)
                    {
                        LoggingUtility.LogApplicationError(result.Exception);
                    }
                    return Model_Dao_Result<Model_ParameterPrefix_Override>.Failure(result.ErrorMessage);
                }

                if (result.Data == null || result.Data.Rows.Count == 0)
                {
                    return Model_Dao_Result<Model_ParameterPrefix_Override>.Failure($"Override {overrideId} not found");
                }

                var model = MapDataRowToModel(result.Data.Rows[0]);
                return Model_Dao_Result<Model_ParameterPrefix_Override>.Success(model, "Override retrieved successfully");
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                return Model_Dao_Result<Model_ParameterPrefix_Override>.Failure($"Unexpected error: {ex.Message}");
            }
        }

        #endregion

        #region CRUD Methods

        /// <summary>
        /// Adds a new parameter prefix override.
        /// </summary>
        /// <param name="model">The override to add (OverrideId will be populated on success).</param>
        /// <returns>Model_Dao_Result containing the new OverrideId on success.</returns>
        public static async Task<Model_Dao_Result<int>> AddAsync(Model_ParameterPrefix_Override model,
            MySqlConnection? connection = null,
            MySqlTransaction? transaction = null)
        {
            try
            {
                if (model == null)
                {
                    return Model_Dao_Result<int>.Failure("Override cannot be null");
                }

                // Validation
                if (string.IsNullOrWhiteSpace(model.ProcedureName))
                {
                    return Model_Dao_Result<int>.Failure("ProcedureName is required");
                }
                if (string.IsNullOrWhiteSpace(model.ParameterName))
                {
                    return Model_Dao_Result<int>.Failure("ParameterName is required");
                }
                if (string.IsNullOrWhiteSpace(model.CreatedBy))
                {
                    return Model_Dao_Result<int>.Failure("CreatedBy is required");
                }
                if (model.OverridePrefix == null)
                {
                    return Model_Dao_Result<int>.Failure("OverridePrefix cannot be null (use empty string for no prefix)");
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
                    progressHelper: null,
                    connection: connection,
                    transaction: transaction);

                if (!result.IsSuccess)
                {
                    if (result.Exception != null)
                    {
                        LoggingUtility.LogApplicationError(result.Exception);
                    }

                    // Check error message for duplicate
                    if (result.ErrorMessage.Contains("already exists"))
                    {
                        return Model_Dao_Result<int>.Failure("Override already exists for this procedure-parameter combination");
                    }

                    return Model_Dao_Result<int>.Failure(result.ErrorMessage);
                }

                // Query back to get the new ID (since OUT parameters aren't exposed by helper yet)
                var getAllResult = await GetAllActiveAsync(connection, transaction);
                if (getAllResult.IsSuccess && getAllResult.Data != null)
                {
                    var newOverride = getAllResult.Data.Find(o =>
                        o.ProcedureName == model.ProcedureName &&
                        o.ParameterName == model.ParameterName);

                    if (newOverride != null)
                    {
                        model.OverrideId = newOverride.OverrideId;
                        return Model_Dao_Result<int>.Success(newOverride.OverrideId, "Override created successfully");
                    }
                }

                return Model_Dao_Result<int>.Success(0, "Override created but ID could not be retrieved");
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                return Model_Dao_Result<int>.Failure($"Unexpected error: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates an existing parameter prefix override.
        /// </summary>
        /// <param name="model">The override with updated values.</param>
        /// <returns>Model_Dao_Result indicating success or failure.</returns>
        public static async Task<Model_Dao_Result> UpdateAsync(Model_ParameterPrefix_Override model,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
        {
            try
            {
                if (model == null)
                {
                    return Model_Dao_Result.Failure("Override cannot be null");
                }

                // Validation
                if (model.OverrideId <= 0)
                {
                    return Model_Dao_Result.Failure("Invalid OverrideId");
                }
                if (string.IsNullOrWhiteSpace(model.ProcedureName))
                {
                    return Model_Dao_Result.Failure("ProcedureName is required");
                }
                if (string.IsNullOrWhiteSpace(model.ParameterName))
                {
                    return Model_Dao_Result.Failure("ParameterName is required");
                }
                if (string.IsNullOrWhiteSpace(model.ModifiedBy))
                {
                    return Model_Dao_Result.Failure("ModifiedBy is required");
                }
                if (model.OverridePrefix == null)
                {
                    return Model_Dao_Result.Failure("OverridePrefix cannot be null");
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
                    progressHelper: null,
                    connection: connection,
                    transaction: transaction);

                if (!result.IsSuccess)
                {
                    if (result.Exception != null)
                    {
                        LoggingUtility.LogApplicationError(result.Exception);
                    }

                    // Check specific error conditions
                    if (result.ErrorMessage.Contains("already exists"))
                    {
                        return Model_Dao_Result.Failure("Override already exists for this procedure-parameter combination");
                    }
                    if (result.ErrorMessage.Contains("not found"))
                    {
                        return Model_Dao_Result.Failure($"Override {model.OverrideId} not found");
                    }

                    return Model_Dao_Result.Failure(result.ErrorMessage);
                }

                return Model_Dao_Result.Success("Override updated successfully");
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                return Model_Dao_Result.Failure($"Unexpected error: {ex.Message}");
            }
        }

        /// <summary>
        /// Soft-deletes a parameter prefix override (sets IsActive = 0).
        /// Idempotent - deleting an already-deleted override returns success.
        /// </summary>
        /// <param name="overrideId">The ID of the override to delete.</param>
        /// <param name="modifiedBy">User performing the deletion.</param>
        /// <returns>Model_Dao_Result indicating success or failure.</returns>
        public static async Task<Model_Dao_Result> DeleteAsync(int overrideId, string modifiedBy,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
        {
            try
            {
                if (overrideId <= 0)
                {
                    return Model_Dao_Result.Failure("Invalid OverrideId");
                }
                if (string.IsNullOrWhiteSpace(modifiedBy))
                {
                    return Model_Dao_Result.Failure("ModifiedBy is required");
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
                    progressHelper: null,
                    connection: connection,
                    transaction: transaction);

                if (!result.IsSuccess)
                {
                    if (result.Exception != null)
                    {
                        LoggingUtility.LogApplicationError(result.Exception);
                    }

                    if (result.ErrorMessage.Contains("not found"))
                    {
                        return Model_Dao_Result.Failure($"Override {overrideId} not found");
                    }

                    return Model_Dao_Result.Failure(result.ErrorMessage);
                }

                return Model_Dao_Result.Success("Override deleted successfully");
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                return Model_Dao_Result.Failure($"Unexpected error: {ex.Message}");
            }
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Maps a DataRow to Model_ParameterPrefix_Override.
        /// </summary>
        private static Model_ParameterPrefix_Override MapDataRowToModel(DataRow row)
        {
            return new Model_ParameterPrefix_Override
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
