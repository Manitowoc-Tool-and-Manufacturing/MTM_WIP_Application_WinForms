using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Services;
using MTM_WIP_Application_Winforms.Services.Visual;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Controls.Visual
{
    public partial class Control_DieToolDiscovery : ThemedUserControl
    {
        private readonly IService_VisualDatabase? _visualService;

        public Control_DieToolDiscovery()
        {
            InitializeComponent();
            _visualService = Program.ServiceProvider?.GetService<IService_VisualDatabase>();
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            await PerformSearch();
        }

        private async void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                await PerformSearch();
            }
        }

        private async Task PerformSearch()
        {
            if (_visualService == null)
            {
                Service_ErrorHandler.ShowError("Visual Database Service not available.");
                return;
            }

            string searchTerm = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(searchTerm))
            {
                Service_ErrorHandler.ShowError("Please enter a search term.");
                return;
            }

            try
            {
                bool searchByPart = radioSearchByPart.Checked;
                
                var result = await _visualService.SearchDiesAsync(searchTerm, searchByPart);

                if (result.IsSuccess)
                {
                    gridResults.DataSource = result.Data;
                    if (result.Data != null && result.Data.Rows.Count > 0)
                    {
                        gridResults.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
                    }
                    else
                    {
                        Service_ErrorHandler.ShowInformation("No results found.");
                    }
                }
                else
                {
                    Service_ErrorHandler.ShowError($"Search failed: {result.ErrorMessage}");
                }
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, controlName: this.Name);
            }
        }
    }
}
