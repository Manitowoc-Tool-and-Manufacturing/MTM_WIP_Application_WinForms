using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;
using MTM_WIP_Application_Winforms.Services.Visual;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace MTM_WIP_Application_Winforms.Forms.Visual
{
    /// <summary>
    /// Displays Visual system purchase-order line details for a specific PO number.
    /// Supports navigation between lines.
    /// </summary>
    public partial class Form_PODetails : ThemedForm
    {
        #region Fields
        private readonly string _poNumber;
        private readonly IService_VisualDatabase? _visualService;
        private DataTable? _poData;
        private int _currentIndex = 0;

        // Controls
        private Label? _lblLoading;
        private Panel? _pnlDetails;
        private Label? _lblCounter;
        private Button? _btnPrev;
        private Button? _btnNext;
        
        private TextBox? _txtLineNo;
        private TextBox? _txtPartNumber;
        private TextBox? _txtOrdered;
        private TextBox? _txtReceived;
        private TextBox? _txtRemaining;
        private TextBox? _txtDesiredDate;
        private TextBox? _txtPromiseDate;
        private TextBox? _txtStatus;
        private TextBox? _txtUnitPrice;
        private TextBox? _txtTotalAmount;
        private RichTextBox? _rtbSpecs;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Form_PODetails"/> class.
        /// </summary>
        /// <param name="poNumber">The purchase order number to display details for.</param>
        public Form_PODetails(string poNumber)
        {
            _poNumber = poNumber;
            _visualService = Program.ServiceProvider?.GetService<IService_VisualDatabase>();

            InitializeComponent();
            Text = $"PO Details: {_poNumber}";
            Size = new Size(600, 550);
            StartPosition = FormStartPosition.CenterParent;

            Load += Form_PODetails_Load;
        }
        #endregion

        #region Methods
        private void InitializeComponent()
        {
            _lblLoading = new Label();
            _pnlDetails = new Panel();
            
            // Navigation
            _btnPrev = new Button { Text = "< Previous", Size = new Size(100, 30), Location = new Point(20, 460) };
            _btnNext = new Button { Text = "Next >", Size = new Size(100, 30), Location = new Point(460, 460) };
            _lblCounter = new Label { Text = "Line 0 of 0", AutoSize = false, TextAlign = ContentAlignment.MiddleCenter, Size = new Size(200, 30), Location = new Point(190, 465) };

            // Fields
            var lblLine = CreateLabel("Line #:", 20, 20);
            _txtLineNo = CreateTextBox(120, 20);

            var lblPart = CreateLabel("Part Number:", 20, 60);
            _txtPartNumber = CreateTextBox(120, 60, 200);

            var lblStatus = CreateLabel("Status:", 340, 60);
            _txtStatus = CreateTextBox(400, 60, 160);

            var lblOrdered = CreateLabel("Ordered:", 20, 100);
            _txtOrdered = CreateTextBox(120, 100);

            var lblReceived = CreateLabel("Received:", 20, 140);
            _txtReceived = CreateTextBox(120, 140);

            var lblRemaining = CreateLabel("Remaining:", 20, 180);
            _txtRemaining = CreateTextBox(120, 180);

            var lblDesired = CreateLabel("Desired Date:", 300, 100);
            _txtDesiredDate = CreateTextBox(400, 100, 160);

            var lblPromise = CreateLabel("Promise Date:", 300, 140);
            _txtPromiseDate = CreateTextBox(400, 140, 160);

            var lblPrice = CreateLabel("Unit Price:", 300, 180);
            _txtUnitPrice = CreateTextBox(400, 180, 160);

            var lblTotal = CreateLabel("Total Amount:", 300, 220);
            _txtTotalAmount = CreateTextBox(400, 220, 160);

            var lblSpecs = CreateLabel("Description / Specs:", 20, 260);
            _rtbSpecs = new RichTextBox { Location = new Point(20, 290), Size = new Size(540, 150), ReadOnly = true, BorderStyle = BorderStyle.FixedSingle };

            // Add to Panel
            _pnlDetails.Controls.AddRange(new Control[] {
                lblLine, _txtLineNo,
                lblPart, _txtPartNumber,
                lblStatus, _txtStatus,
                lblOrdered, _txtOrdered,
                lblReceived, _txtReceived,
                lblRemaining, _txtRemaining,
                lblDesired, _txtDesiredDate,
                lblPromise, _txtPromiseDate,
                lblPrice, _txtUnitPrice,
                lblTotal, _txtTotalAmount,
                lblSpecs, _rtbSpecs,
                _btnPrev, _btnNext, _lblCounter
            });
            _pnlDetails.Dock = DockStyle.Fill;
            _pnlDetails.Visible = false;

            // Loading Label
            _lblLoading.AutoSize = true;
            _lblLoading.Font = new Font("Segoe UI", 12F);
            _lblLoading.Location = new Point(250, 220);
            _lblLoading.Text = "Loading...";

            // Form Controls
            Controls.Add(_pnlDetails);
            Controls.Add(_lblLoading);

            // Events
            _btnPrev.Click += (s, e) => Navigate(-1);
            _btnNext.Click += (s, e) => Navigate(1);
        }

        private Label CreateLabel(string text, int x, int y)
        {
            return new Label { Text = text, Location = new Point(x, y), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
        }

        private TextBox CreateTextBox(int x, int y, int width = 150)
        {
            return new TextBox { Location = new Point(x, y - 3), Size = new Size(width, 23), ReadOnly = true, BackColor = Color.WhiteSmoke };
        }

        private async Task LoadPurchaseOrderDetailsAsync()
        {
            if (_visualService == null)
            {
                Service_ErrorHandler.ShowUserError("Visual Database Service is not available.");
                Close();
                return;
            }

            try
            {
                ToggleLoadingState(true);

                var result = await _visualService.GetPODetailsAsync(_poNumber);

                if (result.IsSuccess && result.Data != null)
                {
                    _poData = result.Data;
                    _currentIndex = 0;
                    DisplayCurrentLine();
                    ToggleLoadingState(false);
                    return;
                }

                var errorMessage = string.IsNullOrWhiteSpace(result.ErrorMessage)
                    ? "An unknown error occurred."
                    : result.ErrorMessage;

                Service_ErrorHandler.ShowUserError($"Failed to load PO details: {errorMessage}");
                Close();
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, callerName: nameof(LoadPurchaseOrderDetailsAsync), controlName: Name);
                Close();
            }
        }

        private void DisplayCurrentLine()
        {
            if (_poData == null || _poData.Rows.Count == 0)
            {
                _lblCounter!.Text = "No lines found";
                _btnPrev!.Enabled = false;
                _btnNext!.Enabled = false;
                return;
            }

            if (_currentIndex < 0) _currentIndex = 0;
            if (_currentIndex >= _poData.Rows.Count) _currentIndex = _poData.Rows.Count - 1;

            var row = _poData.Rows[_currentIndex];

            _txtLineNo!.Text = row["Line #"]?.ToString();
            _txtPartNumber!.Text = row["Part Number"]?.ToString();
            _txtStatus!.Text = row["Status"]?.ToString();
            _txtOrdered!.Text = FormatNumber(row["Ordered"]);
            _txtReceived!.Text = FormatNumber(row["Received"]);
            _txtRemaining!.Text = FormatNumber(row["Remaining"]);
            _txtDesiredDate!.Text = FormatDate(row["Desired Date"]);
            _txtPromiseDate!.Text = FormatDate(row["Promise Date"]);
            _txtUnitPrice!.Text = FormatCurrency(row["Unit Price"]);
            _txtTotalAmount!.Text = FormatCurrency(row["Total Amount"]);
            _rtbSpecs!.Text = row["Specs"]?.ToString();

            _lblCounter!.Text = $"Line {_currentIndex + 1} of {_poData.Rows.Count}";
            _btnPrev!.Enabled = _currentIndex > 0;
            _btnNext!.Enabled = _currentIndex < _poData.Rows.Count - 1;
        }

        private void Navigate(int direction)
        {
            _currentIndex += direction;
            DisplayCurrentLine();
        }

        private string FormatNumber(object? value) => value is decimal d ? d.ToString("G29") : value?.ToString() ?? "";
        private string FormatDate(object? value) => value is DateTime d ? d.ToShortDateString() : "";
        private string FormatCurrency(object? value) => value is decimal d ? d.ToString("C2") : "";

        private void ToggleLoadingState(bool isLoading)
        {
            if (_lblLoading == null || _pnlDetails == null) return;
            _lblLoading.Visible = isLoading;
            _pnlDetails.Visible = !isLoading;
        }
        #endregion

        #region Events
        private async void Form_PODetails_Load(object? sender, EventArgs e)
        {
            await LoadPurchaseOrderDetailsAsync();
        }
        #endregion

        #region Cleanup / Dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Load -= Form_PODetails_Load;
                _btnPrev?.Dispose();
                _btnNext?.Dispose();
                // Other controls disposed by container
            }
            base.Dispose(disposing);
        }
        #endregion
    }
}
