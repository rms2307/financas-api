using System.Linq;
using Microsoft.EntityFrameworkCore;
using Financas.Application.Persistence;
using Financas.Domain;
using Application.Infrastructure;

namespace Financas.Application.Features.Credito
{
    public partial class RecuperarUmGastoCartaoCredito
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

            public CartaoCreditoCompra Handle(Query query)
            {
                var user = _context.Users.FirstOrDefault(u => u.UserName == _currentUser.UserName);

                var result = _context.CartaoCreditoCompra
                    .Include(c => c.CartaoCreditoParcelas)
                    .Include(c => c.CartaoCredito)
                    .FirstOrDefault(c => c.Id == query.Id && c.CartaoCredito.User == user);

                return result;
            }
        }
    }
}
