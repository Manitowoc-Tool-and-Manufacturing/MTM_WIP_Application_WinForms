namespace MTM_WIP_Application_Winforms.Controls.Transactions
{
    partial class TransactionAnalyticsControl
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
            TransactionAnalyticsControl_TableLayout_Main = new TableLayoutPanel();
            TransactionAnalyticsControl_Panel_Total = new Panel();
            TransactionAnalyticsControl_Label_TotalCaption = new Label();
            TransactionAnalyticsControl_Label_TotalValue = new Label();
            TransactionAnalyticsControl_Panel_In = new Panel();
            TransactionAnalyticsControl_Label_InCaption = new Label();
            TransactionAnalyticsControl_Label_InValue = new Label();
            TransactionAnalyticsControl_Label_InPercentage = new Label();
            TransactionAnalyticsControl_Panel_Out = new Panel();
            TransactionAnalyticsControl_Label_OutCaption = new Label();
            TransactionAnalyticsControl_Label_OutValue = new Label();
            TransactionAnalyticsControl_Label_OutPercentage = new Label();
            TransactionAnalyticsControl_Panel_Transfer = new Panel();
            TransactionAnalyticsControl_Label_TransferCaption = new Label();
            TransactionAnalyticsControl_Label_TransferValue = new Label();
            TransactionAnalyticsControl_Label_TransferPercentage = new Label();
            TransactionAnalyticsControl_TableLayout_Main.SuspendLayout();
            TransactionAnalyticsControl_Panel_Total.SuspendLayout();
            TransactionAnalyticsControl_Panel_In.SuspendLayout();
            TransactionAnalyticsControl_Panel_Out.SuspendLayout();
            TransactionAnalyticsControl_Panel_Transfer.SuspendLayout();
            SuspendLayout();
            // 
            // TransactionAnalyticsControl_TableLayout_Main
            // 
            TransactionAnalyticsControl_TableLayout_Main.AutoSize = true;
            TransactionAnalyticsControl_TableLayout_Main.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionAnalyticsControl_TableLayout_Main.ColumnCount = 4;
            TransactionAnalyticsControl_TableLayout_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            TransactionAnalyticsControl_TableLayout_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            TransactionAnalyticsControl_TableLayout_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            TransactionAnalyticsControl_TableLayout_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            TransactionAnalyticsControl_TableLayout_Main.Controls.Add(TransactionAnalyticsControl_Panel_Total, 0, 0);
            TransactionAnalyticsControl_TableLayout_Main.Controls.Add(TransactionAnalyticsControl_Panel_In, 1, 0);
            TransactionAnalyticsControl_TableLayout_Main.Controls.Add(TransactionAnalyticsControl_Panel_Out, 2, 0);
            TransactionAnalyticsControl_TableLayout_Main.Controls.Add(TransactionAnalyticsControl_Panel_Transfer, 3, 0);
            TransactionAnalyticsControl_TableLayout_Main.Dock = DockStyle.Fill;
            TransactionAnalyticsControl_TableLayout_Main.Location = new Point(0, 0);
            TransactionAnalyticsControl_TableLayout_Main.Margin = new Padding(0);
            TransactionAnalyticsControl_TableLayout_Main.Name = "TransactionAnalyticsControl_TableLayout_Main";
            TransactionAnalyticsControl_TableLayout_Main.Padding = new Padding(5);
            TransactionAnalyticsControl_TableLayout_Main.RowCount = 1;
            TransactionAnalyticsControl_TableLayout_Main.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            TransactionAnalyticsControl_TableLayout_Main.Size = new Size(800, 100);
            TransactionAnalyticsControl_TableLayout_Main.TabIndex = 0;
            // 
            // TransactionAnalyticsControl_Panel_Total
            // 
            TransactionAnalyticsControl_Panel_Total.AutoSize = true;
            TransactionAnalyticsControl_Panel_Total.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionAnalyticsControl_Panel_Total.BackColor = Color.FromArgb(229, 231, 235);
            TransactionAnalyticsControl_Panel_Total.BorderStyle = BorderStyle.FixedSingle;
            TransactionAnalyticsControl_Panel_Total.Controls.Add(TransactionAnalyticsControl_Label_TotalCaption);
            TransactionAnalyticsControl_Panel_Total.Controls.Add(TransactionAnalyticsControl_Label_TotalValue);
            TransactionAnalyticsControl_Panel_Total.Dock = DockStyle.Fill;
            TransactionAnalyticsControl_Panel_Total.Location = new Point(8, 8);
            TransactionAnalyticsControl_Panel_Total.Margin = new Padding(3);
            TransactionAnalyticsControl_Panel_Total.MinimumSize = new Size(150, 80);
            TransactionAnalyticsControl_Panel_Total.Name = "TransactionAnalyticsControl_Panel_Total";
            TransactionAnalyticsControl_Panel_Total.Padding = new Padding(10);
            TransactionAnalyticsControl_Panel_Total.Size = new Size(185, 84);
            TransactionAnalyticsControl_Panel_Total.TabIndex = 0;
            // 
            // TransactionAnalyticsControl_Label_TotalCaption
            // 
            TransactionAnalyticsControl_Label_TotalCaption.AutoSize = true;
            TransactionAnalyticsControl_Label_TotalCaption.Dock = DockStyle.Top;
            TransactionAnalyticsControl_Label_TotalCaption.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            TransactionAnalyticsControl_Label_TotalCaption.Location = new Point(10, 10);
            TransactionAnalyticsControl_Label_TotalCaption.Name = "TransactionAnalyticsControl_Label_TotalCaption";
            TransactionAnalyticsControl_Label_TotalCaption.Size = new Size(141, 19);
            TransactionAnalyticsControl_Label_TotalCaption.TabIndex = 0;
            TransactionAnalyticsControl_Label_TotalCaption.Text = "Total Transactions";
            // 
            // TransactionAnalyticsControl_Label_TotalValue
            // 
            TransactionAnalyticsControl_Label_TotalValue.AutoSize = true;
            TransactionAnalyticsControl_Label_TotalValue.Dock = DockStyle.Bottom;
            TransactionAnalyticsControl_Label_TotalValue.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            TransactionAnalyticsControl_Label_TotalValue.Location = new Point(10, 42);
            TransactionAnalyticsControl_Label_TotalValue.Name = "TransactionAnalyticsControl_Label_TotalValue";
            TransactionAnalyticsControl_Label_TotalValue.Size = new Size(28, 32);
            TransactionAnalyticsControl_Label_TotalValue.TabIndex = 1;
            TransactionAnalyticsControl_Label_TotalValue.Text = "—";
            // 
            // TransactionAnalyticsControl_Panel_In
            // 
            TransactionAnalyticsControl_Panel_In.AutoSize = true;
            TransactionAnalyticsControl_Panel_In.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionAnalyticsControl_Panel_In.BackColor = Color.FromArgb(220, 252, 231);
            TransactionAnalyticsControl_Panel_In.BorderStyle = BorderStyle.FixedSingle;
            TransactionAnalyticsControl_Panel_In.Controls.Add(TransactionAnalyticsControl_Label_InCaption);
            TransactionAnalyticsControl_Panel_In.Controls.Add(TransactionAnalyticsControl_Label_InValue);
            TransactionAnalyticsControl_Panel_In.Controls.Add(TransactionAnalyticsControl_Label_InPercentage);
            TransactionAnalyticsControl_Panel_In.Dock = DockStyle.Fill;
            TransactionAnalyticsControl_Panel_In.Location = new Point(199, 8);
            TransactionAnalyticsControl_Panel_In.Margin = new Padding(3);
            TransactionAnalyticsControl_Panel_In.MinimumSize = new Size(150, 80);
            TransactionAnalyticsControl_Panel_In.Name = "TransactionAnalyticsControl_Panel_In";
            TransactionAnalyticsControl_Panel_In.Padding = new Padding(10);
            TransactionAnalyticsControl_Panel_In.Size = new Size(185, 84);
            TransactionAnalyticsControl_Panel_In.TabIndex = 1;
            // 
            // TransactionAnalyticsControl_Label_InCaption
            // 
            TransactionAnalyticsControl_Label_InCaption.AutoSize = true;
            TransactionAnalyticsControl_Label_InCaption.Dock = DockStyle.Top;
            TransactionAnalyticsControl_Label_InCaption.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            TransactionAnalyticsControl_Label_InCaption.ForeColor = Color.FromArgb(22, 163, 74);
            TransactionAnalyticsControl_Label_InCaption.Location = new Point(10, 10);
            TransactionAnalyticsControl_Label_InCaption.Name = "TransactionAnalyticsControl_Label_InCaption";
            TransactionAnalyticsControl_Label_InCaption.Size = new Size(54, 19);
            TransactionAnalyticsControl_Label_InCaption.TabIndex = 0;
            TransactionAnalyticsControl_Label_InCaption.Text = "📥 IN";
            // 
            // TransactionAnalyticsControl_Label_InValue
            // 
            TransactionAnalyticsControl_Label_InValue.AutoSize = true;
            TransactionAnalyticsControl_Label_InValue.Dock = DockStyle.Bottom;
            TransactionAnalyticsControl_Label_InValue.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            TransactionAnalyticsControl_Label_InValue.ForeColor = Color.FromArgb(22, 163, 74);
            TransactionAnalyticsControl_Label_InValue.Location = new Point(10, 42);
            TransactionAnalyticsControl_Label_InValue.Name = "TransactionAnalyticsControl_Label_InValue";
            TransactionAnalyticsControl_Label_InValue.Size = new Size(28, 32);
            TransactionAnalyticsControl_Label_InValue.TabIndex = 1;
            TransactionAnalyticsControl_Label_InValue.Text = "—";
            // 
            // TransactionAnalyticsControl_Label_InPercentage
            // 
            TransactionAnalyticsControl_Label_InPercentage.AutoSize = true;
            TransactionAnalyticsControl_Label_InPercentage.Font = new Font("Segoe UI", 9F);
            TransactionAnalyticsControl_Label_InPercentage.ForeColor = Color.FromArgb(22, 163, 74);
            TransactionAnalyticsControl_Label_InPercentage.Location = new Point(45, 48);
            TransactionAnalyticsControl_Label_InPercentage.Name = "TransactionAnalyticsControl_Label_InPercentage";
            TransactionAnalyticsControl_Label_InPercentage.Size = new Size(0, 15);
            TransactionAnalyticsControl_Label_InPercentage.TabIndex = 2;
            // 
            // TransactionAnalyticsControl_Panel_Out
            // 
            TransactionAnalyticsControl_Panel_Out.AutoSize = true;
            TransactionAnalyticsControl_Panel_Out.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionAnalyticsControl_Panel_Out.BackColor = Color.FromArgb(254, 226, 226);
            TransactionAnalyticsControl_Panel_Out.BorderStyle = BorderStyle.FixedSingle;
            TransactionAnalyticsControl_Panel_Out.Controls.Add(TransactionAnalyticsControl_Label_OutCaption);
            TransactionAnalyticsControl_Panel_Out.Controls.Add(TransactionAnalyticsControl_Label_OutValue);
            TransactionAnalyticsControl_Panel_Out.Controls.Add(TransactionAnalyticsControl_Label_OutPercentage);
            TransactionAnalyticsControl_Panel_Out.Dock = DockStyle.Fill;
            TransactionAnalyticsControl_Panel_Out.Location = new Point(390, 8);
            TransactionAnalyticsControl_Panel_Out.Margin = new Padding(3);
            TransactionAnalyticsControl_Panel_Out.MinimumSize = new Size(150, 80);
            TransactionAnalyticsControl_Panel_Out.Name = "TransactionAnalyticsControl_Panel_Out";
            TransactionAnalyticsControl_Panel_Out.Padding = new Padding(10);
            TransactionAnalyticsControl_Panel_Out.Size = new Size(185, 84);
            TransactionAnalyticsControl_Panel_Out.TabIndex = 2;
            // 
            // TransactionAnalyticsControl_Label_OutCaption
            // 
            TransactionAnalyticsControl_Label_OutCaption.AutoSize = true;
            TransactionAnalyticsControl_Label_OutCaption.Dock = DockStyle.Top;
            TransactionAnalyticsControl_Label_OutCaption.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            TransactionAnalyticsControl_Label_OutCaption.ForeColor = Color.FromArgb(220, 38, 38);
            TransactionAnalyticsControl_Label_OutCaption.Location = new Point(10, 10);
            TransactionAnalyticsControl_Label_OutCaption.Name = "TransactionAnalyticsControl_Label_OutCaption";
            TransactionAnalyticsControl_Label_OutCaption.Size = new Size(72, 19);
            TransactionAnalyticsControl_Label_OutCaption.TabIndex = 0;
            TransactionAnalyticsControl_Label_OutCaption.Text = "📤 OUT";
            // 
            // TransactionAnalyticsControl_Label_OutValue
            // 
            TransactionAnalyticsControl_Label_OutValue.AutoSize = true;
            TransactionAnalyticsControl_Label_OutValue.Dock = DockStyle.Bottom;
            TransactionAnalyticsControl_Label_OutValue.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            TransactionAnalyticsControl_Label_OutValue.ForeColor = Color.FromArgb(220, 38, 38);
            TransactionAnalyticsControl_Label_OutValue.Location = new Point(10, 42);
            TransactionAnalyticsControl_Label_OutValue.Name = "TransactionAnalyticsControl_Label_OutValue";
            TransactionAnalyticsControl_Label_OutValue.Size = new Size(28, 32);
            TransactionAnalyticsControl_Label_OutValue.TabIndex = 1;
            TransactionAnalyticsControl_Label_OutValue.Text = "—";
            // 
            // TransactionAnalyticsControl_Label_OutPercentage
            // 
            TransactionAnalyticsControl_Label_OutPercentage.AutoSize = true;
            TransactionAnalyticsControl_Label_OutPercentage.Font = new Font("Segoe UI", 9F);
            TransactionAnalyticsControl_Label_OutPercentage.ForeColor = Color.FromArgb(220, 38, 38);
            TransactionAnalyticsControl_Label_OutPercentage.Location = new Point(45, 48);
            TransactionAnalyticsControl_Label_OutPercentage.Name = "TransactionAnalyticsControl_Label_OutPercentage";
            TransactionAnalyticsControl_Label_OutPercentage.Size = new Size(0, 15);
            TransactionAnalyticsControl_Label_OutPercentage.TabIndex = 2;
            // 
            // TransactionAnalyticsControl_Panel_Transfer
            // 
            TransactionAnalyticsControl_Panel_Transfer.AutoSize = true;
            TransactionAnalyticsControl_Panel_Transfer.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionAnalyticsControl_Panel_Transfer.BackColor = Color.FromArgb(219, 234, 254);
            TransactionAnalyticsControl_Panel_Transfer.BorderStyle = BorderStyle.FixedSingle;
            TransactionAnalyticsControl_Panel_Transfer.Controls.Add(TransactionAnalyticsControl_Label_TransferCaption);
            TransactionAnalyticsControl_Panel_Transfer.Controls.Add(TransactionAnalyticsControl_Label_TransferValue);
            TransactionAnalyticsControl_Panel_Transfer.Controls.Add(TransactionAnalyticsControl_Label_TransferPercentage);
            TransactionAnalyticsControl_Panel_Transfer.Dock = DockStyle.Fill;
            TransactionAnalyticsControl_Panel_Transfer.Location = new Point(581, 8);
            TransactionAnalyticsControl_Panel_Transfer.Margin = new Padding(3);
            TransactionAnalyticsControl_Panel_Transfer.MinimumSize = new Size(150, 80);
            TransactionAnalyticsControl_Panel_Transfer.Name = "TransactionAnalyticsControl_Panel_Transfer";
            TransactionAnalyticsControl_Panel_Transfer.Padding = new Padding(10);
            TransactionAnalyticsControl_Panel_Transfer.Size = new Size(211, 84);
            TransactionAnalyticsControl_Panel_Transfer.TabIndex = 3;
            // 
            // TransactionAnalyticsControl_Label_TransferCaption
            // 
            TransactionAnalyticsControl_Label_TransferCaption.AutoSize = true;
            TransactionAnalyticsControl_Label_TransferCaption.Dock = DockStyle.Top;
            TransactionAnalyticsControl_Label_TransferCaption.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            TransactionAnalyticsControl_Label_TransferCaption.ForeColor = Color.FromArgb(37, 99, 235);
            TransactionAnalyticsControl_Label_TransferCaption.Location = new Point(10, 10);
            TransactionAnalyticsControl_Label_TransferCaption.Name = "TransactionAnalyticsControl_Label_TransferCaption";
            TransactionAnalyticsControl_Label_TransferCaption.Size = new Size(127, 19);
            TransactionAnalyticsControl_Label_TransferCaption.TabIndex = 0;
            TransactionAnalyticsControl_Label_TransferCaption.Text = "🔄 TRANSFER";
            // 
            // TransactionAnalyticsControl_Label_TransferValue
            // 
            TransactionAnalyticsControl_Label_TransferValue.AutoSize = true;
            TransactionAnalyticsControl_Label_TransferValue.Dock = DockStyle.Bottom;
            TransactionAnalyticsControl_Label_TransferValue.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            TransactionAnalyticsControl_Label_TransferValue.ForeColor = Color.FromArgb(37, 99, 235);
            TransactionAnalyticsControl_Label_TransferValue.Location = new Point(10, 42);
            TransactionAnalyticsControl_Label_TransferValue.Name = "TransactionAnalyticsControl_Label_TransferValue";
            TransactionAnalyticsControl_Label_TransferValue.Size = new Size(28, 32);
            TransactionAnalyticsControl_Label_TransferValue.TabIndex = 1;
            TransactionAnalyticsControl_Label_TransferValue.Text = "—";
            // 
            // TransactionAnalyticsControl_Label_TransferPercentage
            // 
            TransactionAnalyticsControl_Label_TransferPercentage.AutoSize = true;
            TransactionAnalyticsControl_Label_TransferPercentage.Font = new Font("Segoe UI", 9F);
            TransactionAnalyticsControl_Label_TransferPercentage.ForeColor = Color.FromArgb(37, 99, 235);
            TransactionAnalyticsControl_Label_TransferPercentage.Location = new Point(45, 48);
            TransactionAnalyticsControl_Label_TransferPercentage.Name = "TransactionAnalyticsControl_Label_TransferPercentage";
            TransactionAnalyticsControl_Label_TransferPercentage.Size = new Size(0, 15);
            TransactionAnalyticsControl_Label_TransferPercentage.TabIndex = 2;
            // 
            // TransactionAnalyticsControl
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(TransactionAnalyticsControl_TableLayout_Main);
            MinimumSize = new Size(600, 100);
            Name = "TransactionAnalyticsControl";
            Size = new Size(800, 100);
            TransactionAnalyticsControl_TableLayout_Main.ResumeLayout(false);
            TransactionAnalyticsControl_TableLayout_Main.PerformLayout();
            TransactionAnalyticsControl_Panel_Total.ResumeLayout(false);
            TransactionAnalyticsControl_Panel_Total.PerformLayout();
            TransactionAnalyticsControl_Panel_In.ResumeLayout(false);
            TransactionAnalyticsControl_Panel_In.PerformLayout();
            TransactionAnalyticsControl_Panel_Out.ResumeLayout(false);
            TransactionAnalyticsControl_Panel_Out.PerformLayout();
            TransactionAnalyticsControl_Panel_Transfer.ResumeLayout(false);
            TransactionAnalyticsControl_Panel_Transfer.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel TransactionAnalyticsControl_TableLayout_Main;
        private Panel TransactionAnalyticsControl_Panel_Total;
        private Label TransactionAnalyticsControl_Label_TotalCaption;
        private Label TransactionAnalyticsControl_Label_TotalValue;
        private Panel TransactionAnalyticsControl_Panel_In;
        private Label TransactionAnalyticsControl_Label_InCaption;
        private Label TransactionAnalyticsControl_Label_InValue;
        private Label TransactionAnalyticsControl_Label_InPercentage;
        private Panel TransactionAnalyticsControl_Panel_Out;
        private Label TransactionAnalyticsControl_Label_OutCaption;
        private Label TransactionAnalyticsControl_Label_OutValue;
        private Label TransactionAnalyticsControl_Label_OutPercentage;
        private Panel TransactionAnalyticsControl_Panel_Transfer;
        private Label TransactionAnalyticsControl_Label_TransferCaption;
        private Label TransactionAnalyticsControl_Label_TransferValue;
        private Label TransactionAnalyticsControl_Label_TransferPercentage;
    }
}
