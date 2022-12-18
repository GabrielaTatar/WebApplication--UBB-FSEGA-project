using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication_DRUGSTORE.Migrations
{
    public partial class Review : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Review",
                table: "Product");

            migrationBuilder.AddColumn<int>(
                name: "ReviewID",
                table: "Product",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Stars = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_ReviewID",
                table: "Product",
                column: "ReviewID");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Review_ReviewID",
                table: "Product",
                column: "ReviewID",
                principalTable: "Review",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Review_ReviewID",
                table: "Product");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Product_ReviewID",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ReviewID",
                table: "Product");

            migrationBuilder.AddColumn<string>(
                name: "Review",
                table: "Product",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
