using Microsoft.AspNetCore.Identity;

namespace Base.Identity.Entities
{
    public class User: IdentityUser<int>
    {
        public string CustomField { get; set; }
    }
}
