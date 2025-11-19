using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MTM_WIP_Application_Winforms.Forms.Shared;

namespace MTM_WIP_Application_Winforms.Controls.SettingsForm
{
    /// <summary>
    /// Reusable category card with subcategory links.
    /// Fully editable in Visual Studio designer.
    /// </summary>
    public partial class Control_SettingsCategoryCard : ThemedUserControl
    {
        #region Fields

        private Color _accentColor = Color.FromArgb(0, 120, 212);
        private readonly List<SubcategoryLink> _subcategoryLinks = new();

        #endregion

        #region Events

        /// <summary>
        /// Fired when card or subcategory link is clicked.
        /// </summary>
        public event EventHandler<string>? NavigationRequested;

        #endregion

        #region Properties

        /// <summary>
        /// Card title text.
        /// </summary>
        [Category("Appearance")]
        [Description("The title text displayed on the card")]
        public string CardTitle
        {
            get => Control_SettingsCategoryCard_Label_Title.Text;
            set => Control_SettingsCategoryCard_Label_Title.Text = value;
        }

        /// <summary>
        /// Card description text.
        /// </summary>
        [Category("Appearance")]
        [Description("The description text displayed on the card")]
        public string CardDescription
        {
            get => Control_SettingsCategoryCard_Label_Description.Text;
            set => Control_SettingsCategoryCard_Label_Description.Text = value;
        }

        /// <summary>
        /// Icon text (emoji or symbol).
        /// </summary>
        [Category("Appearance")]
        [Description("The icon displayed on the card (emoji or symbol)")]
        public string CardIcon
        {
            get => Control_SettingsCategoryCard_Label_Icon.Text;
            set => Control_SettingsCategoryCard_Label_Icon.Text = value;
        }

        /// <summary>
        /// Accent color for the top bar.
        /// </summary>
        [Category("Appearance")]
        [Description("The accent color for the top bar and links")]
        public Color AccentColor
        {
            get => _accentColor;
            set
            {
                _accentColor = value;
                Control_SettingsCategoryCard_Panel_AccentBar.BackColor = value;
                UpdateLinkColors();
            }
        }

        #endregion

        #region Constructors

        public Control_SettingsCategoryCard()
        {
            InitializeComponent();
            SetupEventHandlers();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a subcategory link to the card.
        /// </summary>
        public void AddSubcategoryLink(string title, string navigationTarget)
        {
            var link = new LinkLabel
            {
                Text = $"→ {title}",
                Font = new Font("Segoe UI", 9F),
                AutoSize = true,
                Margin = new Padding(0, 2, 0, 2),
                LinkColor = _accentColor,
                ActiveLinkColor = Color.FromArgb(
                    Math.Max(0, _accentColor.R - 30),
                    Math.Max(0, _accentColor.G - 30),
                    Math.Max(0, _accentColor.B - 30)),
                Tag = navigationTarget
            };
            link.LinkClicked += SubcategoryLink_Clicked;

            _subcategoryLinks.Add(new SubcategoryLink { LinkLabel = link, NavigationTarget = navigationTarget });
            Control_SettingsCategoryCard_FlowPanel_Subcategories.Controls.Add(link);

            // Adjust card height to accommodate links
            this.Height = 180;
        }

        /// <summary>
        /// Clears all subcategory links.
        /// </summary>
        public void ClearSubcategoryLinks()
        {
            foreach (var link in _subcategoryLinks)
            {
                link.LinkLabel.LinkClicked -= SubcategoryLink_Clicked;
            }
            _subcategoryLinks.Clear();
            Control_SettingsCategoryCard_FlowPanel_Subcategories.Controls.Clear();
            this.Height = 140;
        }

        private void SetupEventHandlers()
        {
            this.MouseEnter += OnMouseEnterCard;
            this.MouseLeave += OnMouseLeaveCard;
            Control_SettingsCategoryCard_Panel_Content.MouseEnter += OnMouseEnterCard;
            Control_SettingsCategoryCard_Panel_Content.MouseLeave += OnMouseLeaveCard;
        }

        private void SubcategoryLink_Clicked(object? sender, LinkLabelLinkClickedEventArgs e)
        {
            if (sender is LinkLabel link && link.Tag is string target)
            {
                NavigationRequested?.Invoke(this, target);
            }
        }

        private void OnMouseEnterCard(object? sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(250, 250, 250);
        }

        private void OnMouseLeaveCard(object? sender, EventArgs e)
        {
            this.BackColor = Color.White;
        }

        private void UpdateLinkColors()
        {
            foreach (var linkInfo in _subcategoryLinks)
            {
                linkInfo.LinkLabel.LinkColor = _accentColor;
                linkInfo.LinkLabel.ActiveLinkColor = Color.FromArgb(
                    Math.Max(0, _accentColor.R - 30),
                    Math.Max(0, _accentColor.G - 30),
                    Math.Max(0, _accentColor.B - 30));
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // Draw border
            using var pen = new Pen(Color.FromArgb(200, 200, 200), 1);
            e.Graphics.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);
        }

        #endregion

        #region Nested Types

        private class SubcategoryLink
        {
            public LinkLabel LinkLabel { get; set; } = null!;
            public string NavigationTarget { get; set; } = string.Empty;
        }

        #endregion
    }
}
