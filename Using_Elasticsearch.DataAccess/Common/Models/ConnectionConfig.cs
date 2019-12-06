using System;
using System.Collections.Generic;
using System.Text;

namespace Using_Elastic.DataAccess.Common.Models
{
    public class ConnectionConfig
    {
        public string ConnectionDb { get; set; }
        public string ElasticIndex { get; set; }
        public string ConnectionElastic { get; set; }
    }
}
