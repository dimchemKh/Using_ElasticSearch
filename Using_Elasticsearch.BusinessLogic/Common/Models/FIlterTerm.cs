using System;
using System.Collections.Generic;
using System.Text;

namespace Using_Elasticsearch.BusinessLogic.Common.Models
{
    public class FIlterTerm
    {
        public int From { get; set; }
        public int Size { get; set; }
        public string Field { get; set; }
        public string Value { get; set; }
        public List<string> Values { get; set; }
    }
}
