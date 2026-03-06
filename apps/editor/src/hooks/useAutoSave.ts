import { useEffect, useRef, useState } from 'react';
import type { PageSchema } from '../types/page-schema';

type AutoSaveStatus = 'idle' | 'saving' | 'saved' | 'error';

interface UseAutoSaveResult {
  status: AutoSaveStatus;
  lastSaved: Date | null;
}

export function useAutoSave(
  pageSchema: PageSchema | null,
  saveFn: () => Promise<void>,
  delay: number = 2000
): UseAutoSaveResult {
  const [status, setStatus] = useState<AutoSaveStatus>('idle');
  const [lastSaved, setLastSaved] = useState<Date | null>(null);
  const timerRef = useRef<ReturnType<typeof setTimeout> | null>(null);
  const isFirstRender = useRef(true);
  // Keep a stable ref so the timeout always calls the latest version of saveFn
  const saveFnRef = useRef(saveFn);
  saveFnRef.current = saveFn;

  useEffect(() => {
    // Skip the initial render – don't auto-save on mount
    if (isFirstRender.current) {
      isFirstRender.current = false;
      return;
    }

    if (!pageSchema) return;

    // Clear any pending timer
    if (timerRef.current) clearTimeout(timerRef.current);

    timerRef.current = setTimeout(async () => {
      setStatus('saving');
      try {
        await saveFnRef.current();
        setStatus('saved');
        setLastSaved(new Date());
      } catch {
        setStatus('error');
      }
    }, delay);

    return () => {
      if (timerRef.current) clearTimeout(timerRef.current);
    };
  // saveFn is excluded intentionally – we use saveFnRef to always call the latest version
  // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [pageSchema, delay]);

  return { status, lastSaved };
}
