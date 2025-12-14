using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Services;
using MTM_WIP_Application_Winforms.Services.Logging;

namespace MTM_WIP_Application_Winforms.Components.Shared
{
    /// <summary>
    /// Dialog for reordering DataGridView columns via drag-and-drop
    /// </summary>
    public class Component_ColumnOrderDialog : Form
    {
        #region Fields

        private  ListBox? _listBox = null;
        private  Button? _btnOK = null;
        private  Button? _btnCancel = null;
        private  Button? _btnPrint = null;
        private  Label? _infoLabel = null;
        private  Label? _lblInstructions = null;
        private  List<string> _columnNames = [];
        private  List<string> _visibleColumnNames = [];
        private  List<string> _hiddenColumnNames = [];
        private int dragIndex = -1;
        private readonly DataGridView _dgv;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ColumnOrderDialog
        /// </summary>
        /// <param name="dgv">DataGridView to configure column order for</param>
        public Component_ColumnOrderDialog(DataGridView dgv)
        {
            _dgv = dgv;
            InitializeDialog();
            InitializeColumnData(dgv);
            InitializeControls(dgv);
            SetupEventHandlers();
        }

        #endregion

        #region Initialization Methods

        /// <summary>
        /// Initialize dialog properties
        /// </summary>
        private void InitializeDialog()
        {
            Text = "Change Column Order";
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoScaleDimensions = new SizeF(96F, 96F);
            Size = new Size(400, 540);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MinimizeBox = false;
            MaximizeBox = false;
            ShowInTaskbar = false;
        }

        /// <summary>
        /// Initialize column data collections
        /// </summary>
        /// <param name="dgv">DataGridView to analyze</param>
        private void InitializeColumnData(DataGridView dgv)
        {
            _columnNames = dgv.Columns.Cast<DataGridViewColumn>()
                .OrderBy(c => c.DisplayIndex)
                .Select(c => c.Name)
                .ToList();

            _visibleColumnNames = dgv.Columns.Cast<DataGridViewColumn>()
                .Where(c => c.Visible)
                .OrderBy(c => c.DisplayIndex)
                .Select(c => c.Name)
                .ToList();

            _hiddenColumnNames = dgv.Columns.Cast<DataGridViewColumn>()
                .Where(c => !c.Visible)
                .OrderBy(c => c.DisplayIndex)
                .Select(c => c.Name)
                .ToList();
        }

        /// <summary>
        /// Initialize form controls
        /// </summary>
        /// <param name="dgv">DataGridView to populate from</param>
        private void InitializeControls(DataGridView dgv)
        {
            // Initialize ListBox
            _listBox = new ListBox
            {
                Dock = DockStyle.Top,
                Height = 320,
                AllowDrop = true
            };

            foreach (var col in _visibleColumnNames)
            {
                var gridCol = dgv.Columns[col];
                _listBox.Items.Add(gridCol.HeaderText);
            }

            // Initialize Labels
            _infoLabel = new Label
            {
                Text = "Drag and drop columns to reorder.\r\nUse Shift+Up/Down to move the selected column.\r\nOnly visible columns are shown. Hidden columns will always appear to the right.",
                Dock = DockStyle.Top,
                Height = 60,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(8),
                Font = new Font(Font.FontFamily, 10, FontStyle.Regular),
                AutoSize = false,
                ForeColor = Color.Black,
                BackColor = Color.Transparent
            };

            _lblInstructions = new Label
            {
                Text = "How to use this form:\n\n- Drag and drop column names to change their order.\n- Use Shift+Up/Down to move a selected column.\n- Only visible columns are shown. Hidden columns will always appear to the right.\n- Click OK to save your changes.",
                Dock = DockStyle.Top,
                Height = 90,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(8),
                Font = new Font(Font.FontFamily, 10, FontStyle.Regular),
                AutoSize = false
            };

            // Initialize Buttons
            _btnOK = new Button { Text = "OK", DialogResult = DialogResult.OK, Dock = DockStyle.Bottom };
            _btnCancel = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel, Dock = DockStyle.Bottom };
            _btnPrint = new Button { Text = "Print...", Dock = DockStyle.Bottom };
            _btnPrint.Click += BtnPrint_Click;

            // Add controls to form
            Controls.Add(_btnOK);
            Controls.Add(_btnCancel);
            Controls.Add(_btnPrint);
            Controls.Add(_lblInstructions);
            Controls.Add(_infoLabel);
            Controls.Add(_listBox);

            AcceptButton = _btnOK;
            CancelButton = _btnCancel;
        }

        /// <summary>
        /// Setup event handlers for drag-and-drop functionality
        /// </summary>
        private void SetupEventHandlers()
        {
            if (_listBox == null) return; // Guard against null reference
            _listBox.MouseDown += ListBox_MouseDown;
            _listBox.MouseMove += ListBox_MouseMove;
            _listBox.DragOver += ListBox_DragOver;
            _listBox.DragDrop += ListBox_DragDrop;
            _listBox.KeyDown += ListBox_KeyDown;
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handle print button click
        /// </summary>
        private async void BtnPrint_Click(object? sender, EventArgs e)
        {
            try
            {
                await Helper_PrintManager.ShowPrintDialogAsync(this, _dgv, _dgv.Name ?? "Grid");
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_ErrorHandler.ShowError(
                    $"Error printing: {ex.Message}",
                    "Print Error");
            }
        }

        /// <summary>
        /// Handle mouse down event for drag operation initiation
        /// </summary>
        private void ListBox_MouseDown(object? sender, MouseEventArgs e)
        {
            if (_listBox == null) return; // Guard against null reference
            dragIndex = _listBox.IndexFromPoint(e.Location);
        }

        /// <summary>
        /// Handle mouse move event for drag operation
        /// </summary>
        private void ListBox_MouseMove(object? sender, MouseEventArgs e)
        {
            if (dragIndex >= 0 && e.Button == MouseButtons.Left)
            {
                if (_listBox == null) return; // Guard against null reference
                _listBox.DoDragDrop(_listBox.Items[dragIndex], DragDropEffects.Move);
            }
        }

        /// <summary>
        /// Handle drag over event
        /// </summary>
        private void ListBox_DragOver(object? sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        /// <summary>
        /// Handle drag drop event to reorder items
        /// </summary>
        private void ListBox_DragDrop(object? sender, DragEventArgs e)
        {
            if (_listBox == null) return; // Guard against null reference
            Point point = _listBox.PointToClient(new Point(e.X, e.Y));
            int index = _listBox.IndexFromPoint(point);
            if (index < 0) index = _listBox.Items.Count - 1;
            object data = e.Data?.GetData(typeof(string)) ?? "";
            if (dragIndex >= 0 && dragIndex < _listBox.Items.Count)
            {
                _listBox.Items.RemoveAt(dragIndex);
                _listBox.Items.Insert(index, data);
                _listBox.SelectedIndex = index;
                dragIndex = -1;
            }
        }

        /// <summary>
        /// Handle key down events for Shift+Up/Down reordering
        /// </summary>
        private void ListBox_KeyDown(object? sender, KeyEventArgs e)
        {
            if (_listBox == null) return; // Guard against null reference

            if (e.Shift && _listBox.SelectedIndex >= 0)
            {
                int idx = _listBox.SelectedIndex;
                if (e.KeyCode == Keys.Up && idx > 0)
                {
                    var item = _listBox.Items[idx];
                    _listBox.Items.RemoveAt(idx);
                    _listBox.Items.Insert(idx - 1, item);
                    _listBox.SelectedIndex = idx - 1;
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Down && idx < _listBox.Items.Count - 1)
                {
                    var item = _listBox.Items[idx];
                    _listBox.Items.RemoveAt(idx);
                    _listBox.Items.Insert(idx + 1, item);
                    _listBox.SelectedIndex = idx + 1;
                    e.Handled = true;
                }
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get the new column order as configured by the user
        /// </summary>
        /// <returns>List of column names in the new order</returns>
        public List<string> GetColumnOrder()
        {
            if (_listBox == null)
            {
                // If listbox was not initialized, return original order
                return new List<string>(_columnNames);
            }

            var visibleOrder = new List<string>();
            foreach (var item in _listBox.Items)
            {
                var header = item?.ToString();
                if (string.IsNullOrEmpty(header)) continue;

                // Attempt to match header text to a visible column name (header text assumed equal to name)
                var colName = _visibleColumnNames.FirstOrDefault(n => string.Equals(header, n, StringComparison.Ordinal));
                if (colName == null)
                {
                    // Fallback: check all columns
                    colName = _columnNames.FirstOrDefault(n => string.Equals(header, n, StringComparison.Ordinal));
                }
                if (colName == null)
                {
                    continue; // Skip if no match found
                }
                visibleOrder.Add(colName);
            }

            // Append hidden columns in their original order
            var finalOrder = new List<string>(visibleOrder);
            finalOrder.AddRange(_hiddenColumnNames);
            return finalOrder;
        }

        #endregion
    }
}
