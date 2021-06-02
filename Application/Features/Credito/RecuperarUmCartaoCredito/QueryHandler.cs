using System.Linq;
using Financas.Application.Persistence;
using Financas.Domain;
using Application.Infrastructure;

namespace Financas.Application.Features.Credito
{
    public partial class RecuperarUmCartaoCredito
    {
        public class QueryHandler
        {
            private readonly FinancasContext _context;
            private readonly ICurrentUser _currentUser;

            public QueryHandler(FinancasContext context, ICurrentUser currentUser)
            {
                _context = context;
                _currentUser = currentUser;
            }

            public CartaoCredito Handle(Query query)
            {
                var user = _context.Users.FirstOrDefault(u => u.UserName == _currentUser.UserName);

                var cartao = _context.CartaoCredito
                    .FirstOrDefault(c => c.Id == query.Id && c.User == user);

                return cartao;
            }
        }
    }
}
