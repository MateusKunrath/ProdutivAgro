import { inject, injectable } from 'inversify';
import type { ProductsRepository } from '@/modules/products/domain/repositories/products-repository.ts';
import type { PagedResult } from '@/core/application/dtos/paged-result';
import type { CreatedProductDetails } from '../../application/dtos/created-product-details';
import type { ProductDetails } from '../../application/dtos/product-details';
import type { ProductInput } from '../../application/dtos/product-input';
import type { ProductsListItem } from '../../application/dtos/products-list-item';
import { DaoRest } from '@/core/api/rest/dao-rest.ts';

@injectable()
export class ProductsRestRepository implements ProductsRepository {
  private readonly collectionName = 'products';

  constructor(@inject(DaoRest) private readonly dao: DaoRest) {}

  async fetchAll(queryString?: string): Promise<PagedResult<ProductsListItem>[]> {
    return await this.dao.fetchAll(this.collectionName, queryString);
  }

  async getById(id: string): Promise<ProductDetails | undefined> {
    return await this.dao.getById(this.collectionName, id);
  }

  async create(product: ProductInput): Promise<CreatedProductDetails> {
    return await this.dao.add(this.collectionName, product);
  }

  async update(id: string, product: ProductInput): Promise<void> {
    return await this.dao.update(this.collectionName, id, product);
  }

  async delete(id: string): Promise<void> {
    await this.dao.delete(this.collectionName, id);
  }
}
