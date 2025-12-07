using System.Data;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Data
{
    /// <summary>
    /// Interface for UserControlMapping Data Access Object.
    /// </summary>
    public interface IDao_UserControlMapping
    {
        /// <summary>
        /// Retrieves user control mappings for a specific window.
        /// </summary>
        /// <param name="windowFormMappingId">The parent window mapping ID.</param>
        /// <param name="includeInactive">Whether to include inactive mappings.</param>
        /// <returns>DataTable containing mappings.</returns>
        Task<Model_Dao_Result<DataTable>> GetByWindowAsync(int windowFormMappingId, bool includeInactive = false);

        /// <summary>
        /// Inserts or updates a user control mapping.
        /// </summary>
        /// <param name="windowFormMappingId">The parent window mapping ID.</param>
        /// <param name="codebaseName">The codebase name.</param>
        /// <param name="userFriendlyName">The user friendly name.</param>
        /// <param name="isActive">Whether the mapping is active.</param>
        /// <returns>The MappingID.</returns>
        Task<Model_Dao_Result<int>> UpsertAsync(int windowFormMappingId, string codebaseName, string userFriendlyName, bool isActive);
    }
}
