import type { TypographyConfig } from '../types/page-schema';

interface TypographyEditorProps {
  value: TypographyConfig;
  onChange: (v: TypographyConfig) => void;
}

const fontSizes = ['xs', 'sm', 'base', 'lg', 'xl', '2xl', '3xl', '4xl', '5xl', '6xl'];
const fontWeights = ['thin', 'light', 'normal', 'medium', 'semibold', 'bold', 'extrabold'];
const textAligns = ['left', 'center', 'right', 'justify'] as const;
const textTransforms = ['normal', 'uppercase', 'lowercase', 'capitalize'] as const;

export default function TypographyEditor({ value, onChange }: TypographyEditorProps) {
  const update = (partial: Partial<TypographyConfig>) => onChange({ ...value, ...partial });
  return (
    <div className="flex flex-col gap-2">
      <div className="flex gap-2 items-center">
        <label className="text-xs text-gray-500 w-20 shrink-0">Size</label>
        <select value={value.fontSize ?? ''} onChange={e => update({ fontSize: e.target.value || undefined })} className="flex-1 px-2 py-1 text-xs border border-gray-200 rounded">
          <option value="">—</option>
          {fontSizes.map(s => <option key={s} value={s}>{s}</option>)}
        </select>
      </div>
      <div className="flex gap-2 items-center">
        <label className="text-xs text-gray-500 w-20 shrink-0">Weight</label>
        <select value={value.fontWeight ?? ''} onChange={e => update({ fontWeight: e.target.value || undefined })} className="flex-1 px-2 py-1 text-xs border border-gray-200 rounded">
          <option value="">—</option>
          {fontWeights.map(w => <option key={w} value={w}>{w}</option>)}
        </select>
      </div>
      <div className="flex gap-2 items-center">
        <label className="text-xs text-gray-500 w-20 shrink-0">Align</label>
        <select value={value.textAlign ?? ''} onChange={e => update({ textAlign: (e.target.value as typeof textAligns[number]) || undefined })} className="flex-1 px-2 py-1 text-xs border border-gray-200 rounded">
          <option value="">—</option>
          {textAligns.map(a => <option key={a} value={a}>{a}</option>)}
        </select>
      </div>
      <div className="flex gap-2 items-center">
        <label className="text-xs text-gray-500 w-20 shrink-0">Transform</label>
        <select value={value.textTransform ?? ''} onChange={e => update({ textTransform: (e.target.value as typeof textTransforms[number]) || undefined })} className="flex-1 px-2 py-1 text-xs border border-gray-200 rounded">
          <option value="">—</option>
          {textTransforms.map(t => <option key={t} value={t}>{t}</option>)}
        </select>
      </div>
    </div>
  );
}
