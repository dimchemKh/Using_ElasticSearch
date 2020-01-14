using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Using_Elasticsearch.BusinessLogic.Helpers.Interfaces;
using Using_Elasticsearch.BusinessLogic.Services.Interfaces;
using Using_Elasticsearch.Common.Models;
using Using_Elasticsearch.Common.Views.AdminScreen.Request;
using Using_Elasticsearch.Common.Views.AdminScreen.Response;
using Using_Elasticsearch.DataAccess.Entities;
using Using_Elasticsearch.DataAccess.Entities.Enums;
using Using_Elasticsearch.DataAccess.Repositories.Interfaces;
using UserPermission = Using_Elasticsearch.DataAccess.Entities.UserPermission;

namespace Using_Elasticsearch.BusinessLogic.Services
{
    public class AdminScreenService : IAdminScreenService
    {

        private readonly IUserRepository _userRepository;
        private readonly IMapperHelper _mapperHelper;
        private readonly IMapper _autoMapper;
        private readonly IUserPermissionsRepository _userPermissionsRepository;

        public AdminScreenService(IUserRepository userRepository, IMapperHelper mapper, IUserPermissionsRepository userPermissionsRepository, IMapper autoMapper)
        {
            _userRepository = userRepository;
            _mapperHelper = mapper;
            _userPermissionsRepository = userPermissionsRepository;
            _autoMapper = autoMapper;
        }

        public async Task<ResponseGetPermissionsAdminScreenView> GetUserPermissionsAsync(RequestGetPermissionsAdminScreenView request)
        {
            var response = new ResponseGetPermissionsAdminScreenView();

            var permissions = await _userPermissionsRepository.GetPermissionsAsync(request.UserId);

            var models = _autoMapper.Map<IList<UserPermission>, IEnumerable<PermissionModel>>(permissions);


            response.Items = models;

            return response;
        }
        public async Task<ResponseGetUsersAdminScreenView> GetUsersAsync(RequestGetUsersAdminScreenView requestModel)
        {
            var result = await _userRepository.GetUsersAsync(requestModel.From, requestModel.Size);

            var response = new ResponseGetUsersAdminScreenView();

            response.Items = result.Items;
            response.TotalCount = result.TotalCount;

            return response;
        }

        public async Task<IEnumerable<string>> CreateUserAsync(RequestCreateUserAdminScreenView requestModel)
        {
            var user = _mapperHelper.Map<RequestCreateUserAdminScreenView, ApplicationUser>(requestModel);

            user.UserName = string.Concat(requestModel.FirstName, requestModel.LastName);

            var result = await _userRepository.CreateUserAsync(user, requestModel.Password);

            if (!result.Succeeded)
            {
                return result.Errors.Select(x => x.Description).ToList();

                //throw new ProjectException(result.Errors.Select(x => x.Code).FirstOrDefault());
            }

            foreach (var permissionPage in requestModel.Permissions)
            {
                await _userPermissionsRepository.AddInGroupAsync(new UserPermission
                {
                    UserId = user.Id.ToString(),
                    Page = permissionPage.Page,
                    CanCreate = permissionPage.CanCreate,
                    CanView = permissionPage.CanView,
                    CanEdit = permissionPage.CanEdit
                });
            }

            return new List<string>();
        }

        public async Task UpdateUserAsync(RequestCreateUserAdminScreenView requestModel)
        {
            var models = _autoMapper.Map<IList<PermissionModel>, IEnumerable<UserPermission>>(requestModel.Permissions);

            foreach (var permissionPage in models)
            {
                await _userPermissionsRepository.UpdatePermissionAsync(permissionPage);
            }

            var user = await _userRepository.FindUserByIdAsync(requestModel.UserId);

            user = _autoMapper.Map(requestModel, user);

            await _userRepository.UpdateUserAsync(user);

        }

        public async Task<IEnumerable<string>> RemoveUserAsync(string userId)
        {
            var result = await _userRepository.RemoveUserAsync(userId);

            if (!result.Succeeded)
            {
                return result.Errors.Select(x => x.Description).ToList();
            }

            return new List<string>();
        }
    }
}
