import { QueryParams } from './query-params.model';

export interface Response<T> extends QueryParams {
  items: T[];
  totalCount: number;
}
