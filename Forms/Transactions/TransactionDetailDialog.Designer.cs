namespace MTM_WIP_Application_Winforms.Forms.Transactions
{
    partial class TransactionDetailPanel
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
            TransactionDetailPanel_Panel_Main = new Panel();
            TransactionDetailPanel_Panel_Actions = new Panel();
            TransactionDetailPanel_Button_Close = new Button();
            TransactionDetailPanel_Panel_Main.SuspendLayout();
            TransactionDetailPanel_Panel_Actions.SuspendLayout();
            SuspendLayout();
            // 
            // TransactionDetailPanel_Panel_Main
            // 
            TransactionDetailPanel_Panel_Main.AutoSize = true;
            TransactionDetailPanel_Panel_Main.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionDetailPanel_Panel_Main.Controls.Add(TransactionDetailPanel_Panel_Actions);
            TransactionDetailPanel_Panel_Main.Dock = DockStyle.Fill;
            TransactionDetailPanel_Panel_Main.Location = new Point(0, 0);
            TransactionDetailPanel_Panel_Main.Margin = new Padding(0);
            TransactionDetailPanel_Panel_Main.Name = "TransactionDetailPanel_Panel_Main";
            TransactionDetailPanel_Panel_Main.Padding = new Padding(10);
            TransactionDetailPanel_Panel_Main.Size = new Size(484, 61);
            TransactionDetailPanel_Panel_Main.TabIndex = 0;
            // 
            // TransactionDetailPanel_Panel_Actions
            // 
            TransactionDetailPanel_Panel_Actions.AutoSize = true;
            TransactionDetailPanel_Panel_Actions.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionDetailPanel_Panel_Actions.Controls.Add(TransactionDetailPanel_Button_Close);
            TransactionDetailPanel_Panel_Actions.Dock = DockStyle.Bottom;
            TransactionDetailPanel_Panel_Actions.Location = new Point(10, 10);
            TransactionDetailPanel_Panel_Actions.Name = "TransactionDetailPanel_Panel_Actions";
            TransactionDetailPanel_Panel_Actions.Padding = new Padding(0, 5, 0, 0);
            TransactionDetailPanel_Panel_Actions.Size = new Size(464, 41);
            TransactionDetailPanel_Panel_Actions.TabIndex = 0;
            // 
            // TransactionDetailPanel_Button_Close
            // 
            TransactionDetailPanel_Button_Close.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            TransactionDetailPanel_Button_Close.Location = new Point(364, 8);
            TransactionDetailPanel_Button_Close.MinimumSize = new Size(75, 23);
            TransactionDetailPanel_Button_Close.Name = "TransactionDetailPanel_Button_Close";
            TransactionDetailPanel_Button_Close.Size = new Size(100, 30);
            TransactionDetailPanel_Button_Close.TabIndex = 0;
            TransactionDetailPanel_Button_Close.Text = "Close";
            TransactionDetailPanel_Button_Close.UseVisualStyleBackColor = true;
            TransactionDetailPanel_Button_Close.Click += BtnClose_Click;
            // 
            // TransactionDetailPanel
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(484, 361);
            Controls.Add(TransactionDetailPanel_Panel_Main);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(500, 400);
            Name = "TransactionDetailPanel";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Transaction Details";
            TransactionDetailPanel_Panel_Main.ResumeLayout(false);
            TransactionDetailPanel_Panel_Main.PerformLayout();
            TransactionDetailPanel_Panel_Actions.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel TransactionDetailPanel_Panel_Main;
        private Panel TransactionDetailPanel_Panel_Actions;
        private Button TransactionDetailPanel_Button_Close;
    }
}
