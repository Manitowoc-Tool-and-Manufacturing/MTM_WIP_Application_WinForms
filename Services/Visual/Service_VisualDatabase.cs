using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Logging;

namespace MTM_WIP_Application_Winforms.Services.Visual
{
    /// <summary>
    /// Service for interacting with the Infor Visual ERP database.
    /// </summary>
    public class Service_VisualDatabase : IService_VisualDatabase
    {
        private const string SERVER_ADDRESS = "VISUALSQL"; 
        private const string DATABASE_NAME = "VMFG"; 

        private string? _userName => Model_Application_Variables.VisualUserName;
        private string? _password => Model_Application_Variables.VisualPassword;

        private string GetConnectionString()
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = SERVER_ADDRESS,
                InitialCatalog = DATABASE_NAME,
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
