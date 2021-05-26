using System;
using LanguageExt;
using Financas.Application.Persistence;
using Financas.Domain;
using Application.Infrastructure;
using System.Linq;

namespace Financas.Application.Features.Diverso
{
    public partial class CadastrarCustoDiverso
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

                var newCusto = new CustoDiverso
                {
                    Desc = command.Desc,
                    Valor = command.Valor,
                    Data = command.Data,
                    Pago = false,
                    User = user
                };

                _context.CustoDiverso.Add(newCusto);
                _context.SaveChanges();

                return newCusto;
            }
        }
    }
}
