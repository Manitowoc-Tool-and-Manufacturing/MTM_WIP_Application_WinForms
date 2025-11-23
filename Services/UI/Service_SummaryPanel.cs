using System.Data;
using MTM_WIP_Application_Winforms.Logging;

namespace MTM_WIP_Application_Winforms.Services
{
    public static class Service_SummaryPanel
    {
        public static void AddSummaryPanel(Panel parentPanel, DataGridView dgv)
        {
            try
            {
                // Create TableLayoutPanel
                var tlp = new TableLayoutPanel
                {
                    Name = "tlpTotalSummary",
                    Dock = DockStyle.Bottom,
                    AutoSize = true,
                    AutoSizeMode = AutoSizeMode.GrowAndShrink,
                    ColumnCount = 3,
                    RowCount = 1,
                    BackColor = parentPanel.BackColor,
                    CellBorderStyle = TableLayoutPanelCellBorderStyle.Single,
                    Visible = false // Hidden by default
                };

                // Column Styles
                tlp.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize)); // Label
                tlp.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize)); // Value
                tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F)); // Spacer

                // Label: "Total Quantity:"
                var lblTitle = new Label
                {
                    Text = "Total Quantity:",
                    TextAlign = ContentAlignment.MiddleRight,
                    Margin = new Padding(3),
                    Dock = DockStyle.Fill,
                    AutoSize = true,
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold)
                };

                // Label: Value
                var lblValue = new Label
                {
                    Name = "lblTotalValue",
                    Text = "0",
                    TextAlign = ContentAlignment.MiddleLeft,
                    Margin = new Padding(3),
                    Dock = DockStyle.Fill,
                    AutoSize = true,
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold)
                };

                // Add controls
                tlp.Controls.Add(lblTitle, 0, 0);
                tlp.Controls.Add(lblValue, 1, 0);
                // Spacer column is empty

                // Add to parent
                parentPanel.Controls.Add(tlp);
                // Ensure it's at the bottom (z-order) so Dock=Bottom works correctly with Dock=Fill siblings
                tlp.BringToFront(); 

                // Event Handlers
                dgv.DataSourceChanged += (s, e) => UpdateSummary(dgv, tlp, lblValue);
                dgv.DataBindingComplete += (s, e) => UpdateSummary(dgv, tlp, lblValue);
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
            }
        }

        private static void UpdateSummary(DataGridView dgv, TableLayoutPanel tlp, Label lblValue)
        {
            try
            {
                if (dgv.DataSource is not DataTable dt || dt.Rows.Count == 0)
                {
                    tlp.Visible = false;
                    return;
                }

                // Check for consistency
                string? firstPart = null;
                string? firstOp = null;
                int totalQty = 0;
                bool allSame = true;

                // Use DataView if available to respect filtering/sorting, or just DataTable
                // The user asked for "searched product", usually implies the visible rows.
                // If the DGV is sorted/filtered, we should iterate the DGV rows or the DefaultView.
                
                // Iterating DGV rows is safer for "what the user sees"
                if (dgv.Rows.Count == 0)
                {
                    tlp.Visible = false;
                    return;
                }

                // Get first row values
                if (dgv.Rows[0].DataBoundItem is DataRowView firstDrv)
                {
                    firstPart = firstDrv["PartID"]?.ToString();
                    firstOp = firstDrv["Operation"]?.ToString();
                }

                if (string.IsNullOrEmpty(firstPart) || string.IsNullOrEmpty(firstOp))
                {
                    tlp.Visible = false;
                    return;
                }

                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (row.DataBoundItem is not DataRowView drv) continue;

                    string? part = drv["PartID"]?.ToString();
                    string? op = drv["Operation"]?.ToString();

                    if (part != firstPart || op != firstOp)
                    {
                        allSame = false;
                        break;
                    }

                    if (int.TryParse(drv["Quantity"]?.ToString(), out int qty))
                    {
                        totalQty += qty;
                    }
                }

                if (allSame)
                {
                    lblValue.Text = totalQty.ToString("N0");
                    tlp.Visible = true;
                }
                else
                {
                    tlp.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                tlp.Visible = false;
            }
        }
    }
}
