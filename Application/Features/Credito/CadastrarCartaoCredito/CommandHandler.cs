using Financas.Application.Persistence;
using Financas.Domain;
using System.Linq;
using Application.Infrastructure;

namespace Financas.Application.Features.Credito
{
    public partial class CadastrarCartaoCredito
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

                var newCartao = new CartaoCredito
                {
                    User = user,
                    Bandeira = command.Bandeira,
                    DiaFechamentoFatura = command.DiaFechamentoFatura,
                    DiaVencimentoFatura = command.DiaVencimentoFatura,
                    Limite = command.Limite
                };

                _context.CartaoCredito.Add(newCartao);
                _context.SaveChanges();

                return newCartao;
            }
        }
    }
}
