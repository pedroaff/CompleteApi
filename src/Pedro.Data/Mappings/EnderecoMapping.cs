using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pedro.Business.Models;

namespace Pedro.Data.Mappings;

public class EnderecoMapping : IEntityTypeConfiguration<Endereco>
{

    public void Configure(EntityTypeBuilder<Endereco> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Logradouro)
            .IsRequired()
            .HasColumnType("varchar(200)");

        builder.Property(e => e.Numero)
            .IsRequired()
            .HasColumnType("varchar(14)");

        builder.Property(e => e.Cep)
            .IsRequired()
            .HasColumnType("varchar(14)");

        builder.Property(e => e.Complemento)
           .IsRequired()
           .HasColumnType("varchar(200)");

        builder.Property(e => e.Bairro)
          .IsRequired()
          .HasColumnType("varchar(20)");

        builder.Property(e => e.Cidade)
          .IsRequired()
          .HasColumnType("varchar(20)");

        builder.Property(e => e.Estado)
          .IsRequired()
          .HasColumnType("varchar(20)");

        builder.ToTable("Endrecos");
    }
}