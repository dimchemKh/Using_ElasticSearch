import { FiltersModel } from 'src/app/shared/models/filters-model';

export class RequestGetFiltersMainScreenView {
  currentFilter: number;
  size: number;

  public filters: FiltersModel;

  constructor() {
    this.reset();
  }

  reset(): void {
    this.filters = new FiltersModel();
    this.filters.holidayYear = [];
    this.filters.weekNumber = [];
    this.filters.keyPeriodName = [];
    this.filters.regionName = [null];
    this.filters.parkName = [null];
    this.filters.accommName = [null];
    this.filters.accommTypeName = [null];
    this.filters.accommBeds = [null];
    this.filters.unitGradeName = [null];
    this.filters.responsibleRevenueManager = [null];
  }
}
