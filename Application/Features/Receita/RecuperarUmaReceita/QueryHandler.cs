using System.Linq;
using Financas.Application.Persistence;
using Financas.Domain;

namespace Financas.Application.Features.Receitas
{
    public partial class RecuperarUmaReceita
    {
        public class QueryHandler
        {
            private readonly FinancasContext _context;

            public QueryHandler(FinancasContext context)
            {
                _context = context;
            }

            public Receita Handle(Query query)
            {
                var receita = _context.Receita
                    .FirstOrDefault(r => r.Id == query.Id);

                return receita;
            }

        }
    }
}
