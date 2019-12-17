using System;
using System.Collections.Generic;
using System.Text;
using Using_Elasticsearch.DataAccess.Entities;

namespace Using_Elasticsearch.Common.Views.AdminScreen.Response
{
    public class ResponseGetLogsAdminScreenView
    {
        public int TotalCount { get; set; }
        public List<LogException> Items { get; set; }
        public ResponseGetLogsAdminScreenView()
        {
            Items = new List<LogException>();
        }
    }
}
