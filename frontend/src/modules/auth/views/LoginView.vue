<script lang="ts" setup>
import { reactive, ref } from 'vue';
import { useRouter } from 'vue-router';
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
  CardFooter,
} from '@/components/ui/card';
import { Button } from '@/components/ui/button';
import { Label } from '@/components/ui/label';
import { Input } from '@/components/ui/input';

import { useAuthStore } from '@/modules/auth/stores/auth.store';
import type { LoginCommand } from '@/modules/auth/application/dtos/login-command';
import Separator from '@/components/ui/separator/Separator.vue';
import { Toast } from '@/shared/services/toast.service';
import {
  Carousel,
  CarouselContent,
  CarouselItem,
  CarouselNext,
  CarouselPrevious,
} from '@/components/ui/carousel';

import cultiveImg from '@/assets/cultive.svg';
import financeImg from '@/assets/finance.svg';

const authStore = useAuthStore();
const router = useRouter();

const isSubmitting = ref(false);

const loginCommand = reactive<LoginCommand>({
  email: '',
  password: '',
});

async function submit() {
  try {
    isSubmitting.value = true;

    await authStore.login({ email: loginCommand.email, password: loginCommand.password });
    const redirect = router.currentRoute.value.query.redirect;
    await router.replace(typeof redirect === 'string' ? redirect : { name: 'Dashboard' });
  } catch {
    Toast.error('Ocorreu um erro ao tentar fazer login.');
  } finally {
    isSubmitting.value = false;
  }
}
</script>

<template>
  <main class="flex h-screen w-full">
    <div class="bg-primary-foreground w-full h-full flex p-16 items-center justify-center">
      <Carousel class="w-full max-w-3xl">
        <CarouselContent>
          <CarouselItem>
            <div class="flex aspect-square rounded p-8">
              <img :src="financeImg" alt="Finance image" />
            </div>
          </CarouselItem>
          <CarouselItem>
            <div class="flex aspect-square rounded p-8">
              <img :src="cultiveImg" alt="Cultive image" />
            </div>
          </CarouselItem>
        </CarouselContent>
        <CarouselPrevious />
        <CarouselNext />
      </Carousel>
    </div>
    <section class="flex bg-background h-full max-w-3xl w-full p-4 items-center justify-center">
      <Card class="w-full max-w-md">
        <CardHeader class="justify-center items-center">
          <CardTitle class="text-2xl font-bold tracking-tighter text-center">
            Bem-vindo de volta
          </CardTitle>
          <CardDescription>Utilize seu e-mail para acessar sua conta</CardDescription>
        </CardHeader>

        <CardContent>
          <form @submit.prevent="submit">
            <div>
              <Label for="email" class="mb-1">E-mail</Label>
              <Input
                id="email"
                v-model="loginCommand.email"
                placeholder="exemplo@email.com"
                type="email"
              />
            </div>
            <div class="mt-4">
              <Label for="password" class="mb-1">Senha</Label>
              <Input
                v-model="loginCommand.password"
                id="password"
                placeholder="Sua senha"
                type="password"
              />
            </div>
            <Button class="mt-6 w-full">Entrar</Button>
            <div class="flex items-center gap-6 mt-4">
              <Separator />
              <span class="text-xs text-muted-foreground">ou</span>
              <Separator />
            </div>
            <Button
              variant="outline"
              class="mt-4 w-full"
              type="button"
              @click="() => router.push('/register')"
            >
              Criar conta
            </Button>
          </form>
        </CardContent>
        <CardFooter>
          <p class="text-muted-foreground text-center text-sm">
            Ao entrar em nossa plataforma você concorda com nossos termos de serviço e política de
            privacidade.
          </p>
        </CardFooter>
      </Card>
    </section>
  </main>
</template>
