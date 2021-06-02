using System;
using System.Linq;
using Application.Infrastructure;
using Financas.Application.Persistence;
using Financas.Domain;
using LanguageExt;

namespace Financas.Application.Features.Receitas
{
    public partial class CadastrarReceita
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

            public Receita Handle(Command command)
            {
                if (command.IsNull()) throw new Exception("Informações faltantes.");

                var user = _context.Users.FirstOrDefault(u => u.UserName == _currentUser.UserName);

                var newReceita = new Receita
                {
                    TipoDeReceita = command.TipoDeReceita,
                    Valor = command.Valor,
                    DataRecebimento = command.DataRecebimento,
                    User = user
                };
                
                _context.Receita.Add(newReceita);
                _context.SaveChanges();

                return newReceita;
            }
        }
    }
}
