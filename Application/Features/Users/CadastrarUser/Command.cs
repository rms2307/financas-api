using System;

namespace Financas.Application.Features.Users
{
    public partial class CadastrarUser
    {
        public class Command
        {
            public string UserName { get; set; }
            public string Password { get; set; }
            public string ConfirmPassword { get; set; }
            public string NomeCompleto { get; set; }
            public string RefreshToken { get; set; }
            public DateTime RefreshTokenExpiryTime { get; set; }
        }
    }    
}
