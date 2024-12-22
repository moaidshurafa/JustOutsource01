using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JustOutsource.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addCvToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CV",
                table: "Freelancers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CV",
                table: "Freelancers");
        }
    }
}
