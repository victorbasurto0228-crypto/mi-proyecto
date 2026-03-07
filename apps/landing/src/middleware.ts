import { defineMiddleware } from 'astro:middleware';

export const onRequest = defineMiddleware(async (context, next) => {
  const host = context.request.headers.get('host') ?? '';
  const url = new URL(context.request.url);

  // Extract subdomain from host (e.g., "demo.localhost" → "demo")
  // Skip IP addresses (e.g., "127.0.0.1", "10.0.0.1")
  const isIpAddress = /^[\d.]+$/.test(host.split(':')[0]);
  const hostParts = host.split('.');
  let tenantId: string | undefined;

  if (!isIpAddress && hostParts.length > 1 && hostParts[0] !== 'www') {
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
