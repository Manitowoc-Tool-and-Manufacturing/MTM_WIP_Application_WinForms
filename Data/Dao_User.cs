using System.Data;
using System.Diagnostics;
using System.Text.Json;
using MTM_Inventory_Application.Helpers;
using MTM_Inventory_Application.Logging;
using MTM_Inventory_Application.Models;
using MTM_Inventory_Application.Services;
using MySql.Data.MySqlClient;

namespace MTM_Inventory_Application.Data;

#region Dao_User

/// <summary>
/// Data Access Object for user management and user settings operations.
/// Implements DaoResult pattern with async/await and Service_DebugTracer integration.
/// </summary>
internal static class Dao_User
{
    #region User Settings Getters/Setters

    internal static async Task<string> GetLastShownVersionAsync(string user)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["user"] = user }, controlName: "Dao_User");
        
        var result = await GetSettingsJsonAsync("LastShownVersion", user);
        
        Service_DebugTracer.TraceMethodExit(result, controlName: "Dao_User");
        return result;
    }

    internal static async Task SetLastShownVersionAsync(string user, string value)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["user"] = user, ["value"] = value }, controlName: "Dao_User");
        
        await SetUserSettingAsync("LastShownVersion", user, value);
        
        Service_DebugTracer.TraceMethodExit(controlName: "Dao_User");
    }

    internal static async Task<string> GetHideChangeLogAsync(string user)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["user"] = user }, controlName: "Dao_User");
        
        var result = await GetSettingsJsonAsync("HideChangeLog", user);
        
        Service_DebugTracer.TraceMethodExit(result, controlName: "Dao_User");
        return result;
    }

    internal static async Task SetHideChangeLogAsync(string user, string value)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["user"] = user, ["value"] = value }, controlName: "Dao_User");
        
        await SetUserSettingAsync("HideChangeLog", user, value);
        
        Service_DebugTracer.TraceMethodExit(controlName: "Dao_User");
    }

    internal static async Task<string?> GetThemeNameAsync(string user)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["user"] = user }, controlName: "Dao_User");
        
        var result = await GetSettingsJsonAsync("Theme_Name", user);
        
        Service_DebugTracer.TraceMethodExit(result, controlName: "Dao_User");
        return result;
    }

    internal static async Task<int?> GetThemeFontSizeAsync(string user)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["user"] = user }, controlName: "Dao_User");
        
        try
        {
            string str = await GetSettingsJsonAsync("Theme_FontSize", user);
            int? result = int.TryParse(str, out int val) ? val : null;
            
            Service_DebugTracer.TraceMethodExit(result, controlName: "Dao_User");
            return result;
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            LoggingUtility.Log($"GetThemeFontSizeAsync failed with exception: {ex.Message}");
            
            Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_User");
            return null;
        }
    }

    internal static async Task SetThemeFontSizeAsync(string user, int value)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["user"] = user, ["value"] = value }, controlName: "Dao_User");
        
        await SetUserSettingAsync("Theme_FontSize", user, value.ToString());
        
        Service_DebugTracer.TraceMethodExit(controlName: "Dao_User");
    }

    internal static async Task<string> GetVisualUserNameAsync(string user)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["user"] = user }, controlName: "Dao_User");
        
        string value = await GetSettingsJsonAsync("VisualUserName", user);
        Model_Users.VisualUserName = value;
        
        Service_DebugTracer.TraceMethodExit(Model_Users.VisualUserName, controlName: "Dao_User");
        return Model_Users.VisualUserName;
    }

    internal static async Task SetVisualUserNameAsync(string user, string value)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["user"] = user, ["value"] = value }, controlName: "Dao_User");
        
        await SetUserSettingAsync("VisualUserName", user, value);
        
        Service_DebugTracer.TraceMethodExit(controlName: "Dao_User");
    }

    internal static async Task<string> GetVisualPasswordAsync(string user)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["user"] = user }, controlName: "Dao_User");
        
        string value = await GetSettingsJsonAsync("VisualPassword", user);
        Model_Users.VisualPassword = value;
        
        Service_DebugTracer.TraceMethodExit(Model_Users.VisualPassword, controlName: "Dao_User");
        return Model_Users.VisualPassword;
    }

    internal static async Task SetVisualPasswordAsync(string user, string value)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["user"] = user, ["value"] = value }, controlName: "Dao_User");
        
        await SetUserSettingAsync("VisualPassword", user, value);
        
        Service_DebugTracer.TraceMethodExit(controlName: "Dao_User");
    }

    internal static async Task<string> GetWipServerAddressAsync(string user)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["user"] = user }, controlName: "Dao_User");
        
        string value = await GetSettingsJsonAsync("WipServerAddress", user);
        Model_Users.WipServerAddress = value;
        
        Service_DebugTracer.TraceMethodExit(Model_Users.WipServerAddress, controlName: "Dao_User");
        return Model_Users.WipServerAddress;
    }

    internal static async Task SetWipServerAddressAsync(string user, string value)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["user"] = user, ["value"] = value }, controlName: "Dao_User");
        
        await SetUserSettingAsync("WipServerAddress", user, value);
        
        Service_DebugTracer.TraceMethodExit(controlName: "Dao_User");
    }

    #region Get/Set Database

    internal static async Task<string> GetDatabaseAsync(string user)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["user"] = user }, controlName: "Dao_User");
        
        string value = await GetSettingsJsonAsync("WIPDatabase", user);
        Model_Users.Database = value;
        
        Service_DebugTracer.TraceMethodExit(Model_Users.Database, controlName: "Dao_User");
        return Model_Users.Database;
    }

    internal static async Task SetDatabaseAsync(string user, string value)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["user"] = user, ["value"] = value }, controlName: "Dao_User");
        
        Model_Users.Database = value;
        await SetUserSettingAsync("WIPDatabase", user, value);
        
        Service_DebugTracer.TraceMethodExit(controlName: "Dao_User");
    }

    #endregion

    #region Get/Set WipServerPort

    internal static async Task<string> GetWipServerPortAsync(string user)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["user"] = user }, controlName: "Dao_User");
        
        string value = await GetSettingsJsonAsync("WipServerPort", user);
        Model_Users.WipServerPort = value;
        
        Service_DebugTracer.TraceMethodExit(Model_Users.WipServerPort, controlName: "Dao_User");
        return Model_Users.WipServerPort;
    }

    internal static async Task SetWipServerPortAsync(string user, string value)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["user"] = user, ["value"] = value }, controlName: "Dao_User");
        
        Model_Users.WipServerPort = value;
        await SetUserSettingAsync("WipServerPort", user, value);
        
        Service_DebugTracer.TraceMethodExit(controlName: "Dao_User");
    }

    #endregion

    internal static async Task<string?> GetUserFullNameAsync(string user)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["user"] = user }, controlName: "Dao_User");
        
        try
        {
            var dataResult = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                Model_AppVariables.ConnectionString,
                "usr_users_Get_ByUser",
                new Dictionary<string, object> { ["User"] = user }
            );

            if (dataResult.IsSuccess && dataResult.Data != null && dataResult.Data.Rows.Count > 0)
            {
                DataRow row = dataResult.Data.Rows[0];
                object? fullNameObj = row["Full Name"];
                string? fullName = fullNameObj == DBNull.Value ? null : fullNameObj?.ToString();
                
                Model_Users.FullName = fullName ?? string.Empty;
                
                Service_DebugTracer.TraceMethodExit(fullName, controlName: "Dao_User");
                return fullName;
            }
            
            Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_User");
            return null;
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            LoggingUtility.Log($"GetUserFullNameAsync failed with exception: {ex.Message}");
            
            Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_User");
            return null;
        }
    }

    public static async Task<string> GetSettingsJsonAsync(string field, string user)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["field"] = field, ["user"] = user }, controlName: "Dao_User");
        
        try
        {
            var dataResult = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                Model_AppVariables.ConnectionString,
                "usr_ui_settings_Get",
                new Dictionary<string, object> { ["UserId"] = user }
            );

            if (dataResult.IsSuccess && dataResult.Data != null && dataResult.Data.Rows.Count > 0)
            {
                object? result = dataResult.Data.Rows[0]["SettingsJson"];
                if (result != null && result != DBNull.Value)
                {
                    string? json = result.ToString();
                    if (!string.IsNullOrWhiteSpace(json))
                    {
                        try
                        {
                            using JsonDocument doc = JsonDocument.Parse(json);
                            if (doc.RootElement.TryGetProperty(field, out JsonElement fieldElement))
                            {
                                string? value = fieldElement.ValueKind switch
                                {
                                    JsonValueKind.Number => fieldElement.ToString(),
                                    JsonValueKind.String => fieldElement.GetString(),
                                    JsonValueKind.True => "true",
                                    JsonValueKind.False => "false",
                                    _ => fieldElement.ToString()
                                };

                                Service_DebugTracer.TraceMethodExit(value, controlName: "Dao_User");
                                return value ?? string.Empty;
                            }
                        }
                        catch (JsonException ex)
                        {
                            Debug.WriteLine($"[Dao_User] JSON parsing error in GetSettingsJsonAsync: {ex.Message}");
                        }
                    }
                }
            }

            Service_DebugTracer.TraceMethodExit(string.Empty, controlName: "Dao_User");
            return string.Empty;
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            
            Service_DebugTracer.TraceMethodExit(string.Empty, controlName: "Dao_User");
            return string.Empty;
        }
    }

    public static async Task SetSettingsJsonAsync(string userId, string themeJson)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["userId"] = userId, ["themeJson"] = themeJson }, controlName: "Dao_User");
        
        try
        {
            Dictionary<string, object> parameters = new()
            {
                ["UserId"] = userId,
                ["ThemeJson"] = themeJson
            };

            var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                Model_AppVariables.ConnectionString,
                "usr_ui_settings_SetThemeJson",
                parameters
            );

            if (!result.IsSuccess)
            {
                throw new Exception(result.ErrorMessage ?? "Failed to set theme JSON");
            }
            
            Service_DebugTracer.TraceMethodExit(controlName: "Dao_User");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            LoggingUtility.Log($"SetSettingsJsonAsync failed with exception: {ex.Message}");
            
            Service_DebugTracer.TraceMethodExit(controlName: "Dao_User");
        }
    }

    public static async Task SetGridViewSettingsJsonAsync(string userId, string dgvName, string settingsJson)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["userId"] = userId, ["dgvName"] = dgvName, ["settingsJson"] = settingsJson }, controlName: "Dao_User");
        
        try
        {
            Dictionary<string, object> parameters = new()
            {
                ["UserId"] = userId,
                ["DgvName"] = dgvName,
                ["SettingJson"] = settingsJson
            };

            var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                Model_AppVariables.ConnectionString,
                "usr_ui_settings_SetJsonSetting",
                parameters
            );

            if (!result.IsSuccess)
            {
                throw new Exception(result.ErrorMessage ?? "Failed to set grid view settings JSON");
            }
            
            Service_DebugTracer.TraceMethodExit(controlName: "Dao_User");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            LoggingUtility.Log($"SetGridViewSettingsJsonAsync failed with exception: {ex.Message}");
            
            Service_DebugTracer.TraceMethodExit(controlName: "Dao_User");
        }
    }

    public static async Task<string> GetGridViewSettingsJsonAsync(string userId)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["userId"] = userId }, controlName: "Dao_User");
        
        try
        {
            var dataResult = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                Model_AppVariables.ConnectionString,
                "usr_ui_settings_GetJsonSetting",
                new Dictionary<string, object> { ["UserId"] = userId }
            );
            
            if (dataResult.IsSuccess && dataResult.Data != null && dataResult.Data.Rows.Count > 0)
            {
                object? result = dataResult.Data.Rows[0][0];
                string? json = result?.ToString();
                
                Service_DebugTracer.TraceMethodExit(json, controlName: "Dao_User");
                return json ?? "";
            }
            
            Service_DebugTracer.TraceMethodExit("", controlName: "Dao_User");
            return "";
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            LoggingUtility.Log($"GetGridViewSettingsJsonAsync failed with exception: {ex.Message}");
            
            Service_DebugTracer.TraceMethodExit("", controlName: "Dao_User");
            return "";
        }
    }

    private static async Task SetUserSettingAsync(string field, string user, string value)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["field"] = field, ["user"] = user, ["value"] = value }, controlName: "Dao_User");
        
        try
        {
            var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                Model_AppVariables.ConnectionString,
                "usr_users_SetUserSetting_ByUserAndField",
                new Dictionary<string, object> 
                { 
                    ["User"] = user,
                    ["Field"] = field,
                    ["Value"] = value
                }
            );

            if (!result.IsSuccess)
            {
                LoggingUtility.Log($"SetUserSettingAsync failed: {result.ErrorMessage}");
            }
            
            Service_DebugTracer.TraceMethodExit(controlName: "Dao_User");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            LoggingUtility.Log($"SetUserSettingAsync failed with exception: {ex.Message}");
            
            Service_DebugTracer.TraceMethodExit(controlName: "Dao_User");
        }
    }

    #endregion

    #region Add / Update / Delete

    /// <summary>
    /// Deletes all UI settings for the specified user.
    /// </summary>
    /// <param name="userName">The username whose settings to delete.</param>
    /// <returns>A DaoResult indicating success or failure.</returns>
    internal static async Task<DaoResult> DeleteUserSettingsAsync(string userName)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["userName"] = userName }, controlName: "Dao_User");
        
        try
        {
            var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                Model_AppVariables.ConnectionString,
                "usr_ui_settings_Delete_ByUserId",
                new Dictionary<string, object> { ["UserId"] = userName }
            );

            Service_DebugTracer.TraceMethodExit(result, controlName: "Dao_User");
            
            if (result.IsSuccess)
            {
                return DaoResult.Success($"User settings for {userName} deleted successfully");
            }
            else
            {
                return DaoResult.Failure($"Failed to delete user settings: {result.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            
            Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_User");
            return DaoResult.Failure($"Error deleting user settings for {userName}", ex);
        }
    }

    /// <summary>
    /// Creates a new user with the specified details and default settings.
    /// </summary>
    internal static async Task<DaoResult> CreateUserAsync(
        string user, string fullName, string shift, bool vitsUser, string pin,
        string lastShownVersion, string hideChangeLog, string themeName, int themeFontSize,
        string visualUserName, string visualPassword, string wipServerAddress, string database,
        string wipServerPort)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> 
        { 
            ["user"] = user, 
            ["fullName"] = fullName, 
            ["shift"] = shift,
            ["vitsUser"] = vitsUser 
        }, controlName: "Dao_User");
        
        try
        {
            var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                Model_AppVariables.ConnectionString,
                "usr_users_Add_User",
                new Dictionary<string, object>
                {
                    ["User"] = user,
                    ["FullName"] = fullName,
                    ["Shift"] = shift,
                    ["VitsUser"] = vitsUser,
                    ["Pin"] = pin,
                    ["LastShownVersion"] = lastShownVersion,
                    ["HideChangeLog"] = hideChangeLog,
                    ["Theme_Name"] = themeName,
                    ["Theme_FontSize"] = themeFontSize,
                    ["VisualUserName"] = visualUserName,
                    ["VisualPassword"] = visualPassword,
                    ["WipServerAddress"] = wipServerAddress,
                    ["WIPDatabase"] = database,
                    ["WipServerPort"] = wipServerPort
                }
            );

            Service_DebugTracer.TraceMethodExit(result, controlName: "Dao_User");
            
            if (result.IsSuccess)
            {
                return DaoResult.Success($"User {user} created successfully");
            }
            else
            {
                return DaoResult.Failure($"Failed to create user: {result.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            
            Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_User");
            return DaoResult.Failure($"Error creating user {user}", ex);
        }
    }

    /// <summary>
    /// Updates an existing user's information.
    /// </summary>
    internal static async Task<DaoResult> UpdateUserAsync(
        string user,
        string fullName,
        string shift,
        string pin,
        string visualUserName,
        string visualPassword)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> 
        { 
            ["user"] = user, 
            ["fullName"] = fullName, 
            ["shift"] = shift
        }, controlName: "Dao_User");
        
        try
        {
            var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                Model_AppVariables.ConnectionString,
                "usr_users_Update_User",
                new Dictionary<string, object>
                {
                    ["User"] = user,
                    ["FullName"] = fullName,
                    ["Shift"] = shift,
                    ["Pin"] = pin,
                    ["VisualUserName"] = visualUserName,
                    ["VisualPassword"] = visualPassword
                }
            );

            Service_DebugTracer.TraceMethodExit(result, controlName: "Dao_User");
            
            if (result.IsSuccess)
            {
                return DaoResult.Success($"User {user} updated successfully");
            }
            else
            {
                return DaoResult.Failure($"Failed to update user: {result.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            
            Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_User");
            return DaoResult.Failure($"Error updating user {user}", ex);
        }
    }

    /// <summary>
    /// Deletes the specified user from the system.
    /// </summary>
    internal static async Task<DaoResult> DeleteUserAsync(string user)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["user"] = user }, controlName: "Dao_User");
        
        try
        {
            var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                Model_AppVariables.ConnectionString,
                "usr_users_Delete_User",
                new Dictionary<string, object> { ["User"] = user }
            );

            Service_DebugTracer.TraceMethodExit(result, controlName: "Dao_User");
            
            if (result.IsSuccess)
            {
                return DaoResult.Success($"User {user} deleted successfully");
            }
            else
            {
                return DaoResult.Failure($"Failed to delete user: {result.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            
            Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_User");
            return DaoResult.Failure($"Error deleting user {user}", ex);
        }
    }

    #endregion

    #region Queries

    /// <summary>
    /// Retrieves all users from the system.
    /// </summary>
    /// <returns>A DaoResult containing a DataTable with all users.</returns>
    internal static async Task<DaoResult<DataTable>> GetAllUsersAsync()
    {
        Service_DebugTracer.TraceMethodEntry(controlName: "Dao_User");
        
        try
        {
            var dataResult = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                Model_AppVariables.ConnectionString,
                "usr_users_Get_All",
                null
            );

            Service_DebugTracer.TraceMethodExit(dataResult, controlName: "Dao_User");
            
            if (dataResult.IsSuccess && dataResult.Data != null)
            {
                return DaoResult<DataTable>.Success(dataResult.Data, $"Retrieved {dataResult.Data.Rows.Count} users");
            }
            else
            {
                return DaoResult<DataTable>.Failure($"Failed to retrieve users: {dataResult.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            
            Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_User");
            return DaoResult<DataTable>.Failure("Error retrieving users", ex);
        }
    }

    /// <summary>
    /// Retrieves a specific user by username.
    /// </summary>
    internal static async Task<DaoResult<DataRow>> GetUserByUsernameAsync(string user)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["user"] = user }, controlName: "Dao_User");
        
        try
        {
            var dataResult = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                Model_AppVariables.ConnectionString,
                "usr_users_Get_ByUser",
                new Dictionary<string, object> { ["User"] = user }
            );

            if (dataResult.IsSuccess && dataResult.Data != null && dataResult.Data.Rows.Count > 0)
            {
                var row = dataResult.Data.Rows[0];
                
                Service_DebugTracer.TraceMethodExit(row, controlName: "Dao_User");
                return DaoResult<DataRow>.Success(row, $"Found user {user}");
            }
            else
            {
                Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_User");
                return DaoResult<DataRow>.Failure($"User {user} not found");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            
            Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_User");
            return DaoResult<DataRow>.Failure($"Error retrieving user {user}", ex);
        }
    }

    /// <summary>
    /// Checks if a user exists in the system.
    /// </summary>
    internal static async Task<DaoResult<bool>> UserExistsAsync(string user)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["user"] = user }, controlName: "Dao_User");
        
        try
        {
            var dataResult = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                Model_AppVariables.ConnectionString,
                "usr_users_Exists",
                new Dictionary<string, object> { ["User"] = user }
            );

            if (dataResult.IsSuccess && dataResult.Data != null && dataResult.Data.Rows.Count > 0)
            {
                bool exists = Convert.ToInt32(dataResult.Data.Rows[0]["UserExists"]) > 0;
                
                Service_DebugTracer.TraceMethodExit(exists, controlName: "Dao_User");
                return DaoResult<bool>.Success(exists, exists ? $"User {user} exists" : $"User {user} does not exist");
            }
            else
            {
                Service_DebugTracer.TraceMethodExit(false, controlName: "Dao_User");
                return DaoResult<bool>.Success(false, $"User {user} does not exist");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            
            Service_DebugTracer.TraceMethodExit(false, controlName: "Dao_User");
            return DaoResult<bool>.Failure($"Error checking if user {user} exists", ex);
        }
    }

    #endregion

    #region User UI Settings

    internal static async Task<string> GetShortcutsJsonAsync(string userId)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["userId"] = userId }, controlName: "Dao_User");
        
        try
        {
            var dataResult = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                Model_AppVariables.ConnectionString,
                "usr_ui_settings_GetShortcutsJson",
                new Dictionary<string, object> { ["UserId"] = userId }
            );

            if (dataResult.IsSuccess && dataResult.Data != null && dataResult.Data.Rows.Count > 0)
            {
                string? json = dataResult.Data.Rows[0]["SettingJson"]?.ToString();
                
                Service_DebugTracer.TraceMethodExit(json, controlName: "Dao_User");
                return json ?? "";
            }

            Service_DebugTracer.TraceMethodExit("", controlName: "Dao_User");
            return "";
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            LoggingUtility.Log($"GetShortcutsJsonAsync failed with exception: {ex.Message}");
            
            Service_DebugTracer.TraceMethodExit("", controlName: "Dao_User");
            return "";
        }
    }

    internal static async Task SetShortcutsJsonAsync(string userId, string shortcutsJson)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["userId"] = userId, ["shortcutsJson"] = shortcutsJson }, controlName: "Dao_User");
        
        try
        {
            Dictionary<string, object> parameters = new()
            {
                ["UserId"] = userId,
                ["ShortcutsJson"] = shortcutsJson
            };

            var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                Model_AppVariables.ConnectionString,
                "usr_ui_settings_SetShortcutsJson",
                parameters
            );

            if (!result.IsSuccess)
            {
                throw new Exception(result.ErrorMessage ?? "Failed to set shortcuts JSON");
            }
            
            Service_DebugTracer.TraceMethodExit(controlName: "Dao_User");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            LoggingUtility.Log($"SetShortcutsJsonAsync failed with exception: {ex.Message}");
            
            Service_DebugTracer.TraceMethodExit(controlName: "Dao_User");
        }
    }

    internal static async Task SetThemeNameAsync(string user, string themeName)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["user"] = user, ["themeName"] = themeName }, controlName: "Dao_User");
        
        try
        {
            await SetUserSettingAsync("Theme_Name", user, themeName);
            
            Service_DebugTracer.TraceMethodExit(controlName: "Dao_User");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            LoggingUtility.Log($"SetThemeNameAsync failed with exception: {ex.Message}");
            
            Service_DebugTracer.TraceMethodExit(controlName: "Dao_User");
            throw;
        }
    }

    #endregion

    #region User Roles

    internal static async Task<DaoResult> AddUserRoleAsync(int userId, int roleId, string assignedBy)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["userId"] = userId, ["roleId"] = roleId, ["assignedBy"] = assignedBy }, controlName: "Dao_User");
        
        try
        {
            var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                Model_AppVariables.ConnectionString,
                "sys_user_roles_Add",
                new Dictionary<string, object>
                {
                    ["UserID"] = userId,
                    ["RoleID"] = roleId,
                    ["AssignedBy"] = assignedBy
                }
            );

            Service_DebugTracer.TraceMethodExit(result, controlName: "Dao_User");
            
            if (result.IsSuccess)
            {
                return DaoResult.Success($"Role {roleId} assigned to user {userId} successfully");
            }
            else
            {
                return DaoResult.Failure($"Failed to add user role: {result.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            
            Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_User");
            return DaoResult.Failure($"Error adding role {roleId} to user {userId}", ex);
        }
    }

    internal static async Task<DaoResult<int>> GetUserRoleIdAsync(int userId)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["userId"] = userId }, controlName: "Dao_User");
        
        try
        {
            var dataResult = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                Model_AppVariables.ConnectionString,
                "usr_user_roles_GetRoleId_ByUserId",
                new Dictionary<string, object> { ["UserID"] = userId }
            );

            if (dataResult.IsSuccess && dataResult.Data != null && dataResult.Data.Rows.Count > 0)
            {
                if (int.TryParse(dataResult.Data.Rows[0]["RoleID"]?.ToString(), out int roleId))
                {
                    Service_DebugTracer.TraceMethodExit(roleId, controlName: "Dao_User");
                    return DaoResult<int>.Success(roleId, $"Found role {roleId} for user {userId}");
                }
            }

            Service_DebugTracer.TraceMethodExit(0, controlName: "Dao_User");
            return DaoResult<int>.Success(0, $"No role found for user {userId}");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            
            Service_DebugTracer.TraceMethodExit(0, controlName: "Dao_User");
            return DaoResult<int>.Failure($"Error retrieving role for user {userId}", ex);
        }
    }

    internal static async Task<DaoResult> SetUserRoleAsync(int userId, int newRoleId, string assignedBy)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["userId"] = userId, ["newRoleId"] = newRoleId, ["assignedBy"] = assignedBy }, controlName: "Dao_User");
        
        try
        {
            var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                Model_AppVariables.ConnectionString,
                "sys_user_roles_Update",
                new Dictionary<string, object>
                {
                    ["UserID"] = userId,
                    ["NewRoleID"] = newRoleId,
                    ["AssignedBy"] = assignedBy
                }
            );

            Service_DebugTracer.TraceMethodExit(result, controlName: "Dao_User");
            
            if (result.IsSuccess)
            {
                return DaoResult.Success($"User {userId} role updated to {newRoleId} successfully");
            }
            else
            {
                return DaoResult.Failure($"Failed to update user role: {result.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            
            Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_User");
            return DaoResult.Failure($"Error updating role for user {userId}", ex);
        }
    }

    internal static async Task<DaoResult> SetUsersRoleAsync(IEnumerable<int> userIds, int newRoleId, string assignedBy)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["userIds"] = string.Join(",", userIds), ["newRoleId"] = newRoleId, ["assignedBy"] = assignedBy }, controlName: "Dao_User");
        
        try
        {
            var errors = new List<string>();
            
            foreach (int userId in userIds)
            {
                var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                    Model_AppVariables.ConnectionString,
                    "sys_user_roles_Update",
                    new Dictionary<string, object>
                    {
                        ["UserID"] = userId,
                        ["NewRoleID"] = newRoleId,
                        ["AssignedBy"] = assignedBy
                    }
                );

                if (!result.IsSuccess)
                {
                    errors.Add($"User {userId}: {result.ErrorMessage}");
                }
            }

            if (errors.Any())
            {
                var errorMessage = $"Failed to update {errors.Count} user(s): {string.Join("; ", errors)}";
                
                Service_DebugTracer.TraceMethodExit(errorMessage, controlName: "Dao_User");
                return DaoResult.Failure(errorMessage);
            }

            Service_DebugTracer.TraceMethodExit("Success", controlName: "Dao_User");
            return DaoResult.Success($"Updated role to {newRoleId} for {userIds.Count()} user(s) successfully");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            
            Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_User");
            return DaoResult.Failure("Error updating roles for multiple users", ex);
        }
    }

    internal static async Task<DaoResult> RemoveUserRoleAsync(int userId, int roleId)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["userId"] = userId, ["roleId"] = roleId }, controlName: "Dao_User");
        
        try
        {
            var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                Model_AppVariables.ConnectionString,
                "sys_user_roles_Delete",
                new Dictionary<string, object> 
                { 
                    ["UserID"] = userId,
                    ["RoleID"] = roleId
                }
            );

            Service_DebugTracer.TraceMethodExit(result, controlName: "Dao_User");
            
            if (result.IsSuccess)
            {
                return DaoResult.Success($"Role {roleId} removed from user {userId} successfully");
            }
            else
            {
                return DaoResult.Failure($"Failed to remove user role: {result.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            
            Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_User");
            return DaoResult.Failure($"Error removing role {roleId} from user {userId}", ex);
        }
    }

    #endregion
}

#endregion
