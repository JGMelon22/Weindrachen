using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Weindrachen.Models;

namespace Weindrachen.Infrastructure.Configuration;

public class GrapeConfiguration : IEntityTypeConfiguration<Grape>
{
    public void Configure(EntityTypeBuilder<Grape> builder)
    {
        builder.ToTable("grapes");

        builder.HasKey(g => g.Id);

        builder.HasIndex(g => g.Id)
            .HasDatabaseName("idx_grapes_id");

        builder.Property(g => g.Id)
            .HasColumnType("INT")
            .HasColumnName("grape_id")
            .ValueGeneratedOnAdd();

        builder.Property(g => g.Name)
            .HasColumnType("VARCHAR")
            .HasColumnName("grape_name")
            .HasMaxLength(100)
            .IsRequired();
    }
}