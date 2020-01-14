using AutoMapper;
using System.Collections.Generic;
using Using_Elasticsearch.Common.Models;
using Using_Elasticsearch.Common.Views.AdminScreen.Request;
using Using_Elasticsearch.DataAccess.Entities;

namespace Using_Elasticsearch.BusinessLogic.Automapper
{
    public class AdminScreenMapping : Profile
    {
        public AdminScreenMapping()
        {
            CreateMap<RequestCreateUserAdminScreenView, ApplicationUser>()
                .ForMember(dest =>
                    dest.UserName,
                    map => map
                        .MapFrom(source =>
                            source.FirstName + source.LastName));
            //CreateMap<IList<UserPermission>, IList<PermissionModel>>();
            CreateMap<UserPermission, PermissionModel>();
        }
    }
}
