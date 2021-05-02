using System.Linq;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using Financas.Application.Persistence;
using Financas.Domain;
using System;

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

            public CustoFixo Handle(Query query)
            {
                var result = _context.CustoFixo
                    .FirstOrDefault(cd => cd.Id == query.Id);

                if (result.IsNull()) throw new Exception("Registro não encontrado");

                return result;
            }
        }
    }
}
