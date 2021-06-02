using System.Collections.Generic;
using System.Linq;
using LanguageExt;
using Financas.Application.Persistence;
using Financas.Domain;
using Application.Infrastructure;

namespace Financas.Application.Features.Credito
{
    public partial class RecuperarListaDeCartoesCreditos
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

            public IEnumerable<CartaoCredito> Handle()
            {
                var user = _context.Users.FirstOrDefault(u => u.UserName == _currentUser.UserName);
                var cartoes = _context.CartaoCredito.Where(c=> c.User == user).ToList();

                return cartoes;
            }
        }
    }
}
