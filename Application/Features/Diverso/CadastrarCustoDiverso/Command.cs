using System;

namespace Financas.Application.Features.Diverso
{
    public partial class CadastrarCustoDiverso
    {
        public class Command
        {
            public string Desc { get; set; }
            public decimal Valor { get; set; }
            public DateTime Data { get; set; }
        }
    }    
}
