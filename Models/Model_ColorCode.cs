using System;

namespace MTM_WIP_Application_Winforms.Models
{
    #region Model_ColorCode

    /// <summary>
    /// Represents a color code used for inventory work order segregation.
    /// </summary>
    /// <remarks>
    /// Color codes are stored in the md_color_codes table and used to track
    /// inventory items by work order. Predefined colors (Red, Blue, Green, etc.)
    /// have IsUserDefined = false, while custom colors entered via "OTHER" option
    /// have IsUserDefined = true.
    /// </remarks>
    public class Model_ColorCode
    {
        #region Properties

        /// <summary>
        /// Gets or sets the color code value.
        /// </summary>
        /// <remarks>
        /// Max length: 50 characters.
        /// Examples: "RED", "BLUE", "Blueberry", "Unknown"
        /// </remarks>
        public string ColorCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets whether this color was user-defined (true) or predefined (false).
        /// </summary>
        /// <remarks>
        /// Predefined colors: Red, Blue, Green, Yellow, Orange, Purple, Pink, White, Black, Unknown
        /// User-defined colors: Custom colors entered via "OTHER" option
        /// </remarks>
        public bool IsUserDefined { get; set; }

        /// <summary>
        /// Gets or sets the date this color code was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Model_ColorCode"/> class.
        /// </summary>
        public Model_ColorCode()
        {
            CreatedDate = DateTime.Now;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Model_ColorCode"/> class with specified values.
        /// </summary>
        /// <param name="colorCode">The color code value.</param>
        /// <param name="isUserDefined">Whether this is a user-defined color.</param>
        public Model_ColorCode(string colorCode, bool isUserDefined)
        {
            ColorCode = colorCode;
            IsUserDefined = isUserDefined;
            CreatedDate = DateTime.Now;
        }

        #endregion
    }

    #endregion
}
