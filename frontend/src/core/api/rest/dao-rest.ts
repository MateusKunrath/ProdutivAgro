import { inject, injectable } from 'inversify';
import { HttpClient } from '@/core/api/http/http-client.ts';
import { HttpConfiguration } from '@/core/api/http/http-configuration.ts';
import type { Dao } from '@/core/api/dao/dao.ts';
import { HttpMethod } from '@/core/api/enums/http-method.ts';

@injectable()
export class DaoRest extends HttpClient implements Dao {
  constructor(@inject(HttpConfiguration) configuration: HttpConfiguration) {
    super(configuration);
  }

  async add<TInput, TResponse>(collectionName: string, entity: TInput): Promise<TResponse> {
    return (await this.request<TResponse>(collectionName, HttpMethod.POST, entity)).content;
  }

  async update<TInput>(collectionName: string, identifier: string, entity: TInput): Promise<void> {
    await this.request(`${collectionName}/${identifier}`, HttpMethod.PUT, entity);
  }

  async delete(collectionName: string, identifier: string): Promise<void> {
    await this.request(`${collectionName}/${identifier}`, HttpMethod.DELETE, {});
  }

  async fetchAll<TResponse>(collectionName: string, queryString = ''): Promise<TResponse[]> {
    const url = queryString !== '' ? `${collectionName}?${queryString}` : collectionName;
    return (await this.request<TResponse[]>(url, HttpMethod.GET, {})).content;
  }

  async getById<TResponse>(collectionName: string, identifier: string): Promise<TResponse> {
    return (await this.request<TResponse>(`${collectionName}/${identifier}`, HttpMethod.GET, {}))
      .content;
  }
}
