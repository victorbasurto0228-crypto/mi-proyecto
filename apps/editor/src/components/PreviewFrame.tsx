import { useRef, useImperativeHandle, forwardRef } from 'react';

export interface PreviewFrameHandle {
  reload: () => void;
}

interface PreviewFrameProps {
  tenantId: string;
  slug?: string;
  previewBase?: string;
}

const PreviewFrame = forwardRef<PreviewFrameHandle, PreviewFrameProps>(
  ({ tenantId, slug = 'index', previewBase = 'http://localhost:4321' }, ref) => {
    const iframeRef = useRef<HTMLIFrameElement>(null);

    useImperativeHandle(ref, () => ({
      reload: () => {
        if (iframeRef.current?.contentWindow) {
          iframeRef.current.contentWindow.location.reload();
        } else if (iframeRef.current) {
          iframeRef.current.src = iframeRef.current.src;
        }
      },
    }));

    const src = slug === 'index'
      ? `${previewBase}?tenant=${encodeURIComponent(tenantId)}`
      : `${previewBase}/${slug}?tenant=${encodeURIComponent(tenantId)}`;

    return (
      <iframe
        ref={iframeRef}
        src={src}
        title="Landing Page Preview"
        className="w-full h-full border-0"
        sandbox="allow-scripts allow-same-origin allow-forms allow-popups"
      />
    );
  }
);

PreviewFrame.displayName = 'PreviewFrame';

export default PreviewFrame;
