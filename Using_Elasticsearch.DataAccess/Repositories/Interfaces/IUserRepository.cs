using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Using_Elasticsearch.DataAccess.Entities;
using Using_Elasticsearch.DataAccess.Models;

namespace Using_Elasticsearch.DataAccess.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<ApplicationUser> FindUserAsync(string userId);
        Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password);
        Task<IdentityResult> UpdateUserAsync(ApplicationUser user);
        Task<IdentityResult> RemoveUserAsync(ApplicationUser user);
        Task<PaginationModel<ApplicationUser>> GetUsersAsync(int from, int size);
        Task<SignInResult> CheckPasswordAsync(ApplicationUser user, string password);
    }
}
