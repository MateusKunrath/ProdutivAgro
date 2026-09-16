import type { InternalAxiosRequestConfig } from 'axios';
import { HttpMethod } from '@/core/api/enums/http-method.ts';

export class AxiosRequestConfiguration {
  constructor(private readonly configuration: InternalAxiosRequestConfig) {}

  getUrl(): string {
    return this.configuration.url!;
  }

  getData(): unknown {
    return this.configuration.data;
  }

  getMethod(): HttpMethod {
    return this.configuration.method as HttpMethod;
  }

  getConfiguration(): InternalAxiosRequestConfig {
    return this.configuration;
  }
}
