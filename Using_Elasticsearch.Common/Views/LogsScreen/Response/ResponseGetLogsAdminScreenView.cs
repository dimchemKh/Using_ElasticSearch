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

    public class ReponseGetLogsAdminScreenViewItem
    {
        public string StackTrace { get; set; }
        public string Message { get; set; }
        public string Action { get; set; }
        public string UserId { get; set; }
    }
}
