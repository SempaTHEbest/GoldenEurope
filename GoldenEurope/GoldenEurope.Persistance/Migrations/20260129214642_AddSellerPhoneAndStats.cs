using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoldenEurope.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddSellerPhoneAndStats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PhoneViewCount",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SellerPhone",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneViewCount",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "SellerPhone",
                table: "Cars");
        }
    }
}
