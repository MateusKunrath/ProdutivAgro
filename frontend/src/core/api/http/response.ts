import type { AxiosResponse, AxiosResponseHeaders, RawAxiosResponseHeaders } from 'axios';
import { AxiosRequestConfiguration } from '@/core/api/http/axios-request-configuration.ts';

export class Response<Content> {
  private constructor(
    public content: Content,
    readonly headers: RawAxiosResponseHeaders | AxiosResponseHeaders,
    readonly request: Record<string, unknown>,
    readonly configuration: AxiosRequestConfiguration,
    readonly status: number,
    readonly statusText: string,
  ) {}

  static fromAxiosResponse<ContentType>(response: AxiosResponse): Response<ContentType> {
    return new Response(
      response.data,
      response.headers,
      response.request,
      new AxiosRequestConfiguration(response.config),
      response.status,
      response.statusText,
    );
  }

  static toAxiosResponse(response: Response<unknown>): AxiosResponse {
    return {
      data: response.content,
      headers: response.headers,
      request: response.request,
      config: response.configuration.getConfiguration(),
      status: response.status,
      statusText: response.statusText,
    };
  }
}
