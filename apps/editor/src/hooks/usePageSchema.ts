import { useState, useEffect, useCallback } from 'react';
import type { PageSchema } from '../types/page-schema';
import { fetchPage, savePage } from '../api/client';

interface UsePageSchemaResult {
  schema: PageSchema | null;
  loading: boolean;
  saving: boolean;
  error: string | null;
  updateSchema: (schema: PageSchema) => void;
  saveSchema: () => Promise<void>;
}

export function usePageSchema(tenantId: string): UsePageSchemaResult {
  const [schema, setSchema] = useState<PageSchema | null>(null);
  const [loading, setLoading] = useState(true);
  const [saving, setSaving] = useState(false);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    setLoading(true);
    setError(null);
    fetchPage(tenantId)
      .then(setSchema)
      .catch(e => setError(e.message))
      .finally(() => setLoading(false));
  }, [tenantId]);

  const updateSchema = useCallback((updated: PageSchema) => {
    setSchema(updated);
  }, []);

  const saveSchema = useCallback(async () => {
    if (!schema) return;
    setSaving(true);
    setError(null);
    try {
      const saved = await savePage(tenantId, schema);
      setSchema(saved);
    } catch (e) {
      setError((e as Error).message);
    } finally {
      setSaving(false);
    }
  }, [schema, tenantId]);

  return { schema, loading, saving, error, updateSchema, saveSchema };
}
