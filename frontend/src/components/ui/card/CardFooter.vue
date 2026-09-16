<script lang="ts" setup>
import type { HTMLAttributes } from 'vue';
import { cn } from '@/core/lib/utils';

const props = defineProps<{
  class?: HTMLAttributes['class'];
}>();
</script>

<template>
  <div
    :class="
      cn(
        'rounded-b-xl px-6 group-data-[size=sm]/card:px-4 [.border-t]:pt-6 group-data-[size=sm]/card:[.border-t]:pt-4 flex items-center',
        props.class,
      )
    "
    data-slot="card-footer"
  >
    <slot />
  </div>
</template>
