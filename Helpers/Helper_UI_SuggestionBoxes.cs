using System.Data;
using MTM_WIP_Application_Winforms.Services.Logging;
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
        #region Helpers

        /// <summary>
        /// The functions provided are used to check if a DataTable has any specified columns and to retrieve
        /// the value of a specified column in a DataRow case-insensitively.
        /// </summary>
        /// <param name="DataTable">A DataTable represents a table of in-memory data. It consists of rows and
        /// columns, similar to a database table.</param>
        /// <returns>
        /// In the `HasAnyColumn` method, a boolean value is being returned to indicate whether any of the
        /// column names in the `DataTable` match any of the candidate names provided.
        /// </returns>
        private static bool HasAnyColumn(DataTable table, params string[] candidateNames)
        {
            foreach (DataColumn col in table.Columns)
            {
                foreach (var name in candidateNames)
                {
                    if (col.ColumnName.Equals(name, StringComparison.OrdinalIgnoreCase))
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// The function `GetValueCaseInsensitive` retrieves a value from a DataRow using a case-insensitive
        /// search based on the provided candidate column names.
        /// </summary>
        /// <param name="DataRow">A DataRow is a single row of data within a DataTable. It represents a record
        /// in a tabular format and contains values corresponding to columns in the DataTable.</param>
        private static string? GetValueCaseInsensitive(DataRow row, params string[] candidateNames)
        {
            foreach (DataColumn col in row.Table.Columns)
            {
                foreach (var name in candidateNames)
                {
                    if (col.ColumnName.Equals(name, StringComparison.OrdinalIgnoreCase))
                    {
                        return row[col]?.ToString();
                    }
                }
            }
            return null;
        }

        #endregion

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

                    // Accept common column name variants
                    var partColumns = new[] { "PartID", "PartId", "Part" };
                    if (!HasAnyColumn(PartIds_DataTable, partColumns))
                    {

                        return false;
                    }

                    // Search for exact match (case-insensitive)
                    foreach (DataRow row in PartIds_DataTable.Rows)
                    {
                        string? rowValue = GetValueCaseInsensitive(row, partColumns);
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

                    // Accept common column name variants (align with SP returns)
                    var opColumns = new[] { "OperationNumber", "Operation", "Op", "OperationNo" };
                    if (!HasAnyColumn(Operations_DataTable, opColumns))
                    {

                        return false;
                    }

                    // Search for exact match (case-insensitive)
                    foreach (DataRow row in Operations_DataTable.Rows)
                    {
                        string? rowValue = GetValueCaseInsensitive(row, opColumns);
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

                    // Accept common column name variants
                    var locColumns = new[] { "Location", "Loc" };
                    if (!HasAnyColumn(Locations_DataTable, locColumns))
                    {

                        return false;
                    }

                    // Search for exact match (case-insensitive)
                    foreach (DataRow row in Locations_DataTable.Rows)
                    {
                        string? rowValue = GetValueCaseInsensitive(row, locColumns);
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
