using System;
using System.Linq;
using LanguageExt;
using Financas.Application.Persistence;
using Financas.Domain;

namespace Financas.Application.Features.Receitas
{
    public partial class RemoverUmaReceita
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
                Receita receita = _context.Receita
                    .FirstOrDefault(r => r.Id == command.Id);

                if (receita.IsNull()) throw new Exception("Registro não encontrado");

                _context.Receita.Remove(receita);
                _context.SaveChanges();
            }
        }
    }
}
