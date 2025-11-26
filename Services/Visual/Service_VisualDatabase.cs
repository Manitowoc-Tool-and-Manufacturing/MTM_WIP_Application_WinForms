using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Logging;
using System.Configuration;

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

        private string GetConnectionString()
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = _serverAddress,
                InitialCatalog = _databaseName,
                UserID = _userName,
                Password = _password,
                ConnectTimeout = 15,
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
            if (string.IsNullOrEmpty(_userName) || string.IsNullOrEmpty(_password))
            {
                return new Model_Dao_Result<bool>
                {
                    IsSuccess = false,
                    ErrorMessage = "Visual ERP credentials are not configured."
                };
            }

            try
            {
                using (var connection = new SqlConnection(GetConnectionString()))
                {
                    await connection.OpenAsync();
                    return new Model_Dao_Result<bool> { IsSuccess = true, Data = true };
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                return new Model_Dao_Result<bool>
                {
                    IsSuccess = false,
                    ErrorMessage = $"Connection failed: {ex.Message}",
                    Data = false
                };
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
        /// <returns>DataTable containing the receiving schedule.</returns>
        public async Task<Model_Dao_Result<DataTable>> GetReceivingScheduleAsync(
            DateTime startDate, 
            DateTime endDate, 
            string dateFilterType, 
            bool includeClosed, 
            bool includeConsignment, 
            bool includeInternal, 
            bool includeService)
        {
            if (string.IsNullOrEmpty(_userName) || string.IsNullOrEmpty(_password))
            {
                return new Model_Dao_Result<DataTable>
                {
                    IsSuccess = false,
                    ErrorMessage = "Visual ERP credentials are not configured."
                };
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
                    PO.INTERNAL_ORDER as [Internal]
                FROM PURCHASE_ORDER PO
                INNER JOIN PURC_ORDER_LINE POL ON PO.ID = POL.PURC_ORDER_ID
                LEFT JOIN VENDOR V ON PO.VENDOR_ID = V.ID
                WHERE 1=1
            ";

            // Date Filter Logic
            string dateCondition = "";
            switch (dateFilterType)
            {
                case "PO Desired Date":
                    dateCondition = " AND PO.DESIRED_RECV_DATE BETWEEN @StartDate AND @EndDate";
                    break;
                case "PO Promise Date":
                    dateCondition = " AND PO.PROMISE_DATE BETWEEN @StartDate AND @EndDate";
                    break;
                case "Line Desired Date":
                    dateCondition = " AND POL.DESIRED_RECV_DATE BETWEEN @StartDate AND @EndDate";
                    break;
                case "Line Promise Date":
                    dateCondition = " AND POL.PROMISE_DATE BETWEEN @StartDate AND @EndDate";
                    break;
                case "Any of the Above":
                default:
                    dateCondition = @" AND (
                        PO.DESIRED_RECV_DATE BETWEEN @StartDate AND @EndDate OR
                        PO.PROMISE_DATE BETWEEN @StartDate AND @EndDate OR
                        POL.DESIRED_RECV_DATE BETWEEN @StartDate AND @EndDate OR
                        POL.PROMISE_DATE BETWEEN @StartDate AND @EndDate
                    )";
                    break;
            }
            sql += dateCondition;

            // Checkbox Filters
            if (!includeClosed)
            {
                sql += " AND PO.STATUS <> 'C' AND POL.LINE_STATUS <> 'C'";
            }

            if (!includeConsignment)
            {
                sql += " AND (PO.CONSIGNMENT IS NULL OR PO.CONSIGNMENT <> 'Y')";
            }

            if (!includeInternal)
            {
                sql += " AND (PO.INTERNAL_ORDER IS NULL OR PO.INTERNAL_ORDER <> 'Y')";
            }

            if (!includeService)
            {
                sql += " AND POL.SERVICE_ID IS NULL";
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
                    ErrorMessage = $"Error retrieving receiving schedule: {ex.Message}"
                };
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
    }
}
