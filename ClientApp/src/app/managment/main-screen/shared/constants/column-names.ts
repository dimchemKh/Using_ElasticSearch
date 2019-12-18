export const STICKY_COLUMNS = [
  { name: 'parkName' },
  { name: 'accommName' },
  { name: 'accommBeds' },
  { name: 'accommTypeName' },
  { name: 'unitGradeName' },
  { name: 'accommPax' },
  { name: 'arrivalDateRevised' }
];

export const REPETED_COLUMNS = [
  { name: 'RackRate', viewName: 'Rack rate' },
  { name: 'CurrentFitPrice', viewName: 'Current fit price' },
  { name: 'CurrentStandardDiscount', viewName: 'Current standart discount' },
  { name: 'CurrentCampaignDiscount', viewName: 'Current campaign discount' },
  { name: 'CurrentEnhancedDiscount', viewName: 'Current enhanced discount' },
  { name: 'CurrentAdditionalDiscount', viewName: 'Current additional discount' },
  { name: 'CurrentSellingPrice', viewName: 'Current selling price' },
  { name: 'NewFitPrice', viewName: 'New Fit price' },
  { name: 'NewDiscount', viewName: 'New standard discount (editable)' },
  { name: 'NewCampaignDiscount', viewName: 'New campaign discount' },
  { name: 'NewEnhancedDiscount', viewName: 'New enhanced discount' },
  { name: 'NewAdditionalDiscount', viewName: 'New additional discount' },
  { name: 'NewSellingPrice', viewName: 'New selling price' },
  { name: 'SellingPriceChange', viewName: '% Change in selling price' },
  { name: 'PriceInclDiscretDiscount', viewName: 'Price including discretionary discounts' },
  { name: 'BlockedUnitsTotal', viewName: 'Total booked units' },
  { name: 'BlockedUnitsVarianceYoy', viewName: 'Bkg Var YoY' },
  { name: 'BlockedUnitsYoyAbvVariance', viewName: 'YoY ABV var' },
  { name: 'CurrentUnitsAvailable', viewName: 'Avail. Units' },
  { name: 'LastPriceChange', viewName: 'Last price change (%)' },
  { name: 'DaysSinceLastChange', viewName: 'Time since Last Change (d)' },
  { name: 'BookingsSinceLastChange', viewName: 'Bkgs since Last Change' },
];

export const DAY_PREFIX = [
  { prefix: 'fri' },
  { prefix: 'sat' },
  { prefix: 'mon' }
];

export const GROUP_HEADERS = [
  { name: 'sti-header', viewName: ' ', colspan: STICKY_COLUMNS.length, sticky: true },
  { name: 'fri-header', viewName: 'Friday', colspan: REPETED_COLUMNS.length, sticky: false  },
  { name: 'sat-header', viewName: 'Saturday', colspan: REPETED_COLUMNS.length, sticky: false  },
  { name: 'mon-header', viewName: 'Monday', colspan: REPETED_COLUMNS.length, sticky: false  },
];
