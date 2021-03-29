﻿using System;
using System.Text.Json.Serialization;

namespace Financas.Domain
{
    public class CartaoCreditoParcela
    {
        public int Id { get; set; }
        public DateTime VencimentoParcela { get; set; }
        public decimal ValorParcela { get; set; }
        [JsonIgnore]
        public CartaoCreditoCompra CartaoCreditoCompra { get; set; }
    }
}