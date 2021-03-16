using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using Financas.Application.Persistence;
using Financas.Domain;

namespace Financas.Application.Features.Fixo
{
    public partial class RecuperarUmCustoFixo
    {
        public class QueryHandler
        {
            private readonly FinancasContext _context;

            public QueryHandler(FinancasContext context)
            {
                _context = context;
            }

            public Option<CustoFixo> Handle(Query query)
            {
                var result = _context.CustoFixo
                    .Where(cd => cd.Id == query.Id)
                    .AsNoTracking()
                    .SingleOrDefault();

                return result;
            }
        }
    }
}
