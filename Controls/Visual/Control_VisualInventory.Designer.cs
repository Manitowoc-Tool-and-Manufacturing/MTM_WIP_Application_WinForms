using MTM_WIP_Application_Winforms.Models.Enums;

namespace MTM_WIP_Application_Winforms.Controls.Visual
{
    partial class Control_VisualInventory
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
            Control_VisualInventory_TableLayoutPanel_Main = new TableLayoutPanel();
            Control_VisualInventory_TableLayoutPanel_Search = new TableLayoutPanel();
            tableLayoutPanel1 = new TableLayoutPanel();
            Control_VisualInventory_SuggestionTextBoxWithLabel_Warehouse = new MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel();
            Control_VisualInventory_SuggestionTextBoxWithLabel_PartNumber = new MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel();
            Control_VisualInventory_SuggestionTextBoxWithLabel_Location = new MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel();
            tableLayoutPanel2 = new TableLayoutPanel();
            Control_VisualInventory_Button_Search = new Button();
            Control_VisualInventory_CheckBox_NonZeroOnly = new CheckBox();
            Control_VisualInventory_Button_Export = new Button();
            panel1 = new Panel();
            Control_VisualInventory_DataGridView_Results = new DataGridView();
            Control_VisualInventory_TableLayoutPanel_Main.SuspendLayout();
            Control_VisualInventory_TableLayoutPanel_Search.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Control_VisualInventory_DataGridView_Results).BeginInit();
            SuspendLayout();
            // 
            // Control_VisualInventory_TableLayoutPanel_Main
            // 
            Control_VisualInventory_TableLayoutPanel_Main.AutoSize = true;
            Control_VisualInventory_TableLayoutPanel_Main.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_VisualInventory_TableLayoutPanel_Main.ColumnCount = 1;
            Control_VisualInventory_TableLayoutPanel_Main.ColumnStyles.Add(new ColumnStyle());
            Control_VisualInventory_TableLayoutPanel_Main.Controls.Add(Control_VisualInventory_TableLayoutPanel_Search, 0, 0);
            Control_VisualInventory_TableLayoutPanel_Main.Controls.Add(panel1, 0, 1);
            Control_VisualInventory_TableLayoutPanel_Main.Dock = DockStyle.Fill;
            Control_VisualInventory_TableLayoutPanel_Main.Location = new Point(0, 0);
            Control_VisualInventory_TableLayoutPanel_Main.Name = "Control_VisualInventory_TableLayoutPanel_Main";
            Control_VisualInventory_TableLayoutPanel_Main.RowCount = 2;
            Control_VisualInventory_TableLayoutPanel_Main.RowStyles.Add(new RowStyle());
            Control_VisualInventory_TableLayoutPanel_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_VisualInventory_TableLayoutPanel_Main.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            Control_VisualInventory_TableLayoutPanel_Main.Size = new Size(782, 399);
            Control_VisualInventory_TableLayoutPanel_Main.TabIndex = 0;
            // 
            // Control_VisualInventory_TableLayoutPanel_Search
            // 
            Control_VisualInventory_TableLayoutPanel_Search.AutoSize = true;
            Control_VisualInventory_TableLayoutPanel_Search.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_VisualInventory_TableLayoutPanel_Search.ColumnCount = 1;
            Control_VisualInventory_TableLayoutPanel_Search.ColumnStyles.Add(new ColumnStyle());
            Control_VisualInventory_TableLayoutPanel_Search.Controls.Add(tableLayoutPanel1, 0, 0);
            Control_VisualInventory_TableLayoutPanel_Search.Controls.Add(tableLayoutPanel2, 0, 1);
            Control_VisualInventory_TableLayoutPanel_Search.Dock = DockStyle.Fill;
            Control_VisualInventory_TableLayoutPanel_Search.Location = new Point(3, 3);
            Control_VisualInventory_TableLayoutPanel_Search.Name = "Control_VisualInventory_TableLayoutPanel_Search";
            Control_VisualInventory_TableLayoutPanel_Search.RowCount = 2;
            Control_VisualInventory_TableLayoutPanel_Search.RowStyles.Add(new RowStyle());
            Control_VisualInventory_TableLayoutPanel_Search.RowStyles.Add(new RowStyle());
            Control_VisualInventory_TableLayoutPanel_Search.Size = new Size(776, 137);
            Control_VisualInventory_TableLayoutPanel_Search.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.Controls.Add(Control_VisualInventory_SuggestionTextBoxWithLabel_Warehouse, 0, 2);
            tableLayoutPanel1.Controls.Add(Control_VisualInventory_SuggestionTextBoxWithLabel_PartNumber, 0, 0);
            tableLayoutPanel1.Controls.Add(Control_VisualInventory_SuggestionTextBoxWithLabel_Location, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(3, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(770, 87);
            tableLayoutPanel1.TabIndex = 5;
            // 
            // Control_VisualInventory_SuggestionTextBoxWithLabel_Warehouse
            // 
            Control_VisualInventory_SuggestionTextBoxWithLabel_Warehouse.AutoSize = true;
            Control_VisualInventory_SuggestionTextBoxWithLabel_Warehouse.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_VisualInventory_SuggestionTextBoxWithLabel_Warehouse.Dock = DockStyle.Fill;
            Control_VisualInventory_SuggestionTextBoxWithLabel_Warehouse.LabelText = "Warehouse";
            Control_VisualInventory_SuggestionTextBoxWithLabel_Warehouse.LabelVisibility = Enum_LabelVisibility.Hidden;
            Control_VisualInventory_SuggestionTextBoxWithLabel_Warehouse.Location = new Point(3, 61);
            Control_VisualInventory_SuggestionTextBoxWithLabel_Warehouse.MaxLength = 90;
            Control_VisualInventory_SuggestionTextBoxWithLabel_Warehouse.MinimumSize = new Size(0, 23);
            Control_VisualInventory_SuggestionTextBoxWithLabel_Warehouse.MinLength = 90;
            Control_VisualInventory_SuggestionTextBoxWithLabel_Warehouse.Name = "Control_VisualInventory_SuggestionTextBoxWithLabel_Warehouse";
            Control_VisualInventory_SuggestionTextBoxWithLabel_Warehouse.PlaceholderText = "Enter Warehouse (002)";
            Control_VisualInventory_SuggestionTextBoxWithLabel_Warehouse.ShowValidationColor = false;
            Control_VisualInventory_SuggestionTextBoxWithLabel_Warehouse.Size = new Size(764, 23);
            Control_VisualInventory_SuggestionTextBoxWithLabel_Warehouse.TabIndex = 1;
            Control_VisualInventory_SuggestionTextBoxWithLabel_Warehouse.ValidatorType = null;
            Control_VisualInventory_SuggestionTextBoxWithLabel_Warehouse.Visible = false;
            // 
            // Control_VisualInventory_SuggestionTextBoxWithLabel_PartNumber
            // 
            Control_VisualInventory_SuggestionTextBoxWithLabel_PartNumber.AutoSize = true;
            Control_VisualInventory_SuggestionTextBoxWithLabel_PartNumber.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_VisualInventory_SuggestionTextBoxWithLabel_PartNumber.Dock = DockStyle.Fill;
            Control_VisualInventory_SuggestionTextBoxWithLabel_PartNumber.LabelText = "Part Number";
            Control_VisualInventory_SuggestionTextBoxWithLabel_PartNumber.LabelVisibility = Enum_LabelVisibility.Hidden;
            Control_VisualInventory_SuggestionTextBoxWithLabel_PartNumber.Location = new Point(3, 3);
            Control_VisualInventory_SuggestionTextBoxWithLabel_PartNumber.MaxLength = 90;
            Control_VisualInventory_SuggestionTextBoxWithLabel_PartNumber.MinimumSize = new Size(0, 23);
            Control_VisualInventory_SuggestionTextBoxWithLabel_PartNumber.MinLength = 90;
            Control_VisualInventory_SuggestionTextBoxWithLabel_PartNumber.Name = "Control_VisualInventory_SuggestionTextBoxWithLabel_PartNumber";
            Control_VisualInventory_SuggestionTextBoxWithLabel_PartNumber.PlaceholderText = "Enter Part Number";
            Control_VisualInventory_SuggestionTextBoxWithLabel_PartNumber.ShowValidationColor = false;
            Control_VisualInventory_SuggestionTextBoxWithLabel_PartNumber.Size = new Size(764, 23);
            Control_VisualInventory_SuggestionTextBoxWithLabel_PartNumber.TabIndex = 0;
            Control_VisualInventory_SuggestionTextBoxWithLabel_PartNumber.ValidatorType = null;
            // 
            // Control_VisualInventory_SuggestionTextBoxWithLabel_Location
            // 
            Control_VisualInventory_SuggestionTextBoxWithLabel_Location.AutoSize = true;
            Control_VisualInventory_SuggestionTextBoxWithLabel_Location.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_VisualInventory_SuggestionTextBoxWithLabel_Location.Dock = DockStyle.Fill;
            Control_VisualInventory_SuggestionTextBoxWithLabel_Location.LabelText = "Location";
            Control_VisualInventory_SuggestionTextBoxWithLabel_Location.LabelVisibility = Enum_LabelVisibility.Hidden;
            Control_VisualInventory_SuggestionTextBoxWithLabel_Location.Location = new Point(3, 32);
            Control_VisualInventory_SuggestionTextBoxWithLabel_Location.MaxLength = 90;
            Control_VisualInventory_SuggestionTextBoxWithLabel_Location.MinimumSize = new Size(0, 23);
            Control_VisualInventory_SuggestionTextBoxWithLabel_Location.MinLength = 90;
            Control_VisualInventory_SuggestionTextBoxWithLabel_Location.Name = "Control_VisualInventory_SuggestionTextBoxWithLabel_Location";
            Control_VisualInventory_SuggestionTextBoxWithLabel_Location.PlaceholderText = "Enter Location";
            Control_VisualInventory_SuggestionTextBoxWithLabel_Location.ShowValidationColor = false;
            Control_VisualInventory_SuggestionTextBoxWithLabel_Location.Size = new Size(764, 23);
            Control_VisualInventory_SuggestionTextBoxWithLabel_Location.TabIndex = 2;
            Control_VisualInventory_SuggestionTextBoxWithLabel_Location.ValidatorType = null;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.Controls.Add(Control_VisualInventory_Button_Search, 1, 0);
            tableLayoutPanel2.Controls.Add(Control_VisualInventory_CheckBox_NonZeroOnly, 0, 0);
            tableLayoutPanel2.Controls.Add(Control_VisualInventory_Button_Export, 2, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 96);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.Size = new Size(770, 38);
            tableLayoutPanel2.TabIndex = 6;
            // 
            // Control_VisualInventory_Button_Search
            // 
            Control_VisualInventory_Button_Search.BackColor = Color.Teal;
            Control_VisualInventory_Button_Search.Dock = DockStyle.Fill;
            Control_VisualInventory_Button_Search.FlatStyle = FlatStyle.Flat;
            Control_VisualInventory_Button_Search.ForeColor = Color.White;
            Control_VisualInventory_Button_Search.Location = new Point(592, 3);
            Control_VisualInventory_Button_Search.MaximumSize = new Size(0, 32);
            Control_VisualInventory_Button_Search.MinimumSize = new Size(0, 32);
            Control_VisualInventory_Button_Search.Name = "Control_VisualInventory_Button_Search";
            Control_VisualInventory_Button_Search.Size = new Size(74, 32);
            Control_VisualInventory_Button_Search.TabIndex = 4;
            Control_VisualInventory_Button_Search.Text = "Search";
            Control_VisualInventory_Button_Search.UseVisualStyleBackColor = false;
            // 
            // Control_VisualInventory_CheckBox_NonZeroOnly
            // 
            Control_VisualInventory_CheckBox_NonZeroOnly.Checked = true;
            Control_VisualInventory_CheckBox_NonZeroOnly.CheckState = CheckState.Checked;
            Control_VisualInventory_CheckBox_NonZeroOnly.Dock = DockStyle.Fill;
            Control_VisualInventory_CheckBox_NonZeroOnly.Location = new Point(3, 3);
            Control_VisualInventory_CheckBox_NonZeroOnly.MaximumSize = new Size(0, 32);
            Control_VisualInventory_CheckBox_NonZeroOnly.MinimumSize = new Size(0, 32);
            Control_VisualInventory_CheckBox_NonZeroOnly.Name = "Control_VisualInventory_CheckBox_NonZeroOnly";
            Control_VisualInventory_CheckBox_NonZeroOnly.Size = new Size(583, 32);
            Control_VisualInventory_CheckBox_NonZeroOnly.TabIndex = 3;
            Control_VisualInventory_CheckBox_NonZeroOnly.Text = "Non-Zero";
            Control_VisualInventory_CheckBox_NonZeroOnly.UseVisualStyleBackColor = true;
            // 
            // Control_VisualInventory_Button_Export
            // 
            Control_VisualInventory_Button_Export.BackColor = Color.SteelBlue;
            Control_VisualInventory_Button_Export.Dock = DockStyle.Fill;
            Control_VisualInventory_Button_Export.FlatStyle = FlatStyle.Flat;
            Control_VisualInventory_Button_Export.ForeColor = Color.White;
            Control_VisualInventory_Button_Export.Location = new Point(672, 3);
            Control_VisualInventory_Button_Export.MaximumSize = new Size(0, 32);
            Control_VisualInventory_Button_Export.MinimumSize = new Size(0, 32);
            Control_VisualInventory_Button_Export.Name = "Control_VisualInventory_Button_Export";
            Control_VisualInventory_Button_Export.Size = new Size(95, 32);
            Control_VisualInventory_Button_Export.TabIndex = 2;
            Control_VisualInventory_Button_Export.Text = "Export to Excel";
            Control_VisualInventory_Button_Export.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            panel1.Controls.Add(Control_VisualInventory_DataGridView_Results);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 146);
            panel1.Name = "panel1";
            panel1.Size = new Size(776, 250);
            panel1.TabIndex = 1;
            // 
            // Control_VisualInventory_DataGridView_Results
            // 
            Control_VisualInventory_DataGridView_Results.AllowUserToAddRows = false;
            Control_VisualInventory_DataGridView_Results.AllowUserToDeleteRows = false;
            Control_VisualInventory_DataGridView_Results.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Control_VisualInventory_DataGridView_Results.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            Control_VisualInventory_DataGridView_Results.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Control_VisualInventory_DataGridView_Results.Dock = DockStyle.Fill;
            Control_VisualInventory_DataGridView_Results.Location = new Point(0, 0);
            Control_VisualInventory_DataGridView_Results.Name = "Control_VisualInventory_DataGridView_Results";
            Control_VisualInventory_DataGridView_Results.ReadOnly = true;
            Control_VisualInventory_DataGridView_Results.Size = new Size(776, 250);
            Control_VisualInventory_DataGridView_Results.TabIndex = 2;
            // 
            // Control_VisualInventory
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(Control_VisualInventory_TableLayoutPanel_Main);
            Name = "Control_VisualInventory";
            Size = new Size(782, 399);
            Control_VisualInventory_TableLayoutPanel_Main.ResumeLayout(false);
            Control_VisualInventory_TableLayoutPanel_Main.PerformLayout();
            Control_VisualInventory_TableLayoutPanel_Search.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)Control_VisualInventory_DataGridView_Results).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel Control_VisualInventory_TableLayoutPanel_Main;
        private TableLayoutPanel Control_VisualInventory_TableLayoutPanel_Search;
        private Shared.SuggestionTextBoxWithLabel Control_VisualInventory_SuggestionTextBoxWithLabel_PartNumber;
        private Shared.SuggestionTextBoxWithLabel Control_VisualInventory_SuggestionTextBoxWithLabel_Location;
        private Button Control_VisualInventory_Button_Search;
        private CheckBox Control_VisualInventory_CheckBox_NonZeroOnly;
        private Button Control_VisualInventory_Button_Export;
        private Shared.SuggestionTextBoxWithLabel Control_VisualInventory_SuggestionTextBoxWithLabel_Warehouse;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private Panel panel1;
        private DataGridView Control_VisualInventory_DataGridView_Results;
    }
}