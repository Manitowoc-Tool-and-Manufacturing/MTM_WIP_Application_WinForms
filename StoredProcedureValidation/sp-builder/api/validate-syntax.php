<?php
/**
 * Validate Syntax API Endpoint
 * 
 * Validates MySQL SQL syntax using PREPARE statement
 */

require_once 'config.php';

// Validate required parameter
validateRequiredParams(['sql'], 'POST');

$sql = $_POST['sql'];

try {
    $conn = getDbConnection();
    
    // Attempt to prepare the SQL statement
    // MySQL will validate syntax during PREPARE
    $stmt = $conn->prepare($sql);
    
    if ($stmt) {
        $stmt->close();
        $conn->close();
        
        sendSuccessResponse([
            'valid' => true,
            'message' => 'SQL syntax is valid'
        ], 'SQL syntax is valid');
    } else {
        // Prepare failed - syntax error
        $error = $conn->error;
        $errno = $conn->errno;
        $conn->close();
        
        sendErrorResponse(
            'syntax',
            'SQL syntax error detected',
            "MySQL Error $errno: $error"
        );
    }
    
} catch (Exception $e) {
    sendErrorResponse(
        'syntax',
        'SQL syntax validation failed',
        $e->getMessage()
    );
}
?>
