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
        private TextBox Control_InventoryTab_TextBox_Quantity;
        private TableLayoutPanel Control_InventoryTab_TableLayout_Main;
        private Button Control_InventoryTab_Button_Toggle_RightPanel;
        public SuggestionTextBoxWithLabel Control_InventoryTab_SuggestionBox_Part;
        private SuggestionTextBoxWithLabel Control_InventoryTab_SuggestionBox_Operation;
        private SuggestionTextBoxWithLabel Control_InventoryTab_SuggestionBox_Location;
        private SuggestionTextBoxWithLabel Control_InventoryTab_SuggestionBox_ColorCode;
        private ToolTip Control_InventoryTab_Tooltip;
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
            Control_InventoryTab_SuggestionBox_Part = new SuggestionTextBoxWithLabel();
            Control_InventoryTab_SuggestionBox_Operation = new SuggestionTextBoxWithLabel();
            Control_InventoryTab_Label_Qty = new Label();
            Control_InventoryTab_TextBox_Quantity = new TextBox();
            Control_InventoryTab_SuggestionBox_Location = new SuggestionTextBoxWithLabel();
            Control_InventoryTab_SuggestionBox_ColorCode = new SuggestionTextBoxWithLabel();
            Control_InventoryTab_Label_WorkOrder = new Label();
            Control_InventoryTab_TextBox_WorkOrder = new TextBox();
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
            Control_InventoryTab_GroupBox_Main.Font = new Font("Segoe UI Emoji", 9F);
            Control_InventoryTab_GroupBox_Main.Location = new Point(0, 0);
            Control_InventoryTab_GroupBox_Main.Name = "Control_InventoryTab_GroupBox_Main";
            Control_InventoryTab_GroupBox_Main.Size = new Size(458, 301);
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
            Control_InventoryTab_TableLayout_Main.Size = new Size(452, 279);
            Control_InventoryTab_TableLayout_Main.TabIndex = 0;
            // 
            // Control_InventoryTab_TableLayout_MiddleGroup
            // 
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
            Control_InventoryTab_TableLayout_MiddleGroup.Size = new Size(446, 49);
            Control_InventoryTab_TableLayout_MiddleGroup.TabIndex = 28;
            // 
            // Control_InventoryTab_Label_Notes
            // 
            Control_InventoryTab_Label_Notes.AutoSize = true;
            Control_InventoryTab_Label_Notes.Dock = DockStyle.Fill;
            Control_InventoryTab_Label_Notes.Font = new Font("Segoe UI Emoji", 9F);
            Control_InventoryTab_Label_Notes.Location = new Point(3, 3);
            Control_InventoryTab_Label_Notes.Margin = new Padding(3);
            Control_InventoryTab_Label_Notes.Name = "Control_InventoryTab_Label_Notes";
            Control_InventoryTab_Label_Notes.Size = new Size(440, 16);
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
            NotesPanel.Size = new Size(440, 21);
            NotesPanel.TabIndex = 10;
            // 
            // Control_InventoryTab_RichTextBox_Notes
            // 
            Control_InventoryTab_RichTextBox_Notes.BorderStyle = BorderStyle.FixedSingle;
            Control_InventoryTab_RichTextBox_Notes.Dock = DockStyle.Fill;
            Control_InventoryTab_RichTextBox_Notes.Font = new Font("Segoe UI Emoji", 9F);
            Control_InventoryTab_RichTextBox_Notes.Location = new Point(0, 0);
            Control_InventoryTab_RichTextBox_Notes.Name = "Control_InventoryTab_RichTextBox_Notes";
            Control_InventoryTab_RichTextBox_Notes.Size = new Size(440, 21);
            Control_InventoryTab_RichTextBox_Notes.TabIndex = 6;
            Control_InventoryTab_RichTextBox_Notes.Text = "";
            // 
            // Control_InventoryTab_TableLayout_TopGroup
            // 
            Control_InventoryTab_TableLayout_TopGroup.AutoSize = true;
            Control_InventoryTab_TableLayout_TopGroup.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_InventoryTab_TableLayout_TopGroup.ColumnCount = 1;
            Control_InventoryTab_TableLayout_TopGroup.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_InventoryTab_TableLayout_TopGroup.Controls.Add(Control_InventoryTab_SuggestionBox_Location, 0, 4);
            Control_InventoryTab_TableLayout_TopGroup.Controls.Add(Control_InventoryTab_TextBox_Quantity, 0, 3);
            Control_InventoryTab_TableLayout_TopGroup.Controls.Add(Control_InventoryTab_SuggestionBox_Operation, 0, 1);
            Control_InventoryTab_TableLayout_TopGroup.Controls.Add(Control_InventoryTab_SuggestionBox_Part, 0, 0);
            Control_InventoryTab_TableLayout_TopGroup.Controls.Add(Control_InventoryTab_Label_Qty, 0, 2);
            Control_InventoryTab_TableLayout_TopGroup.Controls.Add(Control_InventoryTab_SuggestionBox_ColorCode, 0, 5);
            Control_InventoryTab_TableLayout_TopGroup.Controls.Add(Control_InventoryTab_Label_WorkOrder, 0, 6);
            Control_InventoryTab_TableLayout_TopGroup.Controls.Add(Control_InventoryTab_TextBox_WorkOrder, 0, 7);
            Control_InventoryTab_TableLayout_TopGroup.Dock = DockStyle.Fill;
            Control_InventoryTab_TableLayout_TopGroup.Location = new Point(3, 3);
            Control_InventoryTab_TableLayout_TopGroup.Name = "Control_InventoryTab_TableLayout_TopGroup";
            Control_InventoryTab_TableLayout_TopGroup.RowCount = 8;
            Control_InventoryTab_TableLayout_TopGroup.RowStyles.Add(new RowStyle());
            Control_InventoryTab_TableLayout_TopGroup.RowStyles.Add(new RowStyle());
            Control_InventoryTab_TableLayout_TopGroup.RowStyles.Add(new RowStyle());
            Control_InventoryTab_TableLayout_TopGroup.RowStyles.Add(new RowStyle());
            Control_InventoryTab_TableLayout_TopGroup.RowStyles.Add(new RowStyle());
            Control_InventoryTab_TableLayout_TopGroup.RowStyles.Add(new RowStyle());
            Control_InventoryTab_TableLayout_TopGroup.RowStyles.Add(new RowStyle());
            Control_InventoryTab_TableLayout_TopGroup.RowStyles.Add(new RowStyle());Control_InventoryTab_TableLayout_TopGroup.Size = new Size(446, 174);
            Control_InventoryTab_TableLayout_TopGroup.TabIndex = 7;
            // 
            // Control_InventoryTab_Button_PartF4
            // 
                                                                                                                        // 
            // 
            // Control_InventoryTab_SuggestionBox_Location
            // 
            Control_InventoryTab_SuggestionBox_Location.Dock = DockStyle.Fill;
            Control_InventoryTab_SuggestionBox_Location.LabelText = "Location: ";
            Control_InventoryTab_SuggestionBox_Location.Location = new Point(3, 125);
            Control_InventoryTab_SuggestionBox_Location.Name = "Control_InventoryTab_SuggestionBox_Location";
            Control_InventoryTab_SuggestionBox_Location.PlaceholderText = "Enter or Select Location";
            Control_InventoryTab_SuggestionBox_Location.ShowF4Button = true;
            Control_InventoryTab_SuggestionBox_Location.Size = new Size(440, 29);
            Control_InventoryTab_SuggestionBox_Location.TabIndex = 4;
            // 
            // Control_InventoryTab_TextBox_Quantity
            // 
            Control_InventoryTab_TableLayout_TopGroup.SetColumnSpan(Control_InventoryTab_TextBox_Quantity, 2);
            Control_InventoryTab_TextBox_Quantity.Dock = DockStyle.Fill;
            Control_InventoryTab_TextBox_Quantity.Font = new Font("Segoe UI Emoji", 9F);
            Control_InventoryTab_TextBox_Quantity.Location = new Point(87, 61);
            Control_InventoryTab_TextBox_Quantity.Name = "Control_InventoryTab_TextBox_Quantity";
            Control_InventoryTab_TextBox_Quantity.PlaceholderText = "Enter Quantity";
            Control_InventoryTab_TextBox_Quantity.Size = new Size(356, 23);
            Control_InventoryTab_TextBox_Quantity.TabIndex = 3;
            // 
            // 
            // Control_InventoryTab_SuggestionBox_Operation
            // 
            Control_InventoryTab_SuggestionBox_Operation.Dock = DockStyle.Fill;
            Control_InventoryTab_SuggestionBox_Operation.LabelText = "Operation: ";
            Control_InventoryTab_SuggestionBox_Operation.Location = new Point(3, 38);
            Control_InventoryTab_SuggestionBox_Operation.Name = "Control_InventoryTab_SuggestionBox_Operation";
            Control_InventoryTab_SuggestionBox_Operation.PlaceholderText = "Enter or Select Operation";
            Control_InventoryTab_SuggestionBox_Operation.ShowF4Button = true;
            Control_InventoryTab_SuggestionBox_Operation.Size = new Size(440, 29);
            Control_InventoryTab_SuggestionBox_Operation.TabIndex = 2;
            // 
            // 
            // Control_InventoryTab_SuggestionBox_Part
            // 
            Control_InventoryTab_SuggestionBox_Part.Dock = DockStyle.Fill;
            Control_InventoryTab_SuggestionBox_Part.LabelText = "Part Number: ";
            Control_InventoryTab_SuggestionBox_Part.Location = new Point(3, 3);
            Control_InventoryTab_SuggestionBox_Part.Name = "Control_InventoryTab_SuggestionBox_Part";
            Control_InventoryTab_SuggestionBox_Part.PlaceholderText = "Enter or Select Part Number";
            Control_InventoryTab_SuggestionBox_Part.ShowF4Button = true;
            Control_InventoryTab_SuggestionBox_Part.Size = new Size(440, 29);
            Control_InventoryTab_SuggestionBox_Part.TabIndex = 1;
            // 
            // Control_InventoryTab_Label_Loc
            // 
                                                                                                                                    // 
            // Control_InventoryTab_Label_Qty
            // 
            Control_InventoryTab_Label_Qty.AutoSize = true;
            Control_InventoryTab_Label_Qty.Dock = DockStyle.Fill;
            Control_InventoryTab_Label_Qty.Font = new Font("Segoe UI Emoji", 9F);
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
                                                                                                                                    // 
            // Control_InventoryTab_Label_Part
            // 
                                                                                                                                    // 
            // Control_InventoryTab_Label_ColorCode
            // 
                                                                                                                                                // 
            // 
            // Control_InventoryTab_SuggestionBox_ColorCode
            // 
            Control_InventoryTab_SuggestionBox_ColorCode.Dock = DockStyle.Fill;
            Control_InventoryTab_SuggestionBox_ColorCode.LabelText = "Color Code";
            Control_InventoryTab_SuggestionBox_ColorCode.Location = new Point(3, 160);
            Control_InventoryTab_SuggestionBox_ColorCode.Name = "Control_InventoryTab_SuggestionBox_ColorCode";
            Control_InventoryTab_SuggestionBox_ColorCode.PlaceholderText = "Enter or Select Color Code";
            Control_InventoryTab_SuggestionBox_ColorCode.ShowF4Button = true;
            Control_InventoryTab_SuggestionBox_ColorCode.Size = new Size(440, 29);
            Control_InventoryTab_SuggestionBox_ColorCode.TabIndex = 5;
            Control_InventoryTab_SuggestionBox_ColorCode.Visible = false;
                        // 
            // Control_InventoryTab_Label_WorkOrder
            // 
            Control_InventoryTab_Label_WorkOrder.AutoSize = true;
            Control_InventoryTab_Label_WorkOrder.Dock = DockStyle.Fill;
            Control_InventoryTab_Label_WorkOrder.Font = new Font("Segoe UI Emoji", 9F);
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
            Control_InventoryTab_TextBox_WorkOrder.Font = new Font("Segoe UI Emoji", 9F);
            Control_InventoryTab_TextBox_WorkOrder.Location = new Point(87, 148);
            Control_InventoryTab_TextBox_WorkOrder.Name = "Control_InventoryTab_TextBox_WorkOrder";
            Control_InventoryTab_TextBox_WorkOrder.PlaceholderText = "Enter Work Order";
            Control_InventoryTab_TextBox_WorkOrder.Size = new Size(356, 23);
            Control_InventoryTab_TextBox_WorkOrder.TabIndex = 6;
            Control_InventoryTab_TextBox_WorkOrder.Visible = false;
            // 
            // Control_InventoryTab_Button_ColorF4
            // 
                                                                                                                                    // 
            // Control_InventoryTab_Button_LocationF4
            // 
                                                                                                                        // 
            // Control_InventoryTab_Button_OperationF4
            // 
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
            Control_InventoryTab_TableLayout_BottomGroup.Location = new Point(3, 238);
            Control_InventoryTab_TableLayout_BottomGroup.Name = "Control_InventoryTab_TableLayout_BottomGroup";
            Control_InventoryTab_TableLayout_BottomGroup.RowCount = 1;
            Control_InventoryTab_TableLayout_BottomGroup.RowStyles.Add(new RowStyle());
            Control_InventoryTab_TableLayout_BottomGroup.Size = new Size(446, 38);
            Control_InventoryTab_TableLayout_BottomGroup.TabIndex = 27;
            // 
            // Control_InventoryTab_Button_Toggle_RightPanel
            // 
            Control_InventoryTab_Button_Toggle_RightPanel.AutoSize = true;
            Control_InventoryTab_Button_Toggle_RightPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_InventoryTab_Button_Toggle_RightPanel.Dock = DockStyle.Fill;
            Control_InventoryTab_Button_Toggle_RightPanel.Font = new Font("Segoe UI Emoji", 9F);
            Control_InventoryTab_Button_Toggle_RightPanel.Location = new Point(411, 3);
            Control_InventoryTab_Button_Toggle_RightPanel.MaximumSize = new Size(32, 32);
            Control_InventoryTab_Button_Toggle_RightPanel.MinimumSize = new Size(32, 32);
            Control_InventoryTab_Button_Toggle_RightPanel.Name = "Control_InventoryTab_Button_Toggle_RightPanel";
            Control_InventoryTab_Button_Toggle_RightPanel.Size = new Size(32, 32);
            Control_InventoryTab_Button_Toggle_RightPanel.TabIndex = 9;
            Control_InventoryTab_Button_Toggle_RightPanel.Text = "➡";
            Control_InventoryTab_Button_Toggle_RightPanel.UseVisualStyleBackColor = true;
            Control_InventoryTab_Button_Toggle_RightPanel.Click += Control_InventoryTab_Button_Toggle_RightPanel_Click;
            // 
            // Control_InventoryTab_Label_Version
            // 
            Control_InventoryTab_Label_Version.AutoSize = true;
            Control_InventoryTab_Label_Version.Dock = DockStyle.Fill;
            Control_InventoryTab_Label_Version.Font = new Font("Segoe UI Emoji", 9F);
            Control_InventoryTab_Label_Version.Location = new Point(215, 3);
            Control_InventoryTab_Label_Version.Margin = new Padding(3);
            Control_InventoryTab_Label_Version.Name = "Control_InventoryTab_Label_Version";
            Control_InventoryTab_Label_Version.Size = new Size(84, 32);
            Control_InventoryTab_Label_Version.TabIndex = 8;
            Control_InventoryTab_Label_Version.Text = "Version: ";
            Control_InventoryTab_Label_Version.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_InventoryTab_Button_Save
            // 
            Control_InventoryTab_Button_Save.AutoSize = true;
            Control_InventoryTab_Button_Save.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_InventoryTab_Button_Save.Dock = DockStyle.Fill;
            Control_InventoryTab_Button_Save.Font = new Font("Segoe UI Emoji", 9F);
            Control_InventoryTab_Button_Save.Location = new Point(3, 3);
            Control_InventoryTab_Button_Save.MaximumSize = new Size(100, 32);
            Control_InventoryTab_Button_Save.MinimumSize = new Size(100, 32);
            Control_InventoryTab_Button_Save.Name = "Control_InventoryTab_Button_Save";
            Control_InventoryTab_Button_Save.Size = new Size(100, 32);
            Control_InventoryTab_Button_Save.TabIndex = 6;
            Control_InventoryTab_Button_Save.Text = "Save";
            Control_InventoryTab_Button_Save.UseVisualStyleBackColor = true;
            // 
            // Control_InventoryTab_Button_AdvancedEntry
            // 
            Control_InventoryTab_Button_AdvancedEntry.AutoSize = true;
            Control_InventoryTab_Button_AdvancedEntry.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_InventoryTab_Button_AdvancedEntry.Dock = DockStyle.Fill;
            Control_InventoryTab_Button_AdvancedEntry.Font = new Font("Segoe UI Emoji", 9F);
            Control_InventoryTab_Button_AdvancedEntry.ForeColor = Color.DarkRed;
            Control_InventoryTab_Button_AdvancedEntry.Location = new Point(109, 3);
            Control_InventoryTab_Button_AdvancedEntry.MaximumSize = new Size(100, 32);
            Control_InventoryTab_Button_AdvancedEntry.MinimumSize = new Size(100, 32);
            Control_InventoryTab_Button_AdvancedEntry.Name = "Control_InventoryTab_Button_AdvancedEntry";
            Control_InventoryTab_Button_AdvancedEntry.Size = new Size(100, 32);
            Control_InventoryTab_Button_AdvancedEntry.TabIndex = 8;
            Control_InventoryTab_Button_AdvancedEntry.Text = "Advanced";
            Control_InventoryTab_Button_AdvancedEntry.UseVisualStyleBackColor = true;
            // 
            // Control_InventoryTab_Button_Reset
            // 
            Control_InventoryTab_Button_Reset.AutoSize = true;
            Control_InventoryTab_Button_Reset.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_InventoryTab_Button_Reset.Dock = DockStyle.Fill;
            Control_InventoryTab_Button_Reset.Font = new Font("Segoe UI Emoji", 9F);
            Control_InventoryTab_Button_Reset.Location = new Point(305, 3);
            Control_InventoryTab_Button_Reset.MaximumSize = new Size(100, 32);
            Control_InventoryTab_Button_Reset.MinimumSize = new Size(100, 32);
            Control_InventoryTab_Button_Reset.Name = "Control_InventoryTab_Button_Reset";
            Control_InventoryTab_Button_Reset.Size = new Size(100, 32);
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
            Size = new Size(458, 301);
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
        private Label Control_InventoryTab_Label_Qty;
        private TableLayoutPanel tableLayoutPanel3;
        private Panel NotesPanel;
        private RichTextBox Control_InventoryTab_RichTextBox_Notes;
        }

        
        #endregion
    }
