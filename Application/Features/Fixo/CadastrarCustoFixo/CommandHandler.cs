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

                var descricaoId = InserirDescricao(command.Desc);

                List<CustoFixo> listaCustos = new();

                for (int i = 0; i < command.AddPorXMeses; i++)
                {
                    var proxMes = command.Data.AddMonths(i);

                    var newCusto = new CustoFixo
                    {
                        CustoFixoDescricaoId = descricaoId,
                        Valor = command.Valor,
                        Data = proxMes,
                        Pago = false
                    };

                    listaCustos.Add(newCusto);
                }

                _context.AddRange(listaCustos);
                _context.SaveChanges();

                return listaCustos;
            }

            private int InserirDescricao(string descricao)
            {
                var custoFixoDescricao = new CustoFixoDescricao
                {
                    Desc = descricao
                };

                _context.CustoFixoDescricao.Add(custoFixoDescricao);
                _context.SaveChanges();

                return custoFixoDescricao.Id;
            }
        }
    }
}
