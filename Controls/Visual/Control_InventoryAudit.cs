using System;
using System.Windows.Forms;
using MTM_WIP_Application_Winforms.Forms.Shared;

namespace MTM_WIP_Application_Winforms.Controls.Visual
{
    /// <summary>
    /// Main container control for Inventory Auditing features.
    /// Hosts the Part Lifecycle Tracker and potentially other audit tools.
    /// </summary>
    public partial class Control_InventoryAudit : ThemedUserControl
    {
        private Control_VisualPartLifecycle _lifecycleControl = null!;

        public Control_InventoryAudit()
        {
            // InitializeComponent(); // Not using designer
            InitializeContent();
        }

        private void InitializeContent()
        {
            // For now, we just host the Lifecycle control filling the entire space.
            // In the future, this could have a dashboard-like layout or navigation.
            _lifecycleControl = new Control_VisualPartLifecycle
            {
                Dock = DockStyle.Fill
            };
            this.Controls.Add(_lifecycleControl);
        }
    }
}
