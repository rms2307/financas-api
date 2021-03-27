using System;
using Financas.Domain;

namespace Financas.Application.Features.Receitas
{
    public partial class CadastrarReceita
    {
        public class Command
        {
            public TipoDeReceita TipoDeReceita { get; set; }
            public decimal Valor { get; set; }
            public DateTime DataRecebimento { get; set; }
        }
    }
}
