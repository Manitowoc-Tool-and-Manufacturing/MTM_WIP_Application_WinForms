using System.Data;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Logging;
using MySql.Data.MySqlClient;

namespace MTM_WIP_Application_Winforms.Data;

#region Dao_Location

internal static class Dao_Location
{
    #region Delete

    internal static async Task<Model_Dao_Result> DeleteLocation(string location,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        try
        {
            var parameters = new Dictionary<string, object> { ["Location"] = location }; // p_ prefix added automatically

            var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                Model_Application_Variables.ConnectionString,
                "md_locations_Delete_ByLocation",
                parameters,
                null, // No progress helper for this method
                connection: connection,
                transaction: transaction
            );

            if (result.IsSuccess)
            {
                return Model_Dao_Result.Success($"Location {location} deleted successfully");
            }
            else
            {
                return Model_Dao_Result.Failure($"Failed to delete location {location}: {result.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: "DeleteLocation");
            return Model_Dao_Result.Failure($"Error deleting location {location}", ex);
        }
    }

    #endregion

    #region Insert

    internal static async Task<Model_Dao_Result> InsertLocation(string location, string building,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        try
        {
            var parameters = new Dictionary<string, object>
            {
                ["Location"] = location,                         // p_ prefix added automatically
                ["IssuedBy"] = Model_Application_Variables.User ?? "System",
                ["Building"] = building
            };

            var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                Model_Application_Variables.ConnectionString,
                "md_locations_Add_Location",
                parameters,
                null, // No progress helper for this method
                connection: connection,
                transaction: transaction
            );

            if (result.IsSuccess)
            {
                return Model_Dao_Result.Success($"Location {location} created successfully");
            }
            else
            {
                return Model_Dao_Result.Failure($"Failed to create location {location}: {result.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: "InsertLocation");
            return Model_Dao_Result.Failure($"Error creating location {location}", ex);
        }
    }

    #endregion

    #region Update

    internal static async Task<Model_Dao_Result> UpdateLocation(string oldLocation, string newLocation, string building,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        try
        {
            var parameters = new Dictionary<string, object>
            {
                ["OldLocation"] = oldLocation,                   // p_ prefix added automatically
                ["Location"] = newLocation,
                ["IssuedBy"] = Model_Application_Variables.User ?? "System",
                ["Building"] = building
            };

            var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                Model_Application_Variables.ConnectionString,
                "md_locations_Update_Location",
                parameters,
                null, // No progress helper for this method
                connection: connection,
                transaction: transaction
            );

            if (result.IsSuccess)
            {
                return Model_Dao_Result.Success($"Location updated from {oldLocation} to {newLocation}");
            }
            else
            {
                return Model_Dao_Result.Failure($"Failed to update location {oldLocation}: {result.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: "UpdateLocation");
            return Model_Dao_Result.Failure($"Error updating location {oldLocation}", ex);
        }
    }

    internal static async Task<Model_Dao_Result<DataTable>> GetAllLocations(MySqlConnection? connection = null, MySqlTransaction? transaction = null)
    {
        try
        {
            var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                Model_Application_Variables.ConnectionString,
                "md_locations_Get_All",
                null, // No parameters needed
                null, // No progress helper for this method
                connection: connection,
                transaction: transaction
            );

            if (result.IsSuccess && result.Data != null)
            {
                return Model_Dao_Result<DataTable>.Success(result.Data, $"Retrieved {result.Data.Rows.Count} locations");
            }
            else
            {
                return Model_Dao_Result<DataTable>.Failure($"Failed to retrieve locations: {result.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: "GetAllLocations");
            return Model_Dao_Result<DataTable>.Failure("Error retrieving locations", ex);
        }
    }

    internal static async Task<Model_Dao_Result<DataRow>> GetLocationByName(string location, MySqlConnection? connection = null, MySqlTransaction? transaction = null)
    {
        try
        {
            var allLocationsResult = await GetAllLocations(connection, transaction);
            if (!allLocationsResult.IsSuccess)
            {
                return Model_Dao_Result<DataRow>.Failure(allLocationsResult.ErrorMessage, allLocationsResult.Exception);
            }

            var table = allLocationsResult.Data!;
            var rows = table.Select($"Location = '{location.Replace("'", "''")}'");

            if (rows.Length > 0)
            {
                return Model_Dao_Result<DataRow>.Success(rows[0], $"Found location {location}");
            }

            return Model_Dao_Result<DataRow>.Failure($"Location {location} not found");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            return Model_Dao_Result<DataRow>.Failure($"Error retrieving location {location}", ex);
        }
    }

    #endregion

    #region Existence Check

    internal static async Task<Model_Dao_Result<bool>> LocationExists(string location, MySqlConnection? connection = null, MySqlTransaction? transaction = null)
    {
        try
        {
            var parameters = new Dictionary<string, object> { ["Location"] = location }; // p_ prefix added automatically

            var result = await Helper_Database_StoredProcedure.ExecuteScalarWithStatusAsync(
                Model_Application_Variables.ConnectionString,
                "md_locations_Exists_ByLocation",
                parameters,
                null, // No progress helper for this method
                connection: connection,
                transaction: transaction
            );

            if (result.IsSuccess && result.Data != null)
            {
                bool exists = Convert.ToInt32(result.Data) > 0;
                return Model_Dao_Result<bool>.Success(exists, exists ? $"Location {location} exists" : $"Location {location} does not exist");
            }
            else
            {
                return Model_Dao_Result<bool>.Failure($"Failed to check location {location}: {result.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: "LocationExists");
            return Model_Dao_Result<bool>.Failure($"Error checking location {location}", ex);
        }
    }

    #endregion
}

#endregion


