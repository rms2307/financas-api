using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageExt;
using Financas.Application.Persistence;
using Financas.Domain;
using Application.Infrastructure;

namespace Financas.Application.Features.Diverso
{
    public partial class EditarCustoDiverso
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

            public CustoDiverso Handle(Command command)
            {
                if (command == null || command.Desc == null || command.Desc.Trim() == "")
                    throw new Exception("Informações faltantes.");

                var user = _context.Users.FirstOrDefault(u => u.UserName == _currentUser.UserName);

                var custo = _context.CustoDiverso
                    .FirstOrDefault(c => c.Id == command.Id && c.User == user);

                if (custo.IsNull()) throw new Exception("Registro não encontrado");

                custo.Desc = command.Desc;
                custo.Data = command.Data;
                custo.Valor = command.Valor;

                _context.SaveChanges();

                return custo;
            }
        }
    }
}

