using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Base.Identity.Entities
{
    public class Role: IdentityRole
    {
        public Role() : base()
        {
        }

        public Role(string name) : base(name)
        {

        }
        public List<RoleSpecialPermission> SpecialPermissions { get; set; } = new List<RoleSpecialPermission>();
    }
}
