using Weindrachen.Models;

namespace Weindrachen.Infrastructure.Configuration;

public class WineConfiguration : IEntityTypeConfiguration<Wine>
{
    public void Configure(EntityTypeBuilder<Wine> builder)
    {
        builder.ToTable("wines");

        builder.HasKey(w => w.Id);

        builder.HasIndex(w => w.Id)
            .HasDatabaseName("idx_wine_id");

        builder.Property(w => w.Id)
            .HasColumnType("INT")
            .HasColumnName("wine_id")
            .ValueGeneratedOnAdd();

        builder.Property(w => w.Name)
            .HasColumnType("VARCHAR")
            .HasColumnName("wine_name")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(w => w.Price)
            .HasColumnType("DECIMAL")
            .HasColumnName("price")
            .HasPrecision(7, 2)
            .IsRequired();

        builder.Property(w => w.IsDoc)
            .HasColumnType("TINYINT")
            .HasColumnName("is_doc")
            .HasDefaultValue(1);

        builder.Property(w => w.AlcoholicLevel)
            .HasColumnType("FLOAT")
            .HasColumnName("alcoholic_level")
            .IsRequired();

        builder.Property(w => w.OriginCountry)
            .HasColumnType("VARCHAR")
            .HasColumnName("origin_country")
            .HasMaxLength(11)
            .IsRequired();

        builder.Property(w => w.BrandId)
            .HasColumnType("INT")
            .HasColumnName("brand_id")
            .IsRequired();

        builder.Property(w => w.Taste)
            .HasColumnType("VARCHAR")
            .HasColumnName("predominant_flavour")
            .HasMaxLength(12)
            .IsRequired();

        builder.HasOne(w => w.Brand)
            .WithMany(b => b.Wines)
            .HasForeignKey(w => w.BrandId);
    }
}