import type { Entity } from '@/core/base/entities/entity.ts';

export interface ProductsListItem extends Entity {
  description: string;
  unitPrice: number;
  active: boolean;
  createdAt: string;
  updatedAt: string;
}
