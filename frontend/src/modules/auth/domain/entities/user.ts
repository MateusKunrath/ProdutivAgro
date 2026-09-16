import type { Entity } from '@/core/base/entities/entity';
import type { UserRole } from '@/modules/auth/enums/user-role';
import type { UserStatus } from '@/modules/auth/enums/user-status';

export interface User extends Entity {
  name: string;
  email: string;
  role: UserRole;
  status: UserStatus;
}
