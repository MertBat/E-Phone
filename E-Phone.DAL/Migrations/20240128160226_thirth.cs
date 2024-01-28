using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Phone.DAL.Migrations
{
    public partial class thirth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Models",
                newName: "ModelName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Brands",
                newName: "BrandName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModelName",
                table: "Models",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "BrandName",
                table: "Brands",
                newName: "Name");
        }
    }
}
