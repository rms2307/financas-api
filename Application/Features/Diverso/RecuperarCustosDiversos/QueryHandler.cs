using System.Collections.Generic;
using System.Linq;
using LanguageExt;
using Financas.Application.Persistence;
using Financas.Domain;
using Application.Infrastructure;

namespace Financas.Application.Features.Diverso
{
    public partial class RecuperarCustosDiversos
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

            public IEnumerable<CustoDiverso> Handle(Query query)
            {
                var user = _context.Users.FirstOrDefault(u => u.UserName == _currentUser.UserName);

                var custosDiversos = _context.CustoDiverso
                    .Where(cd => cd.Data.Month == query.MesAtual && cd.User == user)
                    .OrderByDescending(c => c.Data)
                    .ToList();

                return custosDiversos;
            }
        }
    }
}
