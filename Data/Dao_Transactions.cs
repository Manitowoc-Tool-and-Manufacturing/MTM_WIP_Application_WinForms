using System.Text;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Services.Logging;
using System.Data;
using MySql.Data.MySqlClient;
using MTM_WIP_Application_Winforms.Services;

namespace MTM_WIP_Application_Winforms.Data;

/// <summary>
/// Data access object for transaction operations
/// </summary>
internal class Dao_Transactions
{
    #region Search Methods

    /// <summary>
    /// Asynchronously search for transactions based on criteria
    /// </summary>
    /// <param name="userName">User performing the search</param>
    /// <param name="isAdmin">Whether user has admin privileges</param>
    /// <param name="partID">Part ID filter</param>
    /// <param name="batchNumber">Batch number filter</param>
    /// <param name="fromLocation">From location filter</param>
    /// <param name="toLocation">To location filter</param>
    /// <param name="operation">Operation filter</param>
    /// <param name="transactionType">Transaction type filter</param>
    /// <param name="quantity">Quantity filter</param>
    /// <param name="notes">Notes filter</param>
    /// <param name="itemType">Item type filter</param>
    /// <param name="fromDate">From date filter</param>
    /// <param name="toDate">To date filter</param>
    /// <param name="sortColumn">Column to sort by</param>
    /// <param name="sortDescending">Sort direction</param>
    /// <param name="page">Page number for pagination</param>
    /// <param name="pageSize">Number of items per page</param>
    /// <returns>Model_Dao_Result containing list of transactions</returns>
    public async Task<Model_Dao_Result<List<Model_Transactions_Core>>> SearchTransactionsAsync(
        string userName,
        bool isAdmin,
        string partID = "",
        string batchNumber = "",
        string fromLocation = "",
        string toLocation = "",
        string operation = "",
        TransactionType? transactionType = null,
        int? quantity = null,
        string notes = "",
        string itemType = "",
        DateTime? fromDate = null,
        DateTime? toDate = null,
        string sortColumn = "ReceiveDate",
        bool sortDescending = true,
        int page = 1,
        int pageSize = 20,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null
    )
    {
        try
        {
            var parameters = new Dictionary<string, object>
            {
                ["UserName"] = userName ?? "",
                ["IsAdmin"] = isAdmin,
                ["PartID"] = partID ?? "",
                ["BatchNumber"] = batchNumber ?? "",
                ["FromLocation"] = fromLocation ?? "",
                ["ToLocation"] = toLocation ?? "",
                ["Operation"] = operation ?? "",
                ["TransactionType"] = transactionType?.ToString() ?? "",
                ["Quantity"] = quantity ?? (object)DBNull.Value,
                ["Notes"] = notes ?? "",
                ["ItemType"] = itemType ?? "",
                ["FromDate"] = fromDate ?? (object)DBNull.Value,
                ["ToDate"] = toDate ?? (object)DBNull.Value,
                ["SortColumn"] = sortColumn,
                ["SortDescending"] = sortDescending,
                ["Page"] = page,
                ["PageSize"] = pageSize
                // Note: TotalCount is an output parameter, handled by stored procedure
            };

            // Use Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync for proper status handling
            var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                Model_Application_Variables.ConnectionString,
                "inv_transactions_Search",
                parameters,
                progressHelper: null,
                connection: connection,
                transaction: transaction
            );

            if (!result.IsSuccess)
            {
                return Model_Dao_Result<List<Model_Transactions_Core>>.Failure(
                    result.ErrorMessage ?? "Failed to search transactions"
                );
            }

            var transactions = new List<Model_Transactions_Core>();
            if (result.Data != null)
            {
                foreach (DataRow row in result.Data.Rows)
                {
                    transactions.Add(MapTransactionFromDataRow(row));
                }
            }





            // Parse total count from status message (format: "Found X transaction(s) matching criteria")
            // The stored procedure returns the total count in the status message
            int totalCount = 0;
            if (!string.IsNullOrEmpty(result.StatusMessage) && result.StatusMessage.StartsWith("Found "))
            {
                var parts = result.StatusMessage.Split(' ');
                if (parts.Length >= 2 && int.TryParse(parts[1], out int parsedCount))
                {
                    totalCount = parsedCount;

                }
                else
                {

                }
            }

            // Fallback to transactions count if parsing failed
            if (totalCount == 0)
            {
                totalCount = transactions.Count;

            }



            return Model_Dao_Result<List<Model_Transactions_Core>>.Success(
                transactions,
                $"Retrieved {transactions.Count} transactions (page {page} of {Math.Ceiling((double)totalCount / pageSize)})",
                totalCount  // Pass total count in RowsAffected
            );
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, callerName: "SearchTransactionsAsync");
            return Model_Dao_Result<List<Model_Transactions_Core>>.Failure(
                "Failed to search transactions", ex
            );
        }
    }

    /// <summary>
    /// Synchronously search for transactions based on criteria (backward compatibility)
    /// </summary>
    /// <param name="userName">User performing the search</param>
    /// <param name="isAdmin">Whether user has admin privileges</param>
    /// <param name="partID">Part ID filter</param>
    /// <param name="batchNumber">Batch number filter</param>
    /// <param name="fromLocation">From location filter</param>
    /// <param name="toLocation">To location filter</param>
    /// <param name="operation">Operation filter</param>
    /// <param name="transactionType">Transaction type filter</param>
    /// <param name="quantity">Quantity filter</param>
    /// <param name="notes">Notes filter</param>
    /// <param name="itemType">Item type filter</param>
    /// <param name="fromDate">From date filter</param>
    /// <param name="toDate">To date filter</param>
    /// <param name="sortColumn">Column to sort by</param>
    /// <param name="sortDescending">Sort direction</param>
    /// <param name="page">Page number for pagination</param>
    /// <param name="pageSize">Number of items per page</param>
    /// <returns>Model_Dao_Result containing list of transactions</returns>
    public Model_Dao_Result<List<Model_Transactions_Core>> SearchTransactions(
        string userName,
        bool isAdmin,
        string partID = "",
        string batchNumber = "",
        string fromLocation = "",
        string toLocation = "",
        string operation = "",
        TransactionType? transactionType = null,
        int? quantity = null,
        string notes = "",
        string itemType = "",
        DateTime? fromDate = null,
        DateTime? toDate = null,
        string sortColumn = "ReceiveDate",
        bool sortDescending = true,
        int page = 1,
        int pageSize = 20
    )
    {
        try
        {
            var result = SearchTransactionsAsync(userName, isAdmin, partID, batchNumber, fromLocation,
                toLocation, operation, transactionType, quantity, notes, itemType, fromDate, toDate,
                sortColumn, sortDescending, page, pageSize).GetAwaiter().GetResult();

            return result;
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, callerName: "SearchTransactions");
            return Model_Dao_Result<List<Model_Transactions_Core>>.Failure(
                "Failed to search transactions (sync)", ex
            );
        }
    }

    /// <summary>
    /// Searches for transactions using a structured criteria object.
    /// Wrapper around SearchTransactionsAsync for cleaner API.
    /// </summary>
    /// <param name="criteria">Search criteria encapsulating all filter parameters</param>
    /// <param name="userName">User performing the search</param>
    /// <param name="isAdmin">Whether user has admin privileges</param>
    /// <param name="page">Page number for pagination (1-based)</param>
    /// <param name="pageSize">Number of records per page</param>
    /// <returns>Model_Dao_Result containing list of transactions matching criteria</returns>
    public async Task<Model_Dao_Result<List<Model_Transactions_Core>>> SearchAsync(
        Model_Transactions_SearchCriteria criteria,
        string userName,
        bool isAdmin,
        int page = 1,
        int pageSize = 50)
    {
        // CRITICAL DEBUGGING: Log method entry IMMEDIATELY
        var entryMessage = $"[Dao_Transactions.SearchAsync] ===== METHOD ENTRY ===== User: {userName}, IsAdmin: {isAdmin}, Page: {page}";

        Console.WriteLine(entryMessage);  // Also write to console
        System.Diagnostics.Debug.WriteLine(entryMessage);  // And debug output

        try
        {



            if (criteria == null)
            {

                return Model_Dao_Result<List<Model_Transactions_Core>>.Failure("Search criteria cannot be null");
            }

            // Parse transaction types
            List<TransactionType> selectedTypes = new List<TransactionType>();
            if (!string.IsNullOrWhiteSpace(criteria.TransactionType))
            {
                var typeStrings = criteria.TransactionType.Split(',');
                foreach (var ts in typeStrings)
                {
                    if (Enum.TryParse<TransactionType>(ts.Trim(), out var type))
                    {
                        selectedTypes.Add(type);
                    }
                }
            }

            // Determine if we need to use SmartSearch (for multi-select subset of types)
            // If 0 types selected (implies all?) or all 3 types selected, we can use standard search (empty string = all)
            // If 1 type selected, we can use standard search
            // If 2 types selected (subset), we MUST use SmartSearch
            
            // Assuming 3 total types (IN, OUT, TRANSFER). 
            // If selectedTypes.Count is 2, use SmartSearch.
            // Actually, let's be more robust. If it's a subset (count > 1 but not "all"), use SmartSearch.
            // But how do we know "all"? Let's assume if it contains comma, and we want to filter precisely, we use SmartSearch.
            // EXCEPT if it's ALL types, then we can just pass empty string to standard search.
            
            bool useSmartSearch = false;
            if (selectedTypes.Count > 1)
            {
                // Check if it's ALL types. 
                // If we assume IN, OUT, TRANSFER are the only ones.
                bool hasIn = selectedTypes.Contains(TransactionType.IN);
                bool hasOut = selectedTypes.Contains(TransactionType.OUT);
                bool hasTransfer = selectedTypes.Contains(TransactionType.TRANSFER);
                
                if (hasIn && hasOut && hasTransfer)
                {
                    // All types selected - standard search with empty type string works
                    useSmartSearch = false;
                }
                else
                {
                    // Subset selected (e.g. IN + TRANSFER) - must use SmartSearch
                    useSmartSearch = true;
                }
            }

            if (useSmartSearch)
            {
                // Map criteria to SmartSearch arguments
                var searchTerms = new Dictionary<string, string>();
                if (!string.IsNullOrWhiteSpace(criteria.PartID)) searchTerms["partid"] = criteria.PartID;
                if (!string.IsNullOrWhiteSpace(criteria.Operation)) searchTerms["operation"] = criteria.Operation;
                if (!string.IsNullOrWhiteSpace(criteria.Notes)) searchTerms["notes"] = criteria.Notes;
                if (!string.IsNullOrWhiteSpace(criteria.User)) searchTerms["user"] = criteria.User;
                if (!string.IsNullOrWhiteSpace(criteria.FromLocation)) searchTerms["fromlocation"] = criteria.FromLocation;
                if (!string.IsNullOrWhiteSpace(criteria.ToLocation)) searchTerms["tolocation"] = criteria.ToLocation;
                
                // Pass to SmartSearch
                return await SmartSearchAsync(
                    searchTerms,
                    selectedTypes,
                    (criteria.DateFrom, criteria.DateTo),
                    new List<string>(), // locations list (used for IN clause, not needed here as we use specific from/to)
                    userName,
                    isAdmin,
                    page,
                    pageSize
                ).ConfigureAwait(false);
            }

            // Fallback to standard search for single type or all types
            TransactionType? transactionType = null;
            string transactionTypeString = "";

            if (!string.IsNullOrWhiteSpace(criteria.TransactionType))
            {
                // If it contains comma, it's multiple types - pass empty to search all
                if (criteria.TransactionType.Contains(','))
                {

                    transactionTypeString = "";  // Empty string tells SP to match all types
                }
                else
                {
                    // Single type - try to parse it
                    if (Enum.TryParse<TransactionType>(criteria.TransactionType, out var type))
                    {
                        transactionType = type;
                        transactionTypeString = type.ToString();

                    }
                    else
                    {

                    }
                }
            }
            else
            {

            }












            var result = await SearchTransactionsAsync(
                userName: criteria.User ?? "",  // Pass empty string if no user filter, specific user if selected
                isAdmin: isAdmin,
                partID: criteria.PartID ?? "",
                batchNumber: "",  // Not in criteria model
                fromLocation: criteria.FromLocation ?? "",
                toLocation: criteria.ToLocation ?? "",
                operation: criteria.Operation ?? "",
                transactionType: transactionType,
                quantity: null,  // Not in criteria model
                notes: criteria.Notes ?? "",
                itemType: "",  // Not in criteria model
                fromDate: criteria.DateFrom,
                toDate: criteria.DateTo,
                sortColumn: "ReceiveDate",
                sortDescending: true,
                page: page,
                pageSize: pageSize
            ).ConfigureAwait(false);



            return result;
        }
        catch (Exception ex)
        {

            LoggingUtility.LogDatabaseError(ex);
            Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, callerName: "SearchAsync");
            return Model_Dao_Result<List<Model_Transactions_Core>>.Failure(
                "Failed to search transactions with criteria", ex
            );
        }
    }

    /// <summary>
    /// Retrieves the complete transaction lifecycle for a specific batch number.
    /// Returns all transactions chronologically ordered for lifecycle visualization.
    /// </summary>
    /// <param name="batchNumber">The batch number to retrieve lifecycle for.</param>
    /// <param name="connection">Optional MySqlConnection for transaction context.</param>
    /// <param name="transaction">Optional MySqlTransaction for transaction context.</param>
    /// <returns>Model_Dao_Result containing list of transactions in chronological order.</returns>
    /// <exception cref="ArgumentException">Thrown when batchNumber is null or empty.</exception>
    public async Task<Model_Dao_Result<List<Model_Transactions_Core>>> GetBatchLifecycleAsync(
        string batchNumber,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null
    )
    {
        try
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(batchNumber))
            {
                return Model_Dao_Result<List<Model_Transactions_Core>>.Failure(
                    "Batch number is required for lifecycle retrieval"
                );
            }



            var parameters = new Dictionary<string, object>
            {
                ["BatchNumber"] = batchNumber
            };

            // Call stored procedure
            var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                Model_Application_Variables.ConnectionString,
                "inv_transactions_GetBatchLifecycle",
                parameters,
                progressHelper: null,
                connection: connection,
                transaction: transaction
            );

            if (!result.IsSuccess)
            {
                return Model_Dao_Result<List<Model_Transactions_Core>>.Failure(
                    result.ErrorMessage ?? "Failed to retrieve batch lifecycle"
                );
            }

            var transactions = new List<Model_Transactions_Core>();
            if (result.Data != null)
            {
                foreach (DataRow row in result.Data.Rows)
                {
                    transactions.Add(MapTransactionFromDataRow(row));
                }
            }



            return Model_Dao_Result<List<Model_Transactions_Core>>.Success(
                transactions,
                result.StatusMessage ?? $"Retrieved {transactions.Count} transactions"
            );
        }
        catch (Exception ex)
        {

            LoggingUtility.LogDatabaseError(ex);
            Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, callerName: "GetBatchLifecycleAsync");
            return Model_Dao_Result<List<Model_Transactions_Core>>.Failure(
                "Failed to retrieve batch lifecycle", ex
            );
        }
    }

    #endregion

    #region Smart Search Methods

    /// <summary>
    /// Advanced smart search for transactions with intelligent parsing using string builder approach
    /// </summary>
    /// <param name="searchTerms">Parsed search terms from user input</param>
    /// <param name="transactionTypes">Selected transaction types filter</param>
    /// <param name="timeRange">Selected time range filter</param>
    /// <param name="locations">Selected locations filter</param>
    /// <param name="userName">Current user name</param>
    /// <param name="isAdmin">Whether user has admin privileges</param>
    /// <param name="page">Page number for pagination</param>
    /// <param name="pageSize">Items per page</param>
    /// <returns>Model_Dao_Result containing smart search results</returns>
    public async Task<Model_Dao_Result<List<Model_Transactions_Core>>> SmartSearchAsync(
        Dictionary<string, string> searchTerms,
        List<TransactionType> transactionTypes,
        (DateTime? from, DateTime? to) timeRange,
        List<string> locations,
        string userName,
        bool isAdmin,
        int page = 1,
        int pageSize = 20,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null
    )
    {
        try
        {
            // DEBUG: Show what the DAO received
            System.Diagnostics.Debug.WriteLine($"[DAO DEBUG] === DAO SmartSearchAsync Called ===");
            System.Diagnostics.Debug.WriteLine($"[DAO DEBUG] Received parameters:");
            System.Diagnostics.Debug.WriteLine($"[DAO DEBUG] - userName: '{userName}'");
            System.Diagnostics.Debug.WriteLine($"[DAO DEBUG] - isAdmin: {isAdmin}");
            System.Diagnostics.Debug.WriteLine($"[DAO DEBUG] - searchTerms count: {searchTerms.Count}");
            System.Diagnostics.Debug.WriteLine($"[DAO DEBUG] - page: {page}, pageSize: {pageSize}");

            // Build WHERE clause in application using StringBuilder for full control
            var whereBuilder = new StringBuilder("1=1");

            System.Diagnostics.Debug.WriteLine($"[DAO DEBUG] Starting WHERE clause build: '{whereBuilder}'");

            // User filtering logic - only apply if user is not admin AND no specific search terms
            bool hasSpecificSearchTerms = searchTerms.Count > 0;
            System.Diagnostics.Debug.WriteLine($"[DAO DEBUG] hasSpecificSearchTerms: {hasSpecificSearchTerms}");

            if (!isAdmin && !hasSpecificSearchTerms && !string.IsNullOrWhiteSpace(userName))
            {
                whereBuilder.Append($" AND User = '{userName.Replace("'", "''")}'");
                System.Diagnostics.Debug.WriteLine($"[DAO DEBUG] Added user filter - WHERE clause now: '{whereBuilder}'");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"[DAO DEBUG] No user filter applied - isAdmin: {isAdmin}, hasSpecificSearchTerms: {hasSpecificSearchTerms}, userName: '{userName}'");
            }

            // Add specific search term filters
            if (searchTerms.ContainsKey("partid") && !string.IsNullOrWhiteSpace(searchTerms["partid"]))
            {
                whereBuilder.Append($" AND PartID = '{searchTerms["partid"].Replace("'", "''")}'");
                System.Diagnostics.Debug.WriteLine($"[DAO DEBUG] Added partid filter - WHERE clause now: '{whereBuilder}'");
            }

            if (searchTerms.ContainsKey("batch") && !string.IsNullOrWhiteSpace(searchTerms["batch"]))
            {
                whereBuilder.Append($" AND BatchNumber = '{searchTerms["batch"].Replace("'", "''")}'");
                System.Diagnostics.Debug.WriteLine($"[DAO DEBUG] Added batch filter - WHERE clause now: '{whereBuilder}'");
            }

            if (searchTerms.ContainsKey("operation") && !string.IsNullOrWhiteSpace(searchTerms["operation"]))
            {
                whereBuilder.Append($" AND Operation = '{searchTerms["operation"].Replace("'", "''")}'");
                System.Diagnostics.Debug.WriteLine($"[DAO DEBUG] Added operation filter - WHERE clause now: '{whereBuilder}'");
            }

            if (searchTerms.ContainsKey("notes") && !string.IsNullOrWhiteSpace(searchTerms["notes"]))
            {
                whereBuilder.Append($" AND Notes LIKE '%{searchTerms["notes"].Replace("'", "''")}%'");
                System.Diagnostics.Debug.WriteLine($"[DAO DEBUG] Added notes filter - WHERE clause now: '{whereBuilder}'");
            }

            if (searchTerms.ContainsKey("user") && !string.IsNullOrWhiteSpace(searchTerms["user"]))
            {
                whereBuilder.Append($" AND User = '{searchTerms["user"].Replace("'", "''")}'");
                System.Diagnostics.Debug.WriteLine($"[DAO DEBUG] Added specific user filter - WHERE clause now: '{whereBuilder}'");
            }

            if (searchTerms.ContainsKey("itemtype") && !string.IsNullOrWhiteSpace(searchTerms["itemtype"]))
            {
                whereBuilder.Append($" AND ItemType = '{searchTerms["itemtype"].Replace("'", "''")}'");
                System.Diagnostics.Debug.WriteLine($"[DAO DEBUG] Added itemtype filter - WHERE clause now: '{whereBuilder}'");
            }

            if (searchTerms.ContainsKey("quantity") && int.TryParse(searchTerms["quantity"], out int qty))
            {
                whereBuilder.Append($" AND Quantity = {qty}");
                System.Diagnostics.Debug.WriteLine($"[DAO DEBUG] Added quantity filter - WHERE clause now: '{whereBuilder}'");
            }

            // NEW: Handle specific From/To location filters from searchTerms
            if (searchTerms.ContainsKey("fromlocation") && !string.IsNullOrWhiteSpace(searchTerms["fromlocation"]))
            {
                whereBuilder.Append($" AND FromLocation = '{searchTerms["fromlocation"].Replace("'", "''")}'");
                System.Diagnostics.Debug.WriteLine($"[DAO DEBUG] Added fromlocation filter - WHERE clause now: '{whereBuilder}'");
            }

            if (searchTerms.ContainsKey("tolocation") && !string.IsNullOrWhiteSpace(searchTerms["tolocation"]))
            {
                whereBuilder.Append($" AND ToLocation = '{searchTerms["tolocation"].Replace("'", "''")}'");
                System.Diagnostics.Debug.WriteLine($"[DAO DEBUG] Added tolocation filter - WHERE clause now: '{whereBuilder}'");
            }

            // Handle transaction types filter
            if (transactionTypes.Count > 0 && transactionTypes.Count < 3) // Only filter if not all types selected
            {
                var typeList = string.Join(",", transactionTypes.Select(t => $"'{t}'"));
                whereBuilder.Append($" AND TransactionType IN ({typeList})");
                System.Diagnostics.Debug.WriteLine($"[DAO DEBUG] Added transaction type filter - WHERE clause now: '{whereBuilder}'");
            }

            // Handle time range filter
            if (timeRange.from.HasValue)
            {
                whereBuilder.Append($" AND ReceiveDate >= '{timeRange.from.Value:yyyy-MM-dd HH:mm:ss}'");
                System.Diagnostics.Debug.WriteLine($"[DAO DEBUG] Added from date filter - WHERE clause now: '{whereBuilder}'");
            }

            if (timeRange.to.HasValue)
            {
                whereBuilder.Append($" AND ReceiveDate <= '{timeRange.to.Value:yyyy-MM-dd HH:mm:ss}'");
                System.Diagnostics.Debug.WriteLine($"[DAO DEBUG] Added to date filter - WHERE clause now: '{whereBuilder}'");
            }

            // Handle locations filter
            if (locations.Count > 0)
            {
                var locationList = string.Join(",", locations.Select(l => $"'{l.Replace("'", "''")}'"));
                whereBuilder.Append($" AND FromLocation IN ({locationList})");
                System.Diagnostics.Debug.WriteLine($"[DAO DEBUG] Added location filter - WHERE clause now: '{whereBuilder}'");
            }

            // Handle general search term (searches across multiple fields)
            if (searchTerms.ContainsKey("general") && !string.IsNullOrWhiteSpace(searchTerms["general"]))
            {
                var generalTerm = searchTerms["general"].Replace("'", "''");
                whereBuilder.Append($" AND (PartID LIKE '%{generalTerm}%' OR BatchNumber LIKE '%{generalTerm}%' OR Operation LIKE '%{generalTerm}%' OR Notes LIKE '%{generalTerm}%' OR User LIKE '%{generalTerm}%' OR ItemType LIKE '%{generalTerm}%' OR FromLocation LIKE '%{generalTerm}%' OR ToLocation LIKE '%{generalTerm}%')");
                System.Diagnostics.Debug.WriteLine($"[DAO DEBUG] Added general search filter - WHERE clause now: '{whereBuilder}'");
            }

            // Build parameters for the simplified stored procedure
            var parameters = new Dictionary<string, object>
            {
                ["WhereClause"] = whereBuilder.ToString(),
                ["Page"] = page,
                ["PageSize"] = pageSize
            };

            System.Diagnostics.Debug.WriteLine($"[DAO DEBUG] === FINAL WHERE CLAUSE ===");
            System.Diagnostics.Debug.WriteLine($"[DAO DEBUG] WhereClause parameter: '{whereBuilder}'");
            System.Diagnostics.Debug.WriteLine($"[DAO DEBUG] Full SQL will be: SELECT ... FROM inv_transaction WHERE {whereBuilder} ORDER BY ReceiveDate DESC LIMIT ...");
            System.Diagnostics.Debug.WriteLine($"[DAO DEBUG] === CALLING STORED PROCEDURE ===");

            // Use Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync for proper status handling
            var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                Model_Application_Variables.ConnectionString,
                "inv_transactions_SmartSearch",
                parameters,
                progressHelper: null,
                connection: connection,
                transaction: transaction
            );

            System.Diagnostics.Debug.WriteLine($"[DAO DEBUG] Stored procedure result: IsSuccess={result.IsSuccess}, ErrorMessage='{result.ErrorMessage}'");

            if (!result.IsSuccess)
            {
                return Model_Dao_Result<List<Model_Transactions_Core>>.Failure(
                    result.ErrorMessage ?? "Smart search failed"
                );
            }

            var transactions = new List<Model_Transactions_Core>();
            if (result.Data != null)
            {
                foreach (DataRow row in result.Data.Rows)
                {
                    transactions.Add(MapTransactionFromDataRow(row));
                }
            }

            System.Diagnostics.Debug.WriteLine($"[DAO DEBUG] Mapped {transactions.Count} transactions from result");




            // Get total count from the result's StatusMessage (format: "Found X transaction(s) matching criteria")
            int totalCount = result.RowsAffected;
            if (totalCount == 0 && !string.IsNullOrEmpty(result.StatusMessage) && result.StatusMessage.StartsWith("Found "))
            {
                var parts = result.StatusMessage.Split(' ');
                if (parts.Length >= 2 && int.TryParse(parts[1], out int parsedCount))
                {
                    totalCount = parsedCount;

                }
                else
                {

                }
            }
            if (totalCount == 0)
            {
                totalCount = transactions.Count; // Final fallback

            }


            System.Diagnostics.Debug.WriteLine($"[DAO DEBUG] === DAO SmartSearchAsync Complete ===");

            return Model_Dao_Result<List<Model_Transactions_Core>>.Success(
                transactions,
                $"Retrieved {transactions.Count} of {totalCount} transactions",
                totalCount  // Pass total count in RowsAffected
            );
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[DAO DEBUG] Exception in SmartSearchAsync: {ex.Message}");
            System.Diagnostics.Debug.WriteLine($"[DAO DEBUG] Stack trace: {ex.StackTrace}");

            LoggingUtility.LogDatabaseError(ex);
            Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, callerName: "SmartSearchAsync");
            return Model_Dao_Result<List<Model_Transactions_Core>>.Failure(
                "Smart search failed", ex
            );
        }
    }

    /// <summary>
    /// Get transaction analytics for dashboard display
    /// </summary>
    /// <param name="userName">Current user name</param>
    /// <param name="isAdmin">Whether user has admin privileges</param>
    /// <param name="timeRange">Time range for analytics</param>
    /// <returns>Model_Dao_Result containing analytics data</returns>
    public async Task<Model_Dao_Result<Dictionary<string, object>>> GetModel_Transactions_Core_AnalyticsAsync(
        string userName,
        bool isAdmin,
        (DateTime? from, DateTime? to) timeRange,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null
    )
    {
        try
        {
            var parameters = new Dictionary<string, object>
            {
                ["UserName"] = userName ?? "",
                ["IsAdmin"] = isAdmin,
                ["FromDate"] = timeRange.from ?? (object)DBNull.Value,
                ["ToDate"] = timeRange.to ?? (object)DBNull.Value
            };

            // Use Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync for proper status handling
            var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                Model_Application_Variables.ConnectionString,
                "inv_transactions_GetAnalytics",
                parameters,
                progressHelper: null,
                connection: connection,
                transaction: transaction
            );

            if (!result.IsSuccess)
            {
                return Model_Dao_Result<Dictionary<string, object>>.Failure(
                    result.ErrorMessage ?? "Failed to retrieve transaction analytics"
                );
            }

            var analytics = new Dictionary<string, object>();

            if (result.Data != null && result.Data.Rows.Count > 0)
            {
                var row = result.Data.Rows[0];
                analytics["TotalTransactions"] = Convert.ToInt32(row["TotalTransactions"]);
                analytics["InTransactions"] = Convert.ToInt32(row["InTransactions"]);
                analytics["OutTransactions"] = Convert.ToInt32(row["OutTransactions"]);
                analytics["TransferTransactions"] = Convert.ToInt32(row["TransferTransactions"]);
                analytics["TotalQuantity"] = Convert.ToInt64(row["TotalQuantity"]);
                analytics["UniquePartIds"] = Convert.ToInt32(row["UniquePartIds"]);
                analytics["ActiveUsers"] = Convert.ToInt32(row["ActiveUsers"]);
                analytics["TopPartId"] = row["TopPartId"]?.ToString() ?? "";
                analytics["TopUser"] = row["TopUser"]?.ToString() ?? "";
            }

            return Model_Dao_Result<Dictionary<string, object>>.Success(
                analytics,
                "Transaction analytics retrieved successfully"
            );
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, callerName: "GetModel_Transactions_Core_AnalyticsAsync");
            return Model_Dao_Result<Dictionary<string, object>>.Failure(
                "Failed to retrieve transaction analytics", ex
            );
        }
    }

    #endregion

    #region Analytics Methods

    /// <summary>
    /// Retrieves transaction analytics for the specified criteria.
    /// </summary>
    /// <param name="userName">Username for filtering (empty string for admin viewing all).</param>
    /// <param name="isAdmin">Whether the user has admin privileges.</param>
    /// <param name="dateFrom">Start date for analytics period.</param>
    /// <param name="dateTo">End date for analytics period.</param>
    /// <returns>Model_Dao_Result containing Model_Transactions_Core_Analytics with counts and percentages.</returns>
    public async Task<Model_Dao_Result<Model_Transactions_Core_Analytics>> GetAnalyticsAsync(
        string userName,
        bool isAdmin,
        DateTime? dateFrom,
        DateTime? dateTo,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        try
        {
            var parameters = new Dictionary<string, object>
            {
                ["UserName"] = userName ?? "",
                ["IsAdmin"] = isAdmin,
                ["FromDate"] = dateFrom ?? (object)DBNull.Value,
                ["ToDate"] = dateTo ?? (object)DBNull.Value
            };

            var result = await Helper_Database_StoredProcedure.ExecuteDataSetWithStatusAsync(
                Model_Application_Variables.ConnectionString,
                "inv_transactions_GetAnalytics",
                parameters,
                progressHelper: null,
                connection: connection,
                transaction: transaction
            ).ConfigureAwait(false);

            if (!result.IsSuccess)
            {

                return Model_Dao_Result<Model_Transactions_Core_Analytics>.Failure(
                    result.StatusMessage ?? "Failed to retrieve transaction analytics"
                );
            }

            if (result.Data == null)
            {

                return Model_Dao_Result<Model_Transactions_Core_Analytics>.Failure(
                    "No data returned from stored procedure"
                );
            }

            if (result.Data.Tables.Count < 10)
            {

                return Model_Dao_Result<Model_Transactions_Core_Analytics>.Failure(
                    $"Invalid analytics data: Expected 10 result sets but received {result.Data.Tables.Count}"
                );
            }

            // Extract data from all 10 result sets
            var analytics = new Model_Transactions_Core_Analytics
            {
                DateRange = (dateFrom, dateTo)
            };

            // Result Set 1: Transaction counts by type
            if (result.Data.Tables[0].Rows.Count > 0)
            {
                var row1 = result.Data.Tables[0].Rows[0];
                analytics.TotalTransactions = row1["TotalTransactions"] == DBNull.Value ? 0 : Convert.ToInt32(row1["TotalTransactions"]);
                analytics.TotalIN = row1["TotalIN"] == DBNull.Value ? 0 : Convert.ToInt32(row1["TotalIN"]);
                analytics.TotalOUT = row1["TotalOUT"] == DBNull.Value ? 0 : Convert.ToInt32(row1["TotalOUT"]);
                analytics.TotalTRANSFER = row1["TotalTRANSFER"] == DBNull.Value ? 0 : Convert.ToInt32(row1["TotalTRANSFER"]);
            }

            // Result Set 2: Quantity totals by type
            if (result.Data.Tables[1].Rows.Count > 0)
            {
                var row2 = result.Data.Tables[1].Rows[0];
                analytics.TotalQuantityMoved = row2["TotalQuantityMoved"] == DBNull.Value ? 0 : Convert.ToInt32(row2["TotalQuantityMoved"]);
                analytics.QuantityIN = row2["QuantityIN"] == DBNull.Value ? 0 : Convert.ToInt32(row2["QuantityIN"]);
                analytics.QuantityOUT = row2["QuantityOUT"] == DBNull.Value ? 0 : Convert.ToInt32(row2["QuantityOUT"]);
                analytics.QuantityTRANSFER = row2["QuantityTRANSFER"] == DBNull.Value ? 0 : Convert.ToInt32(row2["QuantityTRANSFER"]);
            }

            // Result Set 3: Date range
            if (result.Data.Tables[2].Rows.Count > 0)
            {
                var row3 = result.Data.Tables[2].Rows[0];
                analytics.FirstTransactionDate = row3["FirstTransactionDate"] == DBNull.Value ? null : Convert.ToDateTime(row3["FirstTransactionDate"]);
                analytics.LastTransactionDate = row3["LastTransactionDate"] == DBNull.Value ? null : Convert.ToDateTime(row3["LastTransactionDate"]);
            }

            // Result Set 4: Most active user
            if (result.Data.Tables[3].Rows.Count > 0)
            {
                var row4 = result.Data.Tables[3].Rows[0];
                analytics.MostActiveUser = (
                    row4["UserName"]?.ToString(),
                    row4["TransactionCount"] == DBNull.Value ? 0 : Convert.ToInt32(row4["TransactionCount"])
                );
            }
            else
            {
                analytics.MostActiveUser = (null, 0);
            }

            // Result Set 5: Most transacted part
            if (result.Data.Tables[4].Rows.Count > 0)
            {
                var row5 = result.Data.Tables[4].Rows[0];
                analytics.MostTransactedPart = (
                    row5["PartID"]?.ToString(),
                    row5["TransactionCount"] == DBNull.Value ? 0 : Convert.ToInt32(row5["TransactionCount"])
                );
            }
            else
            {
                analytics.MostTransactedPart = (null, 0);
            }

            // Result Set 6: Busiest location
            if (result.Data.Tables[5].Rows.Count > 0)
            {
                var row6 = result.Data.Tables[5].Rows[0];
                analytics.BusiestLocation = (
                    row6["LocationName"]?.ToString(),
                    row6["TransactionCount"] == DBNull.Value ? 0 : Convert.ToInt32(row6["TransactionCount"])
                );
            }
            else
            {
                analytics.BusiestLocation = (null, 0);
            }

            // Result Set 7: Most transferred part
            if (result.Data.Tables[6].Rows.Count > 0)
            {
                var row7 = result.Data.Tables[6].Rows[0];
                analytics.MostTransferredPart = (
                    row7["PartID"]?.ToString(),
                    row7["TransferCount"] == DBNull.Value ? 0 : Convert.ToInt32(row7["TransferCount"])
                );
            }
            else
            {
                analytics.MostTransferredPart = (null, 0);
            }

            // Result Set 8: Busiest day of week
            if (result.Data.Tables[7].Rows.Count > 0)
            {
                var row8 = result.Data.Tables[7].Rows[0];
                analytics.BusiestDay = (
                    row8["DayName"]?.ToString(),
                    row8["TransactionCount"] == DBNull.Value ? 0 : Convert.ToInt32(row8["TransactionCount"])
                );
            }
            else
            {
                analytics.BusiestDay = (null, 0);
            }

            // Result Set 9: Peak hour
            if (result.Data.Tables[8].Rows.Count > 0)
            {
                var row9 = result.Data.Tables[8].Rows[0];
                analytics.PeakHour = (
                    row9["Hour"] == DBNull.Value ? 0 : Convert.ToInt32(row9["Hour"]),
                    row9["TransactionCount"] == DBNull.Value ? 0 : Convert.ToInt32(row9["TransactionCount"])
                );
            }
            else
            {
                analytics.PeakHour = (0, 0);
            }

            // Result Set 10: Transaction rate
            if (result.Data.Tables[9].Rows.Count > 0)
            {
                var row10 = result.Data.Tables[9].Rows[0];
                analytics.TransactionRate = row10["TransactionRate"] == DBNull.Value ? 0.0 : Convert.ToDouble(row10["TransactionRate"]);
                analytics.TransactionRateTrend = row10["TransactionRateTrend"]?.ToString();
            }

            return Model_Dao_Result<Model_Transactions_Core_Analytics>.Success(
                analytics,
                analytics.TotalTransactions > 0
                    ? "Transaction analytics retrieved successfully"
                    : "No transaction data found for the specified period"
            );
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            return Model_Dao_Result<Model_Transactions_Core_Analytics>.Failure(
                "Exception occurred while retrieving transaction analytics",
                ex
            );
        }
    }

    /// <summary>
    /// Retrieves transaction analytics with additional detail fields (legacy method).
    #endregion

    #region Delete Methods

    /// <summary>
    /// Deletes a transaction record by ID (Admin/Developer only).
    /// </summary>
    /// <param name="transactionId">Transaction ID to delete</param>
    /// <returns>Model_Dao_Result indicating success or failure</returns>
    /// <remarks>
    /// SECURITY: This operation should only be called for users with Admin or Developer privileges.
    /// This permanently removes transaction history data from the database.
    /// </remarks>
    public async Task<Model_Dao_Result> DeleteTransactionByIdAsync(int transactionId)
    {
        try
        {


            var parameters = new Dictionary<string, object>
            {
                ["ID"] = transactionId
            };

            var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                Model_Application_Variables.ConnectionString,
                "inv_transaction_Delete_ByID",
                parameters,
                progressHelper: null
            );

            if (result.IsSuccess)
            {

                return Model_Dao_Result.Success($"Transaction ID {transactionId} deleted successfully");
            }
            else
            {

                return Model_Dao_Result.Failure($"Failed to delete transaction: {result.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            return Model_Dao_Result.Failure(
                $"Exception occurred while deleting transaction ID {transactionId}",
                ex
            );
        }
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Maps DataRow to Model_Transactions_Core object
    /// </summary>
    /// <param name="row">DataRow from stored procedure result</param>
    /// <returns>Mapped Model_Transactions_Core object</returns>
    private Model_Transactions_Core MapTransactionFromDataRow(DataRow row) =>
        new()
        {
            ID = Convert.ToInt32(row["ID"]),
            TransactionType =
                Enum.TryParse<TransactionType>(row["TransactionType"].ToString(), out TransactionType type)
                    ? type
                    : TransactionType.IN,
            BatchNumber = row["BatchNumber"] == DBNull.Value ? null : row["BatchNumber"].ToString(),
            PartID = row["PartID"] == DBNull.Value ? null : row["PartID"].ToString(),
            FromLocation = row["FromLocation"] == DBNull.Value ? null : row["FromLocation"].ToString(),
            ToLocation = row["ToLocation"] == DBNull.Value ? null : row["ToLocation"].ToString(),
            Operation = row["Operation"] == DBNull.Value ? null : row["Operation"].ToString(),
            Quantity = row["Quantity"] == DBNull.Value ? 0 : Convert.ToInt32(row["Quantity"]),
            Notes = row["Notes"] == DBNull.Value ? null : row["Notes"].ToString(),
            User = row["User"] == DBNull.Value ? null : row["User"].ToString(),
            ItemType = row["ItemType"] == DBNull.Value ? null : row["ItemType"].ToString(),
            DateTime = row["ReceiveDate"] == DBNull.Value
                ? DateTime.MinValue
                : Convert.ToDateTime(row["ReceiveDate"])
        };

    /// <summary>
    /// Maps a DataRow to Model_Transactions_Core object.
    /// Alias for MapTransactionFromDataRow for consistency with task naming conventions.
    /// </summary>
    /// <param name="row">DataRow from stored procedure result</param>
    /// <returns>Mapped Model_Transactions_Core object</returns>
    private Model_Transactions_Core MapDataRowToModel(DataRow row) => MapTransactionFromDataRow(row);

    #endregion
}
