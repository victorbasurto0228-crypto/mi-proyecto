import type { ComponentNode, ImageValue } from '../types/page-schema';
import { updateValue } from '../utils/schema-helpers';
import type { PageSchema } from '../types/page-schema';
import ColorPicker from './ColorPicker';
import TypographyEditor from './TypographyEditor';
import ImageUploader from './ImageUploader';

interface FieldEditorProps {
  node: ComponentNode;
  schema: PageSchema;
  onUpdate: (schema: PageSchema) => void;
}

const TEXT_TYPES = ['text', 'heading', 'paragraph', 'badge', 'icon'];
const BUTTON_TYPES = ['button', 'nav-item', 'social-link'];

export default function FieldEditor({ node, schema, onUpdate }: FieldEditorProps) {
  const handleValueChange = (val: unknown) => {
    onUpdate(updateValue(schema, node.id, val));
  };

  const stringVal = typeof node.value === 'string' ? node.value
    : typeof node.value === 'number' || typeof node.value === 'boolean' ? String(node.value)
    : '';

  return (
    <div className="flex flex-col gap-2 py-2 border-b border-gray-100 last:border-0">
      <div className="flex items-center gap-1">
        <span className="text-xs font-medium text-gray-700 flex-1">{node.label ?? node.id}</span>
        <span className="text-[10px] text-gray-400 bg-gray-100 px-1 rounded">{node.type}</span>
      </div>

      {/* Text / heading / paragraph / badge / icon */}
      {TEXT_TYPES.includes(node.type) && (
        node.type === 'paragraph' || (typeof stringVal === 'string' && stringVal.length > 80) ? (
          <textarea
            value={stringVal}
            onChange={e => handleValueChange(e.target.value)}
            rows={3}
            className="w-full px-2 py-1 text-xs border border-gray-200 rounded resize-y"
          />
        ) : (
          <input
            type="text"
            value={stringVal}
            onChange={e => handleValueChange(e.target.value)}
            className="w-full px-2 py-1 text-xs border border-gray-200 rounded"
          />
        )
      )}

      {/* Button / nav-item / social-link – also edits href */}
      {BUTTON_TYPES.includes(node.type) && (
        <div className="flex flex-col gap-1">
          <input
            type="text"
            value={stringVal}
            onChange={e => handleValueChange(e.target.value)}
            placeholder="Label"
            className="w-full px-2 py-1 text-xs border border-gray-200 rounded"
          />
        </div>
      )}

      {/* Image */}
      {node.type === 'image' && (
        <ImageUploader
          value={node.value as ImageValue | string | undefined}
          onChange={handleValueChange}
        />
      )}

      {/* List/grid node with plain text value (e.g. pricing features) */}
      {node.type === 'list' && typeof node.value === 'string' && (
        <textarea
          value={stringVal}
          onChange={e => handleValueChange(e.target.value)}
          rows={4}
          className="w-full px-2 py-1 text-xs border border-gray-200 rounded resize-y font-mono"
          placeholder="One item per line"
        />
      )}

      {/* Typography */}
      {node.typography && (
        <details className="mt-1">
          <summary className="text-xs text-gray-400 cursor-pointer">Typography</summary>
          <div className="mt-1 pl-2 border-l border-gray-100">
            <TypographyEditor
              value={node.typography}
              onChange={typo => {
                const clone = JSON.parse(JSON.stringify(schema)) as PageSchema;
                const found = findNode(clone.sections.flatMap(s => s.components), node.id);
                if (found) found.typography = typo;
                onUpdate(clone);
              }}
            />
          </div>
        </details>
      )}

      {/* Color */}
      {node.color && (
        <details className="mt-1">
          <summary className="text-xs text-gray-400 cursor-pointer">Color</summary>
          <div className="mt-1 pl-2 border-l border-gray-100 flex flex-col gap-1">
            <ColorPicker label="Text" value={node.color.text ?? ''} onChange={c => {
              const clone = JSON.parse(JSON.stringify(schema)) as PageSchema;
              const found = findNode(clone.sections.flatMap(s => s.components), node.id);
              if (found?.color) found.color.text = c;
              onUpdate(clone);
            }} />
            <ColorPicker label="Background" value={node.color.background ?? ''} onChange={c => {
              const clone = JSON.parse(JSON.stringify(schema)) as PageSchema;
              const found = findNode(clone.sections.flatMap(s => s.components), node.id);
              if (found?.color) found.color.background = c;
              onUpdate(clone);
            }} />
          </div>
        </details>
      )}
    </div>
  );
}

function findNode(nodes: ComponentNode[], id: string): ComponentNode | undefined {
  for (const node of nodes) {
    if (node.id === id) return node;
    if (node.children) {
      const found = findNode(node.children, id);
      if (found) return found;
    }
  }
  return undefined;
}
