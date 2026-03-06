import type { PageSchema } from '../types/page-schema';
import { getEditableFields } from '../utils/schema-helpers';
import FieldEditor from './FieldEditor';

interface EditorPanelProps {
  schema: PageSchema;
  onUpdate: (schema: PageSchema) => void;
}

export default function EditorPanel({ schema, onUpdate }: EditorPanelProps) {
  const editableFields = getEditableFields(schema);

  // Group by section
  const sectionMap: Record<string, { label: string; fields: typeof editableFields }> = {};
  for (const section of schema.sections) {
    if (!section.visible) continue;
    const fields = editableFields.filter(f => f.path.startsWith(section.id + '.') || f.path.startsWith(section.type + '.'));
    if (fields.length > 0) {
      sectionMap[section.id] = { label: section.label, fields };
    }
  }
  // Any ungrouped fields
  const groupedIds = new Set(Object.values(sectionMap).flatMap(g => g.fields.map(f => f.id)));
  const ungrouped = editableFields.filter(f => !groupedIds.has(f.id));

  return (
    <div className="flex flex-col h-full overflow-hidden">
      <div className="px-4 py-3 border-b border-gray-200 bg-gray-50">
        <h2 className="text-sm font-semibold text-gray-700">Edit Content</h2>
        <p className="text-xs text-gray-400 mt-0.5">{editableFields.length} editable fields</p>
      </div>
      <div className="flex-1 overflow-y-auto px-3 py-2">
        {ungrouped.length > 0 && (
          <div className="mb-4">
            {ungrouped.map(f => (
              <FieldEditor key={f.id} node={f} schema={schema} onUpdate={onUpdate} />
            ))}
          </div>
        )}
        {Object.entries(sectionMap).map(([id, group]) => (
          <details key={id} open className="mb-3">
            <summary className="text-xs font-semibold text-gray-600 uppercase tracking-wide cursor-pointer py-1 px-1 rounded hover:bg-gray-100">
              {group.label}
            </summary>
            <div className="mt-1 pl-1">
              {group.fields.map(f => (
                <FieldEditor key={f.id} node={f} schema={schema} onUpdate={onUpdate} />
              ))}
            </div>
          </details>
        ))}
      </div>
    </div>
  );
}
