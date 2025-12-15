DELIMITER $$

DROP PROCEDURE IF EXISTS `sys_windowform_mapping_Upsert`$$

CREATE PROCEDURE `sys_windowform_mapping_Upsert`(
    IN p_CodebaseName VARCHAR(100),
    IN p_UserFriendlyName VARCHAR(100),
    IN p_IsActive TINYINT,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500),
    OUT p_MappingID INT
)
BEGIN
    DECLARE v_ExistingID INT;
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        SET p_Status = 1;
        SET p_ErrorMsg = 'Error upserting window form mapping';
        ROLLBACK;
    END;

    START TRANSACTION;

    -- Check if exists
    SELECT MappingID INTO v_ExistingID
    FROM WindowFormMapping
    WHERE CodebaseName = p_CodebaseName
    LIMIT 1;

    IF v_ExistingID IS NOT NULL THEN
        -- Update existing
        UPDATE WindowFormMapping
        SET UserFriendlyName = p_UserFriendlyName,
            IsActive = p_IsActive,
            LastModifiedDateTime = NOW()
        WHERE MappingID = v_ExistingID;

        SET p_MappingID = v_ExistingID;
    ELSE
        -- Insert new
        INSERT INTO WindowFormMapping (CodebaseName, UserFriendlyName, IsActive)
        VALUES (p_CodebaseName, p_UserFriendlyName, p_IsActive);

        SET p_MappingID = LAST_INSERT_ID();
    END IF;

    SET p_Status = 0;
    SET p_ErrorMsg = '';

    COMMIT;
END$$

DELIMITER ;
