DELIMITER $$

DROP PROCEDURE IF EXISTS `sys_windowform_mapping_GetAll`$$

CREATE PROCEDURE `sys_windowform_mapping_GetAll`(
    IN p_IncludeInactive TINYINT,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        SET p_Status = 1;
        SET p_ErrorMsg = 'Error retrieving window form mappings';
    END;

    IF p_IncludeInactive = 1 THEN
        SELECT 
            MappingID,
            CodebaseName,
            UserFriendlyName,
            IsActive,
            CreatedDateTime,
            LastModifiedDateTime
        FROM WindowFormMapping
        ORDER BY UserFriendlyName ASC;
    ELSE
        SELECT 
            MappingID,
            CodebaseName,
            UserFriendlyName,
            IsActive,
            CreatedDateTime,
            LastModifiedDateTime
        FROM WindowFormMapping
        WHERE IsActive = 1
        ORDER BY UserFriendlyName ASC;
    END IF;

    SET p_Status = 0;
    SET p_ErrorMsg = '';
END$$

DELIMITER ;
