using Financas.Application.Persistence;
using System.Linq;

namespace Financas.Application.Features.Autenticacao
{
    public partial class RevokeToken
    {
        public class CommandHandler
        {
            private readonly FinancasContext _context;

            public CommandHandler(FinancasContext context)
            {
                _context = context;
            }

            public bool Handle(Command command)
            {
                var user = _context.Users.FirstOrDefault(u => u.UserName == command.UserName);
                if (user is null) return false;
                user.RefreshToken = null;
                _context.SaveChanges();
                return true;
            }
        }
    }
}
