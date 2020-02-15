using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Domain.Identity
{
   public class User:IdentityUser<int>
    {
        public string FullName { get; set; }
        public List<UserRoles> UserRoles { get; set; }
    }
}
