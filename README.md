# mi-proyecto – Multi-Tenant Landing Page Platform

A monorepo that implements a dynamic multi-tenant landing page system with a visual editor.

## Architecture

```
┌─────────────────────────────────────────────────────────────────┐
│                        Request Flow                             │
│                                                                  │
│  demo.localhost:4321 ──► Astro SSR ──► Renders tenant landing  │
│  localhost:4321      ──► Astro SSR ──► Service own landing     │
│  localhost:4321/app  ──► Astro SSR ──► Serves React Editor SPA │
│  localhost:5173      ──► React SPA ──► Visual editor directly  │
│                                                                  │
│  Editor Preview uses an <iframe> pointing to /preview?tenant=  │
│  Saves trigger PUT to .NET API, then iframe.reload()           │
└─────────────────────────────────────────────────────────────────┘

apps/
├── landing/   →  Astro SSR  (renders tenant landings + serves editor proxy)
├── editor/    →  React SPA  (visual editor + live preview via iframe)
└── api/       →  .NET 8 API (in-memory CRUD: tenants, pages, templates)
```

## Projects

### `apps/api` – .NET 8 REST API
- In-memory storage (no database)
- Seed data: tenant `demo`, template `generic-saas`, full SaaS demo page
- **Each tenant has exactly one landing page** (no page slugs)
- Runs on port **5000**

### `apps/landing` – Astro SSR
- Multi-tenant middleware: detects tenant from subdomain or `?tenant=` query param
- Renders landing pages from `PageSchema` fetched from the API
- `/preview?tenant=xxx` — editor preview endpoint (includes PostMessage listener)
- Serves the React editor SPA at `/app`
- Runs on port **4321**

### `apps/editor` – React SPA (Vite)
- Visual editor with content fields panel + live preview iframe
- Preview iframe points to Astro SSR `/preview` and reloads on save
- Auto-saves after 2s of inactivity (debounced)
- Runs on port **5173**

---

## Running the Projects

### 1. Start the API

```bash
cd apps/api
dotnet run
# → http://localhost:5000
```

### 2. Start the Landing (Astro SSR)

```bash
cd apps/landing
npm install
npm run dev
# → http://localhost:4321
```

### 3. Start the Editor (React SPA)

```bash
cd apps/editor
npm install
npm run dev
# → http://localhost:5173
```

---

## Testing

| URL | What you see |
|-----|-------------|
| `http://localhost:4321` | Service own landing page |
| `http://localhost:4321?tenant=demo` | Demo tenant landing page (rendered by Astro SSR) |
| `http://localhost:4321/preview?tenant=demo` | Preview endpoint (editor iframe target) |
| `http://localhost:4321/app` | React editor (proxied via iframe) |
| `http://localhost:5173` | React editor directly |

**Editor flow:**
1. Open `http://localhost:5173`
2. Enter tenant ID `demo`, click **Open Editor**
3. Edit text, images, colors in the left panel
4. Auto-save triggers after 2s — preview iframe reloads showing updated content
5. Or click **Save** manually to trigger an immediate save + iframe reload

---

## API Endpoints

### Tenants
```
GET    /api/tenants                     List all tenants
GET    /api/tenants/{tenantId}          Get tenant by ID
POST   /api/tenants                     Create tenant
```

### Page (one per tenant)
```
GET    /api/tenants/{tenantId}/page     Get tenant's landing page schema
PUT    /api/tenants/{tenantId}/page     Update tenant's landing page schema
```

### Templates
```
GET    /api/templates                   List templates
GET    /api/templates/{templateId}      Get template by ID
```

---

## PageSchema

The `PageSchema` is the core data model shared across all three apps:

```typescript
interface PageSchema {
  id: string;
  templateId: string;   // which Astro template to use
  tenantId: string;
  seo: SeoMeta;
  theme: ThemeConfig;   // CSS custom properties injected into BaseLayout
  sections: Section[];  // ordered, toggleable sections
}
```

Each `Section` contains `ComponentNode[]` – a recursive tree of editable content nodes (text, heading, image, button, list, card, etc.).

## Tech Stack

- **Astro** 5.x with `@astrojs/node` SSR adapter
- **React** 19.x + **Vite** 6.x
- **Tailwind CSS** 3.x
- **.NET 8** with minimal WebAPI
