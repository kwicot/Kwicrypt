using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kwicrypt.Module.Auth.Migrations
{
    /// <inheritdoc />
    public partial class AddedPhone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "List",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "PhoneVerified",
                table: "List",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Phone",
                table: "List");

            migrationBuilder.DropColumn(
                name: "PhoneVerified",
                table: "List");
        }
    }
}
