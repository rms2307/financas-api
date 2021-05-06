using Financas.Domain;

namespace Financas.Application.Features.Credito
{
    public partial class EditarCartaoCredito
    {
        public class Command
        {
            public int Id { get; set; }
            public Bandeira Bandeira { get; set; }
            public int DiaFechamentoFatura { get; set; }
            public int DiaVencimentoFatura { get; set; }
            public decimal Limite { get; set; }
        }
    }    
}
