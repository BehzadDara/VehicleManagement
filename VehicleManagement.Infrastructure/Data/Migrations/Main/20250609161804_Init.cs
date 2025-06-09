using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleManagement.Infrastructure.Data.Migrations.Main
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "v");

            migrationBuilder.EnsureSchema(
                name: "bo");

            migrationBuilder.CreateSequence(
                name: "CarSequence",
                schema: "v");

            migrationBuilder.CreateSequence(
                name: "MotorcycleSequence",
                schema: "v");

            migrationBuilder.CreateTable(
                name: "BackOfficeUsers",
                schema: "bo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    LastLoginAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BackOfficeUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                schema: "v",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [v].[CarSequence]"),
                    TrackingCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Gearbox = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
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
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motorcycles", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "BackOfficeUserPermission",
                schema: "v",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    BackOfficeUserRoleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BackOfficeUserPermission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BackOfficeUserPermission_BackOfficeUserRoles_BackOfficeUserRoleId",
                        column: x => x.BackOfficeUserRoleId,
                        principalSchema: "v",
                        principalTable: "BackOfficeUserRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "bo",
                table: "BackOfficeUsers",
                columns: new[] { "Id", "LastLoginAt", "Password", "Username" },
                values: new object[] { 1, null, "123", "Behzad" });

            migrationBuilder.CreateIndex(
                name: "IX_BackOfficeUserPermission_BackOfficeUserRoleId",
                schema: "v",
                table: "BackOfficeUserPermission",
                column: "BackOfficeUserRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_BackOfficeUserRoles_BackOfficeUserId",
                schema: "v",
                table: "BackOfficeUserRoles",
                column: "BackOfficeUserId");

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
                name: "BackOfficeUserPermission",
                schema: "v");

            migrationBuilder.DropTable(
                name: "CarOptions",
                schema: "v");

            migrationBuilder.DropTable(
                name: "CarTags",
                schema: "v");

            migrationBuilder.DropTable(
                name: "Motorcycles",
                schema: "v");

            migrationBuilder.DropTable(
                name: "BackOfficeUserRoles",
                schema: "v");

            migrationBuilder.DropTable(
                name: "Cars",
                schema: "v");

            migrationBuilder.DropTable(
                name: "BackOfficeUsers",
                schema: "bo");

            migrationBuilder.DropSequence(
                name: "CarSequence",
                schema: "v");

            migrationBuilder.DropSequence(
                name: "MotorcycleSequence",
                schema: "v");
        }
    }
}
