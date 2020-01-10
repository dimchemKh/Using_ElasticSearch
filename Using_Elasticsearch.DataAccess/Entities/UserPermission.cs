using System.ComponentModel.DataAnnotations.Schema;

using Using_Elasticsearch.DataAccess.Entities.Base;

namespace Using_Elasticsearch.DataAccess.Entities
{
    [Table("UserPermissions")]
    public class UserPermission : BaseEntity
    {
        [Column("UserId")]
        public string UserId { get; set; }
        [Column("GroupPermissionId")]
        public string GroupPermissionId { get; set; }
        //[Write(false)]
        //public List<GroupPermission> GroupPermissions { get; set; }
        //public UserPermission()
        //{
        //    GroupPermissions = new List<GroupPermission>();
        //}
    }
}
