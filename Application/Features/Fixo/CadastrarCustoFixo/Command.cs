using System;

namespace Financas.Application.Features.Fixo
{
    public partial class CadastrarCustoFixo
    {
        public class Command
        {
            public string Desc { get; set; }
            public decimal Valor { get; set; }
            public DateTime Data { get; set; }
            public int RepetirCusto { get; set; }
        }
    }    
}
