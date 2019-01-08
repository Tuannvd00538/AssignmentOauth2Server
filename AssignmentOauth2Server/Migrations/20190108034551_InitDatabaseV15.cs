using Microsoft.EntityFrameworkCore.Migrations;

namespace AssignmentOauth2Server.Migrations
{
    public partial class InitDatabaseV15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Log_AccountLogsDefault_DefaultId",
                table: "Log");

            migrationBuilder.DropForeignKey(
                name: "FK_Log_AccountLogsMail_MailId",
                table: "Log");

            migrationBuilder.DropForeignKey(
                name: "FK_Log_AccountLogsMark_MarkId",
                table: "Log");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountLogsMark",
                table: "AccountLogsMark");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountLogsMail",
                table: "AccountLogsMail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountLogsDefault",
                table: "AccountLogsDefault");

            migrationBuilder.RenameTable(
                name: "AccountLogsMark",
                newName: "Mark");

            migrationBuilder.RenameTable(
                name: "AccountLogsMail",
                newName: "Mail");

            migrationBuilder.RenameTable(
                name: "AccountLogsDefault",
                newName: "Default");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Mark",
                table: "Mark",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Mail",
                table: "Mail",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Default",
                table: "Default",
                column: "Id");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_Mark",
                table: "Mark");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Mail",
                table: "Mail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Default",
                table: "Default");

            migrationBuilder.RenameTable(
                name: "Mark",
                newName: "AccountLogsMark");

            migrationBuilder.RenameTable(
                name: "Mail",
                newName: "AccountLogsMail");

            migrationBuilder.RenameTable(
                name: "Default",
                newName: "AccountLogsDefault");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountLogsMark",
                table: "AccountLogsMark",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountLogsMail",
                table: "AccountLogsMail",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountLogsDefault",
                table: "AccountLogsDefault",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Log_AccountLogsDefault_DefaultId",
                table: "Log",
                column: "DefaultId",
                principalTable: "AccountLogsDefault",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Log_AccountLogsMail_MailId",
                table: "Log",
                column: "MailId",
                principalTable: "AccountLogsMail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Log_AccountLogsMark_MarkId",
                table: "Log",
                column: "MarkId",
                principalTable: "AccountLogsMark",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
