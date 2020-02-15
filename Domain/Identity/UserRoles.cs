﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Identity
{
    public class UserRoles: IdentityUserRole<int>
    {
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
