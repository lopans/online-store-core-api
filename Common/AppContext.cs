using Common.Identity;
using Microsoft.AspNetCore.Http;
namespace Common
{
    public interface IApplicationContext
    {
        bool IsAdmin();
        bool IsEditor();
        bool IsBuyer();
    }
    public  class AppContext: Common.IApplicationContext
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public AppContext(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public bool IsAdmin()
        {
            return _contextAccessor.HttpContext.User.IsInRole(Roles.Admin);
        }

        public bool IsBuyer()
        {
            return _contextAccessor.HttpContext.User.IsInRole(Roles.Byuer);
        }

        public bool IsEditor()
        {
            return _contextAccessor.HttpContext.User.IsInRole(Roles.Editor);
        }

    }
}
