using Weindrachen.Models;

namespace Weindrachen.Infrastructure.Configuration;

public class BrandConfiguration : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.ToTable("brands");

        builder.HasKey(b => b.Id);

        builder.HasIndex(b => b.Id)
            .HasDatabaseName("idb_brands_id");

        builder.Property(b => b.Id)
            .HasColumnType("INT")
            .HasColumnName("brand_id")
            .ValueGeneratedOnAdd();

        builder.Property(b => b.Name)
            .HasColumnType("VARCHAR")
            .HasColumnName("brand_name")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(b => b.OriginCountry)
            .HasColumnType("INT")
            .HasColumnName("origin_country")
            .IsRequired();
    }
}