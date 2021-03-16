using Financas.Domain;
using Microsoft.EntityFrameworkCore;

namespace Financas.Application.Persistence
{
    public class FinancasContext : DbContext
    {
        public virtual DbSet<CartaoCredito> CartaoCredito { get; set; }

        public virtual DbSet<CustoFixo> CustoFixo { get; set; }

        public virtual DbSet<CustoDiverso> CustoDiverso { get; set; }

        public FinancasContext(DbContextOptions<FinancasContext> options) : base(options) { }
    }
}
