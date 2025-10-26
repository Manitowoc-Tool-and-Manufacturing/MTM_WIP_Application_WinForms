# Feature Specification: Developer Settings Menu

**Feature Branch**: `feature/developer-settings-menu`  
**Created**: 2025-10-25  
**Status**: Draft  
**Input**: Add new "Developer Settings" category to Settings Form TreeView with access to error reports, application logs, and sync controls

---

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Access Developer Tools from Settings (Priority: P1)

Developers and administrators need a centralized location in the Settings Form to access error reports, application logs, and queue management without cluttering the main UI.

**Why this priority**: Provides organized access to developer tools while keeping them separate from end-user settings. Essential for support and troubleshooting workflows.

**Independent Test**: Open Settings Form, expand "Developer Settings" TreeView node, click each menu item and verify correct window/dialog opens.

**Acceptance Scenarios**:

1. **Given** Settings Form is open, **When** user navigates to TreeView, **Then** "Developer Settings" node is visible and collapsible
2. **Given** "Developer Settings" node is expanded, **When** viewing child nodes, **Then** "View Error Reports", "View Application Logs", and "Sync Pending Reports" options are visible
3. **Given** "View Error Reports" is clicked, **When** menu item is selected, **Then** View Error Reports window opens
4. **Given** "View Application Logs" is clicked, **When** menu item is selected, **Then** View Application Logs window opens
5. **Given** "Sync Pending Reports" is clicked and 3 pending reports exist, **When** menu item is selected, **Then** sync process runs and completion message shows "3 reports submitted"

---

### User Story 2 - Pending Report Count Badge (Priority: P2)

Developers can see at a glance how many error reports are queued for submission without opening the sync dialog, enabling proactive queue monitoring.

**Why this priority**: Visual indicator helps developers identify when offline reports have accumulated, prompting timely action.

**Independent Test**: Queue 5 offline reports, open Settings Form, verify "Sync Pending Reports (5)" shows count. Run sync, verify count updates to (0).

**Acceptance Scenarios**:

1. **Given** 5 pending reports exist in queue, **When** Settings Form opens, **Then** "Sync Pending Reports (5)" shows count in parentheses
2. **Given** no pending reports exist, **When** Settings Form opens, **Then** "Sync Pending Reports" shows without count badge
3. **Given** sync is triggered and completes, **When** Settings Form refreshes, **Then** count badge updates to reflect remaining pending reports

---

### User Story 3 - Role-Based Visibility (Priority: P3)

System administrators can configure whether Developer Settings menu is visible to all users or restricted to specific roles, preventing accidental access by end users.

**Why this priority**: Provides flexibility for deployments where developer tools should be admin-only vs. available to all users for self-service troubleshooting.

**Independent Test**: Set `DeveloperSettings.RequireAdminRole = true` in config. Login as non-admin user. Verify Developer Settings node is hidden. Login as admin. Verify node is visible.

**Acceptance Scenarios**:

1. **Given** `RequireAdminRole` is false, **When** any user opens Settings Form, **Then** Developer Settings node is visible
2. **Given** `RequireAdminRole` is true and user is non-admin, **When** Settings Form opens, **Then** Developer Settings node is hidden
3. **Given** `RequireAdminRole` is true and user is admin, **When** Settings Form opens, **Then** Developer Settings node is visible

---

### Edge Cases

- **TreeView already full**: What if Settings TreeView has many categories? Add Developer Settings at bottom, uses scrollbar if needed.
- **Node expansion state**: Should Developer Settings remember expanded/collapsed state? No, default to collapsed on each open.
- **Double-click vs single-click**: How are menu items activated? Single-click to select, double-click or Enter key to open window.
- **Multiple windows open**: What if user clicks menu item multiple times? Check if window already open, bring to front instead of creating duplicate.
- **Sync in progress**: What if user clicks Sync while sync is running? Show "Sync already in progress" message, disable menu item until complete.
- **Icon resources missing**: What if TreeView icons can't be loaded? Show text-only menu items, log warning.

---

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: System MUST add "Developer Settings" TreeNode to existing Settings Form TreeView
- **FR-002**: Developer Settings node MUST contain three child nodes: "View Error Reports", "View Application Logs", "Sync Pending Reports"
- **FR-003**: System MUST display TreeView icons for each node (folder for parent, specific icons for children)
- **FR-004**: Clicking "View Error Reports" MUST open ViewErrorReportsForm window
- **FR-005**: Clicking "View Application Logs" MUST open ViewApplicationLogsForm window
- **FR-006**: Clicking "Sync Pending Reports" MUST trigger Helper_ErrorReportQueue.ManualRetryQueue()
- **FR-007**: System MUST query pending report count on Settings Form load
- **FR-008**: System MUST display count badge next to "Sync Pending Reports" if count > 0
- **FR-009**: System MUST respect `DeveloperSettings.ShowInSettingsMenu` configuration
- **FR-010**: System MUST respect `DeveloperSettings.RequireAdminRole` configuration if enabled
- **FR-011**: System MUST prevent duplicate windows when menu item clicked multiple times
- **FR-012**: System MUST refresh count badge after sync completes
- **FR-013**: Developer Settings node MUST be positioned below existing user settings categories
- **FR-014**: System MUST use consistent TreeView styling with existing Settings categories
- **FR-015**: Menu item activation MUST support both double-click and Enter key

### Key Entities

- **DeveloperSettingsMenu**: Represents the TreeView category and its menu items
  - Attributes: NodeName, ChildItems[], PendingCount, IsVisible, RequiresAdminRole
  - Relationships: Child of Settings TreeView root, parent of menu item nodes

- **MenuItemNode**: Represents individual menu items under Developer Settings
  - Attributes: DisplayText, ActionType (OpenForm/ExecuteCommand), TargetForm, IconKey, IsEnabled
  - Actions: OpenErrorReports, OpenApplicationLogs, SyncPendingReports

---

## UI Mockups

### Settings Form TreeView Integration

**OPTION A: TreeView with Icons (Hierarchical)**
```
Settings Form TreeView:
┌────────────────────────────────┐
│ ⚙ General Settings            │
│ 🎨 Appearance                  │
│ 🔔 Notifications               │
│ 👤 User Preferences            │
│ ▼ 📁 Developer Settings        │ ← NEW
│   │ 📊 View Error Reports      │
│   │ 📄 View Application Logs   │
│   └ 🔄 Sync Pending Reports(3) │ ← Count badge
└────────────────────────────────┘
```

**OPTION B: Flat List with Separator (Simpler)**
```
Settings Form TreeView:
┌────────────────────────────────┐
│ General Settings               │
│ Appearance                     │
│ Notifications                  │
│ User Preferences               │
│ ──────────────────────────     │ ← Separator line
│ View Error Reports             │ ← Flat, no nesting
│ View Application Logs          │
│ Sync Pending Reports (3)       │
└────────────────────────────────┘
```

**OPTION C: Separate "Developer" Tab**
```
Settings Form with Tabs:
┌─────────────────────────────────────┐
│ [User Settings] [Developer]     [X] │ ← Tab bar
├─────────────────────────────────────┤
│ Developer Tools:                    │
│                                     │
│ ┌─────────────────────────────────┐ │
│ │ 📊 View Error Reports           │ │
│ │                                 │ │
│ │ Browse and manage error reports │ │
│ │ submitted by users.             │ │
│ │                                 │ │
│ │              [Open]             │ │
│ └─────────────────────────────────┘ │
│                                     │
│ ┌─────────────────────────────────┐ │
│ │ 📄 View Application Logs        │ │
│ │                                 │ │
│ │ Access logs for any user.       │ │
│ │                                 │ │
│ │              [Open]             │ │
│ └─────────────────────────────────┘ │
│                                     │
│ ┌─────────────────────────────────┐ │
│ │ 🔄 Sync Pending Reports (3)     │ │
│ │                                 │ │
│ │ Submit queued offline reports.  │ │
│ │                                 │ │
│ │              [Sync Now]         │ │
│ └─────────────────────────────────┘ │
└─────────────────────────────────────┘
```

**OPTION D: Collapsible Panel with Buttons**
```
Settings Form (Panel Style):
┌────────────────────────────────┐
│ User Settings:                 │
│ ▼ General                      │
│ ▼ Appearance                   │
│ ▼ Notifications                │
│                                │
│ Developer Tools:               │
│ ▼ 📁 Developer Settings        │ ← Click to expand
│ ┌──────────────────────────────┤
│ │ [📊 View Error Reports   ]   │ ← Buttons
│ │ [📄 View Application Logs]   │
│ │ [🔄 Sync Reports (3)     ]   │
│ └──────────────────────────────┤
└────────────────────────────────┘
```



---

## Code Integration Points

### Settings Form TreeView Initialization

```csharp
// In SettingsForm_Load or InitializeTreeView method

private void InitializeDeveloperSettingsNode()
{
    // Check configuration
    if (!Model_AppVariables.DeveloperSettings.ShowInSettingsMenu)
        return;
    
    if (Model_AppVariables.DeveloperSettings.RequireAdminRole && 
        !Model_Users.CurrentUser.IsAdmin)
        return;
    
    // Create parent node
    var devNode = new TreeNode("Developer Settings")
    {
        Name = "DevSettings",
        ImageKey = "FolderIcon",
        SelectedImageKey = "FolderIconOpen"
    };
    
    // Add child nodes
    devNode.Nodes.Add(new TreeNode("View Error Reports")
    {
        Name = "ErrorReports",
        Tag = "ErrorReports",
        ImageKey = "ReportIcon"
    });
    
    devNode.Nodes.Add(new TreeNode("View Application Logs")
    {
        Name = "AppLogs",
        Tag = "AppLogs",
        ImageKey = "LogIcon"
    });
    
    // Add sync node with count badge
    int pendingCount = Helper_ErrorReportQueue.GetQueuedReportCount();
    string syncText = pendingCount > 0 
        ? $"Sync Pending Reports ({pendingCount})"
        : "Sync Pending Reports";
    
    devNode.Nodes.Add(new TreeNode(syncText)
    {
        Name = "SyncReports",
        Tag = "SyncReports",
        ImageKey = "SyncIcon"
    });
    
    // Add to main TreeView
    treeViewSettings.Nodes.Add(devNode);
}
```

### TreeView Node Selection Handler

```csharp
private void TreeViewSettings_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
{
    if (e.Node.Tag == null) return;
    
    string action = e.Node.Tag.ToString();
    
    switch (action)
    {
        case "ErrorReports":
            OpenErrorReportsWindow();
            break;
            
        case "AppLogs":
            OpenApplicationLogsWindow();
            break;
            
        case "SyncReports":
            SyncPendingReports();
            break;
    }
}

private void OpenErrorReportsWindow()
{
    // Check if window already open
    var existing = Application.OpenForms.OfType<ViewErrorReportsForm>().FirstOrDefault();
    if (existing != null)
    {
        existing.BringToFront();
        existing.Focus();
        return;
    }
    
    var form = new ViewErrorReportsForm();
    form.Show();
}

private void OpenApplicationLogsWindow()
{
    var existing = Application.OpenForms.OfType<ViewApplicationLogsForm>().FirstOrDefault();
    if (existing != null)
    {
        existing.BringToFront();
        existing.Focus();
        return;
    }
    
    var form = new ViewApplicationLogsForm();
    form.Show();
}

private async void SyncPendingReports()
{
    try
    {
        // Disable node during sync
        var syncNode = treeViewSettings.Nodes.Find("SyncReports", true).FirstOrDefault();
        if (syncNode != null)
            syncNode.ForeColor = Color.Gray;
        
        var result = await Helper_ErrorReportQueue.ManualRetryQueue();
        
        MessageBox.Show(
            $"{result.SuccessCount} of {result.TotalCount} reports submitted successfully.",
            "Sync Complete",
            MessageBoxButtons.OK,
            result.SuccessCount == result.TotalCount 
                ? MessageBoxIcon.Information 
                : MessageBoxIcon.Warning
        );
        
        // Refresh count badge
        RefreshSyncNodeCount();
    }
    catch (Exception ex)
    {
        Service_ErrorHandler.HandleException(ex, ErrorSeverity.Medium, 
            controlName: nameof(SettingsForm));
    }
}

private void RefreshSyncNodeCount()
{
    var syncNode = treeViewSettings.Nodes.Find("SyncReports", true).FirstOrDefault();
    if (syncNode == null) return;
    
    int pendingCount = Helper_ErrorReportQueue.GetQueuedReportCount();
    syncNode.Text = pendingCount > 0 
        ? $"Sync Pending Reports ({pendingCount})"
        : "Sync Pending Reports";
    syncNode.ForeColor = SystemColors.WindowText;
}
```

---

## Configuration

Add to `Model_AppVariables` or appsettings.json:

```json
{
  "DeveloperSettings": {
    "ShowInSettingsMenu": true,
    "RequireAdminRole": false,
    "RefreshCountOnFormLoad": true,
    "PreventDuplicateWindows": true
  }
}
```

---

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: Developer Settings menu loads and displays within 100ms of Settings Form opening
- **SC-002**: Clicking each menu item opens correct window within 200ms
- **SC-003**: Pending count badge updates within 500ms after sync completes
- **SC-004**: Role-based visibility correctly hides menu from 100% of non-admin users when enabled
- **SC-005**: Duplicate window prevention works in 100% of cases (no duplicate forms created)
- **SC-006**: Menu items remain responsive even with 50+ pending reports in queue

---

## Relevant Instruction Files

### For Implementation Phase:
- `.github/instructions/csharp-dotnet8.instructions.md` - WinForms patterns, event handling
- `.github/instructions/testing-standards.instructions.md` - Manual validation approach

### For Quality Assurance:
- `.github/instructions/performance-optimization.instructions.md` - UI responsiveness
- `.github/instructions/code-review-standards.instructions.md` - Review checklist

