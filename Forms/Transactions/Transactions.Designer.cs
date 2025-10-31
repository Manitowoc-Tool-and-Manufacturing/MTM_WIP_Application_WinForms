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
            GridPanel = new Panel();
            MainPanel.SuspendLayout();
            MainTableLayout.SuspendLayout();
            SuspendLayout();
            // 
            // MainPanel
            // 
            MainPanel.Controls.Add(MainTableLayout);
            MainPanel.Dock = DockStyle.Fill;
            MainPanel.Location = new Point(0, 0);
            MainPanel.Name = "MainPanel";
            MainPanel.Size = new Size(784, 561);
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
            MainTableLayout.Size = new Size(784, 561);
            MainTableLayout.TabIndex = 0;
            // 
            // SearchPanel
            // 
            SearchPanel.AutoSize = true;
            SearchPanel.Dock = DockStyle.Fill;
            SearchPanel.Location = new Point(3, 3);
            SearchPanel.Name = "SearchPanel";
            SearchPanel.Size = new Size(778, 1);
            SearchPanel.TabIndex = 0;
            // 
            // GridPanel
            // 
            GridPanel.Dock = DockStyle.Fill;
            GridPanel.Location = new Point(3, 9);
            GridPanel.Name = "GridPanel";
            GridPanel.Size = new Size(778, 549);
            GridPanel.TabIndex = 1;
            // 
            // Transactions
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 561);
            Controls.Add(MainPanel);
            Name = "Transactions";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Transaction Viewer";
            MainPanel.ResumeLayout(false);
            MainTableLayout.ResumeLayout(false);
            MainTableLayout.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        #region Controls

        private Controls.Transactions.TransactionSearchControl searchControl;
        private Controls.Transactions.TransactionGridControl gridControl;

        #endregion
    }
}
