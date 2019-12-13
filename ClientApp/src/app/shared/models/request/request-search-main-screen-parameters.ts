import { FiltersModel } from 'src/app/shared/models/filters-model';

export class RequestSearchMainScreenParameters {
  from: number;
  size: number;
  public filters: FiltersModel;
}
