import type { Entity } from '@/core/base/entities/entity.ts';

export class EntityBase implements Entity {
  private _id = '';

  get id(): string {
    return this._id;
  }

  set id(id: string) {
    this._id = id;
  }
}
