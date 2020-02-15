using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Identity
{
    public class Role: IdentityRole<int>
    {
        public List<UserRoles> UserRoles { get; set; }
    }
}
