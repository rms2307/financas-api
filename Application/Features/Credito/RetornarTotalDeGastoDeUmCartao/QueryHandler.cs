using System.Linq;
using Microsoft.EntityFrameworkCore;
using Financas.Application.Persistence;
using Financas.Domain;
using System;

namespace Financas.Application.Features.Credito
{
    public partial class RetornarTotalDeGastoDeUmCartao
    {
        public class QueryHandler
        {
            private readonly FinancasContext _context;

            public QueryHandler(FinancasContext context)
            {
                _context = context;
            }

            public decimal Handle(Query query)
            {
                var mesAtual = DateTime.Now.Month;

                var gastos = _context.CartaoCreditoParcela
                    .Include(p => p.CartaoCreditoCompra)
                    .ThenInclude(c => c.CartaoCredito)
                    .Where(p => p.CartaoCreditoCompra.CartaoCredito.Id == query.CartaoCreditoId &&
                            p.VencimentoParcela.Month >= mesAtual)
                    .ToList();

                var total = gastos.Sum(g => g.ValorParcela);

                return total;
            }
        }
    }
}
