using System.Collections.Generic;
using Using_Elasticsearch.DataAccess.Entities.Enums;
using static Using_Elasticsearch.DataAccess.Entities.Enums.Enums;

namespace Using_Elasticsearch.Common.Views.AdminScreen.Request
{
    public class RequestCreateUserAdminScreenView
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
        //public IEnumerable<UserPermission> MainScreenPermissions { get; set; }
        //public IEnumerable<UserPermission> AdminScreenPermissions { get; set; }
        //public IEnumerable<UserPermission> LogsScreenPermissions { get; set; }
        public List<PageItem> Permissions { get; set; }
        public RequestCreateUserAdminScreenView()
        {
            Permissions = new List<PageItem>();
        }
    }

    public class PageItem
    {
        public string UserId { get; set; }
        public PagePermission Page { get; set; }
        public bool CanView { get; set; }
        public bool CanEdit { get; set; }
    }
}
