import type { PageSchema } from '../types/page-schema';

interface CacheEntry {
  data: PageSchema;
  expiresAt: number;
}

const cache = new Map<string, CacheEntry>();
const TTL_MS = 5 * 60 * 1000; // 5-minute TTL

export function invalidateTenantCache(tenantId: string): void {
  cache.delete(tenantId);
}

export async function resolveTenantPage(tenantId: string): Promise<PageSchema | null> {
  const now = Date.now();
  const cached = cache.get(tenantId);
  if (cached && cached.expiresAt > now) {
    return cached.data;
  }

  const apiBase = import.meta.env.API_URL ?? 'http://localhost:5000';
  try {
    const res = await fetch(`${apiBase}/api/tenants/${tenantId}/page`, {
      headers: { Accept: 'application/json' },
    });
    if (!res.ok) return null;
    const data = (await res.json()) as PageSchema;
    cache.set(tenantId, { data, expiresAt: now + TTL_MS });
    return data;
  } catch {
    return null;
  }
}
