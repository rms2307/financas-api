namespace Financas.Application.Features.Autenticacao
{
    public partial class RefreshToken
    {
        public class Command
        {
            public string AccessToken { get; set; }
            public string RefreshToken { get; set; }
        }
    }
}
