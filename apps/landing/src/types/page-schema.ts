export interface PageSchema {
  id: string;
  slug: string;
  templateId: string;
  tenantId: string;
  seo: SeoMeta;
  theme: ThemeConfig;
  sections: Section[];
}

export interface SeoMeta {
  title: string;
  description: string;
  ogImage?: string;
  canonical?: string;
  noIndex?: boolean;
}

export interface ThemeConfig {
  primaryColor: string;
  secondaryColor: string;
  accentColor?: string;
  backgroundColor?: string;
  textColor?: string;
  primaryFont?: string;
  secondaryFont?: string;
}

export interface Section {
  id: string;
  type: SectionType;
  order: number;
  visible: boolean;
  label: string;
  components: ComponentNode[];
}

export type SectionType =
  | 'header' | 'hero' | 'features' | 'testimonials'
  | 'pricing' | 'faq' | 'cta' | 'gallery'
  | 'team' | 'stats' | 'contact' | 'footer' | 'custom';

export interface ComponentNode {
  id: string;
  type: ComponentType;
  order: number;
  visible: boolean;
  label?: string;
  path: string;
  editable: boolean;
  repeatable?: boolean;
  repeatableTemplate?: ComponentNode;
  minItems?: number;
  maxItems?: number;
  value?: ComponentValue;
  typography?: TypographyConfig;
  color?: ColorConfig;
  link?: LinkConfig;
  attributes?: AttributeNode[];
  children?: ComponentNode[];
  validation?: ValidationRules;
}

export type ComponentType =
  | 'text' | 'heading' | 'paragraph' | 'image' | 'video'
  | 'icon' | 'button' | 'badge' | 'richtext'
  | 'group' | 'list' | 'grid'
  | 'card' | 'feature-card' | 'testimonial-card'
  | 'pricing-card' | 'stat-card' | 'team-member'
  | 'faq-item' | 'nav-item' | 'social-link';

export type ComponentValue = string | number | boolean | ImageValue | VideoValue;

export interface ImageValue {
  src: string;
  alt: string;
  width?: number;
  height?: number;
}

export interface VideoValue {
  src: string;
  poster?: string;
  autoplay?: boolean;
  muted?: boolean;
}

export interface TypographyConfig {
  fontSize?: string;
  fontWeight?: string;
  textAlign?: 'left' | 'center' | 'right' | 'justify';
  fontFamily?: 'primary' | 'secondary';
  letterSpacing?: string;
  lineHeight?: string;
  textTransform?: 'uppercase' | 'lowercase' | 'capitalize' | 'normal';
}

export interface ColorConfig {
  text?: string;
  background?: string;
  border?: string;
}

export interface LinkConfig {
  href: string;
  target?: '_blank' | '_self';
  rel?: string;
  ariaLabel?: string;
}

export interface AttributeNode {
  id: string;
  name: string;
  type: 'string' | 'number' | 'boolean';
  value: string | number | boolean;
  editable: boolean;
  label?: string;
}

export interface ValidationRules {
  required?: boolean;
  minLength?: number;
  maxLength?: number;
  pattern?: string;
  errorMessage?: string;
  acceptedFileTypes?: string[];
  maxFileSize?: number;
}
