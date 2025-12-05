using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Models.Analytics;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;
using MTM_WIP_Application_Winforms.Helpers;
using Newtonsoft.Json;

namespace MTM_WIP_Application_Winforms.Services.Analytics
{
    /// <summary>
    /// Implementation of IService_UserShiftLogic for user shift calculations.
    /// </summary>
    public class Service_UserShiftLogic : IService_UserShiftLogic
    {
        private readonly IDao_VisualAnalytics _daoVisualAnalytics;

        public Service_UserShiftLogic(IDao_VisualAnalytics daoVisualAnalytics)
        {
            _daoVisualAnalytics = daoVisualAnalytics;
        }

        /// <summary>
        /// Analyzes transaction history to calculate shift assignments for all users.
        /// </summary>
        public async Task<Model_Dao_Result<Dictionary<string, int>>> CalculateAllUserShiftsAsync()
        {
            try
            {
                // 1. Get list of active users from last 30 days of transactions
                // Using stored procedure md_visual_GetUserShiftData
                var connectionString = Helper_Database_Variables.GetConnectionString(null, null, null, null);
                var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                    connectionString,
                    "md_visual_GetUserShiftData",
                    null);

                if (!result.IsSuccess)
                {
                    return Model_Dao_Result<Dictionary<string, int>>.Failure(result.ErrorMessage);
                }

                var userShifts = new Dictionary<string, int>();
                var userTransactions = new Dictionary<string, List<DateTime>>();

                // Group transactions by user
                foreach (DataRow row in result.Data.Rows)
                {
                    string userId = row["USER_ID"]?.ToString()?.Trim().ToUpperInvariant() ?? string.Empty;
                    if (string.IsNullOrEmpty(userId)) continue;

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
                    int maxCount = Math.Max(Math.Max(shift1Count, shift2Count), Math.Max(shift3Count, weekendCount));
                    
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
                Service_ErrorHandler.HandleException(ex, callerName: nameof(CalculateAllUserShiftsAsync));
                return Model_Dao_Result<Dictionary<string, int>>.Failure("Failed to calculate user shifts.", ex);
            }
        }

        /// <summary>
        /// Retrieves full names for all users from Infor Visual database.
        /// </summary>
        public async Task<Model_Dao_Result<Dictionary<string, string>>> FetchUserFullNamesAsync()
        {
            try
            {
                // Query Visual EMPLOYEE table via stored procedure
                var connectionString = Helper_Database_Variables.GetConnectionString(null, null, null, null);
                var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                    connectionString,
                    "md_visual_GetUserFullNames",
                    null);
                
                if (!result.IsSuccess)
                {
                    return Model_Dao_Result<Dictionary<string, string>>.Failure("Could not fetch users from EMPLOYEE table. " + result.ErrorMessage);
                }

                var userNames = new Dictionary<string, string>();

                foreach (DataRow row in result.Data.Rows)
                {
                    string userId = row["USER_ID"]?.ToString()?.Trim().ToUpperInvariant() ?? string.Empty;
                    if (string.IsNullOrEmpty(userId)) continue;

                    string firstName = row["FIRST_NAME"]?.ToString()?.Trim() ?? "";
                    string lastName = row["LAST_NAME"]?.ToString()?.Trim() ?? "";
                    string fullName = $"{firstName} {lastName}".Trim();

                    if (!string.IsNullOrEmpty(fullName))
                    {
                        userNames[userId] = fullName;
                    }
                }

                return Model_Dao_Result<Dictionary<string, string>>.Success(userNames);
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, callerName: nameof(FetchUserFullNamesAsync));
                return Model_Dao_Result<Dictionary<string, string>>.Failure("Failed to fetch user full names.", ex);
            }
        }

        /// <summary>
        /// Persists shift and name data to sys_visual table.
        /// </summary>
        public async Task<Model_Dao_Result<bool>> SaveVisualMetadataAsync(
            Dictionary<string, int> shifts,
            Dictionary<string, string> names)
        {
            try
            {
                string jsonShifts = JsonConvert.SerializeObject(shifts);
                string jsonNames = JsonConvert.SerializeObject(names);

                return await _daoVisualAnalytics.UpdateSysVisualDataAsync(jsonShifts, jsonNames);
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, callerName: nameof(SaveVisualMetadataAsync));
                return Model_Dao_Result<bool>.Failure("Failed to save visual metadata.", ex);
            }
        }
        /// <summary>
        /// Calculates material handler scores with shift balancing and grading curve.
        /// </summary>
        public async Task<Model_Dao_Result<List<Model_Visual_MaterialHandlerScore>>> CalculateMaterialHandlerScoresAsync(DateTime startDate, DateTime endDate)
        {
            try
            {
                // 1. Get Raw Stats
                var statsResult = await _daoVisualAnalytics.GetMaterialHandlerStatsAsync(startDate, endDate);
                if (!statsResult.IsSuccess) return Model_Dao_Result<List<Model_Visual_MaterialHandlerScore>>.Failure(statsResult.ErrorMessage);

                // 2. Get Metadata (Shifts and Names)
                var metaResult = await _daoVisualAnalytics.GetSysVisualDataAsync();
                Dictionary<string, int> userShifts = new Dictionary<string, int>();
                Dictionary<string, string> userNames = new Dictionary<string, string>();

                if (metaResult.IsSuccess)
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(metaResult.Data.JsonShiftData))
                            userShifts = JsonConvert.DeserializeObject<Dictionary<string, int>>(metaResult.Data.JsonShiftData) ?? new Dictionary<string, int>();
                        
                        if (!string.IsNullOrEmpty(metaResult.Data.JsonUserFullNames))
                            userNames = JsonConvert.DeserializeObject<Dictionary<string, string>>(metaResult.Data.JsonUserFullNames) ?? new Dictionary<string, string>();
                    }
                    catch { /* Ignore JSON errors */ }
                }

                // 3. Process Raw Data into User Objects
                var userScores = new Dictionary<string, Model_Visual_MaterialHandlerScore>();
                var shiftTotalScores = new Dictionary<int, double> { { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 0, 0 } };

                foreach (DataRow row in statsResult.Data.Rows)
                {
                    string user = row["User"]?.ToString()?.Trim().ToUpperInvariant() ?? "UNKNOWN";
                    string type = row["TransactionType"]?.ToString() ?? "";
                    int count = Convert.ToInt32(row["TransactionCount"]);

                    if (!userScores.ContainsKey(user))
                    {
                        int shift = userShifts.ContainsKey(user) ? userShifts[user] : 0;
                        string fullName = userNames.ContainsKey(user) ? userNames[user] : user;

                        userScores[user] = new Model_Visual_MaterialHandlerScore
                        {
                            UserName = user,
                            FullName = fullName,
                            Shift = shift
                        };
                    }

                    var scoreModel = userScores[user];
                    if (!scoreModel.TransactionCounts.ContainsKey(type))
                        scoreModel.TransactionCounts[type] = 0;
                    
                    scoreModel.TransactionCounts[type] += count;
                    scoreModel.TotalTransactions += count;

                    // Scoring Logic
                    // Receive/Pick = 2 points, Others = 1 point
                    double points = (type.Equals("Receive", StringComparison.OrdinalIgnoreCase) || 
                                     type.Equals("Pick", StringComparison.OrdinalIgnoreCase)) ? 2.0 : 1.0;
                    
                    double totalPoints = points * count;
                    scoreModel.TotalScore += totalPoints;
                    
                    // Add to shift total
                    if (shiftTotalScores.ContainsKey(scoreModel.Shift))
                        shiftTotalScores[scoreModel.Shift] += totalPoints;
                    else
                        shiftTotalScores[0] += totalPoints;
                }

                // 4. Calculate Shift Volume Factors
                // Find max shift score (excluding shift 0/Unknown if possible, or include it?)
                // Usually we compare Shift 1, 2, 3. Weekend (4) might be separate or included.
                // Let's consider 1, 2, 3 for balancing.
                double maxShiftScore = Math.Max(Math.Max(shiftTotalScores[1], shiftTotalScores[2]), shiftTotalScores[3]);
                
                // Avoid division by zero
                if (maxShiftScore == 0) maxShiftScore = 1;

                var shiftFactors = new Dictionary<int, double>();
                foreach (var kvp in shiftTotalScores)
                {
                    int shift = kvp.Key;
                    double score = kvp.Value;
                    
                    if (shift == 0) 
                    {
                        shiftFactors[shift] = 1.0; // No adjustment for unknown
                        continue;
                    }

                    if (score > 0)
                        shiftFactors[shift] = maxShiftScore / score;
                    else
                        shiftFactors[shift] = 1.0;
                }

                // 5. Apply Factors and Calculate Adjusted Score
                var finalList = userScores.Values.ToList();
                foreach (var item in finalList)
                {
                    item.ShiftVolumeFactor = shiftFactors.ContainsKey(item.Shift) ? shiftFactors[item.Shift] : 1.0;
                    // Apply factor to TotalScore
                    item.TotalScore *= item.ShiftVolumeFactor;
                }

                // 6. Grading Curve
                // Sort by Adjusted Score Descending
                finalList = finalList.OrderByDescending(x => x.TotalScore).ToList();
                int totalUsers = finalList.Count;

                if (totalUsers > 0)
                {
                    int countA = (int)Math.Ceiling(totalUsers * 0.20);
                    int countB = (int)Math.Ceiling(totalUsers * 0.30);
                    int countC = (int)Math.Ceiling(totalUsers * 0.30);
                    int countD = (int)Math.Ceiling(totalUsers * 0.15);
                    // Remainder is F

                    for (int i = 0; i < totalUsers; i++)
                    {
                        if (i < countA) finalList[i].Grade = "A";
                        else if (i < countA + countB) finalList[i].Grade = "B";
                        else if (i < countA + countB + countC) finalList[i].Grade = "C";
                        else if (i < countA + countB + countC + countD) finalList[i].Grade = "D";
                        else finalList[i].Grade = "F";
                    }
                }

                return Model_Dao_Result<List<Model_Visual_MaterialHandlerScore>>.Success(finalList);
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, callerName: nameof(CalculateMaterialHandlerScoresAsync));
                return Model_Dao_Result<List<Model_Visual_MaterialHandlerScore>>.Failure("Failed to calculate scores.", ex);
            }
        }
    }
}
