using Base.Identity.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Data
{
    public class Initializer
    {
        private IApplicationBuilder _app;
        public Initializer()
        {
        }
        public async Task Seed(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var _userManager = scope.ServiceProvider.GetService<UserManager<User>>();
                var admin = await _userManager.FindByNameAsync("admin");
                if (admin == null)
                {
                    var res = await _userManager.CreateAsync(new User()
                    {
                        Email = "admin",
                        EmailConfirmed = true,
                        UserName = "admin"
                    },
                    "111111");
                }
            }
        }
    }
}
