import type { PageSchema } from '../types/page-schema';

interface SectionListProps {
  schema: PageSchema;
  onUpdate: (schema: PageSchema) => void;
}

export default function SectionList({ schema, onUpdate }: SectionListProps) {
  const sections = [...schema.sections].sort((a, b) => a.order - b.order);

  const toggleVisible = (id: string) => {
    const clone = JSON.parse(JSON.stringify(schema)) as PageSchema;
    const sec = clone.sections.find(s => s.id === id);
    if (sec) sec.visible = !sec.visible;
    onUpdate(clone);
  };

  const moveSection = (id: string, dir: -1 | 1) => {
    const clone = JSON.parse(JSON.stringify(schema)) as PageSchema;
    const sorted = [...clone.sections].sort((a, b) => a.order - b.order);
    const idx = sorted.findIndex(s => s.id === id);
    const swapIdx = idx + dir;
    if (swapIdx < 0 || swapIdx >= sorted.length) return;
    const tmp = sorted[idx].order;
    sorted[idx].order = sorted[swapIdx].order;
    sorted[swapIdx].order = tmp;
    onUpdate(clone);
  };

  return (
    <div className="flex flex-col h-full overflow-hidden">
      <div className="px-4 py-3 border-b border-gray-200 bg-gray-50">
        <h2 className="text-sm font-semibold text-gray-700">Sections</h2>
      </div>
      <div className="flex-1 overflow-y-auto">
        {sections.map((section, i) => (
          <div key={section.id} className={`flex items-center gap-2 px-3 py-2 border-b border-gray-100 ${!section.visible ? 'opacity-50' : ''}`}>
            <span className="text-xs font-medium flex-1 truncate" title={section.label}>{section.label}</span>
            <button
              onClick={() => moveSection(section.id, -1)}
              disabled={i === 0}
              className="p-1 text-gray-400 hover:text-gray-600 disabled:opacity-20"
              title="Move up"
            >↑</button>
            <button
              onClick={() => moveSection(section.id, 1)}
              disabled={i === sections.length - 1}
              className="p-1 text-gray-400 hover:text-gray-600 disabled:opacity-20"
              title="Move down"
            >↓</button>
            <button
              onClick={() => toggleVisible(section.id)}
              className={`px-2 py-0.5 text-xs rounded ${section.visible ? 'bg-green-100 text-green-700' : 'bg-gray-100 text-gray-500'}`}
              title={section.visible ? 'Hide' : 'Show'}
            >
              {section.visible ? 'Visible' : 'Hidden'}
            </button>
          </div>
        ))}
      </div>
    </div>
  );
}
