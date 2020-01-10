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

            var sysAdmin = new ApplicationUser();

            sysAdmin.FirstName = InitialUsers.SysAdminFirstName;
            sysAdmin.LastName = InitialUsers.SysAdminLastName;
            sysAdmin.Email = InitialUsers.SysAdminEmail;
            sysAdmin.UserName = string.Concat(InitialUsers.SysAdminFirstName, InitialUsers.SysAdminLastName);
            sysAdmin.Role = UserRole.SysAdmin;
            sysAdmin.EmailConfirmed = true;
            sysAdmin.PhoneNumber = InitialUsers.SysAdminPhoneNumber;

            var admin = new ApplicationUser();

            admin.FirstName = InitialUsers.AdminFirstName;
            admin.LastName = InitialUsers.AdminLastName;
            admin.Email = InitialUsers.AdminEmail;
            admin.UserName = string.Concat(InitialUsers.AdminFirstName, InitialUsers.AdminLastName);
            admin.Role = UserRole.Admin;
            admin.EmailConfirmed = true;
            admin.PhoneNumber = InitialUsers.AdminPhoneNumber;

            var createdSysAdmin = _userManager.CreateAsync(sysAdmin, InitialUsers.SysAdminPassword).GetAwaiter().GetResult();
            var createAdmin = _userManager.CreateAsync(admin, InitialUsers.AdminPassword).GetAwaiter().GetResult();

            if (createdSysAdmin.Succeeded && createAdmin.Succeeded)
            {
                await _userManager.AddToRoleAsync(sysAdmin, UserRole.SysAdmin.ToString());
                await _userManager.AddToRoleAsync(admin, UserRole.Admin.ToString());

                //var adminId = _userManager.Users.Where(x => x.Email.Equals(admin.Email)).Select(z => z.Id).FirstOrDefault();
                //_context.UserPermissions.Add(new UserPermission() { UserId = adminId, CanEdit = true, CanView = true });
            }

            await _context.SaveChangesAsync();
        }

    
    }
}
