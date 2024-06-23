using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Weindrachen.Migrations
{
    /// <inheritdoc />
    public partial class SeedingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "brands",
                columns: new[] { "brand_id", "origin_country", "brand_name" },
                values: new object[,]
                {
                    { 1, "Argentina", "Catena Zapata" },
                    { 2, "Brazil", "Miolo" },
                    { 3, "Chile", "Concha y Toro" },
                    { 4, "France", "Château Margaux" },
                    { 5, "Italy", "Antinori" }
                });

            migrationBuilder.InsertData(
                table: "grapes",
                columns: new[] { "grape_id", "grape_name" },
                values: new object[,]
                {
                    { 1, "Malbec" },
                    { 2, "Merlot" },
                    { 3, "Carmenère" },
                    { 4, "Petit Verdot" },
                    { 5, "Sangiovese" }
                });

            migrationBuilder.InsertData(
                table: "wines",
                columns: new[] { "wine_id", "alcoholic_level", "brand_id", "origin_country", "wine_name", "price", "predominant_flavour" },
                values: new object[,]
                {
                    { 1, 13.5f, 1, "Argentina", "Catena Zapata Malbec", 30.00m, "Cherry" },
                    { 2, 13.5f, 2, "Brazil", "Miolo Merlot", 20.00m, "Plum" },
                    { 3, 14f, 3, "Chile", "Concha y Toro Carmenère", 25.00m, "Blackberry" }
                });

            migrationBuilder.InsertData(
                table: "wines",
                columns: new[] { "wine_id", "alcoholic_level", "brand_id", "origin_country", "is_doc", "wine_name", "price", "predominant_flavour" },
                values: new object[,]
                {
                    { 4, 13f, 4, "France", (sbyte)1, "Château Margaux Petit Verdot", 100.00m, "Chocolate" },
                    { 5, 13.5f, 5, "Italy", (sbyte)1, "Antinori Sangiovese", 50.00m, "StrawBerry" }
                });

            migrationBuilder.InsertData(
                table: "grapes_wines",
                columns: new[] { "grape_id", "wine_id" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 4, 4 },
                    { 5, 5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "grapes_wines",
                keyColumns: new[] { "grape_id", "wine_id" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "grapes_wines",
                keyColumns: new[] { "grape_id", "wine_id" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "grapes_wines",
                keyColumns: new[] { "grape_id", "wine_id" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "grapes_wines",
                keyColumns: new[] { "grape_id", "wine_id" },
                keyValues: new object[] { 4, 4 });

            migrationBuilder.DeleteData(
                table: "grapes_wines",
                keyColumns: new[] { "grape_id", "wine_id" },
                keyValues: new object[] { 5, 5 });

            migrationBuilder.DeleteData(
                table: "grapes",
                keyColumn: "grape_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "grapes",
                keyColumn: "grape_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "grapes",
                keyColumn: "grape_id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "grapes",
                keyColumn: "grape_id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "grapes",
                keyColumn: "grape_id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "wines",
                keyColumn: "wine_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "wines",
                keyColumn: "wine_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "wines",
                keyColumn: "wine_id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "wines",
                keyColumn: "wine_id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "wines",
                keyColumn: "wine_id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "brands",
                keyColumn: "brand_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "brands",
                keyColumn: "brand_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "brands",
                keyColumn: "brand_id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "brands",
                keyColumn: "brand_id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "brands",
                keyColumn: "brand_id",
                keyValue: 5);
        }
    }
}
