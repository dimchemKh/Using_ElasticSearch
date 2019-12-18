using System;
using System.Collections.Generic;
using System.Text;
using Using_Elasticsearch.Common.Enums;

namespace Using_Elasticsearch.Common.Views.AdminScreen.Request
{
    public class RequestGetLogsAdminScreenView
    {
        public int From { get; set; }
        public int Size { get; set; }
        public FilterName CurrentFilter { get; set; }
    }
}
