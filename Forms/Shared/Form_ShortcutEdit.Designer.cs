namespace MTM_WIP_Application_Winforms.Forms.Shared
{
    partial class Form_ShortcutEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_ShortcutEdit));
            _instructionLabel = new Label();
            _keyDisplayLabel = new Label();
            _buttonPanel = new Panel();
            tableLayoutPanel2 = new TableLayoutPanel();
            _cancelButton = new Button();
            _clearButton = new Button();
            _saveButton = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel3 = new TableLayoutPanel();
            _buttonPanel.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            SuspendLayout();
            // 
            // _instructionLabel
            // 
            _instructionLabel.AutoSize = true;
            _instructionLabel.Dock = DockStyle.Fill;
            _instructionLabel.Font = new Font("Segoe UI Emoji", 10F);
            _instructionLabel.Location = new Point(3, 3);
            _instructionLabel.Margin = new Padding(3);
            _instructionLabel.Name = "_instructionLabel";
            _instructionLabel.Size = new Size(208, 19);
            _instructionLabel.TabIndex = 0;
            _instructionLabel.Text = "Press the new key combination...";
            _instructionLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // _keyDisplayLabel
            // 
            _keyDisplayLabel.AutoSize = true;
            _keyDisplayLabel.BackColor = Color.WhiteSmoke;
            _keyDisplayLabel.Dock = DockStyle.Fill;
            _keyDisplayLabel.Font = new Font("Segoe UI Emoji", 16F, FontStyle.Bold);
            _keyDisplayLabel.ForeColor = Color.FromArgb(0, 120, 212);
            _keyDisplayLabel.Location = new Point(3, 28);
            _keyDisplayLabel.Margin = new Padding(3);
            _keyDisplayLabel.Name = "_keyDisplayLabel";
            _keyDisplayLabel.Size = new Size(208, 30);
            _keyDisplayLabel.TabIndex = 1;
            _keyDisplayLabel.Text = "None";
            _keyDisplayLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // _buttonPanel
            // 
            _buttonPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.SetColumnSpan(_buttonPanel, 3);
            _buttonPanel.Controls.Add(tableLayoutPanel2);
            _buttonPanel.Dock = DockStyle.Fill;
            _buttonPanel.Location = new Point(3, 108);
            _buttonPanel.Name = "_buttonPanel";
            _buttonPanel.Padding = new Padding(10);
            _buttonPanel.Size = new Size(338, 60);
            _buttonPanel.TabIndex = 2;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.AutoSize = true;
            tableLayoutPanel2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel2.ColumnCount = 4;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.Controls.Add(_cancelButton, 3, 0);
            tableLayoutPanel2.Controls.Add(_clearButton, 0, 0);
            tableLayoutPanel2.Controls.Add(_saveButton, 2, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(10, 10);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(318, 40);
            tableLayoutPanel2.TabIndex = 4;
            // 
            // _cancelButton
            // 
            _cancelButton.DialogResult = DialogResult.Cancel;
            _cancelButton.Dock = DockStyle.Fill;
            _cancelButton.Location = new Point(235, 3);
            _cancelButton.Name = "_cancelButton";
            _cancelButton.Size = new Size(80, 34);
            _cancelButton.TabIndex = 1;
            _cancelButton.Text = "Cancel";
            _cancelButton.UseVisualStyleBackColor = true;
            // 
            // _clearButton
            // 
            _clearButton.Dock = DockStyle.Fill;
            _clearButton.Location = new Point(3, 3);
            _clearButton.Name = "_clearButton";
            _clearButton.Size = new Size(80, 34);
            _clearButton.TabIndex = 2;
            _clearButton.Text = "Clear";
            _clearButton.UseVisualStyleBackColor = true;
            // 
            // _saveButton
            // 
            _saveButton.DialogResult = DialogResult.OK;
            _saveButton.Dock = DockStyle.Fill;
            _saveButton.Location = new Point(149, 3);
            _saveButton.Name = "_saveButton";
            _saveButton.Size = new Size(80, 34);
            _saveButton.TabIndex = 0;
            _saveButton.Text = "Save";
            _saveButton.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(_buttonPanel, 0, 3);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 1, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(344, 171);
            tableLayoutPanel1.TabIndex = 3;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.AutoSize = true;
            tableLayoutPanel3.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(_instructionLabel, 0, 0);
            tableLayoutPanel3.Controls.Add(_keyDisplayLabel, 0, 2);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(65, 22);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 3;
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.Size = new Size(214, 61);
            tableLayoutPanel3.TabIndex = 3;
            // 
            // Form_ShortcutEdit
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(344, 171);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form_ShortcutEdit";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Edit Shortcut";
            KeyDown += Form_KeyDown;
            _buttonPanel.ResumeLayout(false);
            _buttonPanel.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _instructionLabel;
        private System.Windows.Forms.Label _keyDisplayLabel;
        private System.Windows.Forms.Panel _buttonPanel;
        private System.Windows.Forms.Button _saveButton;
        private System.Windows.Forms.Button _cancelButton;
        private System.Windows.Forms.Button _clearButton;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel3;
    }
}