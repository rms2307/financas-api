using System.Collections.Generic;
using System.Linq;
using Financas.Application.Persistence;
using Financas.Domain;
using LanguageExt;

namespace Financas.Application.Features.Receitas
{
    public partial class RecuperarReceitasDoMes
    {
        public class QueryHandler
        {
            private readonly FinancasContext _context;

            public QueryHandler(FinancasContext context)
            {
                _context = context;
            }

            public IEnumerable<Receita> Handle(Query query)
            {
                var receitas = _context.Receita
                    .Where(r => r.DataRecebimento.Month == query.MesAtual)
                    .OrderByDescending(r => r.DataRecebimento)
                    .ToList();

                return receitas;
            }

        }
    }
}
