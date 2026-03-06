import { useState } from 'react';
import type { ImageValue } from '../types/page-schema';

interface ImageUploaderProps {
  value: ImageValue | string | undefined;
  onChange: (v: ImageValue) => void;
}

export default function ImageUploader({ value, onChange }: ImageUploaderProps) {
  const img: ImageValue = typeof value === 'object' && value !== null && 'src' in value
    ? (value as ImageValue)
    : { src: typeof value === 'string' ? value : '', alt: '' };

  const [urlInput, setUrlInput] = useState(img.src);

  const commit = () => {
    onChange({ ...img, src: urlInput });
  };

  return (
    <div className="flex flex-col gap-2">
      <div className="flex gap-2">
        <input
          type="text"
          value={urlInput}
          onChange={e => setUrlInput(e.target.value)}
          onBlur={commit}
          placeholder="https://..."
          className="flex-1 px-2 py-1 text-xs border border-gray-200 rounded font-mono"
        />
      </div>
      <input
        type="text"
        value={img.alt}
        onChange={e => onChange({ ...img, alt: e.target.value })}
        placeholder="Alt text"
        className="px-2 py-1 text-xs border border-gray-200 rounded"
      />
      {img.src && (
        <img src={img.src} alt={img.alt} className="w-full max-h-32 object-contain rounded border border-gray-100 bg-gray-50" />
      )}
    </div>
  );
}
