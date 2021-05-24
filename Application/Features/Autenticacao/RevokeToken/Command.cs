namespace Financas.Application.Features.Autenticacao
{
    public partial class RevokeToken
    {
        public class Command
        {
            public string UserName { get; set; }
        }
    }
}
