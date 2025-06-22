using Microsoft.EntityFrameworkCore;
using TesteTecnicoCep.Models;

namespace TesteTecnicoCep.Data
{
    public class TesteTecnicoCepDbContext : DbContext
    {
        public TesteTecnicoCepDbContext(DbContextOptions<TesteTecnicoCepDbContext> options) : base(options)
        {
        }

        public DbSet<Cliente> cliente { get; set; }
        public DbSet<Endereco> endereco { get; set; }
        public DbSet<Contato> contato { get; set; }

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

            modelBuilder.Entity<Cliente>()
                .HasOne(c => c.Endereco)
                .WithOne()
                .HasForeignKey<Endereco>(e => e.id_cliente);

            // Configuração do relacionamento Cliente -> Contatos (1:N)
            modelBuilder.Entity<Cliente>()
                .HasMany(c => c.Contato)
                .WithOne()
                .HasForeignKey(ct => ct.id_cliente);
        }
    }
}
