using System;
using System.Collections.Generic;

namespace Financas.Domain
{
    public class CartaoCredito
    {
        public int Id { get; set; }
        public string Bandeira { get; set; }
        public int DiaFechamentoFatura { get; set; }
        public int DiaVencimentoFatura { get; set; }
    }
}
