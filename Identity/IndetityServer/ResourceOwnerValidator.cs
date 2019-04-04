using IdentityServer4.Validation;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Base.Identity.IndetityServer
{
    public class ResourceOwnerValidator : IResourceOwnerPasswordValidator
    {
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var userName = context.UserName;
            var password = context.Password;

            context.Result = new GrantValidationResult(
                subject: userName,
                authenticationMethod: "",
                claims: new[] { new Claim("name", "whatever") });
        }
    }
}
