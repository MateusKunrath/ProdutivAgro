<script lang="ts" setup>
import 'vue-sonner/style.css';
import { Toaster } from '@/components/ui/sonner';
</script>

<template>
  <RouterView />
  <Toaster :expand="false" close-button-position="top-right" />
</template>
