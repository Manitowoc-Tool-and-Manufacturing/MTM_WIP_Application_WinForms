DELIMITER $$
DROP PROCEDURE IF EXISTS `sys_shortcuts_Upsert`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sys_shortcuts_Upsert`(
    IN p_ShortcutName VARCHAR(100),
    IN p_ShortcutKeys INT,
    IN p_Description VARCHAR(255),
    IN p_Category VARCHAR(50),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1 p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = 0;
    END;

    INSERT INTO sys_shortcuts (ShortcutName, ShortcutKeys, Description, Category)
    VALUES (p_ShortcutName, p_ShortcutKeys, p_Description, p_Category)
    ON DUPLICATE KEY UPDATE
        ShortcutKeys = p_ShortcutKeys,
        Description = p_Description,
        Category = p_Category;

    SET p_Status = 1;
    SET p_ErrorMsg = 'Success';
END$$
DELIMITER ;
