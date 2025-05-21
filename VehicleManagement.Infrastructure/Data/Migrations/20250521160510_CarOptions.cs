using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleManagement.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class CarOptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarOptions",
                schema: "v",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CarId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarOptions_Cars_CarId",
                        column: x => x.CarId,
                        principalSchema: "v",
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarOptions_CarId",
                schema: "v",
                table: "CarOptions",
                column: "CarId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarOptions",
                schema: "v");
        }
    }
}
