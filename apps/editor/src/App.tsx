import { useState } from 'react';
import Editor from './components/Editor';

export default function App() {
  const [tenantId, setTenantId] = useState('demo');
  const [slug, setSlug] = useState('index');
  const [started, setStarted] = useState(false);

  if (started) {
    return <Editor tenantId={tenantId} slug={slug} />;
  }

  return (
    <div className="min-h-screen bg-gradient-to-br from-indigo-50 to-purple-50 flex items-center justify-center p-4">
      <div className="bg-white rounded-2xl shadow-xl p-8 w-full max-w-sm">
        <div className="text-center mb-8">
          <span className="text-4xl">✏️</span>
          <h1 className="text-2xl font-bold text-gray-900 mt-2">LandingCraft Editor</h1>
          <p className="text-sm text-gray-500 mt-1">Edit your tenant landing page</p>
        </div>
        <div className="flex flex-col gap-4">
          <div>
            <label className="text-xs font-medium text-gray-600 block mb-1">Tenant ID</label>
            <input
              type="text"
              value={tenantId}
              onChange={e => setTenantId(e.target.value)}
              placeholder="demo"
              className="w-full px-3 py-2 border border-gray-200 rounded-lg text-sm focus:outline-none focus:ring-2 focus:ring-indigo-300"
            />
          </div>
          <div>
            <label className="text-xs font-medium text-gray-600 block mb-1">Page Slug</label>
            <input
              type="text"
              value={slug}
              onChange={e => setSlug(e.target.value)}
              placeholder="index"
              className="w-full px-3 py-2 border border-gray-200 rounded-lg text-sm focus:outline-none focus:ring-2 focus:ring-indigo-300"
            />
          </div>
          <button
            onClick={() => setStarted(true)}
            disabled={!tenantId.trim()}
            className="w-full py-2.5 rounded-xl bg-indigo-600 text-white font-semibold text-sm hover:bg-indigo-700 transition disabled:opacity-50"
          >
            Open Editor →
          </button>
          <p className="text-xs text-gray-400 text-center">
            Make sure the API is running at <span className="font-mono">localhost:5000</span>
          </p>
        </div>
      </div>
    </div>
  );
}
