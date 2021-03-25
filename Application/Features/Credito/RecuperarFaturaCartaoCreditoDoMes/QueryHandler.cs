using System.Collections.Generic;
using System.Linq;
using LanguageExt;
using Financas.Application.Persistence;
using Financas.Domain;
using Microsoft.EntityFrameworkCore;

namespace Financas.Application.Features.Credito
{
    public partial class RecuperarFaturaCartaoCreditoDoMes
    {
        public class QueryHandler
        {
            private readonly FinancasContext _context;

            public QueryHandler(FinancasContext context)
            {
                _context = context;
            }

            public IEnumerable<CartaoCreditoParcela> Handle(Query query)
            {
                var parcelas = _context.CartaoCreditoParcela
                    .Include(c => c.CartaoCreditoCompra)
                    .Where(c => c.VencimentoParcela.Month == query.MesAtual)
                    .ToList();

                return parcelas;
            }
        }
    }
}
