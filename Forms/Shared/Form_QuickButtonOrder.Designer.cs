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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_QuickButtonOrder));
            Form_QuickButtonOrder_ListView_Main = new ListView();
            Form_QuickButtonOrder_ColumnHeader_Position = new ColumnHeader();
            Form_QuickButtonOrder_ColumnHeader_PartId = new ColumnHeader();
            Form_QuickButtonOrder_ColumnHeader_Operation = new ColumnHeader();
            Form_QuickButtonOrder_ColumnHeader_Quantity = new ColumnHeader();
            Form_QuickButtonOrder_Label_Instructions = new Label();
            Form_QuickButtonOrder_Button_OK = new Button();
            Form_QuickButtonOrder_Button_Cancel = new Button();
            SuspendLayout();
            // 
            // Form_QuickButtonOrder_ListView_Main
            // 
            Form_QuickButtonOrder_ListView_Main.AllowDrop = true;
            Form_QuickButtonOrder_ListView_Main.Columns.AddRange(new ColumnHeader[] { Form_QuickButtonOrder_ColumnHeader_Position, Form_QuickButtonOrder_ColumnHeader_PartId, Form_QuickButtonOrder_ColumnHeader_Operation, Form_QuickButtonOrder_ColumnHeader_Quantity });
            Form_QuickButtonOrder_ListView_Main.Dock = DockStyle.Top;
            Form_QuickButtonOrder_ListView_Main.FullRowSelect = true;
            Form_QuickButtonOrder_ListView_Main.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            Form_QuickButtonOrder_ListView_Main.Location = new Point(0, 0);
            Form_QuickButtonOrder_ListView_Main.Name = "Form_QuickButtonOrder_ListView_Main";
            Form_QuickButtonOrder_ListView_Main.Size = new Size(484, 250);
            Form_QuickButtonOrder_ListView_Main.TabIndex = 0;
            Form_QuickButtonOrder_ListView_Main.UseCompatibleStateImageBehavior = false;
            Form_QuickButtonOrder_ListView_Main.View = View.Details;
            // 
            // Form_QuickButtonOrder_ColumnHeader_Position
            // 
            Form_QuickButtonOrder_ColumnHeader_Position.Text = "Position";
            Form_QuickButtonOrder_ColumnHeader_Position.TextAlign = HorizontalAlignment.Center;
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
            Form_QuickButtonOrder_ColumnHeader_Quantity.TextAlign = HorizontalAlignment.Right;
            Form_QuickButtonOrder_ColumnHeader_Quantity.Width = 80;
            // 
            // Form_QuickButtonOrder_Label_Instructions
            // 
            Form_QuickButtonOrder_Label_Instructions.Dock = DockStyle.Top;
            Form_QuickButtonOrder_Label_Instructions.Location = new Point(0, 250);
            Form_QuickButtonOrder_Label_Instructions.Name = "Form_QuickButtonOrder_Label_Instructions";
            Form_QuickButtonOrder_Label_Instructions.Padding = new Padding(8);
            Form_QuickButtonOrder_Label_Instructions.Size = new Size(484, 90);
            Form_QuickButtonOrder_Label_Instructions.TabIndex = 1;
            Form_QuickButtonOrder_Label_Instructions.Text = "How to use this form:\r\n\r\n- Drag and drop rows to change the order.\r\n- Use Shift+Up/Down to move a selected row.\r\n- Click OK to save your changes.";
            Form_QuickButtonOrder_Label_Instructions.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Form_QuickButtonOrder_Button_OK
            // 
            Form_QuickButtonOrder_Button_OK.DialogResult = DialogResult.OK;
            Form_QuickButtonOrder_Button_OK.Dock = DockStyle.Bottom;
            Form_QuickButtonOrder_Button_OK.Location = new Point(0, 418);
            Form_QuickButtonOrder_Button_OK.Name = "Form_QuickButtonOrder_Button_OK";
            Form_QuickButtonOrder_Button_OK.Size = new Size(484, 23);
            Form_QuickButtonOrder_Button_OK.TabIndex = 2;
            Form_QuickButtonOrder_Button_OK.Text = "OK";
            Form_QuickButtonOrder_Button_OK.UseVisualStyleBackColor = true;
            // 
            // Form_QuickButtonOrder_Button_Cancel
            // 
            Form_QuickButtonOrder_Button_Cancel.DialogResult = DialogResult.Cancel;
            Form_QuickButtonOrder_Button_Cancel.Dock = DockStyle.Bottom;
            Form_QuickButtonOrder_Button_Cancel.Location = new Point(0, 441);
            Form_QuickButtonOrder_Button_Cancel.Name = "Form_QuickButtonOrder_Button_Cancel";
            Form_QuickButtonOrder_Button_Cancel.Size = new Size(484, 23);
            Form_QuickButtonOrder_Button_Cancel.TabIndex = 3;
            Form_QuickButtonOrder_Button_Cancel.Text = "Cancel";
            Form_QuickButtonOrder_Button_Cancel.UseVisualStyleBackColor = true;
            // 
            // Form_QuickButtonOrder
            // 
            AcceptButton = Form_QuickButtonOrder_Button_OK;
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            CancelButton = Form_QuickButtonOrder_Button_Cancel;
            ClientSize = new Size(484, 464);
            Controls.Add(Form_QuickButtonOrder_Label_Instructions);
            Controls.Add(Form_QuickButtonOrder_ListView_Main);
            Controls.Add(Form_QuickButtonOrder_Button_OK);
            Controls.Add(Form_QuickButtonOrder_Button_Cancel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form_QuickButtonOrder";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
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
