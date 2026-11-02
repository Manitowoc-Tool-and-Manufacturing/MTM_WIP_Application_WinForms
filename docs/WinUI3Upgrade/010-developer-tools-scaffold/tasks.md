# Migration Task List (Execution Plan)

**Spec**: `010-developer-tools-scaffold`
**Range**: TASK-127 … TASK-129
**Definition of done for every task**: its `Verify` line passes, and `dotnet build MTM_WIP.sln -c Debug` plus `dotnet test MTM_WIP.Tests` are both clean.

> **Scope note: this module is scaffolded only in v1.** The three tasks build inert, navigable surfaces and then validate the pack. No behaviour is implemented. If a task appears to need a DAO or a service, stop — that is out of scope, not an implementation detail.

---

## Phase 1 — Developer Tools page shell

- [ ] **TASK-127** — Build the Developer Tools page shell in `MTM_WIP.Module_DeveloperTools`: the host page `View_DeveloperToolsPage` with four destination tabs (Dashboard, Logs, Feedback, System Info) plus the four destination pages `View_DeveloperDashboardPage`, `View_DeveloperLogsPage`, `View_DeveloperFeedbackPage`, and `View_DeveloperSystemInfoPage`. Register the host with the shell's navigation service (contract from `004`; API **(to confirm)** until `004` is written). Every destination renders the shared unavailable-state surface — a heading naming the destination and the message "This screen is not available in this release." — and nothing else. No ViewModel injects any service; the four page ViewModels expose only the not-available title/message pair.
  - **Verify**: `dotnet build MTM_WIP.sln -c Debug` succeeds; from the shell, Developer Tools is reachable and each of the four tabs shows the not-available state; `Get-ChildItem MTM_WIP.Module_DeveloperTools -Recurse -Include *.cs,*.xaml | Select-String -Pattern 'IDao_','IErrorService','IDialogService','ICurrentUserService'` returns no matches.

## Phase 2 — Database Maintenance page shell

- [ ] **TASK-128** — Build `View_DatabaseMaintenancePage` and its `ViewModel_DatabaseMaintenancePage` with no reachable destructive path. The page renders the shared unavailable-state surface plus one additional sentence stating that no maintenance actions are available in this release. There is no button, command, or handler that could invoke a migration, truncate, delete, optimize, backup, or factory reset.
  - **Verify**: `dotnet build MTM_WIP.sln -c Debug` succeeds; the page navigates and shows only the not-available surface; `Get-ChildItem MTM_WIP.Module_DeveloperTools -Recurse -Include *.cs,*.xaml | Select-String -Pattern 'TRUNCATE','DELETE','OPTIMIZE','backup','factory','IDao_'` returns no matches (no destructive call and no DAO is reachable from the UI).

## Phase 3 — Downstream validation and pack close

- [ ] **TASK-129** — **Downstream validation: `011-deployment-and-cutover` (and a whole-pack close).** Re-read [`../011-deployment-and-cutover/spec.md`](../011-deployment-and-cutover/spec.md), [`../011-deployment-and-cutover/plan.md`](../011-deployment-and-cutover/plan.md) and [`../011-deployment-and-cutover/tasks.md`](../011-deployment-and-cutover/tasks.md), plus the pack index [`../00-index.md`](../00-index.md). Confirm the downstream assumptions in this spec's §4 still hold, record each as *confirmed* or *amended* in the Drift log below, and append any `011`-owned drift rows the pack requires.
  - **Verify**: the Drift log below has one row per assumption, each marked *confirmed* or *amended* (or *pending* with a stated reason), and `dotnet test MTM_WIP.Tests` is green.

---

## Drift log

Filled in by TASK-129. One row per downstream assumption checked.

| Checked | Item | Result | Action taken |
| :--- | :--- | :--- | :--- |
| `011-deployment-and-cutover` | A1 — spec/plan/tasks triplet exists; sole owner of publishing | *(pending)* | — |
| `004-shell-and-startup` | A2 — navigation registration contract exists and matches | *(pending)* | — |
| `011-deployment-and-cutover` | A3 — untrimmed publish ships the scaffold module | *(pending)* | — |
| pack (`00-index.md`, D15) | A4 — no downstream spec adds Developer Tools behaviour in v1 | *(pending)* | — |
| `002-data-access-layer` | A5 — database remains frozen; no new procedure/table | *(pending)* | — |
| `009-help-and-feedback` | A6 — feedback/error-report/help behaviour owned by `009` | *(pending)* | — |
