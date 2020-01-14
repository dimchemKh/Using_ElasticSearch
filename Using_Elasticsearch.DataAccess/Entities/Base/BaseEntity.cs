using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Using_Elasticsearch.DataAccess.Entities.Base
{
    public class BaseEntity
    {
        [Column("Id")]
        [Key]
        public string Id { get; set; }
        [Column("CreationDate")]
        public DateTime CreationDate { get; set; }
        public BaseEntity()
        {
            Id = Guid.NewGuid().ToString();
            CreationDate = DateTime.UtcNow;
        }
    }
}
