import { useRef, useState } from 'react';
import { usePageSchema } from '../hooks/usePageSchema';
import EditorPanel from './EditorPanel';
import ThemeEditor from './ThemeEditor';
import SeoEditor from './SeoEditor';
import PreviewFrame, { type PreviewFrameHandle, type DeviceSize } from './PreviewFrame';

interface EditorProps {
  tenantId: string;
}

type Tab = 'content' | 'theme' | 'seo';

const TABS: { id: Tab; label: string }[] = [
  { id: 'content', label: 'Content' },
  { id: 'theme', label: 'Theme' },
  { id: 'seo', label: 'SEO' },
];

const DEVICES: { id: DeviceSize; label: string; icon: string }[] = [
  { id: 'desktop', label: 'Desktop', icon: '🖥' },
  { id: 'tablet', label: 'Tablet', icon: '📱' },
  { id: 'mobile', label: 'Mobile', icon: '📲' },
];

export default function Editor({ tenantId }: EditorProps) {
  const { schema, loading, saving, error, updateSchema, saveSchema } = usePageSchema(tenantId);
  const previewRef = useRef<PreviewFrameHandle>(null);
  const [tab, setTab] = useState<Tab>('content');
  const [saveSuccess, setSaveSuccess] = useState(false);
  const [deviceSize, setDeviceSize] = useState<DeviceSize>('desktop');

  const handleSave = async () => {
    await saveSchema();
    setSaveSuccess(true);
    previewRef.current?.reload();
    setTimeout(() => setSaveSuccess(false), 2000);
  };

  if (loading) {
    return (
      <div className="flex items-center justify-center h-screen bg-gray-50">
        <div className="text-center">
          <div className="animate-spin rounded-full h-10 w-10 border-b-2 border-indigo-600 mx-auto mb-3"></div>
          <p className="text-sm text-gray-500">Loading page schema…</p>
        </div>
      </div>
    );
  }

  if (error && !schema) {
    return (
      <div className="flex items-center justify-center h-screen bg-gray-50">
        <div className="text-center max-w-sm">
          <p className="text-red-600 font-medium mb-2">Failed to load page</p>
          <p className="text-sm text-gray-500">{error}</p>
          <p className="text-xs text-gray-400 mt-2">Make sure the API is running at localhost:5000</p>
        </div>
      </div>
    );
  }

  return (
    <div className="flex h-screen overflow-hidden bg-gray-100">
      {/* Left Panel */}
      <aside className="w-80 flex flex-col bg-white border-r border-gray-200 shrink-0">
        {/* Header */}
        <div className="px-4 py-3 border-b border-gray-200 bg-indigo-600 flex items-center justify-between">
          <div>
            <h1 className="text-sm font-bold text-white">LandingCraft</h1>
            <p className="text-xs text-indigo-200">{tenantId}</p>
          </div>
          <button
            onClick={handleSave}
            disabled={saving}
            className={`px-3 py-1.5 text-xs font-semibold rounded-lg transition ${
              saveSuccess ? 'bg-green-500 text-white' : 'bg-white text-indigo-600 hover:bg-indigo-50'
            } disabled:opacity-60`}
          >
            {saving ? 'Saving…' : saveSuccess ? '✓ Saved!' : 'Save'}
          </button>
        </div>

        {/* Tabs */}
        <div className="flex border-b border-gray-200">
          {TABS.map(t => (
            <button
              key={t.id}
              onClick={() => setTab(t.id)}
              className={`flex-1 py-2 text-xs font-medium transition ${
                tab === t.id
                  ? 'border-b-2 border-indigo-500 text-indigo-600'
                  : 'text-gray-500 hover:text-gray-700'
              }`}
            >
              {t.label}
            </button>
          ))}
        </div>

        {/* Tab content */}
        <div className="flex-1 overflow-hidden">
          {schema && tab === 'content' && (
            <EditorPanel schema={schema} onUpdate={updateSchema} />
          )}
          {schema && tab === 'theme' && (
            <ThemeEditor schema={schema} onUpdate={updateSchema} />
          )}
          {schema && tab === 'seo' && (
            <SeoEditor schema={schema} onUpdate={updateSchema} />
          )}
        </div>

        {/* Error banner */}
        {error && (
          <div className="px-3 py-2 bg-red-50 border-t border-red-100">
            <p className="text-xs text-red-600">{error}</p>
          </div>
        )}
      </aside>

      {/* Preview */}
      <main className="flex-1 flex flex-col overflow-hidden">
        {/* Device switcher toolbar */}
        <div className="h-11 bg-white border-b border-gray-200 flex items-center justify-between px-4">
          <span className="text-xs font-medium text-gray-500">Preview</span>
          <div className="flex items-center gap-1 bg-gray-100 rounded-lg p-1">
            {DEVICES.map(d => (
              <button
                key={d.id}
                onClick={() => setDeviceSize(d.id)}
                title={d.label}
                className={`flex items-center gap-1.5 px-3 py-1 rounded-md text-xs font-medium transition ${
                  deviceSize === d.id
                    ? 'bg-white text-indigo-600 shadow-sm'
                    : 'text-gray-500 hover:text-gray-700'
                }`}
              >
                <span>{d.icon}</span>
                <span className="hidden sm:inline">{d.label}</span>
              </button>
            ))}
          </div>
          <button
            onClick={() => previewRef.current?.reload()}
            className="text-xs text-gray-500 hover:text-gray-700 px-2 py-1 rounded hover:bg-gray-100 transition"
            title="Reload preview"
          >
            ↻ Reload
          </button>
        </div>
        <div className="flex-1 overflow-hidden">
          <PreviewFrame ref={previewRef} tenantId={tenantId} deviceSize={deviceSize} />
        </div>
      </main>
    </div>
  );
}
