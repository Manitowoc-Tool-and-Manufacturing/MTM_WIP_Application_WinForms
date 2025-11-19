using System.Drawing;
using System.Windows.Forms;

namespace MTM_WIP_Application_Winforms.Forms.Transactions
{
    partial class Transactions
    {
        private Panel Transactions_Panel_Main;
        private TableLayoutPanel Transactions_TableLayout_Main;
        private Panel Transactions_Panel_Search;
        private Panel Transactions_Panel_Grid;


        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            Transactions_Panel_Main = new Panel();
            Transactions_TableLayout_Main = new TableLayoutPanel();
            Transactions_Label_Title = new Label();
            Transactions_Panel_Grid = new Panel();
            Transactions_UserControl_Grid = new MTM_WIP_Application_Winforms.Controls.Transactions.TransactionGridControl();
            Transactions_Panel_Search = new Panel();
            Transactions_UserControl_Search = new MTM_WIP_Application_Winforms.Controls.Transactions.TransactionSearchControl();
            Transactions_Panel_Main.SuspendLayout();
            Transactions_TableLayout_Main.SuspendLayout();
            Transactions_Panel_Grid.SuspendLayout();
            Transactions_Panel_Search.SuspendLayout();
            SuspendLayout();
            // 
            // Transactions_Panel_Main
            // 
            Transactions_Panel_Main.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Transactions_Panel_Main.Controls.Add(Transactions_TableLayout_Main);
            Transactions_Panel_Main.Dock = DockStyle.Fill;
            Transactions_Panel_Main.Location = new Point(0, 0);
            Transactions_Panel_Main.Name = "Transactions_Panel_Main";
            Transactions_Panel_Main.Size = new Size(1251, 661);
            Transactions_Panel_Main.TabIndex = 0;
            // 
            // Transactions_TableLayout_Main
            // 
            Transactions_TableLayout_Main.ColumnCount = 1;
            Transactions_TableLayout_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Transactions_TableLayout_Main.Controls.Add(Transactions_Label_Title, 0, 0);
            Transactions_TableLayout_Main.Controls.Add(Transactions_Panel_Grid, 0, 2);
            Transactions_TableLayout_Main.Controls.Add(Transactions_Panel_Search, 0, 1);
            Transactions_TableLayout_Main.Dock = DockStyle.Fill;
            Transactions_TableLayout_Main.Location = new Point(0, 0);
            Transactions_TableLayout_Main.Name = "Transactions_TableLayout_Main";
            Transactions_TableLayout_Main.RowCount = 3;
            Transactions_TableLayout_Main.RowStyles.Add(new RowStyle());
            Transactions_TableLayout_Main.RowStyles.Add(new RowStyle());
            Transactions_TableLayout_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Transactions_TableLayout_Main.Size = new Size(1251, 661);
            Transactions_TableLayout_Main.TabIndex = 0;
            // 
            // Transactions_Label_Title
            // 
            Transactions_Label_Title.AutoSize = true;
            Transactions_Label_Title.Dock = DockStyle.Fill;
            Transactions_Label_Title.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            Transactions_Label_Title.Location = new Point(3, 3);
            Transactions_Label_Title.Margin = new Padding(3);
            Transactions_Label_Title.Name = "Transactions_Label_Title";
            Transactions_Label_Title.Size = new Size(1245, 19);
            Transactions_Label_Title.TabIndex = 2;
            Transactions_Label_Title.Text = "MTM WIP Application - Transaction History System";
            Transactions_Label_Title.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Transactions_Panel_Grid
            // 
            Transactions_Panel_Grid.AutoSize = true;
            Transactions_Panel_Grid.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Transactions_Panel_Grid.Controls.Add(Transactions_UserControl_Grid);
            Transactions_Panel_Grid.Dock = DockStyle.Fill;
            Transactions_Panel_Grid.Location = new Point(3, 195);
            Transactions_Panel_Grid.Name = "Transactions_Panel_Grid";
            Transactions_Panel_Grid.Size = new Size(1245, 463);
            Transactions_Panel_Grid.TabIndex = 1;
            // 
            // Transactions_UserControl_Grid
            // 
            Transactions_UserControl_Grid.AutoSize = true;
            Transactions_UserControl_Grid.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Transactions_UserControl_Grid.Dock = DockStyle.Fill;
            Transactions_UserControl_Grid.Location = new Point(0, 0);
            Transactions_UserControl_Grid.Margin = new Padding(0);
            Transactions_UserControl_Grid.Name = "Transactions_UserControl_Grid";
            Transactions_UserControl_Grid.Padding = new Padding(2);
            Transactions_UserControl_Grid.Size = new Size(1245, 463);
            Transactions_UserControl_Grid.TabIndex = 0;
            // 
            // Transactions_Panel_Search
            // 
            Transactions_Panel_Search.AutoSize = true;
            Transactions_Panel_Search.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Transactions_Panel_Search.Controls.Add(Transactions_UserControl_Search);
            Transactions_Panel_Search.Dock = DockStyle.Fill;
            Transactions_Panel_Search.Location = new Point(3, 28);
            Transactions_Panel_Search.Name = "Transactions_Panel_Search";
            Transactions_Panel_Search.Size = new Size(1245, 161);
            Transactions_Panel_Search.TabIndex = 0;
            // 
            // Transactions_UserControl_Search
            // 
            Transactions_UserControl_Search.AutoSize = true;
            Transactions_UserControl_Search.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Transactions_UserControl_Search.Dock = DockStyle.Fill;
            Transactions_UserControl_Search.Location = new Point(0, 0);
            Transactions_UserControl_Search.Margin = new Padding(0);
            Transactions_UserControl_Search.Name = "Transactions_UserControl_Search";
            Transactions_UserControl_Search.Size = new Size(1245, 161);
            Transactions_UserControl_Search.TabIndex = 0;
            // 
            // Transactions
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1251, 661);
            Controls.Add(Transactions_Panel_Main);
            MinimumSize = new Size(1200, 700);
            Name = "Transactions";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Transaction Viewer";
            Transactions_Panel_Main.ResumeLayout(false);
            Transactions_TableLayout_Main.ResumeLayout(false);
            Transactions_TableLayout_Main.PerformLayout();
            Transactions_Panel_Grid.ResumeLayout(false);
            Transactions_Panel_Grid.PerformLayout();
            Transactions_Panel_Search.ResumeLayout(false);
            Transactions_Panel_Search.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        #region Controls

        private Controls.Transactions.TransactionSearchControl Transactions_UserControl_Search;
        private Controls.Transactions.TransactionGridControl Transactions_UserControl_Grid;

        #endregion

        private Label Transactions_Label_Title;
    }
}
