import type { Entity } from '@/core/base/entities/entity.ts';
import { MeasurementUnit } from '@/modules/products/enums/measurement-unit';

export interface ProductDetails extends Entity {
  description: string;
  unitPrice: number;
  measurementUnit: MeasurementUnit;
  createdAt: string;
  updatedAt: string;
}
