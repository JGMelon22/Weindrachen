﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Weindrachen.Infrastructure.Data;

#nullable disable

namespace Weindrachen.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240519002711_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Weindrachen.Models.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INT")
                        .HasColumnName("brand_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("brand_name");

                    b.Property<int>("OriginCountry")
                        .HasColumnType("INT")
                        .HasColumnName("origin_country");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .HasDatabaseName("idb_brands_id");

                    b.ToTable("brands", (string)null);
                });

            modelBuilder.Entity("Weindrachen.Models.Grape", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INT")
                        .HasColumnName("grape_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("grape_name");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .HasDatabaseName("idx_grapes_id");

                    b.ToTable("grapes", (string)null);
                });

            modelBuilder.Entity("Weindrachen.Models.GrapeWine", b =>
                {
                    b.Property<int>("GrapeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INT")
                        .HasColumnName("grape_id");

                    b.Property<int>("WineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INT")
                        .HasColumnName("wine_id");

                    b.HasKey("GrapeId", "WineId");

                    b.HasIndex("GrapeId")
                        .HasDatabaseName("idx_grapes_wines_grape_id");

                    b.HasIndex("WineId")
                        .HasDatabaseName("idx_grapes_wines_wine_id");

                    b.ToTable("grapes_wines", (string)null);
                });

            modelBuilder.Entity("Weindrachen.Models.Wine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INT")
                        .HasColumnName("wine_id");

                    b.Property<float>("AlcoholicLevel")
                        .HasColumnType("FLOAT")
                        .HasColumnName("alcoholic_level");

                    b.Property<int>("BrandId")
                        .HasColumnType("INT")
                        .HasColumnName("brand_id");

                    b.Property<sbyte>("IsDoc")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TINYINT")
                        .HasDefaultValue((sbyte)1)
                        .HasColumnName("is_doc");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("wine_name");

                    b.Property<int>("OriginCountry")
                        .HasColumnType("INT")
                        .HasColumnName("origin_country");

                    b.Property<int>("PredominantFlavour")
                        .HasColumnType("INT")
                        .HasColumnName("predominant_flavour");

                    b.Property<decimal>("Price")
                        .HasPrecision(7, 2)
                        .HasColumnType("DECIMAL")
                        .HasColumnName("price");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("Id")
                        .HasDatabaseName("idx_wine_id");

                    b.ToTable("wines", (string)null);
                });

            modelBuilder.Entity("Weindrachen.Models.GrapeWine", b =>
                {
                    b.HasOne("Weindrachen.Models.Grape", "Grape")
                        .WithMany("GrapeWines")
                        .HasForeignKey("GrapeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Weindrachen.Models.Wine", "Wine")
                        .WithMany("GrapeWines")
                        .HasForeignKey("WineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Grape");

                    b.Navigation("Wine");
                });

            modelBuilder.Entity("Weindrachen.Models.Wine", b =>
                {
                    b.HasOne("Weindrachen.Models.Brand", "Brand")
                        .WithMany("Wines")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");
                });

            modelBuilder.Entity("Weindrachen.Models.Brand", b =>
                {
                    b.Navigation("Wines");
                });

            modelBuilder.Entity("Weindrachen.Models.Grape", b =>
                {
                    b.Navigation("GrapeWines");
                });

            modelBuilder.Entity("Weindrachen.Models.Wine", b =>
                {
                    b.Navigation("GrapeWines");
                });
#pragma warning restore 612, 618
        }
    }
}
