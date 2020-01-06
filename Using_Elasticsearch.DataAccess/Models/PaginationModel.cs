using System.Collections.Generic;

namespace Using_Elasticsearch.DataAccess.Models
{
    public class PaginationModel<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalCount { get; set; }
    }
}
