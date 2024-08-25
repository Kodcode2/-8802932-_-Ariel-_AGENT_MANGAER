using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BE_AgentGuard.Migrations
{
    /// <inheritdoc />
    public partial class dsd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "photo_url",
                table: "Target",
                newName: "photoUrl");

            migrationBuilder.RenameColumn(
                name: "photo_url",
                table: "Agent",
                newName: "photoUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "photoUrl",
                table: "Target",
                newName: "photo_url");

            migrationBuilder.RenameColumn(
                name: "photoUrl",
                table: "Agent",
                newName: "photo_url");
        }
    }
}
