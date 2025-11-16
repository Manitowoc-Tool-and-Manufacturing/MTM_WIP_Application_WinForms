using System.Drawing;
using System.Windows.Forms;
using MTM_WIP_Application_Winforms.Controls.Shared;

namespace MTM_WIP_Application_Winforms.Controls.MainForm
{
    partial class Control_RemoveTab
    {
        #region Fields
        


        private System.ComponentModel.IContainer components = null;

        #endregion

        private GroupBox Control_RemoveTab_GroupBox_MainControl;
        internal SuggestionTextBox Control_RemoveTab_TextBox_Part;
        private Label Control_RemoveTab_Label_Part;
        private Label Control_RemoveTab_Label_Operation;
        private SuggestionTextBox Control_RemoveTab_TextBox_Operation;
        private Panel Control_RemoveTab_Panel_DataGridView;
        private PictureBox Control_RemoveTab_Image_NothingFound;
        private DataGridView Control_RemoveTab_DataGridView_Main;
        private ContextMenuStrip Control_RemoveTab_ContextMenu_DataGridView;
        private ToolStripMenuItem Control_RemoveTab_ContextMenuItem_Print;
        private Button Control_RemoveTab_Button_AdvancedItemRemoval;
        private Button Control_RemoveTab_Button_Reset;
        private Button Control_RemoveTab_Button_Delete;
        private Button Control_RemoveTab_Button_Search;
        private Button Control_RemoveTab_Button_Toggle_RightPanel;
        private TableLayoutPanel Control_RemoveTab_Panel_Main;
        private Button Control_RemoveTab_Button_Undo;
        

        
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
            Control_RemoveTab_GroupBox_MainControl = new GroupBox();
            Control_RemoveTab_Panel_Main = new TableLayoutPanel();
            Control_RemoveTab_Panel_DataGridView = new Panel();
            Control_RemoveTab_Image_NothingFound = new PictureBox();
            Control_RemoveTab_DataGridView_Main = new DataGridView();
            Control_RemoveTab_ContextMenu_DataGridView = new ContextMenuStrip(components);
            Control_RemoveTab_ContextMenuItem_Print = new ToolStripMenuItem();
            Control_RemoveTab_Panel_Header = new Panel();
            Control_RemoveTab_TableLayout_Top = new TableLayoutPanel();
            Control_RemoveTab_Label_Part = new Label();
            Control_RemoveTab_TextBox_Part = new SuggestionTextBox();
            Control_RemoveTab_Label_Operation = new Label();
            Control_RemoveTab_TextBox_Operation = new SuggestionTextBox();
            Control_RemoveTab_TableLayout_Bottom = new TableLayoutPanel();
            Control_RemoveTab_Button_ShowAll = new Button();
            Control_RemoveTab_Button_AdvancedItemRemoval = new Button();
            Control_RemoveTab_Button_Delete = new Button();
            Control_RemoveTab_Button_Search = new Button();
            Control_RemoveTab_Button_Toggle_RightPanel = new Button();
            Control_RemoveTab_Button_Reset = new Button();
            Control_RemoveTab_Button_Print = new Button();
            Control_RemoveTab_Button_Undo = new Button();
            RemoveTab_Single_Button_PartF4 = new Button();
            RemoveTab_Single_Button_OperationF4 = new Button();
            Control_RemoveTab_GroupBox_MainControl.SuspendLayout();
            Control_RemoveTab_Panel_Main.SuspendLayout();
            Control_RemoveTab_Panel_DataGridView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Control_RemoveTab_Image_NothingFound).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Control_RemoveTab_DataGridView_Main).BeginInit();
            Control_RemoveTab_ContextMenu_DataGridView.SuspendLayout();
            Control_RemoveTab_Panel_Header.SuspendLayout();
            Control_RemoveTab_TableLayout_Top.SuspendLayout();
            Control_RemoveTab_TableLayout_Bottom.SuspendLayout();
            SuspendLayout();
            // 
            // Control_RemoveTab_GroupBox_MainControl
            // 
            Control_RemoveTab_GroupBox_MainControl.AutoSize = true;
            Control_RemoveTab_GroupBox_MainControl.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_RemoveTab_GroupBox_MainControl.Controls.Add(Control_RemoveTab_Panel_Main);
            Control_RemoveTab_GroupBox_MainControl.Dock = DockStyle.Fill;
            Control_RemoveTab_GroupBox_MainControl.FlatStyle = FlatStyle.Flat;
            Control_RemoveTab_GroupBox_MainControl.Location = new Point(0, 0);
            Control_RemoveTab_GroupBox_MainControl.Name = "Control_RemoveTab_GroupBox_MainControl";
            Control_RemoveTab_GroupBox_MainControl.Size = new Size(800, 375);
            Control_RemoveTab_GroupBox_MainControl.TabIndex = 17;
            Control_RemoveTab_GroupBox_MainControl.TabStop = false;
            Control_RemoveTab_GroupBox_MainControl.Text = "Part Lookup and Remove";
            // 
            // Control_RemoveTab_Panel_Main
            // 
            Control_RemoveTab_Panel_Main.AutoSize = true;
            Control_RemoveTab_Panel_Main.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_RemoveTab_Panel_Main.ColumnCount = 1;
            Control_RemoveTab_Panel_Main.ColumnStyles.Add(new ColumnStyle());
            Control_RemoveTab_Panel_Main.Controls.Add(Control_RemoveTab_Panel_DataGridView, 0, 1);
            Control_RemoveTab_Panel_Main.Controls.Add(Control_RemoveTab_Panel_Header, 0, 0);
            Control_RemoveTab_Panel_Main.Controls.Add(Control_RemoveTab_TableLayout_Bottom, 0, 2);
            Control_RemoveTab_Panel_Main.Dock = DockStyle.Fill;
            Control_RemoveTab_Panel_Main.Location = new Point(3, 19);
            Control_RemoveTab_Panel_Main.Name = "Control_RemoveTab_Panel_Main";
            Control_RemoveTab_Panel_Main.RowCount = 3;
            Control_RemoveTab_Panel_Main.RowStyles.Add(new RowStyle());
            Control_RemoveTab_Panel_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_RemoveTab_Panel_Main.RowStyles.Add(new RowStyle());
            Control_RemoveTab_Panel_Main.Size = new Size(794, 353);
            Control_RemoveTab_Panel_Main.TabIndex = 0;
            // 
            // Control_RemoveTab_Panel_DataGridView
            // 
            Control_RemoveTab_Panel_DataGridView.AutoSize = true;
            Control_RemoveTab_Panel_DataGridView.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_RemoveTab_Panel_DataGridView.Controls.Add(Control_RemoveTab_Image_NothingFound);
            Control_RemoveTab_Panel_DataGridView.Controls.Add(Control_RemoveTab_DataGridView_Main);
            Control_RemoveTab_Panel_DataGridView.Dock = DockStyle.Fill;
            Control_RemoveTab_Panel_DataGridView.Location = new Point(3, 38);
            Control_RemoveTab_Panel_DataGridView.Name = "Control_RemoveTab_Panel_DataGridView";
            Control_RemoveTab_Panel_DataGridView.Size = new Size(788, 271);
            Control_RemoveTab_Panel_DataGridView.TabIndex = 21;
            // 
            // Control_RemoveTab_Image_NothingFound
            // 
            Control_RemoveTab_Image_NothingFound.BackgroundImageLayout = ImageLayout.Zoom;
            Control_RemoveTab_Image_NothingFound.BorderStyle = BorderStyle.FixedSingle;
            Control_RemoveTab_Image_NothingFound.Dock = DockStyle.Fill;
            Control_RemoveTab_Image_NothingFound.ErrorImage = null;
            Control_RemoveTab_Image_NothingFound.Image = Properties.Resources.NothingFound;
            Control_RemoveTab_Image_NothingFound.InitialImage = null;
            Control_RemoveTab_Image_NothingFound.Location = new Point(0, 0);
            Control_RemoveTab_Image_NothingFound.Name = "Control_RemoveTab_Image_NothingFound";
            Control_RemoveTab_Image_NothingFound.Size = new Size(788, 271);
            Control_RemoveTab_Image_NothingFound.SizeMode = PictureBoxSizeMode.CenterImage;
            Control_RemoveTab_Image_NothingFound.TabIndex = 6;
            Control_RemoveTab_Image_NothingFound.TabStop = false;
            Control_RemoveTab_Image_NothingFound.Visible = false;
            // 
            // Control_RemoveTab_DataGridView_Main
            // 
            Control_RemoveTab_DataGridView_Main.AllowUserToAddRows = false;
            Control_RemoveTab_DataGridView_Main.AllowUserToDeleteRows = false;
            Control_RemoveTab_DataGridView_Main.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Control_RemoveTab_DataGridView_Main.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            Control_RemoveTab_DataGridView_Main.BorderStyle = BorderStyle.Fixed3D;
            Control_RemoveTab_DataGridView_Main.CellBorderStyle = DataGridViewCellBorderStyle.Sunken;
            Control_RemoveTab_DataGridView_Main.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
            Control_RemoveTab_DataGridView_Main.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Sunken;
            Control_RemoveTab_DataGridView_Main.ColumnHeadersHeight = 34;
            Control_RemoveTab_DataGridView_Main.ContextMenuStrip = Control_RemoveTab_ContextMenu_DataGridView;
            Control_RemoveTab_DataGridView_Main.Dock = DockStyle.Fill;
            Control_RemoveTab_DataGridView_Main.EditMode = DataGridViewEditMode.EditProgrammatically;
            Control_RemoveTab_DataGridView_Main.Location = new Point(0, 0);
            Control_RemoveTab_DataGridView_Main.Name = "Control_RemoveTab_DataGridView_Main";
            Control_RemoveTab_DataGridView_Main.ReadOnly = true;
            Control_RemoveTab_DataGridView_Main.RowHeadersWidth = 62;
            Control_RemoveTab_DataGridView_Main.RowTemplate.ReadOnly = true;
            Control_RemoveTab_DataGridView_Main.RowTemplate.Resizable = DataGridViewTriState.True;
            Control_RemoveTab_DataGridView_Main.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Control_RemoveTab_DataGridView_Main.ShowCellErrors = false;
            Control_RemoveTab_DataGridView_Main.ShowCellToolTips = false;
            Control_RemoveTab_DataGridView_Main.ShowEditingIcon = false;
            Control_RemoveTab_DataGridView_Main.ShowRowErrors = false;
            Control_RemoveTab_DataGridView_Main.Size = new Size(788, 271);
            Control_RemoveTab_DataGridView_Main.StandardTab = true;
            Control_RemoveTab_DataGridView_Main.TabIndex = 4;
            // 
            // Control_RemoveTab_ContextMenu_DataGridView
            // 
            Control_RemoveTab_ContextMenu_DataGridView.Items.AddRange(new ToolStripItem[] { Control_RemoveTab_ContextMenuItem_Print });
            Control_RemoveTab_ContextMenu_DataGridView.Name = "Control_RemoveTab_ContextMenu_DataGridView";
            Control_RemoveTab_ContextMenu_DataGridView.Size = new Size(109, 26);
            // 
            // Control_RemoveTab_ContextMenuItem_Print
            // 
            Control_RemoveTab_ContextMenuItem_Print.Name = "Control_RemoveTab_ContextMenuItem_Print";
            Control_RemoveTab_ContextMenuItem_Print.Size = new Size(108, 22);
            Control_RemoveTab_ContextMenuItem_Print.Text = "&Print...";
            Control_RemoveTab_ContextMenuItem_Print.Click += Control_RemoveTab_ContextMenuItem_Print_Click;
            // 
            // Control_RemoveTab_Panel_Header
            // 
            Control_RemoveTab_Panel_Header.AutoSize = true;
            Control_RemoveTab_Panel_Header.Controls.Add(Control_RemoveTab_TableLayout_Top);
            Control_RemoveTab_Panel_Header.Dock = DockStyle.Fill;
            Control_RemoveTab_Panel_Header.Location = new Point(3, 3);
            Control_RemoveTab_Panel_Header.Name = "Control_RemoveTab_Panel_Header";
            Control_RemoveTab_Panel_Header.Size = new Size(788, 29);
            Control_RemoveTab_Panel_Header.TabIndex = 22;
            // 
            // Control_RemoveTab_TableLayout_Top
            // 
            Control_RemoveTab_TableLayout_Top.AutoSize = true;
            Control_RemoveTab_TableLayout_Top.ColumnCount = 7;
            Control_RemoveTab_TableLayout_Top.ColumnStyles.Add(new ColumnStyle());
            Control_RemoveTab_TableLayout_Top.ColumnStyles.Add(new ColumnStyle());
            Control_RemoveTab_TableLayout_Top.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30F));
            Control_RemoveTab_TableLayout_Top.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_RemoveTab_TableLayout_Top.ColumnStyles.Add(new ColumnStyle());
            Control_RemoveTab_TableLayout_Top.ColumnStyles.Add(new ColumnStyle());
            Control_RemoveTab_TableLayout_Top.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30F));
            Control_RemoveTab_TableLayout_Top.Controls.Add(RemoveTab_Single_Button_PartF4, 2, 0);
            Control_RemoveTab_TableLayout_Top.Controls.Add(Control_RemoveTab_Label_Part, 0, 0);
            Control_RemoveTab_TableLayout_Top.Controls.Add(Control_RemoveTab_TextBox_Part, 1, 0);
            Control_RemoveTab_TableLayout_Top.Controls.Add(Control_RemoveTab_TextBox_Operation, 5, 0);
            Control_RemoveTab_TableLayout_Top.Controls.Add(Control_RemoveTab_Label_Operation, 4, 0);
            Control_RemoveTab_TableLayout_Top.Controls.Add(RemoveTab_Single_Button_OperationF4, 6, 0);
            Control_RemoveTab_TableLayout_Top.Dock = DockStyle.Fill;
            Control_RemoveTab_TableLayout_Top.Location = new Point(0, 0);
            Control_RemoveTab_TableLayout_Top.Name = "Control_RemoveTab_TableLayout_Top";
            Control_RemoveTab_TableLayout_Top.RowCount = 1;
            Control_RemoveTab_TableLayout_Top.RowStyles.Add(new RowStyle());
            Control_RemoveTab_TableLayout_Top.Size = new Size(788, 29);
            Control_RemoveTab_TableLayout_Top.TabIndex = 6;
            // 
            // Control_RemoveTab_Label_Part
            // 
            Control_RemoveTab_Label_Part.AutoEllipsis = true;
            Control_RemoveTab_Label_Part.AutoSize = true;
            Control_RemoveTab_Label_Part.Dock = DockStyle.Fill;
            Control_RemoveTab_Label_Part.Location = new Point(3, 3);
            Control_RemoveTab_Label_Part.Margin = new Padding(3);
            Control_RemoveTab_Label_Part.Name = "Control_RemoveTab_Label_Part";
            Control_RemoveTab_Label_Part.Size = new Size(78, 23);
            Control_RemoveTab_Label_Part.TabIndex = 4;
            Control_RemoveTab_Label_Part.Text = "Part Number:";
            Control_RemoveTab_Label_Part.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Control_RemoveTab_TextBox_Part
            // 
            Control_RemoveTab_TextBox_Part.Dock = DockStyle.Fill;
            Control_RemoveTab_TextBox_Part.Location = new Point(87, 3);
            Control_RemoveTab_TextBox_Part.Name = "Control_RemoveTab_TextBox_Part";
            Control_RemoveTab_TextBox_Part.PlaceholderText = "Enter or Select Part Number";
            Control_RemoveTab_TextBox_Part.Size = new Size(180, 23);
            Control_RemoveTab_TextBox_Part.TabIndex = 1;
            // 
            // Control_RemoveTab_Label_Operation
            // 
            Control_RemoveTab_Label_Operation.AutoEllipsis = true;
            Control_RemoveTab_Label_Operation.AutoSize = true;
            Control_RemoveTab_Label_Operation.Dock = DockStyle.Fill;
            Control_RemoveTab_Label_Operation.Location = new Point(506, 3);
            Control_RemoveTab_Label_Operation.Margin = new Padding(3);
            Control_RemoveTab_Label_Operation.Name = "Control_RemoveTab_Label_Operation";
            Control_RemoveTab_Label_Operation.Size = new Size(63, 23);
            Control_RemoveTab_Label_Operation.TabIndex = 5;
            Control_RemoveTab_Label_Operation.Text = "Operation:";
            Control_RemoveTab_Label_Operation.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Control_RemoveTab_TextBox_Operation
            // 
            Control_RemoveTab_TextBox_Operation.Dock = DockStyle.Fill;
            Control_RemoveTab_TextBox_Operation.Location = new Point(575, 3);
            Control_RemoveTab_TextBox_Operation.Name = "Control_RemoveTab_TextBox_Operation";
            Control_RemoveTab_TextBox_Operation.PlaceholderText = "Enter or Select Operation";
            Control_RemoveTab_TextBox_Operation.Size = new Size(180, 23);
            Control_RemoveTab_TextBox_Operation.TabIndex = 2;
            // 
            // Control_RemoveTab_TableLayout_Bottom
            // 
            Control_RemoveTab_TableLayout_Bottom.AutoSize = true;
            Control_RemoveTab_TableLayout_Bottom.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_RemoveTab_TableLayout_Bottom.ColumnCount = 9;
            Control_RemoveTab_TableLayout_Bottom.ColumnStyles.Add(new ColumnStyle());
            Control_RemoveTab_TableLayout_Bottom.ColumnStyles.Add(new ColumnStyle());
            Control_RemoveTab_TableLayout_Bottom.ColumnStyles.Add(new ColumnStyle());
            Control_RemoveTab_TableLayout_Bottom.ColumnStyles.Add(new ColumnStyle());
            Control_RemoveTab_TableLayout_Bottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_RemoveTab_TableLayout_Bottom.ColumnStyles.Add(new ColumnStyle());
            Control_RemoveTab_TableLayout_Bottom.ColumnStyles.Add(new ColumnStyle());
            Control_RemoveTab_TableLayout_Bottom.ColumnStyles.Add(new ColumnStyle());
            Control_RemoveTab_TableLayout_Bottom.ColumnStyles.Add(new ColumnStyle());
            Control_RemoveTab_TableLayout_Bottom.Controls.Add(Control_RemoveTab_Button_ShowAll, 3, 0);
            Control_RemoveTab_TableLayout_Bottom.Controls.Add(Control_RemoveTab_Button_AdvancedItemRemoval, 2, 0);
            Control_RemoveTab_TableLayout_Bottom.Controls.Add(Control_RemoveTab_Button_Delete, 1, 0);
            Control_RemoveTab_TableLayout_Bottom.Controls.Add(Control_RemoveTab_Button_Search, 0, 0);
            Control_RemoveTab_TableLayout_Bottom.Controls.Add(Control_RemoveTab_Button_Toggle_RightPanel, 8, 0);
            Control_RemoveTab_TableLayout_Bottom.Controls.Add(Control_RemoveTab_Button_Reset, 7, 0);
            Control_RemoveTab_TableLayout_Bottom.Controls.Add(Control_RemoveTab_Button_Print, 6, 0);
            Control_RemoveTab_TableLayout_Bottom.Controls.Add(Control_RemoveTab_Button_Undo, 5, 0);
            Control_RemoveTab_TableLayout_Bottom.Dock = DockStyle.Fill;
            Control_RemoveTab_TableLayout_Bottom.Location = new Point(3, 315);
            Control_RemoveTab_TableLayout_Bottom.Name = "Control_RemoveTab_TableLayout_Bottom";
            Control_RemoveTab_TableLayout_Bottom.RowCount = 1;
            Control_RemoveTab_TableLayout_Bottom.RowStyles.Add(new RowStyle());
            Control_RemoveTab_TableLayout_Bottom.Size = new Size(788, 35);
            Control_RemoveTab_TableLayout_Bottom.TabIndex = 23;
            // 
            // Control_RemoveTab_Button_ShowAll
            // 
            Control_RemoveTab_Button_ShowAll.AutoSize = true;
            Control_RemoveTab_Button_ShowAll.Dock = DockStyle.Fill;
            Control_RemoveTab_Button_ShowAll.Location = new Point(270, 3);
            Control_RemoveTab_Button_ShowAll.Name = "Control_RemoveTab_Button_ShowAll";
            Control_RemoveTab_Button_ShowAll.Size = new Size(83, 29);
            Control_RemoveTab_Button_ShowAll.TabIndex = 10;
            Control_RemoveTab_Button_ShowAll.TabStop = false;
            Control_RemoveTab_Button_ShowAll.Text = "Show All";
            Control_RemoveTab_Button_ShowAll.UseVisualStyleBackColor = true;
            Control_RemoveTab_Button_ShowAll.Click += Control_RemoveTab_Button_ShowAll_Click;
            // 
            // Control_RemoveTab_Button_AdvancedItemRemoval
            // 
            Control_RemoveTab_Button_AdvancedItemRemoval.AutoSize = true;
            Control_RemoveTab_Button_AdvancedItemRemoval.Dock = DockStyle.Fill;
            Control_RemoveTab_Button_AdvancedItemRemoval.ForeColor = Color.DarkRed;
            Control_RemoveTab_Button_AdvancedItemRemoval.Location = new Point(181, 3);
            Control_RemoveTab_Button_AdvancedItemRemoval.Name = "Control_RemoveTab_Button_AdvancedItemRemoval";
            Control_RemoveTab_Button_AdvancedItemRemoval.Size = new Size(83, 29);
            Control_RemoveTab_Button_AdvancedItemRemoval.TabIndex = 5;
            Control_RemoveTab_Button_AdvancedItemRemoval.Text = "Advanced";
            Control_RemoveTab_Button_AdvancedItemRemoval.UseVisualStyleBackColor = true;
            // 
            // Control_RemoveTab_Button_Delete
            // 
            Control_RemoveTab_Button_Delete.AutoSize = true;
            Control_RemoveTab_Button_Delete.Dock = DockStyle.Fill;
            Control_RemoveTab_Button_Delete.Location = new Point(92, 3);
            Control_RemoveTab_Button_Delete.Name = "Control_RemoveTab_Button_Delete";
            Control_RemoveTab_Button_Delete.Size = new Size(83, 29);
            Control_RemoveTab_Button_Delete.TabIndex = 4;
            Control_RemoveTab_Button_Delete.Text = "Delete";
            Control_RemoveTab_Button_Delete.UseVisualStyleBackColor = true;
            // 
            // Control_RemoveTab_Button_Search
            // 
            Control_RemoveTab_Button_Search.AutoSize = true;
            Control_RemoveTab_Button_Search.Dock = DockStyle.Fill;
            Control_RemoveTab_Button_Search.Location = new Point(3, 3);
            Control_RemoveTab_Button_Search.Name = "Control_RemoveTab_Button_Search";
            Control_RemoveTab_Button_Search.Size = new Size(83, 29);
            Control_RemoveTab_Button_Search.TabIndex = 3;
            Control_RemoveTab_Button_Search.Text = "Search";
            Control_RemoveTab_Button_Search.UseVisualStyleBackColor = true;
            Control_RemoveTab_Button_Search.Click += Control_RemoveTab_Button_Search_Click;
            // 
            // Control_RemoveTab_Button_Toggle_RightPanel
            // 
            Control_RemoveTab_Button_Toggle_RightPanel.Dock = DockStyle.Fill;
            Control_RemoveTab_Button_Toggle_RightPanel.Font = new Font("Segoe UI Emoji", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Control_RemoveTab_Button_Toggle_RightPanel.Location = new Point(754, 3);
            Control_RemoveTab_Button_Toggle_RightPanel.Name = "Control_RemoveTab_Button_Toggle_RightPanel";
            Control_RemoveTab_Button_Toggle_RightPanel.Size = new Size(31, 29);
            Control_RemoveTab_Button_Toggle_RightPanel.TabIndex = 9;
            Control_RemoveTab_Button_Toggle_RightPanel.Text = "➡";
            Control_RemoveTab_Button_Toggle_RightPanel.UseVisualStyleBackColor = true;
            Control_RemoveTab_Button_Toggle_RightPanel.Click += Control_RemoveTab_Button_Toggle_RightPanel_Click;
            // 
            // Control_RemoveTab_Button_Reset
            // 
            Control_RemoveTab_Button_Reset.AutoSize = true;
            Control_RemoveTab_Button_Reset.Dock = DockStyle.Fill;
            Control_RemoveTab_Button_Reset.Location = new Point(658, 3);
            Control_RemoveTab_Button_Reset.Name = "Control_RemoveTab_Button_Reset";
            Control_RemoveTab_Button_Reset.Size = new Size(90, 29);
            Control_RemoveTab_Button_Reset.TabIndex = 8;
            Control_RemoveTab_Button_Reset.TabStop = false;
            Control_RemoveTab_Button_Reset.Text = "Reset";
            Control_RemoveTab_Button_Reset.UseVisualStyleBackColor = true;
            // 
            // Control_RemoveTab_Button_Print
            // 
            Control_RemoveTab_Button_Print.AutoSize = true;
            Control_RemoveTab_Button_Print.Dock = DockStyle.Fill;
            Control_RemoveTab_Button_Print.Enabled = false;
            Control_RemoveTab_Button_Print.Location = new Point(562, 3);
            Control_RemoveTab_Button_Print.Name = "Control_RemoveTab_Button_Print";
            Control_RemoveTab_Button_Print.Size = new Size(90, 29);
            Control_RemoveTab_Button_Print.TabIndex = 7;
            Control_RemoveTab_Button_Print.Text = "Print";
            Control_RemoveTab_Button_Print.UseVisualStyleBackColor = true;
            // 
            // Control_RemoveTab_Button_Undo
            // 
            Control_RemoveTab_Button_Undo.AutoSize = true;
            Control_RemoveTab_Button_Undo.Dock = DockStyle.Fill;
            Control_RemoveTab_Button_Undo.Enabled = false;
            Control_RemoveTab_Button_Undo.Location = new Point(466, 3);
            Control_RemoveTab_Button_Undo.Name = "Control_RemoveTab_Button_Undo";
            Control_RemoveTab_Button_Undo.Size = new Size(90, 29);
            Control_RemoveTab_Button_Undo.TabIndex = 6;
            Control_RemoveTab_Button_Undo.TabStop = false;
            Control_RemoveTab_Button_Undo.Text = "Undo";
            Control_RemoveTab_Button_Undo.UseVisualStyleBackColor = true;
            Control_RemoveTab_Button_Undo.Click += Control_RemoveTab_Button_Undo_Click;
            // 
            // RemoveTab_Single_Button_PartF4
            // 
            RemoveTab_Single_Button_PartF4.Location = new Point(273, 3);
            RemoveTab_Single_Button_PartF4.Name = "RemoveTab_Single_Button_PartF4";
            RemoveTab_Single_Button_PartF4.Size = new Size(23, 23);
            RemoveTab_Single_Button_PartF4.TabIndex = 14;
            RemoveTab_Single_Button_PartF4.Text = "🔎";
            RemoveTab_Single_Button_PartF4.UseVisualStyleBackColor = true;
            // 
            // RemoveTab_Single_Button_OperationF4
            // 
            RemoveTab_Single_Button_OperationF4.Location = new Point(761, 3);
            RemoveTab_Single_Button_OperationF4.Name = "RemoveTab_Single_Button_OperationF4";
            RemoveTab_Single_Button_OperationF4.Size = new Size(23, 23);
            RemoveTab_Single_Button_OperationF4.TabIndex = 15;
            RemoveTab_Single_Button_OperationF4.Text = "🔎";
            RemoveTab_Single_Button_OperationF4.UseVisualStyleBackColor = true;
            // 
            // Control_RemoveTab
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(Control_RemoveTab_GroupBox_MainControl);
            MaximumSize = new Size(800, 375);
            MinimumSize = new Size(800, 375);
            Name = "Control_RemoveTab";
            Size = new Size(800, 375);
            Control_RemoveTab_GroupBox_MainControl.ResumeLayout(false);
            Control_RemoveTab_GroupBox_MainControl.PerformLayout();
            Control_RemoveTab_Panel_Main.ResumeLayout(false);
            Control_RemoveTab_Panel_Main.PerformLayout();
            Control_RemoveTab_Panel_DataGridView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)Control_RemoveTab_Image_NothingFound).EndInit();
            ((System.ComponentModel.ISupportInitialize)Control_RemoveTab_DataGridView_Main).EndInit();
            Control_RemoveTab_ContextMenu_DataGridView.ResumeLayout(false);
            Control_RemoveTab_Panel_Header.ResumeLayout(false);
            Control_RemoveTab_Panel_Header.PerformLayout();
            Control_RemoveTab_TableLayout_Top.ResumeLayout(false);
            Control_RemoveTab_TableLayout_Top.PerformLayout();
            Control_RemoveTab_TableLayout_Bottom.ResumeLayout(false);
            Control_RemoveTab_TableLayout_Bottom.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
        private Button Control_RemoveTab_Button_Print;
        private TableLayoutPanel Control_RemoveTab_TableLayout_Bottom;
        private Panel Control_RemoveTab_Panel_Header;
        private TableLayoutPanel Control_RemoveTab_TableLayout_Top;
        private Button Control_RemoveTab_Button_ShowAll;
        private Button RemoveTab_Single_Button_PartF4;
        private Button RemoveTab_Single_Button_OperationF4;
    }

        
        #endregion
    }
