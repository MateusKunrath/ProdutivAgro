import { inject, injectable } from 'inversify';

import type { User } from '@/modules/auth/domain/entities/user';
import { UserProfileCommandExecutor } from '@/modules/auth/infrastructure/command-executors/user-profile-command-executor';

@injectable()
export class RestoreSessionUseCase {
  constructor(
    @inject(UserProfileCommandExecutor)
    private readonly userProfileCommandExecutor: UserProfileCommandExecutor,
  ) {}

  async execute(): Promise<User | null> {
    return this.userProfileCommandExecutor.getUserProfile();
  }
}
