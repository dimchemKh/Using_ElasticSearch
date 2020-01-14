using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Using_Elasticsearch.DataAccess.Entities;
using Using_Elasticsearch.DataAccess.Models;

namespace Using_Elasticsearch.DataAccess.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<ApplicationUser> FindUserByEmailAsync(string userId);
        Task<ApplicationUser> FindUserByIdAsync(string userId);
        Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password);
        Task<IdentityResult> UpdateUserAsync(ApplicationUser user);
        Task<IdentityResult> RemoveUserAsync(string userId);
        Task<PaginationModel<ApplicationUser>> GetUsersAsync(int from, int size);
        Task<SignInResult> CheckPasswordAsync(ApplicationUser user, string password);
    }
}
