using System.Collections.Generic;

using Using_Elasticsearch.DataAccess.Entities;

namespace Using_Elasticsearch.Common.Views.AdminScreen.Response
{
    public class ResponseGetLogsAdminScreenView
    {
        public int TotalCount { get; set; }
        public IEnumerable<LogException> Items { get; set; }
        public ResponseGetLogsAdminScreenView()
        {
            Items = new List<LogException>();
        }
    }
}
