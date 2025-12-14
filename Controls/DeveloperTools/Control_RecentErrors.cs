using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Models.DeveloperTools;

namespace MTM_WIP_Application_Winforms.Controls.DeveloperTools
{
    public partial class Control_RecentErrors : ThemedUserControl
    {
        public event EventHandler<Model_DevLogEntry>? ErrorSelected;

        public Control_RecentErrors()
        {
            InitializeComponent();
            InitializeGrid();
        }

        private void InitializeGrid()
        {
            Control_RecentErrors_DataGridView_Errors.AutoGenerateColumns = false;
            Control_RecentErrors_DataGridView_Errors.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Timestamp",
                HeaderText = "Time",
                Width = 120,
                DefaultCellStyle = { Format = "MM/dd HH:mm:ss" }
            });
            Control_RecentErrors_DataGridView_Errors.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Source",
                HeaderText = "Source",
                Width = 150
            });
            Control_RecentErrors_DataGridView_Errors.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Message",
                HeaderText = "Message",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            Control_RecentErrors_DataGridView_Errors.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "User",
                HeaderText = "User",
                Width = 100
            });

            Control_RecentErrors_DataGridView_Errors.CellClick += DgvErrors_CellClick;
        }

        private void DgvErrors_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < Control_RecentErrors_DataGridView_Errors.Rows.Count)
            {
                var entry = Control_RecentErrors_DataGridView_Errors.Rows[e.RowIndex].DataBoundItem as Model_DevLogEntry;
                if (entry != null)
                {
                    ErrorSelected?.Invoke(this, entry);
                }
            }
        }

        public void UpdateErrors(List<Model_DevLogEntry> errors)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<List<Model_DevLogEntry>>(UpdateErrors), errors);
                return;
            }

            Control_RecentErrors_DataGridView_Errors.DataSource = errors;
        }
    }
}

