﻿namespace Financas.Application.Features.Autenticacao
{
    public partial class Login
    {
        public class Command
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }
    }
}
