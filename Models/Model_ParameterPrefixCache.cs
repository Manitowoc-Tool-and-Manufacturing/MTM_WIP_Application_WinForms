using System;
using System.Collections.Generic;

namespace MTM_Inventory_Application.Models;

/// <summary>
/// Provides caching and lookup functionality for stored procedure parameter prefixes.
/// Queries INFORMATION_SCHEMA.PARAMETERS at application startup to detect correct parameter prefixes (p_, in_, o_).
/// </summary>
/// <remarks>
/// This cache eliminates MySQL parameter errors by automatically detecting the correct prefix for each stored procedure.
/// The cache is populated once during application startup (Program.cs) with ~100-200ms overhead for 60+ stored procedures.
/// </remarks>
public static class Model_ParameterPrefixCache
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
    /// Thread-safe lock for cache initialization
    /// </summary>
    private static readonly object _lockObject = new();

    /// <summary>
    /// Indicates whether the cache has been initialized
    /// </summary>
    private static bool _isInitialized = false;

    #endregion

    #region Properties

    /// <summary>
    /// Gets whether the parameter prefix cache has been initialized
    /// </summary>
    public static bool IsInitialized => _isInitialized;

    /// <summary>
    /// Gets the number of stored procedures in the cache
    /// </summary>
    public static int ProcedureCount => _cache.Count;

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
    /// Detects the correct parameter prefix for a given stored procedure and parameter name
    /// </summary>
    /// <param name="procedureName">The stored procedure name (e.g., "inv_inventory_get_all")</param>
    /// <param name="parameterName">The parameter name WITHOUT prefix (e.g., "LocationCode")</param>
    /// <returns>The detected prefix: "p_", "in_", or "o_". Defaults to "p_" if not found in cache.</returns>
    /// <remarks>
    /// Detection algorithm:
    /// 1. Look up procedureName in cache dictionary
    /// 2. If found, check if "p_" + parameterName exists → return "p_"
    /// 3. If not found, check if "in_" + parameterName exists → return "in_"
    /// 4. If not found, check if "o_" + parameterName exists → return "o_"
    /// 5. If procedure not in cache or parameter not found, use fallback convention:
    ///    - If procedure name contains "Transfer" or "transaction": return "in_"
    ///    - Otherwise: return "p_" (most common prefix)
    /// </remarks>
    /// <example>
    /// var prefix = GetParameterPrefix("inv_inventory_get_all", "LocationCode");
    /// // Returns "p_" because "p_LocationCode" exists in cache for this procedure
    /// 
    /// var prefix2 = GetParameterPrefix("inv_transaction_log", "FromLocation");
    /// // Returns "in_" because "in_FromLocation" exists in cache for this procedure
    /// </example>
    public static string GetParameterPrefix(string procedureName, string parameterName)
    {
        // Check if cache has been initialized
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

        // Cache miss or parameter not found - use fallback convention
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
        // Transaction and transfer procedures typically use "in_" prefix
        if (procedureName.Contains("Transfer", StringComparison.OrdinalIgnoreCase) ||
            procedureName.Contains("transaction", StringComparison.OrdinalIgnoreCase))
        {
            return "in_";
        }

        // Default to "p_" prefix (most common)
        return "p_";
    }

    #endregion
}
