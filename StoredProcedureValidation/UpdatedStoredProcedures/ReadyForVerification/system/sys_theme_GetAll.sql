DELIMITER //
DROP PROCEDURE IF EXISTS `sys_theme_GetAll`//
CREATE DEFINER=`root`@`localhost` PROCEDURE `sys_theme_GetAll`(
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Count INT DEFAULT 0;
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;
    START TRANSACTION;
    SELECT ThemeName, SettingsJson
    FROM app_themes
    ORDER BY ThemeName;
    SELECT FOUND_ROWS() INTO v_Count;
    IF v_Count > 0 THEN
        SET p_Status = 1;
        SET p_ErrorMsg = CONCAT('Retrieved ', v_Count, ' theme(s)');
    ELSE
        SET p_Status = 0;
        SET p_ErrorMsg = 'No themes found';
    END IF;
    COMMIT;
END
//
DELIMITER ;
