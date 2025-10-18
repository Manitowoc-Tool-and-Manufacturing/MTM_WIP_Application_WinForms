import * as fs from "fs";
import * as path from "path";

interface TaskUpdate {
  taskId: string;
  oldStatus: "incomplete" | "complete";
  newStatus: "incomplete" | "complete";
  timestamp: string;
  note?: string;
}

interface MarkResult {
  success: boolean;
  updates: TaskUpdate[];
  errors: string[];
  updatedFile: string;
}

export async function markTaskComplete(args: {
  tasks_file: string;
  task_ids: string[];
  note?: string;
}): Promise<{ content: Array<{ type: string; text: string }> }> {
  const { tasks_file, task_ids, note } = args;

  if (!fs.existsSync(tasks_file)) {
    throw new Error(`Tasks file not found: ${tasks_file}`);
  }

  const content = fs.readFileSync(tasks_file, "utf-8");
  const lines = content.split("\n");
  const updates: TaskUpdate[] = [];
  const errors: string[] = [];
  const timestamp = new Date().toISOString().split("T")[0]; // YYYY-MM-DD

  let modified = false;

  for (let i = 0; i < lines.length; i++) {
    const line = lines[i];

    // Match task lines: - [ ] **T100** – Description
    const taskMatch = line.match(
      /^(-\s+\[)([ xX])(\]\s+\*\*([T\d]+[a-z]?)\*\*\s+[–—-]\s+.*)/
    );

    if (taskMatch) {
      const taskId = taskMatch[4];

      if (task_ids.includes(taskId)) {
        const currentStatus = taskMatch[2].toLowerCase();
        const wasComplete = currentStatus === "x";

        if (!wasComplete) {
          // Mark as complete
          lines[i] = `${taskMatch[1]}X${taskMatch[3]}`;

          // Add note if provided
          if (note) {
            // Check if next line is indented (part of task details)
            if (
              i + 1 < lines.length &&
              lines[i + 1].match(/^\s{2,}/) &&
              !lines[i + 1].includes("**Reference**")
            ) {
              // Insert note after task line
              lines.splice(
                i + 1,
                0,
                `  - **Completed**: ${timestamp} - ${note}`
              );
            } else {
              // Insert note as new indented line
              lines.splice(
                i + 1,
                0,
                `  - **Completed**: ${timestamp} - ${note}`
              );
            }
          }

          updates.push({
            taskId,
            oldStatus: "incomplete",
            newStatus: "complete",
            timestamp,
            note,
          });

          modified = true;
        } else {
          updates.push({
            taskId,
            oldStatus: "complete",
            newStatus: "complete",
            timestamp,
            note: "Already complete",
          });
        }
      }
    }
  }

  // Check for missing task IDs
  const foundIds = updates.map((u) => u.taskId);
  const missingIds = task_ids.filter((id) => !foundIds.includes(id));
  if (missingIds.length > 0) {
    errors.push(`Tasks not found: ${missingIds.join(", ")}`);
  }

  // Write back to file if modified
  if (modified) {
    fs.writeFileSync(tasks_file, lines.join("\n"), "utf-8");
  }

  const summary = `
## Mark Tasks Complete

**Tasks File**: \`${path.basename(tasks_file)}\`
**Timestamp**: ${timestamp}

### Updates Applied

${updates
  .map((u) => {
    const statusChange =
      u.oldStatus === "incomplete" ? "✅ Marked complete" : "ℹ️ Already complete";
    return `**${u.taskId}**: ${statusChange}${u.note ? `\n  Note: ${u.note}` : ""}`;
  })
  .join("\n\n")}

${errors.length > 0 ? `\n### ⚠️ Errors\n\n${errors.map((e) => `- ${e}`).join("\n")}\n` : ""}

**Result**: ${modified ? `✅ ${updates.filter((u) => u.oldStatus === "incomplete").length} tasks marked complete` : "ℹ️ No changes made"}
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
