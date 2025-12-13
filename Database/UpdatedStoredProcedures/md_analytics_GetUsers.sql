DELIMITER $$

DROP PROCEDURE IF EXISTS `md_analytics_GetUsers`$$

CREATE PROCEDURE `md_analytics_GetUsers`(
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1 p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;

    SELECT User, `Full Name`, Shift FROM usr_users;

    SET p_Status = 0;
    SET p_ErrorMsg = NULL;
END$$

DELIMITER ;