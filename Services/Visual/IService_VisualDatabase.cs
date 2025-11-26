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

        /// <summary>
        /// Searches for dies based on part number or die number.
        /// </summary>
        /// <param name="searchTerm">The term to search for.</param>
        /// <param name="searchByPart">True to search by Part Number, False to search by Die Number.</param>
        /// <returns>DataTable containing search results.</returns>
        Task<Model_Dao_Result<DataTable>> SearchDiesAsync(string searchTerm, bool searchByPart);

        /// <summary>
        /// Retrieves the receiving schedule based on specified filters.
        /// </summary>
        Task<Model_Dao_Result<DataTable>> GetReceivingScheduleAsync(
            DateTime startDate, 
            DateTime endDate, 
            string dateFilterType, 
            bool includeClosed, 
            bool includeConsignment, 
            bool includeInternal, 
            bool includeService,
            string vendorFilter = "",
            string poFilter = "",
            bool mustHavePartNumber = false);

        /// <summary>
        /// Retrieves all line details for a specific Purchase Order.
        /// </summary>
        Task<Model_Dao_Result<DataTable>> GetPODetailsAsync(string poNumber);
    }
}
