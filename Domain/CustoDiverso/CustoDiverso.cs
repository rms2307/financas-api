using System;
using System.Text.Json.Serialization;

namespace Financas.Domain
{
    public class CustoDiverso
    {
        public int Id { get; set; }
        public string Desc { get; set; }
        public decimal Valor { get; set; }
        public bool Pago { get; set; }
        public DateTime Data { get; set; }
        [JsonIgnore]
        public User User { get; set; }
    }
}
