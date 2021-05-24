using Financas.Application.Features.Users;
using Financas.Application.Infrastructure.Autenticacao;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Financas.Application.Features.Autenticacao
{
    public partial class Signin
    {
        public class CommandHandler
        {
            private const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";
            private readonly TokenConfig _tokenConfig;
            private readonly ValidarUser.QueryHandler _validateCredentials;
            private readonly AtualizarUser.CommandHandler _atualizarUser;
            private readonly ITokenService _tokenService;

            public CommandHandler(
                TokenConfig tokenConfig,
                ValidarUser.QueryHandler retornarUser,
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
                var user = _validateCredentials.Handle(
                    new ValidarUser.QueryUserNameAndPassword
                    {
                        UserName = command.UserName,
                        Password = command.Password
                    });
                if (user == null) throw new Exception("Usuário ou senha incorretos");

                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
                };

                var accessToken = _tokenService.GerarAccessToken(claims);
                var refreshToken = _tokenService.GerarRefreshToken();

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
