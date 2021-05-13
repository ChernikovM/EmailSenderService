using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class ErrorIdSetToNullableType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mails_Errors_ErrorId",
                table: "Mails");

            migrationBuilder.DropIndex(
                name: "IX_Mails_ErrorId",
                table: "Mails");

            migrationBuilder.AlterColumn<long>(
                name: "ErrorId",
                table: "Mails",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_Mails_ErrorId",
                table: "Mails",
                column: "ErrorId",
                unique: true,
                filter: "[ErrorId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Mails_Errors_ErrorId",
                table: "Mails",
                column: "ErrorId",
                principalTable: "Errors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mails_Errors_ErrorId",
                table: "Mails");

            migrationBuilder.DropIndex(
                name: "IX_Mails_ErrorId",
                table: "Mails");

            migrationBuilder.AlterColumn<long>(
                name: "ErrorId",
                table: "Mails",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mails_ErrorId",
                table: "Mails",
                column: "ErrorId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Mails_Errors_ErrorId",
                table: "Mails",
                column: "ErrorId",
                principalTable: "Errors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
