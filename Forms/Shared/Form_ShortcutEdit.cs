using MTM_WIP_Application_Winforms.Forms.Shared;

namespace MTM_WIP_Application_Winforms.Forms.Shared
{
    /// <summary>
    /// Modal dialog to capture a new shortcut key combination.
    /// </summary>
    public partial class Form_ShortcutEdit : ThemedForm
    {
        #region Fields

        private Keys _capturedKeys = Keys.None;
        private readonly Label _instructionLabel;
        private readonly Label _keyDisplayLabel;
        private readonly Button _saveButton;
        private readonly Button _cancelButton;
        private readonly Button _clearButton;

        #endregion

        #region Properties

        /// <summary>
        /// The key combination selected by the user.
        /// </summary>
        public Keys SelectedKeys => _capturedKeys;

        #endregion

        #region Constructors

        public Form_ShortcutEdit(string shortcutName, Keys currentKeys)
        {
            this.Text = $"Edit Shortcut: {shortcutName}";
            this.Size = new Size(400, 250);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.KeyPreview = true;

            _capturedKeys = currentKeys;

            // Instruction Label
            _instructionLabel = new Label
            {
                Text = "Press the new key combination...",
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter,
                Height = 40,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular)
            };

            // Key Display Label
            _keyDisplayLabel = new Label
            {
                Text = FormatKeys(currentKeys),
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter,
                Height = 80,
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 120, 212),
                BackColor = Color.WhiteSmoke
            };

            // Buttons Panel
            var buttonPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 60,
                Padding = new Padding(10)
            };

            _saveButton = new Button
            {
                Text = "Save",
                DialogResult = DialogResult.OK,
                Width = 80,
                Height = 30,
                Anchor = AnchorStyles.Right | AnchorStyles.Bottom,
                Location = new Point(200, 15)
            };

            _cancelButton = new Button
            {
                Text = "Cancel",
                DialogResult = DialogResult.Cancel,
                Width = 80,
                Height = 30,
                Anchor = AnchorStyles.Right | AnchorStyles.Bottom,
                Location = new Point(290, 15)
            };

            _clearButton = new Button
            {
                Text = "Clear Shortcut",
                Width = 100,
                Height = 30,
                Anchor = AnchorStyles.Left | AnchorStyles.Bottom,
                Location = new Point(10, 15)
            };
            _clearButton.Click += (s, e) =>
            {
                _capturedKeys = Keys.None;
                UpdateDisplay();
            };

            buttonPanel.Controls.Add(_clearButton);
            buttonPanel.Controls.Add(_saveButton);
            buttonPanel.Controls.Add(_cancelButton);

            this.Controls.Add(buttonPanel);
            this.Controls.Add(_keyDisplayLabel);
            this.Controls.Add(_instructionLabel);

            this.KeyDown += Form_KeyDown;
        }

        #endregion

        #region Methods

        private void Form_KeyDown(object? sender, KeyEventArgs e)
        {
            // Ignore modifier keys alone
            if (e.KeyCode == Keys.ControlKey || e.KeyCode == Keys.ShiftKey || e.KeyCode == Keys.Menu)
            {
                return;
            }

            // Capture the key combination
            _capturedKeys = e.KeyData;
            UpdateDisplay();
            
            // Prevent default handling
            e.SuppressKeyPress = true;
            e.Handled = true;
        }

        private void UpdateDisplay()
        {
            _keyDisplayLabel.Text = FormatKeys(_capturedKeys);
        }

        private string FormatKeys(Keys keys)
        {
            if (keys == Keys.None) return "(None)";
            
            var parts = new List<string>();
            if ((keys & Keys.Control) == Keys.Control) parts.Add("Ctrl");
            if ((keys & Keys.Alt) == Keys.Alt) parts.Add("Alt");
            if ((keys & Keys.Shift) == Keys.Shift) parts.Add("Shift");

            var keyCode = keys & Keys.KeyCode;
            if (keyCode != Keys.None && keyCode != Keys.ControlKey && keyCode != Keys.ShiftKey && keyCode != Keys.Menu)
            {
                parts.Add(keyCode.ToString());
            }

            return string.Join(" + ", parts);
        }

        #endregion
    }
}
