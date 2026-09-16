<script lang="ts" setup>
import AppSidebar from '@/components/layouts/AppSidebar.vue';
import AppHeader from '@/components/layouts/AppHeader.vue';
import { SidebarProvider } from '@/components/ui/sidebar';
</script>

<template>
  <div class="flex min-h-screen bg-background">
    <SidebarProvider>
      <AppSidebar />

      <div class="flex min-w-0 flex-1 flex-col">
        <AppHeader />

        <main class="flex-1 overflow-y-auto p-6">
          <RouterView />
        </main>
      </div>
    </SidebarProvider>
  </div>
</template>
