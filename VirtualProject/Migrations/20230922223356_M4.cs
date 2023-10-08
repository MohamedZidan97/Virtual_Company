using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirtualProject.Migrations
{
    /// <inheritdoc />
    public partial class M4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfilePhoroUrl",
                schema: "rllfrr",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePhoroUrl",
                schema: "rllfrr",
                table: "Users");
        }
    }
}
