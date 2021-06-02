using System.Linq;
using Financas.Application.Persistence;
using Financas.Domain;
using Application.Infrastructure;

namespace Financas.Application.Features.Credito
{
    public partial class EditarCartaoCredito
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

            public CartaoCredito Handle(Command command)
            {
                var user = _context.Users.FirstOrDefault(u => u.UserName == _currentUser.UserName);

                var cartaoCredito = _context.CartaoCredito
                    .FirstOrDefault(c => c.Id == command.Id && c.User == user);

                cartaoCredito.Bandeira = command.Bandeira;
                cartaoCredito.DiaFechamentoFatura = command.DiaFechamentoFatura;
                cartaoCredito.DiaVencimentoFatura = command.DiaVencimentoFatura;
                cartaoCredito.Limite = command.Limite;

                _context.SaveChanges();
                return cartaoCredito;
            }
        }
    }
}

