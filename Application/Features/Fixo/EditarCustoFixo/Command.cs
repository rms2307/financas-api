using System;

namespace Financas.Application.Features.Fixo
{
    public partial class EditarCustoFixo
    {
        public class Command
        {
            public int Id { get; set; }
            public int DescId { get; set; }
            public string Desc { get; set; }
            public decimal Valor { get; set; }
            public DateTime Data { get; set; }
            public bool AlterarApenasMesAtual { get; set; }
            public bool AlterarProximosMeses { get; set; }
            public bool AlterarTodosMeses { get; set; }
        }
    }    
}
