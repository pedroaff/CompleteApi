using Microsoft.EntityFrameworkCore;
using Pedro.Business.Models;

namespace Pedro.Data.Context;

public class MeuDbContext : DbContext
{
    public MeuDbContext(DbContextOptions options) : base(options)
    {
        
    }

    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Endereco> Enderecos { get; set; }
    public DbSet<Fornecedor> Fornecedores { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // registra todos os mappings de uma vez só
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MeuDbContext).Assembly);

        // desatibila o delete onCascade
        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
        }

        base.OnModelCreating(modelBuilder);
    }
}
