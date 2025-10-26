# Stored Procedure Contract: sp_error_reports_UpdateStatus

**Purpose**: Update error report status, add developer notes, and record review metadata.

**Created**: 2025-10-25  
**Database**: mtm_wip_application (Release) / mtm_wip_application_winforms_test (Debug)  
**Table**: error_reports

---

## Input Parameters

| Parameter | Type | Required | Default | Description | Example |
|-----------|------|----------|---------|-------------|---------|
| p_ReportID | INT | Yes | N/A | Primary key of report to update | 123 |
| p_NewStatus | VARCHAR(20) | Yes | N/A | New status value | 'Reviewed' or 'Resolved' |
| p_DeveloperNotes | TEXT | No | NULL | Developer comments/findings | 'Investigating database timeout' |
| p_ReviewedBy | VARCHAR(100) | Yes | N/A | Username of developer making change | 'Dev.Smith' |
| p_ReviewedDate | DATETIME | Yes | N/A | Current timestamp of status change | NOW() |

---

## Output Parameters

| Parameter | Type | Description | Values |
|-----------|------|-------------|--------|
| p_Status | INT | Operation result code | 0 = Success<br>-1 = Database error<br>-2 = ReportID not found<br>-3 = Invalid status value |
| p_ErrorMsg | VARCHAR(500) | Human-readable status message | 'Error report status updated successfully'<br>'ReportID not found'<br>'Invalid status value'<br>'Database error occurred' |

---

## SQL Logic

```sql
-- Validate status value
IF p_NewStatus NOT IN ('New', 'Reviewed', 'Resolved') THEN
    SET p_Status = -3;
    SET p_ErrorMsg = 'Invalid status value. Must be: New, Reviewed, or Resolved';
    LEAVE;
END IF;

-- Check if report exists
IF NOT EXISTS (SELECT 1 FROM error_reports WHERE ReportID = p_ReportID) THEN
    SET p_Status = -2;
    SET p_ErrorMsg = 'ReportID not found';
    LEAVE;
END IF;

-- Update report
START TRANSACTION;

UPDATE error_reports
SET 
    Status = p_NewStatus,
    ReviewedBy = p_ReviewedBy,
    ReviewedDate = p_ReviewedDate,
    DeveloperNotes = COALESCE(p_DeveloperNotes, DeveloperNotes)
WHERE ReportID = p_ReportID;

COMMIT;

SET p_Status = 0;
SET p_ErrorMsg = 'Error report status updated successfully';
```

**Business Logic Notes**:
- Status can transition from any state to any state (flexible workflow per spec edge case)
- If p_DeveloperNotes is NULL, existing DeveloperNotes are preserved
- If p_DeveloperNotes is provided, it replaces existing notes (not append)
- ReviewedBy and ReviewedDate always updated (even if changing from Resolved back to New)
- Transaction ensures atomicity

---

## C# Usage Example

```csharp
public static async Task<DaoResult<bool>> UpdateErrorReportStatusAsync(
    int reportId, 
    string newStatus, 
    string developerNotes, 
    string reviewedBy)
{
    try
    {
        string connectionString = Helper_Database_Variables.GetConnectionString();
        
        var parameters = new Dictionary<string, object>
        {
            ["ReportID"] = reportId,
            ["NewStatus"] = newStatus,
            ["DeveloperNotes"] = developerNotes ?? (object)DBNull.Value,
            ["ReviewedBy"] = reviewedBy,
            ["ReviewedDate"] = DateTime.Now
        };
        
        var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
            connectionString,
            "sp_error_reports_UpdateStatus",
            parameters,
            progressHelper: null);
        
        if (!result.IsSuccess)
        {
            LoggingUtility.Log($"[Dao_ErrorReports] Failed to update report {reportId}: {result.StatusMessage}");
            return DaoResult<bool>.Failure(result.StatusMessage);
        }
        
        LoggingUtility.LogApplicationInfo(
            $"[Dao_ErrorReports] Updated report {reportId} to status '{newStatus}' by {reviewedBy}");
        
        return DaoResult<bool>.Success(true, "Status updated successfully");
    }
    catch (Exception ex)
    {
        LoggingUtility.LogApplicationError(ex);
        return DaoResult<bool>.Failure(
            "An unexpected error occurred while updating error report status.");
    }
}
```

---

## UI Workflow

### Before Calling Stored Procedure

1. **Confirmation Dialog**: Show Service_ErrorHandler.ShowConfirmation()
   ```csharp
   string message = $"Change status to '{newStatus}'?\n\n" +
                    "Optional: Add developer notes below.";
   var confirmation = Service_ErrorHandler.ShowConfirmation(message, "Confirm Status Change");
   if (confirmation != DialogResult.OK)
       return; // User cancelled
   ```

2. **Collect Developer Notes**: Multi-line TextBox for optional notes

3. **Get Current User**: Use Model_AppVariables.CurrentUser.UserName for ReviewedBy

4. **Call DAO Method**: UpdateErrorReportStatusAsync()

5. **Refresh UI**: Reload grid and detail view to reflect changes

---

## Test Cases

### Test Case 1: Mark as Reviewed with Notes
**Input**: ReportID=123, NewStatus='Reviewed', DeveloperNotes='Investigating issue', ReviewedBy='Dev.Smith'  
**Expected Output**: Report updated, Status='Reviewed', notes saved  
**Expected Status**: 0 (Success)

### Test Case 2: Mark as Resolved without Notes
**Input**: ReportID=123, NewStatus='Resolved', DeveloperNotes=NULL, ReviewedBy='Dev.Smith'  
**Expected Output**: Report updated, Status='Resolved', existing notes preserved  
**Expected Status**: 0 (Success)

### Test Case 3: Reopen from Resolved to Reviewed
**Input**: ReportID=456 (currently Resolved), NewStatus='Reviewed', ReviewedBy='Dev.Jones'  
**Expected Output**: Status changed to 'Reviewed', ReviewedBy/ReviewedDate updated  
**Expected Status**: 0 (Success)

### Test Case 4: Invalid Status Value
**Input**: NewStatus='Pending' (invalid)  
**Expected Output**: Update rejected  
**Expected Status**: -3 (Invalid status value)

### Test Case 5: ReportID Not Found
**Input**: ReportID=99999 (does not exist)  
**Expected Output**: Update rejected  
**Expected Status**: -2 (ReportID not found)

### Test Case 6: Update DeveloperNotes on Existing Reviewed Report
**Input**: ReportID=789 (already Reviewed), NewStatus='Reviewed', DeveloperNotes='Updated findings'  
**Expected Output**: DeveloperNotes replaced with new text, ReviewedDate updated to now  
**Expected Status**: 0 (Success)

---

## Validation Rules

1. **ReportID** must exist in database (checked by stored procedure)
2. **NewStatus** must be one of: 'New', 'Reviewed', 'Resolved' (validated by stored procedure)
3. **ReviewedBy** cannot be empty (validated in C# layer before call)
4. **ReviewedDate** should be current timestamp (populated in C# with DateTime.Now)
5. **DeveloperNotes** is optional - NULL preserves existing notes, empty string clears notes
6. Status transitions are unrestricted (any → any allowed per spec)

---

## Error Handling

- **ReportID not found**: Returns p_Status=-2, no changes made
- **Invalid status**: Returns p_Status=-3, no changes made
- **Database connection failure**: Returns p_Status=-1, transaction rolled back
- **SQL syntax error**: Returns p_Status=-1, transaction rolled back
- **Success**: Returns p_Status=0, changes committed

---

## Performance Benchmarks

**Target Performance** (from spec.md):
- Status update: < 300ms

**Optimization Notes**:
- Primary key index ensures fast UPDATE
- Single row update with transaction overhead
- No JOINs or complex logic
- Should easily meet 300ms target

---

## Audit Trail Considerations

**Logged Information**:
- Current status before change (for audit trail)
- New status after change
- DeveloperNotes content (or NULL)
- ReviewedBy username
- ReviewedDate timestamp

**Recommendation**: Add trigger or logging to capture status change history:
```sql
-- Future enhancement: error_reports_audit table
CREATE TABLE error_reports_audit (
    AuditID INT AUTO_INCREMENT PRIMARY KEY,
    ReportID INT,
    OldStatus VARCHAR(20),
    NewStatus VARCHAR(20),
    ChangedBy VARCHAR(100),
    ChangedDate DATETIME,
    Notes TEXT
);
```

---

## Concurrency Handling

**Edge Case**: Two developers update same report simultaneously

**Current Behavior**: Last write wins (no optimistic locking)

**Logged Warning** (in C# layer):
```csharp
// Before update, optionally check if ReviewedBy changed since load
if (loadedReviewedBy != null && loadedReviewedBy != currentReviewedBy)
{
    LoggingUtility.Log(
        $"[CONCURRENCY] Report {reportId} ReviewedBy changed from " +
        $"'{loadedReviewedBy}' to '{currentReviewedBy}' during edit");
}
```

**Recommendation**: Display warning if ReviewedBy changed between load and save (future enhancement)

---

## Change History

| Date | Version | Changes | Author |
|------|---------|---------|--------|
| 2025-10-25 | 1.0.0 | Initial contract definition | AI Planning Agent |
