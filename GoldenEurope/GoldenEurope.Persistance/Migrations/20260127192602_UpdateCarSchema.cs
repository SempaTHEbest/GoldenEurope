using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoldenEurope.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCarSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Condition",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Condition",
                table: "Cars");
        }
    }
}
