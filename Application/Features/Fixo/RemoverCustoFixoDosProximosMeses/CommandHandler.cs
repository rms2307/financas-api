using System;
using System.Linq;
using LanguageExt;
using Financas.Application.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Financas.Application.Features.Fixo
{
    public partial class RemoverCustoFixoDosProximosMeses
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
                var custoAtual = _context.CustoFixo
                    .Include(c => c.CustoFixoDescricao)
                    .SingleOrDefault(c => c.Id == command.Id);

                if (custoAtual.IsNull()) throw new Exception("Registro não encontrado");

                var custos = _context.CustoFixo
                    .Include(c => c.CustoFixoDescricao)
                    .Where(c => c.CustoFixoDescricaoId == custoAtual.CustoFixoDescricaoId
                                && c.Data.Month >= custoAtual.Data.Month)
                    .ToList();

                if (custos.Any())
                {
                    _context.CustoFixo.RemoveRange(custos);
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("Registro não encontrado");
                }
            }
        }
    }
}
