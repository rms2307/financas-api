using Financas.Domain;
using System;

namespace Financas.Application.Features.Credito
{
    public partial class CadastrarCartaoCredito
    {
        public class Command
        {
            public Bandeira Bandeira { get; set; }
            public int DiaFechamentoFatura { get; set; }
            public int DiaVencimentoFatura { get; set; }
            public decimal Limite { get; set; }
        }
    }
}
