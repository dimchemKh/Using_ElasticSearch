using Using_Elasticsearch.Common.Enums;
using Using_Elasticsearch.Common.Models;

namespace Using_Elasticsearch.Common.Views.MainScreen.Request
{
    public class RequestGetFiltersMainScreenView
    {
        public FilterName CurrentFilter { get; set; }
        public int Size { get; set; }
        public FiltersModel Filters { get; set; }
    }
}
