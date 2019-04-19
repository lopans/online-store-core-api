using System;
using Base.DAL;
using Microsoft.AspNetCore.Identity;

namespace Base.Identity.Entities
{
    public class SpecialPermission : BaseEntity
    {
        public string Title { get; set; }
    }
}
