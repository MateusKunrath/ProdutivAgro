import type { AxiosRequestConfiguration } from '@/core/api/http/axios-request-configuration.ts';
import type { Response } from '@/core/api/http/response.ts';

export type RequestMiddleware = (
  config: AxiosRequestConfiguration,
) => AxiosRequestConfiguration | Promise<AxiosRequestConfiguration>;

export type ResponseMiddleware = (
  response: Response<unknown>,
) => Response<unknown> | Promise<Response<unknown>>;

export class HttpConfiguration {
  baseUrl: string = '';
  headers?: { [key: string]: string };
  requestMiddleware?: Array<RequestMiddleware>;
  responseMiddleware?: Array<ResponseMiddleware>;
}
