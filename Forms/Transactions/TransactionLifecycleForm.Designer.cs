namespace MTM_WIP_Application_Winforms.Forms.Transactions
{
    partial class TransactionLifecycleForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            TransactionLifecycleForm_TableLayout_Main = new TableLayoutPanel();
            TransactionLifecycleForm_Panel_TreeView = new Panel();
            TransactionLifecycleForm_TreeView_Lifecycle = new TreeView();
            TransactionLifecycleForm_ImageList_Icons = new ImageList(components);
            TransactionLifecycleForm_Panel_DetailView = new Panel();
            TransactionLifecycleForm_DetailPanel = new Controls.Transactions.TransactionDetailPanel();
            TransactionLifecycleForm_Panel_Buttons = new Panel();
            TransactionLifecycleForm_TableLayout_Buttons = new TableLayoutPanel();
            TransactionLifecycleForm_Button_Export = new Button();
            TransactionLifecycleForm_Button_Print = new Button();
            TransactionLifecycleForm_Button_Close = new Button();
            TransactionLifecycleForm_StatusStrip = new StatusStrip();
            TransactionLifecycleForm_ToolStripStatusLabel_Legend = new ToolStripStatusLabel();
            TransactionLifecycleForm_TableLayout_Main.SuspendLayout();
            TransactionLifecycleForm_Panel_TreeView.SuspendLayout();
            TransactionLifecycleForm_Panel_DetailView.SuspendLayout();
            TransactionLifecycleForm_Panel_Buttons.SuspendLayout();
            TransactionLifecycleForm_TableLayout_Buttons.SuspendLayout();
            TransactionLifecycleForm_StatusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // TransactionLifecycleForm_TableLayout_Main
            // 
            TransactionLifecycleForm_TableLayout_Main.AutoSize = true;
            TransactionLifecycleForm_TableLayout_Main.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionLifecycleForm_TableLayout_Main.ColumnCount = 2;
            TransactionLifecycleForm_TableLayout_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            TransactionLifecycleForm_TableLayout_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            TransactionLifecycleForm_TableLayout_Main.Controls.Add(TransactionLifecycleForm_Panel_TreeView, 0, 0);
            TransactionLifecycleForm_TableLayout_Main.Controls.Add(TransactionLifecycleForm_Panel_DetailView, 1, 0);
            TransactionLifecycleForm_TableLayout_Main.Controls.Add(TransactionLifecycleForm_Panel_Buttons, 1, 1);
            TransactionLifecycleForm_TableLayout_Main.Dock = DockStyle.Fill;
            TransactionLifecycleForm_TableLayout_Main.Location = new Point(0, 0);
            TransactionLifecycleForm_TableLayout_Main.Name = "TransactionLifecycleForm_TableLayout_Main";
            TransactionLifecycleForm_TableLayout_Main.Padding = new Padding(10);
            TransactionLifecycleForm_TableLayout_Main.RowCount = 2;
            TransactionLifecycleForm_TableLayout_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            TransactionLifecycleForm_TableLayout_Main.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            TransactionLifecycleForm_TableLayout_Main.Size = new Size(800, 472);
            TransactionLifecycleForm_TableLayout_Main.TabIndex = 0;
            // 
            // TransactionLifecycleForm_Panel_TreeView
            // 
            TransactionLifecycleForm_Panel_TreeView.BorderStyle = BorderStyle.FixedSingle;
            TransactionLifecycleForm_Panel_TreeView.Controls.Add(TransactionLifecycleForm_TreeView_Lifecycle);
            TransactionLifecycleForm_Panel_TreeView.Dock = DockStyle.Fill;
            TransactionLifecycleForm_Panel_TreeView.Location = new Point(13, 13);
            TransactionLifecycleForm_Panel_TreeView.Name = "TransactionLifecycleForm_Panel_TreeView";
            TransactionLifecycleForm_TableLayout_Main.SetRowSpan(TransactionLifecycleForm_Panel_TreeView, 2);
            TransactionLifecycleForm_Panel_TreeView.Size = new Size(306, 446);
            TransactionLifecycleForm_Panel_TreeView.TabIndex = 0;
            // 
            // TransactionLifecycleForm_TreeView_Lifecycle
            // 
            TransactionLifecycleForm_TreeView_Lifecycle.Dock = DockStyle.Fill;
            TransactionLifecycleForm_TreeView_Lifecycle.Font = new Font("Segoe UI", 9F);
            TransactionLifecycleForm_TreeView_Lifecycle.HideSelection = false;
            TransactionLifecycleForm_TreeView_Lifecycle.ImageIndex = 0;
            TransactionLifecycleForm_TreeView_Lifecycle.ImageList = TransactionLifecycleForm_ImageList_Icons;
            TransactionLifecycleForm_TreeView_Lifecycle.Location = new Point(0, 0);
            TransactionLifecycleForm_TreeView_Lifecycle.Name = "TransactionLifecycleForm_TreeView_Lifecycle";
            TransactionLifecycleForm_TreeView_Lifecycle.SelectedImageIndex = 0;
            TransactionLifecycleForm_TreeView_Lifecycle.Size = new Size(304, 444);
            TransactionLifecycleForm_TreeView_Lifecycle.TabIndex = 0;
            // 
            // TransactionLifecycleForm_ImageList_Icons
            // 
            TransactionLifecycleForm_ImageList_Icons.ColorDepth = ColorDepth.Depth32Bit;
            TransactionLifecycleForm_ImageList_Icons.ImageSize = new Size(16, 16);
            // 
            // TransactionLifecycleForm_Panel_DetailView
            // 
            TransactionLifecycleForm_Panel_DetailView.BorderStyle = BorderStyle.FixedSingle;
            TransactionLifecycleForm_Panel_DetailView.Controls.Add(TransactionLifecycleForm_DetailPanel);
            TransactionLifecycleForm_Panel_DetailView.Dock = DockStyle.Fill;
            TransactionLifecycleForm_Panel_DetailView.Location = new Point(325, 13);
            TransactionLifecycleForm_Panel_DetailView.Name = "TransactionLifecycleForm_Panel_DetailView";
            TransactionLifecycleForm_Panel_DetailView.Size = new Size(462, 396);
            TransactionLifecycleForm_Panel_DetailView.TabIndex = 1;
            // 
            // TransactionLifecycleForm_DetailPanel
            // 
            TransactionLifecycleForm_DetailPanel.Dock = DockStyle.Fill;
            TransactionLifecycleForm_DetailPanel.Location = new Point(0, 0);
            TransactionLifecycleForm_DetailPanel.Name = "TransactionLifecycleForm_DetailPanel";
            TransactionLifecycleForm_DetailPanel.Size = new Size(460, 394);
            TransactionLifecycleForm_DetailPanel.TabIndex = 0;
            // 
            // TransactionLifecycleForm_Panel_Buttons
            // 
            TransactionLifecycleForm_Panel_Buttons.Controls.Add(TransactionLifecycleForm_TableLayout_Buttons);
            TransactionLifecycleForm_Panel_Buttons.Dock = DockStyle.Fill;
            TransactionLifecycleForm_Panel_Buttons.Location = new Point(325, 415);
            TransactionLifecycleForm_Panel_Buttons.Name = "TransactionLifecycleForm_Panel_Buttons";
            TransactionLifecycleForm_Panel_Buttons.Size = new Size(462, 44);
            TransactionLifecycleForm_Panel_Buttons.TabIndex = 2;
            // 
            // TransactionLifecycleForm_TableLayout_Buttons
            // 
            TransactionLifecycleForm_TableLayout_Buttons.AutoSize = true;
            TransactionLifecycleForm_TableLayout_Buttons.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionLifecycleForm_TableLayout_Buttons.ColumnCount = 5;
            TransactionLifecycleForm_TableLayout_Buttons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            TransactionLifecycleForm_TableLayout_Buttons.ColumnStyles.Add(new ColumnStyle());
            TransactionLifecycleForm_TableLayout_Buttons.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 10F));
            TransactionLifecycleForm_TableLayout_Buttons.ColumnStyles.Add(new ColumnStyle());
            TransactionLifecycleForm_TableLayout_Buttons.ColumnStyles.Add(new ColumnStyle());
            TransactionLifecycleForm_TableLayout_Buttons.Controls.Add(TransactionLifecycleForm_Button_Export, 1, 0);
            TransactionLifecycleForm_TableLayout_Buttons.Controls.Add(TransactionLifecycleForm_Button_Print, 3, 0);
            TransactionLifecycleForm_TableLayout_Buttons.Controls.Add(TransactionLifecycleForm_Button_Close, 4, 0);
            TransactionLifecycleForm_TableLayout_Buttons.Dock = DockStyle.Fill;
            TransactionLifecycleForm_TableLayout_Buttons.Location = new Point(0, 0);
            TransactionLifecycleForm_TableLayout_Buttons.Name = "TransactionLifecycleForm_TableLayout_Buttons";
            TransactionLifecycleForm_TableLayout_Buttons.RowCount = 1;
            TransactionLifecycleForm_TableLayout_Buttons.RowStyles.Add(new RowStyle());
            TransactionLifecycleForm_TableLayout_Buttons.Size = new Size(462, 44);
            TransactionLifecycleForm_TableLayout_Buttons.TabIndex = 0;
            // 
            // TransactionLifecycleForm_Button_Export
            // 
            TransactionLifecycleForm_Button_Export.AutoSize = true;
            TransactionLifecycleForm_Button_Export.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionLifecycleForm_Button_Export.BackColor = Color.FromArgb(34, 197, 94);
            TransactionLifecycleForm_Button_Export.FlatStyle = FlatStyle.Flat;
            TransactionLifecycleForm_Button_Export.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            TransactionLifecycleForm_Button_Export.ForeColor = Color.White;
            TransactionLifecycleForm_Button_Export.Location = new Point(267, 5);
            TransactionLifecycleForm_Button_Export.Margin = new Padding(5);
            TransactionLifecycleForm_Button_Export.MinimumSize = new Size(75, 32);
            TransactionLifecycleForm_Button_Export.Name = "TransactionLifecycleForm_Button_Export";
            TransactionLifecycleForm_Button_Export.Size = new Size(75, 34);
            TransactionLifecycleForm_Button_Export.TabIndex = 0;
            TransactionLifecycleForm_Button_Export.Text = "📊 Export";
            TransactionLifecycleForm_Button_Export.UseVisualStyleBackColor = false;
            // 
            // TransactionLifecycleForm_Button_Print
            // 
            TransactionLifecycleForm_Button_Print.AutoSize = true;
            TransactionLifecycleForm_Button_Print.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionLifecycleForm_Button_Print.BackColor = Color.FromArgb(59, 130, 246);
            TransactionLifecycleForm_Button_Print.FlatStyle = FlatStyle.Flat;
            TransactionLifecycleForm_Button_Print.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            TransactionLifecycleForm_Button_Print.ForeColor = Color.White;
            TransactionLifecycleForm_Button_Print.Location = new Point(357, 5);
            TransactionLifecycleForm_Button_Print.Margin = new Padding(5);
            TransactionLifecycleForm_Button_Print.MinimumSize = new Size(75, 32);
            TransactionLifecycleForm_Button_Print.Name = "TransactionLifecycleForm_Button_Print";
            TransactionLifecycleForm_Button_Print.Size = new Size(75, 34);
            TransactionLifecycleForm_Button_Print.TabIndex = 1;
            TransactionLifecycleForm_Button_Print.Text = "🖨️ Print";
            TransactionLifecycleForm_Button_Print.UseVisualStyleBackColor = false;
            // 
            // TransactionLifecycleForm_Button_Close
            // 
            TransactionLifecycleForm_Button_Close.AutoSize = true;
            TransactionLifecycleForm_Button_Close.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionLifecycleForm_Button_Close.BackColor = Color.FromArgb(226, 232, 240);
            TransactionLifecycleForm_Button_Close.DialogResult = DialogResult.Cancel;
            TransactionLifecycleForm_Button_Close.FlatStyle = FlatStyle.Flat;
            TransactionLifecycleForm_Button_Close.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            TransactionLifecycleForm_Button_Close.Location = new Point(442, 5);
            TransactionLifecycleForm_Button_Close.Margin = new Padding(5);
            TransactionLifecycleForm_Button_Close.MinimumSize = new Size(75, 32);
            TransactionLifecycleForm_Button_Close.Name = "TransactionLifecycleForm_Button_Close";
            TransactionLifecycleForm_Button_Close.Size = new Size(75, 34);
            TransactionLifecycleForm_Button_Close.TabIndex = 2;
            TransactionLifecycleForm_Button_Close.Text = "❌ Close";
            TransactionLifecycleForm_Button_Close.UseVisualStyleBackColor = false;
            // 
            // TransactionLifecycleForm_StatusStrip
            // 
            TransactionLifecycleForm_StatusStrip.Items.AddRange(new ToolStripItem[] { TransactionLifecycleForm_ToolStripStatusLabel_Legend });
            TransactionLifecycleForm_StatusStrip.Location = new Point(0, 472);
            TransactionLifecycleForm_StatusStrip.Name = "TransactionLifecycleForm_StatusStrip";
            TransactionLifecycleForm_StatusStrip.Size = new Size(800, 22);
            TransactionLifecycleForm_StatusStrip.TabIndex = 1;
            TransactionLifecycleForm_StatusStrip.Text = "statusStrip1";
            // 
            // TransactionLifecycleForm_ToolStripStatusLabel_Legend
            // 
            TransactionLifecycleForm_ToolStripStatusLabel_Legend.Name = "TransactionLifecycleForm_ToolStripStatusLabel_Legend";
            TransactionLifecycleForm_ToolStripStatusLabel_Legend.Size = new Size(271, 17);
            TransactionLifecycleForm_ToolStripStatusLabel_Legend.Text = "📥 IN (Green)  |  🔄 TRANSFER (Blue)  |  📤 OUT (Red)  |  📦 Split (Orange)";
            // 
            // TransactionLifecycleForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            CancelButton = TransactionLifecycleForm_Button_Close;
            ClientSize = new Size(800, 494);
            Controls.Add(TransactionLifecycleForm_TableLayout_Main);
            Controls.Add(TransactionLifecycleForm_StatusStrip);
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(600, 400);
            Name = "TransactionLifecycleForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Transaction Lifecycle";
            TransactionLifecycleForm_TableLayout_Main.ResumeLayout(false);
            TransactionLifecycleForm_Panel_TreeView.ResumeLayout(false);
            TransactionLifecycleForm_Panel_DetailView.ResumeLayout(false);
            TransactionLifecycleForm_Panel_Buttons.ResumeLayout(false);
            TransactionLifecycleForm_Panel_Buttons.PerformLayout();
            TransactionLifecycleForm_TableLayout_Buttons.ResumeLayout(false);
            TransactionLifecycleForm_TableLayout_Buttons.PerformLayout();
            TransactionLifecycleForm_StatusStrip.ResumeLayout(false);
            TransactionLifecycleForm_StatusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel TransactionLifecycleForm_TableLayout_Main;
        private Panel TransactionLifecycleForm_Panel_TreeView;
        private TreeView TransactionLifecycleForm_TreeView_Lifecycle;
        private ImageList TransactionLifecycleForm_ImageList_Icons;
        private Panel TransactionLifecycleForm_Panel_DetailView;
        private Controls.Transactions.TransactionDetailPanel TransactionLifecycleForm_DetailPanel;
        private Panel TransactionLifecycleForm_Panel_Buttons;
        private TableLayoutPanel TransactionLifecycleForm_TableLayout_Buttons;
        private Button TransactionLifecycleForm_Button_Export;
        private Button TransactionLifecycleForm_Button_Print;
        private Button TransactionLifecycleForm_Button_Close;
        private StatusStrip TransactionLifecycleForm_StatusStrip;
        private ToolStripStatusLabel TransactionLifecycleForm_ToolStripStatusLabel_Legend;
    }
}
