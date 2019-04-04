using System;
using Microsoft.AspNetCore.Identity;

namespace Base.Identity
{
    public class User: IdentityUser
    {
        public string CustomField { get; set; }
    }
}
