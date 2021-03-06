using System;
using System.Collections.Generic;
using System.Linq;
using LanguageExt;
using Financas.Application.Persistence;
using Financas.Domain;
using Microsoft.EntityFrameworkCore;
using Application.Infrastructure;

namespace Financas.Application.Features.Credito
{
    public partial class EditarGastoCartaoCredito
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

            public CartaoCreditoCompra Handle(Command command)
            {
                if (command == null || command.Desc == null || command.Desc.Trim() == "")
                    throw new Exception("Informações faltantes.");

                var user = _context.Users.FirstOrDefault(u => u.UserName == _currentUser.UserName);

                var gasto = _context.CartaoCreditoCompra
                    .Include(c => c.CartaoCredito)
                    .Include(c => c.CartaoCreditoParcelas)
                    .FirstOrDefault(c => c.Id == command.Id);

                var cartaoCredito = _context.CartaoCredito
                    .FirstOrDefault(c => c.Id == command.CartaoCreditoId && c.User == user);

                if (gasto.IsNull() || cartaoCredito.IsNull()) throw new Exception("Registro não encontrado");

                gasto.Desc = command.Desc;
                gasto.Valor = command.Valor;
                gasto.DataCompra = command.DataCompra;
                gasto.CartaoCredito = cartaoCredito;

                _context.CartaoCreditoParcela.RemoveRange(gasto.CartaoCreditoParcelas);

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

                gasto.CartaoCreditoParcelas = parcelas;

                _context.SaveChanges();
                return gasto;
            }
        }
    }
}

