using System.Linq;
using Microsoft.EntityFrameworkCore;
using Financas.Application.Persistence;
using Financas.Domain;

namespace Financas.Application.Features.Credito
{
    public partial class RecuperarUmGastoCartaoCredito
    {
        public class QueryHandler
        {
            private readonly FinancasContext _context;

            public QueryHandler(FinancasContext context)
            {
                _context = context;
            }

            public CartaoCreditoCompra Handle(Query query)
            {
                var result = _context.CartaoCreditoCompra
                    .Include(c => c.CartaoCreditoParcelas)
                    .SingleOrDefault(c => c.Id == query.Id);

                return result;
            }
        }
    }
}
