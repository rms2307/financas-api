using System;
using System.Collections.Generic;
using System.Linq;
using Application.Infrastructure;
using Financas.Application.Persistence;
using Financas.Domain;

namespace Financas.Application.Features.Fixo
{
    public partial class CadastrarCustoFixo
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

            public List<CustoFixo> Handle(Command command)
            {
                if (command == null || command.Desc == null || command.Desc.Trim() == "")
                    throw new Exception("Informações faltantes.");

                var user = _context.Users.FirstOrDefault(u => u.UserName == _currentUser.UserName);

                var custoFixoDescricao = new CustoFixoDescricao
                {
                    Desc = command.Desc,
                    User = user
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