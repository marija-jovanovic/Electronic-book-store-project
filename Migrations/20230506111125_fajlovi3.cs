using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkshopImproved.Migrations
{
    /// <inheritdoc />
    public partial class fajlovi3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FrontPage",
                table: "Book",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FrontPage",
                table: "Book");
        }
    }
}
