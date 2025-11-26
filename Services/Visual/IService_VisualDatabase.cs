using System.Data;
using System.Threading.Tasks;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Services.Visual
{
    public interface IService_VisualDatabase
    {
        /// <summary>
        /// Tests the connection to the Infor Visual database using current credentials.
        /// </summary>
        Task<Model_Dao_Result<bool>> TestConnectionAsync();

        /// <summary>
        /// Executes a read-only query from an embedded resource file.
        /// </summary>
        /// <param name="category">The dashboard category to load query for.</param>
        /// <returns>DataTable containing the results.</returns>
        Task<Model_Dao_Result<DataTable>> GetDashboardDataAsync(Enum_VisualDashboardCategory category);
    }
}
