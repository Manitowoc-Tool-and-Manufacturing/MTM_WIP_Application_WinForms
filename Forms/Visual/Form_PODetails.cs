using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;
using MTM_WIP_Application_Winforms.Services.Visual;
using Microsoft.Extensions.DependencyInjection;

namespace MTM_WIP_Application_Winforms.Forms.Visual
{
    /// <summary>
    /// Displays Visual system purchase-order line details for a specific PO number.
    /// </summary>
    public partial class Form_PODetails : ThemedForm
    {
        #region Fields
        private readonly string _poNumber;
        private readonly IService_VisualDatabase? _visualService;
        private DataGridView? _dataGridView;
        private Label? _lblLoading;
        #endregion

        #region Properties
        // Intentionally left blank.
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
            Size = new Size(800, 500);
            StartPosition = FormStartPosition.CenterParent;

            Load += Form_PODetails_Load;
        }
        #endregion

        #region Methods
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_PODetails));
            _dataGridView = new DataGridView();
            _lblLoading = new Label();
            ((System.ComponentModel.ISupportInitialize)_dataGridView).BeginInit();
            SuspendLayout();
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
            _dataGridView.Size = new Size(284, 261);
            _dataGridView.TabIndex = 0;
            _dataGridView.Visible = false;
            // 
            // _lblLoading
            // 
            _lblLoading.AutoSize = true;
            _lblLoading.Font = new Font("Segoe UI Emoji", 12F);
            _lblLoading.Location = new Point(350, 220);
            _lblLoading.Name = "_lblLoading";
            _lblLoading.Size = new Size(75, 21);
            _lblLoading.TabIndex = 1;
            _lblLoading.Text = "Loading...";
            // 
            // Form_PODetails
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(284, 261);
            Controls.Add(_dataGridView);
            Controls.Add(_lblLoading);
            Name = "Form_PODetails";
            ((System.ComponentModel.ISupportInitialize)_dataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private async Task LoadPurchaseOrderDetailsAsync()
        {
            if (_visualService == null)
            {
                Service_ErrorHandler.ShowUserError("Visual Database Service is not available.");
                Close();
                return;
            }

            if (_dataGridView == null || _lblLoading == null)
            {
                return;
            }

            try
            {
                ToggleLoadingState(true);

                var result = await _visualService.GetPODetailsAsync(_poNumber);

                if (result.IsSuccess && result.Data != null)
                {
                    _dataGridView.DataSource = result.Data;
                    ApplyColumnFormatting(_dataGridView);
                    Service_DataGridView.ApplySmartNumericFormatting(_dataGridView);
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
                Service_ErrorHandler.HandleException(
                    ex,
                    Enum_ErrorSeverity.Medium,
                    contextData: new Dictionary<string, object>
                    {
                        ["PONumber"] = _poNumber
                    },
                    callerName: nameof(LoadPurchaseOrderDetailsAsync),
                    controlName: Name);

                Close();
            }
        }
        #endregion

        #region Events
        private async void Form_PODetails_Load(object? sender, EventArgs e)
        {
            await LoadPurchaseOrderDetailsAsync();
        }
        #endregion

        #region Helpers
        private static void ApplyColumnFormatting(DataGridView grid)
        {
            if (grid.Columns["Desired Date"] != null)
            {
                grid.Columns["Desired Date"].DefaultCellStyle.Format = "d";
            }

            if (grid.Columns["Promise Date"] != null)
            {
                grid.Columns["Promise Date"].DefaultCellStyle.Format = "d";
            }

            if (grid.Columns["Unit Price"] != null)
            {
                grid.Columns["Unit Price"].DefaultCellStyle.Format = "C2";
            }

            if (grid.Columns["Total Amount"] != null)
            {
                grid.Columns["Total Amount"].DefaultCellStyle.Format = "C2";
            }
        }

        private void ToggleLoadingState(bool isLoading)
        {
            if (_lblLoading == null || _dataGridView == null)
            {
                return;
            }

            _lblLoading.Visible = isLoading;
            if (isLoading)
            {
                _lblLoading.BringToFront();
            }

            _dataGridView.Visible = !isLoading;
        }
        #endregion

        #region Cleanup / Dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Load -= Form_PODetails_Load;
                _dataGridView?.Dispose();
                _lblLoading?.Dispose();
            }

            base.Dispose(disposing);
        }
        #endregion
    }
}
