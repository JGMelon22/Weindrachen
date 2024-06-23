using Weindrachen.Models;
using Weindrachen.Models.Enums;

namespace Weindrachen.Infrastructure.Configuration.Seeding;

public static class InitialSeeding
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        # region [Brands]

        var catenaZapata = new Brand { Id = 1, Name = "Catena Zapata", Country = Country.Argentina };
        var miolo = new Brand { Id = 2, Name = "Miolo", Country = Country.Brazil };
        var conchaYToro = new Brand { Id = 3, Name = "Concha y Toro", Country = Country.Chile };
        var chateauMargaux = new Brand { Id = 4, Name = "Château Margaux", Country = Country.France };
        var antinori = new Brand { Id = 5, Name = "Antinori", Country = Country.Italy };

        # endregion

        # region [Grapes]

        var malbec = new Grape { Id = 1, Name = "Malbec" };
        var merlot = new Grape { Id = 2, Name = "Merlot" };
        var carmenere = new Grape { Id = 3, Name = "Carmenère" };
        var petitVerdot = new Grape { Id = 4, Name = "Petit Verdot" };
        var sangiovese = new Grape { Id = 5, Name = "Sangiovese" };

        # endregion

        # region [Wines]

        var catenaZapataMalbec = new Wine
        {
            Id = 1,
            Name = "Catena Zapata Malbec",
            Price = 30.00M,
            IsDoc = false,
            AlcoholicLevel = 13.5F,
            Country = Country.Argentina,
            BrandId = catenaZapata.Id,
            Taste = Taste.Cherry
        };

        var mioloMerlot = new Wine
        {
            Id = 2,
            Name = "Miolo Merlot",
            Price = 20.00M,
            IsDoc = false,
            AlcoholicLevel = 13.5F,
            Country = Country.Brazil,
            BrandId = miolo.Id,
            Taste = Taste.Plum
        };

        var conchaYToroCarmenere = new Wine
        {
            Id = 3,
            Name = "Concha y Toro Carmenère",
            Price = 25.00M,
            IsDoc = false,
            AlcoholicLevel = 14.0F,
            Country = Country.Chile,
            BrandId = conchaYToro.Id,
            Taste = Taste.Blackberry
        };

        var chateauMargauxPetitVerdot = new Wine
        {
            Id = 4,
            Name = "Château Margaux Petit Verdot",
            Price = 100.00M,
            IsDoc = true,
            AlcoholicLevel = 13.0F,
            Country = Country.France,
            BrandId = chateauMargaux.Id,
            Taste = Taste.Chocolate
        };

        var antinoriSangiovese = new Wine
        {
            Id = 5,
            Name = "Antinori Sangiovese",
            Price = 50.00M,
            IsDoc = true,
            AlcoholicLevel = 13.5F,
            Country = Country.Italy,
            BrandId = antinori.Id,
            Taste = Taste.StrawBerry
        };

        # endregion

        # region [GrapesWines]

        var malbecCatenaZapataMalbec = new GrapeWine
        {
            GrapeId = malbec.Id,
            WineId = catenaZapataMalbec.Id
        };

        var merlotMioloMerlot = new GrapeWine
        {
            GrapeId = merlot.Id,
            WineId = mioloMerlot.Id
        };

        var carmenereConchaYToroCarmenere = new GrapeWine
        {
            GrapeId = carmenere.Id,
            WineId = conchaYToroCarmenere.Id
        };

        var petitVerdotChateauMargauxPetitVerdot = new GrapeWine
        {
            GrapeId = petitVerdot.Id,
            WineId = chateauMargauxPetitVerdot.Id
        };

        var sangioveseAntinoriSangiovese = new GrapeWine
        {
            GrapeId = sangiovese.Id,
            WineId = antinoriSangiovese.Id
        };

        # endregion

        # region [Entity Has Data]

        modelBuilder.Entity<Brand>()
            .HasData(catenaZapata, miolo, conchaYToro, chateauMargaux, antinori);
        modelBuilder.Entity<Grape>()
            .HasData(malbec, merlot, carmenere, petitVerdot, sangiovese);
        modelBuilder.Entity<Wine>()
            .HasData(catenaZapataMalbec, mioloMerlot, conchaYToroCarmenere, chateauMargauxPetitVerdot,
                antinoriSangiovese);
        modelBuilder.Entity<GrapeWine>()
            .HasData(malbecCatenaZapataMalbec, merlotMioloMerlot, carmenereConchaYToroCarmenere,
                petitVerdotChateauMargauxPetitVerdot, sangioveseAntinoriSangiovese);

        # endregion
    }
}