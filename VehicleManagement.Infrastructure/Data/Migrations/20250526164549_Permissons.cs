using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleManagement.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Permissons : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Motorcycles_TrackingCode",
                schema: "v",
                table: "Motorcycles");

            migrationBuilder.DropIndex(
                name: "IX_Cars_TrackingCode",
                schema: "v",
                table: "Cars");

            migrationBuilder.CreateTable(
                name: "BackOfficeUserRoles",
                schema: "v",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    BackOfficeUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BackOfficeUserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BackOfficeUserRoles_BackOfficeUsers_BackOfficeUserId",
                        column: x => x.BackOfficeUserId,
                        principalSchema: "bo",
                        principalTable: "BackOfficeUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BackOfficeUserPermissions",
                schema: "v",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    BackOfficeUserRoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BackOfficeUserPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BackOfficeUserPermissions_BackOfficeUserRoles_BackOfficeUserRoleId",
                        column: x => x.BackOfficeUserRoleId,
                        principalSchema: "v",
                        principalTable: "BackOfficeUserRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BackOfficeUserPermissions_BackOfficeUserRoleId",
                schema: "v",
                table: "BackOfficeUserPermissions",
                column: "BackOfficeUserRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_BackOfficeUserRoles_BackOfficeUserId",
                schema: "v",
                table: "BackOfficeUserRoles",
                column: "BackOfficeUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BackOfficeUserPermissions",
                schema: "v");

            migrationBuilder.DropTable(
                name: "BackOfficeUserRoles",
                schema: "v");

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
    }
}
