﻿using System.Collections.Generic;
using System.Linq;
using LanguageExt;
using Financas.Application.Persistence;
using Financas.Domain;

namespace Financas.Application.Features.Credito
{
    public partial class RecuperarGastosCartaoCredito
    {
        public class QueryHandler
        {
            private readonly FinancasContext _context;

            public QueryHandler(FinancasContext context)
            {
                _context = context;
            }

            public IEnumerable<CartaoCredito> Handle(Query query)
            {
                var gastosCartao = _context.CartaoCredito
                    .Where(cd => cd.Data.Month == query.MesAtual)
                    .OrderByDescending(c => c.Data)
                    .ToList();

                return gastosCartao;
            }
        }
    }
}