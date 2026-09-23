<script lang="ts" setup>
import { SidebarTrigger } from '@/components/ui/sidebar';
import { Button } from '@/components/ui/button';
import { useRouter } from 'vue-router';
import { useAuthStore } from '@/modules/auth/stores/auth.store';
import { computed } from 'vue';

const authStore = useAuthStore();
const router = useRouter();
const headerTitle = computed(() => router.currentRoute.value.meta.title ?? 'ProdutivAgro');

async function logout() {
  try {
    await authStore.logout();
  } finally {
    await router.replace({ name: 'Login' });
  }
}
</script>

<template>
  <header class="flex h-16 items-center border-b px-6 gap-4">
    <SidebarTrigger />
    <h1 class="font-semibold">{{ headerTitle }}</h1>
    <Button class="ml-auto" variant="outline" @click="logout">Sair</Button>
  </header>
</template>
