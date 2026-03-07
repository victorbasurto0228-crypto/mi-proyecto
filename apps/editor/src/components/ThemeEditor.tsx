import type { PageSchema, ThemeConfig } from '../types/page-schema';

interface ThemeEditorProps {
  schema: PageSchema;
  onUpdate: (schema: PageSchema) => void;
}

interface ColorFieldProps {
  label: string;
  value: string | undefined;
  onChange: (value: string) => void;
}

function ColorField({ label, value, onChange }: ColorFieldProps) {
  const safeValue = value || '#000000';
  return (
    <div className="flex items-center gap-3 py-2">
      <label className="text-xs text-gray-600 flex-1 truncate">{label}</label>
      <div className="flex items-center gap-2">
        <input
          type="color"
          value={safeValue}
          onChange={e => onChange(e.target.value)}
          className="w-8 h-8 rounded cursor-pointer border border-gray-200 p-0.5 bg-white"
          title={safeValue}
        />
        <input
          type="text"
          value={safeValue}
          onChange={e => onChange(e.target.value)}
          className="w-20 text-xs font-mono border border-gray-200 rounded px-1.5 py-1 focus:outline-none focus:ring-1 focus:ring-indigo-400"
          placeholder="#000000"
          maxLength={7}
        />
      </div>
    </div>
  );
}

interface FontFieldProps {
  label: string;
  value: string | undefined;
  onChange: (value: string) => void;
}

function FontField({ label, value, onChange }: FontFieldProps) {
  return (
    <div className="flex items-center gap-3 py-2">
      <label className="text-xs text-gray-600 flex-1 truncate">{label}</label>
      <input
        type="text"
        value={value || ''}
        onChange={e => onChange(e.target.value)}
        className="w-32 text-xs border border-gray-200 rounded px-2 py-1 focus:outline-none focus:ring-1 focus:ring-indigo-400"
        placeholder="e.g. Inter"
      />
    </div>
  );
}

export default function ThemeEditor({ schema, onUpdate }: ThemeEditorProps) {
  const theme = schema.theme;

  const update = (patch: Partial<ThemeConfig>) => {
    onUpdate({ ...schema, theme: { ...theme, ...patch } });
  };

  return (
    <div className="flex flex-col h-full overflow-hidden">
      <div className="px-4 py-3 border-b border-gray-200 bg-gray-50">
        <h2 className="text-sm font-semibold text-gray-700">Theme & Colors</h2>
        <p className="text-xs text-gray-400 mt-0.5">Customize your brand colors and fonts</p>
      </div>
      <div className="flex-1 overflow-y-auto px-4 py-2">
        <div className="mb-4">
          <p className="text-xs font-semibold text-gray-500 uppercase tracking-wide mb-1">Colors</p>
          <div className="divide-y divide-gray-100">
            <ColorField
              label="Primary Color"
              value={theme.primaryColor}
              onChange={v => update({ primaryColor: v })}
            />
            <ColorField
              label="Secondary Color"
              value={theme.secondaryColor}
              onChange={v => update({ secondaryColor: v })}
            />
            <ColorField
              label="Accent Color"
              value={theme.accentColor}
              onChange={v => update({ accentColor: v })}
            />
            <ColorField
              label="Background Color"
              value={theme.backgroundColor}
              onChange={v => update({ backgroundColor: v })}
            />
            <ColorField
              label="Text Color"
              value={theme.textColor}
              onChange={v => update({ textColor: v })}
            />
          </div>
        </div>
        <div>
          <p className="text-xs font-semibold text-gray-500 uppercase tracking-wide mb-1">Fonts</p>
          <div className="divide-y divide-gray-100">
            <FontField
              label="Primary Font"
              value={theme.primaryFont}
              onChange={v => update({ primaryFont: v })}
            />
            <FontField
              label="Secondary Font"
              value={theme.secondaryFont}
              onChange={v => update({ secondaryFont: v })}
            />
          </div>
        </div>
      </div>
    </div>
  );
}
