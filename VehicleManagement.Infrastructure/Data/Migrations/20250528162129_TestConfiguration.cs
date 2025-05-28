using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleManagement.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class TestConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BackOfficeUserPermissions_BackOfficeUserRoles_BackOfficeUserRoleId",
                schema: "v",
                table: "BackOfficeUserPermissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BackOfficeUserPermissions",
                schema: "v",
                table: "BackOfficeUserPermissions");

            migrationBuilder.RenameTable(
                name: "BackOfficeUserPermissions",
                schema: "v",
                newName: "BackOfficeUserPermission",
                newSchema: "v");

            migrationBuilder.RenameIndex(
                name: "IX_BackOfficeUserPermissions_BackOfficeUserRoleId",
                schema: "v",
                table: "BackOfficeUserPermission",
                newName: "IX_BackOfficeUserPermission_BackOfficeUserRoleId");

            migrationBuilder.AlterColumn<int>(
                name: "BackOfficeUserRoleId",
                schema: "v",
                table: "BackOfficeUserPermission",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BackOfficeUserPermission",
                schema: "v",
                table: "BackOfficeUserPermission",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BackOfficeUserPermission_BackOfficeUserRoles_BackOfficeUserRoleId",
                schema: "v",
                table: "BackOfficeUserPermission",
                column: "BackOfficeUserRoleId",
                principalSchema: "v",
                principalTable: "BackOfficeUserRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BackOfficeUserPermission_BackOfficeUserRoles_BackOfficeUserRoleId",
                schema: "v",
                table: "BackOfficeUserPermission");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BackOfficeUserPermission",
                schema: "v",
                table: "BackOfficeUserPermission");

            migrationBuilder.RenameTable(
                name: "BackOfficeUserPermission",
                schema: "v",
                newName: "BackOfficeUserPermissions",
                newSchema: "v");

            migrationBuilder.RenameIndex(
                name: "IX_BackOfficeUserPermission_BackOfficeUserRoleId",
                schema: "v",
                table: "BackOfficeUserPermissions",
                newName: "IX_BackOfficeUserPermissions_BackOfficeUserRoleId");

            migrationBuilder.AlterColumn<int>(
                name: "BackOfficeUserRoleId",
                schema: "v",
                table: "BackOfficeUserPermissions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BackOfficeUserPermissions",
                schema: "v",
                table: "BackOfficeUserPermissions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BackOfficeUserPermissions_BackOfficeUserRoles_BackOfficeUserRoleId",
                schema: "v",
                table: "BackOfficeUserPermissions",
                column: "BackOfficeUserRoleId",
                principalSchema: "v",
                principalTable: "BackOfficeUserRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
