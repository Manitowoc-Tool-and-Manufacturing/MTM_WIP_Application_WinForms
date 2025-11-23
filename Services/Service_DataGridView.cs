using System.Data;
using System.Text.Json;
using MTM_WIP_Application_Winforms.Core;
using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Services
{
    /// <summary>
    /// Centralized service for DataGridView configuration, theming, and operations.
    /// </summary>
    public static class Service_DataGridView
    {
        #region Fields

        private static readonly HashSet<string> PredefinedColorCodes = new(StringComparer.OrdinalIgnoreCase)
        {
            "Red", "Blue", "Green", "Yellow", "Orange", "Purple", "Pink", "White", "Black"
        };

        #endregion

        #region Configuration

        /// <summary>
        /// Configures column visibility, ordering, and header text.
        /// </summary>
        /// <param name="dgv">The DataGridView to configure.</param>
        /// <param name="visibleColumns">List of column names to show (in order).</param>
        /// <param name="headerRenames">Optional dictionary of column name to header text mappings.</param>
        public static void ConfigureColumns(DataGridView dgv, string[] visibleColumns, Dictionary<string, string>? headerRenames = null)
        {
            if (dgv == null || visibleColumns == null) return;

            try
            {
                // Hide all columns first
                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    col.Visible = false;
                }

                // Show and order specified columns
                for (int i = 0; i < visibleColumns.Length; i++)
                {
                    string colName = visibleColumns[i];
                    if (dgv.Columns.Contains(colName))
                    {
                        var col = dgv.Columns[colName];
                        col.Visible = true;
                        col.DisplayIndex = i;

                        // Apply header rename if provided
                        if (headerRenames != null && headerRenames.TryGetValue(colName, out string? newHeader))
                        {
                            col.HeaderText = newHeader;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Low,
                    controlName: nameof(Service_DataGridView));
            }
        }

        #endregion

        #region Theming & Settings

        /// <summary>
        /// Applies standard theme and loads user-specific grid settings.
        /// </summary>
        /// <param name="dgv">The DataGridView to theme.</param>
        /// <param name="userId">The current user ID for loading settings.</param>
        public static async Task ApplyStandardSettingsAsync(DataGridView dgv, string userId)
        {
            if (dgv == null) return;

            try
            {
                // Apply standard theme
                Core_Themes.ApplyThemeToDataGridView(dgv);

                // Load user settings (column visibility/order)
                if (!string.IsNullOrEmpty(userId))
                {
                    await Core_Themes.LoadAndApplyGridSettingsAsync(dgv, userId);
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                // Non-critical error, log but don't interrupt flow
            }
        }

        #endregion

        #region Color Coding

        /// <summary>
        /// Applies background colors to rows based on the "ColorCode" column.
        /// </summary>
        /// <param name="dgv">The DataGridView to process.</param>
        public static void ApplyInventoryColorCoding(DataGridView dgv)
        {
            if (dgv == null) return;

            try
            {
                // Check if ColorCode column exists
                int colorCodeIndex = -1;
                if (dgv.Columns.Contains("ColorCode"))
                {
                    colorCodeIndex = dgv.Columns["ColorCode"].Index;
                }
                else
                {
                    // Try to find by DataPropertyName
                    foreach (DataGridViewColumn col in dgv.Columns)
                    {
                        if (col.DataPropertyName == "ColorCode")
                        {
                            colorCodeIndex = col.Index;
                            break;
                        }
                    }
                }

                if (colorCodeIndex == -1) return;

                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (row.IsNewRow) continue;

                    var cellValue = row.Cells[colorCodeIndex].Value?.ToString();
                    if (!string.IsNullOrEmpty(cellValue) && PredefinedColorCodes.Contains(cellValue))
                    {
                        Color color;
                        switch (cellValue.ToLowerInvariant())
                        {
                            case "red": color = Color.MistyRose; break;
                            case "blue": color = Color.AliceBlue; break;
                            case "green": color = Color.Honeydew; break;
                            case "yellow": color = Color.LightYellow; break;
                            case "orange": color = Color.Moccasin; break;
                            case "purple": color = Color.Lavender; break;
                            case "pink": color = Color.LavenderBlush; break;
                            case "white": color = Color.WhiteSmoke; break;
                            case "black": color = Color.Gainsboro; break; // light gray for readability
                            default: color = Color.FromName(cellValue); break;
                        }
                        
                        row.DefaultCellStyle.BackColor = color;
                        row.DefaultCellStyle.SelectionBackColor = Color.FromArgb(Math.Max(0, color.R - 60), Math.Max(0, color.G - 60), Math.Max(0, color.B - 60)); // Darker shade for visible selection
                        row.DefaultCellStyle.ForeColor = SystemColors.ControlText;
                        row.DefaultCellStyle.SelectionForeColor = Color.White;
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
            }
        }

        /// <summary>
        /// Sorts inventory data by ColorCode priority (custom order) then Location.
        /// </summary>
        /// <param name="source">The source DataTable.</param>
        /// <returns>A sorted DataTable.</returns>
        public static DataTable SortByColorPriority(DataTable source)
        {
            if (source == null) return new DataTable();
            if (source.Rows.Count == 0) return source;

            try
            {
                // Clone structure
                DataTable sorted = source.Clone();

                // Define color priority (matches original logic if possible, or standard rainbow)
                // Original logic wasn't provided in full detail, assuming standard priority
                // If original logic used a specific order, we should replicate it.
                // For now, we'll group by color presence and then location.
                
                var rows = source.AsEnumerable();

                var orderedRows = rows.OrderBy(r => 
                {
                    string color = r.Field<string>("ColorCode") ?? "";
                    // Items with color come first? Or specific colors first?
                    // Assuming items with color code are prioritized
                    return string.IsNullOrEmpty(color) ? 1 : 0;
                })
                .ThenBy(r => r.Field<string>("ColorCode")) // Group by color
                .ThenBy(r => r.Field<string>("Location"))  // Then by location
                .CopyToDataTable();

                return orderedRows;
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                return source; // Return original on error
            }
        }

        /// <summary>
        /// Applies background colors based on TransactionType (IN/OUT/TRANSFER).
        /// </summary>
        /// <param name="dgv">The DataGridView to process.</param>
        public static void ApplyTransactionRowColors(DataGridView dgv)
        {
            if (dgv == null) return;

            try
            {
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (row.DataBoundItem is Model_Transactions_Core transaction)
                    {
                        Color backgroundColor;
                        Color foregroundColor = Color.Black;

                        switch (transaction.TransactionType)
                        {
                            case TransactionType.IN:
                                backgroundColor = Color.LightGreen;
                                break;
                            case TransactionType.TRANSFER:
                                backgroundColor = Color.LightYellow;
                                break;
                            case TransactionType.OUT:
                                backgroundColor = Color.LightCoral;
                                break;
                            default:
                                backgroundColor = Color.White;
                                break;
                        }

                        row.DefaultCellStyle.BackColor = backgroundColor;
                        row.DefaultCellStyle.ForeColor = foregroundColor;
                        row.DefaultCellStyle.SelectionBackColor = Color.FromArgb(
                            Math.Max(0, backgroundColor.R - 40),
                            Math.Max(0, backgroundColor.G - 40),
                            Math.Max(0, backgroundColor.B - 40)
                        );
                        row.DefaultCellStyle.SelectionForeColor = Color.Black;
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
            }
        }

        private static bool IsDarkColor(Color color)
        {
            // Calculate luminance
            return (0.299 * color.R + 0.587 * color.G + 0.114 * color.B) < 128;
        }

        #endregion

        #region Printing

        /// <summary>
        /// Validates grid content and shows print dialog.
        /// </summary>
        /// <param name="parent">Parent control for the dialog.</param>
        /// <param name="dgv">The DataGridView to print.</param>
        /// <param name="title">Title for the print job.</param>
        public static async Task PrintGridAsync(Control parent, DataGridView dgv, string title)
        {
            if (parent == null || dgv == null) return;

            try
            {
                if (dgv.Rows.Count == 0)
                {
                    // Consistent message across all controls
                    Service_ErrorHandler.HandleValidationError(
                        "No records available to print. Run a search or perform an operation first.",
                        "Print");
                    return;
                }

                await Helper_PrintManager.ShowPrintDialogAsync(parent, dgv, title);
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                    controlName: nameof(Service_DataGridView));
            }
        }

        #endregion
    }
}
