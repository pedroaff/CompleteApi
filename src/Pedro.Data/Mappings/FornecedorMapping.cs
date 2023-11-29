﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pedro.Business.Models;

namespace Pedro.Data.Mappings;

public class FornecedorMapping : IEntityTypeConfiguration<Fornecedor>
{

    public void Configure(EntityTypeBuilder<Fornecedor> builder)
    {
        builder.HasKey(f => f.Id);

        builder.Property(f => f.Nome)
            .IsRequired()
            .HasColumnType("varchar(100)");

        builder.Property(f => f.Documento)
            .IsRequired()
            .HasColumnType("varchar(14)");

        builder.HasOne(f => f.Endereco)
            .WithOne(e => e.Fornecedor);

        builder.HasMany(f => f.Produtos)
            .WithOne(p => p.Fornecedor)
            .HasForeignKey(p => p.FornecedorId);

        builder.ToTable("Fornecedores");
    }
}
