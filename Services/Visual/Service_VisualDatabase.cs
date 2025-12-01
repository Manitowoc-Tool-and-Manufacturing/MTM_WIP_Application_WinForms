using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services.Logging;
using System.Configuration;
using ConfigurationManager = System.Configuration.ConfigurationManager;
using System.Linq;

namespace MTM_WIP_Application_Winforms.Services.Visual
{
    /// <summary>
    /// Service for interacting with the Infor Visual ERP database.
    /// </summary>
    public class Service_VisualDatabase : IService_VisualDatabase
    {
        #region Fields
        private bool _useSampleData = false;
        #endregion

        #region Properties
        private string _serverAddress => ConfigurationManager.AppSettings["VisualServer"] ?? "VISUALSQL";
        private string _databaseName => ConfigurationManager.AppSettings["VisualDatabase"] ?? "VMFG";
        private string? _userName => !string.IsNullOrEmpty(Model_Application_Variables.VisualUserName)
            ? Model_Application_Variables.VisualUserName
            : ConfigurationManager.AppSettings["VisualUserName"];
        private string? _password => !string.IsNullOrEmpty(Model_Application_Variables.VisualPassword)
            ? Model_Application_Variables.VisualPassword
            : ConfigurationManager.AppSettings["VisualPassword"];
        #endregion

        #region Methods
        /// <summary>
        /// Tests the connection to the Infor Visual database using current credentials.
        /// </summary>
        /// <returns>
        /// Model_Dao_Result containing true when the connection succeeds.
        /// Check IsSuccess before accessing Data.
        /// </returns>
        public async Task<Model_Dao_Result<bool>> TestConnectionAsync()
        {
#if DEBUG
            // In DEBUG mode, if credentials are missing, force fallback to sample data immediately
            if (string.IsNullOrEmpty(_userName) || string.IsNullOrEmpty(_password))
            {
                _useSampleData = true;
                return new Model_Dao_Result<bool> { IsSuccess = true, Data = true, ErrorMessage = "Debug Mode: Using Sample Data (No Credentials)" };
            }
#else
            if (string.IsNullOrEmpty(_userName) || string.IsNullOrEmpty(_password))
            {
                return new Model_Dao_Result<bool>
                {
                    IsSuccess = false,
                    ErrorMessage = "Visual ERP credentials are not configured."
                };
            }
#endif

            try
            {
                using (var connection = new SqlConnection(GetConnectionString()))
                {
                    await connection.OpenAsync();
                    _useSampleData = false;
                    return new Model_Dao_Result<bool> { IsSuccess = true, Data = true };
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                // In DEBUG mode, if connection fails, fallback to sample data
                LoggingUtility.Log($"[VisualService] Connection failed in DEBUG mode. Falling back to sample data. Error: {ex.Message}");
                _useSampleData = true;
                return new Model_Dao_Result<bool> { IsSuccess = true, Data = true, ErrorMessage = "Debug Mode: Using Sample Data (Connection Failed)" };
#else
                LoggingUtility.LogApplicationError(ex);
                return new Model_Dao_Result<bool>
                {
                    IsSuccess = false,
                    ErrorMessage = $"Connection failed: {ex.Message}",
                    Data = false
                };
#endif
            }
        }

        /// <summary>
        /// Executes a read-only query from an embedded resource file.
        /// </summary>
        /// <param name="category">The dashboard category to load query for.</param>
        /// <returns>DataTable containing the results.</returns>
        public async Task<Model_Dao_Result<DataTable>> GetDashboardDataAsync(Enum_VisualDashboardCategory category)
        {
            if (string.IsNullOrEmpty(_userName) || string.IsNullOrEmpty(_password))
            {
                return new Model_Dao_Result<DataTable>
                {
                    IsSuccess = false,
                    ErrorMessage = "Visual ERP credentials are not configured."
                };
            }

            string sql = GetEmbeddedSql(category);
            if (string.IsNullOrEmpty(sql))
            {
                return new Model_Dao_Result<DataTable>
                {
                    IsSuccess = false,
                    ErrorMessage = $"SQL resource not found for category: {category}"
                };
            }

            try
            {
                using (var connection = new SqlConnection(GetConnectionString()))
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand(sql, connection))
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            var dataTable = new DataTable();
                            dataTable.Load(reader);
                            return new Model_Dao_Result<DataTable>
                            {
                                IsSuccess = true,
                                Data = dataTable
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                return new Model_Dao_Result<DataTable>
                {
                    IsSuccess = false,
                    ErrorMessage = $"Error retrieving data: {ex.Message}"
                };
            }
        }

        /// <summary>
        /// Searches for dies based on part number or die number.
        /// </summary>
        /// <param name="searchTerm">The term to search for.</param>
        /// <param name="searchByPart">True to search by Part Number, False to search by Die Number.</param>
        /// <returns>DataTable containing search results.</returns>
        public async Task<Model_Dao_Result<DataTable>> SearchDiesAsync(string searchTerm, bool searchByPart)
        {
            if (string.IsNullOrEmpty(_userName) || string.IsNullOrEmpty(_password))
            {
                return new Model_Dao_Result<DataTable>
                {
                    IsSuccess = false,
                    ErrorMessage = "Visual ERP credentials are not configured."
                };
            }

            string sql;
            if (searchByPart)
            {
                // Search by Part Number -> Find Die (USER_1)
                sql = @"
                    SELECT
                        P.ID as [Part Number],
                        P.DESCRIPTION as [Description],
                        P.USER_1 as [Die Number],
                        D.USER_2 as [Die Location],
                        P.USER_7 as [Customer],
                        P.USER_9 as [Coil]
                    FROM PART P
                    LEFT JOIN PART D ON P.USER_1 = D.ID
                    WHERE P.ID LIKE @SearchTerm";
            }
            else
            {
                // Search by Die Number (FGT) -> Find Part (USER_5)
                sql = @"
                    SELECT
                        D.ID as [Die Number],
                        D.DESCRIPTION as [Description],
                        D.USER_5 as [Part Number],
                        D.USER_2 as [Die Location],
                        D.USER_9 as [Coil]
                    FROM PART D
                    WHERE D.ID LIKE @SearchTerm
                    AND D.ID LIKE 'FGT%-01'
                    AND D.ID <> 'FGT0001-01'";
            }

            try
            {
                using (var connection = new SqlConnection(GetConnectionString()))
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            var dataTable = new DataTable();
                            dataTable.Load(reader);
                            return new Model_Dao_Result<DataTable>
                            {
                                IsSuccess = true,
                                Data = dataTable
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                return new Model_Dao_Result<DataTable>
                {
                    IsSuccess = false,
                    ErrorMessage = $"Error searching dies: {ex.Message}"
                };
            }
        }

        /// <summary>
        /// Retrieves the receiving schedule based on specified filters.
        /// </summary>
        /// <param name="startDate">Start date for the filter.</param>
        /// <param name="endDate">End date for the filter.</param>
        /// <param name="dateFilterType">Type of date to filter by (Desired/Promise).</param>
        /// <param name="includeClosed">Whether to include closed POs.</param>
        /// <param name="includeConsignment">Whether to include consignment orders.</param>
        /// <param name="includeInternal">Whether to include internal orders.</param>
        /// <param name="includeService">Whether to include service items.</param>
        /// <param name="vendorFilter">Optional vendor name filter.</param>
        /// <param name="poFilter">Optional PO number filter.</param>
        /// <param name="mustHavePartNumber">Whether to require a part number.</param>
        /// <returns>DataTable containing the receiving schedule.</returns>
        public async Task<Model_Dao_Result<DataTable>> GetReceivingScheduleAsync(
            DateTime startDate,
            DateTime endDate,
            string dateFilterType,
            bool includeClosed,
            bool includeConsignment,
            bool includeInternal,
            bool includeService,
            string vendorFilter = "",
            string poFilter = "",
            bool mustHavePartNumber = false)
        {
            if (_useSampleData)
            {
                return GetSampleReceivingSchedule();
            }

            if (string.IsNullOrEmpty(_userName) || string.IsNullOrEmpty(_password))
            {
#if DEBUG
                _useSampleData = true;
                return GetSampleReceivingSchedule();
#else
                return new Model_Dao_Result<DataTable>
                {
                    IsSuccess = false,
                    ErrorMessage = "Visual ERP credentials are not configured."
                };
#endif
            }

            // Base Query
            string sql = @"
                SELECT
                    PO.ID as [PO Number],
                    V.NAME as [Vendor],
                    PO.ORDER_DATE as [Order Date],
                    PO.DESIRED_RECV_DATE as [PO Desired Date],
                    PO.PROMISE_DATE as [PO Promise Date],
                    POL.LINE_NO as [Line #],
                    POL.PART_ID as [Part Number],
                    POL.SERVICE_ID as [Service ID],
                    POL.ORDER_QTY as [Order Qty],
                    POL.TOTAL_RECEIVED_QTY as [Received Qty],
                    (POL.ORDER_QTY - POL.TOTAL_RECEIVED_QTY) as [Remaining Qty],
                    POL.DESIRED_RECV_DATE as [Line Desired Date],
                    POL.PROMISE_DATE as [Line Promise Date],
                    PO.STATUS as [PO Status],
                    POL.LINE_STATUS as [Line Status],
                    PO.CONSIGNMENT as [Consignment],
                    PO.INTERNAL_ORDER as [Internal],
                    PO.SHIP_VIA as [Ship Via],
                    (SELECT TOP 1 R.USER_ID 
                     FROM RECEIVER R 
                     INNER JOIN RECEIVER_LINE RL ON R.ID = RL.RECEIVER_ID 
                     WHERE RL.PURC_ORDER_ID = POL.PURC_ORDER_ID 
                       AND RL.PURC_ORDER_LINE_NO = POL.LINE_NO 
                     ORDER BY R.RECEIVED_DATE DESC) as [Received By]
                FROM PURCHASE_ORDER PO
                INNER JOIN PURC_ORDER_LINE POL ON PO.ID = POL.PURC_ORDER_ID
                LEFT JOIN VENDOR V ON PO.VENDOR_ID = V.ID
                WHERE 1=1
            ";

            // Date Filter Logic
            string dateCondition = "";
            switch (dateFilterType?.ToUpper())
            {
                case "PO DESIRED DATE":
                    dateCondition = " AND PO.DESIRED_RECV_DATE BETWEEN @StartDate AND @EndDate";
                    break;
                case "PO PROMISE DATE":
                    dateCondition = " AND PO.PROMISE_DATE BETWEEN @StartDate AND @EndDate";
                    break;
                case "LINE DESIRED DATE":
                    dateCondition = " AND POL.DESIRED_RECV_DATE BETWEEN @StartDate AND @EndDate";
                    break;
                case "LINE PROMISE DATE":
                    dateCondition = " AND POL.PROMISE_DATE BETWEEN @StartDate AND @EndDate";
                    break;
                case "ALL OF THE ABOVE":
                default:
                    dateCondition = @" AND (
                        (PO.DESIRED_RECV_DATE IS NULL OR PO.DESIRED_RECV_DATE BETWEEN @StartDate AND @EndDate) AND
                        (PO.PROMISE_DATE IS NULL OR PO.PROMISE_DATE BETWEEN @StartDate AND @EndDate) AND
                        (POL.DESIRED_RECV_DATE IS NULL OR POL.DESIRED_RECV_DATE BETWEEN @StartDate AND @EndDate) AND
                        (POL.PROMISE_DATE IS NULL OR POL.PROMISE_DATE BETWEEN @StartDate AND @EndDate)
                    )";
                    break;
            }
            sql += dateCondition;

            // Checkbox Filters
            if (!includeClosed)
            {
                sql += " AND PO.STATUS <> 'C' AND POL.LINE_STATUS <> 'C'";
            }

            if (includeConsignment)
            {
                sql += " AND PO.CONSIGNMENT = 'Y'";
            }
            else
            {
                sql += " AND (PO.CONSIGNMENT IS NULL OR PO.CONSIGNMENT <> 'Y')";
            }

            if (includeInternal)
            {
                sql += " AND PO.INTERNAL_ORDER = 'Y'";
            }
            else
            {
                sql += " AND (PO.INTERNAL_ORDER IS NULL OR PO.INTERNAL_ORDER <> 'Y')";
            }

            if (includeService)
            {
                sql += " AND POL.SERVICE_ID IS NOT NULL";
            }
            else
            {
                sql += " AND POL.SERVICE_ID IS NULL";
            }

            if (mustHavePartNumber)
            {
                sql += " AND POL.PART_ID IS NOT NULL AND POL.PART_ID <> ''";
            }

            if (!string.IsNullOrWhiteSpace(vendorFilter))
            {
                sql += " AND V.NAME LIKE @VendorFilter";
            }

            if (!string.IsNullOrWhiteSpace(poFilter))
            {
                sql += " AND PO.ID LIKE @PoFilter";
            }

            // Ordering
            sql += " ORDER BY PO.DESIRED_RECV_DATE, PO.ID, POL.LINE_NO";

            try
            {
                using (var connection = new SqlConnection(GetConnectionString()))
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@StartDate", startDate.Date);
                        command.Parameters.AddWithValue("@EndDate", endDate.Date.AddDays(1).AddSeconds(-1)); // End of day

                        if (!string.IsNullOrWhiteSpace(vendorFilter))
                        {
                            command.Parameters.AddWithValue("@VendorFilter", "%" + vendorFilter + "%");
                        }

                        if (!string.IsNullOrWhiteSpace(poFilter))
                        {
                            command.Parameters.AddWithValue("@PoFilter", "%" + poFilter + "%");
                        }

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            var dataTable = new DataTable();
                            dataTable.Load(reader);
                            return new Model_Dao_Result<DataTable>
                            {
                                IsSuccess = true,
                                Data = dataTable
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                LoggingUtility.Log($"[VisualService] GetReceivingScheduleAsync failed in DEBUG. Fallback to sample. Error: {ex.Message}");
                _useSampleData = true;
                return GetSampleReceivingSchedule();
#else
                LoggingUtility.LogApplicationError(ex);
                return new Model_Dao_Result<DataTable>
                {
                    IsSuccess = false,
                    ErrorMessage = $"Error retrieving receiving schedule: {ex.Message}"
                };
#endif
            }
        }

        /// <summary>
        /// Retrieves all line details for a specific Purchase Order.
        /// </summary>
        /// <param name="poNumber">The PO Number to retrieve details for.</param>
        /// <returns>DataTable containing PO details.</returns>
        public async Task<Model_Dao_Result<DataTable>> GetPODetailsAsync(string poNumber)
        {
            if (_useSampleData)
            {
                return GetSamplePODetails(poNumber);
            }

            if (string.IsNullOrEmpty(_userName) || string.IsNullOrEmpty(_password))
            {
#if DEBUG
                _useSampleData = true;
                return GetSamplePODetails(poNumber);
#else
                return new Model_Dao_Result<DataTable>
                {
                    IsSuccess = false,
                    ErrorMessage = "Visual ERP credentials are not configured."
                };
#endif
            }

            string sql = @"
                SELECT
                    POL.LINE_NO as [Line #],
                    POL.PART_ID as [Part Number],
                    P.DESCRIPTION as [Description],
                    POL.ORDER_QTY as [Ordered],
                    POL.TOTAL_RECEIVED_QTY as [Received],
                    (POL.ORDER_QTY - POL.TOTAL_RECEIVED_QTY) as [Remaining],
                    POL.DESIRED_RECV_DATE as [Desired Date],
                    POL.PROMISE_DATE as [Promise Date],
                    POL.LINE_STATUS as [Status],
                    POL.UNIT_PRICE as [Unit Price],
                    POL.TOTAL_AMT_ORDERED as [Total Amount]
                FROM PURC_ORDER_LINE POL
                LEFT JOIN PART P ON POL.PART_ID = P.ID
                WHERE POL.PURC_ORDER_ID = @PoNumber
                ORDER BY POL.LINE_NO
            ";

            try
            {
                using (var connection = new SqlConnection(GetConnectionString()))
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@PoNumber", poNumber);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            var dataTable = new DataTable();
                            dataTable.Load(reader);
                            return new Model_Dao_Result<DataTable>
                            {
                                IsSuccess = true,
                                Data = dataTable
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                LoggingUtility.Log($"[VisualService] GetPODetailsAsync failed in DEBUG. Fallback to sample. Error: {ex.Message}");
                _useSampleData = true;
                return GetSamplePODetails(poNumber);
#else
                LoggingUtility.LogApplicationError(ex);
                return new Model_Dao_Result<DataTable>
                {
                    IsSuccess = false,
                    ErrorMessage = $"Error retrieving PO details: {ex.Message}"
                };
#endif
            }
        }

        /// <summary>
        /// Retrieves analytics data for receiving history and forecast.
        /// </summary>
        public async Task<Model_Dao_Result<Model_ReceivingAnalytics>> GetReceivingAnalyticsAsync(DateTime? startDate = null, DateTime? endDate = null)
        {
            if (_useSampleData)
            {
                return GetSampleReceivingAnalytics();
            }

            if (string.IsNullOrEmpty(_userName) || string.IsNullOrEmpty(_password))
            {
#if DEBUG
                _useSampleData = true;
                return GetSampleReceivingAnalytics();
#else
                return new Model_Dao_Result<Model_ReceivingAnalytics>
                {
                    IsSuccess = false,
                    ErrorMessage = "Visual ERP credentials are not configured."
                };
#endif
            }

            var analytics = new Model_ReceivingAnalytics();
            var historyStart = startDate ?? new DateTime(DateTime.Now.Year, 1, 1); // Default to YTD
            var forecastEnd = endDate ?? DateTime.Today.AddDays(180); // Default to +180 days

            // 1. History Query
            // Get all receipts within the date range, regardless of "Today"
            string sqlHistory = @"
                SELECT [Date], COUNT(*) as [Count], [Type], [PartNumber], [ReceivedBy]
                FROM (
                    SELECT 
                        POL.LAST_RECEIVED_DATE as [Date],
                        CASE 
                            WHEN POL.SERVICE_ID IS NOT NULL THEN 'Service'
                            WHEN POL.PART_ID LIKE 'MMC%' THEN 'MMC'
                            WHEN POL.PART_ID LIKE 'MMF%' THEN 'MMF'
                            ELSE 'Part'
                        END as [Type],
                        POL.PART_ID as [PartNumber],
                        (SELECT TOP 1 R.USER_ID 
                         FROM RECEIVER R 
                         INNER JOIN RECEIVER_LINE RL ON R.ID = RL.RECEIVER_ID 
                         WHERE RL.PURC_ORDER_ID = POL.PURC_ORDER_ID 
                           AND RL.PURC_ORDER_LINE_NO = POL.LINE_NO 
                         ORDER BY R.RECEIVED_DATE DESC) as [ReceivedBy]
                    FROM PURC_ORDER_LINE POL
                    INNER JOIN PURCHASE_ORDER PO ON POL.PURC_ORDER_ID = PO.ID
                    WHERE POL.TOTAL_RECEIVED_QTY > 0 
                      AND POL.LAST_RECEIVED_DATE >= @StartDate
                      AND POL.LAST_RECEIVED_DATE <= @EndDate
                      AND ISNULL(PO.CONSIGNMENT, 'N') <> 'Y' 
                      AND ISNULL(PO.INTERNAL_ORDER, 'N') <> 'Y'
                ) as Sub
                GROUP BY [Date], [Type], [PartNumber], [ReceivedBy]
                ORDER BY [Date]";

            // 2. Forecast Query
            // Get all open lines due within the date range
            // Logic: Service -> Desired Date, Others -> Promise Date
            // Fallback chain: Line Date -> PO Date -> Alternate Date Type
            string sqlForecast = @"
                SELECT [Date], COUNT(*) as [Count], [Type], [PartNumber], '' as [ReceivedBy]
                FROM (
                    SELECT 
                        CASE 
                            WHEN POL.SERVICE_ID IS NOT NULL THEN COALESCE(POL.DESIRED_RECV_DATE, PO.DESIRED_RECV_DATE, POL.PROMISE_DATE, PO.PROMISE_DATE)
                            ELSE COALESCE(POL.PROMISE_DATE, PO.PROMISE_DATE, POL.DESIRED_RECV_DATE, PO.DESIRED_RECV_DATE)
                        END as [Date],
                        CASE 
                            WHEN POL.SERVICE_ID IS NOT NULL THEN 'Service'
                            WHEN POL.PART_ID LIKE 'MMC%' THEN 'MMC'
                            WHEN POL.PART_ID LIKE 'MMF%' THEN 'MMF'
                            ELSE 'Part'
                        END as [Type],
                        POL.PART_ID as [PartNumber]
                    FROM PURC_ORDER_LINE POL
                    INNER JOIN PURCHASE_ORDER PO ON POL.PURC_ORDER_ID = PO.ID
                    WHERE (POL.ORDER_QTY - POL.TOTAL_RECEIVED_QTY) > 0 
                      AND POL.LINE_STATUS <> 'C'
                      AND PO.STATUS <> 'C'
                      AND ISNULL(PO.CONSIGNMENT, 'N') <> 'Y' 
                      AND ISNULL(PO.INTERNAL_ORDER, 'N') <> 'Y'
                      AND (
                          CASE 
                              WHEN POL.SERVICE_ID IS NOT NULL THEN COALESCE(POL.DESIRED_RECV_DATE, PO.DESIRED_RECV_DATE, POL.PROMISE_DATE, PO.PROMISE_DATE)
                              ELSE COALESCE(POL.PROMISE_DATE, PO.PROMISE_DATE, POL.DESIRED_RECV_DATE, PO.DESIRED_RECV_DATE)
                          END
                      ) >= @StartDate
                      AND (
                          CASE 
                              WHEN POL.SERVICE_ID IS NOT NULL THEN COALESCE(POL.DESIRED_RECV_DATE, PO.DESIRED_RECV_DATE, POL.PROMISE_DATE, PO.PROMISE_DATE)
                              ELSE COALESCE(POL.PROMISE_DATE, PO.PROMISE_DATE, POL.DESIRED_RECV_DATE, PO.DESIRED_RECV_DATE)
                          END
                      ) <= @EndDate
                ) as Sub
                GROUP BY [Date], [Type], [PartNumber]
                ORDER BY [Date]";

            try
            {
                using (var connection = new SqlConnection(GetConnectionString()))
                {
                    await connection.OpenAsync();

                    // Execute History
                    using (var command = new SqlCommand(sqlHistory, connection))
                    {
                        // Broad range: 1 year back to 6 months forward (or user specified)
                        // If user didn't specify, we default to a wide window in the caller or here
                        // The caller (Control_ReceivingAnalytics) is now passing -1 year to +6 months
                        command.Parameters.AddWithValue("@StartDate", startDate ?? DateTime.Today.AddYears(-1));
                        command.Parameters.AddWithValue("@EndDate", endDate ?? DateTime.Today.AddMonths(6));
                        
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                if (reader["Date"] != DBNull.Value)
                                {
                                    analytics.History.Add(new AnalyticsDataPoint
                                    {
                                        Date = Convert.ToDateTime(reader["Date"]),
                                        Count = Convert.ToInt32(reader["Count"]),
                                        Type = reader["Type"].ToString() ?? "Part",
                                        PartNumber = reader["PartNumber"]?.ToString() ?? string.Empty,
                                        ReceivedBy = reader["ReceivedBy"]?.ToString() ?? string.Empty
                                    });
                                }
                            }
                        }
                    }

                    // Execute Forecast
                    using (var command = new SqlCommand(sqlForecast, connection))
                    {
                        command.Parameters.AddWithValue("@StartDate", startDate ?? DateTime.Today.AddYears(-1));
                        command.Parameters.AddWithValue("@EndDate", endDate ?? DateTime.Today.AddMonths(6));
                        
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                if (reader["Date"] != DBNull.Value)
                                {
                                    analytics.Forecast.Add(new AnalyticsDataPoint
                                    {
                                        Date = Convert.ToDateTime(reader["Date"]),
                                        Count = Convert.ToInt32(reader["Count"]),
                                        Type = reader["Type"].ToString() ?? "Part",
                                        PartNumber = reader["PartNumber"]?.ToString() ?? string.Empty,
                                        ReceivedBy = reader["ReceivedBy"]?.ToString() ?? string.Empty
                                    });
                                }
                            }
                        }
                    }
                }

                return new Model_Dao_Result<Model_ReceivingAnalytics> { IsSuccess = true, Data = analytics };
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                return new Model_Dao_Result<Model_ReceivingAnalytics>
                {
                    IsSuccess = false,
                    ErrorMessage = $"Error fetching analytics: {ex.Message}"
                };
            }
        }

        private Model_Dao_Result<Model_ReceivingAnalytics> GetSampleReceivingAnalytics()
        {
            var analytics = new Model_ReceivingAnalytics();
            var rnd = new Random();
            var types = new[] { "Part", "MMC", "MMF", "Service", "Consignment", "Internal" };
            var users = new[] { "JDOE", "BSMITH", "MJONES", "ADMIN", "RECEIVING" };

            // Generate History (YTD)
            var startDate = new DateTime(DateTime.Now.Year, 1, 1);
            for (var date = startDate; date <= DateTime.Today; date = date.AddDays(1))
            {
                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) continue;
                
                // Add random data points for each day
                int points = rnd.Next(1, 4);
                for (int i = 0; i < points; i++)
                {
                    analytics.History.Add(new AnalyticsDataPoint
                    {
                        Date = date,
                        Count = rnd.Next(1, 10),
                        Type = types[rnd.Next(types.Length)],
                        ReceivedBy = users[rnd.Next(users.Length)]
                    });
                }
            }

            // Generate Forecast (Next 90 days)
            for (var date = DateTime.Today; date <= DateTime.Today.AddDays(90); date = date.AddDays(1))
            {
                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) continue;

                int points = rnd.Next(1, 4);
                for (int i = 0; i < points; i++)
                {
                    analytics.Forecast.Add(new AnalyticsDataPoint
                    {
                        Date = date,
                        Count = rnd.Next(1, 10),
                        Type = types[rnd.Next(types.Length)],
                        ReceivedBy = string.Empty
                    });
                }
            }

            return new Model_Dao_Result<Model_ReceivingAnalytics> { IsSuccess = true, Data = analytics };
        }

        /// <summary>
        /// Retrieves inventory data based on search criteria.
        /// </summary>
        /// <param name="partNumber">Optional part number filter.</param>
        /// <param name="warehouse">Optional warehouse filter.</param>
        /// <param name="location">Optional location filter.</param>
        /// <param name="nonZeroOnly">True to return only rows with non-zero quantities.</param>
        /// <returns>
        /// Model_Dao_Result containing the inventory DataTable.
        /// Check IsSuccess before accessing Data.
        /// </returns>
        public async Task<Model_Dao_Result<DataTable>> GetInventoryAsync(string partNumber, string warehouse, string location, bool nonZeroOnly)
        {
            if (_useSampleData)
            {
                return await Task.FromResult(GetSampleInventoryData(partNumber, warehouse, location, nonZeroOnly));
            }

            if (string.IsNullOrEmpty(_userName) || string.IsNullOrEmpty(_password))
            {
#if DEBUG
                _useSampleData = true;
                return await Task.FromResult(GetSampleInventoryData(partNumber, warehouse, location, nonZeroOnly));
#else
                return new Model_Dao_Result<DataTable>
                {
                    IsSuccess = false,
                    ErrorMessage = "Visual ERP credentials are not configured."
                };
#endif
            }

            string sql = @"
                SELECT 
                    P.ID as [Part Number],
                    P.DESCRIPTION as [Description],
                    PL.WAREHOUSE_ID as [Warehouse],
                    PL.LOCATION_ID as [Location],
                    PL.QTY as [On Hand],
                    PL.COMMITTED_QTY as [Allocated],
                    (PL.QTY - PL.COMMITTED_QTY) as [Available],
                    P.PRODUCT_CODE as [Product Code],
                    P.COMMODITY_CODE as [Commodity Code]
                FROM PART_LOCATION PL
                INNER JOIN PART P ON PL.PART_ID = P.ID
                WHERE 1=1";

            if (!string.IsNullOrWhiteSpace(partNumber))
            {
                sql += " AND P.ID LIKE @PartNumber";
            }

            if (!string.IsNullOrWhiteSpace(warehouse))
            {
                sql += " AND PL.WAREHOUSE_ID LIKE @Warehouse";
            }

            if (!string.IsNullOrWhiteSpace(location))
            {
                sql += " AND PL.LOCATION_ID LIKE @Location";
            }

            if (nonZeroOnly)
            {
                sql += " AND PL.QTY <> 0";
            }

            sql += " ORDER BY P.ID, PL.WAREHOUSE_ID, PL.LOCATION_ID";

            try
            {
                using (var connection = new SqlConnection(GetConnectionString()))
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand(sql, connection))
                    {
                        if (!string.IsNullOrWhiteSpace(partNumber))
                            command.Parameters.AddWithValue("@PartNumber", "%" + partNumber + "%");
                        
                        if (!string.IsNullOrWhiteSpace(warehouse))
                            command.Parameters.AddWithValue("@Warehouse", "%" + warehouse + "%");

                        if (!string.IsNullOrWhiteSpace(location))
                            command.Parameters.AddWithValue("@Location", "%" + location + "%");

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            var dt = new DataTable();
                            dt.Load(reader);
                            return new Model_Dao_Result<DataTable> { IsSuccess = true, Data = dt };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                return new Model_Dao_Result<DataTable>
                {
                    IsSuccess = false,
                    ErrorMessage = $"Error fetching inventory: {ex.Message}"
                };
            }
        }

        /// <summary>
        /// Retrieves transaction history based on flexible filter criteria.
        /// </summary>
        /// <param name="filter">The filter criteria object.</param>
        /// <returns>
        /// Model_Dao_Result containing the transaction DataTable.
        /// Check IsSuccess before accessing Data.
        /// </returns>
        public async Task<Model_Dao_Result<DataTable>> GetTransactionsAsync(Model_VisualTransactionFilter filter)
        {
            if (_useSampleData)
            {
                return GetSampleTransactions(filter);
            }

            if (string.IsNullOrEmpty(_userName) || string.IsNullOrEmpty(_password))
            {
#if DEBUG
                _useSampleData = true;
                return GetSampleTransactions(filter);
#else
                return new Model_Dao_Result<DataTable>
                {
                    IsSuccess = false,
                    ErrorMessage = "Visual ERP credentials are not configured."
                };
#endif
            }

            // Base Query
            string sql = @"
                SELECT TOP (@MaxRecords)
                    TRANSACTION_ID,
                    TRANSACTION_DATE,
                    CREATE_DATE,
                    TYPE,
                    QTY,
                    WAREHOUSE_ID,
                    LOCATION_ID,
                    USER_ID,
                    DESCRIPTION,
                    PART_ID,
                    WORKORDER_BASE_ID,
                    WORKORDER_LOT_ID,
                    CUST_ORDER_ID,
                    PURC_ORDER_ID
                FROM INVENTORY_TRANS
                WHERE 1=1
            ";

            // Dynamic Filters
            if (!string.IsNullOrWhiteSpace(filter.PartId))
                sql += " AND PART_ID LIKE @PartId";

            if (!string.IsNullOrWhiteSpace(filter.UserId))
                sql += " AND USER_ID LIKE @UserId";

            if (!string.IsNullOrWhiteSpace(filter.WorkOrder))
                sql += " AND WORKORDER_BASE_ID LIKE @WorkOrder";

            if (!string.IsNullOrWhiteSpace(filter.CustomerOrder))
                sql += " AND CUST_ORDER_ID LIKE @CustomerOrder";

            if (!string.IsNullOrWhiteSpace(filter.PurchaseOrder))
                sql += " AND PURC_ORDER_ID LIKE @PurchaseOrder";

            if (filter.StartDate.HasValue)
                sql += " AND CAST(COALESCE(CREATE_DATE, TRANSACTION_DATE) AS DATE) >= @StartDate";

            if (filter.EndDate.HasValue)
                sql += " AND CAST(COALESCE(CREATE_DATE, TRANSACTION_DATE) AS DATE) <= @EndDate";

            // Ordering
            sql += " ORDER BY CREATE_DATE DESC, TRANSACTION_ID DESC";

            try
            {
                using (var connection = new SqlConnection(GetConnectionString()))
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@MaxRecords", filter.MaxRecords > 0 ? filter.MaxRecords : 1000);

                        if (!string.IsNullOrWhiteSpace(filter.PartId))
                            command.Parameters.AddWithValue("@PartId", "%" + filter.PartId + "%");

                        if (!string.IsNullOrWhiteSpace(filter.UserId))
                            command.Parameters.AddWithValue("@UserId", "%" + filter.UserId + "%");

                        if (!string.IsNullOrWhiteSpace(filter.WorkOrder))
                            command.Parameters.AddWithValue("@WorkOrder", "%" + filter.WorkOrder + "%");

                        if (!string.IsNullOrWhiteSpace(filter.CustomerOrder))
                            command.Parameters.AddWithValue("@CustomerOrder", "%" + filter.CustomerOrder + "%");

                        if (!string.IsNullOrWhiteSpace(filter.PurchaseOrder))
                            command.Parameters.AddWithValue("@PurchaseOrder", "%" + filter.PurchaseOrder + "%");

                        if (filter.StartDate.HasValue)
                            command.Parameters.AddWithValue("@StartDate", filter.StartDate.Value.Date);

                        if (filter.EndDate.HasValue)
                            command.Parameters.AddWithValue("@EndDate", filter.EndDate.Value.Date);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            var dataTable = new DataTable();
                            dataTable.Load(reader);
                            return new Model_Dao_Result<DataTable>
                            {
                                IsSuccess = true,
                                Data = dataTable
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                return new Model_Dao_Result<DataTable>
                {
                    IsSuccess = false,
                    ErrorMessage = $"Error retrieving transactions: {ex.Message}"
                };
            }
        }

        /// <summary>
        /// Retrieves a list of all User IDs from the transaction history.
        /// </summary>
        public async Task<Model_Dao_Result<List<string>>> GetUserIdsAsync()
        {
            return await GetDistinctColumnValuesAsync("USER_ID", "INVENTORY_TRANS");
        }

        /// <summary>
        /// Retrieves a list of all Work Order IDs from the transaction history.
        /// </summary>
        public async Task<Model_Dao_Result<List<string>>> GetWorkOrdersAsync()
        {
            return await GetDistinctColumnValuesAsync("WORKORDER_BASE_ID", "INVENTORY_TRANS");
        }

        /// <summary>
        /// Retrieves a list of all Purchase Order IDs from the transaction history.
        /// </summary>
        public async Task<Model_Dao_Result<List<string>>> GetPurchaseOrdersAsync()
        {
            return await GetDistinctColumnValuesAsync("PURC_ORDER_ID", "INVENTORY_TRANS");
        }

        /// <summary>
        /// Retrieves a list of all Customer Order IDs from the transaction history.
        /// </summary>
        public async Task<Model_Dao_Result<List<string>>> GetCustomerOrdersAsync()
        {
            return await GetDistinctColumnValuesAsync("CUST_ORDER_ID", "INVENTORY_TRANS");
        }

        /// <summary>
        /// Retrieves a list of all Part IDs from the Visual database.
        /// </summary>
        public async Task<Model_Dao_Result<List<string>>> GetPartIdsAsync()
        {
            return await GetDistinctColumnValuesAsync("ID", "PART");
        }

        private async Task<Model_Dao_Result<List<string>>> GetDistinctColumnValuesAsync(string columnName, string tableName)
        {
            if (_useSampleData)
            {
                return new Model_Dao_Result<List<string>> { IsSuccess = true, Data = new List<string> { "SAMPLE-1", "SAMPLE-2" } };
            }

            if (string.IsNullOrEmpty(_userName) || string.IsNullOrEmpty(_password))
            {
                return new Model_Dao_Result<List<string>>
                {
                    IsSuccess = false,
                    ErrorMessage = "Visual ERP credentials are not configured."
                };
            }

            string sql = $"SELECT DISTINCT {columnName} FROM {tableName} WHERE {columnName} IS NOT NULL AND {columnName} <> '' ORDER BY {columnName}";

            try
            {
                using (var connection = new SqlConnection(GetConnectionString()))
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand(sql, connection))
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            var list = new List<string>();
                            while (await reader.ReadAsync())
                            {
                                list.Add(reader[0].ToString() ?? "");
                            }
                            return new Model_Dao_Result<List<string>>
                            {
                                IsSuccess = true,
                                Data = list
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                return new Model_Dao_Result<List<string>>
                {
                    IsSuccess = false,
                    ErrorMessage = $"Error retrieving {columnName}: {ex.Message}"
                };
            }
        }

        #endregion

        #region Helpers
        private Model_Dao_Result<DataTable> GetSampleTransactions(Model_VisualTransactionFilter filter)
        {
            var dt = new DataTable();
            dt.Columns.Add("TRANSACTION_ID", typeof(int));
            dt.Columns.Add("TRANSACTION_DATE", typeof(DateTime));
            dt.Columns.Add("CREATE_DATE", typeof(DateTime));
            dt.Columns.Add("TYPE", typeof(string));
            dt.Columns.Add("QTY", typeof(decimal));
            dt.Columns.Add("WAREHOUSE_ID", typeof(string));
            dt.Columns.Add("LOCATION_ID", typeof(string));
            dt.Columns.Add("USER_ID", typeof(string));
            dt.Columns.Add("DESCRIPTION", typeof(string));
            dt.Columns.Add("PART_ID", typeof(string));
            dt.Columns.Add("WORKORDER_BASE_ID", typeof(string));
            dt.Columns.Add("WORKORDER_LOT_ID", typeof(string));
            dt.Columns.Add("CUST_ORDER_ID", typeof(string));
            dt.Columns.Add("PURC_ORDER_ID", typeof(string));

            var rnd = new Random();
            var types = new[] { "I", "O", "A" };
            var parts = new[] { "PART-A", "PART-B", "PART-C" };
            var users = new[] { "USER1", "USER2", "ADMIN" };

            for (int i = 0; i < 50; i++)
            {
                var date = DateTime.Now.AddDays(-rnd.Next(0, 30));
                dt.Rows.Add(
                    1000 + i,
                    date.Date,
                    date,
                    types[rnd.Next(types.Length)],
                    rnd.Next(1, 100),
                    "MAIN",
                    $"LOC-{rnd.Next(1, 10)}",
                    users[rnd.Next(users.Length)],
                    "Sample Transaction",
                    parts[rnd.Next(parts.Length)],
                    rnd.Next(0, 2) == 0 ? $"WO-{rnd.Next(1000, 9999)}" : null,
                    null,
                    rnd.Next(0, 5) == 0 ? $"CO-{rnd.Next(1000, 9999)}" : null,
                    rnd.Next(0, 5) == 0 ? $"PO-{rnd.Next(1000, 9999)}" : null
                );
            }

            return new Model_Dao_Result<DataTable> { IsSuccess = true, Data = dt };
        }

        private string GetConnectionString()
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = _serverAddress,
                InitialCatalog = _databaseName,
                UserID = _userName,
                Password = _password,
                ConnectTimeout = 5, // Reduced timeout for faster fallback
                ApplicationName = "MTM_WIP_App_VisualDashboard",
                TrustServerCertificate = true // Required for some SQL Server configurations
            };
            return builder.ConnectionString;
        }

        private Model_Dao_Result<DataTable> GetSampleInventoryData(string partNumber, string warehouse, string location, bool nonZeroOnly)
        {
            var dt = new DataTable();
            dt.Columns.Add("Part Number");
            dt.Columns.Add("Description");
            dt.Columns.Add("Warehouse");
            dt.Columns.Add("Location");
            dt.Columns.Add("On Hand", typeof(decimal));
            dt.Columns.Add("Allocated", typeof(decimal));
            dt.Columns.Add("Available", typeof(decimal));
            dt.Columns.Add("Product Code");
            dt.Columns.Add("Commodity Code");

            dt.Rows.Add("SAMPLE-PART-1", "Sample Description 1", "MAIN", "A-01-01", 100m, 10m, 90m, "FG", "STEEL");
            dt.Rows.Add("SAMPLE-PART-2", "Sample Description 2", "MAIN", "B-02-02", 50m, 0m, 50m, "RM", "ALUM");

            var filteredRows = dt.AsEnumerable()
                .Where(row => MatchesInventoryFilters(row, partNumber, warehouse, location, nonZeroOnly))
                .ToList();

            if (!filteredRows.Any())
            {
                return new Model_Dao_Result<DataTable> { IsSuccess = true, Data = dt.Clone() };
            }

            var result = dt.Clone();
            foreach (var row in filteredRows)
            {
                result.ImportRow(row);
            }

            return new Model_Dao_Result<DataTable> { IsSuccess = true, Data = result };
        }

        private static bool MatchesInventoryFilters(DataRow row, string partNumber, string warehouse, string location, bool nonZeroOnly)
        {
            string? partValue = row["Part Number"]?.ToString();
            string? warehouseValue = row["Warehouse"]?.ToString();
            string? locationValue = row["Location"]?.ToString();
            decimal onHand = row.Field<decimal>("On Hand");

            if (!string.IsNullOrWhiteSpace(partNumber) &&
                (partValue == null || !partValue.Contains(partNumber, StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }

            if (!string.IsNullOrWhiteSpace(warehouse) &&
                (warehouseValue == null || !warehouseValue.Contains(warehouse, StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }

            if (!string.IsNullOrWhiteSpace(location) &&
                (locationValue == null || !locationValue.Contains(location, StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }

            if (nonZeroOnly && onHand == 0)
            {
                return false;
            }

            return true;
        }

        private string GetEmbeddedSql(Enum_VisualDashboardCategory category)
        {
            string resourceName = $"MTM_WIP_Application_Winforms.Resources.Sql.Visual.{category}.sql";
            var assembly = Assembly.GetExecutingAssembly();

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    return string.Empty;
                }
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        private Model_Dao_Result<DataTable> GetSampleReceivingSchedule()
        {
            var dt = new DataTable();
            dt.Columns.Add("PO Number");
            dt.Columns.Add("Vendor");
            dt.Columns.Add("Order Date", typeof(DateTime));
            dt.Columns.Add("PO Desired Date", typeof(DateTime));
            dt.Columns.Add("PO Promise Date", typeof(DateTime));
            dt.Columns.Add("Line #", typeof(short));
            dt.Columns.Add("Part Number");
            dt.Columns.Add("Service ID");
            dt.Columns.Add("Order Qty", typeof(decimal));
            dt.Columns.Add("Received Qty", typeof(decimal));
            dt.Columns.Add("Remaining Qty", typeof(decimal));
            dt.Columns.Add("Line Desired Date", typeof(DateTime));
            dt.Columns.Add("Line Promise Date", typeof(DateTime));
            dt.Columns.Add("PO Status");
            dt.Columns.Add("Line Status");
            dt.Columns.Add("Consignment");
            dt.Columns.Add("Internal");
            dt.Columns.Add("Ship Via"); // Added for Carrier filter
            dt.Columns.Add("Received By"); // Added for Received By column

            var rnd = new Random();
            var vendors = new[] { "Acme Corp", "Steel Supply Co", "Fasteners Inc", "Global Logistics", "Local Services" };
            var carriers = new[] { "UPS", "FedEx", "DHL", "Our Truck", "Customer Pickup" };
            var parts = new[] { "MMC-1001", "MMF-2002", "PART-3003", "SVC-MAINT", "MMC-5005" };
            var users = new[] { "JDOE", "BSMITH", "MJONES", "ADMIN", "RECEIVING" };

            for (int i = 0; i < 50; i++)
            {
                var orderDate = DateTime.Today.AddDays(-rnd.Next(0, 30));
                var desiredDate = orderDate.AddDays(rnd.Next(5, 20));
                var qty = rnd.Next(10, 1000);
                var rec = rnd.Next(0, qty + 1);
                var part = parts[rnd.Next(parts.Length)];
                var vendor = vendors[rnd.Next(vendors.Length)];
                var carrier = carriers[rnd.Next(carriers.Length)];
                var receivedBy = rec > 0 ? users[rnd.Next(users.Length)] : DBNull.Value;

                dt.Rows.Add(
                    $"PO-{10000 + i}",
                    vendor,
                    orderDate,
                    desiredDate,
                    desiredDate.AddDays(rnd.Next(-2, 5)),
                    (short)rnd.Next(1, 5),
                    part,
                    part.StartsWith("SVC") ? "SERVICE" : DBNull.Value,
                    qty,
                    rec,
                    qty - rec,
                    desiredDate,
                    desiredDate.AddDays(rnd.Next(-2, 5)),
                    rec == qty ? "C" : "O",
                    rec == qty ? "C" : "O",
                    rnd.Next(0, 5) == 0 ? "Y" : "N", // Consignment
                    rnd.Next(0, 10) == 0 ? "Y" : "N", // Internal
                    carrier,
                    receivedBy
                );
            }

            return new Model_Dao_Result<DataTable> { IsSuccess = true, Data = dt };
        }

        private Model_Dao_Result<DataTable> GetSamplePODetails(string poNumber)
        {
            var dt = new DataTable();
            dt.Columns.Add("Line #", typeof(short));
            dt.Columns.Add("Part Number");
            dt.Columns.Add("Description");
            dt.Columns.Add("Ordered", typeof(decimal));
            dt.Columns.Add("Received", typeof(decimal));
            dt.Columns.Add("Remaining", typeof(decimal));
            dt.Columns.Add("Desired Date", typeof(DateTime));
            dt.Columns.Add("Promise Date", typeof(DateTime));
            dt.Columns.Add("Status");
            dt.Columns.Add("Unit Price", typeof(decimal));
            dt.Columns.Add("Total Amount", typeof(decimal));

            var rnd = new Random();
            for (int i = 1; i <= 3; i++)
            {
                var qty = rnd.Next(10, 100);
                var price = (decimal)rnd.NextDouble() * 100;
                dt.Rows.Add(
                    (short)i,
                    $"SAMPLE-PART-{i}",
                    $"Sample Description for Line {i}",
                    qty,
                    0,
                    qty,
                    DateTime.Today.AddDays(7),
                    DateTime.Today.AddDays(7),
                    "O",
                    Math.Round(price, 2),
                    Math.Round(price * qty, 2)
                );
            }

            return new Model_Dao_Result<DataTable> { IsSuccess = true, Data = dt };
        }
        #endregion

    }
}
