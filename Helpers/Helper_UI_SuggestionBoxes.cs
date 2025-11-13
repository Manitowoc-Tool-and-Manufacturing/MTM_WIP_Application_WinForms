using System.Data;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Helpers
{
    #region Helper_UI_SuggestionBoxes

    /// <summary>
    /// Helper class for SuggestionTextBox validation and data management.
    /// Provides validation methods to check if user input matches valid master data.
    /// </summary>
    public static class Helper_UI_SuggestionBoxes
    {
        #region Private Variables - DataTables

        private static readonly DataTable PartIds_DataTable = new();
        private static readonly DataTable Operations_DataTable = new();
        private static readonly DataTable Locations_DataTable = new();

        #endregion

        #region Private Variables - Locks

        private static readonly object PartDataLock = new();
        private static readonly object OperationDataLock = new();
        private static readonly object LocationDataLock = new();

        #endregion

        #region DataTable Setup

        /// <summary>
        /// Loads part IDs from database into memory for validation.
        /// </summary>
        public static async Task LoadPartIdsAsync()
        {
            try
            {
                var dataResult = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                    Model_Application_Variables.ConnectionString,
                    "md_part_ids_Get_All",
                    null);

                lock (PartDataLock)
                {
                    PartIds_DataTable.Clear();
                    if (dataResult.IsSuccess && dataResult.Data != null)
                    {
                        PartIds_DataTable.Merge(dataResult.Data);
                    }
                }
            }
            catch (Exception ex)
            {
                lock (PartDataLock)
                {
                    PartIds_DataTable.Clear();
                }
                LoggingUtility.LogApplicationError(ex);
            }
        }

        /// <summary>
        /// Loads operations from database into memory for validation.
        /// </summary>
        public static async Task LoadOperationsAsync()
        {
            try
            {
                var dataResult = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                    Model_Application_Variables.ConnectionString,
                    "md_operation_numbers_Get_All",
                    null);

                lock (OperationDataLock)
                {
                    Operations_DataTable.Clear();
                    if (dataResult.IsSuccess && dataResult.Data != null)
                    {
                        Operations_DataTable.Merge(dataResult.Data);
                    }
                }
            }
            catch (Exception ex)
            {
                lock (OperationDataLock)
                {
                    Operations_DataTable.Clear();
                }
                LoggingUtility.LogApplicationError(ex);
            }
        }

        /// <summary>
        /// Loads locations from database into memory for validation.
        /// </summary>
        public static async Task LoadLocationsAsync()
        {
            try
            {
                var dataResult = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                    Model_Application_Variables.ConnectionString,
                    "md_locations_Get_All",
                    null);

                lock (LocationDataLock)
                {
                    Locations_DataTable.Clear();
                    if (dataResult.IsSuccess && dataResult.Data != null)
                    {
                        Locations_DataTable.Merge(dataResult.Data);
                    }
                }
            }
            catch (Exception ex)
            {
                lock (LocationDataLock)
                {
                    Locations_DataTable.Clear();
                }
                LoggingUtility.LogApplicationError(ex);
            }
        }

        /// <summary>
        /// Loads all master data tables for validation.
        /// </summary>
        public static async Task LoadAllDataAsync()
        {
            await Task.WhenAll(
                LoadPartIdsAsync(),
                LoadOperationsAsync(),
                LoadLocationsAsync()
            );
        }

        #endregion

        #region Validation Methods

        /// <summary>
        /// Validates if the provided part ID exists in the master data.
        /// </summary>
        /// <param name="partId">Part ID to validate</param>
        /// <returns>True if valid part ID, false otherwise</returns>
        public static bool IsValidPartId(string partId)
        {
            if (string.IsNullOrWhiteSpace(partId))
            {
                return false;
            }

            try
            {
                lock (PartDataLock)
                {
                    if (PartIds_DataTable.Rows.Count == 0)
                    {
                        // No data loaded yet - assume invalid
                        return false;
                    }

                    // Check if PartID column exists
                    if (!PartIds_DataTable.Columns.Contains("PartID"))
                    {
                        LoggingUtility.Log("[Helper_UI_SuggestionBoxes] PartID column not found in PartIds_DataTable");
                        return false;
                    }

                    // Search for exact match (case-insensitive)
                    foreach (DataRow row in PartIds_DataTable.Rows)
                    {
                        string? rowValue = row["PartID"]?.ToString();
                        if (!string.IsNullOrEmpty(rowValue) && 
                            rowValue.Equals(partId, StringComparison.OrdinalIgnoreCase))
                        {
                            return true;
                        }
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                return false;
            }
        }

        /// <summary>
        /// Validates if the provided operation exists in the master data.
        /// </summary>
        /// <param name="operation">Operation to validate</param>
        /// <returns>True if valid operation, false otherwise</returns>
        public static bool IsValidOperation(string operation)
        {
            if (string.IsNullOrWhiteSpace(operation))
            {
                return false;
            }

            try
            {
                lock (OperationDataLock)
                {
                    if (Operations_DataTable.Rows.Count == 0)
                    {
                        // No data loaded yet - assume invalid
                        return false;
                    }

                    // Check if OperationNumber column exists
                    if (!Operations_DataTable.Columns.Contains("OperationNumber"))
                    {
                        LoggingUtility.Log("[Helper_UI_SuggestionBoxes] OperationNumber column not found in Operations_DataTable");
                        return false;
                    }

                    // Search for exact match (case-insensitive)
                    foreach (DataRow row in Operations_DataTable.Rows)
                    {
                        string? rowValue = row["OperationNumber"]?.ToString();
                        if (!string.IsNullOrEmpty(rowValue) && 
                            rowValue.Equals(operation, StringComparison.OrdinalIgnoreCase))
                        {
                            return true;
                        }
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                return false;
            }
        }

        /// <summary>
        /// Validates if the provided location exists in the master data.
        /// </summary>
        /// <param name="location">Location to validate</param>
        /// <returns>True if valid location, false otherwise</returns>
        public static bool IsValidLocation(string location)
        {
            if (string.IsNullOrWhiteSpace(location))
            {
                return false;
            }

            try
            {
                lock (LocationDataLock)
                {
                    if (Locations_DataTable.Rows.Count == 0)
                    {
                        // No data loaded yet - assume invalid
                        return false;
                    }

                    // Check if Location column exists
                    if (!Locations_DataTable.Columns.Contains("Location"))
                    {
                        LoggingUtility.Log("[Helper_UI_SuggestionBoxes] Location column not found in Locations_DataTable");
                        return false;
                    }

                    // Search for exact match (case-insensitive)
                    foreach (DataRow row in Locations_DataTable.Rows)
                    {
                        string? rowValue = row["Location"]?.ToString();
                        if (!string.IsNullOrEmpty(rowValue) && 
                            rowValue.Equals(location, StringComparison.OrdinalIgnoreCase))
                        {
                            return true;
                        }
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                return false;
            }
        }

        #endregion
    }

    #endregion
}
