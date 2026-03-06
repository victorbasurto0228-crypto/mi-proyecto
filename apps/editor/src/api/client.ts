const API_BASE = import.meta.env.VITE_API_URL ?? 'http://localhost:5000';

export async function fetchPage(tenantId: string) {
  const res = await fetch(`${API_BASE}/api/tenants/${tenantId}/page`);
  if (!res.ok) throw new Error(`Failed to fetch page: ${res.statusText}`);
  return res.json();
}

export async function savePage(tenantId: string, data: unknown) {
  const res = await fetch(`${API_BASE}/api/tenants/${tenantId}/page`, {
    method: 'PUT',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(data),
  });
  if (!res.ok) throw new Error(`Failed to save page: ${res.statusText}`);
  return res.json();
}

export async function fetchTenants() {
  const res = await fetch(`${API_BASE}/api/tenants`);
  if (!res.ok) throw new Error(`Failed to fetch tenants: ${res.statusText}`);
  return res.json();
}

export async function fetchTemplates() {
  const res = await fetch(`${API_BASE}/api/templates`);
  if (!res.ok) throw new Error(`Failed to fetch templates: ${res.statusText}`);
  return res.json();
}
