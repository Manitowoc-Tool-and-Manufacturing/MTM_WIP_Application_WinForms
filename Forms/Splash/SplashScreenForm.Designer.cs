#nullable enable
using DocumentFormat.OpenXml.Vml.Spreadsheet;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Forms.Splash
{
    partial class SplashScreenForm
    {
        #region Fields

        private System.ComponentModel.IContainer components = null!;
        private Label? _titleLabel;
        private Label? _versionLabel;
        private TableLayoutPanel? _mainLayout;
        private TableLayoutPanel? _headerLayout;

        #endregion

        #region Methods

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            _mainLayout = new TableLayoutPanel();
            _headerLayout = new TableLayoutPanel();
            _logoBox = new PictureBox();
            _titleLabel = new Label();
            _versionLabel = new Label();
            _progressControl = new MTM_WIP_Application_Winforms.Controls.Shared.Control_ProgressBarUserControl();
            _mainPanel = new Panel();
            _mainLayout.SuspendLayout();
            _headerLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_logoBox).BeginInit();
            _mainPanel.SuspendLayout();
            SuspendLayout();
            // 
            // _mainLayout
            // 
            _mainLayout.AutoSize = true;
            _mainLayout.ColumnCount = 1;
            _mainLayout.ColumnStyles.Add(new ColumnStyle());
            _mainLayout.Controls.Add(_headerLayout, 0, 0);
            _mainLayout.Controls.Add(_progressControl, 0, 1);
            _mainLayout.Dock = DockStyle.Fill;
            _mainLayout.Location = new Point(0, 0);
            _mainLayout.Margin = new Padding(4);
            _mainLayout.Name = "_mainLayout";
            _mainLayout.RowCount = 3;
            _mainLayout.RowStyles.Add(new RowStyle());
            _mainLayout.RowStyles.Add(new RowStyle());
            _mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            _mainLayout.Size = new Size(862, 379);
            _mainLayout.TabIndex = 0;
            // 
            // _headerLayout
            // 
            _headerLayout.AutoSize = true;
            _headerLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            _headerLayout.ColumnCount = 2;
            _headerLayout.ColumnStyles.Add(new ColumnStyle());
            _headerLayout.ColumnStyles.Add(new ColumnStyle());
            _headerLayout.Controls.Add(_logoBox, 0, 0);
            _headerLayout.Controls.Add(_titleLabel, 1, 1);
            _headerLayout.Controls.Add(_versionLabel, 1, 3);
            _headerLayout.Dock = DockStyle.Fill;
            _headerLayout.Location = new Point(4, 4);
            _headerLayout.Margin = new Padding(4);
            _headerLayout.Name = "_headerLayout";
            _headerLayout.Padding = new Padding(3);
            _headerLayout.RowCount = 5;
            _headerLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            _headerLayout.RowStyles.Add(new RowStyle());
            _headerLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            _headerLayout.RowStyles.Add(new RowStyle());
            _headerLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            _headerLayout.Size = new Size(854, 187);
            _headerLayout.TabIndex = 0;
            // 
            // _logoBox
            // 
            _logoBox.Dock = DockStyle.Fill;
            _logoBox.Image = Properties.Resources.MTM;
            _logoBox.Location = new Point(7, 7);
            _logoBox.Margin = new Padding(4);
            _logoBox.Name = "_logoBox";
            _headerLayout.SetRowSpan(_logoBox, 5);
            _logoBox.Size = new Size(268, 173);
            _logoBox.SizeMode = PictureBoxSizeMode.Zoom;
            _logoBox.TabIndex = 0;
            _logoBox.TabStop = false;
            // 
            // _titleLabel
            // 
            _titleLabel.AutoSize = true;
            _titleLabel.Dock = DockStyle.Fill;
            _titleLabel.Font = new Font("PMingLiU-ExtB", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            _titleLabel.ForeColor = Color.White;
            _titleLabel.Location = new Point(283, 25);
            _titleLabel.Margin = new Padding(4);
            _titleLabel.Name = "_titleLabel";
            _titleLabel.Size = new Size(564, 72);
            _titleLabel.TabIndex = 1;
            _titleLabel.Text = "Manitowoc Tool and Manufacturing\r\nWork in Progress Application";
            _titleLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // _versionLabel
            // 
            _versionLabel.AutoSize = true;
            _versionLabel.Dock = DockStyle.Fill;
            _versionLabel.Font = new Font("PMingLiU-ExtB", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            _versionLabel.ForeColor = Color.LightGray;
            _versionLabel.Location = new Point(283, 123);
            _versionLabel.Margin = new Padding(4);
            _versionLabel.Name = "_versionLabel";
            _versionLabel.Size = new Size(564, 36);
            _versionLabel.TabIndex = 2;
            _versionLabel.Text = "18.3.0.0";
            _versionLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // _progressControl
            // 
            _progressControl.AutoSize = true;
            _progressControl.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            _progressControl.BorderStyle = BorderStyle.FixedSingle;
            _progressControl.Dock = DockStyle.Fill;
            _progressControl.Location = new Point(9, 204);
            _progressControl.Margin = new Padding(9);
            _progressControl.Name = "_progressControl";
            _progressControl.Padding = new Padding(3);
            _progressControl.Size = new Size(844, 123);
            _progressControl.TabIndex = 1;
            // 
            // _mainPanel
            // 
            _mainPanel.AutoSize = true;
            _mainPanel.Controls.Add(_mainLayout);
            _mainPanel.Dock = DockStyle.Fill;
            _mainPanel.Location = new Point(0, 0);
            _mainPanel.Margin = new Padding(4);
            _mainPanel.Name = "_mainPanel";
            _mainPanel.Size = new Size(862, 379);
            _mainPanel.TabIndex = 1;
            // 
            // SplashScreenForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            BackColor = Color.FromArgb(30, 30, 30);
            ClientSize = new Size(862, 379);
            Controls.Add(_mainPanel);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4);
            Name = "SplashScreenForm";
            StartPosition = FormStartPosition.CenterScreen;
            _mainLayout.ResumeLayout(false);
            _mainLayout.PerformLayout();
            _headerLayout.ResumeLayout(false);
            _headerLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)_logoBox).EndInit();
            _mainPanel.ResumeLayout(false);
            _mainPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Controls.Shared.Control_ProgressBarUserControl _progressControl;
        private PictureBox _logoBox;
        private Panel _mainPanel;
    }
}

        
