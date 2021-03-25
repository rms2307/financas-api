using System;
using System.Collections.Generic;

namespace Financas.Domain
{
    public class CartaoCreditoCompra
    {
        public int Id { get; set; }
        public string Desc { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataCompra { get; set; }
        public CartaoCredito CartaoCredito { get; set; }
        public List<CartaoCreditoParcela> CartaoCreditoParcelas { get; set; }
    }
}
