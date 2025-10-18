-- Comprehensive CRUD Test Script for sys_parameter_prefix_overrides
-- Purpose: Validate all 5 stored procedures work correctly together
-- Created: 2025-10-18

USE mtm_wip_application_winforms_test;

-- Clean slate
DELETE FROM sys_parameter_prefix_overrides WHERE ProcedureName LIKE 'test_%';

SELECT '========== TEST 1: Add Valid Override ==========' AS Test;
CALL sys_parameter_prefix_overrides_Add(
    'test_procedure', 'UserID', 'p_', 'Test override', 'JOHNK',
    @newId, @status, @errorMsg
);
SELECT @newId AS NewOverrideId, @status AS Status, @errorMsg AS ErrorMessage;

SELECT '========== TEST 2: Attempt Duplicate Add (Expect -3) ==========' AS Test;
CALL sys_parameter_prefix_overrides_Add(
    'test_procedure', 'UserID', 'in_', 'Duplicate attempt', 'JOHNK',
    @dupId, @status, @errorMsg
);
SELECT @dupId AS DuplicateId, @status AS Status, @errorMsg AS ErrorMessage;

SELECT '========== TEST 3: Add With Empty Required Field (Expect -2) ==========' AS Test;
CALL sys_parameter_prefix_overrides_Add(
    '', 'UserID', 'p_', 'Empty proc name', 'JOHNK',
    @emptyId, @status, @errorMsg
);
SELECT @emptyId AS EmptyId, @status AS Status, @errorMsg AS ErrorMessage;

SELECT '========== TEST 4: Get All Active Overrides ==========' AS Test;
CALL sys_parameter_prefix_overrides_Get_All(@status, @errorMsg);
SELECT @status AS Status, @errorMsg AS ErrorMessage;

SELECT '========== TEST 5: Get By Valid ID ==========' AS Test;
CALL sys_parameter_prefix_overrides_Get_ById(@newId, @status, @errorMsg);
SELECT @status AS Status, @errorMsg AS ErrorMessage;

SELECT '========== TEST 6: Get By Non-Existent ID (Expect 1) ==========' AS Test;
CALL sys_parameter_prefix_overrides_Get_ById(999999, @status, @errorMsg);
SELECT @status AS Status, @errorMsg AS ErrorMessage;

SELECT '========== TEST 7: Update Valid Override ==========' AS Test;
CALL sys_parameter_prefix_overrides_Update_ById(
    @newId, 'test_procedure', 'UserID', 'in_', 'Updated reason', 'JOHNK',
    @status, @errorMsg
);
SELECT @status AS Status, @errorMsg AS ErrorMessage;
SELECT * FROM sys_parameter_prefix_overrides WHERE OverrideId = @newId;

SELECT '========== TEST 8: Update With Duplicate Name (Expect -3) ==========' AS Test;
-- First add another override
CALL sys_parameter_prefix_overrides_Add(
    'test_procedure2', 'PartID', 'p_', 'Second test', 'JOHNK',
    @secondId, @status, @errorMsg
);
-- Try to update second to match first
CALL sys_parameter_prefix_overrides_Update_ById(
    @secondId, 'test_procedure', 'UserID', 'p_', 'Duplicate update', 'JOHNK',
    @status, @errorMsg
);
SELECT @status AS Status, @errorMsg AS ErrorMessage;

SELECT '========== TEST 9: Update Non-Existent ID (Expect 1) ==========' AS Test;
CALL sys_parameter_prefix_overrides_Update_ById(
    999999, 'test_proc', 'Param', 'p_', 'No such ID', 'JOHNK',
    @status, @errorMsg
);
SELECT @status AS Status, @errorMsg AS ErrorMessage;

SELECT '========== TEST 10: Soft Delete Override ==========' AS Test;
CALL sys_parameter_prefix_overrides_Delete_ById(@newId, 'JOHNK', @status, @errorMsg);
SELECT @status AS Status, @errorMsg AS ErrorMessage;

SELECT '========== TEST 11: Verify Get_All No Longer Returns Deleted ==========' AS Test;
CALL sys_parameter_prefix_overrides_Get_All(@status, @errorMsg);
SELECT @status AS Status, @errorMsg AS ErrorMessage;

SELECT '========== TEST 12: Verify Get_ById Still Returns Deleted (IsActive=0) ==========' AS Test;
CALL sys_parameter_prefix_overrides_Get_ById(@newId, @status, @errorMsg);
SELECT @status AS Status, @errorMsg AS ErrorMessage;

SELECT '========== TEST 13: Delete Again (Idempotent - Expect 0) ==========' AS Test;
CALL sys_parameter_prefix_overrides_Delete_ById(@newId, 'JOHNK', @status, @errorMsg);
SELECT @status AS Status, @errorMsg AS ErrorMessage;

SELECT '========== TEST 14: Verify Audit Trail Fields ==========' AS Test;
SELECT 
    OverrideId,
    ProcedureName,
    ParameterName,
    CreatedBy,
    CreatedDate,
    ModifiedBy,
    ModifiedDate,
    IsActive
FROM sys_parameter_prefix_overrides
WHERE ProcedureName LIKE 'test_%'
ORDER BY OverrideId;

-- Cleanup
SELECT '========== Cleanup Test Data ==========' AS Test;
DELETE FROM sys_parameter_prefix_overrides WHERE ProcedureName LIKE 'test_%';
SELECT ROW_COUNT() AS DeletedRecords;

SELECT '========== All Tests Complete ==========' AS Test;
