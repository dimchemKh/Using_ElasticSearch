using System;
using System.Collections.Generic;
using System.Text;

namespace Using_Elasticsearch.Common.Views.Authentification.Response
{
    public class ResponseGenerateAuthentificationView
    {
        //public string UserName { get; set; }
        //public string FullName { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
