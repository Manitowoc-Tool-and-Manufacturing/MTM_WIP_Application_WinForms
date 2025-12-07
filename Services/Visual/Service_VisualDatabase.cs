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
                    ErrorMessage = $"Connection failed: {GetFriendlyErrorMessage(ex)}",
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
                    ErrorMessage = $"Error retrieving data: {GetFriendlyErrorMessage(ex)}"
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
                // Search by Part Number -> Find Die
                // Updated to use both Legacy (USER_5) and Engineering Master (BOM) links
                // This handles cases where dies are linked via the Engineering Master (Subordinate parts)
                sql = @"
                    SELECT DISTINCT
                        P.ID as [Part Number],
                        P.DESCRIPTION as [Description],
                        D.ID as [Die Number],
                        D.USER_2 as [Die Location],
                        P.USER_7 as [Customer],
                        P.USER_9 as [Coil]
                    FROM PART P
                    LEFT JOIN PART D ON (
                        D.ID LIKE 'FGT%-01' 
                        AND D.ID <> 'FGT0001-01'
                        AND (
                            -- Link 1: Legacy USER_5 (Die points to Part)
                            (D.USER_5 = P.ID)
                            OR
                            -- Link 2: Engineering Master BOM (Part has Die as component)
                            EXISTS (
                                SELECT 1 FROM WORK_ORDER WO
                                JOIN REQUIREMENT R ON R.WORKORDER_BASE_ID = WO.BASE_ID 
                                                   AND R.WORKORDER_LOT_ID = WO.LOT_ID 
                                                   AND R.WORKORDER_SPLIT_ID = WO.SPLIT_ID 
                                                   AND R.WORKORDER_SUB_ID = WO.SUB_ID
                                WHERE WO.PART_ID = P.ID 
                                AND WO.TYPE = 'M' -- Engineering Master
                                AND R.PART_ID = D.ID
                            )
                        )
                    )
                    WHERE P.ID LIKE @SearchTerm";
            }
            else
            {
                // Search by Die Number (FGT) -> Find Part
                // Updated to use both Legacy (USER_5) and Engineering Master (BOM) links
                sql = @"
                    SELECT DISTINCT
                        D.ID as [Die Number],
                        D.DESCRIPTION as [Description],
                        P.ID as [Part Number],
                        D.USER_2 as [Die Location],
                        D.USER_9 as [Coil]
                    FROM PART D
                    LEFT JOIN PART P ON (
                        -- Link 1: Legacy USER_5 (Die points to Part)
                        (P.ID = D.USER_5)
                        OR
                        -- Link 2: Engineering Master BOM (Part has Die as component)
                        EXISTS (
                            SELECT 1 FROM WORK_ORDER WO
                            JOIN REQUIREMENT R ON R.WORKORDER_BASE_ID = WO.BASE_ID 
                                               AND R.WORKORDER_LOT_ID = WO.LOT_ID 
                                               AND R.WORKORDER_SPLIT_ID = WO.SPLIT_ID 
                                               AND R.WORKORDER_SUB_ID = WO.SUB_ID
                            WHERE WO.PART_ID = P.ID 
                            AND WO.TYPE = 'M' -- Engineering Master
                            AND R.PART_ID = D.ID
                        )
                    )
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
        /// Retrieves coil/flatstock information for a given part number.
        /// </summary>
        /// <param name="partNumber">The part number to search for (e.g., MMC or MMF).</param>
        /// <returns>
        /// Model_Dao_Result containing the coil/flatstock details.
        /// </returns>
        public async Task<Model_Dao_Result<Model_Visual_CoilFlatstock>> GetCoilFlatstockInfoAsync(string partNumber)
        {
            if (string.IsNullOrEmpty(_userName) || string.IsNullOrEmpty(_password))
            {
                return new Model_Dao_Result<Model_Visual_CoilFlatstock>
                {
                    IsSuccess = false,
                    ErrorMessage = "Visual ERP credentials are not configured."
                };
            }

            string sql = @"
                SELECT 
                    P.ID, 
                    P.DESCRIPTION, 
                    P.USER_1, 
                    P.USER_2, 
                    P.USER_3, 
                    P.USER_4, 
                    P.USER_5, 
                    P.USER_6, 
                    P.USER_7, 
                    P.USER_8, 
                    P.USER_9, 
                    P.USER_10,
                    PS.BACKFLUSH_LOC_ID
                FROM PART P
                LEFT JOIN PART_SITE PS ON P.ID = PS.PART_ID
                WHERE P.ID = @PartNumber";

            try
            {
                using (var connection = new SqlConnection(GetConnectionString()))
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@PartNumber", partNumber);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                var model = new Model_Visual_CoilFlatstock
                                {
                                    PartId = reader["ID"]?.ToString() ?? string.Empty,
                                    Description = reader["DESCRIPTION"]?.ToString() ?? string.Empty,
                                    Thickness = reader["USER_1"]?.ToString() ?? string.Empty,
                                    Width = reader["USER_2"]?.ToString() ?? string.Empty,
                                    Length = reader["USER_3"]?.ToString() ?? string.Empty,
                                    Gauge = reader["USER_4"]?.ToString() ?? string.Empty,
                                    WhereUsed = reader["USER_5"]?.ToString() ?? string.Empty,
                                    Progression = reader["USER_6"]?.ToString() ?? string.Empty,
                                    Customer = reader["USER_7"]?.ToString() ?? string.Empty,
                                    ScrapLocation = reader["USER_8"]?.ToString() ?? string.Empty,
                                    GenericType = reader["USER_9"]?.ToString() ?? string.Empty,
                                    DetailedType = reader["USER_10"]?.ToString() ?? string.Empty,
                                    AutoIssueLocation = reader["BACKFLUSH_LOC_ID"]?.ToString() ?? string.Empty
                                };

                                return Model_Dao_Result<Model_Visual_CoilFlatstock>.Success(model);
                            }
                            else
                            {
                                return Model_Dao_Result<Model_Visual_CoilFlatstock>.Failure("Part not found.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                return Model_Dao_Result<Model_Visual_CoilFlatstock>.Failure($"Error retrieving coil info: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a list of parent parts where the specified part is used as a component (BOM lookup).
        /// </summary>
        /// <param name="partId">The component part ID.</param>
        /// <returns>DataTable containing parent part details.</returns>
        public async Task<Model_Dao_Result<DataTable>> GetWhereUsedAsync(string partId)
        {
            if (string.IsNullOrEmpty(_userName) || string.IsNullOrEmpty(_password))
            {
                return new Model_Dao_Result<DataTable>
                {
                    IsSuccess = false,
                    ErrorMessage = "Visual ERP credentials are not configured."
                };
            }

            string sql = @"
                SELECT DISTINCT
                    WO.PART_ID as [Parent Part],
                    P.DESCRIPTION as [Description],
                    R.QTY_PER as [Qty Per]
                FROM REQUIREMENT R
                JOIN WORK_ORDER WO ON R.WORKORDER_BASE_ID = WO.BASE_ID 
                                   AND R.WORKORDER_LOT_ID = WO.LOT_ID 
                                   AND R.WORKORDER_SPLIT_ID = WO.SPLIT_ID 
                                   AND R.WORKORDER_SUB_ID = WO.SUB_ID
                JOIN PART P ON WO.PART_ID = P.ID
                WHERE R.PART_ID = @PartId
                AND WO.TYPE = 'M' -- Engineering Master
                ORDER BY WO.PART_ID";

            try
            {
                using (var connection = new SqlConnection(GetConnectionString()))
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@PartId", partId);

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
                    ErrorMessage = $"Error retrieving Where Used data: {ex.Message}"
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
                    POL.TOTAL_AMT_ORDERED as [Total Amount],
                    CAST(PLB.BITS AS VARCHAR(MAX)) as [Specs]
                FROM PURC_ORDER_LINE POL
                LEFT JOIN PART P ON POL.PART_ID = P.ID
                LEFT JOIN PURC_LINE_BINARY PLB ON POL.PURC_ORDER_ID = PLB.PURC_ORDER_ID 
                                               AND POL.LINE_NO = PLB.PURC_ORDER_LINE_NO
                                               AND PLB.TYPE = 'S'
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

        /// <summary>
        /// Retrieves a list of all Die IDs from the Visual database (FGT%-01).
        /// </summary>
        public async Task<Model_Dao_Result<List<string>>> GetDieIdsAsync()
        {
            if (_useSampleData)
            {
                return new Model_Dao_Result<List<string>> { IsSuccess = true, Data = new List<string> { "FGT1001-01", "FGT1002-01" } };
            }

            if (string.IsNullOrEmpty(_userName) || string.IsNullOrEmpty(_password))
            {
                return new Model_Dao_Result<List<string>>
                {
                    IsSuccess = false,
                    ErrorMessage = "Visual ERP credentials are not configured."
                };
            }

            string sql = "SELECT DISTINCT ID FROM PART WHERE ID LIKE 'FGT%-01' AND ID <> 'FGT0001-01' OR ID LIKE 'MMC%' OR ID LIKE 'MMF%' ORDER BY ID";

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
                    ErrorMessage = $"Error retrieving Die IDs: {ex.Message}"
                };
            }
        }

        /// <summary>
        /// Retrieves a list of all Coil/Flatstock Part IDs from the Visual database (MMC% or MMF%).
        /// </summary>
        public async Task<Model_Dao_Result<List<string>>> GetCoilFlatstockPartIdsAsync()
        {
            if (_useSampleData)
            {
                return new Model_Dao_Result<List<string>> { IsSuccess = true, Data = new List<string> { "MMC-1001", "MMF-2002" } };
            }

            if (string.IsNullOrEmpty(_userName) || string.IsNullOrEmpty(_password))
            {
                return new Model_Dao_Result<List<string>>
                {
                    IsSuccess = false,
                    ErrorMessage = "Visual ERP credentials are not configured."
                };
            }

            string sql = "SELECT DISTINCT ID FROM PART WHERE ID LIKE 'MMC%' OR ID LIKE 'MMF%' ORDER BY ID";

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
                    ErrorMessage = $"Error retrieving Coil/Flatstock IDs: {ex.Message}"
                };
            }
        }

        /// <summary>
        /// Retrieves a list of all Location IDs from the Visual database.
        /// </summary>
        public async Task<Model_Dao_Result<List<string>>> GetLocationIdsAsync()
        {
            return await GetDistinctColumnValuesAsync("ID", "LOCATION");
        }

        /// <summary>
        /// Retrieves a list of all Warehouse IDs from the Visual database.
        /// </summary>
        public async Task<Model_Dao_Result<List<string>>> GetWarehouseIdsAsync()
        {
            return await GetDistinctColumnValuesAsync("ID", "WAREHOUSE");
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
        private string GetFriendlyErrorMessage(Exception ex)
        {
            if (ex.Message.Contains("network-related") || 
                ex.Message.Contains("error: 40") || 
                ex.Message.Contains("error: 26") || 
                ex.Message.Contains("The server was not found"))
            {
                return "Unable to connect to the Visual ERP Server. Please verify your network connection.";
            }
            return ex.Message;
        }

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
                var receivedBy = rec > 0 ? users[rnd.Next(users.Length)] : string.Empty;

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
            dt.Columns.Add("Specs");

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
                    Math.Round(price * qty, 2),
                    $"Sample Specs for Line {i}\n- Spec 1\n- Spec 2"
                );
            }

            return new Model_Dao_Result<DataTable> { IsSuccess = true, Data = dt };
        }

        /// <summary>
        /// Retrieves a list of distinct users who have performed transactions within the specified date range.
        /// </summary>
        /// <param name="start">Start date.</param>
        /// <param name="end">End date.</param>
        /// <returns>List of user IDs.</returns>
        public async Task<Model_Dao_Result<List<string>>> GetDistinctUsersForAnalyticsAsync(DateTime start, DateTime end)
        {
            if (_useSampleData)
            {
                return new Model_Dao_Result<List<string>>
                {
                    IsSuccess = true,
                    Data = new List<string> { "SAMPLE_USER_1", "SAMPLE_USER_2", "SAMPLE_USER_3" }
                };
            }

            string sql = @"
                SELECT DISTINCT USER_ID 
                FROM INVENTORY_TRANS 
                WHERE CAST(COALESCE(CREATE_DATE, TRANSACTION_DATE) AS DATE) >= @Start 
                AND CAST(COALESCE(CREATE_DATE, TRANSACTION_DATE) AS DATE) <= @End
                AND USER_ID IS NOT NULL
                ORDER BY USER_ID";

            try
            {
                using (var connection = new SqlConnection(GetConnectionString()))
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Start", start.Date);
                        command.Parameters.AddWithValue("@End", end.Date);

                        var list = new List<string>();
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                if (!reader.IsDBNull(0))
                                    list.Add(reader.GetString(0));
                            }
                        }
                        return new Model_Dao_Result<List<string>> { IsSuccess = true, Data = list };
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                return new Model_Dao_Result<List<string>>
                {
                    IsSuccess = false,
                    ErrorMessage = $"Error retrieving users: {GetFriendlyErrorMessage(ex)}"
                };
            }
        }

        /// <summary>
        /// Retrieves detailed analytics data for selected users within a date range.
        /// </summary>
        /// <param name="start">Start date.</param>
        /// <param name="end">End date.</param>
        /// <param name="userIds">List of user IDs to include.</param>
        /// <returns>DataTable with analytics columns.</returns>
        public async Task<Model_Dao_Result<DataTable>> GetUserAnalyticsDataAsync(DateTime start, DateTime end, List<string> userIds)
        {
            if (_useSampleData)
            {
                // Return sample data structure
                var dt = new DataTable();
                dt.Columns.Add("User", typeof(string));
                dt.Columns.Add("Type", typeof(string));
                dt.Columns.Add("Part", typeof(string));
                dt.Columns.Add("Qty", typeof(decimal));
                dt.Columns.Add("Date", typeof(DateTime));
                dt.Columns.Add("FromLoc", typeof(string));
                dt.Columns.Add("ToLoc", typeof(string));
                dt.Columns.Add("WorkOrder", typeof(string));

                foreach (var user in userIds)
                {
                    dt.Rows.Add(user, "Work Order", "PART-A", 10, DateTime.Now, "MAIN", null, "WO-123");
                    dt.Rows.Add(user, "Location Transfer", "PART-B", 5, DateTime.Now, "LOC-A", "LOC-B", null);
                    dt.Rows.Add(user, "Adjusted In", "PART-C", 2, DateTime.Now, "LOC-C", null, null);
                }
                return new Model_Dao_Result<DataTable> { IsSuccess = true, Data = dt };
            }

            if (userIds == null || userIds.Count == 0)
            {
                return new Model_Dao_Result<DataTable> { IsSuccess = true, Data = new DataTable() };
            }

            // Build IN clause dynamically
            var userParams = new List<string>();
            for (int i = 0; i < userIds.Count; i++)
            {
                userParams.Add($"@User{i}");
            }
            string inClause = string.Join(",", userParams);

            // Simplified query to fetch raw data
            string sql = $@"
                SELECT 
                    TRANSACTION_ID, 
                    USER_ID, 
                    PART_ID, 
                    TYPE, 
                    QTY, 
                    CREATE_DATE, 
                    LOCATION_ID, 
                    WORKORDER_BASE_ID, 
                    CUST_ORDER_ID,
                    TRANSFER_TRANS_ID
                FROM INVENTORY_TRANS
                WHERE 
                    CREATE_DATE >= @Start AND CREATE_DATE <= @End
                    AND USER_ID IN ({inClause})
                ORDER BY CREATE_DATE DESC";

            try
            {
                var rawData = new DataTable();
                using (var connection = new SqlConnection(GetConnectionString()))
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 60;
                        command.Parameters.AddWithValue("@Start", start);
                        command.Parameters.AddWithValue("@End", end);
                        for (int i = 0; i < userIds.Count; i++)
                        {
                            command.Parameters.AddWithValue($"@User{i}", userIds[i]);
                        }

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            rawData.Load(reader);
                        }
                    }
                }

                // Process data in memory
                var resultDt = new DataTable();
                resultDt.Columns.Add("User", typeof(string));
                resultDt.Columns.Add("Type", typeof(string));
                resultDt.Columns.Add("Part", typeof(string));
                resultDt.Columns.Add("Qty", typeof(decimal));
                resultDt.Columns.Add("Date", typeof(DateTime));
                resultDt.Columns.Add("FromLoc", typeof(string));
                resultDt.Columns.Add("ToLoc", typeof(string));
                resultDt.Columns.Add("WorkOrder", typeof(string));

                var processedIds = new HashSet<int>();
                var rows = rawData.AsEnumerable()
                    .Select(r => new
                    {
                        Id = r.Field<int>("TRANSACTION_ID"),
                        User = r.Field<string>("USER_ID") ?? "",
                        Type = r.Field<string>("TYPE") ?? "",
                        Part = r.Field<string>("PART_ID") ?? "",
                        WO = r.Field<string>("WORKORDER_BASE_ID"),
                        CO = r.Field<string>("CUST_ORDER_ID"),
                        Qty = r.Field<decimal>("QTY"),
                        Date = r.Field<DateTime>("CREATE_DATE"),
                        Loc = r.Field<string>("LOCATION_ID") ?? ""
                    })
                    .ToList();

                // 1. Process Receipts (Work Orders)
                for (int i = 0; i < rows.Count; i++)
                {
                    var t = rows[i];
                    if (processedIds.Contains(t.Id)) continue;

                    if (t.Type == "I" && !string.IsNullOrEmpty(t.WO))
                    {
                        AddAnalyticsRow(resultDt, t.User, t.Part, "Work Order", t.Qty, t.Date, null, t.Loc, t.WO);
                        processedIds.Add(t.Id);
                    }
                }

                // 2. Process Shipments (Customer Orders)
                for (int i = 0; i < rows.Count; i++)
                {
                    var t = rows[i];
                    if (processedIds.Contains(t.Id)) continue;

                    if (t.Type == "O" && !string.IsNullOrEmpty(t.CO))
                    {
                        AddAnalyticsRow(resultDt, t.User, t.Part, "Adjusted Out", t.Qty, t.Date, t.Loc, null, null);
                        processedIds.Add(t.Id);
                    }
                }

                // 3. Process Transfers (Pair OUT with IN)
                for (int i = 0; i < rows.Count; i++)
                {
                    var tOut = rows[i];
                    if (processedIds.Contains(tOut.Id)) continue;

                    if (tOut.Type == "O")
                    {
                        int bestMatchIndex = -1;
                        double minTimeDiff = double.MaxValue;

                        for (int j = 0; j < rows.Count; j++)
                        {
                            if (i == j || processedIds.Contains(rows[j].Id)) continue;
                            var tIn = rows[j];

                            if (tIn.Type == "I" && 
                                string.IsNullOrEmpty(tIn.WO) && 
                                tIn.Part == tOut.Part &&
                                Math.Abs(tIn.Qty) == Math.Abs(tOut.Qty))
                            {
                                var diff = Math.Abs((tIn.Date - tOut.Date).TotalSeconds);
                                if (diff < 300)
                                {
                                    if (diff < minTimeDiff)
                                    {
                                        minTimeDiff = diff;
                                        bestMatchIndex = j;
                                    }
                                }
                            }
                        }

                        if (bestMatchIndex != -1)
                        {
                            var tIn = rows[bestMatchIndex];
                            AddAnalyticsRow(resultDt, tOut.User, tOut.Part, "Location Transfer", tOut.Qty, tOut.Date, tOut.Loc, tIn.Loc, null);
                            processedIds.Add(tOut.Id);
                            processedIds.Add(tIn.Id);
                        }
                    }
                }

                // 4. Process Remaining
                for (int i = 0; i < rows.Count; i++)
                {
                    var t = rows[i];
                    if (processedIds.Contains(t.Id)) continue;

                    if (t.Type == "I")
                        AddAnalyticsRow(resultDt, t.User, t.Part, "Adjusted In", t.Qty, t.Date, null, t.Loc, null);
                    else
                        AddAnalyticsRow(resultDt, t.User, t.Part, "Adjusted Out", t.Qty, t.Date, t.Loc, null, null);
                }

                return new Model_Dao_Result<DataTable> { IsSuccess = true, Data = resultDt };
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                return new Model_Dao_Result<DataTable>
                {
                    IsSuccess = false,
                    ErrorMessage = $"Error retrieving analytics data: {ex.Message}"
                };
            }
        }

        private void AddAnalyticsRow(DataTable dt, string user, string partId, string baseCategory, decimal qty, DateTime date, string? fromLoc, string? toLoc, string? wo)
        {
            string category = baseCategory;
            if (partId.StartsWith("MMF")) category = "Flatstock";
            else if (partId.StartsWith("MMC")) category = "Coil";
            
            dt.Rows.Add(user, category, partId, qty, date, fromLoc, toLoc, wo);
        }

        /// <summary>
        /// Retrieves transaction history for shift calculation (last 30 days).
        /// </summary>
        /// <returns>DataTable with USER_ID and TRANSACTION_DATE.</returns>
        public async Task<Model_Dao_Result<DataTable>> GetUserShiftDataAsync()
        {
            if (_useSampleData)
            {
                // Return sample data
                var dt = new DataTable();
                dt.Columns.Add("USER_ID", typeof(string));
                dt.Columns.Add("TRANSACTION_DATE", typeof(DateTime));
                
                var rnd = new Random();
                var users = new[] { "JDOE", "BSMITH", "MJONES" };
                for (int i = 0; i < 100; i++)
                {
                    dt.Rows.Add(users[rnd.Next(users.Length)], DateTime.Now.AddDays(-rnd.Next(30)));
                }
                return new Model_Dao_Result<DataTable> { IsSuccess = true, Data = dt };
            }

            if (string.IsNullOrEmpty(_userName) || string.IsNullOrEmpty(_password))
            {
                return new Model_Dao_Result<DataTable>
                {
                    IsSuccess = false,
                    ErrorMessage = "Visual ERP credentials are not configured."
                };
            }

            string sql = @"
                SELECT USER_ID, TRANSACTION_DATE
                FROM INVENTORY_TRANS
                WHERE TRANSACTION_DATE >= DATEADD(day, -30, GETDATE())
                ORDER BY USER_ID, TRANSACTION_DATE DESC";

            try
            {
                using (var connection = new SqlConnection(GetConnectionString()))
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand(sql, connection))
                    {
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
                    ErrorMessage = $"Error retrieving shift data: {ex.Message}"
                };
            }
        }

        /// <summary>
        /// Retrieves full names for all employees.
        /// </summary>
        /// <returns>DataTable with USER_ID, FIRST_NAME, LAST_NAME.</returns>
        public async Task<Model_Dao_Result<DataTable>> GetUserFullNamesAsync()
        {
            if (_useSampleData)
            {
                var dt = new DataTable();
                dt.Columns.Add("USER_ID", typeof(string));
                dt.Columns.Add("FIRST_NAME", typeof(string));
                dt.Columns.Add("LAST_NAME", typeof(string));
                dt.Rows.Add("JDOE", "John", "Doe");
                dt.Rows.Add("BSMITH", "Bob", "Smith");
                dt.Rows.Add("MJONES", "Mary", "Jones");
                return new Model_Dao_Result<DataTable> { IsSuccess = true, Data = dt };
            }

            if (string.IsNullOrEmpty(_userName) || string.IsNullOrEmpty(_password))
            {
                return new Model_Dao_Result<DataTable>
                {
                    IsSuccess = false,
                    ErrorMessage = "Visual ERP credentials are not configured."
                };
            }

            string sql = @"
                SELECT USER_ID, FIRST_NAME, LAST_NAME 
                FROM EMPLOYEE
                WHERE USER_ID IS NOT NULL AND USER_ID <> ''";

            try
            {
                using (var connection = new SqlConnection(GetConnectionString()))
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand(sql, connection))
                    {
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
                    ErrorMessage = $"Error retrieving user names: {ex.Message}"
                };
            }
        }

        /// <summary>
        /// Retrieves material handler statistics for scoring.
        /// </summary>
        public async Task<Model_Dao_Result<DataTable>> GetMaterialHandlerStatsAsync(DateTime startDate, DateTime endDate)
        {
            if (_useSampleData)
            {
                var dt = new DataTable();
                dt.Columns.Add("User", typeof(string));
                dt.Columns.Add("TransactionType", typeof(string));
                dt.Columns.Add("TransactionCount", typeof(int));
                
                dt.Rows.Add("JDOE", "Adjusted In", 50);
                dt.Rows.Add("JDOE", "Adjusted Out", 30);
                dt.Rows.Add("BSMITH", "Location Transfer", 80);
                
                return new Model_Dao_Result<DataTable> { IsSuccess = true, Data = dt };
            }

            if (string.IsNullOrEmpty(_userName) || string.IsNullOrEmpty(_password))
            {
                return new Model_Dao_Result<DataTable>
                {
                    IsSuccess = false,
                    ErrorMessage = "Visual ERP credentials are not configured."
                };
            }

            // Fetch raw data for processing
            // Added QTY and CUST_ORDER_ID for proper matching logic
            string sql = @"
                SELECT 
                    TRANSACTION_ID,
                    USER_ID,
                    TYPE,
                    PART_ID,
                    WORKORDER_BASE_ID,
                    CUST_ORDER_ID,
                    QTY,
                    CREATE_DATE
                FROM INVENTORY_TRANS
                WHERE TRANSACTION_DATE >= @StartDate AND TRANSACTION_DATE <= @EndDate
                ORDER BY CREATE_DATE DESC";

            try
            {
                var rawData = new DataTable();
                using (var connection = new SqlConnection(GetConnectionString()))
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.CommandTimeout = 120;
                        command.Parameters.AddWithValue("@StartDate", startDate);
                        command.Parameters.AddWithValue("@EndDate", endDate);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            rawData.Load(reader);
                        }
                    }
                }

                // Process data in memory using Helper_VisualLifecycle logic
                var stats = new Dictionary<(string User, string Category), int>();
                var processedIds = new HashSet<int>();
                
                var rows = rawData.AsEnumerable()
                    .Select(r => new
                    {
                        Id = r.Field<int>("TRANSACTION_ID"),
                        User = r.Field<string>("USER_ID") ?? "",
                        Type = r.Field<string>("TYPE") ?? "",
                        Part = r.Field<string>("PART_ID") ?? "",
                        WO = r.Field<string>("WORKORDER_BASE_ID"),
                        CO = r.Field<string>("CUST_ORDER_ID"),
                        Qty = r.Field<decimal>("QTY"),
                        Date = r.Field<DateTime>("CREATE_DATE")
                    })
                    .ToList();

                // 1. Process Receipts (Work Orders)
                for (int i = 0; i < rows.Count; i++)
                {
                    var t = rows[i];
                    if (processedIds.Contains(t.Id)) continue;

                    if (t.Type == "I" && !string.IsNullOrEmpty(t.WO))
                    {
                        AddStat(stats, t.User, t.Part, "Work Order");
                        processedIds.Add(t.Id);
                    }
                }

                // 2. Process Shipments (Customer Orders)
                for (int i = 0; i < rows.Count; i++)
                {
                    var t = rows[i];
                    if (processedIds.Contains(t.Id)) continue;

                    if (t.Type == "O" && !string.IsNullOrEmpty(t.CO))
                    {
                        AddStat(stats, t.User, t.Part, "Adjusted Out"); // Shipments count as Out
                        processedIds.Add(t.Id);
                    }
                }

                // 3. Process Transfers (Pair OUT with IN)
                for (int i = 0; i < rows.Count; i++)
                {
                    var tOut = rows[i];
                    if (processedIds.Contains(tOut.Id)) continue;

                    if (tOut.Type == "O")
                    {
                        // Look for matching IN
                        // Same Part, Same Abs(Qty), Time diff < 5 mins
                        // Note: rows are sorted by Date DESC
                        int bestMatchIndex = -1;
                        double minTimeDiff = double.MaxValue;

                        for (int j = 0; j < rows.Count; j++)
                        {
                            if (i == j || processedIds.Contains(rows[j].Id)) continue;
                            var tIn = rows[j];

                            if (tIn.Type == "I" && 
                                string.IsNullOrEmpty(tIn.WO) && // Not a WO
                                tIn.Part == tOut.Part &&
                                Math.Abs(tIn.Qty) == Math.Abs(tOut.Qty))
                            {
                                var diff = Math.Abs((tIn.Date - tOut.Date).TotalSeconds);
                                if (diff < 300) // 5 minutes
                                {
                                    if (diff < minTimeDiff)
                                    {
                                        minTimeDiff = diff;
                                        bestMatchIndex = j;
                                    }
                                }
                            }
                        }

                        if (bestMatchIndex != -1)
                        {
                            // Found a pair
                            AddStat(stats, tOut.User, tOut.Part, "Location Transfer");
                            processedIds.Add(tOut.Id);
                            processedIds.Add(rows[bestMatchIndex].Id);
                        }
                    }
                }

                // 4. Process Remaining
                for (int i = 0; i < rows.Count; i++)
                {
                    var t = rows[i];
                    if (processedIds.Contains(t.Id)) continue;

                    string category = t.Type == "I" ? "Adjusted In" : "Adjusted Out";
                    AddStat(stats, t.User, t.Part, category);
                }

                // Convert to DataTable
                var dt = new DataTable();
                dt.Columns.Add("User", typeof(string));
                dt.Columns.Add("TransactionType", typeof(string));
                dt.Columns.Add("TransactionCount", typeof(int));
                dt.Columns.Add("PartCategory", typeof(string)); 
                dt.Columns.Add("HasWorkOrder", typeof(string));

                foreach (var kvp in stats)
                {
                    dt.Rows.Add(kvp.Key.User, kvp.Key.Category, kvp.Value, "", "");
                }

                return new Model_Dao_Result<DataTable> { IsSuccess = true, Data = dt };
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                return new Model_Dao_Result<DataTable>
                {
                    IsSuccess = false,
                    ErrorMessage = $"Error retrieving material handler stats: {ex.Message}"
                };
            }
        }

        private void AddStat(Dictionary<(string User, string Category), int> stats, string user, string partId, string baseCategory)
        {
            string category = baseCategory;
            if (partId.StartsWith("MMF")) category = "Flatstock";
            else if (partId.StartsWith("MMC")) category = "Coil";
            
            var key = (user, category);
            if (!stats.ContainsKey(key)) stats[key] = 0;
            stats[key]++;
        }
        #endregion

    }
}
