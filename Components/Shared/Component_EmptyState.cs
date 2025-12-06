using MTM_WIP_Application_Winforms.Forms.Shared;

namespace MTM_WIP_Application_Winforms.Components.Shared
{
    public partial class Control_EmptyState : ThemedUserControl
    {
        public Action? Action { get; set; }

        public string Message
        {
            get => labelMessage.Text;
            set => labelMessage.Text = value;
        }

        public Image? Image
        {
            get => pictureBox1.Image;
            set => pictureBox1.Image = value;
        }

        public string? ActionText
        {
            get => buttonAction.Text;
            set
            {
                buttonAction.Text = value;
                buttonAction.Visible = !string.IsNullOrEmpty(value);
            }
        }

        public Control_EmptyState()
        {
            InitializeComponent();
            // Try to load default image if available
            try 
            {
                // Assuming Resources is in Properties namespace
                // this.Image = Properties.Resources.NothingFound;
            }
            catch { }
        }

        private void buttonAction_Click(object sender, EventArgs e)
        {
            Action?.Invoke();
        }
    }
}
