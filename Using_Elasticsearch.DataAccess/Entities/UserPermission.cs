using System;
using System.Collections.Generic;
using System.Text;
using Using_Elasticsearch.DataAccess.Entities.Base;

namespace Using_Elasticsearch.DataAccess.Entities
{
    public class UserPermission : BaseEntity
    {
        public bool CanView { get; set; }
        public bool CanEdit { get; set; }
        public Guid UserId { get; set; }        
    }
}
