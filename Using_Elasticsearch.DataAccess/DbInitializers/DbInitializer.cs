using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using Using_Elasticsearch.DataAccess.AppContext;
using Using_Elasticsearch.DataAccess.Entities;
using static Using_Elasticsearch.DataAccess.DbInitializers.Constants.Constants;
using static Using_Elasticsearch.DataAccess.Entities.Enums.Enums;

namespace Using_Elasticsearch.DataAccess.DbInitializers
{
    public class DbInitializer
    {
        protected readonly ApplicationContext _context;
        protected readonly UserManager<ApplicationUser> _userManager;
        protected readonly RoleManager<Role> _roleManager;
        public DbInitializer(ApplicationContext context, UserManager<ApplicationUser> userManager, RoleManager<Role> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Initialize()
        {
            await SeedAdminAndUserRoles();
        }
        protected async Task SeedAdminAndUserRoles()
        {
            var roles = Enum.GetValues(typeof(UserRole));

            foreach (UserRole role in roles)
            {
                if(!_roleManager.Roles.Any(x => x.Name.Equals(role.ToString())))
                {
                    await _roleManager.CreateAsync(new Role()
                    {
                        Name = role.ToString()
                    });
                }
            }

            var admin = new ApplicationUser();

            admin.FirstName = InitialUsers.AdminFirstName;
            admin.LastName = InitialUsers.AdminLastName;
            admin.Email = InitialUsers.AdminEmail;
            admin.UserName = string.Concat(InitialUsers.AdminFirstName, InitialUsers.AdminLastName);
            admin.EmailConfirmed = true;
            admin.LockoutEnabled = false;

            var user = new ApplicationUser();

            user.FirstName = InitialUsers.UserFirstName;
            user.LastName = InitialUsers.UserLastName;
            user.Email = InitialUsers.UserEmail;
            user.UserName = string.Concat(InitialUsers.UserFirstName, InitialUsers.UserLastName);
            admin.EmailConfirmed = true;
            admin.LockoutEnabled = false;

            var createdAdmin = _userManager.CreateAsync(admin, InitialUsers.AdminPassword).GetAwaiter().GetResult();
            var createdUser = _userManager.CreateAsync(user, InitialUsers.UserPassword).GetAwaiter().GetResult();

            if (createdAdmin.Succeeded && createdUser.Succeeded)
            {
                await _userManager.AddToRoleAsync(admin, UserRole.Admin.ToString());
                await _userManager.AddToRoleAsync(user, UserRole.User.ToString());

                //var adminId = _userManager.Users.Where(x => x.Email.Equals(admin.Email)).Select(z => z.Id).FirstOrDefault();
                //_context.UserPermissions.Add(new UserPermission() { UserId = adminId, CanEdit = true, CanView = true });
            }

            await _context.SaveChangesAsync();
        }

    
    }
}
