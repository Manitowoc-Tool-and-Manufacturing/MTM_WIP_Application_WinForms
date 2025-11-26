using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Services;
using MTM_WIP_Application_Winforms.Services.Visual;
using Microsoft.Extensions.DependencyInjection;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Forms.Visual
{
    public partial class Form_PODetails : ThemedForm
    {
        private readonly string _poNumber;
        private readonly IService_VisualDatabase? _visualService;
        private DataGridView? _dataGridView;
        private Label? _lblLoading;

        public Form_PODetails(string poNumber)
        {
            _poNumber = poNumber;
            _visualService = Program.ServiceProvider?.GetService<IService_VisualDatabase>();
            
            InitializeComponent();
            this.Text = $"PO Details: {_poNumber}";
            this.Size = new Size(800, 500);
            this.StartPosition = FormStartPosition.CenterParent;

            this.Load += Form_PODetails_Load;
        }

        private void InitializeComponent()
        {
            _dataGridView = new DataGridView();
            _lblLoading = new Label();
            ((System.ComponentModel.ISupportInitialize)(_dataGridView)).BeginInit();
            this.SuspendLayout();

            // 
            // _dataGridView
            // 
            _dataGridView.AllowUserToAddRows = false;
            _dataGridView.AllowUserToDeleteRows = false;
            _dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _dataGridView.BackgroundColor = Color.White;
            _dataGridView.BorderStyle = BorderStyle.None;
            _dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _dataGridView.Dock = DockStyle.Fill;
            _dataGridView.Location = new Point(0, 0);
            _dataGridView.Name = "_dataGridView";
            _dataGridView.ReadOnly = true;
            _dataGridView.RowHeadersVisible = false;
            _dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _dataGridView.TabIndex = 0;
            _dataGridView.Visible = false;

            // 
            // _lblLoading
            // 
            _lblLoading.AutoSize = true;
            _lblLoading.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            _lblLoading.Location = new Point(350, 220);
            _lblLoading.Name = "_lblLoading";
            _lblLoading.Size = new Size(78, 21);
            _lblLoading.TabIndex = 1;
            _lblLoading.Text = "Loading...";

            this.Controls.Add(_lblLoading);
            this.Controls.Add(_dataGridView);
            ((System.ComponentModel.ISupportInitialize)(_dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private async void Form_PODetails_Load(object? sender, EventArgs e)
        {
            if (_visualService == null)
            {
                Service_ErrorHandler.ShowError("Visual Database Service not available.");
                this.Close();
                return;
            }

            try
            {
                var result = await _visualService.GetPODetailsAsync(_poNumber);

                if (result.IsSuccess && result.Data != null && _dataGridView != null && _lblLoading != null)
                {
                    _dataGridView.DataSource = result.Data;
                    
                    // Format columns
                    if (_dataGridView.Columns["Desired Date"] != null)
                        _dataGridView.Columns["Desired Date"].DefaultCellStyle.Format = "d";
                    if (_dataGridView.Columns["Promise Date"] != null)
                        _dataGridView.Columns["Promise Date"].DefaultCellStyle.Format = "d";
                    if (_dataGridView.Columns["Unit Price"] != null)
                        _dataGridView.Columns["Unit Price"].DefaultCellStyle.Format = "C2";
                    if (_dataGridView.Columns["Total Amount"] != null)
                        _dataGridView.Columns["Total Amount"].DefaultCellStyle.Format = "C2";

                    _dataGridView.Visible = true;
                    _lblLoading.Visible = false;
                    
                    // Apply theme
                    Core.Core_Themes.ApplyThemeToDataGridView(_dataGridView);
                }
                else
                {
                    Service_ErrorHandler.ShowError($"Failed to load PO details: {result.ErrorMessage}");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, controlName: this.Name);
                this.Close();
            }
        }
    }
}
