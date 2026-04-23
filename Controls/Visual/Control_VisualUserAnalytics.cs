using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;

namespace MTM_WIP_Application_Winforms.Controls.Visual
{
    /// <summary>
    /// Launch surface for the role-aware analytics workspace.
    /// </summary>
    public partial class Control_VisualUserAnalytics : ThemedUserControl
    {
        #region Fields
        // Intentionally left blank.
        #endregion

        #region Properties
        private bool CanViewTeam => Model_Application_Variables.UserTypeAdmin || Model_Application_Variables.UserTypeDeveloper;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Control_VisualUserAnalytics"/> class.
        /// </summary>
        public Control_VisualUserAnalytics()
        {
            InitializeComponent();
            MinimumSize = new Size(840, 520);
            UpdateCopy();
        }
        #endregion

        #region Methods
        private void OpenAnalyticsWorkspace()
        {
            try
            {
                using var analyticsViewer = new Forms.Visual.Form_AnalyticsViewer();
                analyticsViewer.ShowDialog(FindForm());
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(
                    ex,
                    Enum_ErrorSeverity.Medium,
                    callerName: nameof(OpenAnalyticsWorkspace),
                    controlName: Name
                );
            }
        }

        private void UpdateCopy()
        {
            Control_VisualUserAnalytics_Label_Title.Text = CanViewTeam
                ? "User & Team Analytics"
                : "My Analytics";

            Control_VisualUserAnalytics_Label_Subtitle.Text = CanViewTeam
                ? "Open the unified analytics workspace to review team activity, drill into individuals, and print or export summary views."
                : "Open the unified analytics workspace to review your activity history, trends, and print-friendly summary.";

            Control_VisualUserAnalytics_Label_Details.Text = CanViewTeam
                ? "The new analytics page loads a single transaction feed, treats advanced inventory and advanced remove the same as single actions, and lets you switch between team context and individual detail without a separate score model."
                : "The new analytics page focuses on clear counts, quantity moved, unique parts, active days, and recent activity. Advanced inventory and advanced remove are labeled for clarity and counted the same as single actions.";

            Control_VisualUserAnalytics_Label_Access.Text = CanViewTeam
                ? "Access: Team summary, user drill-down, print, and export are enabled for admin and developer roles."
                : "Access: Your role is limited to personal analytics only.";

            Control_VisualUserAnalytics_Button_Open.Text = CanViewTeam
                ? "Open Analytics Workspace"
                : "Open My Analytics";
        }
        #endregion

        #region Events
        private void Control_VisualUserAnalytics_Button_Open_Click(object? sender, EventArgs e)
        {
            OpenAnalyticsWorkspace();
        }

        /// <inheritdoc />
        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            UpdateCopy();
        }
        #endregion

        #region Helpers
        // Intentionally left blank.
        #endregion

        #region Cleanup / Dispose
        // Intentionally left blank.
        #endregion
    }
}
