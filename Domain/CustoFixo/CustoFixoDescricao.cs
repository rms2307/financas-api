using System;
using System.Text.Json.Serialization;

namespace Financas.Domain
{
    public class CustoFixoDescricao
    {
        public int Id { get; set; }
        public string Desc { get; set; }
        [JsonIgnore]
        public User User { get; set; }
    }
}
