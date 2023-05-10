using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkshopImproved.Migrations
{
    /// <inheritdoc />
    public partial class CelosenUpload : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DownloadUrl",
                table: "Book",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DownloadUrl",
                table: "Book");
        }
    }
}
