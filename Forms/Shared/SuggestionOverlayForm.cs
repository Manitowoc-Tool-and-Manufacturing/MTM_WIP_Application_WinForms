namespace MTM_WIP_Application_Winforms.Forms.Shared
{
    /// <summary>
    /// Modal overlay form that displays filtered suggestions.
    /// Inherits from ThemedForm for automatic theme integration.
    /// Supports keyboard navigation (arrow keys, Home, End, Enter, Escape)
    /// and mouse interaction (single/double click, click outside to cancel).
    /// </summary>
    public partial class SuggestionOverlayForm : ThemedForm
    {
        #region Fields

        private List<string> _suggestions;
        private string? _selectedItem;
        private Control? _parentControl;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the list of suggestions to display.
        /// Must be set before ShowDialog() is called.
        /// </summary>
        public List<string> Suggestions
        {
            get => _suggestions;
            set
            {
                _suggestions = value ?? new List<string>();
                PopulateListBox();
            }
        }

        /// <summary>
        /// Gets the selected item value after user accepts selection.
        /// Null if cancelled or no selection made.
        /// </summary>
        public string? SelectedItem => _selectedItem;

        /// <summary>
        /// Gets the index of the currently selected item in the ListBox.
        /// </summary>
        public int SelectedIndex
        {
            get => suggestionListBox.SelectedIndex;
            private set => suggestionListBox.SelectedIndex = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of SuggestionOverlayForm.
        /// </summary>
        /// <param name="suggestions">List of suggestions to display.</param>
        /// <param name="parentControl">Parent TextBox control for positioning.</param>
        /// <exception cref="ArgumentNullException">If suggestions is null.</exception>
        public SuggestionOverlayForm(List<string> suggestions, Control parentControl)
        {
            ArgumentNullException.ThrowIfNull(suggestions);
            ArgumentNullException.ThrowIfNull(parentControl);

            InitializeComponent();

            // Apply UI scaling fixes to prevent warping at high DPI

            _suggestions = suggestions;
            _selectedItem = null;
            _parentControl = parentControl;

            // Calculate position and size
            Point overlayLocation = CalculatePosition(parentControl, out Size overlaySize);

            // Capture parent form for full-screen overlay (Click-to-dismiss support)
            Form? topLevelForm = parentControl.FindForm();
            if (topLevelForm != null)
            {
            // 1. Capture screenshot of parent form
            Rectangle bounds = topLevelForm.Bounds;
            Bitmap bmp = new(bounds.Width, bounds.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(bounds.Location, Point.Empty, bounds.Size);
            }

            // 2. Configure this form as full-screen overlay
            BackgroundImage = bmp;
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.Manual;
            Bounds = bounds;
            
            // 3. Create container for actual content
            Panel containerPanel = new()
            {
                BackColor = Color.White,
                Size = overlaySize,
                Location = new Point(
                overlayLocation.X - bounds.X, 
                overlayLocation.Y - bounds.Y),
                BorderStyle = BorderStyle.FixedSingle // Flat black border
            };
            
            // 4. Move existing controls to container
            List<Control> controls = [];
            foreach (Control c in Controls) 
                controls.Add(c);
            foreach (Control c in controls) 
                containerPanel.Controls.Add(c);

            // 5. Add container and setup click handlers
            Controls.Add(containerPanel);
            containerPanel.BringToFront();
            
            // Click on background -> Close
            Click += (s, e) => CancelSelection();
            // Click on container -> Do nothing (consume)
            containerPanel.Click += (s, e) => { };
            }
            else
            {
            // Fallback: Standard positioning
            StartPosition = FormStartPosition.Manual;
            Location = overlayLocation;
            Size = overlaySize;
            }

            // Populate ListBox with suggestions
            PopulateListBox();

            // Enable double buffering for smooth rendering
            DoubleBuffered = true;
            
            // Enable custom drawing for ListBox to add professional styling
            suggestionListBox.DrawMode = DrawMode.OwnerDrawFixed;
            // Scale item height based on font size (approx 1.5x font height)
            suggestionListBox.ItemHeight = (int)(this.Font.Height * 1.8); 
            suggestionListBox.DrawItem += SuggestionListBox_DrawItem;

            // Theme automatically applied by ThemedForm base class
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Custom paint to draw professional border.
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            
            // Only draw border on Form if NOT in full-screen overlay mode
            if (this.BackgroundImage == null)
            {
                using (Pen borderPen = new Pen(Color.FromArgb(100, 100, 100), 1))
                {
                    e.Graphics.DrawRectangle(borderPen, 0, 0, this.Width - 1, this.Height - 1);
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Moves selection to next item in list (Down arrow).
        /// Wraps to first item if at end.
        /// </summary>
        public void SelectNext()
        {
            if (suggestionListBox.Items.Count == 0) return;

            int currentIndex = suggestionListBox.SelectedIndex;
            int nextIndex = (currentIndex + 1) % suggestionListBox.Items.Count;
            suggestionListBox.SelectedIndex = nextIndex;
        }

        /// <summary>
        /// Moves selection to previous item in list (Up arrow).
        /// Wraps to last item if at beginning.
        /// </summary>
        public void SelectPrevious()
        {
            if (suggestionListBox.Items.Count == 0) return;

            int currentIndex = suggestionListBox.SelectedIndex;
            int previousIndex = currentIndex <= 0 
                ? suggestionListBox.Items.Count - 1 
                : currentIndex - 1;
            suggestionListBox.SelectedIndex = previousIndex;
        }

        /// <summary>
        /// Moves selection to first item in list (Home key).
        /// </summary>
        public void SelectFirst()
        {
            if (suggestionListBox.Items.Count > 0)
            {
                suggestionListBox.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Moves selection to last item in list (End key).
        /// </summary>
        public void SelectLast()
        {
            if (suggestionListBox.Items.Count > 0)
            {
                suggestionListBox.SelectedIndex = suggestionListBox.Items.Count - 1;
            }
        }

        /// <summary>
        /// Accepts current selection and closes form with DialogResult.OK.
        /// Sets SelectedItem property to currently highlighted item.
        /// Triggered by Enter key or double-click.
        /// </summary>
        public void AcceptSelection()
        {
            if (suggestionListBox.SelectedItem != null)
            {
                _selectedItem = suggestionListBox.SelectedItem.ToString();
                
                
                
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                
            }
        }

        /// <summary>
        /// Cancels selection and closes form with DialogResult.Cancel.
        /// SelectedItem remains null.
        /// Triggered by Escape key or click outside.
        /// Clears parent textbox to indicate no valid match.
        /// </summary>
        public void CancelSelection()
        {
            
            
            _selectedItem = null;
            
            // Clear parent textbox when cancelling (no matches found)
            if (_parentControl is TextBox textBox)
            {
                textBox.Text = string.Empty;
                
            }
            
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Populates ListBox with suggestions and updates match count label.
        /// </summary>
        private void PopulateListBox()
        {
            suggestionListBox.Items.Clear();

            if (_suggestions != null && _suggestions.Count > 0)
            {
                suggestionListBox.Items.AddRange(_suggestions.ToArray());
                suggestionListBox.SelectedIndex = 0; // Select first item by default
                lblMatchCount.Text = $"{_suggestions.Count} matches found";
            }
            else
            {
                lblMatchCount.Text = "No matches found";
            }
        }

        /// <summary>
        /// Calculates optimal position for overlay relative to parent control.
        /// Positions below parent if space available, above if would extend off-screen.
        /// Handles multi-monitor setups and DPI scaling.
        /// </summary>
        /// <param name="parentControl">Parent TextBox control.</param>
        /// <param name="size">Output size of the overlay.</param>
        /// <returns>Screen coordinates for form location.</returns>
        private Point CalculatePosition(Control parentControl, out Size size)
        {
            // Get parent screen location (handles DPI scaling automatically)
            Point textBoxScreenLocation = parentControl.PointToScreen(new Point(0, parentControl.Height));

            // Get overlay size (use parent width, fixed height)
            size = new Size(parentControl.Width, 300); // Max height 300px

            // Get current screen bounds
            Screen currentScreen = Screen.FromControl(parentControl);
            Rectangle workingArea = currentScreen.WorkingArea;

            // Check if overlay would extend beyond bottom of screen
            if (textBoxScreenLocation.Y + size.Height > workingArea.Bottom)
            {
                // Position above TextBox instead
                return parentControl.PointToScreen(new Point(0, -size.Height));
            }

            // Position below TextBox (default)
            return textBoxScreenLocation;
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handles ListBox double-click to accept selection.
        /// </summary>
        private void suggestionListBox_DoubleClick(object? sender, EventArgs e)
        {
            AcceptSelection();
        }

        /// <summary>
        /// Custom draws ListBox items with professional styling.
        /// Provides visual feedback with hover effects and selected item highlighting.
        /// </summary>
        private void SuggestionListBox_DrawItem(object? sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            // Get item text
            string itemText = suggestionListBox.Items[e.Index].ToString() ?? string.Empty;

            // Determine colors based on selection state
            Color backgroundColor;
            Color textColor;
            
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                // Selected item - use accent color
                backgroundColor = Color.FromArgb(0, 120, 215); // Windows accent blue
                textColor = Color.White;
            }
            else
            {
                // Normal item
                backgroundColor = Color.White;
                textColor = Color.Black;
            }

            // Fill background
            using (SolidBrush backgroundBrush = new SolidBrush(backgroundColor))
            {
                e.Graphics.FillRectangle(backgroundBrush, e.Bounds);
            }

            // Draw text with padding and vertical centering
            using (SolidBrush textBrush = new SolidBrush(textColor))
            using (StringFormat sf = new StringFormat { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Near })
            {
                Rectangle textBounds = new Rectangle(
                    e.Bounds.X + 8, 
                    e.Bounds.Y, 
                    e.Bounds.Width - 16, 
                    e.Bounds.Height);
                
                e.Graphics.DrawString(itemText, e.Font ?? this.Font, textBrush, textBounds, sf);
            }

            // Draw focus rectangle if item has focus
            if ((e.State & DrawItemState.Focus) == DrawItemState.Focus)
            {
                e.DrawFocusRectangle();
            }
        }

        /// <summary>
        /// Handles form Deactivate event for light dismiss (click outside).
        /// </summary>
        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);

            // Light dismiss: user clicked outside overlay
            // BUT: Don't cancel if we're already closing with OK (Enter key pressed)
            if (this.Visible && this.DialogResult != DialogResult.OK)
            {
                CancelSelection();
            }
        }

        /// <summary>
        /// Handles keyboard input for navigation and selection.
        /// </summary>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                    SelectNext();
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    break;

                case Keys.Up:
                    SelectPrevious();
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    break;

                case Keys.Home:
                    SelectFirst();
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    break;

                case Keys.End:
                    SelectLast();
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    break;

                case Keys.Enter:
                    AcceptSelection();
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    break;

                case Keys.Escape:
                    CancelSelection();
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    break;

                default:
                    base.OnKeyDown(e);
                    break;
            }
        }

        #endregion
    }
}
