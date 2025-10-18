param(
    [Parameter(Position = 0)]
    [string]$ProcedureName,

    [switch]$All,

    [string]$MySqlHost = "localhost",
    [int]$MySqlPort = 3306,
    [string]$MySqlUser = "root",
    [string]$MySqlDatabase = "mtm_wip_application_winforms_test",
    [System.Management.Automation.PSCredential]$Credential,
    [string]$MySqlClientPath
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$script:ReadyRoot = Join-Path $PSScriptRoot '..\UpdatedStoredProcedures\ReadyForVerification'

if (-not $Credential) {
    $defaultSecure = ConvertTo-SecureString 'root' -AsPlainText -Force
    $script:MySqlCredential = [System.Management.Automation.PSCredential]::new($MySqlUser, $defaultSecure)
}
else {
    $script:MySqlCredential = $Credential
    if ($MySqlUser -ne $script:MySqlCredential.UserName) {
        $MySqlUser = $script:MySqlCredential.UserName
    }
}

function Resolve-ProcedureFile {
    param([string]$Name)

    $files = Get-ChildItem -Path $script:ReadyRoot -Recurse -Filter '*.sql'
    if ([string]::IsNullOrWhiteSpace($Name)) {
        return $files
    }

    $match = $files | Where-Object { $_.BaseName -eq $Name -or $_.Name -eq $Name -or $_.FullName -like "*\$Name" }
    if (-not $match) {
        throw "Stored procedure file matching '$Name' was not found under ReadyForVerification."
    }
    return $match
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
        'C:\MAMP\bin\mysql\bin\mysql.exe',
        'C:\MAMP PRO\bin\mysql\bin\mysql.exe',
        'C:\MAMP\bin\mysql\bin\mysql',
        'C:\MAMP PRO\bin\mysql\bin\mysql'
    )

    if ($env:MYSQL_PATH) {
        $candidatePaths += $env:MYSQL_PATH
    }
    if ($env:MYSQL_HOME) {
        $candidatePaths += (Join-Path $env:MYSQL_HOME 'bin\mysql.exe')
    }

    foreach ($candidate in $candidatePaths) {
        if ((-not [string]::IsNullOrWhiteSpace($candidate)) -and (Test-Path -Path $candidate)) {
            return (Resolve-Path -Path $candidate).Path
        }
    }

    return $null
}

function Invoke-MySqlScript {
    param([System.IO.FileInfo]$File)

    $mysql = $script:MySqlClientPathResolved
    if (-not $mysql) {
        Write-Warning "MySQL client not found. Skipping execution of $($File.Name)."
        return $false
    }

    $normalizedPath = $File.FullName.Replace('\', '/')
    $sourceCommand = "SOURCE $normalizedPath;"
    $passwordPlain = $script:MySqlCredential.GetNetworkCredential().Password
    $arguments = @(
        "--host=$MySqlHost",
        "--port=$MySqlPort",
        "--user=$MySqlUser",
        "--password=$passwordPlain",
        "--protocol=TCP",
        '--batch',
        '--raw',
        $MySqlDatabase,
        "--execute=$sourceCommand"
    )

    try {
        & $mysql @arguments | Out-Null
        if ($LASTEXITCODE -ne 0) {
            Write-Warning "MySQL returned exit code $LASTEXITCODE for $($File.Name)"
            return $false
        }
        return $true
    }
    catch {
        Write-Warning "MySQL execution failed for $($File.Name): $($_.Exception.Message)"
        return $false
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
        InsertCount        = $insertCount
        UpdateCount        = $updateCount
        DeleteCount        = $totalDelete
        TotalDmlOps        = $totalDml
        DetectedPattern    = $detectedPattern
        RecommendedStrategy= $recommendedStrategy
        ProcedureType      = $procedureType
        Confidence         = $confidence
        HasTransaction     = $hasTransaction
        HasValidation      = $hasValidation
    }
}

function Update-CsvRow {
    param(
        [object[]]$Rows,
        [string]$ProcedureName,
        [pscustomobject]$Analysis
    )

    $row = $Rows | Where-Object { $_.ProcedureName -eq $ProcedureName }
    if (-not $row) {
        Write-Warning "Procedure $ProcedureName not found in procedure-transaction-analysis.csv"
        return $false
    }

    $mutable = $false

    if ([int]$row.InsertCount -ne $Analysis.InsertCount) {
        $row.InsertCount = $Analysis.InsertCount
        $mutable = $true
    }
    if ([int]$row.UpdateCount -ne $Analysis.UpdateCount) {
        $row.UpdateCount = $Analysis.UpdateCount
        $mutable = $true
    }
    if ([int]$row.DeleteCount -ne $Analysis.DeleteCount) {
        $row.DeleteCount = $Analysis.DeleteCount
        $mutable = $true
    }
    if ([int]$row.TotalDMLOps -ne $Analysis.TotalDmlOps) {
        $row.TotalDMLOps = $Analysis.TotalDmlOps
        $mutable = $true
    }
    if ($row.DetectedPattern -ne $Analysis.DetectedPattern) {
        $row.DetectedPattern = $Analysis.DetectedPattern
        $mutable = $true
    }
    if ($row.RecommendedStrategy -ne $Analysis.RecommendedStrategy) {
        $row.RecommendedStrategy = $Analysis.RecommendedStrategy
        $mutable = $true
    }
    if ($row.ProcedureType -ne $Analysis.ProcedureType) {
        $row.ProcedureType = $Analysis.ProcedureType
        $mutable = $true
    }
    if ([int]$row.Confidence -ne $Analysis.Confidence) {
        $row.Confidence = $Analysis.Confidence
        $mutable = $true
    }
    if ([bool]::Parse($row.HasTransactionHandling) -ne $Analysis.HasTransaction) {
        $row.HasTransactionHandling = $Analysis.HasTransaction
        $mutable = $true
    }
    if ([bool]::Parse($row.HasValidation) -ne $Analysis.HasValidation) {
        $row.HasValidation = $Analysis.HasValidation
        $mutable = $true
    }

    if ($mutable) {
        $note = "Corrected via Invoke-T106Review on $(Get-Date -Format 'yyyy-MM-dd HH:mm:ss')"
        $row.DeveloperCorrection = $note
    }

    return $mutable
}

function Update-ChecklistNote {
    param(
        [string]$ProcedureRelativePath,
        [string]$Note
    )

    $checklistPath = Join-Path $PSScriptRoot '..\Checklists\T106a-T106b-Agent-Checklist.md'
    $lines = Get-Content -Path $checklistPath
    $found = $false

    for ($i = 0; $i -lt $lines.Count; $i++) {
        if ($lines[$i] -like "*${ProcedureRelativePath}*") {
            $found = $true
            if ($lines[$i] -match 'NOTE:') {
                $lines[$i] = $lines[$i] -replace 'NOTE:.*$', "NOTE: $Note"
            }
            else {
                $lines[$i] = "$($lines[$i]) — NOTE: $Note"
            }
            break
        }
    }

    if ($found) {
        Set-Content -Path $checklistPath -Value $lines
    }
    else {
        Write-Warning "Checklist entry for $ProcedureRelativePath not found."
    }
}

function Get-ChecklistRelativePath {
    param([System.IO.FileInfo]$FileInfo)

    $rootPathInfo = Resolve-Path -Path $script:ReadyRoot
    $rootPath = $rootPathInfo.Path
    if (-not $rootPath.EndsWith([IO.Path]::DirectorySeparatorChar)) {
        $rootPath += [IO.Path]::DirectorySeparatorChar
    }

    $rootUri = [Uri]::new($rootPath)
    $fileUri = [Uri]::new((Resolve-Path -Path $FileInfo.FullName).Path)
    return $rootUri.MakeRelativeUri($fileUri).ToString()
}

$script:MySqlClientPathResolved = Resolve-MySqlClient -ExplicitPath $MySqlClientPath

$procedures = if ($All.IsPresent) {
    Resolve-ProcedureFile -Name $null
}
else {
    Resolve-ProcedureFile -Name $ProcedureName
}

$csvPath = Join-Path $PSScriptRoot '..\procedure-transaction-analysis.csv'
$csvRows = Import-Csv -Path $csvPath

foreach ($proc in $procedures) {
    Write-Host "Processing $($proc.Name)..."

    $execResult = Invoke-MySqlScript -File $proc
    if (-not $execResult) {
        $relativeChecklistPath = Get-ChecklistRelativePath -FileInfo $proc
        Update-ChecklistNote -ProcedureRelativePath $relativeChecklistPath -Note "MySQL execution failed"
        continue
    }

    $content = Get-Content -Path $proc.FullName -Raw
    $analysis = Measure-ProcedureAnalysis -Content $content

    $procedureName = $proc.BaseName
    $changed = Update-CsvRow -Rows $csvRows -ProcedureName $procedureName -Analysis $analysis

    if ($changed) {
        $csvRows | Export-Csv -Path $csvPath -NoTypeInformation -UseQuotes AsNeeded
        $relative = Get-ChecklistRelativePath -FileInfo $proc
        Update-ChecklistNote -ProcedureRelativePath $relative -Note "CSV updated to match auto-analysis"
        Write-Host "Updated CSV entry for $procedureName" -ForegroundColor Yellow
        continue
    }

    $relativePath = Get-ChecklistRelativePath -FileInfo $proc
    Update-ChecklistNote -ProcedureRelativePath $relativePath -Note "Validation complete; no CSV changes"
}
