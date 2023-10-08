using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirtualProject.Migrations
{
    /// <inheritdoc />
    public partial class AdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [rllfrr].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [ProfilePhoroUrl]) VALUES (N'2c4ed060-3dca-493b-8341-f538a5621b77', N'mohamedzidan6846@gmail.com', N'MOHAMEDZIDAN6846@GMAIL.COM', N'mohamedzidan6846@gmail.com', N'MOHAMEDZIDAN6846@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEP0eopgcHKi4qtHPeu+CIPIHDCsGL/WsTnfBtzV3PAswKAAgytpI1j6SEXOLmrVHDQ==', N'WTPNUYMTQADQZCCYJX4WLUTDR57CUAHO', N'901ec3b4-657c-4b62-83e5-41ce88034c3c', NULL, 0, 0, NULL, 1, 0, N'Mohamed', N'Zidan', NULL)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [rllfrr].[Users] WHERE id='2c4ed060-3dca-493b-8341-f538a5621b77' ");
        }
    }
}
