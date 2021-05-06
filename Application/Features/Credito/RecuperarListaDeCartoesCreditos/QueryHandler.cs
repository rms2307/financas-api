using System.Collections.Generic;
using System.Linq;
using LanguageExt;
using Financas.Application.Persistence;
using Financas.Domain;
using Microsoft.EntityFrameworkCore;

namespace Financas.Application.Features.Credito
{
    public partial class RecuperarListaDeCartoesCreditos
    {
        public class QueryHandler
        {
            private readonly FinancasContext _context;

            public QueryHandler(FinancasContext context)
            {
                _context = context;
            }

            public IEnumerable<CartaoCredito> Handle()
            {
                var cartoes = _context.CartaoCredito.ToList();

                return cartoes;
            }
        }
    }
}
