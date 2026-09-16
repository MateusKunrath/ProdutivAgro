<script lang="ts" setup>
import { Container } from '@/core/di';
import { ProductsRestRepository } from '@/modules/products/infrastructure/repositories/products-rest.repository.ts';
import { onMounted } from 'vue';

const repository = Container.get(ProductsRestRepository);

onMounted(async () => {
  const products = await repository.fetchAll();
  console.log(products);
});
</script>

<template>
  <h1>Produtos</h1>
</template>
