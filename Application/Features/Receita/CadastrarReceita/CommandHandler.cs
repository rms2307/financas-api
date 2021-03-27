using System;
using Financas.Application.Persistence;
using Financas.Domain;
using LanguageExt;

namespace Financas.Application.Features.Receitas
{
    public partial class CadastrarReceita
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

                var newReceita = new Receita
                {
                    TipoDeReceita = command.TipoDeReceita,
                    Valor = command.Valor,
                    DataRecebimento = command.DataRecebimento
                };
                
                _context.Receita.Add(newReceita);
                _context.SaveChanges();

                return newReceita;
            }
        }
    }
}
