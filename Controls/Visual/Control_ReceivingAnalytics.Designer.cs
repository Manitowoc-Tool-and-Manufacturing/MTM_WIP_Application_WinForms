using MTM_WIP_Application_Winforms.Forms.Shared;

namespace MTM_WIP_Application_Winforms.Controls.Visual
{
    partial class Control_ReceivingAnalytics
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.panelFilters = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.chkShowService = new System.Windows.Forms.CheckBox();
            this.chkShowInternal = new System.Windows.Forms.CheckBox();
            this.chkShowConsignment = new System.Windows.Forms.CheckBox();
            this.chkIncludeClosed = new System.Windows.Forms.CheckBox();
            this.cmbDateType = new System.Windows.Forms.ComboBox();
            this.labelDateType = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.labelTo = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.labelDateRange = new System.Windows.Forms.Label();
            this.dataGridViewResults = new System.Windows.Forms.DataGridView();
            this.panelLegend = new System.Windows.Forms.FlowLayoutPanel();
            this.labelLegendTitle = new System.Windows.Forms.Label();
            this.panelLegendClosed = new System.Windows.Forms.Panel();
            this.labelLegendClosed = new System.Windows.Forms.Label();
            this.panelLegendLate = new System.Windows.Forms.Panel();
            this.labelLegendLate = new System.Windows.Forms.Label();
            this.panelLegendPartial = new System.Windows.Forms.Panel();
            this.labelLegendPartial = new System.Windows.Forms.Label();
            this.panelLegendOnTime = new System.Windows.Forms.Panel();
            this.labelLegendOnTime = new System.Windows.Forms.Label();

            this.tableLayoutPanelMain.SuspendLayout();
            this.panelFilters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResults)).BeginInit();
            this.panelLegend.SuspendLayout();
            this.SuspendLayout();

            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 1;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.Controls.Add(this.panelFilters, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.dataGridViewResults, 0, 1);
            this.tableLayoutPanelMain.Controls.Add(this.panelLegend, 0, 2);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 3;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(1000, 600);
            this.tableLayoutPanelMain.TabIndex = 0;

            // 
            // panelFilters
            // 
            this.panelFilters.Controls.Add(this.txtPOFilter);
            this.panelFilters.Controls.Add(this.labelPOFilter);
            this.panelFilters.Controls.Add(this.txtVendorFilter);
            this.panelFilters.Controls.Add(this.labelVendorFilter);
            this.panelFilters.Controls.Add(this.chkShowWithPartNumber);
            this.panelFilters.Controls.Add(this.chkShowOnTime);
            this.panelFilters.Controls.Add(this.chkShowPartial);
            this.panelFilters.Controls.Add(this.chkShowLate);
            this.panelFilters.Controls.Add(this.btnSearch);
            this.panelFilters.Controls.Add(this.chkShowService);
            this.panelFilters.Controls.Add(this.chkShowInternal);
            this.panelFilters.Controls.Add(this.chkShowConsignment);
            this.panelFilters.Controls.Add(this.chkIncludeClosed);
            this.panelFilters.Controls.Add(this.cmbDateType);
            this.panelFilters.Controls.Add(this.labelDateType);
            this.panelFilters.Controls.Add(this.dtpEndDate);
            this.panelFilters.Controls.Add(this.labelTo);
            this.panelFilters.Controls.Add(this.dtpStartDate);
            this.panelFilters.Controls.Add(this.labelDateRange);
            this.panelFilters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFilters.Location = new System.Drawing.Point(3, 3);
            this.panelFilters.Name = "panelFilters";
            this.panelFilters.Size = new System.Drawing.Size(994, 140);
            this.panelFilters.TabIndex = 0;

            // 
            // labelDateRange
            // 
            this.labelDateRange.AutoSize = true;
            this.labelDateRange.Location = new System.Drawing.Point(15, 15);
            this.labelDateRange.Name = "labelDateRange";
            this.labelDateRange.Size = new System.Drawing.Size(70, 15);
            this.labelDateRange.TabIndex = 0;
            this.labelDateRange.Text = "Date Range:";

            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(91, 12);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(100, 23);
            this.dtpStartDate.TabIndex = 1;

            // 
            // labelTo
            // 
            this.labelTo.AutoSize = true;
            this.labelTo.Location = new System.Drawing.Point(197, 15);
            this.labelTo.Name = "labelTo";
            this.labelTo.Size = new System.Drawing.Size(19, 15);
            this.labelTo.TabIndex = 2;
            this.labelTo.Text = "to";

            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(222, 12);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(100, 23);
            this.dtpEndDate.TabIndex = 3;

            // 
            // labelDateType
            // 
            this.labelDateType.AutoSize = true;
            this.labelDateType.Location = new System.Drawing.Point(340, 15);
            this.labelDateType.Name = "labelDateType";
            this.labelDateType.Size = new System.Drawing.Size(61, 15);
            this.labelDateType.TabIndex = 4;
            this.labelDateType.Text = "Filter By:";

            // 
            // cmbDateType
            // 
            this.cmbDateType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDateType.FormattingEnabled = true;
            this.cmbDateType.Items.AddRange(new object[] {
            "PO Desired Date",
            "PO Promise Date",
            "Line Desired Date",
            "Line Promise Date",
            "Any of the Above"});
            this.cmbDateType.Location = new System.Drawing.Point(407, 12);
            this.cmbDateType.Name = "cmbDateType";
            this.cmbDateType.Size = new System.Drawing.Size(150, 23);
            this.cmbDateType.TabIndex = 5;

            // 
            // labelVendorFilter
            // 
            this.labelVendorFilter.AutoSize = true;
            this.labelVendorFilter.Location = new System.Drawing.Point(570, 15);
            this.labelVendorFilter.Name = "labelVendorFilter";
            this.labelVendorFilter.Size = new System.Drawing.Size(47, 15);
            this.labelVendorFilter.TabIndex = 11;
            this.labelVendorFilter.Text = "Vendor:";

            // 
            // txtVendorFilter
            // 
            this.txtVendorFilter.Location = new System.Drawing.Point(623, 12);
            this.txtVendorFilter.Name = "txtVendorFilter";
            this.txtVendorFilter.Size = new System.Drawing.Size(120, 23);
            this.txtVendorFilter.TabIndex = 12;

            // 
            // labelPOFilter
            // 
            this.labelPOFilter.AutoSize = true;
            this.labelPOFilter.Location = new System.Drawing.Point(755, 15);
            this.labelPOFilter.Name = "labelPOFilter";
            this.labelPOFilter.Size = new System.Drawing.Size(37, 15);
            this.labelPOFilter.TabIndex = 13;
            this.labelPOFilter.Text = "PO #:";

            // 
            // txtPOFilter
            // 
            this.txtPOFilter.Location = new System.Drawing.Point(798, 12);
            this.txtPOFilter.Name = "txtPOFilter";
            this.txtPOFilter.Size = new System.Drawing.Size(100, 23);
            this.txtPOFilter.TabIndex = 14;

            // 
            // chkIncludeClosed
            // 
            this.chkIncludeClosed.AutoSize = true;
            this.chkIncludeClosed.Location = new System.Drawing.Point(18, 50);
            this.chkIncludeClosed.Name = "chkIncludeClosed";
            this.chkIncludeClosed.Size = new System.Drawing.Size(105, 19);
            this.chkIncludeClosed.TabIndex = 6;
            this.chkIncludeClosed.Text = "Include Closed";
            this.chkIncludeClosed.UseVisualStyleBackColor = true;

            // 
            // chkShowConsignment
            // 
            this.chkShowConsignment.AutoSize = true;
            this.chkShowConsignment.Location = new System.Drawing.Point(140, 50);
            this.chkShowConsignment.Name = "chkShowConsignment";
            this.chkShowConsignment.Size = new System.Drawing.Size(130, 19);
            this.chkShowConsignment.TabIndex = 7;
            this.chkShowConsignment.Text = "Show Consignment";
            this.chkShowConsignment.UseVisualStyleBackColor = true;

            // 
            // chkShowInternal
            // 
            this.chkShowInternal.AutoSize = true;
            this.chkShowInternal.Location = new System.Drawing.Point(280, 50);
            this.chkShowInternal.Name = "chkShowInternal";
            this.chkShowInternal.Size = new System.Drawing.Size(138, 19);
            this.chkShowInternal.TabIndex = 8;
            this.chkShowInternal.Text = "Show Internal Orders";
            this.chkShowInternal.UseVisualStyleBackColor = true;

            // 
            // chkShowService
            // 
            this.chkShowService.AutoSize = true;
            this.chkShowService.Location = new System.Drawing.Point(430, 50);
            this.chkShowService.Name = "chkShowService";
            this.chkShowService.Size = new System.Drawing.Size(126, 19);
            this.chkShowService.TabIndex = 9;
            this.chkShowService.Text = "Show Service Items";
            this.chkShowService.UseVisualStyleBackColor = true;

            // 
            // chkShowLate
            // 
            this.chkShowLate.AutoSize = true;
            this.chkShowLate.Location = new System.Drawing.Point(18, 80);
            this.chkShowLate.Name = "chkShowLate";
            this.chkShowLate.Size = new System.Drawing.Size(80, 19);
            this.chkShowLate.TabIndex = 15;
            this.chkShowLate.Text = "Show Late";
            this.chkShowLate.UseVisualStyleBackColor = true;
            this.chkShowLate.Checked = true;

            // 
            // chkShowPartial
            // 
            this.chkShowPartial.AutoSize = true;
            this.chkShowPartial.Location = new System.Drawing.Point(140, 80);
            this.chkShowPartial.Name = "chkShowPartial";
            this.chkShowPartial.Size = new System.Drawing.Size(91, 19);
            this.chkShowPartial.TabIndex = 16;
            this.chkShowPartial.Text = "Show Partial";
            this.chkShowPartial.UseVisualStyleBackColor = true;
            this.chkShowPartial.Checked = true;

            // 
            // chkShowOnTime
            // 
            this.chkShowOnTime.AutoSize = true;
            this.chkShowOnTime.Location = new System.Drawing.Point(280, 80);
            this.chkShowOnTime.Name = "chkShowOnTime";
            this.chkShowOnTime.Size = new System.Drawing.Size(103, 19);
            this.chkShowOnTime.TabIndex = 17;
            this.chkShowOnTime.Text = "Show On Time";
            this.chkShowOnTime.UseVisualStyleBackColor = true;
            this.chkShowOnTime.Checked = true;

            // 
            // chkShowWithPartNumber
            // 
            this.chkShowWithPartNumber.AutoSize = true;
            this.chkShowWithPartNumber.Location = new System.Drawing.Point(430, 80);
            this.chkShowWithPartNumber.Name = "chkShowWithPartNumber";
            this.chkShowWithPartNumber.Size = new System.Drawing.Size(150, 19);
            this.chkShowWithPartNumber.TabIndex = 18;
            this.chkShowWithPartNumber.Text = "Must Have Part Number";
            this.chkShowWithPartNumber.UseVisualStyleBackColor = true;

            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(910, 12);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(80, 80);
            this.btnSearch.TabIndex = 10;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);

            // 
            // dataGridViewResults
            // 
            this.dataGridViewResults.AllowUserToAddRows = false;
            this.dataGridViewResults.AllowUserToDeleteRows = false;
            this.dataGridViewResults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewResults.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewResults.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewResults.Location = new System.Drawing.Point(3, 103);
            this.dataGridViewResults.Name = "dataGridViewResults";
            this.dataGridViewResults.ReadOnly = true;
            this.dataGridViewResults.RowHeadersVisible = false;
            this.dataGridViewResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewResults.Size = new System.Drawing.Size(994, 454);
            this.dataGridViewResults.TabIndex = 1;
            this.dataGridViewResults.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewResults_CellFormatting);

            // 
            // panelLegend
            // 
            this.panelLegend.Controls.Add(this.labelLegendTitle);
            this.panelLegend.Controls.Add(this.panelLegendClosed);
            this.panelLegend.Controls.Add(this.labelLegendClosed);
            this.panelLegend.Controls.Add(this.panelLegendLate);
            this.panelLegend.Controls.Add(this.labelLegendLate);
            this.panelLegend.Controls.Add(this.panelLegendPartial);
            this.panelLegend.Controls.Add(this.labelLegendPartial);
            this.panelLegend.Controls.Add(this.panelLegendOnTime);
            this.panelLegend.Controls.Add(this.labelLegendOnTime);
            this.panelLegend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLegend.Location = new System.Drawing.Point(3, 563);
            this.panelLegend.Name = "panelLegend";
            this.panelLegend.Size = new System.Drawing.Size(994, 34);
            this.panelLegend.TabIndex = 2;
            this.panelLegend.Padding = new System.Windows.Forms.Padding(5);

            // 
            // labelLegendTitle
            // 
            this.labelLegendTitle.AutoSize = true;
            this.labelLegendTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.labelLegendTitle.Location = new System.Drawing.Point(8, 5);
            this.labelLegendTitle.Margin = new System.Windows.Forms.Padding(3, 0, 10, 0);
            this.labelLegendTitle.Name = "labelLegendTitle";
            this.labelLegendTitle.Size = new System.Drawing.Size(51, 15);
            this.labelLegendTitle.TabIndex = 0;
            this.labelLegendTitle.Text = "Legend:";
            this.labelLegendTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // 
            // panelLegendClosed
            // 
            this.panelLegendClosed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(255)))), ((int)(((byte)(200)))));
            this.panelLegendClosed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelLegendClosed.Location = new System.Drawing.Point(72, 8);
            this.panelLegendClosed.Name = "panelLegendClosed";
            this.panelLegendClosed.Size = new System.Drawing.Size(15, 15);
            this.panelLegendClosed.TabIndex = 1;

            // 
            // labelLegendClosed
            // 
            this.labelLegendClosed.AutoSize = true;
            this.labelLegendClosed.Location = new System.Drawing.Point(93, 5);
            this.labelLegendClosed.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.labelLegendClosed.Name = "labelLegendClosed";
            this.labelLegendClosed.Size = new System.Drawing.Size(43, 15);
            this.labelLegendClosed.TabIndex = 2;
            this.labelLegendClosed.Text = "Closed";
            this.labelLegendClosed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // 
            // panelLegendLate
            // 
            this.panelLegendLate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.panelLegendLate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelLegendLate.Location = new System.Drawing.Point(154, 8);
            this.panelLegendLate.Name = "panelLegendLate";
            this.panelLegendLate.Size = new System.Drawing.Size(15, 15);
            this.panelLegendLate.TabIndex = 3;

            // 
            // labelLegendLate
            // 
            this.labelLegendLate.AutoSize = true;
            this.labelLegendLate.Location = new System.Drawing.Point(175, 5);
            this.labelLegendLate.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.labelLegendLate.Name = "labelLegendLate";
            this.labelLegendLate.Size = new System.Drawing.Size(29, 15);
            this.labelLegendLate.TabIndex = 4;
            this.labelLegendLate.Text = "Late";
            this.labelLegendLate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // 
            // panelLegendPartial
            // 
            this.panelLegendPartial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(200)))));
            this.panelLegendPartial.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelLegendPartial.Location = new System.Drawing.Point(222, 8);
            this.panelLegendPartial.Name = "panelLegendPartial";
            this.panelLegendPartial.Size = new System.Drawing.Size(15, 15);
            this.panelLegendPartial.TabIndex = 5;

            // 
            // labelLegendPartial
            // 
            this.labelLegendPartial.AutoSize = true;
            this.labelLegendPartial.Location = new System.Drawing.Point(243, 5);
            this.labelLegendPartial.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.labelLegendPartial.Name = "labelLegendPartial";
            this.labelLegendPartial.Size = new System.Drawing.Size(40, 15);
            this.labelLegendPartial.TabIndex = 6;
            this.labelLegendPartial.Text = "Partial";
            this.labelLegendPartial.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // 
            // panelLegendOnTime
            // 
            this.panelLegendOnTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.panelLegendOnTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelLegendOnTime.Location = new System.Drawing.Point(301, 8);
            this.panelLegendOnTime.Name = "panelLegendOnTime";
            this.panelLegendOnTime.Size = new System.Drawing.Size(15, 15);
            this.panelLegendOnTime.TabIndex = 7;

            // 
            // labelLegendOnTime
            // 
            this.labelLegendOnTime.AutoSize = true;
            this.labelLegendOnTime.Location = new System.Drawing.Point(322, 5);
            this.labelLegendOnTime.Margin = new System.Windows.Forms.Padding(3, 0, 15, 0);
            this.labelLegendOnTime.Name = "labelLegendOnTime";
            this.labelLegendOnTime.Size = new System.Drawing.Size(99, 15);
            this.labelLegendOnTime.TabIndex = 8;
            this.labelLegendOnTime.Text = "On Time / Open";
            this.labelLegendOnTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // 
            // Control_ReceivingAnalytics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Name = "Control_ReceivingAnalytics";
            this.Size = new System.Drawing.Size(1000, 600);
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.panelFilters.ResumeLayout(false);
            this.panelFilters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResults)).EndInit();
            this.panelLegend.ResumeLayout(false);
            this.panelLegend.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.Panel panelFilters;
        private System.Windows.Forms.DataGridView dataGridViewResults;
        private System.Windows.Forms.FlowLayoutPanel panelLegend;
        private System.Windows.Forms.Label labelDateRange;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label labelTo;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label labelDateType;
        private System.Windows.Forms.ComboBox cmbDateType;
        private System.Windows.Forms.CheckBox chkIncludeClosed;
        private System.Windows.Forms.CheckBox chkShowConsignment;
        private System.Windows.Forms.CheckBox chkShowInternal;
        private System.Windows.Forms.CheckBox chkShowService;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label labelLegendTitle;
        private System.Windows.Forms.Panel panelLegendClosed;
        private System.Windows.Forms.Label labelLegendClosed;
        private System.Windows.Forms.Panel panelLegendLate;
        private System.Windows.Forms.Label labelLegendLate;
        private System.Windows.Forms.Panel panelLegendPartial;
        private System.Windows.Forms.Label labelLegendPartial;
        private System.Windows.Forms.Panel panelLegendOnTime;
        private System.Windows.Forms.Label labelLegendOnTime;
        private System.Windows.Forms.Label labelVendorFilter;
        private System.Windows.Forms.TextBox txtVendorFilter;
        private System.Windows.Forms.Label labelPOFilter;
        private System.Windows.Forms.TextBox txtPOFilter;
        private System.Windows.Forms.CheckBox chkShowLate;
        private System.Windows.Forms.CheckBox chkShowPartial;
        private System.Windows.Forms.CheckBox chkShowOnTime;
        private System.Windows.Forms.CheckBox chkShowWithPartNumber;
    }
}
