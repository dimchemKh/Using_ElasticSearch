using System;
using Using_Elasticsearch.DataAccess.Entities.Enums;

namespace Using_Elasticsearch.Common.Models
{
    public class PermissionModel
    {
        public string UserId { get; set; }
        public bool CanView { get; set; }
        public bool CanEdit { get; set; }
        public bool CanCreate { get; set; }
        public PagePermission Page { get; set; }
        public string CreationDate { get; set; }
        public PermissionModel()
        {
            CreationDate = DateTime.UtcNow.ToString();
        }
    }

}
