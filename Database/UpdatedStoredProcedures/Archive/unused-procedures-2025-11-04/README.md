# Archived Unused Stored Procedures
**Archive Date**: November 4, 2025  
**Reason**: Procedures not called by application code

---

## Overview

This folder contains 9 stored procedure files that were identified as not being called by the MTM WIP Application codebase. These procedures have been archived to keep the ReadyForVerification folder focused on actively used procedures.

## Archived Procedures

### Data Fix Procedures (1)

**inv_transaction_Fix_IN_ColumnSwap.sql**
- Purpose: One-time data fix for swapped IN transaction columns
- Reason for Archive: Data fix was completed, procedure no longer needed
- Safe to Delete: Yes (after verifying data integrity)

### Master Data Variants (1)

**md_item_types_Delete_ByID.sql**
- Purpose: Delete item type by ID
- Reason for Archive: Application uses `md_item_types_Delete_ByType` instead
- Safe to Delete: Only if `Delete_ByType` covers all use cases
- Note: May have been superseded by the Type-based variant

### Quick Button Management Variants (4)

**sys_last_10_transactions_MoveToLast_ByUser.sql**
- Purpose: Move quick button to last position
- Reason for Archive: Not used by current quick button implementation
- Alternative: Application uses different ordering strategy

**sys_last_10_transactions_Reorder_ByUser.sql**
- Purpose: Reorder quick buttons
- Reason for Archive: Not used by current quick button implementation
- Alternative: Application uses position-based updates

**sys_last_10_transactions_SwapPositions_ByUser.sql**
- Purpose: Swap two quick button positions
- Reason for Archive: Not used by current quick button implementation
- Alternative: Application uses individual position updates

**sys_last_10_transactions_Update_ByUserAndDate.sql**
- Purpose: Update quick button by user and date
- Reason for Archive: Application uses position-based updates instead
- Alternative: `sys_last_10_transactions_Update_ByUserAndPosition`

### System Utility Variants (3)

**sys_role_GetIdByName.sql**
- Purpose: Get role ID by name
- Reason for Archive: Not used by current code
- Alternative: May be superseded by `sys_GetRoleIdByName`

**sys_roles_Get_ById.sql**
- Purpose: Get role details by ID
- Reason for Archive: Not used by current code
- Note: May have been part of abandoned role management feature

**sys_user_GetByName.sql**
- Purpose: Get user by name
- Reason for Archive: Application may use different user lookup
- Alternative: Check `usr_users_Get_ByUser` for similar functionality

---

## Restoration Instructions

If any of these procedures are needed in the future:

1. **Verify the procedure is truly needed**:
   ```powershell
   # Search codebase for procedure name
   Get-ChildItem -Path "C:\...\MTM_WIP_Application_WinForms" -Filter "*.cs" -Recurse | 
       Select-String -Pattern "procedure_name"
   ```

2. **Move file back to ReadyForVerification**:
   ```powershell
   Move-Item "procedure_name.sql" "../ReadyForVerification/appropriate-folder/"
   ```

3. **Re-run synchronization analysis**:
   ```powershell
   cd Database/Scripts
   .\sync_stored_procedures.ps1
   ```

---

## Analysis Details

**Total Procedures Analyzed**: 106  
**Procedures Called by Code**: 85  
**Files Not Called**: 22  
**Maintenance Procedures Kept**: 16 (utilities, logging, queries)  
**Files Archived**: 9  

**Methodology**: 
- Scanned all C# files for ExecuteDataTableWithStatusAsync, ExecuteNonQueryWithStatusAsync, ExecuteScalarWithStatusAsync calls
- Cross-referenced with SQL files in ReadyForVerification
- Excluded maintenance, logging, and query utilities from archival
- Archived only procedures with no code references and no operational need

---

## Related Files

- **Analysis Report**: `Database/STORED_PROCEDURE_SYNC_REPORT.md`
- **Analysis Data**: `Database/sp_sync_analysis.json`
- **Sync Script**: `Database/Scripts/sync_stored_procedures.ps1`
- **Archive Script**: `Database/Scripts/archive_unused_procedures.ps1`

---

## Notes

- These procedures still exist in the database but are not called by application code
- Consider dropping these procedures from the database after verification
- Some may be alternate implementations that were replaced
- Review before permanent deletion in case they're used by external tools

---

**Last Updated**: 2025-11-04  
**Archived By**: Automated synchronization script
