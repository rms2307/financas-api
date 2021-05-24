namespace Financas.Application.Features.Users
{
    public partial class ValidarUser
    {
        public class QueryUserNameAndPassword
        {
            public string Password { get; set; }
            public string UserName { get; set; }
        }
    }
}
