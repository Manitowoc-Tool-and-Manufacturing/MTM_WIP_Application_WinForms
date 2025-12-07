using System.Data;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Data
{
    /// <summary>
    /// Interface for WindowFormMapping Data Access Object.
    /// </summary>
    public interface IDao_WindowFormMapping
    {
        /// <summary>
        /// Retrieves all window form mappings.
        /// </summary>
        /// <param name="includeInactive">Whether to include inactive mappings.</param>
        /// <returns>DataTable containing mappings.</returns>
        Task<Model_Dao_Result<DataTable>> GetAllAsync(bool includeInactive = false);

        /// <summary>
        /// Inserts or updates a window form mapping.
        /// </summary>
        /// <param name="codebaseName">The codebase name (unique).</param>
        /// <param name="userFriendlyName">The user friendly name.</param>
        /// <param name="isActive">Whether the mapping is active.</param>
        /// <returns>The MappingID.</returns>
        Task<Model_Dao_Result<int>> UpsertAsync(string codebaseName, string userFriendlyName, bool isActive);
    }
}
