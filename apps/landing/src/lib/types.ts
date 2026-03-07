export type TemplateId = 'generic-saas' | 'restaurante-uno' | 'organizador-eventos' | 'default';

export const KNOWN_TEMPLATES: readonly TemplateId[] = [
  'generic-saas',
  'restaurante-uno',
  'organizador-eventos',
];

export function resolveTemplateId(templateId: string | undefined): TemplateId {
  if (templateId && (KNOWN_TEMPLATES as readonly string[]).includes(templateId)) {
    return templateId as TemplateId;
  }
  return 'generic-saas';
}
