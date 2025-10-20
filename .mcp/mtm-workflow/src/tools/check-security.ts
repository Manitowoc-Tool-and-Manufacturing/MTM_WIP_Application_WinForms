import * as fs from "fs";
import * as path from "path";

interface SecurityIssue {
  file: string;
  line: number;
  severity: "critical" | "high" | "medium" | "low";
  category: string;
  vulnerability: string;
  description: string;
  remediation: string;
  cwe?: string; // Common Weakness Enumeration ID
}

interface SecurityResult {
  file: string;
  issues: SecurityIssue[];
  critical_count: number;
  high_count: number;
  medium_count: number;
  low_count: number;
  security_score: number; // 0-100
}

export async function checkSecurity(args: {
  source_dir: string;
  recursive?: boolean;
  scan_type?: "code" | "config" | "all";
}): Promise<{ content: Array<{ type: string; text: string }> }> {
  const { source_dir, recursive = true, scan_type = "all" } = args;

  if (!fs.existsSync(source_dir)) {
    throw new Error(`Directory not found: ${source_dir}`);
  }

  const files = findRelevantFiles(source_dir, recursive, scan_type);

  if (files.length === 0) {
    throw new Error(`No files found to scan in ${source_dir}`);
  }

  const results: SecurityResult[] = [];

  for (const file of files) {
    const result = scanFileForVulnerabilities(file);
    if (result.issues.length > 0) {
      results.push(result);
    }
  }

  const totalCritical = results.reduce((sum, r) => sum + r.critical_count, 0);
  const totalHigh = results.reduce((sum, r) => sum + r.high_count, 0);

  const message = generateSecurityReport(results, totalCritical, totalHigh);

  return {
    content: [{ type: "text", text: message }],
  };
}

function findRelevantFiles(
  dir: string,
  recursive: boolean,
  scanType: "code" | "config" | "all"
): string[] {
  const files: string[] = [];
  const items = fs.readdirSync(dir);

  for (const item of items) {
    const fullPath = path.join(dir, item);
    const stat = fs.statSync(fullPath);

    if (stat.isDirectory() && recursive) {
      if (!item.match(/^(bin|obj|\.vs|\.git|node_modules)$/)) {
        files.push(...findRelevantFiles(fullPath, recursive, scanType));
      }
    } else if (stat.isFile()) {
      const ext = path.extname(item);
      const includeFile =
        (scanType === "all" && (ext === ".cs" || ext === ".sql" || ext === ".json" || ext === ".config")) ||
        (scanType === "code" && (ext === ".cs" || ext === ".sql")) ||
        (scanType === "config" && (ext === ".json" || ext === ".config"));

      if (includeFile && !item.includes(".Designer.cs")) {
        files.push(fullPath);
      }
    }
  }

  return files;
}

function scanFileForVulnerabilities(filePath: string): SecurityResult {
  const content = fs.readFileSync(filePath, "utf-8");
  const lines = content.split("\n");
  const issues: SecurityIssue[] = [];
  const fileName = path.basename(filePath);
  const ext = path.extname(filePath);

  if (ext === ".cs") {
    scanCSharpFile(lines, fileName, issues);
  } else if (ext === ".sql") {
    scanSqlFile(lines, fileName, issues);
  } else if (ext === ".json" || ext === ".config") {
    scanConfigFile(content, fileName, issues);
  }

  const criticalCount = issues.filter((i) => i.severity === "critical").length;
  const highCount = issues.filter((i) => i.severity === "high").length;
  const mediumCount = issues.filter((i) => i.severity === "medium").length;
  const lowCount = issues.filter((i) => i.severity === "low").length;

  const score = Math.max(0, 100 - criticalCount * 30 - highCount * 15 - mediumCount * 5 - lowCount * 1);

  return {
    file: fileName,
    issues,
    critical_count: criticalCount,
    high_count: highCount,
    medium_count: mediumCount,
    low_count: lowCount,
    security_score: score,
  };
}

function scanCSharpFile(
  lines: string[],
  fileName: string,
  issues: SecurityIssue[]
): void {
  const content = lines.join("\n");

  lines.forEach((line, idx) => {
    const lineNum = idx + 1;

    // SQL Injection
    if (/\$.*SELECT|INSERT|UPDATE|DELETE/.test(line) || /string\.Format.*SELECT|INSERT|UPDATE/.test(line)) {
      issues.push({
        file: fileName,
        line: lineNum,
        severity: "critical",
        category: "SQL Injection",
        vulnerability: "Potential SQL injection vulnerability",
        description: "SQL query built with string concatenation or interpolation",
        remediation: "Use parameterized queries or stored procedures",
        cwe: "CWE-89",
      });
    }

    // Hardcoded credentials
    if (/password\s*=\s*["'][^"']+["']/i.test(line) || /connectionString\s*=\s*["'].*password=/i.test(line)) {
      issues.push({
        file: fileName,
        line: lineNum,
        severity: "critical",
        category: "Hardcoded Credentials",
        vulnerability: "Hardcoded password or connection string",
        description: "Credentials should never be hardcoded in source code",
        remediation: "Use configuration files, environment variables, or secret management",
        cwe: "CWE-798",
      });
    }

    // Path traversal
    if (/Path\.Combine.*Request\[|Path\.Combine.*input|Path\.Combine.*user/.test(line)) {
      issues.push({
        file: fileName,
        line: lineNum,
        severity: "high",
        category: "Path Traversal",
        vulnerability: "Potential path traversal vulnerability",
        description: "User input used in file path without validation",
        remediation: "Validate and sanitize file paths, use Path.GetFullPath and check against allowed directories",
        cwe: "CWE-22",
      });
    }

    // Insecure deserialization
    if (/BinaryFormatter|ObjectStateFormatter/.test(line)) {
      issues.push({
        file: fileName,
        line: lineNum,
        severity: "high",
        category: "Insecure Deserialization",
        vulnerability: "Use of insecure deserialization",
        description: "BinaryFormatter and ObjectStateFormatter are vulnerable to deserialization attacks",
        remediation: "Use safer alternatives like System.Text.Json or DataContractSerializer",
        cwe: "CWE-502",
      });
    }

    // Weak cryptography
    if (/new MD5|new SHA1|DES|RC2/.test(line)) {
      issues.push({
        file: fileName,
        line: lineNum,
        severity: "medium",
        category: "Weak Cryptography",
        vulnerability: "Use of weak cryptographic algorithm",
        description: "MD5, SHA1, DES, and RC2 are considered weak",
        remediation: "Use SHA256, SHA384, or SHA512 for hashing; AES for encryption",
        cwe: "CWE-327",
      });
    }

    // Missing input validation
    if (/Request\[|Request\.QueryString|Request\.Form/.test(line)) {
      const nextLines = lines.slice(idx, idx + 5).join("\n");
      if (!/Validate|Sanitize|IsValid|TryParse/.test(nextLines)) {
        issues.push({
          file: fileName,
          line: lineNum,
          severity: "medium",
          category: "Input Validation",
          vulnerability: "Missing input validation",
          description: "User input accessed without apparent validation",
          remediation: "Validate all user input before use",
          cwe: "CWE-20",
        });
      }
    }

    // Command injection
    if (/Process\.Start|ProcessStartInfo/.test(line)) {
      if (/\+|string\.Format|\$/.test(line)) {
        issues.push({
          file: fileName,
          line: lineNum,
          severity: "critical",
          category: "Command Injection",
          vulnerability: "Potential command injection",
          description: "External command execution with dynamic input",
          remediation: "Avoid dynamic command construction, use ArgumentList property",
          cwe: "CWE-78",
        });
      }
    }

    // Information disclosure
    if (/throw\s+new\s+Exception\([^)]*\+|\.Message/.test(line)) {
      issues.push({
        file: fileName,
        line: lineNum,
        severity: "low",
        category: "Information Disclosure",
        vulnerability: "Potential information disclosure",
        description: "Exception messages may expose sensitive information",
        remediation: "Use generic error messages for users, log details server-side",
        cwe: "CWE-209",
      });
    }

    // Insecure random
    if (/new Random\(\)/.test(line)) {
      const context = lines.slice(Math.max(0, idx - 3), idx + 3).join("\n");
      if (/password|token|key|salt/i.test(context)) {
        issues.push({
          file: fileName,
          line: lineNum,
          severity: "medium",
          category: "Weak Random",
          vulnerability: "Use of non-cryptographic random generator",
          description: "System.Random is not suitable for security-sensitive operations",
          remediation: "Use RandomNumberGenerator for cryptographic purposes",
          cwe: "CWE-338",
        });
      }
    }
  });

  // Check for missing authentication/authorization
  if (/public\s+class.*Controller|public\s+class.*Api/.test(content)) {
    if (!/\[Authorize\]|\[AllowAnonymous\]/.test(content)) {
      issues.push({
        file: fileName,
        line: 1,
        severity: "high",
        category: "Missing Authorization",
        vulnerability: "No authorization attributes found on controller",
        description: "Controller may be publicly accessible without authentication",
        remediation: "Add [Authorize] or [AllowAnonymous] attributes as appropriate",
        cwe: "CWE-862",
      });
    }
  }
}

function scanSqlFile(
  lines: string[],
  fileName: string,
  issues: SecurityIssue[]
): void {
  const content = lines.join("\n");

  lines.forEach((line, idx) => {
    const lineNum = idx + 1;

    // SQL injection via CONCAT + EXECUTE
    if (/CONCAT.*EXECUTE|EXEC.*CONCAT/i.test(line)) {
      issues.push({
        file: fileName,
        line: lineNum,
        severity: "critical",
        category: "SQL Injection",
        vulnerability: "Dynamic SQL with CONCAT and EXECUTE",
        description: "Building SQL dynamically with CONCAT is vulnerable to injection",
        remediation: "Use parameterized stored procedures or validate input",
        cwe: "CWE-89",
      });
    }

    // Excessive permissions
    if (/GRANT ALL|GRANT .*\s+WITH GRANT OPTION/i.test(line)) {
      issues.push({
        file: fileName,
        line: lineNum,
        severity: "high",
        category: "Excessive Permissions",
        vulnerability: "Overly permissive GRANT statement",
        description: "Granting ALL or WITH GRANT OPTION violates least privilege",
        remediation: "Grant only specific permissions needed",
        cwe: "CWE-269",
      });
    }

    // Weak authentication
    if (/IDENTIFIED BY\s+["'][^"']{1,8}["']/i.test(line)) {
      issues.push({
        file: fileName,
        line: lineNum,
        severity: "medium",
        category: "Weak Password",
        vulnerability: "Weak or short password",
        description: "Password appears to be 8 characters or less",
        remediation: "Use strong passwords (12+ characters, mixed case, symbols)",
        cwe: "CWE-521",
      });
    }
  });
}

function scanConfigFile(
  content: string,
  fileName: string,
  issues: SecurityIssue[]
): void {
  // Parse JSON/XML config files
  try {
    if (fileName.endsWith(".json")) {
      const config = JSON.parse(content);

      // Check for hardcoded secrets
      const jsonString = JSON.stringify(config);
      if (/password|secret|apikey|token/i.test(jsonString)) {
        issues.push({
          file: fileName,
          line: 1,
          severity: "critical",
          category: "Hardcoded Secrets",
          vulnerability: "Configuration contains hardcoded secrets",
          description: "Secrets should not be stored in configuration files",
          remediation: "Use environment variables or secret management systems",
          cwe: "CWE-798",
        });
      }

      // Check for debug mode in production configs
      if (fileName.includes("production") || fileName.includes("prod")) {
        if (config.debug === true || config.Debug === true) {
          issues.push({
            file: fileName,
            line: 1,
            severity: "medium",
            category: "Debug Mode Enabled",
            vulnerability: "Debug mode enabled in production configuration",
            description: "Debug mode may expose sensitive information",
            remediation: "Disable debug mode in production",
            cwe: "CWE-489",
          });
        }
      }
    }
  } catch (e) {
    // Ignore parse errors
  }
}

function generateSecurityReport(
  results: SecurityResult[],
  totalCritical: number,
  totalHigh: number
): string {
  const avgScore =
    results.length > 0
      ? results.reduce((sum, r) => sum + r.security_score, 0) / results.length
      : 100;

  let report = `
## Security Vulnerability Scan Report

**Files Scanned:** ${results.length}
**Average Security Score:** ${avgScore.toFixed(1)}/100
**Critical Vulnerabilities:** ${totalCritical} 🔴
**High Severity:** ${totalHigh} 🟠

---

### Vulnerability Summary

| File | Score | Critical | High | Medium | Low |
|------|-------|----------|------|--------|-----|
${results
  .sort((a, b) => a.security_score - b.security_score)
  .slice(0, 10)
  .map(
    (r) =>
      `| ${r.file} | ${r.security_score}/100 | ${r.critical_count} | ${r.high_count} | ${r.medium_count} | ${r.low_count} |`
  )
  .join("\n")}

---

`;

  // Critical vulnerabilities
  const criticalFiles = results.filter((r) => r.critical_count > 0);
  if (criticalFiles.length > 0) {
    report += "### 🔴 Critical Security Issues\n\n";
    report += "**IMMEDIATE ACTION REQUIRED**\n\n";

    for (const result of criticalFiles.slice(0, 5)) {
      report += `**${result.file}** (Score: ${result.security_score}/100):\n\n`;

      const criticalIssues = result.issues.filter((i) => i.severity === "critical");
      for (const issue of criticalIssues) {
        report += `  🔴 **Line ${issue.line}** [${issue.category}]\n`;
        report += `     ⚠️ **Vulnerability:** ${issue.vulnerability}\n`;
        report += `     📝 **Description:** ${issue.description}\n`;
        report += `     🔧 **Fix:** ${issue.remediation}\n`;
        if (issue.cwe) {
          report += `     🔗 **CWE:** ${issue.cwe}\n`;
        }
        report += "\n";
      }
    }
  }

  // Security recommendations
  report += "\n### 🛡️ Security Recommendations\n\n";

  if (totalCritical > 0) {
    report += "1. **Address critical vulnerabilities immediately** - These pose significant security risks\n";
  }

  report += "2. **Implement security code reviews** - Regular reviews catch issues before deployment\n";
  report += "3. **Use static analysis tools** - Integrate into CI/CD pipeline\n";
  report += "4. **Follow secure coding guidelines** - Train developers on OWASP Top 10\n";
  report += "5. **Regular security audits** - Schedule periodic vulnerability scans\n";

  return report;
}
