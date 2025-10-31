using System.Drawing;
using System.Windows.Forms;

namespace MTM_WIP_Application_Winforms.Forms.Transactions
{
    partial class Transactions
    {
        #region Fields

        private System.ComponentModel.IContainer components = null;

        #endregion

        private Panel MainPanel;
        private TableLayoutPanel MainTableLayout;
        private Panel SearchPanel;
        private Panel GridPanel;


        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            MainPanel = new Panel();
            MainTableLayout = new TableLayoutPanel();
            SearchPanel = new Panel();
            searchControl = new MTM_WIP_Application_Winforms.Controls.Transactions.TransactionSearchControl();
            GridPanel = new Panel();
            gridControl = new MTM_WIP_Application_Winforms.Controls.Transactions.TransactionGridControl();
            MainPanel.SuspendLayout();
            MainTableLayout.SuspendLayout();
            SearchPanel.SuspendLayout();
            GridPanel.SuspendLayout();
            SuspendLayout();
            // 
            // MainPanel
            // 
            MainPanel.Controls.Add(MainTableLayout);
            MainPanel.Dock = DockStyle.Fill;
            MainPanel.Location = new Point(0, 0);
            MainPanel.Name = "MainPanel";
            MainPanel.Size = new Size(1912, 768);
            MainPanel.TabIndex = 0;
            // 
            // MainTableLayout
            // 
            MainTableLayout.ColumnCount = 1;
            MainTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            MainTableLayout.Controls.Add(SearchPanel, 0, 0);
            MainTableLayout.Controls.Add(GridPanel, 0, 1);
            MainTableLayout.Dock = DockStyle.Fill;
            MainTableLayout.Location = new Point(0, 0);
            MainTableLayout.Name = "MainTableLayout";
            MainTableLayout.RowCount = 2;
            MainTableLayout.RowStyles.Add(new RowStyle());
            MainTableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            MainTableLayout.Size = new Size(1912, 768);
            MainTableLayout.TabIndex = 0;
            // 
            // SearchPanel
            // 
            SearchPanel.AutoSize = true;
            SearchPanel.Controls.Add(searchControl);
            SearchPanel.Dock = DockStyle.Fill;
            SearchPanel.Location = new Point(3, 3);
            SearchPanel.Name = "SearchPanel";
            SearchPanel.Size = new Size(1906, 883);
            SearchPanel.TabIndex = 0;
            // 
            // searchControl
            // 
            searchControl.AutoSize = true;
            searchControl.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            searchControl.Dock = DockStyle.Fill;
            searchControl.Location = new Point(0, 0);
            searchControl.Margin = new Padding(41, 19, 41, 19);
            searchControl.MinimumSize = new Size(8229, 768);
            searchControl.Name = "searchControl";
            searchControl.Size = new Size(8229, 883);
            searchControl.TabIndex = 0;
            // 
            // GridPanel
            // 
            GridPanel.Controls.Add(gridControl);
            GridPanel.Dock = DockStyle.Fill;
            GridPanel.Location = new Point(3, 892);
            GridPanel.Name = "GridPanel";
            GridPanel.Size = new Size(1906, 1);
            GridPanel.TabIndex = 1;
            // 
            // gridControl
            // 
            gridControl.Dock = DockStyle.Fill;
            gridControl.Location = new Point(0, 0);
            gridControl.Margin = new Padding(41, 19, 41, 19);
            gridControl.MinimumSize = new Size(5486, 1600);
            gridControl.Name = "gridControl";
            gridControl.Size = new Size(5486, 1600);
            gridControl.TabIndex = 0;
            // 
            // Transactions
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1912, 768);
            Controls.Add(MainPanel);
            MinimumSize = new Size(800, 600);
            Name = "Transactions";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Transaction Viewer";
            MainPanel.ResumeLayout(false);
            MainTableLayout.ResumeLayout(false);
            MainTableLayout.PerformLayout();
            SearchPanel.ResumeLayout(false);
            SearchPanel.PerformLayout();
            GridPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        #region Controls

        private Controls.Transactions.TransactionSearchControl searchControl;
        private Controls.Transactions.TransactionGridControl gridControl;

        #endregion
    }
}
