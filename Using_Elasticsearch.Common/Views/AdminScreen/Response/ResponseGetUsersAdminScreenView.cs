using System;
using System.Collections.Generic;
using Using_Elasticsearch.Common.Models;
using Using_Elasticsearch.DataAccess.Entities;

namespace Using_Elasticsearch.Common.Views.AdminScreen.Response
{
    public class ResponseGetUsersAdminScreenView
    {
        public int TotalCount { get; set; }
        public IEnumerable<ApplicationUser> Items { get; set; }
        //public IList<PermissionModel> Permissions { get; set; }
        public ResponseGetUsersAdminScreenView()
        {
            Items = new List<ApplicationUser>();
            //Permissions = new List<PermissionModel>();
        }
    }
    //public class ResponseGetUsersAdminScreenViewItem
    //{
    //    public string FirstName { get; set; }
    //    public string LastName { get; set; }
    //    public string Password { get; set; }
    //    public Enums.Enums.UserRole Role { get; set; }
    //    public bool IsRemoved { get; set; }
    //}
}
