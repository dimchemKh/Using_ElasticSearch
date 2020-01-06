using Nest;
using System.Linq;
using System.Threading.Tasks;
using Using_Elasticsearch.BusinessLogic.Services.Interfaces;
using Using_Elasticsearch.Common.Exceptions;
using Using_Elasticsearch.Common.Views.AdminScreen.Request;
using Using_Elasticsearch.Common.Views.AdminScreen.Response;
using Using_Elasticsearch.DataAccess.Entities;
using Using_Elasticsearch.DataAccess.Repositories.Interfaces;

namespace Using_Elasticsearch.BusinessLogic.Services
{
    public class AdminScreenService : IAdminScreenService
    {
        private readonly IElasticClient _elasticClient;
        private readonly IUserRepository _userRepository;

        private const string ValueKey = "totalCount";

        public AdminScreenService(IElasticClient elasticClient, IUserRepository userRepository)
        {
            _elasticClient = elasticClient;
            _userRepository = userRepository;
        }

        public async Task<ResponseGetLogsAdminScreenView> GetLogsAsync(RequestGetLogsAdminScreenView requestModel)
        {
            var result = await _elasticClient.SearchAsync<LogException>(x => x
                         .From(requestModel.From)
                         .Size(requestModel.Size)
                         .Query(z => z).Sort(z => z.Ascending(a => a.CreationDate))
                         .Index("log_index")
                         .Sort(s => s.Descending(a => a.CreationDate))

                         .Aggregations(a => a.ValueCount(ValueKey, f => f.Field(r => r.CreationDate))));

            var response = new ResponseGetLogsAdminScreenView();

            response.Items = result.Documents.ToList();
            response.TotalCount = (int)result.Aggregations.ValueCount(ValueKey).Value;

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

        public async Task CreateUserAsync(RequestCreateUserAdminScreenView requestModel)
        {
            var user = new ApplicationUser();

            user.Email = requestModel.Email;
            user.FirstName = requestModel.FirstName;
            user.LastName = requestModel.LastName;
            user.Role = requestModel.Role;
            user.UserName = string.Concat(requestModel.FirstName, requestModel.LastName);

            var result = await _userRepository.CreateUserAsync(user, requestModel.Password);

            if (!result.Succeeded)
            {
                throw new ProjectException(result.Errors.Select(x => x.Code).FirstOrDefault());
            }
        }

        public async Task UpdateUserAsync(RequestCreateUserAdminScreenView requestModel)
        {
            var user = await _userRepository.FindUserAsync(requestModel.UserId);
        }
    }
}
