using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModsenTask.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Migration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BorrowedAt",
                table: "UserBooks",
                newName: "TakenAt");

            migrationBuilder.AddColumn<bool>(
                name: "IsTaken",
                table: "Books",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTaken",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "TakenAt",
                table: "UserBooks",
                newName: "BorrowedAt");
        }
    }
}
