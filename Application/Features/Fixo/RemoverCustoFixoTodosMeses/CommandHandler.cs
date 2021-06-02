using System;
using System.Linq;
using LanguageExt;
using Financas.Application.Persistence;
using Financas.Domain;
using Microsoft.EntityFrameworkCore;
using Application.Infrastructure;

namespace Financas.Application.Features.Fixo
{
    public partial class RemoverCustoFixoTodosMeses
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

                var custoAtual = _context.CustoFixo
                        .Include(c => c.CustoFixoDescricao)
                        .FirstOrDefault(c => c.Id == command.Id && 
                                        c.CustoFixoDescricao.User == user);

                var custos = _context.CustoFixo
                    .Include(c => c.CustoFixoDescricao)
                    .Where(c => c.CustoFixoDescricaoId == custoAtual.CustoFixoDescricaoId &&
                                c.CustoFixoDescricao.User == user)
                    .ToList();

                if (custos.Any())
                {
                    _context.CustoFixo.RemoveRange(custos);
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
