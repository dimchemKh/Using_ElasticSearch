using Using_Elasticsearch.DataAccess.Entities.Base;

namespace Using_Elasticsearch.DataAccess.Entities
{
    public class UserPermissions : BaseEntity
    {
        public bool CanView { get; set; }
        public bool CanEdit { get; set; }
        public bool CanCreate { get; set; }
        public string UserId { get; set; }        
    }
}
