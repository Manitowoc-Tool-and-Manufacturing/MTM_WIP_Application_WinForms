DELIMITER $$

DROP PROCEDURE IF EXISTS `sys_usercontrol_mapping_GetByWindow`$$

CREATE PROCEDURE `sys_usercontrol_mapping_GetByWindow`(
    IN p_WindowFormMappingID INT,
    IN p_IncludeInactive TINYINT,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        SET p_Status = 1;
        SET p_ErrorMsg = 'Error retrieving user control mappings';
    END;

    IF p_IncludeInactive = 1 THEN
        SELECT 
            MappingID,
            WindowFormMappingID,
            CodebaseName,
            UserFriendlyName,
            IsActive,
            CreatedDateTime,
            LastModifiedDateTime
        FROM UserControlMapping
        WHERE WindowFormMappingID = p_WindowFormMappingID
        ORDER BY UserFriendlyName ASC;
    ELSE
        SELECT 
            MappingID,
            WindowFormMappingID,
            CodebaseName,
            UserFriendlyName,
            IsActive,
            CreatedDateTime,
            LastModifiedDateTime
        FROM UserControlMapping
        WHERE WindowFormMappingID = p_WindowFormMappingID
          AND IsActive = 1
        ORDER BY UserFriendlyName ASC;
    END IF;

    SET p_Status = 0;
    SET p_ErrorMsg = '';
END$$

DELIMITER ;
