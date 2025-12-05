using System;

namespace MTM_WIP_Application_Winforms.Models.Analytics
{
    /// <summary>
    /// Represents a user's calculated shift assignment based on transaction history.
    /// </summary>
    public class Model_Visual_UserShift
    {
        /// <summary>
        /// The username from Infor Visual (e.g., JKOLL).
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// The calculated shift number.
        /// 1 = 1st Shift (06:00-14:00)
        /// 2 = 2nd Shift (14:00-22:00)
        /// 3 = 3rd Shift (22:00-06:00)
        /// 4 = Weekend Shift (Fri-Mon 06:00-18:00)
        /// 0 = Unknown
        /// </summary>
        public int ShiftNumber { get; set; }

        /// <summary>
        /// The full name of the user (e.g., John Koll).
        /// </summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// The timestamp when this shift was last calculated.
        /// </summary>
        public DateTime LastCalculated { get; set; }
    }
}
