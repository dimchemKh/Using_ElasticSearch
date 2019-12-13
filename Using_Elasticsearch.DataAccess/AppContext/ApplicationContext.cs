using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using Using_Elastic.DataAccess.Entities;
using Using_Elasticsearch.DataAccess.Entities;

namespace Using_Elasticsearch.DataAccess.AppContext
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser, Role, Guid>
    {
        public DbSet<WebAppData> WebAppData { get; set; }
        public ApplicationContext(DbContextOptions options) : base(options)
        {            
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
