using System;
using System.Data;
using System.Threading.Tasks;
using MTM_WIP_Application_Winforms.Models.Analytics;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Data
{
    /// <summary>
    /// Interface for accessing Infor Visual analytics data and metadata.
    /// </summary>
    public interface IDao_VisualAnalytics
    {
        /// <summary>
        /// Gets the current metadata from sys_visual table.
        /// </summary>
        /// <returns>Model_Dao_Result containing the sys_visual record.</returns>
        Task<Model_Dao_Result<Model_Visual_SysVisual>> GetSysVisualDataAsync();

        /// <summary>
        /// Updates the sys_visual table with new JSON data.
        /// </summary>
        /// <param name="jsonShifts">JSON string for shift data.</param>
        /// <param name="jsonNames">JSON string for user full names.</param>
        /// <returns>Model_Dao_Result indicating success.</returns>
        Task<Model_Dao_Result<bool>> UpdateSysVisualDataAsync(string jsonShifts, string jsonNames);

        /// <summary>
        /// Gets raw transaction counts for material handler scoring.
        /// </summary>
        /// <param name="startDate">Start date for the report.</param>
        /// <param name="endDate">End date for the report.</param>
        /// <returns>Model_Dao_Result containing DataTable with transaction stats.</returns>
        Task<Model_Dao_Result<DataTable>> GetMaterialHandlerStatsAsync(DateTime startDate, DateTime endDate);
    }
}
