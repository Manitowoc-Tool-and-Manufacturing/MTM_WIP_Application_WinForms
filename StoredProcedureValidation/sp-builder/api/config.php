<?php
/**
 * Database Configuration for Stored Procedure Builder
 * 
 * This file defines database connection constants for accessing
 * mtm_wip_application_winforms_test database via MAMP MySQL.
 * 
 * SECURITY: Do not commit production credentials to version control.
 */

// Database connection constants
define('DB_HOST', 'localhost');
define('DB_PORT', '3306');
define('DB_NAME', 'mtm_wip_application_winforms_test');
define('DB_USER', 'root');
define('DB_PASS', 'root');

// CORS headers for file:// and localhost access
header('Content-Type: application/json');
header('Access-Control-Allow-Origin: *');
header('Access-Control-Allow-Methods: GET, POST, OPTIONS');
header('Access-Control-Allow-Headers: Content-Type');

// Handle preflight OPTIONS requests
if ($_SERVER['REQUEST_METHOD'] === 'OPTIONS') {
    http_response_code(200);
    exit();
}

/**
 * Create database connection
 * 
 * @return mysqli Database connection object
 * @throws Exception if connection fails
 */
function getDbConnection() {
    $conn = new mysqli(DB_HOST, DB_USER, DB_PASS, DB_NAME, DB_PORT);
    
    if ($conn->connect_error) {
        throw new Exception("Database connection failed: " . $conn->connect_error);
    }
    
    $conn->set_charset('utf8mb4');
    return $conn;
}

/**
 * Send standardized error response
 * 
 * @param string $errorType Type of error: connection_failed|validation|database|syntax
 * @param string $userMessage User-friendly error message
 * @param string $technicalDetail Technical details for debugging
 * @param int $httpCode HTTP status code (default 500)
 */
function sendErrorResponse($errorType, $userMessage, $technicalDetail = '', $httpCode = 500) {
    http_response_code($httpCode);
    echo json_encode([
        'success' => false,
        'error_type' => $errorType,
        'user_message' => $userMessage,
        'technical_detail' => $technicalDetail
    ]);
    exit();
}

/**
 * Send standardized success response
 * 
 * @param mixed $data Response data payload
 * @param string $message Optional success message
 */
function sendSuccessResponse($data, $message = '') {
    http_response_code(200);
    $response = [
        'success' => true,
        'data' => $data
    ];
    
    if (!empty($message)) {
        $response['message'] = $message;
    }
    
    echo json_encode($response);
    exit();
}

/**
 * Validate required parameters
 * 
 * @param array $params Array of parameter names to check in $_GET or $_POST
 * @param string $method 'GET' or 'POST'
 * @return bool True if all parameters present, false otherwise (sends error response)
 */
function validateRequiredParams($params, $method = 'GET') {
    $source = $method === 'GET' ? $_GET : $_POST;
    
    foreach ($params as $param) {
        if (!isset($source[$param]) || empty($source[$param])) {
            sendErrorResponse(
                'validation',
                ucfirst($param) . ' is required',
                "Missing required parameter: $param",
                400
            );
            return false;
        }
    }
    
    return true;
}
?>
