using System;

namespace MTM_WIP_Application_Winforms.Models;

/// <summary>
/// Describes the exact range of data rows rendered on a single printed page.
/// </summary>
public sealed class Model_Print_PageBoundary
{
    /// <summary>
    /// Gets or sets the 1-based printed page number associated with this boundary.
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// Gets or sets the zero-based index of the first DataTable row rendered on the page.
    /// </summary>
    public int StartRow { get; set; }

    /// <summary>
    /// Gets or sets the zero-based index of the first DataTable row belonging to the next page.
    /// </summary>
    /// <remarks>
    /// This value is exclusive. The number of rows on the page is <c>EndRow - StartRow</c>.
    /// </remarks>
    public int EndRow { get; set; }

    /// <summary>
    /// Creates a defensive copy of the current boundary instance.
    /// </summary>
    public Model_Print_PageBoundary Clone() => new()
    {
        PageNumber = PageNumber,
        StartRow = StartRow,
        EndRow = EndRow
    };
}
