# Manual Verification Checklist: Help System Enhancements

**Feature**: Contact Support System & Help Enhancements
**Date**: 2025-12-08
**Tester**: ____________________

## Performance Targets

- [ ] **SC-011**: Submit feedback + confirmation appears ≤ 2 seconds
  - *Test*: Fill out bug report, click Submit. Measure time until tracking number appears.
  - *Result*: ________ seconds

- [ ] **SC-016**: Filter/Sort in Developer Tools ≤ 1 second
  - *Test*: Open Developer Tools, load 50+ items. Change Status filter. Click column header.
  - *Result*: ________ seconds

- [ ] **SC-017**: CSV Export (10k rows) ≤ 3 seconds
  - *Test*: In Developer Tools, click Export to CSV. (May need to generate dummy data if 10k not available).
  - *Result*: ________ seconds

- [ ] **SC-015**: Email notification queued within 1 minute
  - *Test*: Submit Critical bug. Check logs/database for email trigger.
  - *Result*: ________ seconds

## Functional Verification

- [ ] **Offline Handling**
  - *Test*: Disconnect network/VPN. Attempt to submit feedback.
  - *Expected*: "Offline" or friendly error message in WebView2 (not crash).
  - *Result*: ____________________

- [ ] **Input Limits**
  - *Test*: Paste 45,000 characters into Description.
  - *Expected*: Warning message appears.
  - *Test*: Paste 50,001 characters.
  - *Expected*: Submission blocked or truncated.
  - *Result*: ____________________

- [ ] **Duplicate Handling**
  - *Test*: In Developer Tools, select a feedback item. Click "Mark Duplicate". Enter ID of another item.
  - *Expected*: Status changes to Closed (Duplicate). Link appears in "View Details".
  - *Result*: ____________________

- [ ] **Role Guard**
  - *Test*: Log in as non-admin/non-developer (e.g., Shop user). Attempt to open Developer Tools (via menu or shortcut).
  - *Expected*: Access denied message. Form does not open.
  - *Result*: ____________________

- [ ] **Singleton Help Viewer**
  - *Test*: Open Help from Main Form. Keep open. Open Help from Settings Form.
  - *Expected*: Existing window comes to front (no new window). Content navigates to new topic.
  - *Result*: ____________________

- [ ] **Role Change Handling (FR-047)**
  - *Test*: Open Developer Tools as Admin. Have DB admin revoke role. Click "Update Status".
  - *Expected*: Error message "Your permissions have changed". Controls disabled.
  - *Result*: ____________________

## Sign-off

**Status**: [ ] PASS / [ ] FAIL
**Notes**: __________________________________________________________________
