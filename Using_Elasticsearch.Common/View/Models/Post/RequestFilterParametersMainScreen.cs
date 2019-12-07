using System;
using System.Collections.Generic;
using System.Text;
using Using_Elasticsearch.Common.View.Base;

namespace Using_Elasticsearch.Common.View.MainScreen.Post
{
    public class RequestFilterParametersMainScreen : BasePaginationFilter
    {
        public string ColumnName { get; set; }
        public decimal MinValue { get; set; }
        public decimal MaxValue { get; set; }
        public List<string> Values { get; set; }
    }
}
