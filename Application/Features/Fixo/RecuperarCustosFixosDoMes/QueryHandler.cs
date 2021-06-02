using System.Collections.Generic;
using System.Linq;
using LanguageExt;
using Financas.Application.Persistence;
using Financas.Domain;
using Microsoft.EntityFrameworkCore;
using Application.Infrastructure;

namespace Financas.Application.Features.Fixo
{
    public partial class RecuperarCustosFixosDoMes
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

            public IEnumerable<CustoFixo> Handle(Query query)
            {
                var user = _context.Users.FirstOrDefault(u => u.UserName == _currentUser.UserName);

                var custosFixos = _context.CustoFixo
                    .Include(c => c.CustoFixoDescricao)
                    .Where(c => c.Data.Month == query.MesAtual && c.CustoFixoDescricao.User == user)
                    .OrderByDescending(c => c.Data)
                    .ToList();

                return custosFixos;
            }
        }
    }
}
