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
            TransactionGridControl_StatusStrip_Main = new StatusStrip();
            TransactionGridControl_ToolStripButtons = new ToolStripSplitButton();
            TransactionGridControl_Button_ShowHideSearch = new ToolStripMenuItem();
            TransactionGridControl_Button_Previous = new ToolStripButton();
            TransactionGridControl_Button_Next = new ToolStripButton();
            TransactionGridControl_Label_PageIndicator = new ToolStripStatusLabel();
            TransactionGridControl_ToolStripSeparator1 = new ToolStripSeparator();
            TransactionGridControl_Label_RecordCount = new ToolStripStatusLabel();
            TransactionGridControl_Label_PageNumber = new ToolStripStatusLabel();
            TransactionGridControl_TextBox_GoToPage = new ToolStripTextBox();
            TransactionGridControl_Button_GoToPage = new ToolStripButton();
            TransactionGridControl_ToolStripSeparator2 = new ToolStripSeparator();
            TransactionGridControl_Button_AdminTools = new ToolStripButton();
            TransactionGridControl_ToolStripSeparator4 = new ToolStripSeparator();
            TransactionGridControl_ToolStripSeparator6 = new ToolStripSeparator();
            TransactionGridControl_Button_ToggleDetails = new ToolStripButton();
            TransactionGridControl_ToolStripSeparator5 = new ToolStripSeparator();
            TransactionGridControl_TransactionDetailPanel = new TransactionDetailPanel();
            TransactionGridControl_Panel_DataGridView = new Panel();
            TransactionGridControl_DataGridView_Transactions = new DataGridView();
            TransactionGridControl_Model_Transactions_Core_AnalyticsControl = new Model_Transactions_Core_AnalyticsControl();
            TransactionGridControl_Button_Analytics = new ToolStripButton();
            TransactionGridControl_ToolStripSeparator3 = new ToolStripSeparator();
            TransactionGridControl_Button_Print = new ToolStripButton();
            TransactionGridControl_TableLayout_Main.SuspendLayout();
            TransactionGridControl_StatusStrip_Main.SuspendLayout();
            TransactionGridControl_Panel_DataGridView.SuspendLayout();
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
            TransactionGridControl_TableLayout_Main.Controls.Add(TransactionGridControl_StatusStrip_Main, 0, 1);
            TransactionGridControl_TableLayout_Main.Controls.Add(TransactionGridControl_TransactionDetailPanel, 1, 0);
            TransactionGridControl_TableLayout_Main.Controls.Add(TransactionGridControl_Panel_DataGridView, 0, 0);
            TransactionGridControl_TableLayout_Main.Dock = DockStyle.Fill;
            TransactionGridControl_TableLayout_Main.Location = new Point(2, 2);
            TransactionGridControl_TableLayout_Main.Margin = new Padding(0);
            TransactionGridControl_TableLayout_Main.Name = "TransactionGridControl_TableLayout_Main";
            TransactionGridControl_TableLayout_Main.RowCount = 2;
            TransactionGridControl_TableLayout_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            TransactionGridControl_TableLayout_Main.RowStyles.Add(new RowStyle(SizeType.Absolute, 29F));
            TransactionGridControl_TableLayout_Main.Size = new Size(1159, 378);
            TransactionGridControl_TableLayout_Main.TabIndex = 0;
            // 
            // TransactionGridControl_StatusStrip_Main
            // 
            TransactionGridControl_TableLayout_Main.SetColumnSpan(TransactionGridControl_StatusStrip_Main, 2);
            TransactionGridControl_StatusStrip_Main.Dock = DockStyle.Fill;
            TransactionGridControl_StatusStrip_Main.GripMargin = new Padding(0);
            TransactionGridControl_StatusStrip_Main.Items.AddRange(new ToolStripItem[] { TransactionGridControl_ToolStripButtons, TransactionGridControl_Button_Previous, TransactionGridControl_Button_Next, TransactionGridControl_Label_PageIndicator, TransactionGridControl_ToolStripSeparator1, TransactionGridControl_Label_RecordCount, TransactionGridControl_Label_PageNumber, TransactionGridControl_TextBox_GoToPage, TransactionGridControl_Button_GoToPage, TransactionGridControl_ToolStripSeparator2, TransactionGridControl_Button_Print, TransactionGridControl_ToolStripSeparator3, TransactionGridControl_Button_AdminTools, TransactionGridControl_ToolStripSeparator4, TransactionGridControl_Button_Analytics, TransactionGridControl_ToolStripSeparator6, TransactionGridControl_Button_ToggleDetails, TransactionGridControl_ToolStripSeparator5 });
            TransactionGridControl_StatusStrip_Main.Location = new Point(0, 349);
            TransactionGridControl_StatusStrip_Main.Name = "TransactionGridControl_StatusStrip_Main";
            TransactionGridControl_StatusStrip_Main.Padding = new Padding(0);
            TransactionGridControl_StatusStrip_Main.ShowItemToolTips = true;
            TransactionGridControl_StatusStrip_Main.Size = new Size(1159, 29);
            TransactionGridControl_StatusStrip_Main.TabIndex = 1;
            TransactionGridControl_StatusStrip_Main.Text = "statusStrip1";
            // 
            // TransactionGridControl_ToolStripButtons
            // 
            TransactionGridControl_ToolStripButtons.DisplayStyle = ToolStripItemDisplayStyle.Image;
            TransactionGridControl_ToolStripButtons.DropDownItems.AddRange(new ToolStripItem[] { TransactionGridControl_Button_ShowHideSearch });
            TransactionGridControl_ToolStripButtons.Image = Properties.Resources.MTM;
            TransactionGridControl_ToolStripButtons.ImageTransparentColor = Color.Magenta;
            TransactionGridControl_ToolStripButtons.Name = "TransactionGridControl_ToolStripButtons";
            TransactionGridControl_ToolStripButtons.Size = new Size(32, 27);
            TransactionGridControl_ToolStripButtons.Text = "Transaction Settings / Options";
            // 
            // TransactionGridControl_Button_ShowHideSearch
            // 
            TransactionGridControl_Button_ShowHideSearch.Name = "TransactionGridControl_Button_ShowHideSearch";
            TransactionGridControl_Button_ShowHideSearch.Size = new Size(209, 22);
            TransactionGridControl_Button_ShowHideSearch.Text = "Show / Hide Search Panel";
            TransactionGridControl_Button_ShowHideSearch.ToolTipText = "Toggle search panel visibility";
            // 
            // TransactionGridControl_Button_Previous
            // 
            TransactionGridControl_Button_Previous.DisplayStyle = ToolStripItemDisplayStyle.Text;
            TransactionGridControl_Button_Previous.ImageTransparentColor = Color.Magenta;
            TransactionGridControl_Button_Previous.Margin = new Padding(3);
            TransactionGridControl_Button_Previous.Name = "TransactionGridControl_Button_Previous";
            TransactionGridControl_Button_Previous.Size = new Size(69, 23);
            TransactionGridControl_Button_Previous.Text = "🡰 Previous";
            TransactionGridControl_Button_Previous.ToolTipText = "Go to previous page";
            // 
            // TransactionGridControl_Button_Next
            // 
            TransactionGridControl_Button_Next.DisplayStyle = ToolStripItemDisplayStyle.Text;
            TransactionGridControl_Button_Next.ImageTransparentColor = Color.Magenta;
            TransactionGridControl_Button_Next.Margin = new Padding(3);
            TransactionGridControl_Button_Next.Name = "TransactionGridControl_Button_Next";
            TransactionGridControl_Button_Next.Size = new Size(48, 23);
            TransactionGridControl_Button_Next.Text = "Next 🡲";
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
            // TransactionGridControl_Label_PageNumber
            // 
            TransactionGridControl_Label_PageNumber.Margin = new Padding(3);
            TransactionGridControl_Label_PageNumber.Name = "TransactionGridControl_Label_PageNumber";
            TransactionGridControl_Label_PageNumber.Size = new Size(264, 23);
            TransactionGridControl_Label_PageNumber.Spring = true;
            TransactionGridControl_Label_PageNumber.Text = "Page Number: ";
            TransactionGridControl_Label_PageNumber.TextAlign = ContentAlignment.MiddleRight;
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
            TransactionGridControl_Button_GoToPage.ToolTipText = "Go to entered page number";
            // 
            // TransactionGridControl_ToolStripSeparator2
            // 
            TransactionGridControl_ToolStripSeparator2.Margin = new Padding(3);
            TransactionGridControl_ToolStripSeparator2.Name = "TransactionGridControl_ToolStripSeparator2";
            TransactionGridControl_ToolStripSeparator2.Size = new Size(6, 23);
            // 
            // TransactionGridControl_Button_AdminTools
            // 
            TransactionGridControl_Button_AdminTools.DisplayStyle = ToolStripItemDisplayStyle.Text;
            TransactionGridControl_Button_AdminTools.Margin = new Padding(3);
            TransactionGridControl_Button_AdminTools.Name = "TransactionGridControl_Button_AdminTools";
            TransactionGridControl_Button_AdminTools.Size = new Size(23, 23);
            TransactionGridControl_Button_AdminTools.Text = "👨🏻‍💻";
            TransactionGridControl_Button_AdminTools.ToolTipText = "Administrative Tools";
            // 
            // TransactionGridControl_ToolStripSeparator4
            // 
            TransactionGridControl_ToolStripSeparator4.Margin = new Padding(3);
            TransactionGridControl_ToolStripSeparator4.Name = "TransactionGridControl_ToolStripSeparator4";
            TransactionGridControl_ToolStripSeparator4.Size = new Size(6, 23);
            // 
            // TransactionGridControl_ToolStripSeparator6
            // 
            TransactionGridControl_ToolStripSeparator6.Margin = new Padding(3);
            TransactionGridControl_ToolStripSeparator6.Name = "TransactionGridControl_ToolStripSeparator6";
            TransactionGridControl_ToolStripSeparator6.Size = new Size(6, 23);
            // 
            // TransactionGridControl_Button_ToggleDetails
            // 
            TransactionGridControl_Button_ToggleDetails.DisplayStyle = ToolStripItemDisplayStyle.Text;
            TransactionGridControl_Button_ToggleDetails.Margin = new Padding(3);
            TransactionGridControl_Button_ToggleDetails.Name = "TransactionGridControl_Button_ToggleDetails";
            TransactionGridControl_Button_ToggleDetails.Size = new Size(36, 23);
            TransactionGridControl_Button_ToggleDetails.Text = "📋 🡲";
            TransactionGridControl_Button_ToggleDetails.ToolTipText = "Hide/Show transaction details panel";
            // 
            // TransactionGridControl_ToolStripSeparator5
            // 
            TransactionGridControl_ToolStripSeparator5.Margin = new Padding(3);
            TransactionGridControl_ToolStripSeparator5.Name = "TransactionGridControl_ToolStripSeparator5";
            TransactionGridControl_ToolStripSeparator5.Size = new Size(6, 23);
            // 
            // TransactionGridControl_TransactionDetailPanel
            // 
            TransactionGridControl_TransactionDetailPanel.AutoSize = true;
            TransactionGridControl_TransactionDetailPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionGridControl_TransactionDetailPanel.DetailsCollapsed = false;
            TransactionGridControl_TransactionDetailPanel.Dock = DockStyle.Fill;
            TransactionGridControl_TransactionDetailPanel.Location = new Point(867, 3);
            TransactionGridControl_TransactionDetailPanel.Name = "TransactionGridControl_TransactionDetailPanel";
            TransactionGridControl_TransactionDetailPanel.Size = new Size(289, 343);
            TransactionGridControl_TransactionDetailPanel.TabIndex = 2;
            // 
            // TransactionGridControl_Panel_DataGridView
            // 
            TransactionGridControl_Panel_DataGridView.AutoSize = true;
            TransactionGridControl_Panel_DataGridView.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionGridControl_Panel_DataGridView.Controls.Add(TransactionGridControl_DataGridView_Transactions);
            TransactionGridControl_Panel_DataGridView.Controls.Add(TransactionGridControl_Model_Transactions_Core_AnalyticsControl);
            TransactionGridControl_Panel_DataGridView.Dock = DockStyle.Fill;
            TransactionGridControl_Panel_DataGridView.Location = new Point(3, 3);
            TransactionGridControl_Panel_DataGridView.Name = "TransactionGridControl_Panel_DataGridView";
            TransactionGridControl_Panel_DataGridView.Size = new Size(858, 343);
            TransactionGridControl_Panel_DataGridView.TabIndex = 4;
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
            TransactionGridControl_DataGridView_Transactions.Location = new Point(0, 0);
            TransactionGridControl_DataGridView_Transactions.MultiSelect = false;
            TransactionGridControl_DataGridView_Transactions.Name = "TransactionGridControl_DataGridView_Transactions";
            TransactionGridControl_DataGridView_Transactions.ReadOnly = true;
            TransactionGridControl_DataGridView_Transactions.RowHeadersVisible = false;
            TransactionGridControl_DataGridView_Transactions.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            TransactionGridControl_DataGridView_Transactions.Size = new Size(858, 343);
            TransactionGridControl_DataGridView_Transactions.TabIndex = 1;
            // 
            // TransactionGridControl_Model_Transactions_Core_AnalyticsControl
            // 
            TransactionGridControl_Model_Transactions_Core_AnalyticsControl.AutoSize = true;
            TransactionGridControl_Model_Transactions_Core_AnalyticsControl.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionGridControl_Model_Transactions_Core_AnalyticsControl.Dock = DockStyle.Fill;
            TransactionGridControl_Model_Transactions_Core_AnalyticsControl.Location = new Point(0, 0);
            TransactionGridControl_Model_Transactions_Core_AnalyticsControl.MinimumSize = new Size(600, 200);
            TransactionGridControl_Model_Transactions_Core_AnalyticsControl.Name = "TransactionGridControl_Model_Transactions_Core_AnalyticsControl";
            TransactionGridControl_Model_Transactions_Core_AnalyticsControl.Size = new Size(858, 343);
            TransactionGridControl_Model_Transactions_Core_AnalyticsControl.TabIndex = 3;
            TransactionGridControl_Model_Transactions_Core_AnalyticsControl.Visible = false;
            // 
            // TransactionGridControl_Button_Analytics
            // 
            TransactionGridControl_Button_Analytics.DisplayStyle = ToolStripItemDisplayStyle.Text;
            TransactionGridControl_Button_Analytics.Margin = new Padding(3);
            TransactionGridControl_Button_Analytics.Name = "TransactionGridControl_Button_Analytics";
            TransactionGridControl_Button_Analytics.Size = new Size(59, 23);
            TransactionGridControl_Button_Analytics.Text = "Analytics";
            TransactionGridControl_Button_Analytics.ToolTipText = "Show analytics summary";
            // 
            // TransactionGridControl_ToolStripSeparator3
            // 
            TransactionGridControl_ToolStripSeparator3.Margin = new Padding(3);
            TransactionGridControl_ToolStripSeparator3.Name = "TransactionGridControl_ToolStripSeparator3";
            TransactionGridControl_ToolStripSeparator3.Size = new Size(6, 23);
            // 
            // TransactionGridControl_Button_Print
            // 
            TransactionGridControl_Button_Print.DisplayStyle = ToolStripItemDisplayStyle.Text;
            TransactionGridControl_Button_Print.Margin = new Padding(3);
            TransactionGridControl_Button_Print.Name = "TransactionGridControl_Button_Print";
            TransactionGridControl_Button_Print.Size = new Size(23, 23);
            TransactionGridControl_Button_Print.Text = "🖨️";
            TransactionGridControl_Button_Print.ToolTipText = "Print transaction report";
            // 
            // TransactionGridControl
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(TransactionGridControl_TableLayout_Main);
            Name = "TransactionGridControl";
            Padding = new Padding(2);
            Size = new Size(1163, 382);
            TransactionGridControl_TableLayout_Main.ResumeLayout(false);
            TransactionGridControl_TableLayout_Main.PerformLayout();
            TransactionGridControl_StatusStrip_Main.ResumeLayout(false);
            TransactionGridControl_StatusStrip_Main.PerformLayout();
            TransactionGridControl_Panel_DataGridView.ResumeLayout(false);
            TransactionGridControl_Panel_DataGridView.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)TransactionGridControl_DataGridView_Transactions).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel TransactionGridControl_TableLayout_Main;
        private System.Windows.Forms.StatusStrip TransactionGridControl_StatusStrip_Main;
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
        private Model_Transactions_Core_AnalyticsControl TransactionGridControl_Model_Transactions_Core_AnalyticsControl;
        private ToolStripStatusLabel TransactionGridControl_Label_PageNumber;
        private ToolStripSeparator TransactionGridControl_ToolStripSeparator2;
        private ToolStripButton TransactionGridControl_Button_AdminTools;
        private ToolStripSeparator TransactionGridControl_ToolStripSeparator4;
        private ToolStripSeparator TransactionGridControl_ToolStripSeparator6;
        private ToolStripButton TransactionGridControl_Button_ToggleDetails;
        private ToolStripSeparator TransactionGridControl_ToolStripSeparator5;
        private Panel TransactionGridControl_Panel_DataGridView;
        private DataGridView TransactionGridControl_DataGridView_Transactions;
        private ToolStripButton TransactionGridControl_Button_Print;
        private ToolStripSeparator TransactionGridControl_ToolStripSeparator3;
        private ToolStripButton TransactionGridControl_Button_Analytics;
    }
}

