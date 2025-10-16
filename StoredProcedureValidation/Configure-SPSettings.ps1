# =============================================
# SPSettings Configuration Wizard
# =============================================
# This wizard helps you configure SPSettings.json for your project
# It will guide you through each setting with plain English explanations

param(
    [switch]$Reset,
    [switch]$UseDefaults
)

$ErrorActionPreference = 'Stop'

# Colors for output
function Write-Header {
    param([string]$Text)
    Write-Host "`n╔════════════════════════════════════════════════════════╗" -ForegroundColor Cyan
    Write-Host "║  $Text" -ForegroundColor Cyan
    Write-Host "╚════════════════════════════════════════════════════════╝" -ForegroundColor Cyan
}

function Write-Step {
    param([string]$Text)
    Write-Host "`n▶ $Text" -ForegroundColor Yellow
}

function Write-Info {
    param([string]$Text)
    Write-Host "  ℹ️  $Text" -ForegroundColor Gray
}

function Write-Example {
    param([string]$Text)
    Write-Host "  💡 Example: $Text" -ForegroundColor DarkGray
}

function Write-Success {
    param([string]$Text)
    Write-Host "  ✅ $Text" -ForegroundColor Green
}

function Write-Warning {
    param([string]$Text)
    Write-Host "  ⚠️  $Text" -ForegroundColor Yellow
}

function Get-UserInput {
    param(
        [string]$Prompt,
        [string]$Default = "",
        [switch]$Required,
        [ValidateSet("Text", "Number", "YesNo", "Path", "Array")]
        [string]$Type = "Text"
    )
    
    $defaultText = if ($Default) { " [default: $Default]" } else { " [press ENTER to skip]" }
    $requiredMark = if ($Required) { "*" } else { "" }
    
    while ($true) {
        $input = Read-Host "`n  $requiredMark$Prompt$defaultText"
        
        if ([string]::IsNullOrWhiteSpace($input)) {
            if ($Default) {
                return $Default
            } elseif (-not $Required) {
                return ""
            } else {
                Write-Warning "This field is required. Please enter a value."
                continue
            }
        }
        
        switch ($Type) {
            "Number" {
                if ($input -match '^\d+$') {
                    return [int]$input
                } else {
                    Write-Warning "Please enter a valid number."
                    continue
                }
            }
            "YesNo" {
                if ($input -match '^(y|yes|n|no)$') {
                    return ($input -match '^(y|yes)$')
                } else {
                    Write-Warning "Please enter 'y' for yes or 'n' for no."
                    continue
                }
            }
            "Path" {
                # Convert to relative path if it's absolute
                if ([System.IO.Path]::IsPathRooted($input)) {
                    $scriptPath = $PSScriptRoot
                    $input = [System.IO.Path]::GetRelativePath($scriptPath, $input)
                }
                return $input.Replace('/', '\')
            }
            "Array" {
                return $input -split ',' | ForEach-Object { $_.Trim() }
            }
            default {
                return $input
            }
        }
    }
}

function Get-ProjectRoot {
    $scriptPath = $PSScriptRoot
    $parent = Split-Path $scriptPath -Parent
    return $parent
}

# =============================================
# MAIN WIZARD
# =============================================

Clear-Host

Write-Header "🧙 SPSettings.json Configuration Wizard"

Write-Host @"

Welcome to the SPSettings.json configuration wizard!

This wizard will help you set up the configuration file for the
Stored Procedure Validation Tool. Each step includes:
  • Plain English explanation of what the setting does
  • Examples to guide you
  • Smart defaults based on your project structure

SKIPPING SECTIONS:
  • Press ENTER without typing anything to skip optional fields
  • For entire sections, just press ENTER at the first question
  • Skipped sections will use existing values or smart defaults
  • Fields marked with * are required and cannot be skipped

You can press ENTER to accept default values (shown in brackets).

Let's get started!

"@ -ForegroundColor White

Read-Host "Press ENTER to begin"

# =============================================
# SECTION 1: Project Settings
# =============================================

Write-Header "📁 Section 1: Project Settings"
Write-Info "These are basic details about your project."

$projectName = Get-UserInput `
    -Prompt "What is your project name?" `
    -Default "MyProject" `
    -Required

Write-Info "This is just a friendly name to identify your project in the tool."

$projectRoot = Get-ProjectRoot
Write-Step "Project Root Directory"
Write-Info "This is the absolute path to your project's root folder."
Write-Info "Detected: $projectRoot"

$useDetected = Get-UserInput `
    -Prompt "Use detected path?" `
    -Default "y" `
    -Type YesNo

if (-not $useDetected) {
    $projectRoot = Get-UserInput `
        -Prompt "Enter absolute path to project root" `
        -Default $projectRoot `
        -Required
}

Write-Success "Using: $projectRoot"

$projectDescription = Get-UserInput `
    -Prompt "Brief project description (optional)" `
    -Default "Stored procedure validation for $projectName"

# =============================================
# SECTION 2: File Paths
# =============================================

Write-Header "📂 Section 2: File Paths"
Write-Info "Tell us where your important files are located (relative to this folder)."

Write-Step "Stored Procedures Folder"
Write-Info "This is where your .sql stored procedure files are located."
Write-Example "..\Database\StoredProcedures or ..\sql\procedures"

$spFolder = Get-UserInput `
    -Prompt "Path to stored procedures folder" `
    -Default "..\Database\UpdatedStoredProcedures" `
    -Type Path `
    -Required

Write-Step "C# Code Folder"
Write-Info "This is where your C# source code is located (Forms, DAOs, Services, etc.)."
Write-Example ".. (parent folder) or ..\src"

$csharpFolder = Get-UserInput `
    -Prompt "Path to C# code folder" `
    -Default ".." `
    -Type Path `
    -Required

Write-Step "Folders to Exclude"
Write-Info "Folders to skip during analysis (like obj, bin, node_modules)."
Write-Info "Enter as comma-separated list: obj,bin,Tests"

$excludeFolders = Get-UserInput `
    -Prompt "Folders to exclude" `
    -Default "obj,bin,Tests,.vs,.git,node_modules" `
    -Type Array

Write-Step "Output File Names"
Write-Info "Names for the generated analysis files (just the filename, not path)."

$csvFile = Get-UserInput `
    -Prompt "CSV output filename" `
    -Default "procedure-transaction-analysis.csv"

$sqlJson = Get-UserInput `
    -Prompt "SQL operations JSON filename" `
    -Default "sql-operations-detailed.json"

$hierarchyJson = Get-UserInput `
    -Prompt "Call hierarchy JSON filename" `
    -Default "call-hierarchy-complete.json"

# =============================================
# SECTION 3: Database Connection
# =============================================

Write-Header "🗄️  Section 3: Database Connection"
Write-Info "Connection details for your MySQL database."
Write-Info "Press ENTER without input to skip this section and keep existing values."
Write-Warning "Credentials are only used by analysis scripts, not stored elsewhere."

$dbServer = Get-UserInput `
    -Prompt "Database server address" `
    -Default "localhost"

# Skip section if first field is empty
if ([string]::IsNullOrWhiteSpace($dbServer)) {
    Write-Info "Skipping database configuration section."
    # Load from existing file if available
    if (Test-Path "$PSScriptRoot\SPSettings.json") {
        $existing = Get-Content "$PSScriptRoot\SPSettings.json" -Raw | ConvertFrom-Json
        $dbServer = $existing.Database.Server
        $dbPort = $existing.Database.Port
        $dbName = $existing.Database.DatabaseName
        $dbUser = $existing.Database.Username
        $dbPassword = $existing.Database.Password
        $dbDescription = $existing.Database.Description
        Write-Success "Using existing database configuration."
    } else {
        # Use defaults
        $dbServer = "localhost"
        $dbPort = 3306
        $dbName = "myapp_database"
        $dbUser = "root"
        $dbPassword = "root"
        $dbDescription = "Development database credentials"
        Write-Info "Using default database configuration."
    }
} else {
    $dbPort = Get-UserInput `
        -Prompt "Database port" `
        -Default "3306" `
        -Type Number `
        -Required

    $dbName = Get-UserInput `
        -Prompt "Database name" `
        -Default "myapp_database" `
        -Required

    $dbUser = Get-UserInput `
        -Prompt "Database username" `
        -Default "root" `
        -Required

    Write-Info "For security, consider using a read-only database user for analysis."

    $dbPassword = Get-UserInput `
        -Prompt "Database password" `
        -Default "root" `
        -Required

    $dbDescription = Get-UserInput `
        -Prompt "Connection description (optional)" `
        -Default "Development database credentials"
}

# =============================================
# SECTION 4: Analysis Settings
# =============================================

Write-Header "⚙️  Section 4: Analysis Settings"
Write-Info "Configure how the analysis scripts behave."

Write-Step "Progress Bars"
Write-Info "Show visual progress bars during analysis?"
$enableProgress = Get-UserInput `
    -Prompt "Enable progress bars? (y/n)" `
    -Default "y" `
    -Type YesNo

Write-Step "Recursive Search"
Write-Info "Search subdirectories for stored procedures?"
$recursiveSearch = Get-UserInput `
    -Prompt "Enable recursive search? (y/n)" `
    -Default "y" `
    -Type YesNo

Write-Step "Event Handler Detection"
Write-Info "Trace UI event handlers (like button clicks) in call hierarchies?"
$detectEvents = Get-UserInput `
    -Prompt "Detect event handlers? (y/n)" `
    -Default "y" `
    -Type YesNo

Write-Step "Call Hierarchy Tracing"
Write-Info "Build complete call chains from UI → DAO → Stored Procedure?"
$traceHierarchy = Get-UserInput `
    -Prompt "Trace call hierarchy? (y/n)" `
    -Default "y" `
    -Type YesNo

Write-Step "SQL Operations Analysis"
Write-Info "Extract detailed SQL operation breakdowns (INSERT/UPDATE/DELETE details)?"
$analyzeSql = Get-UserInput `
    -Prompt "Analyze SQL operations? (y/n)" `
    -Default "y" `
    -Type YesNo

Write-Step "Call Depth Limit"
Write-Info "Maximum depth to trace method calls (prevents infinite loops)."
Write-Example "3 (traces 3 levels deep: EventHandler → Method → DAO → SP)"
$maxDepth = Get-UserInput `
    -Prompt "Max call depth" `
    -Default "3" `
    -Type Number

Write-Step "Event Handler Patterns"
Write-Info "Patterns to detect as UI event handlers (e.g., _Click, _Load)."
Write-Info "Enter as comma-separated list."
$eventPatterns = Get-UserInput `
    -Prompt "Event handler patterns" `
    -Default "_Click,_SelectedIndexChanged,_TextChanged,_CheckedChanged,_ValueChanged,_Load,_KeyPress,_CellContentClick" `
    -Type Array

# =============================================
# SECTION 5: Domain Categories
# =============================================

Write-Header "🏷️  Section 5: Domain Categories"
Write-Info "Define how stored procedures are categorized by their name prefixes."
Write-Info "Press ENTER to skip and use default categories."
Write-Example "Inventory procedures start with 'inv_' or 'inventory_'"

$domains = @{
    "Inventory" = "inv_,inventory_"
    "Transactions" = "trans_,transaction_"
    "Logging" = "log_,err_,error_"
    "MasterData" = "md_"
    "System" = "sys_,system_"
    "Users" = "usr_,user_,users_"
}

$customizeDomains = Get-UserInput `
    -Prompt "Customize domain categories? (y/n)" `
    -Default "n" `
    -Type YesNo

if ($customizeDomains -and $customizeDomains -ne "") {
    # Create array copy to avoid collection modified exception
    $domainKeys = @($domains.Keys)
    foreach ($domain in $domainKeys) {
        Write-Step "$domain Domain"
        Write-Info "Current prefixes: $($domains[$domain])"
        $newPrefixes = Get-UserInput `
            -Prompt "Prefixes for $domain (comma-separated)" `
            -Default $domains[$domain]
        $domains[$domain] = $newPrefixes
    }
}

# =============================================
# SECTION 6: File Patterns
# =============================================

Write-Header "📄 Section 6: File Patterns"
Write-Info "Glob patterns to find specific file types in your project."

Write-Step "DAO Files Pattern"
Write-Info "Pattern to find Data Access Object files."
$daoPattern = Get-UserInput `
    -Prompt "DAO files pattern" `
    -Default "Data\Dao_*.cs"

Write-Step "Form Files Patterns"
Write-Info "Patterns to find form/control files (comma-separated)."
$formPatterns = Get-UserInput `
    -Prompt "Form files patterns" `
    -Default "Forms\**\*.cs,Controls\**\*.cs" `
    -Type Array

Write-Step "Helper Files Pattern"
Write-Info "Pattern to find helper/utility files."
$helperPattern = Get-UserInput `
    -Prompt "Helper files pattern" `
    -Default "Helpers\Helper_*.cs"

Write-Step "Stored Procedure Files Pattern"
Write-Info "Pattern to find stored procedure SQL files."
$spPattern = Get-UserInput `
    -Prompt "SP files pattern" `
    -Default "**\*.sql"

# =============================================
# SECTION 7: Validation Settings
# =============================================

Write-Header "✅ Section 7: Validation Settings"
Write-Info "Define quality standards for stored procedures."

$requireTransaction = Get-UserInput `
    -Prompt "Require explicit transaction handling? (y/n)" `
    -Default "y" `
    -Type YesNo

$requireValidation = Get-UserInput `
    -Prompt "Require input validation? (y/n)" `
    -Default "y" `
    -Type YesNo

$checkInjection = Get-UserInput `
    -Prompt "Check for SQL injection vulnerabilities? (y/n)" `
    -Default "y" `
    -Type YesNo

$validateParams = Get-UserInput `
    -Prompt "Validate parameter naming conventions? (y/n)" `
    -Default "y" `
    -Type YesNo

$enforceNaming = Get-UserInput `
    -Prompt "Enforce stored procedure naming conventions? (y/n)" `
    -Default "y" `
    -Type YesNo

# =============================================
# SECTION 8: HTML Viewer Settings
# =============================================

Write-Header "🌐 Section 8: HTML Viewer Settings"
Write-Info "Configure the browser-based review tool."

$enableTooltips = Get-UserInput `
    -Prompt "Enable tooltips? (y/n)" `
    -Default "y" `
    -Type YesNo

$showProgress = Get-UserInput `
    -Prompt "Show progress bar? (y/n)" `
    -Default "y" `
    -Type YesNo

$autoSaveInterval = Get-UserInput `
    -Prompt "Auto-save interval (minutes, 0 to disable)" `
    -Default "30" `
    -Type Number

$enableDragDrop = Get-UserInput `
    -Prompt "Enable drag-and-drop CSV loading? (y/n)" `
    -Default "y" `
    -Type YesNo

$showSqlPreview = Get-UserInput `
    -Prompt "Enable SQL preview button? (y/n)" `
    -Default "y" `
    -Type YesNo

# =============================================
# BUILD JSON
# =============================================

Write-Header "💾 Building Configuration"

# Convert domain categories to proper format
$domainCategories = @{}
foreach ($key in $domains.Keys) {
    $domainCategories[$key] = $domains[$key] -split ',' | ForEach-Object { $_.Trim() }
}

$settings = [ordered]@{
    ProjectSettings = [ordered]@{
        ProjectName = $projectName
        RootDirectory = $projectRoot
        Description = $projectDescription
    }
    Paths = [ordered]@{
        StoredProceduresFolder = $spFolder
        CSharpCodeFolder = $csharpFolder
        ExcludeFolders = $excludeFolders
        OutputCSV = $csvFile
        SQLOperationsJSON = $sqlJson
        CallHierarchyJSON = $hierarchyJson
    }
    DatabaseConnection = [ordered]@{
        Server = $dbServer
        Port = $dbPort
        Database = $dbName
        Username = $dbUser
        Password = $dbPassword
        Description = $dbDescription
    }
    AnalysisSettings = [ordered]@{
        EnableProgressBars = $enableProgress
        RecursiveSearch = $recursiveSearch
        DetectEventHandlers = $detectEvents
        TraceCallHierarchy = $traceHierarchy
        AnalyzeSQLOperations = $analyzeSql
        MaxCallDepth = $maxDepth
        EventHandlerPatterns = $eventPatterns
    }
    DomainCategories = $domainCategories
    FilePatterns = [ordered]@{
        DAOFiles = $daoPattern
        FormFiles = $formPatterns
        HelperFiles = $helperPattern
        StoredProcedureFiles = $spPattern
    }
    ValidationSettings = [ordered]@{
        RequireTransactionHandling = $requireTransaction
        RequireValidation = $requireValidation
        CheckForSQLInjection = $checkInjection
        ValidateParameterNaming = $validateParams
        EnforceNamingConventions = $enforceNaming
    }
    HTMLViewerSettings = [ordered]@{
        DefaultTheme = "purple-gradient"
        EnableTooltips = $enableTooltips
        ShowProgressBar = $showProgress
        AutoSaveInterval = $autoSaveInterval
        EnableDragDrop = $enableDragDrop
        ShowSQLPreview = $showSqlPreview
    }
}

# Convert to JSON
$json = $settings | ConvertTo-Json -Depth 10

# =============================================
# SAVE FILE
# =============================================

$outputPath = Join-Path $PSScriptRoot "SPSettings.json"

# Backup existing file
if (Test-Path $outputPath) {
    $backupPath = Join-Path $PSScriptRoot "SPSettings.backup.$(Get-Date -Format 'yyyyMMdd-HHmmss').json"
    Copy-Item $outputPath $backupPath
    Write-Success "Backed up existing file to: $(Split-Path $backupPath -Leaf)"
}

# Write new file
$json | Out-File -FilePath $outputPath -Encoding UTF8

Write-Header "🎉 Configuration Complete!"

Write-Host @"

✅ SPSettings.json has been created successfully!

📁 Location: $outputPath

Next Steps:
  1. Review the generated SPSettings.json file
  2. Run the analysis: .\RUN-COMPLETE-ANALYSIS.ps1
  3. Open procedure-review-tool.html in your browser
  4. Load the generated CSV and start reviewing

Configuration Summary:
  • Project: $projectName
  • Stored Procedures: $spFolder
  • Database: $dbName on $dbServer
  • Analysis Features: $(if($traceHierarchy){'✓'}else{'✗'}) Call Hierarchy, $(if($analyzeSql){'✓'}else{'✗'}) SQL Operations
  • Domain Categories: $($domainCategories.Count) configured

"@ -ForegroundColor Green

$viewFile = Get-UserInput `
    -Prompt "Open SPSettings.json now? (y/n)" `
    -Default "n" `
    -Type YesNo

if ($viewFile) {
    Start-Process notepad.exe -ArgumentList $outputPath
}

Write-Host "`nThank you for using the configuration wizard! 🧙✨`n" -ForegroundColor Cyan
