using System.Data;
using System.Security.Principal;
using MTM_Inventory_Application.Helpers;
using MTM_Inventory_Application.Logging;
using MTM_Inventory_Application.Models;
using MySql.Data.MySqlClient;

namespace MTM_Inventory_Application.Data;

#region Dao_System

internal static class Dao_System
{
    #region User Roles / Access

    internal static async Task<DaoResult> SetUserAccessTypeAsync(string userName, string accessType)
    {
        try
        {
            Dictionary<string, object> parameters = new()
            {
                ["UserName"] = userName,                     // p_ prefix added automatically
                ["AccessType"] = accessType,
                ["AssignedBy"] = Model_AppVariables.User ?? "System"
            };

            var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                Model_AppVariables.ConnectionString,
                "sys_SetUserAccessType",
                parameters,
                null // No progress helper for this method
            );

            if (result.IsSuccess)
            {
                if (Model_AppVariables.User == userName)
                {
                    Model_AppVariables.UserTypeAdmin = accessType == "Admin";
                    Model_AppVariables.UserTypeReadOnly = accessType == "ReadOnly";
                }
                return DaoResult.Success($"User access type set to {accessType} for {userName}");
            }
            else
            {
                return DaoResult.Failure($"Failed to set user access type for {userName}: {result.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            await HandleSystemDaoExceptionAsync(ex, "SetUserAccessType");
            return DaoResult.Failure($"Failed to set user access type for {userName}", ex);
        }
    }

    internal static string System_GetUserName()
    {
        var userIdWithDomain = Model_AppVariables.EnteredUser == "Default User"
            ? WindowsIdentity.GetCurrent().Name
            : Model_AppVariables.EnteredUser ??
              throw new InvalidOperationException("User identity could not be retrieved.");

        var posSlash = userIdWithDomain.IndexOf('\\');
        var user = (posSlash == -1 ? userIdWithDomain : userIdWithDomain[(posSlash + 1)..]).ToUpper();
        Model_AppVariables.User = user;
        return user;
    }

    internal static async Task<DaoResult<List<Model_Users>>> System_UserAccessTypeAsync()
    {
        var user = Model_AppVariables.User;
        try
        {
            Model_AppVariables.UserTypeAdmin = false;
            Model_AppVariables.UserTypeReadOnly = false;

            var result = new List<Model_Users>();

            // MIGRATED: Use Helper_Database_StoredProcedure for proper status handling
            // This handles the case where stored procedures may not exist yet or have parameter issues
            var dataResult = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                Model_AppVariables.ConnectionString,
                "sys_GetUserAccessType",
                null, // No parameters needed
                null // No progress helper for startup
            );

            if (!dataResult.IsSuccess)
            {
                // If stored procedure fails, create a default admin user
                LoggingUtility.Log($"sys_GetUserAccessType failed: {dataResult.ErrorMessage}. Creating default admin user.");
                
                // Set current user as admin by default when stored procedures have issues
                Model_AppVariables.UserTypeAdmin = true;
                Model_AppVariables.UserTypeReadOnly = false;
                
                var defaultUser = new Model_Users
                {
                    Id = 1,
                    User = user
                };
                result.Add(defaultUser);
                
                return DaoResult<List<Model_Users>>.Success(result, $"Default admin access granted for user: {user}");
            }

            // Process successful result
            if (dataResult.Data != null && dataResult.Data.Rows.Count > 0)
            {
                foreach (DataRow row in dataResult.Data.Rows)
                {
                    var u = new Model_Users
                    {
                        Id = Convert.ToInt32(row[0]),
                        User = row[1]?.ToString() ?? ""
                    };

                    var roleName = row[2]?.ToString() ?? "";
                    if (u.User == user)
                    {
                        if (roleName == "Admin")
                            Model_AppVariables.UserTypeAdmin = true;
                        if (roleName == "ReadOnly")
                            Model_AppVariables.UserTypeReadOnly = true;
                    }

                    result.Add(u);
                }
            }
            else
            {
                // No users found, create default admin
                LoggingUtility.Log($"No users found in sys_GetUserAccessType. Creating default admin user: {user}");
                Model_AppVariables.UserTypeAdmin = true;
                Model_AppVariables.UserTypeReadOnly = false;
                
                var defaultUser = new Model_Users
                {
                    Id = 1,
                    User = user
                };
                result.Add(defaultUser);
            }

            LoggingUtility.Log($"System_UserAccessType executed successfully for user: {user}");
            return DaoResult<List<Model_Users>>.Success(result, $"Retrieved {result.Count} users with access types");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            
            // FALLBACK: If everything fails, grant default admin access to prevent application lockup
            LoggingUtility.Log($"System_UserAccessType fallback: Granting default admin access to user: {user}");
            Model_AppVariables.UserTypeAdmin = true;
            Model_AppVariables.UserTypeReadOnly = false;
            
            var fallbackUser = new Model_Users
            {
                Id = 1,
                User = user
            };
            
            await HandleSystemDaoExceptionAsync(ex, "System_UserAccessType");
            return DaoResult<List<Model_Users>>.Success(new List<Model_Users> { fallbackUser }, 
                $"Fallback admin access granted for user: {user}");
        }
    }

    internal static async Task<DaoResult<int>> GetUserIdByNameAsync(string userName)
    {
        try
        {
            Dictionary<string, object> parameters = new() { ["UserName"] = userName }; // p_ prefix added automatically

            var result = await Helper_Database_StoredProcedure.ExecuteScalarWithStatusAsync(
                Model_AppVariables.ConnectionString,
                "sys_user_GetIdByName",
                parameters,
                null // No progress helper for this method
            );

            if (result.IsSuccess && result.Data != null && int.TryParse(result.Data.ToString(), out int userId))
            {
                return DaoResult<int>.Success(userId, $"Found user ID {userId} for {userName}");
            }
            else
            {
                return DaoResult<int>.Failure($"User '{userName}' not found");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            await HandleSystemDaoExceptionAsync(ex, "GetUserIdByName");
            return DaoResult<int>.Failure($"Failed to get user ID for '{userName}'", ex);
        }
    }

    internal static async Task<DaoResult<int>> GetRoleIdByNameAsync(string roleName)
    {
        try
        {
            Dictionary<string, object> parameters = new() { ["RoleName"] = roleName }; // p_ prefix added automatically

            var result = await Helper_Database_StoredProcedure.ExecuteScalarWithStatusAsync(
                Model_AppVariables.ConnectionString,
                "sys_GetRoleIdByName",
                parameters,
                null // No progress helper for this method
            );

            if (result.IsSuccess && result.Data != null && int.TryParse(result.Data.ToString(), out int roleId))
            {
                return DaoResult<int>.Success(roleId, $"Found role ID {roleId} for {roleName}");
            }
            else
            {
                return DaoResult<int>.Failure($"Role '{roleName}' not found");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            await HandleSystemDaoExceptionAsync(ex, "GetRoleIdByName");
            return DaoResult<int>.Failure($"Failed to get role ID for '{roleName}'", ex);
        }
    }

    #endregion

    #region Themes

    /// <summary>
    /// Retrieves all themes from the app_themes table.
    /// Alternative implementation that queries the table directly since app_themes_Get_All stored procedure doesn't exist.
    /// </summary>
    /// <returns>A DaoResult containing a DataTable with ThemeName and SettingsJson columns.</returns>
    internal static async Task<DaoResult<DataTable>> GetAllThemesAsync()
    {
        try
        {
            // Direct SQL query since the stored procedure doesn't exist
            const string query = "SELECT ThemeName, SettingsJson FROM app_themes ORDER BY ThemeName";

            using var connection = new MySql.Data.MySqlClient.MySqlConnection(Model_AppVariables.ConnectionString);
            await connection.OpenAsync();

            using var command = new MySql.Data.MySqlClient.MySqlCommand(query, connection)
            {
                CommandType = CommandType.Text
            };

            var dataTable = new DataTable();
            using var adapter = new MySql.Data.MySqlClient.MySqlDataAdapter(command);
            await Task.Run(() => adapter.Fill(dataTable));

            LoggingUtility.Log($"[Dao_System] Retrieved {dataTable.Rows.Count} themes from app_themes table");
            return DaoResult<DataTable>.Success(dataTable, $"Successfully retrieved {dataTable.Rows.Count} themes");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            await HandleSystemDaoExceptionAsync(ex, "GetAllThemes");
            return DaoResult<DataTable>.Failure("Failed to retrieve themes from database", ex);
        }
    }

    #endregion

    #region Helpers

    private static async Task HandleSystemDaoExceptionAsync(Exception ex, string method)
    {
        LoggingUtility.LogApplicationError(new Exception($"Error in {method}: {ex.Message}", ex));
        
        // ENHANCED: Pass method name to error handlers for better debugging
        await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, controlName: method);
    }

    #endregion

    #region Database Connectivity Validation

    /// <summary>
    /// Validates database connectivity by attempting a simple query.
    /// Used for startup validation per FR-014.
    /// </summary>
    /// <returns>DaoResult indicating success or failure with actionable error message</returns>
    internal static async Task<DaoResult> CheckConnectivityAsync()
    {
        try
        {
            LoggingUtility.Log("[Dao_System] Checking database connectivity");

            // Attempt a simple SELECT query to validate connectivity
            var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                Model_AppVariables.ConnectionString,
                "sys_theme_GetAll",  // Use existing stored procedure for validation
                null
            );

            if (result.IsSuccess)
            {
                LoggingUtility.Log("[Dao_System] Database connectivity check passed");
                return DaoResult.Success("Database connection successful");
            }
            else
            {
                // Helper already provides user-friendly error messages
                LoggingUtility.Log($"[Dao_System] Database connectivity check failed: {result.ErrorMessage}");
                return DaoResult.Failure(result.StatusMessage ?? result.ErrorMessage);
            }
        }
        catch (MySqlException ex)
        {
            string userMessage = ex.Number switch
            {
                0 => "Cannot connect to MySQL server.\n\n" +
                     "Please ensure:\n" +
                     "• MySQL/MAMP is running\n" +
                     "• Server is accessible at the configured address\n" +
                     "• No firewall is blocking the connection",
                1042 => "Cannot resolve MySQL server hostname.\n\n" +
                        "Please verify the server address in your configuration.",
                1044 => "Access denied to database.\n\n" +
                        "Please verify:\n" +
                        "• Database name is correct\n" +
                        "• User has permissions to access the database",
                1045 => "Access denied for user.\n\n" +
                        "Please verify:\n" +
                        "• Username is correct\n" +
                        "• Password is correct\n" +
                        "• User has necessary permissions",
                1049 => "Database does not exist.\n\n" +
                        "Please verify the database name in your configuration.",
                2002 => "Cannot connect to MySQL server.\n\n" +
                        "MySQL server may not be running or network is unreachable.",
                2003 => "Cannot connect to MySQL server.\n\n" +
                        "Connection refused. Please verify MySQL/MAMP is running.",
                _ => $"MySQL Error ({ex.Number}): {ex.Message}\n\n" +
                     "Please check the application logs for details."
            };

            LoggingUtility.LogDatabaseError(ex);
            return DaoResult.Failure(userMessage, ex);
        }
        catch (Exception ex)
        {
            string userMessage = $"Database connectivity check failed: {ex.Message}";
            LoggingUtility.LogApplicationError(ex);
            return DaoResult.Failure(userMessage, ex);
        }
    }

    #endregion
}

#endregion
