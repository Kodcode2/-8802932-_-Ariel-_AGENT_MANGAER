using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BE_AgentGuard.Migrations
{
    /// <inheritdoc />
    public partial class a : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "is_alive",
                table: "Target",
                newName: "is_active");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Agent",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "nickname",
                table: "Agent",
                newName: "name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "is_active",
                table: "Target",
                newName: "is_alive");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Agent",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Agent",
                newName: "nickname");
        }
    }
}
