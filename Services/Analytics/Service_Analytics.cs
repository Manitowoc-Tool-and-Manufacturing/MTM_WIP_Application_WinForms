using System.Data;
using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Models;
using MySql.Data.MySqlClient;
using MTM_WIP_Application_Winforms.Services.Logging;
using Newtonsoft.Json;

namespace MTM_WIP_Application_Winforms.Services.Analytics
{
    /// <summary>
    /// Provides analytics and performance aggregation for transaction history.
    /// </summary>
    public class Service_Analytics
    {
        #region Fields
        private const string SqlTransactionsByRange = @"
            SELECT ID, TransactionType, BatchNumber, PartID, FromLocation, ToLocation,
                   Operation, Quantity, Notes, User, ItemType, ReceiveDate
            FROM inv_transaction
            WHERE ReceiveDate >= @Start AND ReceiveDate <= @End";

        private const string SqlUsers = "SELECT User, `Full Name`, Shift FROM usr_users";

        private const string SqlUserHistory = @"
            SELECT ID, TransactionType, BatchNumber, PartID, FromLocation, ToLocation,
                   Operation, Quantity, Notes, User, ItemType, ReceiveDate
            FROM inv_transaction
            WHERE User = @User AND ReceiveDate >= @Start AND ReceiveDate <= @End
            ORDER BY ReceiveDate DESC";

        private const string SqlAllUsers = "SELECT User FROM usr_users ORDER BY User";
        
        private readonly Dao_VisualAnalytics _visualDao = new Dao_VisualAnalytics();
        #endregion


        #region Methods
        /// <summary>
        /// Builds team performance statistics for the specified date range.
        /// </summary>
        /// <param name="start">Inclusive start date.</param>
        /// <param name="end">Inclusive end date.</param>
        /// <returns>
        /// Model_Dao_Result containing a list of <see cref="Model_User_Performance"/>.
        /// Check IsSuccess before accessing Data.
        /// </returns>
        public async Task<Model_Dao_Result<List<Model_User_Performance>>> GetTeamPerformanceAsync(DateTime start, DateTime end)
        {
            if (start > end)
            {
                return Model_Dao_Result<List<Model_User_Performance>>.Failure("Start date must be earlier than or equal to the end date.");
            }

            try
            {
                var transactions = new List<Model_Transactions_Core>();
                var usersTable = new DataTable();

                await using (var connection = new MySqlConnection(Model_Application_Variables.ConnectionString))
                {
                    await connection.OpenAsync().ConfigureAwait(false);

                    using (var cmd = new MySqlCommand(SqlTransactionsByRange, connection))
                    {
                        cmd.Parameters.AddWithValue("@Start", start);
                        cmd.Parameters.AddWithValue("@End", end);

                        using (var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            while (await reader.ReadAsync().ConfigureAwait(false))
                            {
                                transactions.Add(new Model_Transactions_Core
                                {
                                    ID = Convert.ToInt32(reader["ID"]),
                                    TransactionType = Enum.TryParse(reader["TransactionType"]?.ToString(), out TransactionType txType) ? txType : TransactionType.IN,
                                    BatchNumber = reader["BatchNumber"]?.ToString(),
                                    PartID = reader["PartID"]?.ToString(),
                                    FromLocation = reader["FromLocation"]?.ToString(),
                                    ToLocation = reader["ToLocation"]?.ToString(),
                                    Operation = reader["Operation"]?.ToString(),
                                    Quantity = Convert.ToInt32(reader["Quantity"]),
                                    Notes = reader["Notes"]?.ToString(),
                                    User = reader["User"]?.ToString(),
                                    ItemType = reader["ItemType"]?.ToString(),
                                    DateTime = Convert.ToDateTime(reader["ReceiveDate"])
                                });
                            }
                        }
                    }

                    using (var cmdUsers = new MySqlCommand(SqlUsers, connection))
                    using (var adapter = new MySqlDataAdapter(cmdUsers))
                    {
                        await Task.Run(() => adapter.Fill(usersTable)).ConfigureAwait(false);
                    }
                }

                // Fetch Visual Data for Shift Mapping
                var visualDataResult = await _visualDao.GetSysVisualDataAsync();
                Dictionary<string, int> visualShifts = new Dictionary<string, int>();
                if (visualDataResult.IsSuccess && !string.IsNullOrEmpty(visualDataResult.Data?.JsonShiftData))
                {
                    try 
                    {
                        visualShifts = JsonConvert.DeserializeObject<Dictionary<string, int>>(visualDataResult.Data.JsonShiftData) 
                                       ?? new Dictionary<string, int>();
                    }
                    catch (Exception ex)
                    {
                        LoggingUtility.LogApplicationError(ex);
                    }
                }

                var performanceList = new List<Model_User_Performance>();
                foreach (var group in transactions.Where(t => !string.IsNullOrWhiteSpace(t.User))
                                                  .GroupBy(t => t.User))
                {
                    string userKey = group.Key ?? "Unknown";
                    DataRow? userRow = FindUserRow(usersTable, userKey);
                    
                    // Determine Shift from Visual first, then fallback to WIP DB
                    string shift = "Unknown";
                    foreach (var visualUser in visualShifts.Keys)
                    {
                        if (IsUserMatch(visualUser, userKey))
                        {
                            shift = ConvertShiftCodeToString(visualShifts[visualUser]);
                            break;
                        }
                    }
                    
                    if (shift == "Unknown")
                    {
                        shift = userRow?["Shift"]?.ToString() ?? "Unknown";
                    }

                    string fullName = userRow?["Full Name"]?.ToString() ?? userKey;

                    var stats = AnalyzeUserTransactions(group.ToList(), shift);
                    stats.UserName = userKey;
                    stats.FullName = fullName;
                    stats.Shift = shift;

                    performanceList.Add(stats);
                }

                return Model_Dao_Result<List<Model_User_Performance>>.Success(
                    performanceList.OrderByDescending(x => x.TotalTransactions).ToList());
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                return Model_Dao_Result<List<Model_User_Performance>>.Failure(ex.Message);
            }
        }

        /// <summary>
        /// Retrieves transaction history for a single user.
        /// </summary>
        /// <param name="user">The user identifier to filter by.</param>
        /// <param name="start">Inclusive start date.</param>
        /// <param name="end">Inclusive end date.</param>
        /// <returns>
        /// Model_Dao_Result containing the user’s transactions.
        /// Check IsSuccess before accessing Data.
        /// </returns>
        public async Task<Model_Dao_Result<List<Model_Transactions_Core>>> GetUserHistoryAsync(string user, DateTime start, DateTime end)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(user);

            try
            {
                var transactions = new List<Model_Transactions_Core>();
                await using (var connection = new MySqlConnection(Model_Application_Variables.ConnectionString))
                {
                    await connection.OpenAsync().ConfigureAwait(false);

                    using (var cmd = new MySqlCommand(SqlUserHistory, connection))
                    {
                        cmd.Parameters.AddWithValue("@User", user);
                        cmd.Parameters.AddWithValue("@Start", start);
                        cmd.Parameters.AddWithValue("@End", end);

                        using (var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            while (await reader.ReadAsync().ConfigureAwait(false))
                            {
                                string txTypeStr = reader["TransactionType"]?.ToString() ?? nameof(TransactionType.IN);
                                _ = Enum.TryParse(txTypeStr, out TransactionType txType);

                                transactions.Add(new Model_Transactions_Core
                                {
                                    ID = Convert.ToInt32(reader["ID"]),
                                    TransactionType = txType,
                                    BatchNumber = reader["BatchNumber"]?.ToString(),
                                    PartID = reader["PartID"]?.ToString(),
                                    FromLocation = reader["FromLocation"]?.ToString(),
                                    ToLocation = reader["ToLocation"]?.ToString(),
                                    Operation = reader["Operation"]?.ToString(),
                                    Quantity = Convert.ToInt32(reader["Quantity"]),
                                    Notes = reader["Notes"]?.ToString(),
                                    User = reader["User"]?.ToString(),
                                    ItemType = reader["ItemType"]?.ToString(),
                                    DateTime = Convert.ToDateTime(reader["ReceiveDate"])
                                });
                            }
                        }
                    }
                }

                return Model_Dao_Result<List<Model_Transactions_Core>>.Success(transactions);
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                return Model_Dao_Result<List<Model_Transactions_Core>>.Failure(ex.Message);
            }
        }

        /// <summary>
        /// Retrieves all user names for selection lists.
        /// </summary>
        /// <returns>
        /// Model_Dao_Result containing user names.
        /// Check IsSuccess before accessing Data.
        /// </returns>
        public async Task<Model_Dao_Result<List<string>>> GetAllUserNamesAsync()
        {
            try
            {
                var users = new List<string>();
                await using (var connection = new MySqlConnection(Model_Application_Variables.ConnectionString))
                {
                    await connection.OpenAsync().ConfigureAwait(false);
                    using (var cmd = new MySqlCommand(SqlAllUsers, connection))
                    using (var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                    {
                        while (await reader.ReadAsync().ConfigureAwait(false))
                        {
                            var value = reader["User"]?.ToString();
                            if (!string.IsNullOrWhiteSpace(value))
                            {
                                users.Add(value);
                            }
                        }
                    }
                }

                return Model_Dao_Result<List<string>>.Success(users);
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                return Model_Dao_Result<List<string>>.Failure(ex.Message);
            }
        }
        #endregion

        #region Helpers
        private static DataRow? FindUserRow(DataTable usersTable, string userName)
        {
            foreach (DataRow row in usersTable.Rows)
            {
                if (row["User"]?.ToString()?.Equals(userName, StringComparison.OrdinalIgnoreCase) == true)
                {
                    return row;
                }
            }

            return null;
        }

        private bool IsUserMatch(string visualUser, string wipUser)
        {
            if (string.IsNullOrEmpty(visualUser) || visualUser.Length < 5) return false;
            
            char firstInitial = visualUser[0];
            string lastPart = visualUser.Substring(1); // First 4 of last name (assuming 5 chars total)
            
            // Check if WIP User starts with First Initial AND contains Last Part
            // Example: MSAMZ (Visual) vs MIKESAMZ (WIP)
            // M matches M
            // SAMZ is in MIKESAMZ
            
            return wipUser.StartsWith(firstInitial.ToString(), StringComparison.OrdinalIgnoreCase) &&
                   wipUser.IndexOf(lastPart, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private string ConvertShiftCodeToString(int shiftCode)
        {
            return shiftCode switch
            {
                1 => "First",
                2 => "Second",
                3 => "Third",
                4 => "Weekend",
                _ => "Unknown"
            };
        }

        /// <summary>
        /// Analyzes transaction activity for a single user and shift.
        /// </summary>
        private Model_User_Performance AnalyzeUserTransactions(List<Model_Transactions_Core> transactions, string shift)
        {
            var stats = new Model_User_Performance
            {
                TotalTransactions = transactions.Count,
                UniqueParts = transactions.Select(t => t.PartID).Distinct().Count(),
                TotalQuantity = transactions.Sum(t => t.Quantity)
            };

            // Sort by time for sequence analysis
            var sorted = transactions.OrderBy(t => t.DateTime).ToList();

            // 1. Rapid Fire (Potential blind entry or rushing)
            // Threshold: < 10 seconds between transactions AND Same Part
            // EXCLUDE: < 2 seconds (System generated bulk operations like Advanced Inventory or Bulk Remove)
            //          Also excludes "Lag Spike Bursts" where a user's buffered keystrokes (due to RDP/Cloud lag) 
            //          arrive at the server all at once, resulting in near-instantaneous DB timestamps.
            for (int i = 1; i < sorted.Count; i++)
            {
                var diff = sorted[i].DateTime - sorted[i - 1].DateTime;
                // Must be at least 2 seconds apart (human typing speed) but less than 10 seconds (rushing)
                if (diff.TotalSeconds >= 2.0 && diff.TotalSeconds < 10.0 && sorted[i].PartID == sorted[i - 1].PartID)
                {
                    stats.RapidFireCount++;
                }
            }

            // 2. Ping Pong (Moving A->B, then B->A shortly after)
            // Threshold: Same Part, Reverse Locations, within 20 mins
            for (int i = 0; i < sorted.Count; i++)
            {
                var t1 = sorted[i];
                // Look ahead
                for (int j = i + 1; j < sorted.Count; j++)
                {
                    var t2 = sorted[j];
                    if ((t2.DateTime - t1.DateTime).TotalMinutes > 20) break; // Stop looking if too far apart

                    if (t1.PartID == t2.PartID && 
                        t1.ToLocation == t2.FromLocation && 
                        t1.FromLocation == t2.ToLocation)
                    {
                        stats.PingPongCount++;
                        // Skip to avoid double counting pairs
                        break; 
                    }
                }
            }

            // 3. Shift Adherence (Transactions outside shift hours)
            // First: 6am-2pm, Second: 2pm-10pm, Third: 10pm-6am
            // Weekend: 6am-6pm (Fri-Mon)
            foreach (var t in sorted)
            {
                int hour = t.DateTime.Hour;
                DayOfWeek day = t.DateTime.DayOfWeek;
                bool isOutside = false;
                
                // Allow 1 hour buffer before/after shift
                switch (shift.ToLower())
                {
                    case "first":
                        // 06:00 - 14:00 (Buffer 05:00 - 15:00)
                        if (hour < 5 || hour >= 15) isOutside = true;
                        break;
                    case "second":
                        // 14:00 - 22:00 (Buffer 13:00 - 23:00)
                        if (hour < 13 || hour >= 23) isOutside = true;
                        break;
                    case "third":
                        // 22:00 - 06:00 (Buffer 21:00 - 07:00)
                        // Crosses midnight: Valid if hour >= 21 OR hour < 7
                        if (!(hour >= 21 || hour < 7)) isOutside = true;
                        break;
                    case "weekend":
                        // 06:00 - 18:00 (Buffer 05:00 - 19:00)
                        // Days: Fri, Sat, Sun, Mon
                        bool isWeekendDay = day == DayOfWeek.Friday || day == DayOfWeek.Saturday || 
                                          day == DayOfWeek.Sunday || day == DayOfWeek.Monday;
                        
                        if (!isWeekendDay) 
                        {
                            isOutside = true;
                        }
                        else
                        {
                            if (hour < 5 || hour >= 19) isOutside = true;
                        }
                        break;
                }
                if (isOutside) stats.OutsideShiftCount++;
            }

            return stats;
        }
        #endregion

    }

}
