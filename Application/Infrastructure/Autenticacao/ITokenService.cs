using System.Collections.Generic;
using System.Security.Claims;

namespace Financas.Application.Infrastructure.Autenticacao
{
    public interface ITokenService
    {
        string GerarAccessToken(IEnumerable<Claim> claims);
        string GerarRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
