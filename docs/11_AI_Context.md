# Smart Device Manager (SDM) - AI Context Document
**Version:** 1.0
**Status:** Active — Updated for Dual-Portal Architecture

This document is the single source of truth for all AI assistants working on the Smart Device Manager (SDM) project. Read this before generating any output.

---

## 1. Project Overview

**Name:** Smart Device Manager (SDM)

**Purpose:**
An enterprise desktop platform split into two applications:

- **Admin Portal** — for IT administrators to manage software, orders, knowledge base articles, company info, and users.
- **Customer Application** — for end customers to monitor their PC, run diagnostics, install software, browse articles, and place orders.

**Host Environment:**
Both applications run inside WPF desktop shells using WebView2 (Edge Chromium). React renders inside WebView2.

**Backend:**
ASP.NET Core 9, Clean Architecture, CQRS, MediatR, JWT, REST API, SignalR.

---

## 2. Technology Stack (Frontend — Both Portals)

| Technology | Role |
| :--- | :--- |
| React 19 | UI rendering |
| Vite | Build tooling |
| TypeScript (strict) | Type safety |
| Tailwind CSS v4 | Design tokens and styling |
| shadcn/ui | Primitive UI components |
| Lucide React | Icons |
| TanStack Query | Server state management |
| Zustand | Global client state |
| React Router v7 | Routing + data loaders |
| React Hook Form + Zod | Forms and validation |
| Axios | HTTP client |
| Sonner | Toast notifications |
| TanStack Table | Data tables |

---

## 3. Design Philosophy

**Visual Direction:** Professional, high-density, enterprise-grade.

**Inspired by:** Microsoft Admin Center, Azure Portal, GitHub Enterprise, Cloudflare Dashboard, VMware Workspace ONE.

**Admin Portal feel:** Dense information tables, collapsible sidebar, command search, right-side drawers for details and edits.

**Customer Application feel:** Clean, approachable, card-based, bottom navigation, no clutter.

**Non-negotiable rules:**
- No playful or consumer aesthetics in either portal.
- No emojis in UI.
- No excessive whitespace.
- No floating action buttons.
- No bouncy animations.

---

## 4. Admin Portal Pages

| Page | Route | Purpose |
| :--- | :--- | :--- |
| Login | `/login` | JWT authentication |
| Dashboard | `/dashboard` | Stats, recent orders, system status, quick actions |
| Orders Management | `/orders` | View and manage all customer orders |
| Software Management | `/software` | Unified apps + drivers management |
| Knowledge Base Management | `/knowledge-base` | Create and manage troubleshooting articles |
| Device Monitor | `/devices` | Hardware inspection using shared DeviceHardwarePanel |
| Users | `/users` | Manage admin accounts |
| Company Information | `/company` | Edit company profile visible to customers |
| Settings | `/settings` | App preferences |

---

## 5. Customer Application Pages

| Page | Route | Purpose |
| :--- | :--- | :--- |
| Dashboard | `/` | PC health summary, quick actions |
| Device Details | `/device` | Full hardware info via shared DeviceHardwarePanel |
| Device Check | `/device-check` | Full diagnostic scan + recommendations |
| Software Center | `/software` | Browse and install/update admin-uploaded software |
| Knowledge Base | `/knowledge-base` | Browse troubleshooting articles |
| Orders | `/orders` | Browse products and place orders |
| Company Information | `/company` | View company contact info |

---

## 6. Critical Shared Component

**`DeviceHardwarePanel`** is defined ONCE in `packages/ui/components/DeviceHardwarePanel.tsx` and imported by both portals.

It is used in:
- Admin Portal → Device Monitor page
- Customer Application → Device Details page

**Rule: This component must never be duplicated or re-implemented in either portal.**

It displays: CPU, GPU, RAM, Storage (per disk + per partition), Displays, Network, Motherboard, BIOS, Windows Version, Installed Drivers, Real-time usage.

---

## 7. Notifications

There is NO Notifications page in either portal.

SignalR is used only for order alerts:
- Customer submits an order → API dispatches SignalR event → Admin WPF shell shows a native Windows notification → Click navigates to `/orders`.

---

## 8. Software Management Rules

- Drivers and applications are managed in ONE unified page at `/software`.
- The admin never configures silent install flags or detection rules.
- The admin never sets up deployment scripts.
- Updating software = uploading a new setup file. Metadata is preserved.
- Version is auto-extracted from the setup file when possible.

---

## 9. UX Principles

| Principle | Rule |
| :--- | :--- |
| **Tables first** | All lists default to compact, sortable, filterable data tables |
| **Drawers for detail** | Open item detail and edit forms in right-side drawers, not full page routes |
| **Minimal clicks** | High-frequency actions visible directly on table rows |
| **Keyboard efficiency** | `Ctrl+K` search, `Ctrl+\` sidebar, `Esc` closes drawers |
| **Inline validation** | Validate on blur; show errors below the specific field |
| **No Notifications page** | Order alerts go to native Windows notification system |

---

## 10. Layout Rules

### Admin Portal
- **Header:** 56px. Breadcrumbs left, search center, badge + theme + profile right.
- **Sidebar:** Left, collapsible 240px → 56px.
- **Content:** Scrollable, max-width on forms, full-width on tables.
- **Drawers:** Right-side, 400–600px wide, slide in on row click or add button.

### Customer Application
- **Top bar:** Page title. No sidebar.
- **Navigation:** Bottom navigation bar (7 tabs).
- **Content:** Full-width, card-based, vertical scroll.

---

## 11. Design Tokens

| Token | Value |
| :--- | :--- |
| **Font — UI** | Inter |
| **Font — Technical logs, versions, hashes** | JetBrains Mono |
| **Body text size** | 13px |
| **Base spacing unit** | 4px (use multiples only) |
| **Border radius — buttons, inputs** | 6px |
| **Border radius — cards, drawers, dialogs** | 8px |
| **Table row height** | 34px |
| **Sidebar expanded width** | 240px |
| **Sidebar collapsed width** | 56px |
| **Header height** | 56px |
| **Primary color** | Azure Blue (Tailwind semantic `--primary`) |
| **Destructive color** | Alert Red (Tailwind semantic `--destructive`) |
| **Transition speed** | 150ms (hover), 300ms (drawer slide) |

---

## 12. AI Working Rules

**THESE RULES ARE NON-NEGOTIABLE.**

1. **Always follow this document.** It overrides any assumptions or defaults.
2. **Never create pages that do not exist in the page maps above.** Products, Drivers, Notifications, Roles, and Components are NOT standalone pages.
3. **Never duplicate `DeviceHardwarePanel`.** Import it from the shared package.
4. **Never add a Notifications page.** Notifications are handled by native Windows + SignalR only.
5. **Never configure silent install in the Software Management UI.** The admin only uploads a setup file.
6. **Never invent colors.** Use semantic Tailwind CSS v4 tokens (`bg-primary`, `text-destructive`, etc.).
7. **Never create playful, consumer, or marketing UI.** This is an enterprise tool.
8. **Always enforce high density in the Admin Portal.** Tables over cards. Small row heights. Compact padding.
9. **Customer Application may be warmer and simpler.** Cards are acceptable. Bottom navigation is correct.
10. **Never move navigation.** Admin has left sidebar. Customer has bottom nav. Both are fixed.
11. **Never generate backend code unless explicitly requested.**
12. **Always include accessibility attributes.** `aria-label`, `aria-live`, `role`, `focus-visible`.
13. **Prefer reuse over reimplementation.** Check `packages/ui` before building anything new.
14. **Prefer consistency over creativity.** Identical tasks must have identical UI treatment across pages.

---

## 13. Prompt Contract

Before generating any output, confirm:

- [ ] I know which portal this is for (Admin Portal or Customer Application).
- [ ] The page I am working on exists in the page map for that portal.
- [ ] I am not duplicating `DeviceHardwarePanel`.
- [ ] I am not creating a Notifications page or standalone Drivers/Products/Roles/Components page.
- [ ] I am using Tailwind CSS v4 semantic tokens, not hardcoded colors or arbitrary pixel values.
- [ ] I am using shadcn/ui primitives, not raw HTML or ad-hoc styles.
- [ ] I am generating only what was explicitly requested.
- [ ] My output matches the enterprise design standard, not a consumer app aesthetic.
