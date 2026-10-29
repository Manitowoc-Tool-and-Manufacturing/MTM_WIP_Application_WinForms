# Receiving Analytics Filtering Guide

This page shows purchase order lines from Visual and lets you narrow them down with date, status, text, and part-type filters.

## How Filters Work

Every filter change reloads the page data and then applies the selected filters again.
The page does not keep an old filtered list on screen.

If you change one filter, that filter is applied on its own.
The page does not combine unrelated filter actions into one shared step.

## Date Selection Priority

When the page decides which receive date to use, it checks the dates in this order:

1. Line Desired Date
2. Line Promise Date
3. PO Desired Date
4. PO Promise Date

If none of those dates are available, the page uses the Order Date.
Rows with no receive-date type are highlighted in pure red so they stand out.

## Status Options

The status checkboxes control which rows stay visible:

- `Show Late` keeps late lines visible.
- `Show Partial` keeps partial lines visible.
- `Show On Time` keeps on-time lines visible.
- `Show Open` keeps open lines visible.
- `Show Closed` includes closed lines in the refreshed results.

## What Counts As Partial

A line is treated as partial only when it is more than 0 received and still below 90% complete.

That means:

- Less than 90% received = partial
- 90% or more received = complete, not partial

If `Show Partial` is unchecked, those partial lines are removed from the grid.

## Text Filters

You can filter the list by typing or selecting values for:

- Part Number
- PO Number
- Vendor
- Carrier

These filters match text that appears in the current data.
If a value contains what you typed, the row stays visible.

## Part-Type Filters

These checkboxes narrow the list to specific part families:

- `Only Part Numbers`
- `Only Coils (MMC)`
- `Only Flat Stock (MMF)`

These options affect which part family stays on the page after the data refresh.

## Row Colors

Rows are color-coded to help you read the list quickly:

- Green: closed
- Red: late
- Yellow: partial
- Blue: on time or open
- Pure red: no receive-date type was available, so the page used the Order Date fallback

## Date Range Buttons

The `Previous`, `Current`, and `Next` buttons change the date range and reload the results.

## Summary

The Receiving Analytics page always reloads data when a filter changes, then applies the current filter settings to the refreshed results.
The page uses the date priority above, and partial lines are only lines below 90% received.
