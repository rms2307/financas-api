using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using Financas.Application.Persistence;
using Financas.Domain;

namespace Financas.Application.Features.Diverso
{
    public partial class RecuperarUmCustoDiverso
    {
        public class QueryHandler
        {
            private readonly FinancasContext _context;

            public QueryHandler(FinancasContext context)
            {
                _context = context;
            }

            public Option<CustoDiverso> Handle(Query query)
            {
                var result = _context.CustoDiverso
                    .Where(cd => cd.Id == query.Id)
                    .AsNoTracking()
                    .SingleOrDefault();

                return result;
            }
        }
    }
}
