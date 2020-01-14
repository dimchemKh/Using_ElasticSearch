using System;
using System.Collections.Generic;
using Using_Elasticsearch.DataAccess.Entities;

namespace Using_Elasticsearch.Common.Views.MainScreen.Response
{
    public class ResponseSearchMainScreenView
    {
        public int TotalCount { get; set; }
        public IEnumerable<WebAppData> Items { get; set; }
        public ResponseSearchMainScreenView()
        {
            Items = new List<WebAppData>();
        }
    }
    public class ResponseSearchMainScreenViewItem
    {
        public string DocId { get; set; }
        public int RecId { get; set; }
        public short? ProfileTypeId { get; set; }
        public int? HolidayYear { get; set; }
        public int? WeekNumber { get; set; }
        public string KeyPeriodName { get; set; }
        public string RegionName { get; set; }
        public string ParkName { get; set; }
        public string AccommName { get; set; }
        public string AccommTypeName { get; set; }
        public int? AccommBeds { get; set; }
        public int? AccommPax { get; set; }
        public string UnitGradeName { get; set; }
        public DateTime? ArrivalDateRevised { get; set; }
        public string ResponsibleRevenueManager { get; set; }
        public decimal? ParkPrioritisationScore { get; set; }
        public decimal? ParkNightsTargetDeviation { get; set; }
        public decimal? ParkWeekOccupancy { get; set; }
        public decimal? UnitGradePrioritisationScore { get; set; }
        public decimal? UnitGradeNightsTargetDeviation { get; set; }
        public decimal? UnitGradeWeekOccupancy { get; set; }
        public int? UnitGradeRecencyOfLastSale { get; set; }
        public decimal? AccommWeekOccupancy { get; set; }

        //--------------FRIDAY----------------------
        public decimal? FriRackRate { get; set; }
        public decimal? FriCurrentFitPrice { get; set; }
        public decimal? FriCurrentStandardDiscount { get; set; }
        public decimal? FriCurrentEnhancedDiscount { get; set; }
        public decimal? FriCurrentCampaignDiscount { get; set; }
        public decimal? FriCurrentAdditionalDiscount { get; set; }
        public decimal? FriCurrentSellingPrice { get; set; }
        public decimal? FriRecommendedFitPrice { get; set; }
        public decimal? FriNewFitPrice { get; set; }
        public decimal? FriRecommendedDiscount { get; set; }
        public decimal? FriNewDiscount { get; set; }
        public decimal? FriRecommendedSellingPrice { get; set; }
        public decimal? FriNewEnhancedDiscount { get; set; }
        public decimal? FriNewCampaignDiscount { get; set; }
        public decimal? FriNewAdditionalDiscount { get; set; }
        public decimal? FriNewSellingPrice { get; set; }
        public decimal? FriSellingPriceChange { get; set; }
        public short? FriPriceChangeStatus { get; set; }
        public decimal? FriPriceInclDiscretDiscount { get; set; }
        public int? FriBookedUnitsTotal { get; set; }
        public int? FriBookedUnitsVarianceYoy { get; set; }
        public decimal? FriBookedUnitsYoyAbvVariance { get; set; }
        public int? FriCurrentUnitsAvailable { get; set; }
        public decimal? FriLastPriceChange { get; set; }
        public int? FriDaysSinceLastChange { get; set; }
        public int? FriBookingsSinceLastChange { get; set; }
        public decimal? FriMinimumPrice { get; set; }
        public string FriUpdatedBy { get; set; }

        //--------------SATURDAY----------------------
        public decimal? SatRackRate { get; set; }
        public decimal? SatCurrentFitPrice { get; set; }
        public decimal? SatCurrentStandardDiscount { get; set; }
        public decimal? SatCurrentEnhancedDiscount { get; set; }
        public decimal? SatCurrentCampaignDiscount { get; set; }
        public decimal? SatCurrentAdditionalDiscount { get; set; }
        public decimal? SatCurrentSellingPrice { get; set; }
        public decimal? SatRecommendedFitPrice { get; set; }
        public decimal? SatNewFitPrice { get; set; }
        public decimal? SatRecommendedDiscount { get; set; }
        public decimal? SatNewDiscount { get; set; }
        public decimal? SatRecommendedSellingPrice { get; set; }
        public decimal? SatNewEnhancedDiscount { get; set; }
        public decimal? SatNewCampaignDiscount { get; set; }
        public decimal? SatNewAdditionalDiscount { get; set; }
        public decimal? SatNewSellingPrice { get; set; }
        public decimal? SatSellingPriceChange { get; set; }
        public short? SatPriceChangeStatus { get; set; }
        public decimal? SatPriceInclDiscretDiscount { get; set; }
        public int? SatBookedUnitsTotal { get; set; }
        public int? SatBookedUnitsVarianceYoy { get; set; }
        public decimal? SatBookedUnitsYoyAbvVariance { get; set; }
        public int? SatCurrentUnitsAvailable { get; set; }
        public decimal? SatLastPriceChange { get; set; }
        public int? SatDaysSinceLastChange { get; set; }
        public int? SatBookingsSinceLastChange { get; set; }
        public decimal? SatMinimumPrice { get; set; }
        public string SatUpdatedBy { get; set; }

        //--------------MONDAY----------------------
        public decimal? MonRackRate { get; set; }
        public decimal? MonCurrentFitPrice { get; set; }
        public decimal? MonCurrentStandardDiscount { get; set; }
        public decimal? MonCurrentEnhancedDiscount { get; set; }
        public decimal? MonCurrentCampaignDiscount { get; set; }
        public decimal? MonCurrentAdditionalDiscount { get; set; }
        public decimal? MonCurrentSellingPrice { get; set; }
        public decimal? MonRecommendedFitPrice { get; set; }
        public decimal? MonNewFitPrice { get; set; }
        public decimal? MonRecommendedDiscount { get; set; }
        public decimal? MonNewDiscount { get; set; }
        public decimal? MonRecommendedSellingPrice { get; set; }
        public decimal? MonNewEnhancedDiscount { get; set; }
        public decimal? MonNewCampaignDiscount { get; set; }
        public decimal? MonNewAdditionalDiscount { get; set; }
        public decimal? MonNewSellingPrice { get; set; }
        public decimal? MonSellingPriceChange { get; set; }
        public short? MonPriceChangeStatus { get; set; }
        public decimal? MonPriceInclDiscretDiscount { get; set; }
        public int? MonBookedUnitsTotal { get; set; }
        public int? MonBookedUnitsVarianceYoy { get; set; }
        public decimal? MonBookedUnitsYoyAbvVariance { get; set; }
        public int? MonCurrentUnitsAvailable { get; set; }
        public decimal? MonLastPriceChange { get; set; }
        public int? MonDaysSinceLastChange { get; set; }
        public int? MonBookingsSinceLastChange { get; set; }
        public decimal? MonMinimumPrice { get; set; }
        public string MonUpdatedBy { get; set; }

    }
}
