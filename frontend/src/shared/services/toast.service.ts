import { toast, type ExternalToast } from 'vue-sonner';

export class Toast {
  static success(message: string): void {
    toast.success(message, {
      ...Toast.getToastOptions(),
    });
  }

  static error(message: string): void {
    toast.error(message, {
      ...Toast.getToastOptions(),
    });
  }

  static info(message: string): void {
    toast.info(message, {
      ...Toast.getToastOptions(),
    });
  }

  static warning(message: string): void {
    toast.warning(message, {
      ...Toast.getToastOptions(),
    });
  }

  static action(message: string, actionText: string, actionCallback: () => void): void {
    toast(message, {
      action: {
        label: actionText,
        onClick: () => actionCallback(),
      },
      ...Toast.getToastOptions(),
    });
  }

  private static getToastOptions(): ExternalToast {
    return {
      position: 'bottom-center',
      duration: 3000,
      closeButton: true,
      closeButtonPosition: 'top-right',
      richColors: true,
    };
  }
}
