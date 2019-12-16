using System;
using System.Collections.Generic;
using System.Text;
using Using_Elasticsearch.DataAccess.Entities.Base;

namespace Using_Elasticsearch.DataAccess.Entities
{
    public class LogException : BaseEntity
    {
        public string StackTrace { get; set; }
        public string Message { get; set; }
        public string Action { get; set; }
        public Guid? UserId { get; set; }
    }
}
