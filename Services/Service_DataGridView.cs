using System.Data;
using MTM_WIP_Application_Winforms.Core;
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

        #region Automatic Color Coding

        /// <summary>
        /// Enables automatic re-application of inventory color coding when the grid is sorted or data is bound.
        /// </summary>
        /// <param name="dgv">The DataGridView to enable automatic coloring for.</param>
        public static void EnableAutomaticInventoryColorCoding(DataGridView dgv)
        {
            if (dgv == null) return;

            // Remove existing handlers to prevent duplicates
            dgv.Sorted -= Dgv_Sorted_ColorCoding;
            dgv.DataBindingComplete -= Dgv_DataBindingComplete_ColorCoding;

            // Add handlers
            dgv.Sorted += Dgv_Sorted_ColorCoding;
            dgv.DataBindingComplete += Dgv_DataBindingComplete_ColorCoding;

            // Apply immediately
            ApplyInventoryColorCoding(dgv);
        }

        /// <summary>
        /// Disables automatic re-application of inventory color coding.
        /// </summary>
        /// <param name="dgv">The DataGridView to disable automatic coloring for.</param>
        public static void DisableAutomaticInventoryColorCoding(DataGridView dgv)
        {
            if (dgv == null) return;

            dgv.Sorted -= Dgv_Sorted_ColorCoding;
            dgv.DataBindingComplete -= Dgv_DataBindingComplete_ColorCoding;
        }

        private static void Dgv_Sorted_ColorCoding(object? sender, EventArgs e)
        {
            if (sender is DataGridView dgv)
            {
                ApplyInventoryColorCoding(dgv);
            }
        }

        private static void Dgv_DataBindingComplete_ColorCoding(object? sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (sender is DataGridView dgv)
            {
                ApplyInventoryColorCoding(dgv);
            }
        }

        #endregion

        #region Totals

        /// <summary>
        /// Adds a total summary panel to the DataGridView's parent container if all rows share the same PartID and Operation.
        /// </summary>
        /// <param name="dgv">The DataGridView to process.</param>
        public static void AddTotalRowIfApplicable(DataGridView dgv)
        {
            if (dgv == null || dgv.DataSource is not DataTable dt || dt.Rows.Count == 0) return;

            // Check if feature is enabled
            if (!Model_Application_Variables.ShowTotalSummaryPanel) return;

            try
            {
                // Remove existing panel if any
                RemoveTotalSummaryPanel(dgv);

                // Check if we already have a total row (legacy check, just in case)
                if (dt.Rows[dt.Rows.Count - 1]["PartID"]?.ToString() == "TOTAL") return;

                string? firstPart = dt.Rows[0]["PartID"]?.ToString();
                string? firstOp = dt.Rows[0]["Operation"]?.ToString();

                if (string.IsNullOrEmpty(firstPart) || string.IsNullOrEmpty(firstOp)) return;

                bool allSame = true;
                int totalQty = 0;
                int rowCount = 0;

                foreach (DataRow row in dt.Rows)
                {
                    // Skip if this is somehow already a total row (safety check)
                    if (row["PartID"]?.ToString() == "TOTAL") continue;

                    if (row["PartID"]?.ToString() != firstPart || row["Operation"]?.ToString() != firstOp)
                    {
                        allSame = false;
                        break;
                    }

                    if (int.TryParse(row["Quantity"]?.ToString(), out int qty))
                    {
                        totalQty += qty;
                    }
                    rowCount++;
                }

                if (allSame)
                {
                    CreateTotalSummaryPanel(dgv, totalQty, rowCount);
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
            }
        }

        private static void RemoveTotalSummaryPanel(DataGridView dgv)
        {
            if (dgv.Parent == null) return;

            var existingPanel = dgv.Parent.Controls.Find("TotalSummaryPanel_" + dgv.Name, true).FirstOrDefault();
            if (existingPanel != null)
            {
                existingPanel.Parent?.Controls.Remove(existingPanel);
                existingPanel.Dispose();
            }
        }

        private static void CreateTotalSummaryPanel(DataGridView dgv, int totalQty, int rowCount)
        {
            if (dgv.Parent == null) return;

            // Create TableLayoutPanel
            TableLayoutPanel panel = new TableLayoutPanel();
            panel.Name = "TotalSummaryPanel_" + dgv.Name;
            panel.AutoSize = true;
            panel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel.BackColor = dgv.Parent.BackColor; // Match parent background
            panel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            panel.ColumnCount = 2;
            panel.RowCount = 3;
            
            // Add rows/columns styles
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            panel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            panel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            panel.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            // Row 1: Total Quantity
            Label lblQtyTitle = new Label();
            lblQtyTitle.Text = "Total Quantity:";
            lblQtyTitle.TextAlign = ContentAlignment.MiddleRight;
            lblQtyTitle.AutoSize = true;
            lblQtyTitle.Margin = new Padding(3);
            lblQtyTitle.Anchor = AnchorStyles.Right;

            Label lblQtyValue = new Label();
            lblQtyValue.Text = totalQty.ToString("N0");
            lblQtyValue.TextAlign = ContentAlignment.MiddleLeft;
            lblQtyValue.AutoSize = true;
            lblQtyValue.Margin = new Padding(3);
            lblQtyValue.Anchor = AnchorStyles.Left;
            lblQtyValue.Font = new Font(dgv.Font, FontStyle.Bold);

            panel.Controls.Add(lblQtyTitle, 0, 0);
            panel.Controls.Add(lblQtyValue, 1, 0);

            // Row 2: Total Rows
            Label lblRowsTitle = new Label();
            lblRowsTitle.Text = "Total Rows:";
            lblRowsTitle.TextAlign = ContentAlignment.MiddleRight;
            lblRowsTitle.AutoSize = true;
            lblRowsTitle.Margin = new Padding(3);
            lblRowsTitle.Anchor = AnchorStyles.Right;

            Label lblRowsValue = new Label();
            lblRowsValue.Text = rowCount.ToString("N0");
            lblRowsValue.TextAlign = ContentAlignment.MiddleLeft;
            lblRowsValue.AutoSize = true;
            lblRowsValue.Margin = new Padding(3);
            lblRowsValue.Anchor = AnchorStyles.Left;

            panel.Controls.Add(lblRowsTitle, 0, 1);
            panel.Controls.Add(lblRowsValue, 1, 1);

            // Row 3: Close Button
            Button btnClose = new Button();
            btnClose.Text = "Close";
            btnClose.AutoSize = true;
            btnClose.Margin = new Padding(3);
            btnClose.Anchor = AnchorStyles.None;
            btnClose.Click += (s, e) => { panel.Visible = false; panel.Dispose(); };
            // Span across both columns
            panel.SetColumnSpan(btnClose, 2);
            panel.Controls.Add(btnClose, 0, 2);

            // Position: Bottom Left of DGV
            // We need to add it to the DGV's parent and position it relative to the DGV
            // Since we can't easily "dock" without pushing, we'll use Anchor and BringToFront
            // But if DGV is Dock=Fill, we need to be careful.
            // Best approach: Add to parent, BringToFront, set Location manually based on DGV position
            
            dgv.Parent.Controls.Add(panel);
            panel.BringToFront();

            // Initial positioning
            UpdatePanelPosition(dgv, panel);

            // Handle resize to keep position
            dgv.Resize += (s, e) => UpdatePanelPosition(dgv, panel);
        }

        private static void UpdatePanelPosition(DataGridView dgv, Control panel)
        {
            if (dgv.Parent == null || panel.IsDisposed) return;
            
            // Position at bottom left of DGV, with a small offset
            int x = dgv.Left + 20; // 20px padding from left
            int y = dgv.Bottom - panel.Height - 20; // 20px padding from bottom

            // Ensure it stays within parent bounds
            if (y < dgv.Top) y = dgv.Top;
            
            panel.Location = new Point(x, y);
        }

        #endregion
    }
}
