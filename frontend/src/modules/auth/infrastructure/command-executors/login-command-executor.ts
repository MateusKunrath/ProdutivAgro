import { inject, injectable } from 'inversify';

import { CommandExecutor, HttpMethod } from '@/core/api';
import { HttpConfiguration } from '@/core/api/http/http-configuration.ts';

import type { LoginCommand } from '@/modules/auth/application/dtos/login-command';

@injectable()
export class LoginCommandExecutor extends CommandExecutor {
  constructor(@inject(HttpConfiguration) configuration: HttpConfiguration) {
    super(configuration);
  }

  async login(command: LoginCommand): Promise<void> {
    await this.execute({ path: '/Auth/Login', method: HttpMethod.POST, body: command });
  }
}
