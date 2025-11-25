using System.Drawing;
using System.Windows.Forms;
using MTM_WIP_Application_Winforms.Controls.Shared;

namespace MTM_WIP_Application_Winforms.Controls.MainForm
{
    partial class Control_TransferTab
    {
        #region Fields



        private System.ComponentModel.IContainer components = null;

        #endregion
        private GroupBox Control_TransferTab_GroupBox_MainControl;



        #region Methods


        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion
        #region Component Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Control_TransferTab_GroupBox_MainControl = new GroupBox();
            Control_TransferTab_Panel_Main = new TableLayoutPanel();
            Control_TransferTab_TableLayout_Main = new TableLayoutPanel();
            Control_TransferTab_Panel_DataGridView = new Panel();
            Control_TransferTab_Image_NothingFound = new PictureBox();
            Control_TransferTab_DataGridView_Main = new DataGridView();
            Control_TransferTab_ContextMenu_DataGridView = new ContextMenuStrip(components);
            Control_TransferTab_Panel_Inputs = new Panel();
            Control_TransferTab_TableLayout_Inputs = new TableLayoutPanel();
            Control_TransferTab_TextBox_Part = new SuggestionTextBoxWithLabel();
            Control_TransferTab_TextBox_Operation = new SuggestionTextBoxWithLabel();
            Control_TransferTab_TextBox_ToLocation = new SuggestionTextBoxWithLabel();
            Control_TransferTab_Label_Quantity = new Label();
            Control_TransferTab_NumericUpDown_Quantity = new NumericUpDown();
            tableLayoutPanel2 = new TableLayoutPanel();
            Control_TransferTab_TableLayout_SaveSearch = new TableLayoutPanel();
            Control_TransferTab_Button_Transfer = new Button();
            Control_TransferTab_Button_Search = new Button();
            Control_TransferTab_TableLayout_Print = new TableLayoutPanel();
            Control_TransferTab_Button_Print = new Button();
            tableLayoutPanel3 = new TableLayoutPanel();
            Control_TransferTab_Button_Toggle_Split = new Button();
            Control_TransferTab_Button_Reset = new Button();
            Control_TransferTab_Button_Toggle_RightPanel = new Button();
            Control_TransferTab_GroupBox_MainControl.SuspendLayout();
            Control_TransferTab_Panel_Main.SuspendLayout();
            Control_TransferTab_TableLayout_Main.SuspendLayout();
            Control_TransferTab_Panel_DataGridView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Control_TransferTab_Image_NothingFound).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Control_TransferTab_DataGridView_Main).BeginInit();
            Control_TransferTab_ContextMenu_DataGridView.SuspendLayout();
            Control_TransferTab_Panel_Inputs.SuspendLayout();
            Control_TransferTab_TableLayout_Inputs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Control_TransferTab_NumericUpDown_Quantity).BeginInit();
            tableLayoutPanel2.SuspendLayout();
            Control_TransferTab_TableLayout_SaveSearch.SuspendLayout();
            Control_TransferTab_TableLayout_Print.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            SuspendLayout();
            //
            // Control_TransferTab_GroupBox_MainControl
            //
            Control_TransferTab_GroupBox_MainControl.AutoSize = true;
            Control_TransferTab_GroupBox_MainControl.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_TransferTab_GroupBox_MainControl.Controls.Add(Control_TransferTab_Panel_Main);
            Control_TransferTab_GroupBox_MainControl.Dock = DockStyle.Fill;
            Control_TransferTab_GroupBox_MainControl.FlatStyle = FlatStyle.Flat;
            Control_TransferTab_GroupBox_MainControl.Font = new Font("Segoe UI Emoji", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Control_TransferTab_GroupBox_MainControl.Location = new Point(0, 0);
            Control_TransferTab_GroupBox_MainControl.Name = "Control_TransferTab_GroupBox_MainControl";
            Control_TransferTab_GroupBox_MainControl.Size = new Size(836, 375);
            Control_TransferTab_GroupBox_MainControl.TabIndex = 17;
            Control_TransferTab_GroupBox_MainControl.TabStop = false;
            Control_TransferTab_GroupBox_MainControl.Text = "Inventory Transfer";
            //
            // Control_TransferTab_Panel_Main
            //
            Control_TransferTab_Panel_Main.AutoSize = true;
            Control_TransferTab_Panel_Main.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_TransferTab_Panel_Main.ColumnCount = 1;
            Control_TransferTab_Panel_Main.ColumnStyles.Add(new ColumnStyle());
            Control_TransferTab_Panel_Main.Controls.Add(Control_TransferTab_TableLayout_Main, 0, 0);
            Control_TransferTab_Panel_Main.Controls.Add(tableLayoutPanel3, 0, 1);
            Control_TransferTab_Panel_Main.Dock = DockStyle.Fill;
            Control_TransferTab_Panel_Main.Location = new Point(3, 19);
            Control_TransferTab_Panel_Main.Name = "Control_TransferTab_Panel_Main";
            Control_TransferTab_Panel_Main.RowCount = 2;
            Control_TransferTab_Panel_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_TransferTab_Panel_Main.RowStyles.Add(new RowStyle());
            Control_TransferTab_Panel_Main.Size = new Size(830, 353);
            Control_TransferTab_Panel_Main.TabIndex = 0;
            //
            // Control_TransferTab_TableLayout_Main
            //
            Control_TransferTab_TableLayout_Main.AutoSize = true;
            Control_TransferTab_TableLayout_Main.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_TransferTab_TableLayout_Main.ColumnCount = 2;
            Control_TransferTab_TableLayout_Main.ColumnStyles.Add(new ColumnStyle());
            Control_TransferTab_TableLayout_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_TransferTab_TableLayout_Main.Controls.Add(Control_TransferTab_Panel_DataGridView, 1, 0);
            Control_TransferTab_TableLayout_Main.Controls.Add(Control_TransferTab_Panel_Inputs, 0, 0);
            Control_TransferTab_TableLayout_Main.Dock = DockStyle.Fill;
            Control_TransferTab_TableLayout_Main.Location = new Point(3, 3);
            Control_TransferTab_TableLayout_Main.Name = "Control_TransferTab_TableLayout_Main";
            Control_TransferTab_TableLayout_Main.RowCount = 1;
            Control_TransferTab_TableLayout_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_TransferTab_TableLayout_Main.Size = new Size(824, 303);
            Control_TransferTab_TableLayout_Main.TabIndex = 0;
            //
            // Control_TransferTab_Panel_DataGridView
            //
            Control_TransferTab_Panel_DataGridView.AutoSize = true;
            Control_TransferTab_Panel_DataGridView.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_TransferTab_Panel_DataGridView.BorderStyle = BorderStyle.FixedSingle;
            Control_TransferTab_Panel_DataGridView.Controls.Add(Control_TransferTab_Image_NothingFound);
            Control_TransferTab_Panel_DataGridView.Controls.Add(Control_TransferTab_DataGridView_Main);
            Control_TransferTab_Panel_DataGridView.Dock = DockStyle.Fill;
            Control_TransferTab_Panel_DataGridView.Location = new Point(307, 3);
            Control_TransferTab_Panel_DataGridView.Name = "Control_TransferTab_Panel_DataGridView";
            Control_TransferTab_Panel_DataGridView.Padding = new Padding(3);
            Control_TransferTab_Panel_DataGridView.Size = new Size(514, 297);
            Control_TransferTab_Panel_DataGridView.TabIndex = 9;
            Control_TransferTab_Panel_DataGridView.TabStop = true;
            //
            // Control_TransferTab_Image_NothingFound
            //
            Control_TransferTab_Image_NothingFound.BackColor = Color.White;
            Control_TransferTab_Image_NothingFound.BorderStyle = BorderStyle.FixedSingle;
            Control_TransferTab_Image_NothingFound.Dock = DockStyle.Fill;
            Control_TransferTab_Image_NothingFound.ErrorImage = null;
            Control_TransferTab_Image_NothingFound.Image = Properties.Resources.NothingFound;
            Control_TransferTab_Image_NothingFound.InitialImage = null;
            Control_TransferTab_Image_NothingFound.Location = new Point(3, 3);
            Control_TransferTab_Image_NothingFound.Name = "Control_TransferTab_Image_NothingFound";
            Control_TransferTab_Image_NothingFound.Size = new Size(506, 289);
            Control_TransferTab_Image_NothingFound.SizeMode = PictureBoxSizeMode.CenterImage;
            Control_TransferTab_Image_NothingFound.TabIndex = 6;
            Control_TransferTab_Image_NothingFound.TabStop = false;
            Control_TransferTab_Image_NothingFound.Visible = false;
            //
            // Control_TransferTab_DataGridView_Main
            //
            Control_TransferTab_DataGridView_Main.AllowUserToAddRows = false;
            Control_TransferTab_DataGridView_Main.AllowUserToDeleteRows = false;
            Control_TransferTab_DataGridView_Main.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Control_TransferTab_DataGridView_Main.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            Control_TransferTab_DataGridView_Main.BorderStyle = BorderStyle.Fixed3D;
            Control_TransferTab_DataGridView_Main.CellBorderStyle = DataGridViewCellBorderStyle.Sunken;
            Control_TransferTab_DataGridView_Main.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
            Control_TransferTab_DataGridView_Main.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Sunken;
            Control_TransferTab_DataGridView_Main.ColumnHeadersHeight = 34;
            Control_TransferTab_DataGridView_Main.ContextMenuStrip = Control_TransferTab_ContextMenu_DataGridView;
            Control_TransferTab_DataGridView_Main.Dock = DockStyle.Fill;
            Control_TransferTab_DataGridView_Main.EditMode = DataGridViewEditMode.EditProgrammatically;
            Control_TransferTab_DataGridView_Main.Location = new Point(3, 3);
            Control_TransferTab_DataGridView_Main.Name = "Control_TransferTab_DataGridView_Main";
            Control_TransferTab_DataGridView_Main.ReadOnly = true;
            Control_TransferTab_DataGridView_Main.RowHeadersWidth = 62;
            Control_TransferTab_DataGridView_Main.RowTemplate.ReadOnly = true;
            Control_TransferTab_DataGridView_Main.RowTemplate.Resizable = DataGridViewTriState.True;
            Control_TransferTab_DataGridView_Main.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Control_TransferTab_DataGridView_Main.ShowCellErrors = false;
            Control_TransferTab_DataGridView_Main.ShowCellToolTips = false;
            Control_TransferTab_DataGridView_Main.ShowEditingIcon = false;
            Control_TransferTab_DataGridView_Main.ShowRowErrors = false;
            Control_TransferTab_DataGridView_Main.Size = new Size(506, 289);
            Control_TransferTab_DataGridView_Main.StandardTab = true;
            Control_TransferTab_DataGridView_Main.TabIndex = 4;
            //
            // Control_TransferTab_ContextMenu_DataGridView
            //
            Control_TransferTab_ContextMenu_DataGridView.Name = "Control_TransferTab_ContextMenu_DataGridView";
            Control_TransferTab_ContextMenu_DataGridView.Size = new Size(61, 4);
            //
            // Control_TransferTab_Panel_Inputs
            //
            Control_TransferTab_Panel_Inputs.AutoSize = true;
            Control_TransferTab_Panel_Inputs.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_TransferTab_Panel_Inputs.BorderStyle = BorderStyle.FixedSingle;
            Control_TransferTab_Panel_Inputs.Controls.Add(Control_TransferTab_TableLayout_Inputs);
            Control_TransferTab_Panel_Inputs.Dock = DockStyle.Fill;
            Control_TransferTab_Panel_Inputs.Location = new Point(3, 3);
            Control_TransferTab_Panel_Inputs.Name = "Control_TransferTab_Panel_Inputs";
            Control_TransferTab_Panel_Inputs.Size = new Size(298, 297);
            Control_TransferTab_Panel_Inputs.TabIndex = 1001;
            //
            // Control_TransferTab_TableLayout_Inputs
            //
            Control_TransferTab_TableLayout_Inputs.AutoSize = true;
            Control_TransferTab_TableLayout_Inputs.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_TransferTab_TableLayout_Inputs.ColumnCount = 2;
            Control_TransferTab_TableLayout_Inputs.ColumnStyles.Add(new ColumnStyle());
            Control_TransferTab_TableLayout_Inputs.ColumnStyles.Add(new ColumnStyle());
            Control_TransferTab_TableLayout_Inputs.Controls.Add(Control_TransferTab_TextBox_Part, 0, 0);
            Control_TransferTab_TableLayout_Inputs.Controls.Add(Control_TransferTab_TextBox_Operation, 0, 1);
            Control_TransferTab_TableLayout_Inputs.Controls.Add(Control_TransferTab_TextBox_ToLocation, 0, 2);
            Control_TransferTab_TableLayout_Inputs.Controls.Add(Control_TransferTab_Label_Quantity, 0, 3);
            Control_TransferTab_TableLayout_Inputs.Controls.Add(tableLayoutPanel2, 0, 6);
            Control_TransferTab_TableLayout_Inputs.Controls.Add(Control_TransferTab_NumericUpDown_Quantity, 1, 3);
            Control_TransferTab_TableLayout_Inputs.Dock = DockStyle.Fill;
            Control_TransferTab_TableLayout_Inputs.Location = new Point(0, 0);
            Control_TransferTab_TableLayout_Inputs.MaximumSize = new Size(302, 0);
            Control_TransferTab_TableLayout_Inputs.Name = "Control_TransferTab_TableLayout_Inputs";
            Control_TransferTab_TableLayout_Inputs.RowCount = 7;
            Control_TransferTab_TableLayout_Inputs.RowStyles.Add(new RowStyle());
            Control_TransferTab_TableLayout_Inputs.RowStyles.Add(new RowStyle());
            Control_TransferTab_TableLayout_Inputs.RowStyles.Add(new RowStyle());
            Control_TransferTab_TableLayout_Inputs.RowStyles.Add(new RowStyle());
            Control_TransferTab_TableLayout_Inputs.RowStyles.Add(new RowStyle());
            Control_TransferTab_TableLayout_Inputs.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_TransferTab_TableLayout_Inputs.RowStyles.Add(new RowStyle());
            Control_TransferTab_TableLayout_Inputs.Size = new Size(296, 295);
            Control_TransferTab_TableLayout_Inputs.TabIndex = 0;
            //
            // Control_TransferTab_TextBox_Part
            //
            Control_TransferTab_TextBox_Part.AutoSize = true;
            Control_TransferTab_TextBox_Part.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_TransferTab_TableLayout_Inputs.SetColumnSpan(Control_TransferTab_TextBox_Part, 2);
            Control_TransferTab_TextBox_Part.Dock = DockStyle.Fill;
            Control_TransferTab_TextBox_Part.LabelText = "Part Number";
            Control_TransferTab_TextBox_Part.Location = new Point(3, 3);
            Control_TransferTab_TextBox_Part.Name = "Control_TransferTab_TextBox_Part";
            Control_TransferTab_TextBox_Part.PlaceholderText = "Enter Part Number";
            Control_TransferTab_TextBox_Part.Size = new Size(290, 23);
            Control_TransferTab_TextBox_Part.TabIndex = 1;
            //
            // Control_TransferTab_TextBox_Operation
            //
            Control_TransferTab_TextBox_Operation.AutoSize = true;
            Control_TransferTab_TextBox_Operation.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_TransferTab_TableLayout_Inputs.SetColumnSpan(Control_TransferTab_TextBox_Operation, 2);
            Control_TransferTab_TextBox_Operation.Dock = DockStyle.Fill;
            Control_TransferTab_TextBox_Operation.LabelText = "Operation";
            Control_TransferTab_TextBox_Operation.Location = new Point(3, 32);
            Control_TransferTab_TextBox_Operation.Name = "Control_TransferTab_TextBox_Operation";
            Control_TransferTab_TextBox_Operation.PlaceholderText = "Enter Operation";
            Control_TransferTab_TextBox_Operation.Size = new Size(290, 23);
            Control_TransferTab_TextBox_Operation.TabIndex = 2;
            //
            // Control_TransferTab_TextBox_ToLocation
            //
            Control_TransferTab_TextBox_ToLocation.AutoSize = true;
            Control_TransferTab_TextBox_ToLocation.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_TransferTab_TableLayout_Inputs.SetColumnSpan(Control_TransferTab_TextBox_ToLocation, 2);
            Control_TransferTab_TextBox_ToLocation.Dock = DockStyle.Fill;
            Control_TransferTab_TextBox_ToLocation.Enabled = false;
            Control_TransferTab_TextBox_ToLocation.LabelText = "To Location";
            Control_TransferTab_TextBox_ToLocation.Location = new Point(3, 61);
            Control_TransferTab_TextBox_ToLocation.Name = "Control_TransferTab_TextBox_ToLocation";
            Control_TransferTab_TextBox_ToLocation.PlaceholderText = "Enter Location";
            Control_TransferTab_TextBox_ToLocation.Size = new Size(290, 23);
            Control_TransferTab_TextBox_ToLocation.TabIndex = 3;
            //
            // Control_TransferTab_Label_Quantity
            //
            Control_TransferTab_Label_Quantity.Dock = DockStyle.Fill;
            Control_TransferTab_Label_Quantity.Font = new Font("Segoe UI Emoji", 9F);
            Control_TransferTab_Label_Quantity.Location = new Point(3, 90);
            Control_TransferTab_Label_Quantity.Margin = new Padding(3);
            Control_TransferTab_Label_Quantity.Name = "Control_TransferTab_Label_Quantity";
            Control_TransferTab_Label_Quantity.Size = new Size(95, 23);
            Control_TransferTab_Label_Quantity.TabIndex = 10;
            Control_TransferTab_Label_Quantity.Text = "Quantity:";
            Control_TransferTab_Label_Quantity.TextAlign = ContentAlignment.MiddleRight;
            //
            // Control_TransferTab_NumericUpDown_Quantity
            //
            Control_TransferTab_NumericUpDown_Quantity.Dock = DockStyle.Fill;
            Control_TransferTab_NumericUpDown_Quantity.Enabled = false;
            Control_TransferTab_NumericUpDown_Quantity.Font = new Font("Segoe UI Emoji", 9F);
            Control_TransferTab_NumericUpDown_Quantity.Location = new Point(104, 90);
            Control_TransferTab_NumericUpDown_Quantity.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            Control_TransferTab_NumericUpDown_Quantity.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            Control_TransferTab_NumericUpDown_Quantity.Name = "Control_TransferTab_NumericUpDown_Quantity";
            Control_TransferTab_NumericUpDown_Quantity.Size = new Size(189, 23);
            Control_TransferTab_NumericUpDown_Quantity.TabIndex = 4;
            Control_TransferTab_NumericUpDown_Quantity.Value = new decimal(new int[] { 1, 0, 0, 0 });
            //
            // tableLayoutPanel2
            //
            tableLayoutPanel2.AutoSize = true;
            tableLayoutPanel2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel2.ColumnCount = 3;
            Control_TransferTab_TableLayout_Inputs.SetColumnSpan(tableLayoutPanel2, 2);
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 49.9999962F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50.0000076F));
            tableLayoutPanel2.Controls.Add(Control_TransferTab_TableLayout_SaveSearch, 1, 0);
            tableLayoutPanel2.Controls.Add(Control_TransferTab_TableLayout_Print, 1, 1);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 204);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.Size = new Size(290, 88);
            tableLayoutPanel2.TabIndex = 11;
            //
            // Control_TransferTab_TableLayout_SaveSearch
            //
            Control_TransferTab_TableLayout_SaveSearch.AutoSize = true;
            Control_TransferTab_TableLayout_SaveSearch.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_TransferTab_TableLayout_SaveSearch.ColumnCount = 2;
            Control_TransferTab_TableLayout_SaveSearch.ColumnStyles.Add(new ColumnStyle());
            Control_TransferTab_TableLayout_SaveSearch.ColumnStyles.Add(new ColumnStyle());
            Control_TransferTab_TableLayout_SaveSearch.Controls.Add(Control_TransferTab_Button_Transfer, 1, 0);
            Control_TransferTab_TableLayout_SaveSearch.Controls.Add(Control_TransferTab_Button_Search, 0, 0);
            Control_TransferTab_TableLayout_SaveSearch.Location = new Point(38, 3);
            Control_TransferTab_TableLayout_SaveSearch.Name = "Control_TransferTab_TableLayout_SaveSearch";
            Control_TransferTab_TableLayout_SaveSearch.RowCount = 1;
            Control_TransferTab_TableLayout_SaveSearch.RowStyles.Add(new RowStyle());
            Control_TransferTab_TableLayout_SaveSearch.Size = new Size(212, 38);
            Control_TransferTab_TableLayout_SaveSearch.TabIndex = 1001;
            //
            // Control_TransferTab_Button_Transfer
            //
            Control_TransferTab_Button_Transfer.AutoSize = true;
            Control_TransferTab_Button_Transfer.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_TransferTab_Button_Transfer.Enabled = false;
            Control_TransferTab_Button_Transfer.Font = new Font("Segoe UI Emoji", 9F);
            Control_TransferTab_Button_Transfer.Location = new Point(109, 3);
            Control_TransferTab_Button_Transfer.MaximumSize = new Size(100, 32);
            Control_TransferTab_Button_Transfer.MinimumSize = new Size(100, 32);
            Control_TransferTab_Button_Transfer.Name = "Control_TransferTab_Button_Transfer";
            Control_TransferTab_Button_Transfer.RightToLeft = RightToLeft.Yes;
            Control_TransferTab_Button_Transfer.Size = new Size(100, 32);
            Control_TransferTab_Button_Transfer.TabIndex = 6;
            Control_TransferTab_Button_Transfer.Text = "Save";
            Control_TransferTab_Button_Transfer.UseVisualStyleBackColor = true;
            //
            // Control_TransferTab_Button_Search
            //
            Control_TransferTab_Button_Search.AutoSize = true;
            Control_TransferTab_Button_Search.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_TransferTab_Button_Search.Enabled = false;
            Control_TransferTab_Button_Search.Font = new Font("Segoe UI Emoji", 9F);
            Control_TransferTab_Button_Search.Location = new Point(3, 3);
            Control_TransferTab_Button_Search.MaximumSize = new Size(100, 32);
            Control_TransferTab_Button_Search.MinimumSize = new Size(100, 32);
            Control_TransferTab_Button_Search.Name = "Control_TransferTab_Button_Search";
            Control_TransferTab_Button_Search.Size = new Size(100, 32);
            Control_TransferTab_Button_Search.TabIndex = 5;
            Control_TransferTab_Button_Search.Text = "Search";
            Control_TransferTab_Button_Search.UseVisualStyleBackColor = true;
            //
            // Control_TransferTab_TableLayout_Print
            //
            Control_TransferTab_TableLayout_Print.AutoSize = true;
            Control_TransferTab_TableLayout_Print.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_TransferTab_TableLayout_Print.ColumnCount = 3;
            Control_TransferTab_TableLayout_Print.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 49.9999962F));
            Control_TransferTab_TableLayout_Print.ColumnStyles.Add(new ColumnStyle());
            Control_TransferTab_TableLayout_Print.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50.0000076F));
            Control_TransferTab_TableLayout_Print.Controls.Add(Control_TransferTab_Button_Print, 1, 0);
            Control_TransferTab_TableLayout_Print.Dock = DockStyle.Fill;
            Control_TransferTab_TableLayout_Print.Location = new Point(38, 47);
            Control_TransferTab_TableLayout_Print.Name = "Control_TransferTab_TableLayout_Print";
            Control_TransferTab_TableLayout_Print.RowCount = 1;
            Control_TransferTab_TableLayout_Print.RowStyles.Add(new RowStyle());
            Control_TransferTab_TableLayout_Print.Size = new Size(212, 38);
            Control_TransferTab_TableLayout_Print.TabIndex = 1002;
            //
            // Control_TransferTab_Button_Print
            //
            Control_TransferTab_Button_Print.AutoSize = true;
            Control_TransferTab_Button_Print.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_TransferTab_Button_Print.Enabled = false;
            Control_TransferTab_Button_Print.Font = new Font("Segoe UI Emoji", 9F);
            Control_TransferTab_Button_Print.Location = new Point(5, 3);
            Control_TransferTab_Button_Print.MaximumSize = new Size(200, 32);
            Control_TransferTab_Button_Print.MinimumSize = new Size(200, 32);
            Control_TransferTab_Button_Print.Name = "Control_TransferTab_Button_Print";
            Control_TransferTab_Button_Print.Size = new Size(200, 32);
            Control_TransferTab_Button_Print.TabIndex = 1000;
            Control_TransferTab_Button_Print.TabStop = false;
            Control_TransferTab_Button_Print.Text = "Print";
            Control_TransferTab_Button_Print.UseVisualStyleBackColor = true;
            //
            // tableLayoutPanel3
            //
            tableLayoutPanel3.AutoSize = true;
            tableLayoutPanel3.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel3.ColumnCount = 4;
            Control_TransferTab_Panel_Main.SetColumnSpan(tableLayoutPanel3, 2);
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel3.Controls.Add(Control_TransferTab_Button_Toggle_Split, 0, 0);
            tableLayoutPanel3.Controls.Add(Control_TransferTab_Button_Reset, 1, 0);
            tableLayoutPanel3.Controls.Add(Control_TransferTab_Button_Toggle_RightPanel, 3, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(3, 312);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.Size = new Size(824, 38);
            tableLayoutPanel3.TabIndex = 12;
            //
            // Control_TransferTab_Button_Toggle_Split
            //
            Control_TransferTab_Button_Toggle_Split.AutoSize = true;
            Control_TransferTab_Button_Toggle_Split.Font = new Font("Segoe UI Emoji", 9F);
            Control_TransferTab_Button_Toggle_Split.Location = new Point(3, 3);
            Control_TransferTab_Button_Toggle_Split.MaximumSize = new Size(64, 32);
            Control_TransferTab_Button_Toggle_Split.MinimumSize = new Size(64, 32);
            Control_TransferTab_Button_Toggle_Split.Name = "Control_TransferTab_Button_Toggle_Split";
            Control_TransferTab_Button_Toggle_Split.Size = new Size(64, 32);
            Control_TransferTab_Button_Toggle_Split.TabIndex = 1000;
            Control_TransferTab_Button_Toggle_Split.UseVisualStyleBackColor = true;
            Control_TransferTab_Button_Toggle_Split.Click += Control_TransferTab_Button_Toggle_Split_Click;
            //
            // Control_TransferTab_Button_Reset
            //
            Control_TransferTab_Button_Reset.AutoSize = true;
            Control_TransferTab_Button_Reset.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_TransferTab_Button_Reset.Font = new Font("Segoe UI Emoji", 9F);
            Control_TransferTab_Button_Reset.Location = new Point(41, 3);
            Control_TransferTab_Button_Reset.MaximumSize = new Size(200, 32);
            Control_TransferTab_Button_Reset.MinimumSize = new Size(200, 32);
            Control_TransferTab_Button_Reset.Name = "Control_TransferTab_Button_Reset";
            Control_TransferTab_Button_Reset.Size = new Size(200, 32);
            Control_TransferTab_Button_Reset.TabIndex = 7;
            Control_TransferTab_Button_Reset.TabStop = false;
            Control_TransferTab_Button_Reset.Text = "Reset";
            Control_TransferTab_Button_Reset.UseVisualStyleBackColor = true;
            //
            // Control_TransferTab_Button_Toggle_RightPanel
            //
            Control_TransferTab_Button_Toggle_RightPanel.AutoSize = true;
            Control_TransferTab_Button_Toggle_RightPanel.Font = new Font("Segoe UI Emoji", 9F);
            Control_TransferTab_Button_Toggle_RightPanel.Location = new Point(789, 3);
            Control_TransferTab_Button_Toggle_RightPanel.MaximumSize = new Size(64, 32);
            Control_TransferTab_Button_Toggle_RightPanel.MinimumSize = new Size(64, 32);
            Control_TransferTab_Button_Toggle_RightPanel.Name = "Control_TransferTab_Button_Toggle_RightPanel";
            Control_TransferTab_Button_Toggle_RightPanel.Size = new Size(64, 32);
            Control_TransferTab_Button_Toggle_RightPanel.TabIndex = 999;
            Control_TransferTab_Button_Toggle_RightPanel.TabStop = false;
            Control_TransferTab_Button_Toggle_RightPanel.UseVisualStyleBackColor = true;
            Control_TransferTab_Button_Toggle_RightPanel.Click += Control_TransferTab_Button_Toggle_RightPanel_Click;
            //
            // Control_TransferTab
            //
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            Controls.Add(Control_TransferTab_GroupBox_MainControl);
            Name = "Control_TransferTab";
            Size = new Size(836, 375);
            Control_TransferTab_GroupBox_MainControl.ResumeLayout(false);
            Control_TransferTab_GroupBox_MainControl.PerformLayout();
            Control_TransferTab_Panel_Main.ResumeLayout(false);
            Control_TransferTab_Panel_Main.PerformLayout();
            Control_TransferTab_TableLayout_Main.ResumeLayout(false);
            Control_TransferTab_TableLayout_Main.PerformLayout();
            Control_TransferTab_Panel_DataGridView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)Control_TransferTab_Image_NothingFound).EndInit();
            ((System.ComponentModel.ISupportInitialize)Control_TransferTab_DataGridView_Main).EndInit();
            Control_TransferTab_ContextMenu_DataGridView.ResumeLayout(false);
            Control_TransferTab_Panel_Inputs.ResumeLayout(false);
            Control_TransferTab_Panel_Inputs.PerformLayout();
            Control_TransferTab_TableLayout_Inputs.ResumeLayout(false);
            Control_TransferTab_TableLayout_Inputs.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)Control_TransferTab_NumericUpDown_Quantity).EndInit();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            Control_TransferTab_TableLayout_SaveSearch.ResumeLayout(false);
            Control_TransferTab_TableLayout_SaveSearch.PerformLayout();
            Control_TransferTab_TableLayout_Print.ResumeLayout(false);
            Control_TransferTab_TableLayout_Print.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
        private TableLayoutPanel Control_TransferTab_Panel_Main;
        private ContextMenuStrip Control_TransferTab_ContextMenu_DataGridView;
        private TableLayoutPanel tableLayoutPanel3;
        private Button Control_TransferTab_Button_Toggle_Split;
        private Button Control_TransferTab_Button_Toggle_RightPanel;
        private TableLayoutPanel Control_TransferTab_TableLayout_Inputs;
        private SuggestionTextBoxWithLabel Control_TransferTab_TextBox_Operation;
        internal SuggestionTextBoxWithLabel Control_TransferTab_TextBox_Part;
        private NumericUpDown Control_TransferTab_NumericUpDown_Quantity;
        private SuggestionTextBoxWithLabel Control_TransferTab_TextBox_ToLocation;
        private Label Control_TransferTab_Label_Quantity;
        private TableLayoutPanel tableLayoutPanel2;
        private Button Control_TransferTab_Button_Transfer;
        private Button Control_TransferTab_Button_Search;
        private Button Control_TransferTab_Button_Print;
        private Button Control_TransferTab_Button_Reset;
        private Panel Control_TransferTab_Panel_DataGridView;
        private PictureBox Control_TransferTab_Image_NothingFound;
        private DataGridView Control_TransferTab_DataGridView_Main;
        private TableLayoutPanel Control_TransferTab_TableLayout_SaveSearch;
        private TableLayoutPanel Control_TransferTab_TableLayout_Print;
        private Panel Control_TransferTab_Panel_Inputs;
        private TableLayoutPanel Control_TransferTab_TableLayout_Main;
    }


        #endregion
    }
