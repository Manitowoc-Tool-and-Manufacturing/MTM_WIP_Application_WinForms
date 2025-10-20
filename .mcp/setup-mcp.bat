@echo off
REM MTM Workflow MCP Server Setup - Windows Batch Wrapper
REM This script ensures PowerShell execution policy allows the setup script to run

echo MTM Workflow MCP Server Setup
echo ==============================
echo.

REM Check if PowerShell is available
where pwsh >nul 2>nul
if %ERRORLEVEL% EQU 0 (
    echo Using PowerShell Core...
    pwsh -NoProfile -ExecutionPolicy Bypass -File "%~dp0setup-mcp.ps1" %*
) else (
    where powershell >nul 2>nul
    if %ERRORLEVEL% EQU 0 (
        echo Using Windows PowerShell...
        powershell -NoProfile -ExecutionPolicy Bypass -File "%~dp0setup-mcp.ps1" %*
    ) else (
        echo ERROR: PowerShell not found!
        echo Please install PowerShell or PowerShell Core.
        pause
        exit /b 1
    )
)

pause
