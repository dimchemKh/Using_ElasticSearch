using Nest;
using System.Linq;
using Using_Elastic.DataAccess.Entities;
using Using_Elasticsearch.Common.Models;
using Using_Elasticsearch.Common.Views.MainScreen.Request;

namespace Using_Elasticsearch.BusinessLogic.Helpers
{
    public static class ElasticHelper
    {
        private static readonly string Key = "keyword";
        public static string StringToLower(string word)
        {
            return word.Substring(0, 1).ToLower() + word.Substring(1);
        }
        public static AggregationContainerDescriptor<WebAppData> TermsInit(this AggregationContainerDescriptor<WebAppData> aggregation, RequestGetFiltersMainScreenView filters)
        {
            var word = StringToLower(filters.CurrentFilter.ToString());

            var dataProperty = typeof(WebAppData).GetProperty(filters.CurrentFilter.ToString());

            var type = dataProperty.PropertyType;

            var field = type.IsValueType ? word : word + $".{Key}";

            aggregation.Terms(word, t => t.Field(field).Size(filters.Size).Order(o => o.KeyAscending()));

            return aggregation;
        }
        public static QueryContainerDescriptor<WebAppData> SearchQuery(this QueryContainerDescriptor<WebAppData> query, FiltersModel filter)
        {
            query.Bool(x => x
                    .Must(
                          m => m.Terms(t => t.Field(f => f.HolidayYear).Terms(filter.HolidayYear)),
                          m => m.Terms(t => t.Field(f => f.WeekNumber).Terms(filter.WeekNumber)),
                          m => m.Terms(t => t.Field(f => f.RegionName.Suffix(Key)).Terms(filter.RegionName)),
                          m => m.Term(t => t.Field(f => f.ResponsibleRevenueManager.Suffix(Key)).Value(filter.ResponsibleRevenueManager.FirstOrDefault())),
                          m => m.Terms(t => t.Field(f => f.ParkName.Suffix(Key)).Terms(filter.ParkName)),
                          m => m.Terms(t => t.Field(f => f.AccommTypeName.Suffix(Key)).Terms(filter.AccommTypeName)),
                          m => m.Terms(t => t.Field(f => f.AccommBeds).Terms(filter.AccommBeds)),
                          m => m.Terms(t => t.Field(f => f.AccommName.Suffix(Key)).Terms(filter.AccommName)),
                          m => m.Terms(t => t.Field(f => f.UnitGradeName.Suffix(Key)).Terms(filter.UnitGradeName)),
                          m => m.Terms(t => t.Field(f => f.KeyPeriodName.Suffix(Key)).Terms(filter.KeyPeriodName)),
                          m => m.TermRange(t => t.Field(f => f.ParkWeekOccupancy).GreaterThanOrEquals(filter.GreaterParkWeekOccupancy).LessThanOrEquals(filter.LessParkWeekOccupancy))
                        ));

            return query;
        }
        public static SortDescriptor<WebAppData> SortByPredicate(this SortDescriptor<WebAppData> sortDescriptor)
        {
            // TODO sorting_table

            return sortDescriptor;
        }
    }
}
