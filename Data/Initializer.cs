using Base.Identity.Entities;
using Common.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;
namespace Data
{
    public class Initializer
    {
        public Initializer()
        {
        }
        public async Task Seed(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var _roleManager = scope.ServiceProvider.GetService<RoleManager<Role>>();
                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new Role(Roles.Admin));
                    await _roleManager.CreateAsync(new Role(Roles.Editor));
                    await _roleManager.CreateAsync(new Role(Roles.Byuer));
                    await _roleManager.CreateAsync(new Role(Roles.Public));
                }

                var _userManager = scope.ServiceProvider.GetService<UserManager<User>>();
                var admin = await _userManager.FindByNameAsync("admin");
                if (admin == null)
                {
                    admin = new User()
                    {
                        Email = "admin",
                        EmailConfirmed = true,
                        UserName = "admin"
                    };
                    var res = await _userManager.CreateAsync(admin, "111111");
                    await _userManager.AddToRoleAsync(admin, Roles.Admin);
                }


                //var context = scope.ServiceProvider.GetService<DataContext>();
                //using(var uofw = new UnitOfWork(context))
                //{
                    
                //}
            }
        }
    }
}
