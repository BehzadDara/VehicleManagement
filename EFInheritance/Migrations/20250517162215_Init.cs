using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFInheritance.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "Cat1Sequence");

            migrationBuilder.CreateSequence(
                name: "Dog1Sequence");

            migrationBuilder.CreateTable(
                name: "Animal2",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Lives = table.Column<int>(type: "int", nullable: true),
                    Breed = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animal2", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Animal3",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animal3", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cat1",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [Cat1Sequence]"),
                    Lives = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cat1", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dog1",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [Dog1Sequence]"),
                    Breed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dog1", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cat3",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Lives = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cat3", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cat3_Animal3_Id",
                        column: x => x.Id,
                        principalTable: "Animal3",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dog3",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Breed = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dog3", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dog3_Animal3_Id",
                        column: x => x.Id,
                        principalTable: "Animal3",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Animal2");

            migrationBuilder.DropTable(
                name: "Cat1");

            migrationBuilder.DropTable(
                name: "Cat3");

            migrationBuilder.DropTable(
                name: "Dog1");

            migrationBuilder.DropTable(
                name: "Dog3");

            migrationBuilder.DropTable(
                name: "Animal3");

            migrationBuilder.DropSequence(
                name: "Cat1Sequence");

            migrationBuilder.DropSequence(
                name: "Dog1Sequence");
        }
    }
}
