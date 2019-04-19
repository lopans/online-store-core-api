using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Base.Identity.Entities.Static
{
    public static class Roles
    {
        public static string Admin { get => "Admin"; }
        public static string Editor { get => "Editor"; }
        public static string Byuer { get => "Byuer"; }
        public static string Public { get => "Public"; }
        public static string RolePrefix { get => "r_"; }

        public static IEnumerable<string> GetRolesList
        {
            get => new List<string> { Admin, Editor, Byuer, Public };
        }
    }
}
