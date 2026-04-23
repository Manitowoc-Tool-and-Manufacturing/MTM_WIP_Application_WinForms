using System.Data;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Models.Analytics;

namespace MTM_WIP_Application_Winforms.Services.Analytics
{
    /// <summary>
    /// Builds role-aware analytics data from WIP transaction history.
    /// </summary>
    public class Service_Analytics
    {
        #region Fields
        private const int DATA_WINDOW_DAYS = 365;
        private const int DEFAULT_RANGE_DAYS = 30;
        #endregion

        #region Properties
        // Intentionally left blank.
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Service_Analytics"/> class.
        /// </summary>
        public Service_Analytics()
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Gets the analytics snapshot used by the HTML viewer.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token for the request.</param>
        /// <returns>
        /// Role-aware analytics snapshot.
        /// Check IsSuccess before accessing Data.
        /// </returns>
        public async Task<Model_Dao_Result<Model_AnalyticsSnapshot>> GetAnalyticsSnapshotAsync(
            CancellationToken cancellationToken = default
        )
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                DateTime dataWindowEnd = DateTime.Today;
                DateTime dataWindowStart = DateTime.Today.AddDays(-(DATA_WINDOW_DAYS - 1));
                DateTime defaultStartDate = DateTime.Today.AddDays(-(DEFAULT_RANGE_DAYS - 1));

                var transactionsResult = await LoadTransactionsAsync(dataWindowStart, dataWindowEnd);
                if (!transactionsResult.IsSuccess || transactionsResult.Data == null)
                {
                    return Model_Dao_Result<Model_AnalyticsSnapshot>.Failure(transactionsResult.ErrorMessage);
                }

                cancellationToken.ThrowIfCancellationRequested();

                var usersResult = await LoadUsersAsync();
                if (!usersResult.IsSuccess)
                {
                    return Model_Dao_Result<Model_AnalyticsSnapshot>.Failure(usersResult.ErrorMessage);
                }

                bool canViewTeam = Model_Application_Variables.UserTypeAdmin
                    || Model_Application_Variables.UserTypeDeveloper;

                string currentUserId = NormalizeUserId(Model_Application_Variables.User);
                Dictionary<string, string> displayNames = BuildDisplayNames(usersResult.Data);

                var filteredTransactions = transactionsResult.Data
                    .Where(transaction =>
                        canViewTeam
                        || string.Equals(
                            NormalizeUserId(transaction.User),
                            currentUserId,
                            StringComparison.OrdinalIgnoreCase
                        )
                    )
                    .OrderByDescending(transaction => transaction.DateTime)
                    .ToList();

                var users = filteredTransactions
                    .Select(transaction => NormalizeUserId(transaction.User))
                    .Where(userId => !string.IsNullOrWhiteSpace(userId))
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .Select(userId => new Model_AnalyticsUser
                    {
                        UserId = userId,
                        DisplayName = ResolveDisplayName(userId, displayNames),
                    })
                    .OrderBy(user => user.DisplayName, StringComparer.OrdinalIgnoreCase)
                    .ToList();

                if (!users.Any(user => string.Equals(user.UserId, currentUserId, StringComparison.OrdinalIgnoreCase)))
                {
                    users.Insert(
                        0,
                        new Model_AnalyticsUser
                        {
                            UserId = currentUserId,
                            DisplayName = ResolveDisplayName(currentUserId, displayNames),
                        }
                    );
                }

                var snapshot = new Model_AnalyticsSnapshot
                {
                    Title = canViewTeam ? "Analytics" : "My Analytics",
                    CurrentUserId = currentUserId,
                    CurrentUserDisplayName = ResolveDisplayName(currentUserId, displayNames),
                    CanViewTeam = canViewTeam,
                    GeneratedAt = DateTime.Now,
                    DataWindowStart = dataWindowStart,
                    DataWindowEnd = dataWindowEnd,
                    DefaultStartDate = defaultStartDate,
                    DefaultEndDate = dataWindowEnd,
                    Users = users,
                    Transactions = filteredTransactions
                        .Select(transaction => MapTransaction(transaction, displayNames))
                        .ToList(),
                    Glossary = BuildGlossary(),
                };

                return Model_Dao_Result<Model_AnalyticsSnapshot>.Success(snapshot);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                return Model_Dao_Result<Model_AnalyticsSnapshot>.Failure(
                    $"Failed to build analytics snapshot. {ex.Message}",
                    ex
                );
            }
        }
        #endregion

        #region Events
        // Intentionally left blank.
        #endregion

        #region Helpers
        private static List<Model_AnalyticsGlossaryItem> BuildGlossary()
        {
            return new List<Model_AnalyticsGlossaryItem>
            {
                new()
                {
                    Key = "Total Transactions",
                    Definition = "Every recorded inventory movement in the selected date range.",
                },
                new()
                {
                    Key = "Total Quantity Moved",
                    Definition = "The sum of quantities moved across the selected transactions.",
                },
                new()
                {
                    Key = "Unique Parts Handled",
                    Definition = "Distinct part numbers touched during the selected range.",
                },
                new()
                {
                    Key = "Active Days",
                    Definition = "Calendar days with at least one recorded transaction.",
                },
            };
        }

        private static Dictionary<string, string> BuildDisplayNames(DataTable? usersTable)
        {
            var displayNames = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            if (usersTable == null)
            {
                return displayNames;
            }

            foreach (DataRow row in usersTable.Rows)
            {
                string userId = NormalizeUserId(row["User"]?.ToString());
                if (string.IsNullOrWhiteSpace(userId))
                {
                    continue;
                }

                string fullName = row["Full Name"]?.ToString()?.Trim() ?? string.Empty;
                displayNames[userId] = string.IsNullOrWhiteSpace(fullName)
                    ? userId
                    : $"{fullName} ({userId})";
            }

            return displayNames;
        }

        private static string GetActivityLabel(Model_Transactions_Core transaction)
        {
            if (!string.IsNullOrWhiteSpace(transaction.Notes)
                && transaction.Notes.Contains("Excel Import", StringComparison.OrdinalIgnoreCase))
            {
                return "Advanced Inventory";
            }

            if (string.Equals(transaction.Operation, "AdvancedSearch", StringComparison.OrdinalIgnoreCase)
                || (!string.IsNullOrWhiteSpace(transaction.Notes)
                    && transaction.Notes.Contains("Advanced Remove", StringComparison.OrdinalIgnoreCase)))
            {
                return "Advanced Remove";
            }

            return transaction.TransactionType switch
            {
                TransactionType.IN => "Inventory",
                TransactionType.OUT => "Remove",
                TransactionType.TRANSFER => "Transfer",
                _ => transaction.TransactionType.ToString(),
            };
        }

        private async Task<Model_Dao_Result<List<Model_Transactions_Core>>> LoadTransactionsAsync(
            DateTime startDate,
            DateTime endDate
        )
        {
            var parameters = new Dictionary<string, object>
            {
                ["StartDate"] = startDate,
                ["EndDate"] = endDate.AddDays(1).AddTicks(-1),
            };

            var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                Model_Application_Variables.ConnectionString,
                "md_analytics_GetTransactionsByRange",
                parameters
            );

            if (!result.IsSuccess || result.Data == null)
            {
                return Model_Dao_Result<List<Model_Transactions_Core>>.Failure(result.ErrorMessage);
            }

            var transactions = new List<Model_Transactions_Core>();
            foreach (DataRow row in result.Data.Rows)
            {
                _ = Enum.TryParse(row["TransactionType"]?.ToString(), out TransactionType transactionType);

                transactions.Add(
                    new Model_Transactions_Core
                    {
                        ID = Convert.ToInt32(row["ID"]),
                        TransactionType = transactionType,
                        BatchNumber = row["BatchNumber"]?.ToString(),
                        PartID = row["PartID"]?.ToString(),
                        FromLocation = row["FromLocation"]?.ToString(),
                        ToLocation = row["ToLocation"]?.ToString(),
                        Operation = row["Operation"]?.ToString(),
                        Quantity = row["Quantity"] == DBNull.Value ? 0 : Convert.ToInt32(row["Quantity"]),
                        Notes = row["Notes"]?.ToString(),
                        User = row["User"]?.ToString(),
                        ItemType = row["ItemType"]?.ToString(),
                        DateTime = Convert.ToDateTime(row["ReceiveDate"]),
                    }
                );
            }

            return Model_Dao_Result<List<Model_Transactions_Core>>.Success(transactions);
        }

        private async Task<Model_Dao_Result<DataTable>> LoadUsersAsync()
        {
            return await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                Model_Application_Variables.ConnectionString,
                "md_analytics_GetUsers",
                null
            );
        }

        private static Model_AnalyticsTransaction MapTransaction(
            Model_Transactions_Core transaction,
            IReadOnlyDictionary<string, string> displayNames
        )
        {
            string userId = NormalizeUserId(transaction.User);

            return new Model_AnalyticsTransaction
            {
                TransactionId = transaction.ID,
                UserId = userId,
                UserDisplayName = ResolveDisplayName(userId, displayNames),
                ActivityType = GetActivityLabel(transaction),
                TransactionType = transaction.TransactionType.ToString(),
                PartId = transaction.PartID ?? string.Empty,
                Quantity = Math.Abs(transaction.Quantity),
                FromLocation = transaction.FromLocation,
                ToLocation = transaction.ToLocation,
                Operation = transaction.Operation,
                Notes = transaction.Notes,
                ItemType = transaction.ItemType,
                BatchNumber = transaction.BatchNumber,
                OccurredAt = transaction.DateTime,
            };
        }

        private static string NormalizeUserId(string? userId)
        {
            return (userId ?? string.Empty).Trim().ToUpperInvariant();
        }

        private static string ResolveDisplayName(
            string userId,
            IReadOnlyDictionary<string, string> displayNames
        )
        {
            return displayNames.TryGetValue(userId, out string? displayName)
                ? displayName
                : userId;
        }
        #endregion

        #region Cleanup / Dispose
        // Intentionally left blank.
        #endregion
    }
}
