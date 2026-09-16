import type { HttpMethod } from '@/core/api/enums/http-method.ts';

/**
 * Descreve uma operação HTTP que não representa persistência de uma entidade.
 */
export interface HttpCommand<TBody = undefined> {
  path: string;
  method: HttpMethod;
  body?: TBody;
}
