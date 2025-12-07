using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;
using MTM_WIP_Application_Winforms.Services.Logging;
using MTM_WIP_Application_Winforms.Services.Visual;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using MTM_WIP_Application_Winforms.Forms.Help;

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
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Form_PODetails"/> class.
        /// </summary>
        /// <param name="poNumber">The purchase order number to display details for.</param>
        public Form_PODetails(string poNumber)
        {
            LoggingUtility.Log($"[Form_PODetails] Constructor called for PO: {poNumber}");
            _poNumber = poNumber;
            _visualService = Program.ServiceProvider?.GetService<IService_VisualDatabase>();

            InitializeComponent();
            InitializeHelpButton();
            Text = $"PO Details: {_poNumber}";
            
            Load += Form_PODetails_Load;
        }
        #endregion

        #region Methods

        private async Task LoadPurchaseOrderDetailsAsync()
        {
            LoggingUtility.Log($"[Form_PODetails] Loading details for PO: {_poNumber}");
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
                    LoggingUtility.Log($"[Form_PODetails] Successfully loaded {_poData.Rows.Count} lines for PO: {_poNumber}");
                    return;
                }

                var errorMessage = string.IsNullOrWhiteSpace(result.ErrorMessage)
                    ? "An unknown error occurred."
                    : result.ErrorMessage;

                LoggingUtility.Log($"[Form_PODetails] Failed to load details: {errorMessage}");
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
                Form_PODetails_Label_Counter.Text = "No lines found";
                Form_PODetails_Button_Prev.Enabled = false;
                Form_PODetails_Button_Next.Enabled = false;
                return;
            }

            if (_currentIndex < 0) _currentIndex = 0;
            if (_currentIndex >= _poData.Rows.Count) _currentIndex = _poData.Rows.Count - 1;

            var row = _poData.Rows[_currentIndex];

            Form_PODetails_TextBox_LineNo.Text = row["Line #"]?.ToString();
            Form_PODetails_TextBox_PartNumber.Text = row["Part Number"]?.ToString();
            Form_PODetails_TextBox_Status.Text = row["Status"]?.ToString();
            Form_PODetails_TextBox_Ordered.Text = FormatNumber(row["Ordered"]);
            Form_PODetails_TextBox_Received.Text = FormatNumber(row["Received"]);
            Form_PODetails_TextBox_Remaining.Text = FormatNumber(row["Remaining"]);
            Form_PODetails_TextBox_DesiredDate.Text = FormatDate(row["Desired Date"]);
            Form_PODetails_TextBox_PromiseDate.Text = FormatDate(row["Promise Date"]);
            Form_PODetails_TextBox_UnitPrice.Text = FormatCurrency(row["Unit Price"]);
            Form_PODetails_TextBox_TotalAmount.Text = FormatCurrency(row["Total Amount"]);
            Form_PODetails_RichTextBox_Specs.Text = row["Specs"]?.ToString();

            Form_PODetails_Label_Counter.Text = $"Line {_currentIndex + 1} of {_poData.Rows.Count}";
            Form_PODetails_Button_Prev.Enabled = _currentIndex > 0;
            Form_PODetails_Button_Next.Enabled = _currentIndex < _poData.Rows.Count - 1;
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
            Form_PODetails_Label_Loading.Visible = isLoading;
            Form_PODetails_Panel_Details.Visible = !isLoading;
        }
        #endregion

        #region Events
        private async void Form_PODetails_Load(object? sender, EventArgs e)
        {
            await LoadPurchaseOrderDetailsAsync();
        }

        private void Form_PODetails_Button_Prev_Click(object sender, EventArgs e)
        {
            Navigate(-1);
        }

        private void Form_PODetails_Button_Next_Click(object sender, EventArgs e)
        {
            Navigate(1);
        }
        #endregion

        #region Cleanup / Dispose
        // Dispose is now handled in Designer file for components.
        #endregion
    #region Helpers

    private Button? Form_PODetails_Button_Help;

    private void InitializeHelpButton()
    {
        Form_PODetails_Button_Help = new Button();
        Form_PODetails_Button_Help.Name = "Form_PODetails_Button_Help";
        Form_PODetails_Button_Help.Text = "?";
        Form_PODetails_Button_Help.Size = new Size(24, 24);
        Form_PODetails_Button_Help.Location = new Point(this.Width - 40, 5); 
        Form_PODetails_Button_Help.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        Form_PODetails_Button_Help.Click += (s, e) => 
        {
            var helpForm = new HelpViewerForm();
            helpForm.Show();
            helpForm.ShowHelp("infor-visual-integration", "po-details");
        };
        
        this.Controls.Add(Form_PODetails_Button_Help);
        Form_PODetails_Button_Help.BringToFront();
    }

    #endregion

    }
}
