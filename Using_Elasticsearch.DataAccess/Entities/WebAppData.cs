using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Using_Elasticsearch.DataAccess.Entities
{
    [Table("SKP_Web_App_Data")]
    public class WebAppData
    {
        [Key]
        [Column("rec_id")]
        public int RecId { get; set; }

        [Column("profile_type_id")]
        public short? ProfileTypeId { get; set; }

        [Column("holiday_year")]
        public int? HolidayYear { get; set; }

        [Column("week_number")]
        public int? WeekNumber { get; set; }

        [Column("key_period_name")]
        public string KeyPeriodName { get; set; }

        [Column("region_name")]
        public string RegionName { get; set; }

        [Column("park_name")]
        public string ParkName { get; set; }

        [Column("accomm_name")]
        public string AccommName { get; set; }

        [Column("accomm_type_name")]
        public string AccommTypeName { get; set; }

        [Column("accomm_beds")]
        public int? AccommBeds { get; set; }

        [Column("accomm_pax")]
        public int? AccommPax { get; set; }

        [Column("unit_grade_name")]
        public string UnitGradeName { get; set; }

        [Column("arrival_date_revised")]
        public DateTime? ArrivalDateRevised { get; set; }

        [Column("responsible_revenue_manager")]
        public string ResponsibleRevenueManager { get; set; }

        [Column("park_prioritisation_score")]
        public decimal? ParkPrioritisationScore { get; set; }

        [Column("park_nights_target_deviation")]
        public decimal? ParkNightsTargetDeviation { get; set; }

        [Column("park_week_occupancy")]
        public decimal? ParkWeekOccupancy { get; set; }

        [Column("unit_grade_prioritisation_score")]
        public decimal? UnitGradePrioritisationScore { get; set; }

        [Column("unit_grade_nights_target_deviation")]
        public decimal? UnitGradeNightsTargetDeviation { get; set; }

        [Column("unit_grade_week_occupancy")]
        public decimal? UnitGradeWeekOccupancy { get; set; }

        [Column("unit_grade_recency_of_last_sale")]
        public int? UnitGradeRecencyOfLastSale { get; set; }

        [Column("accomm_week_occupancy")]
        public decimal? AccommWeekOccupancy { get; set; }

        [Column("all_not_aproved")]
        public bool AllNotAproved { get; set; }


        //------FRIDAY---------
        [Column("fri_rack_rate")]
        public decimal? FriRackRate { get; set; }

        [Column("fri_current_fit_price")]
        public decimal? FriCurrentFitPrice { get; set; }

        [Column("fri_current_standard_discount")]
        public decimal? FriCurrentStandardDiscount { get; set; }

        [Column("fri_current_enhanced_discount")]
        public decimal? FriCurrentEnhancedDiscount { get; set; }

        [Column("fri_current_campaign_discount")]
        public decimal? FriCurrentCampaignDiscount { get; set; }

        [Column("fri_current_additional_discount")]
        public decimal? FriCurrentAdditionalDiscount { get; set; }

        [Column("fri_current_selling_price")]
        public decimal? FriCurrentSellingPrice { get; set; }

        [Column("fri_recommended_fit_price")]
        public decimal? FriRecommendedFitPrice { get; set; }

        [Column("fri_new_fit_price")]
        public decimal? FriNewFitPrice { get; set; }

        [Column("fri_recommended_discount")]
        public decimal? FriRecommendedDiscount { get; set; }

        [Column("fri_new_discount")]
        public decimal? FriNewDiscount { get; set; }

        [Column("fri_recommended_selling_price")]
        public decimal? FriRecommendedSellingPrice { get; set; }

        [Column("fri_new_enhanced_discount")]
        public decimal? FriNewEnhancedDiscount { get; set; }

        [Column("fri_new_campaign_discount")]
        public decimal? FriNewCampaignDiscount { get; set; }

        [Column("fri_new_additional_discount")]
        public decimal? FriNewAdditionalDiscount { get; set; }

        [Column("fri_new_selling_price")]
        public decimal? FriNewSellingPrice { get; set; }

        [Column("fri_selling_price_change")]
        public decimal? FriSellingPriceChange { get; set; }

        [Column("fri_price_change_status")]
        public short? FriPriceChangeStatus { get; set; }

        [Column("fri_price_incl_discret_discount")]
        public decimal? FriPriceInclDiscretDiscount { get; set; }

        [Column("fri_booked_units_total")]
        public int? FriBookedUnitsTotal { get; set; }

        [Column("fri_booked_units_variance_yoy")]
        public int? FriBookedUnitsVarianceYoy { get; set; }

        [Column("fri_booked_units_yoy_abv_variance")]
        public decimal? FriBookedUnitsYoyAbvVariance { get; set; }

        [Column("fri_current_units_available")]
        public int? FriCurrentUnitsAvailable { get; set; }

        [Column("fri_last_price_change")]
        public decimal? FriLastPriceChange { get; set; }

        [Column("fri_days_since_last_change")]
        public int? FriDaysSinceLastChange { get; set; }

        [Column("fri_bookings_since_last_change")]
        public int? FriBookingsSinceLastChange { get; set; }

        [Column("fri_minimum_price")]
        public decimal? FriMinimumPrice { get; set; }

        [Column("fri_updated_by")]
        public string FriUpdatedBy { get; set; }


        //--SATURDAY---
        [Column("sat_rack_rate")]
        public decimal? SatRackRate { get; set; }

        [Column("sat_current_fit_price")]
        public decimal? SatCurrentFitPrice { get; set; }

        [Column("sat_current_standard_discount")]
        public decimal? SatCurrentStandardDiscount { get; set; }

        [Column("sat_current_enhanced_discount")]
        public decimal? SatCurrentEnhancedDiscount { get; set; }

        [Column("sat_current_campaign_discount")]
        public decimal? SatCurrentCampaignDiscount { get; set; }

        [Column("sat_current_additional_discount")]
        public decimal? SatCurrentAdditionalDiscount { get; set; }

        [Column("sat_current_selling_price")]
        public decimal? SatCurrentSellingPrice { get; set; }

        [Column("sat_recommended_fit_price")]
        public decimal? SatRecommendedFitPrice { get; set; }

        [Column("sat_new_fit_price")]
        public decimal? SatNewFitPrice { get; set; }

        [Column("sat_recommended_discount")]
        public decimal? SatRecommendedDiscount { get; set; }

        [Column("sat_new_discount")]
        public decimal? SatNewDiscount { get; set; }

        [Column("sat_recommended_selling_price")]
        public decimal? SatRecommendedSellingPrice { get; set; }

        [Column("sat_new_enhanced_discount")]
        public decimal? SatNewEnhancedDiscount { get; set; }

        [Column("sat_new_campaign_discount")]
        public decimal? SatNewCampaignDiscount { get; set; }

        [Column("sat_new_additional_discount")]
        public decimal? SatNewAdditionalDiscount { get; set; }

        [Column("sat_new_selling_price")]
        public decimal? SatNewSellingPrice { get; set; }

        [Column("sat_selling_price_change")]
        public decimal? SatSellingPriceChange { get; set; }

        [Column("sat_price_change_status")]
        public short? SatPriceChangeStatus { get; set; }

        [Column("sat_price_incl_discret_discount")]
        public decimal? SatPriceInclDiscretDiscount { get; set; }

        [Column("sat_booked_units_total")]
        public int? SatBookedUnitsTotal { get; set; }

        [Column("sat_booked_units_variance_yoy")]
        public int? SatBookedUnitsVarianceYoy { get; set; }

        [Column("sat_booked_units_yoy_abv_variance")]
        public decimal? SatBookedUnitsYoyAbvVariance { get; set; }

        [Column("sat_current_units_available")]
        public int? SatCurrentUnitsAvailable { get; set; }

        [Column("sat_last_price_change")]
        public decimal? SatLastPriceChange { get; set; }

        [Column("sat_days_since_last_change")]
        public int? SatDaysSinceLastChange { get; set; }

        [Column("sat_bookings_since_last_change")]
        public int? SatBookingsSinceLastChange { get; set; }

        [Column("sat_minimum_price")]
        public decimal? SatMinimumPrice { get; set; }

        [Column("sat_updated_by")]
        public string SatUpdatedBy { get; set; }


        //----MONDAY---------
        [Column("mon_rack_rate")]
        public decimal? MonRackRate { get; set; }

        [Column("mon_current_fit_price")]
        public decimal? MonCurrentFitPrice { get; set; }

        [Column("mon_current_standard_discount")]
        public decimal? MonCurrentStandardDiscount { get; set; }

        [Column("mon_current_enhanced_discount")]
        public decimal? MonCurrentEnhancedDiscount { get; set; }

        [Column("mon_current_campaign_discount")]
        public decimal? MonCurrentCampaignDiscount { get; set; }

        [Column("mon_current_additional_discount")]
        public decimal? MonCurrentAdditionalDiscount { get; set; }

        [Column("mon_current_selling_price")]
        public decimal? MonCurrentSellingPrice { get; set; }

        [Column("mon_recommended_fit_price")]
        public decimal? MonRecommendedFitPrice { get; set; }

        [Column("mon_new_fit_price")]
        public decimal? MonNewFitPrice { get; set; }

        [Column("mon_recommended_discount")]
        public decimal? MonRecommendedDiscount { get; set; }

        [Column("mon_new_discount")]
        public decimal? MonNewDiscount { get; set; }

        [Column("mon_recommended_selling_price")]
        public decimal? MonRecommendedSellingPrice { get; set; }

        [Column("mon_new_enhanced_discount")]
        public decimal? MonNewEnhancedDiscount { get; set; }

        [Column("mon_new_campaign_discount")]
        public decimal? MonNewCampaignDiscount { get; set; }

        [Column("mon_new_additional_discount")]
        public decimal? MonNewAdditionalDiscount { get; set; }

        [Column("mon_new_selling_price")]
        public decimal? MonNewSellingPrice { get; set; }

        [Column("mon_selling_price_change")]
        public decimal? MonSellingPriceChange { get; set; }

        [Column("mon_price_change_status")]
        public short? MonPriceChangeStatus { get; set; }

        [Column("mon_price_incl_discret_discount")]
        public decimal? MonPriceInclDiscretDiscount { get; set; }

        [Column("mon_booked_units_total")]
        public int? MonBookedUnitsTotal { get; set; }

        [Column("mon_booked_units_variance_yoy")]
        public int? MonBookedUnitsVarianceYoy { get; set; }

        [Column("mon_booked_units_yoy_abv_variance")]
        public decimal? MonBookedUnitsYoyAbvVariance { get; set; }

        [Column("mon_current_units_available")]
        public int? MonCurrentUnitsAvailable { get; set; }

        [Column("mon_last_price_change")]
        public decimal? MonLastPriceChange { get; set; }

        [Column("mon_days_since_last_change")]
        public int? MonDaysSinceLastChange { get; set; }

        [Column("mon_bookings_since_last_change")]
        public int? MonBookingsSinceLastChange { get; set; }

        [Column("mon_minimum_price")]
        public decimal? MonMinimumPrice { get; set; }

        [Column("mon_updated_by")]
        public string MonUpdatedBy { get; set; }
    }
}