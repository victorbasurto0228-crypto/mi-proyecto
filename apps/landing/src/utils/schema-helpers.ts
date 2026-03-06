import type { ComponentNode, PageSchema } from '../types/page-schema';

export function findNodeById(schema: PageSchema, id: string): ComponentNode | undefined {
  for (const section of schema.sections) {
    const found = searchNodes(section.components, id);
    if (found) return found;
  }
  return undefined;
}

function searchNodes(nodes: ComponentNode[], id: string): ComponentNode | undefined {
  for (const node of nodes) {
    if (node.id === id) return node;
    if (node.children) {
      const found = searchNodes(node.children, id);
      if (found) return found;
    }
  }
  return undefined;
}

export function getEditableFields(schema: PageSchema): ComponentNode[] {
  const result: ComponentNode[] = [];
  for (const section of schema.sections) {
    collectEditable(section.components, result);
  }
  return result;
}

function collectEditable(nodes: ComponentNode[], result: ComponentNode[]): void {
  for (const node of nodes) {
    if (node.editable) result.push(node);
    if (node.children) collectEditable(node.children, result);
  }
}

export function updateValue(schema: PageSchema, id: string, value: unknown): PageSchema {
  const clone = JSON.parse(JSON.stringify(schema)) as PageSchema;
  for (const section of clone.sections) {
    if (updateNodeValue(section.components, id, value)) break;
  }
  return clone;
}

function updateNodeValue(nodes: ComponentNode[], id: string, value: unknown): boolean {
  for (const node of nodes) {
    if (node.id === id) {
      node.value = value as ComponentNode['value'];
      return true;
    }
    if (node.children && updateNodeValue(node.children, id, value)) return true;
  }
  return false;
}

export function addRepeatableItem(schema: PageSchema, listNodeId: string): PageSchema {
  const clone = JSON.parse(JSON.stringify(schema)) as PageSchema;
  for (const section of clone.sections) {
    if (addItem(section.components, listNodeId)) break;
  }
  return clone;
}

function addItem(nodes: ComponentNode[], listNodeId: string): boolean {
  for (const node of nodes) {
    if (node.id === listNodeId && node.repeatable && node.repeatableTemplate) {
      const tpl = JSON.parse(JSON.stringify(node.repeatableTemplate)) as ComponentNode;
      const idx = (node.children ?? []).length;
      tpl.id = `${listNodeId}-item-${Date.now()}`;
      tpl.order = idx;
      if (!node.children) node.children = [];
      node.children.push(tpl);
      return true;
    }
    if (node.children && addItem(node.children, listNodeId)) return true;
  }
  return false;
}

export function removeRepeatableItem(schema: PageSchema, listNodeId: string, itemId: string): PageSchema {
  const clone = JSON.parse(JSON.stringify(schema)) as PageSchema;
  for (const section of clone.sections) {
    if (removeItem(section.components, listNodeId, itemId)) break;
  }
  return clone;
}

function removeItem(nodes: ComponentNode[], listNodeId: string, itemId: string): boolean {
  for (const node of nodes) {
    if (node.id === listNodeId && node.children) {
      const idx = node.children.findIndex(c => c.id === itemId);
      if (idx !== -1) {
        node.children.splice(idx, 1);
        return true;
      }
    }
    if (node.children && removeItem(node.children, listNodeId, itemId)) return true;
  }
  return false;
}
