using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;
using MTM_WIP_Application_Winforms.Services.Visual;

namespace MTM_WIP_Application_Winforms.Forms.Visual
{
    public partial class Form_PODetails : ThemedForm
    {
        private readonly string _poNumber;
        private readonly IService_VisualDatabase? _visualService;
        private FlowLayoutPanel? _linesPanel;
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
            this.Resize += (_, _) => ResizeDetailPanels();
        }

        private void InitializeComponent()
        {
            _linesPanel = new FlowLayoutPanel();
            _lblLoading = new Label();
            this.SuspendLayout();

            // 
            // _linesPanel
            // 
            if (_linesPanel != null)
            {
                _linesPanel.AutoScroll = true;
                _linesPanel.Dock = DockStyle.Fill;
                _linesPanel.FlowDirection = FlowDirection.TopDown;
                _linesPanel.WrapContents = false;
                _linesPanel.Padding = new Padding(10);
                _linesPanel.Visible = false;
                _linesPanel.Margin = new Padding(0);
                _linesPanel.SizeChanged += (_, _) => ResizeDetailPanels();
                this.Controls.Add(_linesPanel);
            }

            // 
            // _lblLoading
            // 
            _lblLoading.AutoSize = true;
            _lblLoading.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            _lblLoading.Location = new Point((this.Width - _lblLoading.Width) / 2, (this.Height - _lblLoading.Height) / 2);
            _lblLoading.Name = "_lblLoading";
            _lblLoading.Size = new Size(78, 21);
            _lblLoading.TabIndex = 1;
            _lblLoading.Text = "Loading...";

            this.Controls.Add(_lblLoading);
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

                if (result.IsSuccess && result.Data != null && _linesPanel != null && _lblLoading != null)
                {
                    RenderLineItems(result.Data);
                    _linesPanel.Visible = true;
                    _lblLoading.Visible = false;
                    ResizeDetailPanels();
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

        private void RenderLineItems(DataTable data)
        {
            if (_linesPanel == null)
            {
                return;
            }

            _linesPanel.SuspendLayout();
            _linesPanel.Controls.Clear();

            if (data.Rows.Count == 0)
            {
                _linesPanel.Controls.Add(new Label
                {
                    Text = "No line items were returned for this PO.",
                    AutoSize = true,
                    Font = new Font(this.Font, FontStyle.Italic),
                    Margin = new Padding(0, 0, 0, 10)
                });
            }

            foreach (DataRow row in data.Rows)
            {
                _linesPanel.Controls.Add(CreateLinePanel(row));
            }

            _linesPanel.ResumeLayout();
        }

        private Control CreateLinePanel(DataRow row)
        {
            var container = new Panel
            {
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(12),
                Margin = new Padding(0, 0, 0, 10),
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };

            var header = new Label
            {
                Text = $"Line {row["Line #"]} - {row["Part Number"]}",
                AutoSize = true,
                Font = new Font(this.Font, FontStyle.Bold)
            };

            var layout = new TableLayoutPanel
            {
                ColumnCount = 4,
                AutoSize = true,
                Dock = DockStyle.Top,
                Margin = new Padding(0, 8, 0, 0)
            };

            layout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

            AddField(layout, "Description", row["Description"]?.ToString());
            AddField(layout, "Ordered", FormatDecimal(row["Ordered"]));
            AddField(layout, "Received", FormatDecimal(row["Received"]));
            AddField(layout, "Remaining", FormatDecimal(row["Remaining"]));
            AddField(layout, "Desired Date", FormatDate(row["Desired Date"]));
            AddField(layout, "Promise Date", FormatDate(row["Promise Date"]));
            AddField(layout, "Status", row["Status"]?.ToString());
            AddField(layout, "Unit Price", FormatCurrency(row["Unit Price"]));
            AddField(layout, "Total Amount", FormatCurrency(row["Total Amount"]));

            container.Controls.Add(header);
            container.Controls.Add(layout);

            return container;
        }

        private void AddField(TableLayoutPanel layout, string labelText, string? value)
        {
            var label = new Label
            {
                Text = labelText + ":",
                AutoSize = true,
                Margin = new Padding(0, 6, 6, 0)
            };

            var textBox = new TextBox
            {
                ReadOnly = true,
                BorderStyle = BorderStyle.FixedSingle,
                Dock = DockStyle.Top,
                Text = value ?? string.Empty,
                Margin = new Padding(0, 3, 12, 3)
            };

            layout.Controls.Add(label);
            layout.Controls.Add(textBox);
        }

        private static string FormatDecimal(object? value)
        {
            return decimal.TryParse(Convert.ToString(value, CultureInfo.CurrentCulture), out var number)
                ? number.ToString("N2", CultureInfo.CurrentCulture)
                : string.Empty;
        }

        private static string FormatCurrency(object? value)
        {
            return decimal.TryParse(Convert.ToString(value, CultureInfo.CurrentCulture), out var number)
                ? number.ToString("C2", CultureInfo.CurrentCulture)
                : string.Empty;
        }

        private static string FormatDate(object? value)
        {
            return DateTime.TryParse(Convert.ToString(value, CultureInfo.CurrentCulture), out var date)
                ? date.ToShortDateString()
                : string.Empty;
        }

        private void ResizeDetailPanels()
        {
            if (_linesPanel == null || _linesPanel.Controls.Count == 0)
            {
                return;
            }

            int availableWidth = _linesPanel.ClientSize.Width - _linesPanel.Padding.Horizontal - 20;

            foreach (Control control in _linesPanel.Controls)
            {
                if (control is Panel panel)
                {
                    panel.Width = availableWidth > 0 ? availableWidth : panel.Width;
                }
            }
        }
    }
}
