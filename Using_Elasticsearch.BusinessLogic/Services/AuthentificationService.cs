using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Using_Elasticsearch.BusinessLogic.Helpers.Interfaces;
using Using_Elasticsearch.BusinessLogic.Services.Interfaces;
using Using_Elasticsearch.Common.Constants;
using Using_Elasticsearch.Common.Exceptions;
using Using_Elasticsearch.Common.Views.Authentification.Request;
using Using_Elasticsearch.Common.Views.Authentification.Response;
using Using_Elasticsearch.DataAccess.Entities;
using Using_Elasticsearch.DataAccess.Repositories.Interfaces;

namespace Using_Elasticsearch.BusinessLogic.Services
{
    public class AuthentificationService : IAuthentificationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtFactoryHelper _jwtFactory;
        private readonly IUserPermissionsRepository _userPermissionsRepository;
        public AuthentificationService(IUserRepository userRepository, IJwtFactoryHelper jwtFactory, IUserPermissionsRepository userPermissionsRepository)
        {
            _userRepository = userRepository;
            _jwtFactory = jwtFactory;
            _userPermissionsRepository = userPermissionsRepository;
        }
        public async Task<ResponseGenerateAuthentificationView> LoginAsync(RequestLoginAuthentificationView requestLogin)
        {
            var user = await CheckUser(requestLogin);

            var permissions = await _userPermissionsRepository.GetPermissionsAsync(user.Id.ToString());

            var resposne = _jwtFactory.Generate(user, permissions);

            return resposne;
        }

        public async Task<ResponseGenerateAuthentificationView> RefreshAsync(string email)
        {
            var user = await _userRepository.FindUserByEmailAsync(email);

            if (user == null)
            {
                throw new ProjectException(statusCode: StatusCodes.Status404NotFound, message: Messages.UserExistedError);
            }

            var permissions = await _userPermissionsRepository.GetPermissionsAsync(user.Id.ToString());

            var response = _jwtFactory.Generate(user, permissions);

            return response;
        }

        private async Task<ApplicationUser> CheckUser(RequestLoginAuthentificationView requestLogin)
        {
            var user = await _userRepository.FindUserByEmailAsync(requestLogin.Email);

            if (user == null)
            {
                throw new ProjectException(statusCode: StatusCodes.Status404NotFound, message: Messages.UserExistedError);
            }

            var result = await _userRepository.CheckPasswordAsync(user, requestLogin.Password);

            if (!result.Succeeded)
            {
                throw new ProjectException(statusCode: StatusCodes.Status400BadRequest, message: Messages.UserIncorrectPassword);
            }

            return user;
        }
    }
}
