using System.ComponentModel.DataAnnotations.Schema;

using Using_Elasticsearch.DataAccess.Entities.Base;
using Using_Elasticsearch.DataAccess.Entities.Enums;

namespace Using_Elasticsearch.DataAccess.Entities
{
    [Table("UserPermissions")]
    public class UserPermission : BaseEntity
    {
        [Column("UserId")]
        public string UserId { get; set; }
        [Column("CanView")]
        public bool CanView { get; set; }
        [Column("CanEdit")]
        public bool CanEdit { get; set; }
        [Column("CanCreate")]
        public bool CanCreate { get; set; }
        [Column("CanRemove")]
        public bool CanRemove { get; set; }
        [Column("Page")]
        public PagePermission Page { get; set; }
    }
}
