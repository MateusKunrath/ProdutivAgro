import { defineStore } from 'pinia';
import { computed, ref } from 'vue';

import { Container } from '@/core/di';

import type { LoginCommand } from '@/modules/auth/application/dtos/login-command';
import type { User } from '@/modules/auth/domain/entities/user';
import type { ChangePasswordCommand } from '@/modules/auth/application/dtos/change-password-command';
import { LoginUseCase } from '@/modules/auth/application/use-cases/login-use-case';
import { LogoutUseCase } from '@/modules/auth/application/use-cases/logout-use-case';
import { ChangePasswordUseCase } from '@/modules/auth/application/use-cases/change-password-use-case';
import { RestoreSessionUseCase } from '@/modules/auth/application/use-cases/restore-session-use-case';

export const useAuthStore = defineStore('auth', () => {
  const user = ref<User | null>(null);
  const isSessionInitialized = ref(false);
  let restoreSessionPromise: Promise<void> | null = null;

  const isAuthenticated = computed(() => user.value !== null);

  const loginUseCase = Container.get(LoginUseCase);
  const logoutUseCase = Container.get(LogoutUseCase);
  const changePasswordUseCase = Container.get(ChangePasswordUseCase);
  const restoreSessionUseCase = Container.get(RestoreSessionUseCase);

  async function login(command: LoginCommand) {
    const authenticatedUser = await loginUseCase.execute(command);
    user.value = authenticatedUser;
    isSessionInitialized.value = true;
  }

  async function logout() {
    try {
      await logoutUseCase.execute();
    } finally {
      user.value = null;
      isSessionInitialized.value = true;
    }
  }

  async function changePassword(command: ChangePasswordCommand) {
    await changePasswordUseCase.execute(command);
  }

  async function restoreSession(): Promise<void> {
    if (isSessionInitialized.value) return;

    if (!restoreSessionPromise) {
      restoreSessionPromise = (async () => {
        try {
          user.value = await restoreSessionUseCase.execute();
        } catch {
          // Cookies HttpOnly não são lidos pelo frontend. Sem confirmação do
          // backend, a aplicação falha de forma segura e considera a sessão ausente.
          user.value = null;
        } finally {
          isSessionInitialized.value = true;
        }
      })().finally(() => {
        restoreSessionPromise = null;
      });
    }

    await restoreSessionPromise;
  }

  return {
    user,
    isAuthenticated,
    isSessionInitialized,
    login,
    logout,
    changePassword,
    restoreSession,
  };
});

export type AuthStore = ReturnType<typeof useAuthStore>;
