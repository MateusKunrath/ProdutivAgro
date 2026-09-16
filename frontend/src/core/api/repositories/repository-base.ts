import type { WriteonlyRepository } from '@/core/api/repositories/interfaces/writeonly-repository.ts';
import { ReadonlyRepositoryBase } from '@/core/api/repositories/readonly-repository-base.ts';
import type { Dao } from '@/core/api/dao/dao.ts';
import type { Entity } from '@/core/base/entities/entity.ts';

export class RepositoryBase<TEntity extends Entity>
  extends ReadonlyRepositoryBase<TEntity>
  implements WriteonlyRepository<TEntity>
{
  constructor(dao: Dao, collectionName: string) {
    super(dao, collectionName);
  }

  async save(entity: TEntity): Promise<TEntity | void> {
    if (entity.id !== '') {
      return await this.update(entity);
    }
    return await this.add(entity);
  }

  async delete(id: string): Promise<void> {
    await this.dao.delete(this.collectionName, id);
  }

  private async update(entity: TEntity): Promise<void> {
    await this.dao.update(this.collectionName, entity);
  }

  private async add(entity: TEntity): Promise<TEntity> {
    return (await this.dao.add(this.collectionName, entity)) as TEntity;
  }
}
