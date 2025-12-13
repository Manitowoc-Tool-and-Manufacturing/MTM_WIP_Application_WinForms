# Suggested Commands - MTM WIP Application

## Build Commands

### Debug Build (Default)
```powershell
dotnet build MTM_WIP_Application_Winforms.csproj
```

### Release Build
```powershell
dotnet build MTM_WIP_Application_Winforms.csproj --configuration Release
```

### Clean Build Artifacts
```powershell
dotnet clean MTM_WIP_Application_Winforms.csproj
```

### Restore Dependencies
```powershell
dotnet restore MTM_WIP_Application_Winforms.csproj
```

## Run Commands

### Run Application
```powershell
dotnet run --project MTM_WIP_Application_Winforms.csproj
```

## Database Commands

### Connect to Production Database
```powershell
& "C:\MAMP\bin\mysql\bin\mysql.exe" -h localhost -P 3306 -u root -proot mtm_wip_application_winforms
```

### Connect to Test Database
```powershell
& "C:\MAMP\bin\mysql\bin\mysql.exe" -h localhost -P 3306 -u root -proot mtm_wip_application_winforms_test
```

### Show All Databases
```powershell
& "C:\MAMP\bin\mysql\bin\mysql.exe" -h localhost -P 3306 -u root -proot -e "SHOW DATABASES;"
```

### Show Tables in Production
```powershell
& "C:\MAMP\bin\mysql\bin\mysql.exe" -h localhost -P 3306 -u root -proot mtm_wip_application_winforms -e "SHOW TABLES;"
```

### Execute SQL File
```powershell
& "C:\MAMP\bin\mysql\bin\mysql.exe" -h localhost -P 3306 -u root -proot mtm_wip_application_winforms < Database/CurrentDatabase/schema.sql
```

## Windows Utility Commands

### List Directory Contents
```powershell
Get-ChildItem [path]
# Or short form
ls [path]
# Or dir [path]
```

### Find Files
```powershell
Get-ChildItem -Path . -Filter "*.cs" -Recurse
```

### Search File Contents (like grep)
```powershell
Select-String -Path "*.cs" -Pattern "pattern"
```

### Change Directory
```powershell
Set-Location [path]
# Or short form
cd [path]
```

### Copy Files
```powershell
Copy-Item -Path source -Destination dest
```

### Move/Rename Files
```powershell
Move-Item -Path old -Destination new
```

### Remove Files
```powershell
Remove-Item -Path file
```

## Git Commands

### Check Status
```powershell
git status
```

### Stage Changes
```powershell
git add [file]
git add .  # Stage all changes
```

### Commit
```powershell
git commit -m "message"
```

### Push Changes
```powershell
git push origin master
```

### Pull Changes
```powershell
git pull origin master
```

### View Branches
```powershell
git branch
```

### Create New Branch
```powershell
git checkout -b branch-name
```

## Testing Commands

### Run All Tests (if test project exists)
```powershell
dotnet test
```

### Run Specific Test
```powershell
dotnet test --filter "TestName"
```

## Output Locations

- **Debug Build**: `bin/Debug/net8.0-windows/`
- **Release Build**: `bin/Release/net8.0-windows/`
- **Application Logs**: `%APPDATA%\MTM\Logs\`
- **Executable**: `MTM_WIP_Application_Winforms.exe`
