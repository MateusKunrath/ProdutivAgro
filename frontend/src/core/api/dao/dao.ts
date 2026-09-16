export interface Dao {
  fetchAll<TResponse>(collectionName: string, queryString?: string): Promise<TResponse[]>;
  getById<TResponse>(collectionName: string, identifier: string): Promise<TResponse | undefined>;
  add<TInput, TResponse>(collectionName: string, entity: TInput): Promise<TResponse>;
  update<TInput>(collectionName: string, identifier: string, entity: TInput): Promise<void>;
  delete(collectionName: string, identifier: string): Promise<void>;
}
