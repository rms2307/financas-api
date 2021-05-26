
namespace Financas.Application.Features.Credito
{
    public partial class RecuperarFaturaCartaoCreditoDoMes
    {
        public class Query
        {
            public int MesAtual { get; set; }
            public int CartaoCreditoId { get; set; }
        }
    }
}
