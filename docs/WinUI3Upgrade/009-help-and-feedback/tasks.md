# Migration Task List (Execution Plan)

**Spec**: `009-help-and-feedback`
**Range**: TASK-120 … TASK-126
**Definition of done for every task**: its `Verify` line passes, and `dotnet build MTM_WIP.sln -c Debug` plus `dotnet test MTM_WIP.Tests` are both clean.

> **Ordering is load-bearing.** The loader and search (TASK-120, TASK-121) are pure and testable before any view exists. The surface (TASK-122) builds on them; the topic host (TASK-123) fills the body slot. Feedback and mapping (TASK-124) depend on the `002` DAOs being registered by `004`'s composition root. The content-coverage mechanism (TASK-125) closes the per-feature loop. TASK-126 closes the spec against `010` and `011`.

---

## Phase 1 — Content and search

- [ ] **TASK-120** — Implement `Service_HelpContentLoader` and `Service_HelpSystem` (`IHelpSystem`) in `MTM_WIP.Shared/Help` per [`plan.md`](./plan.md) §2.1: read every `*.json` from the injected content directory, deserialise into `Model_HelpCategory`, per-file `try/catch` that logs and continues, missing/empty directory → empty list; `GetCategories()` ordered by `Order`, `GetCategory(id)` case-insensitive; and the three model classes field-for-field identical to the legacy shape.
  - **Verify**: a test reads the real 15 files and asserts 15 categories, 54 topics and 15 distinct `CategoryId` values; a test on a nonexistent directory returns an empty list without throwing; a test drops one malformed file into a scratch content set and asserts the remaining files still load.

- [ ] **TASK-121** — Implement `Search` with the exact legacy scoring per [`spec.md`](./spec.md) §3.2: split on a single space with `RemoveEmptyEntries`, lower-invariant both sides; per term `+10` title, `+8` keywords (field scored once per term), `+5` summary, `+1` content; one increment per term per field, sum across terms; keep `score > 0`; order descending by `RelevanceScore` with **no tie-break**; return `Model_HelpSearchResult`.
  - **Verify**: a table-driven test asserts every row of the [`plan.md`](./plan.md) §2.2 matrix, including the tag-name content match, the multi-term sum, the no-tie-break ordering, and that an empty query is not a search.

---

## Phase 2 — Native surface and topic host

- [ ] **TASK-122** — Build the native help surface in `MTM_WIP.Module_Shell`: `View_HelpHome` (category cards in `Order` order, persistent search box), `View_HelpTopic` (title, summary, back/previous/next navigation, the "Was this helpful?" widget, and the body slot filled by the TASK-123 host), and `View_HelpSearchResults` (query box, relevance-ordered results). Search input debounces at 300 ms; an empty box reloads the index. Wire navigation through `004`'s `INavigationService.NavigateTo<TViewModel>()`.
  - **Verify**: manual smoke in `Light` and `Dark` at 100 % and 150 % scaling reaches every one of the 54 topics from the index and from search; `Get-ChildItem MTM_WIP.Module_Shell,MTM_WIP.Shared -Recurse -Include *.cs,*.xaml | Select-String -Pattern '\{\{component:','chrome.webview','postMessage'` returns no matches.

- [ ] **TASK-123** — Implement the WebView2 topic host and the CSS shell per [`plan.md`](./plan.md) §2.4: one WebView2 in `View_HelpTopic` rendering only the topic body in an in-memory wrapper; explicit user-data folder `%LOCALAPPDATA%\MTM\WIP\WebView2`; Content-Security-Policy on the host document; the single static `topic-shell.css` (typography, tables, code blocks, alert variants, watermark) inlined into the wrapper; runtime detection with a plain-message fallback; add `Microsoft.Web.WebView2` to the solution. No temp file, no network call, no template engine.
  - **Verify**: a tag/class sweep over the 54 topics confirms the CSS shell styles every tag and alert/code-block class used and every topic renders with formatting preserved; the runtime-absent path shows the plain message while index, search, navigation and feedback still work; grep asserts exactly one WebView2 in the help module, a configured user-data folder, and no `%TEMP%` write, no network call and no surviving HTML template file.

---

## Phase 3 — Feedback flows, mapping and content coverage

- [ ] **TASK-124** — Implement the six feedback flows in `Service_FeedbackManager` (`MTM_WIP.Module_Shell/Services`) per [`spec.md`](./spec.md) §3.5 and [`plan.md`](./plan.md) §2.5: helpfulness → `HelpSystem`; the four report forms discriminated by `bugCategory`/`suggestionCategory`/`inconsistencyType`/`questionCategory`; anything else → `General`. Resolve `UserID` via `sys_user_GetIdByName`, add the C#-added fields, HTML-sanitise free text, insert via `md_feedback_Insert`, and surface the returned `TrackingNumber`. Build the submissions view with comments (`md_feedback_GetByUser`, `md_feedback_comment_Insert`, `md_feedback_comment_GetByFeedbackId`). Implement the window/control friendly-name mapping in the bug form: load `sys_windowform_mapping_GetAll` into the window picker and `sys_usercontrol_mapping_GetByWindow` into the control picker; resolve the current page to its friendly name, falling back to the codebase name when no row exists.
  - **Verify**: integration tests against `mtm_wip_application_winforms_test` in a rolled-back transaction submit one of each of the six shapes and assert `IsSuccess`, a non-empty `TrackingNumber` and the correct `FeedbackType` (`HelpSystem` for the helpfulness shape); a validation test asserts a bug report without `severity` fails; the bug form resolves a mapped page to its friendly name and an unmapped page to its codebase name without crashing; a grep shows the ViewModel uses `IDao_WindowFormMapping`/`IDao_UserControlMapping` only (no inline SQL, no static DAO).

- [ ] **TASK-125** — Implement the content-coverage mechanism per [`plan.md`](./plan.md) §2.8: a documented convention tying each feature spec's declared user-visible screens to the help topic that covers them; a check (script or test) that lists the topics and flags any declared screen without a covering topic, wired into the build/test gate so an uncovered screen fails the gate.
  - **Verify**: the check passes against the current 15 files with every declared screen covered; adding a synthetic screen declaration with no covering topic makes the check fail; the convention is documented in [`spec.md`](./spec.md) §3.7 and the declaration location is recorded in `spec.md` §4 (gap 10).

---

## Phase 4 — Downstream validation

- [ ] **TASK-126** — **Downstream validation: `010-developer-tools-scaffold` (and a sweep of `011`).** Re-read [`../010-developer-tools-scaffold/spec.md`](../010-developer-tools-scaffold/spec.md), [`../010-developer-tools-scaffold/plan.md`](../010-developer-tools-scaffold/plan.md) and [`../010-developer-tools-scaffold/tasks.md`](../010-developer-tools-scaffold/tasks.md), and [`../011-deployment-and-cutover/spec.md`](../011-deployment-and-cutover/spec.md) (and its `plan.md`/`tasks.md` if present). Confirm the assumptions below still hold and record each in the Drift log.
  - **Verify**: the Drift log below has one row per assumption, each marked *confirmed* or *amended* (or *pending* with a stated reason), and `dotnet test MTM_WIP.Tests` is green.

The assumptions TASK-126 checks:

1. `010` scaffolds the feedback **review** UI only and does not implement `SubmitFeedbackAsync`/`AddCommentAsync`/the six flows — those stay in this spec (`010` §2.2, A6).
2. `010` consumes the same `Model_UserFeedback` type and the same `md_feedback_*` tables without requiring a different submission shape.
3. The window/control mapping tables are **read-only** for this spec; their `_Upsert` procedures have no caller here and no management UI appears in `010` in v1.
4. The feedback DAOs (`IDao_UserFeedback`, `IDao_UserFeedbackComments`, `IDao_WindowFormMapping`, `IDao_UserControlMapping`, `IDao_System`) exist in `MTM_WIP.Data/Contracts` per `002` and are registered in DI by the shell.
5. `011`'s publish ships `Assets/Help/Content/*.json` (`PreserveNewest` from `001`) **and** `Assets/Help/topic-shell.css` in the self-contained `win-x64` payload so the help loads offline at runtime.
6. `011` records **Microsoft Edge WebView2 Runtime** as a deployment prerequisite. This is now unconditional — D20 made the topic body HTML in one WebView2 host, so the runtime is always a dependency; when it is absent the help surface degrades to a plain message in the topic slot rather than failing.
7. No downstream spec reintroduces a help HTML template, the JS bridge, a `help://` interception, the `{{component:}}` preprocessor, or a `%TEMP%` help write.
8. Downstream feature specs declare their user-visible screens for the TASK-125 coverage check and keep each screen covered by a topic.

---

## Drift log

Filled in by TASK-126. One row per downstream assumption checked.

| Checked | Item | Result | Action taken |
| :--- | :--- | :--- | :--- |
| `010-developer-tools-scaffold` | Review UI only; six flows stay in `009` | *(pending)* | — |
| `010-developer-tools-scaffold` | Shared `Model_UserFeedback` + `md_feedback_*` tables | *(pending)* | — |
| `010-developer-tools-scaffold` | Mapping tables read-only; no `_Upsert` UI in v1 | *(pending)* | — |
| `002` / `004` | Feedback DAOs exist and are DI-registered | *(pending)* | — |
| `011-deployment-and-cutover` | Help content and `topic-shell.css` ship in the payload | *(pending)* | — |
| `011-deployment-and-cutover` | Microsoft Edge WebView2 Runtime recorded as a prerequisite | *(pending)* | — |
| `010`–`011` | No reintroduction of templates / bridge / `help://` / temp writes | *(pending)* | — |
| all feature specs | Screens declared and covered for the coverage check | *(pending)* | — |