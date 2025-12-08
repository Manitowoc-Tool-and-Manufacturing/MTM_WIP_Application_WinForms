using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Forms.Help;

namespace MTM_WIP_Application_Winforms.Forms.Shared
{
    /// <summary>
    /// Modal dialog to capture a new shortcut key combination.
    /// </summary>
    public partial class Form_ShortcutEdit : ThemedForm
    {
        #region Fields

        private Keys _capturedKeys = Keys.None;

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
            InitializeComponent();
            InitializeHelpButton();
            
            this.Text = $"Edit Shortcut: {shortcutName}";
            _capturedKeys = currentKeys;
            UpdateDisplay();

            // Wire up events
            _clearButton.Click += _clearButton_Click;
        }

        #endregion

        #region Methods

        private void _clearButton_Click(object? sender, EventArgs e)
        {
            _capturedKeys = Keys.None;
            UpdateDisplay();
        }

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
    #region Helpers

    private Button? Form_ShortcutEdit_Button_Help;

    private void InitializeHelpButton()
    {
        Form_ShortcutEdit_Button_Help = new Button();
        Form_ShortcutEdit_Button_Help.Name = "Form_ShortcutEdit_Button_Help";
        Form_ShortcutEdit_Button_Help.Text = "?";
        Form_ShortcutEdit_Button_Help.Size = new Size(24, 24);
        Form_ShortcutEdit_Button_Help.Location = new Point(this.Width - 40, 5); 
        Form_ShortcutEdit_Button_Help.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        Form_ShortcutEdit_Button_Help.Click += (s, e) => 
        {
            HelpViewerForm.GetInstance().BringToFrontAndNavigate("settings-management", "shortcuts");
        };
        
        this.Controls.Add(Form_ShortcutEdit_Button_Help);
        Form_ShortcutEdit_Button_Help.BringToFront();
    }

    #endregion

    }
}
