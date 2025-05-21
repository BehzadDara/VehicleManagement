using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleManagement.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class CarTags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarTags",
                schema: "v",
                columns: table => new
                {
                    Title = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    CarId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarTags", x => new { x.CarId, x.Title, x.Priority });
                    table.ForeignKey(
                        name: "FK_CarTags_Cars_CarId",
                        column: x => x.CarId,
                        principalSchema: "v",
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarTags",
                schema: "v");
        }
    }
}
