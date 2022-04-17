using Microsoft.EntityFrameworkCore.Migrations;

namespace WheatherForecast.Migrations
{
    public partial class AddHashedPassword : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HashedPassword",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HashedPassword",
                table: "Users");
        }
    }
}
