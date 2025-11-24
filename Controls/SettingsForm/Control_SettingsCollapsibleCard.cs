using System.ComponentModel;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Helpers;

namespace MTM_WIP_Application_Winforms.Controls.SettingsForm
{
    /// <summary>
    /// Reusable collapsible card for settings.
    /// Inherits from ThemedUserControl for consistent theming.
    /// </summary>
    public partial class Control_SettingsCollapsibleCard : ThemedUserControl
    {
        #region Fields

        private bool _isExpanded = true;
        private Color _accentColor = Color.FromArgb(0, 120, 212);
        private DateTime _lastClickTime = DateTime.MinValue;

        #endregion

        #region Events

        /// <summary>
        /// Fired when the expanded state changes.
        /// </summary>
        public event EventHandler<bool>? ExpandedChanged;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the expanded state of the card.
        /// </summary>
        [Category("Behavior")]
        [Description("Whether the card is expanded to show content")]
        public bool IsExpanded
        {
            get => _isExpanded;
            set
            {
                if (_isExpanded != value)
                {
                    _isExpanded = value;
                    UpdateExpandedState();
                    ExpandedChanged?.Invoke(this, _isExpanded);
                }
            }
        }

        /// <summary>
        /// Card title text.
        /// </summary>
        [Category("Appearance")]
        [Description("The title text displayed on the card")]
        public string CardTitle
        {
            get => _titleLabel.Text;
            set => _titleLabel.Text = value;
        }

        /// <summary>
        /// Card description text.
        /// </summary>
        [Category("Appearance")]
        [Description("The description text displayed on the card")]
        public string CardDescription
        {
            get => _descriptionLabel.Text;
            set => _descriptionLabel.Text = value;
        }

        /// <summary>
        /// Icon text (emoji or symbol).
        /// </summary>
        [Category("Appearance")]
        [Description("The icon displayed on the card (emoji or symbol)")]
        public string CardIcon
        {
            get => _iconLabel.Text;
            set => _iconLabel.Text = value;
        }

        /// <summary>
        /// Accent color for the top bar.
        /// </summary>
        [Category("Appearance")]
        [Description("The accent color for the top bar")]
        public Color AccentColor
        {
            get => _accentColor;
            set
            {
                _accentColor = value;
                _accentBar.BackColor = value;
                _titleLabel.ForeColor = value;
            }
        }

        #endregion

        #region Constructors

        public Control_SettingsCollapsibleCard()
        {
            InitializeComponent();
            
            // Wire up events for all header children to ensure clicking anywhere toggles the card
            if (_headerPanel != null)
            {
                WireUpHeaderEvents(_headerPanel);
            }
            
            // Initial state
            UpdateExpandedState();
        }

        private void WireUpHeaderEvents(Control parent)
        {
            foreach (Control child in parent.Controls)
            {
                // Skip Labels as they are already wired in Designer to prevent double-firing
                if (child is not Label)
                {
                    // Remove existing handlers first to prevent duplicates if called multiple times
                    child.Click -= Header_Click;
                    child.MouseEnter -= Header_MouseEnter;
                    child.MouseLeave -= Header_MouseLeave;

                    child.Click += Header_Click;
                    child.MouseEnter += Header_MouseEnter;
                    child.MouseLeave += Header_MouseLeave;
                }
                
                if (child.HasChildren)
                {
                    WireUpHeaderEvents(child);
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds content control to the card body.
        /// </summary>
        public void AddContent(Control content)
        {
            content.Dock = DockStyle.Top;
            _contentPanel.Controls.Add(content);
            _contentPanel.Controls.SetChildIndex(content, 0); // Add to top
            AdjustHeight();
        }

        /// <summary>
        /// Clears all content from the card body.
        /// </summary>
        public void ClearContent()
        {
            _contentPanel.Controls.Clear();
            AdjustHeight();
        }

        public void AdjustHeight()
        {
            if (_isExpanded)
            {
                int contentHeight = 0;
                foreach (Control ctrl in _contentPanel.Controls)
                {
                    contentHeight += ctrl.Height + ctrl.Margin.Vertical;
                }
                // Header + Accent + Separator + Padding + Content
                this.Height = 60 + 4 + 1 + 20 + contentHeight; 
            }
            else
            {
                // Header + Accent + Border
                this.Height = 60 + 4 + 2;
            }
        }

        private void Header_Click(object? sender, EventArgs e)
        {
            // Debounce click to prevent double-toggling
            if ((DateTime.Now - _lastClickTime).TotalMilliseconds < 300)
                return;

            _lastClickTime = DateTime.Now;
            IsExpanded = !IsExpanded;
        }

        private void Header_MouseEnter(object? sender, EventArgs e)
        {
            _headerPanel.BackColor = Color.FromArgb(250, 250, 250);
            this.Cursor = Cursors.Hand;
        }

        private void Header_MouseLeave(object? sender, EventArgs e)
        {
            _headerPanel.BackColor = Color.White;
            this.Cursor = Cursors.Default;
        }

        private void UpdateExpandedState()
        {
            _contentPanel.Visible = _isExpanded;
            _separatorLine.Visible = _isExpanded;
            _expandIconLabel.Text = _isExpanded ? Helper_ButtonToggleAnimations.ArrowDown : Helper_ButtonToggleAnimations.ArrowRight;
            AdjustHeight();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (_expandIconLabel != null)
            {
                _expandIconLabel.Location = new Point(this.Width - 30, 20);
            }
        }

        #endregion
    }
}
