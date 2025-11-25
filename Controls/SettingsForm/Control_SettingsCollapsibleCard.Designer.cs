namespace MTM_WIP_Application_Winforms.Controls.SettingsForm
{
    partial class Control_SettingsCollapsibleCard
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            _accentBar = new Panel();
            _headerPanel = new Panel();
            _tableLayoutHeaderContents = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            _expandIconLabel = new Label();
            _titleLabel = new Label();
            _iconLabel = new Label();
            _descriptionLabel = new Label();
            _separatorLine = new Panel();
            _contentPanel = new Panel();
            _headerPanel.SuspendLayout();
            _tableLayoutHeaderContents.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // _accentBar
            // 
            _accentBar.BackColor = Color.FromArgb(0, 120, 212);
            _accentBar.Dock = DockStyle.Top;
            _accentBar.Location = new Point(1, 1);
            _accentBar.Margin = new Padding(0);
            _accentBar.Name = "_accentBar";
            _accentBar.Size = new Size(442, 4);
            _accentBar.TabIndex = 0;
            // 
            // _headerPanel
            // 
            _headerPanel.BackColor = Color.White;
            _headerPanel.BorderStyle = BorderStyle.FixedSingle;
            _headerPanel.Controls.Add(_tableLayoutHeaderContents);
            _headerPanel.Dock = DockStyle.Top;
            _headerPanel.Location = new Point(1, 5);
            _headerPanel.Margin = new Padding(0);
            _headerPanel.Name = "_headerPanel";
            _headerPanel.Padding = new Padding(5);
            _headerPanel.Size = new Size(442, 60);
            _headerPanel.TabIndex = 1;
            _headerPanel.Click += Header_Click;
            _headerPanel.MouseEnter += Header_MouseEnter;
            _headerPanel.MouseLeave += Header_MouseLeave;
            // 
            // _tableLayoutHeaderContents
            // 
            _tableLayoutHeaderContents.AutoSize = true;
            _tableLayoutHeaderContents.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            _tableLayoutHeaderContents.ColumnCount = 4;
            _tableLayoutHeaderContents.ColumnStyles.Add(new ColumnStyle());
            _tableLayoutHeaderContents.ColumnStyles.Add(new ColumnStyle());
            _tableLayoutHeaderContents.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            _tableLayoutHeaderContents.ColumnStyles.Add(new ColumnStyle());
            _tableLayoutHeaderContents.Controls.Add(tableLayoutPanel2, 3, 0);
            _tableLayoutHeaderContents.Controls.Add(_titleLabel, 1, 0);
            _tableLayoutHeaderContents.Controls.Add(_iconLabel, 0, 0);
            _tableLayoutHeaderContents.Controls.Add(_descriptionLabel, 1, 1);
            _tableLayoutHeaderContents.Dock = DockStyle.Fill;
            _tableLayoutHeaderContents.Location = new Point(5, 5);
            _tableLayoutHeaderContents.Margin = new Padding(0);
            _tableLayoutHeaderContents.Name = "_tableLayoutHeaderContents";
            _tableLayoutHeaderContents.RowCount = 2;
            _tableLayoutHeaderContents.RowStyles.Add(new RowStyle());
            _tableLayoutHeaderContents.RowStyles.Add(new RowStyle());
            _tableLayoutHeaderContents.Size = new Size(430, 48);
            _tableLayoutHeaderContents.TabIndex = 4;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.AutoSize = true;
            tableLayoutPanel2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50.0000076F));
            tableLayoutPanel2.Controls.Add(_expandIconLabel, 1, 1);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(405, 0);
            tableLayoutPanel2.Margin = new Padding(0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 3;
            _tableLayoutHeaderContents.SetRowSpan(tableLayoutPanel2, 2);
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(25, 48);
            tableLayoutPanel2.TabIndex = 4;
            // 
            // _expandIconLabel
            // 
            _expandIconLabel.AutoSize = true;
            _expandIconLabel.BackColor = Color.Transparent;
            _expandIconLabel.BorderStyle = BorderStyle.FixedSingle;
            _expandIconLabel.Dock = DockStyle.Fill;
            _expandIconLabel.Font = new Font("Segoe UI Emoji", 10F);
            _expandIconLabel.Location = new Point(0, 11);
            _expandIconLabel.Margin = new Padding(0);
            _expandIconLabel.MaximumSize = new Size(50, 25);
            _expandIconLabel.MinimumSize = new Size(50, 25);
            _expandIconLabel.Name = "_expandIconLabel";
            _expandIconLabel.Size = new Size(50, 25);
            _expandIconLabel.TabIndex = 3;
            _expandIconLabel.Text = "🡱 📋";
            _expandIconLabel.TextAlign = ContentAlignment.MiddleCenter;
            _expandIconLabel.Click += Header_Click;
            // 
            // _titleLabel
            // 
            _titleLabel.AutoSize = true;
            _titleLabel.BackColor = Color.Transparent;
            _titleLabel.Dock = DockStyle.Fill;
            _titleLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            _titleLabel.Location = new Point(63, 0);
            _titleLabel.Margin = new Padding(0);
            _titleLabel.Name = "_titleLabel";
            _titleLabel.Size = new Size(157, 21);
            _titleLabel.TabIndex = 1;
            _titleLabel.Text = "Card Title";
            _titleLabel.TextAlign = ContentAlignment.MiddleLeft;
            _titleLabel.Click += Header_Click;
            // 
            // _iconLabel
            // 
            _iconLabel.AutoSize = true;
            _iconLabel.BackColor = Color.Transparent;
            _iconLabel.Dock = DockStyle.Fill;
            _iconLabel.Font = new Font("Segoe UI Emoji", 24F);
            _iconLabel.Location = new Point(0, 0);
            _iconLabel.Margin = new Padding(0);
            _iconLabel.Name = "_iconLabel";
            _tableLayoutHeaderContents.SetRowSpan(_iconLabel, 2);
            _iconLabel.Size = new Size(63, 48);
            _iconLabel.TabIndex = 0;
            _iconLabel.Text = "⚙️";
            _iconLabel.TextAlign = ContentAlignment.MiddleCenter;
            _iconLabel.Click += Header_Click;
            // 
            // _descriptionLabel
            // 
            _descriptionLabel.AutoSize = true;
            _descriptionLabel.BackColor = Color.Transparent;
            _descriptionLabel.Dock = DockStyle.Fill;
            _descriptionLabel.Font = new Font("Segoe UI", 9F);
            _descriptionLabel.Location = new Point(63, 21);
            _descriptionLabel.Margin = new Padding(0);
            _descriptionLabel.Name = "_descriptionLabel";
            _descriptionLabel.Size = new Size(157, 27);
            _descriptionLabel.TabIndex = 2;
            _descriptionLabel.Text = "Card description goes here...";
            _descriptionLabel.TextAlign = ContentAlignment.MiddleLeft;
            _descriptionLabel.Click += Header_Click;
            // 
            // _separatorLine
            // 
            _separatorLine.BackColor = Color.FromArgb(200, 200, 200);
            _separatorLine.Dock = DockStyle.Top;
            _separatorLine.Location = new Point(1, 65);
            _separatorLine.Margin = new Padding(0);
            _separatorLine.Name = "_separatorLine";
            _separatorLine.Size = new Size(442, 1);
            _separatorLine.TabIndex = 2;
            // 
            // _contentPanel
            // 
            _contentPanel.BackColor = Color.White;
            _contentPanel.BorderStyle = BorderStyle.FixedSingle;
            _contentPanel.Dock = DockStyle.Fill;
            _contentPanel.Location = new Point(1, 66);
            _contentPanel.Margin = new Padding(0);
            _contentPanel.Name = "_contentPanel";
            _contentPanel.Padding = new Padding(10);
            _contentPanel.Size = new Size(442, 203);
            _contentPanel.TabIndex = 3;
            // 
            // Control_SettingsCollapsibleCard
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(200, 200, 200);
            Controls.Add(_contentPanel);
            Controls.Add(_separatorLine);
            Controls.Add(_headerPanel);
            Controls.Add(_accentBar);
            Name = "Control_SettingsCollapsibleCard";
            Padding = new Padding(1);
            Size = new Size(444, 270);
            _headerPanel.ResumeLayout(false);
            _headerPanel.PerformLayout();
            _tableLayoutHeaderContents.ResumeLayout(false);
            _tableLayoutHeaderContents.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel _headerPanel;
        private System.Windows.Forms.Panel _separatorLine;
        private System.Windows.Forms.Label _titleLabel;
        private System.Windows.Forms.Label _descriptionLabel;
        private System.Windows.Forms.Label _iconLabel;
        private System.Windows.Forms.Label _expandIconLabel;
        private System.Windows.Forms.Panel _contentPanel;
        private System.Windows.Forms.Panel _accentBar;
        private TableLayoutPanel _tableLayoutHeaderContents;
        private TableLayoutPanel tableLayoutPanel2;
    }
}
