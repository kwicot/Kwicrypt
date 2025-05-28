using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kwicrypt.Module.LoginSecrets.Migrations
{
    /// <inheritdoc />
    public partial class AddedTotpSecret : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TwoFactorSecret",
                table: "List",
                newName: "TotpSecret");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotpSecret",
                table: "List",
                newName: "TwoFactorSecret");
        }
    }
}
