using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Models.Analytics;
using MTM_WIP_Application_Winforms.Services.Analytics;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;
using ClosedXML.Excel;
using MTM_WIP_Application_Winforms.Services;
using MTM_WIP_Application_Winforms.Services.Logging;

namespace MTM_WIP_Application_Winforms.Controls.Visual
{
    public partial class Control_MaterialHandlerAnalytics : ThemedUserControl
    {
        private Microsoft.Web.WebView2.WinForms.WebView2 webView = null!;
        private DateTimePicker dtpStart = null!;
        private DateTimePicker dtpEnd = null!;
        private Button btnRefresh = null!;
        private Button btnExport = null!;
        private Label lblStatus = null!;
        private List<Model_Visual_MaterialHandlerScore> _currentData = new();

        public Control_MaterialHandlerAnalytics()
        {
            InitializeComponent();
            InitializeWebView();
            
            // Default to last 7 days
            dtpStart.Value = DateTime.Today.AddDays(-7);
            dtpEnd.Value = DateTime.Today;
        }

        private void InitializeComponent()
        {
            this.webView = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.dtpStart = new DateTimePicker();
            this.dtpEnd = new DateTimePicker();
            this.btnRefresh = new Button();
            this.btnExport = new Button();
            this.lblStatus = new Label();
            ((System.ComponentModel.ISupportInitialize)(this.webView)).BeginInit();
            this.SuspendLayout();
            // 
            // webView
            // 
            this.webView.Anchor = (((AnchorStyles.Top | AnchorStyles.Bottom)
            | AnchorStyles.Left)
            | AnchorStyles.Right);
            this.webView.CreationProperties = null;
            this.webView.DefaultBackgroundColor = Color.FromArgb(30, 30, 30);
            this.webView.Location = new Point(0, 50);
            this.webView.Name = "webView";
            this.webView.Size = new Size(800, 550);
            this.webView.TabIndex = 0;
            this.webView.ZoomFactor = 1D;
            // 
            // dtpStart
            // 
            this.dtpStart.Format = DateTimePickerFormat.Short;
            this.dtpStart.Location = new Point(13, 13);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new Size(100, 23);
            this.dtpStart.TabIndex = 1;
            // 
            // dtpEnd
            // 
            this.dtpEnd.Format = DateTimePickerFormat.Short;
            this.dtpEnd.Location = new Point(119, 13);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new Size(100, 23);
            this.dtpEnd.TabIndex = 2;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new Point(225, 12);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new Size(75, 25);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click!);
            // 
            // btnExport
            // 
            this.btnExport.Location = new Point(306, 12);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new Size(75, 25);
            this.btnExport.TabIndex = 5;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click!);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new Point(400, 17);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new Size(39, 15);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Text = "Ready";
            // 
            // Control_MaterialHandlerAnalytics
            // 
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.dtpEnd);
            this.Controls.Add(this.dtpStart);
            this.Controls.Add(this.webView);
            this.Name = "Control_MaterialHandlerAnalytics";
            this.Size = new Size(800, 600);
            ((System.ComponentModel.ISupportInitialize)(this.webView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private async void InitializeWebView()
        {
            try
            {
                await webView.EnsureCoreWebView2Async();
                string htmlPath = Path.Combine(Application.StartupPath, "Resources", "Html", "WIPUserAnalytics_Enhanced.html");
                if (File.Exists(htmlPath))
                {
                    string html = await File.ReadAllTextAsync(htmlPath);
                    webView.NavigateToString(html);
                }
                else
                {
                    lblStatus.Text = "Error: HTML template not found.";
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "WebView Error: " + ex.Message;
            }
        }

        private async void btnRefresh_Click(object? sender, EventArgs e)
        {
            try
            {
                LoggingUtility.Log("[Control_MaterialHandlerAnalytics] Refresh clicked");
                lblStatus.Text = "Loading...";
                btnRefresh.Enabled = false;
                btnExport.Enabled = false;

                if (Program.ServiceProvider == null)
                {
                    lblStatus.Text = "Error: Service Provider not initialized.";
                    return;
                }

                var service = Program.ServiceProvider.GetRequiredService<IService_UserShiftLogic>();
                var result = await service.CalculateMaterialHandlerScoresAsync(dtpStart.Value, dtpEnd.Value);

                if (result.IsSuccess)
                {
                    _currentData = result.Data ?? new List<Model_Visual_MaterialHandlerScore>();
                    string jsonData = JsonConvert.SerializeObject(_currentData);
                    await webView.ExecuteScriptAsync($"renderData({jsonData});");
                    lblStatus.Text = $"Loaded {_currentData.Count} records.";
                }
                else
                {
                    lblStatus.Text = "Error loading data.";
                    Service_ErrorHandler.ShowError(result.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Error.";
                Service_ErrorHandler.HandleException(ex, Models.Enum_ErrorSeverity.Medium, controlName: this.Name);
            }
            finally
            {
                btnRefresh.Enabled = true;
                btnExport.Enabled = true;
            }
        }

        private async void btnExport_Click(object? sender, EventArgs e)
        {
            if (_currentData == null || _currentData.Count == 0)
            {
                Service_ErrorHandler.ShowInformation("No data to export.", "Export");
                return;
            }

            LoggingUtility.Log("[Control_MaterialHandlerAnalytics] Export clicked");

            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel Workbook|*.xlsx";
                sfd.FileName = $"MaterialHandlerAnalytics_{DateTime.Now:yyyyMMdd}.xlsx";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        lblStatus.Text = "Exporting...";
                        btnExport.Enabled = false;

                        await Task.Run(() =>
                        {
                            using (var workbook = new XLWorkbook())
                            {
                                var worksheet = workbook.Worksheets.Add("Analytics");
                                
                                // Headers
                                worksheet.Cell(1, 1).Value = "User";
                                worksheet.Cell(1, 2).Value = "Shift";
                                worksheet.Cell(1, 3).Value = "Total Score";
                                worksheet.Cell(1, 4).Value = "1pt Moves";
                                worksheet.Cell(1, 5).Value = "2pt Moves";
                                worksheet.Cell(1, 6).Value = "Shift Factor";
                                worksheet.Cell(1, 7).Value = "Adjusted Score";
                                worksheet.Cell(1, 8).Value = "Avg Time (min)";

                                var header = worksheet.Range(1, 1, 1, 8);
                                header.Style.Font.Bold = true;
                                header.Style.Fill.BackgroundColor = XLColor.LightGray;

                                // Data
                                int row = 2;
                                foreach (var item in _currentData)
                                {
                                    worksheet.Cell(row, 1).Value = item.UserName;
                                    worksheet.Cell(row, 2).Value = item.Shift;
                                    worksheet.Cell(row, 3).Value = item.TotalScore;
                                    worksheet.Cell(row, 4).Value = item.OnePointMoves;
                                    worksheet.Cell(row, 5).Value = item.TwoPointMoves;
                                    worksheet.Cell(row, 6).Value = item.ShiftVolumeFactor;
                                    worksheet.Cell(row, 7).Value = item.AdjustedScore;
                                    worksheet.Cell(row, 8).Value = item.AverageTimeMinutes;
                                    row++;
                                }

                                worksheet.Columns().AdjustToContents();
                                workbook.SaveAs(sfd.FileName);
                            }
                        });

                        lblStatus.Text = "Export Complete.";
                        Service_ErrorHandler.ShowInformation("Export completed successfully.", "Export");
                    }
                    catch (Exception ex)
                    {
                        lblStatus.Text = "Export Error.";
                        Service_ErrorHandler.HandleException(ex, Models.Enum_ErrorSeverity.Medium, controlName: this.Name);
                    }
                    finally
                    {
                        btnExport.Enabled = true;
                    }
                }
            }
        }
    }
}
