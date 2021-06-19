using Application.Infrastructure;
using Financas.Application.Persistence;
using System.Linq;

namespace Financas.Application.Features.Autenticacao
{
    public partial class RevokeToken
    {
        public class CommandHandler
        {
            private readonly FinancasContext _context;
            private readonly ICurrentUser _currentUser;

            public CommandHandler(FinancasContext context, ICurrentUser currentUser)
            {
                _context = context;
                _currentUser = currentUser;
            }

            public bool Handle()
            {
                var user = _context.Users.FirstOrDefault(u => u.UserName == _currentUser.UserName);
                if (user is null) return false;
                user.RefreshToken = null;
                _context.SaveChanges();
                return true;
            }
        }
    }
}
