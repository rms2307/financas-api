using System;
using System.Linq;
using LanguageExt;
using Financas.Application.Persistence;
using Microsoft.EntityFrameworkCore;
using Application.Infrastructure;

namespace Financas.Application.Features.Fixo
{
    public partial class RemoverCustoFixoDoMesAtual
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

                var custoDoMes = _context.CustoFixo
                    .Include(c => c.CustoFixoDescricao)
                    .FirstOrDefault(c => c.Id == command.Id && c.CustoFixoDescricao.User == user);

                if (custoDoMes.IsNull()) throw new Exception("Registro não encontrado");

                _context.CustoFixo.Remove(custoDoMes);
                _context.SaveChanges();

            }
        }
    }
}


