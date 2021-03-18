using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageExt;
using Financas.Application.Persistence;
using Financas.Domain;

namespace Financas.Application.Features.Diverso
{
    public partial class CadastrarCustoDiverso
    {
        public class CommandHandler
        {
            private readonly FinancasContext _context;

            public CommandHandler(FinancasContext context)
            {
                _context = context;
            }

            public Option<CustoDiverso> Handle(Command command)
            {
                if (command == null || command.Desc == null || command.Desc.Trim() == "") 
                    throw new Exception("Informações faltantes.");
                
                var newCusto = new CustoDiverso
                {
                    Desc = command.Desc,
                    Valor = command.Valor,
                    Data = command.Data,
                    Pago = false
                };

                _context.Add(newCusto);
                _context.SaveChanges();

                return newCusto;
            }
        }
    }
}
