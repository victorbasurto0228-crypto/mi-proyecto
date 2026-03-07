import { useState, useRef, useImperativeHandle, forwardRef } from 'react';

export interface PreviewFrameHandle {
  reload: () => void;
}

interface PreviewFrameProps {
  tenantId: string;
  previewBase?: string;
}

const PreviewFrame = forwardRef<PreviewFrameHandle, PreviewFrameProps>(
  ({ tenantId, previewBase = 'http://localhost:4321' }, ref) => {
    const [reloadKey, setReloadKey] = useState(0);
    const iframeRef = useRef<HTMLIFrameElement>(null);
    const previewBaseRef = useRef(previewBase);
    previewBaseRef.current = previewBase;

    useImperativeHandle(ref, () => ({
      reload: () => {
        // Send RELOAD postMessage to the current iframe as a secondary reload mechanism
        try {
          const origin = new URL(previewBaseRef.current).origin;
          iframeRef.current?.contentWindow?.postMessage({ type: 'RELOAD' }, origin);
        } catch {
          // ignore invalid URL
        }
        // Increment key to force React to fully remount the iframe (primary reload mechanism)
        setReloadKey(k => k + 1);
      },
    }));

    const src = `${previewBase}/preview?tenant=${encodeURIComponent(tenantId)}&t=${reloadKey}`;

    return (
      <iframe
        key={reloadKey}
        ref={iframeRef}
        src={src}
        title="Landing Page Preview"
        className="w-full h-full border-0"
      />
    );
  }
);

PreviewFrame.displayName = 'PreviewFrame';

export default PreviewFrame;
