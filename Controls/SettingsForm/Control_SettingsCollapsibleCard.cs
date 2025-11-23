using System.ComponentModel;
using MTM_WIP_Application_Winforms.Forms.Shared;

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
        private readonly Panel _headerPanel;
        private readonly Label _titleLabel;
        private readonly Label _descriptionLabel;
        private readonly Label _iconLabel;
        private readonly Label _expandIconLabel;
        private readonly Panel _contentPanel;
        private readonly Panel _accentBar;

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
            // Initialize components programmatically to ensure correct layout
            this.DoubleBuffered = true;
            this.Padding = new Padding(1); // Border width
            this.BackColor = Color.FromArgb(200, 200, 200); // Border color

            // Accent Bar
            _accentBar = new Panel
            {
                Dock = DockStyle.Top,
                Height = 4,
                BackColor = _accentColor
            };

            // Header Panel
            _headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.White,
                Padding = new Padding(10)
            };
            _headerPanel.Click += Header_Click;
            _headerPanel.MouseEnter += Header_MouseEnter;
            _headerPanel.MouseLeave += Header_MouseLeave;

            // Icon Label
            _iconLabel = new Label
            {
                Text = "⚙️",
                Font = new Font("Segoe UI Emoji", 24F, FontStyle.Regular, GraphicsUnit.Point),
                AutoSize = true,
                Location = new Point(10, 8),
                BackColor = Color.Transparent
            };
            _iconLabel.Click += Header_Click;

            // Title Label
            _titleLabel = new Label
            {
                Text = "Card Title",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point),
                ForeColor = _accentColor,
                AutoSize = true,
                Location = new Point(60, 10),
                BackColor = Color.Transparent
            };
            _titleLabel.Click += Header_Click;

            // Description Label
            _descriptionLabel = new Label
            {
                Text = "Card description goes here...",
                Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point),
                ForeColor = Color.Gray,
                AutoSize = true,
                Location = new Point(60, 35),
                BackColor = Color.Transparent
            };
            _descriptionLabel.Click += Header_Click;

            // Expand Icon Label
            _expandIconLabel = new Label
            {
                Text = "▼",
                Font = new Font("Segoe UI Symbol", 10F, FontStyle.Regular, GraphicsUnit.Point),
                ForeColor = Color.Gray,
                AutoSize = true,
                Anchor = AnchorStyles.Right | AnchorStyles.Top,
                BackColor = Color.Transparent
            };
            _expandIconLabel.Location = new Point(this.Width - 30, 20);
            _expandIconLabel.Click += Header_Click;

            _headerPanel.Controls.Add(_iconLabel);
            _headerPanel.Controls.Add(_titleLabel);
            _headerPanel.Controls.Add(_descriptionLabel);
            _headerPanel.Controls.Add(_expandIconLabel);

            // Content Panel
            _contentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(10)
            };

            // Add controls to main container
            this.Controls.Add(_contentPanel);
            this.Controls.Add(_headerPanel);
            this.Controls.Add(_accentBar);

            // Initial state
            UpdateExpandedState();
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

        private void Header_Click(object? sender, EventArgs e)
        {
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
            _expandIconLabel.Text = _isExpanded ? "▼" : "▶";
            AdjustHeight();
        }

        private void AdjustHeight()
        {
            if (_isExpanded)
            {
                int contentHeight = 0;
                foreach (Control ctrl in _contentPanel.Controls)
                {
                    contentHeight += ctrl.Height + ctrl.Margin.Vertical;
                }
                // Header + Accent + Padding + Content
                this.Height = 60 + 4 + 20 + contentHeight; 
            }
            else
            {
                // Header + Accent + Border
                this.Height = 60 + 4 + 2;
            }
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
