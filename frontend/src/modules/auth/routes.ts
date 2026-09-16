import type { RouteRecordRaw } from 'vue-router';
import { AuthRoutesNames } from '@/modules/auth/enums/auth-routes-names.ts';

export const authRoutes: RouteRecordRaw = {
  path: '/auth',
  component: () => import('@/app/layouts/AuthLayout.vue'),
  meta: { guestOnly: true },
  redirect: { name: AuthRoutesNames.Login },
  children: [
    {
      path: 'login',
      name: AuthRoutesNames.Login,
      meta: {
        guestOnly: true,
      },
      component: () => import('@/modules/auth/views/LoginView.vue'),
    },
  ],
};
