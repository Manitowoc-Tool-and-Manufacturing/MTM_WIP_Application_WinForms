@echo off
REM ============================================================================
REM MTM Inventory Data Integrity Validation Runner
REM ============================================================================

echo ============================================================================
echo MTM WIP Application - Inventory Data Integrity Validation
echo ============================================================================
echo.
echo Starting validation... This may take a few moments.
echo.

"C:\MAMP\bin\mysql\bin\mysql.exe" -h localhost -P 3306 -u root -proot MTM_WIP_Application_Winforms < "%~dp0validate_inventory_integrity.sql"

echo.
echo ============================================================================
echo Validation Complete!
echo ============================================================================
echo.
pause
