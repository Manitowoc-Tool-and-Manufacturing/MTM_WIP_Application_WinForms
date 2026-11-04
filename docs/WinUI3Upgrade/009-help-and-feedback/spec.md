# Functional Specification: WinForms to WinUI 3 Mapping

**Spec**: `009-help-and-feedback`
**Depends on**: `001-foundation-and-tooling` (the verbatim asset copy), `003-crosscutting-services-and-theming` (dialogs, error service, dispatcher), `004-shell-and-startup` (navigation shell). The feedback DAOs are delivered by `002-data-access-layer` and registered through the shell's composition root.
**Added package**: `Microsoft.Web.WebView2` — `001`'s package list excluded it ("unless §2.8 finds a need"); §2.8 now needs it, so the package joins the solution here.
**Unblocks**: `011-deployment-and-cutover` (publishes the help content, the topic shell CSS and the help module, and records the WebView2 Runtime prerequisite)
**Tasks**: TASK-120 … TASK-126
**Governing rules**: [`../constitution.md`](../constitution.md) §1.3 (forbidden APIs), §2 Rules 1, 2, 5, 10, 11, 13

---

## 1. Executive Summary & Scope

The WinForms help system renders 15 JSON category files (54 topics) as HTML inside a single WebView2 control, with a template engine that preprocesses `{{component:}}` markers, a JavaScript bridge that carries inbound and outbound messages, and a feedback pipeline that resolves every submission to the `md_feedback_*` tables. This spec rebuilds the surface as a **hybrid**: the category index, search, topic-to-topic navigation and all six feedback flows become native WinUI pages and ViewModels, while only the topic **body** stays HTML and is rendered in **one** WebView2 host (decision D20).

The content itself is not changed. The 15 JSON files are copied verbatim by `001` into `MTM_WIP/Assets/Help/Content/` and deserialised into the same model shape. Each topic's body already lives in the `Content` field as raw HTML, so it needs no conversion — the host renders it as-is inside a single static CSS shell.

Help content is updated **in the same change as the feature it documents** (D20, constitution Rule 13). This spec makes that enforceable: a content-coverage mechanism (§3.7) ties every feature spec's declared user-visible screens to a covering help topic and fails the build when a screen has none. Content coverage is a checked property, not manual diligence.

### In scope

- The help content loader that reads the 15 category files into the existing model shape.
- The search service with the exact legacy scoring.
- The native help pages: category index, search box + results list, topic page (title, summary, navigation), and the "Was this helpful?" widget.
- The single WebView2 topic host and its one static CSS shell.
- The six feedback flows (helpfulness, bug report, suggestion, inconsistency, question, view-my-submissions with comments).
- The window/control friendly-name mapping used to label "where were you" in the bug form.
- The content-coverage mechanism that keeps help content aligned with feature screens (§3.7).

### Out of scope

- Release notes — rendered natively from `RELEASE_NOTES.json` by `007`; this spec only notes it and does not own it.
- The analytics HTML surfaces and their `%TEMP%` writes — `008`.
- The error dialog and its error-report queue — `003` and `010`.
- The developer-tools feedback manager (review/list/status UI) — `010` (scaffolded only in v1).
- The generic `Form_HtmlViewer` wrapper — not ported here; it belongs to whichever spec still needs a document viewer, if any.
- Authoring topic prose — each feature spec authors or updates its own help topics as part of its change; this spec owns the host and the coverage mechanism, not the prose.

### Success condition

All 54 topics are reachable, searchable and readable offline, with typography, table, code-block and alert formatting preserved by the CSS shell; the topic body renders in the single WebView2 host with no temp file, no network access and no template engine; all six feedback flows write through the existing procedures and issue tracking numbers; a missing WebView2 runtime shows a plain message in the topic slot while the rest of the surface keeps working; and a grep gate finds no `{{component:`, no `chrome.webview`, no `postMessage`, no `%TEMP%` write and no network call anywhere in the help module.

---

## 2. Legacy Feature Assessment & UI Mapping Table

| WinForms Artefact | Target WinUI 3 Artefact | Functional Requirement / Core Behavior |
| :--- | :--- | :--- |
| `Service_HelpContentLoader.LoadCategoriesAsync(dir)` — reads every `*.json`, `JsonSerializer.DeserializeAsync<Model_HelpCategory>`, per-file try/catch that continues on error, missing directory → empty list | `Service_HelpContentLoader` in `MTM_WIP.Shared/Help` reading `Assets/Help/Content/` | Same model, same continue-on-error rule, same empty-list result on a missing directory. The content path is the only change. |
| `Service_HelpSystem` (`IHelpSystem`) — owns `_categories`, composes the loader; `GetCategories` ordered by `Order`, `GetCategory` case-insensitive, `Search`, `GenerateIndexHtml`, `GenerateTopicHtml`, `SubmitFeedbackAsync`, `AddCommentAsync` | `Service_HelpSystem` (`IHelpSystem`) in `MTM_WIP.Shared/Help` | `GetCategories`, `GetCategory` and `Search` survive with identical semantics. `GenerateIndexHtml`/`GenerateTopicHtml` are **deleted** (native pages + one topic host replace them). `SubmitFeedbackAsync`/`AddCommentAsync` move to a module-level feedback service. |
| `Service_HelpTemplateEngine` — loads HTML templates with an in-memory cache, does placeholder replacement, applies `{{component:}}` only inside `WrapInBaseTemplate` | *(deleted)* | No templates, no placeholder replacement, no component preprocessor. The shell is one static CSS plus an in-memory wrapper, not an engine. |
| `HelpViewerForm` — a **singleton** with one WebView2 (Dock Fill, `CreationProperties` null) plus a message bridge and `help://` URI interception | Native help pages in `MTM_WIP.Module_Shell/Views/Help/` plus **one** WebView2 topic host in the topic page | One host page navigated through `004`'s `INavigationService`. The only WebView2 is the single topic-body host; the bridge and the `help://` interception are deleted. |
| `Model_HelpCategory { CategoryId, Title, Icon, Description, Order, Topics[] }`; `Model_HelpTopic { TopicId, Title, Summary, Content, Keywords[], LastUpdated }`; `Model_HelpSearchResult { Topic, Category, RelevanceScore }` | Same three classes in `MTM_WIP.Shared/Help/Models/` | Field-for-field identical so the verbatim JSON deserialises unchanged. `Content` stays raw HTML; the topic host renders it. |
| 15 JSON category files / 54 topics in `Documentation\Help\JSON\` | `MTM_WIP/Assets/Help/Content/*.json` (copied by `001`) | Copied verbatim; not retyped. |
| The HTML templates — `help-base-template.html`, `topic-card-template.html`, `search-box-template.html` (declared but never used), `help-contact-support-page.html`, the four form templates, `help-view-submissions.html`, `components\alert-component.html`, `components\code-block-component.html` | *(deleted)* — replaced by one static `topic-shell.css` + a minimal in-memory wrapper owned by the topic page | No page template survives; no template engine replaces them. The shell is a single CSS file plus a wrapper, not a template. |
| `{{component:NAME attr="value"}}body{{/component:NAME}}` regex (Singleline \| IgnoreCase), alert and code-block components, unknown components emit an HTML comment plus the body | *(deleted)* | Alert and code-block formatting are reproduced as CSS classes in the topic shell, not by a preprocessor. |
| JS ↔ C# bridge: `window.chrome.webview.postMessage` payloads, the JSON string root parsed twice; inbound `search`, `submitFeedback`, `viewSubmissions`, `getSubmissionDetails`, `addComment`, `getWindowMappings`, `getControlMappings`; outbound `onFeedbackSubmitted`, `onSubmissionsLoaded`, `onSubmissionDetailsLoaded`, `onWindowMappingsLoaded`, `onControlMappingsLoaded`, `refreshSubmissions` | Direct ViewModel ↔ service calls | No message bridge. Each bridge message becomes one command on a help ViewModel. |
| Feedback payload discrimination (§3.5) and the C#-added fields | `Service_FeedbackManager` submission path in `MTM_WIP.Module_Shell/Services/` | Same discrimination, same added fields, same `md_feedback_Insert` write and tracking number. |
| Window/control friendly-name mapping (`sys_windowform_mapping_GetAll`, `sys_usercontrol_mapping_GetByWindow`) | Two ComboBox sources loaded through `IDao_WindowFormMapping` / `IDao_UserControlMapping` | Friendly names label the "where were you" fields; the codebase name is the fallback. |
| Watermark `.content-area::after { background-image: url('{{WATERMARK_URL}}'); opacity 0.05; … }` with `Resources\MTM.png` or an SVG data-URI fallback | Reproduced in the topic shell CSS | The watermark moves from the deleted base template into the one static CSS owned by the topic page, using `Resources\MTM.png` or the SVG data-URI fallback. |
| WebView2 specifics — no custom environment, no explicit user-data folder (default beside the exe for help; `%LOCALAPPDATA%\MTM\WebView2` for two other surfaces), no CSP meta tag, fully offline via local files + `NavigateToString` | One WebView2 topic host | Explicit user-data folder `%LOCALAPPDATA%\MTM\WIP\WebView2`; Content-Security-Policy set on the host document (legacy shipped none); strict offline; runtime detection with a plain-message fallback (§3.4). |
| Topic body rendering — the `Content` field of each topic (raw HTML) | The single WebView2 host in the topic page, wrapped in a minimal in-memory document + the static CSS shell | Serves only the topic body; no template, no temp file, no network, no bridge. The `alert` and `code` component outputs are CSS classes, so existing content renders unchanged. |
| Release-notes viewer (HTML with JSON injected) | Native page rendering `RELEASE_NOTES.json` | Owned by `007`. One line here: release notes are rendered natively by `007`; this spec does not own them. |
| Images `Documentation\Help\images\Print1..6.png` | *(not copied by this spec)* | Referenced by nothing; dead assets. Whether `001` carried them is outside this spec. |

---

## 3. Behavioral Edge Cases & State Constraints

> **Screen constraints** (`000-pre-foundation` D58, D59): every screen must be fully usable at **1366×768 with 100 % DPI**, and every interactive control carries `AutomationProperties.Name` matching its visible label plus a stable `AutomationId`. High contrast and text scaling are out of scope by decision.

### 3.1 Content loading

- The loader reads every `*.json` file in the content directory.
- Each file deserialises into `Model_HelpCategory`; a file that fails to parse is logged and skipped — one bad file must not fail the whole load.
- A missing or empty directory yields an **empty** category list, never an exception.
- `GetCategories` returns categories ordered by `Order`. `GetCategory(id)` matches `CategoryId` case-insensitively.

### 3.2 Search — reproduced exactly

Given a query string `q` and the loaded categories:

1. Split `q` on a single space with `RemoveEmptyEntries`; lower-case each term with the invariant culture.
2. For every topic in every category, compute `score`:
   - lower-case the title, each keyword, the summary and the content (content is raw HTML, so tag names can match);
   - for each term: `+10` if the title contains it; `+8` if **any** lower-cased keyword contains it (the keywords field is scored once per term, not once per matching keyword); `+5` if the summary contains it; `+1` if the content contains it.
   - One increment per term per field, never per occurrence; terms sum.
3. Keep every topic with `score > 0`.
4. Order by `RelevanceScore` descending, with **no tie-break** — ties keep the sort's order.
5. An **empty** query does not search; it reloads the index.

The UI debounces keystrokes at 300 ms (matching the legacy client-side debounce); the service itself performs no debounce and is unit-testable without a UI thread.

### 3.3 Native surface

- The category index lists the categories in `Order` order; each card shows `Icon`, `Title` and `Description`.
- The topic page shows `Title`, `Summary` and the topic body rendered by the single WebView2 host, with native navigation back to its category and between topics.
- Search results show each matching topic's `Title` and `Summary` in relevance order, and open the topic on selection.
- The "Was this helpful?" widget (helpfulness feedback) lives on the topic page.

### 3.4 Topic host constraints

- Exactly one WebView2 instance exists in the help module; it renders only the topic body, wrapped in a minimal in-memory document — never a template and never a temp file.
- The host uses an explicit user-data folder, `%LOCALAPPDATA%\MTM\WIP\WebView2`, not the default beside the executable.
- The host document carries a Content-Security-Policy; the legacy help HTML shipped with **no CSP at all**. The policy allows only inline styles and local/in-memory content and blocks all network origins, external fonts, remote images and scripts.
- No network access of any kind: no CDN, no external fonts, no remote images. The body is served from memory (static styles from the packaged asset path); nothing is written to `%TEMP%`.
- The host detects the WebView2 runtime before constructing the environment. The legacy `HelpViewerForm` called `EnsureCoreWebView2Async` with no custom environment and no user-data folder, so a missing runtime left a blank control and silently queued navigations. The new host must **fail loudly**: when the runtime is absent it shows a plain native message in the topic slot, and every other part of the help surface — index, search, navigation, feedback — keeps working.
- The single static CSS provides typography, table styling, code-block styling, the alert variants (info / warning / error / success) and the watermark. The `alert` and `code` component outputs are expressed as CSS classes, so the 54 topics render without being rewritten.

### 3.5 Feedback flows and discrimination

Six flows, all writing through the existing procedures (no new procedure, no schema change — the database is frozen):

| Flow | Discriminator / shape | `FeedbackType` |
| :--- | :--- | :--- |
| Helpfulness | `{ topicId, categoryId, isHelpful, comment }` | `HelpSystem` |
| Bug report | has `bugCategory` — `severity`, `activeSection`, `windowForm`, `description`, `stepsToReproduce`, `expectedBehavior`, `actualBehavior` | `Bug Report` |
| Suggestion | has `suggestionCategory` — `priority`, `title`, `description`, `businessJustification`, `affectedUsers` | `Suggestion` |
| Inconsistency | has `inconsistencyType` — `activeSection`, `description`, `location1`, `location2`, `expectedConsistency` | `Inconsistency` |
| Question | has `questionCategory` — `priority`, `question` | `Question` |
| Anything else | none of the above | `General` |

The C# side adds, exactly as the legacy bridge did: `ApplicationVersion`, `OSVersion`, `MachineName`, `SubmissionDateTime`, `Status = New`, and `UserID` resolved through `sys_user_GetIdByName` (`IDao_System.GetUserIdByNameAsync`). Free-text fields are HTML-sanitised (`HtmlSanitizer`) before insert. `md_feedback_Insert` returns the `FeedbackID` and `TrackingNumber`; the tracking number is shown to the user. View-my-submissions lists the current user's rows via `md_feedback_GetByUser` and adds/reads comments via `md_feedback_comment_Insert` / `md_feedback_comment_GetByFeedbackId`.

### 3.6 State

- The category set is a read-only singleton (an injected `IHelpSystem` cache) — no static mutable state (constitution Rule 11).
- Feedback writes go through `Result<T>`-returning DAOs; a failure surfaces through `IErrorService`, never a `MessageBox`.
- No temp files, no JS bridge, no template engine and no network call exist in this module. The single WebView2 renders only topic bodies.

### 3.7 Content coverage is part of the change

Help content is updated **in the same change as the feature it documents** (D20; constitution Rule 13). This is enforced, not aspirational:

- Every feature spec that adds or changes a user-visible screen updates the help content in the same change.
- A documented convention ties each feature spec's declared user-visible screens to the help topics that cover them.
- A check (script or test) enumerates the declared screens and the help topics, lists every topic, and flags any screen declared without a covering topic. The check runs with the build/tests and fails on an uncovered screen.

The exact location and format of the screen declaration is set by TASK-125 and recorded in §4 (gap 10). The check is a mechanism, not a manual checklist.

**Who writes a topic, and when** (`000-pre-foundation` D62): each topic is **drafted by the model** from the corresponding legacy topic in the WinForms `Documentation/Help/JSON/` set, then **edited and approved by the owner** before the feature task that needed it is considered done. Every topic records which legacy topic it derives from and who approved it, so "the help is out of date" and "nobody checked this" are both visible rather than inferred. The 54 existing topics are the source each draft starts from — this is the mechanism that makes Rule 13 affordable without either writing everything from scratch or deferring help to a documentation phase that never happens.

---

## 4. Gaps, Missing Data & Clarifications

| # | Item | Status |
| :--- | :--- | :--- |
| 1 | **Topic-content renderer** — hybrid per D20: native index/search/navigation/feedback; only the topic body is HTML, rendered in one WebView2 host. | **Resolved (D20, 2026-11-02).** |
| 2 | The shell's module page-registration contract exposed by `004` (the exact `INavigationService` registration API the help pages use). | **(to confirm)** until `004`'s tasks are finalised. |
| 3 | The home of `Model_UserFeedback` and the submission half of `Service_FeedbackManager`. This spec places the service in `MTM_WIP.Module_Shell/Services/` and the model in `MTM_WIP.Core/Models/` (so `Data` and the module can both reference it); `010` scaffolds the review UI and must consume the same model. | **(to confirm)** with `010`. |
| 4 | The tag/class inventory actually used by the 54 topic bodies — determines what the one static CSS shell must style (typography, tables, code blocks, alerts). | **(to confirm)** by a tag-sweep script during TASK-123. |
| 5 | `HtmlSanitizer` (9.0.892, referenced by the legacy feedback path) is carried into the module and is the only sanitisation for user-entered feedback text. | **(to confirm)** the package lands without pulling WinForms. |
| 6 | Window/control mapping coverage — some pages may have no `sys_windowform_mapping` row. | Fallback is the codebase name; a page with no mapping must still submit. No mapping rows are created by this spec. |
| 7 | Release notes. | Owned by `007`; noted here only to avoid overlap. |
| 8 | Dead images `Print1..6.png`. | Referenced by nothing; outside this spec. |
| 9 | **WebView2 runtime + package** — `Microsoft.Web.WebView2` is added to the solution; `011` must record Microsoft Edge WebView2 Runtime as a deployment prerequisite; the topic slot degrades to a plain message when the runtime is absent. | **(to confirm)** with `011` (checked in TASK-126). |
| 10 | **Screen-declaration location/format** for the content-coverage mechanism (§3.7). | **(to confirm)** — set by TASK-125. |