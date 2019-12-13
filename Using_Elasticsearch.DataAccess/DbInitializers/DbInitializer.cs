using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Using_Elastic.DataAccess.Entities;
using Using_Elasticsearch.DataAccess.AppContext;
using Using_Elasticsearch.DataAccess.Entities;
using static Using_Elasticsearch.DataAccess.DbInitializers.Constants.Constants;

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

        public void Initialize()
        {

            //if (!_roleManager.Roles.Any(role => role.Name == InitialData.UserRole || role.Name == InitialData.AdminRole))
            //{
            //    SeedAdminAndUserRoles();
            //}

        }
        protected void SeedAdminAndUserRoles()
        {
            _roleManager.CreateAsync(new Role()
            {
                Name = InitialData.AdminRole
            }).GetAwaiter()
              .GetResult();

            var admin = new ApplicationUser();

            admin.FirstName = InitialData.AdminFirstName;
            admin.LastName = InitialData.AdminLastName;
            admin.Email = InitialData.AdminEmail;
            admin.UserName = string.Concat(InitialData.AdminFirstName, InitialData.AdminLastName);
            admin.EmailConfirmed = true;
            admin.LockoutEnabled = false;

            var result = _userManager.CreateAsync(admin, InitialData.AdminPassword)
                                     .GetAwaiter()
                                     .GetResult();

            if (result.Succeeded)
            {
                _userManager.AddToRoleAsync(admin, InitialData.AdminRole)
                            .GetAwaiter()
                            .GetResult();
            }
            _context.SaveChanges();

        }

    
    }
}
