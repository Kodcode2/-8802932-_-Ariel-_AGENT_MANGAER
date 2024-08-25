using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BE_AgentGuard.Migrations
{
    /// <inheritdoc />
    public partial class aAa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "x",
                table: "Agent",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "y",
                table: "Agent",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "x",
                table: "Agent");

            migrationBuilder.DropColumn(
                name: "y",
                table: "Agent");
        }
    }
}
