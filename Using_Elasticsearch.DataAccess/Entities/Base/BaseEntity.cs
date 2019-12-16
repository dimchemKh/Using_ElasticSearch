using Dapper.Contrib.Extensions;
using System;

namespace Using_Elasticsearch.DataAccess.Entities.Base
{
    public class BaseEntity
    {
        [Key]
        public string Id { get; set; }
        public DateTime CreationDate { get; set; }
        public BaseEntity()
        {
            Id = Guid.NewGuid().ToString();
            CreationDate = DateTime.Now;
        }
    }
}
