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
    public partial class EditarCustoDiverso
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

                Option<CustoDiverso> custo = _context.CustoDiverso
                    .SingleOrDefault(c => c.Id == command.Id);

                return custo.Match(
                    Some: c =>
                    {
                        c.Desc = command.Desc;
                        c.Data = command.Data;
                        c.Valor = command.Valor;

                        _context.SaveChanges();

                        return c;
                    },
                    None: () =>
                    {
                        throw new Exception("Registro não encontrado");
                    });
            }
        }
    }
}
