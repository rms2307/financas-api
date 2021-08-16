using System.Collections.Generic;
using System.Linq;
using Financas.Application.Persistence;
using Financas.Domain;
using LanguageExt;

namespace Financas.Application.Features.Receitas
{
    public partial class RecuperarOsTiposDeReceitas
    {
        public class QueryHandler
        {
            private readonly FinancasContext _context;

            public QueryHandler(FinancasContext context)
            {
                _context = context;
            }

            public IEnumerable<TipoDeReceita> Handle()
            {
                var tipos = _context.TipoDeReceita.ToList();

                return tipos;
            }
        }
    }
}
