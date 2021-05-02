using System;
using System.Collections.Generic;
using Financas.Application.Persistence;
using Financas.Domain;
using System.Linq;

namespace Financas.Application.Features.Credito
{
    public partial class CadastrarGastoCartaoCredito
    {
        public class CommandHandler
        {
            private readonly FinancasContext _context;

            public CommandHandler(FinancasContext context)
            {
                _context = context;
            }

            public CartaoCreditoCompra Handle(Command command)
            {
                if (command == null || command.Desc == null || command.Desc.Trim() == "")
                    throw new Exception("Informações faltantes.");

                var cartaoCredito = _context.CartaoCredito
                    .FirstOrDefault(c => c.Id == command.CartaoCreditoId);
                
                int incrementaMes;
                if (command.DataCompra.Day > cartaoCredito.DiaFechamentoFatura) incrementaMes = 1; 
                else incrementaMes = 0;

                var parcelas = new List<CartaoCreditoParcela>();
                for (int i = 0; i < command.NumeroDeParcelas; i++)
                {
                    var parcela = new CartaoCreditoParcela
                    {
                        ValorParcela = command.Valor / command.NumeroDeParcelas,
                        VencimentoParcela = new DateTime(command.DataCompra.Year,
                                                         command.DataCompra.Month + incrementaMes, 
                                                         cartaoCredito.DiaVencimentoFatura)
                    };
                    parcelas.Add(parcela);
                    incrementaMes++;
                }

                var newGasto = new CartaoCreditoCompra
                {
                    Desc = command.Desc,
                    Valor = command.Valor,
                    DataCompra = command.DataCompra,
                    CartaoCreditoParcelas = parcelas,
                    CartaoCredito = cartaoCredito
                };

                _context.CartaoCreditoCompra.Add(newGasto);

                _context.SaveChanges();

                return newGasto;
            }
        }
    }
}
