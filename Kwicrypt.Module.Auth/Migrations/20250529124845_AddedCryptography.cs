using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kwicrypt.Module.Auth.Migrations
{
    /// <inheritdoc />
    public partial class AddedCryptography : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Mail",
                table: "List",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "MailVerified",
                table: "List",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PublicRsaKey",
                table: "List",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mail",
                table: "List");

            migrationBuilder.DropColumn(
                name: "MailVerified",
                table: "List");

            migrationBuilder.DropColumn(
                name: "PublicRsaKey",
                table: "List");
        }
    }
}
