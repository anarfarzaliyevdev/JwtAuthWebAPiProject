using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JwtAuthWebAPiProject.Migrations
{
    public partial class addRefreshTokenExpireDateToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RefreshTokenExpiryTime",
                table: "Users",
                newName: "RefreshTokenExpireDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RefreshTokenExpireDate",
                table: "Users",
                newName: "RefreshTokenExpiryTime");
        }
    }
}
