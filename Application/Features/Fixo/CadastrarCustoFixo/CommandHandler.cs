using System;
using System.Collections.Generic;
using Financas.Application.Persistence;
using Financas.Domain;

namespace Financas.Application.Features.Fixo
{
    public partial class CadastrarCustoFixo
    {
        public class CommandHandler
        {
            private readonly FinancasContext _context;

            public CommandHandler(FinancasContext context)
            {
                _context = context;
            }

            public List<CustoFixo> Handle(Command command)
            {
                if (command == null || command.Desc == null || command.Desc.Trim() == "")
                    throw new Exception("Informações faltantes.");

                var custoFixoDescricao = new CustoFixoDescricao
                {
                    Desc = command.Desc
                };

                List<CustoFixo> listaCustos = new();

                for (int i = 0; i < command.RepetirCusto; i++)
                {
                    var proxMes = command.Data.AddMonths(i);

                    var newCusto = new CustoFixo
                    {
                        Valor = command.Valor,
                        Data = proxMes,
                        Pago = false,
                        CustoFixoDescricao = custoFixoDescricao
                    };

                    listaCustos.Add(newCusto);
                }

                _context.AddRange(listaCustos);
                _context.SaveChanges();

                return listaCustos;
            }
        }
    }
}