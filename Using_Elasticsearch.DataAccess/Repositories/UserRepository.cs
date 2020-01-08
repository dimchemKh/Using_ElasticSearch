using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Using_Elasticsearch.DataAccess.Entities;
using Using_Elasticsearch.DataAccess.Models;
using Using_Elasticsearch.DataAccess.Repositories.Interfaces;

namespace Using_Elasticsearch.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public UserRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<ApplicationUser> FindUserAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            return user;
        }
        public async Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password: password);
                
            return result;
        }
        public async Task<IdentityResult> UpdateUserAsync(ApplicationUser user)
        {
            var result = await _userManager.UpdateAsync(user);

            return result;
        }
        public async Task<IdentityResult> RemoveUserAsync(ApplicationUser user)
        {
            user.IsRemoved = true;

            var result = await _userManager.UpdateAsync(user);

            return result;
        }
        public async Task<PaginationModel<ApplicationUser>> GetUsersAsync(int from, int size)
        {
            var response = new PaginationModel<ApplicationUser>();

            var query = _userManager.Users.Where(x => x.IsRemoved == false);

            response.TotalCount = await query.CountAsync();
            response.Items = await query.Skip((from) * size).Take(size).ToListAsync();

            return response;
        }
        public async Task<ApplicationUser> FindByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user;
        }

        public async Task<SignInResult> CheckPasswordAsync(ApplicationUser user, string password)
        {
            return await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: false);
        }
    }
}
