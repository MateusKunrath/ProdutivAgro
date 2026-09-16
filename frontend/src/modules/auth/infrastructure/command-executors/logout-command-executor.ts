import { inject, injectable } from 'inversify';

import { CommandExecutor, HttpMethod } from '@/core/api';
import { HttpConfiguration } from '@/core/api/http/http-configuration.ts';

@injectable()
export class LogoutCommandExecutor extends CommandExecutor {
  constructor(@inject(HttpConfiguration) configuration: HttpConfiguration) {
    super(configuration);
  }

  async logout(): Promise<void> {
    await this.execute({ path: '/Auth/Logout', method: HttpMethod.POST });
  }
}
