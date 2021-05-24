namespace Financas.Domain
{
    public class Token
    {
        public Token(
            bool autenticado, 
            string criado, 
            string expiraEm, 
            string accessToken, 
            string refreshToken)
        {
            Autenticado = autenticado;
            Criado = criado;
            ExpiraEm = expiraEm;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }

        public bool Autenticado { get; set; }
        public string Criado { get; set; }
        public string ExpiraEm { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
