using System.ComponentModel;
using MTM_WIP_Application_Winforms.Forms.Shared;

namespace MTM_WIP_Application_Winforms.Controls.SettingsForm
{
    /// <summary>
    /// Reusable category tile for settings homepage.
    /// Fully editable in Visual Studio designer.
    /// </summary>
    public partial class Control_SettingsCategoryTile : ThemedUserControl
    {
        #region Fields

        private Color _accentColor = Color.FromArgb(0, 120, 212);
        private string? _navigationTarget;

        #endregion

        #region Events

        /// <summary>
        /// Fired when tile is clicked.
        /// </summary>
        public event EventHandler<string>? TileClicked;

        #endregion

        #region Properties

        /// <summary>
        /// Tile title text.
        /// </summary>
        [Category("Appearance")]
        [Description("The title text displayed on the tile")]
        public string TileTitle
        {
            get => Control_SettingsCategoryTile_Label_Title.Text;
            set => Control_SettingsCategoryTile_Label_Title.Text = value;
        }

        /// <summary>
        /// Tile description text.
        /// </summary>
        [Category("Appearance")]
        [Description("The description text displayed on the tile")]
        public string TileDescription
        {
            get => Control_SettingsCategoryTile_Label_Description.Text;
            set => Control_SettingsCategoryTile_Label_Description.Text = value;
        }

        /// <summary>
        /// Icon text (emoji or symbol).
        /// </summary>
        [Category("Appearance")]
        [Description("The icon displayed on the tile (emoji or symbol)")]
        public string TileIcon
        {
            get => Control_SettingsCategoryTile_Label_Icon.Text;
            set => Control_SettingsCategoryTile_Label_Icon.Text = value;
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
                Control_SettingsCategoryTile_Panel_AccentBar.BackColor = value;
            }
        }

        /// <summary>
        /// Navigation target when clicked.
        /// </summary>
        [Category("Behavior")]
        [Description("The navigation target identifier")]
        public string? NavigationTarget
        {
            get => _navigationTarget;
            set => _navigationTarget = value;
        }

        #endregion

        #region Constructors

        public Control_SettingsCategoryTile()
        {
            InitializeComponent();
            SetupEventHandlers();
        }

        #endregion

        #region Methods

        private void SetupEventHandlers()
        {
            this.Click += OnTileClick;
            Control_SettingsCategoryTile_Panel_Content.Click += OnTileClick;
            Control_SettingsCategoryTile_Label_Icon.Click += OnTileClick;
            Control_SettingsCategoryTile_Label_Title.Click += OnTileClick;
            Control_SettingsCategoryTile_Label_Description.Click += OnTileClick;

            this.MouseEnter += OnMouseEnterTile;
            this.MouseLeave += OnMouseLeaveTile;
            Control_SettingsCategoryTile_Panel_Content.MouseEnter += OnMouseEnterTile;
            Control_SettingsCategoryTile_Panel_Content.MouseLeave += OnMouseLeaveTile;
        }

        private void OnTileClick(object? sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(_navigationTarget))
            {
                TileClicked?.Invoke(this, _navigationTarget);
            }
        }

        private void OnMouseEnterTile(object? sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(250, 250, 250);
        }

        private void OnMouseLeaveTile(object? sender, EventArgs e)
        {
            this.BackColor = Color.White;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // Draw border
            using var pen = new Pen(Color.FromArgb(200, 200, 200), 1);
            e.Graphics.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);
        }

        #endregion
    }
}
