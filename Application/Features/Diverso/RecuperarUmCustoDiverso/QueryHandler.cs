using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using Financas.Application.Persistence;
using Financas.Domain;
using Application.Infrastructure;

namespace Financas.Application.Features.Diverso
{
    public partial class RecuperarUmCustoDiverso
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

            public CustoDiverso Handle(Query query)
            {
                var user = _context.Users.FirstOrDefault(u => u.UserName == _currentUser.UserName);

                var result = _context.CustoDiverso
                    .FirstOrDefault(c => c.Id == query.Id && c.User == user);

                return result;
            }
        }
    }
}
