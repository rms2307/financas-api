using System;
using System.Linq;
using LanguageExt;
using Financas.Application.Persistence;
using Financas.Domain;
using Application.Infrastructure;

namespace Financas.Application.Features.Receitas
{
    public partial class RemoverUmaReceita
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

            public void Handle(Command command)
            {
                var user = _context.Users.FirstOrDefault(u => u.UserName == _currentUser.UserName);

                Receita receita = _context.Receita
                    .FirstOrDefault(r => r.Id == command.Id && r.User == user);

                if (receita.IsNull()) throw new Exception("Registro não encontrado");

                _context.Receita.Remove(receita);
                _context.SaveChanges();
            }
        }
    }
}
