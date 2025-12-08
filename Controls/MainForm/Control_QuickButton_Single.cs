using System.ComponentModel;

namespace MTM_WIP_Application_Winforms.Controls.MainForm
{
    /// <summary>
    /// Single QuickButton control with visual designer support.
    /// Displays hotkey, part number, operation, and quantity in a compact layout.
    /// </summary>
    [ToolboxItem(true)]
    [Description("QuickButton control for displaying transaction shortcuts")]
    public partial class Control_QuickButton_Single : UserControl
    {
        #region Fields

        private string _hotkeyText = "F1";
        private string _partId = "";
        private string _operation = "";
        private int _quantity = 0;
        private string _workOrder = "";
        private string _colorCode = "";

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the hotkey text (F1-F10).
        /// </summary>
        [Category("Appearance")]
        [Description("The hotkey text to display (F1-F10).")]
        [DefaultValue("F1")]
        public string HotkeyText
        {
            get => _hotkeyText;
            set
            {
                _hotkeyText = value;
                if (_lblHotkey != null)
                {
                    // Format multi-key shortcuts to fit in small label
                    _lblHotkey.Text = FormatHotkeyForDisplay(value);
                }
            }
        }

        /// <summary>
        /// Formats hotkey text for compact display with line breaks if needed.
        /// Example: "Shift+Alt+F1" becomes "Shift+Alt\nF1"
        /// </summary>
        private static string FormatHotkeyForDisplay(string hotkey)
        {
            if (string.IsNullOrWhiteSpace(hotkey))
                return "F1";

            // Handle "D1, Shift" style from Keys.ToString()
            // Replace ", " with "+" to normalize separators
            var normalized = hotkey.Replace(", ", "+");
            var parts = normalized.Split('+');
            
            var modifiers = new List<string>();
            string key = "";

            foreach (var part in parts)
            {
                var trimmed = part.Trim();
                
                // Map Control to Ctrl for brevity
                if (trimmed == "Control") trimmed = "Ctrl";
                
                if (trimmed == "Shift" || trimmed == "Ctrl" || trimmed == "Alt")
                {
                    modifiers.Add(trimmed);
                }
                else
                {
                    // Handle D0-D9 -> 0-9
                    if (trimmed.Length == 2 && trimmed.StartsWith("D") && char.IsDigit(trimmed[1]))
                    {
                        key = trimmed.Substring(1);
                    }
                    else
                    {
                        key = trimmed;
                    }
                }
            }

            // Reconstruct list: Modifiers first, then Key
            var displayParts = new List<string>(modifiers);
            if (!string.IsNullOrEmpty(key))
            {
                displayParts.Add(key);
            }

            if (displayParts.Count == 0) return hotkey;

            if (displayParts.Count <= 1)
            {
                return displayParts[0];
            }
            else if (displayParts.Count == 2)
            {
                // Check if the first part is a modifier
                var firstPart = displayParts[0];
                if (firstPart == "Shift" || firstPart == "Ctrl" || firstPart == "Alt")
                {
                    // Modifier + Key -> Split to two lines
                    return $"{firstPart}\n{displayParts[1]}";
                }
                
                return string.Join("+", displayParts);
            }
            else
            {
                // Multiple modifiers + key
                
                if (displayParts.Count == 4)
                {
                    // 4 parts (e.g. Ctrl+Shift+Alt+Key) -> 2 on top, 2 on bottom
                    return $"{displayParts[0]}+{displayParts[1]}\n{displayParts[2]}+{displayParts[3]}";
                }

                // Default behavior (works for 3 parts: 2 on top, 1 on bottom)
                // Example: Shift+Alt+Key -> "Shift+Alt\nKey"
                var last = displayParts[^1];
                var mods = string.Join("+", displayParts.GetRange(0, displayParts.Count - 1));
                return $"{mods}\n{last}";
            }
        }

        /// <summary>
        /// Gets or sets the part ID/number.
        /// </summary>
        [Category("Data")]
        [Description("The part ID/number to display.")]
        [DefaultValue("")]
        public string PartId
        {
            get => _partId;
            set
            {
                _partId = value;
                if (_lblPartId != null)
                {
                    _lblPartId.Text = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the operation number.
        /// </summary>
        [Category("Data")]
        [Description("The operation number to display.")]
        [DefaultValue("")]
        public string Operation
        {
            get => _operation;
            set
            {
                _operation = value;
                if (_lblOperation != null)
                {
                    _lblOperation.Text = $"Op: {value}";
                }
            }
        }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        [Category("Data")]
        [Description("The quantity to display.")]
        [DefaultValue(0)]
        public int Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                if (_lblQuantity != null)
                {
                    _lblQuantity.Text = $"Qty: {value}";
                }
            }
        }

        /// <summary>
        /// Gets or sets the work order.
        /// </summary>
        [Category("Data")]
        [Description("The work order associated with this button.")]
        [DefaultValue("")]
        public string WorkOrder
        {
            get => _workOrder;
            set => _workOrder = value;
        }

        /// <summary>
        /// Gets or sets the color code.
        /// </summary>
        [Category("Data")]
        [Description("The color code associated with this button.")]
        [DefaultValue("")]
        public string ColorCode
        {
            get => _colorCode;
            set => _colorCode = value;
        }

        /// <summary>
        /// Gets or sets the border color at the top of the control.
        /// </summary>
        [Category("Appearance")]
        [Description("The color of the border at the top of the control.")]
        public Color BorderColor
        {
            get => _borderPanel?.BackColor ?? Color.SteelBlue;
            set
            {
                if (_borderPanel != null)
                {
                    _borderPanel.BackColor = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the hotkey text color.
        /// </summary>
        [Category("Appearance")]
        [Description("The color of the hotkey text.")]
        public Color HotkeyColor
        {
            get => _lblHotkey?.ForeColor ?? Color.SteelBlue;
            set
            {
                if (_lblHotkey != null)
                {
                    _lblHotkey.ForeColor = value;
                }
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Control_QuickButton_Single class.
        /// </summary>
        public Control_QuickButton_Single()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Updates the font sizes based on control height (for responsive design).
        /// </summary>
        public void UpdateFontSizes()
        {
            // if (_lblPartId == null || _lblOperation == null || _lblQuantity == null)
            //     return;

            // float buttonHeight = Math.Max(this.Height, 40);
            // float partIdFontSize = Math.Max(9f, Math.Min(14f, buttonHeight / 4.5f));
            // float detailFontSize = Math.Max(7.5f, Math.Min(11f, buttonHeight / 6f));

            // _lblPartId.Font = new Font("Segoe UI Emoji", partIdFontSize, FontStyle.Bold, GraphicsUnit.Point);
            // _lblOperation.Font = new Font("Segoe UI Emoji", detailFontSize, FontStyle.Regular, GraphicsUnit.Point);
            // _lblQuantity.Font = new Font("Segoe UI Emoji", detailFontSize, FontStyle.Regular, GraphicsUnit.Point);
        }

        /// <summary>
        /// Sets all data properties at once.
        /// </summary>
        public void SetData(string hotkeyText, string partId, string operation, int quantity, string workOrder = "", string colorCode = "")
        {
            HotkeyText = hotkeyText;
            PartId = partId;
            Operation = operation;
            Quantity = quantity;
            WorkOrder = workOrder;
            ColorCode = colorCode;
            
            // Tooltip is now handled by the parent control (Control_QuickButtons)
            // UpdateTooltip(); 
        }

        /// <summary>
        /// Programmatically raises the Click event (simulates user click).
        /// </summary>
        public void PerformClick()
        {
            OnClick(EventArgs.Empty);
        }

        /// <summary>
        /// Updates the tooltip to display all button information.
        /// </summary>
        private void UpdateTooltip()
        {
            var tooltipText = $"Hotkey: {_hotkeyText}\n" +
                             $"Part: {_partId}\n" +
                             $"Operation: {_operation}\n" +
                             $"Quantity: {_quantity}";
            
            toolTip?.SetToolTip(this, tooltipText);
        }

        #endregion

        #region Event Handlers

        // Label event handlers that forward to the control
        private void Label_Click(object? sender, EventArgs e)
        {
            OnClick(e);
        }

        private void Label_MouseEnter(object? sender, EventArgs e)
        {
            OnMouseEnter(e);
        }

        private void Label_MouseLeave(object? sender, EventArgs e)
        {
            OnMouseLeave(e);
        }

        private void Label_MouseDown(object? sender, MouseEventArgs e)
        {
            OnMouseDown(e);
        }

        private void Label_MouseUp(object? sender, MouseEventArgs e)
        {
            OnMouseUp(e);
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            
            // Revert any theme colors that may have been applied
            // This control manages its own colors
            this.BackColor = SystemColors.Control;
            if (_borderPanel != null)
            {
                _borderPanel.BackColor = Color.SteelBlue;
            }
            if (_lblHotkey != null)
            {
                _lblHotkey.ForeColor = Color.SteelBlue;
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            this.BackColor = SystemColors.ControlLight;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.BackColor = SystemColors.Control;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            this.BackColor = SystemColors.ControlDark;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            // Check if mouse is still over control
            if (this.ClientRectangle.Contains(e.Location))
            {
                this.BackColor = SystemColors.ControlLight;
            }
            else
            {
                this.BackColor = SystemColors.Control;
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateFontSizes();
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            
            // Prevent theme system from changing our background color
            if (this.BackColor != SystemColors.Control && 
                this.BackColor != SystemColors.ControlLight && 
                this.BackColor != SystemColors.ControlDark)
            {
                this.BackColor = SystemColors.Control;
            }
        }

        protected override void OnForeColorChanged(EventArgs e)
        {
            base.OnForeColorChanged(e);
            
            // Prevent theme system from changing our foreground color  
            if (this.ForeColor != SystemColors.ControlText)
            {
                this.ForeColor = SystemColors.ControlText;
            }
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            
            // Preserve our font - theme system should not change it
            // Font changes only happen through UpdateFontSizes()
        }

        protected override void OnPaddingChanged(EventArgs e)
        {
            base.OnPaddingChanged(e);
            
            // Preserve our layout - prevent theme changes
        }

        protected override void OnMarginChanged(EventArgs e)
        {
            base.OnMarginChanged(e);
            
            // Preserve our margins - prevent theme changes
        }

        #endregion
    }
}
