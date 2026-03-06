import type { TypographyConfig, ColorConfig } from '../types/page-schema';

const fontSizeMap: Record<string, string> = {
  xs: 'text-xs',
  sm: 'text-sm',
  base: 'text-base',
  lg: 'text-lg',
  xl: 'text-xl',
  '2xl': 'text-2xl',
  '3xl': 'text-3xl',
  '4xl': 'text-4xl',
  '5xl': 'text-5xl',
  '6xl': 'text-6xl',
};

const fontWeightMap: Record<string, string> = {
  thin: 'font-thin',
  light: 'font-light',
  normal: 'font-normal',
  medium: 'font-medium',
  semibold: 'font-semibold',
  bold: 'font-bold',
  extrabold: 'font-extrabold',
};

const textAlignMap: Record<string, string> = {
  left: 'text-left',
  center: 'text-center',
  right: 'text-right',
  justify: 'text-justify',
};

const textTransformMap: Record<string, string> = {
  uppercase: 'uppercase',
  lowercase: 'lowercase',
  capitalize: 'capitalize',
  normal: 'normal-case',
};

export function typographyToClasses(typography?: TypographyConfig): string {
  if (!typography) return '';
  const classes: string[] = [];
  if (typography.fontSize) classes.push(fontSizeMap[typography.fontSize] ?? `text-[${typography.fontSize}]`);
  if (typography.fontWeight) classes.push(fontWeightMap[typography.fontWeight] ?? `font-[${typography.fontWeight}]`);
  if (typography.textAlign) classes.push(textAlignMap[typography.textAlign] ?? '');
  if (typography.textTransform) classes.push(textTransformMap[typography.textTransform] ?? '');
  if (typography.letterSpacing) classes.push(`tracking-[${typography.letterSpacing}]`);
  if (typography.lineHeight) classes.push(`leading-[${typography.lineHeight}]`);
  return classes.filter(Boolean).join(' ');
}

export function colorToInlineStyle(color?: ColorConfig): string {
  if (!color) return '';
  const styles: string[] = [];
  if (color.text) styles.push(`color: ${color.text}`);
  if (color.background) styles.push(`background-color: ${color.background}`);
  if (color.border) styles.push(`border-color: ${color.border}`);
  return styles.join('; ');
}
