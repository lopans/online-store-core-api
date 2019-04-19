using System;
using Base.DAL;
using Microsoft.AspNetCore.Identity;

namespace Base.Identity.Entities
{
    public class RoleSpecialPermission : BaseEntity
    {
        public int SpecialPermissionID { get; set; }
        public SpecialPermission SpecialPermission { get; set; }
        public string RoleID { get; set; }
        public Role Role { get; set; }
    }
}
