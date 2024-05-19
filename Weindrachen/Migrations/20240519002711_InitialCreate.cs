using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Weindrachen.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "brands",
                columns: table => new
                {
                    brand_id = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    brand_name = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    origin_country = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_brands", x => x.brand_id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "grapes",
                columns: table => new
                {
                    grape_id = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    grape_name = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_grapes", x => x.grape_id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "wines",
                columns: table => new
                {
                    wine_id = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    wine_name = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    price = table.Column<decimal>(type: "DECIMAL(7,2)", precision: 7, scale: 2, nullable: false),
                    is_doc = table.Column<sbyte>(type: "TINYINT", nullable: false, defaultValue: (sbyte)1),
                    alcoholic_level = table.Column<float>(type: "FLOAT", nullable: false),
                    origin_country = table.Column<int>(type: "INT", nullable: false),
                    brand_id = table.Column<int>(type: "INT", nullable: false),
                    predominant_flavour = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wines", x => x.wine_id);
                    table.ForeignKey(
                        name: "FK_wines_brands_brand_id",
                        column: x => x.brand_id,
                        principalTable: "brands",
                        principalColumn: "brand_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "grapes_wines",
                columns: table => new
                {
                    grape_id = table.Column<int>(type: "INT", nullable: false),
                    wine_id = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_grapes_wines", x => new { x.grape_id, x.wine_id });
                    table.ForeignKey(
                        name: "FK_grapes_wines_grapes_grape_id",
                        column: x => x.grape_id,
                        principalTable: "grapes",
                        principalColumn: "grape_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_grapes_wines_wines_wine_id",
                        column: x => x.wine_id,
                        principalTable: "wines",
                        principalColumn: "wine_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "idb_brands_id",
                table: "brands",
                column: "brand_id");

            migrationBuilder.CreateIndex(
                name: "idx_grapes_id",
                table: "grapes",
                column: "grape_id");

            migrationBuilder.CreateIndex(
                name: "idx_grapes_wines_grape_id",
                table: "grapes_wines",
                column: "grape_id");

            migrationBuilder.CreateIndex(
                name: "idx_grapes_wines_wine_id",
                table: "grapes_wines",
                column: "wine_id");

            migrationBuilder.CreateIndex(
                name: "idx_wine_id",
                table: "wines",
                column: "wine_id");

            migrationBuilder.CreateIndex(
                name: "IX_wines_brand_id",
                table: "wines",
                column: "brand_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "grapes_wines");

            migrationBuilder.DropTable(
                name: "grapes");

            migrationBuilder.DropTable(
                name: "wines");

            migrationBuilder.DropTable(
                name: "brands");
        }
    }
}
