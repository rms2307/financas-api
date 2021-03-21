using System;
using System.Linq;
using LanguageExt;
using Financas.Application.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Financas.Application.Features.Fixo
{
    public partial class RemoverCustoFixoDoMesAtual
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
                var custoDoMes = _context.CustoFixo
                    .Include(c => c.CustoFixoDescricao)
                    .SingleOrDefault(c => c.Id == command.Id);

                if (custoDoMes.IsNull()) throw new Exception("Registro não encontrado");

                _context.CustoFixo.Remove(custoDoMes);
                _context.SaveChanges();

            }
        }
    }
}


