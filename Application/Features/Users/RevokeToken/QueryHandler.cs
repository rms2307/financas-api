using Financas.Application.Persistence;
using Financas.Domain;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Financas.Application.Features.Users
{
    public partial class RevokeToken
    {
        public class QueryHandler
        {
            private readonly FinancasContext _context;

            public QueryHandler(FinancasContext context)
            {
                _context = context;
            }
            public bool Handle(Query query)
            {
                var user = _context.Users.FirstOrDefault(u => u.UserName == query.UserName);
                if (user is null) return false;
                user.RefreshToken = null;
                _context.SaveChanges();
                return true;
            }
        }
    }
}
