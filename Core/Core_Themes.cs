using System.Collections.Concurrent;
using System.Data;
using System.Diagnostics;
using System.Text.Json;
using MTM_WIP_Application_Winforms.Controls.Addons;
using MTM_WIP_Application_Winforms.Controls.MainForm;
using MTM_WIP_Application_Winforms.Controls.SettingsForm;
using MTM_WIP_Application_Winforms.Controls.Shared;
using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Models;
using System.Reflection;

namespace MTM_WIP_Application_Winforms.Core
{
    /// <summary>
    /// Core theming system that provides comprehensive DPI scaling and UI responsiveness.
    ///
    /// This class handles:
    /// 1. Runtime DPI scaling for all forms and controls (per Telerik WinForms DPI scaling article)
    /// 2. Async/await UI responsiveness improvements (per Grant Winney async WinForms article)
    /// 3. Theme application with proper color handling
    /// 4. Runtime layout adjustments moved from designer files
    /// 5. Dynamic DPI change handling for multi-monitor scenarios
    ///
    /// Key Features:
    /// - AutoScaleMode.Dpi set on all forms and controls for proper DPI scaling
    /// - Runtime margin/padding adjustments for TableLayoutPanel, GroupBox, Panel
    /// - SplitContainer distance calculations based on DPI scale
    /// - Event-driven DPI change handling for monitor switching
    /// - Comprehensive control hierarchy traversal for complete coverage
    ///
    /// References:
    /// - https://www.telerik.com/blogs/winforms-scaling-at-large-dpi-settings-is-it-even-possible-
    /// - https://grantwinney.com/using-async-await-and-task-to-keep-the-winforms-ui-more-responsive/
    /// </summary>
    public static class Core_Themes
    {
        #region Public API

        public static async Task<Model_Shared_UserUiColors> GetUserThemeColorsAsync(string userId)
        {
            var themeNameResult = await Dao_User.GetThemeNameAsync(userId);
            Model_Application_Variables.ThemeName = themeNameResult.IsSuccess && themeNameResult.Data != null ? themeNameResult.Data : "Default";
            if (!Core_AppThemes.GetThemeNames().Contains(Model_Application_Variables.ThemeName))
            {
                await Core_AppThemes.LoadThemesFromDatabaseAsync();
            }

            Core_AppThemes.AppTheme appTheme = Core_AppThemes.GetTheme(Model_Application_Variables.ThemeName);
            return appTheme.Colors;
        }

        /// <summary>
        /// Applies theme colors to DataGridView, hides row header, and attaches column visibility context menu.
        /// </summary>
        public static void ApplyThemeToDataGridView(DataGridView dataGridView)
        {
            if (dataGridView == null)
            {
                return;
            }

            // Get current theme colors
            var theme = Core_AppThemes.GetCurrentTheme();
            var colors = theme.Colors;

            // Apply theme colors
            dataGridView.BackgroundColor = colors.DataGridBackColor ?? SystemColors.AppWorkspace;
            dataGridView.GridColor = colors.DataGridGridColor ?? SystemColors.ControlDark;
            dataGridView.DefaultCellStyle.BackColor = colors.DataGridRowBackColor ?? SystemColors.Window;
            dataGridView.DefaultCellStyle.ForeColor = colors.DataGridForeColor ?? SystemColors.ControlText;
            dataGridView.DefaultCellStyle.SelectionBackColor = colors.DataGridSelectionBackColor ?? SystemColors.Highlight;
            dataGridView.DefaultCellStyle.SelectionForeColor = colors.DataGridSelectionForeColor ?? SystemColors.HighlightText;
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = colors.DataGridHeaderBackColor ?? SystemColors.Control;
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = colors.DataGridHeaderForeColor ?? SystemColors.ControlText;
            dataGridView.EnableHeadersVisualStyles = false; // Required for custom header colors

            // Hide row header (selection indicator bar on left)
            dataGridView.RowHeadersVisible = false;

            // Attach column visibility context menu
            AttachColumnVisibilityMenu(dataGridView);
        }

        /// <summary>
        /// Attaches a context menu to a DataGridView header row for toggling column visibility
        /// </summary>
        private static void AttachColumnVisibilityMenu(DataGridView dataGridView)
        {
            // Remove any existing handlers to avoid duplicates
            dataGridView.MouseDown -= DataGridView_MouseDown;
            dataGridView.MouseDown += DataGridView_MouseDown;

            // Store the original context menu to restore when not on header
            if (dataGridView.Tag == null)
            {
                dataGridView.Tag = dataGridView.ContextMenuStrip;
            }

            static void DataGridView_MouseDown(object? sender, MouseEventArgs e)
            {
                if (sender is not DataGridView dgv || e.Button != MouseButtons.Right)
                {
                    return;
                }

                // Check if click was on the column header area
                Rectangle headerRect = dgv.DisplayRectangle;
                headerRect.Height = dgv.ColumnHeadersHeight;

                if (headerRect.Contains(e.Location))
                {
                    // On header - show column visibility menu and suppress default context menu
                    ShowColumnVisibilityMenu(dgv, e.Location);

                    // Temporarily clear the context menu to prevent print menu from showing
                    var originalMenu = dgv.Tag as ContextMenuStrip;
                    dgv.ContextMenuStrip = null;

                    // Restore it after a short delay
                    Task.Delay(100).ContinueWith(_ =>
                    {
                        if (dgv.IsHandleCreated && !dgv.IsDisposed)
                        {
                            dgv.BeginInvoke(new Action(() =>
                            {
                                if (!dgv.IsDisposed)
                                {
                                    dgv.ContextMenuStrip = originalMenu;
                                }
                            }));
                        }
                    });
                }
            }
        }

        /// <summary>
        /// Shows the column visibility context menu for a DataGridView
        /// </summary>
        private static void ShowColumnVisibilityMenu(DataGridView dgv, Point location)
        {
            // Get the column that was clicked (if any)
            DataGridViewColumn? clickedColumn = null;
            int headerHeight = dgv.ColumnHeadersHeight;

            // If click is on the header row, find which column was clicked
            if (location.Y <= headerHeight)
            {
                int x = location.X;
                int cumulativeWidth = 0;
                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    if (!col.Visible)
                    {
                        continue;
                    }

                    cumulativeWidth += col.Width;
                    if (x < cumulativeWidth)
                    {
                        clickedColumn = col;
                        break;
                    }
                }
            }

            // Create a new context menu each time to ensure it's up to date
            ContextMenuStrip menu = new();

            // Add a title item
            ToolStripMenuItem titleItem = new("Column Visibility")
            {
                Enabled = false, Font = new Font(menu.Font, FontStyle.Bold)
            };
            menu.Items.Add(titleItem);
            menu.Items.Add(new ToolStripSeparator());

            // Add each column as a checkbox menu item
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                ToolStripMenuItem item = new(col.HeaderText) { Checked = col.Visible, Tag = col.Name };

                item.Click += (s, e) =>
                {
                    if (s is ToolStripMenuItem menuItem && menuItem.Tag is string colName)
                    {
                        DataGridViewColumn? column = dgv.Columns[colName];
                        if (column != null)
                        {
                            // Toggle visibility
                            column.Visible = !column.Visible;
                            menuItem.Checked = column.Visible;
                        }
                    }
                };

                menu.Items.Add(item);
            }

            // Add "Change Column Order" option
            menu.Items.Add(new ToolStripSeparator());
            ToolStripMenuItem changeOrderItem = new("Change Column Order");
            changeOrderItem.Click += (s, e) =>
            {
                using (ColumnOrderDialog dlg = new(dgv))
                {
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        // Apply new order
                        List<string> newOrder = dlg.GetColumnOrder();
                        for (int i = 0; i < newOrder.Count; i++)
                        {
                            DataGridViewColumn? col = dgv.Columns[newOrder[i]];
                            col.DisplayIndex = i;
                        }
                    }
                }
            };
            menu.Items.Add(changeOrderItem);

            // Add "Select All" and "Deselect All" options
            menu.Items.Add(new ToolStripSeparator());
            ToolStripMenuItem selectAllItem = new("Select All");
            selectAllItem.Click += (s, e) =>
            {
                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    col.Visible = true;
                }

                // Update all checkboxes
                foreach (ToolStripItem item in menu.Items)
                {
                    if (item is ToolStripMenuItem menuItem && menuItem.Tag is string)
                    {
                        menuItem.Checked = true;
                    }
                }
            };
            menu.Items.Add(selectAllItem);

            ToolStripMenuItem deselectAllItem = new("Deselect All");
            deselectAllItem.Click += (s, e) =>
            {
                // Don't hide all columns - keep at least one visible
                bool hasOneVisible = false;

                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    if (!hasOneVisible)
                    {
                        col.Visible = true;
                        hasOneVisible = true;
                    }
                    else
                    {
                        col.Visible = false;
                    }
                }

                // Update all checkboxes
                foreach (ToolStripItem item in menu.Items)
                {
                    if (item is ToolStripMenuItem menuItem && menuItem.Tag is string colName)
                    {
                        menuItem.Checked = dgv.Columns[colName]?.Visible ?? false;
                    }
                }
            };
            menu.Items.Add(deselectAllItem);

            // Add "Save Grid Settings" option
            menu.Items.Add(new ToolStripSeparator());
            ToolStripMenuItem saveSettingsItem = new("Save Grid Settings");
            saveSettingsItem.Click += async (s, e) =>
            {
                try
                {
                    // Serialize column visibility and order
                    var columns = dgv.Columns.Cast<DataGridViewColumn>()
                        .OrderBy(c => c.DisplayIndex)
                        .Select(c => new { Name = c.Name, Visible = c.Visible, DisplayIndex = c.DisplayIndex })
                        .ToList();
                    string json = JsonSerializer.Serialize(new { Columns = columns });
                    // Get userId (from Model_Application_Variables.User)
                    string userId = Model_Application_Variables.User;
                    string gridName = dgv.Name;
                    await Dao_User.SetGridViewSettingsJsonAsync(userId, gridName, json);

                    MessageBox.Show(@"Grid settings saved successfully.", @"Grid Settings", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($@"Error saving grid settings: {ex.Message}", @"Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            };
            menu.Items.Add(saveSettingsItem);

            // Show the menu
            menu.Show(dgv, location);
        }

        public static void SizeDataGrid(DataGridView dataGridView)
        {
            if (dataGridView == null)
            {
                return;
            }

            try
            {
                // Hide row header to maximize usable space
                dataGridView.RowHeadersVisible = false;

                // Use Fill mode to utilize full available area
                dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
            }
        }

        public static void ApplyFocusHighlighting(Control parentControl)
        {
            Core_AppThemes.AppTheme theme = Core_AppThemes.GetCurrentTheme();
            FocusUtils.ApplyFocusEventHandlingToControls(parentControl.Controls, theme.Colors);
        }

        /// <summary>
        /// Applies auto-sizing to all columns and rows of the DataGridView.
        /// </summary>
        private static void ApplyAutoSizingToDataGrid(DataGridView dataGridView)
        {
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
            dataGridView.AutoResizeRows(DataGridViewAutoSizeRowsMode.DisplayedCells);
        }

        /// <summary>
        /// Loads and applies saved grid settings (column visibility and order) for the specified DataGridView.
        /// </summary>
        public static async Task LoadAndApplyGridSettingsAsync(DataGridView dgv, string userId)
        {
            try
            {
                string gridName = dgv.Name;
                var result = await Dao_User.GetGridViewSettingsJsonAsync(userId);

                if (result.IsSuccess && !string.IsNullOrEmpty(result.Data))
                {
                    using JsonDocument doc = JsonDocument.Parse(result.Data);
                    if (doc.RootElement.TryGetProperty(gridName, out JsonElement gridSettingsElement))
                    {
                        string? settingsJson = null;
                        if (gridSettingsElement.ValueKind == JsonValueKind.String)
                        {
                            settingsJson = gridSettingsElement.GetString();
                        }
                        else
                        {
                            settingsJson = gridSettingsElement.GetRawText();
                        }

                        if (!string.IsNullOrEmpty(settingsJson))
                        {
                            var settings = JsonSerializer.Deserialize<GridSettings>(settingsJson);
                            if (settings?.Columns != null)
                            {
                                // Sort by DisplayIndex to apply order correctly
                                var orderedSettings = settings.Columns.OrderBy(c => c.DisplayIndex).ToList();

                                foreach (var colSetting in orderedSettings)
                                {
                                    if (dgv.Columns.Contains(colSetting.Name))
                                    {
                                        var col = dgv.Columns[colSetting.Name];
                                        col.Visible = colSetting.Visible;
                                        col.DisplayIndex = colSetting.DisplayIndex;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
            }
        }

        private class GridSettings
        {
            public List<ColumnSetting>? Columns { get; set; }
        }

        private class ColumnSetting
        {
            public string Name { get; set; } = string.Empty;
            public bool Visible { get; set; }
            public int DisplayIndex { get; set; }
        }

        #endregion
    }
}
