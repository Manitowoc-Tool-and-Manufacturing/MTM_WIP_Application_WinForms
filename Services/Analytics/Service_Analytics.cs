using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Models;
using MySql.Data.MySqlClient;
using MTM_WIP_Application_Winforms.Logging;

namespace MTM_WIP_Application_Winforms.Services.Analytics
{
    public class Service_Analytics
    {
        public Service_Analytics()
        {
        }

        public async Task<Model_Dao_Result<List<Model_User_Performance>>> GetTeamPerformanceAsync(DateTime start, DateTime end)
        {
            try
            {
                var transactions = new List<Model_Transactions_Core>();
                var usersTable = new DataTable();

                using (var connection = new MySqlConnection(Model_Application_Variables.ConnectionString))
                {
                    await connection.OpenAsync();

                    // 1. Get all transactions for the period using RAW SQL (Requested by user for this form only)
                    string sqlTransactions = @"
                        SELECT ID, TransactionType, BatchNumber, PartID, FromLocation, ToLocation, 
                               Operation, Quantity, Notes, User, ItemType, ReceiveDate
                        FROM inv_transaction
                        WHERE ReceiveDate >= @Start AND ReceiveDate <= @End";

                    using (var cmd = new MySqlCommand(sqlTransactions, connection))
                    {
                        cmd.Parameters.AddWithValue("@Start", start);
                        cmd.Parameters.AddWithValue("@End", end);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                transactions.Add(new Model_Transactions_Core
                                {
                                    ID = Convert.ToInt32(reader["ID"]),
                                    TransactionType = Enum.TryParse<TransactionType>(reader["TransactionType"]?.ToString(), out var txType) ? txType : TransactionType.IN,
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

                    // 2. Get all users to map shifts/names using RAW SQL
                    string sqlUsers = "SELECT User, `Full Name`, Shift FROM usr_users";
                    using (var cmdUsers = new MySqlCommand(sqlUsers, connection))
                    {
                        using (var adapter = new MySqlDataAdapter(cmdUsers))
                        {
                            await Task.Run(() => adapter.Fill(usersTable));
                        }
                    }
                }

                // 3. Group by User
                var userGroups = transactions
                    .GroupBy(t => t.User)
                    .Where(g => !string.IsNullOrEmpty(g.Key))
                    .ToList();

                var performanceList = new List<Model_User_Performance>();

                foreach (var group in userGroups)
                {
                    var user = group.Key ?? "Unknown";
                    
                    // Find user row manually since AsEnumerable might be tricky without reference
                    DataRow? userRow = null;
                    if (usersTable != null)
                    {
                        foreach (DataRow row in usersTable.Rows)
                        {
                            if (row["User"].ToString()?.Equals(user, StringComparison.OrdinalIgnoreCase) == true)
                            {
                                userRow = row;
                                break;
                            }
                        }
                    }

                    var shift = userRow?["Shift"]?.ToString() ?? "Unknown";
                    var fullName = userRow?["Full Name"]?.ToString() ?? user;

                    var stats = AnalyzeUserTransactions(group.ToList(), shift);
                    stats.UserName = user;
                    stats.FullName = fullName;
                    stats.Shift = shift;

                    performanceList.Add(stats);
                }

                return Model_Dao_Result<List<Model_User_Performance>>.Success(performanceList.OrderByDescending(x => x.TotalTransactions).ToList());
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                return Model_Dao_Result<List<Model_User_Performance>>.Failure(ex.Message);
            }
        }

        public async Task<Model_Dao_Result<List<Model_Transactions_Core>>> GetUserHistoryAsync(string user, DateTime start, DateTime end)
        {
            try
            {
                var transactions = new List<Model_Transactions_Core>();
                using (var connection = new MySqlConnection(Model_Application_Variables.ConnectionString))
                {
                    await connection.OpenAsync();
                    string sql = @"
                        SELECT ID, TransactionType, BatchNumber, PartID, FromLocation, ToLocation, 
                               Operation, Quantity, Notes, User, ItemType, ReceiveDate
                        FROM inv_transaction
                        WHERE User = @User AND ReceiveDate >= @Start AND ReceiveDate <= @End
                        ORDER BY ReceiveDate DESC";

                    using (var cmd = new MySqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@User", user);
                        cmd.Parameters.AddWithValue("@Start", start);
                        cmd.Parameters.AddWithValue("@End", end);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                string txTypeStr = reader["TransactionType"]?.ToString() ?? "IN";
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

        public async Task<Model_Dao_Result<List<string>>> GetAllUserNamesAsync()
        {
            try
            {
                var users = new List<string>();
                using (var connection = new MySqlConnection(Model_Application_Variables.ConnectionString))
                {
                    await connection.OpenAsync();
                    string sql = "SELECT User FROM usr_users ORDER BY User";
                    using (var cmd = new MySqlCommand(sql, connection))
                    {
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                users.Add(reader["User"].ToString() ?? "");
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
    }

    public class Model_User_Performance
    {
        public string UserName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Shift { get; set; } = string.Empty;
        public int TotalTransactions { get; set; }
        public long TotalQuantity { get; set; }
        public int UniqueParts { get; set; }
        
        // Quality Metrics
        public int RapidFireCount { get; set; } // < 10s gap
        public int PingPongCount { get; set; }  // A->B->A
        public int OutsideShiftCount { get; set; }
        
        public double QualityScore 
        { 
            get 
            {
                // Arbitrary scoring: Start at 100, deduct for flags
                // Normalized by volume (more transactions = more chance for flags, but ratio matters)
                if (TotalTransactions == 0) return 100;
                
                // OffShift is considered positive (helping out), so removed from penalty
                double penalty = (RapidFireCount * 0.5) + (PingPongCount * 5);
                double score = 100 - (penalty / TotalTransactions * 100);
                return Math.Max(0, Math.Min(100, score));
            } 
        }
    }
}
