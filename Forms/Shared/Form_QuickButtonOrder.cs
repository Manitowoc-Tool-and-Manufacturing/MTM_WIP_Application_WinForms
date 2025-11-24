using MTM_WIP_Application_Winforms.Controls.MainForm;

namespace MTM_WIP_Application_Winforms.Forms.Shared;

/// <summary>
/// Dialog form for reordering QuickButtons through drag-and-drop or keyboard navigation.
/// Displays current button order in a ListView with Part ID, Operation, and Quantity columns.
/// Supports drag-and-drop reordering and Shift+Up/Down keyboard shortcuts.
/// Migrated to ThemedForm for automatic DPI scaling and theme support.
/// </summary>
/// <remarks>
/// Layout: ListView fills top portion with instructions below and OK/Cancel buttons at bottom.
/// Minimum size: 500x500
/// Form is non-resizable dialog.
/// Changes are not saved until user clicks OK button.
/// </remarks>
public partial class Form_QuickButtonOrder : ThemedForm
{
    #region Fields

    private readonly List<Control_QuickButton_Single> buttonOrder;
    private int dragIndex = -1;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the Form_QuickButtonOrder dialog.
    /// </summary>
    /// <param name="buttons">List of QuickButtons to display and reorder. Only visible buttons are included.</param>
    public Form_QuickButtonOrder(List<Control_QuickButton_Single> buttons)
    {
        InitializeComponent();

        // DPI scaling and layout now handled by ThemedForm.OnLoad
        SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

        // Store visible buttons only
        buttonOrder = new List<Control_QuickButton_Single>(buttons.Where(b => b.Visible));

        // Populate ListView with button data
        PopulateListView();

        // Wire up event handlers
        Form_QuickButtonOrder_ListView_Main.MouseDown += ListView_MouseDown;
        Form_QuickButtonOrder_ListView_Main.ItemDrag += ListView_ItemDrag;
        Form_QuickButtonOrder_ListView_Main.DragEnter += ListView_DragEnter;
        Form_QuickButtonOrder_ListView_Main.DragDrop += ListView_DragDrop;
        Form_QuickButtonOrder_ListView_Main.KeyDown += ListView_KeyDown;
    }

    #endregion

    #region ListView Population

    /// <summary>
    /// Populates the ListView with current button order data.
    /// Each row shows Position, Part ID, Operation, and Quantity.
    /// </summary>
    private void PopulateListView()
    {
        Form_QuickButtonOrder_ListView_Main.Items.Clear();

        for (int i = 0; i < buttonOrder.Count; i++)
        {
            Control_QuickButton_Single btn = buttonOrder[i];
            
            string partId = btn.PartId;
            string op = btn.Operation;
            string qty = btn.Quantity.ToString();

            ListViewItem lvi = new(new[] { (i + 1).ToString(), partId, op, qty });
            Form_QuickButtonOrder_ListView_Main.Items.Add(lvi);
        }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Returns the reordered button list based on current ListView order.
    /// Call this after dialog closes with OK result to get new ordering.
    /// </summary>
    /// <returns>List of buttons in the order shown in the ListView.</returns>
    public List<Control_QuickButton_Single> GetButtonOrder()
    {
        // Return the button order as currently shown in the ListView
        List<Control_QuickButton_Single> result = new();

        for (int i = 0; i < Form_QuickButtonOrder_ListView_Main.Items.Count; i++)
        {
            // Find the button that corresponds to this ListView item
            ListViewItem listViewItem = Form_QuickButtonOrder_ListView_Main.Items[i];
            string partId = listViewItem.SubItems[1].Text;
            string operation = listViewItem.SubItems[2].Text;
            string quantity = listViewItem.SubItems[3].Text;

            // Find the button with matching data
            Control_QuickButton_Single? matchingButton = buttonOrder.FirstOrDefault(btn =>
            {
                return btn.PartId == partId &&
                       btn.Operation == operation &&
                       btn.Quantity.ToString() == quantity;
            });

            if (matchingButton != null)
            {
                result.Add(matchingButton);
            }
        }

        return result;
    }

    #endregion

    #region Drag and Drop Event Handlers

    /// <summary>
    /// Handles mouse down to track which item is being dragged.
    /// </summary>
    private void ListView_MouseDown(object? sender, MouseEventArgs e)
    {
        dragIndex = Form_QuickButtonOrder_ListView_Main.GetItemAt(e.X, e.Y)?.Index ?? -1;
    }

    /// <summary>
    /// Handles item drag initiation to start drag-and-drop operation.
    /// </summary>
    private void ListView_ItemDrag(object? sender, ItemDragEventArgs e)
    {
        if (Form_QuickButtonOrder_ListView_Main.SelectedItems.Count > 0)
        {
            Form_QuickButtonOrder_ListView_Main.DoDragDrop(Form_QuickButtonOrder_ListView_Main.SelectedItems[0], DragDropEffects.Move);
        }
    }

    /// <summary>
    /// Handles drag enter to allow drop if dragging a ListViewItem.
    /// </summary>
    private void ListView_DragEnter(object? sender, DragEventArgs e)
    {
        if (e.Data?.GetDataPresent(typeof(ListViewItem)) == true)
        {
            e.Effect = DragDropEffects.Move;
        }
    }

    /// <summary>
    /// Handles drop operation to reorder items in both ListView and button list.
    /// Updates position numbers after reordering.
    /// </summary>
    private void ListView_DragDrop(object? sender, DragEventArgs e)
    {
        if (e.Data?.GetDataPresent(typeof(ListViewItem)) == true)
        {
            Point cp = Form_QuickButtonOrder_ListView_Main.PointToClient(new Point(e.X, e.Y));
            object? obj = e.Data.GetData(typeof(ListViewItem));
            if (obj is not ListViewItem dragItem)
            {
                return;
            }

            int dropIndex = Form_QuickButtonOrder_ListView_Main.GetItemAt(cp.X, cp.Y)?.Index ?? Form_QuickButtonOrder_ListView_Main.Items.Count - 1;
            if (dragItem.Index == dropIndex || dragItem.Index < 0)
            {
                return;
            }

            // Reorder both button list and ListView
            Control_QuickButton_Single btn = buttonOrder[dragItem.Index];
            buttonOrder.RemoveAt(dragItem.Index);
            Form_QuickButtonOrder_ListView_Main.Items.RemoveAt(dragItem.Index);
            buttonOrder.Insert(dropIndex, btn);
            Form_QuickButtonOrder_ListView_Main.Items.Insert(dropIndex, (ListViewItem)dragItem.Clone());
            Form_QuickButtonOrder_ListView_Main.Items[dropIndex].Selected = true;

            // Update position numbers
            UpdatePositionNumbers();
        }
    }

    #endregion

    #region Keyboard Event Handlers

    /// <summary>
    /// Handles keyboard shortcuts for reordering (Shift+Up/Down).
    /// </summary>
    private void ListView_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Shift && Form_QuickButtonOrder_ListView_Main.SelectedIndices.Count > 0)
        {
            int idx = Form_QuickButtonOrder_ListView_Main.SelectedIndices[0];

            if (e.KeyCode == Keys.Up && idx > 0)
            {
                // Move item up
                Control_QuickButton_Single btn = buttonOrder[idx];
                buttonOrder.RemoveAt(idx);
                buttonOrder.Insert(idx - 1, btn);

                ListViewItem lvi = (ListViewItem)Form_QuickButtonOrder_ListView_Main.Items[idx].Clone();
                Form_QuickButtonOrder_ListView_Main.Items.RemoveAt(idx);
                Form_QuickButtonOrder_ListView_Main.Items.Insert(idx - 1, lvi);
                Form_QuickButtonOrder_ListView_Main.Items[idx - 1].Selected = true;

                UpdatePositionNumbers();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Down && idx < Form_QuickButtonOrder_ListView_Main.Items.Count - 1)
            {
                // Move item down
                Control_QuickButton_Single btn = buttonOrder[idx];
                buttonOrder.RemoveAt(idx);
                buttonOrder.Insert(idx + 1, btn);

                ListViewItem lvi = (ListViewItem)Form_QuickButtonOrder_ListView_Main.Items[idx].Clone();
                Form_QuickButtonOrder_ListView_Main.Items.RemoveAt(idx);
                Form_QuickButtonOrder_ListView_Main.Items.Insert(idx + 1, lvi);
                Form_QuickButtonOrder_ListView_Main.Items[idx + 1].Selected = true;

                UpdatePositionNumbers();
                e.Handled = true;
            }
        }
    }

    #endregion

    #region Helper Methods

    /// <summary>
    /// Updates position numbers in ListView after reordering.
    /// </summary>
    private void UpdatePositionNumbers()
    {
        for (int i = 0; i < Form_QuickButtonOrder_ListView_Main.Items.Count; i++)
        {
            Form_QuickButtonOrder_ListView_Main.Items[i].SubItems[0].Text = (i + 1).ToString();
        }
    }

    #endregion
}
