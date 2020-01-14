using Dapper.Contrib.Extensions;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Using_Elasticsearch.DataAccess.Entities
{
    [System.ComponentModel.DataAnnotations.Schema.Table("GroupPermissions")]
    public class GroupPermission
    {
        //[Column("Id")]
        //[ExplicitKey]
        //public string Id { get; set; }
        //[Column("CreationDate")]
        //public DateTime CreationDate { get; set; }

        ////[Column("Page")]
        ////public PagePermission Page { get; set; }
        //public GroupPermission()
        //{
        //    Id = Guid.NewGuid().ToString();
        //    CreationDate = DateTime.Now;
        //}
    }
}
