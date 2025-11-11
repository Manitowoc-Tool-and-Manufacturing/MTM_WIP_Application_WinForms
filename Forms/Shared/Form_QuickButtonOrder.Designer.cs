namespace MTM_WIP_Application_Winforms.Forms.Shared
{
    partial class Form_QuickButtonOrder
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
            Form_QuickButtonOrder_ListView_Main = new System.Windows.Forms.ListView();
            Form_QuickButtonOrder_ColumnHeader_Position = new System.Windows.Forms.ColumnHeader();
            Form_QuickButtonOrder_ColumnHeader_PartId = new System.Windows.Forms.ColumnHeader();
            Form_QuickButtonOrder_ColumnHeader_Operation = new System.Windows.Forms.ColumnHeader();
            Form_QuickButtonOrder_ColumnHeader_Quantity = new System.Windows.Forms.ColumnHeader();
            Form_QuickButtonOrder_Label_Instructions = new System.Windows.Forms.Label();
            Form_QuickButtonOrder_Button_OK = new System.Windows.Forms.Button();
            Form_QuickButtonOrder_Button_Cancel = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // Form_QuickButtonOrder_ListView_Main
            // 
            Form_QuickButtonOrder_ListView_Main.AllowDrop = true;
            Form_QuickButtonOrder_ListView_Main.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { Form_QuickButtonOrder_ColumnHeader_Position, Form_QuickButtonOrder_ColumnHeader_PartId, Form_QuickButtonOrder_ColumnHeader_Operation, Form_QuickButtonOrder_ColumnHeader_Quantity });
            Form_QuickButtonOrder_ListView_Main.Dock = System.Windows.Forms.DockStyle.Top;
            Form_QuickButtonOrder_ListView_Main.FullRowSelect = true;
            Form_QuickButtonOrder_ListView_Main.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            Form_QuickButtonOrder_ListView_Main.Location = new System.Drawing.Point(0, 0);
            Form_QuickButtonOrder_ListView_Main.Name = "Form_QuickButtonOrder_ListView_Main";
            Form_QuickButtonOrder_ListView_Main.Size = new System.Drawing.Size(484, 250);
            Form_QuickButtonOrder_ListView_Main.TabIndex = 0;
            Form_QuickButtonOrder_ListView_Main.UseCompatibleStateImageBehavior = false;
            Form_QuickButtonOrder_ListView_Main.View = System.Windows.Forms.View.Details;
            // 
            // Form_QuickButtonOrder_ColumnHeader_Position
            // 
            Form_QuickButtonOrder_ColumnHeader_Position.Text = "Position";
            Form_QuickButtonOrder_ColumnHeader_Position.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            Form_QuickButtonOrder_ColumnHeader_Position.Width = 70;
            // 
            // Form_QuickButtonOrder_ColumnHeader_PartId
            // 
            Form_QuickButtonOrder_ColumnHeader_PartId.Text = "Part ID";
            Form_QuickButtonOrder_ColumnHeader_PartId.Width = 120;
            // 
            // Form_QuickButtonOrder_ColumnHeader_Operation
            // 
            Form_QuickButtonOrder_ColumnHeader_Operation.Text = "Operation";
            Form_QuickButtonOrder_ColumnHeader_Operation.Width = 120;
            // 
            // Form_QuickButtonOrder_ColumnHeader_Quantity
            // 
            Form_QuickButtonOrder_ColumnHeader_Quantity.Text = "Quantity";
            Form_QuickButtonOrder_ColumnHeader_Quantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            Form_QuickButtonOrder_ColumnHeader_Quantity.Width = 80;
            // 
            // Form_QuickButtonOrder_Label_Instructions
            // 
            Form_QuickButtonOrder_Label_Instructions.Dock = System.Windows.Forms.DockStyle.Top;
            Form_QuickButtonOrder_Label_Instructions.Location = new System.Drawing.Point(0, 250);
            Form_QuickButtonOrder_Label_Instructions.Name = "Form_QuickButtonOrder_Label_Instructions";
            Form_QuickButtonOrder_Label_Instructions.Padding = new System.Windows.Forms.Padding(8);
            Form_QuickButtonOrder_Label_Instructions.Size = new System.Drawing.Size(484, 90);
            Form_QuickButtonOrder_Label_Instructions.TabIndex = 1;
            Form_QuickButtonOrder_Label_Instructions.Text = "How to use this form:\r\n\r\n- Drag and drop rows to change the order.\r\n- Use Shift+Up/Down to move a selected row.\r\n- Click OK to save your changes.";
            Form_QuickButtonOrder_Label_Instructions.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Form_QuickButtonOrder_Button_OK
            // 
            Form_QuickButtonOrder_Button_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            Form_QuickButtonOrder_Button_OK.Dock = System.Windows.Forms.DockStyle.Bottom;
            Form_QuickButtonOrder_Button_OK.Location = new System.Drawing.Point(0, 418);
            Form_QuickButtonOrder_Button_OK.Name = "Form_QuickButtonOrder_Button_OK";
            Form_QuickButtonOrder_Button_OK.Size = new System.Drawing.Size(484, 23);
            Form_QuickButtonOrder_Button_OK.TabIndex = 2;
            Form_QuickButtonOrder_Button_OK.Text = "OK";
            Form_QuickButtonOrder_Button_OK.UseVisualStyleBackColor = true;
            // 
            // Form_QuickButtonOrder_Button_Cancel
            // 
            Form_QuickButtonOrder_Button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Form_QuickButtonOrder_Button_Cancel.Dock = System.Windows.Forms.DockStyle.Bottom;
            Form_QuickButtonOrder_Button_Cancel.Location = new System.Drawing.Point(0, 441);
            Form_QuickButtonOrder_Button_Cancel.Name = "Form_QuickButtonOrder_Button_Cancel";
            Form_QuickButtonOrder_Button_Cancel.Size = new System.Drawing.Size(484, 23);
            Form_QuickButtonOrder_Button_Cancel.TabIndex = 3;
            Form_QuickButtonOrder_Button_Cancel.Text = "Cancel";
            Form_QuickButtonOrder_Button_Cancel.UseVisualStyleBackColor = true;
            // 
            // Form_QuickButtonOrder
            // 
            AcceptButton = Form_QuickButtonOrder_Button_OK;
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            CancelButton = Form_QuickButtonOrder_Button_Cancel;
            ClientSize = new System.Drawing.Size(484, 464);
            Controls.Add(Form_QuickButtonOrder_Label_Instructions);
            Controls.Add(Form_QuickButtonOrder_ListView_Main);
            Controls.Add(Form_QuickButtonOrder_Button_OK);
            Controls.Add(Form_QuickButtonOrder_Button_Cancel);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form_QuickButtonOrder";
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Change Quick Button Order";
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.ListView Form_QuickButtonOrder_ListView_Main;
        private System.Windows.Forms.ColumnHeader Form_QuickButtonOrder_ColumnHeader_Position;
        private System.Windows.Forms.ColumnHeader Form_QuickButtonOrder_ColumnHeader_PartId;
        private System.Windows.Forms.ColumnHeader Form_QuickButtonOrder_ColumnHeader_Operation;
        private System.Windows.Forms.ColumnHeader Form_QuickButtonOrder_ColumnHeader_Quantity;
        private System.Windows.Forms.Label Form_QuickButtonOrder_Label_Instructions;
        private System.Windows.Forms.Button Form_QuickButtonOrder_Button_OK;
        private System.Windows.Forms.Button Form_QuickButtonOrder_Button_Cancel;
    }
}
