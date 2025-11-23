using System.ComponentModel;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Controls.Addons
{
    #region ConnectionStrengthControl

    public partial class Control_ConnectionStrengthControl : ThemedUserControl
    {
        #region Fields

        private int _strength;
        private int _ping = -1;
        private readonly ToolTip _toolTip = new();

        #endregion

        #region Constructors

        #region Properties

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int Strength
        {
            get => _strength;
            set
            {
                if (InvokeRequired)
                {
                    Invoke(new MethodInvoker(() =>
                    {
                        _strength = Math.Max(0, Math.Min(5, value));
                        UpdateToolTip();
                        Invalidate();
                    }));
                    return;
                }

                _strength = Math.Max(0, Math.Min(5, value));
                UpdateToolTip();
                Invalidate();
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int Ping
        {
            get => _ping;
            set
            {
                if (InvokeRequired)
                {
                    Invoke(new MethodInvoker(() =>
                    {
                        _ping = value;
                        UpdateToolTip();
                    }));
                    return;
                }

                _ping = value;
                UpdateToolTip();
            }
        }

        #endregion

        #region Initialization

        public Control_ConnectionStrengthControl()
        {
            InitializeComponent();
            Size = new Size(80, 14);
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint,
                true);
            Paint += ConnectionStrengthControl_Paint;
            UpdateToolTip();
            TabStop = false;

            MouseHover += ConnectionStrengthControl_MouseHover;

            // Force StatusStrip background color when control loads
            this.Load += (s, e) =>
            {
                var theme = Model_Application_Variables.UserUiColors;
                BackColor = theme.StatusStripBackColor
                    ?? theme.PanelBackColor
                    ?? theme.ControlBackColor
                    ?? SystemColors.Control;
                Invalidate();
            };
        }

        #endregion

        #region Methods

        protected override void ApplyTheme(Model_Shared_UserUiColors theme)
        {
            // Sync with parent's background color after theme is applied
            SyncBackgroundWithParent();
        }

        #endregion

        #region UI Events

        private void ConnectionStrengthControl_MouseHover(object? sender, EventArgs e) => UpdateToolTip();

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            SyncBackgroundWithParent();
        }

        #endregion

        #region Drawing

        private void ConnectionStrengthControl_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            int barCount = 5;
            int spacing = 4;
            int barWidth = 5;
            int barMaxHeight = 10;
            int barMinHeight = 4;
            int totalWidth = barCount * barWidth + (barCount - 1) * spacing;
            int rightPadding = 4;
            int startX = Width - totalWidth - rightPadding;
            int baseY = Height - 2;

            for (int i = 0; i < barCount; i++)
            {
                Color color = i < _strength
                    ? GetBarColor(i, barCount)
                    : Model_Application_Variables.UserUiColors.ControlBackColor ?? Color.LightGray;
                int height = barMinHeight + (barMaxHeight - barMinHeight) * i / (barCount - 1);
                int x = startX + i * (barWidth + spacing);
                int y = baseY - height;
                using (SolidBrush brush = new(color))
                {
                    g.FillRectangle(brush, x, y, barWidth, height);
                }

                using Pen pen = new(Model_Application_Variables.UserUiColors.ControlForeColor ?? Color.DarkGray);
                g.DrawRectangle(pen, x, y, barWidth, height);
            }
        }

        private static Color GetBarColor(int barIndex, int barCount)
        {
            Color lowColor = Model_Application_Variables.UserUiColors.ErrorColor ?? Color.Red;
            Color highColor = Model_Application_Variables.UserUiColors.SuccessColor ?? Color.LimeGreen;

            float t = barCount == 1 ? 1f : (float)barIndex / (barCount - 1);
            int r = (int)(lowColor.R * (1 - t) + highColor.R * t);
            int g = (int)(lowColor.G * (1 - t) + highColor.G * t);
            int b = (int)(lowColor.B * (1 - t) + highColor.B * t);
            return Color.FromArgb(r, g, b);
        }

        #endregion

        #region Helpers

        private void UpdateToolTip()
        {
            string quality = _strength switch
            {
                5 => "Excellent",
                4 => "Very Good",
                3 => "Good",
                2 => "Fair",
                1 => "Poor",
                _ => "No Signal"
            };
            string pingText = _ping >= 0 ? $"{_ping} ms" : "N/A";
            _toolTip.SetToolTip(this, $"Ping: {pingText} ({quality})");
        }

        private void SyncBackgroundWithParent()
        {
            // Simply match the parent container's background color (like it was in master branch)
            if (Parent != null)
            {
                BackColor = Parent.BackColor;
            }
            else
            {
                // Fallback to StatusStrip color if no parent yet
                BackColor = Model_Application_Variables.UserUiColors.StatusStripBackColor
                    ?? Model_Application_Variables.UserUiColors.ControlBackColor
                    ?? SystemColors.Control;
            }

            Invalidate();
        }

        #endregion

        #endregion
    }

    #endregion
}
