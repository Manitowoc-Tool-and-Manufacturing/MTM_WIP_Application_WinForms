---
description: 'HTML form layout patterns, tooltip implementation, and CSS styling for interactive forms'
applyTo: '**/*.html,**/*.css'
---

# HTML/CSS Forms Memory

Form layout patterns, tooltip integration, and responsive styling for modern web applications.

## Tooltip Implementation with Info Icons

Use native HTML title attributes with styled info icons for field-level help:

```html
<label for="field-name">
    <span>Field Label</span>
    <span class="info-icon" title="Help text here" aria-label="Help text here">ⓘ</span>
    <span class="required">*</span>
</label>
```

```css
.info-icon {
    display: inline-flex;
    align-items: center;
    justify-content: center;
    width: 20px;
    height: 20px;
    margin-left: var(--spacing-sm);
    border-radius: 50%;
    background-color: var(--color-bg-secondary);
    color: var(--color-primary);
    font-size: 0.75rem;
    cursor: help;
}

.info-icon:hover {
    background-color: var(--color-primary);
    color: #fff;
}
```

**Key benefits:**
- Native browser tooltip (no JavaScript)
- Accessible with aria-label
- Consistent styling
- Mobile-friendly

## Flex Layout for Form Labels

Use flexbox to align label text, tooltips, and required indicators:

```css
.form-group label {
    display: flex;
    align-items: center;
    gap: var(--spacing-sm);
    font-weight: var(--font-weight-medium);
    margin-bottom: var(--spacing-sm);
}
```

This layout prevents tooltips and required indicators from wrapping awkwardly and maintains consistent spacing.

## Unified Form Field Styling

Style all input types consistently to avoid visual inconsistencies:

```css
.form-group input[type="text"],
.form-group textarea,
.form-group select {
    width: 100%;
    padding: var(--spacing-md, 1rem);
    border: 1px solid var(--color-border, #e0e0e0);
    border-radius: var(--radius-md, 0.375rem);
    font-family: inherit;
    background: #ffffff;
    color: #1a1a1a;
}
```

Include `select` elements in the same rule to ensure dropdowns match text inputs visually.

## CSS Nesting Issues

Avoid accidentally nesting CSS properties inside other rule blocks:

```css
/* ❌ WRONG: Properties nested inside .template-badge */
.template-badge {
    padding: 2px 8px;
    .form-group input[type="text"] {  /* This creates invalid CSS */
        width: 100%;
    }
}

/* ✅ CORRECT: Separate rule blocks */
.template-badge {
    padding: 2px 8px;
    border-radius: var(--radius-sm);
}

.form-group input[type="text"] {
    width: 100%;
    padding: var(--spacing-md);
}
```

When CSS rules appear inside other rules unexpectedly, check for missing closing braces or incorrect indentation.

## Missing CSS Files

When referencing CSS files that don't exist (404 errors):

1. **Verify the file exists** in the expected directory
2. **Move inline** if the styles are page-specific:
   ```html
   <style>
   /* Page-specific styles here */
   </style>
   ```
3. **Remove the reference** if the file is not needed
4. **Create the file** if shared styles are intended

Never leave broken CSS references in production code - they slow page load with 404 requests.
