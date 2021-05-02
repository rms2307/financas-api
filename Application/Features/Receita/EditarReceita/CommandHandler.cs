using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageExt;
using Financas.Application.Persistence;
using Financas.Domain;

namespace Financas.Application.Features.Receitas
{
    public partial class EditarReceita
    {
        public class CommandHandler
        {
            private readonly FinancasContext _context;

            public CommandHandler(FinancasContext context)
            {
                _context = context;
            }

            public Receita Handle(Command command)
            {
                if (command.IsNull()) throw new Exception("Informações faltantes.");

                var receita = _context.Receita
                    .FirstOrDefault(r => r.Id == command.Id);

                if (receita.IsNull()) throw new Exception("Registro não encontrado");

                receita.TipoDeReceita = command.TipoDeReceita;
                receita.DataRecebimento = command.DataRecebimento;
                receita.Valor = command.Valor;

                _context.SaveChanges();
                return receita;
            }
        }
    }
}

