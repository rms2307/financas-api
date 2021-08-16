using Application.Infrastructure;

namespace Financas.Api.Infrastructure
{
    public class CurrentUserMock : ICurrentUser
    {
        public string UserName => "robson.moraes";
    }
}
