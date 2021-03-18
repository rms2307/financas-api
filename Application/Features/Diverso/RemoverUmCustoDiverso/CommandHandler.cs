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
                Option<CustoDiverso> custo = _context.CustoDiverso
                    .SingleOrDefault(c => c.Id == command.Id);

                custo.Match(
                    Some: c =>
                    {
                        _context.CustoDiverso.Remove(c);
                        _context.SaveChanges();                        
                    },
                    None: () =>
                    {
                        throw new Exception("Registro não encontrado");
                    });               
            }
        }
    }
}
