import type { Entity } from '@/core/base/entities/entity.ts';

export interface WriteonlyRepository<TEntity extends Entity> {
  save(dados: TEntity): Promise<TEntity | void>;
  delete(id: string): Promise<void>;
}
