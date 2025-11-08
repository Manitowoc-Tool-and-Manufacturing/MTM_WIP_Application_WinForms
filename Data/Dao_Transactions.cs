using System.Text;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Logging;
using System.Data;
using MySql.Data.MySqlClient;

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
    /// <returns>DaoResult containing list of transactions</returns>
    public async Task<DaoResult<List<Model_Transactions>>> SearchTransactionsAsync(
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
                Model_AppVariables.ConnectionString,
                "inv_transactions_Search",
                parameters,
                progressHelper: null,
                connection: connection,
                transaction: transaction
            );

            if (!result.IsSuccess)
            {
                return DaoResult<List<Model_Transactions>>.Failure(
                    result.ErrorMessage ?? "Failed to search transactions"
                );
            }

            var transactions = new List<Model_Transactions>();
            if (result.Data != null)
            {
                foreach (DataRow row in result.Data.Rows)
                {
                    transactions.Add(MapTransactionFromDataRow(row));
                }
            }

            LoggingUtility.Log($"[Dao_Transactions.SearchTransactionsAsync] Retrieved {transactions.Count} transactions from DB");
            LoggingUtility.Log($"[Dao_Transactions.SearchTransactionsAsync] result.StatusMessage: '{result.StatusMessage}'");
            LoggingUtility.Log($"[Dao_Transactions.SearchTransactionsAsync] result.RowsAffected: {result.RowsAffected}");

            // Parse total count from status message (format: "Found X transaction(s) matching criteria")
            // The stored procedure returns the total count in the status message
            int totalCount = 0;
            if (!string.IsNullOrEmpty(result.StatusMessage) && result.StatusMessage.StartsWith("Found "))
            {
                var parts = result.StatusMessage.Split(' ');
                if (parts.Length >= 2 && int.TryParse(parts[1], out int parsedCount))
                {
                    totalCount = parsedCount;
                    LoggingUtility.Log($"[Dao_Transactions.SearchTransactionsAsync] Parsed totalCount from StatusMessage: {totalCount}");
                }
                else
                {
                    LoggingUtility.Log($"[Dao_Transactions.SearchTransactionsAsync] Failed to parse totalCount from StatusMessage");
                }
            }
            
            // Fallback to transactions count if parsing failed
            if (totalCount == 0)
            {
                totalCount = transactions.Count;
                LoggingUtility.Log($"[Dao_Transactions.SearchTransactionsAsync] Using fallback totalCount: {totalCount}");
            }

            LoggingUtility.Log($"[Dao_Transactions.SearchTransactionsAsync] Final totalCount: {totalCount}, page: {page}, pageSize: {pageSize}");

            return DaoResult<List<Model_Transactions>>.Success(
                transactions,
                $"Retrieved {transactions.Count} transactions (page {page} of {Math.Ceiling((double)totalCount / pageSize)})",
                totalCount  // Pass total count in RowsAffected
            );
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: "SearchTransactionsAsync");
            return DaoResult<List<Model_Transactions>>.Failure(
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
    /// <returns>DaoResult containing list of transactions</returns>
    public DaoResult<List<Model_Transactions>> SearchTransactions(
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
            _ = Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: "SearchTransactions");
            return DaoResult<List<Model_Transactions>>.Failure(
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
    /// <returns>DaoResult containing list of transactions matching criteria</returns>
    public async Task<DaoResult<List<Model_Transactions>>> SearchAsync(
        TransactionSearchCriteria criteria,
        string userName,
        bool isAdmin,
        int page = 1,
        int pageSize = 50)
    {
        // CRITICAL DEBUGGING: Log method entry IMMEDIATELY
        var entryMessage = $"[Dao_Transactions.SearchAsync] ===== METHOD ENTRY ===== User: {userName}, IsAdmin: {isAdmin}, Page: {page}";
        LoggingUtility.Log(entryMessage);
        Console.WriteLine(entryMessage);  // Also write to console
        System.Diagnostics.Debug.WriteLine(entryMessage);  // And debug output
        
        try
        {
            LoggingUtility.Log($"[Dao_Transactions] SearchAsync called. User: {userName}, IsAdmin: {isAdmin}, Page: {page}, PageSize: {pageSize}");
            LoggingUtility.Log($"[Dao_Transactions] Criteria: {criteria}");

            if (criteria == null)
            {
                LoggingUtility.Log("[Dao_Transactions] ERROR: Criteria is null");
                return DaoResult<List<Model_Transactions>>.Failure("Search criteria cannot be null");
            }

            LoggingUtility.Log($"[Dao_Transactions] Criteria.TransactionType raw value: '{criteria.TransactionType}'");

            // ISSUE: The stored procedure inv_transactions_Search expects a single TransactionType value,
            // but criteria.TransactionType is a comma-separated string like "IN,OUT,TRANSFER".
            // When all types are selected, we should pass empty string to the SP to match all.
            // When specific types are selected, we need to call the SP multiple times OR use SmartSearch instead.
            
            // For now, if multiple transaction types are specified (contains comma), pass empty string to match all
            TransactionType? transactionType = null;
            string transactionTypeString = "";
            
            if (!string.IsNullOrWhiteSpace(criteria.TransactionType))
            {
                // If it contains comma, it's multiple types - pass empty to search all
                if (criteria.TransactionType.Contains(','))
                {
                    LoggingUtility.Log($"[Dao_Transactions] Multiple transaction types specified: '{criteria.TransactionType}'. Searching all types.");
                    transactionTypeString = "";  // Empty string tells SP to match all types
                }
                else
                {
                    // Single type - try to parse it
                    if (Enum.TryParse<TransactionType>(criteria.TransactionType, out var type))
                    {
                        transactionType = type;
                        transactionTypeString = type.ToString();
                        LoggingUtility.Log($"[Dao_Transactions] Single transaction type parsed: {transactionTypeString}");
                    }
                    else
                    {
                        LoggingUtility.Log($"[Dao_Transactions] WARNING: Failed to parse transaction type: '{criteria.TransactionType}'");
                    }
                }
            }
            else
            {
                LoggingUtility.Log("[Dao_Transactions] No transaction type specified, will search all");
            }

            LoggingUtility.Log($"[Dao_Transactions] Calling SearchTransactionsAsync with:");
            LoggingUtility.Log($"  - PartID: '{criteria.PartID ?? ""}'");
            LoggingUtility.Log($"  - User: '{criteria.User ?? ""}'");
            LoggingUtility.Log($"  - FromLocation: '{criteria.FromLocation ?? ""}'");
            LoggingUtility.Log($"  - ToLocation: '{criteria.ToLocation ?? ""}'");
            LoggingUtility.Log($"  - Operation: '{criteria.Operation ?? ""}'");
            LoggingUtility.Log($"  - TransactionType: '{transactionTypeString}' (parsed: {transactionType})");
            LoggingUtility.Log($"  - Notes: '{criteria.Notes ?? ""}'");
            LoggingUtility.Log($"  - DateFrom: {criteria.DateFrom}");
            LoggingUtility.Log($"  - DateTo: {criteria.DateTo}");

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

            LoggingUtility.Log($"[Dao_Transactions] SearchTransactionsAsync returned. Success: {result.IsSuccess}, Count: {result.Data?.Count ?? 0}");

            return result;
        }
        catch (Exception ex)
        {
            LoggingUtility.Log($"[Dao_Transactions] SearchAsync exception: {ex.Message}");
            LoggingUtility.LogDatabaseError(ex);
            await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: "SearchAsync");
            return DaoResult<List<Model_Transactions>>.Failure(
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
    /// <returns>DaoResult containing list of transactions in chronological order.</returns>
    /// <exception cref="ArgumentException">Thrown when batchNumber is null or empty.</exception>
    public async Task<DaoResult<List<Model_Transactions>>> GetBatchLifecycleAsync(
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
                return DaoResult<List<Model_Transactions>>.Failure(
                    "Batch number is required for lifecycle retrieval"
                );
            }

            LoggingUtility.Log($"[Dao_Transactions.GetBatchLifecycleAsync] Retrieving lifecycle for batch: {batchNumber}");

            var parameters = new Dictionary<string, object>
            {
                ["BatchNumber"] = batchNumber
            };

            // Call stored procedure
            var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                Model_AppVariables.ConnectionString,
                "inv_transactions_GetBatchLifecycle",
                parameters,
                progressHelper: null,
                connection: connection,
                transaction: transaction
            );

            if (!result.IsSuccess)
            {
                return DaoResult<List<Model_Transactions>>.Failure(
                    result.ErrorMessage ?? "Failed to retrieve batch lifecycle"
                );
            }

            var transactions = new List<Model_Transactions>();
            if (result.Data != null)
            {
                foreach (DataRow row in result.Data.Rows)
                {
                    transactions.Add(MapTransactionFromDataRow(row));
                }
            }

            LoggingUtility.Log($"[Dao_Transactions.GetBatchLifecycleAsync] Retrieved {transactions.Count} transactions for batch {batchNumber}");

            return DaoResult<List<Model_Transactions>>.Success(
                transactions,
                result.StatusMessage ?? $"Retrieved {transactions.Count} transactions"
            );
        }
        catch (Exception ex)
        {
            LoggingUtility.Log($"[Dao_Transactions] GetBatchLifecycleAsync exception: {ex.Message}");
            LoggingUtility.LogDatabaseError(ex);
            await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: "GetBatchLifecycleAsync");
            return DaoResult<List<Model_Transactions>>.Failure(
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
    /// <returns>DaoResult containing smart search results</returns>
    public async Task<DaoResult<List<Model_Transactions>>> SmartSearchAsync(
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
                Model_AppVariables.ConnectionString,
                "inv_transactions_SmartSearch",
                parameters,
                progressHelper: null,
                connection: connection,
                transaction: transaction
            );

            System.Diagnostics.Debug.WriteLine($"[DAO DEBUG] Stored procedure result: IsSuccess={result.IsSuccess}, ErrorMessage='{result.ErrorMessage}'");

            if (!result.IsSuccess)
            {
                return DaoResult<List<Model_Transactions>>.Failure(
                    result.ErrorMessage ?? "Smart search failed"
                );
            }

            var transactions = new List<Model_Transactions>();
            if (result.Data != null)
            {
                foreach (DataRow row in result.Data.Rows)
                {
                    transactions.Add(MapTransactionFromDataRow(row));
                }
            }

            System.Diagnostics.Debug.WriteLine($"[DAO DEBUG] Mapped {transactions.Count} transactions from result");
            LoggingUtility.Log($"[Dao_Transactions.SmartSearchAsync] Retrieved {transactions.Count} transactions from DB");
            LoggingUtility.Log($"[Dao_Transactions.SmartSearchAsync] result.StatusMessage: '{result.StatusMessage}'");
            LoggingUtility.Log($"[Dao_Transactions.SmartSearchAsync] result.RowsAffected: {result.RowsAffected}");

            // Get total count from the result's StatusMessage (format: "Found X transaction(s) matching criteria")
            int totalCount = result.RowsAffected;
            if (totalCount == 0 && !string.IsNullOrEmpty(result.StatusMessage) && result.StatusMessage.StartsWith("Found "))
            {
                var parts = result.StatusMessage.Split(' ');
                if (parts.Length >= 2 && int.TryParse(parts[1], out int parsedCount))
                {
                    totalCount = parsedCount;
                    LoggingUtility.Log($"[Dao_Transactions.SmartSearchAsync] Parsed totalCount from StatusMessage: {totalCount}");
                }
                else
                {
                    LoggingUtility.Log($"[Dao_Transactions.SmartSearchAsync] Failed to parse totalCount from StatusMessage");
                }
            }
            if (totalCount == 0)
            {
                totalCount = transactions.Count; // Final fallback
                LoggingUtility.Log($"[Dao_Transactions.SmartSearchAsync] Using fallback totalCount: {totalCount}");
            }

            LoggingUtility.Log($"[Dao_Transactions.SmartSearchAsync] Final totalCount: {totalCount}, page: {page}, pageSize: {pageSize}");
            System.Diagnostics.Debug.WriteLine($"[DAO DEBUG] === DAO SmartSearchAsync Complete ===");

            return DaoResult<List<Model_Transactions>>.Success(
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
            await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: "SmartSearchAsync");
            return DaoResult<List<Model_Transactions>>.Failure(
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
    /// <returns>DaoResult containing analytics data</returns>
    public async Task<DaoResult<Dictionary<string, object>>> GetTransactionAnalyticsAsync(
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
                Model_AppVariables.ConnectionString,
                "inv_transactions_GetAnalytics",
                parameters,
                progressHelper: null,
                connection: connection,
                transaction: transaction
            );

            if (!result.IsSuccess)
            {
                return DaoResult<Dictionary<string, object>>.Failure(
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

            return DaoResult<Dictionary<string, object>>.Success(
                analytics,
                "Transaction analytics retrieved successfully"
            );
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: "GetTransactionAnalyticsAsync");
            return DaoResult<Dictionary<string, object>>.Failure(
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
    /// <returns>DaoResult containing TransactionAnalytics with counts and percentages.</returns>
    public async Task<DaoResult<TransactionAnalytics>> GetAnalyticsAsync(
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
                Model_AppVariables.ConnectionString,
                "inv_transactions_GetAnalytics",
                parameters,
                progressHelper: null,
                connection: connection,
                transaction: transaction
            ).ConfigureAwait(false);

            if (!result.IsSuccess)
            {
                LoggingUtility.Log($"[Dao_Transactions.GetAnalyticsAsync] Error from SP: {result.StatusMessage}");
                return DaoResult<TransactionAnalytics>.Failure(
                    result.StatusMessage ?? "Failed to retrieve transaction analytics"
                );
            }

            if (result.Data == null)
            {
                LoggingUtility.Log("[Dao_Transactions.GetAnalyticsAsync] No data returned from stored procedure");
                return DaoResult<TransactionAnalytics>.Failure(
                    "No data returned from stored procedure"
                );
            }

            if (result.Data.Tables.Count < 10)
            {
                LoggingUtility.Log($"[Dao_Transactions.GetAnalyticsAsync] Expected 10 result sets but got {result.Data.Tables.Count}");
                return DaoResult<TransactionAnalytics>.Failure(
                    $"Invalid analytics data: Expected 10 result sets but received {result.Data.Tables.Count}"
                );
            }

            // Extract data from all 10 result sets
            var analytics = new TransactionAnalytics
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

            return DaoResult<TransactionAnalytics>.Success(
                analytics,
                analytics.TotalTransactions > 0 
                    ? "Transaction analytics retrieved successfully" 
                    : "No transaction data found for the specified period"
            );
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            return DaoResult<TransactionAnalytics>.Failure(
                "Exception occurred while retrieving transaction analytics",
                ex
            );
        }
    }

    /// <summary>
    /// Retrieves transaction analytics with additional detail fields (legacy method).
    #endregion

    #region Private Methods

    /// <summary>
    /// Maps DataRow to Model_Transactions object
    /// </summary>
    /// <param name="row">DataRow from stored procedure result</param>
    /// <returns>Mapped Model_Transactions object</returns>
    private Model_Transactions MapTransactionFromDataRow(DataRow row) =>
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
    /// Maps a DataRow to Model_Transactions object.
    /// Alias for MapTransactionFromDataRow for consistency with task naming conventions.
    /// </summary>
    /// <param name="row">DataRow from stored procedure result</param>
    /// <returns>Mapped Model_Transactions object</returns>
    private Model_Transactions MapDataRowToModel(DataRow row) => MapTransactionFromDataRow(row);

    #endregion
}
