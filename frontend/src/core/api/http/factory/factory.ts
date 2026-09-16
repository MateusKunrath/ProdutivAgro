export type Constructor<T> = new () => T;
export type ArrayConstructor<T> = T extends Array<T[keyof T]> ? Constructor<T[keyof T]> : undefined;
export type ArrayHandlingConstructor<T> = Constructor<T> | ArrayConstructor<T>;
