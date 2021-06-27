using System;

namespace Financas.Application.Features.Users
{
    public partial class AtualizarUser
    {
        public class Command
        {
            public long Id { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string NomeCompleto { get; set; }
            public string RefreshToken { get; set; }
            public DateTime RefreshTokenExpiryTime { get; set; }
        }
    }    
}
