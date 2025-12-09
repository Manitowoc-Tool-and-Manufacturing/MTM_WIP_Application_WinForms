# Research (Phase 0)

All clarifications from the specification are resolved. Key confirmations and best practices recorded below.

## Decisions and Rationale

- **WebView2 Security**
  - **Decision**: Local templates only; secure JS bridge (`window.chrome.webview.postMessage`); enforce CSP.
  - **Rationale**: Prevent XSS/remote content risks; aligns with FR-041/042.
  - **Alternatives Considered**: Remote content (rejected due to security/availability risk); iframe hosting (rejected, more attack surface).

- **Input Sanitization**
  - **Decision**: Use HtmlSanitizer (or equivalent) for any HTML rendered from WebView2 inputs; parameterized stored procedures for all DB writes.
  - **Rationale**: Prevent XSS/SQL injection; consistent with FR-040/031.
  - **Alternatives Considered**: Custom regex sanitization (rejected; brittle), client-only sanitization (rejected; WebView2 untrusted).

- **Tracking Number Generation**
  - **Decision**: Annual reset via TrackingNumberSequence table (FeedbackType, Year, NextNumber); retry on collision.
  - **Rationale**: Human-readable, prevents sequence exhaustion; MySQL 5.7.24 compatible.
  - **Alternatives Considered**: Global ever-incrementing sequence (rejected: less readable, exhaustion risk); MySQL 8 SEQUENCE (rejected: incompatible with 5.7.24).

- **Email Notifications**
  - **Decision**: Per-category recipients stored in EmailNotificationConfig table; managed via admin UI (Developer Tools/Database Maintenance tab).
  - **Rationale**: Runtime configurability without redeploy; supports team-specific routing.
  - **Alternatives Considered**: App.config only (rejected: requires restart); hard-coded lists (rejected: brittle).

- **Assignment & Duplicates**
  - **Decision**: Single assignee per submission; manual duplicate marking only.
  - **Rationale**: Clear ownership; avoids noisy false positives; matches team size/process.
  - **Alternatives Considered**: Multi-assign (rejected: unclear ownership); auto-suggest (rejected: false positives, extra complexity).

- **Database Access Pattern**
  - **Decision**: Stored procedures only via Helper_Database_StoredProcedure; DAOs return Model_Dao_Result<T>; async everywhere.
  - **Rationale**: Constitution compliance; consistent with existing MTM patterns.
  - **Alternatives Considered**: Inline SQL (rejected), sync I/O (rejected).

## Open Questions

None. All specification clarifications are addressed.
