using System;
using System.Linq;
using LanguageExt;
using Financas.Application.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Financas.Application.Features.Fixo
{
    public partial class EditarCustoFixo
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
                if (command == null || command.Desc.Trim() == "" || command.Desc == null)
                    throw new Exception("Informações faltantes.");

                if (command.AlterarApenasMesAtual)
                {
                    var custoDoMes = _context.CustoFixo
                        .Include(c => c.CustoFixoDescricao)
                        .FirstOrDefault(c => c.CustoFixoDescricaoId == command.DescId
                                    && c.Id == command.Id);

                    if (custoDoMes.IsNull()) throw new Exception("Registro não encontrado");

                    custoDoMes.CustoFixoDescricao.Desc = command.Desc;
                    custoDoMes.Data = command.Data;
                    custoDoMes.Valor = command.Valor;

                    _context.SaveChanges();
                }
                else if (command.AlterarProximosMeses)
                {
                    var custoAtual = _context.CustoFixo
                        .Include(c => c.CustoFixoDescricao)
                        .Where(c => c.CustoFixoDescricaoId == command.DescId && c.Id == command.Id)
                        .SingleOrDefault();

                    if (custoAtual.IsNull()) throw new Exception("Registro não encontrado");

                    var custos = _context.CustoFixo
                        .Include(c => c.CustoFixoDescricao)
                        .Where(c => c.CustoFixoDescricaoId == command.DescId && c.Data.Month >= custoAtual.Data.Month)
                        .ToList();

                    if (custos.Any())
                    {
                        var mes = 0;
                        foreach (var c in custos)
                        {
                            c.CustoFixoDescricao.Desc = command.Desc;
                            c.Data = command.Data.AddMonths(mes);
                            c.Valor = command.Valor;
                            mes++;
                        };

                        _context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Registro não encontrado");
                    }
                }
                else if (command.AlterarTodosMeses)
                {
                    var custos = _context.CustoFixo
                        .Include(c => c.CustoFixoDescricao)
                        .Where(c => c.CustoFixoDescricaoId == command.DescId)
                        .ToList();

                    if (custos.Any())
                    {
                        foreach (var c in custos)
                        {
                            c.CustoFixoDescricao.Desc = command.Desc;
                            c.Data = new DateTime(c.Data.Year, c.Data.Month, command.Data.Day);
                            c.Valor = command.Valor;
                        };

                        _context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Registro não encontrado");
                    }
                }
            }
        }
    }
}
