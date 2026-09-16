import { inject, injectable } from 'inversify';

import { CommandExecutor, HttpMethod } from '@/core/api';
import { HttpConfiguration } from '@/core/api/http/http-configuration.ts';

import type { ChangePasswordCommand } from '@/modules/auth/application/dtos/change-password-command';

@injectable()
export class ChangePasswordCommandExecutor extends CommandExecutor {
  constructor(@inject(HttpConfiguration) configuration: HttpConfiguration) {
    super(configuration);
  }

  async changePassword(command: ChangePasswordCommand): Promise<void> {
    await this.execute({ path: '/Users/ChangePassword', method: HttpMethod.POST, body: command });
  }
}
