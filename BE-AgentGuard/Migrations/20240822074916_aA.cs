using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BE_AgentGuard.Migrations
{
    /// <inheritdoc />
    public partial class aA : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "x",
                table: "Target");

            migrationBuilder.DropColumn(
                name: "y",
                table: "Target");

            migrationBuilder.DropColumn(
                name: "x",
                table: "Agent");

            migrationBuilder.DropColumn(
                name: "y",
                table: "Agent");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "x",
                table: "Target",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "y",
                table: "Target",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
    }
}
