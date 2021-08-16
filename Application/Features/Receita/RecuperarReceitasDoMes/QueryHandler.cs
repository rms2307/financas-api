using System.Collections.Generic;
using System.Linq;
using Application.Infrastructure;
using Financas.Application.Persistence;
using Financas.Domain;
using LanguageExt;
using Microsoft.EntityFrameworkCore;

namespace Financas.Application.Features.Receitas
{
    public partial class RecuperarReceitasDoMes
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

            public IEnumerable<Receita> Handle(Query query)
            {
                var user = _context.Users.FirstOrDefault(u => u.UserName == _currentUser.UserName);

                var receitas = _context.Receita
                    .Include(r => r.TipoDeReceita)
                    .Where(r => r.DataRecebimento.Month == query.MesAtual && r.User == user)
                    .OrderByDescending(r => r.DataRecebimento)
                    .ToList();

                return receitas;
            }
        }
    }
}
