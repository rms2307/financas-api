using System.Linq;
using Microsoft.EntityFrameworkCore;
using Financas.Application.Persistence;
using System;
using Application.Infrastructure;

namespace Financas.Application.Features.Credito
{
    public partial class RetornarTotalDeGastoDeUmCartao
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

            public decimal Handle(Query query)
            {
                var mesAtual = DateTime.Now.Month;
                var user = _context.Users.FirstOrDefault(u => u.UserName == _currentUser.UserName);

                var gastos = _context.CartaoCreditoParcela
                    .Include(p => p.CartaoCreditoCompra)
                    .ThenInclude(c => c.CartaoCredito)
                    .Where(p => p.CartaoCreditoCompra.CartaoCredito.Id == query.CartaoCreditoId &&
                            p.VencimentoParcela.Month >= mesAtual &&
                            p.CartaoCreditoCompra.CartaoCredito.User == user)
                    .ToList();

                var total = gastos.Sum(g => g.ValorParcela);

                return total;
            }
        }
    }
}
