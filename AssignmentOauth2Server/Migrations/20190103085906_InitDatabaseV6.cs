using Microsoft.EntityFrameworkCore.Migrations;

namespace AssignmentOauth2Server.Migrations
{
    public partial class InitDatabaseV6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountInfomation_Account_OwnerId",
                table: "AccountInfomation");

            migrationBuilder.DropIndex(
                name: "IX_AccountInfomation_OwnerId",
                table: "AccountInfomation");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AccountInfomation_OwnerId",
                table: "AccountInfomation",
                column: "OwnerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountInfomation_Account_OwnerId",
                table: "AccountInfomation",
                column: "OwnerId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
