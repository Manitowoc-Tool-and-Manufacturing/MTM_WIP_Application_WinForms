# MTM WIP Application Dev Container

## ⚠️ Critical Limitation
This container runs on **Linux (Debian 12)**. 
- **You CANNOT run the WinForms GUI** from this container.
- `dotnet run` will fail with platform errors.
- This environment is strictly for **compilation, unit testing, database management, and backend logic development**.

## Features
- **MySQL 5.7.24**: Matches production database version (No CTEs, No Window Functions).
- **.NET 8 SDK**: For building and testing.
- **Python 3.12**: For AI agents and data scripts.

## Usage
1. Open folder in VS Code.
2. When prompted, click "Reopen in Container".
3. Use the terminal to run tests: `dotnet test`.
4. Use the Database extension to manage MySQL.