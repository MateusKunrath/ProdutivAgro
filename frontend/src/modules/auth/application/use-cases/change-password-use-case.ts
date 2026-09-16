import { inject, injectable } from 'inversify';

import type { ChangePasswordCommand } from '@/modules/auth/application/dtos/change-password-command';
import { ChangePasswordCommandExecutor } from '@/modules/auth/infrastructure/command-executors/change-password-command-executor';

@injectable()
export class ChangePasswordUseCase {
  constructor(
    @inject(ChangePasswordCommandExecutor)
    private readonly commandExecutor: ChangePasswordCommandExecutor,
  ) {}

  async execute(command: ChangePasswordCommand): Promise<void> {
    await this.commandExecutor.changePassword(command);
  }
}
