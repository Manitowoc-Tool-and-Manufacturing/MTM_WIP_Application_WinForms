using System.Data;
using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Models;
using Newtonsoft.Json;

namespace MTM_WIP_Application_Winforms.Services.Analytics
{
    /// <summary>
    /// Implementation of IService_UserShiftLogic for user shift calculations.
    /// </summary>
    public class Service_UserShiftLogic : IService_UserShiftLogic
    {
        private readonly IDao_VisualAnalytics _daoVisualAnalytics;
        private readonly Visual.IService_VisualDatabase _serviceVisualDatabase;

        public Service_UserShiftLogic(
            IDao_VisualAnalytics daoVisualAnalytics,
            Visual.IService_VisualDatabase serviceVisualDatabase
        )
        {
            _daoVisualAnalytics = daoVisualAnalytics;
            _serviceVisualDatabase = serviceVisualDatabase;
        }

        /// <summary>
        /// Analyzes transaction history to calculate shift assignments for all users.
        /// </summary>
        public async Task<Model_Dao_Result<Dictionary<string, int>>> CalculateAllUserShiftsAsync(
            CancellationToken cancellationToken = default
        )
        {
            try
            {
                // 1. Get list of active users from last 30 days of transactions
                // Using Visual Service directly to query SQL Server
                var result = await _serviceVisualDatabase.GetUserShiftDataAsync(cancellationToken);

                if (!result.IsSuccess)
                {
                    return Model_Dao_Result<Dictionary<string, int>>.Failure(result.ErrorMessage);
                }

                var userShifts = new Dictionary<string, int>();
                var userTransactions = new Dictionary<string, List<DateTime>>();

                // Group transactions by user
                if (result.Data != null)
                {
                    foreach (DataRow row in result.Data.Rows)
                    {
                        string userId =
                            row["USER_ID"]?.ToString()?.Trim().ToUpperInvariant() ?? string.Empty;
                        if (string.IsNullOrEmpty(userId))
                            continue;

                        if (!userTransactions.ContainsKey(userId))
                        {
                            userTransactions[userId] = new List<DateTime>();
                        }

                        // Only keep last 50
                        if (userTransactions[userId].Count < 50)
                        {
                            DateTime transDate = Convert.ToDateTime(row["TRANSACTION_DATE"]);
                            userTransactions[userId].Add(transDate);
                        }
                    }
                }

                // Calculate shift for each user
                foreach (var kvp in userTransactions)
                {
                    string userId = kvp.Key;
                    List<DateTime> timestamps = kvp.Value;

                    if (timestamps.Count == 0)
                    {
                        userShifts[userId] = 0; // Unknown
                        continue;
                    }

                    // Count occurrences in each shift window
                    int shift1Count = 0; // 06:00 - 14:00
                    int shift2Count = 0; // 14:00 - 22:00
                    int shift3Count = 0; // 22:00 - 06:00
                    int weekendCount = 0; // Fri 06:00 - Mon 06:00 (Simplified: Sat/Sun)

                    foreach (var dt in timestamps)
                    {
                        // Check for weekend first (Saturday or Sunday)
                        if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday)
                        {
                            weekendCount++;
                            continue; // Count as weekend shift primarily
                        }

                        TimeSpan time = dt.TimeOfDay;

                        if (time >= new TimeSpan(6, 0, 0) && time < new TimeSpan(14, 0, 0))
                        {
                            shift1Count++;
                        }
                        else if (time >= new TimeSpan(14, 0, 0) && time < new TimeSpan(22, 0, 0))
                        {
                            shift2Count++;
                        }
                        else
                        {
                            // 22:00 - 06:00 (crosses midnight)
                            shift3Count++;
                        }
                    }

                    // Determine dominant shift
                    int maxCount = Math.Max(
                        Math.Max(shift1Count, shift2Count),
                        Math.Max(shift3Count, weekendCount)
                    );

                    if (maxCount == 0)
                    {
                        userShifts[userId] = 0;
                    }
                    else if (maxCount == weekendCount)
                    {
                        userShifts[userId] = 4; // Weekend
                    }
                    else if (maxCount == shift1Count)
                    {
                        userShifts[userId] = 1;
                    }
                    else if (maxCount == shift2Count)
                    {
                        userShifts[userId] = 2;
                    }
                    else
                    {
                        userShifts[userId] = 3;
                    }
                }

                return Model_Dao_Result<Dictionary<string, int>>.Success(userShifts);
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(
                    ex,
                    callerName: nameof(CalculateAllUserShiftsAsync)
                );
                return Model_Dao_Result<Dictionary<string, int>>.Failure(
                    "Failed to calculate user shifts.",
                    ex
                );
            }
        }

        /// <summary>
        /// Retrieves full names for all users from Infor Visual database.
        /// </summary>
        public async Task<Model_Dao_Result<Dictionary<string, string>>> FetchUserFullNamesAsync(
            CancellationToken cancellationToken = default
        )
        {
            try
            {
                // Query Visual EMPLOYEE table via Visual Service
                var result = await _serviceVisualDatabase.GetUserFullNamesAsync(cancellationToken);

                if (!result.IsSuccess)
                {
                    return Model_Dao_Result<Dictionary<string, string>>.Failure(
                        "Could not fetch users from EMPLOYEE table. " + result.ErrorMessage
                    );
                }

                var userNames = new Dictionary<string, string>();

                if (result.Data != null)
                {
                    foreach (DataRow row in result.Data.Rows)
                    {
                        string userId =
                            row["USER_ID"]?.ToString()?.Trim().ToUpperInvariant() ?? string.Empty;
                        if (string.IsNullOrEmpty(userId))
                            continue;

                        string firstName = row["FIRST_NAME"]?.ToString()?.Trim() ?? "";
                        string lastName = row["LAST_NAME"]?.ToString()?.Trim() ?? "";
                        string fullName = $"{firstName} {lastName}".Trim();

                        if (!string.IsNullOrEmpty(fullName))
                        {
                            userNames[userId] = fullName;
                        }
                    }
                }

                return Model_Dao_Result<Dictionary<string, string>>.Success(userNames);
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(
                    ex,
                    callerName: nameof(FetchUserFullNamesAsync)
                );
                return Model_Dao_Result<Dictionary<string, string>>.Failure(
                    "Failed to fetch user full names.",
                    ex
                );
            }
        }

        /// <summary>
        /// Persists shift and name data to sys_visual table.
        /// </summary>
        public async Task<Model_Dao_Result<bool>> SaveVisualMetadataAsync(
            Dictionary<string, int> shifts,
            Dictionary<string, string> names,
            CancellationToken cancellationToken = default
        )
        {
            try
            {
                string jsonShifts = JsonConvert.SerializeObject(shifts);
                string jsonNames = JsonConvert.SerializeObject(names);

                return await _daoVisualAnalytics.UpdateSysVisualDataAsync(jsonShifts, jsonNames);
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(
                    ex,
                    callerName: nameof(SaveVisualMetadataAsync)
                );
                return Model_Dao_Result<bool>.Failure("Failed to save visual metadata.", ex);
            }
        }
    }
}
