# Technical Implementation Plan: WinForms to WinUI 3

**Spec**: `009-help-and-feedback`
**Tasks**: TASK-120 … TASK-126

---

## 1. Project Topology & Directory Mapping

All help content services and models land in **`MTM_WIP.Shared`** (references `Core` only); all help views, viewmodels, the topic host and the feedback submission service land in **`MTM_WIP.Module_Shell`** (references `Core`, `Shared`, `Data`).

```text
MTM_WIP.Shared/
├── Help/
│   ├── Models/
│   │   ├── Model_HelpCategory.cs      # CategoryId, Title, Icon, Description, Order, Topics[]
│   │   ├── Model_HelpTopic.cs         # TopicId, Title, Summary, Content, Keywords[], LastUpdated
│   │   └── Model_HelpSearchResult.cs  # Topic, Category, RelevanceScore
│   ├── Contracts/
│   │   ├── IHelpSystem.cs             # GetCategories, GetCategory, Search
│   │   └── IHelpContentLoader.cs      # LoadCategoriesAsync
│   └── Services/
│       ├── Service_HelpContentLoader.cs
│       └── Service_HelpSystem.cs

MTM_WIP.Module_Shell/
├── Contracts/
│   └── IService_FeedbackManager.cs
├── Services/
│   ├── Service_FeedbackManager.cs     # SubmitFeedbackAsync, GetUserSubmissionsAsync,
│   │                                  # GetSubmissionAsync, AddCommentAsync,
│   │                                  # GetWindowMappingsAsync, GetControlMappingsAsync
│   └── Service_HelpTopicHost.cs       # builds the in-memory wrapper, sets the CSP and the
│                                      # user-data folder, detects the WebView2 runtime
├── ViewModels/
│   ├── ViewModel_HelpHome.cs
│   ├── ViewModel_HelpTopic.cs
│   ├── ViewModel_HelpSearchResults.cs
│   ├── ViewModel_HelpBugReport.cs
│   ├── ViewModel_HelpSuggestion.cs
│   ├── ViewModel_HelpInconsistency.cs
│   ├── ViewModel_HelpQuestion.cs
│   └── ViewModel_HelpSubmissions.cs
└── Views/Help/
    ├── View_HelpHome.xaml
    ├── View_HelpTopic.xaml            # hosts the single WebView2 topic body
    ├── View_HelpSearchResults.xaml
    ├── View_HelpBugReport.xaml
    ├── View_HelpSuggestion.xaml
    ├── View_HelpInconsistency.xaml
    ├── View_HelpQuestion.xaml
    └── View_HelpSubmissions.xaml

MTM_WIP/Assets/Help/
├── Content/                           # 15 JSON files copied verbatim by 001
└── topic-shell.css                    # the one static CSS this spec owns (typography,
                                       # tables, code blocks, alert variants, watermark)
```

`Model_UserFeedback` (the feedback entity `IDao_UserFeedback.InsertAsync` already returns) lives in **`MTM_WIP.Core/Models/`** so both `Data` and `Module_Shell` reference it without a module-to-module edge (constitution Rule 10). `010` will consume the same type.

| WinForms file | Target file | Notes |
| :--- | :--- | :--- |
| `Services/Help/Service_HelpContentLoader.cs` | `MTM_WIP.Shared/Help/Services/Service_HelpContentLoader.cs` | Content directory injected as options. |
| `Services/Help/Service_HelpSystem.cs` | `MTM_WIP.Shared/Help/Services/Service_HelpSystem.cs` | `GenerateIndexHtml`/`GenerateTopicHtml` dropped. |
| `Services/Help/Service_HelpTemplateEngine.cs` | *(deleted)* | Templates and the component preprocessor are gone. |
| `Models/Help/Model_HelpCategory.cs`, `Model_HelpTopic.cs`, `Model_HelpSearchResult.cs` | `MTM_WIP.Shared/Help/Models/*.cs` | Field-for-field identical. |
| `Forms/Help/HelpViewerForm.cs` | `MTM_WIP.Module_Shell/Views/Help/*.xaml` + the topic host | Singleton WebView2 + bridge + `help://` interception replaced by pages + one topic-body host. |
| `Services/Service_FeedbackManager.cs` | `MTM_WIP.Module_Shell/Services/Service_FeedbackManager.cs` | Submission half only; the review UI is `010` (scaffolded). |
| `Models/Entities/Model_UserFeedback.cs` | `MTM_WIP.Core/Models/Model_UserFeedback.cs` | Shared with `010`. |
| `Documentation/Help/JSON/*.json` | `MTM_WIP/Assets/Help/Content/*.json` | Copied verbatim by `001`. |
| `Documentation/Help/Templates/**` | *(deleted)* — replaced by `MTM_WIP/Assets/Help/topic-shell.css` + an in-memory wrapper | No template engine; one CSS shell. |

---

## 2. Component Migration Strategy

### 2.1 Content loader and system

`Service_HelpContentLoader.LoadCategoriesAsync(contentDirectory)` enumerates `*.json`, deserialises each with `JsonSerializer.DeserializeAsync<Model_HelpCategory>` in a per-file `try/catch` that logs and continues, and returns an empty list for a missing directory. `Service_HelpSystem` composes the loader, holds the ordered category list, and exposes `GetCategories()` (ordered by `Order`), `GetCategory(string id)` (case-insensitive `CategoryId` match) and `Search(string query)`. Both are singletons; `IHelpSystem` is the only surface the views use.

### 2.2 Search — testable specification

The algorithm in `spec.md` §3.2 is the contract. As a test matrix:

| Input | Expected |
| :--- | :--- |
| Term in title only | score 10 |
| Term in one keyword | score 8 |
| Term in several keywords | score 8 (the keywords field is scored once per term) |
| Term in summary only | score 5 |
| Term in content only (e.g. matches an HTML tag name) | score 1 |
| Two terms, one in title and one in summary | 10 + 5 = 15 |
| Term occurring twice in the title | 10 (per term, not per occurrence) |
| Query with only spaces / empty | index, not a search |
| Query matching nothing | result omitted |

Result is `Model_HelpSearchResult { Topic, Category, RelevanceScore }`, ordered descending by score with no tie-break.

### 2.3 Native page structure

- **Category index** (`View_HelpHome`): a card per category in `Order` order — `Icon`, `Title`, `Description`; a persistent search box at the top with a 300 ms debounce; an empty search box reloads the index.
- **Topic page** (`View_HelpTopic`): `Title`, `Summary`, a back-to-category affordance, previous/next topic navigation, the "Was this helpful?" widget, and the topic body rendered by the single WebView2 host (§2.4).
- **Search results** (`View_HelpSearchResults`): the query box, a list ordered by relevance, each row showing `Title` and `Summary` and opening the topic on selection.
- **Feedback forms** (`View_HelpBugReport`, `View_HelpSuggestion`, `View_HelpInconsistency`, `View_HelpQuestion`): the fields from `spec.md` §3.5, with the bug form's window/control friendly-name pickers.
- **Submissions** (`View_HelpSubmissions`): the current user's submissions, and on a selected row its comments plus an add-comment field.
- All navigation goes through `004`'s `INavigationService.NavigateTo<TViewModel>()`.

### 2.4 Topic host and CSS shell

The renderer decision is made (D20): the topic body stays HTML and renders in **one** WebView2 host; the category index, search, navigation and feedback are native. `Model_HelpTopic.Content` is fed to the host unchanged.

- One WebView2 instance, embedded in `View_HelpTopic`, renders only the topic body wrapped in a minimal in-memory document.
- Explicit user-data folder `%LOCALAPPDATA%\MTM\WIP\WebView2`; never the default beside the executable.
- The host document sets a Content-Security-Policy (the legacy help shipped none) that allows only inline styles and local/in-memory content and blocks all network origins, external fonts, remote images and scripts.
- One static CSS (`Assets/Help/topic-shell.css`) is inlined into the wrapper and provides typography, table styling, code-block styling, the alert variants (info / warning / error / success) and the watermark. The `alert` and `code` component outputs are CSS classes, so the 54 topics render unchanged.
- Runtime detection before environment construction: absent runtime → a plain native `TextBlock` message in the topic slot; the rest of the help surface works (`spec.md` §3.4).
- No temp file, no network call, no template engine, no `{{component:}}`, no JS bridge, no `help://` interception, and no `NavigateToString` full-page assembly.
- Package: `Microsoft.Web.WebView2` is added to the solution. `001` excluded it ("unless §2.8 finds a need"); §2.8 now needs it.

### 2.5 Feedback flows

`Service_FeedbackManager` (module-scoped, injected) exposes `SubmitFeedbackAsync`, `GetUserSubmissionsAsync`, `GetSubmissionAsync`, `AddCommentAsync`, `GetWindowMappingsAsync` and `GetControlMappingsAsync`, all `async Task<Model_Result<T>>`. `SubmitFeedbackAsync`:

1. Discriminates the payload per `spec.md` §3.5 and sets `Model_UserFeedback.FeedbackType`.
2. Resolves `UserID` through `IDao_System.GetUserIdByNameAsync` (`sys_user_GetIdByName`).
3. Adds `ApplicationVersion`, `OSVersion`, `MachineName`, `SubmissionDateTime`, `Status = New`.
4. HTML-sanitises free-text fields with `HtmlSanitizer`.
5. Inserts via `IDao_UserFeedback.InsertAsync` → `md_feedback_Insert`, which returns `FeedbackID` and `TrackingNumber`; the tracking number is returned and shown.

Writes and reads use only the existing procedures: `md_feedback_Insert`, `md_feedback_GetByUser`, `md_feedback_GetById`, `md_feedback_comment_Insert`, `md_feedback_comment_GetByFeedbackId`, `sys_windowform_mapping_GetAll`, `sys_usercontrol_mapping_GetByWindow`. No new procedure, no schema change.

### 2.6 Window/control friendly-name mapping

The bug form labels "where were you": a window picker bound to `GetWindowMappingsAsync()` (`sys_windowform_mapping_GetAll`) and, when the selected window has control rows, a control picker bound to `GetControlMappingsAsync(windowFormMappingId)` (`sys_usercontrol_mapping_GetByWindow`). The current page resolves its codebase name to a friendly name; a page with no mapping row falls back to its codebase name and still submits.

### 2.7 Deletions and negative constraints

Gone, and asserted by tests/gates: the HTML templates, the `{{component:}}` preprocessor, the JS↔C# bridge and its inbound/outbound message set, the `NavigateToString` full-page assembly path (`GenerateIndexHtml`/`GenerateTopicHtml`), the in-page JavaScript (the 300 ms search debounce and the Ctrl+F handler), the `help://` URI interception, and the singleton WebView2 that rendered whole pages. The one remaining WebView2 renders only the topic body. The `%TEMP%` HTML writes belonged to the analytics surfaces (`008`), not to help, and are likewise absent. The help module makes no network request.

### 2.8 Content coverage — part of every feature change

Help content is updated **in the same change as the feature it documents** (D20, constitution Rule 13). The mechanism, delivered by TASK-125:

- A documented convention: each feature spec declares its user-visible screens and, per screen, the help `TopicId` that covers it.
- A check (script or test) enumerates those declarations and the help topics, lists every topic, and fails when a declared screen has no covering topic. The check is part of the build/test gate, not a manual list.
- The declaration file's exact location and format is set in TASK-125 and recorded in `spec.md` §4 (gap 10).

---

## 3. Data & State Migration

### 3.1 Read state

- The category set is a singleton `IHelpSystem` cache loaded once from the copied assets; it is immutable after load and is persisted nowhere.
- The topic shell CSS is a packaged asset read at load and inlined into the in-memory wrapper; no temp file is produced.

### 3.2 Written state

- All feedback persistence uses the frozen database. The exact write surface is `md_feedback_Insert` (with `OUT FeedbackID`, `OUT TrackingNumber`), `md_feedback_comment_Insert` (`OUT CommentID`), and the reads `md_feedback_GetByUser`, `md_feedback_GetById`, `md_feedback_comment_GetByFeedbackId`, `sys_windowform_mapping_GetAll`, `sys_usercontrol_mapping_GetByWindow`. Parameters are passed without the `p_` prefix (the helper adds it).
- No new table, column, procedure or status contract. `p_Status >= 0` is success, `p_Status < 0` is failure, `p_ErrorMsg` is passed through unchanged — per `002`.

### 3.3 What is not carried over

- The template cache and every HTML template — the page templates, the base template and the component fragments. They are replaced by one static CSS shell and an in-memory wrapper, not by another template.
- The JS bridge in both directions and the `help://` URI interception.
- The in-page JavaScript (the 300 ms search debounce and the Ctrl+F handler). The debounce moves into the search ViewModel; there is no in-page script.
- The `NavigateToString` full-page assembly path (`GenerateIndexHtml` / `GenerateTopicHtml` building a whole shell document).
- The WebView2 default user-data-folder behaviour — help used the folder beside the exe; two other surfaces used `%LOCALAPPDATA%\MTM\WebView2`. Replaced by one explicit folder, `%LOCALAPPDATA%\MTM\WIP\WebView2`, for the single topic host.
- Any `Model_Application_Variables` static state the legacy help touched — none of it is referenced by the new help module.
- The `Print1..6.png` help images (dead assets).