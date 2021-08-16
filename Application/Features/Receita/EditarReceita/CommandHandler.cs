using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageExt;
using Financas.Application.Persistence;
using Financas.Domain;
using Application.Infrastructure;

namespace Financas.Application.Features.Receitas
{
    public partial class EditarReceita
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
                var tipoDeReceita = _context.TipoDeReceita.FirstOrDefault(tr => tr.Id == command.TipoDeReceitaId);

                var receita = _context.Receita
                    .FirstOrDefault(r => r.Id == command.Id && r.User == user);

                if (receita.IsNull()) throw new Exception("Registro não encontrado");

                receita.TipoDeReceita = tipoDeReceita;
                receita.DataRecebimento = command.DataRecebimento;
                receita.Valor = command.Valor;

                _context.SaveChanges();
                return receita;
            }
        }
    }
}

