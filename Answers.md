Now I have enough context. Let me ask clarification questions before updating the files:

Clarification Questions
Based on my analysis of all speckit prompt files, template files, and MCP tools, I need clarification on the following before making updates:

1. MCP Tool Documentation Placement
The MCP tools are well-documented in speckit.implement.prompt.md but not in other prompt files. Should I:

A) Add full MCP tool documentation to ALL prompt files that could benefit (constitution, plan, specify, clarify, checklist, analyze, tasks)?

B) Add only RELEVANT MCP tools to each prompt file based on its specific workflow (e.g., check_checklists only in checklist and analyze prompts)?

C) Add a brief reference section pointing to speckit.implement.prompt.md for full MCP documentation, then list only the 2-3 most relevant tools per prompt?

ANSWER: OPTION B

Why it matters: Option A is comprehensive but adds significant length to each file. Option B is targeted but requires careful selection. Option C keeps files lean but requires cross-referencing.

2. Instruction File References
I notice that:

tasks-template.md now has instruction file references
speckit.tasks.prompt.md mentions adding instruction file references
Other templates (spec-template, plan-template, checklist-template) do NOT have instruction file reference sections
Should I:

A) Add instruction file reference sections to spec-template.md and plan-template.md (e.g., "Relevant Instruction Files: mysql-database.instructions.md, csharp-dotnet8.instructions.md")?

B) Keep instruction file references ONLY in tasks.md (since that's where implementation happens)?

C) Add instruction file reference guidance to the PROMPTS but not the templates?

ANSWER: OPTION A

Why it matters: Adding references to spec/plan templates could help with consistency but might clutter high-level design docs with implementation details.

3. Premium Request Maximization
The "maximize premium request value" guidance is now in:

copilot-instructions.md (agent communication rules)
speckit.implement.prompt.md (implementation workflow)
tasks-template.md (task completion tracking)
Should I add this guidance to:

A) ALL speckit prompt files (constitution, plan, specify, clarify, checklist, analyze, tasks) with context-specific wording?

B) Only IMPLEMENTATION-focused prompts (implement, tasks, analyze) where multi-task work is expected?

C) Keep it centralized in copilot-instructions.md and just reference it in other prompts?

ANSWER: OPTION A

Why it matters: Option A ensures agents see it regardless of entry point. Option B targets where it's most relevant. Option C avoids repetition but may be overlooked.

4. Task Completion Note Format
tasks-template.md now includes detailed completion note formats for partial and full completion. Should I:

A) Add this same format guidance to speckit.tasks.prompt.md (which generates tasks.md)?

B) Add it to speckit.implement.prompt.md (which executes tasks)?

C) Add it to BOTH prompts for consistency?

ANSWER: OPTION B

Why it matters: The prompt that generates tasks.md should probably include instructions about the completion format, and the prompt that implements should know how to use it.

5. Missing Sections Priority
I've identified these missing sections across files:

High Priority:

MCP tool references in relevant prompts
Task completion tracking in speckit.tasks.prompt.md
Instruction file integration in speckit.specify.prompt.md and speckit.plan.prompt.md
Medium Priority:

Premium request maximization in other prompts
Checklist validation integration in speckit.analyze.prompt.md
Agent file template updates (agent-file-template.md, feature-request-template.md)
Low Priority:

Constitution amendment tracking
Complexity tracking enhancements
Which priority level should I target? Should I:

A) Update ALL missing sections (comprehensive approach, longer session)?

B) Update only High Priority items (focused approach, complete sooner)?

C) Update High + Medium Priority (balanced approach)?

ANSWER: OPTION A

Why it matters: This determines scope and time required for the update session.

6. Table Formatting Standards
speckit.clarify.prompt.md has explicit table formatting rules. Should I:

A) Add similar table formatting standards to ALL prompts that generate markdown tables (specify, checklist, analyze, tasks)?

B) Create a shared "Markdown Formatting Standards" section that all prompts reference?

C) Keep table formatting rules only where explicitly needed (clarify prompt)?

ANSWER: A and Other

Other: In the clarification prompt instead of posting the prompts to the chat window do the following:

1) change the amount of questions when relevant to do so to be more than 5 (max of 15)
2) no long ask questions in the chat window instead:
    2a) Create an HTML File
    2b) Each question MUST be formatted in a way that a user / non-programmer / 8th grade reading level can comprehend it
    2c) Include your suggested answer by having that answer selected by default
    2d) include a textbox at the bottom of each question where the reader can enter there own answer
    2e) include a "Save to Clipboard" button that only activates when ALL questions have answers
    2f) add an Other option (5th option) to each question for the event where teh user wants to enter something in the textbox but not use any of the choices provided
    2g) do not use tech-jargin in your questions OR answers, instead when the user clicks on the "Save to Clipboard" button the data get copied to the clipboard will be "AGENT READY Questions with AGENT READY Answers (so the agent understands)"
    2h) create a new template in the .specify/templates folder for both the clarifcation html file and the AGENT READY answers.

Why it matters: Consistent table formatting across all generated files improves readability and prevents rendering issues.

Please respond with your choices (e.g., "1B, 2A, 3B, 4C, 5B, 6A") and I'll proceed with the updates accordingly. Or provide custom guidance for any question.

