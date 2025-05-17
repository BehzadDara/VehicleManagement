using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleManagement.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Vehicle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "v");

            migrationBuilder.CreateSequence(
                name: "CarSequence",
                schema: "v");

            migrationBuilder.CreateSequence(
                name: "MotorcycleSequence",
                schema: "v");

            migrationBuilder.CreateTable(
                name: "Cars",
                schema: "v",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [v].[CarSequence]"),
                    TrackingCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Gearbox = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Motorcycles",
                schema: "v",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [v].[MotorcycleSequence]"),
                    TrackingCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Fuel = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motorcycles", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars",
                schema: "v");

            migrationBuilder.DropTable(
                name: "Motorcycles",
                schema: "v");

            migrationBuilder.DropSequence(
                name: "CarSequence",
                schema: "v");

            migrationBuilder.DropSequence(
                name: "MotorcycleSequence",
                schema: "v");
        }
    }
}
