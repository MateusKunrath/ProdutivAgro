import { inject, injectable } from 'inversify';

import type { User } from '@/modules/auth/domain/entities/user';
import type { LoginCommand } from '@/modules/auth/application/dtos/login-command';
import { LoginCommandExecutor } from '@/modules/auth/infrastructure/command-executors/login-command-executor';
import { UserProfileCommandExecutor } from '@/modules/auth/infrastructure/command-executors/user-profile-command-executor';

@injectable()
export class LoginUseCase {
  constructor(
    @inject(LoginCommandExecutor) private readonly loginCommandExecutor: LoginCommandExecutor,
    @inject(UserProfileCommandExecutor)
    private readonly userProfileCommandExecutor: UserProfileCommandExecutor,
  ) {}

  async execute(command: LoginCommand): Promise<User> {
    await this.loginCommandExecutor.login(command);

    const user = await this.userProfileCommandExecutor.getUserProfile();
    if (!user) {
      throw new Error('Failed to retrieve user profile after login.');
    }

    return user;
  }
}
