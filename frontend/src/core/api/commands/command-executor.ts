import { HttpClient } from '@/core/api/http/http-client.ts';
import type { HttpConfiguration } from '@/core/api/http/http-configuration.ts';
import type { HttpCommand } from '@/core/api/commands/http-command.ts';

export class CommandExecutor extends HttpClient {
  constructor(configuration: HttpConfiguration) {
    super(configuration);
  }

  protected async execute<TResponse = void, TBody = undefined>(
    command: HttpCommand<TBody>,
  ): Promise<TResponse> {
    return (await this.request<TResponse>(command.path, command.method, command.body)).content;
  }
}
