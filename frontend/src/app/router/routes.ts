import { createRouter, createWebHistory, type RouteRecordRaw } from 'vue-router';
import { authRoutes } from '@/modules/auth/routes.ts';
import { AuthRoutesNames } from '@/modules/auth/enums/auth-routes-names.ts';
import { useAuthStore } from '@/modules/auth/stores/auth.store.ts';

const routes: RouteRecordRaw[] = [
  authRoutes,
  {
    path: '/',
    component: () => import('@/app/layouts/AppLayout.vue'),
    meta: { requiresAuth: true },
    children: [
      {
        path: '/',
        name: 'Dashboard',
        component: () => import('@/modules/dashboard/views/DashboardView.vue'),
      },
      {
        path: '/produtos',
        name: 'Produtos',
        component: () => import('@/modules/products/views/ProductsView.vue'),
      },
    ],
  },
  {
    path: '/:pathMatch(.*)*',
    redirect: { name: 'Dashboard' },
  },
];

export const router = createRouter({
  history: createWebHistory(),
  routes,
});

router.beforeEach(async (to) => {
  const authStore = useAuthStore();
  await authStore.restoreSession();

  if (to.meta.requiresAuth && !authStore.isAuthenticated) {
    return {
      name: AuthRoutesNames.Login,
      query: { redirect: to.fullPath },
    };
  }

  if (to.meta.guestOnly && authStore.isAuthenticated) {
    return { name: 'Dashboard' };
  }
});
