import { defineMiddleware } from 'astro:middleware';

export const onRequest = defineMiddleware(async (context, next) => {
  const host = context.request.headers.get('host') ?? '';
  const url = new URL(context.request.url);

  // Extract subdomain from host (e.g., "demo.localhost" → "demo")
  const hostParts = host.split('.');
  let tenantId: string | undefined;

  if (hostParts.length > 1 && hostParts[0] !== 'www') {
    // Filter out common non-tenant subdomains
    const nonTenant = ['www', 'app', 'api', 'localhost'];
    if (!nonTenant.includes(hostParts[0])) {
      tenantId = hostParts[0];
    }
  }

  // Fall back to ?tenant= query param
  if (!tenantId) {
    tenantId = url.searchParams.get('tenant') ?? undefined;
  }

  context.locals.tenantId = tenantId;

  return next();
});
