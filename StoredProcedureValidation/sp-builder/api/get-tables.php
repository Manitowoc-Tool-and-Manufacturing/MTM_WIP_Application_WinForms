<?php
/**
 * Get Tables API Endpoint
 * 
 * Returns list of tables from the database
 */

require_once 'config.php';

try {
    $conn = getDbConnection();
    
    // Query for tables in current database
    $dbName = DB_NAME; // Must use variable for bind_param reference
    $stmt = $conn->prepare("
        SELECT TABLE_NAME, TABLE_TYPE, TABLE_COMMENT, CREATE_TIME, UPDATE_TIME
        FROM information_schema.TABLES
        WHERE TABLE_SCHEMA = ?
        ORDER BY TABLE_NAME
    ");
    
    $stmt->bind_param('s', $dbName);
    $stmt->execute();
    $result = $stmt->get_result();
    
    $tables = [];
    while ($row = $result->fetch_assoc()) {
        $tables[] = [
            'name' => $row['TABLE_NAME'],
            'type' => $row['TABLE_TYPE'],
            'comment' => $row['TABLE_COMMENT'],
            'created' => $row['CREATE_TIME'],
            'updated' => $row['UPDATE_TIME']
        ];
    }
    
    $stmt->close();
    $conn->close();
    
    sendSuccessResponse([
        'database' => DB_NAME,
        'tables' => $tables,
        'count' => count($tables)
    ]);
    
} catch (Exception $e) {
    sendErrorResponse(
        'database',
        'Failed to retrieve tables',
        $e->getMessage()
    );
}
?>
