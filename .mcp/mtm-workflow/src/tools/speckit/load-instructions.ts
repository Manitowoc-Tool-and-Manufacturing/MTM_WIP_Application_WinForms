import * as fs from "fs";
import * as path from "path";

interface InstructionFileRef {
  taskId: string;
  taskTitle: string;
  instructionFiles: string[];
  fileExists: boolean[];
  allFilesExist: boolean;
}

interface InstructionFileScan {
  totalReferences: number;
  uniqueInstructionFiles: string[];
  missingFiles: string[];
  taskReferences: InstructionFileRef[];
}

export async function loadInstructions(args: {
  tasks_file: string;
  instructions_dir: string;
}): Promise<{ content: Array<{ type: string; text: string }> }> {
  const { tasks_file, instructions_dir } = args;

  if (!fs.existsSync(tasks_file)) {
    throw new Error(`Tasks file not found: ${tasks_file}`);
  }

  if (!fs.existsSync(instructions_dir)) {
    throw new Error(`Instructions directory not found: ${instructions_dir}`);
  }

  const tasksContent = fs.readFileSync(tasks_file, "utf-8");
  const lines = tasksContent.split("\n");

  const references: InstructionFileRef[] = [];
  const allInstructionFiles = new Set<string>();
  let currentTaskId = "";
  let currentTaskTitle = "";

  for (let i = 0; i < lines.length; i++) {
    const line = lines[i];

    // Detect task items - more flexible pattern to match various formats
    // Matches: - [ ] **T001** - Title
    //          - [x] **T001** [Story: US1] - Title
    //          - [X] **T001** [Story: US1] [P] - Title
    const taskMatch = line.match(
      /^-\s+\[([ xX])\]\s+\*\*([T\d]+[a-z]?)\*\*/
    );
    if (taskMatch) {
      currentTaskId = taskMatch[2];
      // Extract title - everything after the task ID and optional tags
      const titleMatch = line.match(/\*\*[T\d]+[a-z]?\*\*.*?[-–—]\s+(.*)/);
      currentTaskTitle = titleMatch ? titleMatch[1].trim() : "";
      continue;
    }

    // Detect reference lines - support multiple formats
    // Matches: **Reference**: `.github/instructions/file.md` - Description
    //          **Reference**: .github/instructions/file.md
    //          **Reference**: `.github/instructions/file.md`
    const refMatch = line.match(
      /\*\*Reference\*\*:\s+`?\.github\/instructions\/([a-zA-Z0-9_-]+\.instructions\.md)`?/i
    );
    if (refMatch && currentTaskId) {
      const instructionFile = refMatch[1];
      const fullPath = path.join(instructions_dir, instructionFile);
      const exists = fs.existsSync(fullPath);

      allInstructionFiles.add(instructionFile);

      // Check if this task already has references
      let taskRef = references.find((r) => r.taskId === currentTaskId);
      if (!taskRef) {
        taskRef = {
          taskId: currentTaskId,
          taskTitle: currentTaskTitle,
          instructionFiles: [],
          fileExists: [],
          allFilesExist: true,
        };
        references.push(taskRef);
      }

      taskRef.instructionFiles.push(instructionFile);
      taskRef.fileExists.push(exists);
      taskRef.allFilesExist = taskRef.allFilesExist && exists;
    }
  }

  // Check which files are missing
  const missingFiles: string[] = [];
  for (const file of allInstructionFiles) {
    const fullPath = path.join(instructions_dir, file);
    if (!fs.existsSync(fullPath)) {
      missingFiles.push(file);
    }
  }

  // Load content of existing instruction files
  const instructionContents = new Map<string, string>();
  for (const file of allInstructionFiles) {
    const fullPath = path.join(instructions_dir, file);
    if (fs.existsSync(fullPath)) {
      instructionContents.set(file, fs.readFileSync(fullPath, "utf-8"));
    }
  }

  const summary = `
## Instruction Files Analysis

**Total References**: ${references.length}
**Unique Instruction Files**: ${allInstructionFiles.size}
**Missing Files**: ${missingFiles.length}

### Referenced Instruction Files

${Array.from(allInstructionFiles)
  .map((f) => {
    const fullPath = path.join(instructions_dir, f);
    const exists = fs.existsSync(fullPath);
    const status = exists ? "✅" : "❌";
    return `${status} \`${f}\``;
  })
  .join("\n")}

${
  missingFiles.length > 0
    ? `\n### ⚠️ Missing Instruction Files\n\n${missingFiles
        .map((f) => `- \`${f}\``)
        .join("\n")}\n`
    : ""
}

### Tasks with Instruction References

${references
  .map(
    (r) =>
      `**${r.taskId}**: ${r.taskTitle}\n${r.instructionFiles
        .map((f, idx) => {
          const status = r.fileExists[idx] ? "✅" : "❌";
          return `  ${status} \`${f}\``;
        })
        .join("\n")}`
  )
  .join("\n\n")}

### Instruction File Contents Loaded

${Array.from(instructionContents.keys())
  .map((file) => {
    const content = instructionContents.get(file)!;
    const lines = content.split("\n").length;
    const sizeKb = (Buffer.byteLength(content, "utf-8") / 1024).toFixed(1);
    return `- \`${file}\`: ${lines} lines (${sizeKb} KB)`;
  })
  .join("\n")}

${
  missingFiles.length > 0
    ? "\n⚠️ **Warning**: Some instruction files are missing. Create them before proceeding with implementation."
    : "\n✅ **Success**: All referenced instruction files are available."
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
