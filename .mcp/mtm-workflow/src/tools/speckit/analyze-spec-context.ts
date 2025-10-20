import * as fs from "fs";
import * as path from "path";

interface SpecDoc {
  name: string;
  path: string;
  exists: boolean;
  size: number;
  lastModified: Date;
  sections: string[];
}

interface SpecContextAnalysis {
  featureDir: string;
  availableDocs: SpecDoc[];
  missingDocs: string[];
  techStack: string[];
  entities: string[];
  contracts: string[];
  recommendations: string[];
}

export async function analyzeSpecContext(args: {
  feature_dir: string;
}): Promise<{ content: Array<{ type: string; text: string }> }> {
  const { feature_dir } = args;

  if (!fs.existsSync(feature_dir)) {
    throw new Error(`Feature directory not found: ${feature_dir}`);
  }

  const docs: SpecDoc[] = [];
  const missingDocs: string[] = [];

  // Standard spec files to check
  const standardFiles = [
    "spec.md",
    "plan.md",
    "tasks.md",
    "data-model.md",
    "research.md",
    "quickstart.md",
  ];

  for (const file of standardFiles) {
    const filePath = path.join(feature_dir, file);
    const exists = fs.existsSync(filePath);

    if (exists) {
      const stats = fs.statSync(filePath);
      const content = fs.readFileSync(filePath, "utf-8");
      const sections = extractSections(content);

      docs.push({
        name: file,
        path: filePath,
        exists: true,
        size: stats.size,
        lastModified: stats.mtime,
        sections,
      });
    } else {
      missingDocs.push(file);
    }
  }

  // Check for contracts directory
  const contractsDir = path.join(feature_dir, "contracts");
  if (fs.existsSync(contractsDir)) {
    const contractFiles = fs
      .readdirSync(contractsDir)
      .filter((f) => f.endsWith(".json"));

    docs.push({
      name: "contracts/",
      path: contractsDir,
      exists: true,
      size: 0,
      lastModified: fs.statSync(contractsDir).mtime,
      sections: contractFiles,
    });
  }

  // Check for checklists directory
  const checklistsDir = path.join(feature_dir, "checklists");
  if (fs.existsSync(checklistsDir)) {
    const checklistFiles = fs
      .readdirSync(checklistsDir)
      .filter((f) => f.endsWith(".md"));

    docs.push({
      name: "checklists/",
      path: checklistsDir,
      exists: true,
      size: 0,
      lastModified: fs.statSync(checklistsDir).mtime,
      sections: checklistFiles,
    });
  }

  // Extract tech stack from plan.md
  const techStack = extractTechStack(docs);

  // Extract entities from data-model.md
  const entities = extractEntities(docs);

  // Extract contract types
  const contracts = extractContracts(docs);

  // Generate recommendations
  const recommendations = generateRecommendations(docs, missingDocs);

  const summary = `
## Spec Context Analysis

**Feature Directory**: \`${path.basename(feature_dir)}\`

### Available Documentation

${docs
  .map((doc) => {
    const sizeKb = (doc.size / 1024).toFixed(1);
    const lastMod = doc.lastModified.toISOString().split("T")[0];

    let docInfo = `✅ **${doc.name}**`;
    if (doc.size > 0) {
      docInfo += ` (${sizeKb} KB, updated ${lastMod})`;
    }

    if (doc.sections.length > 0) {
      if (doc.name.endsWith("/")) {
        docInfo += `\n  - Files: ${doc.sections.slice(0, 5).join(", ")}${
          doc.sections.length > 5 ? ` (+${doc.sections.length - 5} more)` : ""
        }`;
      } else {
        docInfo += `\n  - Sections: ${doc.sections.slice(0, 5).join(", ")}${
          doc.sections.length > 5 ? ` (+${doc.sections.length - 5} more)` : ""
        }`;
      }
    }

    return docInfo;
  })
  .join("\n\n")}

${
  missingDocs.length > 0
    ? `\n### Missing Documentation\n\n${missingDocs.map((d) => `❌ ${d}`).join("\n")}\n`
    : ""
}

### Extracted Context

**Tech Stack**: ${techStack.length > 0 ? techStack.join(", ") : "Not specified"}

**Entities**: ${entities.length > 0 ? entities.join(", ") : "Not specified"}

**Contracts**: ${contracts.length > 0 ? contracts.join(", ") : "Not specified"}

### Recommendations

${recommendations.map((r) => `- ${r}`).join("\n")}

### Summary

- **Documentation Coverage**: ${docs.length}/${standardFiles.length + 2} standard files
- **Total Size**: ${(docs.reduce((sum, d) => sum + d.size, 0) / 1024).toFixed(1)} KB
- **Ready for Implementation**: ${
    missingDocs.length === 0 || (docs.length >= 3 && docs.some((d) => d.name === "tasks.md"))
      ? "✅ Yes"
      : "⚠️ Review missing docs"
  }
`;

  return {
    content: [
      {
        type: "text",
        text: summary,
      },
    ],
  };
}

function extractSections(content: string): string[] {
  const lines = content.split("\n");
  const sections: string[] = [];

  for (const line of lines) {
    const match = line.match(/^#{1,4}\s+(.+)/);
    if (match) {
      sections.push(match[1].trim());
    }
  }

  return sections;
}

function extractTechStack(docs: SpecDoc[]): string[] {
  const planDoc = docs.find((d) => d.name === "plan.md");
  if (!planDoc) return [];

  const content = fs.readFileSync(planDoc.path, "utf-8");
  const techStack: string[] = [];

  // Look for common patterns
  if (content.match(/\b\.NET\s+8\b/i)) techStack.push(".NET 8");
  if (content.match(/\bC#\s+12\b/i)) techStack.push("C# 12");
  if (content.match(/\bMySQL\b/i)) techStack.push("MySQL");
  if (content.match(/\bWinForms\b/i)) techStack.push("WinForms");
  if (content.match(/\bNode\.js\b/i)) techStack.push("Node.js");
  if (content.match(/\bTypeScript\b/i)) techStack.push("TypeScript");
  if (content.match(/\bReact\b/i)) techStack.push("React");

  return techStack;
}

function extractEntities(docs: SpecDoc[]): string[] {
  const dataModelDoc = docs.find((d) => d.name === "data-model.md");
  if (!dataModelDoc) return [];

  const content = fs.readFileSync(dataModelDoc.path, "utf-8");
  const entities: string[] = [];

  // Look for entity definitions (### EntityName pattern)
  const matches = content.matchAll(/^###\s+([A-Z][a-zA-Z_]+)\s*\(/gm);
  for (const match of matches) {
    entities.push(match[1]);
  }

  return entities;
}

function extractContracts(docs: SpecDoc[]): string[] {
  const contractsDoc = docs.find((d) => d.name === "contracts/");
  if (!contractsDoc || contractsDoc.sections.length === 0) return [];

  return contractsDoc.sections.map((f) => path.basename(f, ".json"));
}

function generateRecommendations(
  docs: SpecDoc[],
  missingDocs: string[]
): string[] {
  const recommendations: string[] = [];

  if (!docs.some((d) => d.name === "tasks.md")) {
    recommendations.push("❌ Critical: tasks.md is missing. Run /tasks to generate.");
  }

  if (!docs.some((d) => d.name === "plan.md")) {
    recommendations.push("⚠️ plan.md is missing. Technical architecture undefined.");
  }

  if (!docs.some((d) => d.name === "data-model.md")) {
    recommendations.push(
      "ℹ️ data-model.md is missing. Entity definitions may be inline in plan.md."
    );
  }

  if (!docs.some((d) => d.name === "contracts/")) {
    recommendations.push(
      "ℹ️ contracts/ directory is missing. API contracts undefined."
    );
  }

  if (recommendations.length === 0) {
    recommendations.push("✅ All critical documentation is present.");
    recommendations.push("✅ Ready to proceed with implementation.");
  }

  return recommendations;
}
