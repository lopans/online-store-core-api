using IdentityServer4.Services;
using IdentityServer4.Validation;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Base.Identity.IndetityServer
{
    public class ICorsPolicyProvider : ICorsPolicyService
    {
        public Task<bool> IsOriginAllowedAsync(string origin)
        {
            return Task.FromResult(true);
        }
    }
}
