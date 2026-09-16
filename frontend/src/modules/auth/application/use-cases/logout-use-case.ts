import { inject, injectable } from 'inversify';

import { LogoutCommandExecutor } from '@/modules/auth/infrastructure/command-executors/logout-command-executor';

@injectable()
export class LogoutUseCase {
  constructor(@inject(LogoutCommandExecutor) private readonly commandExecutor: LogoutCommandExecutor) {}

  async execute(): Promise<void> {
    await this.commandExecutor.logout();
  }
}
