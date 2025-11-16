using System.Drawing;
using System.Windows.Forms;

using MTM_WIP_Application_Winforms.Controls.Shared;
using MTM_WIP_Application_Winforms.Forms.Shared;

namespace MTM_WIP_Application_Winforms.Controls.MainForm
{
    partial class Control_InventoryTab : ThemedUserControl
    {
        #region Fields
        


        private System.ComponentModel.IContainer components = null;

        #endregion

        private GroupBox Control_InventoryTab_GroupBox_Main;
        private Button Control_InventoryTab_Button_Reset;
        private Button Control_InventoryTab_Button_Save;
        private Label Control_InventoryTab_Label_Version;
        private Button Control_InventoryTab_Button_AdvancedEntry;
        private SuggestionTextBox Control_InventoryTab_TextBox_Operation;
        private TextBox Control_InventoryTab_TextBox_Quantity;
        private TableLayoutPanel Control_InventoryTab_TableLayout_Main;
        private Button Control_InventoryTab_Button_Toggle_RightPanel;
        public SuggestionTextBox Control_InventoryTab_TextBox_Part;
        private SuggestionTextBox Control_InventoryTab_TextBox_Location;
        private ToolTip Control_InventoryTab_Tooltip;
        private Label Control_InventoryTab_Label_ColorCode;
        private SuggestionTextBox Control_InventoryTab_TextBox_ColorCode;
        private Label Control_InventoryTab_Label_WorkOrder;
        private TextBox Control_InventoryTab_TextBox_WorkOrder;
        

        
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
            Control_InventoryTab_GroupBox_Main = new GroupBox();
            Control_InventoryTab_TableLayout_Main = new TableLayoutPanel();
            Control_InventoryTab_TableLayout_MiddleGroup = new TableLayoutPanel();
            Control_InventoryTab_Label_Notes = new Label();
            NotesPanel = new Panel();
            Control_InventoryTab_RichTextBox_Notes = new RichTextBox();
            Control_InventoryTab_TableLayout_TopGroup = new TableLayoutPanel();
            Control_InventoryTab_Button_PartF4 = new Button();
            Control_InventoryTab_TextBox_Location = new SuggestionTextBox();
            Control_InventoryTab_TextBox_Quantity = new TextBox();
            Control_InventoryTab_TextBox_Operation = new SuggestionTextBox();
            Control_InventoryTab_TextBox_Part = new SuggestionTextBox();
            Control_InventoryTab_Label_Loc = new Label();
            Control_InventoryTab_Label_Qty = new Label();
            Control_InventoryTab_Label_Op = new Label();
            Control_InventoryTab_Label_Part = new Label();
            Control_InventoryTab_Label_ColorCode = new Label();
            Control_InventoryTab_TextBox_ColorCode = new SuggestionTextBox();
            Control_InventoryTab_Label_WorkOrder = new Label();
            Control_InventoryTab_TextBox_WorkOrder = new TextBox();
            Control_InventoryTab_Button_ColorF4 = new Button();
            Control_InventoryTab_Button_LocationF4 = new Button();
            Control_InventoryTab_Button_OperationF4 = new Button();
            Control_InventoryTab_TableLayout_BottomGroup = new TableLayoutPanel();
            Control_InventoryTab_Button_Toggle_RightPanel = new Button();
            Control_InventoryTab_Label_Version = new Label();
            Control_InventoryTab_Button_Save = new Button();
            Control_InventoryTab_Button_AdvancedEntry = new Button();
            Control_InventoryTab_Button_Reset = new Button();
            Control_InventoryTab_Tooltip = new ToolTip(components);
            Control_InventoryTab_GroupBox_Main.SuspendLayout();
            Control_InventoryTab_TableLayout_Main.SuspendLayout();
            Control_InventoryTab_TableLayout_MiddleGroup.SuspendLayout();
            NotesPanel.SuspendLayout();
            Control_InventoryTab_TableLayout_TopGroup.SuspendLayout();
            Control_InventoryTab_TableLayout_BottomGroup.SuspendLayout();
            SuspendLayout();
            // 
            // Control_InventoryTab_GroupBox_Main
            // 
            Control_InventoryTab_GroupBox_Main.AutoSize = true;
            Control_InventoryTab_GroupBox_Main.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_InventoryTab_GroupBox_Main.Controls.Add(Control_InventoryTab_TableLayout_Main);
            Control_InventoryTab_GroupBox_Main.Dock = DockStyle.Fill;
            Control_InventoryTab_GroupBox_Main.Location = new Point(0, 0);
            Control_InventoryTab_GroupBox_Main.Name = "Control_InventoryTab_GroupBox_Main";
            Control_InventoryTab_GroupBox_Main.Size = new Size(800, 375);
            Control_InventoryTab_GroupBox_Main.TabIndex = 1;
            Control_InventoryTab_GroupBox_Main.TabStop = false;
            Control_InventoryTab_GroupBox_Main.Text = "Inventory Entry";
            // 
            // Control_InventoryTab_TableLayout_Main
            // 
            Control_InventoryTab_TableLayout_Main.AutoSize = true;
            Control_InventoryTab_TableLayout_Main.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_InventoryTab_TableLayout_Main.ColumnCount = 1;
            Control_InventoryTab_TableLayout_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_InventoryTab_TableLayout_Main.Controls.Add(Control_InventoryTab_TableLayout_MiddleGroup, 0, 1);
            Control_InventoryTab_TableLayout_Main.Controls.Add(Control_InventoryTab_TableLayout_TopGroup, 0, 0);
            Control_InventoryTab_TableLayout_Main.Controls.Add(Control_InventoryTab_TableLayout_BottomGroup, 0, 2);
            Control_InventoryTab_TableLayout_Main.Dock = DockStyle.Fill;
            Control_InventoryTab_TableLayout_Main.Location = new Point(3, 19);
            Control_InventoryTab_TableLayout_Main.Name = "Control_InventoryTab_TableLayout_Main";
            Control_InventoryTab_TableLayout_Main.RowCount = 3;
            Control_InventoryTab_TableLayout_Main.RowStyles.Add(new RowStyle());
            Control_InventoryTab_TableLayout_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_InventoryTab_TableLayout_Main.RowStyles.Add(new RowStyle());
            Control_InventoryTab_TableLayout_Main.Size = new Size(794, 353);
            Control_InventoryTab_TableLayout_Main.TabIndex = 0;
            // 
            // Control_InventoryTab_TableLayout_MiddleGroup
            // 
            Control_InventoryTab_TableLayout_MiddleGroup.AutoSize = true;
            Control_InventoryTab_TableLayout_MiddleGroup.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_InventoryTab_TableLayout_MiddleGroup.ColumnCount = 1;
            Control_InventoryTab_TableLayout_MiddleGroup.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_InventoryTab_TableLayout_MiddleGroup.Controls.Add(Control_InventoryTab_Label_Notes, 0, 0);
            Control_InventoryTab_TableLayout_MiddleGroup.Controls.Add(NotesPanel, 0, 1);
            Control_InventoryTab_TableLayout_MiddleGroup.Dock = DockStyle.Fill;
            Control_InventoryTab_TableLayout_MiddleGroup.Location = new Point(3, 183);
            Control_InventoryTab_TableLayout_MiddleGroup.Name = "Control_InventoryTab_TableLayout_MiddleGroup";
            Control_InventoryTab_TableLayout_MiddleGroup.RowCount = 2;
            Control_InventoryTab_TableLayout_MiddleGroup.RowStyles.Add(new RowStyle());
            Control_InventoryTab_TableLayout_MiddleGroup.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            Control_InventoryTab_TableLayout_MiddleGroup.Size = new Size(788, 126);
            Control_InventoryTab_TableLayout_MiddleGroup.TabIndex = 28;
            // 
            // Control_InventoryTab_Label_Notes
            // 
            Control_InventoryTab_Label_Notes.AutoSize = true;
            Control_InventoryTab_Label_Notes.Dock = DockStyle.Fill;
            Control_InventoryTab_Label_Notes.Font = new Font("Segoe UI Emoji", 9F, FontStyle.Underline);
            Control_InventoryTab_Label_Notes.Location = new Point(3, 3);
            Control_InventoryTab_Label_Notes.Margin = new Padding(3);
            Control_InventoryTab_Label_Notes.Name = "Control_InventoryTab_Label_Notes";
            Control_InventoryTab_Label_Notes.Size = new Size(782, 16);
            Control_InventoryTab_Label_Notes.TabIndex = 9;
            Control_InventoryTab_Label_Notes.Text = "Notes";
            Control_InventoryTab_Label_Notes.TextAlign = ContentAlignment.BottomCenter;
            // 
            // NotesPanel
            // 
            NotesPanel.AutoSize = true;
            NotesPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            NotesPanel.Controls.Add(Control_InventoryTab_RichTextBox_Notes);
            NotesPanel.Dock = DockStyle.Fill;
            NotesPanel.Location = new Point(3, 25);
            NotesPanel.Name = "NotesPanel";
            NotesPanel.Size = new Size(782, 98);
            NotesPanel.TabIndex = 10;
            // 
            // Control_InventoryTab_RichTextBox_Notes
            // 
            Control_InventoryTab_RichTextBox_Notes.Dock = DockStyle.Fill;
            Control_InventoryTab_RichTextBox_Notes.Location = new Point(0, 0);
            Control_InventoryTab_RichTextBox_Notes.Name = "Control_InventoryTab_RichTextBox_Notes";
            Control_InventoryTab_RichTextBox_Notes.Size = new Size(782, 98);
            Control_InventoryTab_RichTextBox_Notes.TabIndex = 6;
            Control_InventoryTab_RichTextBox_Notes.Text = "";
            // 
            // Control_InventoryTab_TableLayout_TopGroup
            // 
            Control_InventoryTab_TableLayout_TopGroup.AutoSize = true;
            Control_InventoryTab_TableLayout_TopGroup.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_InventoryTab_TableLayout_TopGroup.ColumnCount = 3;
            Control_InventoryTab_TableLayout_TopGroup.ColumnStyles.Add(new ColumnStyle());
            Control_InventoryTab_TableLayout_TopGroup.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_InventoryTab_TableLayout_TopGroup.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30F));
            Control_InventoryTab_TableLayout_TopGroup.Controls.Add(Control_InventoryTab_Button_PartF4, 2, 0);
            Control_InventoryTab_TableLayout_TopGroup.Controls.Add(Control_InventoryTab_TextBox_Location, 1, 3);
            Control_InventoryTab_TableLayout_TopGroup.Controls.Add(Control_InventoryTab_TextBox_Quantity, 1, 2);
            Control_InventoryTab_TableLayout_TopGroup.Controls.Add(Control_InventoryTab_TextBox_Operation, 1, 1);
            Control_InventoryTab_TableLayout_TopGroup.Controls.Add(Control_InventoryTab_TextBox_Part, 1, 0);
            Control_InventoryTab_TableLayout_TopGroup.Controls.Add(Control_InventoryTab_Label_Loc, 0, 3);
            Control_InventoryTab_TableLayout_TopGroup.Controls.Add(Control_InventoryTab_Label_Qty, 0, 2);
            Control_InventoryTab_TableLayout_TopGroup.Controls.Add(Control_InventoryTab_Label_Op, 0, 1);
            Control_InventoryTab_TableLayout_TopGroup.Controls.Add(Control_InventoryTab_Label_Part, 0, 0);
            Control_InventoryTab_TableLayout_TopGroup.Controls.Add(Control_InventoryTab_Label_ColorCode, 0, 4);
            Control_InventoryTab_TableLayout_TopGroup.Controls.Add(Control_InventoryTab_TextBox_ColorCode, 1, 4);
            Control_InventoryTab_TableLayout_TopGroup.Controls.Add(Control_InventoryTab_Label_WorkOrder, 0, 5);
            Control_InventoryTab_TableLayout_TopGroup.Controls.Add(Control_InventoryTab_TextBox_WorkOrder, 1, 5);
            Control_InventoryTab_TableLayout_TopGroup.Controls.Add(Control_InventoryTab_Button_ColorF4, 2, 4);
            Control_InventoryTab_TableLayout_TopGroup.Controls.Add(Control_InventoryTab_Button_LocationF4, 2, 3);
            Control_InventoryTab_TableLayout_TopGroup.Controls.Add(Control_InventoryTab_Button_OperationF4, 2, 1);
            Control_InventoryTab_TableLayout_TopGroup.Dock = DockStyle.Fill;
            Control_InventoryTab_TableLayout_TopGroup.Location = new Point(3, 3);
            Control_InventoryTab_TableLayout_TopGroup.Name = "Control_InventoryTab_TableLayout_TopGroup";
            Control_InventoryTab_TableLayout_TopGroup.RowCount = 6;
            Control_InventoryTab_TableLayout_TopGroup.RowStyles.Add(new RowStyle());
            Control_InventoryTab_TableLayout_TopGroup.RowStyles.Add(new RowStyle());
            Control_InventoryTab_TableLayout_TopGroup.RowStyles.Add(new RowStyle());
            Control_InventoryTab_TableLayout_TopGroup.RowStyles.Add(new RowStyle());
            Control_InventoryTab_TableLayout_TopGroup.RowStyles.Add(new RowStyle());
            Control_InventoryTab_TableLayout_TopGroup.RowStyles.Add(new RowStyle());
            Control_InventoryTab_TableLayout_TopGroup.Size = new Size(788, 174);
            Control_InventoryTab_TableLayout_TopGroup.TabIndex = 7;
            // 
            // InventoryTab_Single_Button_PartF4
            // 
            Control_InventoryTab_Button_PartF4.Location = new Point(761, 3);
            Control_InventoryTab_Button_PartF4.Name = "InventoryTab_Single_Button_PartF4";
            Control_InventoryTab_Button_PartF4.Size = new Size(23, 23);
            Control_InventoryTab_Button_PartF4.TabIndex = 13;
            Control_InventoryTab_Button_PartF4.Text = "🔎";
            Control_InventoryTab_Button_PartF4.UseVisualStyleBackColor = true;
            // 
            // Control_InventoryTab_TextBox_Location
            // 
            Control_InventoryTab_TextBox_Location.Dock = DockStyle.Fill;
            Control_InventoryTab_TextBox_Location.Location = new Point(87, 90);
            Control_InventoryTab_TextBox_Location.Name = "Control_InventoryTab_TextBox_Location";
            Control_InventoryTab_TextBox_Location.PlaceholderText = "Enter or Select Location";
            Control_InventoryTab_TextBox_Location.Size = new Size(668, 23);
            Control_InventoryTab_TextBox_Location.TabIndex = 4;
            // 
            // Control_InventoryTab_TextBox_Quantity
            // 
            Control_InventoryTab_TableLayout_TopGroup.SetColumnSpan(Control_InventoryTab_TextBox_Quantity, 2);
            Control_InventoryTab_TextBox_Quantity.Dock = DockStyle.Fill;
            Control_InventoryTab_TextBox_Quantity.Location = new Point(87, 61);
            Control_InventoryTab_TextBox_Quantity.Name = "Control_InventoryTab_TextBox_Quantity";
            Control_InventoryTab_TextBox_Quantity.PlaceholderText = "Enter Quantity";
            Control_InventoryTab_TextBox_Quantity.Size = new Size(698, 23);
            Control_InventoryTab_TextBox_Quantity.TabIndex = 3;
            // 
            // Control_InventoryTab_TextBox_Operation
            // 
            Control_InventoryTab_TextBox_Operation.Dock = DockStyle.Fill;
            Control_InventoryTab_TextBox_Operation.Location = new Point(87, 32);
            Control_InventoryTab_TextBox_Operation.Name = "Control_InventoryTab_TextBox_Operation";
            Control_InventoryTab_TextBox_Operation.PlaceholderText = "Enter or Select Operation";
            Control_InventoryTab_TextBox_Operation.Size = new Size(668, 23);
            Control_InventoryTab_TextBox_Operation.TabIndex = 2;
            // 
            // Control_InventoryTab_TextBox_Part
            // 
            Control_InventoryTab_TextBox_Part.Dock = DockStyle.Fill;
            Control_InventoryTab_TextBox_Part.Location = new Point(87, 3);
            Control_InventoryTab_TextBox_Part.Name = "Control_InventoryTab_TextBox_Part";
            Control_InventoryTab_TextBox_Part.PlaceholderText = "Enter or Select Part Number";
            Control_InventoryTab_TextBox_Part.Size = new Size(668, 23);
            Control_InventoryTab_TextBox_Part.TabIndex = 1;
            // 
            // Control_InventoryTab_Label_Loc
            // 
            Control_InventoryTab_Label_Loc.AutoSize = true;
            Control_InventoryTab_Label_Loc.Dock = DockStyle.Fill;
            Control_InventoryTab_Label_Loc.Location = new Point(3, 90);
            Control_InventoryTab_Label_Loc.Margin = new Padding(3);
            Control_InventoryTab_Label_Loc.Name = "Control_InventoryTab_Label_Loc";
            Control_InventoryTab_Label_Loc.Size = new Size(78, 23);
            Control_InventoryTab_Label_Loc.TabIndex = 11;
            Control_InventoryTab_Label_Loc.Text = "Location:";
            Control_InventoryTab_Label_Loc.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Control_InventoryTab_Label_Qty
            // 
            Control_InventoryTab_Label_Qty.AutoSize = true;
            Control_InventoryTab_Label_Qty.Dock = DockStyle.Fill;
            Control_InventoryTab_Label_Qty.Location = new Point(3, 61);
            Control_InventoryTab_Label_Qty.Margin = new Padding(3);
            Control_InventoryTab_Label_Qty.Name = "Control_InventoryTab_Label_Qty";
            Control_InventoryTab_Label_Qty.Size = new Size(78, 23);
            Control_InventoryTab_Label_Qty.TabIndex = 10;
            Control_InventoryTab_Label_Qty.Text = "Quantity:";
            Control_InventoryTab_Label_Qty.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Control_InventoryTab_Label_Op
            // 
            Control_InventoryTab_Label_Op.AutoSize = true;
            Control_InventoryTab_Label_Op.Dock = DockStyle.Fill;
            Control_InventoryTab_Label_Op.Location = new Point(3, 32);
            Control_InventoryTab_Label_Op.Margin = new Padding(3);
            Control_InventoryTab_Label_Op.Name = "Control_InventoryTab_Label_Op";
            Control_InventoryTab_Label_Op.Size = new Size(78, 23);
            Control_InventoryTab_Label_Op.TabIndex = 9;
            Control_InventoryTab_Label_Op.Text = "Operation:";
            Control_InventoryTab_Label_Op.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Control_InventoryTab_Label_Part
            // 
            Control_InventoryTab_Label_Part.AutoSize = true;
            Control_InventoryTab_Label_Part.Dock = DockStyle.Fill;
            Control_InventoryTab_Label_Part.Location = new Point(3, 3);
            Control_InventoryTab_Label_Part.Margin = new Padding(3);
            Control_InventoryTab_Label_Part.Name = "Control_InventoryTab_Label_Part";
            Control_InventoryTab_Label_Part.Size = new Size(78, 23);
            Control_InventoryTab_Label_Part.TabIndex = 8;
            Control_InventoryTab_Label_Part.Text = "Part Number:";
            Control_InventoryTab_Label_Part.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Control_InventoryTab_Label_ColorCode
            // 
            Control_InventoryTab_Label_ColorCode.AutoSize = true;
            Control_InventoryTab_Label_ColorCode.Dock = DockStyle.Fill;
            Control_InventoryTab_Label_ColorCode.Location = new Point(3, 119);
            Control_InventoryTab_Label_ColorCode.Margin = new Padding(3);
            Control_InventoryTab_Label_ColorCode.Name = "Control_InventoryTab_Label_ColorCode";
            Control_InventoryTab_Label_ColorCode.Size = new Size(78, 23);
            Control_InventoryTab_Label_ColorCode.TabIndex = 9;
            Control_InventoryTab_Label_ColorCode.Text = "Color Code:";
            Control_InventoryTab_Label_ColorCode.TextAlign = ContentAlignment.MiddleRight;
            Control_InventoryTab_Label_ColorCode.Visible = false;
            // 
            // Control_InventoryTab_TextBox_ColorCode
            // 
            Control_InventoryTab_TextBox_ColorCode.Dock = DockStyle.Fill;
            Control_InventoryTab_TextBox_ColorCode.Location = new Point(87, 119);
            Control_InventoryTab_TextBox_ColorCode.Name = "Control_InventoryTab_TextBox_ColorCode";
            Control_InventoryTab_TextBox_ColorCode.Size = new Size(668, 23);
            Control_InventoryTab_TextBox_ColorCode.TabIndex = 5;
            Control_InventoryTab_TextBox_ColorCode.Visible = false;
            // 
            // Control_InventoryTab_Label_WorkOrder
            // 
            Control_InventoryTab_Label_WorkOrder.AutoSize = true;
            Control_InventoryTab_Label_WorkOrder.Dock = DockStyle.Fill;
            Control_InventoryTab_Label_WorkOrder.Location = new Point(3, 148);
            Control_InventoryTab_Label_WorkOrder.Margin = new Padding(3);
            Control_InventoryTab_Label_WorkOrder.Name = "Control_InventoryTab_Label_WorkOrder";
            Control_InventoryTab_Label_WorkOrder.Size = new Size(78, 23);
            Control_InventoryTab_Label_WorkOrder.TabIndex = 10;
            Control_InventoryTab_Label_WorkOrder.Text = "Work Order:";
            Control_InventoryTab_Label_WorkOrder.TextAlign = ContentAlignment.MiddleRight;
            Control_InventoryTab_Label_WorkOrder.Visible = false;
            // 
            // Control_InventoryTab_TextBox_WorkOrder
            // 
            Control_InventoryTab_TableLayout_TopGroup.SetColumnSpan(Control_InventoryTab_TextBox_WorkOrder, 2);
            Control_InventoryTab_TextBox_WorkOrder.Dock = DockStyle.Fill;
            Control_InventoryTab_TextBox_WorkOrder.Location = new Point(87, 148);
            Control_InventoryTab_TextBox_WorkOrder.Name = "Control_InventoryTab_TextBox_WorkOrder";
            Control_InventoryTab_TextBox_WorkOrder.Size = new Size(698, 23);
            Control_InventoryTab_TextBox_WorkOrder.TabIndex = 6;
            Control_InventoryTab_TextBox_WorkOrder.Visible = false;
            // 
            // InventoryTab_Single_Button_ColorF4
            // 
            Control_InventoryTab_Button_ColorF4.Location = new Point(761, 119);
            Control_InventoryTab_Button_ColorF4.Name = "InventoryTab_Single_Button_ColorF4";
            Control_InventoryTab_Button_ColorF4.Size = new Size(23, 23);
            Control_InventoryTab_Button_ColorF4.TabIndex = 16;
            Control_InventoryTab_Button_ColorF4.Text = "🔎";
            Control_InventoryTab_Button_ColorF4.UseVisualStyleBackColor = true;
            Control_InventoryTab_Button_ColorF4.Visible = false;
            // 
            // InventoryTab_Single_Button_LocationF4
            // 
            Control_InventoryTab_Button_LocationF4.Location = new Point(761, 90);
            Control_InventoryTab_Button_LocationF4.Name = "InventoryTab_Single_Button_LocationF4";
            Control_InventoryTab_Button_LocationF4.Size = new Size(23, 23);
            Control_InventoryTab_Button_LocationF4.TabIndex = 15;
            Control_InventoryTab_Button_LocationF4.Text = "🔎";
            Control_InventoryTab_Button_LocationF4.UseVisualStyleBackColor = true;
            // 
            // InventoryTab_Single_Button_OperationF4
            // 
            Control_InventoryTab_Button_OperationF4.Location = new Point(761, 32);
            Control_InventoryTab_Button_OperationF4.Name = "InventoryTab_Single_Button_OperationF4";
            Control_InventoryTab_Button_OperationF4.Size = new Size(23, 23);
            Control_InventoryTab_Button_OperationF4.TabIndex = 14;
            Control_InventoryTab_Button_OperationF4.Text = "🔎";
            Control_InventoryTab_Button_OperationF4.UseVisualStyleBackColor = true;
            // 
            // Control_InventoryTab_TableLayout_BottomGroup
            // 
            Control_InventoryTab_TableLayout_BottomGroup.AutoSize = true;
            Control_InventoryTab_TableLayout_BottomGroup.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_InventoryTab_TableLayout_BottomGroup.ColumnCount = 5;
            Control_InventoryTab_TableLayout_BottomGroup.ColumnStyles.Add(new ColumnStyle());
            Control_InventoryTab_TableLayout_BottomGroup.ColumnStyles.Add(new ColumnStyle());
            Control_InventoryTab_TableLayout_BottomGroup.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_InventoryTab_TableLayout_BottomGroup.ColumnStyles.Add(new ColumnStyle());
            Control_InventoryTab_TableLayout_BottomGroup.ColumnStyles.Add(new ColumnStyle());
            Control_InventoryTab_TableLayout_BottomGroup.Controls.Add(Control_InventoryTab_Button_Toggle_RightPanel, 4, 0);
            Control_InventoryTab_TableLayout_BottomGroup.Controls.Add(Control_InventoryTab_Label_Version, 2, 0);
            Control_InventoryTab_TableLayout_BottomGroup.Controls.Add(Control_InventoryTab_Button_Save, 0, 0);
            Control_InventoryTab_TableLayout_BottomGroup.Controls.Add(Control_InventoryTab_Button_AdvancedEntry, 1, 0);
            Control_InventoryTab_TableLayout_BottomGroup.Controls.Add(Control_InventoryTab_Button_Reset, 3, 0);
            Control_InventoryTab_TableLayout_BottomGroup.Dock = DockStyle.Fill;
            Control_InventoryTab_TableLayout_BottomGroup.Location = new Point(3, 315);
            Control_InventoryTab_TableLayout_BottomGroup.Name = "Control_InventoryTab_TableLayout_BottomGroup";
            Control_InventoryTab_TableLayout_BottomGroup.RowCount = 1;
            Control_InventoryTab_TableLayout_BottomGroup.RowStyles.Add(new RowStyle());
            Control_InventoryTab_TableLayout_BottomGroup.Size = new Size(788, 35);
            Control_InventoryTab_TableLayout_BottomGroup.TabIndex = 27;
            // 
            // Control_InventoryTab_Button_Toggle_RightPanel
            // 
            Control_InventoryTab_Button_Toggle_RightPanel.Font = new Font("Segoe UI Emoji", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Control_InventoryTab_Button_Toggle_RightPanel.Location = new Point(747, 3);
            Control_InventoryTab_Button_Toggle_RightPanel.Name = "Control_InventoryTab_Button_Toggle_RightPanel";
            Control_InventoryTab_Button_Toggle_RightPanel.Size = new Size(38, 29);
            Control_InventoryTab_Button_Toggle_RightPanel.TabIndex = 9;
            Control_InventoryTab_Button_Toggle_RightPanel.Text = "➡";
            Control_InventoryTab_Button_Toggle_RightPanel.UseVisualStyleBackColor = true;
            Control_InventoryTab_Button_Toggle_RightPanel.Click += Control_InventoryTab_Button_Toggle_RightPanel_Click;
            // 
            // Control_InventoryTab_Label_Version
            // 
            Control_InventoryTab_Label_Version.AutoSize = true;
            Control_InventoryTab_Label_Version.Dock = DockStyle.Fill;
            Control_InventoryTab_Label_Version.Font = new Font("Segoe UI Emoji", 7F);
            Control_InventoryTab_Label_Version.Location = new Point(160, 3);
            Control_InventoryTab_Label_Version.Margin = new Padding(3);
            Control_InventoryTab_Label_Version.Name = "Control_InventoryTab_Label_Version";
            Control_InventoryTab_Label_Version.Size = new Size(494, 29);
            Control_InventoryTab_Label_Version.TabIndex = 8;
            Control_InventoryTab_Label_Version.Text = "Version: ";
            Control_InventoryTab_Label_Version.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_InventoryTab_Button_Save
            // 
            Control_InventoryTab_Button_Save.Font = new Font("Segoe UI Emoji", 8F);
            Control_InventoryTab_Button_Save.Location = new Point(3, 3);
            Control_InventoryTab_Button_Save.Name = "Control_InventoryTab_Button_Save";
            Control_InventoryTab_Button_Save.Size = new Size(60, 29);
            Control_InventoryTab_Button_Save.TabIndex = 6;
            Control_InventoryTab_Button_Save.Text = "Save";
            Control_InventoryTab_Button_Save.UseVisualStyleBackColor = true;
            // 
            // Control_InventoryTab_Button_AdvancedEntry
            // 
            Control_InventoryTab_Button_AdvancedEntry.ForeColor = Color.DarkRed;
            Control_InventoryTab_Button_AdvancedEntry.Location = new Point(69, 3);
            Control_InventoryTab_Button_AdvancedEntry.Name = "Control_InventoryTab_Button_AdvancedEntry";
            Control_InventoryTab_Button_AdvancedEntry.Size = new Size(85, 29);
            Control_InventoryTab_Button_AdvancedEntry.TabIndex = 8;
            Control_InventoryTab_Button_AdvancedEntry.Text = "Advanced";
            Control_InventoryTab_Button_AdvancedEntry.UseVisualStyleBackColor = true;
            // 
            // Control_InventoryTab_Button_Reset
            // 
            Control_InventoryTab_Button_Reset.Font = new Font("Segoe UI Emoji", 9F);
            Control_InventoryTab_Button_Reset.Location = new Point(660, 3);
            Control_InventoryTab_Button_Reset.Name = "Control_InventoryTab_Button_Reset";
            Control_InventoryTab_Button_Reset.Size = new Size(81, 29);
            Control_InventoryTab_Button_Reset.TabIndex = 7;
            Control_InventoryTab_Button_Reset.TabStop = false;
            Control_InventoryTab_Button_Reset.Text = "Reset";
            Control_InventoryTab_Button_Reset.UseVisualStyleBackColor = true;
            // 
            // Control_InventoryTab
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(Control_InventoryTab_GroupBox_Main);
            Name = "Control_InventoryTab";
            Size = new Size(800, 375);
            Control_InventoryTab_GroupBox_Main.ResumeLayout(false);
            Control_InventoryTab_GroupBox_Main.PerformLayout();
            Control_InventoryTab_TableLayout_Main.ResumeLayout(false);
            Control_InventoryTab_TableLayout_Main.PerformLayout();
            Control_InventoryTab_TableLayout_MiddleGroup.ResumeLayout(false);
            Control_InventoryTab_TableLayout_MiddleGroup.PerformLayout();
            NotesPanel.ResumeLayout(false);
            Control_InventoryTab_TableLayout_TopGroup.ResumeLayout(false);
            Control_InventoryTab_TableLayout_TopGroup.PerformLayout();
            Control_InventoryTab_TableLayout_BottomGroup.ResumeLayout(false);
            Control_InventoryTab_TableLayout_BottomGroup.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
        private TableLayoutPanel Control_InventoryTab_TableLayout_BottomGroup;
        private TableLayoutPanel Control_InventoryTab_TableLayout_TopGroup;
        private TableLayoutPanel Control_InventoryTab_TableLayout_MiddleGroup;
        private Label Control_InventoryTab_Label_Notes;
        private Label Control_InventoryTab_Label_Loc;
        private Label Control_InventoryTab_Label_Qty;
        private Label Control_InventoryTab_Label_Op;
        private Label Control_InventoryTab_Label_Part;
        private TableLayoutPanel tableLayoutPanel3;
        private Panel NotesPanel;
        private RichTextBox Control_InventoryTab_RichTextBox_Notes;
        private Button Control_InventoryTab_Button_PartF4;
        private Button Control_InventoryTab_Button_ColorF4;
        private Button Control_InventoryTab_Button_LocationF4;
        private Button Control_InventoryTab_Button_OperationF4;
    }

        
        #endregion
    }
