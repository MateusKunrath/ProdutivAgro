import { inject, injectable } from 'inversify';
import axios from 'axios';

import { CommandExecutor, HttpMethod } from '@/core/api';
import { HttpConfiguration } from '@/core/api/http/http-configuration.ts';

import type { User } from '@/modules/auth/domain/entities/user';

@injectable()
export class UserProfileCommandExecutor extends CommandExecutor {
  constructor(@inject(HttpConfiguration) configuration: HttpConfiguration) {
    super(configuration);
  }

  async getUserProfile(): Promise<User | null> {
    try {
      const userProfile = await this.execute<User>({
        path: '/Users/Current',
        method: HttpMethod.GET,
      });
      return userProfile ?? null;
    } catch (error) {
      if (axios.isAxiosError(error) && error.response?.status === 401) {
        return null;
      }

      throw error;
    }
  }
}
