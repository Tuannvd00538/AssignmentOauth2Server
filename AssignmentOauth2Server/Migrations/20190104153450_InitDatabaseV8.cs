using Microsoft.EntityFrameworkCore.Migrations;

namespace AssignmentOauth2Server.Migrations
{
    public partial class InitDatabaseV8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountInfomationId",
                table: "Role",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Class",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Role_AccountInfomationId",
                table: "Role",
                column: "AccountInfomationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Role_AccountInfomation_AccountInfomationId",
                table: "Role",
                column: "AccountInfomationId",
                principalTable: "AccountInfomation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Role_AccountInfomation_AccountInfomationId",
                table: "Role");

            migrationBuilder.DropIndex(
                name: "IX_Role_AccountInfomationId",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "AccountInfomationId",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Class");
        }
    }
}
