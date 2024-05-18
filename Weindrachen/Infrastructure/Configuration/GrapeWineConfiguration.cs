using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Weindrachen.Models;

namespace Weindrachen.Infrastructure.Configuration;

public class GrapeWineConfiguration : IEntityTypeConfiguration<GrapeWine>
{
    public void Configure(EntityTypeBuilder<GrapeWine> builder)
    {
        builder.ToTable("grapes_wines");

        builder.HasKey(gw => new
        {
            gw.GrapeId,
            gw.WineId
        });

        builder.HasIndex(gw => gw.GrapeId)
            .HasDatabaseName("idx_grapes_wines_grape_id");

        builder.HasIndex(gw => gw.WineId)
            .HasDatabaseName("idx_grapes_wines_wine_id");

        builder.HasOne(gw => gw.Grape)
            .WithMany(g => g.GrapeWines)
            .HasForeignKey(gw => gw.GrapeId);

        builder.HasOne(gw => gw.Wine)
            .WithMany(w => w.GrapeWines)
            .HasForeignKey(gw => gw.WineId);

        builder.Property(gw => gw.GrapeId)
            .HasColumnType("INT")
            .HasColumnName("grape_id")
            .ValueGeneratedOnAdd();

        builder.Property(gw => gw.WineId)
            .HasColumnType("INT")
            .HasColumnName("wine_id")
            .ValueGeneratedOnAdd();
    }
}