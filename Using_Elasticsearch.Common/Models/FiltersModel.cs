using System;
using System.Collections.Generic;
using System.Text;

namespace Using_Elasticsearch.Common.Models
{
    public class FiltersModel
    {
        public IEnumerable<string> HolidayYear { get; set; }
        public IEnumerable<string> WeekNumber { get; set; }
        public IEnumerable<string> RegionName { get; set; }
        public IEnumerable<string> ParkName { get; set; }
        public IEnumerable<string> AccommTypeName { get; set; }
        public IEnumerable<string> AccommBeds { get; set; }
        public IEnumerable<string> AccommName { get; set; }
        public IEnumerable<string> UnitGradeName { get; set; }
        public IEnumerable<string> KeyPeriodName { get; set; }
        public IEnumerable<string> ResponsibleRevenueManager { get; set; }
        public string GreaterParkWeekOccupancy { get; set; }
        public string LessParkWeekOccupancy { get; set; }
    }
}
