using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financas.Application.Features.Diverso
{
    public partial class CadastrarCustoDiverso
    {
        public class Command
        {
            public string Desc { get; set; }
            public decimal Valor { get; set; }
            public DateTime Data { get; set; }
        }
    }    
}
