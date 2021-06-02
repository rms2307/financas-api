using System.Linq;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using Financas.Application.Persistence;
using Financas.Domain;
using System;
using Application.Infrastructure;

namespace Financas.Application.Features.Fixo
{
    public partial class RecuperarUmCustoFixo
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

            public CustoFixo Handle(Query query)
            {
                var user = _context.Users.FirstOrDefault(u => u.UserName == _currentUser.UserName);

                var result = _context.CustoFixo
                    .Include(cd => cd.CustoFixoDescricao)
                    .FirstOrDefault(cd => cd.Id == query.Id && cd.CustoFixoDescricao.User == user);

                if (result.IsNull()) throw new Exception("Registro não encontrado");

                return result;
            }
        }
    }
}
