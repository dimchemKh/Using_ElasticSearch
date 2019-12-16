using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Using_Elasticsearch.DataAccess.Entities;

namespace Using_Elasticsearch.DataAccess.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<ApplicationUser> FindByEmailAsync(string email);
        Task<SignInResult> CheckPasswordAsync(ApplicationUser user, string password);
    }
}
