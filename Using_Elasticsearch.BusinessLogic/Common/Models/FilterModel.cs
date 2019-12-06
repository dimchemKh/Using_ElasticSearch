using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Using_Elastic.DataAccess.Entities;

namespace Using_Elasticsearch.BusinessLogic.Common.Models
{
    public class FilterModel
    {
        public int From { get; set; }
        public int Size { get; set; }
        public string ColumnName { get; set; }
        //public string ExpressionParameter { get; set; }
        //public string ExpressionProperty { get; set; }
        public decimal MinValue { get; set; }
        public decimal MaxValue { get; set; }
    }
}
