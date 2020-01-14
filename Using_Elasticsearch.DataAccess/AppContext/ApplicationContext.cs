using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using Using_Elasticsearch.DataAccess.Entities;

namespace Using_Elasticsearch.DataAccess.AppContext
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser, Role, Guid>
    {
        public DbSet<UserPermission> UserPermissions { get; set; }
        //public DbSet<GroupPermission> GroupPermissions { get; set; }
        public DbSet<LogException> LogExceptions { get; set; }
        public ApplicationContext(DbContextOptions options) : base(options)
        {            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
