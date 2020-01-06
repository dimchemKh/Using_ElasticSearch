using Microsoft.AspNetCore.Identity;
using System;

namespace Using_Elasticsearch.DataAccess.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public Enums.Enums.UserRole Role { get; set; }
        public bool IsRemoved { get; set; }
    }
}
