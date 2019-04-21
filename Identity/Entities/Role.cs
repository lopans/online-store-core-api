using Microsoft.AspNetCore.Identity;

namespace Base.Identity.Entities
{
    public class Role: IdentityRole<int>
    {
        public Role() : base()
        {
        }

        public Role(string name) : base(name)
        {

        }
    }
}
