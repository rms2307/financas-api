using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financas.Domain
{
    public class Receita
    {
        public int Id { get; set; }
        public TipoDeReceita TipoDeReceita { get; set; }
        public DateTime DataRecebimento { get; set; }
        public decimal Valor { get; set; }
    }
}
