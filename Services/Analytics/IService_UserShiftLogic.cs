using System.Collections.Generic;
using System.Threading.Tasks;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Models.Analytics;

namespace MTM_WIP_Application_Winforms.Services.Analytics
{
    /// <summary>
    /// Service for calculating and managing user shift assignments and metadata.
    /// </summary>
    public interface IService_UserShiftLogic
    {
        /// <summary>
        /// Analyzes transaction history to calculate shift assignments for all users.
        /// </summary>
        /// <returns>Dictionary mapping UserName to ShiftNumber.</returns>
        Task<Model_Dao_Result<Dictionary<string, int>>> CalculateAllUserShiftsAsync();

        /// <summary>
        /// Retrieves full names for all users from Infor Visual database.
        /// </summary>
        /// <returns>Dictionary mapping UserName to FullName.</returns>
        Task<Model_Dao_Result<Dictionary<string, string>>> FetchUserFullNamesAsync();

        /// <summary>
        /// Persists shift and name data to sys_visual table.
        /// </summary>
        /// <param name="shifts">Dictionary of user shifts.</param>
        /// <param name="names">Dictionary of user names.</param>
        /// <returns>Success status.</returns>
        Task<Model_Dao_Result<bool>> SaveVisualMetadataAsync(
            Dictionary<string, int> shifts,
            Dictionary<string, string> names);

        /// <summary>
        /// Calculates material handler scores with shift balancing and grading curve.
        /// </summary>
        /// <param name="startDate">Start date.</param>
        /// <param name="endDate">End date.</param>
        /// <returns>List of scored material handlers.</returns>
        Task<Model_Dao_Result<List<Model_Visual_MaterialHandlerScore>>> CalculateMaterialHandlerScoresAsync(DateTime startDate, DateTime endDate);
    }
}
