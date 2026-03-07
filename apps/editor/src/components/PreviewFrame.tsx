import { useState, useRef, useImperativeHandle, forwardRef } from 'react';

export type DeviceSize = 'desktop' | 'tablet' | 'mobile';

export interface PreviewFrameHandle {
  reload: () => void;
}

interface PreviewFrameProps {
  tenantId: string;
  previewBase?: string;
  deviceSize?: DeviceSize;
}

const DEVICE_WIDTHS: Record<DeviceSize, number | undefined> = {
  desktop: undefined,
  tablet: 768,
  mobile: 375,
};

const PreviewFrame = forwardRef<PreviewFrameHandle, PreviewFrameProps>(
  ({ tenantId, previewBase = 'http://localhost:4321', deviceSize = 'desktop' }, ref) => {
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
    const frameWidth = DEVICE_WIDTHS[deviceSize];
    const isConstrained = frameWidth !== undefined;

    return (
      <div className="w-full h-full flex items-start justify-center overflow-auto bg-gray-200 p-6">
        <div
          className={`relative bg-white shadow-2xl rounded-lg overflow-hidden transition-all duration-300 ${
            isConstrained ? 'h-full' : 'w-full h-full'
          }`}
          style={isConstrained ? { width: frameWidth, minHeight: '100%' } : undefined}
        >
          {isConstrained && (
            <div className="absolute top-0 left-0 right-0 h-6 bg-gray-800 flex items-center justify-center z-10 rounded-t-lg">
              <span className="w-10 h-1 bg-gray-600 rounded-full" />
            </div>
          )}
          <iframe
            key={reloadKey}
            ref={iframeRef}
            src={src}
            title="Landing Page Preview"
            className={`border-0 w-full ${isConstrained ? 'mt-6' : ''}`}
            style={{ height: isConstrained ? 'calc(100% - 1.5rem)' : '100%' }}
          />
        </div>
      </div>
    );
  }
);

PreviewFrame.displayName = 'PreviewFrame';

export default PreviewFrame;
