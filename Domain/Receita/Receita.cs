using System;
using System.Text.Json.Serialization;

namespace Financas.Domain
{
    public class Receita
    {
        public int Id { get; set; }
        public TipoDeReceita TipoDeReceita { get; set; }
        public DateTime DataRecebimento { get; set; }
        public decimal Valor { get; set; }
        [JsonIgnore]
        public User User { get; set; }
    }
}
