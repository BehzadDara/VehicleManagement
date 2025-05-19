using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleManagement.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class TrackingCodeIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Motorcycles_TrackingCode",
                schema: "v",
                table: "Motorcycles",
                column: "TrackingCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cars_TrackingCode",
                schema: "v",
                table: "Cars",
                column: "TrackingCode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Motorcycles_TrackingCode",
                schema: "v",
                table: "Motorcycles");

            migrationBuilder.DropIndex(
                name: "IX_Cars_TrackingCode",
                schema: "v",
                table: "Cars");
        }
    }
}
