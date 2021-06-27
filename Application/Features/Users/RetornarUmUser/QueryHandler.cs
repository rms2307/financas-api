using Application.Infrastructure;
using Financas.Application.Persistence;
using Financas.Domain;
using System;
using System.Linq;

namespace Financas.Application.Features.Users
{
    public partial class RecuperarUmUser
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

            public User Handle()
            {
                var user = _context.Users.Where(u => u.UserName == _currentUser.UserName)
                    .Select(u => new User
                    {
                        NomeCompleto = u.NomeCompleto,
                        Email = u.Email
                    })
                    .FirstOrDefault();
                return user;
            }
        }
    }
}
