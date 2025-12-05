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
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.webView)).BeginInit();
            this.SuspendLayout();
            // 
            // webView
            // 
            this.webView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webView.CreationProperties = null;
            this.webView.DefaultBackgroundColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.webView.Location = new System.Drawing.Point(0, 50);
            this.webView.Name = "webView";
            this.webView.Size = new System.Drawing.Size(800, 550);
            this.webView.TabIndex = 0;
            this.webView.ZoomFactor = 1D;
            // 
            // dtpStart
            // 
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStart.Location = new System.Drawing.Point(13, 13);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(100, 23);
            this.dtpStart.TabIndex = 1;
            // 
            // dtpEnd
            // 
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEnd.Location = new System.Drawing.Point(119, 13);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(100, 23);
            this.dtpEnd.TabIndex = 2;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(225, 12);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 25);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click!);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(306, 12);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 25);
            this.btnExport.TabIndex = 5;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click!);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(400, 17);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(39, 15);
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
            this.Size = new System.Drawing.Size(800, 600);
            ((System.ComponentModel.ISupportInitialize)(this.webView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private async void InitializeWebView()
        {
            try
            {
                await webView.EnsureCoreWebView2Async();
                string htmlPath = Path.Combine(Application.StartupPath, "Resources", "Html", "MaterialHandlerAnalytics_Enhanced.html");
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
                    MessageBox.Show(result.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Error.";
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("No data to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

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
                        MessageBox.Show("Export completed successfully.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        lblStatus.Text = "Export Error.";
                        MessageBox.Show("Export failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
