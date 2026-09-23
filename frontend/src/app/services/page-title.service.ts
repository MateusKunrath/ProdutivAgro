import type { RouteLocationNormalizedLoaded } from 'vue-router';

const applicationName = 'ProdutivAgro';

/** Mantém o título da aba sincronizado com os metadados da rota atual. */
export class PageTitleService {
  static update(route: RouteLocationNormalizedLoaded) {
    const browserTitle =
      route.meta.browserTitle === undefined ? route.meta.title : route.meta.browserTitle;
    document.title = browserTitle
      ? `${browserTitle} - ${applicationName}`
      : applicationName;
  }
}
