import type { PageSchema, SeoMeta } from '../types/page-schema';

interface SeoEditorProps {
  schema: PageSchema;
  onUpdate: (schema: PageSchema) => void;
}

export default function SeoEditor({ schema, onUpdate }: SeoEditorProps) {
  const seo = schema.seo;

  const update = (patch: Partial<SeoMeta>) => {
    onUpdate({ ...schema, seo: { ...seo, ...patch } });
  };

  return (
    <div className="flex flex-col h-full overflow-hidden">
      <div className="px-4 py-3 border-b border-gray-200 bg-gray-50">
        <h2 className="text-sm font-semibold text-gray-700">SEO</h2>
        <p className="text-xs text-gray-400 mt-0.5">Optimize for search engines and social sharing</p>
      </div>
      <div className="flex-1 overflow-y-auto px-4 py-4 space-y-4">
        <div>
          <label className="block text-xs font-medium text-gray-600 mb-1">Page Title</label>
          <input
            type="text"
            value={seo.title}
            onChange={e => update({ title: e.target.value })}
            className="w-full text-sm border border-gray-200 rounded-lg px-3 py-2 focus:outline-none focus:ring-2 focus:ring-indigo-400"
            placeholder="My Awesome Page"
          />
          <p className="text-xs text-gray-400 mt-1">{seo.title.length} / 60 chars recommended</p>
        </div>

        <div>
          <label className="block text-xs font-medium text-gray-600 mb-1">Description</label>
          <textarea
            value={seo.description}
            onChange={e => update({ description: e.target.value })}
            rows={3}
            className="w-full text-sm border border-gray-200 rounded-lg px-3 py-2 focus:outline-none focus:ring-2 focus:ring-indigo-400 resize-none"
            placeholder="A brief description of this page…"
          />
          <p className="text-xs text-gray-400 mt-1">{seo.description.length} / 160 chars recommended</p>
        </div>

        <div>
          <label className="block text-xs font-medium text-gray-600 mb-1">OG Image URL</label>
          <input
            type="url"
            value={seo.ogImage || ''}
            onChange={e => update({ ogImage: e.target.value || undefined })}
            className="w-full text-sm border border-gray-200 rounded-lg px-3 py-2 focus:outline-none focus:ring-2 focus:ring-indigo-400"
            placeholder="https://example.com/og-image.png"
          />
          <p className="text-xs text-gray-400 mt-1">Recommended size: 1200×630 px</p>
        </div>

        {seo.ogImage && (
          <div className="rounded-lg overflow-hidden border border-gray-200">
            <img
              src={seo.ogImage}
              alt="OG preview"
              className="w-full h-32 object-cover"
              onError={e => { (e.target as HTMLImageElement).style.display = 'none'; }}
            />
          </div>
        )}

        <div>
          <label className="block text-xs font-medium text-gray-600 mb-1">Canonical URL</label>
          <input
            type="url"
            value={seo.canonical || ''}
            onChange={e => update({ canonical: e.target.value || undefined })}
            className="w-full text-sm border border-gray-200 rounded-lg px-3 py-2 focus:outline-none focus:ring-2 focus:ring-indigo-400"
            placeholder="https://example.com/page"
          />
        </div>

        <div className="flex items-center gap-3">
          <input
            id="noindex"
            type="checkbox"
            checked={seo.noIndex ?? false}
            onChange={e => update({ noIndex: e.target.checked || undefined })}
            className="w-4 h-4 rounded border-gray-300 text-indigo-600 focus:ring-indigo-500"
          />
          <label htmlFor="noindex" className="text-xs text-gray-600">
            No Index (hide from search engines)
          </label>
        </div>
      </div>
    </div>
  );
}
