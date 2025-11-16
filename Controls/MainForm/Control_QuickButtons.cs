using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using MTM_WIP_Application_Winforms.Core;
using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Services;
using MySql.Data.MySqlClient;
using MTM_WIP_Application_Winforms.Controls.Shared;

namespace MTM_WIP_Application_Winforms.Controls.MainForm
{
    public partial class Control_QuickButtons : ThemedUserControl
    {
        #region Fields

        internal List<Button>? quickButtons; // Changed from static to instance
        public static Forms.MainForm.MainForm? MainFormInstance { get; set; }
        private Helper_StoredProcedureProgress? _progressHelper;

        #endregion

        #region Progress Control Methods

        public void SetProgressControls(ToolStripProgressBar progressBar, ToolStripStatusLabel statusLabel)
        {
            _progressHelper = Helper_StoredProcedureProgress.Create(progressBar, statusLabel, 
                this.FindForm() ?? throw new InvalidOperationException("Control must be added to a form"));
        }

        #endregion

        #region Constructors

        public Control_QuickButtons()
        {
            Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object>
            {
                ["ControlType"] = nameof(Control_QuickButtons),
                ["InitializationTime"] = DateTime.Now,
                ["Thread"] = Thread.CurrentThread.ManagedThreadId
            }, nameof(Control_QuickButtons), nameof(Control_QuickButtons));

            Service_DebugTracer.TraceUIAction("QUICK_BUTTONS_INITIALIZATION", nameof(Control_QuickButtons),
                new Dictionary<string, object>
                {
                    ["Phase"] = "START",
                    ["ComponentType"] = "UserControl"
                });

            InitializeComponent();

            Service_DebugTracer.TraceUIAction("TABLE_LAYOUT_SETUP", nameof(Control_QuickButtons),
                new Dictionary<string, object>
                {
                    ["RowCount"] = 10,
                    ["LayoutType"] = "TableLayoutPanel"
                });
            
            Control_QuickButtons_TableLayoutPanel_Main.RowCount = 10;
            Control_QuickButtons_TableLayoutPanel_Main.RowStyles.Clear();
            
            for (int i = 0; i < 10; i++)
            {
                Control_QuickButtons_TableLayoutPanel_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            }

            quickButtons = new List<Button>();
            for (int i = 0; i < 10; i++)
            {
                var btn = new Button
                {
                    Name = $"Control_QuickButtons_Button_Button{i + 1}",
                    Dock = DockStyle.Fill,          // Fill the entire row
                    Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom,
                    AutoSize = false,               // Don't auto-size the button itself
                    Margin = new Padding(1),        // 1px margin on all sides
                    MinimumSize = new Size(0, 0),   // No minimum - let row define size
                    TabIndex = i + 1,
                    UseVisualStyleBackColor = false, // Use explicit colors
                    BackColor = SystemColors.Control,
                    ForeColor = SystemColors.ControlText,
                    FlatStyle = FlatStyle.Standard,
                    UseMnemonic = false,
                    ContextMenuStrip = Control_QuickButtons_ContextMenu
                };
                
                btn.Click += QuickButton_Click;
                quickButtons.Add(btn);
                
                // Add button to its proper row in the TableLayoutPanel
                Control_QuickButtons_TableLayoutPanel_Main.Controls.Add(btn, 0, i);
            }

            this.Resize += Control_QuickButtons_Resize;

            this.Load += async (s, e) => 
            {
                try
                {
                    Service_DebugTracer.TraceUIAction("QUICK_BUTTONS_LOAD_EVENT", nameof(Control_QuickButtons),
                        new Dictionary<string, object>
                        {
                            ["User"] = Model_Application_Variables.User ?? "Unknown",
                            ["Phase"] = "BEFORE_DELAY"
                        });

                    await Task.Delay(100); // Small delay to ensure UI is fully ready

                    Service_DebugTracer.TraceUIAction("QUICK_BUTTONS_LOAD_START", nameof(Control_QuickButtons),
                        new Dictionary<string, object>
                        {
                            ["User"] = Model_Application_Variables.User ?? "Unknown",
                            ["Phase"] = "AFTER_DELAY"
                        });

                    await LoadLast10Transactions(Model_Application_Variables.User);

                    Service_DebugTracer.TraceUIAction("QUICK_BUTTONS_LOAD_COMPLETE", nameof(Control_QuickButtons),
                        new Dictionary<string, object>
                        {
                            ["User"] = Model_Application_Variables.User ?? "Unknown",
                            ["Success"] = true
                        });
                }
                catch (Exception ex)
                {
                    LoggingUtility.LogApplicationError(ex);
                    Service_DebugTracer.TraceUIAction("QUICK_BUTTONS_LOAD_ERROR", nameof(Control_QuickButtons),
                        new Dictionary<string, object>
                        {
                            ["User"] = Model_Application_Variables.User ?? "Unknown",
                            ["Error"] = ex.Message
                        });
                }
            };
            
            Service_DebugTracer.TraceUIAction("QUICK_BUTTONS_POST_CONSTRUCTOR", nameof(Control_QuickButtons),
                new Dictionary<string, object>
                {
                    ["Phase"] = "BEFORE_DPI_SCALING"
                });

            menuItemEdit.Click += MenuItemEdit_Click;
            menuItemRemove.Click += MenuItemRemove_Click;
            menuItemReorder.Click += MenuItemReorder_Click;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles control resize to update button font sizes adaptively
        /// </summary>
        private void Control_QuickButtons_Resize(object? sender, EventArgs e)
        {
            if (quickButtons == null)
            {
                return;
            }
            // Refresh all button layouts with new adaptive font sizes
            foreach (Button btn in quickButtons)
            {
                if (btn.Tag != null)
                {
                    dynamic tag = btn.Tag!;
                    string partId = tag.partId?.ToString() ?? "";
                    string operation = tag.operation?.ToString() ?? "";
                    int quantity = tag.quantity != null ? Convert.ToInt32(tag.quantity) : 0;
                    
                    // Repopulate with adaptive font sizes based on new button height
                    PopulateQuickButtonLayout(btn, partId, operation, quantity);
                }
            }
        }

        public async Task LoadLast10Transactions(string? currentUser)
        {
            try
            {
                LoggingUtility.Log($"");
                LoggingUtility.Log($"[QuickButtons] ════════════════════════════════════════════════════════════");
                LoggingUtility.Log($"[QuickButtons] LoadLast10Transactions STARTED");
                LoggingUtility.Log($"[QuickButtons]   User: {currentUser}");
                LoggingUtility.Log($"[QuickButtons] ════════════════════════════════════════════════════════════");

                Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object>
                {
                    ["CurrentUser"] = currentUser ?? "NULL",
                    ["ConnectionString"] = Model_Application_Variables.ConnectionString?.Substring(0, Math.Min(50, Model_Application_Variables.ConnectionString?.Length ?? 0)) + "..."
                }, nameof(Control_QuickButtons), nameof(LoadLast10Transactions));

                // FIXED: Use Helper_Database_StoredProcedure instead of direct MySqlConnection
                // because the stored procedure has p_Status and p_ErrorMsg parameters
                if (string.IsNullOrEmpty(currentUser)) { currentUser = string.Empty; }

                // STEP 1: Cleanup duplicates and gaps BEFORE loading
                LoggingUtility.Log($"[QuickButtons] STEP 1: Running cleanup before loading");
                var cleanupResult = await Dao_QuickButtons.CleanupGapsAndDuplicatesAsync(currentUser);
                if (!cleanupResult.IsSuccess)
                {
                    LoggingUtility.Log($"[QuickButtons] STEP 1: ⚠ Cleanup failed: {cleanupResult.ErrorMessage}");
                }
                else
                {
                    LoggingUtility.Log($"[QuickButtons] STEP 1: ✓ Cleanup completed: {cleanupResult.StatusMessage}");
                }

                // STEP 2: Load data from database
                LoggingUtility.Log($"[QuickButtons] STEP 2: Loading data from database");
                var dataResult = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                    Model_Application_Variables.BootstrapConnectionString,
                    "sys_last_10_transactions_Get_ByUser",
                    new Dictionary<string, object> { ["User"] = currentUser },
                    null // No progress helper for this method
                );

                Service_DebugTracer.TraceBusinessLogic("QUICK_BUTTONS_DATA_RESULT", 
                    inputData: new { User = currentUser },
                    outputData: new 
                    { 
                        IsSuccess = dataResult.IsSuccess,
                        RowCount = dataResult.Data?.Rows.Count ?? 0,
                        ErrorMessage = dataResult.ErrorMessage
                    });

                if (!dataResult.IsSuccess || dataResult.Data == null)
                {
                    LoggingUtility.Log($"[QuickButtons] STEP 2: ✗ Failed to load or no data: {dataResult.ErrorMessage}");
                    
                    Service_DebugTracer.TraceBusinessLogic("QUICK_BUTTONS_LOAD_FAILED",
                        inputData: new { User = currentUser },
                        outputData: new { ErrorMessage = dataResult.ErrorMessage });

                    // Clear all buttons if no data can be loaded
                    if (quickButtons != null)
                    {
                        LoggingUtility.Log($"[QuickButtons] STEP 2: Clearing all {quickButtons.Count} buttons");
                        for (int j = 0; j < quickButtons.Count; j++)
                        {
                            quickButtons[j].Text = string.Empty;
                            Control_QuickButtons_Tooltip.SetToolTip(quickButtons[j], $"Position: {j + 1}");
                            quickButtons[j].Tag = new
                            {
                                partId = (string?)null,
                                operation = (string?)null,
                                quantity = (int?)null,
                                position = j + 1
                            };
                            quickButtons[j].Visible = false;
                        }
                        RefreshButtonLayout();
                    }

                    LoggingUtility.Log($"[QuickButtons] LoadLast10Transactions COMPLETED (no data)");
                    LoggingUtility.Log($"[QuickButtons] ════════════════════════════════════════════════════════════");
                    Service_DebugTracer.TraceMethodExit(null, nameof(Control_QuickButtons), nameof(LoadLast10Transactions));
                    return;
                }

                var dataTable = dataResult.Data;
                LoggingUtility.Log($"[QuickButtons] STEP 2: ✓ Retrieved {dataTable.Rows.Count} button(s) from database");
                
                // STEP 3: Populate UI buttons with database data
                LoggingUtility.Log($"[QuickButtons] STEP 3: Populating UI buttons");
                int i = 0;
                
                // Fill buttons with data from DB
                foreach (System.Data.DataRow row in dataTable.Rows)
                {
                    if (i >= quickButtons?.Count) break;
                    
                    string? partId = row["PartID"] == DBNull.Value ? null : row["PartID"].ToString();
                    string? operation = row["Operation"] == DBNull.Value ? null : row["Operation"].ToString();
                    int? quantity = row["Quantity"] == DBNull.Value ? null :
                        row["Quantity"] is int q ? q : Convert.ToInt32(row["Quantity"]);
                    
                    // Always set position 1-10, no duplicates
                    int displayPosition = i + 1;

                    LoggingUtility.Log($"[QuickButtons] STEP 3:   Button {displayPosition}: {partId ?? "NULL"} + Op:{operation ?? "NULL"} (Qty: {quantity?.ToString() ?? "NULL"})");
                    
                    if (quickButtons != null)
                    {
                        // Create structured layout inside button
                        if (partId != null && operation != null && quantity != null)
                        {
                            PopulateQuickButtonLayout(quickButtons[i], partId, operation, quantity.Value);
                        }
                        else
                        {
                            quickButtons[i].Controls.Clear();
                            quickButtons[i].Text = string.Empty;
                        }
                        
                        string tooltipText = partId != null && operation != null && quantity != null
                            ? $"Part ID: {partId}, Operation: {operation}, Quantity: {quantity}\nPosition: {displayPosition}"
                            : $"Position: {displayPosition}";
                        Control_QuickButtons_Tooltip.SetToolTip(quickButtons[i], tooltipText);
                        
                        quickButtons[i].Tag = new { partId, operation, quantity, position = displayPosition };
                        quickButtons[i].Visible = partId != null && operation != null && quantity != null;
                    }
                    i++;
                }

                LoggingUtility.Log($"[QuickButtons] STEP 3: Filled {i} button(s) with data");

                // Clear remaining buttons
                if (quickButtons != null && i < quickButtons.Count)
                {
                    LoggingUtility.Log($"[QuickButtons] STEP 3: Clearing remaining {quickButtons.Count - i} button(s)");
                    for (; i < quickButtons.Count; i++)
                    {
                        quickButtons[i].Controls.Clear();
                        quickButtons[i].Text = string.Empty;
                        Control_QuickButtons_Tooltip.SetToolTip(quickButtons[i], $"Position: {i + 1}");
                        quickButtons[i].Tag = new
                        {
                            partId = (string?)null,
                            operation = (string?)null,
                            quantity = (int?)null,
                            position = i + 1
                        };
                        quickButtons[i].Visible = false;
                    }
                }

                // STEP 4: Refresh UI layout
                LoggingUtility.Log($"[QuickButtons] STEP 4: Refreshing button layout");
                if (quickButtons != null)
                {
                    RefreshButtonLayout();
                    int visibleCount = quickButtons.Count(b => b.Visible);
                    LoggingUtility.Log($"[QuickButtons] STEP 4: Layout refreshed - {visibleCount} visible button(s)");
                }

                LoggingUtility.Log($"[QuickButtons] ╔══════════════════════════════════════════════════════════╗");
                LoggingUtility.Log($"[QuickButtons] ║ LoadLast10Transactions COMPLETED SUCCESSFULLY");
                LoggingUtility.Log($"[QuickButtons] ║ User: {currentUser}");
                LoggingUtility.Log($"[QuickButtons] ║ Visible Buttons: {quickButtons?.Count(b => b.Visible) ?? 0}");
                LoggingUtility.Log($"[QuickButtons] ╚══════════════════════════════════════════════════════════╝");
                LoggingUtility.Log($"[QuickButtons] ════════════════════════════════════════════════════════════");
                LoggingUtility.Log($"");
                
                Service_DebugTracer.TraceMethodExit(new { VisibleButtons = quickButtons?.Count(b => b.Visible) ?? 0 }, 
                    nameof(Control_QuickButtons), nameof(LoadLast10Transactions));
            }
            catch (Exception ex)
            {
                LoggingUtility.Log($"[QuickButtons] ╔══════════════════════════════════════════════════════════╗");
                LoggingUtility.Log($"[QuickButtons] ║ ERROR in LoadLast10Transactions");
                LoggingUtility.Log($"[QuickButtons] ║ {ex.Message}");
                LoggingUtility.Log($"[QuickButtons] ╚══════════════════════════════════════════════════════════╝");
                LoggingUtility.LogApplicationError(ex);
                
                // Clear all buttons on error to prevent showing control names
                if (quickButtons != null)
                {
                    for (int j = 0; j < quickButtons.Count; j++)
                    {
                        quickButtons[j].Text = string.Empty;
                        quickButtons[j].Visible = false;
                    }
                    RefreshButtonLayout();
                }
            }
        }

        private void RefreshButtonLayout()
        {
            // Force UI refresh: clear and re-add only visible buttons
            Control_QuickButtons_TableLayoutPanel_Main.SuspendLayout();
            Control_QuickButtons_TableLayoutPanel_Main.Controls.Clear();
            
            if (quickButtons != null)
            {
                for (int j = 0; j < quickButtons.Count; j++)
                {
                    if (quickButtons[j].Visible)
                    {
                        Control_QuickButtons_TableLayoutPanel_Main.Controls.Add(quickButtons[j], 0, j);
                    }
                }
            }
            
            Control_QuickButtons_TableLayoutPanel_Main.ResumeLayout();
        }

        private void PopulateQuickButtonLayout(Button button, string partId, string operation, int quantity)
        {
            button.Controls.Clear();
            button.Text = string.Empty;
            
            // Button configuration: Auto-fill row, minimal padding
            button.Padding = new Padding(4);
            button.AutoSize = false; // Button fills row, doesn't auto-size
            button.UseVisualStyleBackColor = false; // Need to set BackColor for hover effects
            button.BackColor = SystemColors.Control;
            button.ForeColor = SystemColors.ControlText;
            button.FlatStyle = FlatStyle.Flat; // Flat style for custom hover effects
            button.FlatAppearance.BorderSize = 1;
            button.FlatAppearance.BorderColor = SystemColors.ControlDark;

            // Calculate adaptive font size based on button height
            // Base calculation: button height / 6 for Part ID, / 8 for Op/Qty
            float buttonHeight = Math.Max(button.Height, 40); // Minimum 40px for safety
            float partIdFontSize = Math.Max(8f, Math.Min(14f, buttonHeight / 4.5f)); // 8-14pt range
            float detailFontSize = Math.Max(7f, Math.Min(11f, buttonHeight / 6f));   // 7-11pt range

            // TableLayoutPanel: Fill button completely, constrained to button size
            var tableLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 2,
                AutoSize = false,  // Don't auto-size - stay within button bounds
                Padding = new Padding(2),
                Margin = Padding.Empty,
                BackColor = Color.Transparent
            };

            // Columns: 50% / 50%
            tableLayout.ColumnStyles.Clear();
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            // Rows: First row larger for Part ID, second row for Op/Qty
            tableLayout.RowStyles.Clear();
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 55F)); // Part ID row
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 45F)); // Op/Qty row

            // Part ID Label: Bold, adaptive font, centered, spans both columns
            var lblPartId = new Label
            {
                Text = partId,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", partIdFontSize, FontStyle.Bold, GraphicsUnit.Point),
                AutoSize = false,  // Don't auto-size - fit within cell
                AutoEllipsis = true, // Show ... if text too long
                BackColor = Color.Transparent,
                Margin = new Padding(2, 0, 2, 0),
                Padding = Padding.Empty
            };
            tableLayout.Controls.Add(lblPartId, 0, 0);
            tableLayout.SetColumnSpan(lblPartId, 2);

            // Operation Label: Adaptive font, centered, left column
            var lblOperation = new Label
            {
                Text = $"Op: {operation}",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", detailFontSize, FontStyle.Regular, GraphicsUnit.Point),
                AutoSize = false,  // Don't auto-size - fit within cell
                AutoEllipsis = true, // Show ... if text too long
                BackColor = Color.Transparent,
                Margin = new Padding(2, 0, 1, 0),
                Padding = Padding.Empty
            };
            tableLayout.Controls.Add(lblOperation, 0, 1);

            // Quantity Label: Adaptive font, centered, right column
            var lblQuantity = new Label
            {
                Text = $"Qty: {quantity}",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", detailFontSize, FontStyle.Regular, GraphicsUnit.Point),
                AutoSize = false,  // Don't auto-size - fit within cell
                AutoEllipsis = true, // Show ... if text too long
                BackColor = Color.Transparent,
                Margin = new Padding(1, 0, 2, 0),
                Padding = Padding.Empty
            };
            tableLayout.Controls.Add(lblQuantity, 1, 1);

            button.Controls.Add(tableLayout);

            // Wire up mouse events for hover/click effects
            // Note: With nested controls (labels), we need to wire events to both button and children
            button.MouseEnter += QuickButton_MouseEnter;
            button.MouseLeave += QuickButton_MouseLeave;
            button.MouseDown += QuickButton_MouseDown;
            button.MouseUp += QuickButton_MouseUp;
            
            // Wire child control events to propagate to button
            WireMouseEventsToChildren(button, tableLayout);
            WireMouseEventsToChildren(button, lblPartId);
            WireMouseEventsToChildren(button, lblOperation);
            WireMouseEventsToChildren(button, lblQuantity);

            // Pass clicks through from labels to button
            lblPartId.Click += (s, e) => button.PerformClick();
            lblOperation.Click += (s, e) => button.PerformClick();
            lblQuantity.Click += (s, e) => button.PerformClick();
        }

        /// <summary>
        /// Wires mouse events from child controls to propagate to parent button for hover/click effects
        /// </summary>
        private void WireMouseEventsToChildren(Button parentButton, Control childControl)
        {
            childControl.MouseEnter += (s, e) => QuickButton_MouseEnter(parentButton, e);
            childControl.MouseLeave += (s, e) => QuickButton_MouseLeave(parentButton, e);
            childControl.MouseDown += (s, e) => QuickButton_MouseDown(parentButton, e);
            childControl.MouseUp += (s, e) => QuickButton_MouseUp(parentButton, e);
        }

        /// <summary>
        /// Handles mouse enter for hover effect using system colors
        /// </summary>
        private void QuickButton_MouseEnter(object? sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                btn.BackColor = SystemColors.ControlLight;
            }
        }

        /// <summary>
        /// Handles mouse leave to restore normal color
        /// </summary>
        private void QuickButton_MouseLeave(object? sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                btn.BackColor = SystemColors.Control;
            }
        }

        /// <summary>
        /// Handles mouse down for pressed effect using system colors
        /// </summary>
        private void QuickButton_MouseDown(object? sender, MouseEventArgs e)
        {
            if (sender is Button btn)
            {
                btn.BackColor = SystemColors.ControlDark;
            }
        }

        /// <summary>
        /// Handles mouse up to restore hover or normal color
        /// </summary>
        private void QuickButton_MouseUp(object? sender, MouseEventArgs e)
        {
            if (sender is Button btn)
            {
                // Check if mouse is still over button - if yes, use hover color, else normal color
                if (btn.ClientRectangle.Contains(btn.PointToClient(Cursor.Position)))
                {
                    btn.BackColor = SystemColors.ControlLight;
                }
                else
                {
                    btn.BackColor = SystemColors.Control;
                }
            }
        }

        private static void SetComboBoxText(object control, string fieldName, string value)
        {
            FieldInfo? field = control.GetType().GetField(fieldName,
                BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            
            var fieldValue = field?.GetValue(control);
            
            // Handle TextBox/SuggestionTextBox controls
            if (fieldValue is TextBox tb)
            {
                tb.Text = value;
                
                // Set proper ForeColor for SuggestionTextBox to prevent error coloring
                tb.ForeColor = Model_Application_Variables.UserUiColors.ComboBoxForeColor ?? Color.Black;
                
                // Update Model_Application_Variables based on field name to sync state
                if (fieldName.Contains("Part"))
                {
                    Model_Application_Variables.PartId = value;
                }
                else if (fieldName.Contains("Operation"))
                {
                    Model_Application_Variables.Operation = value;
                }
                else if (fieldName.Contains("Location"))
                {
                    Model_Application_Variables.Location = value;
                }
                
                return;
            }
            
            // Handle ComboBox controls (legacy)
            if (fieldValue is not ComboBox cb)
            {
                return;
            }

            // Search for the item by display text (not SelectedValue which expects integer ID)
            // This handles multi-column ComboBoxes where DisplayMember is the text we want to match
            if (cb.DataSource != null && !string.IsNullOrEmpty(cb.DisplayMember))
            {
                // Search through items by display text
                for (int i = 0; i < cb.Items.Count; i++)
                {
                    string? displayText = cb.GetItemText(cb.Items[i]);
                    if (string.IsNullOrEmpty(displayText)) continue;
                    
                    // Match if display text starts with the value (handles "PartID | Customer | Description" format)
                    if (displayText.StartsWith(value, StringComparison.OrdinalIgnoreCase))
                    {
                        cb.SelectedIndex = i;
                        return;
                    }
                }
                
                // If no match found, set text directly (allows typing)
                cb.Text = value;
            }
            else
            {
                // Fallback to text search for simple ComboBoxes
                cb.SelectedIndex = cb.FindStringExact(value);
                if (cb.SelectedIndex < 0)
                {
                    cb.Text = value;
                }
            }
        }

        private static void SetTextBoxText(object control, string fieldName, string value)
        {
            const BindingFlags bindingFlags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance;
            FieldInfo? field = control.GetType().GetField(fieldName, bindingFlags);
            if (field?.GetValue(control) is SuggestionTextBoxWithLabel tb)
            {
                tb.Text = value;
                return;
            }

            field = control.GetType().GetField(fieldName, bindingFlags);
            if (field?.GetValue(control) is SuggestionTextBox tbLegacy)
            {
                tbLegacy.Text = value;
            }
        }

        private void QuickButton_Click(object? sender, EventArgs? e) // Changed from static to instance method
        {
            if (sender is not Button btn)
            {
                return;
            }

            object? tagObj = btn.Tag;
            if (tagObj == null)
            {
                return;
            }

            dynamic tag = tagObj;
            string partId = tag.partId;
            string operation = tag.operation;
            int quantity = tag.quantity;
            Forms.MainForm.MainForm? mainForm = MainFormInstance;
            if (mainForm == null)
            {
                return;
            }

            void SetComboBoxes(object control, string partField, string opField, string part, string op)
            {
                SetComboBoxText(control, partField, part);
                SetComboBoxText(control, opField, op);
            }

            void TriggerEnterEvent(Control control)
            {
                EventArgs enterEventArgs = EventArgs.Empty;
                MethodInfo? onEnterMethod =
                    control.GetType().GetMethod("OnEnter", BindingFlags.NonPublic | BindingFlags.Instance);
                onEnterMethod?.Invoke(control, new object[] { enterEventArgs });
            }

            void SetFocusOnControl(object parentControl, string fieldName)
            {
                FieldInfo? field = parentControl.GetType().GetField(fieldName,
                    BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                if (field?.GetValue(parentControl) is Control targetControl && targetControl.CanFocus)
                {
                    targetControl.Focus();
                    TriggerEnterEvent(targetControl);
                }
            }

            void ClickSearchButtonIfAvailable(object control, string fieldName, bool allowHidden = false, string? handlerName = null)
            {
                FieldInfo? field =
                    control.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
                if (field?.GetValue(control) is Button searchButton && searchButton.Enabled)
                {
                    if (searchButton.Visible)
                    {
                        searchButton.PerformClick();
                    }
                    else if (allowHidden && !string.IsNullOrWhiteSpace(handlerName))
                    {
                        MethodInfo? handler = control.GetType().GetMethod(handlerName,
                            BindingFlags.NonPublic | BindingFlags.Instance);
                        handler?.Invoke(control, new object?[] { searchButton, EventArgs.Empty });
                    }
                }
            }

            if (mainForm.MainForm_UserControl_InventoryTab?.Visible == true)
            {
                Control_InventoryTab? inv = mainForm.MainForm_UserControl_InventoryTab;                
                SetTextBoxText(inv, "Control_InventoryTab_SuggestionBox_Part", partId);
                SetTextBoxText(inv, "Control_InventoryTab_SuggestionBox_Operation", operation);
                SetTextBoxText(inv, "Control_InventoryTab_SuggestionBox_Quantity", quantity.ToString());
                SetFocusOnControl(inv, "Control_InventoryTab_SuggestionBox_Location");
                mainForm.MainForm_UserControl_InventoryTab.UpdateColorCodeFieldsVisibility();
                return;
            }

            if (mainForm.MainForm_UserControl_RemoveTab?.Visible == true)
            {
                Control_RemoveTab? rem = mainForm.MainForm_UserControl_RemoveTab;
                SetTextBoxText(rem, "Control_RemoveTab_TextBox_Part", partId);
                SetTextBoxText(rem, "Control_RemoveTab_TextBox_Operation", operation);
                SetFocusOnControl(rem, "Control_RemoveTab_TextBox_Operation");
                rem.Focus();
                TriggerEnterEvent(rem);
                ClickSearchButtonIfAvailable(rem, "Control_RemoveTab_Button_Search");
                return;
            }

            if (mainForm.MainForm_UserControl_TransferTab?.Visible == true)
            {
                Control_TransferTab? trn = mainForm.MainForm_UserControl_TransferTab;
                SetTextBoxText(trn, "Control_TransferTab_TextBox_Part", partId);
                SetTextBoxText(trn, "Control_TransferTab_TextBox_Operation", operation);
                trn.Focus();
                TriggerEnterEvent(trn);
                SetFocusOnControl(trn, "Control_TransferTab_TextBox_ToLocation");
                ClickSearchButtonIfAvailable(trn, "Control_TransferTab_Button_Search",
                    allowHidden: true,
                    handlerName: "Control_TransferTab_Button_Search_Click");
                return;
            }

            if (mainForm.MainForm_UserControl_AdvancedInventory?.Visible == true)
            {
                Control_AdvancedInventory? advInv = mainForm.MainForm_UserControl_AdvancedInventory;
                FieldInfo? tabControlField = advInv.GetType().GetField("AdvancedInventory_TabControl",
                    BindingFlags.NonPublic | BindingFlags.Instance);
                TabControl? tabControl = tabControlField?.GetValue(advInv) as TabControl;
                if (tabControl != null)
                {
                    TabPage? selectedTab = tabControl.SelectedTab;
                    if (selectedTab != null && selectedTab.Name == "AdvancedInventory_TabControl_MultiLoc")
                    {
                        // Multi-Location tab - use SuggestionTextBox controls
                        SetTextBoxText(advInv, "AdvancedInventory_MultiLoc_TextBox_Part", partId);
                        SetTextBoxText(advInv, "AdvancedInventory_MultiLoc_TextBox_Op", operation);
                        SetTextBoxText(advInv, "AdvancedInventory_MultiLoc_TextBox_Qty", quantity.ToString());
                        SetFocusOnControl(advInv, "AdvancedInventory_MultiLoc_TextBox_Loc");
                    }
                    else
                    {
                        // Single Entry tab - use SuggestionTextBox controls
                        SetTextBoxText(advInv, "AdvancedInventory_Single_TextBox_Part", partId);
                        SetTextBoxText(advInv, "AdvancedInventory_Single_TextBox_Op", operation);
                        SetTextBoxText(advInv, "AdvancedInventory_Single_TextBox_Qty", quantity.ToString());
                        SetFocusOnControl(advInv, "AdvancedInventory_Single_TextBox_Loc");
                    }
                }

                advInv.Focus();
                TriggerEnterEvent(advInv);
                return;
            }

            if (mainForm.MainForm_UserControl_AdvancedRemove?.Visible == true)
            {
                Control_AdvancedRemove? advRem = mainForm.MainForm_UserControl_AdvancedRemove;
                SetComboBoxes(advRem, "Control_AdvancedRemove_ComboBox_Part", "Control_AdvancedRemove_ComboBox_Op",
                    partId, operation);
                advRem.Focus();
                TriggerEnterEvent(advRem);
                ClickSearchButtonIfAvailable(advRem, "Control_AdvancedRemove_Button_Search");
                return;
            }
        }

        private async void MenuItemEdit_Click(object? sender, EventArgs? e)
        {
                if (Control_QuickButtons_ContextMenu.SourceControl is Button btn && btn.Tag != null && quickButtons != null)
            {
                int idx = quickButtons.IndexOf(btn);
                int position = idx + 1; // Convert 0-based index to 1-based position
                object tagObj = btn.Tag;
                    if (tagObj == null)
                    {
                        return;
                    }
                    dynamic tag = tagObj;
                    string oldPartId = tag.partId;
                    string oldOperation = tag.operation;
                    int oldQuantity = tag.quantity;
                string user = Model_Application_Variables.User;

                using Form_QuickButtonEdit dlg = new(oldPartId, oldOperation, oldQuantity);
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    // Check if the edited values create a duplicate (same PartID + Operation already exists)
                    bool isDuplicate = false;
                    foreach (Button otherBtn in quickButtons)
                    {
                        if (otherBtn == btn) continue; // Skip the current button being edited
                        
                        if (otherBtn.Tag != null)
                        {
                            object otherTagObj = otherBtn.Tag;
                            if (otherTagObj == null) continue;
                            dynamic otherTag = otherTagObj;
                            string otherPartId = otherTag.partId?.ToString() ?? "";
                            string otherOperation = otherTag.operation?.ToString() ?? "";
                            
                            if (string.Equals(otherPartId, dlg.PartId, StringComparison.OrdinalIgnoreCase) &&
                                string.Equals(otherOperation, dlg.Operation, StringComparison.OrdinalIgnoreCase))
                            {
                                isDuplicate = true;
                                break;
                            }
                        }
                    }
                    
                    if (isDuplicate)
                    {
                        LoggingUtility.Log($"[QuickButtons] Edit cancelled - PartID '{dlg.PartId}' + Operation '{dlg.Operation}' already exists");
                        MessageBox.Show(
                            $"A quick button for Part '{dlg.PartId}' with Operation '{dlg.Operation}' already exists.\n\nNo changes were made.",
                            "Duplicate Quick Button",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        return;
                    }
                    
                    try
                    {
                        // Update the button in the database
                        var result = await Dao_QuickButtons.UpdateQuickButtonAsync(user, position, dlg.PartId, dlg.Operation,
                            dlg.Quantity);
                        if (!result.IsSuccess)
                        {
                            Service_ErrorHandler.HandleDatabaseError(
                                result.Exception ?? new Exception(result.ErrorMessage),
                                contextData: new Dictionary<string, object>
                                {
                                    ["User"] = user,
                                    ["ButtonIndex"] = idx,
                                    ["Position"] = position,
                                    ["PartId"] = dlg.PartId,
                                    ["Operation"] = dlg.Operation,
                                    ["Quantity"] = dlg.Quantity
                                },
                                methodName: nameof(MenuItemEdit_Click),
                                dbSeverity: Enum_DatabaseEnum_ErrorSeverity.Error);
                            return;
                        }
                        
                        // CRITICAL: Reload from database to ensure UI matches database state
                        await LoadLast10Transactions(user);
                        LoggingUtility.Log($"[QuickButtons] Successfully updated quick button at position {position}");
                    }
                    catch (MySqlException ex)
                    {
                        LoggingUtility.LogDatabaseError(ex);
                        Service_ErrorHandler.HandleDatabaseError(ex,
                            contextData: new Dictionary<string, object>
                            {
                                ["User"] = user,
                                ["ButtonIndex"] = idx,
                                ["Position"] = position
                            },
                            methodName: nameof(MenuItemEdit_Click),
                            dbSeverity: Enum_DatabaseEnum_ErrorSeverity.Error);
                    }
                    catch (Exception ex)
                    {
                        LoggingUtility.LogApplicationError(ex);
                        Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                            contextData: new Dictionary<string, object>
                            {
                                ["User"] = user,
                                ["ButtonIndex"] = idx,
                                ["Position"] = position
                            },
                            controlName: nameof(Control_QuickButtons));
                    }
                }
            }
        }

        private async void MenuItemRemove_Click(object? sender, EventArgs? e)
        {
            if (Control_QuickButtons_ContextMenu.SourceControl is Button btn && quickButtons != null)
            {
                int idx = quickButtons.IndexOf(btn);
                if (idx >= 0 && btn.Tag is not null)
                {
                    string user = Model_Application_Variables.User;
                    int position = idx + 1; // Convert 0-based index to 1-based position
                    
                    try
                    {
                        var result = await Dao_QuickButtons.RemoveQuickButtonAndShiftAsync(user, position);
                        if (!result.IsSuccess)
                        {
                            Service_ErrorHandler.HandleDatabaseError(
                                result.Exception ?? new Exception(result.ErrorMessage),
                                contextData: new Dictionary<string, object>
                                {
                                    ["User"] = user,
                                    ["ButtonIndex"] = idx,
                                    ["Position"] = position
                                },
                                methodName: nameof(MenuItemRemove_Click),
                                dbSeverity: Enum_DatabaseEnum_ErrorSeverity.Error);
                            return;
                        }
                        
                        // CRITICAL: Reload from database to ensure UI matches database state
                        await LoadLast10Transactions(user);
                        LoggingUtility.Log($"[QuickButtons] Successfully removed quick button at position {position}");
                    }
                    catch (MySqlException ex)
                    {
                        LoggingUtility.LogDatabaseError(ex);
                        Service_ErrorHandler.HandleDatabaseError(ex,
                            contextData: new Dictionary<string, object>
                            {
                                ["User"] = user,
                                ["ButtonIndex"] = idx,
                                ["Position"] = position
                            },
                            methodName: nameof(MenuItemRemove_Click),
                            dbSeverity: Enum_DatabaseEnum_ErrorSeverity.Error);
                    }
                    catch (Exception ex)
                    {
                        LoggingUtility.LogApplicationError(ex);
                        Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                            contextData: new Dictionary<string, object>
                            {
                                ["User"] = user,
                                ["ButtonIndex"] = idx,
                                ["Position"] = position
                            },
                            controlName: nameof(Control_QuickButtons));
                    }
                }
                else
                {
                    LoggingUtility.Log($"[QuickButtons] MenuItemRemove_Click: Invalid button index or Tag is null. idx={idx}");
                }
            }
        }

        private async void MenuItemReorder_Click(object? sender, EventArgs? e)
        {
            if (quickButtons == null)
            {
                return;
            }

            using Form_QuickButtonOrder dlg = new(quickButtons);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                List<Button> newOrder = dlg.GetButtonOrder();
                string user = Model_Application_Variables.User;
                
                // Remove all quick buttons for the user from the server
                var deleteResult = await Dao_QuickButtons.DeleteAllQuickButtonsForUserAsync(user);
                if (!deleteResult.IsSuccess)
                {
                    Service_ErrorHandler.HandleDatabaseError(
                        deleteResult.Exception ?? new Exception(deleteResult.ErrorMessage),
                        contextData: new Dictionary<string, object>
                        {
                            ["User"] = user,
                            ["Operation"] = "DeleteAllQuickButtons"
                        },
                        methodName: nameof(MenuItemReorder_Click),
                        dbSeverity: Enum_DatabaseEnum_ErrorSeverity.Error);
                    return;
                }
                
                // Re-add them in the exact order shown in ListView
                for (int i = 0; i < newOrder.Count; i++)
                {
                    Button btn = newOrder[i];
                    if (btn.Tag == null) continue; // Skip if no tag
                    
                    dynamic tag = btn.Tag!; // Null-forgiving operator since we checked above
                    string partId = tag?.partId ?? string.Empty;
                    string operation = tag?.operation ?? string.Empty;
                    int quantity = tag?.quantity ?? 0;
                    
                    // Use the new method that doesn't shift - directly insert at position i+1
                    var addResult = await Dao_QuickButtons.AddQuickButtonAtPositionAsync(user, partId, operation, quantity, i + 1);
                    if (!addResult.IsSuccess)
                    {
                        Service_ErrorHandler.HandleDatabaseError(
                            addResult.Exception ?? new Exception(addResult.ErrorMessage),
                            contextData: new Dictionary<string, object>
                            {
                                ["User"] = user,
                                ["Position"] = i + 1,
                                ["PartId"] = partId,
                                ["Operation"] = operation,
                                ["Quantity"] = quantity
                            },
                            methodName: nameof(MenuItemReorder_Click),
                            dbSeverity: Enum_DatabaseEnum_ErrorSeverity.Error);
                        // Continue with remaining buttons despite error
                    }
                }

                // CRITICAL: Cleanup after all adds to ensure data integrity
                var cleanupResult = await Dao_QuickButtons.CleanupGapsAndDuplicatesAsync(user);
                if (!cleanupResult.IsSuccess)
                {
                    LoggingUtility.Log($"[QuickButtons] Cleanup after reorder failed: {cleanupResult.ErrorMessage}");
                }

                // CRITICAL: Reload from database to ensure UI matches database state
                await LoadLast10Transactions(user);
            }
        }

        #endregion Methods
    }
}
