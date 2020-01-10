using System;

namespace Using_Elasticsearch.Common.Configs
{
    public class JwtConfig
    {
        public string AccessName { get; set; }
        public string RefreshName { get; set; }
        public string JwtKey { get; set; }
        public string JwtIssuer { get; set; }
        public string JwtAudience { get; set; }
        public TimeSpan AccessTokenExpiration { get; set; } 
        public TimeSpan RefreshTokenExpiration { get; set; }
        public JwtConfig()
        {
            AccessTokenExpiration = TimeSpan.FromMinutes(10);
            RefreshTokenExpiration = TimeSpan.FromDays(30);
        }
    }
}
