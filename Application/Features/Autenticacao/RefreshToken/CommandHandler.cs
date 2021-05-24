using Financas.Application.Features.Users;
using Financas.Application.Infrastructure.Autenticacao;
using System;

namespace Financas.Application.Features.Autenticacao
{
    public partial class RefreshToken
    {
        public class CommandHandler
        {
            private const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";
            private readonly TokenConfig _tokenConfig;
            private readonly ValidateCredentials.QueryHandler _validateCredentials;
            private readonly AtualizarUser.CommandHandler _atualizarUser;
            private readonly ITokenService _tokenService;

            public CommandHandler(
                TokenConfig tokenConfig,
                ValidateCredentials.QueryHandler retornarUser,
                AtualizarUser.CommandHandler atualizarUser,
                ITokenService tokenService)
            {
                _tokenConfig = tokenConfig;
                _validateCredentials = retornarUser;
                _atualizarUser = atualizarUser;
                _tokenService = tokenService;
            }                     

            public Token Handle(Command command)
            {
                var accessToken = command.AccessToken;
                var refreshToken = command.RefreshToken;

                var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);

                var username = principal.Identity.Name;
                var user = _validateCredentials.Handle(
                    new ValidateCredentials.QueryUserName
                    {
                        UserName = username
                    });

                if (user == null) throw new Exception("Usuário ou senha incorretos");
                if (user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now) return null;

                accessToken = _tokenService.GerarAccessToken(principal.Claims);
                refreshToken = _tokenService.GerarRefreshToken();

                DateTime createDate = DateTime.Now;

                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = createDate.AddDays(_tokenConfig.DaysToExpiry);

                _atualizarUser.Handle(new AtualizarUser.Command
                {
                    Id = user.Id,
                    NomeCompleto = user.NomeCompleto,
                    UserName = user.UserName,
                    Password = user.Password,
                    RefreshToken = user.RefreshToken,
                    RefreshTokenExpiryTime = user.RefreshTokenExpiryTime
                });

                DateTime expirationDate = createDate.AddMinutes(_tokenConfig.Minutes);

                return new Token(
                    true,
                    createDate.ToString(DATE_FORMAT),
                    expirationDate.ToString(DATE_FORMAT),
                    accessToken,
                    refreshToken
                    );
            }
        }
    }
}
