using Application.Infrastructure;
using Financas.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;

namespace Financas.Api.Infrastructure
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

        private string GetUserClaim(string claimName)
        {
            return _user.Claims.SingleOrDefault(
                claim => claim.Type.Equals(claimName, 
                StringComparison.CurrentCultureIgnoreCase))?.Value;
        }
    }
}
