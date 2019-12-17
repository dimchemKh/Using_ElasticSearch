using System.ComponentModel.DataAnnotations.Schema;
using Using_Elasticsearch.DataAccess.Entities.Base;

namespace Using_Elasticsearch.DataAccess.Entities
{
    [Table("LogExceptions")]
    public class LogException : BaseEntity
    {
        [Column("StackTrace")]
        public string StackTrace { get; set; }
        [Column("Message")]
        public string Message { get; set; }
        [Column("Action")]
        public string Action { get; set; }
        [Column("UserId")]
        public string UserId { get; set; }
    }

}
