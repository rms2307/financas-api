using System;
using System.Collections.Generic;
using Financas.Application.Persistence;
using Financas.Domain;
using System.Linq;

namespace Financas.Application.Features.Credito
{
    public partial class CadastrarCartaoCredito
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
                var newCartao = new CartaoCredito
                {
                    Bandeira = command.Bandeira,
                    DiaFechamentoFatura = command.DiaFechamentoFatura,
                    DiaVencimentoFatura = command.DiaVencimentoFatura,
                    Limite = command.Limite
                };

                _context.CartaoCredito.Add(newCartao);
                _context.SaveChanges();

                return newCartao;
            }
        }
    }
}
