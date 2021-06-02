using System.Linq;
using Application.Infrastructure;
using Financas.Application.Persistence;
using Financas.Domain;

namespace Financas.Application.Features.Receitas
{
    public partial class RecuperarUmaReceita
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

            public Receita Handle(Query query)
            {
                var user = _context.Users.FirstOrDefault(u => u.UserName == _currentUser.UserName);

                var receita = _context.Receita
                    .FirstOrDefault(r => r.Id == query.Id && r.User == user);

                return receita;
            }

        }
    }
}
