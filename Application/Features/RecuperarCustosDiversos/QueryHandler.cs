﻿using System.Collections.Generic;
using System.Linq;
using LanguageExt;
using Financas.Application.Persistence;
using Financas.Domain;

namespace Financas.Application.Features.RecuperarCustosDiversos
{
    public partial class RecuperarCustosDiversos
    {
        public class QueryHandler
        {
            private readonly FinancasContext _context;

            public QueryHandler(FinancasContext context)
            {
                _context = context;
            }

            public IEnumerable<CustoDiverso> Handle(Query query)
            {
                var custosDiversos = _context.CustoDiverso
                    .OrderByDescending(c => c.Data)
                    .ToList();

                return custosDiversos;
            }
        }
    }
}
