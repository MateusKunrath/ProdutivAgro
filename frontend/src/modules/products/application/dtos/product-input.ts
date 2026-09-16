import type { MeasurementUnit } from '@/modules/products/enums/measurement-unit';

export interface ProductInput {
  description: string;
  unitPrice: number;
  measurementUnit: MeasurementUnit;
}
