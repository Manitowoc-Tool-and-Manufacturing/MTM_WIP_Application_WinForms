namespace MTM_WIP_Application_Winforms.Controls.MainForm
{
    partial class Control_QuickButton_Single
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ToolTip toolTip = null!;

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
            components = new System.ComponentModel.Container();
            _tableLayout = new TableLayoutPanel();
            _borderPanel = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            _lblHotkey = new Label();
            _lblPartId = new Label();
            tableLayoutPanel2 = new TableLayoutPanel();
            _lblOperation = new Label();
            _lblQuantity = new Label();
            toolTip = new ToolTip(components);
            _tableLayout.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // _tableLayout
            // 
            _tableLayout.AutoSize = true;
            _tableLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            _tableLayout.BackColor = Color.Transparent;
            _tableLayout.ColumnCount = 3;
            _tableLayout.ColumnStyles.Add(new ColumnStyle());
            _tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            _tableLayout.ColumnStyles.Add(new ColumnStyle());
            _tableLayout.Controls.Add(_borderPanel, 0, 0);
            _tableLayout.Controls.Add(tableLayoutPanel1, 0, 1);
            _tableLayout.Controls.Add(tableLayoutPanel2, 1, 2);
            _tableLayout.Dock = DockStyle.Fill;
            _tableLayout.Location = new Point(0, 0);
            _tableLayout.Margin = new Padding(0);
            _tableLayout.Name = "_tableLayout";
            _tableLayout.RowCount = 3;
            _tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 3F));
            _tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            _tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            _tableLayout.Size = new Size(200, 50);
            _tableLayout.TabIndex = 0;
            // 
            // _borderPanel
            // 
            _borderPanel.BackColor = Color.SteelBlue;
            _tableLayout.SetColumnSpan(_borderPanel, 3);
            _borderPanel.Dock = DockStyle.Fill;
            _borderPanel.Enabled = false;
            _borderPanel.Location = new Point(0, 0);
            _borderPanel.Margin = new Padding(0);
            _borderPanel.Name = "_borderPanel";
            _borderPanel.Size = new Size(200, 3);
            _borderPanel.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.ColumnCount = 2;
            _tableLayout.SetColumnSpan(tableLayoutPanel1, 3);
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(_lblHotkey, 0, 0);
            tableLayoutPanel1.Controls.Add(_lblPartId, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 3);
            tableLayoutPanel1.Margin = new Padding(0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(200, 23);
            tableLayoutPanel1.TabIndex = 5;
            // 
            // _lblHotkey
            // 
            _lblHotkey.BackColor = Color.Transparent;
            _lblHotkey.Cursor = Cursors.Hand;
            _lblHotkey.Dock = DockStyle.Fill;
            _lblHotkey.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
            _lblHotkey.ForeColor = Color.SteelBlue;
            _lblHotkey.Location = new Point(0, 0);
            _lblHotkey.Margin = new Padding(0);
            _lblHotkey.Name = "_lblHotkey";
            _lblHotkey.Size = new Size(50, 23);
            _lblHotkey.TabIndex = 1;
            _lblHotkey.Text = "F1";
            _lblHotkey.TextAlign = ContentAlignment.MiddleCenter;
            _lblHotkey.Click += Label_Click;
            _lblHotkey.MouseDown += Label_MouseDown;
            _lblHotkey.MouseEnter += Label_MouseEnter;
            _lblHotkey.MouseLeave += Label_MouseLeave;
            _lblHotkey.MouseUp += Label_MouseUp;
            // 
            // _lblPartId
            // 
            _lblPartId.AutoEllipsis = true;
            _lblPartId.BackColor = Color.Transparent;
            _lblPartId.Cursor = Cursors.Hand;
            _lblPartId.Dock = DockStyle.Fill;
            _lblPartId.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            _lblPartId.Location = new Point(50, 0);
            _lblPartId.Margin = new Padding(0);
            _lblPartId.Name = "_lblPartId";
            _lblPartId.Size = new Size(150, 23);
            _lblPartId.TabIndex = 2;
            _lblPartId.Text = "Part-Number-Placeholder";
            _lblPartId.TextAlign = ContentAlignment.MiddleRight;
            _lblPartId.Click += Label_Click;
            _lblPartId.MouseDown += Label_MouseDown;
            _lblPartId.MouseEnter += Label_MouseEnter;
            _lblPartId.MouseLeave += Label_MouseLeave;
            _lblPartId.MouseUp += Label_MouseUp;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.AutoSize = true;
            tableLayoutPanel2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(_lblOperation, 0, 0);
            tableLayoutPanel2.Controls.Add(_lblQuantity, 1, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 29);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(194, 18);
            tableLayoutPanel2.TabIndex = 6;
            // 
            // _lblOperation
            // 
            _lblOperation.BackColor = Color.Transparent;
            _lblOperation.Cursor = Cursors.Hand;
            _lblOperation.Dock = DockStyle.Fill;
            _lblOperation.Font = new Font("Segoe UI", 8F);
            _lblOperation.Location = new Point(0, 0);
            _lblOperation.Margin = new Padding(0);
            _lblOperation.Name = "_lblOperation";
            _lblOperation.Size = new Size(97, 18);
            _lblOperation.TabIndex = 3;
            _lblOperation.Text = "Op: 260";
            _lblOperation.TextAlign = ContentAlignment.MiddleLeft;
            _lblOperation.Click += Label_Click;
            _lblOperation.MouseDown += Label_MouseDown;
            _lblOperation.MouseEnter += Label_MouseEnter;
            _lblOperation.MouseLeave += Label_MouseLeave;
            _lblOperation.MouseUp += Label_MouseUp;
            // 
            // _lblQuantity
            // 
            _lblQuantity.BackColor = Color.Transparent;
            _lblQuantity.Cursor = Cursors.Hand;
            _lblQuantity.Dock = DockStyle.Fill;
            _lblQuantity.Font = new Font("Segoe UI", 8F);
            _lblQuantity.Location = new Point(97, 0);
            _lblQuantity.Margin = new Padding(0);
            _lblQuantity.Name = "_lblQuantity";
            _lblQuantity.Size = new Size(97, 18);
            _lblQuantity.TabIndex = 4;
            _lblQuantity.Text = "Qty: 25000";
            _lblQuantity.TextAlign = ContentAlignment.MiddleRight;
            _lblQuantity.Click += Label_Click;
            _lblQuantity.MouseDown += Label_MouseDown;
            _lblQuantity.MouseEnter += Label_MouseEnter;
            _lblQuantity.MouseLeave += Label_MouseLeave;
            _lblQuantity.MouseUp += Label_MouseUp;
            // 
            // Control_QuickButton_Single
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = SystemColors.Control;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(_tableLayout);
            Cursor = Cursors.Hand;
            MinimumSize = new Size(150, 40);
            Name = "Control_QuickButton_Single";
            Size = new Size(200, 50);
            _tableLayout.ResumeLayout(false);
            _tableLayout.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel _tableLayout;
        private System.Windows.Forms.Panel _borderPanel;
        private System.Windows.Forms.Label _lblHotkey;
        private System.Windows.Forms.Label _lblPartId;
        private System.Windows.Forms.Label _lblOperation;
        private System.Windows.Forms.Label _lblQuantity;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
    }
}
