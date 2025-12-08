namespace MTM_WIP_Application_Winforms.Forms.Shared
{
    public static class Form_InputBox
    {
        public static string Show(string prompt, string title, string defaultValue = "")
        {
            Form promptForm = new Form()
            {
                Width = 500,
                Height = 180,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = title,
                StartPosition = FormStartPosition.CenterScreen,
                MaximizeBox = false,
                MinimizeBox = false
            };
            Label textLabel = new Label() { Left = 20, Top = 20, Text = prompt, AutoSize = true, MaximumSize = new Size(450, 0) };
            TextBox textBox = new TextBox() { Left = 20, Top = 60, Width = 440, Text = defaultValue };
            Button confirmation = new Button() { Text = "OK", Left = 360, Width = 100, Top = 100, DialogResult = DialogResult.OK };
            Button cancel = new Button() { Text = "Cancel", Left = 250, Width = 100, Top = 100, DialogResult = DialogResult.Cancel };
            
            confirmation.Click += (sender, e) => { promptForm.Close(); };
            cancel.Click += (sender, e) => { promptForm.Close(); };
            
            promptForm.Controls.Add(textLabel);
            promptForm.Controls.Add(textBox);
            promptForm.Controls.Add(confirmation);
            promptForm.Controls.Add(cancel);
            promptForm.AcceptButton = confirmation;
            promptForm.CancelButton = cancel;

            return promptForm.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }
    }
}
