using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkshopImproved.Migrations
{
    /// <inheritdoc />
    public partial class UploadFiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DownloadUrl",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "FrontPage",
                table: "Book");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DownloadUrl",
                table: "Book",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FrontPage",
                table: "Book",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
