import type { HttpConfiguration } from '@/core/api/http/http-configuration.ts';
import { HttpCommunicationFactory } from '@/core/api/http/factory/http-communication-factory.ts';
import type { Http } from '@/core/api/http/http.ts';
import { HttpMethod } from '@/core/api/enums/http-method.ts';

export class HttpClient {
  private http: Http;

  constructor(configuration: HttpConfiguration) {
    configuration.headers = {
      'Content-Type': 'Application/json',
      ...configuration.headers,
    };
    this.http = HttpCommunicationFactory.create(configuration);
  }

  protected request<Response>(path: string, method: HttpMethod, command: unknown) {
    return this.http.request<Response>(path, method, command);
  }
}
