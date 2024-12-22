using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JustOutsource.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addDateOfBirthAndYearsOfExperenceToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Freelancers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "YearsOfExperience",
                table: "Freelancers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Freelancers");

            migrationBuilder.DropColumn(
                name: "YearsOfExperience",
                table: "Freelancers");
        }
    }
}
