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
                if (!string.IsNullOrWhiteSpace(command.NomeCompleto) ||
                    !string.IsNullOrWhiteSpace(command.UserName) ||
                    !string.IsNullOrWhiteSpace(command.Password) ||
                    !string.IsNullOrWhiteSpace(command.RefreshToken) ||
                    !string.IsNullOrWhiteSpace(command.Email))
                {
                    var user = _context.Users
                       .FirstOrDefault(c => c.Id.Equals(command.Id));

                    if (user.IsNull()) throw new Exception("Usuario não encontrado.");

                    _context.Entry(user).CurrentValues.SetValues(command);

                    _context.SaveChanges();

                    return user;
                }

                throw new Exception("Informações faltantes.");
            }
        }
    }
}

