using System;
using System.Data;
using System.Threading.Tasks;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Services.Visual
{

    #region Methods
    /// <summary>
    /// Provides Visual database access operations for dashboards, analytics, and inventory.
    /// </summary>
    public interface IService_VisualDatabase
    {
        /// <summary>
        /// Tests the connection to the Infor Visual database using current credentials.
        /// </summary>
        /// <returns>
        /// Model_Dao_Result containing true when the connection succeeds.
        /// Check IsSuccess before accessing Data.
        /// </returns>
        Task<Model_Dao_Result<bool>> TestConnectionAsync();

        /// <summary>
        /// Executes a read-only query from an embedded resource file.
        /// </summary>
        /// <param name="category">The dashboard category that determines the query resource.</param>
        /// <returns>
        /// Model_Dao_Result containing a DataTable of dashboard results.
        /// Check IsSuccess before accessing Data.
        /// </returns>
        Task<Model_Dao_Result<DataTable>> GetDashboardDataAsync(Enum_VisualDashboardCategory category);

        /// <summary>
        /// Searches for dies based on part number or die number.
        /// </summary>
        /// <param name="searchTerm">The part or die identifier to search for.</param>
        /// <param name="searchByPart">True to search by Part Number; false to search by Die Number.</param>
        /// <returns>
        /// Model_Dao_Result containing search results in a DataTable.
        /// Check IsSuccess before accessing Data.
        /// </returns>
        Task<Model_Dao_Result<DataTable>> SearchDiesAsync(string searchTerm, bool searchByPart);

        /// <summary>
        /// Retrieves the receiving schedule based on specified filters.
        /// </summary>
        /// <param name="startDate">Start date for the schedule range.</param>
        /// <param name="endDate">End date for the schedule range.</param>
        /// <param name="dateFilterType">Type of date column used for filtering.</param>
        /// <param name="includeClosed">True to include closed purchase orders.</param>
        /// <param name="includeConsignment">True to include consignment orders.</param>
        /// <param name="includeInternal">True to include internal orders.</param>
        /// <param name="includeService">True to include outside service orders.</param>
        /// <param name="vendorFilter">Optional vendor filter string.</param>
        /// <param name="poFilter">Optional purchase order number filter.</param>
        /// <param name="mustHavePartNumber">True to restrict results to rows with part numbers.</param>
        /// <returns>
        /// Model_Dao_Result containing the filtered schedule DataTable.
        /// Check IsSuccess before accessing Data.
        /// </returns>
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
        /// <param name="poNumber">The purchase order number to inspect.</param>
        /// <returns>
        /// Model_Dao_Result containing a DataTable of PO line details.
        /// Check IsSuccess before accessing Data.
        /// </returns>
        Task<Model_Dao_Result<DataTable>> GetPODetailsAsync(string poNumber);

        /// <summary>
        /// Retrieves analytics data for receiving history and forecast.
        /// </summary>
        /// <returns>
        /// Model_Dao_Result containing receiving analytics data.
        /// Check IsSuccess before accessing Data.
        /// </returns>
        Task<Model_Dao_Result<Model_ReceivingAnalytics>> GetReceivingAnalyticsAsync();

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
        Task<Model_Dao_Result<DataTable>> GetInventoryAsync(string partNumber, string warehouse, string location, bool nonZeroOnly);

        /// <summary>
        /// Retrieves transaction history based on flexible filter criteria.
        /// </summary>
        /// <param name="filter">The filter criteria object.</param>
        /// <returns>
        /// Model_Dao_Result containing the transaction DataTable.
        /// Check IsSuccess before accessing Data.
        /// </returns>
        Task<Model_Dao_Result<DataTable>> GetTransactionsAsync(Model_VisualTransactionFilter filter);

        /// <summary>
        /// Retrieves a list of all User IDs from the transaction history.
        /// </summary>
        Task<Model_Dao_Result<List<string>>> GetUserIdsAsync();

        /// <summary>
        /// Retrieves a list of all Work Order IDs from the transaction history.
        /// </summary>
        Task<Model_Dao_Result<List<string>>> GetWorkOrdersAsync();

        /// <summary>
        /// Retrieves a list of all Purchase Order IDs from the transaction history.
        /// </summary>
        Task<Model_Dao_Result<List<string>>> GetPurchaseOrdersAsync();

        /// <summary>
        /// Retrieves a list of all Customer Order IDs from the transaction history.
        /// </summary>
        Task<Model_Dao_Result<List<string>>> GetCustomerOrdersAsync();
    }
    #endregion

}
