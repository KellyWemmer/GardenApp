using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GardenApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlantInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommonName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LatinName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LifeCycle = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlantStart",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlantInfoId = table.Column<int>(type: "int", nullable: false),
                    YearId = table.Column<int>(type: "int", nullable: false),
                    RecommendedIndoorStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualIndoorStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SeedlingEnvironment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GerminationRate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Issues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IssuesFixes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPreferredMethod = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantStart", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlantSuccess",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlantId = table.Column<int>(type: "int", nullable: false),
                    YearId = table.Column<int>(type: "int", nullable: false),
                    RecommendedTransplantDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualPlantingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecommendedUpperTemp = table.Column<int>(type: "int", nullable: false),
                    RecommendedLowerTemp = table.Column<int>(type: "int", nullable: false),
                    StartedFromSeedOutdoors = table.Column<bool>(type: "bit", nullable: false),
                    PestsEncountered = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HowToWinterize = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtherIssues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IssuesFixes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPreferredMethod = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantSuccess", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Year",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Year", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlantInfo");

            migrationBuilder.DropTable(
                name: "PlantStart");

            migrationBuilder.DropTable(
                name: "PlantSuccess");

            migrationBuilder.DropTable(
                name: "Year");
        }
    }
}
