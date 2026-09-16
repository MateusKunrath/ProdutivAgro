import 'reflect-metadata';
import { Container as InversifyContainer } from 'inversify';
import { HttpConfiguration } from '@/core/api/http/http-configuration.ts';
import { DaoRest } from '@/core/api/rest/dao-rest.ts';

const httpConfiguration = new HttpConfiguration();
httpConfiguration.baseUrl = import.meta.env.VITE_API_URL;

export const Container = new InversifyContainer({ autobind: true });

Container.bind(HttpConfiguration).toConstantValue(httpConfiguration);
Container.bind(DaoRest).toSelf().inSingletonScope();
