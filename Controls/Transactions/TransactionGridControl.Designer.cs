namespace MTM_WIP_Application_Winforms.Controls.Transactions
{
    partial class TransactionGridControl
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
            TransactionGridControl_TableLayout_Main = new TableLayoutPanel();
            TransactionGridControl_StatusStrip_Pagination = new StatusStrip();
            TransactionGridControl_ToolStripButtons = new ToolStripSplitButton();
            TransactionGridControl_Button_ShowHideSearch = new ToolStripMenuItem();
            TransactionGridControl_Button_Previous = new ToolStripButton();
            TransactionGridControl_Button_Next = new ToolStripButton();
            TransactionGridControl_Label_PageIndicator = new ToolStripStatusLabel();
            TransactionGridControl_ToolStripSeparator1 = new ToolStripSeparator();
            TransactionGridControl_Label_RecordCount = new ToolStripStatusLabel();
            TransactionGridControl_TextBox_GoToPage = new ToolStripTextBox();
            TransactionGridControl_Button_GoToPage = new ToolStripButton();
            TransactionGridControl_DataGridView_Transactions = new DataGridView();
            TransactionGridControl_TransactionDetailPanel = new TransactionDetailPanel();
            TransactionGridControl_ToolStripSeparator2 = new ToolStripSeparator();
            TransactionGridControl_Label_PageNumber = new ToolStripStatusLabel();
            TransactionGridControl_TableLayout_Main.SuspendLayout();
            TransactionGridControl_StatusStrip_Pagination.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)TransactionGridControl_DataGridView_Transactions).BeginInit();
            SuspendLayout();
            // 
            // TransactionGridControl_TableLayout_Main
            // 
            TransactionGridControl_TableLayout_Main.AutoSize = true;
            TransactionGridControl_TableLayout_Main.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionGridControl_TableLayout_Main.ColumnCount = 2;
            TransactionGridControl_TableLayout_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            TransactionGridControl_TableLayout_Main.ColumnStyles.Add(new ColumnStyle());
            TransactionGridControl_TableLayout_Main.Controls.Add(TransactionGridControl_StatusStrip_Pagination, 0, 1);
            TransactionGridControl_TableLayout_Main.Controls.Add(TransactionGridControl_DataGridView_Transactions, 0, 0);
            TransactionGridControl_TableLayout_Main.Controls.Add(TransactionGridControl_TransactionDetailPanel, 1, 0);
            TransactionGridControl_TableLayout_Main.Dock = DockStyle.Fill;
            TransactionGridControl_TableLayout_Main.Location = new Point(2, 2);
            TransactionGridControl_TableLayout_Main.Margin = new Padding(0);
            TransactionGridControl_TableLayout_Main.Name = "TransactionGridControl_TableLayout_Main";
            TransactionGridControl_TableLayout_Main.RowCount = 2;
            TransactionGridControl_TableLayout_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            TransactionGridControl_TableLayout_Main.RowStyles.Add(new RowStyle(SizeType.Absolute, 29F));
            TransactionGridControl_TableLayout_Main.Size = new Size(915, 544);
            TransactionGridControl_TableLayout_Main.TabIndex = 0;
            // 
            // TransactionGridControl_StatusStrip_Pagination
            // 
            TransactionGridControl_TableLayout_Main.SetColumnSpan(TransactionGridControl_StatusStrip_Pagination, 2);
            TransactionGridControl_StatusStrip_Pagination.Dock = DockStyle.Fill;
            TransactionGridControl_StatusStrip_Pagination.GripMargin = new Padding(0);
            TransactionGridControl_StatusStrip_Pagination.Items.AddRange(new ToolStripItem[] { TransactionGridControl_ToolStripButtons, TransactionGridControl_Button_Previous, TransactionGridControl_Button_Next, TransactionGridControl_Label_PageIndicator, TransactionGridControl_ToolStripSeparator1, TransactionGridControl_Label_RecordCount, TransactionGridControl_Label_PageNumber, TransactionGridControl_TextBox_GoToPage, TransactionGridControl_Button_GoToPage, TransactionGridControl_ToolStripSeparator2 });
            TransactionGridControl_StatusStrip_Pagination.Location = new Point(0, 515);
            TransactionGridControl_StatusStrip_Pagination.Name = "TransactionGridControl_StatusStrip_Pagination";
            TransactionGridControl_StatusStrip_Pagination.Padding = new Padding(0);
            TransactionGridControl_StatusStrip_Pagination.Size = new Size(915, 29);
            TransactionGridControl_StatusStrip_Pagination.TabIndex = 1;
            TransactionGridControl_StatusStrip_Pagination.Text = "statusStrip1";
            // 
            // TransactionGridControl_ToolStripButtons
            // 
            TransactionGridControl_ToolStripButtons.DisplayStyle = ToolStripItemDisplayStyle.Image;
            TransactionGridControl_ToolStripButtons.DropDownItems.AddRange(new ToolStripItem[] { TransactionGridControl_Button_ShowHideSearch });
            TransactionGridControl_ToolStripButtons.Image = Properties.Resources.MTM;
            TransactionGridControl_ToolStripButtons.ImageTransparentColor = Color.Magenta;
            TransactionGridControl_ToolStripButtons.Name = "TransactionGridControl_ToolStripButtons";
            TransactionGridControl_ToolStripButtons.Size = new Size(32, 27);
            TransactionGridControl_ToolStripButtons.Text = "toolStripSplitButton1";
            // 
            // TransactionGridControl_Button_ShowHideSearch
            // 
            TransactionGridControl_Button_ShowHideSearch.Name = "TransactionGridControl_Button_ShowHideSearch";
            TransactionGridControl_Button_ShowHideSearch.Size = new Size(209, 22);
            TransactionGridControl_Button_ShowHideSearch.Text = "Show / Hide Search Panel";
            // 
            // TransactionGridControl_Button_Previous
            // 
            TransactionGridControl_Button_Previous.DisplayStyle = ToolStripItemDisplayStyle.Text;
            TransactionGridControl_Button_Previous.ImageTransparentColor = Color.Magenta;
            TransactionGridControl_Button_Previous.Margin = new Padding(3);
            TransactionGridControl_Button_Previous.Name = "TransactionGridControl_Button_Previous";
            TransactionGridControl_Button_Previous.Size = new Size(69, 23);
            TransactionGridControl_Button_Previous.Text = "← Previous";
            TransactionGridControl_Button_Previous.ToolTipText = "Go to previous page";
            // 
            // TransactionGridControl_Button_Next
            // 
            TransactionGridControl_Button_Next.DisplayStyle = ToolStripItemDisplayStyle.Text;
            TransactionGridControl_Button_Next.ImageTransparentColor = Color.Magenta;
            TransactionGridControl_Button_Next.Margin = new Padding(3);
            TransactionGridControl_Button_Next.Name = "TransactionGridControl_Button_Next";
            TransactionGridControl_Button_Next.Size = new Size(48, 23);
            TransactionGridControl_Button_Next.Text = "Next →";
            TransactionGridControl_Button_Next.ToolTipText = "Go to next page";
            // 
            // TransactionGridControl_Label_PageIndicator
            // 
            TransactionGridControl_Label_PageIndicator.Margin = new Padding(3);
            TransactionGridControl_Label_PageIndicator.Name = "TransactionGridControl_Label_PageIndicator";
            TransactionGridControl_Label_PageIndicator.Size = new Size(65, 23);
            TransactionGridControl_Label_PageIndicator.Text = "Page 1 of 1";
            // 
            // TransactionGridControl_ToolStripSeparator1
            // 
            TransactionGridControl_ToolStripSeparator1.Margin = new Padding(3);
            TransactionGridControl_ToolStripSeparator1.Name = "TransactionGridControl_ToolStripSeparator1";
            TransactionGridControl_ToolStripSeparator1.Size = new Size(6, 23);
            // 
            // TransactionGridControl_Label_RecordCount
            // 
            TransactionGridControl_Label_RecordCount.Margin = new Padding(3);
            TransactionGridControl_Label_RecordCount.Name = "TransactionGridControl_Label_RecordCount";
            TransactionGridControl_Label_RecordCount.Size = new Size(264, 23);
            TransactionGridControl_Label_RecordCount.Spring = true;
            TransactionGridControl_Label_RecordCount.Text = "0 records";
            TransactionGridControl_Label_RecordCount.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TransactionGridControl_TextBox_GoToPage
            // 
            TransactionGridControl_TextBox_GoToPage.BorderStyle = BorderStyle.FixedSingle;
            TransactionGridControl_TextBox_GoToPage.Margin = new Padding(3);
            TransactionGridControl_TextBox_GoToPage.Name = "TransactionGridControl_TextBox_GoToPage";
            TransactionGridControl_TextBox_GoToPage.Size = new Size(80, 23);
            TransactionGridControl_TextBox_GoToPage.ToolTipText = "Enter page number and press Enter";
            // 
            // TransactionGridControl_Button_GoToPage
            // 
            TransactionGridControl_Button_GoToPage.DisplayStyle = ToolStripItemDisplayStyle.Text;
            TransactionGridControl_Button_GoToPage.Margin = new Padding(3);
            TransactionGridControl_Button_GoToPage.Name = "TransactionGridControl_Button_GoToPage";
            TransactionGridControl_Button_GoToPage.Size = new Size(26, 23);
            TransactionGridControl_Button_GoToPage.Text = "Go";
            TransactionGridControl_Button_GoToPage.ToolTipText = "Jump to entered page number";
            // 
            // TransactionGridControl_DataGridView_Transactions
            // 
            TransactionGridControl_DataGridView_Transactions.AllowUserToAddRows = false;
            TransactionGridControl_DataGridView_Transactions.AllowUserToDeleteRows = false;
            TransactionGridControl_DataGridView_Transactions.AllowUserToOrderColumns = true;
            TransactionGridControl_DataGridView_Transactions.AllowUserToResizeRows = false;
            TransactionGridControl_DataGridView_Transactions.BackgroundColor = SystemColors.Window;
            TransactionGridControl_DataGridView_Transactions.BorderStyle = BorderStyle.Fixed3D;
            TransactionGridControl_DataGridView_Transactions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            TransactionGridControl_DataGridView_Transactions.Dock = DockStyle.Fill;
            TransactionGridControl_DataGridView_Transactions.Location = new Point(3, 3);
            TransactionGridControl_DataGridView_Transactions.MultiSelect = false;
            TransactionGridControl_DataGridView_Transactions.Name = "TransactionGridControl_DataGridView_Transactions";
            TransactionGridControl_DataGridView_Transactions.ReadOnly = true;
            TransactionGridControl_DataGridView_Transactions.RowHeadersVisible = false;
            TransactionGridControl_DataGridView_Transactions.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            TransactionGridControl_DataGridView_Transactions.Size = new Size(551, 509);
            TransactionGridControl_DataGridView_Transactions.TabIndex = 0;
            // 
            // TransactionGridControl_TransactionDetailPanel
            // 
            TransactionGridControl_TransactionDetailPanel.AutoSize = true;
            TransactionGridControl_TransactionDetailPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionGridControl_TransactionDetailPanel.Dock = DockStyle.Fill;
            TransactionGridControl_TransactionDetailPanel.Location = new Point(560, 3);
            TransactionGridControl_TransactionDetailPanel.MinimumSize = new Size(300, 400);
            TransactionGridControl_TransactionDetailPanel.Name = "TransactionGridControl_TransactionDetailPanel";
            TransactionGridControl_TransactionDetailPanel.Size = new Size(352, 509);
            TransactionGridControl_TransactionDetailPanel.TabIndex = 2;
            // 
            // TransactionGridControl_ToolStripSeparator2
            // 
            TransactionGridControl_ToolStripSeparator2.Margin = new Padding(3);
            TransactionGridControl_ToolStripSeparator2.Name = "TransactionGridControl_ToolStripSeparator2";
            TransactionGridControl_ToolStripSeparator2.Size = new Size(6, 23);
            // 
            // TransactionGridControl_Label_PageNumber
            // 
            TransactionGridControl_Label_PageNumber.Margin = new Padding(3);
            TransactionGridControl_Label_PageNumber.Name = "TransactionGridControl_Label_PageNumber";
            TransactionGridControl_Label_PageNumber.Size = new Size(264, 23);
            TransactionGridControl_Label_PageNumber.Spring = true;
            TransactionGridControl_Label_PageNumber.Text = "Page Number: ";
            TransactionGridControl_Label_PageNumber.TextAlign = ContentAlignment.MiddleRight;
            // 
            // TransactionGridControl
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(TransactionGridControl_TableLayout_Main);
            Name = "TransactionGridControl";
            Padding = new Padding(2);
            Size = new Size(919, 548);
            TransactionGridControl_TableLayout_Main.ResumeLayout(false);
            TransactionGridControl_TableLayout_Main.PerformLayout();
            TransactionGridControl_StatusStrip_Pagination.ResumeLayout(false);
            TransactionGridControl_StatusStrip_Pagination.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)TransactionGridControl_DataGridView_Transactions).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel TransactionGridControl_TableLayout_Main;
        private System.Windows.Forms.DataGridView TransactionGridControl_DataGridView_Transactions;
        private System.Windows.Forms.StatusStrip TransactionGridControl_StatusStrip_Pagination;
        private System.Windows.Forms.ToolStripButton TransactionGridControl_Button_Previous;
        private System.Windows.Forms.ToolStripButton TransactionGridControl_Button_Next;
        private System.Windows.Forms.ToolStripStatusLabel TransactionGridControl_Label_PageIndicator;
        private System.Windows.Forms.ToolStripStatusLabel TransactionGridControl_Label_RecordCount;
        private System.Windows.Forms.ToolStripSeparator TransactionGridControl_ToolStripSeparator1;
        private System.Windows.Forms.ToolStripTextBox TransactionGridControl_TextBox_GoToPage;
        private System.Windows.Forms.ToolStripButton TransactionGridControl_Button_GoToPage;
        private ToolStripSplitButton TransactionGridControl_ToolStripButtons;
        private ToolStripMenuItem TransactionGridControl_Button_ShowHideSearch;
        private TransactionDetailPanel TransactionGridControl_TransactionDetailPanel;
        private ToolStripStatusLabel TransactionGridControl_Label_PageNumber;
        private ToolStripSeparator TransactionGridControl_ToolStripSeparator2;
    }
}

