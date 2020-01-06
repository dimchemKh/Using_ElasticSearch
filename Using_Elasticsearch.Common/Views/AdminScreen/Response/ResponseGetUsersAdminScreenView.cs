using System;
using System.Collections.Generic;
using System.Text;
using Using_Elasticsearch.DataAccess.Entities;

namespace Using_Elasticsearch.Common.Views.AdminScreen.Response
{
    public class ResponseGetUsersAdminScreenView
    {
        public int TotalCount { get; set; }
        public IEnumerable<ApplicationUser> Items { get; set; }
        public ResponseGetUsersAdminScreenView()
        {
            Items = new List<ApplicationUser>();
        }
    }
}
