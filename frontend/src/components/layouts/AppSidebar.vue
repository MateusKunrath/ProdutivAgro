<script lang="ts" setup>
import {
  Sidebar,
  SidebarContent,
  SidebarGroup,
  SidebarGroupContent,
  SidebarGroupLabel,
  SidebarMenu,
  SidebarMenuButton,
  SidebarMenuItem,
} from '@/components/ui/sidebar';
import { Carrot, Home } from '@lucide/vue';

const menus = [
  {
    title: 'Início',
    url: '/',
    icon: Home,
  },
  {
    title: 'Produtos',
    url: '/produtos',
    icon: Carrot,
  },
];
</script>

<template>
  <Sidebar collapsible="icon">
    <SidebarContent>
      <SidebarGroup>
        <SidebarGroupLabel>Geral</SidebarGroupLabel>
        <SidebarGroupContent>
          <SidebarMenu>
            <SidebarMenuItem v-for="menu in menus" :key="menu.title">
              <RouterLink v-slot="{ href, navigate, isActive }" :to="menu.url" custom>
                <SidebarMenuButton :is-active="isActive" as-child>
                  <a :href="href" @click="navigate">
                    <component :is="menu.icon" />
                    <span>{{ menu.title }}</span>
                  </a>
                </SidebarMenuButton>
              </RouterLink>
            </SidebarMenuItem>
          </SidebarMenu>
        </SidebarGroupContent>
      </SidebarGroup>
    </SidebarContent>
  </Sidebar>
</template>

<style scoped></style>
