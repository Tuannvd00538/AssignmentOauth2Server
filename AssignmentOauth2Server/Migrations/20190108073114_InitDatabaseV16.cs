using Microsoft.EntityFrameworkCore.Migrations;

namespace AssignmentOauth2Server.Migrations
{
    public partial class InitDatabaseV16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Log_Default_DefaultId",
                table: "Log");

            migrationBuilder.DropForeignKey(
                name: "FK_Log_Mail_MailId",
                table: "Log");

            migrationBuilder.DropForeignKey(
                name: "FK_Log_Mark_MarkId",
                table: "Log");

            migrationBuilder.DropIndex(
                name: "IX_Log_DefaultId",
                table: "Log");

            migrationBuilder.DropIndex(
                name: "IX_Log_MailId",
                table: "Log");

            migrationBuilder.DropIndex(
                name: "IX_Log_MarkId",
                table: "Log");

            migrationBuilder.DropColumn(
                name: "DefaultId",
                table: "Log");

            migrationBuilder.DropColumn(
                name: "MailId",
                table: "Log");

            migrationBuilder.DropColumn(
                name: "MarkId",
                table: "Log");

            migrationBuilder.AddColumn<int>(
                name: "AccountLogsId",
                table: "Mark",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AccountLogsId",
                table: "Mail",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AccountLogsId",
                table: "Default",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "AccountLogsId",
                table: "Mark");

            migrationBuilder.DropColumn(
                name: "AccountLogsId",
                table: "Mail");

            migrationBuilder.DropColumn(
                name: "AccountLogsId",
                table: "Default");

            migrationBuilder.AddColumn<int>(
                name: "DefaultId",
                table: "Log",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MailId",
                table: "Log",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MarkId",
                table: "Log",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Log_DefaultId",
                table: "Log",
                column: "DefaultId");

            migrationBuilder.CreateIndex(
                name: "IX_Log_MailId",
                table: "Log",
                column: "MailId");

            migrationBuilder.CreateIndex(
                name: "IX_Log_MarkId",
                table: "Log",
                column: "MarkId");

            migrationBuilder.AddForeignKey(
                name: "FK_Log_Default_DefaultId",
                table: "Log",
                column: "DefaultId",
                principalTable: "Default",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Log_Mail_MailId",
                table: "Log",
                column: "MailId",
                principalTable: "Mail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Log_Mark_MarkId",
                table: "Log",
                column: "MarkId",
                principalTable: "Mark",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
