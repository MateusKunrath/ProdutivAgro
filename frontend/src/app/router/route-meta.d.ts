import 'vue-router';

declare module 'vue-router' {
  interface RouteMeta {
    /** Título exibido no cabeçalho da área autenticada. */
    title?: string;
    /** Título específico da aba. Use null para exibir somente a marca. */
    browserTitle?: string | null;
  }
}
