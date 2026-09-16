<script lang="ts" setup>
import type { HTMLAttributes } from 'vue';
import { cn } from '@/core/lib/utils';

const props = defineProps<{
  class?: HTMLAttributes['class'];
}>();
</script>

<template>
  <div :class="cn('px-6 group-data-[size=sm]/card:px-4', props.class)" data-slot="card-content">
    <slot />
  </div>
</template>
