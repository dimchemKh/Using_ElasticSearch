import { BasePaginationFilter } from 'src/app/shared/models/base/base-pagination-filter';

export class RequestFilterParametersMainScreen extends BasePaginationFilter {
  public columnName: string;
  public minValue: number;
  public maxValue: number;
  public values: Array<string>;
}
