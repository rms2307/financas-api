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
    public partial class RemoverUmCustoDiverso
    {
        public class CommandHandler
        {
            private readonly FinancasContext _context;

            public CommandHandler(FinancasContext context)
            {
                _context = context;
            }

            public void Handle(Command command)
            {
                CustoDiverso custo = _context.CustoDiverso
                    .FirstOrDefault(c => c.Id == command.Id);

                if (custo.IsNull()) throw new Exception("Registro não encontrado");

                _context.CustoDiverso.Remove(custo);
                _context.SaveChanges();
            }
        }
    }
}
