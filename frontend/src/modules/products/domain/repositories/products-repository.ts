import type { PagedResult } from '@/core/application/dtos/paged-result.ts';
import type { ProductsListItem } from '@/modules/products/application/dtos/products-list-item.ts';
import type { ProductDetails } from '@/modules/products/application/dtos/product-details.ts';
import type { ProductInput } from '@/modules/products/application/dtos/product-input.ts';
import type { CreatedProductDetails } from '@/modules/products/application/dtos/created-product-details.ts';

export interface ProductsRepository {
  fetchAll(queryString?: string): Promise<PagedResult<ProductsListItem>[]>;
  getById(id: string): Promise<ProductDetails | undefined>;
  create(product: ProductInput): Promise<CreatedProductDetails>;
  update(id: string, product: ProductInput): Promise<void>;
  delete(id: string): Promise<void>;
}
