using System;

namespace MTM_WIP_Application_Winforms.Models.Analytics
{
    /// <summary>
    /// Represents the configuration data stored in the sys_visual table.
    /// </summary>
    public class Model_Visual_SysVisual
    {
        /// <summary>
        /// Unique identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// JSON string containing user shift mappings: {"USERNAME": int_shift}
        /// </summary>
        public string JsonShiftData { get; set; } = "{}";

        /// <summary>
        /// JSON string containing user name mappings: {"USERNAME": "Full Name"}
        /// </summary>
        public string JsonUserFullNames { get; set; } = "{}";

        /// <summary>
        /// Timestamp of the last update.
        /// </summary>
        public DateTime LastUpdated { get; set; }
    }
}
