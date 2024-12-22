using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JustOutsource.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class hjhj : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdditionalFile",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdditionalFile",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Jobs");
        }
    }
}
