using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Using_Elasticsearch.DataAccess.AppContext;
using Using_Elasticsearch.DataAccess.Entities;
using Using_Elasticsearch.DataAccess.Entities.Enums;
using Using_Elasticsearch.DataAccess.Repositories.Interfaces;
using static Using_Elasticsearch.DataAccess.DbInitializers.Constants.Constants;


namespace Using_Elasticsearch.DataAccess.DbInitializers
{
    public class DbInitializer
    {
        protected readonly ApplicationContext _context;
        protected readonly UserManager<ApplicationUser> _userManager;
        protected readonly RoleManager<Role> _roleManager;
        protected readonly IUserPermissionsRepository _userPermissionsRepository;
        public DbInitializer(ApplicationContext context, UserManager<ApplicationUser> userManager, RoleManager<Role> roleManager, 
                            IUserPermissionsRepository userPermissionsRepository)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _userPermissionsRepository = userPermissionsRepository;
        }

        public async Task Initialize()
        {
            await SeedAdminAndUserRoles();
        }
        protected async Task SeedAdminAndUserRoles()
        {
            var roles = Enum.GetValues(typeof(Enums.UserRole));

            foreach (Enums.UserRole role in roles)
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
            sysAdmin.Role = Enums.UserRole.SysAdmin;
            sysAdmin.EmailConfirmed = true;
            sysAdmin.PhoneNumber = InitialUsers.SysAdminPhoneNumber;

            var admin = new ApplicationUser();

            admin.FirstName = InitialUsers.AdminFirstName;
            admin.LastName = InitialUsers.AdminLastName;
            admin.Email = InitialUsers.AdminEmail;
            admin.UserName = string.Concat(InitialUsers.AdminFirstName, InitialUsers.AdminLastName);
            admin.Role = Enums.UserRole.Admin;
            admin.EmailConfirmed = true;
            admin.PhoneNumber = InitialUsers.AdminPhoneNumber;

            var createdSysAdmin = _userManager.CreateAsync(sysAdmin, InitialUsers.SysAdminPassword).GetAwaiter().GetResult();
            var createAdmin = _userManager.CreateAsync(admin, InitialUsers.AdminPassword).GetAwaiter().GetResult();


            var pages = Enum.GetValues(typeof(PagePermission));

            var groups = new List<GroupPermission>();

            foreach (PagePermission page in pages)
            {            
                groups.Add(new GroupPermission()
                {
                    Page = page
                });

                groups.Add(new GroupPermission()
                {
                    CanCreate = true,
                    Page = page
                });
                groups.Add(new GroupPermission()
                {
                    CanCreate = true,
                    CanEdit = true,
                    Page = page
                });
                groups.Add(new GroupPermission()
                {
                    CanCreate = true,
                    CanEdit = true,
                    CanView = true,
                    Page = page
                });

                groups.Add(new GroupPermission()
                {
                    CanCreate = true,
                    CanView = true,
                    Page = page
                });

                groups.Add(new GroupPermission()
                {
                    CanEdit = true,
                    Page = page
                });
                groups.Add(new GroupPermission()
                {
                    CanEdit = true,
                    CanView = true,
                    Page = page
                });
                groups.Add(new GroupPermission()
                {
                    CanView = true,
                    Page = page
                });
            }

            await _userPermissionsRepository.CreateGroup(groups);

            if (createdSysAdmin.Succeeded && createAdmin.Succeeded )
            {
                await _userManager.AddToRoleAsync(sysAdmin, Enums.UserRole.SysAdmin.ToString());
                await _userManager.AddToRoleAsync(admin, Enums.UserRole.Admin.ToString());


                //var adminId = _userManager.Users.Where(x => x.Email.Equals(admin.Email)).Select(z => z.Id).FirstOrDefault();
                //_context.UserPermissions.Add(new UserPermission() { UserId = adminId, CanEdit = true, CanView = true });
            }

            await _context.SaveChangesAsync();
        }

    
    }
}
