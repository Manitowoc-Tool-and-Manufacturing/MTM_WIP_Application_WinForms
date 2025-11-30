namespace MTM_WIP_Application_Winforms.Models;

/// <summary>
/// Provides caching and lookup functionality for stored procedure parameter prefixes.
/// Queries INFORMATION_SCHEMA.PARAMETERS at application startup to detect correct parameter prefixes (p_, in_, o_).
/// </summary>
/// <remarks>
/// This cache eliminates MySQL parameter errors by automatically detecting the correct prefix for each stored procedure.
/// The cache is populated once during application startup (Program.cs) with ~100-200ms overhead for 60+ stored procedures.
/// </remarks>
public static class Model_ParameterPrefix_Cache
{
    #region Fields

    /// <summary>
    /// Two-level dictionary: Outer key = stored procedure name, Inner key = parameter name (with prefix), Value = parameter mode (IN/OUT/INOUT)
    /// </summary>
    /// <example>
    /// Cache structure:
    /// {
    ///   "inv_inventory_get_all": {
    ///     "p_LocationCode": "IN",
    ///     "p_IncludeInactive": "IN",
    ///     "o_Status": "OUT",
    ///     "o_ErrorMsg": "OUT"
    ///   },
    ///   "inv_transaction_log": {
    ///     "in_TransactionType": "IN",
    ///     "in_PartID": "IN",
    ///     "in_FromLocation": "IN",
    ///     "in_ToLocation": "IN",
    ///     "in_Quantity": "IN",
    ///     "in_User": "IN",
    ///     "o_Status": "OUT",
    ///     "o_ErrorMsg": "OUT"
    ///   }
    /// }
    /// </example>
    private static Dictionary<string, Dictionary<string, string>> _cache = new();

    /// <summary>
    /// Parameter prefix overrides: Outer key = stored procedure name, Inner key = parameter name (WITHOUT prefix), Value = override prefix
    /// Takes precedence over INFORMATION_SCHEMA cache for gradual stored procedure standardization.
    /// Loaded from sys_parameter_prefix_overrides table at application startup.
    /// </summary>
    /// <example>
    /// Override structure:
    /// {
    ///   "legacy_transfer_procedure": {
    ///     "UserID": "in_",      // Override p_UserID → in_UserID
    ///     "PartNumber": ""      // Override p_PartNumber → PartNumber (no prefix)
    ///   }
    /// }
    /// </example>
    private static Dictionary<string, Dictionary<string, string>> _overrides = new();

    /// <summary>
    /// Thread-safe lock for cache initialization
    /// </summary>
    private static readonly object _lockObject = new();

    /// <summary>
    /// Indicates whether the cache has been initialized
    /// </summary>
    private static bool _isInitialized = false;

    /// <summary>
    /// Indicates whether the override cache has been loaded
    /// </summary>
    private static bool _overridesLoaded = false;

    #endregion

    #region Properties

    /// <summary>
    /// Gets whether the parameter prefix cache has been initialized
    /// </summary>
    public static bool IsInitialized => _isInitialized;

    /// <summary>
    /// Gets whether the parameter prefix overrides have been loaded
    /// </summary>
    public static bool OverridesLoaded => _overridesLoaded;

    /// <summary>
    /// Gets the number of stored procedures in the cache
    /// </summary>
    public static int ProcedureCount => _cache.Count;

    /// <summary>
    /// Gets the number of override entries loaded
    /// </summary>
    public static int OverrideCount
    {
        get
        {
            int count = 0;
            foreach (var proc in _overrides.Values)
            {
                count += proc.Count;
            }
            return count;
        }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Initializes the parameter prefix cache with data from INFORMATION_SCHEMA query results
    /// </summary>
    /// <param name="parameterData">Dictionary mapping procedure names to their parameter definitions</param>
    /// <remarks>
    /// This method should be called once during application startup (Program.cs) after querying INFORMATION_SCHEMA.PARAMETERS.
    /// Thread-safe initialization ensures cache is only populated once even if called multiple times.
    /// </remarks>
    public static void Initialize(Dictionary<string, Dictionary<string, string>> parameterData)
    {
        lock (_lockObject)
        {
            if (_isInitialized)
            {
                return; // Already initialized, skip
            }

            _cache = parameterData ?? new Dictionary<string, Dictionary<string, string>>();
            _isInitialized = true;
        }
    }

    /// <summary>
    /// Loads parameter prefix overrides from sys_parameter_prefix_overrides table.
    /// Called after Initialize() during application startup.
    /// </summary>
    /// <param name="overrides">List of override records from Dao_ParameterPrefixOverrides.GetAllActiveAsync()</param>
    /// <remarks>
    /// Overrides take precedence over INFORMATION_SCHEMA cache for parameter prefix resolution.
    /// This enables gradual stored procedure standardization without breaking existing DAO code.
    /// </remarks>
    public static void LoadOverrides(List<Model_ParameterPrefix_Override> overrides)
    {
        lock (_lockObject)
        {
            _overrides.Clear();

            foreach (var @override in overrides)
            {
                if (!_overrides.ContainsKey(@override.ProcedureName))
                {
                    _overrides[@override.ProcedureName] = new Dictionary<string, string>();
                }

                // Store parameter name WITHOUT prefix → override prefix mapping
                _overrides[@override.ProcedureName][@override.ParameterName] = @override.OverridePrefix;
            }

            _overridesLoaded = true;
        }
    }

    /// <summary>
    /// Reloads parameter prefix overrides from database (for runtime refresh after UI changes).
    /// </summary>
    /// <param name="overrides">Fresh list of override records</param>
    public static void ReloadOverrides(List<Model_ParameterPrefix_Override> overrides)
    {
        lock (_lockObject)
        {
            _overridesLoaded = false;
            LoadOverrides(overrides);
        }
    }

    /// <summary>
    /// Detects the correct parameter prefix for a given stored procedure and parameter name
    /// </summary>
    /// <param name="procedureName">The stored procedure name (e.g., "inv_inventory_get_all")</param>
    /// <param name="parameterName">The parameter name WITHOUT prefix (e.g., "LocationCode")</param>
    /// <returns>The detected prefix: "p_", "in_", or "o_". Defaults to "p_" if not found in cache.</returns>
    /// <remarks>
    /// Detection algorithm (PRIORITY ORDER):
    /// 1. **Check override cache first** - if override exists for procedure + parameter, use override prefix
    /// 2. Look up procedureName in INFORMATION_SCHEMA cache dictionary
    /// 3. If found, check if "p_" + parameterName exists → return "p_"
    /// 4. If not found, check if "in_" + parameterName exists → return "in_"
    /// 5. If not found, check if "o_" + parameterName exists → return "o_"
    /// 6. If procedure not in cache or parameter not found, use fallback convention:
    ///    - If procedure name contains "Transfer" or "transaction": return "in_"
    ///    - Otherwise: return "p_" (most common prefix)
    /// </remarks>
    /// <example>
    /// var prefix = GetParameterPrefix("inv_inventory_get_all", "LocationCode");
    /// // Returns "p_" because "p_LocationCode" exists in cache for this procedure
    ///
    /// var prefix2 = GetParameterPrefix("inv_transaction_log", "FromLocation");
    /// // Returns "in_" because "in_FromLocation" exists in cache for this procedure
    ///
    /// // With override:
    /// // Override: inv_transaction_log.FromLocation → "" (no prefix)
    /// var prefix3 = GetParameterPrefix("inv_transaction_log", "FromLocation");
    /// // Returns "" because override takes precedence over cache
    /// </example>
    public static string GetParameterPrefix(string procedureName, string parameterName)
    {
        // Standardize output parameter prefixes: all modern procedures expose p_Status/p_ErrorMsg
        // This avoids fallback heuristics incorrectly selecting in_/o_ prefixes for OUT parameters.
        if (string.Equals(parameterName, "Status", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(parameterName, "ErrorMsg", StringComparison.OrdinalIgnoreCase))
        {
            return "p_";
        }

        // PRIORITY 1: Check override cache first
        if (_overridesLoaded &&
            _overrides.TryGetValue(procedureName, out var procOverrides) &&
            procOverrides.TryGetValue(parameterName, out var overridePrefix))
        {
            return overridePrefix; // Override takes precedence
        }

        // PRIORITY 2: Check INFORMATION_SCHEMA cache
        if (!_isInitialized)
        {
            return ApplyFallbackConvention(procedureName);
        }

        // Look up procedure in cache
        if (_cache.TryGetValue(procedureName, out var parameters))
        {
            // Try common prefixes in order of frequency: p_ > in_ > o_
            string[] prefixes = { "p_", "in_", "o_" };

            foreach (var prefix in prefixes)
            {
                string fullParameterName = prefix + parameterName;
                if (parameters.ContainsKey(fullParameterName))
                {
                    return prefix;
                }
            }
        }

        // PRIORITY 3: Cache miss or parameter not found - use fallback convention
        return ApplyFallbackConvention(procedureName);
    }

    /// <summary>
    /// Gets all parameters for a given stored procedure from the cache
    /// </summary>
    /// <param name="procedureName">The stored procedure name</param>
    /// <returns>Dictionary of parameter names (with prefix) to their modes (IN/OUT/INOUT), or null if procedure not found</returns>
    public static Dictionary<string, string>? GetProcedureParameters(string procedureName)
    {
        if (!_isInitialized)
        {
            return null;
        }

        return _cache.TryGetValue(procedureName, out var parameters) ? parameters : null;
    }

    /// <summary>
    /// Clears the parameter prefix cache (primarily for testing purposes)
    /// </summary>
    public static void Clear()
    {
        lock (_lockObject)
        {
            _cache.Clear();
            _isInitialized = false;
        }
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Applies fallback convention when cache lookup fails
    /// </summary>
    /// <param name="procedureName">The stored procedure name</param>
    /// <returns>Conventional prefix based on procedure name patterns</returns>
    /// <remarks>
    /// Fallback rules:
    /// - If procedure name contains "Transfer" or "transaction": use "in_" prefix
    /// - Otherwise: use "p_" prefix (most common)
    /// </remarks>
    private static string ApplyFallbackConvention(string procedureName)
    {
        // Default to modern convention: all stored procedures use p_ prefix for parameters
        // Legacy procedures requiring alternative prefixes should define explicit overrides.
        return "p_";
    }

    #endregion
}
