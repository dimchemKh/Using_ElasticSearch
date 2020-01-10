using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Using_Elasticsearch.BusinessLogic.Helpers.Interfaces;
using Using_Elasticsearch.BusinessLogic.Services.Interfaces;
using Using_Elasticsearch.Common.Views.AdminScreen.Request;
using Using_Elasticsearch.Common.Views.AdminScreen.Response;
using Using_Elasticsearch.DataAccess.Entities;
using Using_Elasticsearch.DataAccess.Repositories.Interfaces;

namespace Using_Elasticsearch.BusinessLogic.Services
{
    public class AdminScreenService : IAdminScreenService
    {

        private readonly IUserRepository _userRepository;
        private readonly IMapperHelper _mapper;

        public AdminScreenService(IUserRepository userRepository, IMapperHelper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
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
            //var user = new ApplicationUser();

            //user.Email = requestModel.Email;
            //user.FirstName = requestModel.FirstName;
            //user.LastName = requestModel.LastName;
            //user.Role = requestModel.Role;

            var user = _mapper.Map<RequestCreateUserAdminScreenView, ApplicationUser>(requestModel);

            user.UserName = string.Concat(requestModel.FirstName, requestModel.LastName);

            var result = await _userRepository.CreateUserAsync(user, requestModel.Password);

            if (!result.Succeeded)
            {
                return result.Errors.Select(x => x.Description).ToList();
                //throw new ProjectException(result.Errors.Select(x => x.Code).FirstOrDefault());
            }

            return new List<string>();
        }

        public async Task UpdateUserAsync(RequestCreateUserAdminScreenView requestModel)
        {
            var user = await _userRepository.FindUserAsync(requestModel.UserId);


        }
    }
}
