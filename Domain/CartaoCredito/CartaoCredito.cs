namespace Financas.Domain
{
    public class CartaoCredito
    {
        public int Id { get; set; }
        public Bandeira Bandeira { get; set; }
        public int DiaFechamentoFatura { get; set; }
        public int DiaVencimentoFatura { get; set; }
        public decimal Limite { get; set; }
    }
}
