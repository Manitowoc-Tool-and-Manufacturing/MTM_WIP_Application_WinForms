namespace MTM_WIP_Application_Winforms.Controls.MainForm.Control_AdvancedInventory
{
    partial class Control_AdvInv_Main
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
            Button_SwitchToClassic = new Button();
            Label_StepTitle = new Label();
            Label_Step1 = new Label();
            Label_Step2 = new Label();
            Label_Step3 = new Label();
            Label_Step4 = new Label();
            Label_CurrentStep = new Label();
            Label_SelectMode = new Label();
            Panel_SingleTransaction = new Panel();
            TableLayout_SingleTransaction = new TableLayoutPanel();
            Button_SelectSingleTransaction = new Button();
            Label_SingleTransaction_BestFor = new Label();
            Label_SingleTransaction_Desc = new Label();
            Label_SingleTransaction_Title = new Label();
            Label_BatchEntry_Title = new Label();
            Label_BatchEntry_Desc = new Label();
            Label_BatchEntry_BestFor = new Label();
            Button_SelectBatchEntry = new Button();
            Label_ExcelImport_Title = new Label();
            Label_ExcelImport_Desc = new Label();
            Label_ExcelImport_BestFor = new Label();
            Button_SelectExcelImport = new Button();
            Label_Templates_Title = new Label();
            Label_Templates_Desc = new Label();
            Label_Templates_BestFor = new Label();
            Button_SelectTemplate = new Button();
            Button_Back = new Button();
            Button_Next = new Button();
            TableLayout_Main = new TableLayoutPanel();
            TabelLayout_StepNavigation = new TableLayoutPanel();
            TableLayout_BottomNavigation = new TableLayoutPanel();
            TableLayout_ModeSelection = new TableLayoutPanel();
            Panel_Templates = new Panel();
            TableLayout_Templates = new TableLayoutPanel();
            Panel_ExcelImport = new Panel();
            TableLayout_ExcelImport = new TableLayoutPanel();
            Panel_BatchEntry = new Panel();
            TableLayout_BatchEntry = new TableLayoutPanel();
            Panel_SingleTransaction.SuspendLayout();
            TableLayout_SingleTransaction.SuspendLayout();
            TableLayout_Main.SuspendLayout();
            TabelLayout_StepNavigation.SuspendLayout();
            TableLayout_BottomNavigation.SuspendLayout();
            TableLayout_ModeSelection.SuspendLayout();
            Panel_Templates.SuspendLayout();
            TableLayout_Templates.SuspendLayout();
            Panel_ExcelImport.SuspendLayout();
            TableLayout_ExcelImport.SuspendLayout();
            Panel_BatchEntry.SuspendLayout();
            TableLayout_BatchEntry.SuspendLayout();
            SuspendLayout();
            // 
            // Button_SwitchToClassic
            // 
            Button_SwitchToClassic.AutoSize = true;
            Button_SwitchToClassic.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Button_SwitchToClassic.Font = new Font("Segoe UI", 11F);
            Button_SwitchToClassic.Location = new Point(552, 3);
            Button_SwitchToClassic.Name = "Button_SwitchToClassic";
            Button_SwitchToClassic.Size = new Size(171, 30);
            Button_SwitchToClassic.TabIndex = 3;
            Button_SwitchToClassic.Text = "Switch to Classic Mode";
            Button_SwitchToClassic.UseVisualStyleBackColor = true;
            // 
            // Label_StepTitle
            // 
            Label_StepTitle.AutoSize = true;
            TabelLayout_StepNavigation.SetColumnSpan(Label_StepTitle, 9);
            Label_StepTitle.Dock = DockStyle.Fill;
            Label_StepTitle.Font = new Font("Segoe UI Emoji", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label_StepTitle.Location = new Point(3, 3);
            Label_StepTitle.Margin = new Padding(3);
            Label_StepTitle.Name = "Label_StepTitle";
            Label_StepTitle.Size = new Size(788, 16);
            Label_StepTitle.TabIndex = 0;
            Label_StepTitle.Text = "Step Navigation";
            Label_StepTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label_Step1
            // 
            Label_Step1.AutoSize = true;
            Label_Step1.Dock = DockStyle.Fill;
            Label_Step1.Font = new Font("Segoe UI Emoji", 9F);
            Label_Step1.Location = new Point(105, 25);
            Label_Step1.Margin = new Padding(3);
            Label_Step1.Name = "Label_Step1";
            Label_Step1.Size = new Size(81, 16);
            Label_Step1.TabIndex = 1;
            Label_Step1.Text = "● Entry Mode";
            Label_Step1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label_Step2
            // 
            Label_Step2.AutoSize = true;
            Label_Step2.Dock = DockStyle.Fill;
            Label_Step2.Font = new Font("Segoe UI Emoji", 9F);
            Label_Step2.Location = new Point(294, 25);
            Label_Step2.Margin = new Padding(3);
            Label_Step2.Name = "Label_Step2";
            Label_Step2.Size = new Size(74, 16);
            Label_Step2.TabIndex = 2;
            Label_Step2.Text = "○ Data Entry";
            Label_Step2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label_Step3
            // 
            Label_Step3.AutoSize = true;
            Label_Step3.Dock = DockStyle.Fill;
            Label_Step3.Font = new Font("Segoe UI Emoji", 9F);
            Label_Step3.Location = new Point(476, 25);
            Label_Step3.Margin = new Padding(3);
            Label_Step3.Name = "Label_Step3";
            Label_Step3.Size = new Size(57, 16);
            Label_Step3.TabIndex = 3;
            Label_Step3.Text = "○ Review";
            Label_Step3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label_Step4
            // 
            Label_Step4.AutoSize = true;
            Label_Step4.Dock = DockStyle.Fill;
            Label_Step4.Font = new Font("Segoe UI Emoji", 9F);
            Label_Step4.Location = new Point(641, 25);
            Label_Step4.Margin = new Padding(3);
            Label_Step4.Name = "Label_Step4";
            Label_Step4.Size = new Size(44, 16);
            Label_Step4.TabIndex = 4;
            Label_Step4.Text = "○ Save";
            Label_Step4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label_CurrentStep
            // 
            Label_CurrentStep.AutoSize = true;
            TabelLayout_StepNavigation.SetColumnSpan(Label_CurrentStep, 9);
            Label_CurrentStep.Dock = DockStyle.Fill;
            Label_CurrentStep.Font = new Font("Segoe UI Emoji", 9F);
            Label_CurrentStep.Location = new Point(3, 47);
            Label_CurrentStep.Margin = new Padding(3);
            Label_CurrentStep.Name = "Label_CurrentStep";
            Label_CurrentStep.Size = new Size(788, 16);
            Label_CurrentStep.TabIndex = 5;
            Label_CurrentStep.Text = "Current: Step 1 - Choose Entry Mode";
            Label_CurrentStep.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Label_SelectMode
            // 
            Label_SelectMode.AutoSize = true;
            TableLayout_ModeSelection.SetColumnSpan(Label_SelectMode, 4);
            Label_SelectMode.Dock = DockStyle.Fill;
            Label_SelectMode.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            Label_SelectMode.Location = new Point(3, 3);
            Label_SelectMode.Margin = new Padding(3);
            Label_SelectMode.Name = "Label_SelectMode";
            Label_SelectMode.Size = new Size(788, 21);
            Label_SelectMode.TabIndex = 0;
            Label_SelectMode.Text = "Select How You Want to Inventory";
            Label_SelectMode.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Panel_SingleTransaction
            // 
            Panel_SingleTransaction.AutoSize = true;
            Panel_SingleTransaction.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Panel_SingleTransaction.BorderStyle = BorderStyle.FixedSingle;
            Panel_SingleTransaction.Controls.Add(TableLayout_SingleTransaction);
            Panel_SingleTransaction.Dock = DockStyle.Fill;
            Panel_SingleTransaction.Location = new Point(3, 30);
            Panel_SingleTransaction.Name = "Panel_SingleTransaction";
            Panel_SingleTransaction.Size = new Size(192, 222);
            Panel_SingleTransaction.TabIndex = 1;
            // 
            // TableLayout_SingleTransaction
            // 
            TableLayout_SingleTransaction.AutoSize = true;
            TableLayout_SingleTransaction.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TableLayout_SingleTransaction.ColumnCount = 3;
            TableLayout_SingleTransaction.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            TableLayout_SingleTransaction.ColumnStyles.Add(new ColumnStyle());
            TableLayout_SingleTransaction.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            TableLayout_SingleTransaction.Controls.Add(Button_SelectSingleTransaction, 1, 3);
            TableLayout_SingleTransaction.Controls.Add(Label_SingleTransaction_BestFor, 0, 2);
            TableLayout_SingleTransaction.Controls.Add(Label_SingleTransaction_Desc, 0, 1);
            TableLayout_SingleTransaction.Controls.Add(Label_SingleTransaction_Title, 0, 0);
            TableLayout_SingleTransaction.Dock = DockStyle.Fill;
            TableLayout_SingleTransaction.Location = new Point(0, 0);
            TableLayout_SingleTransaction.Name = "TableLayout_SingleTransaction";
            TableLayout_SingleTransaction.RowCount = 4;
            TableLayout_SingleTransaction.RowStyles.Add(new RowStyle());
            TableLayout_SingleTransaction.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            TableLayout_SingleTransaction.RowStyles.Add(new RowStyle());
            TableLayout_SingleTransaction.RowStyles.Add(new RowStyle());
            TableLayout_SingleTransaction.Size = new Size(190, 220);
            TableLayout_SingleTransaction.TabIndex = 6;
            // 
            // Button_SelectSingleTransaction
            // 
            Button_SelectSingleTransaction.Dock = DockStyle.Bottom;
            Button_SelectSingleTransaction.Location = new Point(30, 178);
            Button_SelectSingleTransaction.Margin = new Padding(3, 3, 3, 6);
            Button_SelectSingleTransaction.Name = "Button_SelectSingleTransaction";
            Button_SelectSingleTransaction.Size = new Size(130, 36);
            Button_SelectSingleTransaction.TabIndex = 3;
            Button_SelectSingleTransaction.Text = "Select Mode →";
            Button_SelectSingleTransaction.UseVisualStyleBackColor = true;
            // 
            // Label_SingleTransaction_BestFor
            // 
            Label_SingleTransaction_BestFor.AutoSize = true;
            TableLayout_SingleTransaction.SetColumnSpan(Label_SingleTransaction_BestFor, 3);
            Label_SingleTransaction_BestFor.Dock = DockStyle.Fill;
            Label_SingleTransaction_BestFor.Font = new Font("Segoe UI", 8F, FontStyle.Italic);
            Label_SingleTransaction_BestFor.Location = new Point(3, 159);
            Label_SingleTransaction_BestFor.Margin = new Padding(3);
            Label_SingleTransaction_BestFor.Name = "Label_SingleTransaction_BestFor";
            Label_SingleTransaction_BestFor.Size = new Size(184, 13);
            Label_SingleTransaction_BestFor.TabIndex = 2;
            Label_SingleTransaction_BestFor.Text = "Best for: Quick adds";
            Label_SingleTransaction_BestFor.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Label_SingleTransaction_Desc
            // 
            Label_SingleTransaction_Desc.AutoSize = true;
            TableLayout_SingleTransaction.SetColumnSpan(Label_SingleTransaction_Desc, 3);
            Label_SingleTransaction_Desc.Dock = DockStyle.Fill;
            Label_SingleTransaction_Desc.Location = new Point(3, 28);
            Label_SingleTransaction_Desc.Margin = new Padding(3);
            Label_SingleTransaction_Desc.Name = "Label_SingleTransaction_Desc";
            Label_SingleTransaction_Desc.Size = new Size(184, 125);
            Label_SingleTransaction_Desc.TabIndex = 1;
            Label_SingleTransaction_Desc.Text = "Inventory one part at one location, one time";
            Label_SingleTransaction_Desc.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Label_SingleTransaction_Title
            // 
            Label_SingleTransaction_Title.AutoSize = true;
            TableLayout_SingleTransaction.SetColumnSpan(Label_SingleTransaction_Title, 3);
            Label_SingleTransaction_Title.Dock = DockStyle.Fill;
            Label_SingleTransaction_Title.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            Label_SingleTransaction_Title.Location = new Point(3, 3);
            Label_SingleTransaction_Title.Margin = new Padding(3);
            Label_SingleTransaction_Title.Name = "Label_SingleTransaction_Title";
            Label_SingleTransaction_Title.Size = new Size(184, 19);
            Label_SingleTransaction_Title.TabIndex = 0;
            Label_SingleTransaction_Title.Text = "📦 Single Transaction";
            Label_SingleTransaction_Title.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Label_BatchEntry_Title
            // 
            Label_BatchEntry_Title.AutoSize = true;
            TableLayout_BatchEntry.SetColumnSpan(Label_BatchEntry_Title, 3);
            Label_BatchEntry_Title.Dock = DockStyle.Fill;
            Label_BatchEntry_Title.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            Label_BatchEntry_Title.Location = new Point(3, 3);
            Label_BatchEntry_Title.Margin = new Padding(3);
            Label_BatchEntry_Title.Name = "Label_BatchEntry_Title";
            Label_BatchEntry_Title.Size = new Size(184, 19);
            Label_BatchEntry_Title.TabIndex = 0;
            Label_BatchEntry_Title.Text = "🗂️ Batch Entry";
            Label_BatchEntry_Title.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Label_BatchEntry_Desc
            // 
            Label_BatchEntry_Desc.AutoSize = true;
            TableLayout_BatchEntry.SetColumnSpan(Label_BatchEntry_Desc, 3);
            Label_BatchEntry_Desc.Dock = DockStyle.Fill;
            Label_BatchEntry_Desc.Location = new Point(3, 28);
            Label_BatchEntry_Desc.Margin = new Padding(3);
            Label_BatchEntry_Desc.Name = "Label_BatchEntry_Desc";
            Label_BatchEntry_Desc.Size = new Size(184, 125);
            Label_BatchEntry_Desc.TabIndex = 1;
            Label_BatchEntry_Desc.Text = "Multiple transactions for one part (multi-location or repeated)";
            Label_BatchEntry_Desc.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Label_BatchEntry_BestFor
            // 
            Label_BatchEntry_BestFor.AutoSize = true;
            TableLayout_BatchEntry.SetColumnSpan(Label_BatchEntry_BestFor, 3);
            Label_BatchEntry_BestFor.Dock = DockStyle.Fill;
            Label_BatchEntry_BestFor.Font = new Font("Segoe UI", 8F, FontStyle.Italic);
            Label_BatchEntry_BestFor.Location = new Point(3, 159);
            Label_BatchEntry_BestFor.Margin = new Padding(3);
            Label_BatchEntry_BestFor.Name = "Label_BatchEntry_BestFor";
            Label_BatchEntry_BestFor.Size = new Size(184, 13);
            Label_BatchEntry_BestFor.TabIndex = 2;
            Label_BatchEntry_BestFor.Text = "Best for: Batch ops";
            Label_BatchEntry_BestFor.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Button_SelectBatchEntry
            // 
            Button_SelectBatchEntry.Dock = DockStyle.Bottom;
            Button_SelectBatchEntry.Location = new Point(30, 178);
            Button_SelectBatchEntry.Margin = new Padding(3, 3, 3, 6);
            Button_SelectBatchEntry.Name = "Button_SelectBatchEntry";
            Button_SelectBatchEntry.Size = new Size(130, 36);
            Button_SelectBatchEntry.TabIndex = 3;
            Button_SelectBatchEntry.Text = "Select Mode →";
            Button_SelectBatchEntry.UseVisualStyleBackColor = true;
            // 
            // Label_ExcelImport_Title
            // 
            Label_ExcelImport_Title.AutoSize = true;
            TableLayout_ExcelImport.SetColumnSpan(Label_ExcelImport_Title, 3);
            Label_ExcelImport_Title.Dock = DockStyle.Fill;
            Label_ExcelImport_Title.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            Label_ExcelImport_Title.Location = new Point(3, 3);
            Label_ExcelImport_Title.Margin = new Padding(3);
            Label_ExcelImport_Title.Name = "Label_ExcelImport_Title";
            Label_ExcelImport_Title.Size = new Size(184, 19);
            Label_ExcelImport_Title.TabIndex = 0;
            Label_ExcelImport_Title.Text = "📊 Import from Excel";
            Label_ExcelImport_Title.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Label_ExcelImport_Desc
            // 
            Label_ExcelImport_Desc.AutoSize = true;
            TableLayout_ExcelImport.SetColumnSpan(Label_ExcelImport_Desc, 3);
            Label_ExcelImport_Desc.Dock = DockStyle.Fill;
            Label_ExcelImport_Desc.Location = new Point(3, 28);
            Label_ExcelImport_Desc.Margin = new Padding(3);
            Label_ExcelImport_Desc.Name = "Label_ExcelImport_Desc";
            Label_ExcelImport_Desc.Size = new Size(184, 125);
            Label_ExcelImport_Desc.TabIndex = 1;
            Label_ExcelImport_Desc.Text = "Bulk import from your Excel template file";
            Label_ExcelImport_Desc.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Label_ExcelImport_BestFor
            // 
            Label_ExcelImport_BestFor.AutoSize = true;
            TableLayout_ExcelImport.SetColumnSpan(Label_ExcelImport_BestFor, 3);
            Label_ExcelImport_BestFor.Dock = DockStyle.Fill;
            Label_ExcelImport_BestFor.Font = new Font("Segoe UI", 8F, FontStyle.Italic);
            Label_ExcelImport_BestFor.Location = new Point(3, 159);
            Label_ExcelImport_BestFor.Margin = new Padding(3);
            Label_ExcelImport_BestFor.Name = "Label_ExcelImport_BestFor";
            Label_ExcelImport_BestFor.Size = new Size(184, 13);
            Label_ExcelImport_BestFor.TabIndex = 2;
            Label_ExcelImport_BestFor.Text = "Best for: Large batches";
            Label_ExcelImport_BestFor.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Button_SelectExcelImport
            // 
            Button_SelectExcelImport.Dock = DockStyle.Bottom;
            Button_SelectExcelImport.Location = new Point(29, 178);
            Button_SelectExcelImport.Margin = new Padding(3, 3, 3, 6);
            Button_SelectExcelImport.Name = "Button_SelectExcelImport";
            Button_SelectExcelImport.Size = new Size(130, 36);
            Button_SelectExcelImport.TabIndex = 3;
            Button_SelectExcelImport.Text = "Select Mode →";
            Button_SelectExcelImport.UseVisualStyleBackColor = true;
            // 
            // Label_Templates_Title
            // 
            Label_Templates_Title.AutoSize = true;
            TableLayout_Templates.SetColumnSpan(Label_Templates_Title, 3);
            Label_Templates_Title.Dock = DockStyle.Fill;
            Label_Templates_Title.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            Label_Templates_Title.Location = new Point(3, 3);
            Label_Templates_Title.Margin = new Padding(3);
            Label_Templates_Title.Name = "Label_Templates_Title";
            Label_Templates_Title.Size = new Size(186, 19);
            Label_Templates_Title.TabIndex = 0;
            Label_Templates_Title.Text = "📋 Quick Templates";
            Label_Templates_Title.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Label_Templates_Desc
            // 
            Label_Templates_Desc.AutoSize = true;
            TableLayout_Templates.SetColumnSpan(Label_Templates_Desc, 3);
            Label_Templates_Desc.Dock = DockStyle.Fill;
            Label_Templates_Desc.Location = new Point(3, 28);
            Label_Templates_Desc.Margin = new Padding(3);
            Label_Templates_Desc.Name = "Label_Templates_Desc";
            Label_Templates_Desc.Size = new Size(186, 125);
            Label_Templates_Desc.TabIndex = 1;
            Label_Templates_Desc.Text = "Use saved presets for common inventory ops";
            Label_Templates_Desc.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Label_Templates_BestFor
            // 
            Label_Templates_BestFor.AutoSize = true;
            TableLayout_Templates.SetColumnSpan(Label_Templates_BestFor, 3);
            Label_Templates_BestFor.Dock = DockStyle.Fill;
            Label_Templates_BestFor.Font = new Font("Segoe UI", 8F, FontStyle.Italic);
            Label_Templates_BestFor.Location = new Point(3, 159);
            Label_Templates_BestFor.Margin = new Padding(3);
            Label_Templates_BestFor.Name = "Label_Templates_BestFor";
            Label_Templates_BestFor.Size = new Size(186, 13);
            Label_Templates_BestFor.TabIndex = 2;
            Label_Templates_BestFor.Text = "Best for: Repeating";
            Label_Templates_BestFor.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Button_SelectTemplate
            // 
            Button_SelectTemplate.Dock = DockStyle.Bottom;
            Button_SelectTemplate.Location = new Point(31, 178);
            Button_SelectTemplate.Margin = new Padding(3, 3, 3, 6);
            Button_SelectTemplate.Name = "Button_SelectTemplate";
            Button_SelectTemplate.Size = new Size(130, 36);
            Button_SelectTemplate.TabIndex = 3;
            Button_SelectTemplate.Text = "Select Template →";
            Button_SelectTemplate.UseVisualStyleBackColor = true;
            // 
            // Button_Back
            // 
            Button_Back.AutoSize = true;
            Button_Back.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Button_Back.Dock = DockStyle.Fill;
            Button_Back.Font = new Font("Segoe UI", 11F);
            Button_Back.Location = new Point(3, 3);
            Button_Back.Name = "Button_Back";
            Button_Back.Size = new Size(62, 30);
            Button_Back.TabIndex = 0;
            Button_Back.Text = "❮ Back";
            Button_Back.UseVisualStyleBackColor = true;
            // 
            // Button_Next
            // 
            Button_Next.AutoSize = true;
            Button_Next.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Button_Next.Font = new Font("Segoe UI", 11F);
            Button_Next.Location = new Point(729, 3);
            Button_Next.Name = "Button_Next";
            Button_Next.Size = new Size(62, 30);
            Button_Next.TabIndex = 3;
            Button_Next.Text = "Next ❯";
            Button_Next.UseVisualStyleBackColor = true;
            // 
            // TableLayout_Main
            // 
            TableLayout_Main.AutoSize = true;
            TableLayout_Main.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TableLayout_Main.ColumnCount = 1;
            TableLayout_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            TableLayout_Main.Controls.Add(TabelLayout_StepNavigation, 0, 1);
            TableLayout_Main.Controls.Add(TableLayout_BottomNavigation, 0, 3);
            TableLayout_Main.Controls.Add(TableLayout_ModeSelection, 0, 2);
            TableLayout_Main.Dock = DockStyle.Fill;
            TableLayout_Main.Location = new Point(0, 0);
            TableLayout_Main.Name = "TableLayout_Main";
            TableLayout_Main.RowCount = 4;
            TableLayout_Main.RowStyles.Add(new RowStyle());
            TableLayout_Main.RowStyles.Add(new RowStyle());
            TableLayout_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            TableLayout_Main.RowStyles.Add(new RowStyle());
            TableLayout_Main.Size = new Size(800, 375);
            TableLayout_Main.TabIndex = 4;
            // 
            // TabelLayout_StepNavigation
            // 
            TabelLayout_StepNavigation.AutoSize = true;
            TabelLayout_StepNavigation.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TabelLayout_StepNavigation.ColumnCount = 9;
            TabelLayout_StepNavigation.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            TabelLayout_StepNavigation.ColumnStyles.Add(new ColumnStyle());
            TabelLayout_StepNavigation.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            TabelLayout_StepNavigation.ColumnStyles.Add(new ColumnStyle());
            TabelLayout_StepNavigation.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            TabelLayout_StepNavigation.ColumnStyles.Add(new ColumnStyle());
            TabelLayout_StepNavigation.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            TabelLayout_StepNavigation.ColumnStyles.Add(new ColumnStyle());
            TabelLayout_StepNavigation.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            TabelLayout_StepNavigation.Controls.Add(Label_CurrentStep, 0, 2);
            TabelLayout_StepNavigation.Controls.Add(Label_Step4, 7, 1);
            TabelLayout_StepNavigation.Controls.Add(Label_Step3, 5, 1);
            TabelLayout_StepNavigation.Controls.Add(Label_Step2, 3, 1);
            TabelLayout_StepNavigation.Controls.Add(Label_Step1, 1, 1);
            TabelLayout_StepNavigation.Controls.Add(Label_StepTitle, 0, 0);
            TabelLayout_StepNavigation.Dock = DockStyle.Fill;
            TabelLayout_StepNavigation.Font = new Font("Segoe UI", 18F);
            TabelLayout_StepNavigation.Location = new Point(3, 3);
            TabelLayout_StepNavigation.Name = "TabelLayout_StepNavigation";
            TabelLayout_StepNavigation.RowCount = 3;
            TabelLayout_StepNavigation.RowStyles.Add(new RowStyle());
            TabelLayout_StepNavigation.RowStyles.Add(new RowStyle());
            TabelLayout_StepNavigation.RowStyles.Add(new RowStyle());
            TabelLayout_StepNavigation.Size = new Size(794, 66);
            TabelLayout_StepNavigation.TabIndex = 5;
            // 
            // TableLayout_BottomNavigation
            // 
            TableLayout_BottomNavigation.AutoSize = true;
            TableLayout_BottomNavigation.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TableLayout_BottomNavigation.ColumnCount = 7;
            TableLayout_BottomNavigation.ColumnStyles.Add(new ColumnStyle());
            TableLayout_BottomNavigation.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            TableLayout_BottomNavigation.ColumnStyles.Add(new ColumnStyle());
            TableLayout_BottomNavigation.ColumnStyles.Add(new ColumnStyle());
            TableLayout_BottomNavigation.ColumnStyles.Add(new ColumnStyle());
            TableLayout_BottomNavigation.ColumnStyles.Add(new ColumnStyle());
            TableLayout_BottomNavigation.ColumnStyles.Add(new ColumnStyle());
            TableLayout_BottomNavigation.Controls.Add(Button_Back, 0, 0);
            TableLayout_BottomNavigation.Controls.Add(Button_Next, 6, 0);
            TableLayout_BottomNavigation.Controls.Add(Button_SwitchToClassic, 2, 0);
            TableLayout_BottomNavigation.Dock = DockStyle.Fill;
            TableLayout_BottomNavigation.Location = new Point(3, 336);
            TableLayout_BottomNavigation.Name = "TableLayout_BottomNavigation";
            TableLayout_BottomNavigation.RowCount = 1;
            TableLayout_BottomNavigation.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            TableLayout_BottomNavigation.Size = new Size(794, 36);
            TableLayout_BottomNavigation.TabIndex = 4;
            // 
            // TableLayout_ModeSelection
            // 
            TableLayout_ModeSelection.AutoSize = true;
            TableLayout_ModeSelection.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TableLayout_ModeSelection.ColumnCount = 4;
            TableLayout_ModeSelection.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            TableLayout_ModeSelection.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            TableLayout_ModeSelection.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            TableLayout_ModeSelection.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            TableLayout_ModeSelection.Controls.Add(Label_SelectMode, 0, 0);
            TableLayout_ModeSelection.Controls.Add(Panel_SingleTransaction, 0, 1);
            TableLayout_ModeSelection.Controls.Add(Panel_Templates, 3, 1);
            TableLayout_ModeSelection.Controls.Add(Panel_ExcelImport, 2, 1);
            TableLayout_ModeSelection.Controls.Add(Panel_BatchEntry, 1, 1);
            TableLayout_ModeSelection.Dock = DockStyle.Fill;
            TableLayout_ModeSelection.Location = new Point(3, 75);
            TableLayout_ModeSelection.Name = "TableLayout_ModeSelection";
            TableLayout_ModeSelection.RowCount = 2;
            TableLayout_ModeSelection.RowStyles.Add(new RowStyle());
            TableLayout_ModeSelection.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            TableLayout_ModeSelection.Size = new Size(794, 255);
            TableLayout_ModeSelection.TabIndex = 5;
            // 
            // Panel_Templates
            // 
            Panel_Templates.AutoSize = true;
            Panel_Templates.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Panel_Templates.BorderStyle = BorderStyle.FixedSingle;
            Panel_Templates.Controls.Add(TableLayout_Templates);
            Panel_Templates.Dock = DockStyle.Fill;
            Panel_Templates.Location = new Point(597, 30);
            Panel_Templates.Name = "Panel_Templates";
            Panel_Templates.Size = new Size(194, 222);
            Panel_Templates.TabIndex = 4;
            // 
            // TableLayout_Templates
            // 
            TableLayout_Templates.AutoSize = true;
            TableLayout_Templates.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TableLayout_Templates.ColumnCount = 3;
            TableLayout_Templates.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            TableLayout_Templates.ColumnStyles.Add(new ColumnStyle());
            TableLayout_Templates.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            TableLayout_Templates.Controls.Add(Button_SelectTemplate, 1, 3);
            TableLayout_Templates.Controls.Add(Label_Templates_BestFor, 0, 2);
            TableLayout_Templates.Controls.Add(Label_Templates_Desc, 0, 1);
            TableLayout_Templates.Controls.Add(Label_Templates_Title, 0, 0);
            TableLayout_Templates.Dock = DockStyle.Fill;
            TableLayout_Templates.Location = new Point(0, 0);
            TableLayout_Templates.Name = "TableLayout_Templates";
            TableLayout_Templates.RowCount = 4;
            TableLayout_Templates.RowStyles.Add(new RowStyle());
            TableLayout_Templates.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            TableLayout_Templates.RowStyles.Add(new RowStyle());
            TableLayout_Templates.RowStyles.Add(new RowStyle());
            TableLayout_Templates.Size = new Size(192, 220);
            TableLayout_Templates.TabIndex = 6;
            // 
            // Panel_ExcelImport
            // 
            Panel_ExcelImport.AutoSize = true;
            Panel_ExcelImport.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Panel_ExcelImport.BorderStyle = BorderStyle.FixedSingle;
            Panel_ExcelImport.Controls.Add(TableLayout_ExcelImport);
            Panel_ExcelImport.Dock = DockStyle.Fill;
            Panel_ExcelImport.Location = new Point(399, 30);
            Panel_ExcelImport.Name = "Panel_ExcelImport";
            Panel_ExcelImport.Size = new Size(192, 222);
            Panel_ExcelImport.TabIndex = 3;
            // 
            // TableLayout_ExcelImport
            // 
            TableLayout_ExcelImport.AutoSize = true;
            TableLayout_ExcelImport.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TableLayout_ExcelImport.ColumnCount = 3;
            TableLayout_ExcelImport.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 49.9999962F));
            TableLayout_ExcelImport.ColumnStyles.Add(new ColumnStyle());
            TableLayout_ExcelImport.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50.0000076F));
            TableLayout_ExcelImport.Controls.Add(Button_SelectExcelImport, 1, 3);
            TableLayout_ExcelImport.Controls.Add(Label_ExcelImport_BestFor, 0, 2);
            TableLayout_ExcelImport.Controls.Add(Label_ExcelImport_Desc, 0, 1);
            TableLayout_ExcelImport.Controls.Add(Label_ExcelImport_Title, 0, 0);
            TableLayout_ExcelImport.Dock = DockStyle.Fill;
            TableLayout_ExcelImport.Location = new Point(0, 0);
            TableLayout_ExcelImport.Name = "TableLayout_ExcelImport";
            TableLayout_ExcelImport.RowCount = 4;
            TableLayout_ExcelImport.RowStyles.Add(new RowStyle());
            TableLayout_ExcelImport.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            TableLayout_ExcelImport.RowStyles.Add(new RowStyle());
            TableLayout_ExcelImport.RowStyles.Add(new RowStyle());
            TableLayout_ExcelImport.Size = new Size(190, 220);
            TableLayout_ExcelImport.TabIndex = 6;
            // 
            // Panel_BatchEntry
            // 
            Panel_BatchEntry.AutoSize = true;
            Panel_BatchEntry.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Panel_BatchEntry.BorderStyle = BorderStyle.FixedSingle;
            Panel_BatchEntry.Controls.Add(TableLayout_BatchEntry);
            Panel_BatchEntry.Dock = DockStyle.Fill;
            Panel_BatchEntry.Location = new Point(201, 30);
            Panel_BatchEntry.Name = "Panel_BatchEntry";
            Panel_BatchEntry.Size = new Size(192, 222);
            Panel_BatchEntry.TabIndex = 2;
            // 
            // TableLayout_BatchEntry
            // 
            TableLayout_BatchEntry.AutoSize = true;
            TableLayout_BatchEntry.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TableLayout_BatchEntry.ColumnCount = 3;
            TableLayout_BatchEntry.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            TableLayout_BatchEntry.ColumnStyles.Add(new ColumnStyle());
            TableLayout_BatchEntry.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            TableLayout_BatchEntry.Controls.Add(Button_SelectBatchEntry, 1, 3);
            TableLayout_BatchEntry.Controls.Add(Label_BatchEntry_BestFor, 0, 2);
            TableLayout_BatchEntry.Controls.Add(Label_BatchEntry_Desc, 0, 1);
            TableLayout_BatchEntry.Controls.Add(Label_BatchEntry_Title, 0, 0);
            TableLayout_BatchEntry.Dock = DockStyle.Fill;
            TableLayout_BatchEntry.Location = new Point(0, 0);
            TableLayout_BatchEntry.Name = "TableLayout_BatchEntry";
            TableLayout_BatchEntry.RowCount = 4;
            TableLayout_BatchEntry.RowStyles.Add(new RowStyle());
            TableLayout_BatchEntry.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            TableLayout_BatchEntry.RowStyles.Add(new RowStyle());
            TableLayout_BatchEntry.RowStyles.Add(new RowStyle());
            TableLayout_BatchEntry.Size = new Size(190, 220);
            TableLayout_BatchEntry.TabIndex = 6;
            // 
            // Control_AdvInv_Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(TableLayout_Main);
            Name = "Control_AdvInv_Main";
            Size = new Size(800, 375);
            Panel_SingleTransaction.ResumeLayout(false);
            Panel_SingleTransaction.PerformLayout();
            TableLayout_SingleTransaction.ResumeLayout(false);
            TableLayout_SingleTransaction.PerformLayout();
            TableLayout_Main.ResumeLayout(false);
            TableLayout_Main.PerformLayout();
            TabelLayout_StepNavigation.ResumeLayout(false);
            TabelLayout_StepNavigation.PerformLayout();
            TableLayout_BottomNavigation.ResumeLayout(false);
            TableLayout_BottomNavigation.PerformLayout();
            TableLayout_ModeSelection.ResumeLayout(false);
            TableLayout_ModeSelection.PerformLayout();
            Panel_Templates.ResumeLayout(false);
            Panel_Templates.PerformLayout();
            TableLayout_Templates.ResumeLayout(false);
            TableLayout_Templates.PerformLayout();
            Panel_ExcelImport.ResumeLayout(false);
            Panel_ExcelImport.PerformLayout();
            TableLayout_ExcelImport.ResumeLayout(false);
            TableLayout_ExcelImport.PerformLayout();
            Panel_BatchEntry.ResumeLayout(false);
            Panel_BatchEntry.PerformLayout();
            TableLayout_BatchEntry.ResumeLayout(false);
            TableLayout_BatchEntry.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        #region Controls
        private System.Windows.Forms.Button Button_SwitchToClassic;
        private System.Windows.Forms.Label Label_StepTitle;
        private System.Windows.Forms.Label Label_Step1;
        private System.Windows.Forms.Label Label_Step2;
        private System.Windows.Forms.Label Label_Step3;
        private System.Windows.Forms.Label Label_Step4;
        private System.Windows.Forms.Label Label_CurrentStep;

        // Mode Selection
        private System.Windows.Forms.Panel Panel_ModeSelection;
        private System.Windows.Forms.Label Label_SelectMode;

        // Single Transaction Card
        private System.Windows.Forms.Panel Panel_SingleTransaction;
        private System.Windows.Forms.Label Label_SingleTransaction_Title;
        private System.Windows.Forms.Label Label_SingleTransaction_Desc;
        private System.Windows.Forms.Label Label_SingleTransaction_BestFor;
        private System.Windows.Forms.Button Button_SelectSingleTransaction;
        private System.Windows.Forms.Label Label_BatchEntry_Title;
        private System.Windows.Forms.Label Label_BatchEntry_Desc;
        private System.Windows.Forms.Label Label_BatchEntry_BestFor;
        private System.Windows.Forms.Button Button_SelectBatchEntry;
        private System.Windows.Forms.Label Label_ExcelImport_Title;
        private System.Windows.Forms.Label Label_ExcelImport_Desc;
        private System.Windows.Forms.Label Label_ExcelImport_BestFor;
        private System.Windows.Forms.Button Button_SelectExcelImport;
        private System.Windows.Forms.Label Label_Templates_Title;
        private System.Windows.Forms.Label Label_Templates_Desc;
        private System.Windows.Forms.Label Label_Templates_BestFor;
        private System.Windows.Forms.Button Button_SelectTemplate;
        private System.Windows.Forms.Button Button_Back;
        private System.Windows.Forms.Button Button_Next;

        #endregion

        private TableLayoutPanel TableLayout_Main;
        private TableLayoutPanel TableLayout_BottomNavigation;
        private TableLayoutPanel TableLayout_ModeSelection;
        private TableLayoutPanel TableLayout_SingleTransaction;
        private TableLayoutPanel TableLayout_BatchEntry;
        private TableLayoutPanel TableLayout_ExcelImport;
        private TableLayoutPanel TableLayout_Templates;
        private Panel Panel_Templates;
        private Panel Panel_ExcelImport;
        private Panel Panel_BatchEntry;
        private TableLayoutPanel TabelLayout_StepNavigation;
    }
}
