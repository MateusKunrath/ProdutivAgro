import type { Entity } from '@/core/base/entities/entity.ts';

export interface ReadonlyRepository<TEntity extends Entity> {
  fetchAll(queryString?: string): Promise<TEntity[]>;
  getById(id: string): Promise<TEntity | undefined>;
}
