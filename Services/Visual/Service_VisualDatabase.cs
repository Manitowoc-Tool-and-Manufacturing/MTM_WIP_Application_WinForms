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
