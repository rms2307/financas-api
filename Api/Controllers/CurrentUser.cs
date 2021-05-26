using Application.Infrastructure;
using Financas.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;

namespace Api.Controllers
{
    public class CurrentUser :  ICurrentUser
    {
        private readonly ClaimsPrincipal _user;
        private readonly Lazy<string> _userName;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _user = httpContextAccessor.HttpContext.User;
            _userName = new Lazy<string>(() => GetUserClaim(nameof(User.UserName)));
        }

        public string UserName => _userName.Value;

        private T GetClaimValue<T>(string claimType) where T : IConvertible
        {
            var value = _user.Claims.SingleOrDefault(claim => claim.Type.Equals(claimType))?.Value;
            return value != null ? (T)Convert.ChangeType(value, typeof(T)) : default(T);
        }

        private string GetUserClaim(string claimName)
        {
            return _user.Claims.SingleOrDefault(
                claim => claim.Type.Equals(claimName, 
                StringComparison.CurrentCultureIgnoreCase))?.Value;
        }
    }
}
