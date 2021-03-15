using System.Collections.Generic;
using System.Linq;
using LanguageExt;
using Financas.Application.Persistence;
using Financas.Domain;

namespace Financas.Application.Features.RecuperarCustosFixos
{
    public partial class RecuperarCustosFixos
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
                    .OrderByDescending(c => c.Data)
                    .ToList();

                return custosFixos;
            }
        }
    }
}
