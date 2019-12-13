using Using_Elasticsearch.Common.Enums;
using Using_Elasticsearch.Common.Models;

namespace Using_Elasticsearch.Common.Views.MainScreen.Request
{
    public class RequestSearchMainScreenView
    {
        public int From { get; set; }
        public int Size { get; set; }
        public FilterName MyProperty { get; set; }
        public FiltersModel Filters { get; set; }
    }
}
