using System;
using System.Collections.Generic;
using System.Text;

namespace Using_Elasticsearch.Common.View.Base
{
    public class BasePaginationFilter
    {
        public int From { get; set; }
        public int Size { get; set; }
        public int Page { get; set; }

    }
}
