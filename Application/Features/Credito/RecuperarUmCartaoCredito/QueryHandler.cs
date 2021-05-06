using System.Linq;
using Microsoft.EntityFrameworkCore;
using Financas.Application.Persistence;
using Financas.Domain;

namespace Financas.Application.Features.Credito
{
    public partial class RecuperarUmCartaoCredito
    {
        public class QueryHandler
        {
            private readonly FinancasContext _context;

            public QueryHandler(FinancasContext context)
            {
                _context = context;
            }

            public CartaoCredito Handle(Query query)
            {
                var cartao = _context.CartaoCredito
                    .FirstOrDefault(c => c.Id == query.Id);

                return cartao;
            }
        }
    }
}
