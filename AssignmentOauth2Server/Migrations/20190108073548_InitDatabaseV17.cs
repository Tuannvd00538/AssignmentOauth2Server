using Microsoft.EntityFrameworkCore.Migrations;

namespace AssignmentOauth2Server.Migrations
{
    public partial class InitDatabaseV17 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Default_Log_AccountLogsId",
                table: "Default");

            migrationBuilder.DropForeignKey(
                name: "FK_Mail_Log_AccountLogsId",
                table: "Mail");

            migrationBuilder.DropForeignKey(
                name: "FK_Mark_Log_AccountLogsId",
                table: "Mark");

            migrationBuilder.DropIndex(
                name: "IX_Mark_AccountLogsId",
                table: "Mark");

            migrationBuilder.DropIndex(
                name: "IX_Mail_AccountLogsId",
                table: "Mail");

            migrationBuilder.DropIndex(
                name: "IX_Default_AccountLogsId",
                table: "Default");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Mark_AccountLogsId",
                table: "Mark",
                column: "AccountLogsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mail_AccountLogsId",
                table: "Mail",
                column: "AccountLogsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Default_AccountLogsId",
                table: "Default",
                column: "AccountLogsId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Default_Log_AccountLogsId",
                table: "Default",
                column: "AccountLogsId",
                principalTable: "Log",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Mail_Log_AccountLogsId",
                table: "Mail",
                column: "AccountLogsId",
                principalTable: "Log",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Mark_Log_AccountLogsId",
                table: "Mark",
                column: "AccountLogsId",
                principalTable: "Log",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
