namespace MTM_WIP_Application_Winforms.Models;

/// <summary>
/// User's last-used print settings (persisted locally)
/// </summary>
public class Model_Print_Settings
{
    #region Properties

    /// <summary>
    /// Last selected printer name
    /// </summary>
    public string? LastPrinterName { get; set; }

    /// <summary>
    /// Last selected orientation (true = Landscape, false = Portrait)
    /// </summary>
    public bool LastOrientation { get; set; }

    /// <summary>
    /// Last number of copies
    /// </summary>
    public int LastCopies { get; set; } = 1;

    #endregion

    #region Constructors

    /// <summary>
    /// Creates new print settings with defaults
    /// </summary>
    public Model_Print_Settings()
    {
    }

    #endregion
}
