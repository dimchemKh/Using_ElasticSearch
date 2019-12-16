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

            admin.FirstName = InitialSuperUsers.AdminFirstName;
            admin.LastName = InitialSuperUsers.AdminLastName;
            admin.Email = InitialSuperUsers.AdminEmail;
            admin.UserName = string.Concat(InitialSuperUsers.AdminFirstName, InitialSuperUsers.AdminLastName);
            admin.EmailConfirmed = true;
            admin.LockoutEnabled = false;

            var result = _userManager.CreateAsync(admin, InitialSuperUsers.AdminPassword)
                                     .GetAwaiter().GetResult();

            if (result.Succeeded)
            {
                //_userManager.AddToRoleAsync(admin, InitialData.SuperAdminRole).GetAwaiter().GetResult();
                await _userManager.AddToRoleAsync(admin, UserRole.SuperUser.ToString());
                var adminId = _userManager.Users.Where(x => x.Email.Equals(admin.Email)).Select(z => z.Id).FirstOrDefault();

                //_context.UserPermissions.Add(new UserPermission() { UserId = adminId, CanEdit = true, CanView = true });
            }

            await _context.SaveChangesAsync();
        }

    
    }
}
