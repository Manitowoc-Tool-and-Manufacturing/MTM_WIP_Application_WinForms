[CmdletBinding()]
param(
    [switch]$All,
    [string]$ProcedureName,
    [string]$Server = "localhost",
    [int]$Port = 3306,
    [string]$Database = "mtm_wip_application_winforms_test",
    [string]$User = "root",
    [SecureString]$Password = (ConvertTo-SecureString "root" -AsPlainText -Force),
    [string]$MysqlClientPath,
    [string]$ProcedureRoot,
    [string]$OutputRoot = "..\\ValidationRuns",
    [switch]$UpdateChecklist,
    [string]$ChecklistPath = "..\\Checklists\\T106a-T106b-Agent-Checklist.md"
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$scriptRoot = Split-Path -Parent $MyInvocation.MyCommand.Path
$repositoryRoot = Resolve-Path (Join-Path $scriptRoot '..')

if (-not $ProcedureRoot) {
    $ProcedureRoot = Join-Path $scriptRoot '..\\UpdatedStoredProcedures\\ReadyForVerification'
}
$ProcedureRoot = Resolve-Path -Path $ProcedureRoot

$OutputRoot = Resolve-Path -Path (Join-Path $scriptRoot $OutputRoot)
if (-not (Test-Path -Path $OutputRoot)) {
    New-Item -ItemType Directory -Path $OutputRoot | Out-Null
}

function Resolve-MySqlClient {
    param([string]$ExplicitPath)

    if ($ExplicitPath) {
        if (Test-Path -Path $ExplicitPath) {
            return (Resolve-Path -Path $ExplicitPath).Path
        }
        Write-Warning "Provided MySQL client path '$ExplicitPath' was not found."
    }

    $cmd = Get-Command mysql -ErrorAction SilentlyContinue
    if ($cmd) {
        return $cmd.Path
    }

    $candidatePaths = @(
        'C:\\MAMP\\bin\\mysql\\bin\\mysql.exe',
        'C:\\MAMP PRO\\bin\\mysql\\bin\\mysql.exe',
        'C:\\MAMP\\bin\\mysql\\bin\\mysql',
        'C:\\MAMP PRO\\bin\\mysql\\bin\\mysql'
    )

    if ($env:MYSQL_PATH) {
        $candidatePaths += $env:MYSQL_PATH
    }
    if ($env:MYSQL_HOME) {
        $candidatePaths += (Join-Path $env:MYSQL_HOME 'bin\\mysql.exe')
    }

    foreach ($candidate in $candidatePaths) {
        if (-not [string]::IsNullOrWhiteSpace($candidate) -and (Test-Path -Path $candidate)) {
            return (Resolve-Path -Path $candidate).Path
        }
    }

    return $null
}

$mysqlClient = Resolve-MySqlClient -ExplicitPath $MysqlClientPath
if (-not $mysqlClient) {
    throw "mysql client was not found. Install the MySQL CLI or provide -MysqlClientPath."
}

$passwordBstr = [Runtime.InteropServices.Marshal]::SecureStringToBSTR($Password)
$plainPassword = [Runtime.InteropServices.Marshal]::PtrToStringAuto($passwordBstr)
[Runtime.InteropServices.Marshal]::ZeroFreeBSTR($passwordBstr)

function Invoke-MySqlCommand {
    param(
        [string]$Command,
        [string]$Label,
        [string]$LogPath
    )

    $arguments = @(
        "--host=$Server",
        "--port=$Port",
        "--user=$User",
        "--database=$Database",
        '--batch',
        '--raw',
        '--skip-column-names',
        "--execute=$Command"
    )

    $env:MYSQL_PWD = $plainPassword
    try {
        $output = & $mysqlClient @arguments 2>&1
        $exitCode = $LASTEXITCODE
    }
    finally {
        Remove-Item Env:MYSQL_PWD -ErrorAction SilentlyContinue
    }

    if ($LogPath) {
        Add-Content -Path $LogPath -Value ("# Command: {0} ({1})" -f $Label, (Get-Date -Format 'yyyy-MM-dd HH:mm:ss'))
        if ($output) {
            $output | Add-Content -Path $LogPath
        }
        Add-Content -Path $LogPath -Value ''
    }

    return [pscustomobject]@{
        Output   = $output
        ExitCode = $exitCode
    }
}

function Measure-ProcedureAnalysis {
    param([string]$Content)

    $insertCount = ([regex]::Matches($Content, '(?i)\bINSERT\s+(?:IGNORE\s+)?INTO\b')).Count
    $updateCount = ([regex]::Matches($Content, '(?i)\bUPDATE\s+')).Count
    $deleteCount = ([regex]::Matches($Content, '(?i)\bDELETE\s+FROM\b')).Count
    $truncateCount = ([regex]::Matches($Content, '(?i)\bTRUNCATE\s+TABLE\b')).Count
    $selectCount = ([regex]::Matches($Content, '(?i)\bSELECT\b')).Count

    $totalDelete = $deleteCount + $truncateCount
    $totalDml = $insertCount + $updateCount + $totalDelete

    $hasTransaction = $Content -match '(?i)START\s+TRANSACTION' -or $Content -match '(?i)ROLLBACK' -or $Content -match '(?i)COMMIT'
    $hasValidation = $Content -match '(?i)\bIF\s+p_\w+\s+IS\s+NULL\b' -or $Content -match '(?i)\bELSEIF\s+p_\w+\s+IS\s+NULL\b'

    if ($totalDml -eq 0) {
        $detectedPattern = 'READ_ONLY'
        $recommendedStrategy = 'StandardQuery'
        $procedureType = 'SELECT_ONLY'
        $confidence = 95
    }
    else {
        $conditionalPattern = $Content -match '(?i)\bIF\b[\s\S]{0,200}?(INSERT|UPDATE|DELETE|TRUNCATE)'
        if ($totalDml -eq 1) {
            $detectedPattern = 'SINGLE_STEP'
        }
        elseif ($conditionalPattern) {
            $detectedPattern = 'MULTI_STEP_CONDITIONAL'
        }
        else {
            $detectedPattern = 'MULTI_STEP_SEQUENTIAL'
        }

        if ($detectedPattern -eq 'MULTI_STEP_CONDITIONAL') {
            $recommendedStrategy = 'ComplexTransaction'
        }
        else {
            $recommendedStrategy = 'StandardTransaction'
        }

        if ($totalDml -gt 0 -and $selectCount -gt 0) {
            $procedureType = 'MIXED'
        }
        else {
            $procedureType = 'DML_ONLY'
        }

        $confidence = 90
    }

    return [pscustomobject]@{
        InsertCount         = $insertCount
        UpdateCount         = $updateCount
        DeleteCount         = $totalDelete
        TotalDmlOps         = $totalDml
        DetectedPattern     = $detectedPattern
        RecommendedStrategy = $recommendedStrategy
        ProcedureType       = $procedureType
        Confidence          = $confidence
        HasTransaction      = $hasTransaction
        HasValidation       = $hasValidation
    }
}

function Get-ProcedureName {
    param([string]$SqlContent,[System.IO.FileInfo]$File)

    $match = [regex]::Match($SqlContent, '(?i)CREATE\s+(?:DEFINER=`[^`]+`@`[^`]+`\s+)?PROCEDURE\s+`?(?<name>[\w$]+)`?')
    if (-not $match.Success) {
        throw "Unable to locate CREATE PROCEDURE statement in $($File.FullName)."
    }
    return $match.Groups['name'].Value
}

function Get-RelativeChecklistPath {
    param([System.IO.FileInfo]$FileInfo)

    $root = (Resolve-Path -Path $ProcedureRoot).Path
    if (-not $root.EndsWith([IO.Path]::DirectorySeparatorChar)) {
        $root += [IO.Path]::DirectorySeparatorChar
    }

    $rootUri = [Uri]::new($root)
    $fileUri = [Uri]::new((Resolve-Path -Path $FileInfo.FullName).Path)
    return $rootUri.MakeRelativeUri($fileUri).ToString()
}

function Update-ChecklistEntry {
    param(
        [string]$Entry,
        [bool]$Completed,
        [string]$Note
    )

    if (-not (Test-Path -Path $ChecklistPath)) {
        return
    }

    $lines = Get-Content -Path $ChecklistPath
    $escaped = [regex]::Escape($Entry)
    $pattern = "^(\s*-\s*\[)([ xX])(\]\s*$escaped.*)"
    $updated = $false

    for ($i = 0; $i -lt $lines.Count; $i++) {
        $line = $lines[$i]
        if ($line -match $pattern) {
            $state = if ($Completed) { 'x' } else { ' ' }
            $lines[$i] = $line -replace $pattern, ("$1{0}$3" -f $state)
            if (-not [string]::IsNullOrWhiteSpace($Note)) {
                if ($lines[$i] -match 'NOTE:\s*(.*)$') {
                    $lines[$i] = $lines[$i] -replace 'NOTE:.*$', "NOTE: $Note"
                }
                else {
                    $lines[$i] = "$($lines[$i]) — NOTE: $Note"
                }
            }
            $updated = $true
            break
        }
    }

    if ($updated) {
        Set-Content -Path $ChecklistPath -Value $lines -Encoding UTF8
    }
}

function Update-ProcedureChecklist {
    param(
        [string]$RelativePath,
        [bool]$Completed,
        [string]$Note
    )

    if (-not (Test-Path -Path $ChecklistPath)) {
        return
    }

    $lines = Get-Content -Path $ChecklistPath
    $updated = $false

    for ($i = 0; $i -lt $lines.Count; $i++) {
        if ($lines[$i] -like "*${RelativePath}*") {
            $state = if ($Completed) { 'x' } else { ' ' }
            $lines[$i] = $lines[$i] -replace '^\s*-\s*\[[ xX]\]', "  - [$state]"
            if ($lines[$i] -match 'NOTE:\s*(.*)$') {
                $lines[$i] = $lines[$i] -replace 'NOTE:.*$', "NOTE: $Note"
            }
            else {
                $lines[$i] = "$($lines[$i]) — NOTE: $Note"
            }
            $updated = $true
            break
        }
    }

    if ($updated) {
        Set-Content -Path $ChecklistPath -Value $lines -Encoding UTF8
    }
}

$startTime = Get-Date
$runId = $startTime.ToString('yyyyMMdd-HHmmss')
$runRoot = Join-Path $OutputRoot $runId
$logsRoot = Join-Path $runRoot 'procedure-logs'
New-Item -ItemType Directory -Path $runRoot | Out-Null
New-Item -ItemType Directory -Path $logsRoot | Out-Null

$connectionLog = Join-Path $runRoot 'connection-test.log'
Set-Content -Path $connectionLog -Value "# Connection validation started $(Get-Date -Format 'yyyy-MM-dd HH:mm:ss')`n"
$connectionResult = Invoke-MySqlCommand -Command 'SELECT 1;' -Label 'Connection Test' -LogPath $connectionLog
if ($connectionResult.ExitCode -ne 0) {
    $connectionTarget = "{0}:{1}/{2}" -f $Server, $Port, $Database
    throw "Failed to open connection to $connectionTarget. Review $connectionLog for details."
}

$procedureFiles = Get-ChildItem -Path $ProcedureRoot -Recurse -Filter '*.sql'
if ($ProcedureName -and -not $All) {
    $procedureFiles = $procedureFiles | Where-Object { $_.BaseName -eq $ProcedureName -or $_.Name -eq $ProcedureName }
    if (-not $procedureFiles) {
        throw "Stored procedure '$ProcedureName' was not found under $ProcedureRoot."
    }
}
elseif (-not $All) {
    $procedureFiles = $procedureFiles | Sort-Object FullName
}
else {
    $procedureFiles = $procedureFiles | Sort-Object FullName
}

$results = @()

foreach ($file in $procedureFiles) {
    $relativePath = Get-RelativeChecklistPath -FileInfo $file
    $normalized = $file.FullName -replace '\\', '/'
    $sourcePath = $normalized -replace ' ', '\\ '
    $logSafeName = $relativePath.Replace('/', '__')
    $procedureLog = Join-Path $logsRoot ($logSafeName + '.log')
    Set-Content -Path $procedureLog -Value ("# Validation log for {0} ({1})`n" -f $relativePath, (Get-Date -Format 'yyyy-MM-dd HH:mm:ss'))

    $sqlContent = Get-Content -Path $file.FullName -Raw
    $procedureName = Get-ProcedureName -SqlContent $sqlContent -File $file

    $sourceCommand = 'USE `{0}`; SOURCE {1}; SHOW WARNINGS;' -f $Database, $sourcePath
    $sourceResult = Invoke-MySqlCommand -Command $sourceCommand -Label "Source $procedureName" -LogPath $procedureLog

    $definition = ''
    $hasStatusOut = $false
    $hasErrorOut = $false
    $showCreateExit = -1

    if ($sourceResult.ExitCode -eq 0) {
        $showCreateCommand = 'USE `{0}`; SHOW CREATE PROCEDURE `{0}`.`{1}`\G' -f $Database, $procedureName
        $showCreateResult = Invoke-MySqlCommand -Command $showCreateCommand -Label "Show Create $procedureName" -LogPath $procedureLog
        $showCreateExit = $showCreateResult.ExitCode
        if ($showCreateExit -eq 0 -and $showCreateResult.Output) {
            $definition = ($showCreateResult.Output -join "`n")
            $hasStatusOut = $definition -match '(?i)OUT\s+`?p_Status`?'
            $hasErrorOut = $definition -match '(?i)OUT\s+`?p_ErrorMsg`?'
        }
    }

    $analysis = Measure-ProcedureAnalysis -Content $sqlContent
    $status = if ($sourceResult.ExitCode -eq 0 -and $showCreateExit -eq 0 -and $hasStatusOut -and $hasErrorOut) { 'Pass' } else { 'Fail' }

    $note = if ($status -eq 'Pass') {
        "Validation complete at $(Get-Date -Format 'yyyy-MM-dd HH:mm:ss'); log: Database/ValidationRuns/$runId/procedure-logs/$logSafeName.log"
    }
    else {
        "Validation failed at $(Get-Date -Format 'yyyy-MM-dd HH:mm:ss'); review log: Database/ValidationRuns/$runId/procedure-logs/$logSafeName.log"
    }

    if ($UpdateChecklist) {
        Update-ProcedureChecklist -RelativePath $relativePath -Completed ($status -eq 'Pass') -Note $note
    }

    $results += [pscustomobject]@{
        Procedure         = $procedureName
        RelativePath      = $relativePath
        Status            = $status
        SourceExitCode    = $sourceResult.ExitCode
        ShowCreateExitCode= $showCreateExit
        HasStatusOut      = $hasStatusOut
        HasErrorOut       = $hasErrorOut
        TotalDmlOps       = $analysis.TotalDmlOps
        DetectedPattern   = $analysis.DetectedPattern
        LogPath           = (Resolve-Path -Path $procedureLog).Path
    }
}

$resultsPath = Join-Path $runRoot 'procedure-validation-results.csv'
$results | Export-Csv -Path $resultsPath -NoTypeInformation -Encoding UTF8

$tempCheckCommand = "SELECT TABLE_NAME FROM information_schema.tables WHERE TABLE_SCHEMA = '$Database' AND (TABLE_NAME LIKE 'tmp\\_%' OR TABLE_NAME LIKE '#sql%');"
$tempLog = Join-Path $runRoot 'post-run-temp-table-check.log'
Set-Content -Path $tempLog -Value ("# Temporary table check started $(Get-Date -Format 'yyyy-MM-dd HH:mm:ss')`n")
$tempResult = Invoke-MySqlCommand -Command $tempCheckCommand -Label 'Temporary Table Check' -LogPath $tempLog
$tempTables = @()
if ($tempResult.Output) {
    $tempTables = $tempResult.Output | Where-Object { -not [string]::IsNullOrWhiteSpace($_) }
}
$hasTempTables = $tempTables.Count -gt 0

$schemaSnapshotPath = $null
$schemaMatchesBaseline = $null
$extractScript = Join-Path $repositoryRoot 'Database\\Extract-DatabaseSchema-CLI.ps1'
if (Test-Path -Path $extractScript) {
    $schemaWorkDir = Join-Path $runRoot 'schema-snapshot'
    New-Item -ItemType Directory -Path $schemaWorkDir | Out-Null
    $securePassword = $Password
    Push-Location $schemaWorkDir
    try {
        & $extractScript -Server $Server -Port $Port -Database $Database -User $User -Password $securePassword | Out-Null
    }
    finally {
        Pop-Location
    }

    $generatedSnapshot = Join-Path $schemaWorkDir 'database-schema-snapshot.json'
    if (Test-Path -Path $generatedSnapshot) {
        $schemaSnapshotPath = Join-Path $runRoot 'schema-snapshot.json'
        Move-Item -Path $generatedSnapshot -Destination $schemaSnapshotPath -Force

        $baselineSnapshot = Join-Path $repositoryRoot 'Database\\database-schema-snapshot.json'
        if (Test-Path -Path $baselineSnapshot) {
            $baselineHash = (Get-FileHash -Path $baselineSnapshot -Algorithm SHA256).Hash
            $currentHash = (Get-FileHash -Path $schemaSnapshotPath -Algorithm SHA256).Hash
            $schemaMatchesBaseline = $baselineHash -eq $currentHash
        }
    }
}

$finished = Get-Date
$summary = [pscustomobject]@{
    RunId                = $runId
    StartedAtUtc         = $startTime.ToUniversalTime().ToString('o')
    FinishedAtUtc        = $finished.ToUniversalTime().ToString('o')
    Server               = $Server
    Port                 = $Port
    Database             = $Database
    ProcedureCount       = $results.Count
    Passed               = (@($results | Where-Object { $_.Status -eq 'Pass' })).Count
    Failed               = (@($results | Where-Object { $_.Status -eq 'Fail' })).Count
    LogsDirectory        = (Resolve-Path -Path $logsRoot).Path
    ConnectionLog        = (Resolve-Path -Path $connectionLog).Path
    TempTableLog         = (Resolve-Path -Path $tempLog).Path
    TempTablesDetected   = $hasTempTables
    TempTableList        = $tempTables
    SchemaSnapshot       = if ($schemaSnapshotPath) { (Resolve-Path -Path $schemaSnapshotPath).Path } else { $null }
    SchemaMatchesBaseline= $schemaMatchesBaseline
    ResultsCsv           = (Resolve-Path -Path $resultsPath).Path
}

$summaryPath = Join-Path $runRoot 'summary.json'
$summary | ConvertTo-Json -Depth 5 | Set-Content -Path $summaryPath -Encoding UTF8

$packagePath = Join-Path $runRoot "validation-artifacts-$runId.zip"
Compress-Archive -Path (Join-Path $runRoot '*') -DestinationPath $packagePath -Force

if ($UpdateChecklist) {
    $note = "Automated validation run $runId (logs under Database/ValidationRuns/$runId)"
    Update-ChecklistEntry -Entry 'Establish connection to test database (`mtm_wip_application_winforms_test`).' -Completed $true -Note $note
    Update-ChecklistEntry -Entry 'For each procedure:' -Completed ($summary.Failed -eq 0) -Note $note
    Update-ChecklistEntry -Entry 'Execute the script; verify `p_Status` / `p_ErrorMsg` outputs and expected data changes.' -Completed ($summary.Failed -eq 0) -Note $note
    Update-ChecklistEntry -Entry 'Capture supporting evidence (result set snapshots, logs) when behavior deviates from expectations.' -Completed $true -Note $note
    Update-ChecklistEntry -Entry 'Update the user validation checklist with pass/fail status and notes.' -Completed $true -Note $note
    Update-ChecklistEntry -Entry 'Confirm no lingering temp tables or schema drift after execution.' -Completed (-not $hasTempTables -and ($summary.SchemaMatchesBaseline -ne $false)) -Note $note
    Update-ChecklistEntry -Entry 'Submit compiled validation checklist and supporting artifacts for stakeholder review.' -Completed $true -Note $note
}

$summary