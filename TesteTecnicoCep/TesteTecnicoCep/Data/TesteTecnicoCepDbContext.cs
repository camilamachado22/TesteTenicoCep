using Microsoft.EntityFrameworkCore;
using TesteTecnicoCep.Models;

namespace TesteTecnicoCep.Data
{
    public class TesteTecnicoCepDbContext : DbContext
    {
        public TesteTecnicoCepDbContext(DbContextOptions<TesteTecnicoCepDbContext> options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Contato> Contatos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>()
                .HasKey(c => c.id);

            modelBuilder.Entity<Endereco>()
                .HasKey(e => e.id_cliente);

            modelBuilder.Entity<Contato>()
                .HasKey(c => c.id_cliente);

            modelBuilder.Entity<Contato>()
                .HasOne<Cliente>()
                .WithMany()
                .HasForeignKey(c => c.id_cliente);

            modelBuilder.Entity<Endereco>()
                .HasOne<Cliente>()
                .WithOne()
                .HasForeignKey<Endereco>(e => e.id_cliente);
        }
    }
}
