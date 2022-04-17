using Microsoft.EntityFrameworkCore.Migrations;

namespace WheatherForecast.Migrations
{
    public partial class RemoveHashedFromPassword : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HashedPassword",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "HashedPassword",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
