using System;

namespace MTM_WIP_Application_Winforms.Models
{
    /// <summary>
    /// Represents a parameter prefix override for stored procedure execution.
    /// Used by Helper_Database_StoredProcedure to resolve parameter prefixes dynamically.
    /// </summary>
    public class Model_ParameterPrefix_Override
    {
        #region Properties

        /// <summary>
        /// Unique identifier for the override record.
        /// </summary>
        public int OverrideId { get; set; }

        /// <summary>
        /// Stored procedure name (e.g., "inv_inventory_Add").
        /// </summary>
        public string ProcedureName { get; set; } = string.Empty;

        /// <summary>
        /// Parameter name without prefix (e.g., "PartNumber", "UserID").
        /// </summary>
        public string ParameterName { get; set; } = string.Empty;

        /// <summary>
        /// Override prefix to use (e.g., "p_", "in_", or empty string for no prefix).
        /// </summary>
        public string OverridePrefix { get; set; } = string.Empty;

        /// <summary>
        /// Optional explanation for why this override exists.
        /// </summary>
        public string? Reason { get; set; }

        /// <summary>
        /// User who created this override.
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// Timestamp when override was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// User who last modified this override (null if never modified).
        /// </summary>
        public string? ModifiedBy { get; set; }

        /// <summary>
        /// Timestamp of last modification (null if never modified).
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Soft delete flag (true = active, false = deleted).
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Computed property: Full parameter name with prefix applied.
        /// Example: If OverridePrefix="p_" and ParameterName="UserID", returns "p_UserID".
        /// </summary>
        public string FullParameterName => $"{OverridePrefix}{ParameterName}";

        #endregion

        #region Methods

        /// <summary>
        /// Returns a string representation of the override for debugging.
        /// </summary>
        public override string ToString()
        {
            return $"{ProcedureName}.{ParameterName} -> {FullParameterName} (ID: {OverrideId})";
        }

        #endregion
    }
}
