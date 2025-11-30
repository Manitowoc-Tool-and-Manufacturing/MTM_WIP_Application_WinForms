using System.Data;
using System.Text.Json;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Services.Logging;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;
using MySql.Data.MySqlClient;

namespace MTM_WIP_Application_Winforms.Data;

#region Dao_User

/// <summary>
/// Data Access Object for user management and user settings operations.
/// Implements Model_Dao_Result pattern with async/await and Service_DebugTracer integration.
/// </summary>
internal static class Dao_User
{
    #region User Settings Getters/Setters

    /// <summary>
    /// Gets the last shown version for the specified user.
    /// </summary>
    /// <param name="user">The username.</param>
    /// <returns>A Model_Dao_Result containing the last shown version string.</returns>
    internal static async Task<Model_Dao_Result<string>> GetLastShownVersionAsync(string user,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["user"] = user }, controlName: "Dao_User");

        try
        {
            var result = await GetSettingsJsonInternalAsync("LastShownVersion", user, connection, transaction);

            Service_DebugTracer.TraceMethodExit(result, controlName: "Dao_User");
            return Model_Dao_Result<string>.Success(result, $"Retrieved LastShownVersion for user {user}");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_User");
            return Model_Dao_Result<string>.Failure($"Error retrieving LastShownVersion for user {user}", ex);
        }
    }

    /// <summary>
    /// Sets the last shown version for the specified user.
    /// </summary>
    /// <param name="user">The username.</param>
    /// <param name="value">The version value to set.</param>
    /// <returns>A Model_Dao_Result indicating success or failure.</returns>
    internal static async Task<Model_Dao_Result> SetLastShownVersionAsync(string user, string value,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["user"] = user, ["value"] = value }, controlName: "Dao_User");

        try
        {
            await SetUserSettingInternalAsync("LastShownVersion", user, value, connection, transaction);

            Service_DebugTracer.TraceMethodExit(controlName: "Dao_User");
            return Model_Dao_Result.Success($"Set LastShownVersion to {value} for user {user}");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_User");
            return Model_Dao_Result.Failure($"Error setting LastShownVersion for user {user}", ex);
        }
    }

    /// <summary>
    /// Gets the HideChangeLog setting for the specified user.
    /// </summary>
    /// <param name="user">The username.</param>
    /// <returns>A Model_Dao_Result containing the HideChangeLog value.</returns>
    internal static async Task<Model_Dao_Result<string>> GetHideChangeLogAsync(string user,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["user"] = user }, controlName: "Dao_User");

        try
        {
            var result = await GetSettingsJsonInternalAsync("HideChangeLog", user, connection, transaction);

            Service_DebugTracer.TraceMethodExit(result, controlName: "Dao_User");
            return Model_Dao_Result<string>.Success(result, $"Retrieved HideChangeLog for user {user}");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_User");
            return Model_Dao_Result<string>.Failure($"Error retrieving HideChangeLog for user {user}", ex);
        }
    }

    /// <summary>
    /// Sets the HideChangeLog setting for the specified user.
    /// </summary>
    /// <param name="user">The username.</param>
    /// <param name="value">The value to set.</param>
    /// <returns>A Model_Dao_Result indicating success or failure.</returns>
    internal static async Task<Model_Dao_Result> SetHideChangeLogAsync(string user, string value,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["user"] = user, ["value"] = value }, controlName: "Dao_User");

        try
        {
            await SetUserSettingInternalAsync("HideChangeLog", user, value, connection, transaction);

            Service_DebugTracer.TraceMethodExit(controlName: "Dao_User");
            return Model_Dao_Result.Success($"Set HideChangeLog to {value} for user {user}");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_User");
            return Model_Dao_Result.Failure($"Error setting HideChangeLog for user {user}", ex);
        }
    }

    /// <summary>
    /// Gets the theme name for the specified user.
    /// </summary>
    /// <param name="user">The username.</param>
    /// <returns>A Model_Dao_Result containing the theme name.</returns>
    internal static async Task<Model_Dao_Result<string?>> GetThemeNameAsync(string user,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["user"] = user }, controlName: "Dao_User");

        try
        {
            var result = await GetSettingsJsonInternalAsync("Theme_Name", user, connection, transaction);

            Service_DebugTracer.TraceMethodExit(result, controlName: "Dao_User");
            return Model_Dao_Result<string?>.Success(result, $"Retrieved Theme_Name for user {user}");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_User");
            return Model_Dao_Result<string?>.Failure($"Error retrieving Theme_Name for user {user}", ex);
        }
    }

    /// <summary>
    /// Gets whether the theme system is enabled for the specified user.
    /// </summary>
    /// <param name="user">The username.</param>
    /// <returns>A Model_Dao_Result containing true if enabled, false if disabled. Defaults to true.</returns>
    internal static async Task<Model_Dao_Result<bool>> GetThemeEnabledAsync(string user,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        // Always return true as theming is now mandatory
        return await Task.FromResult(Model_Dao_Result<bool>.Success(true, $"Theme_Enabled is always true"));
    }

    /// <summary>
    /// Sets whether the theme system is enabled for the specified user.
    /// </summary>
    /// <param name="user">The username.</param>
    /// <param name="enabled">True to enable theming, false to disable (DPI scaling remains active).</param>
    /// <returns>A Model_Dao_Result indicating success or failure.</returns>
    internal static async Task<Model_Dao_Result> SetThemeEnabledAsync(string user, bool enabled,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        // No-op as theming is always enabled
        return await Task.FromResult(Model_Dao_Result.Success($"Theme_Enabled is always true (ignored set to {enabled})"));
    }

    /// <summary>
    /// Gets the theme font size for the specified user.
    /// </summary>
    /// <param name="user">The username.</param>
    /// <returns>A Model_Dao_Result containing the font size, or null if not set.</returns>
    internal static async Task<Model_Dao_Result<int?>> GetThemeFontSizeAsync(string user,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["user"] = user }, controlName: "Dao_User");

        try
        {
            string str = await GetSettingsJsonInternalAsync("Theme_FontSize", user, connection, transaction);
            int? result = int.TryParse(str, out int val) ? val : null;

            Service_DebugTracer.TraceMethodExit(result, controlName: "Dao_User");
            return Model_Dao_Result<int?>.Success(result, $"Retrieved Theme_FontSize for user {user}");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);


            Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_User");
            return Model_Dao_Result<int?>.Failure($"Error retrieving Theme_FontSize for user {user}", ex);
        }
    }

    /// <summary>
    /// Sets the theme font size for the specified user.
    /// </summary>
    /// <param name="user">The username.</param>
    /// <param name="value">The font size value.</param>
    /// <returns>A Model_Dao_Result indicating success or failure.</returns>
    internal static async Task<Model_Dao_Result> SetThemeFontSizeAsync(string user, int value,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["user"] = user, ["value"] = value }, controlName: "Dao_User");

        try
        {
            await SetUserSettingInternalAsync("Theme_FontSize", user, value.ToString(), connection, transaction);

            Service_DebugTracer.TraceMethodExit(controlName: "Dao_User");
            return Model_Dao_Result.Success($"Set Theme_FontSize to {value} for user {user}");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_User");
            return Model_Dao_Result.Failure($"Error setting Theme_FontSize for user {user}", ex);
        }
    }

    /// <summary>
    /// Gets the Visual username for the specified user and updates Model_Shared_Users.
    /// </summary>
    /// <param name="user">The username.</param>
    /// <returns>A Model_Dao_Result containing the Visual username.</returns>
    internal static async Task<Model_Dao_Result<string>> GetVisualUserNameAsync(string user,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["user"] = user }, controlName: "Dao_User");

        try
        {
            string value = await GetSettingsJsonInternalAsync("VisualUserName", user, connection, transaction);
            Model_Shared_Users.VisualUserName = value;

            Service_DebugTracer.TraceMethodExit(Model_Shared_Users.VisualUserName, controlName: "Dao_User");
            return Model_Dao_Result<string>.Success(Model_Shared_Users.VisualUserName, $"Retrieved VisualUserName for user {user}");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_User");
            return Model_Dao_Result<string>.Failure($"Error retrieving VisualUserName for user {user}", ex);
        }
    }

    /// <summary>
    /// Sets the Visual username for the specified user.
    /// </summary>
    /// <param name="user">The username.</param>
    /// <param name="value">The Visual username value.</param>
    /// <returns>A Model_Dao_Result indicating success or failure.</returns>
    internal static async Task<Model_Dao_Result> SetVisualUserNameAsync(string user, string value,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["user"] = user, ["value"] = value }, controlName: "Dao_User");

        try
        {
            await SetUserSettingInternalAsync("VisualUserName", user, value, connection, transaction);

            Service_DebugTracer.TraceMethodExit(controlName: "Dao_User");
            return Model_Dao_Result.Success($"Set VisualUserName to {value} for user {user}");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_User");
            return Model_Dao_Result.Failure($"Error setting VisualUserName for user {user}", ex);
        }
    }

    /// <summary>
    /// Gets the Visual password for the specified user and updates Model_Shared_Users.
    /// </summary>
    /// <param name="user">The username.</param>
    /// <returns>A Model_Dao_Result containing the Visual password.</returns>
    internal static async Task<Model_Dao_Result<string>> GetVisualPasswordAsync(string user,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["user"] = user }, controlName: "Dao_User");

        try
        {
            string value = await GetSettingsJsonInternalAsync("VisualPassword", user, connection, transaction);
            Model_Shared_Users.VisualPassword = value;

            Service_DebugTracer.TraceMethodExit(Model_Shared_Users.VisualPassword, controlName: "Dao_User");
            return Model_Dao_Result<string>.Success(Model_Shared_Users.VisualPassword, $"Retrieved VisualPassword for user {user}");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_User");
            return Model_Dao_Result<string>.Failure($"Error retrieving VisualPassword for user {user}", ex);
        }
    }

    /// <summary>
    /// Sets the Visual password for the specified user.
    /// </summary>
    /// <param name="user">The username.</param>
    /// <param name="value">The Visual password value.</param>
    /// <returns>A Model_Dao_Result indicating success or failure.</returns>
    internal static async Task<Model_Dao_Result> SetVisualPasswordAsync(string user, string value,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["user"] = user, ["value"] = value }, controlName: "Dao_User");

        try
        {
            await SetUserSettingInternalAsync("VisualPassword", user, value, connection, transaction);

            Service_DebugTracer.TraceMethodExit(controlName: "Dao_User");
            return Model_Dao_Result.Success($"Set VisualPassword for user {user}");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_User");
            return Model_Dao_Result.Failure($"Error setting VisualPassword for user {user}", ex);
        }
    }

    /// <summary>
    /// Gets the WIP server address for the specified user and updates Model_Shared_Users.
    /// </summary>
    /// <param name="user">The username.</param>
    /// <returns>A Model_Dao_Result containing the WIP server address.</returns>
    internal static async Task<Model_Dao_Result<string>> GetWipServerAddressAsync(string user,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["user"] = user }, controlName: "Dao_User");

        try
        {
            string value = await GetSettingsJsonInternalAsync("WipServerAddress", user, connection, transaction);
            // DO NOT update Model_Shared_Users.WipServerAddress here to avoid circular dependency
            // Let the caller decide when to update global state

            Service_DebugTracer.TraceMethodExit(value, controlName: "Dao_User");
            return Model_Dao_Result<string>.Success(value, $"Retrieved WipServerAddress for user {user}");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_User");
            return Model_Dao_Result<string>.Failure($"Error retrieving WipServerAddress for user {user}", ex);
        }
    }

    /// <summary>
    /// Sets the WIP server address for the specified user.
    /// </summary>
    /// <param name="user">The username.</param>
    /// <param name="value">The server address value.</param>
    /// <returns>A Model_Dao_Result indicating success or failure.</returns>
    internal static async Task<Model_Dao_Result> SetWipServerAddressAsync(string user, string value,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["user"] = user, ["value"] = value }, controlName: "Dao_User");

        try
        {
            // Save setting to CURRENT database/server before updating global state
            await SetUserSettingInternalAsync("WipServerAddress", user, value, connection, transaction);

            // Update global state after successful save
            Model_Shared_Users.WipServerAddress = value;

            Service_DebugTracer.TraceMethodExit(controlName: "Dao_User");
            return Model_Dao_Result.Success($"Set WipServerAddress to {value} for user {user}");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_User");
            return Model_Dao_Result.Failure($"Error setting WipServerAddress for user {user}", ex);
        }
    }

    #region Get/Set Database

    /// <summary>
    /// Gets the database name for the specified user and updates Model_Shared_Users.
    /// </summary>
    /// <param name="user">The username.</param>
    /// <returns>A Model_Dao_Result containing the database name.</returns>
    internal static async Task<Model_Dao_Result<string>> GetDatabaseAsync(string user,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["user"] = user }, controlName: "Dao_User");

        try
        {
            string value = await GetSettingsJsonInternalAsync("WIPDatabase", user, connection, transaction);
            // DO NOT update Model_Shared_Users.Database here to avoid circular dependency
            // Let the caller decide when to update global state

            Service_DebugTracer.TraceMethodExit(value, controlName: "Dao_User");
            return Model_Dao_Result<string>.Success(value, $"Retrieved WIPDatabase for user {user}");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_User");
            return Model_Dao_Result<string>.Failure($"Error retrieving WIPDatabase for user {user}", ex);
        }
    }

    /// <summary>
    /// Sets the database name for the specified user and updates Model_Shared_Users.
    /// </summary>
    /// <param name="user">The username.</param>
    /// <param name="value">The database name value.</param>
    /// <returns>A Model_Dao_Result indicating success or failure.</returns>
    internal static async Task<Model_Dao_Result> SetDatabaseAsync(string user, string value,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["user"] = user, ["value"] = value }, controlName: "Dao_User");

        try
        {
            // Save setting to CURRENT database before updating global state
            await SetUserSettingInternalAsync("WIPDatabase", user, value, connection, transaction);

            // Update global state after successful save
            Model_Shared_Users.Database = value;

            Service_DebugTracer.TraceMethodExit(controlName: "Dao_User");
            return Model_Dao_Result.Success($"Set WIPDatabase to {value} for user {user}");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_User");
            return Model_Dao_Result.Failure($"Error setting WIPDatabase for user {user}", ex);
        }
    }

    #endregion

    #region Get/Set WipServerPort

    /// <summary>
    /// Gets the WIP server port for the specified user and updates Model_Shared_Users.
    /// </summary>
    /// <param name="user">The username.</param>
    /// <returns>A Model_Dao_Result containing the server port.</returns>
    internal static async Task<Model_Dao_Result<string>> GetWipServerPortAsync(string user,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["user"] = user }, controlName: "Dao_User");

        try
        {
            string value = await GetSettingsJsonInternalAsync("WipServerPort", user, connection, transaction);
            // DO NOT update Model_Shared_Users.WipServerPort here to avoid circular dependency
            // Let the caller decide when to update global state

            Service_DebugTracer.TraceMethodExit(value, controlName: "Dao_User");
            return Model_Dao_Result<string>.Success(value, $"Retrieved WipServerPort for user {user}");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_User");
            return Model_Dao_Result<string>.Failure($"Error retrieving WipServerPort for user {user}", ex);
        }
    }

    /// <summary>
    /// Sets the WIP server port for the specified user and updates Model_Shared_Users.
    /// </summary>
    /// <param name="user">The username.</param>
    /// <param name="value">The server port value.</param>
    /// <returns>A Model_Dao_Result indicating success or failure.</returns>
    internal static async Task<Model_Dao_Result> SetWipServerPortAsync(string user, string value,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["user"] = user, ["value"] = value }, controlName: "Dao_User");

        try
        {
            // Save setting to CURRENT database/server before updating global state
            await SetUserSettingInternalAsync("WipServerPort", user, value, connection, transaction);

            // Update global state after successful save
            Model_Shared_Users.WipServerPort = value;

            Service_DebugTracer.TraceMethodExit(controlName: "Dao_User");
            return Model_Dao_Result.Success($"Set WipServerPort to {value} for user {user}");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_User");
            return Model_Dao_Result.Failure($"Error setting WipServerPort for user {user}", ex);
        }
    }

    #endregion

    /// <summary>
    /// Gets the full name for the specified user and updates Model_Shared_Users.
    /// </summary>
    /// <param name="user">The username.</param>
    /// <returns>A Model_Dao_Result containing the user's full name.</returns>
    internal static async Task<Model_Dao_Result<string?>> GetUserFullNameAsync(string user,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["user"] = user }, controlName: "Dao_User");

        try
        {
            var dataResult = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                Model_Application_Variables.ConnectionString,
                "usr_users_Get_ByUser",
                new Dictionary<string, object> { ["User"] = user },
                connection: connection,
                transaction: transaction
            );

            if (dataResult.IsSuccess && dataResult.Data != null && dataResult.Data.Rows.Count > 0)
            {
                DataRow row = dataResult.Data.Rows[0];
                object? fullNameObj = row["Full Name"];
                string? fullName = fullNameObj == DBNull.Value ? null : fullNameObj?.ToString();

                Model_Shared_Users.FullName = fullName ?? string.Empty;

                Service_DebugTracer.TraceMethodExit(fullName, controlName: "Dao_User");
                return Model_Dao_Result<string?>.Success(fullName, $"Retrieved full name for user {user}");
            }

            Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_User");
            return Model_Dao_Result<string?>.Success(null, $"No full name found for user {user}");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);


            Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_User");
            return Model_Dao_Result<string?>.Failure($"Error retrieving full name for user {user}", ex);
        }
    }

    /// <summary>
    /// Internal helper to get settings JSON field value.
    /// </summary>
    private static async Task<string> GetSettingsJsonInternalAsync(string field, string user,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["field"] = field, ["user"] = user }, controlName: "Dao_User");

        try
        {
            // Use a safe connection string that works during initial bootstrap
            // If Bootstrap is already initialized, use it. Otherwise, use current connection string.
            string connectionString = Model_Application_Variables.ConnectionString;

            var dataResult = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                connectionString,
                "usr_settings_Get",
                new Dictionary<string, object> { ["UserId"] = user },
                connection: connection,
                transaction: transaction
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
                            LoggingUtility.LogDatabaseError(ex);
                            Service_DebugTracer.TraceMethodExit(string.Empty, controlName: "Dao_User");
                            return string.Empty;
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

    /// <summary>
    /// Sets theme JSON settings for the specified user.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <param name="themeJson">The theme JSON to set.</param>
    /// <returns>A Model_Dao_Result indicating success or failure.</returns>
    public static async Task<Model_Dao_Result> SetSettingsJsonAsync(string userId, string themeJson,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
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
                Model_Application_Variables.ConnectionString,
                "usr_settings_SetThemeJson",
                parameters,
                connection: connection,
                transaction: transaction
            );

            if (result.IsSuccess)
            {
                Service_DebugTracer.TraceMethodExit(result, controlName: "Dao_User");
                return Model_Dao_Result.Success($"Set theme JSON for user {userId}");
            }
            else
            {
                Service_DebugTracer.TraceMethodExit(result, controlName: "Dao_User");
                return Model_Dao_Result.Failure($"Failed to set theme JSON: {result.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);


            Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_User");
            return Model_Dao_Result.Failure($"Error setting theme JSON for user {userId}", ex);
        }
    }

    /// <summary>
    /// Sets grid view settings JSON for the specified user and DataGridView.
    /// Uses the dedicated usr_dgv_settings table.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <param name="dgvName">The DataGridView name.</param>
    /// <param name="settingsJson">The settings JSON to set.</param>
    /// <returns>A Model_Dao_Result indicating success or failure.</returns>
    public static async Task<Model_Dao_Result> SetGridViewSettingsJsonAsync(string userId, string dgvName, string settingsJson,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["userId"] = userId, ["dgvName"] = dgvName, ["settingsJson"] = settingsJson }, controlName: "Dao_User");

        try
        {
            Dictionary<string, object> parameters = new()
            {
                ["p_UserId"] = userId,
                ["p_DgvName"] = dgvName,
                ["p_SettingsJson"] = settingsJson
            };

            var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                Model_Application_Variables.ConnectionString,
                "usr_dgv_settings_Set",
                parameters,
                connection: connection,
                transaction: transaction
            );

            if (result.IsSuccess)
            {
                Service_DebugTracer.TraceMethodExit(result, controlName: "Dao_User");
                return Model_Dao_Result.Success($"Set grid view settings for {dgvName}");
            }
            else
            {
                Service_DebugTracer.TraceMethodExit(result, controlName: "Dao_User");
                return Model_Dao_Result.Failure($"Failed to set grid view settings: {result.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_User");
            return Model_Dao_Result.Failure($"Error setting grid view settings for {dgvName}", ex);
        }
    }

    /// <summary>
    /// Gets grid view settings JSON for the specified user and DataGridView.
    /// Uses the dedicated usr_dgv_settings table.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <param name="dgvName">The DataGridView name.</param>
    /// <returns>A Model_Dao_Result containing the settings JSON string.</returns>
    public static async Task<Model_Dao_Result<string>> GetGridViewSettingsJsonAsync(string userId, string dgvName,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["userId"] = userId, ["dgvName"] = dgvName }, controlName: "Dao_User");

        try
        {
            Dictionary<string, object> parameters = new()
            {
                ["p_UserId"] = userId,
                ["p_DgvName"] = dgvName
            };

            var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                Model_Application_Variables.ConnectionString,
                "usr_dgv_settings_Get",
                parameters,
                connection: connection,
                transaction: transaction
            );

            if (result.IsSuccess && result.Data != null && result.Data.Rows.Count > 0)
            {
                string json = result.Data.Rows[0]["SettingsJson"]?.ToString() ?? string.Empty;
                Service_DebugTracer.TraceMethodExit(result, controlName: "Dao_User");
                return Model_Dao_Result<string>.Success(json, "Settings retrieved successfully");
            }
            else
            {
                Service_DebugTracer.TraceMethodExit(result, controlName: "Dao_User");
                return Model_Dao_Result<string>.Failure("No settings found", null);
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_User");
            return Model_Dao_Result<string>.Failure($"Error getting grid view settings for {dgvName}", ex);
        }
    }



    /// <summary>
    /// Internal helper to set a user setting field value in usr_settings.SettingsJson.
    /// </summary>
    private static async Task SetUserSettingInternalAsync(string field, string user, string value,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["field"] = field, ["user"] = user, ["value"] = value }, controlName: "Dao_User");

        try
        {
            // Use current connection string (settings are now in usr_settings, not usr_users)
            string connectionString = Model_Application_Variables.ConnectionString;

            var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                connectionString,
                "usr_settings_SetUserSetting_ByUserAndField",
                new Dictionary<string, object>
                {
                    ["User"] = user,
                    ["Field"] = field,
                    ["Value"] = value
                },
                connection: connection,
                transaction: transaction
            );

            if (!result.IsSuccess)
            {
                LoggingUtility.Log($"Failed to set user setting '{field}' for user '{user}': {result.ErrorMessage}");
            }

            Service_DebugTracer.TraceMethodExit(controlName: "Dao_User");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);


            Service_DebugTracer.TraceMethodExit(controlName: "Dao_User");
        }
    }

    #endregion

    #region Add / Update / Delete

    /// <summary>
    /// Deletes all UI settings for the specified user.
    /// </summary>
    /// <param name="userName">The username whose settings to delete.</param>
    /// <returns>A Model_Dao_Result indicating success or failure.</returns>
    internal static async Task<Model_Dao_Result> DeleteUserSettingsAsync(string userName,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["userName"] = userName }, controlName: "Dao_User");

        try
        {
            var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                Model_Application_Variables.ConnectionString,
                "usr_settings_Delete_ByUserId",
                new Dictionary<string, object> { ["UserId"] = userName },
                connection: connection,
                transaction: transaction
            );

            Service_DebugTracer.TraceMethodExit(result, controlName: "Dao_User");

            if (result.IsSuccess)
            {
                return Model_Dao_Result.Success($"User settings for {userName} deleted successfully");
            }
            else
            {
                return Model_Dao_Result.Failure($"Failed to delete user settings: {result.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);

            Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_User");
            return Model_Dao_Result.Failure($"Error deleting user settings for {userName}", ex);
        }
    }

    /// <summary>
    /// Creates a new user with the specified details and default settings.
    /// </summary>
    internal static async Task<Model_Dao_Result> CreateUserAsync(
        string user, string fullName, string shift, bool vitsUser, string pin,
        string lastShownVersion, string hideChangeLog, string themeName, int themeFontSize,
        string visualUserName, string visualPassword, string wipServerAddress, string database,
        string wipServerPort,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
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
                Model_Application_Variables.ConnectionString,
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
                    ["ThemeName"] = themeName,              // Fixed: was Theme_Name (SP param is p_ThemeName)
                    ["ThemeFontSize"] = themeFontSize,      // Fixed: was Theme_FontSize (SP param is p_ThemeFontSize)
                    ["VisualUserName"] = visualUserName,
                    ["VisualPassword"] = visualPassword,
                    ["WipServerAddress"] = wipServerAddress,
                    ["WipServerPort"] = wipServerPort,      // Fixed: Parameter order - WipServerPort comes before WipDatabase in SP
                    ["WipDatabase"] = database              // Fixed: was WIPDatabase, now WipDatabase (SP param is p_WipDatabase)
                },
                connection: connection,
                transaction: transaction
            );

            Service_DebugTracer.TraceMethodExit(result, controlName: "Dao_User");

            if (result.IsSuccess)
            {
                return Model_Dao_Result.Success($"User {user} created successfully");
            }
            else
            {
                return Model_Dao_Result.Failure($"Failed to create user: {result.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);

            Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_User");
            return Model_Dao_Result.Failure($"Error creating user {user}", ex);
        }
    }

    /// <summary>
    /// Updates an existing user's information.
    /// </summary>
    internal static async Task<Model_Dao_Result> UpdateUserAsync(
        string user,
        string fullName,
        string shift,
        string pin,
        string visualUserName,
        string visualPassword,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
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
                Model_Application_Variables.ConnectionString,
                "usr_users_Update_User",
                new Dictionary<string, object>
                {
                    ["User"] = user,
                    ["FullName"] = fullName,
                    ["Shift"] = shift,
                    ["Pin"] = pin,
                    ["VisualUserName"] = visualUserName,
                    ["VisualPassword"] = visualPassword
                },
                connection: connection,
                transaction: transaction
            );

            Service_DebugTracer.TraceMethodExit(result, controlName: "Dao_User");

            if (result.IsSuccess)
            {
                return Model_Dao_Result.Success($"User {user} updated successfully");
            }
            else
            {
                return Model_Dao_Result.Failure($"Failed to update user: {result.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);

            Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_User");
            return Model_Dao_Result.Failure($"Error updating user {user}", ex);
        }
    }

    /// <summary>
    /// Deletes the specified user from the system.
    /// </summary>
    internal static async Task<Model_Dao_Result> DeleteUserAsync(string user,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["user"] = user }, controlName: "Dao_User");

        try
        {
            var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                Model_Application_Variables.ConnectionString,
                "usr_users_Delete_User",
                new Dictionary<string, object> { ["User"] = user },
                connection: connection,
                transaction: transaction
            );

            Service_DebugTracer.TraceMethodExit(result, controlName: "Dao_User");

            if (result.IsSuccess)
            {
                return Model_Dao_Result.Success($"User {user} deleted successfully");
            }
            else
            {
                return Model_Dao_Result.Failure($"Failed to delete user: {result.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);

            Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_User");
            return Model_Dao_Result.Failure($"Error deleting user {user}", ex);
        }
    }

    #endregion

    #region Queries

    /// <summary>
    /// Retrieves all users from the system.
    /// </summary>
    /// <returns>A Model_Dao_Result containing a DataTable with all users.</returns>
    internal static async Task<Model_Dao_Result<DataTable>> GetAllUsersAsync(
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        Service_DebugTracer.TraceMethodEntry(controlName: "Dao_User");

        try
        {
            var dataResult = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                Model_Application_Variables.ConnectionString,
                "usr_users_Get_All",
                null,
                connection: connection,
                transaction: transaction
            );

            Service_DebugTracer.TraceMethodExit(dataResult, controlName: "Dao_User");

            if (dataResult.IsSuccess && dataResult.Data != null)
            {
                return Model_Dao_Result<DataTable>.Success(dataResult.Data, $"Retrieved {dataResult.Data.Rows.Count} users");
            }
            else
            {
                return Model_Dao_Result<DataTable>.Failure($"Failed to retrieve users: {dataResult.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);

            Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_User");
            return Model_Dao_Result<DataTable>.Failure("Error retrieving users", ex);
        }
    }

    /// <summary>
    /// Retrieves a specific user by username.
    /// </summary>
    internal static async Task<Model_Dao_Result<DataRow>> GetUserByUsernameAsync(string user,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["user"] = user }, controlName: "Dao_User");

        try
        {
            var dataResult = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                Model_Application_Variables.ConnectionString,
                "usr_users_Get_ByUser",
                new Dictionary<string, object> { ["User"] = user },
                connection: connection,
                transaction: transaction
            );

            if (dataResult.IsSuccess && dataResult.Data != null && dataResult.Data.Rows.Count > 0)
            {
                var row = dataResult.Data.Rows[0];

                Service_DebugTracer.TraceMethodExit(row, controlName: "Dao_User");
                return Model_Dao_Result<DataRow>.Success(row, $"Found user {user}");
            }
            else
            {
                Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_User");
                return Model_Dao_Result<DataRow>.Failure($"User {user} not found");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);

            Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_User");
            return Model_Dao_Result<DataRow>.Failure($"Error retrieving user {user}", ex);
        }
    }

    /// <summary>
    /// Checks if a user exists in the system.
    /// </summary>
    internal static async Task<Model_Dao_Result<bool>> UserExistsAsync(string user,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["user"] = user }, controlName: "Dao_User");

        try
        {
            var dataResult = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                Model_Application_Variables.ConnectionString,
                "usr_users_Exists",
                new Dictionary<string, object> { ["User"] = user },
                connection: connection,
                transaction: transaction
            );

            if (dataResult.IsSuccess && dataResult.Data != null && dataResult.Data.Rows.Count > 0)
            {
                bool exists = Convert.ToInt32(dataResult.Data.Rows[0]["UserExists"]) > 0;

                Service_DebugTracer.TraceMethodExit(exists, controlName: "Dao_User");
                return Model_Dao_Result<bool>.Success(exists, exists ? $"User {user} exists" : $"User {user} does not exist");
            }
            else
            {
                Service_DebugTracer.TraceMethodExit(false, controlName: "Dao_User");
                return Model_Dao_Result<bool>.Success(false, $"User {user} does not exist");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);

            Service_DebugTracer.TraceMethodExit(false, controlName: "Dao_User");
            return Model_Dao_Result<bool>.Failure($"Error checking if user {user} exists", ex);
        }
    }

    #endregion

    #region User UI Settings

    /// <summary>
    /// Sets the theme name for the specified user.
    /// </summary>
    /// <param name="user">The username.</param>
    /// <param name="themeName">The theme name to set.</param>
    /// <returns>A Model_Dao_Result indicating success or failure.</returns>
    internal static async Task<Model_Dao_Result> SetThemeNameAsync(string user, string themeName,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["user"] = user, ["themeName"] = themeName }, controlName: "Dao_User");

        try
        {
            // Theme_Name is stored in SettingsJson, not as a column
            // We need to update the JSON field, not a table column
            await SetSettingsJsonFieldAsync(user, "Theme_Name", themeName, connection, transaction);

            Service_DebugTracer.TraceMethodExit(controlName: "Dao_User");
            return Model_Dao_Result.Success($"Set theme name to {themeName} for user {user}");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);


            Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_User");
            return Model_Dao_Result.Failure($"Error setting theme name for user {user}", ex);
        }
    }

    /// <summary>
    /// Sets a single field in the user's SettingsJson.
    /// </summary>
    /// <param name="user">The username.</param>
    /// <param name="field">The JSON field name.</param>
    /// <param name="value">The value to set.</param>
    private static async Task SetSettingsJsonFieldAsync(string user, string field, string value,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        // Use the existing usr_settings_SetThemeJson procedure which merges JSON
        string connectionString = Model_Application_Variables.ConnectionString;

        // Create a simple JSON object with just this field
        string themeJson = $"{{\"{field}\": \"{value}\"}}";

        // This procedure will merge the JSON with existing settings
        await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
            connectionString,
            "usr_settings_SetThemeJson",
            new Dictionary<string, object>
            {
                ["UserID"] = user,
                ["ThemeJson"] = themeJson
            },
            connection: connection,
            transaction: transaction
        );
    }

    #endregion

    #region User Roles

    internal static async Task<Model_Dao_Result> AddUserRoleAsync(int userId, int roleId, string assignedBy,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["userId"] = userId, ["roleId"] = roleId, ["assignedBy"] = assignedBy }, controlName: "Dao_User");

        try
        {
            var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                Model_Application_Variables.ConnectionString,
                "sys_user_roles_Add",
                new Dictionary<string, object>
                {
                    ["UserID"] = userId,
                    ["RoleID"] = roleId,
                    ["AssignedBy"] = assignedBy
                },
                connection: connection,
                transaction: transaction
            );

            Service_DebugTracer.TraceMethodExit(result, controlName: "Dao_User");

            if (result.IsSuccess)
            {
                return Model_Dao_Result.Success($"Role {roleId} assigned to user {userId} successfully");
            }
            else
            {
                return Model_Dao_Result.Failure($"Failed to add user role: {result.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);

            Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_User");
            return Model_Dao_Result.Failure($"Error adding role {roleId} to user {userId}", ex);
        }
    }

    internal static async Task<Model_Dao_Result<int>> GetUserRoleIdAsync(int userId,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["userId"] = userId }, controlName: "Dao_User");

        try
        {
            var dataResult = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                Model_Application_Variables.ConnectionString,
                "sys_user_roles_Get_ById",
                new Dictionary<string, object> { ["UserID"] = userId },
                connection: connection,
                transaction: transaction
            );

            if (dataResult.IsSuccess && dataResult.Data != null && dataResult.Data.Rows.Count > 0)
            {
                if (int.TryParse(dataResult.Data.Rows[0]["RoleID"]?.ToString(), out int roleId))
                {
                    Service_DebugTracer.TraceMethodExit(roleId, controlName: "Dao_User");
                    return Model_Dao_Result<int>.Success(roleId, $"Found role {roleId} for user {userId}");
                }
            }

            Service_DebugTracer.TraceMethodExit(0, controlName: "Dao_User");
            return Model_Dao_Result<int>.Success(0, $"No role found for user {userId}");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);

            Service_DebugTracer.TraceMethodExit(0, controlName: "Dao_User");
            return Model_Dao_Result<int>.Failure($"Error retrieving role for user {userId}", ex);
        }
    }

    internal static async Task<Model_Dao_Result> SetUserRoleAsync(int userId, int newRoleId, string assignedBy,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["userId"] = userId, ["newRoleId"] = newRoleId, ["assignedBy"] = assignedBy }, controlName: "Dao_User");

        try
        {
            var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                Model_Application_Variables.ConnectionString,
                "sys_user_roles_Update",
                new Dictionary<string, object>
                {
                    ["UserID"] = userId,
                    ["NewRoleID"] = newRoleId,
                    ["AssignedBy"] = assignedBy
                },
                connection: connection,
                transaction: transaction
            );

            Service_DebugTracer.TraceMethodExit(result, controlName: "Dao_User");

            if (result.IsSuccess)
            {
                return Model_Dao_Result.Success($"User {userId} role updated to {newRoleId} successfully");
            }
            else
            {
                return Model_Dao_Result.Failure($"Failed to update user role: {result.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);

            Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_User");
            return Model_Dao_Result.Failure($"Error updating role for user {userId}", ex);
        }
    }

    internal static async Task<Model_Dao_Result> SetUsersRoleAsync(IEnumerable<int> userIds, int newRoleId, string assignedBy,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["userIds"] = string.Join(",", userIds), ["newRoleId"] = newRoleId, ["assignedBy"] = assignedBy }, controlName: "Dao_User");

        try
        {
            var errors = new List<string>();

            foreach (int userId in userIds)
            {
                var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                    Model_Application_Variables.ConnectionString,
                    "sys_user_roles_Update",
                    new Dictionary<string, object>
                    {
                        ["UserID"] = userId,
                        ["NewRoleID"] = newRoleId,
                        ["AssignedBy"] = assignedBy
                    },
                    connection: connection,
                    transaction: transaction
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
                return Model_Dao_Result.Failure(errorMessage);
            }

            Service_DebugTracer.TraceMethodExit("Success", controlName: "Dao_User");
            return Model_Dao_Result.Success($"Updated role to {newRoleId} for {userIds.Count()} user(s) successfully");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);

            Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_User");
            return Model_Dao_Result.Failure("Error updating roles for multiple users", ex);
        }
    }

    internal static async Task<Model_Dao_Result> RemoveUserRoleAsync(int userId, int roleId,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["userId"] = userId, ["roleId"] = roleId }, controlName: "Dao_User");

        try
        {
            var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                Model_Application_Variables.ConnectionString,
                "sys_user_roles_Delete",
                new Dictionary<string, object>
                {
                    ["UserID"] = userId,
                    ["RoleID"] = roleId
                },
                connection: connection,
                transaction: transaction
            );

            Service_DebugTracer.TraceMethodExit(result, controlName: "Dao_User");

            if (result.IsSuccess)
            {
                return Model_Dao_Result.Success($"Role {roleId} removed from user {userId} successfully");
            }
            else
            {
                return Model_Dao_Result.Failure($"Failed to remove user role: {result.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);

            Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_User");
            return Model_Dao_Result.Failure($"Error removing role {roleId} from user {userId}", ex);
        }
    }

    #endregion

    #region Animations

    /// <summary>
    /// Gets whether animations are enabled for the specified user.
    /// </summary>
    /// <param name="user">The username.</param>
    /// <returns>A Model_Dao_Result containing true if enabled, false if disabled. Defaults to true.</returns>
    internal static async Task<Model_Dao_Result<bool>> GetAnimationsEnabledAsync(string user,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["user"] = user }, controlName: "Dao_User");

        try
        {
            string str = await GetSettingsJsonInternalAsync("AnimationsEnabled", user, connection, transaction);
            // Default to true if not set or invalid
            bool result = !bool.TryParse(str, out bool val) || val;

            Service_DebugTracer.TraceMethodExit(result, controlName: "Dao_User");
            return Model_Dao_Result<bool>.Success(result, $"Retrieved AnimationsEnabled for user {user}");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            Service_DebugTracer.TraceMethodExit(true, controlName: "Dao_User");
            return Model_Dao_Result<bool>.Failure($"Error retrieving AnimationsEnabled for user {user}", ex);
        }
    }

    /// <summary>
    /// Sets whether animations are enabled for the specified user.
    /// </summary>
    /// <param name="user">The username.</param>
    /// <param name="enabled">True to enable animations, false to disable.</param>
    /// <returns>A Model_Dao_Result indicating success or failure.</returns>
    internal static async Task<Model_Dao_Result> SetAnimationsEnabledAsync(string user, bool enabled,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["user"] = user, ["enabled"] = enabled }, controlName: "Dao_User");

        try
        {
            await SetUserSettingInternalAsync("AnimationsEnabled", user, enabled.ToString().ToLower(), connection, transaction);

            Service_DebugTracer.TraceMethodExit(controlName: "Dao_User");
            return Model_Dao_Result.Success($"Set AnimationsEnabled to {enabled} for user {user}");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_User");
            return Model_Dao_Result.Failure($"Error setting AnimationsEnabled for user {user}", ex);
        }
    }

    #endregion

    #region AutoExpandPanels

    /// <summary>
    /// Gets the AutoExpandPanels setting for the specified user.
    /// </summary>
    /// <param name="user">The username.</param>
    /// <returns>A Model_Dao_Result containing the AutoExpandPanels value (default true).</returns>
    internal static async Task<Model_Dao_Result<bool>> GetAutoExpandPanelsAsync(string user,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["user"] = user }, controlName: "Dao_User");

        try
        {
            string str = await GetSettingsJsonInternalAsync("AutoExpandPanels", user, connection, transaction);
            // Default to true if not set or invalid
            bool result = !bool.TryParse(str, out bool val) || val;

            Service_DebugTracer.TraceMethodExit(result, controlName: "Dao_User");
            return Model_Dao_Result<bool>.Success(result, $"Retrieved AutoExpandPanels for user {user}");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_User");
            return Model_Dao_Result<bool>.Failure($"Error retrieving AutoExpandPanels for user {user}", ex);
        }
    }

    /// <summary>
    /// Sets the AutoExpandPanels setting for the specified user.
    /// </summary>
    /// <param name="user">The username.</param>
    /// <param name="value">The value to set.</param>
    /// <returns>A Model_Dao_Result indicating success or failure.</returns>
    internal static async Task<Model_Dao_Result> SetAutoExpandPanelsAsync(string user, bool value,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["user"] = user, ["value"] = value }, controlName: "Dao_User");

        try
        {
            await SetUserSettingInternalAsync("AutoExpandPanels", user, value.ToString(), connection, transaction);

            Service_DebugTracer.TraceMethodExit(controlName: "Dao_User");
            return Model_Dao_Result.Success($"Set AutoExpandPanels to {value} for user {user}");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_User");
            return Model_Dao_Result.Failure($"Error setting AutoExpandPanels for user {user}", ex);
        }
    }

    #endregion

    #region ShowTotalSummaryPanel

    /// <summary>
    /// Gets the ShowTotalSummaryPanel setting for the specified user.
    /// </summary>
    /// <param name="user">The username.</param>
    /// <returns>A Model_Dao_Result containing the ShowTotalSummaryPanel value (default false).</returns>
    internal static async Task<Model_Dao_Result<bool>> GetShowTotalSummaryPanelAsync(string user,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["user"] = user }, controlName: "Dao_User");

        try
        {
            string str = await GetSettingsJsonInternalAsync("ShowTotalSummaryPanel", user, connection, transaction);
            // Default to false if not set or invalid
            bool result = bool.TryParse(str, out bool val) && val;

            Service_DebugTracer.TraceMethodExit(result, controlName: "Dao_User");
            return Model_Dao_Result<bool>.Success(result, $"Retrieved ShowTotalSummaryPanel for user {user}");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_User");
            return Model_Dao_Result<bool>.Failure($"Error retrieving ShowTotalSummaryPanel for user {user}", ex);
        }
    }

    /// <summary>
    /// Sets the ShowTotalSummaryPanel setting for the specified user.
    /// </summary>
    /// <param name="user">The username.</param>
    /// <param name="value">The value to set.</param>
    /// <returns>A Model_Dao_Result indicating success or failure.</returns>
    internal static async Task<Model_Dao_Result> SetShowTotalSummaryPanelAsync(string user, bool value,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["user"] = user, ["value"] = value }, controlName: "Dao_User");

        try
        {
            await SetUserSettingInternalAsync("ShowTotalSummaryPanel", user, value.ToString().ToLower(), connection, transaction);

            Service_DebugTracer.TraceMethodExit(controlName: "Dao_User");
            return Model_Dao_Result.Success($"Set ShowTotalSummaryPanel to {value} for user {user}");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_User");
            return Model_Dao_Result.Failure($"Error setting ShowTotalSummaryPanel for user {user}", ex);
        }
    }

    #endregion
}

#endregion
