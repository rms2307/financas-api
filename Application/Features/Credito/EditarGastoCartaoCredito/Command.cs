using System;

namespace Financas.Application.Features.Credito
{
    public partial class EditarGastoCartaoCredito
    {
        public class Command
        {
            public int Id { get; set; }
            public string Desc { get; set; }
            public decimal Valor { get; set; }
            public DateTime DataCompra { get; set; }
            public int NumeroDeParcelas { get; set; }
            public int CartaoCreditoId { get; set; }
        }
    }    
}
