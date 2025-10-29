using System.Data;
using System.Security.Principal;
using MTM_WIP_Application_Winforms.Core;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Models;
using MySql.Data.MySqlClient;

namespace MTM_WIP_Application_Winforms.Data;

#region Dao_System

internal static class Dao_System
{
    #region User Roles / Access

    internal static async Task<Model_Dao_Result> SetUserAccessTypeAsync(string userName, string accessType,
        string? connectionString = null,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        try
        {
            // Convert string access type to INT for stored procedure
            // Note: Stored procedure expects INT (0 = standard user, 1 = admin/vits user)
            int accessTypeInt = accessType?.ToLower() switch
            {
                "admin" => 1,
                "vitsuser" => 1,
                "user" => 0,
                "standard" => 0,
                _ => 0 // Default to standard user
            };

            Dictionary<string, object> parameters = new()
            {
                ["User"] = userName,           // p_ prefix added automatically
                ["AccessType"] = accessTypeInt // INT not string
            };

            var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                connectionString ?? Model_Application_Variables.ConnectionString,
                "sys_user_access_SetType",     // Correct procedure name
                parameters,
                null, // No progress helper for this method
                connection: connection,
                transaction: transaction
            );

            if (result.IsSuccess)
            {
                if (Model_Application_Variables.User == userName)
                {
                    // Update flags based on role priority
                    Model_Application_Variables.UserTypeDeveloper = accessType == "Developer";
                    Model_Application_Variables.UserTypeAdmin = accessType == "Admin";
                    Model_Application_Variables.UserTypeReadOnly = accessType == "ReadOnly";
                    Model_Application_Variables.UserTypeNormal = accessType == "User" || accessType == "Normal";
                }
                return Model_Dao_Result.Success($"User access type set to {accessType} for {userName}");
            }
            else
            {
                return Model_Dao_Result.Failure($"Failed to set user access type for {userName}: {result.ErrorMessage}", result.Exception);
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            await HandleSystemDaoExceptionAsync(ex, "SetUserAccessType");
            return Model_Dao_Result.Failure($"Failed to set user access type for {userName}", ex);
        }
    }

    internal static string System_GetUserName()
    {
        var userIdWithDomain = Model_Application_Variables.EnteredUser == "Default User"
            ? WindowsIdentity.GetCurrent().Name
            : Model_Application_Variables.EnteredUser ??
              throw new InvalidOperationException("User identity could not be retrieved.");

        var posSlash = userIdWithDomain.IndexOf('\\');
        var user = (posSlash == -1 ? userIdWithDomain : userIdWithDomain[(posSlash + 1)..]).ToUpper();
        Model_Application_Variables.User = user;
        return user;
    }

    internal static async Task<Model_Dao_Result<List<Model_Shared_Users>>> System_UserAccessTypeAsync(MySqlConnection? connection = null, MySqlTransaction? transaction = null)
    {
        var user = Model_Application_Variables.User;
        try
        {
            Model_Application_Variables.UserTypeDeveloper = false;
            Model_Application_Variables.UserTypeAdmin = false;
            Model_Application_Variables.UserTypeReadOnly = false;
            Model_Application_Variables.UserTypeNormal = false;

            var result = new List<Model_Shared_Users>();

            // MIGRATED: Use Helper_Database_StoredProcedure for proper status handling
            // This handles the case where stored procedures may not exist yet or have parameter issues
            var dataResult = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                Model_Application_Variables.ConnectionString,
                "sys_GetUserAccessType",
                null, // No parameters needed
                progressHelper: null,
                connection: connection,
                transaction: transaction
            );

            if (!dataResult.IsSuccess)
            {
                // If stored procedure fails, create a default developer user
                LoggingUtility.Log($"sys_GetUserAccessType failed: {dataResult.ErrorMessage}. Creating default developer user.");

                // Set current user as developer by default when stored procedures have issues
                Model_Application_Variables.UserTypeDeveloper = true;
                Model_Application_Variables.UserTypeAdmin = false;
                Model_Application_Variables.UserTypeReadOnly = false;

                var defaultUser = new Model_Shared_Users
                {
                    Id = 1,
                    User = user
                };
                result.Add(defaultUser);

                return Model_Dao_Result<List<Model_Shared_Users>>.Success(result, $"Default admin access granted for user: {user}");
            }

            // Process successful result
            if (dataResult.Data != null && dataResult.Data.Rows.Count > 0)
            {
                foreach (DataRow row in dataResult.Data.Rows)
                {
                    var u = new Model_Shared_Users
                    {
                        Id = Convert.ToInt32(row[0]),
                        User = row[1]?.ToString() ?? ""
                    };

                    var roleName = row[2]?.ToString() ?? "";
                    if (u.User == user)
                    {
                        // Priority order: Developer > Admin > ReadOnly > User/Normal
                        if (roleName == "Developer")
                            Model_Application_Variables.UserTypeDeveloper = true;
                        else if (roleName == "Admin")
                            Model_Application_Variables.UserTypeAdmin = true;
                        else if (roleName == "ReadOnly")
                            Model_Application_Variables.UserTypeReadOnly = true;
                        else
                            // User or any other role is treated as Normal User
                            Model_Application_Variables.UserTypeNormal = true;
                    }

                    result.Add(u);
                }
            }
            else
            {
                // No users found, create default developer
                LoggingUtility.Log($"No users found in sys_GetUserAccessType. Creating default developer user: {user}");
                Model_Application_Variables.UserTypeDeveloper = true;
                Model_Application_Variables.UserTypeAdmin = false;
                Model_Application_Variables.UserTypeReadOnly = false;

                var defaultUser = new Model_Shared_Users
                {
                    Id = 1,
                    User = user
                };
                result.Add(defaultUser);
            }

            LoggingUtility.Log($"System_UserAccessType executed successfully for user: {user}");
            return Model_Dao_Result<List<Model_Shared_Users>>.Success(result, $"Retrieved {result.Count} users with access types");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);

            // FALLBACK: If everything fails, grant default developer access to prevent application lockup
            LoggingUtility.Log($"System_UserAccessType fallback: Granting default developer access to user: {user}");
            Model_Application_Variables.UserTypeDeveloper = true;
            Model_Application_Variables.UserTypeAdmin = false;
            Model_Application_Variables.UserTypeReadOnly = false;

            var fallbackUser = new Model_Shared_Users
            {
                Id = 1,
                User = user
            };

            await HandleSystemDaoExceptionAsync(ex, "System_UserAccessType");
            return Model_Dao_Result<List<Model_Shared_Users>>.Success(new List<Model_Shared_Users> { fallbackUser },
                $"Fallback developer access granted for user: {user}");
        }
    }

    internal static async Task<Model_Dao_Result<int>> GetUserIdByNameAsync(string userName,
        string? connectionString = null,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        try
        {
            Dictionary<string, object> parameters = new() { ["UserName"] = userName }; // p_ prefix added automatically

            var result = await Helper_Database_StoredProcedure.ExecuteScalarWithStatusAsync(
                connectionString ?? Model_Application_Variables.ConnectionString,
                "sys_user_GetIdByName",
                parameters,
                progressHelper: null,
                connection: connection,
                transaction: transaction
            );

            if (result.IsSuccess && result.Data != null && int.TryParse(result.Data.ToString(), out int userId))
            {
                return Model_Dao_Result<int>.Success(userId, result.StatusMessage ?? $"Found user ID {userId} for {userName}");
            }
            else
            {
                // Preserve the stored procedure's error/status message (e.g., "User name is required" for validation errors)
                // Note: For validation errors (status -2), message is in StatusMessage; for other errors it's in ErrorMessage
                string errorMsg = !string.IsNullOrWhiteSpace(result.ErrorMessage) ? result.ErrorMessage :
                                  !string.IsNullOrWhiteSpace(result.StatusMessage) ? result.StatusMessage :
                                  $"User '{userName}' not found";
                return Model_Dao_Result<int>.Failure(errorMsg);
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            await HandleSystemDaoExceptionAsync(ex, "GetUserIdByName");
            return Model_Dao_Result<int>.Failure($"Failed to get user ID for '{userName}'", ex);
        }
    }

    internal static async Task<Model_Dao_Result<int>> GetRoleIdByNameAsync(string roleName,
        string? connectionString = null,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        try
        {
            Dictionary<string, object> parameters = new() { ["RoleName"] = roleName }; // p_ prefix added automatically

            var result = await Helper_Database_StoredProcedure.ExecuteScalarWithStatusAsync(
                connectionString ?? Model_Application_Variables.ConnectionString,
                "sys_GetRoleIdByName",
                parameters,
                progressHelper: null,
                connection: connection,
                transaction: transaction
            );

            if (result.IsSuccess && result.Data != null && int.TryParse(result.Data.ToString(), out int roleId))
            {
                return Model_Dao_Result<int>.Success(roleId, $"Found role ID {roleId} for {roleName}");
            }
            else
            {
                return Model_Dao_Result<int>.Failure($"Role '{roleName}' not found");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            await HandleSystemDaoExceptionAsync(ex, "GetRoleIdByName");
            return Model_Dao_Result<int>.Failure($"Failed to get role ID for '{roleName}'", ex);
        }
    }

    #endregion

    #region Themes

    /// <summary>
    /// Retrieves all themes from the app_themes table.
    /// Alternative implementation that queries the table directly since app_themes_Get_All stored procedure doesn't exist.
    /// </summary>
    /// <param name="connection">Optional external MySqlConnection to use instead of creating a new one</param>
    /// <param name="transaction">Optional external MySqlTransaction to participate in</param>
    /// <returns>A Model_Dao_Result containing a DataTable with ThemeName and SettingsJson columns.</returns>
    internal static async Task<Model_Dao_Result<DataTable>> GetAllThemesAsync(
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        try
        {
            // Use stored procedure instead of direct SQL query
            var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                Model_Application_Variables.ConnectionString,
                "sys_theme_GetAll",
                null, // No parameters needed
                progressHelper: null,
                connection: connection,
                transaction: transaction
            );

            if (result.IsSuccess)
            {
                LoggingUtility.Log($"[Dao_System] Retrieved {result.Data?.Rows.Count ?? 0} themes using stored procedure");
                return Model_Dao_Result<DataTable>.Success(result.Data ?? new DataTable(), $"Successfully retrieved {result.Data?.Rows.Count ?? 0} themes");
            }
            else
            {
                LoggingUtility.Log($"[Dao_System] Failed to retrieve themes: {result.ErrorMessage}");
                return Model_Dao_Result<DataTable>.Failure($"Failed to retrieve themes: {result.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            await HandleSystemDaoExceptionAsync(ex, "GetAllThemes");
            return Model_Dao_Result<DataTable>.Failure("Failed to retrieve themes from database", ex);
        }
    }

    #endregion

    #region JSON Validation

    /// <summary>
    /// Validates JSON settings in app_themes and usr_ui_settings tables
    /// </summary>
    /// <returns>Model_Dao_Result with validation report</returns>
    internal static async Task<Model_Dao_Result<string>> ValidateJsonSettingsAsync()
    {
        try
        {
            LoggingUtility.Log("[Dao_System] Starting JSON validation for theme and user settings");
            var report = new System.Text.StringBuilder();
            int totalErrors = 0;
            int totalChecked = 0;

            // Validate theme settings
            var themesResult = await GetAllThemesAsync();
            if (themesResult.IsSuccess && themesResult.Data != null)
            {
                report.AppendLine("=== Theme Settings Validation ===");
                foreach (DataRow row in themesResult.Data.Rows)
                {
                    totalChecked++;
                    string themeName = row["ThemeName"]?.ToString() ?? "Unknown";
                    string settingsJson = row["SettingsJson"]?.ToString() ?? "";

                    if (string.IsNullOrWhiteSpace(settingsJson))
                    {
                        report.AppendLine($"⚠️  Theme '{themeName}': Empty JSON");
                        totalErrors++;
                        continue;
                    }

                    try
                    {
                        var options = new System.Text.Json.JsonSerializerOptions
                        {
                            AllowTrailingCommas = true,
                            ReadCommentHandling = System.Text.Json.JsonCommentHandling.Skip,
                            PropertyNameCaseInsensitive = false
                        };
                        options.Converters.Add(new JsonColorConverter());

                        var colors = System.Text.Json.JsonSerializer.Deserialize<Models.Model_Shared_UserUiColors>(settingsJson, options);

                        if (colors != null)
                        {
                            report.AppendLine($"✅ Theme '{themeName}': Valid JSON");
                        }
                        else
                        {
                            report.AppendLine($"❌ Theme '{themeName}': Deserialization returned null");
                            totalErrors++;
                        }
                    }
                    catch (System.Text.Json.JsonException jsonEx)
                    {
                        report.AppendLine($"❌ Theme '{themeName}': JSON Error - {jsonEx.Message}");
                        report.AppendLine($"   Preview: {(settingsJson.Length > 100 ? settingsJson.Substring(0, 100) + "..." : settingsJson)}");
                        totalErrors++;
                        LoggingUtility.LogApplicationError(jsonEx);
                    }
                }
                report.AppendLine();
            }
            else
            {
                report.AppendLine($"⚠️  Could not retrieve themes: {themesResult.ErrorMessage}");
                report.AppendLine();
            }

            // Validate user UI settings
            try
            {
                var userSettingsResult = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                    Model_Application_Variables.ConnectionString,
                    "usr_ui_settings_Get_All",
                    null
                );

                if (userSettingsResult.IsSuccess && userSettingsResult.Data != null)
                {
                    report.AppendLine("=== User Settings Validation ===");
                    foreach (DataRow row in userSettingsResult.Data.Rows)
                    {
                        totalChecked++;
                        string userId = row["UserId"]?.ToString() ?? "Unknown";
                        string settingsJson = row["SettingsJson"]?.ToString() ?? "";

                        if (string.IsNullOrWhiteSpace(settingsJson))
                        {
                            report.AppendLine($"ℹ️  User '{userId}': Empty JSON (using defaults)");
                            continue;
                        }

                        try
                        {
                        var options = new System.Text.Json.JsonSerializerOptions
                        {
                            AllowTrailingCommas = true,
                            ReadCommentHandling = System.Text.Json.JsonCommentHandling.Skip,
                            PropertyNameCaseInsensitive = true
                        };
                        options.Converters.Add(new JsonColorConverter());                            var colors = System.Text.Json.JsonSerializer.Deserialize<Models.Model_Shared_UserUiColors>(settingsJson, options);

                            if (colors != null)
                            {
                                report.AppendLine($"✅ User '{userId}': Valid JSON");
                            }
                            else
                            {
                                report.AppendLine($"❌ User '{userId}': Deserialization returned null");
                                totalErrors++;
                            }
                        }
                        catch (System.Text.Json.JsonException jsonEx)
                        {
                            report.AppendLine($"❌ User '{userId}': JSON Error - {jsonEx.Message}");
                            report.AppendLine($"   Preview: {(settingsJson.Length > 100 ? settingsJson.Substring(0, 100) + "..." : settingsJson)}");
                            totalErrors++;
                            LoggingUtility.LogApplicationError(jsonEx);
                        }
                    }
                    report.AppendLine();
                }
                else
                {
                    report.AppendLine($"⚠️  Could not retrieve user settings: {userSettingsResult.ErrorMessage}");
                    report.AppendLine();
                }
            }
            catch (Exception ex)
            {
                report.AppendLine($"⚠️  Error checking user settings: {ex.Message}");
                LoggingUtility.LogApplicationError(ex);
            }

            // Summary
            report.AppendLine("=== Validation Summary ===");
            report.AppendLine($"Total items checked: {totalChecked}");
            report.AppendLine($"Total errors found: {totalErrors}");
            report.AppendLine($"Success rate: {(totalChecked > 0 ? ((totalChecked - totalErrors) * 100.0 / totalChecked).ToString("F1") : "N/A")}%");

            string finalReport = report.ToString();
            LoggingUtility.Log($"[Dao_System] JSON validation complete:\n{finalReport}");

            if (totalErrors > 0)
            {
                return Model_Dao_Result<string>.Failure($"Found {totalErrors} JSON validation errors. Report:\n{finalReport}", null);
            }
            else
            {
                return Model_Dao_Result<string>.Success(finalReport, $"All {totalChecked} JSON settings are valid");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            return Model_Dao_Result<string>.Failure($"JSON validation failed: {ex.Message}", ex);
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
    /// Check database connectivity with automatic fallback to localhost.
    /// Tries the configured server first, then falls back to localhost if connection fails.
    /// Used for startup validation per FR-014.
    /// </summary>
    /// <returns>DaoResult indicating success or failure with actionable error message</returns>
    internal static async Task<DaoResult> CheckConnectivityWithFallbackAsync()
    {
        try
        {
            LoggingUtility.Log("[Dao_System] Checking database connectivity with fallback");

            // First attempt: Try configured server
            var primaryResult = await CheckConnectivityAsync();
            
            if (primaryResult.IsSuccess)
            {
                return primaryResult;
            }

            // Check if the failure was a connection timeout/unable to connect
            bool isConnectionFailure = primaryResult.Exception is MySqlException mysqlEx &&
                                      (mysqlEx.Message.Contains("Unable to connect") ||
                                       mysqlEx.Message.Contains("timeout") ||
                                       mysqlEx.Number == 0 ||
                                       mysqlEx.Number == 1042);

            if (!isConnectionFailure)
            {
                // Not a connection failure - return the error (e.g., auth error, database doesn't exist)
                return primaryResult;
            }

            // Connection failure detected - try localhost fallback
            LoggingUtility.Log($"[Dao_System] Primary server connection failed: {Model_Users.WipServerAddress}. Attempting localhost fallback...");

            // Temporarily switch to localhost
            string originalServer = Model_Users.WipServerAddress;
            Model_Users.WipServerAddress = "localhost";
            
            try
            {
                var localhostResult = await CheckConnectivityAsync();
                
                if (localhostResult.IsSuccess)
                {
                    LoggingUtility.Log("[Dao_System] Successfully connected to localhost");
                    return DaoResult.Success($"Connected to MySQL on localhost (primary server {originalServer} was unavailable)");
                }
                else
                {
                    // Localhost also failed - restore original and return comprehensive error
                    Model_Users.WipServerAddress = originalServer;
                    
                    string errorMessage = $"Unable to connect to MySQL server.\n\n" +
                                        $"Attempted connections:\n" +
                                        $"• Primary server: {originalServer} - Failed\n" +
                                        $"• Localhost fallback: Failed\n\n" +
                                        $"Primary error: {primaryResult.StatusMessage}\n" +
                                        $"Localhost error: {localhostResult.StatusMessage}\n\n" +
                                        $"Please ensure MySQL/MAMP is running and accessible.";
                    
                    LoggingUtility.Log($"[Dao_System] Both primary and localhost connections failed");
                    return DaoResult.Failure(errorMessage, primaryResult.Exception);
                }
            }
            catch
            {
                // Restore original server on any error
                Model_Users.WipServerAddress = originalServer;
                throw;
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            return DaoResult.Failure($"Database connectivity check failed: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Validates database connectivity by attempting a simple query.
    /// Used for startup validation per FR-014.
    /// </summary>
    /// <returns>Model_Dao_Result indicating success or failure with actionable error message</returns>
    internal static async Task<Model_Dao_Result> CheckConnectivityAsync(
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        try
        {
            LoggingUtility.Log("[Dao_System] Checking database connectivity");

            // Attempt a simple SELECT query to validate connectivity
            var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                Model_Application_Variables.ConnectionString,
                "sys_theme_GetAll",  // Use existing stored procedure for validation
                null
            );

            if (result.IsSuccess)
            {
                LoggingUtility.Log("[Dao_System] Database connectivity check passed");
                return Model_Dao_Result.Success("Database connection successful");
            }
            else
            {
                // Helper already provides user-friendly error messages
                string errorMessage = result.StatusMessage ?? result.ErrorMessage ?? "Unable to connect to database";
                LoggingUtility.Log($"[Dao_System] Database connectivity check failed: {errorMessage}");
                return Model_Dao_Result.Failure(
                    errorMessage,
                    exception: result.Exception ?? new Exception(errorMessage));
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
            return Model_Dao_Result.Failure(userMessage, ex);
        }
        catch (Exception ex)
        {
            string userMessage = $"Database connectivity check failed: {ex.Message}";
            LoggingUtility.LogApplicationError(ex);
            return Model_Dao_Result.Failure(userMessage, ex);
        }
    }

    #endregion
}

#endregion
