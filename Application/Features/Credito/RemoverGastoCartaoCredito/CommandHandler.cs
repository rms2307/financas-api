using System;
using System.Linq;
using LanguageExt;
using Financas.Application.Persistence;
using Financas.Domain;
using Microsoft.EntityFrameworkCore;
using Application.Infrastructure;
using System.Collections.Generic;

namespace Financas.Application.Features.Credito
{
    public partial class RemoverGastoCartaoCredito
    {
        public class CommandHandler
        {
            private readonly FinancasContext _context;
            private readonly ICurrentUser _currentUser;

            public CommandHandler(FinancasContext context, ICurrentUser currentUser)
            {
                _context = context;
                _currentUser = currentUser;
            }


            public void Handle(Command command)
            {
                var user = _context.Users.FirstOrDefault(u => u.UserName == _currentUser.UserName);

                CartaoCreditoCompra gasto = _context.CartaoCreditoCompra
                    .Include(c => c.CartaoCreditoParcelas)
                    .Include(c => c.CartaoCredito)
                    .FirstOrDefault(c => c.Id == command.Id && c.CartaoCredito.User == user);

                List<CartaoCreditoParcela> parcelas = _context.CartaoCreditoParcela
                    .Include(p => p.CartaoCreditoCompra)
                    .Where(p => p.CartaoCreditoCompra.Id == command.Id)
                    .ToList();

                if (gasto.IsNull() && parcelas.IsNull()) throw new Exception("Registro não encontrado");


                _context.CartaoCreditoParcela.RemoveRange(parcelas);
                _context.CartaoCreditoCompra.Remove(gasto);
                _context.SaveChanges();
            }
        }
    }
}
