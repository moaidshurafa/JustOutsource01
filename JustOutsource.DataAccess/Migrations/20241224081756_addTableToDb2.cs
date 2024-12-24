using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JustOutsource.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addTableToDb2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Jobs_JobId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_JobId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "RecipientId",
                table: "Notifications");

            migrationBuilder.RenameColumn(
                name: "SenderId",
                table: "Notifications",
                newName: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Notifications",
                newName: "SenderId");

            migrationBuilder.AddColumn<int>(
                name: "JobId",
                table: "Notifications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecipientId",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_JobId",
                table: "Notifications",
                column: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Jobs_JobId",
                table: "Notifications",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id");
        }
    }
}
