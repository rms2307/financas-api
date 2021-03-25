using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageExt;
using Financas.Application.Persistence;
using Financas.Domain;
using Microsoft.EntityFrameworkCore;

namespace Financas.Application.Features.Credito
{
    public partial class RemoverGastoCartaoCredito
    {
        public class CommandHandler
        {
            private readonly FinancasContext _context;

            public CommandHandler(FinancasContext context)
            {
                _context = context;
            }

            public void Handle(Command command)
            {
                CartaoCreditoCompra gasto = _context.CartaoCreditoCompra
                    .Include(c => c.CartaoCreditoParcelas)
                    .SingleOrDefault(c => c.Id == command.Id);

                if (gasto.IsNull()) throw new Exception("Registro não encontrado");

                _context.CartaoCreditoCompra.Remove(gasto);
                _context.SaveChanges();
            }
        }
    }
}
