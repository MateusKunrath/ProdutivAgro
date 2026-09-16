import axios, { type AxiosInstance, type AxiosResponse } from 'axios';
import { HttpMethod } from '@/core/api/enums/http-method.ts';
import { Response } from '@/core/api/http/response.ts';
import {
  type RequestMiddleware,
  type ResponseMiddleware,
} from '@/core/api/http/http-configuration.ts';
import { AxiosRequestConfiguration } from '@/core/api/http/axios-request-configuration.ts';

export class Http {
  readonly axios: AxiosInstance;

  constructor(baseUrl: string, headers: object = {}) {
    this.axios = axios.create({
      baseURL: baseUrl,
      headers,
      withCredentials: true,
    });
  }

  async request<Content>(
    url: string,
    method: HttpMethod,
    content?: unknown,
  ): Promise<Response<Content>> {
    const response = await this.executeRequest(url, method, content);
    return Response.fromAxiosResponse<Content>(response);
  }

  addHeader(header: object) {
    this.axios.defaults.headers.common = {
      ...this.axios.defaults.headers.common,
      ...header,
    };
  }

  addRequestMiddleware(middleware: RequestMiddleware): void {
    this.axios.interceptors.request.use(async (axiosConfiguration) => {
      const middlewareConfiguration: AxiosRequestConfiguration = await middleware(
        new AxiosRequestConfiguration(axiosConfiguration),
      );
      return middlewareConfiguration.getConfiguration();
    });
  }

  addResponseMiddleware(middleware: ResponseMiddleware): void {
    this.axios.interceptors.response.use(async (axiosResponse) => {
      const responseMiddleware = await middleware(Response.fromAxiosResponse(axiosResponse));
      return Response.toAxiosResponse(responseMiddleware);
    });
  }

  private async executeRequest(
    url: string,
    method: HttpMethod,
    content?: unknown,
  ): Promise<AxiosResponse> {
    return this.axios.request({
      url,
      method,
      data: content,
    });
  }
}
