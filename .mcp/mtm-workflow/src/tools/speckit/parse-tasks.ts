import * as fs from "fs";
import * as path from "path";

interface Task {
  id: string;
  phase: string;
  part?: string;
  title: string;
  description: string;
  completed: boolean;
  references: string[];
  dependencies: string[];
  parallelizable: boolean;
  filePaths: string[];
  notes: string[];
}

interface TaskPhase {
  phase: string;
  parts: {
    [partName: string]: Task[];
  };
}

interface ParsedTasks {
  phases: TaskPhase[];
  allTasks: Task[];
  incompleteTasks: Task[];
  nextTasks: Task[];
}

export async function parseTasks(args: {
  tasks_file: string;
}): Promise<{ content: Array<{ type: string; text: string }> }> {
  const { tasks_file } = args;

  if (!fs.existsSync(tasks_file)) {
    throw new Error(`Tasks file not found: ${tasks_file}`);
  }

  const content = fs.readFileSync(tasks_file, "utf-8");
  const lines = content.split("\n");

  const tasks: Task[] = [];
  let currentPhase = "";
  let currentPart = "";
  let currentTask: Task | null = null;

  for (let i = 0; i < lines.length; i++) {
    const line = lines[i];

    // Detect phase headers (## Phase X)
    const phaseMatch = line.match(/^##\s+(Phase\s+[\d.]+.*)/i);
    if (phaseMatch) {
      currentPhase = phaseMatch[1].trim();
      currentPart = "";
      continue;
    }

    // Detect part headers (### Part A)
    const partMatch = line.match(/^###\s+(Part\s+[A-Z].*)/i);
    if (partMatch) {
      currentPart = partMatch[1].trim();
      continue;
    }

    // Detect task items (- [x] **T100** – Description)
    const taskMatch = line.match(
      /^-\s+\[([ xX])\]\s+\*\*([T\d]+[a-z]?)\*\*\s+[–—-]\s+(.*)/
    );
    if (taskMatch) {
      const completed = taskMatch[1].toLowerCase() === "x";
      const taskId = taskMatch[2];
      const description = taskMatch[3].trim();

      currentTask = {
        id: taskId,
        phase: currentPhase,
        part: currentPart,
        title: description,
        description: description,
        completed,
        references: [],
        dependencies: [],
        parallelizable: description.includes("[P]"),
        filePaths: [],
        notes: [],
      };

      tasks.push(currentTask);
      continue;
    }

    // Collect additional task context (indented lines)
    if (currentTask && line.match(/^\s{2,}/)) {
      const trimmed = line.trim();

      // Extract references
      const refMatch = trimmed.match(
        /\*\*Reference\*\*:\s+`?([^`]+)`?/i
      );
      if (refMatch) {
        currentTask.references.push(refMatch[1].trim());
      }

      // Extract file paths
      const fileMatch = trimmed.match(/`([^`]+\.(?:cs|ts|md|sql))`/g);
      if (fileMatch) {
        currentTask.filePaths.push(
          ...fileMatch.map((m) => m.replace(/`/g, ""))
        );
      }

      // Collect progress notes
      if (
        trimmed.startsWith("-") &&
        (trimmed.includes("Progress:") ||
          trimmed.includes("Completion:") ||
          trimmed.includes("2025-"))
      ) {
        currentTask.notes.push(trimmed);
      }
    }
  }

  // Group tasks by phase
  const phaseMap = new Map<string, TaskPhase>();
  for (const task of tasks) {
    if (!phaseMap.has(task.phase)) {
      phaseMap.set(task.phase, {
        phase: task.phase,
        parts: {},
      });
    }

    const phaseData = phaseMap.get(task.phase)!;
    const partKey = task.part || "default";

    if (!phaseData.parts[partKey]) {
      phaseData.parts[partKey] = [];
    }

    phaseData.parts[partKey].push(task);
  }

  const phases = Array.from(phaseMap.values());
  const incompleteTasks = tasks.filter((t) => !t.completed);

  // Determine next actionable tasks (incomplete with no incomplete dependencies)
  const nextTasks = incompleteTasks.filter((task, idx) => {
    // First incomplete task in sequence
    if (idx === 0) return true;

    // Check if all previous tasks in same phase/part are complete
    const previousTasks = tasks.slice(0, tasks.indexOf(task));
    const samePhasePartTasks = previousTasks.filter(
      (t) => t.phase === task.phase && t.part === task.part
    );

    return samePhasePartTasks.every((t) => t.completed);
  });

  const summary = `
## Task Parsing Results

**Total Tasks**: ${tasks.length}
**Completed**: ${tasks.filter((t) => t.completed).length}
**Incomplete**: ${incompleteTasks.length}
**Next Actionable**: ${nextTasks.length}

### Phases Breakdown

${phases
  .map((p) => {
    const phaseTasks = Object.values(p.parts).flat();
    const completed = phaseTasks.filter((t) => t.completed).length;
    return `**${p.phase}**: ${completed}/${phaseTasks.length} complete`;
  })
  .join("\n")}

### Next Tasks to Execute

${nextTasks
  .slice(0, 5)
  .map(
    (t) =>
      `- **${t.id}**: ${t.title}${
        t.references.length > 0
          ? `\n  - References: ${t.references.join(", ")}`
          : ""
      }`
  )
  .join("\n")}

${
  nextTasks.length > 5
    ? `\n... and ${nextTasks.length - 5} more tasks`
    : ""
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
