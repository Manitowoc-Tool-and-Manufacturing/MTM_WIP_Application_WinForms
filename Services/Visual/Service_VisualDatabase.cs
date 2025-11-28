using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Logging;
using System.Configuration;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace MTM_WIP_Application_Winforms.Services.Visual
{
    /// <summary>
    /// Service for interacting with the Infor Visual ERP database.
    /// </summary>
    public class Service_VisualDatabase : IService_VisualDatabase
    {
        private string _serverAddress => ConfigurationManager.AppSettings["VisualServer"] ?? "VISUALSQL";
        private string _databaseName => ConfigurationManager.AppSettings["VisualDatabase"] ?? "VMFG";

        private string? _userName => !string.IsNullOrEmpty(Model_Application_Variables.VisualUserName)
            ? Model_Application_Variables.VisualUserName
            : ConfigurationManager.AppSettings["VisualUserName"];

        private string? _password => !string.IsNullOrEmpty(Model_Application_Variables.VisualPassword)
            ? Model_Application_Variables.VisualPassword
            : ConfigurationManager.AppSettings["VisualPassword"];

        private bool _useSampleData = false;

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

        /// <summary>
        /// Tests the connection to the Infor Visual database using current credentials.
        /// </summary>
        /// <returns>A result indicating success or failure of the connection test.</returns>
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
                    PO.SHIP_VIA as [Ship Via]
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

            var rnd = new Random();
            var vendors = new[] { "Acme Corp", "Steel Supply Co", "Fasteners Inc", "Global Logistics", "Local Services" };
            var carriers = new[] { "UPS", "FedEx", "DHL", "Our Truck", "Customer Pickup" };
            var parts = new[] { "MMC-1001", "MMF-2002", "PART-3003", "SVC-MAINT", "MMC-5005" };

            for (int i = 0; i < 50; i++)
            {
                var orderDate = DateTime.Today.AddDays(-rnd.Next(0, 30));
                var desiredDate = orderDate.AddDays(rnd.Next(5, 20));
                var qty = rnd.Next(10, 1000);
                var rec = rnd.Next(0, qty + 1);
                var part = parts[rnd.Next(parts.Length)];
                var vendor = vendors[rnd.Next(vendors.Length)];
                var carrier = carriers[rnd.Next(carriers.Length)];

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
                    carrier
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

        /// <summary>
        /// Retrieves analytics data for receiving history and forecast.
        /// </summary>
        public async Task<Model_Dao_Result<Model_ReceivingAnalytics>> GetReceivingAnalyticsAsync()
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
            var startDate = new DateTime(DateTime.Now.Year, 1, 1); // YTD

            // 1. History Query (Approximation using PURC_ORDER_LINE.LAST_RECEIVED_DATE for simplicity)
            // Using COUNT(*) to count lines as "workload"
            string sqlHistory = @"
                SELECT 
                    POL.LAST_RECEIVED_DATE as [Date],
                    COUNT(*) as [Count],
                    CASE 
                        WHEN PO.CONSIGNMENT = 'Y' THEN 'Consignment'
                        WHEN PO.INTERNAL_ORDER = 'Y' THEN 'Internal'
                        WHEN POL.SERVICE_ID IS NOT NULL THEN 'Service'
                        WHEN POL.PART_ID LIKE 'MMC%' THEN 'MMC'
                        WHEN POL.PART_ID LIKE 'MMF%' THEN 'MMF'
                        ELSE 'Part'
                    END as [Type]
                FROM PURC_ORDER_LINE POL
                INNER JOIN PURCHASE_ORDER PO ON POL.PURC_ORDER_ID = PO.ID
                WHERE POL.TOTAL_RECEIVED_QTY > 0 
                  AND POL.LAST_RECEIVED_DATE >= @StartDate
                GROUP BY POL.LAST_RECEIVED_DATE, 
                    CASE 
                        WHEN PO.CONSIGNMENT = 'Y' THEN 'Consignment'
                        WHEN PO.INTERNAL_ORDER = 'Y' THEN 'Internal'
                        WHEN POL.SERVICE_ID IS NOT NULL THEN 'Service'
                        WHEN POL.PART_ID LIKE 'MMC%' THEN 'MMC'
                        WHEN POL.PART_ID LIKE 'MMF%' THEN 'MMF'
                        ELSE 'Part'
                    END
                ORDER BY POL.LAST_RECEIVED_DATE";

            // 2. Forecast Query (Open Lines)
            string sqlForecast = @"
                SELECT 
                    ISNULL(POL.PROMISE_DATE, POL.DESIRED_RECV_DATE) as [Date],
                    COUNT(*) as [Count],
                    CASE 
                        WHEN PO.CONSIGNMENT = 'Y' THEN 'Consignment'
                        WHEN PO.INTERNAL_ORDER = 'Y' THEN 'Internal'
                        WHEN POL.SERVICE_ID IS NOT NULL THEN 'Service'
                        WHEN POL.PART_ID LIKE 'MMC%' THEN 'MMC'
                        WHEN POL.PART_ID LIKE 'MMF%' THEN 'MMF'
                        ELSE 'Part'
                    END as [Type]
                FROM PURC_ORDER_LINE POL
                INNER JOIN PURCHASE_ORDER PO ON POL.PURC_ORDER_ID = PO.ID
                WHERE (POL.ORDER_QTY - POL.TOTAL_RECEIVED_QTY) > 0 
                  AND POL.LINE_STATUS <> 'C'
                  AND PO.STATUS <> 'C'
                  AND ISNULL(POL.PROMISE_DATE, POL.DESIRED_RECV_DATE) >= @Today
                GROUP BY ISNULL(POL.PROMISE_DATE, POL.DESIRED_RECV_DATE),
                    CASE 
                        WHEN PO.CONSIGNMENT = 'Y' THEN 'Consignment'
                        WHEN PO.INTERNAL_ORDER = 'Y' THEN 'Internal'
                        WHEN POL.SERVICE_ID IS NOT NULL THEN 'Service'
                        WHEN POL.PART_ID LIKE 'MMC%' THEN 'MMC'
                        WHEN POL.PART_ID LIKE 'MMF%' THEN 'MMF'
                        ELSE 'Part'
                    END
                ORDER BY [Date]";

            try
            {
                using (var connection = new SqlConnection(GetConnectionString()))
                {
                    await connection.OpenAsync();

                    // Execute History
                    using (var command = new SqlCommand(sqlHistory, connection))
                    {
                        command.Parameters.AddWithValue("@StartDate", startDate);
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
                                        Type = reader["Type"].ToString() ?? "Part"
                                    });
                                }
                            }
                        }
                    }

                    // Execute Forecast
                    using (var command = new SqlCommand(sqlForecast, connection))
                    {
                        command.Parameters.AddWithValue("@Today", DateTime.Today);
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
                                        Type = reader["Type"].ToString() ?? "Part"
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
                        Type = types[rnd.Next(types.Length)]
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
                        Type = types[rnd.Next(types.Length)]
                    });
                }
            }

            return new Model_Dao_Result<Model_ReceivingAnalytics> { IsSuccess = true, Data = analytics };
        }

        /// <summary>
        /// Retrieves inventory data based on search criteria.
        /// </summary>
        public async Task<Model_Dao_Result<DataTable>> GetInventoryAsync(string partNumber, string warehouse, string location, bool nonZeroOnly)
        {
            if (_useSampleData)
            {
                // Return sample data
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

                dt.Rows.Add("SAMPLE-PART-1", "Sample Description 1", "MAIN", "A-01-01", 100, 10, 90, "FG", "STEEL");
                dt.Rows.Add("SAMPLE-PART-2", "Sample Description 2", "MAIN", "B-02-02", 50, 0, 50, "RM", "ALUM");
                
                return new Model_Dao_Result<DataTable> { IsSuccess = true, Data = dt };
            }

            if (string.IsNullOrEmpty(_userName) || string.IsNullOrEmpty(_password))
            {
#if DEBUG
                // Fallback to sample data in debug
                return GetInventoryAsync(partNumber, warehouse, location, nonZeroOnly).Result; 
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
        /// Retrieves inventory audit history based on search criteria.
        /// </summary>
        public async Task<Model_Dao_Result<DataTable>> GetInventoryAuditAsync(DateTime startDate, DateTime endDate, string userId, string transType)
        {
            if (_useSampleData)
            {
                // Return sample data
                var dt = new DataTable();
                dt.Columns.Add("Date", typeof(DateTime));
                dt.Columns.Add("User");
                dt.Columns.Add("Type");
                dt.Columns.Add("Part Number");
                dt.Columns.Add("Warehouse");
                dt.Columns.Add("Location");
                dt.Columns.Add("Qty", typeof(decimal));
                dt.Columns.Add("Description");

                dt.Rows.Add(DateTime.Now.AddDays(-1), "JDOE", "ISS", "SAMPLE-PART-1", "MAIN", "A-01-01", -10, "Issue to WO");
                dt.Rows.Add(DateTime.Now.AddDays(-2), "BSMITH", "REC", "SAMPLE-PART-2", "MAIN", "B-02-02", 50, "PO Receipt");
                
                return new Model_Dao_Result<DataTable> { IsSuccess = true, Data = dt };
            }

            if (string.IsNullOrEmpty(_userName) || string.IsNullOrEmpty(_password))
            {
#if DEBUG
                // Fallback to sample data in debug
                return GetInventoryAuditAsync(startDate, endDate, userId, transType).Result; 
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
                    IT.TRANSACTION_DATE as [Date],
                    IT.USER_ID as [User],
                    IT.TYPE as [Type],
                    IT.PART_ID as [Part Number],
                    IT.WAREHOUSE_ID as [Warehouse],
                    IT.LOCATION_ID as [Location],
                    IT.QTY as [Qty],
                    IT.DESCRIPTION as [Description]
                FROM INVENTORY_TRANS IT
                WHERE IT.TRANSACTION_DATE BETWEEN @StartDate AND @EndDate";

            if (!string.IsNullOrWhiteSpace(userId))
            {
                sql += " AND IT.USER_ID LIKE @UserId";
            }

            if (!string.IsNullOrWhiteSpace(transType))
            {
                sql += " AND IT.TYPE LIKE @TransType";
            }

            sql += " ORDER BY IT.TRANSACTION_DATE DESC";

            try
            {
                using (var connection = new SqlConnection(GetConnectionString()))
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@StartDate", startDate);
                        command.Parameters.AddWithValue("@EndDate", endDate);

                        if (!string.IsNullOrWhiteSpace(userId))
                            command.Parameters.AddWithValue("@UserId", "%" + userId + "%");
                        
                        if (!string.IsNullOrWhiteSpace(transType))
                            command.Parameters.AddWithValue("@TransType", "%" + transType + "%");

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
                    ErrorMessage = $"Error fetching inventory audit: {ex.Message}"
                };
            }
        }
    }
}
