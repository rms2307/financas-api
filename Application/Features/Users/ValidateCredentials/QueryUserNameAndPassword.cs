namespace Financas.Application.Features.Users
{
    public partial class ValidateCredentials
    {
        public class QueryUserNameAndPassword
        {
            public string Password { get; set; }
            public string UserName { get; set; }
        }
    }
}
