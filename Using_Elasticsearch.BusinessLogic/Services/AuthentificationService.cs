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
        public AuthentificationService(IUserRepository userRepository, IJwtFactoryHelper jwtFactory)
        {
            _userRepository = userRepository;
            _jwtFactory = jwtFactory;
        }
        public async Task<ResponseGenerateAuthentificationView> LoginAsync(RequestLoginAuthentificationView requestLogin)
        {
            var user = await CheckUser(requestLogin);

            var resposne = _jwtFactory.Generate(user);

            return resposne;
        }
        public async Task<ApplicationUser> FindUserAsync(string email)
        {
            var user = await _userRepository.FindUserByEmailAsync(email);

            if (user == null)
            {
                throw new ProjectException(statusCode: StatusCodes.Status404NotFound, message: Messages.UserExistedError);
            }

            return user;
        }

        private async Task<ApplicationUser> CheckUser(RequestLoginAuthentificationView requestLogin)
        {
            var user = await FindUserAsync(requestLogin.Email);

            var result = await _userRepository.CheckPasswordAsync(user, requestLogin.Password);

            if (!result.Succeeded)
            {
                throw new ProjectException(statusCode: StatusCodes.Status400BadRequest, message: Messages.UserIncorrectPassword);
            }

            return user;
        }
    }
}
