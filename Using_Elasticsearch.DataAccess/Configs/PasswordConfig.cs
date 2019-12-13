using System;
using System.Collections.Generic;
using System.Text;

namespace Using_Elasticsearch.DataAccess.Configs
{
    public class PasswordConfig
    {
        public int RequiredLength { get; set; }
        public int RequiredUniqueChars { get; set; }
        public int DefaultLockoutTimeSpan { get; set; }
        public int MaxFailedAccessAttempts { get; set; }
        public bool RequireDigit { get; set; }
        public bool RequireUppercase { get; set; }
        public bool RequireLowercase { get; set; }
        public bool RequireNonAlphanumeric { get; set; }
        public bool AllowedForNewUsers { get; set; }
        public bool RequireUniqueEmail { get; set; }
    }
}
