using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageExt;
using Financas.Application.Persistence;
using Financas.Domain;

namespace Financas.Application.Features.Users
{
    public partial class AtualizarUser
    {
        public class CommandHandler
        {
            private readonly FinancasContext _context;

            public CommandHandler(FinancasContext context)
            {
                _context = context;
            }

            public User Handle(Command command)
            {
                if (command == null || command.UserName == null || command.UserName.Trim() == "" ||
                        command.Password == null || command.Password.Trim() == "" ||
                        command.NomeCompleto == null || command.NomeCompleto.Trim() == "")
                    throw new Exception("Informações faltantes.");

                var user = _context.Users
                    .FirstOrDefault(c => c.Id.Equals(command.Id));

                if (user.IsNull()) throw new Exception("Usuario não encontrado.");

                _context.Entry(user).CurrentValues.SetValues(command);

                _context.SaveChanges();

                return user;
            }
        }
    }
}

