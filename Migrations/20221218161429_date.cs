using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication_DRUGSTORE.Migrations
{
    public partial class date : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PublishingDate",
                table: "Product",
                newName: "ReleaseDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReleaseDate",
                table: "Product",
                newName: "PublishingDate");
        }
    }
}
