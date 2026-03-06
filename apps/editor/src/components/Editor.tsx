import { useRef, useState } from 'react';
import { usePageSchema } from '../hooks/usePageSchema';
import EditorPanel from './EditorPanel';
import SectionList from './SectionList';
import PreviewFrame, { type PreviewFrameHandle } from './PreviewFrame';

interface EditorProps {
  tenantId: string;
  slug?: string;
}

type Tab = 'content' | 'sections';

export default function Editor({ tenantId, slug = 'index' }: EditorProps) {
  const { schema, loading, saving, error, updateSchema, saveSchema } = usePageSchema(tenantId, slug);
  const previewRef = useRef<PreviewFrameHandle>(null);
  const [tab, setTab] = useState<Tab>('content');
  const [saveSuccess, setSaveSuccess] = useState(false);

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
            <p className="text-xs text-indigo-200">{tenantId} / {slug}</p>
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
          {(['content', 'sections'] as const).map(t => (
            <button
              key={t}
              onClick={() => setTab(t)}
              className={`flex-1 py-2 text-xs font-medium capitalize transition ${
                tab === t ? 'border-b-2 border-indigo-500 text-indigo-600' : 'text-gray-500 hover:text-gray-700'
              }`}
            >
              {t}
            </button>
          ))}
        </div>

        {/* Tab content */}
        <div className="flex-1 overflow-hidden">
          {schema && tab === 'content' && (
            <EditorPanel schema={schema} onUpdate={updateSchema} />
          )}
          {schema && tab === 'sections' && (
            <SectionList schema={schema} onUpdate={updateSchema} />
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
        {/* Preview toolbar */}
        <div className="h-10 bg-white border-b border-gray-200 flex items-center px-4 gap-3">
          <span className="text-xs text-gray-400">Preview</span>
          <div className="flex-1 flex items-center bg-gray-100 rounded px-2 py-1">
            <span className="text-xs text-gray-500 font-mono truncate">
              http://localhost:4321?tenant={tenantId}
            </span>
          </div>
          <button
            onClick={() => previewRef.current?.reload()}
            className="text-xs text-gray-500 hover:text-gray-700 px-2 py-1 rounded hover:bg-gray-100"
            title="Reload preview"
          >
            ↻ Reload
          </button>
        </div>
        <div className="flex-1 overflow-hidden">
          <PreviewFrame ref={previewRef} tenantId={tenantId} slug={slug} />
        </div>
      </main>
    </div>
  );
}
