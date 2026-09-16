import type { ReadonlyRepository } from '@/core/api/repositories/interfaces/readonly-repository.ts';
import type { Entity } from '@/core/base/entities/entity.ts';
import type { Dao } from '@/core/api/dao/dao.ts';

export abstract class ReadonlyRepositoryBase<
  TEntity extends Entity,
> implements ReadonlyRepository<TEntity> {
  protected constructor(
    protected readonly dao: Dao,
    protected readonly collectionName: string,
  ) {}
  async fetchAll(queryString?: string): Promise<TEntity[]> {
    return await this.dao.fetchAll(this.collectionName, queryString);
  }

  async getById(id: string): Promise<TEntity | undefined> {
    return await this.dao.getById(this.collectionName, id);
  }
}
