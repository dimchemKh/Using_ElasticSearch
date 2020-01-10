using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Using_Elasticsearch.DataAccess.Entities.Base;
using Using_Elasticsearch.DataAccess.Entities.Enums;

namespace Using_Elasticsearch.DataAccess.Entities
{
    [Table("GroupPermissions")]
    public class GroupPermission
    {
        [Column("Id")]
        [Key]
        public int Id { get; set; }
        [Column("CreationDate")]
        public DateTime CreationDate { get; set; }
        [Column("CanView")]
        public bool CanView { get; set; }
        [Column("CanEdit")]
        public bool CanEdit { get; set; }
        [Column("CanCreate")]
        public bool CanCreate { get; set; }
        [Column("Page")]
        public PagePermission Page { get; set; }
        public GroupPermission()
        {
            CreationDate = DateTime.Now;
        }
    }
}
