USE mtm_wip_application_winforms;

CREATE TABLE IF NOT EXISTS sys_visual (
    id INT AUTO_INCREMENT PRIMARY KEY,
    json_shift_data JSON,
    json_user_fullnames JSON,
    last_updated DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
