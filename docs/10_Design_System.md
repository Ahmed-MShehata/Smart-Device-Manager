# Smart Device Manager (SDM) - Design System Specification
**Version:** 1.0  
**Author:** Principal Design Systems Architect  
**Status:** Released  
**Target Platform:** React 19, Tailwind CSS v4, shadcn/ui, Lucide Icons

---

## 1. Color System

SDM utilizes a high-contrast, professional, and accessible semantic palette based on Tailwind CSS v4 variables. The palette is optimized for information density, fast scanning, and visual comfort across both light and dark environments.

| CSS Variable (Semantic Token) | HSL Value (Light Theme) | HSL Value (Dark Theme) | Description |
| :--- | :--- | :--- | :--- |
| `--background` | `0 0% 100%` (Pure White) | `224 71% 4%` (Deep Slate-Black) | Main app viewport canvas |
| `--foreground` | `224 71% 4%` (Dark Slate) | `210 20% 98%` (Bright White-Gray) | Body text, titles, copy |
| `--card` | `0 0% 100%` (White) | `224 71% 4%` (Deep Slate-Black) | Dashboard widgets and panels |
| `--card-foreground` | `224 71% 4%` | `210 20% 98%` | Card textual elements |
| `--popover` | `0 0% 100%` | `224 71% 4%` | Menus, dropdowns, popovers |
| `--popover-foreground` | `224 71% 4%` | `210 20% 98%` | Popover text |
| `--primary` | `221.2 83.2% 53.3%` (Azure Blue) | `217.2 91.2% 59.8%` (Neon Azure) | Primary actions, CTA buttons |
| `--primary-foreground` | `210 20% 98%` | `224 71% 4%` | Text on primary elements |
| `--secondary` | `210 40% 96.1%` (Light Gray) | `217.2 32.6% 17.5%` (Dark Gray) | Secondary triggers, badges |
| `--secondary-foreground` | `222.2 47.4% 11.2%` | `210 20% 98%` | Text on secondary elements |
| `--muted` | `210 40% 96.1%` | `217.2 32.6% 17.5%` | Secondary tabs, table striping |
| `--muted-foreground` | `215.4 16.3% 46.9%` | `215 20.2% 65.1%` | Secondary info, helper text |
| `--accent` | `210 40% 96.1%` | `217.2 32.6% 17.5%` | Hover target overlays |
| `--accent-foreground` | `222.2 47.4% 11.2%` | `210 20% 98%` | Selection/hover states text |
| `--destructive` | `0 84.2% 60.2%` (Alert Red) | `0 62.8% 30.6%` (Deep Red) | Serious errors, deletes |
| `--destructive-foreground` | `210 20% 98%` | `210 20% 98%` | Danger button label text |
| `--border` | `214.3 31.8% 91.4%` | `217.2 32.6% 17.5%` | Subtle container lines |
| `--input` | `214.3 31.8% 91.4%` | `217.2 32.6% 17.5%` | Form borders |
| `--ring` | `221.2 83.2% 53.3%` | `224.3 76.3% 48%` | Interactive keyboard focus rings |

---

## 2. Typography

The default typography is set to high-density sans-serif fonts, favoring strict line-height values to protect readability in packed screens.

| Token | Family | Size | Line Height | Font Weight | Usage Guidelines |
| :--- | :--- | :--- | :--- | :--- | :--- |
| `display-1` | Sans (Inter) | `30px` | `38px` | `700` (Bold) | Large metrics presentation, login titles |
| `heading-1` | Sans (Inter) | `20px` | `28px` | `600` (SemiBold) | Dashboard pages header targets |
| `heading-2` | Sans (Inter) | `16px` | `24px` | `600` (SemiBold) | Drawer title headers, table categories |
| `body-medium`| Sans (Inter) | `13px` | `18px` | `500` (Medium) | Default grid context texts |
| `body-small` | Sans (Inter) | `12px` | `16px` | `400` (Regular) | Help lines, descriptors, secondary logs |
| `code-mono`  | Mono (JetBrains) | `12px` | `16px` | `400` (Regular) | CLI logs, registry hashes, shell args |

---

## 3. Spacing Scale

Spacing uses a strict 4px grid. Variables map directly to Tailwind CSS v4 class properties.

| Tailwind Token | Relative (rem) | Absolute (pixel) | Primary Placement Target |
| :--- | :--- | :--- | :--- |
| `space-1` | `0.25rem` | `4px` | Internal icon-to-text spacing |
| `space-2` | `0.5rem` | `8px` | Small list items gap, action badge padding |
| `space-3` | `0.75rem` | `12px` | Form field stacks, card horizontal paddings |
| `space-4` | `1.0rem` | `16px` | Grid column gaps, main dashboard section gaps |
| `space-6` | `1.5rem` | `24px` | Primary outer canvas padding bounds |
| `space-8` | `2.0rem` | `32px` | Login card margins |

---

## 4. Border Radius

*   `--radius-none` (`0px`): Full sharp borders, used in WPF WebView2 container frames.
*   `--radius-sm` (`4px`): Used on smaller elements (Badges, Checkbox grids, Tooltips).
*   `--radius-md` (`6px`): Default element curvature (Buttons, Input cards, Dropdowns).
*   `--radius-lg` (`8px`): Content panels, primary Dialog panels, right-side Drawers.
*   `--radius-full` (`9999px`): Pill selectors and user profile avatars.

---

## 5. Shadows

*   `shadow-none`: Embedded widgets.
*   `shadow-sm`: Basic cards, datatables.
*   `shadow-md`: Default popovers, context menus.
*   `shadow-lg`: Right-side drawers, modal dialogue interfaces.

---

## 6. Elevation

| Elevation Level | Z-Index | Visual Component Targets |
| :--- | :--- | :--- |
| `level-floor` | `z-0` | Default body canvas, page backgrounds |
| `level-base` | `z-10` | Interactive grids, card containers |
| `level-sticky` | `z-20` | Sticky table headers |
| `level-header` | `z-30` | Top menu headers, sidebar overlays |
| `level-drawer` | `z-40` | Slide-out drawers, sidebar collapsible overlays |
| `level-popup` | `z-50` | Dialog boxes, dropdown overlays, toast triggers |

---

## 7. Icons

*   **Primary Library:** Lucide React.
*   **Grid Dimensions:** Standarize layout sizing:
    *   *Small (Actionable icons)*: `14px x 14px` (Stroke width: `2px`).
    *   *Medium (Sidebar/Default)*: `18px x 18px` (Stroke width: `1.5px`).
    *   *Large (Empty States)*: `32px x 32px` (Stroke width: `1.25px`).
*   **Semantic Mapping:**
    *   `DeviceScanner`: `Cpu` / `Laptop`
    *   `SoftwarePackage`: `Package`
    *   `Driver`: `HardDrive`
    *   `DiagnosticTree`: `GitFork`
    *   `Notifications`: `Bell`
    *   `Security`: `ShieldAlert`

---

## 8. Buttons

*   **Purpose:** Allow administrators to trigger distinct CRUD actions and commands.
*   **Variants:**
    *   *Primary:* Solid blue background (`--primary`), white text.
    *   *Secondary:* Slate-gray styling (`--secondary`), neutral text.
    *   *Outline:* Transparent backgrounds with `--border` contours.
    *   *Destructive:* Alert red backgrounds (`--destructive`).
*   **Sizes:**
    *   *Small (Inline list actions):* 28px height, 12px text.
    *   *Medium (View controls):* 36px height, 13px text.
    *   *Large (Form submissions):* 44px height, 14px text.
*   **States:** Default, Hover (accent background), Active (pressed depth), Focused (outline ring), Disabled (30% opacity, `cursor-not-allowed`).
*   **Spacing:** Medium Variant: padding `8px` top/bottom, `16px` left/right.
*   **Accessibility:** WAI-ARIA Role: `button`. Enter or Space triggers events. Explicit `aria-busy` state during api calls.

---

## 9. Inputs

*   **Purpose:** Gather textual datasets (hostnames, package keys, registry values).
*   **Variants:** Standard Input text field, Textarea input field, Password input with toggle icon.
*   **Sizes:** Height matches button lines: 36px height (13px text).
*   **States:** Default (muted borders), Active/Hover (dark border lines), Focused (blue outlines and inner HSL ring), Error (rose highlight borders), Disabled (gray fill, no cursor inputs).
*   **Accessibility:** Must explicitly bind tags to `aria-invalid` and associate description elements using `aria-describedby`.

---

## 10. Selects

*   **Purpose:** Filter options (OS types, administrator profiles, order stages).
*   **Variants:** Custom dropdown select wrapper, Combobox autocomplete selection.
*   **Sizes:** 36px height bounds.
*   **States:** Inline dropdown menus open downward or upward if bounds restrict view. Option selection highlights items with accent colors.
*   **Accessibility:** Employs Radix primitive accessibility rules. Keys supported: Up/Down arrow selections, Enter confirmation.

---

## 11. Checkboxes

*   **Purpose:** Toggle binary preferences or select multiple datatable rows.
*   **Variants:** Single inline form control, select-all table checkbox.
*   **Sizes:** `16px x 16px` squared vector grid.
*   **States:** Default (transparent, gray border), Selected (blue background, checkmark vector), Focus (blue ring), Disabled.
*   **Accessibility:** Keyboard selection via `Space`. `aria-checked` mirrors selection state.

---

## 12. Radio Buttons

*   **Purpose:** Select a single option from a small group.
*   **Variants:** Vertical group stack, horizontal group strip.
*   **Sizes:** `16px x 16px` rounded container.
*   **States:** Unselected (muted border), Outlined (outer ring), Checked (inner dot solid), Disabled.
*   **Accessibility:** Keyboard nav matches arrow keys controls. WAI-ARIA role: `radio`.

---

## 13. Switches

*   **Purpose:** Instantly toggle settings, bypass validation, or disable tasks.
*   **Variants:** Toggle tracks with visual slider.
*   **Sizes:** 36px track width, 20px height track lines.
*   **States:** Default Off (gray container, left indicator), On (blue container, right slider tracker).
*   **Accessibility:** Keyboard interaction handles check states cleanly via `Space`. `role="switch"`.

---

## 14. Tables

*   **Purpose:** Render high-density databases (device logs, orders lists, driver identifiers).
*   **Variants:** Standard datatable grid layout, compact system-log table view.
*   **Sizes:** Row target height: `34px`. Header height: `38px`.
*   **Behavior:** Lazy scrolling virtual layers, page selectors, sticky target headers, and toggleable column visibilities.
*   **Accessibility:** Explicit tables elements structural mappings (`<thead>`, `<tbody>`, `<tr>`, `<td>`). Screen reader announcements for active sort queries.

---

## 15. Cards

*   **Purpose:** Component boxes highlighting summary stats and key metrics.
*   **Variants:** Alert cards, simple text info cards, diagnostic action cards.
*   **Sizes:** Variable grids (configured to fit 1-column, 2-column, or 4-column structures).
*   **Guidelines:** Avoid nesting cards deeper than 2 layers. Keep action buttons grouped along the bottom margins.

---

## 16. Dialogs (Modals)

*   **Purpose:** Require confirmation for high-risk operations (e.g., delete actions, manual synchronization overrides).
*   **Sizes:** Standard Size: `520px` width. Large configuration: `800px` width.
*   **Behavior:** Traps standard keyboard focus inside the dynamic layout. Pressing `Esc` closes the view.
*   **Accessibility:** Explicitly mark elements using `aria-modal="true"`, pointing targets to parent header classes.

---

## 17. Drawers

*   **Purpose:** Quick inspect side drawers sliding in from the right edge.
*   **Sizes:** Widths capped at 400px (Default) or 600px (Split logs).
*   **Behavior:** Slide transitions from viewport edge, retaining primary grid details underneath.
*   **Accessibility:** Traps keyboard focus upon open state. Clicking the background overlay closes the drawer.

---

## 18. Toast Notifications

*   **Purpose:** Transient alerts confirming action state results without interrupting workflows.
*   **Variants:** Success (green checkmark), Warning (amber icon), Error Alert (red alert), Activity Info (spinner outline).
*   **Behavior:** Slide up from bottom right of screen, Auto-dismissing after 4 seconds.
*   **Accessibility:** Role set to toast alert (`role="status"`). Accessible screen reader narration for background checks.

---

## 19. Alerts

*   **Purpose:** Deliver inline static notification updates across page views.
*   **Variants:** Danger Alert (red warning banner), Warning Update (amber text background), Success Info (green badge), Standard System Alert (blue banner).
*   **Sizes:** Full span elements, wrapping headers.
*   **Accessibility:** Role configurations map to `role="alert"`.

---

## 20. Badges

*   **Purpose:** Categorize attributes or display system statuses (e.g., critical log levels).
*   **Variants:** Badges reflect health statuses: Nominal, Attention Required, Off-Line.
*   **Sizes:** Compact container padding (`2px` vertical, `8px` horizontal).
*   **Guidelines:** Text must remain uppercase helper terms, avoiding long sentences.

---

## 21. Breadcrumbs

*   **Purpose:** Keep track of the user's location within deep information hierarchies.
*   **Variants:** Compact breadcrumbs, slash separated lists.
*   **Spacing:** 12px font sizes, 8px indentations.
*   **Accessibility:** Wrapped inside semantic navigation containers (`<nav aria-label="Breadcrumb">`).

---

## 22. Tabs

*   **Purpose:** Toggle views within the same page context.
*   **Variants:** Underlined tabs, rounded pills.
*   **States:** Active node (solid color, blue indicator), Inactive node (slate typography), Focus (ring).
*   **Accessibility:** Navigation maps to `role="tablist"` utilizing arrow selectors.

---

## 23. Sidebar

*   **Purpose:** Primary navigation controller.
*   **Sizes:** Full size: `240px` width. Collapsed size: `56px` narrow icons strip.
*   **Behavior:** Left side pin. When collapsed, nav titles convert to persistent tooltips on icon hover.
*   **Accessibility:** Roles are defined with semantic navigation tags (`<nav role="navigation">`).

---

## 24. Header

*   **Purpose:** Render global tools (Search command bars, notify trays, active user settings).
*   **Sizes:** Vertical height standard bounds: `56px`.
*   **Elevation:** Level sticky (`z-index: 30`, bordered styling, backdrop filter: blur 8px).

---

## 25. Search Box

*   **Purpose:** Quick filter datasets or jump routes.
*   **Variants:** Table filter search, Global Cmd-K dialog box.
*   **States:** Expanded, focused, filtered.
*   **Icons:** Left-aligned magnifying glass icon (`Search`), right-aligned command keys placeholder tooltip (`Ctrl + K`).

---

## 26. Pagination

*   **Purpose:** Navigate large tabular datasets.
*   **Variants:** standard footer pagination (First, Prev, Page numbers, Next, Last).
*   **Sizes:** 32px height buttons.
*   **Accessibility:** Explicitly define navigation elements with `aria-label="Pagination Navigation"`.

---

## 27. Empty States

*   **Purpose:** Guide administrators when no dataset records are found.
*   **Structure:** Standard center elements, context-specific graphic icons, explanatory texts, and a primary CTA button.

---

## 28. Skeleton Loading

*   **Purpose:** Prevent page flashing and layout shifts during async queries.
*   **Variants:** Table skeleton row placeholder blocks, card structure shimmers.
*   **Behavior:** Subtle fade loop animations.

---

## 29. Charts

*   **Purpose:** Plot computer availability and driver configuration metrics.
*   **Variants:** Bar chart breakdowns, Line area trends, Pie alerts maps.
*   **Colors:** Accent hues derived from the UI color system (Nominal Green, Warning Amber, Destructive Red, Primary Blue).

---

## 30. Forms

*   **Purpose:** Collect configuration parameters (company information, diagnostic values).
*   **Variants:** Settings panel page flows, popup wizard steps.
*   **Spacing:** Vertical fields stack margins: `16px`.

---

## 31. Validation Messages

*   **Purpose:** Display input format errors dynamically.
*   **Variants:** Red inline warning texts, summary error list panels.
*   **States:** Toggle check runs immediately on field blur.

---

## 32. Responsive Rules

*   `sm`: `640px` - Handheld mobile devices (sidebars collapse into responsive sliders).
*   `md`: `768px` - Tablet layouts (grids wrap to 1-column layouts).
*   `lg`: `1024px` - Desktop viewports (default sidebar visible).
*   `xl`: `1280px` - Wide desks (full sidebar configurations open).

---

## 33. Motion & Animation

SDM prioritizes performance by limiting animations to subtle, non-distracting UI cues:
*   **Transitions:** `150ms` ease-in-out curve for hover overlays and button state checks.
*   **Drawer Slide:** `300ms` slide transitions.
*   **Reduced Motion:** Respects the user's OS preference (`@media (prefers-reduced-motion)` resets transitions to 0ms).

---

## 34. Dark Mode

The interface uses standard media queries and a `.dark` class selector applied to the root documentElement, swapping light and dark CSS variable values instantly.

---

## 35. Accessibility Rules

*   **Contrast Bounds:** Strictly follows WCAG 2.1 AA parameters.
*   **Keyboard Navigation:** Support keyboard navigation across all interactive widgets.
*   **Focus Management:** Maintain visible focus rings (`focus-visible`) across all form inputs and interactive tables.
