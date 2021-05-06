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
    public partial class EditarCartaoCredito
    {
        public class CommandHandler
        {
            private readonly FinancasContext _context;

            public CommandHandler(FinancasContext context)
            {
                _context = context;
            }

            public CartaoCredito Handle(Command command)
            {
                var cartaoCredito = _context.CartaoCredito
                    .FirstOrDefault(c => c.Id == command.Id);

                cartaoCredito.Bandeira = command.Bandeira;
                cartaoCredito.DiaFechamentoFatura = command.DiaFechamentoFatura;
                cartaoCredito.DiaVencimentoFatura = command.DiaVencimentoFatura;
                cartaoCredito.Limite = command.Limite;

                _context.SaveChanges();
                return cartaoCredito;
            }
        }
    }
}

