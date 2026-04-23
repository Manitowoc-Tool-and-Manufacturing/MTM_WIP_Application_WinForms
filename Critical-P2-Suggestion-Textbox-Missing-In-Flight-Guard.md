# Critical P2: Suggestion Textbox Missing In-Flight Guard

## Summary

The suggestion textbox can start multiple overlapping provider calls before the overlay becomes visible.

## Why This Needs Fixing

- Lost-focus and F4/full-list paths can duplicate the same lookup.
- `_isOverlayVisible` does not protect the await window before the overlay is shown.
- A slow provider call can be duplicated by normal user interaction.

## Evidence

- `Components/Shared/Component_SuggestionTextBox.cs:35`
- `Components/Shared/Component_SuggestionTextBox.cs:435-454`
- `Components/Shared/Component_SuggestionTextBox.cs:621-678`
- `Components/Shared/Component_SuggestionTextBox.cs:701`
- `Components/Shared/Component_SuggestionTextBox.cs:858-877`
- `Components/Shared/Component_SuggestionTextBox.cs:928`

## Risk

- Parallel duplicate suggestion queries from a single control.
- Out-of-order results and unnecessary SQL pressure.

## Recommended Fix

1. Add a per-control in-flight flag or semaphore around provider execution.
2. Ignore or coalesce repeated trigger attempts while a provider call is active.
3. Clear the in-flight state in all success, no-match, cancel, and exception paths.

## Acceptance Criteria

- A single suggestion control cannot run more than one provider call at a time.
- Repeated F4, focus churn, or rapid blur events do not duplicate the query.
- Exception paths do not leave the control permanently locked.
