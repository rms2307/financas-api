using System.Collections.Generic;
using System.Linq;
using LanguageExt;
using Financas.Application.Persistence;
using Financas.Domain;
using Microsoft.EntityFrameworkCore;

namespace Financas.Application.Features.Fixo
{
    public partial class RecuperarCustosFixosDoMes
    {
        public class QueryHandler
        {
            private readonly FinancasContext _context;

            public QueryHandler(FinancasContext context)
            {
                _context = context;
            }

            public IEnumerable<CustoFixo> Handle(Query query)
            {
                var custosFixos = _context.CustoFixo
                    .Include(c => c.CustoFixoDescricao)
                    .Where(c => c.Data.Month == query.MesAtual)
                    .OrderByDescending(c => c.Data)
                    .ToList();

                return custosFixos;
            }
        }
    }
}
