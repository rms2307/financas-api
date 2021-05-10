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
            private readonly RetornarTotalDeGastoDeUmCartao.QueryHandler _totalDeGastos;

            public CommandHandler(FinancasContext context,
                RetornarTotalDeGastoDeUmCartao.QueryHandler totalDeGastos)
            {
                _context = context;
                _totalDeGastos = totalDeGastos;
            }

            public CartaoCreditoCompra Handle(Command command)
            {
                if (command == null || command.Desc == null || command.Desc.Trim() == "")
                    throw new Exception("Informações faltantes.");

                var totalDeGastos = _totalDeGastos.Handle(
                    new RetornarTotalDeGastoDeUmCartao.Query { CartaoCreditoId = command.CartaoCreditoId });

                var cartaoCredito = _context.CartaoCredito
                    .FirstOrDefault(c => c.Id == command.CartaoCreditoId);

                if (totalDeGastos + command.Valor > cartaoCredito.Limite) throw new Exception("Limite do cartão excedido.");

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
                                                         cartaoCredito.DiaVencimentoFatura),
                        NumeroDaParcela = i + 1
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
                    CartaoCredito = cartaoCredito,
                    QtdDeParcelas = command.NumeroDeParcelas
                };

                _context.CartaoCreditoCompra.Add(newGasto);

                _context.SaveChanges();

                return newGasto;
            }

            private List<decimal> RetornarParcelas(decimal valor, decimal totalParcelas)
            {
                decimal varValorParcela = valor / totalParcelas;

                var varParcelas = new List<decimal>();
                for (int i = 0; i < totalParcelas - 1; i++)
                    varParcelas.Add(Math.Round(varValorParcela, 0));

                varParcelas.Add(valor - (Math.Round(varValorParcela, 0) * (totalParcelas - 1)));

                return varParcelas;
            }
        }
    }
}
