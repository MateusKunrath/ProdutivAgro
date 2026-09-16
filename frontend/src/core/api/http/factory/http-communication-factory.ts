import { HttpConfiguration } from '@/core/api/http/http-configuration.ts';
import { Http } from '@/core/api/http/http.ts';

export class HttpCommunicationFactory {
  static create(configuration: HttpConfiguration): Http {
    const http = new Http(configuration.baseUrl, configuration.headers);

    for (const middleware of configuration.requestMiddleware || []) {
      http.addRequestMiddleware(middleware);
    }

    for (const middleware of configuration.responseMiddleware || []) {
      http.addResponseMiddleware(middleware);
    }

    return http;
  }
}
