using System;

namespace Financas.Domain
{
    public class CustoFixo
    {
        public int Id { get; set; }
        public string Desc { get; set; }
        public decimal Valor { get; set; }
        public bool Pago { get; set; }
        public DateTime Data { get; set; }
    }
}
