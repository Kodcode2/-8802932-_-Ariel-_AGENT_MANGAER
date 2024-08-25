using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BE_AgentGuard.Migrations
{
    /// <inheritdoc />
    public partial class aAaaa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "Agent",
                newName: "nickname");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "nickname",
                table: "Agent",
                newName: "name");
        }
    }
}
