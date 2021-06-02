using System.Collections.Generic;
using System.Linq;
using LanguageExt;
using Financas.Application.Persistence;
using Financas.Domain;
using Microsoft.EntityFrameworkCore;
using Application.Infrastructure;

namespace Financas.Application.Features.Credito
{
    public partial class RecuperarFaturaCartaoCreditoDoMes
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

            public IEnumerable<CartaoCreditoParcela> Handle(Query query)
            {
                var user = _context.Users.FirstOrDefault(u => u.UserName == _currentUser.UserName);

                var parcelas = _context.CartaoCreditoParcela
                    .Include(c => c.CartaoCreditoCompra)
                    .ThenInclude(c => c.CartaoCredito)
                    .Where(c => c.VencimentoParcela.Month == query.MesAtual &&
                           c.CartaoCreditoCompra.CartaoCredito.Id == query.CartaoCreditoId &&
                           c.CartaoCreditoCompra.CartaoCredito.User == user)
                    .OrderByDescending(c => c.CartaoCreditoCompra.DataCompra)
                    .ToList();

                return parcelas;
            }
        }
    }
}
