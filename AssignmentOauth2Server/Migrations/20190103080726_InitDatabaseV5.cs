using Microsoft.EntityFrameworkCore.Migrations;

namespace AssignmentOauth2Server.Migrations
{
    public partial class InitDatabaseV5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "AccountInfomation",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "AccountInfomation");
        }
    }
}
