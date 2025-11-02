#nullable disable

using System.Drawing;
using System.Windows.Forms;

namespace MTM_WIP_Application_Winforms.Controls.Transactions
{
    partial class TransactionDetailPanel
    {
        private System.ComponentModel.IContainer components = null;

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            TransactionDetailPanel_TableLayout_Main = new TableLayoutPanel();
            TransactionDetailPanel_GroupBox_Main = new GroupBox();
            TransactionDetailPanel_TableLayout_Inner = new TableLayoutPanel();
            TransactionDetailPanel_TableLayout_RelatedHeader = new TableLayoutPanel();
            TransactionDetailPanel_Label_RelatedTitle = new Label();
            TransactionDetailPanel_Button_ViewBatchHistory = new Button();
            TransactionDetailPanel_TextBox_Notes = new TextBox();
            TransactionDetailPanel_Label_RelatedStatus = new Label();
            TransactionDetailPanel_TableLayout_Details = new TableLayoutPanel();
            TransactionDetailPanel_Label_IdCaption = new Label();
            TransactionDetailPanel_Label_IdValue = new Label();
            TransactionDetailPanel_Label_TypeCaption = new Label();
            TransactionDetailPanel_Label_TypeValue = new Label();
            TransactionDetailPanel_Label_ItemTypeCaption = new Label();
            TransactionDetailPanel_Label_ItemTypeValue = new Label();
            TransactionDetailPanel_Label_PartCaption = new Label();
            TransactionDetailPanel_Label_PartValue = new Label();
            TransactionDetailPanel_Label_BatchCaption = new Label();
            TransactionDetailPanel_Label_BatchValue = new Label();
            TransactionDetailPanel_Label_QuantityCaption = new Label();
            TransactionDetailPanel_Label_QuantityValue = new Label();
            TransactionDetailPanel_Label_FromCaption = new Label();
            TransactionDetailPanel_Label_FromValue = new Label();
            TransactionDetailPanel_Label_ToCaption = new Label();
            TransactionDetailPanel_Label_ToValue = new Label();
            TransactionDetailPanel_Label_OperationCaption = new Label();
            TransactionDetailPanel_Label_OperationValue = new Label();
            TransactionDetailPanel_Label_UserCaption = new Label();
            TransactionDetailPanel_Label_UserValue = new Label();
            TransactionDetailPanel_Label_DateCaption = new Label();
            TransactionDetailPanel_Label_DateValue = new Label();
            TransactionDetailPanel_Label_NotesCaption = new Label();
            TransactionDetailPanel_TableLayout_Main.SuspendLayout();
            TransactionDetailPanel_GroupBox_Main.SuspendLayout();
            TransactionDetailPanel_TableLayout_Inner.SuspendLayout();
            TransactionDetailPanel_TableLayout_RelatedHeader.SuspendLayout();
            TransactionDetailPanel_TableLayout_Details.SuspendLayout();
            SuspendLayout();
            // 
            // TransactionDetailPanel_TableLayout_Main
            // 
            TransactionDetailPanel_TableLayout_Main.AutoSize = true;
            TransactionDetailPanel_TableLayout_Main.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionDetailPanel_TableLayout_Main.ColumnCount = 1;
            TransactionDetailPanel_TableLayout_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            TransactionDetailPanel_TableLayout_Main.Controls.Add(TransactionDetailPanel_GroupBox_Main, 0, 0);
            TransactionDetailPanel_TableLayout_Main.Dock = DockStyle.Fill;
            TransactionDetailPanel_TableLayout_Main.Location = new Point(0, 0);
            TransactionDetailPanel_TableLayout_Main.Margin = new Padding(0);
            TransactionDetailPanel_TableLayout_Main.Name = "TransactionDetailPanel_TableLayout_Main";
            TransactionDetailPanel_TableLayout_Main.Padding = new Padding(5);
            TransactionDetailPanel_TableLayout_Main.RowCount = 1;
            TransactionDetailPanel_TableLayout_Main.RowStyles.Add(new RowStyle());
            TransactionDetailPanel_TableLayout_Main.Size = new Size(307, 430);
            TransactionDetailPanel_TableLayout_Main.TabIndex = 0;
            // 
            // TransactionDetailPanel_GroupBox_Main
            // 
            TransactionDetailPanel_GroupBox_Main.AutoSize = true;
            TransactionDetailPanel_GroupBox_Main.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionDetailPanel_GroupBox_Main.Controls.Add(TransactionDetailPanel_TableLayout_Inner);
            TransactionDetailPanel_GroupBox_Main.Dock = DockStyle.Fill;
            TransactionDetailPanel_GroupBox_Main.Location = new Point(8, 8);
            TransactionDetailPanel_GroupBox_Main.Name = "TransactionDetailPanel_GroupBox_Main";
            TransactionDetailPanel_GroupBox_Main.Padding = new Padding(5);
            TransactionDetailPanel_GroupBox_Main.Size = new Size(291, 414);
            TransactionDetailPanel_GroupBox_Main.TabIndex = 7;
            TransactionDetailPanel_GroupBox_Main.TabStop = false;
            TransactionDetailPanel_GroupBox_Main.Text = "Transaction Details";
            // 
            // TransactionDetailPanel_TableLayout_Inner
            // 
            TransactionDetailPanel_TableLayout_Inner.AutoSize = true;
            TransactionDetailPanel_TableLayout_Inner.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionDetailPanel_TableLayout_Inner.ColumnCount = 1;
            TransactionDetailPanel_TableLayout_Inner.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            TransactionDetailPanel_TableLayout_Inner.Controls.Add(TransactionDetailPanel_TableLayout_RelatedHeader, 0, 4);
            TransactionDetailPanel_TableLayout_Inner.Controls.Add(TransactionDetailPanel_TextBox_Notes, 0, 2);
            TransactionDetailPanel_TableLayout_Inner.Controls.Add(TransactionDetailPanel_Label_RelatedStatus, 0, 3);
            TransactionDetailPanel_TableLayout_Inner.Controls.Add(TransactionDetailPanel_TableLayout_Details, 0, 0);
            TransactionDetailPanel_TableLayout_Inner.Controls.Add(TransactionDetailPanel_Label_NotesCaption, 0, 1);
            TransactionDetailPanel_TableLayout_Inner.Dock = DockStyle.Fill;
            TransactionDetailPanel_TableLayout_Inner.Location = new Point(5, 21);
            TransactionDetailPanel_TableLayout_Inner.Name = "TransactionDetailPanel_TableLayout_Inner";
            TransactionDetailPanel_TableLayout_Inner.RowCount = 5;
            TransactionDetailPanel_TableLayout_Inner.RowStyles.Add(new RowStyle());
            TransactionDetailPanel_TableLayout_Inner.RowStyles.Add(new RowStyle());
            TransactionDetailPanel_TableLayout_Inner.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            TransactionDetailPanel_TableLayout_Inner.RowStyles.Add(new RowStyle());
            TransactionDetailPanel_TableLayout_Inner.RowStyles.Add(new RowStyle());
            TransactionDetailPanel_TableLayout_Inner.Size = new Size(281, 388);
            TransactionDetailPanel_TableLayout_Inner.TabIndex = 0;
            // 
            // TransactionDetailPanel_TableLayout_RelatedHeader
            // 
            TransactionDetailPanel_TableLayout_RelatedHeader.AutoSize = true;
            TransactionDetailPanel_TableLayout_RelatedHeader.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionDetailPanel_TableLayout_RelatedHeader.ColumnCount = 2;
            TransactionDetailPanel_TableLayout_RelatedHeader.ColumnStyles.Add(new ColumnStyle());
            TransactionDetailPanel_TableLayout_RelatedHeader.ColumnStyles.Add(new ColumnStyle());
            TransactionDetailPanel_TableLayout_RelatedHeader.Controls.Add(TransactionDetailPanel_Label_RelatedTitle, 0, 0);
            TransactionDetailPanel_TableLayout_RelatedHeader.Controls.Add(TransactionDetailPanel_Button_ViewBatchHistory, 1, 0);
            TransactionDetailPanel_TableLayout_RelatedHeader.Dock = DockStyle.Fill;
            TransactionDetailPanel_TableLayout_RelatedHeader.Location = new Point(0, 357);
            TransactionDetailPanel_TableLayout_RelatedHeader.Margin = new Padding(0);
            TransactionDetailPanel_TableLayout_RelatedHeader.Name = "TransactionDetailPanel_TableLayout_RelatedHeader";
            TransactionDetailPanel_TableLayout_RelatedHeader.RowCount = 1;
            TransactionDetailPanel_TableLayout_RelatedHeader.RowStyles.Add(new RowStyle());
            TransactionDetailPanel_TableLayout_RelatedHeader.Size = new Size(281, 31);
            TransactionDetailPanel_TableLayout_RelatedHeader.TabIndex = 4;
            // 
            // TransactionDetailPanel_Label_RelatedTitle
            // 
            TransactionDetailPanel_Label_RelatedTitle.AutoSize = true;
            TransactionDetailPanel_Label_RelatedTitle.Dock = DockStyle.Fill;
            TransactionDetailPanel_Label_RelatedTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            TransactionDetailPanel_Label_RelatedTitle.Location = new Point(3, 3);
            TransactionDetailPanel_Label_RelatedTitle.Margin = new Padding(3);
            TransactionDetailPanel_Label_RelatedTitle.Name = "TransactionDetailPanel_Label_RelatedTitle";
            TransactionDetailPanel_Label_RelatedTitle.Size = new Size(121, 25);
            TransactionDetailPanel_Label_RelatedTitle.TabIndex = 0;
            TransactionDetailPanel_Label_RelatedTitle.Text = "Related Transactions";
            TransactionDetailPanel_Label_RelatedTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TransactionDetailPanel_Button_ViewBatchHistory
            // 
            TransactionDetailPanel_Button_ViewBatchHistory.AutoSize = true;
            TransactionDetailPanel_Button_ViewBatchHistory.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionDetailPanel_Button_ViewBatchHistory.Dock = DockStyle.Fill;
            TransactionDetailPanel_Button_ViewBatchHistory.Location = new Point(130, 3);
            TransactionDetailPanel_Button_ViewBatchHistory.Name = "TransactionDetailPanel_Button_ViewBatchHistory";
            TransactionDetailPanel_Button_ViewBatchHistory.Size = new Size(148, 25);
            TransactionDetailPanel_Button_ViewBatchHistory.TabIndex = 1;
            TransactionDetailPanel_Button_ViewBatchHistory.Text = "Transaction Life Cycle";
            TransactionDetailPanel_Button_ViewBatchHistory.UseVisualStyleBackColor = true;
            TransactionDetailPanel_Button_ViewBatchHistory.Click += TransactionDetailPanel_Button_ViewBatchHistory_Click;
            // 
            // TransactionDetailPanel_TextBox_Notes
            // 
            TransactionDetailPanel_TextBox_Notes.BorderStyle = BorderStyle.FixedSingle;
            TransactionDetailPanel_TextBox_Notes.Dock = DockStyle.Fill;
            TransactionDetailPanel_TextBox_Notes.Location = new Point(3, 261);
            TransactionDetailPanel_TextBox_Notes.Multiline = true;
            TransactionDetailPanel_TextBox_Notes.Name = "TransactionDetailPanel_TextBox_Notes";
            TransactionDetailPanel_TextBox_Notes.ReadOnly = true;
            TransactionDetailPanel_TextBox_Notes.ScrollBars = ScrollBars.Vertical;
            TransactionDetailPanel_TextBox_Notes.Size = new Size(275, 70);
            TransactionDetailPanel_TextBox_Notes.TabIndex = 3;
            TransactionDetailPanel_TextBox_Notes.Text = "—";
            // 
            // TransactionDetailPanel_Label_RelatedStatus
            // 
            TransactionDetailPanel_Label_RelatedStatus.AutoSize = true;
            TransactionDetailPanel_Label_RelatedStatus.Dock = DockStyle.Fill;
            TransactionDetailPanel_Label_RelatedStatus.Location = new Point(3, 339);
            TransactionDetailPanel_Label_RelatedStatus.Margin = new Padding(3, 5, 3, 3);
            TransactionDetailPanel_Label_RelatedStatus.Name = "TransactionDetailPanel_Label_RelatedStatus";
            TransactionDetailPanel_Label_RelatedStatus.Size = new Size(275, 15);
            TransactionDetailPanel_Label_RelatedStatus.TabIndex = 6;
            TransactionDetailPanel_Label_RelatedStatus.Text = "Select a transaction to view related activity.";
            TransactionDetailPanel_Label_RelatedStatus.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TransactionDetailPanel_TableLayout_Details
            // 
            TransactionDetailPanel_TableLayout_Details.AutoSize = true;
            TransactionDetailPanel_TableLayout_Details.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionDetailPanel_TableLayout_Details.ColumnCount = 2;
            TransactionDetailPanel_TableLayout_Details.ColumnStyles.Add(new ColumnStyle());
            TransactionDetailPanel_TableLayout_Details.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            TransactionDetailPanel_TableLayout_Details.Controls.Add(TransactionDetailPanel_Label_IdCaption, 0, 0);
            TransactionDetailPanel_TableLayout_Details.Controls.Add(TransactionDetailPanel_Label_IdValue, 1, 0);
            TransactionDetailPanel_TableLayout_Details.Controls.Add(TransactionDetailPanel_Label_TypeCaption, 0, 1);
            TransactionDetailPanel_TableLayout_Details.Controls.Add(TransactionDetailPanel_Label_TypeValue, 1, 1);
            TransactionDetailPanel_TableLayout_Details.Controls.Add(TransactionDetailPanel_Label_ItemTypeCaption, 0, 2);
            TransactionDetailPanel_TableLayout_Details.Controls.Add(TransactionDetailPanel_Label_ItemTypeValue, 1, 2);
            TransactionDetailPanel_TableLayout_Details.Controls.Add(TransactionDetailPanel_Label_PartCaption, 0, 3);
            TransactionDetailPanel_TableLayout_Details.Controls.Add(TransactionDetailPanel_Label_PartValue, 1, 3);
            TransactionDetailPanel_TableLayout_Details.Controls.Add(TransactionDetailPanel_Label_BatchCaption, 0, 4);
            TransactionDetailPanel_TableLayout_Details.Controls.Add(TransactionDetailPanel_Label_BatchValue, 1, 4);
            TransactionDetailPanel_TableLayout_Details.Controls.Add(TransactionDetailPanel_Label_QuantityCaption, 0, 5);
            TransactionDetailPanel_TableLayout_Details.Controls.Add(TransactionDetailPanel_Label_QuantityValue, 1, 5);
            TransactionDetailPanel_TableLayout_Details.Controls.Add(TransactionDetailPanel_Label_FromCaption, 0, 6);
            TransactionDetailPanel_TableLayout_Details.Controls.Add(TransactionDetailPanel_Label_FromValue, 1, 6);
            TransactionDetailPanel_TableLayout_Details.Controls.Add(TransactionDetailPanel_Label_ToCaption, 0, 7);
            TransactionDetailPanel_TableLayout_Details.Controls.Add(TransactionDetailPanel_Label_ToValue, 1, 7);
            TransactionDetailPanel_TableLayout_Details.Controls.Add(TransactionDetailPanel_Label_OperationCaption, 0, 8);
            TransactionDetailPanel_TableLayout_Details.Controls.Add(TransactionDetailPanel_Label_OperationValue, 1, 8);
            TransactionDetailPanel_TableLayout_Details.Controls.Add(TransactionDetailPanel_Label_UserCaption, 0, 9);
            TransactionDetailPanel_TableLayout_Details.Controls.Add(TransactionDetailPanel_Label_UserValue, 1, 9);
            TransactionDetailPanel_TableLayout_Details.Controls.Add(TransactionDetailPanel_Label_DateCaption, 0, 10);
            TransactionDetailPanel_TableLayout_Details.Controls.Add(TransactionDetailPanel_Label_DateValue, 1, 10);
            TransactionDetailPanel_TableLayout_Details.Dock = DockStyle.Fill;
            TransactionDetailPanel_TableLayout_Details.Location = new Point(3, 3);
            TransactionDetailPanel_TableLayout_Details.Name = "TransactionDetailPanel_TableLayout_Details";
            TransactionDetailPanel_TableLayout_Details.RowCount = 11;
            TransactionDetailPanel_TableLayout_Details.RowStyles.Add(new RowStyle());
            TransactionDetailPanel_TableLayout_Details.RowStyles.Add(new RowStyle());
            TransactionDetailPanel_TableLayout_Details.RowStyles.Add(new RowStyle());
            TransactionDetailPanel_TableLayout_Details.RowStyles.Add(new RowStyle());
            TransactionDetailPanel_TableLayout_Details.RowStyles.Add(new RowStyle());
            TransactionDetailPanel_TableLayout_Details.RowStyles.Add(new RowStyle());
            TransactionDetailPanel_TableLayout_Details.RowStyles.Add(new RowStyle());
            TransactionDetailPanel_TableLayout_Details.RowStyles.Add(new RowStyle());
            TransactionDetailPanel_TableLayout_Details.RowStyles.Add(new RowStyle());
            TransactionDetailPanel_TableLayout_Details.RowStyles.Add(new RowStyle());
            TransactionDetailPanel_TableLayout_Details.RowStyles.Add(new RowStyle());
            TransactionDetailPanel_TableLayout_Details.Size = new Size(275, 231);
            TransactionDetailPanel_TableLayout_Details.TabIndex = 1;
            // 
            // TransactionDetailPanel_Label_IdCaption
            // 
            TransactionDetailPanel_Label_IdCaption.AutoSize = true;
            TransactionDetailPanel_Label_IdCaption.Dock = DockStyle.Fill;
            TransactionDetailPanel_Label_IdCaption.Location = new Point(3, 3);
            TransactionDetailPanel_Label_IdCaption.Margin = new Padding(3);
            TransactionDetailPanel_Label_IdCaption.Name = "TransactionDetailPanel_Label_IdCaption";
            TransactionDetailPanel_Label_IdCaption.Size = new Size(1, 15);
            TransactionDetailPanel_Label_IdCaption.TabIndex = 0;
            TransactionDetailPanel_Label_IdCaption.TextAlign = ContentAlignment.MiddleRight;
            // 
            // TransactionDetailPanel_Label_IdValue
            // 
            TransactionDetailPanel_Label_IdValue.AutoSize = true;
            TransactionDetailPanel_Label_IdValue.Dock = DockStyle.Fill;
            TransactionDetailPanel_Label_IdValue.Location = new Point(9, 3);
            TransactionDetailPanel_Label_IdValue.Margin = new Padding(3);
            TransactionDetailPanel_Label_IdValue.Name = "TransactionDetailPanel_Label_IdValue";
            TransactionDetailPanel_Label_IdValue.Size = new Size(263, 15);
            TransactionDetailPanel_Label_IdValue.TabIndex = 1;
            TransactionDetailPanel_Label_IdValue.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TransactionDetailPanel_Label_TypeCaption
            // 
            TransactionDetailPanel_Label_TypeCaption.AutoSize = true;
            TransactionDetailPanel_Label_TypeCaption.Dock = DockStyle.Fill;
            TransactionDetailPanel_Label_TypeCaption.Location = new Point(3, 24);
            TransactionDetailPanel_Label_TypeCaption.Margin = new Padding(3);
            TransactionDetailPanel_Label_TypeCaption.Name = "TransactionDetailPanel_Label_TypeCaption";
            TransactionDetailPanel_Label_TypeCaption.Size = new Size(1, 15);
            TransactionDetailPanel_Label_TypeCaption.TabIndex = 2;
            TransactionDetailPanel_Label_TypeCaption.TextAlign = ContentAlignment.MiddleRight;
            // 
            // TransactionDetailPanel_Label_TypeValue
            // 
            TransactionDetailPanel_Label_TypeValue.AutoSize = true;
            TransactionDetailPanel_Label_TypeValue.Dock = DockStyle.Fill;
            TransactionDetailPanel_Label_TypeValue.Location = new Point(9, 24);
            TransactionDetailPanel_Label_TypeValue.Margin = new Padding(3);
            TransactionDetailPanel_Label_TypeValue.Name = "TransactionDetailPanel_Label_TypeValue";
            TransactionDetailPanel_Label_TypeValue.Size = new Size(263, 15);
            TransactionDetailPanel_Label_TypeValue.TabIndex = 3;
            TransactionDetailPanel_Label_TypeValue.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TransactionDetailPanel_Label_ItemTypeCaption
            // 
            TransactionDetailPanel_Label_ItemTypeCaption.AutoSize = true;
            TransactionDetailPanel_Label_ItemTypeCaption.Dock = DockStyle.Fill;
            TransactionDetailPanel_Label_ItemTypeCaption.Location = new Point(3, 45);
            TransactionDetailPanel_Label_ItemTypeCaption.Margin = new Padding(3);
            TransactionDetailPanel_Label_ItemTypeCaption.Name = "TransactionDetailPanel_Label_ItemTypeCaption";
            TransactionDetailPanel_Label_ItemTypeCaption.Size = new Size(1, 15);
            TransactionDetailPanel_Label_ItemTypeCaption.TabIndex = 4;
            TransactionDetailPanel_Label_ItemTypeCaption.TextAlign = ContentAlignment.MiddleRight;
            // 
            // TransactionDetailPanel_Label_ItemTypeValue
            // 
            TransactionDetailPanel_Label_ItemTypeValue.AutoSize = true;
            TransactionDetailPanel_Label_ItemTypeValue.Dock = DockStyle.Fill;
            TransactionDetailPanel_Label_ItemTypeValue.Location = new Point(9, 45);
            TransactionDetailPanel_Label_ItemTypeValue.Margin = new Padding(3);
            TransactionDetailPanel_Label_ItemTypeValue.Name = "TransactionDetailPanel_Label_ItemTypeValue";
            TransactionDetailPanel_Label_ItemTypeValue.Size = new Size(263, 15);
            TransactionDetailPanel_Label_ItemTypeValue.TabIndex = 5;
            TransactionDetailPanel_Label_ItemTypeValue.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TransactionDetailPanel_Label_PartCaption
            // 
            TransactionDetailPanel_Label_PartCaption.AutoSize = true;
            TransactionDetailPanel_Label_PartCaption.Dock = DockStyle.Fill;
            TransactionDetailPanel_Label_PartCaption.Location = new Point(3, 66);
            TransactionDetailPanel_Label_PartCaption.Margin = new Padding(3);
            TransactionDetailPanel_Label_PartCaption.Name = "TransactionDetailPanel_Label_PartCaption";
            TransactionDetailPanel_Label_PartCaption.Size = new Size(1, 15);
            TransactionDetailPanel_Label_PartCaption.TabIndex = 6;
            TransactionDetailPanel_Label_PartCaption.TextAlign = ContentAlignment.MiddleRight;
            // 
            // TransactionDetailPanel_Label_PartValue
            // 
            TransactionDetailPanel_Label_PartValue.AutoSize = true;
            TransactionDetailPanel_Label_PartValue.Dock = DockStyle.Fill;
            TransactionDetailPanel_Label_PartValue.Location = new Point(9, 66);
            TransactionDetailPanel_Label_PartValue.Margin = new Padding(3);
            TransactionDetailPanel_Label_PartValue.Name = "TransactionDetailPanel_Label_PartValue";
            TransactionDetailPanel_Label_PartValue.Size = new Size(263, 15);
            TransactionDetailPanel_Label_PartValue.TabIndex = 7;
            TransactionDetailPanel_Label_PartValue.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TransactionDetailPanel_Label_BatchCaption
            // 
            TransactionDetailPanel_Label_BatchCaption.AutoSize = true;
            TransactionDetailPanel_Label_BatchCaption.Dock = DockStyle.Fill;
            TransactionDetailPanel_Label_BatchCaption.Location = new Point(3, 87);
            TransactionDetailPanel_Label_BatchCaption.Margin = new Padding(3);
            TransactionDetailPanel_Label_BatchCaption.Name = "TransactionDetailPanel_Label_BatchCaption";
            TransactionDetailPanel_Label_BatchCaption.Size = new Size(1, 15);
            TransactionDetailPanel_Label_BatchCaption.TabIndex = 8;
            TransactionDetailPanel_Label_BatchCaption.TextAlign = ContentAlignment.MiddleRight;
            // 
            // TransactionDetailPanel_Label_BatchValue
            // 
            TransactionDetailPanel_Label_BatchValue.AutoSize = true;
            TransactionDetailPanel_Label_BatchValue.Dock = DockStyle.Fill;
            TransactionDetailPanel_Label_BatchValue.Location = new Point(9, 87);
            TransactionDetailPanel_Label_BatchValue.Margin = new Padding(3);
            TransactionDetailPanel_Label_BatchValue.Name = "TransactionDetailPanel_Label_BatchValue";
            TransactionDetailPanel_Label_BatchValue.Size = new Size(263, 15);
            TransactionDetailPanel_Label_BatchValue.TabIndex = 9;
            TransactionDetailPanel_Label_BatchValue.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TransactionDetailPanel_Label_QuantityCaption
            // 
            TransactionDetailPanel_Label_QuantityCaption.AutoSize = true;
            TransactionDetailPanel_Label_QuantityCaption.Dock = DockStyle.Fill;
            TransactionDetailPanel_Label_QuantityCaption.Location = new Point(3, 108);
            TransactionDetailPanel_Label_QuantityCaption.Margin = new Padding(3);
            TransactionDetailPanel_Label_QuantityCaption.Name = "TransactionDetailPanel_Label_QuantityCaption";
            TransactionDetailPanel_Label_QuantityCaption.Size = new Size(1, 15);
            TransactionDetailPanel_Label_QuantityCaption.TabIndex = 10;
            TransactionDetailPanel_Label_QuantityCaption.TextAlign = ContentAlignment.MiddleRight;
            // 
            // TransactionDetailPanel_Label_QuantityValue
            // 
            TransactionDetailPanel_Label_QuantityValue.AutoSize = true;
            TransactionDetailPanel_Label_QuantityValue.Dock = DockStyle.Fill;
            TransactionDetailPanel_Label_QuantityValue.Location = new Point(9, 108);
            TransactionDetailPanel_Label_QuantityValue.Margin = new Padding(3);
            TransactionDetailPanel_Label_QuantityValue.Name = "TransactionDetailPanel_Label_QuantityValue";
            TransactionDetailPanel_Label_QuantityValue.Size = new Size(263, 15);
            TransactionDetailPanel_Label_QuantityValue.TabIndex = 11;
            TransactionDetailPanel_Label_QuantityValue.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TransactionDetailPanel_Label_FromCaption
            // 
            TransactionDetailPanel_Label_FromCaption.AutoSize = true;
            TransactionDetailPanel_Label_FromCaption.Dock = DockStyle.Fill;
            TransactionDetailPanel_Label_FromCaption.Location = new Point(3, 129);
            TransactionDetailPanel_Label_FromCaption.Margin = new Padding(3);
            TransactionDetailPanel_Label_FromCaption.Name = "TransactionDetailPanel_Label_FromCaption";
            TransactionDetailPanel_Label_FromCaption.Size = new Size(1, 15);
            TransactionDetailPanel_Label_FromCaption.TabIndex = 12;
            TransactionDetailPanel_Label_FromCaption.TextAlign = ContentAlignment.MiddleRight;
            // 
            // TransactionDetailPanel_Label_FromValue
            // 
            TransactionDetailPanel_Label_FromValue.AutoSize = true;
            TransactionDetailPanel_Label_FromValue.Dock = DockStyle.Fill;
            TransactionDetailPanel_Label_FromValue.Location = new Point(9, 129);
            TransactionDetailPanel_Label_FromValue.Margin = new Padding(3);
            TransactionDetailPanel_Label_FromValue.Name = "TransactionDetailPanel_Label_FromValue";
            TransactionDetailPanel_Label_FromValue.Size = new Size(263, 15);
            TransactionDetailPanel_Label_FromValue.TabIndex = 13;
            TransactionDetailPanel_Label_FromValue.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TransactionDetailPanel_Label_ToCaption
            // 
            TransactionDetailPanel_Label_ToCaption.AutoSize = true;
            TransactionDetailPanel_Label_ToCaption.Dock = DockStyle.Fill;
            TransactionDetailPanel_Label_ToCaption.Location = new Point(3, 150);
            TransactionDetailPanel_Label_ToCaption.Margin = new Padding(3);
            TransactionDetailPanel_Label_ToCaption.Name = "TransactionDetailPanel_Label_ToCaption";
            TransactionDetailPanel_Label_ToCaption.Size = new Size(1, 15);
            TransactionDetailPanel_Label_ToCaption.TabIndex = 14;
            TransactionDetailPanel_Label_ToCaption.TextAlign = ContentAlignment.MiddleRight;
            // 
            // TransactionDetailPanel_Label_ToValue
            // 
            TransactionDetailPanel_Label_ToValue.AutoSize = true;
            TransactionDetailPanel_Label_ToValue.Dock = DockStyle.Fill;
            TransactionDetailPanel_Label_ToValue.Location = new Point(9, 150);
            TransactionDetailPanel_Label_ToValue.Margin = new Padding(3);
            TransactionDetailPanel_Label_ToValue.Name = "TransactionDetailPanel_Label_ToValue";
            TransactionDetailPanel_Label_ToValue.Size = new Size(263, 15);
            TransactionDetailPanel_Label_ToValue.TabIndex = 15;
            TransactionDetailPanel_Label_ToValue.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TransactionDetailPanel_Label_OperationCaption
            // 
            TransactionDetailPanel_Label_OperationCaption.AutoSize = true;
            TransactionDetailPanel_Label_OperationCaption.Dock = DockStyle.Fill;
            TransactionDetailPanel_Label_OperationCaption.Location = new Point(3, 171);
            TransactionDetailPanel_Label_OperationCaption.Margin = new Padding(3);
            TransactionDetailPanel_Label_OperationCaption.Name = "TransactionDetailPanel_Label_OperationCaption";
            TransactionDetailPanel_Label_OperationCaption.Size = new Size(1, 15);
            TransactionDetailPanel_Label_OperationCaption.TabIndex = 16;
            TransactionDetailPanel_Label_OperationCaption.TextAlign = ContentAlignment.MiddleRight;
            // 
            // TransactionDetailPanel_Label_OperationValue
            // 
            TransactionDetailPanel_Label_OperationValue.AutoSize = true;
            TransactionDetailPanel_Label_OperationValue.Dock = DockStyle.Fill;
            TransactionDetailPanel_Label_OperationValue.Location = new Point(9, 171);
            TransactionDetailPanel_Label_OperationValue.Margin = new Padding(3);
            TransactionDetailPanel_Label_OperationValue.Name = "TransactionDetailPanel_Label_OperationValue";
            TransactionDetailPanel_Label_OperationValue.Size = new Size(263, 15);
            TransactionDetailPanel_Label_OperationValue.TabIndex = 17;
            TransactionDetailPanel_Label_OperationValue.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TransactionDetailPanel_Label_UserCaption
            // 
            TransactionDetailPanel_Label_UserCaption.AutoSize = true;
            TransactionDetailPanel_Label_UserCaption.Dock = DockStyle.Fill;
            TransactionDetailPanel_Label_UserCaption.Location = new Point(3, 192);
            TransactionDetailPanel_Label_UserCaption.Margin = new Padding(3);
            TransactionDetailPanel_Label_UserCaption.Name = "TransactionDetailPanel_Label_UserCaption";
            TransactionDetailPanel_Label_UserCaption.Size = new Size(1, 15);
            TransactionDetailPanel_Label_UserCaption.TabIndex = 18;
            TransactionDetailPanel_Label_UserCaption.TextAlign = ContentAlignment.MiddleRight;
            // 
            // TransactionDetailPanel_Label_UserValue
            // 
            TransactionDetailPanel_Label_UserValue.AutoSize = true;
            TransactionDetailPanel_Label_UserValue.Dock = DockStyle.Fill;
            TransactionDetailPanel_Label_UserValue.Location = new Point(9, 192);
            TransactionDetailPanel_Label_UserValue.Margin = new Padding(3);
            TransactionDetailPanel_Label_UserValue.Name = "TransactionDetailPanel_Label_UserValue";
            TransactionDetailPanel_Label_UserValue.Size = new Size(263, 15);
            TransactionDetailPanel_Label_UserValue.TabIndex = 19;
            TransactionDetailPanel_Label_UserValue.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TransactionDetailPanel_Label_DateCaption
            // 
            TransactionDetailPanel_Label_DateCaption.AutoSize = true;
            TransactionDetailPanel_Label_DateCaption.Dock = DockStyle.Fill;
            TransactionDetailPanel_Label_DateCaption.Location = new Point(3, 213);
            TransactionDetailPanel_Label_DateCaption.Margin = new Padding(3);
            TransactionDetailPanel_Label_DateCaption.Name = "TransactionDetailPanel_Label_DateCaption";
            TransactionDetailPanel_Label_DateCaption.Size = new Size(1, 15);
            TransactionDetailPanel_Label_DateCaption.TabIndex = 20;
            TransactionDetailPanel_Label_DateCaption.TextAlign = ContentAlignment.MiddleRight;
            // 
            // TransactionDetailPanel_Label_DateValue
            // 
            TransactionDetailPanel_Label_DateValue.AutoSize = true;
            TransactionDetailPanel_Label_DateValue.Dock = DockStyle.Fill;
            TransactionDetailPanel_Label_DateValue.Location = new Point(9, 213);
            TransactionDetailPanel_Label_DateValue.Margin = new Padding(3);
            TransactionDetailPanel_Label_DateValue.Name = "TransactionDetailPanel_Label_DateValue";
            TransactionDetailPanel_Label_DateValue.Size = new Size(263, 15);
            TransactionDetailPanel_Label_DateValue.TabIndex = 21;
            TransactionDetailPanel_Label_DateValue.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TransactionDetailPanel_Label_NotesCaption
            // 
            TransactionDetailPanel_Label_NotesCaption.AutoSize = true;
            TransactionDetailPanel_Label_NotesCaption.Dock = DockStyle.Fill;
            TransactionDetailPanel_Label_NotesCaption.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            TransactionDetailPanel_Label_NotesCaption.Location = new Point(3, 240);
            TransactionDetailPanel_Label_NotesCaption.Margin = new Padding(3);
            TransactionDetailPanel_Label_NotesCaption.Name = "TransactionDetailPanel_Label_NotesCaption";
            TransactionDetailPanel_Label_NotesCaption.Size = new Size(275, 15);
            TransactionDetailPanel_Label_NotesCaption.TabIndex = 2;
            TransactionDetailPanel_Label_NotesCaption.Text = "Notes";
            // 
            // TransactionDetailPanel
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(TransactionDetailPanel_TableLayout_Main);
            Name = "TransactionDetailPanel";
            Size = new Size(307, 430);
            TransactionDetailPanel_TableLayout_Main.ResumeLayout(false);
            TransactionDetailPanel_TableLayout_Main.PerformLayout();
            TransactionDetailPanel_GroupBox_Main.ResumeLayout(false);
            TransactionDetailPanel_GroupBox_Main.PerformLayout();
            TransactionDetailPanel_TableLayout_Inner.ResumeLayout(false);
            TransactionDetailPanel_TableLayout_Inner.PerformLayout();
            TransactionDetailPanel_TableLayout_RelatedHeader.ResumeLayout(false);
            TransactionDetailPanel_TableLayout_RelatedHeader.PerformLayout();
            TransactionDetailPanel_TableLayout_Details.ResumeLayout(false);
            TransactionDetailPanel_TableLayout_Details.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private static void ConfigureCaptionLabel(Label label, string text)
        {
            label.AutoSize = true;
            label.Dock = DockStyle.Fill;
            label.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label.Margin = new Padding(0, 0, 6, 6);
            label.Text = text;
            label.TextAlign = ContentAlignment.MiddleLeft;
        }

        private static void ConfigureValueLabel(Label label)
        {
            label.AutoSize = true;
            label.Dock = DockStyle.Fill;
            label.Margin = new Padding(0, 0, 0, 6);
            label.Text = "—";
            label.TextAlign = ContentAlignment.MiddleLeft;
        }

        #endregion

        private TableLayoutPanel TransactionDetailPanel_TableLayout_Main;
        private TableLayoutPanel TransactionDetailPanel_TableLayout_Details;
        private Label TransactionDetailPanel_Label_IdCaption;
        private Label TransactionDetailPanel_Label_IdValue;
        private Label TransactionDetailPanel_Label_TypeCaption;
        private Label TransactionDetailPanel_Label_TypeValue;
        private Label TransactionDetailPanel_Label_ItemTypeCaption;
        private Label TransactionDetailPanel_Label_ItemTypeValue;
        private Label TransactionDetailPanel_Label_PartCaption;
        private Label TransactionDetailPanel_Label_PartValue;
        private Label TransactionDetailPanel_Label_BatchCaption;
        private Label TransactionDetailPanel_Label_BatchValue;
        private Label TransactionDetailPanel_Label_QuantityCaption;
        private Label TransactionDetailPanel_Label_QuantityValue;
        private Label TransactionDetailPanel_Label_FromCaption;
        private Label TransactionDetailPanel_Label_FromValue;
        private Label TransactionDetailPanel_Label_ToCaption;
        private Label TransactionDetailPanel_Label_ToValue;
        private Label TransactionDetailPanel_Label_OperationCaption;
        private Label TransactionDetailPanel_Label_OperationValue;
        private Label TransactionDetailPanel_Label_UserCaption;
        private Label TransactionDetailPanel_Label_UserValue;
        private Label TransactionDetailPanel_Label_DateCaption;
        private Label TransactionDetailPanel_Label_DateValue;
        private Label TransactionDetailPanel_Label_NotesCaption;
        private TextBox TransactionDetailPanel_TextBox_Notes;
        private TableLayoutPanel TransactionDetailPanel_TableLayout_RelatedHeader;
        private Label TransactionDetailPanel_Label_RelatedTitle;
        private Button TransactionDetailPanel_Button_ViewBatchHistory;
        private Label TransactionDetailPanel_Label_RelatedStatus;
        private GroupBox TransactionDetailPanel_GroupBox_Main;
        private TableLayoutPanel TransactionDetailPanel_TableLayout_Inner;
    }
}

#nullable restore
