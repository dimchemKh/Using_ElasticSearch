using System;
using System.Collections.Generic;
using System.Text;
using Using_Elasticsearch.Common.Views.Authentification.Response;
using Using_Elasticsearch.DataAccess.Entities;

namespace Using_Elasticsearch.BusinessLogic.Helpers.Interfaces
{
    public interface IJwtFactoryHelper
    {
        ResponseGenerateAuthentificationView ValidateData(string token);
        ResponseGenerateAuthentificationView Generate(ApplicationUser user);
    }
}
