#nullable enable
using DocumentFormat.OpenXml.Vml.Spreadsheet;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Forms.Splash
{
    partial class SplashScreenForm
    {
        #region Fields

        private System.ComponentModel.IContainer components = null!;
        private Controls.Shared.Control_ProgressBarUserControl? _progressControl;
        private Label? _titleLabel;
        private Label? _versionLabel;
        private PictureBox? _logoBox;
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
            this.components = new System.ComponentModel.Container();
            this._mainLayout = new System.Windows.Forms.TableLayoutPanel();
            this._headerLayout = new System.Windows.Forms.TableLayoutPanel();
            this._logoBox = new System.Windows.Forms.PictureBox();
            this._titleLabel = new System.Windows.Forms.Label();
            this._versionLabel = new System.Windows.Forms.Label();
            this._progressControl = new Controls.Shared.Control_ProgressBarUserControl();
            
            this._mainLayout.SuspendLayout();
            this._headerLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._logoBox)).BeginInit();
            this.SuspendLayout();

            // 
            // Form
            // 
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ClientSize = new System.Drawing.Size(400, 250);
            this.Name = "SplashScreenForm";
            this.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.AutoScaleMode = AutoScaleMode.Dpi;

            // 
            // _mainLayout - Main container (2 rows: header, progress)
            // 
            this._mainLayout.ColumnCount = 1;
            this._mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._mainLayout.RowCount = 2;
            this._mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this._mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._mainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mainLayout.Name = "_mainLayout";

            // 
            // _headerLayout - Header area (2 columns: logo, text)
            // 
            this._headerLayout.ColumnCount = 2;
            this._headerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this._headerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this._headerLayout.RowCount = 4;            
            this._headerLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._headerLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this._headerLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this._headerLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._headerLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._headerLayout.Name = "_headerLayout";

            // 
            // _logoBox
            // 
            this._logoBox.Image = Properties.Resources.MTM;
            this._logoBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this._logoBox.Dock = System.Windows.Forms.DockStyle.Fill;            
            this._logoBox.Name = "_logoBox";
            this._logoBox.TabStop = false;

            // 
            // _titleLabel
            // 
            this._titleLabel.AutoSize = true;
            this._titleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._titleLabel.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this._titleLabel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this._titleLabel.ForeColor = System.Drawing.Color.White;
            this._titleLabel.Name = "_titleLabel";
            this._titleLabel.Text = "MTM WIP Application";
            this._titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // _versionLabel
            // 
            this._versionLabel.AutoSize = true;
            this._versionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._versionLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._versionLabel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this._versionLabel.ForeColor = System.Drawing.Color.LightGray;
            this._versionLabel.Name = "_versionLabel";
            this._versionLabel.Text = Model_Application_Variables.Version;
            this._versionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // _progressControl
            // 
            this._progressControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._progressControl.Name = "_progressControl";

            // 
            // Add controls to layouts
            // 
            this._headerLayout.Controls.Add(this._logoBox, 0, 0);
            this._headerLayout.SetRowSpan(this._logoBox, 4);
            this._headerLayout.Controls.Add(this._titleLabel, 1, 1);
            this._headerLayout.Controls.Add(this._versionLabel, 1, 2);

            this._mainLayout.Controls.Add(this._headerLayout, 0, 0);
            this._mainLayout.Controls.Add(this._progressControl, 0, 1);

            this.Controls.Add(this._mainLayout);

            this._mainLayout.ResumeLayout(false);
            this._headerLayout.ResumeLayout(false);
            this._headerLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._logoBox)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion
    }
}

        
