using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Logging;
using System.Data;

namespace MTM_WIP_Application_Winforms.Models;

/// <summary>
/// ViewModel for transaction viewer form, handling business logic and state management.
/// Implements passive ViewModel pattern (not full MVVM with data binding).
/// </summary>
internal sealed class TransactionViewModel
{
    #region Fields

    private readonly Dao_Transactions _dao;
    private TransactionSearchCriteria? _currentCriteria;
    private TransactionSearchResult? _currentResults;
    private List<string>? _cachedParts;
    private List<string>? _cachedUsers;
    private List<string>? _cachedLocations;

    #endregion

    #region Properties

    /// <summary>
    /// Gets the current search criteria used in the last query.
    /// </summary>
    public TransactionSearchCriteria? CurrentCriteria => _currentCriteria;

    /// <summary>
    /// Gets the current search results from the last query.
    /// </summary>
    public TransactionSearchResult? CurrentResults => _currentResults;

    /// <summary>
    /// Gets the page size for pagination.
    /// </summary>
    public int PageSize => Model_AppVariables.TransactionPageSize;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="TransactionViewModel"/> class.
    /// </summary>
    public TransactionViewModel()
    {
        _dao = new Dao_Transactions();
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Searches for transactions based on provided criteria.
    /// Updates CurrentCriteria and CurrentResults properties on success.
    /// </summary>
    /// <param name="criteria">Search criteria to apply</param>
    /// <param name="userName">Current user name</param>
    /// <param name="isAdmin">Whether current user has admin privileges</param>
    /// <param name="page">Page number for pagination (1-based)</param>
    /// <returns>DaoResult containing TransactionSearchResult with pagination metadata</returns>
    public async Task<DaoResult<TransactionSearchResult>> SearchTransactionsAsync(
        TransactionSearchCriteria criteria,
        string userName,
        bool isAdmin,
        int page = 1)
    {
        try
        {
            LoggingUtility.Log($"[TransactionViewModel] SearchTransactionsAsync starting. User: {userName}, IsAdmin: {isAdmin}, Page: {page}");
            LoggingUtility.Log($"[TransactionViewModel] Criteria: {criteria}");

            // Validate criteria
            if (criteria == null)
            {
                LoggingUtility.Log("[TransactionViewModel] ERROR: Criteria is null");
                return DaoResult<TransactionSearchResult>.Failure("Search criteria cannot be null");
            }

            if (!criteria.IsValid())
            {
                LoggingUtility.Log("[TransactionViewModel] ERROR: Criteria is invalid (no filters specified)");
                return DaoResult<TransactionSearchResult>.Failure("At least one search filter must be specified");
            }

            if (!criteria.IsDateRangeValid())
            {
                LoggingUtility.Log($"[TransactionViewModel] ERROR: Invalid date range. From: {criteria.DateFrom}, To: {criteria.DateTo}");
                return DaoResult<TransactionSearchResult>.Failure("Date range is invalid: From date must be before To date");
            }

            LoggingUtility.Log("[TransactionViewModel] Calling DAO SearchAsync...");

            // Execute search via DAO
            var daoResult = await _dao.SearchAsync(
                criteria,
                userName,
                isAdmin,
                page,
                PageSize
            ).ConfigureAwait(false);

            LoggingUtility.Log($"[TransactionViewModel] DAO returned. Success: {daoResult.IsSuccess}, DataCount: {daoResult.Data?.Count ?? 0}");

            if (!daoResult.IsSuccess)
            {
                LoggingUtility.Log($"[TransactionViewModel] DAO search failed: {daoResult.ErrorMessage}");
                return DaoResult<TransactionSearchResult>.Failure(daoResult.ErrorMessage, daoResult.Exception);
            }

            // Build search result with pagination metadata
            var searchResult = new TransactionSearchResult
            {
                Transactions = daoResult.Data ?? new List<Model_Transactions>(),
                TotalRecordCount = daoResult.RowsAffected > 0 ? daoResult.RowsAffected : (daoResult.Data?.Count ?? 0),  // Use RowsAffected for total count from DB
                CurrentPage = page,
                PageSize = PageSize
            };

            LoggingUtility.Log($"[TransactionViewModel] SearchResult created:");
            LoggingUtility.Log($"  - TotalRecordCount: {searchResult.TotalRecordCount}");
            LoggingUtility.Log($"  - CurrentPage: {searchResult.CurrentPage}");
            LoggingUtility.Log($"  - PageSize: {searchResult.PageSize}");
            LoggingUtility.Log($"  - TotalPages: {searchResult.TotalPages}");
            LoggingUtility.Log($"  - HasPreviousPage: {searchResult.HasPreviousPage}");
            LoggingUtility.Log($"  - HasNextPage: {searchResult.HasNextPage}");
            LoggingUtility.Log($"  - Transactions.Count: {searchResult.Transactions.Count}");

            // Update current state
            _currentCriteria = criteria;
            _currentResults = searchResult;

            return DaoResult<TransactionSearchResult>.Success(
                searchResult,
                $"Found {searchResult.TotalRecordCount} transactions"
            );
        }
        catch (Exception ex)
        {
            LoggingUtility.Log($"[TransactionViewModel] SearchTransactionsAsync failed. Criteria: {criteria?.ToString() ?? "null"}");
            LoggingUtility.LogApplicationError(ex);
            return DaoResult<TransactionSearchResult>.Failure(
                "Failed to search transactions",
                ex
            );
        }
    }

    /// <summary>
    /// Loads all part identifiers for dropdown population.
    /// Results are cached to improve performance on subsequent calls.
    /// </summary>
    /// <returns>DaoResult containing list of part IDs</returns>
    public async Task<DaoResult<List<string>>> LoadPartsAsync()
    {
        try
        {
            // Return cached data if available
            if (_cachedParts != null)
            {
                return DaoResult<List<string>>.Success(
                    _cachedParts,
                    $"Retrieved {_cachedParts.Count} parts from cache"
                );
            }

            // Load parts from database
            var result = await Dao_Part.GetAllPartsAsync().ConfigureAwait(false);

            if (!result.IsSuccess || result.Data == null)
            {
                return DaoResult<List<string>>.Failure(
                    result.ErrorMessage ?? "Failed to load parts",
                    result.Exception
                );
            }

            // Extract part IDs from DataTable
            var parts = new List<string>();
            foreach (System.Data.DataRow row in result.Data.Rows)
            {
                var partId = row["PartID"]?.ToString();
                if (!string.IsNullOrWhiteSpace(partId))
                {
                    parts.Add(partId);
                }
            }

            // Cache results
            _cachedParts = parts;

            return DaoResult<List<string>>.Success(
                parts,
                $"Loaded {parts.Count} parts"
            );
        }
        catch (Exception ex)
        {
            LoggingUtility.Log("[TransactionViewModel] LoadPartsAsync failed");
            LoggingUtility.LogApplicationError(ex);
            return DaoResult<List<string>>.Failure("Failed to load parts", ex);
        }
    }

    /// <summary>
    /// Loads all user names for dropdown population.
    /// Filters results based on admin privileges (admin sees all, regular users see only themselves).
    /// Results are cached to improve performance on subsequent calls.
    /// </summary>
    /// <param name="currentUser">Current logged-in user name</param>
    /// <param name="isAdmin">Whether current user has admin privileges</param>
    /// <returns>DaoResult containing list of user names</returns>
    public async Task<DaoResult<List<string>>> LoadUsersAsync(string currentUser, bool isAdmin)
    {
        try
        {
            // Return cached data if available
            if (_cachedUsers != null)
            {
                return DaoResult<List<string>>.Success(
                    _cachedUsers,
                    $"Retrieved {_cachedUsers.Count} users from cache"
                );
            }

            // Load users from database
            var result = await Dao_User.GetAllUsersAsync().ConfigureAwait(false);

            if (!result.IsSuccess || result.Data == null)
            {
                return DaoResult<List<string>>.Failure(
                    result.ErrorMessage ?? "Failed to load users",
                    result.Exception
                );
            }

            // Extract user names from DataTable
            var users = new List<string>();
            foreach (System.Data.DataRow row in result.Data.Rows)
            {
                var userName = row["User"]?.ToString();
                if (!string.IsNullOrWhiteSpace(userName))
                {
                    users.Add(userName);
                }
            }

            // Filter based on admin privileges
            List<string> filteredUsers;
            if (isAdmin)
            {
                // Admins see all users
                filteredUsers = users;
            }
            else
            {
                // Regular users see only themselves
                filteredUsers = users.Where(u =>
                    u.Equals(currentUser, StringComparison.OrdinalIgnoreCase)
                ).ToList();
            }

            // Cache results
            _cachedUsers = filteredUsers;

            return DaoResult<List<string>>.Success(
                filteredUsers,
                $"Loaded {filteredUsers.Count} users"
            );
        }
        catch (Exception ex)
        {
            LoggingUtility.Log($"[TransactionViewModel] LoadUsersAsync failed. CurrentUser: {currentUser}, IsAdmin: {isAdmin}");
            LoggingUtility.LogApplicationError(ex);
            return DaoResult<List<string>>.Failure("Failed to load users", ex);
        }
    }

    /// <summary>
    /// Loads all location codes for dropdown population.
    /// Results are cached to improve performance on subsequent calls.
    /// </summary>
    /// <returns>DaoResult containing list of location codes</returns>
    public async Task<DaoResult<List<string>>> LoadLocationsAsync()
    {
        try
        {
            LoggingUtility.Log("[TransactionViewModel] LoadLocationsAsync starting...");

            // Return cached data if available
            if (_cachedLocations != null)
            {
                LoggingUtility.Log($"[TransactionViewModel] Returning {_cachedLocations.Count} cached locations");
                return DaoResult<List<string>>.Success(
                    _cachedLocations,
                    $"Retrieved {_cachedLocations.Count} locations from cache"
                );
            }

            // Load locations from database
            LoggingUtility.Log("[TransactionViewModel] Calling Dao_Location.GetAllLocations...");
            var result = await Dao_Location.GetAllLocations().ConfigureAwait(false);

            LoggingUtility.Log($"[TransactionViewModel] DAO returned. Success: {result.IsSuccess}, HasData: {result.Data != null}");

            if (!result.IsSuccess || result.Data == null)
            {
                LoggingUtility.Log($"[TransactionViewModel] Failed to load locations: {result.ErrorMessage}");
                return DaoResult<List<string>>.Failure(
                    result.ErrorMessage ?? "Failed to load locations",
                    result.Exception
                );
            }

            LoggingUtility.Log($"[TransactionViewModel] DataTable has {result.Data.Rows.Count} rows, {result.Data.Columns.Count} columns");
            
            // Log column names to debug the issue
            var columnNames = new List<string>();
            foreach (System.Data.DataColumn col in result.Data.Columns)
            {
                columnNames.Add(col.ColumnName);
            }
            LoggingUtility.Log($"[TransactionViewModel] DataTable columns: {string.Join(", ", columnNames)}");

            // Extract location codes from DataTable
            var locations = new List<string>();
            try
            {
                foreach (System.Data.DataRow row in result.Data.Rows)
                {
                    var locationCode = row["Location"]?.ToString();
                    if (!string.IsNullOrWhiteSpace(locationCode))
                    {
                        locations.Add(locationCode);
                    }
                }
                
                LoggingUtility.Log($"[TransactionViewModel] Extracted {locations.Count} non-empty locations from DataTable");
            }
            catch (ArgumentException argEx)
            {
                LoggingUtility.Log($"[TransactionViewModel] ArgumentException extracting locations: {argEx.Message}");
                LoggingUtility.Log($"[TransactionViewModel] ArgumentException ParamName: {argEx.ParamName}");
                throw;
            }

            // Cache results
            _cachedLocations = locations;

            LoggingUtility.Log($"[TransactionViewModel] LoadLocationsAsync complete: {locations.Count} locations cached");

            return DaoResult<List<string>>.Success(
                locations,
                $"Loaded {locations.Count} locations from database"
            );
        }
        catch (Exception ex)
        {
            LoggingUtility.Log($"[TransactionViewModel] LoadLocationsAsync failed with exception: {ex.GetType().Name}");
            LoggingUtility.Log($"[TransactionViewModel] Exception message: {ex.Message}");
            LoggingUtility.LogApplicationError(ex);
            return DaoResult<List<string>>.Failure("Failed to load locations", ex);
        }
    }

    // Additional method implementations will be added in story-specific tasks:
    // - LoadRelatedTransactionsAsync (US-010, T060)
    // - PrintPreviewAsync (US-009, T055)

    /// <summary>
    /// Exports the current search results to an Excel file.
    /// </summary>
    /// <param name="filePath">The full path where the Excel file should be saved.</param>
    /// <param name="transactions">The list of transactions to export. If null, uses CurrentResults.</param>
    /// <returns>A DaoResult indicating success or failure with the file path.</returns>
    public async Task<DaoResult<string>> ExportToExcelAsync(string filePath, List<Model_Transactions>? transactions = null)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            return DaoResult<string>.Failure("File path is required for export");
        }

        try
        {
            var dataToExport = transactions ?? CurrentResults?.Transactions ?? new List<Model_Transactions>();
            
            if (dataToExport.Count == 0)
            {
                return DaoResult<string>.Failure("No transactions to export");
            }

            // Use Task.Run to avoid blocking UI thread during Excel generation
            await Task.Run(() =>
            {
                using var workbook = new ClosedXML.Excel.XLWorkbook();
                var worksheet = workbook.Worksheets.Add("Transactions");

                // Add headers
                worksheet.Cell(1, 1).Value = "ID";
                worksheet.Cell(1, 2).Value = "Type";
                worksheet.Cell(1, 3).Value = "Batch Number";
                worksheet.Cell(1, 4).Value = "Part Number";
                worksheet.Cell(1, 5).Value = "From Location";
                worksheet.Cell(1, 6).Value = "To Location";
                worksheet.Cell(1, 7).Value = "Operation";
                worksheet.Cell(1, 8).Value = "Quantity";
                worksheet.Cell(1, 9).Value = "User";
                worksheet.Cell(1, 10).Value = "Item Type";
                worksheet.Cell(1, 11).Value = "Date/Time";
                worksheet.Cell(1, 12).Value = "Notes";

                // Style headers
                var headerRange = worksheet.Range(1, 1, 1, 12);
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = ClosedXML.Excel.XLColor.LightGray;
                headerRange.Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;

                // Add data rows
                int row = 2;
                foreach (var transaction in dataToExport)
                {
                    worksheet.Cell(row, 1).Value = transaction.ID;
                    worksheet.Cell(row, 2).Value = transaction.TransactionType.ToString();
                    worksheet.Cell(row, 3).Value = transaction.BatchNumber ?? "";
                    worksheet.Cell(row, 4).Value = transaction.PartID ?? "";
                    worksheet.Cell(row, 5).Value = transaction.FromLocation ?? "";
                    worksheet.Cell(row, 6).Value = transaction.ToLocation ?? "";
                    worksheet.Cell(row, 7).Value = transaction.Operation ?? "";
                    worksheet.Cell(row, 8).Value = transaction.Quantity;
                    worksheet.Cell(row, 9).Value = transaction.User ?? "";
                    worksheet.Cell(row, 10).Value = transaction.ItemType ?? "";
                    worksheet.Cell(row, 11).Value = transaction.DateTime.ToString("yyyy-MM-dd HH:mm:ss");
                    worksheet.Cell(row, 12).Value = transaction.Notes ?? "";
                    row++;
                }

                // Auto-fit columns for readability
                worksheet.Columns().AdjustToContents();

                // Save workbook
                workbook.SaveAs(filePath);
            }).ConfigureAwait(false);

            LoggingUtility.Log($"[TransactionViewModel] Successfully exported {dataToExport.Count} transactions to {filePath}");
            return DaoResult<string>.Success(filePath, $"Exported {dataToExport.Count} transactions");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            return DaoResult<string>.Failure($"Failed to export transactions: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Retrieves analytics data for transactions within the specified date range.
    /// </summary>
    /// <param name="fromDate">Start date for analytics period.</param>
    /// <param name="toDate">End date for analytics period.</param>
    /// <returns>A DaoResult containing TransactionAnalytics or error details.</returns>
    public async Task<DaoResult<TransactionAnalytics>> GetAnalyticsAsync(DateTime fromDate, DateTime toDate)
    {
        if (fromDate > toDate)
        {
            return DaoResult<TransactionAnalytics>.Failure("Start date cannot be after end date");
        }

        try
        {
            var userName = Model_AppVariables.User;
            var isAdmin = Model_AppVariables.UserTypeAdmin;

            var result = await _dao.GetAnalyticsAsync(userName, isAdmin, fromDate, toDate);
            
            if (!result.IsSuccess || result.Data == null)
            {
                return DaoResult<TransactionAnalytics>.Failure(result.ErrorMessage ?? "Failed to retrieve analytics", result.Exception);
            }

            return DaoResult<TransactionAnalytics>.Success(result.Data, "Analytics retrieved successfully");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            return DaoResult<TransactionAnalytics>.Failure($"Failed to retrieve analytics: {ex.Message}", ex);
        }
    }

    #endregion

    #region Private Methods

    // Helper methods will be added as needed during implementation

    #endregion
}
