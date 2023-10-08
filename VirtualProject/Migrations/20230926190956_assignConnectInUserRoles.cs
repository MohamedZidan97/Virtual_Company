using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirtualProject.Migrations
{
    /// <inheritdoc />
    public partial class assignConnectInUserRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [rllfrr].[UserRoles] (UserId,RoleId) SELECT '2c4ed060-3dca-493b-8341-f538a5621b77', Id FROM [rllfrr].[Roles]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [rllfrr].[UserRoles] WHERE UserId='2c4ed060-3dca-493b-8341-f538a5621b77'");
        }
    }
}
