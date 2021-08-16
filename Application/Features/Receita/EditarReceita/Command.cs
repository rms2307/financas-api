using Financas.Domain;
using System;

namespace Financas.Application.Features.Receitas
{
    public partial class EditarReceita
    {
        public class Command
        {
            public int Id { get; set; }
            public int TipoDeReceitaId { get; set; }
            public decimal Valor { get; set; }
            public DateTime DataRecebimento { get; set; }
        }
    }    
}
